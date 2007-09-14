Imports System.Threading
Imports System.IO

Public Class frmMain
    Const VERSION As String = "1.3.0"

    Const STRING_ROOT As String = "[root]"
    Const IMAGE_FOLDER_CLOSED As Integer = 0
    Const IMAGE_FOLDER_OPEN As Integer = 1
    Const IMAGE_FILE_UNKOWN As Integer = 0
    Const IMAGE_FILE_MUSIC As Integer = 1
    Const IMAGE_FILE_MOVIE As Integer = 2
    Const IMAGE_FILE_TEXT As Integer = 3
    Const IMAGE_FILE_IMAGE As Integer = 4
    Const IMAGE_FILE_AUDIO As Integer = 5
    Const IMAGE_FILE_DATABASE As Integer = 6
    Const BACKUP_DIRECTORY As String = "BACKUPS"
    Const BACKUP_SUFFIX As String = "iPhone.backup."

    Private FILE_TEMPORARY_VIEWER As String = "iPhone.temp"
    Private APP_PATH As String = ""

    Private iPhoneInterface As Manzana.iPhone

    Private txtSerial As String
    Private bNowConnected As Boolean = False
    Private bConnectionChanged As Boolean = False
    Private bUpdateInProgress As Boolean = False
    Private bSupressFiles As Boolean = False
    Private wasQTpreview As Boolean = False
    Private QTpreviewFile As String

    Private lstFilesSortOrder As SortOrder

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
            txtSerial = iPhoneInterface.Device.serial
            If Me.InvokeRequired Then
                Me.Invoke(New NoParmDel(AddressOf DelayedConnectionChange))
            Else
                DelayedConnectionChange()
            End If
        End If
    End Sub

    Public Sub iPhoneDisconnected_Details() Handles Me.iPhoneDisconnected
        If bNowConnected Then
            txtSerial = ""
            If Me.InvokeRequired Then
                Me.Invoke(New NoParmDel(AddressOf DelayedConnectionChange))
            Else
                DelayedConnectionChange()
            End If
        End If
    End Sub

    Private Sub iPhoneConnected_EventHandler(ByVal sender As Object, ByVal args As Manzana.ConnectEventArgs)
        RaiseEvent iPhoneConnected()
    End Sub

    Private Sub iPhoneDisconnected_EventHandler(ByVal sender As Object, ByVal args As Manzana.ConnectEventArgs)
        RaiseEvent iPhoneDisconnected()
    End Sub

    Private Sub connectionChange()
        If bConnectionChanged Then
            bConnectionChanged = False

            If bNowConnected Then
                StatusNormal("iPhone is connected")
            Else
                StatusWarning("iPhone is NOT connected, please check your connections!")
            End If

            'changed the enable/disable ofthe form elements
            trvFolders.Enabled = bNowConnected
            lstFiles.Enabled = bNowConnected
            txtFileDetails.Enabled = bNowConnected
            toolStripGoTo.Enabled = bNowConnected
            ToolStripMenuItemNewFolder.Enabled = bNowConnected
            ToolStripMenuItemDeleteFolder.Enabled = bNowConnected

            If bNowConnected Then
                'the phone was just recognized as connected
                refreshFolders()
            End If
        End If
    End Sub


    Private Sub startStatus(ByVal iMax As Integer)
        tlbProgressBar.Minimum = 0
        tlbProgressBar.Value = 0
        tlbProgressBar.Maximum = iMax + 1
        tlbProgressBar.Step = 1
        tlbProgressBar.Visible = True
    End Sub
    Private Sub endStatus()
        tlbProgressBar.Value = tlbProgressBar.Maximum
        tlbStatusStrip.Refresh()
        tlbProgressBar.Visible = False
    End Sub
    Private Sub incrementStatus(Optional ByVal iMax As Integer = 1)
        If tlbProgressBar.Value < tlbProgressBar.Maximum Then
            tlbProgressBar.PerformStep()
            tlbStatusStrip.Refresh()
        End If
    End Sub

    Private Sub loadChildrenFolders()
        Dim sTemp As String, iTemp As Integer, tmpNode As TreeNode
        Dim selectedNode As TreeNode

        If Not bUpdateInProgress Then
            bUpdateInProgress = True
            trvFolders.SuspendLayout()
            selectedNode = trvFolders.SelectedNode

            'we need to go through all the children and refresh them
            startStatus(selectedNode.Nodes.Count)
            For iTemp = 0 To selectedNode.Nodes.Count - 1
                incrementStatus()
                'Application.DoEvents()

                tmpNode = selectedNode.Nodes(iTemp)
                sTemp = tmpNode.FullPath

                'remove anything if it is there already
                tmpNode.Nodes.Clear()
                'tmpNode.ImageIndex = IMAGE_FOLDER_OPEN
                'now add it (only if it is not the root)
                If sTemp <> STRING_ROOT Then
                    sTemp = Replace(Mid(sTemp, Len(STRING_ROOT) + 1), "\", "/")
                    Try
                        addFolders(sTemp, tmpNode, 1)
                    Catch
                        selectedNode.Nodes(iTemp).Remove() ' someone deleted the folder
                    End Try
                End If
            Next
            endStatus()
            trvFolders.ResumeLayout()
        End If
        bUpdateInProgress = False

    End Sub

    Private Sub loadFiles()
        Dim tmpNode As TreeNode, iTemp As Integer, sFiles() As String
        Dim sTemp As String, lstTemp As ListViewItem
        Dim iFileSize As Integer, sFileSize As String

        If Not bSupressFiles Then
            'loads the files into the list view based on the current directory

            'first clear out any files that may be there
            lstFiles.Items.Clear()
            lstFiles.ListViewItemSorter = Nothing
            lstFilesSortOrder = SortOrder.None
            StatusNormal("")

            'now get the information from the folder
            tmpNode = trvFolders.SelectedNode
            sTemp = tmpNode.FullPath
            If sTemp <> STRING_ROOT Then
                sTemp = Replace(Mid(sTemp, Len(STRING_ROOT) + 1), "\", "/")
            Else
                sTemp = "/"
            End If

            'now get the files from the iphone
            sFiles = iPhoneInterface.GetFiles(sTemp)

            'now go one by one and add it
            startStatus(sFiles.Length)
            For iTemp = 0 To sFiles.Length - 1
                incrementStatus()
                'Application.DoEvents()

                lstTemp = New ListViewItem(sFiles(iTemp))
                lstTemp.ImageIndex = getImageIndexForFile(sFiles(iTemp))

                'get some more information for the file

                'first the size
                iFileSize = iPhoneInterface.FileSize(sTemp & "/" & sFiles(iTemp))
                'make it look pretty
                If iFileSize > 10240000 Then
                    sFileSize = Format(iFileSize / 1024000, "0.##") & " mb"
                ElseIf iFileSize > 10240 Then
                    sFileSize = Format(iFileSize / 1024, "0.##") & " kb"
                Else
                    sFileSize = iFileSize & " bytes"
                End If
                lstTemp.SubItems.Add(sFileSize)

                'get the file type
                lstTemp.SubItems.Add(getFiletype(lstTemp.ImageIndex))

                'finally add it to the view
                lstFiles.Items.Add(lstTemp)

            Next
            endStatus()

            'update thet text
            grpFiles.Text = "Files on your iPhone in the '" & trvFolders.SelectedNode.FullPath & "' Directory"
        End If
    End Sub

    Private Function getImageIndexForFile(ByVal sFilename As String) As Integer
        'returns the image index for the given file type
        Dim iReturn As Integer = 0

        'convert it to lowercase for comparison reasons
        sFilename = LCase(sFilename)

        If InStr(sFilename, ".png") Or InStr(sFilename, ".jpg") Or InStr(sFilename, ".jpeg") Or InStr(sFilename, ".gif") Then
            iReturn = IMAGE_FILE_IMAGE
        ElseIf InStr(sFilename, ".mov") Then
            iReturn = IMAGE_FILE_MOVIE
        ElseIf InStr(sFilename, ".strings") Or InStr(sFilename, ".conf") Or InStr(sFilename, ".txt") Or InStr(sFilename, ".plist") Or InStr(sFilename, ".script") Or InStr(sFilename, ".html") Then
            iReturn = IMAGE_FILE_TEXT
        ElseIf InStr(sFilename, ".db") Then
            iReturn = IMAGE_FILE_DATABASE
        ElseIf InStr(sFilename, ".aiff") Or InStr(sFilename, ".amr") Or InStr(sFilename, ".aif") Or InStr(sFilename, ".caf") Then
            iReturn = IMAGE_FILE_AUDIO
        ElseIf InStr(sFilename, ".m4a") Or InStr(sFilename, ".m4p") Or InStr(sFilename, ".mp3") Or InStr(sFilename, ".aac") Then
            iReturn = IMAGE_FILE_MUSIC
        End If


        getImageIndexForFile = iReturn
    End Function

    Private Function getFiletype(ByVal iImageIndex As Integer) As String
        'gets the string file type given the image index
        Dim sReturn As String = "Unknown File Type"

        Select Case iImageIndex
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
            Case Else
                'including image_file_unkonwn

        End Select

        getFiletype = sReturn
    End Function

    Private Sub refreshFolders()
        Dim rootNode As TreeNode

        Me.Cursor = Cursors.WaitCursor

        'reset the tree view
        trvFolders.SuspendLayout()

        trvFolders.Nodes.Clear()
        rootNode = New TreeNode(STRING_ROOT)
        rootNode.ContextMenuStrip = menuRightClickFolders
        trvFolders.Nodes.Add(rootNode)
        trvFolders.SelectedNode = rootNode

        addFolders("", rootNode)

        rootNode.Expand()

        trvFolders.ResumeLayout()
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub addFolders(ByVal sPath As String, ByRef selectedNode As TreeNode, Optional ByVal iDepth As Integer = 0)
        'This function is recursive to add all of the folders to the tree view
        ' you give it one folder and it will continue to drill down and add folders
        Dim sFolder() As String, iFolder As Integer, newNode As TreeNode ', origNode As TreeNode
        Dim showStatus As Boolean

        'save the orignal node so we can move back to it
        'origNode = trvFolders.SelectedNode

        'get the data from the phone
        sFolder = iPhoneInterface.GetDirectories(sPath)

        If Not tlbProgressBar.Visible Then ' update user
            startStatus(sFolder.Length)
            showStatus = True
        End If

        For iFolder = 0 To sFolder.Length - 1
            'create the new node
            newNode = New TreeNode(sFolder(iFolder))
            newNode.Name = sPath & "/" & sFolder(iFolder)
            newNode.ContextMenuStrip = menuRightClickFolders

            'Debug.Print(newNode.Name)

            selectedNode.Nodes.Add(newNode)

            'now make the recursive call on this folder
            If iDepth < 1 Then
                'trvFolders.SelectedNode = newNode
                addFolders(sPath & "/" & sFolder(iFolder), newNode, iDepth + 1)
                'now go back to our original node
                'trvFolders.SelectedNode = origNode
            End If

            If showStatus Then
                incrementStatus()
            End If
        Next

        If showStatus Then
            endStatus()
        End If
    End Sub

    Private Function copyFromPhone(ByVal sourceOnPhone As String, ByVal destinationOnComputer As String) As Boolean
        Dim sBuffer(1023) As Byte, iDataBytes As Integer
        Dim iPhoneFileInterface As Manzana.iPhoneFile
        Dim fileTemp As FileStream
        Dim bReturn As Boolean = False

        'remove our local file if it exists
        If File.Exists(destinationOnComputer) Then Kill(destinationOnComputer)

        'make sure the source file exists
        If iPhoneInterface.Exists(sourceOnPhone) Then
            startStatus(iPhoneInterface.FileSize(sourceOnPhone) / sBuffer.Length) 'show our progress bar

            'open a connection to the file and read it
            iPhoneFileInterface = Manzana.iPhoneFile.OpenRead(iPhoneInterface, sourceOnPhone)

            fileTemp = File.OpenWrite(destinationOnComputer)
            iDataBytes = iPhoneFileInterface.Read(sBuffer, 0, sBuffer.Length)
            While iDataBytes > 0
                fileTemp.Write(sBuffer, 0, iDataBytes)
                iDataBytes = iPhoneFileInterface.Read(sBuffer, 0, sBuffer.Length)
                incrementStatus() 'increment our progressbar
            End While
            endStatus() 'fill our progressbar

            iPhoneFileInterface.Close()
            fileTemp.Close()

            bReturn = True
        End If

        copyFromPhone = bReturn
    End Function

    Private Function copyToPhone(ByVal sourceOnComputer As String, ByVal destinationOnPhone As String) As Boolean
        Dim sBuffer(1023) As Byte, iDataBytes As Integer
        Dim iPhoneFileInterface As Manzana.iPhoneFile
        Dim fileTemp As FileStream
        Dim bReturn As Boolean = False
        Dim sPath As String, sFile As String, dPath As String

        'get the details out
        sPath = Microsoft.VisualBasic.Left(sourceOnComputer, InStrRev(sourceOnComputer, "\"))
        sFile = Mid(sourceOnComputer, InStrRev(sourceOnComputer, "\") + 1)

        'update the status bar
        StatusNormal("Copying '" & sourceOnComputer & "'...")

        'are we copying a file?
        If File.Exists(sourceOnComputer) Then
            If destinationOnPhone.EndsWith("/") Then
                'they did not pass a specific file name, so add the source filename
                destinationOnPhone = destinationOnPhone & sFile
            End If

            'see if the destination file exists
            If iPhoneInterface.Exists(destinationOnPhone) Then
                'it exists, back it up before overwriting
                sPath = Microsoft.VisualBasic.Left(destinationOnPhone, InStrRev(destinationOnPhone, "/"))
                sFile = Mid(destinationOnPhone, InStrRev(destinationOnPhone, "/") + 1)
                backupFileFromPhone(sPath, sFile)
            End If

            'open a connection to the file and write it
            iPhoneFileInterface = Manzana.iPhoneFile.OpenWrite(iPhoneInterface, destinationOnPhone)
            'open a connection locally for read
            fileTemp = File.OpenRead(sourceOnComputer)
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

            bReturn = True
        ElseIf Directory.Exists(sourceOnComputer) Then
            If destinationOnPhone.EndsWith("/") Then
                'they did not pass a specific name, so add the source name
                destinationOnPhone = destinationOnPhone & sFile
            End If

            ' Create matching directory on phone
            If Not iPhoneInterface.Exists(destinationOnPhone) Then
                iPhoneInterface.CreateDirectory(destinationOnPhone)
            End If

            dPath = destinationOnPhone & "/"

            ' copy all files over recursively
            For Each filepath As String In Directory.GetFiles(sourceOnComputer)
                copyToPhone(filepath, dPath)
            Next

            ' copy all directories over recursively
            For Each dirpath As String In Directory.GetDirectories(sourceOnComputer)
                copyToPhone(dirpath, dPath)
            Next

            bReturn = True
        End If

        copyToPhone = bReturn
    End Function

    Private Sub backupFileFromPhone(ByVal sSourcePath As String, ByVal sSourceFile As String)
        'copies a file from the phone and backs it up in the appropriate directory
        'grab the file then save it with an extra extension
        Dim sDestinationPath As String, sDestinationFile As String

        'make sure it ends in a /
        If Not sSourcePath.EndsWith("/") Then sSourcePath = sSourcePath & "/"

        sDestinationPath = APP_PATH & BACKUP_DIRECTORY & Replace(sSourcePath, "/", "\")
        sDestinationFile = BACKUP_SUFFIX & Format(Now, "yyyyMMdd.HHmmss.") & sSourceFile

        'Create the directory if it does not already exist
        If Not Directory.Exists(sDestinationPath) Then Directory.CreateDirectory(sDestinationPath)

        'copy it into the backup directory
        copyFromPhone(sSourcePath & sSourceFile, sDestinationPath & sDestinationFile)

        'display a message box
        StatusNormal("The file was successfully backed up as '" & sDestinationFile & "'.")
    End Sub

    Private Function createDirectoryOnPhone(ByVal sNewDirectoryOnPhone As String) As Boolean
        Return iPhoneInterface.CreateDirectory(sNewDirectoryOnPhone)
    End Function

    Private Function delFromPhone(ByVal sourceOnPhone As String) As Boolean
        Dim sPath As String, sFile As String
        Dim bReturn As Boolean = False

        'make sure the source file exists
        If iPhoneInterface.Exists(sourceOnPhone) Then

            'see if the destination file exists
            If iPhoneInterface.Exists(sourceOnPhone) Then
                'it exists, back it up before overwriting
                sPath = Microsoft.VisualBasic.Left(sourceOnPhone, InStrRev(sourceOnPhone, "/"))
                sFile = Mid(sourceOnPhone, InStrRev(sourceOnPhone, "/") + 1)
                backupFileFromPhone(sPath, sFile)
            End If

            iPhoneInterface.DeleteFile(sourceOnPhone)

            bReturn = True
        End If

        delFromPhone = bReturn
    End Function

    Private Function FileCompare(ByVal fileName1 As String, ByVal fileName2 As String) As Boolean
        ' This method accepts two strings that represent two files to 
        ' compare. A return value of 0 indicates that the contents of the files
        ' are the same. A return value of any other value indicates that the 
        ' files are not the same.
        Dim file1byte As Integer
        Dim file2byte As Integer
        Dim fs1 As FileStream
        Dim fs2 As FileStream

        ' Determine if the same file was referenced two times.
        If (fileName1 = fileName2) Then
            ' Return 0 to indicate that the files are the same.
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

            ' Return a non-zero value to indicate that the files are different.
            Return False
        End If

        ' Read and compare a byte from each file until either a
        ' non-matching set of bytes is found or until the end of
        ' file1 is reached.
        Do
            ' Read one byte from each file.
            file1byte = fs1.ReadByte()
            file2byte = fs2.ReadByte()
        Loop While ((file1byte = file2byte) And (file1byte <> -1))

        ' Close the files.
        fs1.Close()
        fs2.Close()

        ' Return the success of the comparison. "file1byte" is
        ' equal to "file2byte" at this point only if the files are 
        ' the same.
        Return ((file1byte - file2byte) = 0)
    End Function

    Private Function getSelectedFilename() As String
        'returns the currently selected filename
        getSelectedFilename = getSelectedFolder() & lstFiles.SelectedItems(0).Text
    End Function

    Private Function getSelectedFolder() As String
        'returns the currently selected folder
        getSelectedFolder = Mid(trvFolders.SelectedNode.FullPath, Len(STRING_ROOT) + 1) & "/"
    End Function

    Private Function selectSpecificPath(ByVal sPathOnPhone As String) As Boolean
        'selects a specifc path in the tree view
        Dim sTemp As String, iNode As Integer, tn() As TreeNode, bReturn As Boolean = False

        bSupressFiles = True 'so we don't load files until the end

        'first, lets try to find it without expanding
        tn = trvFolders.Nodes.Find(sPathOnPhone, True)
        If tn.Length = 0 Then
            'we couldn't find it
            'select the root first
            tn = trvFolders.Nodes.Find("", True)
            If tn.Length > 0 Then
                tn(0).EnsureVisible()
                trvFolders.SelectedNode = tn(0)
            End If

            'go through and select each node
            iNode = 2
            Do While InStr(iNode, sPathOnPhone, "/") > 0

                'pull out the full path to the next node
                sTemp = Microsoft.VisualBasic.Left(sPathOnPhone, InStr(iNode, sPathOnPhone, "/") - 1)
                iNode = InStr(iNode, sPathOnPhone, "/") + 1

                'select the one we found
                tn = trvFolders.Nodes.Find(sTemp, True)
                If tn.Length > 0 Then
                    tn(0).EnsureVisible()
                    trvFolders.SelectedNode = tn(0)
                End If

            Loop

            'now it should definitely be available
            bSupressFiles = False
            tn = trvFolders.Nodes.Find(sPathOnPhone, True)
            If tn.Length > 0 Then
                tn(0).EnsureVisible()
                trvFolders.SelectedNode = tn(0)
                bReturn = True
            Else
                'we couldn't find it
                bReturn = False
            End If

        Else
            'we found it first try
            bSupressFiles = False
            tn(0).EnsureVisible()
            trvFolders.SelectedNode = tn(0)
            bReturn = True
        End If

        selectSpecificPath = bReturn
    End Function


    'SYSTEM CREATED EVENTS

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo ErrorHandler
        'Dim myLog As New EventLog()


        'If Not EventLog.SourceExists("iPhoneBrowser") Then EventLog.CreateEventSource("iPhoneBrowser", "Application")

        ' Create an EventLog instance and assign its source.
        'myLog.Source = "iPhoneBrowser"

        ' Write an informational entry to the event log.    
        'myLog.WriteEntry("Writing to event log.")

        'myLog.WriteEntry("Loading iPhoneBrowser: frmMain_Load, initializing Manzana iPhone")

        'initialize our object
        iPhoneInterface = New Manzana.iPhone

        'myLog.WriteEntry("Loading iPhoneBrowser: frmMain_Load, compeleted initializing Manzana iPhone, loading variables and resizing forms")

        'Use the new retarded way to get the path to the application
        APP_PATH = Microsoft.VisualBasic.Left(Application.ExecutablePath, InStrRev(Application.ExecutablePath, "\"))
        FILE_TEMPORARY_VIEWER = APP_PATH & FILE_TEMPORARY_VIEWER

        tlbProgressBar.Visible = False
        Me.Text = "iPhoneBrowser (v" & VERSION & ")"

        'setup the event handlers
        'myLog.WriteEntry("Loading iPhoneBrowser: frmMain_Load, compeleted loading variables and resizing forms, setting up event handlers")
        AddHandler iPhoneInterface.Connect, AddressOf iPhoneConnected_EventHandler
        AddHandler iPhoneInterface.Disconnect, AddressOf iPhoneDisconnected_EventHandler

        'myLog.WriteEntry("Loading iPhoneBrowser: frmMain_Load, complete")
        Exit Sub

ErrorHandler:
        'myLog.WriteEntry("Loading iPhoneBrowser: Exception occured: " & Err.Description)
        MsgBox("An error has occured during load.  Error text: " & Err.Description & ".  The program will now exit, sorry. Please report this online.", MsgBoxStyle.Critical, "Error!")
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItemLargeIcons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemLargeIcons.Click
        lstFiles.View = View.LargeIcon
        ToolStripMenuItemLargeIcons.Checked = True
        ToolStripMenuItemDetails.Checked = False

    End Sub

    Private Sub ToolStripMenuItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemDetails.Click
        lstFiles.View = View.Details
        ToolStripMenuItemLargeIcons.Checked = False
        ToolStripMenuItemDetails.Checked = True
    End Sub

    Private Sub ToolStripMenuItemExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemExit.Click
        'time to go
        Me.Close()
    End Sub

    Private Sub trvFolders_AfterExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFolders.AfterExpand
        trvFolders.SelectedNode = e.Node
    End Sub

    Private Sub trvFolders_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFolders.AfterSelect
        loadChildrenFolders()
        trvFolders.SelectedNode.Expand()
        loadFiles()
    End Sub

    Private Sub lstFiles_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstFiles.DragDrop
        Dim sFiles() As String
        Dim i As Integer, initFolder As String

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            ' Assign the files to an array.
            sFiles = e.Data.GetData(DataFormats.FileDrop)

            initFolder = getSelectedFolder()
            ' Loop through the array 
            For i = 0 To sFiles.Length - 1
                'copy the file to the phone
                copyToPhone(sFiles(i), initFolder)
            Next
            StatusNormal("")

            selectSpecificPath(initFolder) ' fix up tree view

            'refresh the list view
            loadFiles()
        End If
    End Sub

    Private Sub lstFiles_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstFiles.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub lstFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        Dim sFile As String, sTemp As String, sr As StreamReader, picOK As Boolean

        'unlock the file so we can load another
        If wasQTpreview Then
            qtPlugin.FileName = "" 'to unlock it
            Kill(Replace(QTpreviewFile, "localhost\", ""))
            wasQTpreview = False
        End If

        'only do this if something is selected
        If lstFiles.SelectedItems.Count > 0 Then
            StatusNormal("Loading file...")

            'get the name and full path of the file selected
            sFile = getSelectedFilename()

            'and only if it is less than a big file size
            If lstFiles.SelectedItems(0).SubItems(1).Text.EndsWith("kb") Or lstFiles.SelectedItems(0).SubItems(1).Text.EndsWith("bytes") Then

                'grab the extension
                sTemp = Mid(sFile, InStrRev(sFile, "/") + 1) 'so we skip over the files without extensions but inside folders with extensions
                If InStr(sTemp, ".") > 0 Then
                    sTemp = Mid(sFile, InStrRev(sFile, "."))
                Else
                    sTemp = ".temp"
                End If

                If copyFromPhone(sFile, FILE_TEMPORARY_VIEWER & sTemp) Then
                    'default to off for everything
                    picFileDetails.Visible = False
                    txtFileDetails.Visible = False
                    qtPlugin.Visible = False

                    grpDetails.Text = "File details for '" & sFile & "'."
                    txtFileDetails.Text = "<UNKNOWN FILE FORMAT>"

                    'now we have a temporary file, lets try to read it
                    Select Case lstFiles.SelectedItems(0).ImageIndex
                        Case IMAGE_FILE_TEXT, IMAGE_FILE_UNKOWN, IMAGE_FILE_DATABASE

                            'clean up the temp file
                            sr = My.Computer.FileSystem.OpenTextFileReader(FILE_TEMPORARY_VIEWER & sTemp)
                            txtFileDetails.Text = Replace(sr.ReadToEnd(), Chr(10), vbCrLf)
                            sr.Close()

                            txtFileDetails.Visible = True

                        Case IMAGE_FILE_IMAGE
                            picFileDetails.ImageLocation = FILE_TEMPORARY_VIEWER & sTemp
                            Try
                                picOK = True
                                picFileDetails.Load()
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
                            QTpreviewFile = FILE_TEMPORARY_VIEWER & sTemp
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
            Else
                'the file is too big to be loaded in the preview
                StatusWarning("The file " & sFile & " is too large to be previewed")
            End If

        End If
    End Sub

    Private Sub menuRightSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightSaveAs.Click
        Dim sSaveAsFilename As String, sFileFromPhone As String

        If lstFiles.SelectedItems.Count > 0 Then
            'show them the save dialog
            fileSaveDialog.Title = "Save " & lstFiles.SelectedItems(0).Text & " as ..."
            fileSaveDialog.FileName = lstFiles.SelectedItems(0).Text
            If fileSaveDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                sSaveAsFilename = fileSaveDialog.FileName
                sFileFromPhone = getSelectedFilename()

                copyFromPhone(sFileFromPhone, sSaveAsFilename)
            End If

        End If
    End Sub

    Private Sub menuRightBackupFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightBackupFile.Click
        'grab the file, see if it is different from the one we have already (if any) then save it with an extra extension
        Dim sSourcePath As String, sSourceFile As String

        sSourcePath = getSelectedFolder()
        sSourceFile = lstFiles.SelectedItems(0).Text
        backupFileFromPhone(sSourcePath, sSourceFile)

    End Sub

    Private Sub menuRightReplaceFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightReplaceFile.Click
        'replace the selected file with one of their own
        Dim sSourceFilename As String, sFileToPhone As String

        If lstFiles.SelectedItems.Count > 0 Then

            'show them the open dialog
            fileOpenDialog.Title = "Select a file to replace " & lstFiles.SelectedItems(0).Text & " with ..."
            fileOpenDialog.FileName = lstFiles.SelectedItems(0).Text
            If fileOpenDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                sSourceFilename = fileOpenDialog.FileName
                'replace the selected file with the source one
                sFileToPhone = getSelectedFilename()
                'this function also makes a backup
                copyToPhone(sSourceFilename, sFileToPhone)
                'refresh the list view
                loadFiles()
            End If

        End If

    End Sub

    Private Sub menuRightDeleteFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuRightDeleteFile.Click
        'delete the selected file
        Dim sDeleteFilename As String

        If lstFiles.SelectedItems.Count > 0 Then

            'Make them confirm it
            sDeleteFilename = getSelectedFilename()
            If MsgBox("Are you sure you wish to delete the '" & sDeleteFilename & "' file? (A backup copy will automatically be created)", MsgBoxStyle.YesNo, "Delete file?") = MsgBoxResult.Yes Then

                delFromPhone(sDeleteFilename)

                'refresh the list view
                loadFiles()

            End If

        End If

    End Sub

    Private Sub ToolStripMenuItemCleanUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemCleanUp.Click
        'this function goes through the backup files and cleans them, comparing them to others in the same folder with the same name
        Dim sMessage As String

        sMessage = "This function goes through all of your backup files and attempts to remove the duplicates to save space." & vbCrLf & _
            "You really don't have to do this, it is better to have more backups then less...but press ok if you've got a lot of backups."

        If MsgBox(sMessage, MsgBoxStyle.OkCancel, "Confirm cleanup") = MsgBoxResult.Ok Then
            sMessage = "Function not implemented yet, sorry :)"
            MsgBox(sMessage, MsgBoxStyle.OkOnly, "Whoops")
        End If


    End Sub

    Private Sub ToolStripMenuItemViewBackups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemViewBackups.Click
        Shell("explorer """ & BACKUP_DIRECTORY & """", AppWinStyle.NormalFocus)
    End Sub

    Private Sub toolStripGoTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles toolStripGoTo1.Click, toolStripGoTo2.Click, _
toolStripGoTo3.Click, toolStripGoTo4.Click, toolStripGoTo5.Click, toolStripGoTo6.Click, toolStripGoTo7.Click, toolStripGoTo8.Click, toolStripGoTo9.Click, _
toolStripGoTo10.Click, toolStripGoTo11.Click, toolStripGoTo12.Click, toolStripGoTo13.Click, toolStripGoTo14.Click, toolStripGoTo15.Click, toolStripGoTo16.Click, _
toolStripGoTo17.Click, toolStripGoTo18.Click, toolStripGoTo19.Click

        Dim sPath As String, ts As ToolStripMenuItem
        ts = sender
        sPath = ts.Tag()

        If Not selectSpecificPath(sPath) Then
            MsgBox("Error: The program could not find the path '" & sPath & "' on your iPhone.  Have you successfully used jailbreak?", MsgBoxStyle.Critical)
        End If

    End Sub

    Private Sub ToolStripMenuItemNewFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemNewFolder.Click
        Dim sPath As String, sNewFolder As String, bValid As Boolean = False

        sPath = getSelectedFolder()
        sNewFolder = InputBox("Please enter the name of the new folder you wish to create.  The new path of this folder will be:" & sPath & "<new folder>, and the name is case-sensitive!", "Create Folder In " & sPath, "NewFolder")
        sPath = sPath & sNewFolder

        'make sure it is valid
        If sNewFolder = "" Then
            ' user canceled
        ElseIf sNewFolder = "NewFolder" Then
            MsgBox("You didn't change the default name, I am pretty sure you don't want a 'NewFolder' folder name...", MsgBoxStyle.Information, "Canceled")
        ElseIf InStr(sNewFolder, " ") > 0 Or InStr(sNewFolder, "/") > 0 Or InStr(sNewFolder, "\") > 0 Or _
                InStr(sNewFolder, "*") > 0 Or InStr(sNewFolder, "?") > 0 Or InStr(sNewFolder, "[") > 0 Or InStr(sNewFolder, "]") > 0 Then
            MsgBox("No spaces, slashes or other special characters are allowed in the folder name.", MsgBoxStyle.Information, "Canceled")
        ElseIf iPhoneInterface.Exists(sPath) Then
            MsgBox("The path '" & sPath & "' already exists.", MsgBoxStyle.Information, "Canceled")
        Else
            bValid = True
        End If

        If bValid Then
            'lets create the directory
            If createDirectoryOnPhone(sPath) Then
                'it created successfully
                selectSpecificPath(sPath)
            Else
                'it failed
                MsgBox("The path '" & sPath & "' failed to create due to an unknown interface failure.", MsgBoxStyle.Information, "Canceled")
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItemDeleteFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemDeleteFolder.Click
        Dim tNode As TreeNode, sPath As String, sNewFolder As String, bValid As Boolean = False, findNode() As TreeNode

        If lstFiles.Items.Count > 0 Then
            MsgBox("You must delete all of the files in this folder first, before trying to remove the folder.")

        Else
            tNode = trvFolders.SelectedNode
            sPath = getSelectedFolder()

            'trim off the trailing /, we don't need it
            sPath = Microsoft.VisualBasic.Left(sPath, Len(sPath) - 1)

            If tNode.Nodes.Count > 0 Then
                MsgBox("You must delete all of the folders in this folder as well, before trying to remove this folder. The folder has to be empty to remove it.")
            Else
                If MsgBox("Are you absolutely sure you wish to delete this folder (" & sPath & ")?  This cannot be undone.  " & vbCrLf & vbCrLf & "[For your safety, this will not delete folders that have files in them, the files must be deleted first]", MsgBoxStyle.YesNo, "Are you Sure?") = MsgBoxResult.Yes Then

                    iPhoneInterface.DeleteDirectory(sPath)

                    sNewFolder = Microsoft.VisualBasic.Left(sPath, InStrRev(sPath, "/") - 1)
                    findNode = trvFolders.Nodes.Find(sPath, True)
                    If findNode.Length = 0 Then
                        'could not find it for some reason, refresh the whole thing
                        refreshFolders()
                    Else
                        'delete the specific one 
                        trvFolders.Nodes.Remove(findNode(0))
                        'selectSpecificPath(sNewFolder)
                    End If
                End If
            End If
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

    Private Sub lstFiles_ColumnClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lstFiles.ColumnClick
        ' Set the ListViewItemSorter property to a new ListViewItemComparer 
        ' object. Setting this property immediately sorts the 
        ' ListView using the ListViewItemComparer object.
        If lstFilesSortOrder = SortOrder.None Or lstFilesSortOrder = SortOrder.Descending Then
            lstFilesSortOrder = SortOrder.Ascending
        Else
            lstFilesSortOrder = SortOrder.Descending
        End If
        If lstFiles.Columns(e.Column).Text = "Size" Then
            Me.lstFiles.ListViewItemSorter = New ListViewSizeComparer(e.Column, lstFilesSortOrder)
        Else
            Me.lstFiles.ListViewItemSorter = New ListViewStringComparer(e.Column, lstFilesSortOrder)
        End If
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
