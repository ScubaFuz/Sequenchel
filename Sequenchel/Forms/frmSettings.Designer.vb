<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.tabSettings = New System.Windows.Forms.TabControl()
        Me.tpgGeneral = New System.Windows.Forms.TabPage()
        Me.txtTimerHours = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblLimitLookupLists = New System.Windows.Forms.Label()
        Me.txtLimitLookupLists = New System.Windows.Forms.TextBox()
        Me.chkLimitLookupLists = New System.Windows.Forms.CheckBox()
        Me.chkIncludeDateInExportFiles = New System.Windows.Forms.CheckBox()
        Me.cbxDateFormats = New System.Windows.Forms.ComboBox()
        Me.lblDateFormatting = New System.Windows.Forms.Label()
        Me.btnConnectionsFileSystem = New System.Windows.Forms.Button()
        Me.btnSettingsFileSystem = New System.Windows.Forms.Button()
        Me.grpSecurity = New System.Windows.Forms.GroupBox()
        Me.btnShowOverridePassword = New System.Windows.Forms.Button()
        Me.chkAllowSmartUpdate = New System.Windows.Forms.CheckBox()
        Me.chkAllowDataImport = New System.Windows.Forms.CheckBox()
        Me.chkAllowQueryEdit = New System.Windows.Forms.CheckBox()
        Me.lblUsageText = New System.Windows.Forms.Label()
        Me.chkAllowSettingsChange = New System.Windows.Forms.CheckBox()
        Me.lblUsage = New System.Windows.Forms.Label()
        Me.chkAllowConfiguration = New System.Windows.Forms.CheckBox()
        Me.txtOverridePassword = New System.Windows.Forms.TextBox()
        Me.chkAllowLinkedServers = New System.Windows.Forms.CheckBox()
        Me.lblOverridePassword = New System.Windows.Forms.Label()
        Me.chkAllowUpdate = New System.Windows.Forms.CheckBox()
        Me.chkAllowDelete = New System.Windows.Forms.CheckBox()
        Me.chkAllowInsert = New System.Windows.Forms.CheckBox()
        Me.btnConnectionsFileDefault = New System.Windows.Forms.Button()
        Me.btnSettingsFileDefault = New System.Windows.Forms.Button()
        Me.btnDefaultConfigFilePath = New System.Windows.Forms.Button()
        Me.btnConfigFilePath = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDefaultConfigFilePath = New System.Windows.Forms.TextBox()
        Me.btnConnectionsFile = New System.Windows.Forms.Button()
        Me.btnSettingsFile = New System.Windows.Forms.Button()
        Me.btnSettingsGeneralSave = New System.Windows.Forms.Button()
        Me.lblConnectionsFile = New System.Windows.Forms.Label()
        Me.txtConnectionsFile = New System.Windows.Forms.TextBox()
        Me.lblSettingsFile = New System.Windows.Forms.Label()
        Me.txtSettingsFile = New System.Windows.Forms.TextBox()
        Me.tpgLicense = New System.Windows.Forms.TabPage()
        Me.lblLicenseKey = New System.Windows.Forms.Label()
        Me.txtLicenseKey = New System.Windows.Forms.TextBox()
        Me.btnSaveLicense = New System.Windows.Forms.Button()
        Me.lblLicenseName = New System.Windows.Forms.Label()
        Me.txtLicenseName = New System.Windows.Forms.TextBox()
        Me.btnValidateLicense = New System.Windows.Forms.Button()
        Me.tpgLogging = New System.Windows.Forms.TabPage()
        Me.btnLogLocationBrowse = New System.Windows.Forms.Button()
        Me.btnLogLocationDatabase = New System.Windows.Forms.Button()
        Me.btnLogfileNameDefault = New System.Windows.Forms.Button()
        Me.btnLogLocationSystem = New System.Windows.Forms.Button()
        Me.btnLogLocationDefault = New System.Windows.Forms.Button()
        Me.grpLogsToKeep = New System.Windows.Forms.GroupBox()
        Me.chkAutoDeleteOldLogs = New System.Windows.Forms.CheckBox()
        Me.btnClearOldLogs = New System.Windows.Forms.Button()
        Me.rbtKeepLogMonth = New System.Windows.Forms.RadioButton()
        Me.rbtKeepLogWeek = New System.Windows.Forms.RadioButton()
        Me.rbtKeepLogDay = New System.Windows.Forms.RadioButton()
        Me.txtLogfileLocation = New System.Windows.Forms.TextBox()
        Me.txtLogfileName = New System.Windows.Forms.TextBox()
        Me.btnSaveSettingsLog = New System.Windows.Forms.Button()
        Me.lblLogfileName = New System.Windows.Forms.Label()
        Me.lblLogfileLocation = New System.Windows.Forms.Label()
        Me.rbtLoggingLevel5 = New System.Windows.Forms.RadioButton()
        Me.lblLoggingLevel = New System.Windows.Forms.Label()
        Me.rbtLoggingLevel1 = New System.Windows.Forms.RadioButton()
        Me.rbtLoggingLevel2 = New System.Windows.Forms.RadioButton()
        Me.rbtLoggingLevel3 = New System.Windows.Forms.RadioButton()
        Me.rbtLoggingLevel4 = New System.Windows.Forms.RadioButton()
        Me.rbtLoggingLevel0 = New System.Windows.Forms.RadioButton()
        Me.tpgDatabase = New System.Windows.Forms.TabPage()
        Me.btnShowDatabasePassword = New System.Windows.Forms.Button()
        Me.btnRefreshDatabase = New System.Windows.Forms.Button()
        Me.btnTestConnection = New System.Windows.Forms.Button()
        Me.lblBackupPath = New System.Windows.Forms.Label()
        Me.txtBackupDatabase = New System.Windows.Forms.TextBox()
        Me.lblBackupDatabase = New System.Windows.Forms.Label()
        Me.btnBackupDatabase = New System.Windows.Forms.Button()
        Me.txtUpgradeDatabase = New System.Windows.Forms.TextBox()
        Me.lblDatabaseVersion = New System.Windows.Forms.Label()
        Me.btnUpgradeDatabase = New System.Windows.Forms.Button()
        Me.prbCreateDatabase = New System.Windows.Forms.ProgressBar()
        Me.btnCreateExtraProcs = New System.Windows.Forms.Button()
        Me.btnCreateDatabase = New System.Windows.Forms.Button()
        Me.btnSaveSettingsDatabase = New System.Windows.Forms.Button()
        Me.btnDatabaseDefaultsUse = New System.Windows.Forms.Button()
        Me.lblLoginMethod = New System.Windows.Forms.Label()
        Me.lblLoginName = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.cbxLoginMethod = New System.Windows.Forms.ComboBox()
        Me.txtLoginName = New System.Windows.Forms.TextBox()
        Me.lblDatabaseName = New System.Windows.Forms.Label()
        Me.txtDatabaseName = New System.Windows.Forms.TextBox()
        Me.lblDatabaseLocation = New System.Windows.Forms.Label()
        Me.cbxDataProvider = New System.Windows.Forms.ComboBox()
        Me.txtDatabaseLocation = New System.Windows.Forms.TextBox()
        Me.lblDataProvider = New System.Windows.Forms.Label()
        Me.lblStatusDatabase = New System.Windows.Forms.Label()
        Me.tpgScheduler = New System.Windows.Forms.TabPage()
        Me.btnErrorlogPathBrowse = New System.Windows.Forms.Button()
        Me.btnErrorlogPathDatabase = New System.Windows.Forms.Button()
        Me.btnErrorlogPathSystem = New System.Windows.Forms.Button()
        Me.btnErrorlogPathDefault = New System.Windows.Forms.Button()
        Me.lblJobNamePrefix = New System.Windows.Forms.Label()
        Me.txtJobNamePrefix = New System.Windows.Forms.TextBox()
        Me.chkIncludeHigherSqlVersions = New System.Windows.Forms.CheckBox()
        Me.lblIncludeHigherSqlVersions = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblErrorlogPath = New System.Windows.Forms.Label()
        Me.txtErrorlogPath = New System.Windows.Forms.TextBox()
        Me.nudTimeSpan = New System.Windows.Forms.NumericUpDown()
        Me.lblRepeatEvery = New System.Windows.Forms.Label()
        Me.btnCreateScheduledJob = New System.Windows.Forms.Button()
        Me.cbxSqlVersion = New System.Windows.Forms.ComboBox()
        Me.chkMailStatistics = New System.Windows.Forms.CheckBox()
        Me.lblSeparator = New System.Windows.Forms.Label()
        Me.lblSqlVersion = New System.Windows.Forms.Label()
        Me.lblMailStatistics = New System.Windows.Forms.Label()
        Me.txtSeparator = New System.Windows.Forms.TextBox()
        Me.lblExceptionList = New System.Windows.Forms.Label()
        Me.txtExceptionList = New System.Windows.Forms.TextBox()
        Me.lblRecipient = New System.Windows.Forms.Label()
        Me.txtRecipient = New System.Windows.Forms.TextBox()
        Me.cbxEndMinute = New System.Windows.Forms.ComboBox()
        Me.cbxEndHour = New System.Windows.Forms.ComboBox()
        Me.lblEndTime = New System.Windows.Forms.Label()
        Me.lblEndTimeColon = New System.Windows.Forms.Label()
        Me.cbxStartMinute = New System.Windows.Forms.ComboBox()
        Me.cbxStartHour = New System.Windows.Forms.ComboBox()
        Me.lblStartTime = New System.Windows.Forms.Label()
        Me.cbxTimespan = New System.Windows.Forms.ComboBox()
        Me.lblOccurence = New System.Windows.Forms.Label()
        Me.cbxOccurence = New System.Windows.Forms.ComboBox()
        Me.chkTeusday = New System.Windows.Forms.CheckBox()
        Me.chkWednesday = New System.Windows.Forms.CheckBox()
        Me.chkThursday = New System.Windows.Forms.CheckBox()
        Me.chkFriday = New System.Windows.Forms.CheckBox()
        Me.chkSaturday = New System.Windows.Forms.CheckBox()
        Me.chkSunday = New System.Windows.Forms.CheckBox()
        Me.chkMonday = New System.Windows.Forms.CheckBox()
        Me.lblProcedure = New System.Windows.Forms.Label()
        Me.cbxProcedures = New System.Windows.Forms.ComboBox()
        Me.lblStartTimeColon = New System.Windows.Forms.Label()
        Me.tpgMonitorDataspaces = New System.Windows.Forms.TabPage()
        Me.btnMonitorDataSpacesLoad = New System.Windows.Forms.Button()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.lblUpperLimitHelp = New System.Windows.Forms.Label()
        Me.lblLowerLimitHelp = New System.Windows.Forms.Label()
        Me.lblLargeGrowthHelp = New System.Windows.Forms.Label()
        Me.lblMediumGrowthHelp = New System.Windows.Forms.Label()
        Me.lblSmallGrowthHelp = New System.Windows.Forms.Label()
        Me.lblMinFreeSpaceHelp = New System.Windows.Forms.Label()
        Me.lblMinPercGrowthHelp = New System.Windows.Forms.Label()
        Me.lblMinPercGrowth = New System.Windows.Forms.Label()
        Me.txtMinPercGrowth = New System.Windows.Forms.TextBox()
        Me.lblMinFreeSpace = New System.Windows.Forms.Label()
        Me.txtMinFreeSpace = New System.Windows.Forms.TextBox()
        Me.lblLargeGrowth = New System.Windows.Forms.Label()
        Me.txtLargeGrowth = New System.Windows.Forms.TextBox()
        Me.lblMediumGrowth = New System.Windows.Forms.Label()
        Me.txtMediumGrowth = New System.Windows.Forms.TextBox()
        Me.lblSmallGrowth = New System.Windows.Forms.Label()
        Me.txtSmallGrowth = New System.Windows.Forms.TextBox()
        Me.lblUpperLimit = New System.Windows.Forms.Label()
        Me.txtUpperLimit = New System.Windows.Forms.TextBox()
        Me.lblLowerLimit = New System.Windows.Forms.Label()
        Me.txtLowerLimit = New System.Windows.Forms.TextBox()
        Me.btnMonitorDataSpacesSave = New System.Windows.Forms.Button()
        Me.tpgFtp = New System.Windows.Forms.TabPage()
        Me.btnCreateDownloadProcedure = New System.Windows.Forms.Button()
        Me.lblFtpStatus = New System.Windows.Forms.Label()
        Me.rtbFtpCmdshell = New System.Windows.Forms.RichTextBox()
        Me.chkCmdshell = New System.Windows.Forms.CheckBox()
        Me.lblCmdshell = New System.Windows.Forms.Label()
        Me.rtbFtpDefaultValues = New System.Windows.Forms.RichTextBox()
        Me.cbxFtpMode = New System.Windows.Forms.ComboBox()
        Me.chkEncryptProcedure = New System.Windows.Forms.CheckBox()
        Me.lblEncryptProcedure = New System.Windows.Forms.Label()
        Me.btnCreateUploadProcedure = New System.Windows.Forms.Button()
        Me.chkRemoveFiles = New System.Windows.Forms.CheckBox()
        Me.lblRemoveFiles = New System.Windows.Forms.Label()
        Me.lblFtpMode = New System.Windows.Forms.Label()
        Me.lblTargetFiles = New System.Windows.Forms.Label()
        Me.txtTargetFiles = New System.Windows.Forms.TextBox()
        Me.lblFtpServer = New System.Windows.Forms.Label()
        Me.txtFtpServer = New System.Windows.Forms.TextBox()
        Me.lblFtpUserName = New System.Windows.Forms.Label()
        Me.txtFtpUserName = New System.Windows.Forms.TextBox()
        Me.lblFtpPassword = New System.Windows.Forms.Label()
        Me.txtFtpPassword = New System.Windows.Forms.TextBox()
        Me.lblFtpLocation = New System.Windows.Forms.Label()
        Me.txtFtpLocation = New System.Windows.Forms.TextBox()
        Me.lblDownloadDestination = New System.Windows.Forms.Label()
        Me.txtDownloadDestination = New System.Windows.Forms.TextBox()
        Me.lblUploadSource = New System.Windows.Forms.Label()
        Me.txtUploadSource = New System.Windows.Forms.TextBox()
        Me.tpgEmail = New System.Windows.Forms.TabPage()
        Me.btnShowEmailPassword = New System.Windows.Forms.Button()
        Me.btnSettingsEmailSave = New System.Windows.Forms.Button()
        Me.lblSmtpPortNumber = New System.Windows.Forms.Label()
        Me.txtSmtpPortNumber = New System.Windows.Forms.TextBox()
        Me.lblSmtpServer = New System.Windows.Forms.Label()
        Me.chkUseSslEncryption = New System.Windows.Forms.CheckBox()
        Me.txtSmtpServer = New System.Windows.Forms.TextBox()
        Me.lblSmtpReply = New System.Windows.Forms.Label()
        Me.txtSmtpServerUsername = New System.Windows.Forms.TextBox()
        Me.txtSmtpReply = New System.Windows.Forms.TextBox()
        Me.lblSmtpServerUsername = New System.Windows.Forms.Label()
        Me.chkSmtpCredentials = New System.Windows.Forms.CheckBox()
        Me.txtSmtpServerPassword = New System.Windows.Forms.TextBox()
        Me.lblSmtpServerPassword = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ttpDefaultLogLocation = New System.Windows.Forms.ToolTip(Me.components)
        Me.tabSettings.SuspendLayout()
        Me.tpgGeneral.SuspendLayout()
        Me.grpSecurity.SuspendLayout()
        Me.tpgLicense.SuspendLayout()
        Me.tpgLogging.SuspendLayout()
        Me.grpLogsToKeep.SuspendLayout()
        Me.tpgDatabase.SuspendLayout()
        Me.tpgScheduler.SuspendLayout()
        CType(Me.nudTimeSpan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpgMonitorDataspaces.SuspendLayout()
        Me.tpgFtp.SuspendLayout()
        Me.tpgEmail.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabSettings
        '
        Me.tabSettings.Controls.Add(Me.tpgGeneral)
        Me.tabSettings.Controls.Add(Me.tpgLicense)
        Me.tabSettings.Controls.Add(Me.tpgLogging)
        Me.tabSettings.Controls.Add(Me.tpgDatabase)
        Me.tabSettings.Controls.Add(Me.tpgScheduler)
        Me.tabSettings.Controls.Add(Me.tpgMonitorDataspaces)
        Me.tabSettings.Controls.Add(Me.tpgFtp)
        Me.tabSettings.Controls.Add(Me.tpgEmail)
        Me.tabSettings.Location = New System.Drawing.Point(12, 11)
        Me.tabSettings.Multiline = True
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.SelectedIndex = 0
        Me.tabSettings.Size = New System.Drawing.Size(611, 394)
        Me.tabSettings.TabIndex = 3
        '
        'tpgGeneral
        '
        Me.tpgGeneral.Controls.Add(Me.txtTimerHours)
        Me.tpgGeneral.Controls.Add(Me.Label4)
        Me.tpgGeneral.Controls.Add(Me.Label3)
        Me.tpgGeneral.Controls.Add(Me.lblLimitLookupLists)
        Me.tpgGeneral.Controls.Add(Me.txtLimitLookupLists)
        Me.tpgGeneral.Controls.Add(Me.chkLimitLookupLists)
        Me.tpgGeneral.Controls.Add(Me.chkIncludeDateInExportFiles)
        Me.tpgGeneral.Controls.Add(Me.cbxDateFormats)
        Me.tpgGeneral.Controls.Add(Me.lblDateFormatting)
        Me.tpgGeneral.Controls.Add(Me.btnConnectionsFileSystem)
        Me.tpgGeneral.Controls.Add(Me.btnSettingsFileSystem)
        Me.tpgGeneral.Controls.Add(Me.grpSecurity)
        Me.tpgGeneral.Controls.Add(Me.btnConnectionsFileDefault)
        Me.tpgGeneral.Controls.Add(Me.btnSettingsFileDefault)
        Me.tpgGeneral.Controls.Add(Me.btnDefaultConfigFilePath)
        Me.tpgGeneral.Controls.Add(Me.btnConfigFilePath)
        Me.tpgGeneral.Controls.Add(Me.Label2)
        Me.tpgGeneral.Controls.Add(Me.txtDefaultConfigFilePath)
        Me.tpgGeneral.Controls.Add(Me.btnConnectionsFile)
        Me.tpgGeneral.Controls.Add(Me.btnSettingsFile)
        Me.tpgGeneral.Controls.Add(Me.btnSettingsGeneralSave)
        Me.tpgGeneral.Controls.Add(Me.lblConnectionsFile)
        Me.tpgGeneral.Controls.Add(Me.txtConnectionsFile)
        Me.tpgGeneral.Controls.Add(Me.lblSettingsFile)
        Me.tpgGeneral.Controls.Add(Me.txtSettingsFile)
        Me.tpgGeneral.Location = New System.Drawing.Point(4, 22)
        Me.tpgGeneral.Name = "tpgGeneral"
        Me.tpgGeneral.Size = New System.Drawing.Size(603, 368)
        Me.tpgGeneral.TabIndex = 12
        Me.tpgGeneral.Text = "General"
        Me.tpgGeneral.UseVisualStyleBackColor = True
        '
        'txtTimerHours
        '
        Me.txtTimerHours.Location = New System.Drawing.Point(156, 286)
        Me.txtTimerHours.Name = "txtTimerHours"
        Me.txtTimerHours.Size = New System.Drawing.Size(31, 20)
        Me.txtTimerHours.TabIndex = 118
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(193, 289)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 13)
        Me.Label4.TabIndex = 117
        Me.Label4.Text = "hours of inactivity (0 = unlimited)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 289)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 13)
        Me.Label3.TabIndex = 116
        Me.Label3.Text = "Close Sequenchel after"
        '
        'lblLimitLookupLists
        '
        Me.lblLimitLookupLists.AutoSize = True
        Me.lblLimitLookupLists.Location = New System.Drawing.Point(502, 342)
        Me.lblLimitLookupLists.Name = "lblLimitLookupLists"
        Me.lblLimitLookupLists.Size = New System.Drawing.Size(31, 13)
        Me.lblLimitLookupLists.TabIndex = 115
        Me.lblLimitLookupLists.Text = "items"
        '
        'txtLimitLookupLists
        '
        Me.txtLimitLookupLists.Location = New System.Drawing.Point(455, 339)
        Me.txtLimitLookupLists.Name = "txtLimitLookupLists"
        Me.txtLimitLookupLists.Size = New System.Drawing.Size(41, 20)
        Me.txtLimitLookupLists.TabIndex = 114
        '
        'chkLimitLookupLists
        '
        Me.chkLimitLookupLists.AutoSize = True
        Me.chkLimitLookupLists.Location = New System.Drawing.Point(340, 341)
        Me.chkLimitLookupLists.Name = "chkLimitLookupLists"
        Me.chkLimitLookupLists.Size = New System.Drawing.Size(121, 17)
        Me.chkLimitLookupLists.TabIndex = 113
        Me.chkLimitLookupLists.Text = "Limit Lookup lists to "
        Me.chkLimitLookupLists.UseVisualStyleBackColor = True
        '
        'chkIncludeDateInExportFiles
        '
        Me.chkIncludeDateInExportFiles.AutoSize = True
        Me.chkIncludeDateInExportFiles.Location = New System.Drawing.Point(36, 341)
        Me.chkIncludeDateInExportFiles.Name = "chkIncludeDateInExportFiles"
        Me.chkIncludeDateInExportFiles.Size = New System.Drawing.Size(196, 17)
        Me.chkIncludeDateInExportFiles.TabIndex = 108
        Me.chkIncludeDateInExportFiles.Text = "Include the date in export file names"
        Me.chkIncludeDateInExportFiles.UseVisualStyleBackColor = True
        '
        'cbxDateFormats
        '
        Me.cbxDateFormats.FormattingEnabled = True
        Me.cbxDateFormats.Location = New System.Drawing.Point(180, 314)
        Me.cbxDateFormats.Name = "cbxDateFormats"
        Me.cbxDateFormats.Size = New System.Drawing.Size(353, 21)
        Me.cbxDateFormats.TabIndex = 112
        '
        'lblDateFormatting
        '
        Me.lblDateFormatting.AutoSize = True
        Me.lblDateFormatting.Location = New System.Drawing.Point(33, 317)
        Me.lblDateFormatting.Name = "lblDateFormatting"
        Me.lblDateFormatting.Size = New System.Drawing.Size(79, 13)
        Me.lblDateFormatting.TabIndex = 111
        Me.lblDateFormatting.Text = "Date formatting"
        '
        'btnConnectionsFileSystem
        '
        Me.btnConnectionsFileSystem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnectionsFileSystem.Image = Global.Sequenchel.My.Resources.Resources.Settings
        Me.btnConnectionsFileSystem.Location = New System.Drawing.Point(126, 102)
        Me.btnConnectionsFileSystem.Name = "btnConnectionsFileSystem"
        Me.btnConnectionsFileSystem.Size = New System.Drawing.Size(23, 23)
        Me.btnConnectionsFileSystem.TabIndex = 110
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnConnectionsFileSystem, "System Connections File Name and Path")
        '
        'btnSettingsFileSystem
        '
        Me.btnSettingsFileSystem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSettingsFileSystem.Image = Global.Sequenchel.My.Resources.Resources.Settings
        Me.btnSettingsFileSystem.Location = New System.Drawing.Point(126, 77)
        Me.btnSettingsFileSystem.Name = "btnSettingsFileSystem"
        Me.btnSettingsFileSystem.Size = New System.Drawing.Size(23, 23)
        Me.btnSettingsFileSystem.TabIndex = 109
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnSettingsFileSystem, "System Settings File Name and Path")
        '
        'grpSecurity
        '
        Me.grpSecurity.Controls.Add(Me.btnShowOverridePassword)
        Me.grpSecurity.Controls.Add(Me.chkAllowSmartUpdate)
        Me.grpSecurity.Controls.Add(Me.chkAllowDataImport)
        Me.grpSecurity.Controls.Add(Me.chkAllowQueryEdit)
        Me.grpSecurity.Controls.Add(Me.lblUsageText)
        Me.grpSecurity.Controls.Add(Me.chkAllowSettingsChange)
        Me.grpSecurity.Controls.Add(Me.lblUsage)
        Me.grpSecurity.Controls.Add(Me.chkAllowConfiguration)
        Me.grpSecurity.Controls.Add(Me.txtOverridePassword)
        Me.grpSecurity.Controls.Add(Me.chkAllowLinkedServers)
        Me.grpSecurity.Controls.Add(Me.lblOverridePassword)
        Me.grpSecurity.Controls.Add(Me.chkAllowUpdate)
        Me.grpSecurity.Controls.Add(Me.chkAllowDelete)
        Me.grpSecurity.Controls.Add(Me.chkAllowInsert)
        Me.grpSecurity.Location = New System.Drawing.Point(30, 132)
        Me.grpSecurity.Name = "grpSecurity"
        Me.grpSecurity.Size = New System.Drawing.Size(532, 147)
        Me.grpSecurity.TabIndex = 108
        Me.grpSecurity.TabStop = False
        Me.grpSecurity.Text = "Security"
        '
        'btnShowOverridePassword
        '
        Me.btnShowOverridePassword.Image = Global.Sequenchel.My.Resources.Resources.eye
        Me.btnShowOverridePassword.Location = New System.Drawing.Point(333, 96)
        Me.btnShowOverridePassword.Name = "btnShowOverridePassword"
        Me.btnShowOverridePassword.Size = New System.Drawing.Size(23, 23)
        Me.btnShowOverridePassword.TabIndex = 110
        Me.btnShowOverridePassword.UseVisualStyleBackColor = True
        '
        'chkAllowSmartUpdate
        '
        Me.chkAllowSmartUpdate.AutoSize = True
        Me.chkAllowSmartUpdate.Location = New System.Drawing.Point(175, 70)
        Me.chkAllowSmartUpdate.Name = "chkAllowSmartUpdate"
        Me.chkAllowSmartUpdate.Size = New System.Drawing.Size(181, 17)
        Me.chkAllowSmartUpdate.TabIndex = 109
        Me.chkAllowSmartUpdate.Text = "Allow SmartUpdate Configuration"
        Me.chkAllowSmartUpdate.UseVisualStyleBackColor = True
        '
        'chkAllowDataImport
        '
        Me.chkAllowDataImport.AutoSize = True
        Me.chkAllowDataImport.Location = New System.Drawing.Point(175, 47)
        Me.chkAllowDataImport.Name = "chkAllowDataImport"
        Me.chkAllowDataImport.Size = New System.Drawing.Size(109, 17)
        Me.chkAllowDataImport.TabIndex = 108
        Me.chkAllowDataImport.Text = "Allow Data Import"
        Me.chkAllowDataImport.UseVisualStyleBackColor = True
        '
        'chkAllowQueryEdit
        '
        Me.chkAllowQueryEdit.AutoSize = True
        Me.chkAllowQueryEdit.Location = New System.Drawing.Point(175, 24)
        Me.chkAllowQueryEdit.Name = "chkAllowQueryEdit"
        Me.chkAllowQueryEdit.Size = New System.Drawing.Size(138, 17)
        Me.chkAllowQueryEdit.TabIndex = 52
        Me.chkAllowQueryEdit.Text = "Allow Report Query Edit"
        Me.chkAllowQueryEdit.UseVisualStyleBackColor = True
        '
        'lblUsageText
        '
        Me.lblUsageText.AutoSize = True
        Me.lblUsageText.Location = New System.Drawing.Point(153, 126)
        Me.lblUsageText.Name = "lblUsageText"
        Me.lblUsageText.Size = New System.Drawing.Size(277, 13)
        Me.lblUsageText.TabIndex = 107
        Me.lblUsageText.Text = "Start: Sequenchel.exe /securityoverride:<YourPassword>"
        '
        'chkAllowSettingsChange
        '
        Me.chkAllowSettingsChange.AutoSize = True
        Me.chkAllowSettingsChange.Location = New System.Drawing.Point(6, 24)
        Me.chkAllowSettingsChange.Name = "chkAllowSettingsChange"
        Me.chkAllowSettingsChange.Size = New System.Drawing.Size(132, 17)
        Me.chkAllowSettingsChange.TabIndex = 53
        Me.chkAllowSettingsChange.Text = "Allow Settings Change"
        Me.chkAllowSettingsChange.UseVisualStyleBackColor = True
        '
        'lblUsage
        '
        Me.lblUsage.AutoSize = True
        Me.lblUsage.Location = New System.Drawing.Point(3, 126)
        Me.lblUsage.Name = "lblUsage"
        Me.lblUsage.Size = New System.Drawing.Size(38, 13)
        Me.lblUsage.TabIndex = 106
        Me.lblUsage.Text = "Usage"
        '
        'chkAllowConfiguration
        '
        Me.chkAllowConfiguration.AutoSize = True
        Me.chkAllowConfiguration.Location = New System.Drawing.Point(6, 47)
        Me.chkAllowConfiguration.Name = "chkAllowConfiguration"
        Me.chkAllowConfiguration.Size = New System.Drawing.Size(156, 17)
        Me.chkAllowConfiguration.TabIndex = 54
        Me.chkAllowConfiguration.Text = "Allow Configuration Change"
        Me.chkAllowConfiguration.UseVisualStyleBackColor = True
        '
        'txtOverridePassword
        '
        Me.txtOverridePassword.Location = New System.Drawing.Point(150, 98)
        Me.txtOverridePassword.Name = "txtOverridePassword"
        Me.txtOverridePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOverridePassword.Size = New System.Drawing.Size(180, 20)
        Me.txtOverridePassword.TabIndex = 105
        Me.txtOverridePassword.Tag = "<Keep Current Password>"
        '
        'chkAllowLinkedServers
        '
        Me.chkAllowLinkedServers.AutoSize = True
        Me.chkAllowLinkedServers.Location = New System.Drawing.Point(6, 70)
        Me.chkAllowLinkedServers.Name = "chkAllowLinkedServers"
        Me.chkAllowLinkedServers.Size = New System.Drawing.Size(160, 17)
        Me.chkAllowLinkedServers.TabIndex = 55
        Me.chkAllowLinkedServers.Text = "Allow Linked Server Change"
        Me.chkAllowLinkedServers.UseVisualStyleBackColor = True
        '
        'lblOverridePassword
        '
        Me.lblOverridePassword.AutoSize = True
        Me.lblOverridePassword.Location = New System.Drawing.Point(3, 101)
        Me.lblOverridePassword.Name = "lblOverridePassword"
        Me.lblOverridePassword.Size = New System.Drawing.Size(96, 13)
        Me.lblOverridePassword.TabIndex = 104
        Me.lblOverridePassword.Text = "Override Password"
        '
        'chkAllowUpdate
        '
        Me.chkAllowUpdate.AutoSize = True
        Me.chkAllowUpdate.Location = New System.Drawing.Point(366, 24)
        Me.chkAllowUpdate.Name = "chkAllowUpdate"
        Me.chkAllowUpdate.Size = New System.Drawing.Size(89, 17)
        Me.chkAllowUpdate.TabIndex = 56
        Me.chkAllowUpdate.Text = "Allow Update"
        Me.chkAllowUpdate.UseVisualStyleBackColor = True
        '
        'chkAllowDelete
        '
        Me.chkAllowDelete.AutoSize = True
        Me.chkAllowDelete.Location = New System.Drawing.Point(366, 70)
        Me.chkAllowDelete.Name = "chkAllowDelete"
        Me.chkAllowDelete.Size = New System.Drawing.Size(85, 17)
        Me.chkAllowDelete.TabIndex = 57
        Me.chkAllowDelete.Text = "Allow Delete"
        Me.chkAllowDelete.UseVisualStyleBackColor = True
        '
        'chkAllowInsert
        '
        Me.chkAllowInsert.AutoSize = True
        Me.chkAllowInsert.Location = New System.Drawing.Point(366, 47)
        Me.chkAllowInsert.Name = "chkAllowInsert"
        Me.chkAllowInsert.Size = New System.Drawing.Size(80, 17)
        Me.chkAllowInsert.TabIndex = 58
        Me.chkAllowInsert.Text = "Allow Insert"
        Me.chkAllowInsert.UseVisualStyleBackColor = True
        '
        'btnConnectionsFileDefault
        '
        Me.btnConnectionsFileDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnectionsFileDefault.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnConnectionsFileDefault.Location = New System.Drawing.Point(151, 103)
        Me.btnConnectionsFileDefault.Name = "btnConnectionsFileDefault"
        Me.btnConnectionsFileDefault.Size = New System.Drawing.Size(23, 23)
        Me.btnConnectionsFileDefault.TabIndex = 103
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnConnectionsFileDefault, "Default Connections File Name and Path")
        '
        'btnSettingsFileDefault
        '
        Me.btnSettingsFileDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSettingsFileDefault.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnSettingsFileDefault.Location = New System.Drawing.Point(151, 77)
        Me.btnSettingsFileDefault.Name = "btnSettingsFileDefault"
        Me.btnSettingsFileDefault.Size = New System.Drawing.Size(23, 23)
        Me.btnSettingsFileDefault.TabIndex = 102
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnSettingsFileDefault, "Default Settings File Name and Path")
        '
        'btnDefaultConfigFilePath
        '
        Me.btnDefaultConfigFilePath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefaultConfigFilePath.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnDefaultConfigFilePath.Location = New System.Drawing.Point(151, 51)
        Me.btnDefaultConfigFilePath.Name = "btnDefaultConfigFilePath"
        Me.btnDefaultConfigFilePath.Size = New System.Drawing.Size(23, 23)
        Me.btnDefaultConfigFilePath.TabIndex = 101
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnDefaultConfigFilePath, "Aplication Startup Path")
        '
        'btnConfigFilePath
        '
        Me.btnConfigFilePath.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnConfigFilePath.Location = New System.Drawing.Point(539, 51)
        Me.btnConfigFilePath.Name = "btnConfigFilePath"
        Me.btnConfigFilePath.Size = New System.Drawing.Size(23, 23)
        Me.btnConfigFilePath.TabIndex = 61
        Me.btnConfigFilePath.Text = "..."
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnConfigFilePath, "Browse for Folder")
        Me.btnConfigFilePath.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Default Config File Path"
        '
        'txtDefaultConfigFilePath
        '
        Me.txtDefaultConfigFilePath.Location = New System.Drawing.Point(180, 53)
        Me.txtDefaultConfigFilePath.Name = "txtDefaultConfigFilePath"
        Me.txtDefaultConfigFilePath.Size = New System.Drawing.Size(353, 20)
        Me.txtDefaultConfigFilePath.TabIndex = 59
        '
        'btnConnectionsFile
        '
        Me.btnConnectionsFile.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnConnectionsFile.Location = New System.Drawing.Point(539, 103)
        Me.btnConnectionsFile.Name = "btnConnectionsFile"
        Me.btnConnectionsFile.Size = New System.Drawing.Size(23, 23)
        Me.btnConnectionsFile.TabIndex = 51
        Me.btnConnectionsFile.Text = "..."
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnConnectionsFile, "Browse for File")
        Me.btnConnectionsFile.UseVisualStyleBackColor = True
        '
        'btnSettingsFile
        '
        Me.btnSettingsFile.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnSettingsFile.Location = New System.Drawing.Point(539, 77)
        Me.btnSettingsFile.Name = "btnSettingsFile"
        Me.btnSettingsFile.Size = New System.Drawing.Size(23, 23)
        Me.btnSettingsFile.TabIndex = 50
        Me.btnSettingsFile.Text = "..."
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnSettingsFile, "Browse for File")
        Me.btnSettingsFile.UseVisualStyleBackColor = True
        '
        'btnSettingsGeneralSave
        '
        Me.btnSettingsGeneralSave.Location = New System.Drawing.Point(427, 19)
        Me.btnSettingsGeneralSave.Name = "btnSettingsGeneralSave"
        Me.btnSettingsGeneralSave.Size = New System.Drawing.Size(135, 23)
        Me.btnSettingsGeneralSave.TabIndex = 49
        Me.btnSettingsGeneralSave.Text = "Save General Settings"
        Me.btnSettingsGeneralSave.UseVisualStyleBackColor = True
        '
        'lblConnectionsFile
        '
        Me.lblConnectionsFile.AutoSize = True
        Me.lblConnectionsFile.Location = New System.Drawing.Point(27, 108)
        Me.lblConnectionsFile.Name = "lblConnectionsFile"
        Me.lblConnectionsFile.Size = New System.Drawing.Size(85, 13)
        Me.lblConnectionsFile.TabIndex = 11
        Me.lblConnectionsFile.Text = "Connections File"
        '
        'txtConnectionsFile
        '
        Me.txtConnectionsFile.Location = New System.Drawing.Point(180, 105)
        Me.txtConnectionsFile.Name = "txtConnectionsFile"
        Me.txtConnectionsFile.Size = New System.Drawing.Size(353, 20)
        Me.txtConnectionsFile.TabIndex = 10
        '
        'lblSettingsFile
        '
        Me.lblSettingsFile.AutoSize = True
        Me.lblSettingsFile.Location = New System.Drawing.Point(27, 82)
        Me.lblSettingsFile.Name = "lblSettingsFile"
        Me.lblSettingsFile.Size = New System.Drawing.Size(64, 13)
        Me.lblSettingsFile.TabIndex = 9
        Me.lblSettingsFile.Text = "Settings File"
        '
        'txtSettingsFile
        '
        Me.txtSettingsFile.Location = New System.Drawing.Point(180, 79)
        Me.txtSettingsFile.Name = "txtSettingsFile"
        Me.txtSettingsFile.Size = New System.Drawing.Size(353, 20)
        Me.txtSettingsFile.TabIndex = 8
        '
        'tpgLicense
        '
        Me.tpgLicense.Controls.Add(Me.lblLicenseKey)
        Me.tpgLicense.Controls.Add(Me.txtLicenseKey)
        Me.tpgLicense.Controls.Add(Me.btnSaveLicense)
        Me.tpgLicense.Controls.Add(Me.lblLicenseName)
        Me.tpgLicense.Controls.Add(Me.txtLicenseName)
        Me.tpgLicense.Controls.Add(Me.btnValidateLicense)
        Me.tpgLicense.Location = New System.Drawing.Point(4, 22)
        Me.tpgLicense.Name = "tpgLicense"
        Me.tpgLicense.Size = New System.Drawing.Size(603, 368)
        Me.tpgLicense.TabIndex = 10
        Me.tpgLicense.Tag = "tpgLicense"
        Me.tpgLicense.Text = "License"
        Me.tpgLicense.UseVisualStyleBackColor = True
        '
        'lblLicenseKey
        '
        Me.lblLicenseKey.AutoSize = True
        Me.lblLicenseKey.Location = New System.Drawing.Point(46, 80)
        Me.lblLicenseKey.Name = "lblLicenseKey"
        Me.lblLicenseKey.Size = New System.Drawing.Size(65, 13)
        Me.lblLicenseKey.TabIndex = 7
        Me.lblLicenseKey.Text = "License Key"
        '
        'txtLicenseKey
        '
        Me.txtLicenseKey.Location = New System.Drawing.Point(136, 77)
        Me.txtLicenseKey.Name = "txtLicenseKey"
        Me.txtLicenseKey.Size = New System.Drawing.Size(204, 20)
        Me.txtLicenseKey.TabIndex = 6
        '
        'btnSaveLicense
        '
        Me.btnSaveLicense.Location = New System.Drawing.Point(390, 232)
        Me.btnSaveLicense.Name = "btnSaveLicense"
        Me.btnSaveLicense.Size = New System.Drawing.Size(127, 23)
        Me.btnSaveLicense.TabIndex = 3
        Me.btnSaveLicense.Text = "Save"
        Me.btnSaveLicense.UseVisualStyleBackColor = True
        '
        'lblLicenseName
        '
        Me.lblLicenseName.AutoSize = True
        Me.lblLicenseName.Location = New System.Drawing.Point(46, 54)
        Me.lblLicenseName.Name = "lblLicenseName"
        Me.lblLicenseName.Size = New System.Drawing.Size(75, 13)
        Me.lblLicenseName.TabIndex = 2
        Me.lblLicenseName.Text = "License Name"
        '
        'txtLicenseName
        '
        Me.txtLicenseName.Location = New System.Drawing.Point(136, 51)
        Me.txtLicenseName.Name = "txtLicenseName"
        Me.txtLicenseName.Size = New System.Drawing.Size(204, 20)
        Me.txtLicenseName.TabIndex = 1
        '
        'btnValidateLicense
        '
        Me.btnValidateLicense.Location = New System.Drawing.Point(390, 203)
        Me.btnValidateLicense.Name = "btnValidateLicense"
        Me.btnValidateLicense.Size = New System.Drawing.Size(127, 23)
        Me.btnValidateLicense.TabIndex = 0
        Me.btnValidateLicense.Text = "Validate"
        Me.btnValidateLicense.UseVisualStyleBackColor = True
        '
        'tpgLogging
        '
        Me.tpgLogging.Controls.Add(Me.btnLogLocationBrowse)
        Me.tpgLogging.Controls.Add(Me.btnLogLocationDatabase)
        Me.tpgLogging.Controls.Add(Me.btnLogfileNameDefault)
        Me.tpgLogging.Controls.Add(Me.btnLogLocationSystem)
        Me.tpgLogging.Controls.Add(Me.btnLogLocationDefault)
        Me.tpgLogging.Controls.Add(Me.grpLogsToKeep)
        Me.tpgLogging.Controls.Add(Me.txtLogfileLocation)
        Me.tpgLogging.Controls.Add(Me.txtLogfileName)
        Me.tpgLogging.Controls.Add(Me.btnSaveSettingsLog)
        Me.tpgLogging.Controls.Add(Me.lblLogfileName)
        Me.tpgLogging.Controls.Add(Me.lblLogfileLocation)
        Me.tpgLogging.Controls.Add(Me.rbtLoggingLevel5)
        Me.tpgLogging.Controls.Add(Me.lblLoggingLevel)
        Me.tpgLogging.Controls.Add(Me.rbtLoggingLevel1)
        Me.tpgLogging.Controls.Add(Me.rbtLoggingLevel2)
        Me.tpgLogging.Controls.Add(Me.rbtLoggingLevel3)
        Me.tpgLogging.Controls.Add(Me.rbtLoggingLevel4)
        Me.tpgLogging.Controls.Add(Me.rbtLoggingLevel0)
        Me.tpgLogging.Location = New System.Drawing.Point(4, 22)
        Me.tpgLogging.Name = "tpgLogging"
        Me.tpgLogging.Size = New System.Drawing.Size(603, 368)
        Me.tpgLogging.TabIndex = 6
        Me.tpgLogging.Tag = "tpgLogging"
        Me.tpgLogging.Text = "Logging"
        Me.tpgLogging.UseVisualStyleBackColor = True
        '
        'btnLogLocationBrowse
        '
        Me.btnLogLocationBrowse.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnLogLocationBrowse.Location = New System.Drawing.Point(539, 258)
        Me.btnLogLocationBrowse.Name = "btnLogLocationBrowse"
        Me.btnLogLocationBrowse.Size = New System.Drawing.Size(23, 23)
        Me.btnLogLocationBrowse.TabIndex = 107
        Me.btnLogLocationBrowse.Text = "..."
        Me.btnLogLocationBrowse.UseVisualStyleBackColor = True
        '
        'btnLogLocationDatabase
        '
        Me.btnLogLocationDatabase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogLocationDatabase.Image = Global.Sequenchel.My.Resources.Resources.Database
        Me.btnLogLocationDatabase.Location = New System.Drawing.Point(110, 258)
        Me.btnLogLocationDatabase.Name = "btnLogLocationDatabase"
        Me.btnLogLocationDatabase.Size = New System.Drawing.Size(23, 23)
        Me.btnLogLocationDatabase.TabIndex = 106
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnLogLocationDatabase, "Log to Database")
        '
        'btnLogfileNameDefault
        '
        Me.btnLogfileNameDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogfileNameDefault.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnLogfileNameDefault.Location = New System.Drawing.Point(158, 232)
        Me.btnLogfileNameDefault.Name = "btnLogfileNameDefault"
        Me.btnLogfileNameDefault.Size = New System.Drawing.Size(23, 23)
        Me.btnLogfileNameDefault.TabIndex = 105
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnLogfileNameDefault, "Default Logfile Name")
        '
        'btnLogLocationSystem
        '
        Me.btnLogLocationSystem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogLocationSystem.Image = Global.Sequenchel.My.Resources.Resources.Settings
        Me.btnLogLocationSystem.Location = New System.Drawing.Point(134, 258)
        Me.btnLogLocationSystem.Name = "btnLogLocationSystem"
        Me.btnLogLocationSystem.Size = New System.Drawing.Size(23, 23)
        Me.btnLogLocationSystem.TabIndex = 104
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnLogLocationSystem, "System Logfile Location")
        '
        'btnLogLocationDefault
        '
        Me.btnLogLocationDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLogLocationDefault.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnLogLocationDefault.Location = New System.Drawing.Point(158, 258)
        Me.btnLogLocationDefault.Name = "btnLogLocationDefault"
        Me.btnLogLocationDefault.Size = New System.Drawing.Size(23, 23)
        Me.btnLogLocationDefault.TabIndex = 102
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnLogLocationDefault, "Default Logfile Location")
        '
        'grpLogsToKeep
        '
        Me.grpLogsToKeep.Controls.Add(Me.chkAutoDeleteOldLogs)
        Me.grpLogsToKeep.Controls.Add(Me.btnClearOldLogs)
        Me.grpLogsToKeep.Controls.Add(Me.rbtKeepLogMonth)
        Me.grpLogsToKeep.Controls.Add(Me.rbtKeepLogWeek)
        Me.grpLogsToKeep.Controls.Add(Me.rbtKeepLogDay)
        Me.grpLogsToKeep.Location = New System.Drawing.Point(410, 71)
        Me.grpLogsToKeep.Margin = New System.Windows.Forms.Padding(2)
        Me.grpLogsToKeep.Name = "grpLogsToKeep"
        Me.grpLogsToKeep.Padding = New System.Windows.Forms.Padding(2)
        Me.grpLogsToKeep.Size = New System.Drawing.Size(152, 149)
        Me.grpLogsToKeep.TabIndex = 55
        Me.grpLogsToKeep.TabStop = False
        Me.grpLogsToKeep.Text = "Remove Logs older than:"
        Me.grpLogsToKeep.Visible = False
        '
        'chkAutoDeleteOldLogs
        '
        Me.chkAutoDeleteOldLogs.AutoSize = True
        Me.chkAutoDeleteOldLogs.Location = New System.Drawing.Point(8, 119)
        Me.chkAutoDeleteOldLogs.Name = "chkAutoDeleteOldLogs"
        Me.chkAutoDeleteOldLogs.Size = New System.Drawing.Size(119, 17)
        Me.chkAutoDeleteOldLogs.TabIndex = 61
        Me.chkAutoDeleteOldLogs.Text = "Auto delete old logs"
        Me.chkAutoDeleteOldLogs.UseVisualStyleBackColor = True
        '
        'btnClearOldLogs
        '
        Me.btnClearOldLogs.Location = New System.Drawing.Point(8, 86)
        Me.btnClearOldLogs.Name = "btnClearOldLogs"
        Me.btnClearOldLogs.Size = New System.Drawing.Size(140, 23)
        Me.btnClearOldLogs.TabIndex = 57
        Me.btnClearOldLogs.Text = "Clear Old Logs"
        Me.btnClearOldLogs.UseVisualStyleBackColor = True
        '
        'rbtKeepLogMonth
        '
        Me.rbtKeepLogMonth.AutoSize = True
        Me.rbtKeepLogMonth.Checked = True
        Me.rbtKeepLogMonth.Location = New System.Drawing.Point(8, 63)
        Me.rbtKeepLogMonth.Margin = New System.Windows.Forms.Padding(2)
        Me.rbtKeepLogMonth.Name = "rbtKeepLogMonth"
        Me.rbtKeepLogMonth.Size = New System.Drawing.Size(55, 17)
        Me.rbtKeepLogMonth.TabIndex = 58
        Me.rbtKeepLogMonth.TabStop = True
        Me.rbtKeepLogMonth.Text = "Month"
        Me.rbtKeepLogMonth.UseVisualStyleBackColor = True
        '
        'rbtKeepLogWeek
        '
        Me.rbtKeepLogWeek.AutoSize = True
        Me.rbtKeepLogWeek.Location = New System.Drawing.Point(8, 41)
        Me.rbtKeepLogWeek.Margin = New System.Windows.Forms.Padding(2)
        Me.rbtKeepLogWeek.Name = "rbtKeepLogWeek"
        Me.rbtKeepLogWeek.Size = New System.Drawing.Size(54, 17)
        Me.rbtKeepLogWeek.TabIndex = 59
        Me.rbtKeepLogWeek.Text = "Week"
        Me.rbtKeepLogWeek.UseVisualStyleBackColor = True
        '
        'rbtKeepLogDay
        '
        Me.rbtKeepLogDay.AutoSize = True
        Me.rbtKeepLogDay.Location = New System.Drawing.Point(8, 20)
        Me.rbtKeepLogDay.Margin = New System.Windows.Forms.Padding(2)
        Me.rbtKeepLogDay.Name = "rbtKeepLogDay"
        Me.rbtKeepLogDay.Size = New System.Drawing.Size(44, 17)
        Me.rbtKeepLogDay.TabIndex = 60
        Me.rbtKeepLogDay.Text = "Day"
        Me.rbtKeepLogDay.UseVisualStyleBackColor = True
        '
        'txtLogfileLocation
        '
        Me.txtLogfileLocation.Location = New System.Drawing.Point(187, 260)
        Me.txtLogfileLocation.Name = "txtLogfileLocation"
        Me.txtLogfileLocation.Size = New System.Drawing.Size(346, 20)
        Me.txtLogfileLocation.TabIndex = 47
        '
        'txtLogfileName
        '
        Me.txtLogfileName.Location = New System.Drawing.Point(187, 234)
        Me.txtLogfileName.Name = "txtLogfileName"
        Me.txtLogfileName.Size = New System.Drawing.Size(375, 20)
        Me.txtLogfileName.TabIndex = 46
        '
        'btnSaveSettingsLog
        '
        Me.btnSaveSettingsLog.Location = New System.Drawing.Point(427, 19)
        Me.btnSaveSettingsLog.Name = "btnSaveSettingsLog"
        Me.btnSaveSettingsLog.Size = New System.Drawing.Size(135, 23)
        Me.btnSaveSettingsLog.TabIndex = 48
        Me.btnSaveSettingsLog.Text = "Save Logging Settings"
        Me.btnSaveSettingsLog.UseVisualStyleBackColor = True
        '
        'lblLogfileName
        '
        Me.lblLogfileName.AutoSize = True
        Me.lblLogfileName.Location = New System.Drawing.Point(24, 237)
        Me.lblLogfileName.Name = "lblLogfileName"
        Me.lblLogfileName.Size = New System.Drawing.Size(69, 13)
        Me.lblLogfileName.TabIndex = 8
        Me.lblLogfileName.Text = "Logfile Name"
        '
        'lblLogfileLocation
        '
        Me.lblLogfileLocation.AutoSize = True
        Me.lblLogfileLocation.Location = New System.Drawing.Point(24, 263)
        Me.lblLogfileLocation.Name = "lblLogfileLocation"
        Me.lblLogfileLocation.Size = New System.Drawing.Size(82, 13)
        Me.lblLogfileLocation.TabIndex = 7
        Me.lblLogfileLocation.Text = "Logfile Location"
        '
        'rbtLoggingLevel5
        '
        Me.rbtLoggingLevel5.AutoSize = True
        Me.rbtLoggingLevel5.Location = New System.Drawing.Point(27, 163)
        Me.rbtLoggingLevel5.Name = "rbtLoggingLevel5"
        Me.rbtLoggingLevel5.Size = New System.Drawing.Size(172, 17)
        Me.rbtLoggingLevel5.TabIndex = 45
        Me.rbtLoggingLevel5.Text = "All (includes Database Queries)"
        Me.rbtLoggingLevel5.UseVisualStyleBackColor = True
        '
        'lblLoggingLevel
        '
        Me.lblLoggingLevel.AutoSize = True
        Me.lblLoggingLevel.Location = New System.Drawing.Point(24, 24)
        Me.lblLoggingLevel.Name = "lblLoggingLevel"
        Me.lblLoggingLevel.Size = New System.Drawing.Size(74, 13)
        Me.lblLoggingLevel.TabIndex = 5
        Me.lblLoggingLevel.Text = "Logging Level"
        '
        'rbtLoggingLevel1
        '
        Me.rbtLoggingLevel1.AutoSize = True
        Me.rbtLoggingLevel1.Checked = True
        Me.rbtLoggingLevel1.Location = New System.Drawing.Point(27, 71)
        Me.rbtLoggingLevel1.Name = "rbtLoggingLevel1"
        Me.rbtLoggingLevel1.Size = New System.Drawing.Size(102, 17)
        Me.rbtLoggingLevel1.TabIndex = 41
        Me.rbtLoggingLevel1.TabStop = True
        Me.rbtLoggingLevel1.Text = "Low (errors only)"
        Me.rbtLoggingLevel1.UseVisualStyleBackColor = True
        '
        'rbtLoggingLevel2
        '
        Me.rbtLoggingLevel2.AutoSize = True
        Me.rbtLoggingLevel2.Location = New System.Drawing.Point(27, 94)
        Me.rbtLoggingLevel2.Name = "rbtLoggingLevel2"
        Me.rbtLoggingLevel2.Size = New System.Drawing.Size(186, 17)
        Me.rbtLoggingLevel2.TabIndex = 42
        Me.rbtLoggingLevel2.Text = "Medium (includes program events)"
        Me.rbtLoggingLevel2.UseVisualStyleBackColor = True
        '
        'rbtLoggingLevel3
        '
        Me.rbtLoggingLevel3.AutoSize = True
        Me.rbtLoggingLevel3.Location = New System.Drawing.Point(27, 117)
        Me.rbtLoggingLevel3.Name = "rbtLoggingLevel3"
        Me.rbtLoggingLevel3.Size = New System.Drawing.Size(194, 17)
        Me.rbtLoggingLevel3.TabIndex = 43
        Me.rbtLoggingLevel3.Text = "Medium-High (Includes user events)"
        Me.rbtLoggingLevel3.UseVisualStyleBackColor = True
        '
        'rbtLoggingLevel4
        '
        Me.rbtLoggingLevel4.AutoSize = True
        Me.rbtLoggingLevel4.Location = New System.Drawing.Point(27, 140)
        Me.rbtLoggingLevel4.Name = "rbtLoggingLevel4"
        Me.rbtLoggingLevel4.Size = New System.Drawing.Size(229, 17)
        Me.rbtLoggingLevel4.TabIndex = 44
        Me.rbtLoggingLevel4.Text = "High (Everything except Database Queries)"
        Me.rbtLoggingLevel4.UseVisualStyleBackColor = True
        '
        'rbtLoggingLevel0
        '
        Me.rbtLoggingLevel0.AutoSize = True
        Me.rbtLoggingLevel0.Location = New System.Drawing.Point(27, 48)
        Me.rbtLoggingLevel0.Name = "rbtLoggingLevel0"
        Me.rbtLoggingLevel0.Size = New System.Drawing.Size(196, 17)
        Me.rbtLoggingLevel0.TabIndex = 40
        Me.rbtLoggingLevel0.Text = "Off (Program Startup && Security only)"
        Me.rbtLoggingLevel0.UseVisualStyleBackColor = True
        '
        'tpgDatabase
        '
        Me.tpgDatabase.Controls.Add(Me.btnShowDatabasePassword)
        Me.tpgDatabase.Controls.Add(Me.btnRefreshDatabase)
        Me.tpgDatabase.Controls.Add(Me.btnTestConnection)
        Me.tpgDatabase.Controls.Add(Me.lblBackupPath)
        Me.tpgDatabase.Controls.Add(Me.txtBackupDatabase)
        Me.tpgDatabase.Controls.Add(Me.lblBackupDatabase)
        Me.tpgDatabase.Controls.Add(Me.btnBackupDatabase)
        Me.tpgDatabase.Controls.Add(Me.txtUpgradeDatabase)
        Me.tpgDatabase.Controls.Add(Me.lblDatabaseVersion)
        Me.tpgDatabase.Controls.Add(Me.btnUpgradeDatabase)
        Me.tpgDatabase.Controls.Add(Me.prbCreateDatabase)
        Me.tpgDatabase.Controls.Add(Me.btnCreateExtraProcs)
        Me.tpgDatabase.Controls.Add(Me.btnCreateDatabase)
        Me.tpgDatabase.Controls.Add(Me.btnSaveSettingsDatabase)
        Me.tpgDatabase.Controls.Add(Me.btnDatabaseDefaultsUse)
        Me.tpgDatabase.Controls.Add(Me.lblLoginMethod)
        Me.tpgDatabase.Controls.Add(Me.lblLoginName)
        Me.tpgDatabase.Controls.Add(Me.lblPassword)
        Me.tpgDatabase.Controls.Add(Me.txtPassword)
        Me.tpgDatabase.Controls.Add(Me.cbxLoginMethod)
        Me.tpgDatabase.Controls.Add(Me.txtLoginName)
        Me.tpgDatabase.Controls.Add(Me.lblDatabaseName)
        Me.tpgDatabase.Controls.Add(Me.txtDatabaseName)
        Me.tpgDatabase.Controls.Add(Me.lblDatabaseLocation)
        Me.tpgDatabase.Controls.Add(Me.cbxDataProvider)
        Me.tpgDatabase.Controls.Add(Me.txtDatabaseLocation)
        Me.tpgDatabase.Controls.Add(Me.lblDataProvider)
        Me.tpgDatabase.Controls.Add(Me.lblStatusDatabase)
        Me.tpgDatabase.Location = New System.Drawing.Point(4, 22)
        Me.tpgDatabase.Name = "tpgDatabase"
        Me.tpgDatabase.Size = New System.Drawing.Size(603, 368)
        Me.tpgDatabase.TabIndex = 7
        Me.tpgDatabase.Tag = "tpgDatabase"
        Me.tpgDatabase.Text = "Database"
        Me.tpgDatabase.UseVisualStyleBackColor = True
        '
        'btnShowDatabasePassword
        '
        Me.btnShowDatabasePassword.Enabled = False
        Me.btnShowDatabasePassword.Image = Global.Sequenchel.My.Resources.Resources.eye
        Me.btnShowDatabasePassword.Location = New System.Drawing.Point(331, 151)
        Me.btnShowDatabasePassword.Name = "btnShowDatabasePassword"
        Me.btnShowDatabasePassword.Size = New System.Drawing.Size(23, 23)
        Me.btnShowDatabasePassword.TabIndex = 89
        Me.btnShowDatabasePassword.UseVisualStyleBackColor = True
        '
        'btnRefreshDatabase
        '
        Me.btnRefreshDatabase.Location = New System.Drawing.Point(427, 227)
        Me.btnRefreshDatabase.Name = "btnRefreshDatabase"
        Me.btnRefreshDatabase.Size = New System.Drawing.Size(135, 23)
        Me.btnRefreshDatabase.TabIndex = 88
        Me.btnRefreshDatabase.Text = "Refresh Database"
        Me.btnRefreshDatabase.UseVisualStyleBackColor = True
        '
        'btnTestConnection
        '
        Me.btnTestConnection.Location = New System.Drawing.Point(427, 47)
        Me.btnTestConnection.Name = "btnTestConnection"
        Me.btnTestConnection.Size = New System.Drawing.Size(135, 23)
        Me.btnTestConnection.TabIndex = 87
        Me.btnTestConnection.Text = "Test Connection"
        Me.btnTestConnection.UseVisualStyleBackColor = True
        '
        'lblBackupPath
        '
        Me.lblBackupPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupPath.Location = New System.Drawing.Point(26, 278)
        Me.lblBackupPath.Name = "lblBackupPath"
        Me.lblBackupPath.Size = New System.Drawing.Size(328, 31)
        Me.lblBackupPath.TabIndex = 86
        Me.lblBackupPath.Text = "This must be a valid location on the PC or Server where the database resides"
        '
        'txtBackupDatabase
        '
        Me.txtBackupDatabase.Location = New System.Drawing.Point(165, 258)
        Me.txtBackupDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBackupDatabase.Name = "txtBackupDatabase"
        Me.txtBackupDatabase.Size = New System.Drawing.Size(190, 20)
        Me.txtBackupDatabase.TabIndex = 85
        '
        'lblBackupDatabase
        '
        Me.lblBackupDatabase.AutoSize = True
        Me.lblBackupDatabase.Location = New System.Drawing.Point(24, 260)
        Me.lblBackupDatabase.Name = "lblBackupDatabase"
        Me.lblBackupDatabase.Size = New System.Drawing.Size(125, 13)
        Me.lblBackupDatabase.TabIndex = 84
        Me.lblBackupDatabase.Text = "Backup Database Folder"
        '
        'btnBackupDatabase
        '
        Me.btnBackupDatabase.Location = New System.Drawing.Point(427, 256)
        Me.btnBackupDatabase.Name = "btnBackupDatabase"
        Me.btnBackupDatabase.Size = New System.Drawing.Size(135, 23)
        Me.btnBackupDatabase.TabIndex = 83
        Me.btnBackupDatabase.Text = "Backup Database"
        Me.btnBackupDatabase.UseVisualStyleBackColor = True
        '
        'txtUpgradeDatabase
        '
        Me.txtUpgradeDatabase.Location = New System.Drawing.Point(165, 202)
        Me.txtUpgradeDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUpgradeDatabase.Name = "txtUpgradeDatabase"
        Me.txtUpgradeDatabase.ReadOnly = True
        Me.txtUpgradeDatabase.Size = New System.Drawing.Size(190, 20)
        Me.txtUpgradeDatabase.TabIndex = 82
        '
        'lblDatabaseVersion
        '
        Me.lblDatabaseVersion.AutoSize = True
        Me.lblDatabaseVersion.Location = New System.Drawing.Point(24, 203)
        Me.lblDatabaseVersion.Name = "lblDatabaseVersion"
        Me.lblDatabaseVersion.Size = New System.Drawing.Size(91, 13)
        Me.lblDatabaseVersion.TabIndex = 81
        Me.lblDatabaseVersion.Text = "Database Version"
        '
        'btnUpgradeDatabase
        '
        Me.btnUpgradeDatabase.Enabled = False
        Me.btnUpgradeDatabase.Location = New System.Drawing.Point(427, 198)
        Me.btnUpgradeDatabase.Name = "btnUpgradeDatabase"
        Me.btnUpgradeDatabase.Size = New System.Drawing.Size(135, 23)
        Me.btnUpgradeDatabase.TabIndex = 80
        Me.btnUpgradeDatabase.Text = "Update Database"
        Me.btnUpgradeDatabase.UseVisualStyleBackColor = True
        '
        'prbCreateDatabase
        '
        Me.prbCreateDatabase.Location = New System.Drawing.Point(427, 125)
        Me.prbCreateDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.prbCreateDatabase.Name = "prbCreateDatabase"
        Me.prbCreateDatabase.Size = New System.Drawing.Size(135, 19)
        Me.prbCreateDatabase.Step = 1
        Me.prbCreateDatabase.TabIndex = 79
        Me.prbCreateDatabase.Visible = False
        '
        'btnCreateExtraProcs
        '
        Me.btnCreateExtraProcs.Location = New System.Drawing.Point(427, 99)
        Me.btnCreateExtraProcs.Name = "btnCreateExtraProcs"
        Me.btnCreateExtraProcs.Size = New System.Drawing.Size(135, 23)
        Me.btnCreateExtraProcs.TabIndex = 77
        Me.btnCreateExtraProcs.Text = "Create Extra Procs"
        Me.btnCreateExtraProcs.UseVisualStyleBackColor = True
        '
        'btnCreateDatabase
        '
        Me.btnCreateDatabase.Location = New System.Drawing.Point(427, 73)
        Me.btnCreateDatabase.Name = "btnCreateDatabase"
        Me.btnCreateDatabase.Size = New System.Drawing.Size(135, 23)
        Me.btnCreateDatabase.TabIndex = 76
        Me.btnCreateDatabase.Text = "Create Database"
        Me.btnCreateDatabase.UseVisualStyleBackColor = True
        '
        'btnSaveSettingsDatabase
        '
        Me.btnSaveSettingsDatabase.Location = New System.Drawing.Point(427, 19)
        Me.btnSaveSettingsDatabase.Name = "btnSaveSettingsDatabase"
        Me.btnSaveSettingsDatabase.Size = New System.Drawing.Size(135, 23)
        Me.btnSaveSettingsDatabase.TabIndex = 74
        Me.btnSaveSettingsDatabase.Text = "Save Database Settings"
        Me.btnSaveSettingsDatabase.UseVisualStyleBackColor = True
        '
        'btnDatabaseDefaultsUse
        '
        Me.btnDatabaseDefaultsUse.Location = New System.Drawing.Point(427, 147)
        Me.btnDatabaseDefaultsUse.Name = "btnDatabaseDefaultsUse"
        Me.btnDatabaseDefaultsUse.Size = New System.Drawing.Size(135, 23)
        Me.btnDatabaseDefaultsUse.TabIndex = 75
        Me.btnDatabaseDefaultsUse.Text = "Use Defaults"
        Me.btnDatabaseDefaultsUse.UseVisualStyleBackColor = True
        '
        'lblLoginMethod
        '
        Me.lblLoginMethod.AutoSize = True
        Me.lblLoginMethod.Location = New System.Drawing.Point(24, 103)
        Me.lblLoginMethod.Name = "lblLoginMethod"
        Me.lblLoginMethod.Size = New System.Drawing.Size(114, 13)
        Me.lblLoginMethod.TabIndex = 11
        Me.lblLoginMethod.Text = "Authentication Method"
        '
        'lblLoginName
        '
        Me.lblLoginName.AutoSize = True
        Me.lblLoginName.Location = New System.Drawing.Point(24, 130)
        Me.lblLoginName.Name = "lblLoginName"
        Me.lblLoginName.Size = New System.Drawing.Size(64, 13)
        Me.lblLoginName.TabIndex = 10
        Me.lblLoginName.Text = "Login Name"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(24, 156)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 9
        Me.lblPassword.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(165, 153)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(163, 20)
        Me.txtPassword.TabIndex = 73
        Me.txtPassword.Tag = "<Keep Current Password>"
        '
        'cbxLoginMethod
        '
        Me.cbxLoginMethod.FormattingEnabled = True
        Me.cbxLoginMethod.Items.AddRange(New Object() {"Windows", "SQL"})
        Me.cbxLoginMethod.Location = New System.Drawing.Point(165, 100)
        Me.cbxLoginMethod.Name = "cbxLoginMethod"
        Me.cbxLoginMethod.Size = New System.Drawing.Size(100, 21)
        Me.cbxLoginMethod.TabIndex = 71
        Me.cbxLoginMethod.Text = "Windows"
        '
        'txtLoginName
        '
        Me.txtLoginName.Enabled = False
        Me.txtLoginName.Location = New System.Drawing.Point(165, 127)
        Me.txtLoginName.Name = "txtLoginName"
        Me.txtLoginName.Size = New System.Drawing.Size(189, 20)
        Me.txtLoginName.TabIndex = 72
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.Location = New System.Drawing.Point(24, 77)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Size = New System.Drawing.Size(84, 13)
        Me.lblDatabaseName.TabIndex = 5
        Me.lblDatabaseName.Text = "Database Name"
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.Location = New System.Drawing.Point(165, 74)
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.Size = New System.Drawing.Size(189, 20)
        Me.txtDatabaseName.TabIndex = 70
        '
        'lblDatabaseLocation
        '
        Me.lblDatabaseLocation.AutoSize = True
        Me.lblDatabaseLocation.Location = New System.Drawing.Point(24, 51)
        Me.lblDatabaseLocation.Name = "lblDatabaseLocation"
        Me.lblDatabaseLocation.Size = New System.Drawing.Size(97, 13)
        Me.lblDatabaseLocation.TabIndex = 3
        Me.lblDatabaseLocation.Text = "Database Location"
        '
        'cbxDataProvider
        '
        Me.cbxDataProvider.AllowDrop = True
        Me.cbxDataProvider.FormattingEnabled = True
        Me.cbxDataProvider.Items.AddRange(New Object() {"SQL"})
        Me.cbxDataProvider.Location = New System.Drawing.Point(165, 21)
        Me.cbxDataProvider.Name = "cbxDataProvider"
        Me.cbxDataProvider.Size = New System.Drawing.Size(100, 21)
        Me.cbxDataProvider.TabIndex = 68
        Me.cbxDataProvider.Text = "SQL"
        '
        'txtDatabaseLocation
        '
        Me.txtDatabaseLocation.Location = New System.Drawing.Point(165, 48)
        Me.txtDatabaseLocation.Name = "txtDatabaseLocation"
        Me.txtDatabaseLocation.Size = New System.Drawing.Size(190, 20)
        Me.txtDatabaseLocation.TabIndex = 69
        '
        'lblDataProvider
        '
        Me.lblDataProvider.AutoSize = True
        Me.lblDataProvider.Location = New System.Drawing.Point(24, 24)
        Me.lblDataProvider.Name = "lblDataProvider"
        Me.lblDataProvider.Size = New System.Drawing.Size(72, 13)
        Me.lblDataProvider.TabIndex = 0
        Me.lblDataProvider.Text = "Data Provider"
        '
        'lblStatusDatabase
        '
        Me.lblStatusDatabase.AutoSize = True
        Me.lblStatusDatabase.BackColor = System.Drawing.Color.Transparent
        Me.lblStatusDatabase.Location = New System.Drawing.Point(445, 104)
        Me.lblStatusDatabase.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblStatusDatabase.Name = "lblStatusDatabase"
        Me.lblStatusDatabase.Size = New System.Drawing.Size(110, 13)
        Me.lblStatusDatabase.TabIndex = 78
        Me.lblStatusDatabase.Text = "Working. Please wait."
        Me.lblStatusDatabase.Visible = False
        '
        'tpgScheduler
        '
        Me.tpgScheduler.Controls.Add(Me.btnErrorlogPathBrowse)
        Me.tpgScheduler.Controls.Add(Me.btnErrorlogPathDatabase)
        Me.tpgScheduler.Controls.Add(Me.btnErrorlogPathSystem)
        Me.tpgScheduler.Controls.Add(Me.btnErrorlogPathDefault)
        Me.tpgScheduler.Controls.Add(Me.lblJobNamePrefix)
        Me.tpgScheduler.Controls.Add(Me.txtJobNamePrefix)
        Me.tpgScheduler.Controls.Add(Me.chkIncludeHigherSqlVersions)
        Me.tpgScheduler.Controls.Add(Me.lblIncludeHigherSqlVersions)
        Me.tpgScheduler.Controls.Add(Me.Label1)
        Me.tpgScheduler.Controls.Add(Me.lblErrorlogPath)
        Me.tpgScheduler.Controls.Add(Me.txtErrorlogPath)
        Me.tpgScheduler.Controls.Add(Me.nudTimeSpan)
        Me.tpgScheduler.Controls.Add(Me.lblRepeatEvery)
        Me.tpgScheduler.Controls.Add(Me.btnCreateScheduledJob)
        Me.tpgScheduler.Controls.Add(Me.cbxSqlVersion)
        Me.tpgScheduler.Controls.Add(Me.chkMailStatistics)
        Me.tpgScheduler.Controls.Add(Me.lblSeparator)
        Me.tpgScheduler.Controls.Add(Me.lblSqlVersion)
        Me.tpgScheduler.Controls.Add(Me.lblMailStatistics)
        Me.tpgScheduler.Controls.Add(Me.txtSeparator)
        Me.tpgScheduler.Controls.Add(Me.lblExceptionList)
        Me.tpgScheduler.Controls.Add(Me.txtExceptionList)
        Me.tpgScheduler.Controls.Add(Me.lblRecipient)
        Me.tpgScheduler.Controls.Add(Me.txtRecipient)
        Me.tpgScheduler.Controls.Add(Me.cbxEndMinute)
        Me.tpgScheduler.Controls.Add(Me.cbxEndHour)
        Me.tpgScheduler.Controls.Add(Me.lblEndTime)
        Me.tpgScheduler.Controls.Add(Me.lblEndTimeColon)
        Me.tpgScheduler.Controls.Add(Me.cbxStartMinute)
        Me.tpgScheduler.Controls.Add(Me.cbxStartHour)
        Me.tpgScheduler.Controls.Add(Me.lblStartTime)
        Me.tpgScheduler.Controls.Add(Me.cbxTimespan)
        Me.tpgScheduler.Controls.Add(Me.lblOccurence)
        Me.tpgScheduler.Controls.Add(Me.cbxOccurence)
        Me.tpgScheduler.Controls.Add(Me.chkTeusday)
        Me.tpgScheduler.Controls.Add(Me.chkWednesday)
        Me.tpgScheduler.Controls.Add(Me.chkThursday)
        Me.tpgScheduler.Controls.Add(Me.chkFriday)
        Me.tpgScheduler.Controls.Add(Me.chkSaturday)
        Me.tpgScheduler.Controls.Add(Me.chkSunday)
        Me.tpgScheduler.Controls.Add(Me.chkMonday)
        Me.tpgScheduler.Controls.Add(Me.lblProcedure)
        Me.tpgScheduler.Controls.Add(Me.cbxProcedures)
        Me.tpgScheduler.Controls.Add(Me.lblStartTimeColon)
        Me.tpgScheduler.Location = New System.Drawing.Point(4, 22)
        Me.tpgScheduler.Name = "tpgScheduler"
        Me.tpgScheduler.Size = New System.Drawing.Size(603, 368)
        Me.tpgScheduler.TabIndex = 13
        Me.tpgScheduler.Text = "Scheduler"
        Me.tpgScheduler.UseVisualStyleBackColor = True
        '
        'btnErrorlogPathBrowse
        '
        Me.btnErrorlogPathBrowse.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnErrorlogPathBrowse.Location = New System.Drawing.Point(557, 264)
        Me.btnErrorlogPathBrowse.Name = "btnErrorlogPathBrowse"
        Me.btnErrorlogPathBrowse.Size = New System.Drawing.Size(23, 23)
        Me.btnErrorlogPathBrowse.TabIndex = 110
        Me.btnErrorlogPathBrowse.Text = "..."
        Me.btnErrorlogPathBrowse.UseVisualStyleBackColor = True
        '
        'btnErrorlogPathDatabase
        '
        Me.btnErrorlogPathDatabase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnErrorlogPathDatabase.Image = Global.Sequenchel.My.Resources.Resources.Database
        Me.btnErrorlogPathDatabase.Location = New System.Drawing.Point(263, 266)
        Me.btnErrorlogPathDatabase.Name = "btnErrorlogPathDatabase"
        Me.btnErrorlogPathDatabase.Size = New System.Drawing.Size(23, 23)
        Me.btnErrorlogPathDatabase.TabIndex = 109
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnErrorlogPathDatabase, "Database Logfile Location")
        '
        'btnErrorlogPathSystem
        '
        Me.btnErrorlogPathSystem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnErrorlogPathSystem.Image = Global.Sequenchel.My.Resources.Resources.Settings
        Me.btnErrorlogPathSystem.Location = New System.Drawing.Point(287, 266)
        Me.btnErrorlogPathSystem.Name = "btnErrorlogPathSystem"
        Me.btnErrorlogPathSystem.Size = New System.Drawing.Size(23, 23)
        Me.btnErrorlogPathSystem.TabIndex = 108
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnErrorlogPathSystem, "System Logfile Location")
        '
        'btnErrorlogPathDefault
        '
        Me.btnErrorlogPathDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnErrorlogPathDefault.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnErrorlogPathDefault.Location = New System.Drawing.Point(311, 266)
        Me.btnErrorlogPathDefault.Name = "btnErrorlogPathDefault"
        Me.btnErrorlogPathDefault.Size = New System.Drawing.Size(23, 23)
        Me.btnErrorlogPathDefault.TabIndex = 107
        Me.ttpDefaultLogLocation.SetToolTip(Me.btnErrorlogPathDefault, "Default Logfile Location")
        '
        'lblJobNamePrefix
        '
        Me.lblJobNamePrefix.AutoSize = True
        Me.lblJobNamePrefix.Location = New System.Drawing.Point(345, 55)
        Me.lblJobNamePrefix.Name = "lblJobNamePrefix"
        Me.lblJobNamePrefix.Size = New System.Drawing.Size(84, 13)
        Me.lblJobNamePrefix.TabIndex = 42
        Me.lblJobNamePrefix.Text = "Job Name Prefix"
        '
        'txtJobNamePrefix
        '
        Me.txtJobNamePrefix.Location = New System.Drawing.Point(435, 52)
        Me.txtJobNamePrefix.Name = "txtJobNamePrefix"
        Me.txtJobNamePrefix.Size = New System.Drawing.Size(145, 20)
        Me.txtJobNamePrefix.TabIndex = 41
        '
        'chkIncludeHigherSqlVersions
        '
        Me.chkIncludeHigherSqlVersions.AutoSize = True
        Me.chkIncludeHigherSqlVersions.Location = New System.Drawing.Point(565, 246)
        Me.chkIncludeHigherSqlVersions.Name = "chkIncludeHigherSqlVersions"
        Me.chkIncludeHigherSqlVersions.Size = New System.Drawing.Size(15, 14)
        Me.chkIncludeHigherSqlVersions.TabIndex = 40
        Me.chkIncludeHigherSqlVersions.UseVisualStyleBackColor = True
        '
        'lblIncludeHigherSqlVersions
        '
        Me.lblIncludeHigherSqlVersions.AutoSize = True
        Me.lblIncludeHigherSqlVersions.Location = New System.Drawing.Point(416, 246)
        Me.lblIncludeHigherSqlVersions.Name = "lblIncludeHigherSqlVersions"
        Me.lblIncludeHigherSqlVersions.Size = New System.Drawing.Size(143, 13)
        Me.lblIncludeHigherSqlVersions.TabIndex = 39
        Me.lblIncludeHigherSqlVersions.Text = "Include Higher SQL Versions"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(294, 289)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(286, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "The errorlog path should be a valid path on the SQL Server"
        '
        'lblErrorlogPath
        '
        Me.lblErrorlogPath.AutoSize = True
        Me.lblErrorlogPath.Location = New System.Drawing.Point(189, 269)
        Me.lblErrorlogPath.Name = "lblErrorlogPath"
        Me.lblErrorlogPath.Size = New System.Drawing.Size(68, 13)
        Me.lblErrorlogPath.TabIndex = 37
        Me.lblErrorlogPath.Text = "Errorlog Path"
        '
        'txtErrorlogPath
        '
        Me.txtErrorlogPath.Location = New System.Drawing.Point(340, 266)
        Me.txtErrorlogPath.Name = "txtErrorlogPath"
        Me.txtErrorlogPath.Size = New System.Drawing.Size(211, 20)
        Me.txtErrorlogPath.TabIndex = 36
        '
        'nudTimeSpan
        '
        Me.nudTimeSpan.Location = New System.Drawing.Point(435, 111)
        Me.nudTimeSpan.Name = "nudTimeSpan"
        Me.nudTimeSpan.Size = New System.Drawing.Size(49, 20)
        Me.nudTimeSpan.TabIndex = 35
        Me.nudTimeSpan.Visible = False
        '
        'lblRepeatEvery
        '
        Me.lblRepeatEvery.AutoSize = True
        Me.lblRepeatEvery.Location = New System.Drawing.Point(19, 87)
        Me.lblRepeatEvery.Name = "lblRepeatEvery"
        Me.lblRepeatEvery.Size = New System.Drawing.Size(75, 13)
        Me.lblRepeatEvery.TabIndex = 33
        Me.lblRepeatEvery.Text = "Repeat Every:"
        '
        'btnCreateScheduledJob
        '
        Me.btnCreateScheduledJob.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateScheduledJob.Location = New System.Drawing.Point(225, 317)
        Me.btnCreateScheduledJob.Name = "btnCreateScheduledJob"
        Me.btnCreateScheduledJob.Size = New System.Drawing.Size(145, 23)
        Me.btnCreateScheduledJob.TabIndex = 32
        Me.btnCreateScheduledJob.Text = "Create Scheduled Job"
        Me.btnCreateScheduledJob.UseVisualStyleBackColor = True
        '
        'cbxSqlVersion
        '
        Me.cbxSqlVersion.FormattingEnabled = True
        Me.cbxSqlVersion.Items.AddRange(New Object() {"0", "8", "9", "10", "11", "12"})
        Me.cbxSqlVersion.Location = New System.Drawing.Point(538, 218)
        Me.cbxSqlVersion.Name = "cbxSqlVersion"
        Me.cbxSqlVersion.Size = New System.Drawing.Size(42, 21)
        Me.cbxSqlVersion.TabIndex = 31
        Me.cbxSqlVersion.Text = "0"
        '
        'chkMailStatistics
        '
        Me.chkMailStatistics.AutoSize = True
        Me.chkMailStatistics.Location = New System.Drawing.Point(340, 246)
        Me.chkMailStatistics.Name = "chkMailStatistics"
        Me.chkMailStatistics.Size = New System.Drawing.Size(15, 14)
        Me.chkMailStatistics.TabIndex = 30
        Me.chkMailStatistics.UseVisualStyleBackColor = True
        '
        'lblSeparator
        '
        Me.lblSeparator.AutoSize = True
        Me.lblSeparator.Location = New System.Drawing.Point(189, 221)
        Me.lblSeparator.Name = "lblSeparator"
        Me.lblSeparator.Size = New System.Drawing.Size(53, 13)
        Me.lblSeparator.TabIndex = 29
        Me.lblSeparator.Text = "Separator"
        '
        'lblSqlVersion
        '
        Me.lblSqlVersion.AutoSize = True
        Me.lblSqlVersion.Location = New System.Drawing.Point(466, 221)
        Me.lblSqlVersion.Name = "lblSqlVersion"
        Me.lblSqlVersion.Size = New System.Drawing.Size(66, 13)
        Me.lblSqlVersion.TabIndex = 28
        Me.lblSqlVersion.Text = "SQL Version"
        '
        'lblMailStatistics
        '
        Me.lblMailStatistics.AutoSize = True
        Me.lblMailStatistics.Location = New System.Drawing.Point(189, 246)
        Me.lblMailStatistics.Name = "lblMailStatistics"
        Me.lblMailStatistics.Size = New System.Drawing.Size(71, 13)
        Me.lblMailStatistics.TabIndex = 26
        Me.lblMailStatistics.Text = "Mail Statistics"
        '
        'txtSeparator
        '
        Me.txtSeparator.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeparator.Location = New System.Drawing.Point(339, 215)
        Me.txtSeparator.Name = "txtSeparator"
        Me.txtSeparator.Size = New System.Drawing.Size(41, 26)
        Me.txtSeparator.TabIndex = 25
        Me.txtSeparator.Text = ","
        '
        'lblExceptionList
        '
        Me.lblExceptionList.AutoSize = True
        Me.lblExceptionList.Location = New System.Drawing.Point(189, 193)
        Me.lblExceptionList.Name = "lblExceptionList"
        Me.lblExceptionList.Size = New System.Drawing.Size(73, 13)
        Me.lblExceptionList.TabIndex = 24
        Me.lblExceptionList.Text = "Exception List"
        '
        'txtExceptionList
        '
        Me.txtExceptionList.Location = New System.Drawing.Point(339, 190)
        Me.txtExceptionList.Name = "txtExceptionList"
        Me.txtExceptionList.Size = New System.Drawing.Size(241, 20)
        Me.txtExceptionList.TabIndex = 23
        '
        'lblRecipient
        '
        Me.lblRecipient.AutoSize = True
        Me.lblRecipient.Location = New System.Drawing.Point(189, 167)
        Me.lblRecipient.Name = "lblRecipient"
        Me.lblRecipient.Size = New System.Drawing.Size(52, 13)
        Me.lblRecipient.TabIndex = 22
        Me.lblRecipient.Text = "Recipient"
        '
        'txtRecipient
        '
        Me.txtRecipient.Location = New System.Drawing.Point(339, 164)
        Me.txtRecipient.Name = "txtRecipient"
        Me.txtRecipient.Size = New System.Drawing.Size(241, 20)
        Me.txtRecipient.TabIndex = 21
        Me.txtRecipient.Text = "Screen"
        '
        'cbxEndMinute
        '
        Me.cbxEndMinute.FormattingEnabled = True
        Me.cbxEndMinute.Location = New System.Drawing.Point(539, 137)
        Me.cbxEndMinute.Name = "cbxEndMinute"
        Me.cbxEndMinute.Size = New System.Drawing.Size(41, 21)
        Me.cbxEndMinute.TabIndex = 19
        Me.cbxEndMinute.Text = "00"
        Me.cbxEndMinute.Visible = False
        '
        'cbxEndHour
        '
        Me.cbxEndHour.FormattingEnabled = True
        Me.cbxEndHour.Location = New System.Drawing.Point(490, 137)
        Me.cbxEndHour.Name = "cbxEndHour"
        Me.cbxEndHour.Size = New System.Drawing.Size(41, 21)
        Me.cbxEndHour.TabIndex = 18
        Me.cbxEndHour.Text = "00"
        Me.cbxEndHour.Visible = False
        '
        'lblEndTime
        '
        Me.lblEndTime.AutoSize = True
        Me.lblEndTime.Location = New System.Drawing.Point(432, 140)
        Me.lblEndTime.Name = "lblEndTime"
        Me.lblEndTime.Size = New System.Drawing.Size(52, 13)
        Me.lblEndTime.TabIndex = 17
        Me.lblEndTime.Text = "End Time"
        Me.lblEndTime.Visible = False
        '
        'lblEndTimeColon
        '
        Me.lblEndTimeColon.AutoSize = True
        Me.lblEndTimeColon.Location = New System.Drawing.Point(530, 140)
        Me.lblEndTimeColon.Name = "lblEndTimeColon"
        Me.lblEndTimeColon.Size = New System.Drawing.Size(10, 13)
        Me.lblEndTimeColon.TabIndex = 20
        Me.lblEndTimeColon.Text = ":"
        Me.lblEndTimeColon.Visible = False
        '
        'cbxStartMinute
        '
        Me.cbxStartMinute.FormattingEnabled = True
        Me.cbxStartMinute.Location = New System.Drawing.Point(388, 137)
        Me.cbxStartMinute.Name = "cbxStartMinute"
        Me.cbxStartMinute.Size = New System.Drawing.Size(41, 21)
        Me.cbxStartMinute.TabIndex = 15
        Me.cbxStartMinute.Text = "00"
        '
        'cbxStartHour
        '
        Me.cbxStartHour.FormattingEnabled = True
        Me.cbxStartHour.Location = New System.Drawing.Point(339, 137)
        Me.cbxStartHour.Name = "cbxStartHour"
        Me.cbxStartHour.Size = New System.Drawing.Size(41, 21)
        Me.cbxStartHour.TabIndex = 14
        Me.cbxStartHour.Text = "00"
        '
        'lblStartTime
        '
        Me.lblStartTime.AutoSize = True
        Me.lblStartTime.Location = New System.Drawing.Point(189, 140)
        Me.lblStartTime.Name = "lblStartTime"
        Me.lblStartTime.Size = New System.Drawing.Size(55, 13)
        Me.lblStartTime.TabIndex = 13
        Me.lblStartTime.Text = "Start Time"
        '
        'cbxTimespan
        '
        Me.cbxTimespan.FormattingEnabled = True
        Me.cbxTimespan.Items.AddRange(New Object() {"Hour(s)", "Minute(s)"})
        Me.cbxTimespan.Location = New System.Drawing.Point(490, 110)
        Me.cbxTimespan.Name = "cbxTimespan"
        Me.cbxTimespan.Size = New System.Drawing.Size(90, 21)
        Me.cbxTimespan.TabIndex = 12
        Me.cbxTimespan.Text = "Hour(s)"
        Me.cbxTimespan.Visible = False
        '
        'lblOccurence
        '
        Me.lblOccurence.AutoSize = True
        Me.lblOccurence.Location = New System.Drawing.Point(189, 113)
        Me.lblOccurence.Name = "lblOccurence"
        Me.lblOccurence.Size = New System.Drawing.Size(60, 13)
        Me.lblOccurence.TabIndex = 10
        Me.lblOccurence.Text = "Occurence"
        '
        'cbxOccurence
        '
        Me.cbxOccurence.FormattingEnabled = True
        Me.cbxOccurence.Items.AddRange(New Object() {"One Time", "Every"})
        Me.cbxOccurence.Location = New System.Drawing.Point(339, 110)
        Me.cbxOccurence.Name = "cbxOccurence"
        Me.cbxOccurence.Size = New System.Drawing.Size(90, 21)
        Me.cbxOccurence.TabIndex = 9
        Me.cbxOccurence.Text = "One Time"
        '
        'chkTeusday
        '
        Me.chkTeusday.AutoSize = True
        Me.chkTeusday.Location = New System.Drawing.Point(19, 139)
        Me.chkTeusday.Name = "chkTeusday"
        Me.chkTeusday.Size = New System.Drawing.Size(67, 17)
        Me.chkTeusday.TabIndex = 8
        Me.chkTeusday.Text = "Tuesday"
        Me.chkTeusday.UseVisualStyleBackColor = True
        '
        'chkWednesday
        '
        Me.chkWednesday.AutoSize = True
        Me.chkWednesday.Location = New System.Drawing.Point(19, 166)
        Me.chkWednesday.Name = "chkWednesday"
        Me.chkWednesday.Size = New System.Drawing.Size(83, 17)
        Me.chkWednesday.TabIndex = 7
        Me.chkWednesday.Text = "Wednesday"
        Me.chkWednesday.UseVisualStyleBackColor = True
        '
        'chkThursday
        '
        Me.chkThursday.AutoSize = True
        Me.chkThursday.Location = New System.Drawing.Point(19, 192)
        Me.chkThursday.Name = "chkThursday"
        Me.chkThursday.Size = New System.Drawing.Size(70, 17)
        Me.chkThursday.TabIndex = 6
        Me.chkThursday.Text = "Thursday"
        Me.chkThursday.UseVisualStyleBackColor = True
        '
        'chkFriday
        '
        Me.chkFriday.AutoSize = True
        Me.chkFriday.Location = New System.Drawing.Point(18, 217)
        Me.chkFriday.Name = "chkFriday"
        Me.chkFriday.Size = New System.Drawing.Size(54, 17)
        Me.chkFriday.TabIndex = 5
        Me.chkFriday.Text = "Friday"
        Me.chkFriday.UseVisualStyleBackColor = True
        '
        'chkSaturday
        '
        Me.chkSaturday.AutoSize = True
        Me.chkSaturday.Location = New System.Drawing.Point(18, 243)
        Me.chkSaturday.Name = "chkSaturday"
        Me.chkSaturday.Size = New System.Drawing.Size(68, 17)
        Me.chkSaturday.TabIndex = 4
        Me.chkSaturday.Text = "Saturday"
        Me.chkSaturday.UseVisualStyleBackColor = True
        '
        'chkSunday
        '
        Me.chkSunday.AutoSize = True
        Me.chkSunday.Location = New System.Drawing.Point(19, 270)
        Me.chkSunday.Name = "chkSunday"
        Me.chkSunday.Size = New System.Drawing.Size(62, 17)
        Me.chkSunday.TabIndex = 3
        Me.chkSunday.Text = "Sunday"
        Me.chkSunday.UseVisualStyleBackColor = True
        '
        'chkMonday
        '
        Me.chkMonday.AutoSize = True
        Me.chkMonday.Location = New System.Drawing.Point(19, 112)
        Me.chkMonday.Name = "chkMonday"
        Me.chkMonday.Size = New System.Drawing.Size(64, 17)
        Me.chkMonday.TabIndex = 2
        Me.chkMonday.Text = "Monday"
        Me.chkMonday.UseVisualStyleBackColor = True
        '
        'lblProcedure
        '
        Me.lblProcedure.AutoSize = True
        Me.lblProcedure.Location = New System.Drawing.Point(19, 36)
        Me.lblProcedure.Name = "lblProcedure"
        Me.lblProcedure.Size = New System.Drawing.Size(56, 13)
        Me.lblProcedure.TabIndex = 1
        Me.lblProcedure.Text = "Procedure"
        '
        'cbxProcedures
        '
        Me.cbxProcedures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxProcedures.FormattingEnabled = True
        Me.cbxProcedures.Location = New System.Drawing.Point(19, 52)
        Me.cbxProcedures.Name = "cbxProcedures"
        Me.cbxProcedures.Size = New System.Drawing.Size(200, 21)
        Me.cbxProcedures.TabIndex = 0
        '
        'lblStartTimeColon
        '
        Me.lblStartTimeColon.AutoSize = True
        Me.lblStartTimeColon.Location = New System.Drawing.Point(379, 140)
        Me.lblStartTimeColon.Name = "lblStartTimeColon"
        Me.lblStartTimeColon.Size = New System.Drawing.Size(10, 13)
        Me.lblStartTimeColon.TabIndex = 16
        Me.lblStartTimeColon.Text = ":"
        '
        'tpgMonitorDataspaces
        '
        Me.tpgMonitorDataspaces.Controls.Add(Me.btnMonitorDataSpacesLoad)
        Me.tpgMonitorDataspaces.Controls.Add(Me.RichTextBox1)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblUpperLimitHelp)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblLowerLimitHelp)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblLargeGrowthHelp)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblMediumGrowthHelp)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblSmallGrowthHelp)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblMinFreeSpaceHelp)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblMinPercGrowthHelp)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblMinPercGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.txtMinPercGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblMinFreeSpace)
        Me.tpgMonitorDataspaces.Controls.Add(Me.txtMinFreeSpace)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblLargeGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.txtLargeGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblMediumGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.txtMediumGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblSmallGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.txtSmallGrowth)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblUpperLimit)
        Me.tpgMonitorDataspaces.Controls.Add(Me.txtUpperLimit)
        Me.tpgMonitorDataspaces.Controls.Add(Me.lblLowerLimit)
        Me.tpgMonitorDataspaces.Controls.Add(Me.txtLowerLimit)
        Me.tpgMonitorDataspaces.Controls.Add(Me.btnMonitorDataSpacesSave)
        Me.tpgMonitorDataspaces.Location = New System.Drawing.Point(4, 22)
        Me.tpgMonitorDataspaces.Name = "tpgMonitorDataspaces"
        Me.tpgMonitorDataspaces.Size = New System.Drawing.Size(603, 368)
        Me.tpgMonitorDataspaces.TabIndex = 11
        Me.tpgMonitorDataspaces.Text = "Monitor DataSpaces"
        Me.tpgMonitorDataspaces.UseVisualStyleBackColor = True
        '
        'btnMonitorDataSpacesLoad
        '
        Me.btnMonitorDataSpacesLoad.Location = New System.Drawing.Point(427, 18)
        Me.btnMonitorDataSpacesLoad.Name = "btnMonitorDataSpacesLoad"
        Me.btnMonitorDataSpacesLoad.Size = New System.Drawing.Size(135, 23)
        Me.btnMonitorDataSpacesLoad.TabIndex = 98
        Me.btnMonitorDataSpacesLoad.Text = "Reload Monitor Settings"
        Me.btnMonitorDataSpacesLoad.UseVisualStyleBackColor = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Location = New System.Drawing.Point(19, 297)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(543, 56)
        Me.RichTextBox1.TabIndex = 97
        Me.RichTextBox1.Text = resources.GetString("RichTextBox1.Text")
        '
        'lblUpperLimitHelp
        '
        Me.lblUpperLimitHelp.AutoSize = True
        Me.lblUpperLimitHelp.Location = New System.Drawing.Point(16, 221)
        Me.lblUpperLimitHelp.Name = "lblUpperLimitHelp"
        Me.lblUpperLimitHelp.Size = New System.Drawing.Size(282, 13)
        Me.lblUpperLimitHelp.TabIndex = 96
        Me.lblUpperLimitHelp.Text = "The boundary between medium sized and large databases"
        '
        'lblLowerLimitHelp
        '
        Me.lblLowerLimitHelp.AutoSize = True
        Me.lblLowerLimitHelp.Location = New System.Drawing.Point(16, 169)
        Me.lblLowerLimitHelp.Name = "lblLowerLimitHelp"
        Me.lblLowerLimitHelp.Size = New System.Drawing.Size(282, 13)
        Me.lblLowerLimitHelp.TabIndex = 95
        Me.lblLowerLimitHelp.Text = "The boundary between small and medium sized databases"
        '
        'lblLargeGrowthHelp
        '
        Me.lblLargeGrowthHelp.AutoSize = True
        Me.lblLargeGrowthHelp.Location = New System.Drawing.Point(16, 247)
        Me.lblLargeGrowthHelp.Name = "lblLargeGrowthHelp"
        Me.lblLargeGrowthHelp.Size = New System.Drawing.Size(280, 13)
        Me.lblLargeGrowthHelp.TabIndex = 94
        Me.lblLargeGrowthHelp.Text = "The growth in MB for database sizes above the upper limit"
        '
        'lblMediumGrowthHelp
        '
        Me.lblMediumGrowthHelp.AutoSize = True
        Me.lblMediumGrowthHelp.Location = New System.Drawing.Point(16, 195)
        Me.lblMediumGrowthHelp.Name = "lblMediumGrowthHelp"
        Me.lblMediumGrowthHelp.Size = New System.Drawing.Size(340, 13)
        Me.lblMediumGrowthHelp.TabIndex = 93
        Me.lblMediumGrowthHelp.Text = "The growth in MB for database sizes between the lower and upper limit"
        '
        'lblSmallGrowthHelp
        '
        Me.lblSmallGrowthHelp.AutoSize = True
        Me.lblSmallGrowthHelp.Location = New System.Drawing.Point(16, 143)
        Me.lblSmallGrowthHelp.Name = "lblSmallGrowthHelp"
        Me.lblSmallGrowthHelp.Size = New System.Drawing.Size(287, 13)
        Me.lblSmallGrowthHelp.TabIndex = 92
        Me.lblSmallGrowthHelp.Text = "The growth in MB for database sizes beneath the lower limit"
        '
        'lblMinFreeSpaceHelp
        '
        Me.lblMinFreeSpaceHelp.AutoSize = True
        Me.lblMinFreeSpaceHelp.Location = New System.Drawing.Point(16, 78)
        Me.lblMinFreeSpaceHelp.Name = "lblMinFreeSpaceHelp"
        Me.lblMinFreeSpaceHelp.Size = New System.Drawing.Size(328, 13)
        Me.lblMinFreeSpaceHelp.TabIndex = 91
        Me.lblMinFreeSpaceHelp.Text = "How much free space in percentage should there be, at a minimum?"
        '
        'lblMinPercGrowthHelp
        '
        Me.lblMinPercGrowthHelp.AutoSize = True
        Me.lblMinPercGrowthHelp.Location = New System.Drawing.Point(16, 23)
        Me.lblMinPercGrowthHelp.Name = "lblMinPercGrowthHelp"
        Me.lblMinPercGrowthHelp.Size = New System.Drawing.Size(373, 13)
        Me.lblMinPercGrowthHelp.TabIndex = 90
        Me.lblMinPercGrowthHelp.Text = "If database growth is a percentage, what should the minimum percentage be?"
        '
        'lblMinPercGrowth
        '
        Me.lblMinPercGrowth.AutoSize = True
        Me.lblMinPercGrowth.Location = New System.Drawing.Point(16, 48)
        Me.lblMinPercGrowth.Name = "lblMinPercGrowth"
        Me.lblMinPercGrowth.Size = New System.Drawing.Size(143, 13)
        Me.lblMinPercGrowth.TabIndex = 88
        Me.lblMinPercGrowth.Text = "Minimum Percentage Growth"
        '
        'txtMinPercGrowth
        '
        Me.txtMinPercGrowth.Location = New System.Drawing.Point(209, 45)
        Me.txtMinPercGrowth.Name = "txtMinPercGrowth"
        Me.txtMinPercGrowth.Size = New System.Drawing.Size(100, 20)
        Me.txtMinPercGrowth.TabIndex = 89
        '
        'lblMinFreeSpace
        '
        Me.lblMinFreeSpace.AutoSize = True
        Me.lblMinFreeSpace.Location = New System.Drawing.Point(16, 100)
        Me.lblMinFreeSpace.Name = "lblMinFreeSpace"
        Me.lblMinFreeSpace.Size = New System.Drawing.Size(164, 13)
        Me.lblMinFreeSpace.TabIndex = 86
        Me.lblMinFreeSpace.Text = "Minimum Percentage Free Space"
        '
        'txtMinFreeSpace
        '
        Me.txtMinFreeSpace.Location = New System.Drawing.Point(209, 97)
        Me.txtMinFreeSpace.Name = "txtMinFreeSpace"
        Me.txtMinFreeSpace.Size = New System.Drawing.Size(100, 20)
        Me.txtMinFreeSpace.TabIndex = 87
        '
        'lblLargeGrowth
        '
        Me.lblLargeGrowth.AutoSize = True
        Me.lblLargeGrowth.Location = New System.Drawing.Point(367, 247)
        Me.lblLargeGrowth.Name = "lblLargeGrowth"
        Me.lblLargeGrowth.Size = New System.Drawing.Size(71, 13)
        Me.lblLargeGrowth.TabIndex = 84
        Me.lblLargeGrowth.Text = "Large Growth"
        '
        'txtLargeGrowth
        '
        Me.txtLargeGrowth.Location = New System.Drawing.Point(462, 244)
        Me.txtLargeGrowth.Name = "txtLargeGrowth"
        Me.txtLargeGrowth.Size = New System.Drawing.Size(100, 20)
        Me.txtLargeGrowth.TabIndex = 85
        '
        'lblMediumGrowth
        '
        Me.lblMediumGrowth.AutoSize = True
        Me.lblMediumGrowth.Location = New System.Drawing.Point(367, 195)
        Me.lblMediumGrowth.Name = "lblMediumGrowth"
        Me.lblMediumGrowth.Size = New System.Drawing.Size(81, 13)
        Me.lblMediumGrowth.TabIndex = 82
        Me.lblMediumGrowth.Text = "Medium Growth"
        '
        'txtMediumGrowth
        '
        Me.txtMediumGrowth.Location = New System.Drawing.Point(462, 192)
        Me.txtMediumGrowth.Name = "txtMediumGrowth"
        Me.txtMediumGrowth.Size = New System.Drawing.Size(100, 20)
        Me.txtMediumGrowth.TabIndex = 83
        '
        'lblSmallGrowth
        '
        Me.lblSmallGrowth.AutoSize = True
        Me.lblSmallGrowth.Location = New System.Drawing.Point(367, 143)
        Me.lblSmallGrowth.Name = "lblSmallGrowth"
        Me.lblSmallGrowth.Size = New System.Drawing.Size(69, 13)
        Me.lblSmallGrowth.TabIndex = 80
        Me.lblSmallGrowth.Text = "Small Growth"
        '
        'txtSmallGrowth
        '
        Me.txtSmallGrowth.Location = New System.Drawing.Point(462, 140)
        Me.txtSmallGrowth.Name = "txtSmallGrowth"
        Me.txtSmallGrowth.Size = New System.Drawing.Size(100, 20)
        Me.txtSmallGrowth.TabIndex = 81
        '
        'lblUpperLimit
        '
        Me.lblUpperLimit.AutoSize = True
        Me.lblUpperLimit.Location = New System.Drawing.Point(367, 221)
        Me.lblUpperLimit.Name = "lblUpperLimit"
        Me.lblUpperLimit.Size = New System.Drawing.Size(60, 13)
        Me.lblUpperLimit.TabIndex = 78
        Me.lblUpperLimit.Text = "Upper Limit"
        '
        'txtUpperLimit
        '
        Me.txtUpperLimit.Location = New System.Drawing.Point(462, 218)
        Me.txtUpperLimit.Name = "txtUpperLimit"
        Me.txtUpperLimit.Size = New System.Drawing.Size(100, 20)
        Me.txtUpperLimit.TabIndex = 79
        '
        'lblLowerLimit
        '
        Me.lblLowerLimit.AutoSize = True
        Me.lblLowerLimit.Location = New System.Drawing.Point(367, 169)
        Me.lblLowerLimit.Name = "lblLowerLimit"
        Me.lblLowerLimit.Size = New System.Drawing.Size(60, 13)
        Me.lblLowerLimit.TabIndex = 76
        Me.lblLowerLimit.Text = "Lower Limit"
        '
        'txtLowerLimit
        '
        Me.txtLowerLimit.Location = New System.Drawing.Point(462, 166)
        Me.txtLowerLimit.Name = "txtLowerLimit"
        Me.txtLowerLimit.Size = New System.Drawing.Size(100, 20)
        Me.txtLowerLimit.TabIndex = 77
        '
        'btnMonitorDataSpacesSave
        '
        Me.btnMonitorDataSpacesSave.Location = New System.Drawing.Point(427, 47)
        Me.btnMonitorDataSpacesSave.Name = "btnMonitorDataSpacesSave"
        Me.btnMonitorDataSpacesSave.Size = New System.Drawing.Size(135, 23)
        Me.btnMonitorDataSpacesSave.TabIndex = 75
        Me.btnMonitorDataSpacesSave.Text = "Save Monitor Settings"
        Me.btnMonitorDataSpacesSave.UseVisualStyleBackColor = True
        '
        'tpgFtp
        '
        Me.tpgFtp.Controls.Add(Me.btnCreateDownloadProcedure)
        Me.tpgFtp.Controls.Add(Me.lblFtpStatus)
        Me.tpgFtp.Controls.Add(Me.rtbFtpCmdshell)
        Me.tpgFtp.Controls.Add(Me.chkCmdshell)
        Me.tpgFtp.Controls.Add(Me.lblCmdshell)
        Me.tpgFtp.Controls.Add(Me.rtbFtpDefaultValues)
        Me.tpgFtp.Controls.Add(Me.cbxFtpMode)
        Me.tpgFtp.Controls.Add(Me.chkEncryptProcedure)
        Me.tpgFtp.Controls.Add(Me.lblEncryptProcedure)
        Me.tpgFtp.Controls.Add(Me.btnCreateUploadProcedure)
        Me.tpgFtp.Controls.Add(Me.chkRemoveFiles)
        Me.tpgFtp.Controls.Add(Me.lblRemoveFiles)
        Me.tpgFtp.Controls.Add(Me.lblFtpMode)
        Me.tpgFtp.Controls.Add(Me.lblTargetFiles)
        Me.tpgFtp.Controls.Add(Me.txtTargetFiles)
        Me.tpgFtp.Controls.Add(Me.lblFtpServer)
        Me.tpgFtp.Controls.Add(Me.txtFtpServer)
        Me.tpgFtp.Controls.Add(Me.lblFtpUserName)
        Me.tpgFtp.Controls.Add(Me.txtFtpUserName)
        Me.tpgFtp.Controls.Add(Me.lblFtpPassword)
        Me.tpgFtp.Controls.Add(Me.txtFtpPassword)
        Me.tpgFtp.Controls.Add(Me.lblFtpLocation)
        Me.tpgFtp.Controls.Add(Me.txtFtpLocation)
        Me.tpgFtp.Controls.Add(Me.lblDownloadDestination)
        Me.tpgFtp.Controls.Add(Me.txtDownloadDestination)
        Me.tpgFtp.Controls.Add(Me.lblUploadSource)
        Me.tpgFtp.Controls.Add(Me.txtUploadSource)
        Me.tpgFtp.Location = New System.Drawing.Point(4, 22)
        Me.tpgFtp.Name = "tpgFtp"
        Me.tpgFtp.Size = New System.Drawing.Size(603, 368)
        Me.tpgFtp.TabIndex = 14
        Me.tpgFtp.Text = "FTP"
        Me.tpgFtp.UseVisualStyleBackColor = True
        '
        'btnCreateDownloadProcedure
        '
        Me.btnCreateDownloadProcedure.Location = New System.Drawing.Point(416, 95)
        Me.btnCreateDownloadProcedure.Name = "btnCreateDownloadProcedure"
        Me.btnCreateDownloadProcedure.Size = New System.Drawing.Size(154, 23)
        Me.btnCreateDownloadProcedure.TabIndex = 108
        Me.btnCreateDownloadProcedure.Text = "Create Download Procedure"
        Me.btnCreateDownloadProcedure.UseVisualStyleBackColor = True
        '
        'lblFtpStatus
        '
        Me.lblFtpStatus.Location = New System.Drawing.Point(415, 137)
        Me.lblFtpStatus.Name = "lblFtpStatus"
        Me.lblFtpStatus.Size = New System.Drawing.Size(157, 28)
        Me.lblFtpStatus.TabIndex = 107
        '
        'rtbFtpCmdshell
        '
        Me.rtbFtpCmdshell.BackColor = System.Drawing.SystemColors.Control
        Me.rtbFtpCmdshell.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbFtpCmdshell.Location = New System.Drawing.Point(27, 331)
        Me.rtbFtpCmdshell.Name = "rtbFtpCmdshell"
        Me.rtbFtpCmdshell.Size = New System.Drawing.Size(543, 33)
        Me.rtbFtpCmdshell.TabIndex = 106
        Me.rtbFtpCmdshell.Text = resources.GetString("rtbFtpCmdshell.Text")
        '
        'chkCmdshell
        '
        Me.chkCmdshell.AutoSize = True
        Me.chkCmdshell.Checked = True
        Me.chkCmdshell.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCmdshell.Location = New System.Drawing.Point(342, 309)
        Me.chkCmdshell.Name = "chkCmdshell"
        Me.chkCmdshell.Size = New System.Drawing.Size(15, 14)
        Me.chkCmdshell.TabIndex = 10
        Me.chkCmdshell.UseVisualStyleBackColor = True
        '
        'lblCmdshell
        '
        Me.lblCmdshell.AutoSize = True
        Me.lblCmdshell.Location = New System.Drawing.Point(26, 309)
        Me.lblCmdshell.Name = "lblCmdshell"
        Me.lblCmdshell.Size = New System.Drawing.Size(198, 13)
        Me.lblCmdshell.TabIndex = 104
        Me.lblCmdshell.Text = "Automatically Enable / Disable  cmdshell"
        '
        'rtbFtpDefaultValues
        '
        Me.rtbFtpDefaultValues.BackColor = System.Drawing.SystemColors.Control
        Me.rtbFtpDefaultValues.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbFtpDefaultValues.Location = New System.Drawing.Point(27, 19)
        Me.rtbFtpDefaultValues.Name = "rtbFtpDefaultValues"
        Me.rtbFtpDefaultValues.Size = New System.Drawing.Size(543, 33)
        Me.rtbFtpDefaultValues.TabIndex = 103
        Me.rtbFtpDefaultValues.Text = resources.GetString("rtbFtpDefaultValues.Text")
        '
        'cbxFtpMode
        '
        Me.cbxFtpMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxFtpMode.FormattingEnabled = True
        Me.cbxFtpMode.Items.AddRange(New Object() {"ascii", "binary"})
        Me.cbxFtpMode.Location = New System.Drawing.Point(167, 240)
        Me.cbxFtpMode.Name = "cbxFtpMode"
        Me.cbxFtpMode.Size = New System.Drawing.Size(190, 21)
        Me.cbxFtpMode.TabIndex = 7
        '
        'chkEncryptProcedure
        '
        Me.chkEncryptProcedure.AutoSize = True
        Me.chkEncryptProcedure.Checked = True
        Me.chkEncryptProcedure.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEncryptProcedure.Location = New System.Drawing.Point(342, 289)
        Me.chkEncryptProcedure.Name = "chkEncryptProcedure"
        Me.chkEncryptProcedure.Size = New System.Drawing.Size(15, 14)
        Me.chkEncryptProcedure.TabIndex = 9
        Me.chkEncryptProcedure.UseVisualStyleBackColor = True
        '
        'lblEncryptProcedure
        '
        Me.lblEncryptProcedure.AutoSize = True
        Me.lblEncryptProcedure.Location = New System.Drawing.Point(26, 289)
        Me.lblEncryptProcedure.Name = "lblEncryptProcedure"
        Me.lblEncryptProcedure.Size = New System.Drawing.Size(192, 13)
        Me.lblEncryptProcedure.TabIndex = 100
        Me.lblEncryptProcedure.Text = "Excrypt Procedure to Protect Password"
        '
        'btnCreateUploadProcedure
        '
        Me.btnCreateUploadProcedure.Location = New System.Drawing.Point(416, 66)
        Me.btnCreateUploadProcedure.Name = "btnCreateUploadProcedure"
        Me.btnCreateUploadProcedure.Size = New System.Drawing.Size(154, 23)
        Me.btnCreateUploadProcedure.TabIndex = 11
        Me.btnCreateUploadProcedure.Text = "Create Upload Procedure"
        Me.btnCreateUploadProcedure.UseVisualStyleBackColor = True
        '
        'chkRemoveFiles
        '
        Me.chkRemoveFiles.AutoSize = True
        Me.chkRemoveFiles.Location = New System.Drawing.Point(342, 269)
        Me.chkRemoveFiles.Name = "chkRemoveFiles"
        Me.chkRemoveFiles.Size = New System.Drawing.Size(15, 14)
        Me.chkRemoveFiles.TabIndex = 8
        Me.chkRemoveFiles.UseVisualStyleBackColor = True
        '
        'lblRemoveFiles
        '
        Me.lblRemoveFiles.AutoSize = True
        Me.lblRemoveFiles.Location = New System.Drawing.Point(26, 269)
        Me.lblRemoveFiles.Name = "lblRemoveFiles"
        Me.lblRemoveFiles.Size = New System.Drawing.Size(264, 13)
        Me.lblRemoveFiles.TabIndex = 86
        Me.lblRemoveFiles.Text = "Remove Source Files from FTP Server After Download"
        '
        'lblFtpMode
        '
        Me.lblFtpMode.AutoSize = True
        Me.lblFtpMode.Location = New System.Drawing.Point(26, 243)
        Me.lblFtpMode.Name = "lblFtpMode"
        Me.lblFtpMode.Size = New System.Drawing.Size(57, 13)
        Me.lblFtpMode.TabIndex = 84
        Me.lblFtpMode.Text = "FTP Mode"
        '
        'lblTargetFiles
        '
        Me.lblTargetFiles.AutoSize = True
        Me.lblTargetFiles.Location = New System.Drawing.Point(26, 217)
        Me.lblTargetFiles.Name = "lblTargetFiles"
        Me.lblTargetFiles.Size = New System.Drawing.Size(136, 13)
        Me.lblTargetFiles.TabIndex = 82
        Me.lblTargetFiles.Text = "Target File(s) Name / Mask"
        '
        'txtTargetFiles
        '
        Me.txtTargetFiles.Location = New System.Drawing.Point(167, 214)
        Me.txtTargetFiles.Name = "txtTargetFiles"
        Me.txtTargetFiles.Size = New System.Drawing.Size(190, 20)
        Me.txtTargetFiles.TabIndex = 6
        Me.txtTargetFiles.Tag = "<FileName.txt / File*.txt / *.*>"
        '
        'lblFtpServer
        '
        Me.lblFtpServer.AutoSize = True
        Me.lblFtpServer.Location = New System.Drawing.Point(26, 61)
        Me.lblFtpServer.Name = "lblFtpServer"
        Me.lblFtpServer.Size = New System.Drawing.Size(61, 13)
        Me.lblFtpServer.TabIndex = 80
        Me.lblFtpServer.Text = "FTP Server"
        '
        'txtFtpServer
        '
        Me.txtFtpServer.Location = New System.Drawing.Point(167, 58)
        Me.txtFtpServer.Name = "txtFtpServer"
        Me.txtFtpServer.Size = New System.Drawing.Size(190, 20)
        Me.txtFtpServer.TabIndex = 0
        Me.txtFtpServer.Tag = "<Server Name or IP Address>"
        '
        'lblFtpUserName
        '
        Me.lblFtpUserName.AutoSize = True
        Me.lblFtpUserName.Location = New System.Drawing.Point(26, 87)
        Me.lblFtpUserName.Name = "lblFtpUserName"
        Me.lblFtpUserName.Size = New System.Drawing.Size(83, 13)
        Me.lblFtpUserName.TabIndex = 78
        Me.lblFtpUserName.Text = "FTP User Name"
        '
        'txtFtpUserName
        '
        Me.txtFtpUserName.Location = New System.Drawing.Point(167, 84)
        Me.txtFtpUserName.Name = "txtFtpUserName"
        Me.txtFtpUserName.Size = New System.Drawing.Size(190, 20)
        Me.txtFtpUserName.TabIndex = 1
        '
        'lblFtpPassword
        '
        Me.lblFtpPassword.AutoSize = True
        Me.lblFtpPassword.Location = New System.Drawing.Point(26, 113)
        Me.lblFtpPassword.Name = "lblFtpPassword"
        Me.lblFtpPassword.Size = New System.Drawing.Size(76, 13)
        Me.lblFtpPassword.TabIndex = 76
        Me.lblFtpPassword.Text = "FTP Password"
        '
        'txtFtpPassword
        '
        Me.txtFtpPassword.Location = New System.Drawing.Point(167, 110)
        Me.txtFtpPassword.Name = "txtFtpPassword"
        Me.txtFtpPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtFtpPassword.Size = New System.Drawing.Size(190, 20)
        Me.txtFtpPassword.TabIndex = 2
        '
        'lblFtpLocation
        '
        Me.lblFtpLocation.AutoSize = True
        Me.lblFtpLocation.Location = New System.Drawing.Point(26, 165)
        Me.lblFtpLocation.Name = "lblFtpLocation"
        Me.lblFtpLocation.Size = New System.Drawing.Size(104, 13)
        Me.lblFtpLocation.TabIndex = 74
        Me.lblFtpLocation.Text = "FTP Location / Path"
        '
        'txtFtpLocation
        '
        Me.txtFtpLocation.Location = New System.Drawing.Point(167, 162)
        Me.txtFtpLocation.Name = "txtFtpLocation"
        Me.txtFtpLocation.Size = New System.Drawing.Size(190, 20)
        Me.txtFtpLocation.TabIndex = 4
        Me.txtFtpLocation.Tag = "<Path on FTP Server>"
        '
        'lblDownloadDestination
        '
        Me.lblDownloadDestination.AutoSize = True
        Me.lblDownloadDestination.Location = New System.Drawing.Point(26, 191)
        Me.lblDownloadDestination.Name = "lblDownloadDestination"
        Me.lblDownloadDestination.Size = New System.Drawing.Size(136, 13)
        Me.lblDownloadDestination.TabIndex = 72
        Me.lblDownloadDestination.Text = "Download Destination Path"
        '
        'txtDownloadDestination
        '
        Me.txtDownloadDestination.Location = New System.Drawing.Point(167, 188)
        Me.txtDownloadDestination.Name = "txtDownloadDestination"
        Me.txtDownloadDestination.Size = New System.Drawing.Size(190, 20)
        Me.txtDownloadDestination.TabIndex = 5
        Me.txtDownloadDestination.Tag = "<Local Destination Path>"
        '
        'lblUploadSource
        '
        Me.lblUploadSource.AutoSize = True
        Me.lblUploadSource.Location = New System.Drawing.Point(26, 139)
        Me.lblUploadSource.Name = "lblUploadSource"
        Me.lblUploadSource.Size = New System.Drawing.Size(103, 13)
        Me.lblUploadSource.TabIndex = 70
        Me.lblUploadSource.Text = "Upload Source Path"
        '
        'txtUploadSource
        '
        Me.txtUploadSource.Location = New System.Drawing.Point(167, 136)
        Me.txtUploadSource.Name = "txtUploadSource"
        Me.txtUploadSource.Size = New System.Drawing.Size(190, 20)
        Me.txtUploadSource.TabIndex = 3
        Me.txtUploadSource.Tag = "<Local Source Path>"
        '
        'tpgEmail
        '
        Me.tpgEmail.Controls.Add(Me.btnShowEmailPassword)
        Me.tpgEmail.Controls.Add(Me.btnSettingsEmailSave)
        Me.tpgEmail.Controls.Add(Me.lblSmtpPortNumber)
        Me.tpgEmail.Controls.Add(Me.txtSmtpPortNumber)
        Me.tpgEmail.Controls.Add(Me.lblSmtpServer)
        Me.tpgEmail.Controls.Add(Me.chkUseSslEncryption)
        Me.tpgEmail.Controls.Add(Me.txtSmtpServer)
        Me.tpgEmail.Controls.Add(Me.lblSmtpReply)
        Me.tpgEmail.Controls.Add(Me.txtSmtpServerUsername)
        Me.tpgEmail.Controls.Add(Me.txtSmtpReply)
        Me.tpgEmail.Controls.Add(Me.lblSmtpServerUsername)
        Me.tpgEmail.Controls.Add(Me.chkSmtpCredentials)
        Me.tpgEmail.Controls.Add(Me.txtSmtpServerPassword)
        Me.tpgEmail.Controls.Add(Me.lblSmtpServerPassword)
        Me.tpgEmail.Location = New System.Drawing.Point(4, 22)
        Me.tpgEmail.Name = "tpgEmail"
        Me.tpgEmail.Size = New System.Drawing.Size(603, 368)
        Me.tpgEmail.TabIndex = 15
        Me.tpgEmail.Text = "Email"
        Me.tpgEmail.UseVisualStyleBackColor = True
        '
        'btnShowEmailPassword
        '
        Me.btnShowEmailPassword.Enabled = False
        Me.btnShowEmailPassword.Image = Global.Sequenchel.My.Resources.Resources.eye
        Me.btnShowEmailPassword.Location = New System.Drawing.Point(369, 137)
        Me.btnShowEmailPassword.Name = "btnShowEmailPassword"
        Me.btnShowEmailPassword.Size = New System.Drawing.Size(23, 23)
        Me.btnShowEmailPassword.TabIndex = 88
        Me.btnShowEmailPassword.UseVisualStyleBackColor = True
        '
        'btnSettingsEmailSave
        '
        Me.btnSettingsEmailSave.Location = New System.Drawing.Point(427, 19)
        Me.btnSettingsEmailSave.Name = "btnSettingsEmailSave"
        Me.btnSettingsEmailSave.Size = New System.Drawing.Size(135, 23)
        Me.btnSettingsEmailSave.TabIndex = 87
        Me.btnSettingsEmailSave.Text = "Save Email Settings"
        Me.btnSettingsEmailSave.UseVisualStyleBackColor = True
        '
        'lblSmtpPortNumber
        '
        Me.lblSmtpPortNumber.AutoSize = True
        Me.lblSmtpPortNumber.Location = New System.Drawing.Point(61, 190)
        Me.lblSmtpPortNumber.Name = "lblSmtpPortNumber"
        Me.lblSmtpPortNumber.Size = New System.Drawing.Size(99, 13)
        Me.lblSmtpPortNumber.TabIndex = 93
        Me.lblSmtpPortNumber.Text = "SMTP Port Number"
        '
        'txtSmtpPortNumber
        '
        Me.txtSmtpPortNumber.Location = New System.Drawing.Point(202, 187)
        Me.txtSmtpPortNumber.Name = "txtSmtpPortNumber"
        Me.txtSmtpPortNumber.Size = New System.Drawing.Size(43, 20)
        Me.txtSmtpPortNumber.TabIndex = 94
        '
        'lblSmtpServer
        '
        Me.lblSmtpServer.AutoSize = True
        Me.lblSmtpServer.Location = New System.Drawing.Point(61, 68)
        Me.lblSmtpServer.Name = "lblSmtpServer"
        Me.lblSmtpServer.Size = New System.Drawing.Size(71, 13)
        Me.lblSmtpServer.TabIndex = 83
        Me.lblSmtpServer.Text = "SMTP Server"
        '
        'chkUseSslEncryption
        '
        Me.chkUseSslEncryption.AutoSize = True
        Me.chkUseSslEncryption.Location = New System.Drawing.Point(63, 169)
        Me.chkUseSslEncryption.Name = "chkUseSslEncryption"
        Me.chkUseSslEncryption.Size = New System.Drawing.Size(146, 17)
        Me.chkUseSslEncryption.TabIndex = 92
        Me.chkUseSslEncryption.Text = "Use SSL/TLS Encryption"
        Me.chkUseSslEncryption.UseVisualStyleBackColor = True
        '
        'txtSmtpServer
        '
        Me.txtSmtpServer.Location = New System.Drawing.Point(202, 65)
        Me.txtSmtpServer.Name = "txtSmtpServer"
        Me.txtSmtpServer.Size = New System.Drawing.Size(190, 20)
        Me.txtSmtpServer.TabIndex = 84
        '
        'lblSmtpReply
        '
        Me.lblSmtpReply.AutoSize = True
        Me.lblSmtpReply.Location = New System.Drawing.Point(61, 224)
        Me.lblSmtpReply.Name = "lblSmtpReply"
        Me.lblSmtpReply.Size = New System.Drawing.Size(91, 13)
        Me.lblSmtpReply.TabIndex = 90
        Me.lblSmtpReply.Text = "Reply To Address"
        '
        'txtSmtpServerUsername
        '
        Me.txtSmtpServerUsername.Enabled = False
        Me.txtSmtpServerUsername.Location = New System.Drawing.Point(202, 115)
        Me.txtSmtpServerUsername.Name = "txtSmtpServerUsername"
        Me.txtSmtpServerUsername.Size = New System.Drawing.Size(190, 20)
        Me.txtSmtpServerUsername.TabIndex = 86
        '
        'txtSmtpReply
        '
        Me.txtSmtpReply.Location = New System.Drawing.Point(202, 220)
        Me.txtSmtpReply.Name = "txtSmtpReply"
        Me.txtSmtpReply.Size = New System.Drawing.Size(190, 20)
        Me.txtSmtpReply.TabIndex = 91
        '
        'lblSmtpServerUsername
        '
        Me.lblSmtpServerUsername.AutoSize = True
        Me.lblSmtpServerUsername.Location = New System.Drawing.Point(61, 118)
        Me.lblSmtpServerUsername.Name = "lblSmtpServerUsername"
        Me.lblSmtpServerUsername.Size = New System.Drawing.Size(122, 13)
        Me.lblSmtpServerUsername.TabIndex = 85
        Me.lblSmtpServerUsername.Text = "SMTP Server Username"
        '
        'chkSmtpCredentials
        '
        Me.chkSmtpCredentials.AutoSize = True
        Me.chkSmtpCredentials.Checked = True
        Me.chkSmtpCredentials.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSmtpCredentials.Location = New System.Drawing.Point(63, 95)
        Me.chkSmtpCredentials.Name = "chkSmtpCredentials"
        Me.chkSmtpCredentials.Size = New System.Drawing.Size(141, 17)
        Me.chkSmtpCredentials.TabIndex = 89
        Me.chkSmtpCredentials.Text = "use Network Credentials"
        Me.chkSmtpCredentials.UseVisualStyleBackColor = True
        '
        'txtSmtpServerPassword
        '
        Me.txtSmtpServerPassword.Enabled = False
        Me.txtSmtpServerPassword.Location = New System.Drawing.Point(202, 139)
        Me.txtSmtpServerPassword.Name = "txtSmtpServerPassword"
        Me.txtSmtpServerPassword.Size = New System.Drawing.Size(165, 20)
        Me.txtSmtpServerPassword.TabIndex = 88
        Me.txtSmtpServerPassword.Tag = "<Keep Current Password>"
        '
        'lblSmtpServerPassword
        '
        Me.lblSmtpServerPassword.AutoSize = True
        Me.lblSmtpServerPassword.Location = New System.Drawing.Point(61, 142)
        Me.lblSmtpServerPassword.Name = "lblSmtpServerPassword"
        Me.lblSmtpServerPassword.Size = New System.Drawing.Size(120, 13)
        Me.lblSmtpServerPassword.TabIndex = 87
        Me.lblSmtpServerPassword.Text = "SMTP Server Password"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(523, 411)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 443)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.tabSettings)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(653, 481)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(653, 481)
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.tabSettings.ResumeLayout(False)
        Me.tpgGeneral.ResumeLayout(False)
        Me.tpgGeneral.PerformLayout()
        Me.grpSecurity.ResumeLayout(False)
        Me.grpSecurity.PerformLayout()
        Me.tpgLicense.ResumeLayout(False)
        Me.tpgLicense.PerformLayout()
        Me.tpgLogging.ResumeLayout(False)
        Me.tpgLogging.PerformLayout()
        Me.grpLogsToKeep.ResumeLayout(False)
        Me.grpLogsToKeep.PerformLayout()
        Me.tpgDatabase.ResumeLayout(False)
        Me.tpgDatabase.PerformLayout()
        Me.tpgScheduler.ResumeLayout(False)
        Me.tpgScheduler.PerformLayout()
        CType(Me.nudTimeSpan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpgMonitorDataspaces.ResumeLayout(False)
        Me.tpgMonitorDataspaces.PerformLayout()
        Me.tpgFtp.ResumeLayout(False)
        Me.tpgFtp.PerformLayout()
        Me.tpgEmail.ResumeLayout(False)
        Me.tpgEmail.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tabSettings As System.Windows.Forms.TabControl
    Friend WithEvents tpgLogging As System.Windows.Forms.TabPage
    Friend WithEvents grpLogsToKeep As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoDeleteOldLogs As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearOldLogs As System.Windows.Forms.Button
    Friend WithEvents rbtKeepLogMonth As System.Windows.Forms.RadioButton
    Friend WithEvents rbtKeepLogWeek As System.Windows.Forms.RadioButton
    Friend WithEvents rbtKeepLogDay As System.Windows.Forms.RadioButton
    Friend WithEvents txtLogfileLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtLogfileName As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveSettingsLog As System.Windows.Forms.Button
    Friend WithEvents lblLogfileName As System.Windows.Forms.Label
    Friend WithEvents lblLogfileLocation As System.Windows.Forms.Label
    Friend WithEvents rbtLoggingLevel5 As System.Windows.Forms.RadioButton
    Friend WithEvents lblLoggingLevel As System.Windows.Forms.Label
    Friend WithEvents rbtLoggingLevel1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtLoggingLevel2 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtLoggingLevel3 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtLoggingLevel4 As System.Windows.Forms.RadioButton
    Friend WithEvents rbtLoggingLevel0 As System.Windows.Forms.RadioButton
    Friend WithEvents tpgDatabase As System.Windows.Forms.TabPage
    Friend WithEvents btnTestConnection As System.Windows.Forms.Button
    Friend WithEvents lblBackupPath As System.Windows.Forms.Label
    Friend WithEvents txtBackupDatabase As System.Windows.Forms.TextBox
    Friend WithEvents lblBackupDatabase As System.Windows.Forms.Label
    Friend WithEvents btnBackupDatabase As System.Windows.Forms.Button
    Friend WithEvents txtUpgradeDatabase As System.Windows.Forms.TextBox
    Friend WithEvents lblDatabaseVersion As System.Windows.Forms.Label
    Friend WithEvents btnUpgradeDatabase As System.Windows.Forms.Button
    Friend WithEvents prbCreateDatabase As System.Windows.Forms.ProgressBar
    Friend WithEvents btnCreateExtraProcs As System.Windows.Forms.Button
    Friend WithEvents btnCreateDatabase As System.Windows.Forms.Button
    Friend WithEvents btnSaveSettingsDatabase As System.Windows.Forms.Button
    Friend WithEvents btnDatabaseDefaultsUse As System.Windows.Forms.Button
    Friend WithEvents lblLoginMethod As System.Windows.Forms.Label
    Friend WithEvents lblLoginName As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents cbxLoginMethod As System.Windows.Forms.ComboBox
    Friend WithEvents txtLoginName As System.Windows.Forms.TextBox
    Friend WithEvents lblDatabaseName As System.Windows.Forms.Label
    Friend WithEvents txtDatabaseName As System.Windows.Forms.TextBox
    Friend WithEvents lblDatabaseLocation As System.Windows.Forms.Label
    Friend WithEvents cbxDataProvider As System.Windows.Forms.ComboBox
    Friend WithEvents txtDatabaseLocation As System.Windows.Forms.TextBox
    Friend WithEvents lblDataProvider As System.Windows.Forms.Label
    Friend WithEvents lblStatusDatabase As System.Windows.Forms.Label
    Friend WithEvents tpgLicense As System.Windows.Forms.TabPage
    Friend WithEvents lblLicenseKey As System.Windows.Forms.Label
    Friend WithEvents txtLicenseKey As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveLicense As System.Windows.Forms.Button
    Friend WithEvents lblLicenseName As System.Windows.Forms.Label
    Friend WithEvents txtLicenseName As System.Windows.Forms.TextBox
    Friend WithEvents btnValidateLicense As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnRefreshDatabase As System.Windows.Forms.Button
    Friend WithEvents tpgMonitorDataspaces As System.Windows.Forms.TabPage
    Friend WithEvents btnMonitorDataSpacesSave As System.Windows.Forms.Button
    Friend WithEvents lblMinPercGrowth As System.Windows.Forms.Label
    Friend WithEvents txtMinPercGrowth As System.Windows.Forms.TextBox
    Friend WithEvents lblMinFreeSpace As System.Windows.Forms.Label
    Friend WithEvents txtMinFreeSpace As System.Windows.Forms.TextBox
    Friend WithEvents lblLargeGrowth As System.Windows.Forms.Label
    Friend WithEvents txtLargeGrowth As System.Windows.Forms.TextBox
    Friend WithEvents lblMediumGrowth As System.Windows.Forms.Label
    Friend WithEvents txtMediumGrowth As System.Windows.Forms.TextBox
    Friend WithEvents lblSmallGrowth As System.Windows.Forms.Label
    Friend WithEvents txtSmallGrowth As System.Windows.Forms.TextBox
    Friend WithEvents lblUpperLimit As System.Windows.Forms.Label
    Friend WithEvents txtUpperLimit As System.Windows.Forms.TextBox
    Friend WithEvents lblLowerLimit As System.Windows.Forms.Label
    Friend WithEvents txtLowerLimit As System.Windows.Forms.TextBox
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents lblUpperLimitHelp As System.Windows.Forms.Label
    Friend WithEvents lblLowerLimitHelp As System.Windows.Forms.Label
    Friend WithEvents lblLargeGrowthHelp As System.Windows.Forms.Label
    Friend WithEvents lblMediumGrowthHelp As System.Windows.Forms.Label
    Friend WithEvents lblSmallGrowthHelp As System.Windows.Forms.Label
    Friend WithEvents lblMinFreeSpaceHelp As System.Windows.Forms.Label
    Friend WithEvents lblMinPercGrowthHelp As System.Windows.Forms.Label
    Friend WithEvents tpgGeneral As System.Windows.Forms.TabPage
    Friend WithEvents btnConnectionsFile As System.Windows.Forms.Button
    Friend WithEvents btnSettingsFile As System.Windows.Forms.Button
    Friend WithEvents btnSettingsGeneralSave As System.Windows.Forms.Button
    Friend WithEvents lblConnectionsFile As System.Windows.Forms.Label
    Friend WithEvents txtConnectionsFile As System.Windows.Forms.TextBox
    Friend WithEvents lblSettingsFile As System.Windows.Forms.Label
    Friend WithEvents txtSettingsFile As System.Windows.Forms.TextBox
    Friend WithEvents tpgScheduler As System.Windows.Forms.TabPage
    Friend WithEvents lblProcedure As System.Windows.Forms.Label
    Friend WithEvents cbxProcedures As System.Windows.Forms.ComboBox
    Friend WithEvents cbxStartMinute As System.Windows.Forms.ComboBox
    Friend WithEvents cbxStartHour As System.Windows.Forms.ComboBox
    Friend WithEvents lblStartTime As System.Windows.Forms.Label
    Friend WithEvents cbxTimespan As System.Windows.Forms.ComboBox
    Friend WithEvents lblOccurence As System.Windows.Forms.Label
    Friend WithEvents cbxOccurence As System.Windows.Forms.ComboBox
    Friend WithEvents chkTeusday As System.Windows.Forms.CheckBox
    Friend WithEvents chkWednesday As System.Windows.Forms.CheckBox
    Friend WithEvents chkThursday As System.Windows.Forms.CheckBox
    Friend WithEvents chkFriday As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaturday As System.Windows.Forms.CheckBox
    Friend WithEvents chkSunday As System.Windows.Forms.CheckBox
    Friend WithEvents chkMonday As System.Windows.Forms.CheckBox
    Friend WithEvents lblStartTimeColon As System.Windows.Forms.Label
    Friend WithEvents cbxEndMinute As System.Windows.Forms.ComboBox
    Friend WithEvents cbxEndHour As System.Windows.Forms.ComboBox
    Friend WithEvents lblEndTime As System.Windows.Forms.Label
    Friend WithEvents lblEndTimeColon As System.Windows.Forms.Label
    Friend WithEvents lblExceptionList As System.Windows.Forms.Label
    Friend WithEvents txtExceptionList As System.Windows.Forms.TextBox
    Friend WithEvents lblRecipient As System.Windows.Forms.Label
    Friend WithEvents txtRecipient As System.Windows.Forms.TextBox
    Friend WithEvents lblMailStatistics As System.Windows.Forms.Label
    Friend WithEvents txtSeparator As System.Windows.Forms.TextBox
    Friend WithEvents lblSeparator As System.Windows.Forms.Label
    Friend WithEvents lblSqlVersion As System.Windows.Forms.Label
    Friend WithEvents cbxSqlVersion As System.Windows.Forms.ComboBox
    Friend WithEvents chkMailStatistics As System.Windows.Forms.CheckBox
    Friend WithEvents btnCreateScheduledJob As System.Windows.Forms.Button
    Friend WithEvents lblRepeatEvery As System.Windows.Forms.Label
    Friend WithEvents nudTimeSpan As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblErrorlogPath As System.Windows.Forms.Label
    Friend WithEvents txtErrorlogPath As System.Windows.Forms.TextBox
    Friend WithEvents chkIncludeHigherSqlVersions As System.Windows.Forms.CheckBox
    Friend WithEvents lblIncludeHigherSqlVersions As System.Windows.Forms.Label
    Friend WithEvents lblJobNamePrefix As System.Windows.Forms.Label
    Friend WithEvents txtJobNamePrefix As System.Windows.Forms.TextBox
    Friend WithEvents chkAllowQueryEdit As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowSettingsChange As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowConfiguration As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowLinkedServers As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowInsert As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowDelete As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents btnConfigFilePath As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDefaultConfigFilePath As System.Windows.Forms.TextBox
    Friend WithEvents btnDefaultConfigFilePath As System.Windows.Forms.Button
    Friend WithEvents btnConnectionsFileDefault As System.Windows.Forms.Button
    Friend WithEvents btnSettingsFileDefault As System.Windows.Forms.Button
    Friend WithEvents btnLogLocationDefault As System.Windows.Forms.Button
    Friend WithEvents ttpDefaultLogLocation As System.Windows.Forms.ToolTip
    Friend WithEvents btnLogLocationSystem As System.Windows.Forms.Button
    Friend WithEvents btnLogfileNameDefault As System.Windows.Forms.Button
    Friend WithEvents btnLogLocationDatabase As System.Windows.Forms.Button
    Friend WithEvents btnLogLocationBrowse As System.Windows.Forms.Button
    Friend WithEvents lblUsageText As System.Windows.Forms.Label
    Friend WithEvents lblUsage As System.Windows.Forms.Label
    Friend WithEvents txtOverridePassword As System.Windows.Forms.TextBox
    Friend WithEvents lblOverridePassword As System.Windows.Forms.Label
    Friend WithEvents grpSecurity As System.Windows.Forms.GroupBox
    Friend WithEvents btnErrorlogPathBrowse As System.Windows.Forms.Button
    Friend WithEvents btnErrorlogPathDatabase As System.Windows.Forms.Button
    Friend WithEvents btnErrorlogPathSystem As System.Windows.Forms.Button
    Friend WithEvents btnErrorlogPathDefault As System.Windows.Forms.Button
    Friend WithEvents btnConnectionsFileSystem As System.Windows.Forms.Button
    Friend WithEvents btnSettingsFileSystem As System.Windows.Forms.Button
    Friend WithEvents btnMonitorDataSpacesLoad As System.Windows.Forms.Button
    Friend WithEvents cbxDateFormats As System.Windows.Forms.ComboBox
    Friend WithEvents lblDateFormatting As System.Windows.Forms.Label
    Friend WithEvents chkIncludeDateInExportFiles As System.Windows.Forms.CheckBox
    Friend WithEvents lblLimitLookupLists As System.Windows.Forms.Label
    Friend WithEvents txtLimitLookupLists As System.Windows.Forms.TextBox
    Friend WithEvents chkLimitLookupLists As System.Windows.Forms.CheckBox
    Friend WithEvents tpgFtp As System.Windows.Forms.TabPage
    Friend WithEvents lblFtpServer As System.Windows.Forms.Label
    Friend WithEvents txtFtpServer As System.Windows.Forms.TextBox
    Friend WithEvents lblFtpUserName As System.Windows.Forms.Label
    Friend WithEvents txtFtpUserName As System.Windows.Forms.TextBox
    Friend WithEvents lblFtpPassword As System.Windows.Forms.Label
    Friend WithEvents txtFtpPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblFtpLocation As System.Windows.Forms.Label
    Friend WithEvents txtFtpLocation As System.Windows.Forms.TextBox
    Friend WithEvents lblDownloadDestination As System.Windows.Forms.Label
    Friend WithEvents txtDownloadDestination As System.Windows.Forms.TextBox
    Friend WithEvents lblUploadSource As System.Windows.Forms.Label
    Friend WithEvents txtUploadSource As System.Windows.Forms.TextBox
    Friend WithEvents chkEncryptProcedure As System.Windows.Forms.CheckBox
    Friend WithEvents lblEncryptProcedure As System.Windows.Forms.Label
    Friend WithEvents btnCreateUploadProcedure As System.Windows.Forms.Button
    Friend WithEvents chkRemoveFiles As System.Windows.Forms.CheckBox
    Friend WithEvents lblRemoveFiles As System.Windows.Forms.Label
    Friend WithEvents lblFtpMode As System.Windows.Forms.Label
    Friend WithEvents lblTargetFiles As System.Windows.Forms.Label
    Friend WithEvents txtTargetFiles As System.Windows.Forms.TextBox
    Friend WithEvents cbxFtpMode As System.Windows.Forms.ComboBox
    Friend WithEvents rtbFtpDefaultValues As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbFtpCmdshell As System.Windows.Forms.RichTextBox
    Friend WithEvents chkCmdshell As System.Windows.Forms.CheckBox
    Friend WithEvents lblCmdshell As System.Windows.Forms.Label
    Friend WithEvents lblFtpStatus As System.Windows.Forms.Label
    Friend WithEvents btnCreateDownloadProcedure As System.Windows.Forms.Button
    Friend WithEvents chkAllowDataImport As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllowSmartUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents tpgEmail As System.Windows.Forms.TabPage
    Friend WithEvents btnSettingsEmailSave As System.Windows.Forms.Button
    Friend WithEvents lblSmtpPortNumber As System.Windows.Forms.Label
    Friend WithEvents txtSmtpPortNumber As System.Windows.Forms.TextBox
    Friend WithEvents chkUseSslEncryption As System.Windows.Forms.CheckBox
    Friend WithEvents lblSmtpReply As System.Windows.Forms.Label
    Friend WithEvents txtSmtpReply As System.Windows.Forms.TextBox
    Friend WithEvents chkSmtpCredentials As System.Windows.Forms.CheckBox
    Friend WithEvents lblSmtpServerPassword As System.Windows.Forms.Label
    Friend WithEvents txtSmtpServerPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblSmtpServerUsername As System.Windows.Forms.Label
    Friend WithEvents txtSmtpServerUsername As System.Windows.Forms.TextBox
    Friend WithEvents lblSmtpServer As System.Windows.Forms.Label
    Friend WithEvents txtSmtpServer As System.Windows.Forms.TextBox
    Friend WithEvents btnShowEmailPassword As System.Windows.Forms.Button
    Friend WithEvents btnShowOverridePassword As System.Windows.Forms.Button
    Friend WithEvents btnShowDatabasePassword As System.Windows.Forms.Button
    Friend WithEvents txtTimerHours As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
