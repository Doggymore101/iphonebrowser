Imports System.Threading
Imports System.IO
Imports System.Drawing.Imaging
Imports Manzana
Imports SCW_iPhonePNG

Public Class frmMain
    Const STRING_ROOT As String = "[root]"
    Const IMAGE_FOLDER_CLOSED As Integer = 0
    Const IMAGE_FOLDER_OPEN As Integer = 1
    Const IMAGE_FILE_UNKNOWN As Integer = 0
    Const IMAGE_FILE_MUSIC As Integer = 1
    Const IMAGE_FILE_MOVIE As Integer = 2
    Const IMAGE_FILE_TEXT As Integer = 3
    Const IMAGE_FILE_IMAGE As Integer = 4
    Const IMAGE_FILE_AUDIO As Integer = 5
    Const IMAGE_FILE_DATABASE As Integer = 6
    Const IMAGE_FILE_RINGTONE As Integer = 7
    Const BACKUP_DIRECTORY As String = "BACKUPS"
    Const MAX_PROG_DEPTH As Integer = 1

    Private FILE_TEMPORARY_VIEWER As String = "iPhone.temp"
    Private APP_PATH As String = ""
    Private BackupPath As String = ""

    Private iPhoneInterface As iPhone

    Private bNowConnected As Boolean = False
    Private bConnectionChanged As Boolean = False
    Private bUpdateInProgress As Boolean = False
    Private bSupressFiles As Boolean = False
    Private wasQTpreview As Boolean = False
    Private QTpreviewFile As String
    Private prevSelectedFile As String = ""
    Private bConvertToiPhonePNG As Boolean, bConvertToPNG As Boolean
    Private bConfirmDeletions As Boolean, bDontBackupEver As Boolean, bDontBackupRun As Boolean
    Private bShowPreview As Boolean, bIgnoreThumbsFile As Boolean, bIgnoreDSStoreFile As Boolean
    Private bShowGroups As Boolean
    Private favNames As Specialized.StringCollection, favPaths As Specialized.StringCollection
    Private IsCollapsing As Boolean
    Private ProgressBars(MAX_PROG_DEPTH) As ToolStripProgressBar
    Private ProgressDepth As Integer
    Private tildeDir As String

    Private lstFilesSortOrder As SortOrder, lstFilesGroupOrder As SortOrder
    Private lstFilesSortCol As Integer

    ' import some functions
    Public Enum MessageBeepType
        [Default] = -1
        OK = &H0
        [Error] = &H10
        Question = &H20
        Warning = &H30
        Information = &H40
    End Enum

    ' This allows you to 'beep' using one of the sounds mapped using the
    ' sound mapper in control panel.
    <Runtime.InteropServices.DllImport("USER32.DLL", setlasterror:=True)> _
    Public Shared Function MessageBeep(ByVal type As MessageBeepType) As Boolean
    End Function

    'CUSTOM  CREATED FUNCTIONS

    Private Sub StatusNormal(ByVal msg As String)
        tlbStatusLabel.Image = Nothing
        tlbStatusLabel.Text = msg
    End Sub

    Private Sub StatusWarning(ByVal msg As String)
        tlbStatusLabel.Image = My.Resources.warning
        tlbStatusLabel.Text = msg
        MessageBeep(MessageBeepType.Warning)
    End Sub

    Private Delegate Sub NoParmDel()

    Public Sub DelayedConnectionChange()
        bNowConnected = Not bNowConnected
        bConnectionChanged = True
        connectionChange()
    End Sub

    Public Event iPhoneConnected()
    Public Event iPhoneDisconnected()

    Public Sub iPhoneConnected_Details() Handles Me.iPhoneConnected
        If Not bNowConnected Then
            Try
                If Me.InvokeRequired Then
                    Me.Invoke(New NoParmDel(AddressOf DelayedConnectionChange))
                Else
                    DelayedConnectionChange()
                End If
            Catch
            End Try
        End If
    End Sub

    Public Sub iPhoneDisconnected_Details() Handles Me.iPhoneDisconnected
        If bNowConnected Then
            Try
                If Me.InvokeRequired Then
                    Me.Invoke(New NoParmDel(AddressOf DelayedConnectionChange))
                Else
                    DelayedConnectionChange()
                End If
            Catch
            End Try
        End If
    End Sub

    Private Sub iPhoneConnected_EventHandler(ByVal sender As Object, ByVal args As ConnectEventArgs)
        RaiseEvent iPhoneConnected()
    End Sub

    Private Sub iPhoneDisconnected_EventHandler(ByVal sender As Object, ByVal args As ConnectEventArgs)
        RaiseEvent iPhoneDisconnected()
    End Sub

    Private Sub connectionChange()
        If bConnectionChanged Then
            bConnectionChanged = False

            trvFolders.Enabled = bNowConnected
            lstFiles.Enabled = bNowConnected
            txtFileDetails.Enabled = bNowConnected
            mnuGoTo.Enabled = bNowConnected
            mnuFavorites.Enabled = bNowConnected
            ToolStripMenuItemNewFolder.Enabled = bNowConnected
            ToolStripMenuItemDeleteFolder.Enabled = bNowConnected

            If bNowConnected Then
                'the phone was just recognized as connected
                refreshFolders()
                trvFolders.Focus()

                ' build the status text
                Dim deviceName As String = iPhoneInterface.DeviceName
                deviceName = IIf(String.IsNullOrEmpty(deviceName), String.Empty, " (" & deviceName & ")")
                Dim devStatus As String = String.Format("{0} {1}{2} is connected" & IIf(iPhoneInterface.IsJailbreak, " and jailbroken.", ", not jailbroken (no afc2 service found)"), iPhoneInterface.DeviceType, iPhoneInterface.DeviceVersion, deviceName)

                If iPhoneInterface.IsJailbreak Then
                    StatusNormal(devStatus)
                    If iPhoneInterface.Exists("/var/root/Media/DCIM") Then
                        tildeDir = "/var/root"
                    End If
                Else
                    StatusWarning(devStatus)
                End If
            Else
                StatusWarning("Device is NOT connected, please check your connections!")
            End If

        End If
    End Sub

    Private Sub startStatus(ByVal iMax As Integer)
        ProgressDepth = ProgressDepth + 1
        If ProgressDepth <= MAX_PROG_DEPTH Then
            If ProgressDepth = 0 Then ' first bar - turn all on
                For j1 As Integer = 0 To MAX_PROG_DEPTH
                    ProgressBars(j1).Visible = True
                    ProgressBars(j1).Value = 0
                Next
            End If
            ProgressBars(ProgressDepth).Maximum = iMax + 1
        End If
    End Sub
    Private Sub endStatus()
        If ProgressDepth <= MAX_PROG_DEPTH Then
            With ProgressBars(ProgressDepth)
                .Value = 0
            End With
            If ProgressDepth = 0 Then ' first bar - turn all off
                For j1 As Integer = 0 To MAX_PROG_DEPTH
                    ProgressBars(j1).Visible = False
                Next
            End If
        End If
        If ProgressDepth >= 0 Then
            ProgressDepth = ProgressDepth - 1
        End If
    End Sub
    Private Sub incrementStatus()
        Application.DoEvents() ' ensure UI continues when doing long processes
        If ProgressDepth <= MAX_PROG_DEPTH Then
            With ProgressBars(ProgressDepth)
                If .Value < .Maximum Then
                    .PerformStep()
                    tlbStatusStrip.Refresh()
                End If
            End With
        End If
    End Sub
    Private Sub refreshChildFolders(ByVal forceRefresh As Boolean)
        If Not bUpdateInProgress Then
            refreshChildFolders(trvFolders.SelectedNode, forceRefresh)
        End If
    End Sub
    Private Sub refreshChildFolders(ByVal rootNode As TreeNode, ByVal forceRefresh As Boolean)
        Dim sWorkingPath As String, iTemp As Integer, tmpNode As TreeNode

        If rootNode.Tag And Not forceRefresh Then
            Exit Sub
        End If

        If Not bUpdateInProgress Then
            bUpdateInProgress = True
            Me.Cursor = Cursors.WaitCursor
            trvFolders.SuspendLayout()

            'we need to go through all the children and refresh them
            StatusNormal("Refreshing folders for " & rootNode.Name & "...")
            startStatus(rootNode.Nodes.Count)
            iTemp = 0
            While iTemp < rootNode.Nodes.Count
                incrementStatus()

                tmpNode = rootNode.Nodes(iTemp)
                sWorkingPath = nodeiPhonePath(tmpNode)

                tmpNode.Nodes.Clear()
                'now add it (only if it is not the root)
                If sWorkingPath <> "/" Then
                    Try
                        addFolders(sWorkingPath, tmpNode, 1)
                    Catch
                        rootNode.Nodes(iTemp).Remove() ' someone deleted the folder
                    End Try
                End If
                iTemp = iTemp + 1
            End While
            endStatus()
            StatusNormal("")
            rootNode.Tag = True
            trvFolders.ResumeLayout()
            Me.Cursor = Cursors.Arrow
            bUpdateInProgress = False
        End If
    End Sub
    Private Function fileSizeAsString(ByVal sFilePath As String) As String
        Dim iFileSize As ULong = iPhoneInterface.FileSize(sFilePath)
        'make it look pretty
        If iFileSize > 10240000 Then
            Return Format(iFileSize / 1024000, "0.##") & " mb"
        ElseIf iFileSize > 10240 Then
            Return Format(iFileSize / 1024, "0.##") & " kb"
        Else
            Return iFileSize & " bytes"
        End If
    End Function
    Private Function nodeiPhonePath(ByRef aNode As TreeNode) As String
        Dim sTemp As String = aNode.FullPath
        If sTemp <> STRING_ROOT Then
            sTemp = Replace(Mid(sTemp, Len(STRING_ROOT) + 1), "\", "/")
        Else
            sTemp = "/"
        End If

        Return sTemp
    End Function
    Private Sub loadFiles()
        Dim sFiles() As String, iPhonePath As String
        Dim lstTemp As ListViewItem

        If Not bSupressFiles Then
            Application.UseWaitCursor = True
            'loads the files into the list view based on the current directory
            iPhonePath = nodeiPhonePath(trvFolders.SelectedNode)
            StatusNormal("Loading Files for " & iPhonePath & "...")
            Application.DoEvents()

            'first clear out any files that may be there
            lstFiles.Items.Clear()
            lstFiles.ListViewItemSorter = Nothing
            lstFilesSortOrder = SortOrder.None
            lstFilesGroupOrder = SortOrder.None
            lstFilesSortCol = -1 ' not being sorted
            lstFiles.ShowGroups = True ' bug in Windows - must add items when true

            lstFiles.BeginUpdate()
            'now get the files from the iphone
            sFiles = iPhoneInterface.GetFiles(iPhonePath)

            'now go one by one and add it
            startStatus(sFiles.Length)
            For Each sFile As String In sFiles
                If sFile = "." Or sFile = ".." Then
                    Continue For
                End If
                incrementStatus()

                lstTemp = New ListViewItem(sFile)
                lstTemp.ImageIndex = getImageIndexForFile(sFile)
                lstTemp.Group = lstFiles.Groups(lstTemp.ImageIndex)

                ' add the file size
                Dim fullPath As String
                If Microsoft.VisualBasic.Right(iPhonePath, 1) <> "/" Then
                    fullPath = iPhonePath & "/" & sFile
                Else
                    fullPath = iPhonePath & sFile
                End If
                lstTemp.SubItems.Add(fileSizeAsString(fullPath))

                ' add the file type
                lstTemp.SubItems.Add(getFiletype(lstTemp.ImageIndex))

                'finally add it to the view
                lstFiles.Items.Add(lstTemp)
            Next
            endStatus()
            StatusNormal("")

            lstFiles.ShowGroups = bShowGroups
            lstFiles.EndUpdate()

            ClearPreview()
            grpFiles.Text = "Files on your iPhone in the '" & nodeiPhonePath(trvFolders.SelectedNode) & "' Directory"
            Application.UseWaitCursor = False
        End If
    End Sub
    ''' <summary>
    ''' returns the image index for the given file's extension
    ''' </summary>
    ''' <param name="sFilename">file to lookup</param>
    ''' <returns>image index</returns>
    Private Function getImageIndexForFile(ByVal sFilename As String) As Integer
        Dim iReturn As Integer = IMAGE_FILE_UNKNOWN

        sFilename = Mid(LCase(sFilename), Strings.InStrRev(sFilename, "\") + 1)
        Dim sExt As String = Mid(sFilename, Strings.InStrRev(sFilename, ".") + 1)

        Select Case sExt
            Case "png", "jpg", "jpeg", "gif"
                iReturn = IMAGE_FILE_IMAGE
            Case "strings", "conf", "txt", "plist", "script", "html", "css", "js"
                iReturn = IMAGE_FILE_TEXT
            Case "db", "sqlite"
                iReturn = IMAGE_FILE_DATABASE
            Case "aiff", "amr", "aif", "caf"
                iReturn = IMAGE_FILE_AUDIO
            Case "m4r"
                iReturn = IMAGE_FILE_RINGTONE
            Case "m4a", "m4p", "mp3", "aac"
                iReturn = IMAGE_FILE_MUSIC
        End Select

        Return iReturn
    End Function

    Private Function getFiletype(ByVal iImageIndex As Integer) As String
        'gets the string file type given the image index
        Dim sReturn As String = "Undefined File Type"

        Select Case iImageIndex
            Case IMAGE_FILE_RINGTONE
                sReturn = "Ringtone"
            Case IMAGE_FILE_AUDIO
                sReturn = "Audio File"
            Case IMAGE_FILE_DATABASE
                sReturn = "Database"
            Case IMAGE_FILE_MOVIE
                sReturn = "Movie File"
            Case IMAGE_FILE_MUSIC
                sReturn = "Music File"
            Case IMAGE_FILE_TEXT
                sReturn = "Text File"
            Case IMAGE_FILE_IMAGE
                sReturn = "Image File"
            Case IMAGE_FILE_UNKNOWN
                sReturn = "Unknown File Type"
            Case Else
                'including image_file_unkonwn

        End Select

        getFiletype = sReturn
    End Function

    Private Sub refreshFolders()
        Dim rootNode As TreeNode

        Application.UseWaitCursor = True
        trvFolders.BeginUpdate()

        startStatus(3)
        trvFolders.Nodes.Clear()
        rootNode = New TreeNode(STRING_ROOT)
        rootNode.ContextMenuStrip = menuRightClickFolders
        rootNode.Name = "/"
        trvFolders.Nodes.Add(rootNode)
        incrementStatus()

        Try
            addFolders("", rootNode)
        Catch ex As Exception
            ' ignore all exceptions
            Exit Try
        End Try
        incrementStatus()

        rootNode.Expand()
        trvFolders.SelectedNode = rootNode
        incrementStatus()

        trvFolders.Sort()
        trvFolders.EndUpdate()
        endStatus()

        Application.UseWaitCursor = False
    End Sub
    Private Sub addFoldersBeneath(ByRef aNode As TreeNode)
        addFolders(nodeiPhonePath(aNode), aNode)
    End Sub
    Private Sub addFolders(ByVal sPath As String, ByRef selectedNode As TreeNode, Optional ByVal iDepth As Integer = 0)
        'This function is recursive to add one level of folders to the tree view
        ' you give it one folder and will drill down and add one level of folders
        Dim sFolders() As String, newNode As TreeNode

        If sPath = "/" Then ' handle root special case
            sPath = ""
        End If

        'get the data from the phone
        sFolders = iPhoneInterface.GetDirectories(sPath)

        startStatus(sFolders.Length)

        selectedNode.Nodes.Clear() ' remove any existing nodes

        For Each sFolder As String In sFolders
            'create the new node
            newNode = New TreeNode(sFolder)
            newNode.Name = sPath & "/" & sFolder
            newNode.ContextMenuStrip = menuRightClickFolders

            selectedNode.Nodes.Add(newNode)

            'now make the recursive call on this folder
            If iDepth < 1 Then ' only load first tree level beneath
                addFolders(sPath & "/" & sFolder, newNode, iDepth + 1)
            End If

            incrementStatus()
        Next
        selectedNode.Tag = False
        endStatus()
    End Sub

    Private Function CopyFromPhoneMakePNG(ByVal sPhone As String, ByVal dComputer As String)
        Dim tmpOnPC As String = getTempFilename(sPhone)
        Dim ans As Boolean = copyFromPhone(sPhone, tmpOnPC)
        If ans Then
            Try
                Dim cvtImage As Image = iPhonePNG.ImageFromFile(tmpOnPC)
                cvtImage.Save(dComputer, ImageFormat.Png)
            Catch
                ans = copyFromPhone(sPhone, dComputer)
            End Try
        End If

        Return ans
    End Function

    Private Function CopyFromPhonePNG(ByVal sPhone As String, ByVal dComputer As String)
        If bConvertToPNG And LCase(sPhone).EndsWith(".png") Then
            Dim tmpOnPC As String = getTempFilename(sPhone)
            Dim ans As Boolean = copyFromPhone(sPhone, tmpOnPC)
            If ans Then
                Try
                    Dim cvtImage As Image = iPhonePNG.ImageFromFile(tmpOnPC)
                    cvtImage.Save(dComputer, ImageFormat.Png)
                Catch
                    ans = copyFromPhone(sPhone, dComputer)
                End Try
                Kill(tmpOnPC)
            End If

            Return ans
        Else
            Return copyFromPhone(sPhone, dComputer)
        End If
    End Function

    Private Function copyFromPhone(ByVal sourceOnPhone As String, ByVal destinationOnComputer As String) As Boolean
        Dim sBuffer(8191) As Byte, iDataBytes As Integer
        Dim iPhoneFileInterface As iPhoneFile
        Dim fileTemp As FileStream
        Dim bReturn As Boolean = False

        'remove our local file if it exists
        If File.Exists(destinationOnComputer) Then
            Try
                Kill(destinationOnComputer)
            Catch
                Exit Function
            End Try
        End If

        'make sure the source file exists
        If iPhoneInterface.Exists(sourceOnPhone) Then
            startStatus(iPhoneInterface.FileSize(sourceOnPhone) / sBuffer.Length) 'show our progress bar

            Try
                'open a connection to the file and read it
                iPhoneFileInterface = iPhoneFile.OpenRead(iPhoneInterface, sourceOnPhone)

                fileTemp = File.OpenWrite(destinationOnComputer)
                iDataBytes = iPhoneFileInterface.Read(sBuffer, 0, sBuffer.Length)
                While iDataBytes > 0
                    fileTemp.Write(sBuffer, 0, iDataBytes)
                    iDataBytes = iPhoneFileInterface.Read(sBuffer, 0, sBuffer.Length)
                    incrementStatus() 'increment our progressbar
                End While

                iPhoneFileInterface.Close()
                fileTemp.Close()

                bReturn = True
            Catch Ex As Exception
                bReturn = False
            Finally
                endStatus()
            End Try
        End If

        copyFromPhone = bReturn
    End Function
    Private Function copy1ToPhonePNGAt(ByVal sComputer As String, ByVal dPhone As String, ByVal aBackupTime As Date)
        Dim ans As Boolean = True

        If LCase(sComputer).EndsWith(".png") Then
            Dim tmpOnPC As String = Path.GetTempFileName
            Try
                Dim cvtImage As Image = iPhonePNG.ImageFromFile(sComputer)
                iPhonePNG.Save(cvtImage, tmpOnPC)
            Catch
                ans = False
            End Try

            If ans Then
                If dPhone.EndsWith("/") Then
                    dPhone = dPhone & Path.GetFileName(sComputer)
                End If
                ans = copyToPhoneAt(tmpOnPC, dPhone, False, aBackupTime)
                Kill(tmpOnPC)
            Else
                ans = copyToPhoneAt(sComputer, dPhone, False, aBackupTime)
            End If
        Else
            ans = copyToPhoneAt(sComputer, dPhone, False, aBackupTime)
        End If

        Return ans
    End Function

    Private Function copy1ToPhoneAt(ByVal sPC As String, ByVal dPhone As String, ByVal aBackupTime As Date)
        Dim iPhoneFileInterface As iPhoneFile
        Dim sPath As String, sFile As String
        Dim fileTemp As FileStream
        Dim sBuffer(8191) As Byte, iDataBytes As Integer

        If bIgnoreThumbsFile And Path.GetFileName(sPC) = "Thumbs.db" Then
            Return True ' pretend copying was ok, but don't copy thumbs
        End If

        If bIgnoreDSStoreFile And Path.GetFileName(sPC) = ".DS_Store" Then
            Return True ' pretend copying was ok, but don't copy .DS_Store files
        End If

        'see if the destination file exists
        If iPhoneInterface.Exists(dPhone) Then
            'it exists, back it up before overwriting
            sPath = Microsoft.VisualBasic.Left(dPhone, InStrRev(dPhone, "/"))
            sFile = Mid(dPhone, InStrRev(dPhone, "/") + 1)
            If Not bDontBackupRun Then
                backupFileFromPhoneAt(sPath, sFile, aBackupTime)
            End If
        End If

        'open a connection to the file and write it
        iPhoneFileInterface = iPhoneFile.OpenWrite(iPhoneInterface, dPhone)
        'open a connection locally for read
        fileTemp = File.OpenRead(sPC)
        startStatus(fileTemp.Length / sBuffer.Length) 'show our progress bar

        iDataBytes = fileTemp.Read(sBuffer, 0, sBuffer.Length)
        While iDataBytes > 0
            incrementStatus() 'increment our progressbar
            iPhoneFileInterface.Write(sBuffer, 0, iDataBytes)
            iDataBytes = fileTemp.Read(sBuffer, 0, sBuffer.Length)
        End While
        endStatus() 'fill our progressbar
        iPhoneFileInterface.Close()
        fileTemp.Close()

        Return True
    End Function

    Private Function copyToPhone(ByVal sourceOnComputer As String, ByVal destinationOnPhone As String, ByVal fixPNG As Boolean) As Boolean
        Me.Cursor = Cursors.WaitCursor
        Dim ans As Boolean = copyToPhoneAt(sourceOnComputer, destinationOnPhone, fixPNG, Now)
        Me.Cursor = Cursors.Arrow
    End Function

    Private Function copyToPhoneAt(ByVal sourceOnComputer As String, ByVal destinationOnPhone As String, ByVal fixPNG As Boolean, ByVal aBackupTime As Date) As Boolean
        Dim bReturn As Boolean = False
        Dim sPath As String, sFile As String, dPath As String

        'get the details out
        sPath = Path.GetDirectoryName(sourceOnComputer)
        sFile = Path.GetFileName(sourceOnComputer)

        If destinationOnPhone.EndsWith("/") Then
            'they did not pass a specific file name, so add the source filename
            destinationOnPhone = destinationOnPhone & UnfixPhoneFilename(sFile)
        End If

        'update the status bar
        StatusNormal("Copying '" & sourceOnComputer & "'...")

        'are we copying a file?
        If File.Exists(sourceOnComputer) Then
            If fixPNG Then
                bReturn = copy1ToPhonePNGAt(sourceOnComputer, destinationOnPhone, aBackupTime)
            Else
                bReturn = copy1ToPhoneAt(sourceOnComputer, destinationOnPhone, aBackupTime)
            End If
        ElseIf Directory.Exists(sourceOnComputer) Then
            ' Create matching directory on phone
            If Not iPhoneInterface.Exists(destinationOnPhone) Then
                iPhoneInterface.CreateDirectory(destinationOnPhone)
            End If

            dPath = destinationOnPhone & "/"

            ' copy all files over recursively
            For Each filepath As String In Directory.GetFiles(sourceOnComputer)
                'Application.DoEvents() ' make sure screen updates
                copyToPhoneAt(filepath, dPath, fixPNG, aBackupTime)
            Next

            ' copy all directories over recursively
            For Each dirpath As String In Directory.GetDirectories(sourceOnComputer)
                copyToPhoneAt(dirpath, dPath, fixPNG, aBackupTime)
            Next

            ' update TreeView
            addFoldersBeneath(trvFolders.SelectedNode)

            bReturn = True
        End If

        copyToPhoneAt = bReturn
    End Function

    Private Sub backupFileFromPhoneAt(ByVal sSourcePath As String, ByVal sSourceFile As String, ByVal aTime As Date)
        'copies a file from the phone and backs it up in the appropriate directory
        'grab the file then save it with an extra extension
        Dim sDestinationPath As String, sDestinationFile As String

        StatusNormal("Backing up '" & sSourceFile & "'.")
        'make sure it ends in a /
        If Not sSourcePath.EndsWith("/") Then sSourcePath = sSourcePath & "/"

        sDestinationFile = FixPhoneFilename(sSourceFile)
        sDestinationPath = BackupPath & Replace(sSourcePath, "/", "\")

        'Create the directory if it does not already exist
        If Not Directory.Exists(sDestinationPath) Then
            Directory.CreateDirectory(sDestinationPath)
        Else
            ' make sure there isn't a file conflict - create a new backup dir if necessary
            If File.Exists(sDestinationPath & sDestinationFile) Then
                SetBackupPath(aTime)
                sDestinationPath = BackupPath & Replace(sSourcePath, "/", "\")
                Directory.CreateDirectory(sDestinationPath)
            End If
        End If

        'copy file into the backup directory
        copyFromPhone(sSourcePath & sSourceFile, sDestinationPath & sDestinationFile)

        StatusNormal("Backed up as '" & sDestinationFile & "'.")
    End Sub

    Private Sub backupFileFromPhone(ByVal sSourcePath As String, ByVal sSourceFile As String)
        backupFileFromPhoneAt(sSourcePath, sSourceFile, Now)
    End Sub

    Private Sub BackupDirectoryAt(ByVal sPath As String, ByVal aTime As Date)
        For Each sFile As String In iPhoneInterface.GetFiles(sPath)
            backupFileFromPhoneAt(sPath, sFile, aTime)
        Next
        For Each sDir As String In iPhoneInterface.GetDirectories(sPath)
            BackupDirectoryAt(sPath & "/" & sDir, aTime)
        Next
    End Sub

    Private Sub BackupDirectory(ByVal sPath As String)
        BackupDirectoryAt(sPath, Now)
    End Sub
    Private Function PhoneGetDirectoryName(ByVal phonePath As String) As String
        Return Microsoft.VisualBasic.Left(phonePath, InStrRev(phonePath, "/") - 1)
    End Function
    ' just use Path.GetFileName instead...
    '    Private Function PhoneGetFileName(ByVal phonePath As String) As String
    '        Return Mid(phonePath, InStrRev(phonePath, "/") + 1)
    '    End Function
    Private Function delFromPhone(ByVal sourceOnPhone As String) As Boolean
        Dim sPath As String, sFile As String
        Dim bReturn As Boolean = False

        'make sure the source file exists
        If iPhoneInterface.Exists(sourceOnPhone) Then
            sPath = PhoneGetDirectoryName(sourceOnPhone)
            sFile = Path.GetFileName(sourceOnPhone)
            If Not bDontBackupRun Then
                backupFileFromPhone(sPath, sFile)
            End If

            iPhoneInterface.DeleteFile(sourceOnPhone)

            bReturn = True
        End If

        delFromPhone = bReturn
    End Function

    Private Function FileCompare(ByVal fileName1 As String, ByVal fileName2 As String) As Boolean
        ' This method accepts two strings that represent two files to compare.
        Dim file1byte As Integer, file2byte As Integer
        Dim fs1 As FileStream, fs2 As FileStream

        ' Determine if the same file was referenced two times.
        If (fileName1 = fileName2) Then
            Return True
        End If

        ' Open the two files.
        fs1 = New FileStream(fileName1, FileMode.Open)
        fs2 = New FileStream(fileName2, FileMode.Open)

        ' Check the file sizes. If they are not the same, the files
        ' are not equal.
        If (fs1.Length <> fs2.Length) Then
            ' Close the file
            fs1.Close()
            fs2.Close()

            Return False
        End If

        ' Read and compare a byte from each file until either a
        ' non-matching set of bytes is found or until the end of the
        ' files is reached.
        Do
            ' Read one byte from each file.
            file1byte = fs1.ReadByte()
            file2byte = fs2.ReadByte()
        Loop While (file1byte = file2byte) And (file1byte <> -1)

        ' Close the files.
        fs1.Close()
        fs2.Close()

        ' Return the success of the comparison. "file1byte" is
        ' equal to "file2byte" at this point only if the files are 
        ' the same.
        Return (file1byte = file2byte)
    End Function

    Private Function getSelectedFilename() As String
        'returns the currently selected filename
        Return getSelectedPath() & lstFiles.SelectedItems(0).Text
    End Function
    Private Function getSelectedFolder() As String
        Return Mid(trvFolders.SelectedNode.FullPath, Len(STRING_ROOT) + 1)
    End Function
    Private Function getSelectedPath() As String
        'returns the currently selected folder
        Return getSelectedFolder() & "/"
    End Function
    ' courtesy Greg Martin
    Private Function CountStr(ByVal InStr As String, ByVal MatchString As String) As Integer
        Try
            Dim SourceString As String = InStr
            Dim StringExpr As New System.Text.RegularExpressions.Regex(MatchString)
            Return CStr(StringExpr.Matches(SourceString).Count)
        Catch
            Return -1
        End Try
    End Function
    Private Function selectSpecificPath(ByVal sPathOnPhone As String) As Boolean
        Return selectSpecificPath(sPathOnPhone, False)
    End Function
    Private Function selectSpecificPath(ByVal sPathOnPhone As String, ByVal forceRefresh As Boolean) As Boolean

        'selects a specifc path in the tree view
        Dim sTemp As String, iNode As Integer, tn() As TreeNode, bReturn As Boolean = False

        bSupressFiles = True 'so we don't load files until the end

        ' drop trailing /
        If Microsoft.VisualBasic.Right(sPathOnPhone, 1) = "/" Then
            sPathOnPhone = Microsoft.VisualBasic.Left(sPathOnPhone, Len(sPathOnPhone) - 1)
        End If
        'first, lets try to find it without expanding
        tn = trvFolders.Nodes.Find(sPathOnPhone, True)
        If tn.Length = 0 Then ' we couldn't find it
            startStatus(CountStr(sPathOnPhone, "/"))

            'select the root first
            tn = trvFolders.Nodes.Find("/", True)

            'go through and load each node
            iNode = 2
            Do While InStr(iNode, sPathOnPhone, "/") > 0
                'pull out the full path to the next node
                sTemp = Microsoft.VisualBasic.Left(sPathOnPhone, InStr(iNode, sPathOnPhone, "/") - 1)
                iNode = InStr(iNode, sPathOnPhone, "/") + 1

                tn = trvFolders.Nodes.Find(sTemp, True)
                If tn.Length > 0 Then
                    refreshChildFolders(tn(0), forceRefresh)
                End If
                incrementStatus()
            Loop

            'now it should definitely be available
            bSupressFiles = False
            tn = trvFolders.Nodes.Find(sPathOnPhone, True)
            If tn.Length > 0 Then
                tn(0).EnsureVisible()
                trvFolders.SelectedNode = tn(0)
                trvFolders.Focus()
                bReturn = True
            Else
                'we couldn't find it
                bReturn = False

                ' update files display with our partial location
                loadFiles()
            End If
            endStatus()
        Else
            'we found it first try
            bSupressFiles = False
            tn(0).EnsureVisible()
            trvFolders.SelectedNode = tn(0)
            trvFolders.Focus()
            bReturn = True
        End If

        Return bReturn
    End Function

    Private Sub SetBackupPath(ByVal aTime As Date)
        BackupPath = APP_PATH & BACKUP_DIRECTORY & Format(aTime, ".yyyyMMdd.HHmmss")
    End Sub
    'SYSTEM CREATED EVENTS

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrorHandler

        ProgressBars(0) = tlbProgress0
        tlbProgress0.Visible = False
        ProgressBars(1) = tlbProgressBar
        tlbProgressBar.Visible = False
        ProgressDepth = -1

        APP_PATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\Cranium\iPhoneBrowser\"
        SetBackupPath(Now)

        FILE_TEMPORARY_VIEWER = Path.GetTempPath & FILE_TEMPORARY_VIEWER

        Me.Text = "iPhoneBrowser (v" & Application.ProductVersion & ")"

        ' initialize the file list groups
        For j1 As Integer = 0 To 7
            Dim lvg As ListViewGroup = New ListViewGroup(getFiletype(j1))
            lstFiles.Groups.Add(lvg)
        Next

        ' load up the user settings
        If My.Settings.CallUpgrade Then
            My.Settings.Upgrade()
            My.Settings.CallUpgrade = False
            My.Settings.Save()
        End If
        bConfirmDeletions = My.Settings.ConfirmDeletions
        bConvertToiPhonePNG = My.Settings.PCToiPhonePNG
        bConvertToPNG = My.Settings.iPhoneToPCPNG
        bShowPreview = My.Settings.ShowPreviews
        bIgnoreThumbsFile = My.Settings.IgnoreThumbsFile
        bIgnoreDSStoreFile = My.Settings.IgnoreDSStoreFile
        bDontBackupEver = My.Settings.DontBackupEver
        bDontBackupRun = bDontBackupEver
        bShowGroups = My.Settings.ShowGroups

        favNames = My.Settings.FavNames
        If favNames Is Nothing Then
            favNames = New Specialized.StringCollection()
        End If

        favPaths = My.Settings.FavPaths
        If favPaths Is Nothing Then
            favPaths = New Specialized.StringCollection()
        End If

        LoadFavoritesMenu()

        ' setup the tooltips
        menuSetTooltips(mnuGoTo)
        menuSetTooltips(mnuStdApps)
        menuSetTooltips(mnuThirdPartyApps)

        tildeDir = "/var/mobile"

        iPhoneInterface = New iPhone(AddressOf iPhoneConnected_EventHandler, AddressOf iPhoneDisconnected_EventHandler)

        Exit Sub

ErrorHandler:
        MsgBox("An error has occured during load.  Error text: " & Err.Description & ".  The program will now exit, sorry. Please report this online.", MsgBoxStyle.Critical, "Error!")
        Me.Close()
    End Sub
    Private Sub SetViewChecks()
        ToolStripMenuItemLargeIcons.Checked = (lstFiles.View = View.LargeIcon)
        ToolStripMenuItemDetails.Checked = (lstFiles.View = View.Details)
        cmdSmallIcons.Checked = (lstFiles.View = View.SmallIcon)
    End Sub
    Private Sub ToolStripMenuItemLargeIcons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemLargeIcons.Click
        lstFiles.View = View.LargeIcon
        SetViewChecks()
    End Sub

    Private Sub ToolStripMenuItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemDetails.Click
        lstFiles.View = View.Details
        SetViewChecks()
    End Sub

    Private Sub cmdSmallIcons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSmallIcons.Click
        lstFiles.View = View.SmallIcon
        SetViewChecks()
    End Sub

    Private Sub ToolStripMenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemExit.Click
        'time to go
        Me.Close()
    End Sub

    Private Sub trvFolders_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFolders.AfterExpand
        'trvFolders.SelectedNode = e.Node
        refreshChildFolders(e.Node, False)
    End Sub

    Private Sub trvFolders_BeforeCollapse(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvFolders.BeforeCollapse
        IsCollapsing = True
    End Sub

    Private Sub trvFolders_AfterCollapse(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFolders.AfterCollapse
        IsCollapsing = False
    End Sub

    Private Sub trvFolders_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFolders.AfterSelect
        'refreshChildFolders(e.Node, False)
        If Not IsCollapsing And Not trvFolders.SelectedNode.IsExpanded() Then
            trvFolders.SelectedNode.Expand()
        End If
        Try
            loadFiles()
        Catch ' ignore all errors
            Exit Try
        End Try
    End Sub

    Private Sub lstFiles_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstFiles.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim initFolder As String = getSelectedPath()
            Dim drops() As String = e.Data.GetData(DataFormats.FileDrop)
            startStatus(drops.Length)
            For Each s As String In e.Data.GetData(DataFormats.FileDrop)
                copyToPhone(s, initFolder, bConvertToiPhonePNG)
                incrementStatus()
            Next
            endStatus()
            StatusNormal("")

            selectSpecificPath(initFolder) ' fix up tree view
            loadFiles() ' refresh the list view
        End If
    End Sub

    Private Sub lstFiles_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstFiles.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub
    Private Function FixPhoneFilename(ByVal aName As String) As String
        Dim ans As String = aName
        For Each c As Char In "%:/\*?<>|"""
            ans = Replace(ans, c, "%" & Hex(Asc(c)))
        Next

        Return ans
    End Function
    Private Function UnfixPhoneFilename(ByVal aName As String) As String
        Dim ans As String = aName
        For Each c As Char In ":/\*?<>|""%"
            ans = Replace(ans, "%" & Hex(Asc(c)), c)
        Next

        Return ans
    End Function
    Private Function getTempFilename(ByVal sFile As String) As String
        'grab the extension
        Dim sTemp As String = Mid(sFile, InStrRev(sFile, "/") + 1)
        'so we skip over the files without extensions but inside folders with extensions
        If InStr(sTemp, ".") > 0 Then
            sTemp = Mid(sFile, InStrRev(sFile, "."))
        Else
            sTemp = ".temp"
        End If

        sTemp = FixPhoneFilename(sTemp)

        Return FILE_TEMPORARY_VIEWER & sTemp
    End Function
    Private Sub unlockQT()
        'unlock the file so we can load another
        If wasQTpreview Then
            qtPlugin.FileName = "" 'to unlock it
            Kill(Replace(QTpreviewFile, "localhost\", ""))
            wasQTpreview = False
        End If
    End Sub
    Private Sub ClearPreview()
        unlockQT()

        picFileDetails.Visible = False
        txtFileDetails.Visible = False
        qtPlugin.Visible = False

        btnPreview.Enabled = True
    End Sub
    Private Sub ShowFileDetails(ByVal sFile As String)
        txtFileDetails.Text = "Filename: " & Path.GetFileName(sFile) & vbCrLf & "Size: " & fileSizeAsString(sFile)

        txtFileDetails.Visible = True
    End Sub
    Private Sub ShowPreview(ByVal sFile As String)
        ' don't preview links
        If iPhoneInterface.IsLink(sFile) Then
            StatusWarning("Unable to preview symbolic link: " & sFile)
            Exit Sub
        End If

        Dim sr As StreamReader, picOK As Boolean

        StatusNormal("Loading file " & sFile)

        Dim tmpOnPC As String = getTempFilename(sFile)

        If copyFromPhone(sFile, tmpOnPC) Then
            btnPreview.Enabled = False

            grpDetails.Text = "File details for '" & sFile & "'."
            txtFileDetails.Text = "<UNKNOWN FILE FORMAT>"

            'now we have a temporary file, lets try to read it
            Select Case lstFiles.SelectedItems(0).ImageIndex
                Case IMAGE_FILE_TEXT, IMAGE_FILE_DATABASE

                    'clean up the temp file
                    sr = My.Computer.FileSystem.OpenTextFileReader(tmpOnPC)
                    txtFileDetails.Text = Replace(sr.ReadToEnd(), Chr(10), vbCrLf)
                    sr.Close()

                    txtFileDetails.Visible = True

                Case IMAGE_FILE_UNKNOWN, IMAGE_FILE_RINGTONE
                    ShowFileDetails(sFile)

                Case IMAGE_FILE_IMAGE
                    Try
                        picOK = True
                        picFileDetails.Image = iPhonePNG.ImageFromFile(tmpOnPC)
                    Catch
                        picOK = False
                    End Try
                    If picOK Then
                        If Not picFileDetails.Image Is Nothing Then
                            If picFileDetails.Image.Width > picFileDetails.Width Or picFileDetails.Image.Height > picFileDetails.Height Then
                                picFileDetails.SizeMode = PictureBoxSizeMode.Zoom
                            Else
                                picFileDetails.SizeMode = PictureBoxSizeMode.CenterImage
                            End If
                        End If
                        picFileDetails.Visible = True
                    End If

                Case IMAGE_FILE_AUDIO, IMAGE_FILE_MOVIE, IMAGE_FILE_MUSIC
                    QTpreviewFile = tmpOnPC
                    qtPlugin.FileName = QTpreviewFile
                    qtPlugin.AutoPlay = True
                    qtPlugin.Visible = True
                    wasQTpreview = True

            End Select
            StatusNormal("")
        Else
            'it didn't copy from the phone correctly
            StatusWarning("The program was unable to copy " & sFile & " from the iPhone.  Sorry, try again!")
        End If
    End Sub
    Private Sub DoFileSelectedPreview(ByVal anySize As Boolean)
        If lstFiles.SelectedItems.Count > 0 Then
            Dim sFile As String = getSelectedFilename()

            If prevSelectedFile = sFile Then ' make sure we changed selections (handles multi-select better)
                Exit Sub
            End If
            prevSelectedFile = sFile

            ClearPreview()

            ' only if it is less than a big file size
            Dim sSize As String = lstFiles.SelectedItems(0).SubItems(1).Text
            If (sSize.EndsWith("kb") And Val(sSize) < 100) Or sSize.EndsWith("bytes") Or anySize Then
                If sSize <> "0 bytes" Then
                    ShowPreview(sFile)
                Else
                    ShowFileDetails(sFile)
                    StatusNormal("The file " & sFile & " is 0 bytes in length")
                    btnPreview.Enabled = False
                End If
            Else
                'the file is too big to auto-preview
                ShowFileDetails(sFile)
                'StatusWarning("The file " & sFile & " is too large to be previewed")
                btnPreview.Enabled = True
            End If
        End If
    End Sub
    Private Sub ShowSelectedFileDetails()
        If lstFiles.SelectedItems.Count > 0 Then
            Dim sFile As String = getSelectedFilename()

            If prevSelectedFile = sFile Then ' make sure we changed selections (handles multi-selecte better)
                Exit Sub
            End If
            prevSelectedFile = sFile

            ClearPreview()

            ShowFileDetails(sFile)
        End If
    End Sub
    Private Sub lstFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        If bShowPreview Then
            DoFileSelectedPreview(False)
        Else
            ShowSelectedFileDetails()
        End If
    End Sub

    Private Sub menuRightSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightSaveAs.Click
        Dim sSaveAsFilename As String, sFolder As String, sFileFromPhone As String

        If lstFiles.SelectedItems.Count = 1 Then
            sFolder = getSelectedPath()
            Dim sItem As ListViewItem = lstFiles.SelectedItems(0)
            'show them the save dialog
            fileSaveDialog.Title = "Save " & sItem.Text & " as ..."
            fileSaveDialog.FileName = FixPhoneFilename(sItem.Text)
            If fileSaveDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                sSaveAsFilename = fileSaveDialog.FileName
                sFileFromPhone = sFolder & sItem.Text

                StatusNormal("Saving " & sItem.Text & "...")
                CopyFromPhonePNG(sFileFromPhone, sSaveAsFilename)
            End If
        Else
            Dim dFolder As String

            sFolder = getSelectedPath()
            If folderBrowserDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                dFolder = folderBrowserDialog.SelectedPath & "\"
                For Each sItem As ListViewItem In lstFiles.SelectedItems
                    'show them the save dialog
                    sSaveAsFilename = dFolder & sItem.Text
                    sFileFromPhone = sFolder & sItem.Text

                    StatusNormal("Saving " & sItem.Text & "...")
                    CopyFromPhonePNG(sFileFromPhone, sSaveAsFilename)
                Next
            End If
        End If
    End Sub

    Private Sub menuRightBackupFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightBackupFile.Click
        Dim sSourcePath As String

        If lstFiles.SelectedItems.Count > 0 Then
            Application.UseWaitCursor = True
            sSourcePath = getSelectedPath()
            Dim aTime As Date = Now

            For Each sItem As ListViewItem In lstFiles.SelectedItems
                backupFileFromPhoneAt(sSourcePath, sItem.Text, aTime)
            Next
            Application.UseWaitCursor = False
        End If

    End Sub

    Private Sub menuRightReplaceFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightReplaceFile.Click
        'replace the selected file with one of their own
        Dim sSourceFilename As String, sFileToPhone As String

        If lstFiles.SelectedItems.Count = 1 Then
            'show them the open dialog
            fileOpenDialog.Title = "Select a file to replace " & lstFiles.SelectedItems(0).Text & " with ..."
            fileOpenDialog.FileName = lstFiles.SelectedItems(0).Text
            If fileOpenDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                sSourceFilename = fileOpenDialog.FileName
                'replace the selected file with the source one
                sFileToPhone = getSelectedFilename()
                'this function also makes a backup
                copyToPhone(sSourceFilename, sFileToPhone, bConvertToiPhonePNG)
                'refresh the list view
                loadFiles()
            End If
        Else
            MsgBox("Only one file can be replaced at a time", MsgBoxStyle.Exclamation, "Selection error")
        End If

    End Sub

    Private Sub menuRightDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightDeleteFile.Click
        'delete the selected file
        Dim sFolder As String, sDeleteFilename As String, okDel As Boolean

        If lstFiles.SelectedItems.Count > 0 Then
            sFolder = getSelectedPath()
            For Each sItem As ListViewItem In lstFiles.SelectedItems
                sDeleteFilename = sFolder & sItem.Text
                okDel = True
                If bConfirmDeletions Then
                    'Make them confirm it
                    Dim ans As MsgBoxResult
                    ans = MsgBox("Are you sure you wish to delete the '" & sDeleteFilename & "' file? (A backup copy will automatically be created)", MsgBoxStyle.YesNoCancel, "Delete file?")
                    If ans = MsgBoxResult.No Then
                        okDel = False
                    ElseIf ans = MsgBoxResult.Cancel Then
                        Exit For
                    End If
                End If
                If okDel Then
                    delFromPhone(sDeleteFilename)
                End If
            Next

            'refresh the list view
            loadFiles()
        End If

    End Sub

    Private Sub ToolStripMenuItemCleanUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemCleanUp.Click
        'this function goes through the backup files and cleans them, comparing them to others in the same folder with the same name
        Dim sMessage As String

        sMessage = "This function goes through all of your backup files and attempts to remove the duplicates to save space." & vbCrLf & _
            "You really don't have to do this, it is better to have more backups then less...but press ok if you've got a lot of backups."

        If MsgBox(sMessage, MsgBoxStyle.OkCancel, "Confirm cleanup") = MsgBoxResult.Ok Then
            MsgBox("Function not implemented yet, sorry :)", MsgBoxStyle.OkOnly, "Whoops")
        End If
    End Sub

    Private Sub ToolStripMenuItemViewBackups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemViewBackups.Click
        Shell("explorer """ & APP_PATH & """", AppWinStyle.NormalFocus)
    End Sub

    Private Sub toolStripGoTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles toolStripGoTo1.Click, toolStripGoTo2.Click, toolStripGoTo3.Click, _
        toolStripGoTo19.Click, toolStripGoTo9.Click, toolStripGoTo8.Click, _
        toolStripGoTo7.Click, toolStripGoTo6.Click, toolStripGoTo5.Click, toolStripGoTo4.Click, _
        toolStripGoTo18.Click, toolStripGoTo17.Click, toolStripGoTo16.Click, toolStripGoTo15.Click, _
        toolStripGoTo14.Click, toolStripGoTo13.Click, toolStripGoTo12.Click, toolStripGoTo11.Click, _
        toolStripGoTo10.Click, ToolStripMenuItem2.Click, TTRToolStripMenuItem.Click, NESROMSToolStripMenuItem.Click, ISwitcherThemesToolStripMenuItem.Click, InstallerPackageSourcesToolStripMenuItem.Click, FrotzGamesToolStripMenuItem.Click, EBooksToolStripMenuItem.Click, DockSwapDocksToolStripMenuItem.Click, ToolStripMenuItem5.Click, ToolStripMenuItem6.Click, cmdGBAROMs.Click, CameraRollToolStripMenuItem.Click, ToolStripMenuItem1.Click

        Dim sPath As String, ts As ToolStripMenuItem, try2 As Boolean = False

        ts = sender
        sPath = ts.Tag()
        try2 = Microsoft.VisualBasic.Left(sPath, 10) = "/var/mobile/"

        If Not selectSpecificPath(sPath) Then
            If try2 Then
                sPath = "/var/root" & Mid(sPath, 10)
            End If
            If Not selectSpecificPath(sPath) Then
                If iPhoneInterface.IsJailbreak Then
                    If Not iPhoneInterface.Exists(sPath) Then
                        If MsgBox("Do you want to create " & sPath & "?", MsgBoxStyle.YesNo, "Create Special Folder") = MsgBoxResult.Yes Then
                            If iPhoneInterface.CreateDirectory(sPath) Then
                                If Not selectSpecificPath(sPath) Then
                                    MsgBox("Error: The program could not find the path '" & sPath & "' on your iPhone.  Creation appeared to be successful", MsgBoxStyle.Critical)
                                End If
                            Else
                                MsgBox("Error: The program could not create the path '" & sPath & "' on your iPhone.  Have you successfully used jailbreak?", MsgBoxStyle.Critical)
                            End If
                        End If
                    End If
                Else
                    MsgBox("Error: The program could not find the path '" & sPath & "' on your iPhone.  Have you successfully used jailbreak?", MsgBoxStyle.Critical)
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItemNewFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemNewFolder.Click
        Dim sPath As String, sNewFolder As String, bValid As Boolean = False

        sPath = getSelectedPath()
        sNewFolder = InputBox("Please enter the name of the new folder you wish to create.  The new path of this folder will be:" & sPath & "<new folder>, and the name is case-sensitive!", "Create Folder In " & sPath, "NewFolder")
        sPath = sPath & sNewFolder

        'make sure it is valid
        If sNewFolder = "" Then
            ' user canceled
        ElseIf sNewFolder = "NewFolder" Then
            MsgBox("You didn't change the default name, I am pretty sure you don't want a 'NewFolder' folder name...", MsgBoxStyle.Information, "Canceled")
        ElseIf InStr(sNewFolder, "/") > 0 Or InStr(sNewFolder, "\") > 0 Or _
                InStr(sNewFolder, "*") > 0 Or InStr(sNewFolder, "?") > 0 Or InStr(sNewFolder, "[") > 0 Or InStr(sNewFolder, "]") > 0 Then
            MsgBox("No slashes or other special characters are allowed in the folder name.", MsgBoxStyle.Information, "Canceled")
        ElseIf iPhoneInterface.Exists(sPath) Then
            MsgBox("The path '" & sPath & "' already exists.", MsgBoxStyle.Information, "Canceled")
        Else
            bValid = True
        End If

        If bValid Then
            'lets create the directory
            If iPhoneInterface.CreateDirectory(sPath) Then
                'it created successfully
                selectSpecificPath(sPath, True)
            Else
                'it failed
                MsgBox("The path '" & sPath & "' failed to create due to an unknown interface failure.", MsgBoxStyle.Information, "Canceled")
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItemDeleteFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemDeleteFolder.Click
        Dim tNode As TreeNode, sPath As String, bValid As Boolean = False, findNode() As TreeNode

        sPath = getSelectedFolder()
        tNode = trvFolders.SelectedNode

        If lstFiles.Items.Count > 0 Or tNode.Nodes.Count > 0 Then
            If bConfirmDeletions Then
                If MsgBox("Are you sure you want to remove " & sPath & "?" & vbCrLf & vbCrLf & "[For your safety, all contents will be backed up.]", MsgBoxStyle.YesNo, "Confirm Folder Deletion") = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
        End If

        If tNode.Nodes.Count > 0 Then ' always ask for folders with sub-folders
            If MsgBox("Are you absolutely sure you wish to delete this folder (" & sPath & ")?" & vbCrLf & vbCrLf & "This cannot be (easily) undone." & vbCrLf & vbCrLf & "[For your safety, all contents will be backed up.]", MsgBoxStyle.YesNo, "Are you Sure?") = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        If Not bDontBackupRun Then
            BackupDirectory(sPath)
        End If
        iPhoneInterface.DeleteDirectory(sPath, True)

        findNode = trvFolders.Nodes.Find(sPath, True)
        If findNode.Length = 0 Then
            'could not find it for some reason, refresh the whole thing
            refreshFolders()
        Else
            ' select the parent path
            Dim sNewFolder As String = Microsoft.VisualBasic.Left(sPath, InStrRev(sPath, "/") - 1)
            If sNewFolder = "" Then
                sNewFolder = "/"
            End If
            trvFolders.Nodes.Remove(findNode(0)) ' delete the selected node

            selectSpecificPath(sNewFolder)
        End If
    End Sub

    Private Sub picFileDetails_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picFileDetails.Resize
        If picFileDetails.Visible Then
            If Not picFileDetails.Image Is Nothing Then
                If picFileDetails.Image.Width > picFileDetails.Width Or picFileDetails.Image.Height > picFileDetails.Height Then
                    picFileDetails.SizeMode = PictureBoxSizeMode.Zoom
                Else
                    picFileDetails.SizeMode = PictureBoxSizeMode.Normal
                End If
            End If
        End If
    End Sub
    Private Sub sortLstFilesGroups()
        Dim holder(lstFiles.Groups.Count - 1) As ListViewGroup
        lstFiles.Groups.CopyTo(holder, 0)
        Array.Sort(holder, New ListViewGroupSorter(lstFilesGroupOrder))
        lstFiles.Groups.Clear()
        lstFiles.Groups.AddRange(holder)
    End Sub
    Private Sub lstFiles_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstFiles.ColumnClick

        If bShowGroups And e.Column = 2 Then ' toggle group order
            If lstFilesGroupOrder = SortOrder.None Or lstFilesGroupOrder = SortOrder.Descending Then
                lstFilesGroupOrder = SortOrder.Ascending
            Else
                lstFilesGroupOrder = SortOrder.Descending
            End If
            sortLstFilesGroups()
            lstFiles.Sort()
        Else
            If lstFilesSortOrder = SortOrder.None Or lstFilesSortOrder = SortOrder.Descending Or e.Column <> lstFilesSortCol Then
                lstFilesSortOrder = SortOrder.Ascending
            Else
                lstFilesSortOrder = SortOrder.Descending
            End If
            lstFilesSortCol = e.Column

            ' Set the ListViewItemSorter property to a new ListViewItemComparer 
            ' object. Setting this property immediately sorts the 
            ' ListView using the ListViewItemComparer object.
            If lstFiles.Columns(e.Column).Text = "Size" Then
                Me.lstFiles.ListViewItemSorter = New ListViewSizeComparer(e.Column, lstFilesSortOrder)
            Else
                Me.lstFiles.ListViewItemSorter = New ListViewStringComparer(e.Column, lstFilesSortOrder)
            End If
        End If

        lstFiles.ShowGroups = False
        lstFiles.ShowGroups = True
        lstFiles.ShowGroups = bShowGroups
    End Sub

    Private Sub trvFolders_NodeMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFolders.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            trvFolders.SelectedNode = e.Node
        End If
    End Sub

    Private Sub menuRightClickFiles_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles menuRightClickFiles.Opening
        If lstFiles.SelectedItems.Count = 0 Then
            e.Cancel = True
        End If
    End Sub

    Private Sub ColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlackToolStripMenuItem.Click, WhiteToolStripMenuItem.Click, GrayToolStripMenuItem.Click
        Dim s As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        If s.Checked Then
            Select Case s.Text
                Case "Black"
                    picFileDetails.BackColor = Color.Black
                Case "Gray"
                    picFileDetails.BackColor = Color.Gray
                Case "White"
                    picFileDetails.BackColor = Color.White
            End Select
            GrayToolStripMenuItem.Checked = (picFileDetails.BackColor = Color.Gray)
            BlackToolStripMenuItem.Checked = (picFileDetails.BackColor = Color.Black)
            WhiteToolStripMenuItem.Checked = (picFileDetails.BackColor = Color.White)
        Else
            picFileDetails.BackColor = PictureBox.DefaultBackColor
        End If
    End Sub

    Private Sub menuSaveSummerboardTheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AsSummerboardFolderToolStripMenuItem.Click
        Dim dFolder As String, sSaveAsFilename As String, sFileFromPhone As String
        Dim sPath As String, sFolders() As String

        If My.Settings.SummerboardPath <> "" Then
            folderBrowserDialog.SelectedPath = My.Settings.SummerboardPath
        End If

        If folderBrowserDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.Cursor = Cursors.WaitCursor
            Application.DoEvents() ' update display

            My.Settings.SummerboardPath = Path.GetDirectoryName(folderBrowserDialog.SelectedPath) ' save the parent

            dFolder = folderBrowserDialog.SelectedPath & "\"
            ' save wallpaper jpg as PNG (~/Library/LockBackground.jpg)
            StatusNormal("Copy Wallpaper Image")
            CopyFromPhoneMakePNG(tildeDir + "/Library/LockBackground.jpg", dFolder & "Wallpaper.png")

            ' save Dock Background (SBDockBG2.png -> Dock.png)
            StatusNormal("Copy Dock Image")
            CopyFromPhonePNG("/System/Library/CoreServices/SpringBoard.app/SBDockBG2.png", dFolder & "Dock.png")

            ' save Icons for applications

            Dim appFolders() As String = {"MobileCal", "MobileMail", "MobileMusicPlayer", "MobileNotes", "MobilePhone", "MobileSafari", "MobileSMS", "MobileTimer", "Preferences"}
            Dim appNames() As String = {"Calendar", "Mail", "iPod", "Notes", "Phone", "Safari", "Text", "Clock", "Settings"}
            Dim newIconName As String

            dFolder = dFolder & "Icons\"
            Directory.CreateDirectory(dFolder)
            sPath = "/Applications/"
            sFolders = iPhoneInterface.GetDirectories(sPath)
            For Each sFolder As String In sFolders
                If sFolder.EndsWith(".app") Then
                    StatusNormal("Copy Icon for " + sFolder)
                    If sFolder = "MobileSlideShow.app" Then
                        Dim iNames() As String = {"icon-Photos.png", "icon-Camera.png"}
                        For Each sIcon As String In (iNames)
                            sFileFromPhone = sPath & sFolder & "/" & sIcon
                            sSaveAsFilename = dFolder & Mid(sIcon, 6, 6) & ".png"
                            CopyFromPhonePNG(sFileFromPhone, sSaveAsFilename)
                        Next
                    Else
                        sFileFromPhone = sPath & sFolder & "/icon.png"
                        newIconName = Microsoft.VisualBasic.Left(sFolder, sFolder.Length - 4)
                        Dim nii As Integer = Array.IndexOf(appFolders, newIconName)
                        If nii >= 0 Then
                            newIconName = appNames(nii)
                        End If
                        sSaveAsFilename = dFolder & newIconName & ".png"
                        CopyFromPhonePNG(sFileFromPhone, sSaveAsFilename)
                    End If
                End If
            Next
            StatusNormal("")
            Me.Cursor = Cursors.Arrow
        End If
    End Sub

    Private Sub NeedDir(ByVal aDir As String)
        If Not Directory.Exists(aDir) Then
            Directory.CreateDirectory(aDir)
        End If
    End Sub

    Private Sub SaveCustomizeToFolder(ByVal frmOptions As frmCustomizeOptions, ByVal destPath As String)
        Dim sb As String = "/System/Library/CoreServices/SpringBoard.app/"
        With frmOptions
            Dim themeName As String = "\" & .txtThemeName.Text
            Dim catName As String = "\" & .txtCategory.Text
            NeedDir(destPath)
            destPath = destPath & "\"
            If .chkDock.Checked Then
                Dim destDock As String = destPath & "DockSwap" & catName
                NeedDir(destDock)
                CopyFromPhonePNG(sb & "SBDockBG2.png", destDock & themeName & ".png")
            End If

            destPath = destPath & "Customize"
            NeedDir(destPath)
            destPath = destPath & "\"

            If .chkCarrier.Checked Then
                Dim destCarrier As String = destPath & "CarrierImages" & catName
                NeedDir(destCarrier)
                Dim curCarrier As String = "_CARRIER_" & .cbCarriers.SelectedItem & ".png"

                CopyFromPhonePNG(sb & "FSO" & curCarrier, destCarrier & themeName & ".png")
                CopyFromPhonePNG(sb & "Default" & curCarrier, destCarrier & themeName & "-1.png")
            End If

            Dim sTypes() As String = {"FSO_", "Default_"}

            If .chkBars.Checked Then
                Dim destBars As String = destPath & "BarsImages" & catName
                NeedDir(destBars)
                destBars = destBars & themeName

                ' Default_[0-5]_Bars.png -> ThemeName.png - ThemeName-4.png
                ' FSO_[0-5]_Bars.png -> themeName-5.png - themeName-11.png
                For j2 As Integer = 0 To 1
                    For j1 As Integer = 0 To 5
                        Dim destNum As String = ".png"
                        If j1 <> 0 Or j2 <> 0 Then
                            destNum = "-" & (j1 + 6 * j2).ToString() & destNum
                        End If
                        CopyFromPhonePNG(sb & sTypes(j2) & j1.ToString() & "_Bars.png", destBars & destNum)
                    Next
                Next
            End If

            If .chkWiFi.Checked Then
                Dim destBars As String = destPath & "WiFiImages" & catName
                NeedDir(destBars)
                destBars = destBars & themeName

                ' Default_[0-5]_Bars.png -> ThemeName.png - ThemeName-4.png
                ' FSO_[0-5]_Bars.png -> themeName-5.png - themeName-11.png
                For j2 As Integer = 0 To 1
                    For j1 As Integer = 0 To 3
                        Dim destNum As String = ".png"
                        If j1 <> 0 Or j2 <> 0 Then
                            destNum = "-" & (j1 + 6 * j2).ToString() & destNum
                        End If
                        CopyFromPhonePNG(sb & sTypes(j2) & j1.ToString() & "_AirPort.png", destBars & destNum)
                    Next
                Next
            End If

            If .chkBadge.Checked Then
                Dim dest As String = destPath & "BadgeImages" & catName
                NeedDir(dest)
                CopyFromPhonePNG(sb & "SBBadgeBG.png", dest & themeName & ".png")
            End If

            If .chkBattery.Checked Then
                Dim dest As String = destPath & "BatteryImages" & catName
                NeedDir(dest)
                ' 17 is used as default
                CopyFromPhonePNG(sb & "BatteryBG_17.png", dest & themeName & ".png")

                For j1 As Integer = 1 To 16
                    CopyFromPhonePNG(sb & "BatteryBG_" & j1.ToString() & ".png", dest & themeName & "-" & j1.ToString() & ".png")
                Next
            End If

            If .chkSound.Checked Then
                Dim dest As String = destPath & "SoundImages" & catName
                NeedDir(dest)
                CopyFromPhonePNG(sb & "ring.png", dest & themeName & ".png")
                CopyFromPhonePNG(sb & "mute.png", dest & themeName & "-1.png")
                CopyFromPhonePNG(sb & "silent.png", dest & themeName & "-2.png")
                CopyFromPhonePNG(sb & "speaker.png", dest & themeName & "-3.png")
            End If

            If .chkBalloons.Checked Then
                Dim dest As String = destPath & "Chat1Images" & catName
                NeedDir(dest)
                CopyFromPhonePNG("/Applications/MobileSMS.app/Balloon_1.png", dest & themeName & ".png")

                dest = destPath & "Chat2Images" & catName
                NeedDir(dest)
                CopyFromPhonePNG("/Applications/MobileSMS.app/Balloon_2.png", dest & themeName & ".png")
            End If
            If .chkKeypad.Checked Then
                Dim dest As String = destPath & "DialerImages" & catName
                NeedDir(dest)
                CopyFromPhonePNG("/Applications/MobilePhone.app/BarDialer_Sel.png", dest & themeName & ".png")
            End If

            If .chkMainSlider.Checked Then
                Dim dest As String = destPath & "MainSliderImages" & catName
                NeedDir(dest)
                CopyFromPhonePNG("/System/Library/Frameworks/TelephonyUI.Framework/bottombarknobgrey.png", dest & themeName & ".png")
            End If
            If .chkPowerSlider.Checked Then
                Dim dest As String = destPath & "PowerSliderImages" & catName
                NeedDir(dest)
                CopyFromPhonePNG("/System/Library/Frameworks/TelephonyUI.Framework/bottombarknobred.png", dest & themeName & ".png")
            End If
            If .chkCallSlider.Checked Then
                Dim dest As String = destPath & "CallSliderImages" & catName
                NeedDir(dest)
                CopyFromPhonePNG("/System/Library/Frameworks/TelephonyUI.Framework/bottombarknobgreen.png", dest & themeName & ".png")
            End If
            If .chkHiMask.Checked Then
                Dim dest As String = destPath & "MaskSliderImages" & catName
                NeedDir(dest)
                CopyFromPhonePNG("/System/Library/Frameworks/TelephonyUI.Framework/bottombarlocktextmask.png", dest & themeName & ".png")
            End If

            If .cbSounds.CheckedItems.Count > 0 Then
                Dim dest As String = destPath & "AudioFiles" & catName
                NeedDir(dest)

                Dim sndNameArray() As String = {"Unlock", "Lock", "Received", "Sent", "Voicemail", "Alarm", _
                    "BeepBeep", "LowPower", "Mail", "NewMail", "Photo", "SMSReceived"}
                Dim sndArray() As String = {"unlock", "lock", "ReceivedMessage", "SentMessage", "Voicemail", "alarm", _
                    "beep-beep", "low_power", "mail-sent", "New-mail", "photoShutter", "sms-received"}

                For Each cb As String In .cbSounds.CheckedItems
                    Dim j1 As Integer = Array.IndexOf(sndNameArray, Microsoft.VisualBasic.Left(cb, Len(cb) - 6))
                    CopyFromPhonePNG("/System/Library/Audio/UISounds/" & sndArray(j1) & ".caf", dest & themeName & "_" & sndNameArray(j1) & ".aif")
                Next
            End If
        End With
    End Sub

    Private Sub AsCustomizeFoldersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AsCustomizeFoldersToolStripMenuItem.Click
        Dim frmOptions As frmCustomizeOptions = New frmCustomizeOptions()
        If frmOptions.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If frmOptions.HasChecked() Then
                folderBrowserDialog.SelectedPath = My.Settings.CustomizePath
                If folderBrowserDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    My.Settings.CustomizePath = folderBrowserDialog.SelectedPath
                    Me.Cursor = Cursors.WaitCursor
                    Application.DoEvents() ' show cursor
                    SaveCustomizeToFolder(frmOptions, folderBrowserDialog.SelectedPath)
                    Me.Cursor = Cursors.Arrow
                End If
            End If
        End If
        frmOptions.Dispose()
    End Sub

    Private Sub IPhoneToPCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IPhoneToPCToolStripMenuItem.Click
        bConvertToPNG = IPhoneToPCToolStripMenuItem.Checked
    End Sub

    Private Sub PCToIPhoneToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PCToIPhoneToolStripMenuItem.Click
        bConvertToiPhonePNG = PCToIPhoneToolStripMenuItem.Checked
    End Sub
    Private Sub PreviewChanged()
        chkPreviewEnabled.Checked = bShowPreview
        ShowPreviewsToolStripMenuItem.Checked = bShowPreview
        btnPreview.Enabled = bShowPreview
        If bShowPreview Then
            DoFileSelectedPreview(False)
        Else
            ClearPreview()
            prevSelectedFile = "" ' force preview next time
        End If
    End Sub
    Private Sub ShowPreviewsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowPreviewsToolStripMenuItem.Click
        bShowPreview = ShowPreviewsToolStripMenuItem.Checked
        PreviewChanged()
    End Sub

    Private Sub chkPreviewEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPreviewEnabled.CheckedChanged
        bShowPreview = chkPreviewEnabled.Checked
        PreviewChanged()
    End Sub

    Private Sub DoSaveFolderIn(ByVal sPath As String, ByVal dPath As String)
        NeedDir(dPath)
        dPath = dPath & "\"

        ' save the files
        Dim phFiles() As String = iPhoneInterface.GetFiles(sPath)
        sPath = sPath & "/"
        For Each phF As String In phFiles
            copyFromPhone(sPath & phF, dPath & FixPhoneFilename(Path.GetFileName(phF)))
        Next

        Dim phDirs() As String = iPhoneInterface.GetDirectories(sPath)
        For Each phD As String In phDirs
            DoSaveFolderIn(sPath & phD, dPath & phD)
        Next
    End Sub

    Private Sub ToolStripMenuItemSaveFolderIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemSaveFolderIn.Click
        folderBrowserDialog.SelectedPath = My.Settings.SaveFolderPath
        If folderBrowserDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.Cursor = Cursors.WaitCursor
            Dim dPath As String = folderBrowserDialog.SelectedPath
            My.Settings.SaveFolderPath = dPath
            Dim sPath As String = getSelectedFolder()
            dPath = dPath & "\" & Path.GetFileName(sPath)
            DoSaveFolderIn(sPath, dPath)
            Me.Cursor = Cursors.Arrow
        End If
    End Sub

    Private Sub BackupFolderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackupFolderToolStripMenuItem.Click
        Application.UseWaitCursor = True
        BackupDirectory(getSelectedFolder())
        Application.UseWaitCursor = False
    End Sub

    Private Sub cmdRenameFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRenameFile.Click
        Dim oldPath As String = getSelectedFilename(), newPath As String
        Dim newName As String = InputBox("Enter new folder name:", "Rename file ", Path.GetFileName(oldPath))
        If newName <> "" Then
            If InStr(newName, "/") Then
                StatusWarning("Can not use rename to move a file")
                Exit Sub
            End If
            newPath = PhoneGetDirectoryName(oldPath) & "/" & newName
            If iPhoneInterface.Rename(oldPath, newPath) Then
                loadFiles()
            End If
        End If

    End Sub

    Private Sub cmdRenameFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRenameFolder.Click
        Dim oldPath As String = getSelectedFolder(), newPath As String
        Dim newName As String = InputBox("Enter new folder name:", "Rename folder", Path.GetFileName(oldPath))
        If newName <> "" Then
            If InStr(newName, "/") Then
                StatusWarning("Can not use rename to move a folder")
                Exit Sub
            End If
            newPath = PhoneGetDirectoryName(oldPath) & "/" & newName
            If iPhoneInterface.Rename(oldPath, newPath) Then
                Dim tNode As TreeNode = trvFolders.SelectedNode
                tNode.Text = newName
                tNode.Name = newPath
            End If
        End If
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        lstFiles.Select()
        prevSelectedFile = "" ' force preview
        DoFileSelectedPreview(True)
    End Sub

    Private Sub chkPreviewEnabled_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPreviewEnabled.Enter
        lstFiles.Select()
    End Sub

    Private Sub cmdShowGroups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowGroups.Click
        ' work around massive .Net bugs with grouping and sorting in ListView
        bShowGroups = Not bShowGroups
        lstFiles.ShowGroups = bShowGroups
        If bShowGroups And lstFilesSortCol = 2 Then ' we were sorted by groups, so sort the groups
            lstFilesGroupOrder = lstFilesSortOrder
            sortLstFilesGroups()
        End If
        lstFiles.Sort()
        lstFiles.ShowGroups = Not bShowGroups
        lstFiles.Sort()
        lstFiles.ShowGroups = bShowGroups
        lstFiles.Sort()
    End Sub
    Private Sub cmdFavorite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim anItem As ToolStripMenuItem = sender
        selectSpecificPath(anItem.Tag)
    End Sub
    Private Sub AddNewFavorite(ByVal favName As String, ByVal favPath As String)
        favNames.Add(favName)
        favPaths.Add(favPath)
        Dim newFav As ToolStripMenuItem = mnuFavorites.DropDownItems.Add(favName, Nothing, AddressOf cmdFavorite_Click)
        newFav.Tag = favPath
        newFav.ToolTipText = favPath
    End Sub
    Private Sub AddToFavoritesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToFavoritesToolStripMenuItem.Click
        If getSelectedFolder() <> "" Then
            Dim frmFav As frmAddFavorite = New frmAddFavorite(getSelectedFolder())
            If frmFav.ShowDialog() = Windows.Forms.DialogResult.OK Then
                AddNewFavorite(frmFav.favName, frmFav.favPath)
            End If
        End If
    End Sub

    Private Sub mnuFavorites_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFavorites.DropDownOpening
        AddToFavoritesToolStripMenuItem.Enabled = (getSelectedFolder() <> "")
        OrganizeFavoritesToolStripMenuItem.Enabled = (favNames.Count > 0)
    End Sub

    Private Sub trvFolders_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles trvFolders.KeyUp
        If e.KeyCode = Keys.F5 Then
            addFoldersBeneath(trvFolders.SelectedNode)
            trvFolders.Sort()
        End If
    End Sub

    Private Sub menuSetTooltips(ByRef aMenu As ToolStripDropDownItem)
        ' setup tooltips
        For Each anItem As ToolStripItem In aMenu.DropDownItems
            If TypeOf anItem Is ToolStripMenuItem Then
                anItem.ToolTipText = anItem.Tag
            End If
        Next
    End Sub
    Private Sub LoadFavoritesMenu()
        ' clear any existing menuitems
        For j1 As Integer = mnuFavorites.DropDownItems.Count - 1 To 3 Step -1
            mnuFavorites.DropDownItems.RemoveAt(j1)
        Next

        ' load up the favorite menu items
        For j1 As Integer = 0 To favNames.Count - 1
            Dim newFav As ToolStripMenuItem = mnuFavorites.DropDownItems.Add(favNames(j1), Nothing, AddressOf cmdFavorite_Click)
            newFav.Tag = favPaths(j1)
            newFav.ToolTipText = favPaths(j1)
        Next
    End Sub
    Private Sub OrganizeFavoritesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrganizeFavoritesToolStripMenuItem.Click
        Dim orgDialog As New frmOrganizeFavorites(favNames, favPaths)

        If orgDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            favNames.Clear()
            favPaths.Clear()
            For Each m As String In orgDialog.favNames
                favNames.Add(m)
            Next
            For Each m As String In orgDialog.favPaths
                favPaths.Add(m)
            Next
        End If
        LoadFavoritesMenu()
    End Sub

    Private Sub OptionsToolStripMenuItem_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.DropDownOpening
        ConfirmDeletionsToolStripMenuItem.Checked = bConfirmDeletions
        IPhoneToPCToolStripMenuItem.Checked = bConvertToPNG
        PCToIPhoneToolStripMenuItem.Checked = bConvertToiPhonePNG
        ShowPreviewsToolStripMenuItem.Checked = bShowPreview
        IgnoreThumbsdbToolStripMenuItem.Checked = bIgnoreThumbsFile
        IgnoreDSStoreToolStripMenuItem.Checked = bIgnoreDSStoreFile
        DontBackupRunToolStripMenuItem.Checked = bDontBackupRun
        DontBackupEverToolStripMenuItem.Enabled = bDontBackupRun Or bDontBackupEver
        DontBackupEverToolStripMenuItem.Checked = bDontBackupEver
    End Sub
    Private Sub LoadOptionBooleans()
        bConfirmDeletions = ConfirmDeletionsToolStripMenuItem.Checked
        bConvertToPNG = IPhoneToPCToolStripMenuItem.Checked
        bConvertToiPhonePNG = PCToIPhoneToolStripMenuItem.Checked
        bShowPreview = ShowPreviewsToolStripMenuItem.Checked
        bIgnoreThumbsFile = IgnoreThumbsdbToolStripMenuItem.Checked
        bIgnoreDSStoreFile = IgnoreDSStoreToolStripMenuItem.Checked
        bDontBackupRun = DontBackupRunToolStripMenuItem.Checked
        bDontBackupEver = DontBackupEverToolStripMenuItem.Checked
    End Sub
    Private Sub OptionsToolStripMenuItem_DropDownClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.DropDownClosed
        Dim mi As New MethodInvoker(AddressOf LoadOptionBooleans)
        mi.BeginInvoke(Nothing, Nothing) ' must delay to give CheckOnClick time to be processed (.Net bug)
    End Sub

    Private Sub menuSaveSummerboardTheme_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuSaveSummerboardTheme.DropDownOpening
        AsSummerboardFolderToolStripMenuItem.Enabled = bNowConnected
    End Sub

    Private Sub ToolStripMenuItem4_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.DropDownOpening
        AsCustomizeFoldersToolStripMenuItem.Enabled = bNowConnected
    End Sub

    Private Sub mnuView_DropDownOpening(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuView.DropDownOpening
        cmdShowGroups.Checked = bShowGroups
    End Sub
End Class

' Implements the manual sorting of items by columns.
Class ListViewStringComparer
    Implements IComparer

    Private col As Integer
    Private sortOrder As SortOrder

    Public Sub New()
        col = 0
        sortOrder = Windows.Forms.SortOrder.Ascending
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
        sortOrder = Windows.Forms.SortOrder.Ascending
    End Sub
    Public Sub New(ByVal column As Integer, ByVal sort As SortOrder)
        col = column
        sortOrder = sort
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim res As Integer
        res = String.Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        If sortOrder = Windows.Forms.SortOrder.Ascending Then
            Return res
        Else
            Return -res
        End If
    End Function
End Class

Class ListViewSizeComparer
    Implements IComparer

    Private col As Integer
    Private sortOrder As SortOrder

    Public Sub New()
        col = 0
        sortOrder = Windows.Forms.SortOrder.Ascending
    End Sub

    Public Sub New(ByVal column As Integer)
        col = column
        sortOrder = Windows.Forms.SortOrder.Ascending
    End Sub
    Public Sub New(ByVal column As Integer, ByVal sort As SortOrder)
        col = column
        sortOrder = sort
    End Sub

    Private Function SizeToLong(ByVal aSize As String) As Double
        Dim ans As Double
        ans = Conversion.Val(aSize)
        If aSize.EndsWith("kb") Then
            ans = 1024 * ans
        ElseIf aSize.EndsWith("mb") Then
            ans = 1024 * 1024 * ans
        End If
        Return ans
    End Function
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim xs As Double, ys As Double
        xs = SizeToLong(CType(x, ListViewItem).SubItems(col).Text)
        ys = SizeToLong(CType(y, ListViewItem).SubItems(col).Text)
        If sortOrder = Windows.Forms.SortOrder.Ascending Then
            Return CInt(xs - ys)
        Else
            Return CInt(ys - xs)
        End If
    End Function
End Class

Class ListViewGroupSorter
    Implements IComparer

    Private order As SortOrder

    ' Stores the sort order.
    Public Sub New(ByVal theOrder As SortOrder)
        order = theOrder
    End Sub 'New

    ' Compares the groups by header value, using the saved sort
    ' order to return the correct value.
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
        Implements IComparer.Compare
        Dim result As Integer = String.Compare( _
            CType(x, ListViewGroup).Header, _
            CType(y, ListViewGroup).Header)
        If order = SortOrder.Ascending Then
            Return result
        Else
            Return -result
        End If
    End Function 'Compare
End Class
