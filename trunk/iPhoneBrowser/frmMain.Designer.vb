<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    '<System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        With My.Settings
            .ConfirmDeletions = bConfirmDeletions
            .PCToiPhonePNG = bConvertToiPhonePNG
            .iPhoneToPCPNG = bConvertToPNG
            .ShowPreviews = bShowPreview
            .IgnoreThumbsFile = bIgnoreThumbsFile
            .IgnoreDSStoreFile = bIgnoreDSStoreFile
            .ShowGroups = bShowGroups
            .DontBackupEver = bDontBackupEver
            .FavNames = favNames
            .FavPaths = favPaths
        End With
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.imgFolders = New System.Windows.Forms.ImageList(Me.components)
        Me.imgFilesLarge = New System.Windows.Forms.ImageList(Me.components)
        Me.imgFilesSmall = New System.Windows.Forms.ImageList(Me.components)
        Me.tlbStatusStrip = New System.Windows.Forms.StatusStrip
        Me.tlbStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.tlbProgress0 = New System.Windows.Forms.ToolStripProgressBar
        Me.tlbProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.toolStrip = New System.Windows.Forms.ToolStrip
        Me.ToolStripMenuItemFile = New System.Windows.Forms.ToolStripDropDownButton
        Me.ToolStripMenuItemCleanUp = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemViewBackups = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.menuSaveSummerboardTheme = New System.Windows.Forms.ToolStripMenuItem
        Me.AsSummerboardFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AsPXLPackageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.AsCustomizeFoldersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AsPXLPackageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItemExit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuEdit = New System.Windows.Forms.ToolStripDropDownButton
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConfirmDeletionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DontBackupRunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DontBackupEverToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IgnoreThumbsdbToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IgnoreDSStoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.ConvertPNGsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IPhoneToPCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PCToIPhoneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConvertBothToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowPreviewsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PictureBackgroundToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BlackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GrayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WhiteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuView = New System.Windows.Forms.ToolStripDropDownButton
        Me.ToolStripMenuItemDetails = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemLargeIcons = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdSmallIcons = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdShowGroups = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuGoTo = New System.Windows.Forms.ToolStripDropDownButton
        Me.toolStripGoTo1 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.toolStripGoTo3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuStdApps = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo4 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo6 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo13 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo14 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo8 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo7 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo5 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo9 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo10 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo12 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo15 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo11 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo16 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo17 = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo18 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuThirdPartyApps = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.DockSwapDocksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EBooksToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FrotzGamesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdGBAROMs = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.InstallerPackageSourcesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ISwitcherThemesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NESROMSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TTRToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WeDictDictionariesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.CameraRollToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripGoTo19 = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuFavorites = New System.Windows.Forms.ToolStripDropDownButton
        Me.AddToFavoritesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OrganizeFavoritesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripSeparator
        Me.menuRightClickFiles = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuRightSaveAs = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.menuRightBackupFile = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRightRestoreFile = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemLoading = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.cmdRenameFile = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRightReplaceFile = New System.Windows.Forms.ToolStripMenuItem
        Me.menuRightDeleteFile = New System.Windows.Forms.ToolStripMenuItem
        Me.fileSaveDialog = New System.Windows.Forms.SaveFileDialog
        Me.fileOpenDialog = New System.Windows.Forms.OpenFileDialog
        Me.splMain = New System.Windows.Forms.SplitContainer
        Me.grpFolders = New System.Windows.Forms.GroupBox
        Me.trvFolders = New System.Windows.Forms.TreeView
        Me.splFiles = New System.Windows.Forms.SplitContainer
        Me.grpFiles = New System.Windows.Forms.GroupBox
        Me.lstFiles = New System.Windows.Forms.ListView
        Me.cohFilename = New System.Windows.Forms.ColumnHeader
        Me.cohSize = New System.Windows.Forms.ColumnHeader
        Me.cohFiletype = New System.Windows.Forms.ColumnHeader
        Me.grpDetails = New System.Windows.Forms.GroupBox
        Me.picFileDetails = New System.Windows.Forms.PictureBox
        Me.txtFileDetails = New System.Windows.Forms.TextBox
        Me.qtPlugin = New AxQTOControlLib.AxQTControl
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnPreview = New System.Windows.Forms.Button
        Me.chkPreviewEnabled = New System.Windows.Forms.CheckBox
        Me.menuRightClickFolders = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemNewFolder = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.BackupFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItemSaveFolderIn = New System.Windows.Forms.ToolStripMenuItem
        Me.cmdRenameFolder = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItemDeleteFolder = New System.Windows.Forms.ToolStripMenuItem
        Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
        Me.tlbStatusStrip.SuspendLayout()
        Me.toolStrip.SuspendLayout()
        Me.menuRightClickFiles.SuspendLayout()
        Me.splMain.Panel1.SuspendLayout()
        Me.splMain.Panel2.SuspendLayout()
        Me.splMain.SuspendLayout()
        Me.grpFolders.SuspendLayout()
        Me.splFiles.Panel1.SuspendLayout()
        Me.splFiles.Panel2.SuspendLayout()
        Me.splFiles.SuspendLayout()
        Me.grpFiles.SuspendLayout()
        Me.grpDetails.SuspendLayout()
        CType(Me.picFileDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.qtPlugin, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.menuRightClickFolders.SuspendLayout()
        Me.SuspendLayout()
        '
        'imgFolders
        '
        Me.imgFolders.ImageStream = CType(resources.GetObject("imgFolders.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgFolders.TransparentColor = System.Drawing.Color.Transparent
        Me.imgFolders.Images.SetKeyName(0, "Folder.ico")
        Me.imgFolders.Images.SetKeyName(1, "folderopen.ico")
        Me.imgFolders.Images.SetKeyName(2, "folder_closed.gif")
        Me.imgFolders.Images.SetKeyName(3, "folder_open.gif")
        '
        'imgFilesLarge
        '
        Me.imgFilesLarge.ImageStream = CType(resources.GetObject("imgFilesLarge.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgFilesLarge.TransparentColor = System.Drawing.Color.Transparent
        Me.imgFilesLarge.Images.SetKeyName(0, "")
        Me.imgFilesLarge.Images.SetKeyName(1, "")
        Me.imgFilesLarge.Images.SetKeyName(2, "")
        Me.imgFilesLarge.Images.SetKeyName(3, "")
        Me.imgFilesLarge.Images.SetKeyName(4, "")
        Me.imgFilesLarge.Images.SetKeyName(5, "")
        Me.imgFilesLarge.Images.SetKeyName(6, "")
        Me.imgFilesLarge.Images.SetKeyName(7, "RingToneLarge.gif")
        '
        'imgFilesSmall
        '
        Me.imgFilesSmall.ImageStream = CType(resources.GetObject("imgFilesSmall.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgFilesSmall.TransparentColor = System.Drawing.Color.Transparent
        Me.imgFilesSmall.Images.SetKeyName(0, "")
        Me.imgFilesSmall.Images.SetKeyName(1, "")
        Me.imgFilesSmall.Images.SetKeyName(2, "")
        Me.imgFilesSmall.Images.SetKeyName(3, "")
        Me.imgFilesSmall.Images.SetKeyName(4, "")
        Me.imgFilesSmall.Images.SetKeyName(5, "")
        Me.imgFilesSmall.Images.SetKeyName(6, "")
        Me.imgFilesSmall.Images.SetKeyName(7, "RingToneSmall.gif")
        '
        'tlbStatusStrip
        '
        Me.tlbStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tlbStatusLabel, Me.tlbProgress0, Me.tlbProgressBar})
        Me.tlbStatusStrip.Location = New System.Drawing.Point(0, 575)
        Me.tlbStatusStrip.Name = "tlbStatusStrip"
        Me.tlbStatusStrip.ShowItemToolTips = True
        Me.tlbStatusStrip.Size = New System.Drawing.Size(747, 22)
        Me.tlbStatusStrip.TabIndex = 3
        Me.tlbStatusStrip.Text = "StatusStrip1"
        '
        'tlbStatusLabel
        '
        Me.tlbStatusLabel.AutoSize = False
        Me.tlbStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.tlbStatusLabel.Image = Global.iPhoneBrowser.My.Resources.Resources.warning
        Me.tlbStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tlbStatusLabel.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.tlbStatusLabel.Name = "tlbStatusLabel"
        Me.tlbStatusLabel.Size = New System.Drawing.Size(568, 17)
        Me.tlbStatusLabel.Spring = True
        Me.tlbStatusLabel.Text = "Please plug in your iPhone via USB.  If it is already plugged in, disconnect and " & _
            "reconnect it."
        Me.tlbStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.tlbStatusLabel.ToolTipText = "This section displays the current status."
        '
        'tlbProgress0
        '
        Me.tlbProgress0.AutoSize = False
        Me.tlbProgress0.BackColor = System.Drawing.SystemColors.Desktop
        Me.tlbProgress0.Margin = New System.Windows.Forms.Padding(0, 4, 4, 4)
        Me.tlbProgress0.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.tlbProgress0.Name = "tlbProgress0"
        Me.tlbProgress0.Size = New System.Drawing.Size(75, 14)
        Me.tlbProgress0.Step = 1
        Me.tlbProgress0.ToolTipText = "Progress 0"
        '
        'tlbProgressBar
        '
        Me.tlbProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tlbProgressBar.AutoSize = False
        Me.tlbProgressBar.Margin = New System.Windows.Forms.Padding(0, 4, 10, 4)
        Me.tlbProgressBar.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.tlbProgressBar.MergeIndex = 2
        Me.tlbProgressBar.Name = "tlbProgressBar"
        Me.tlbProgressBar.Size = New System.Drawing.Size(75, 14)
        Me.tlbProgressBar.Step = 1
        Me.tlbProgressBar.ToolTipText = "Progress 1"
        '
        'toolStrip
        '
        Me.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.toolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemFile, Me.mnuEdit, Me.mnuView, Me.mnuGoTo, Me.mnuFavorites})
        Me.toolStrip.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip.Name = "toolStrip"
        Me.toolStrip.Padding = New System.Windows.Forms.Padding(0)
        Me.toolStrip.Size = New System.Drawing.Size(747, 25)
        Me.toolStrip.TabIndex = 4
        '
        'ToolStripMenuItemFile
        '
        Me.ToolStripMenuItemFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripMenuItemFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemCleanUp, Me.ToolStripMenuItemViewBackups, Me.ToolStripSeparator7, Me.menuSaveSummerboardTheme, Me.ToolStripMenuItem4, Me.ToolStripSeparator3, Me.ToolStripMenuItemExit})
        Me.ToolStripMenuItemFile.Image = CType(resources.GetObject("ToolStripMenuItemFile.Image"), System.Drawing.Image)
        Me.ToolStripMenuItemFile.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile"
        Me.ToolStripMenuItemFile.Size = New System.Drawing.Size(66, 22)
        Me.ToolStripMenuItemFile.Text = "&Functions"
        '
        'ToolStripMenuItemCleanUp
        '
        Me.ToolStripMenuItemCleanUp.Enabled = False
        Me.ToolStripMenuItemCleanUp.Name = "ToolStripMenuItemCleanUp"
        Me.ToolStripMenuItemCleanUp.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemCleanUp.Text = "&Clean Up Backup Files"
        '
        'ToolStripMenuItemViewBackups
        '
        Me.ToolStripMenuItemViewBackups.Name = "ToolStripMenuItemViewBackups"
        Me.ToolStripMenuItemViewBackups.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemViewBackups.Text = "&View Backup Files"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(186, 6)
        '
        'menuSaveSummerboardTheme
        '
        Me.menuSaveSummerboardTheme.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AsSummerboardFolderToolStripMenuItem, Me.AsPXLPackageToolStripMenuItem})
        Me.menuSaveSummerboardTheme.Name = "menuSaveSummerboardTheme"
        Me.menuSaveSummerboardTheme.Size = New System.Drawing.Size(189, 22)
        Me.menuSaveSummerboardTheme.Text = "&Summerboard"
        '
        'AsSummerboardFolderToolStripMenuItem
        '
        Me.AsSummerboardFolderToolStripMenuItem.Name = "AsSummerboardFolderToolStripMenuItem"
        Me.AsSummerboardFolderToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.AsSummerboardFolderToolStripMenuItem.Text = "Save as Summerboard &Folder"
        '
        'AsPXLPackageToolStripMenuItem
        '
        Me.AsPXLPackageToolStripMenuItem.Enabled = False
        Me.AsPXLPackageToolStripMenuItem.Name = "AsPXLPackageToolStripMenuItem"
        Me.AsPXLPackageToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
        Me.AsPXLPackageToolStripMenuItem.Text = "Save as &PXL Package"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AsCustomizeFoldersToolStripMenuItem, Me.AsPXLPackageToolStripMenuItem1})
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItem4.Text = "&Customize"
        '
        'AsCustomizeFoldersToolStripMenuItem
        '
        Me.AsCustomizeFoldersToolStripMenuItem.Name = "AsCustomizeFoldersToolStripMenuItem"
        Me.AsCustomizeFoldersToolStripMenuItem.Size = New System.Drawing.Size(220, 22)
        Me.AsCustomizeFoldersToolStripMenuItem.Text = "Save as Customize &Folder..."
        '
        'AsPXLPackageToolStripMenuItem1
        '
        Me.AsPXLPackageToolStripMenuItem1.Enabled = False
        Me.AsPXLPackageToolStripMenuItem1.Name = "AsPXLPackageToolStripMenuItem1"
        Me.AsPXLPackageToolStripMenuItem1.Size = New System.Drawing.Size(220, 22)
        Me.AsPXLPackageToolStripMenuItem1.Text = "Save as &PXL Package..."
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(186, 6)
        '
        'ToolStripMenuItemExit
        '
        Me.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit"
        Me.ToolStripMenuItemExit.Size = New System.Drawing.Size(189, 22)
        Me.ToolStripMenuItemExit.Text = "E&xit"
        '
        'mnuEdit
        '
        Me.mnuEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem})
        Me.mnuEdit.Image = CType(resources.GetObject("mnuEdit.Image"), System.Drawing.Image)
        Me.mnuEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuEdit.Name = "mnuEdit"
        Me.mnuEdit.Size = New System.Drawing.Size(38, 22)
        Me.mnuEdit.Text = "&Edit"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfirmDeletionsToolStripMenuItem, Me.DontBackupRunToolStripMenuItem, Me.DontBackupEverToolStripMenuItem, Me.IgnoreThumbsdbToolStripMenuItem, Me.IgnoreDSStoreToolStripMenuItem, Me.ToolStripSeparator11, Me.ConvertPNGsToolStripMenuItem, Me.ToolStripSeparator8, Me.ShowPreviewsToolStripMenuItem, Me.PictureBackgroundToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'ConfirmDeletionsToolStripMenuItem
        '
        Me.ConfirmDeletionsToolStripMenuItem.Checked = True
        Me.ConfirmDeletionsToolStripMenuItem.CheckOnClick = True
        Me.ConfirmDeletionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ConfirmDeletionsToolStripMenuItem.Name = "ConfirmDeletionsToolStripMenuItem"
        Me.ConfirmDeletionsToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.ConfirmDeletionsToolStripMenuItem.Text = "Confirm Deletions"
        '
        'DontBackupRunToolStripMenuItem
        '
        Me.DontBackupRunToolStripMenuItem.CheckOnClick = True
        Me.DontBackupRunToolStripMenuItem.Name = "DontBackupRunToolStripMenuItem"
        Me.DontBackupRunToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.DontBackupRunToolStripMenuItem.Text = "No Automatic Backup (this run)"
        '
        'DontBackupEverToolStripMenuItem
        '
        Me.DontBackupEverToolStripMenuItem.CheckOnClick = True
        Me.DontBackupEverToolStripMenuItem.Enabled = False
        Me.DontBackupEverToolStripMenuItem.Name = "DontBackupEverToolStripMenuItem"
        Me.DontBackupEverToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.DontBackupEverToolStripMenuItem.Text = "No Automatic Backup (Startup default)"
        '
        'IgnoreThumbsdbToolStripMenuItem
        '
        Me.IgnoreThumbsdbToolStripMenuItem.Checked = True
        Me.IgnoreThumbsdbToolStripMenuItem.CheckOnClick = True
        Me.IgnoreThumbsdbToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IgnoreThumbsdbToolStripMenuItem.Name = "IgnoreThumbsdbToolStripMenuItem"
        Me.IgnoreThumbsdbToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.IgnoreThumbsdbToolStripMenuItem.Text = "Ignore Thumbs.db"
        '
        'IgnoreDSStoreToolStripMenuItem
        '
        Me.IgnoreDSStoreToolStripMenuItem.Checked = True
        Me.IgnoreDSStoreToolStripMenuItem.CheckOnClick = True
        Me.IgnoreDSStoreToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IgnoreDSStoreToolStripMenuItem.Name = "IgnoreDSStoreToolStripMenuItem"
        Me.IgnoreDSStoreToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.IgnoreDSStoreToolStripMenuItem.Text = "Ignore .DS_Store"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(267, 6)
        '
        'ConvertPNGsToolStripMenuItem
        '
        Me.ConvertPNGsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IPhoneToPCToolStripMenuItem, Me.PCToIPhoneToolStripMenuItem, Me.ConvertBothToolStripMenuItem})
        Me.ConvertPNGsToolStripMenuItem.Name = "ConvertPNGsToolStripMenuItem"
        Me.ConvertPNGsToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.ConvertPNGsToolStripMenuItem.Text = "Convert PNGs"
        '
        'IPhoneToPCToolStripMenuItem
        '
        Me.IPhoneToPCToolStripMenuItem.Checked = True
        Me.IPhoneToPCToolStripMenuItem.CheckOnClick = True
        Me.IPhoneToPCToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IPhoneToPCToolStripMenuItem.Name = "IPhoneToPCToolStripMenuItem"
        Me.IPhoneToPCToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.IPhoneToPCToolStripMenuItem.Text = "iPhone to PC"
        '
        'PCToIPhoneToolStripMenuItem
        '
        Me.PCToIPhoneToolStripMenuItem.CheckOnClick = True
        Me.PCToIPhoneToolStripMenuItem.Enabled = False
        Me.PCToIPhoneToolStripMenuItem.Name = "PCToIPhoneToolStripMenuItem"
        Me.PCToIPhoneToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.PCToIPhoneToolStripMenuItem.Text = "PC to iPhone"
        '
        'ConvertBothToolStripMenuItem
        '
        Me.ConvertBothToolStripMenuItem.Enabled = False
        Me.ConvertBothToolStripMenuItem.Name = "ConvertBothToolStripMenuItem"
        Me.ConvertBothToolStripMenuItem.Size = New System.Drawing.Size(149, 22)
        Me.ConvertBothToolStripMenuItem.Text = "Convert Both"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(267, 6)
        '
        'ShowPreviewsToolStripMenuItem
        '
        Me.ShowPreviewsToolStripMenuItem.Checked = True
        Me.ShowPreviewsToolStripMenuItem.CheckOnClick = True
        Me.ShowPreviewsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowPreviewsToolStripMenuItem.Name = "ShowPreviewsToolStripMenuItem"
        Me.ShowPreviewsToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.ShowPreviewsToolStripMenuItem.Text = "Show Previews"
        '
        'PictureBackgroundToolStripMenuItem
        '
        Me.PictureBackgroundToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BlackToolStripMenuItem, Me.GrayToolStripMenuItem, Me.WhiteToolStripMenuItem})
        Me.PictureBackgroundToolStripMenuItem.Name = "PictureBackgroundToolStripMenuItem"
        Me.PictureBackgroundToolStripMenuItem.Size = New System.Drawing.Size(270, 22)
        Me.PictureBackgroundToolStripMenuItem.Text = "Picture Background"
        '
        'BlackToolStripMenuItem
        '
        Me.BlackToolStripMenuItem.CheckOnClick = True
        Me.BlackToolStripMenuItem.Name = "BlackToolStripMenuItem"
        Me.BlackToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.BlackToolStripMenuItem.Text = "Black"
        '
        'GrayToolStripMenuItem
        '
        Me.GrayToolStripMenuItem.CheckOnClick = True
        Me.GrayToolStripMenuItem.Name = "GrayToolStripMenuItem"
        Me.GrayToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.GrayToolStripMenuItem.Text = "Gray"
        '
        'WhiteToolStripMenuItem
        '
        Me.WhiteToolStripMenuItem.CheckOnClick = True
        Me.WhiteToolStripMenuItem.Name = "WhiteToolStripMenuItem"
        Me.WhiteToolStripMenuItem.Size = New System.Drawing.Size(113, 22)
        Me.WhiteToolStripMenuItem.Text = "White"
        '
        'mnuView
        '
        Me.mnuView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemDetails, Me.ToolStripMenuItemLargeIcons, Me.cmdSmallIcons, Me.ToolStripMenuItem8, Me.cmdShowGroups})
        Me.mnuView.Image = CType(resources.GetObject("mnuView.Image"), System.Drawing.Image)
        Me.mnuView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuView.Name = "mnuView"
        Me.mnuView.Size = New System.Drawing.Size(42, 22)
        Me.mnuView.Text = "&View"
        '
        'ToolStripMenuItemDetails
        '
        Me.ToolStripMenuItemDetails.Checked = True
        Me.ToolStripMenuItemDetails.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ToolStripMenuItemDetails.Name = "ToolStripMenuItemDetails"
        Me.ToolStripMenuItemDetails.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItemDetails.Text = "&Details"
        '
        'ToolStripMenuItemLargeIcons
        '
        Me.ToolStripMenuItemLargeIcons.Name = "ToolStripMenuItemLargeIcons"
        Me.ToolStripMenuItemLargeIcons.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItemLargeIcons.Text = "&Large Icons"
        '
        'cmdSmallIcons
        '
        Me.cmdSmallIcons.Name = "cmdSmallIcons"
        Me.cmdSmallIcons.Size = New System.Drawing.Size(152, 22)
        Me.cmdSmallIcons.Text = "&Small Icons"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(149, 6)
        '
        'cmdShowGroups
        '
        Me.cmdShowGroups.Checked = Global.iPhoneBrowser.My.MySettings.Default.ShowGroups
        Me.cmdShowGroups.Name = "cmdShowGroups"
        Me.cmdShowGroups.Size = New System.Drawing.Size(152, 22)
        Me.cmdShowGroups.Text = "Show &Groups"
        '
        'mnuGoTo
        '
        Me.mnuGoTo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuGoTo.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripGoTo1, Me.toolStripGoTo2, Me.ToolStripSeparator5, Me.toolStripGoTo3, Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.ToolStripSeparator4, Me.mnuStdApps, Me.mnuThirdPartyApps, Me.ToolStripSeparator6, Me.CameraRollToolStripMenuItem, Me.toolStripGoTo19})
        Me.mnuGoTo.Enabled = False
        Me.mnuGoTo.Image = CType(resources.GetObject("mnuGoTo.Image"), System.Drawing.Image)
        Me.mnuGoTo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuGoTo.Name = "mnuGoTo"
        Me.mnuGoTo.Size = New System.Drawing.Size(86, 22)
        Me.mnuGoTo.Tag = "ar"
        Me.mnuGoTo.Text = "&Goto Location"
        '
        'toolStripGoTo1
        '
        Me.toolStripGoTo1.Name = "toolStripGoTo1"
        Me.toolStripGoTo1.Size = New System.Drawing.Size(244, 22)
        Me.toolStripGoTo1.Tag = "/Library/Ringtones"
        Me.toolStripGoTo1.Text = "&Ringtones"
        '
        'toolStripGoTo2
        '
        Me.toolStripGoTo2.Name = "toolStripGoTo2"
        Me.toolStripGoTo2.Size = New System.Drawing.Size(244, 22)
        Me.toolStripGoTo2.Tag = "/System/Library/Audio/UISounds"
        Me.toolStripGoTo2.Text = "&UI Sounds"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(241, 6)
        '
        'toolStripGoTo3
        '
        Me.toolStripGoTo3.Name = "toolStripGoTo3"
        Me.toolStripGoTo3.Size = New System.Drawing.Size(244, 22)
        Me.toolStripGoTo3.Tag = "/System/Library/CoreServices/SpringBoard.app"
        Me.toolStripGoTo3.Text = "&Springboard Images and Settings"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(244, 22)
        Me.ToolStripMenuItem1.Tag = "/Library/Themes"
        Me.ToolStripMenuItem1.Text = "WinterBoard Themes"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(244, 22)
        Me.ToolStripMenuItem2.Tag = "/var/mobile/Library/Summerboard/Themes"
        Me.ToolStripMenuItem2.Text = "SummerBoard &Themes"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(241, 6)
        '
        'mnuStdApps
        '
        Me.mnuStdApps.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripGoTo4, Me.toolStripGoTo6, Me.toolStripGoTo13, Me.toolStripGoTo14, Me.toolStripGoTo8, Me.toolStripGoTo7, Me.toolStripGoTo5, Me.toolStripGoTo9, Me.toolStripGoTo10, Me.toolStripGoTo12, Me.toolStripGoTo15, Me.toolStripGoTo11, Me.toolStripGoTo16, Me.toolStripGoTo17, Me.toolStripGoTo18})
        Me.mnuStdApps.Name = "mnuStdApps"
        Me.mnuStdApps.Size = New System.Drawing.Size(244, 22)
        Me.mnuStdApps.Text = "Standard &Applications"
        '
        'toolStripGoTo4
        '
        Me.toolStripGoTo4.Name = "toolStripGoTo4"
        Me.toolStripGoTo4.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo4.Tag = "/Applications/Calculator.app"
        Me.toolStripGoTo4.Text = "&Calculator Icon and Images"
        '
        'toolStripGoTo6
        '
        Me.toolStripGoTo6.Name = "toolStripGoTo6"
        Me.toolStripGoTo6.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo6.Tag = "/Applications/MobileCal.app"
        Me.toolStripGoTo6.Text = "C&alendar Icon and Images"
        '
        'toolStripGoTo13
        '
        Me.toolStripGoTo13.Name = "toolStripGoTo13"
        Me.toolStripGoTo13.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo13.Tag = "/Applications/MobileSlideShow.app"
        Me.toolStripGoTo13.Text = "Came&ra/Photos Icons and Images"
        '
        'toolStripGoTo14
        '
        Me.toolStripGoTo14.Name = "toolStripGoTo14"
        Me.toolStripGoTo14.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo14.Tag = "/Applications/MobileTimer.app"
        Me.toolStripGoTo14.Text = "C&lock Icon and Images"
        '
        'toolStripGoTo8
        '
        Me.toolStripGoTo8.Name = "toolStripGoTo8"
        Me.toolStripGoTo8.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo8.Tag = "/Applications/MobileMusicPlayer.app"
        Me.toolStripGoTo8.Text = "&iPod Icon and Images"
        '
        'toolStripGoTo7
        '
        Me.toolStripGoTo7.Name = "toolStripGoTo7"
        Me.toolStripGoTo7.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo7.Tag = "/Applications/MobileMail.app"
        Me.toolStripGoTo7.Text = "Mai&l Icon and Images"
        '
        'toolStripGoTo5
        '
        Me.toolStripGoTo5.Name = "toolStripGoTo5"
        Me.toolStripGoTo5.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo5.Tag = "/Applications/Maps.app"
        Me.toolStripGoTo5.Text = "&Maps Icon and Images"
        '
        'toolStripGoTo9
        '
        Me.toolStripGoTo9.Name = "toolStripGoTo9"
        Me.toolStripGoTo9.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo9.Tag = "/Applications/MobileNotes.app"
        Me.toolStripGoTo9.Text = "&Notes Icon and Images"
        '
        'toolStripGoTo10
        '
        Me.toolStripGoTo10.Name = "toolStripGoTo10"
        Me.toolStripGoTo10.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo10.Tag = "/Applications/MobilePhone.app"
        Me.toolStripGoTo10.Text = "&Phone Icon and Images"
        '
        'toolStripGoTo12
        '
        Me.toolStripGoTo12.Name = "toolStripGoTo12"
        Me.toolStripGoTo12.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo12.Tag = "/Applications/MobileSafari.app"
        Me.toolStripGoTo12.Text = "Sa&fari Icon and Images"
        '
        'toolStripGoTo15
        '
        Me.toolStripGoTo15.Name = "toolStripGoTo15"
        Me.toolStripGoTo15.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo15.Tag = "/Applications/Preferences.app"
        Me.toolStripGoTo15.Text = "Se&ttings Icon and Images"
        '
        'toolStripGoTo11
        '
        Me.toolStripGoTo11.Name = "toolStripGoTo11"
        Me.toolStripGoTo11.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo11.Tag = "/Applications/MobileSMS.app"
        Me.toolStripGoTo11.Text = "&SMS Icon and Images"
        '
        'toolStripGoTo16
        '
        Me.toolStripGoTo16.Name = "toolStripGoTo16"
        Me.toolStripGoTo16.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo16.Tag = "/Applications/Stocks.app"
        Me.toolStripGoTo16.Text = "Stoc&ks Icon and Images"
        '
        'toolStripGoTo17
        '
        Me.toolStripGoTo17.Name = "toolStripGoTo17"
        Me.toolStripGoTo17.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo17.Tag = "/Applications/Weather.app"
        Me.toolStripGoTo17.Text = "&Weather Icon and Images"
        '
        'toolStripGoTo18
        '
        Me.toolStripGoTo18.Name = "toolStripGoTo18"
        Me.toolStripGoTo18.Size = New System.Drawing.Size(247, 22)
        Me.toolStripGoTo18.Tag = "/Applications/YouTube.app"
        Me.toolStripGoTo18.Text = "&YouTube Icon and Images"
        '
        'mnuThirdPartyApps
        '
        Me.mnuThirdPartyApps.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem5, Me.DockSwapDocksToolStripMenuItem, Me.EBooksToolStripMenuItem, Me.FrotzGamesToolStripMenuItem, Me.cmdGBAROMs, Me.ToolStripMenuItem6, Me.InstallerPackageSourcesToolStripMenuItem, Me.ISwitcherThemesToolStripMenuItem, Me.NESROMSToolStripMenuItem, Me.TTRToolStripMenuItem, Me.WeDictDictionariesToolStripMenuItem})
        Me.mnuThirdPartyApps.Name = "mnuThirdPartyApps"
        Me.mnuThirdPartyApps.Size = New System.Drawing.Size(244, 22)
        Me.mnuThirdPartyApps.Text = "Third-&Party Applications"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(218, 22)
        Me.ToolStripMenuItem5.Tag = "/var/mobile/Library/Customize"
        Me.ToolStripMenuItem5.Text = "&Customize Files"
        '
        'DockSwapDocksToolStripMenuItem
        '
        Me.DockSwapDocksToolStripMenuItem.Name = "DockSwapDocksToolStripMenuItem"
        Me.DockSwapDocksToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.DockSwapDocksToolStripMenuItem.Tag = "/var/mobile/Library/DockSwap"
        Me.DockSwapDocksToolStripMenuItem.Text = "&DockSwap/Customize Docks"
        '
        'EBooksToolStripMenuItem
        '
        Me.EBooksToolStripMenuItem.Name = "EBooksToolStripMenuItem"
        Me.EBooksToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.EBooksToolStripMenuItem.Tag = "/var/mobile/Media/EBooks"
        Me.EBooksToolStripMenuItem.Text = "E&Books"
        '
        'FrotzGamesToolStripMenuItem
        '
        Me.FrotzGamesToolStripMenuItem.Name = "FrotzGamesToolStripMenuItem"
        Me.FrotzGamesToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.FrotzGamesToolStripMenuItem.Tag = "/var/mobile/Media/Frotz/Games"
        Me.FrotzGamesToolStripMenuItem.Text = "&Frotz Games"
        '
        'cmdGBAROMs
        '
        Me.cmdGBAROMs.Name = "cmdGBAROMs"
        Me.cmdGBAROMs.Size = New System.Drawing.Size(218, 22)
        Me.cmdGBAROMs.Tag = "/var/mobile/Media/ROMs/GBA"
        Me.cmdGBAROMs.Text = "&GBA ROMs"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(218, 22)
        Me.ToolStripMenuItem6.Tag = "/var/mobile/Library/iFlashCards"
        Me.ToolStripMenuItem6.Text = "iFlashCards"
        '
        'InstallerPackageSourcesToolStripMenuItem
        '
        Me.InstallerPackageSourcesToolStripMenuItem.Name = "InstallerPackageSourcesToolStripMenuItem"
        Me.InstallerPackageSourcesToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.InstallerPackageSourcesToolStripMenuItem.Tag = "/var/mobile/Library/Installer"
        Me.InstallerPackageSourcesToolStripMenuItem.Text = "&Installer Package Sources"
        '
        'ISwitcherThemesToolStripMenuItem
        '
        Me.ISwitcherThemesToolStripMenuItem.Name = "ISwitcherThemesToolStripMenuItem"
        Me.ISwitcherThemesToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.ISwitcherThemesToolStripMenuItem.Tag = "/var/mobile/Media/Themes"
        Me.ISwitcherThemesToolStripMenuItem.Text = "i&Switcher Themes"
        '
        'NESROMSToolStripMenuItem
        '
        Me.NESROMSToolStripMenuItem.Name = "NESROMSToolStripMenuItem"
        Me.NESROMSToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.NESROMSToolStripMenuItem.Tag = "/var/mobile/Media/ROMs/NES"
        Me.NESROMSToolStripMenuItem.Text = "&NES ROMS"
        '
        'TTRToolStripMenuItem
        '
        Me.TTRToolStripMenuItem.Name = "TTRToolStripMenuItem"
        Me.TTRToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.TTRToolStripMenuItem.Tag = "/var/mobile/Media/TTR"
        Me.TTRToolStripMenuItem.Text = "&TTR"
        '
        'WeDictDictionariesToolStripMenuItem
        '
        Me.WeDictDictionariesToolStripMenuItem.Name = "WeDictDictionariesToolStripMenuItem"
        Me.WeDictDictionariesToolStripMenuItem.Size = New System.Drawing.Size(218, 22)
        Me.WeDictDictionariesToolStripMenuItem.Tag = "/var/mobile/Libary/weDict"
        Me.WeDictDictionariesToolStripMenuItem.Text = "&weDict Dictionaries"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(241, 6)
        '
        'CameraRollToolStripMenuItem
        '
        Me.CameraRollToolStripMenuItem.Name = "CameraRollToolStripMenuItem"
        Me.CameraRollToolStripMenuItem.Size = New System.Drawing.Size(244, 22)
        Me.CameraRollToolStripMenuItem.Tag = "/var/mobile/Media/DCIM"
        Me.CameraRollToolStripMenuItem.Text = "Camera Roll  (DCIM)"
        '
        'toolStripGoTo19
        '
        Me.toolStripGoTo19.Name = "toolStripGoTo19"
        Me.toolStripGoTo19.Size = New System.Drawing.Size(244, 22)
        Me.toolStripGoTo19.Tag = "/System/Library/Fonts"
        Me.toolStripGoTo19.Text = "&Fonts"
        '
        'mnuFavorites
        '
        Me.mnuFavorites.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.mnuFavorites.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToFavoritesToolStripMenuItem, Me.OrganizeFavoritesToolStripMenuItem, Me.ToolStripMenuItem7})
        Me.mnuFavorites.Enabled = False
        Me.mnuFavorites.Image = CType(resources.GetObject("mnuFavorites.Image"), System.Drawing.Image)
        Me.mnuFavorites.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuFavorites.Name = "mnuFavorites"
        Me.mnuFavorites.Size = New System.Drawing.Size(65, 22)
        Me.mnuFavorites.Text = "Favorites"
        '
        'AddToFavoritesToolStripMenuItem
        '
        Me.AddToFavoritesToolStripMenuItem.Name = "AddToFavoritesToolStripMenuItem"
        Me.AddToFavoritesToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.AddToFavoritesToolStripMenuItem.Text = "Add to Favorites..."
        '
        'OrganizeFavoritesToolStripMenuItem
        '
        Me.OrganizeFavoritesToolStripMenuItem.Name = "OrganizeFavoritesToolStripMenuItem"
        Me.OrganizeFavoritesToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.OrganizeFavoritesToolStripMenuItem.Text = "Organize Favorites..."
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(185, 6)
        '
        'menuRightClickFiles
        '
        Me.menuRightClickFiles.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuRightSaveAs, Me.ToolStripSeparator2, Me.menuRightBackupFile, Me.menuRightRestoreFile, Me.ToolStripSeparator1, Me.cmdRenameFile, Me.menuRightReplaceFile, Me.menuRightDeleteFile})
        Me.menuRightClickFiles.Name = "menuRightClickFiles"
        Me.menuRightClickFiles.Size = New System.Drawing.Size(156, 148)
        '
        'menuRightSaveAs
        '
        Me.menuRightSaveAs.Name = "menuRightSaveAs"
        Me.menuRightSaveAs.Size = New System.Drawing.Size(155, 22)
        Me.menuRightSaveAs.Text = "Save &As..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(152, 6)
        '
        'menuRightBackupFile
        '
        Me.menuRightBackupFile.Name = "menuRightBackupFile"
        Me.menuRightBackupFile.Size = New System.Drawing.Size(155, 22)
        Me.menuRightBackupFile.Text = "&Backup File"
        '
        'menuRightRestoreFile
        '
        Me.menuRightRestoreFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemLoading})
        Me.menuRightRestoreFile.Enabled = False
        Me.menuRightRestoreFile.Name = "menuRightRestoreFile"
        Me.menuRightRestoreFile.Size = New System.Drawing.Size(155, 22)
        Me.menuRightRestoreFile.Text = "&Restore File"
        '
        'ToolStripMenuItemLoading
        '
        Me.ToolStripMenuItemLoading.Name = "ToolStripMenuItemLoading"
        Me.ToolStripMenuItemLoading.Size = New System.Drawing.Size(243, 22)
        Me.ToolStripMenuItemLoading.Text = "Loading List of Backed Up Files..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(152, 6)
        '
        'cmdRenameFile
        '
        Me.cmdRenameFile.Name = "cmdRenameFile"
        Me.cmdRenameFile.Size = New System.Drawing.Size(155, 22)
        Me.cmdRenameFile.Text = "Rename File..."
        '
        'menuRightReplaceFile
        '
        Me.menuRightReplaceFile.Name = "menuRightReplaceFile"
        Me.menuRightReplaceFile.Size = New System.Drawing.Size(155, 22)
        Me.menuRightReplaceFile.Text = "Re&place File..."
        '
        'menuRightDeleteFile
        '
        Me.menuRightDeleteFile.Name = "menuRightDeleteFile"
        Me.menuRightDeleteFile.Size = New System.Drawing.Size(155, 22)
        Me.menuRightDeleteFile.Text = "&Delete File"
        '
        'fileSaveDialog
        '
        Me.fileSaveDialog.AddExtension = False
        '
        'splMain
        '
        Me.splMain.AllowDrop = True
        Me.splMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splMain.Location = New System.Drawing.Point(0, 25)
        Me.splMain.Margin = New System.Windows.Forms.Padding(5)
        Me.splMain.MinimumSize = New System.Drawing.Size(100, 100)
        Me.splMain.Name = "splMain"
        '
        'splMain.Panel1
        '
        Me.splMain.Panel1.Controls.Add(Me.grpFolders)
        Me.splMain.Panel1.Padding = New System.Windows.Forms.Padding(3)
        '
        'splMain.Panel2
        '
        Me.splMain.Panel2.Controls.Add(Me.splFiles)
        Me.splMain.Panel2.Padding = New System.Windows.Forms.Padding(3)
        Me.splMain.Size = New System.Drawing.Size(747, 550)
        Me.splMain.SplitterDistance = 251
        Me.splMain.TabIndex = 5
        '
        'grpFolders
        '
        Me.grpFolders.Controls.Add(Me.trvFolders)
        Me.grpFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpFolders.Location = New System.Drawing.Point(3, 3)
        Me.grpFolders.Margin = New System.Windows.Forms.Padding(0)
        Me.grpFolders.Name = "grpFolders"
        Me.grpFolders.Size = New System.Drawing.Size(245, 544)
        Me.grpFolders.TabIndex = 2
        Me.grpFolders.TabStop = False
        Me.grpFolders.Text = "Folders on your iPhone"
        '
        'trvFolders
        '
        Me.trvFolders.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvFolders.ImageIndex = 0
        Me.trvFolders.ImageList = Me.imgFolders
        Me.trvFolders.Location = New System.Drawing.Point(3, 16)
        Me.trvFolders.Name = "trvFolders"
        Me.trvFolders.PathSeparator = "/"
        Me.trvFolders.SelectedImageIndex = 1
        Me.trvFolders.ShowRootLines = False
        Me.trvFolders.Size = New System.Drawing.Size(239, 525)
        Me.trvFolders.TabIndex = 0
        '
        'splFiles
        '
        Me.splFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splFiles.Location = New System.Drawing.Point(3, 3)
        Me.splFiles.MinimumSize = New System.Drawing.Size(100, 100)
        Me.splFiles.Name = "splFiles"
        Me.splFiles.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splFiles.Panel1
        '
        Me.splFiles.Panel1.Controls.Add(Me.grpFiles)
        '
        'splFiles.Panel2
        '
        Me.splFiles.Panel2.Controls.Add(Me.grpDetails)
        Me.splFiles.Size = New System.Drawing.Size(486, 544)
        Me.splFiles.SplitterDistance = 362
        Me.splFiles.TabIndex = 0
        '
        'grpFiles
        '
        Me.grpFiles.Controls.Add(Me.lstFiles)
        Me.grpFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpFiles.Location = New System.Drawing.Point(0, 0)
        Me.grpFiles.Name = "grpFiles"
        Me.grpFiles.Size = New System.Drawing.Size(486, 362)
        Me.grpFiles.TabIndex = 1
        Me.grpFiles.TabStop = False
        Me.grpFiles.Text = "Files on your iPhone"
        '
        'lstFiles
        '
        Me.lstFiles.AllowColumnReorder = True
        Me.lstFiles.AllowDrop = True
        Me.lstFiles.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cohFilename, Me.cohSize, Me.cohFiletype})
        Me.lstFiles.ContextMenuStrip = Me.menuRightClickFiles
        Me.lstFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstFiles.LargeImageList = Me.imgFilesLarge
        Me.lstFiles.Location = New System.Drawing.Point(3, 16)
        Me.lstFiles.Name = "lstFiles"
        Me.lstFiles.ShowGroups = False
        Me.lstFiles.Size = New System.Drawing.Size(480, 343)
        Me.lstFiles.SmallImageList = Me.imgFilesSmall
        Me.lstFiles.TabIndex = 0
        Me.lstFiles.UseCompatibleStateImageBehavior = False
        Me.lstFiles.View = System.Windows.Forms.View.Details
        '
        'cohFilename
        '
        Me.cohFilename.Text = "Filename"
        Me.cohFilename.Width = 260
        '
        'cohSize
        '
        Me.cohSize.Text = "Size"
        Me.cohSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.cohSize.Width = 75
        '
        'cohFiletype
        '
        Me.cohFiletype.Text = "File Type"
        Me.cohFiletype.Width = 140
        '
        'grpDetails
        '
        Me.grpDetails.Controls.Add(Me.picFileDetails)
        Me.grpDetails.Controls.Add(Me.txtFileDetails)
        Me.grpDetails.Controls.Add(Me.qtPlugin)
        Me.grpDetails.Controls.Add(Me.Panel1)
        Me.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grpDetails.Location = New System.Drawing.Point(0, 0)
        Me.grpDetails.Name = "grpDetails"
        Me.grpDetails.Size = New System.Drawing.Size(486, 178)
        Me.grpDetails.TabIndex = 0
        Me.grpDetails.TabStop = False
        Me.grpDetails.Text = "File Details"
        '
        'picFileDetails
        '
        Me.picFileDetails.BackColor = System.Drawing.SystemColors.Control
        Me.picFileDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picFileDetails.ImageLocation = ""
        Me.picFileDetails.Location = New System.Drawing.Point(3, 40)
        Me.picFileDetails.Name = "picFileDetails"
        Me.picFileDetails.Size = New System.Drawing.Size(480, 135)
        Me.picFileDetails.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.picFileDetails.TabIndex = 2
        Me.picFileDetails.TabStop = False
        Me.picFileDetails.Visible = False
        '
        'txtFileDetails
        '
        Me.txtFileDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtFileDetails.Location = New System.Drawing.Point(3, 40)
        Me.txtFileDetails.Multiline = True
        Me.txtFileDetails.Name = "txtFileDetails"
        Me.txtFileDetails.ReadOnly = True
        Me.txtFileDetails.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFileDetails.Size = New System.Drawing.Size(480, 135)
        Me.txtFileDetails.TabIndex = 0
        Me.txtFileDetails.Visible = False
        '
        'qtPlugin
        '
        Me.qtPlugin.Dock = System.Windows.Forms.DockStyle.Fill
        Me.qtPlugin.Enabled = True
        Me.qtPlugin.Location = New System.Drawing.Point(3, 40)
        Me.qtPlugin.Name = "qtPlugin"
        Me.qtPlugin.OcxState = CType(resources.GetObject("qtPlugin.OcxState"), System.Windows.Forms.AxHost.State)
        Me.qtPlugin.Size = New System.Drawing.Size(480, 135)
        Me.qtPlugin.TabIndex = 4
        Me.qtPlugin.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnPreview)
        Me.Panel1.Controls.Add(Me.chkPreviewEnabled)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(480, 24)
        Me.Panel1.TabIndex = 5
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(399, 0)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.TabStop = False
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'chkPreviewEnabled
        '
        Me.chkPreviewEnabled.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkPreviewEnabled.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkPreviewEnabled.AutoSize = True
        Me.chkPreviewEnabled.Checked = Global.iPhoneBrowser.My.MySettings.Default.ShowPreviews
        Me.chkPreviewEnabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPreviewEnabled.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.iPhoneBrowser.My.MySettings.Default, "ShowPreviews", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkPreviewEnabled.Location = New System.Drawing.Point(271, 0)
        Me.chkPreviewEnabled.Name = "chkPreviewEnabled"
        Me.chkPreviewEnabled.Size = New System.Drawing.Size(122, 23)
        Me.chkPreviewEnabled.TabIndex = 0
        Me.chkPreviewEnabled.TabStop = False
        Me.chkPreviewEnabled.Text = "Auto Preview Enabled"
        Me.chkPreviewEnabled.UseVisualStyleBackColor = True
        '
        'menuRightClickFolders
        '
        Me.menuRightClickFolders.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemNewFolder, Me.ToolStripSeparator9, Me.BackupFolderToolStripMenuItem, Me.ToolStripMenuItemSaveFolderIn, Me.cmdRenameFolder, Me.ToolStripSeparator10, Me.ToolStripMenuItemDeleteFolder})
        Me.menuRightClickFolders.Name = "menuRightClickFolders"
        Me.menuRightClickFolders.Size = New System.Drawing.Size(170, 126)
        '
        'ToolStripMenuItemNewFolder
        '
        Me.ToolStripMenuItemNewFolder.Name = "ToolStripMenuItemNewFolder"
        Me.ToolStripMenuItemNewFolder.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItemNewFolder.Text = "&New Folder..."
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(166, 6)
        '
        'BackupFolderToolStripMenuItem
        '
        Me.BackupFolderToolStripMenuItem.Name = "BackupFolderToolStripMenuItem"
        Me.BackupFolderToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.BackupFolderToolStripMenuItem.Text = "Backup Folder"
        '
        'ToolStripMenuItemSaveFolderIn
        '
        Me.ToolStripMenuItemSaveFolderIn.Name = "ToolStripMenuItemSaveFolderIn"
        Me.ToolStripMenuItemSaveFolderIn.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItemSaveFolderIn.Text = "Save Folder In..."
        '
        'cmdRenameFolder
        '
        Me.cmdRenameFolder.Name = "cmdRenameFolder"
        Me.cmdRenameFolder.Size = New System.Drawing.Size(169, 22)
        Me.cmdRenameFolder.Text = "Rename Folder..."
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(166, 6)
        '
        'ToolStripMenuItemDeleteFolder
        '
        Me.ToolStripMenuItemDeleteFolder.Name = "ToolStripMenuItemDeleteFolder"
        Me.ToolStripMenuItemDeleteFolder.Size = New System.Drawing.Size(169, 22)
        Me.ToolStripMenuItemDeleteFolder.Text = "&Delete Folder"
        '
        'folderBrowserDialog
        '
        Me.folderBrowserDialog.Description = "Select the destination folder for saving"
        Me.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(747, 597)
        Me.Controls.Add(Me.splMain)
        Me.Controls.Add(Me.toolStrip)
        Me.Controls.Add(Me.tlbStatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(300, 300)
        Me.Name = "frmMain"
        Me.Text = "iPhoneBrowser"
        Me.tlbStatusStrip.ResumeLayout(False)
        Me.tlbStatusStrip.PerformLayout()
        Me.toolStrip.ResumeLayout(False)
        Me.toolStrip.PerformLayout()
        Me.menuRightClickFiles.ResumeLayout(False)
        Me.splMain.Panel1.ResumeLayout(False)
        Me.splMain.Panel2.ResumeLayout(False)
        Me.splMain.ResumeLayout(False)
        Me.grpFolders.ResumeLayout(False)
        Me.splFiles.Panel1.ResumeLayout(False)
        Me.splFiles.Panel2.ResumeLayout(False)
        Me.splFiles.ResumeLayout(False)
        Me.grpFiles.ResumeLayout(False)
        Me.grpDetails.ResumeLayout(False)
        Me.grpDetails.PerformLayout()
        CType(Me.picFileDetails, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.qtPlugin, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.menuRightClickFolders.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlbStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents tlbStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tlbProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents imgFolders As System.Windows.Forms.ImageList
    Friend WithEvents imgFilesSmall As System.Windows.Forms.ImageList
    Friend WithEvents imgFilesLarge As System.Windows.Forms.ImageList
    Friend WithEvents toolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripMenuItemFile As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItemExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuView As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripMenuItemDetails As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemLargeIcons As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRightClickFiles As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents menuRightSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRightBackupFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRightRestoreFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents fileSaveDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuRightReplaceFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents fileOpenDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents menuRightDeleteFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCleanUp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemLoading As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemViewBackups As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents splMain As System.Windows.Forms.SplitContainer
    Friend WithEvents grpFolders As System.Windows.Forms.GroupBox
    Friend WithEvents trvFolders As System.Windows.Forms.TreeView
    Friend WithEvents splFiles As System.Windows.Forms.SplitContainer
    Friend WithEvents grpFiles As System.Windows.Forms.GroupBox
    Friend WithEvents lstFiles As System.Windows.Forms.ListView
    Friend WithEvents cohFilename As System.Windows.Forms.ColumnHeader
    Friend WithEvents cohSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents cohFiletype As System.Windows.Forms.ColumnHeader
    Friend WithEvents grpDetails As System.Windows.Forms.GroupBox
    Friend WithEvents qtPlugin As AxQTOControlLib.AxQTControl
    Friend WithEvents picFileDetails As System.Windows.Forms.PictureBox
    Friend WithEvents txtFileDetails As System.Windows.Forms.TextBox
    Friend WithEvents mnuGoTo As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents toolStripGoTo1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripGoTo19 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuRightClickFolders As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItemNewFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemDeleteFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStdApps As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo13 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo14 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo8 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo9 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo12 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo11 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo16 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo17 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripGoTo18 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuThirdPartyApps As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DockSwapDocksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EBooksToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FrotzGamesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ISwitcherThemesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NESROMSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TTRToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InstallerPackageSourcesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEdit As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfirmDeletionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WeDictDictionariesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConvertPNGsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents PictureBackgroundToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BlackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GrayToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WhiteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuSaveSummerboardTheme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AsSummerboardFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AsPXLPackageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AsCustomizeFoldersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AsPXLPackageToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IPhoneToPCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PCToIPhoneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConvertBothToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowPreviewsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemSaveFolderIn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BackupFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents IgnoreThumbsdbToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IgnoreDSStoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdRenameFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdRenameFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuFavorites As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents AddToFavoritesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrganizeFavoritesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents chkPreviewEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmdSmallIcons As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmdShowGroups As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdGBAROMs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CameraRollToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tlbProgress0 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DontBackupRunToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DontBackupEverToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
