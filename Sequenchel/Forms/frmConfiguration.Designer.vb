<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfiguration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfiguration))
        Me.tabConfiguration = New System.Windows.Forms.TabControl()
        Me.tpgConnections = New System.Windows.Forms.TabPage()
        Me.btnShowDatabasePassword = New System.Windows.Forms.Button()
        Me.btnDefaultTableSetFile = New System.Windows.Forms.Button()
        Me.btnTableSetFileBrowse = New System.Windows.Forms.Button()
        Me.lblBackupPath = New System.Windows.Forms.Label()
        Me.btnBackupLocation = New System.Windows.Forms.Button()
        Me.lblBackupLocation = New System.Windows.Forms.Label()
        Me.txtBackupLocation = New System.Windows.Forms.TextBox()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.lstDatabases = New System.Windows.Forms.ListBox()
        Me.lstServers = New System.Windows.Forms.ListBox()
        Me.btnCrawlServers = New System.Windows.Forms.Button()
        Me.btnCrawlDatabases = New System.Windows.Forms.Button()
        Me.btnConnectionDelete = New System.Windows.Forms.Button()
        Me.btnConnectionClear = New System.Windows.Forms.Button()
        Me.lblTableSetsFile = New System.Windows.Forms.Label()
        Me.txtTableSetsFile = New System.Windows.Forms.TextBox()
        Me.cbxDataProvider = New System.Windows.Forms.ComboBox()
        Me.cbxLoginMethod = New System.Windows.Forms.ComboBox()
        Me.btnConnectionAddOrUpdate = New System.Windows.Forms.Button()
        Me.btnConnectionDefault = New System.Windows.Forms.Button()
        Me.lblConnectionName = New System.Windows.Forms.Label()
        Me.txtConnectionName = New System.Windows.Forms.TextBox()
        Me.lblDataLocation = New System.Windows.Forms.Label()
        Me.lblLoginName = New System.Windows.Forms.Label()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblDataProvider = New System.Windows.Forms.Label()
        Me.lblLoginMethod = New System.Windows.Forms.Label()
        Me.lblTimeOut = New System.Windows.Forms.Label()
        Me.lblDataBaseName = New System.Windows.Forms.Label()
        Me.txtDataLocation = New System.Windows.Forms.TextBox()
        Me.txtLoginName = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtTimeOut = New System.Windows.Forms.TextBox()
        Me.txtDataBaseName = New System.Windows.Forms.TextBox()
        Me.btnConnectionsShow = New System.Windows.Forms.Button()
        Me.lblSelectedConnection = New System.Windows.Forms.Label()
        Me.tvwConnection = New System.Windows.Forms.TreeView()
        Me.lblConnections = New System.Windows.Forms.Label()
        Me.lvwConnections = New System.Windows.Forms.ListView()
        Me.colConnectionName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDataBaseName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDataLocation = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tpgTableSets = New System.Windows.Forms.TabPage()
        Me.btnDefaultPathSet = New System.Windows.Forms.Button()
        Me.btnDefaultPathBrowse = New System.Windows.Forms.Button()
        Me.lblDefaultPath = New System.Windows.Forms.Label()
        Me.txtDefaultPath = New System.Windows.Forms.TextBox()
        Me.chkDefaultValues = New System.Windows.Forms.CheckBox()
        Me.lblSearchFile = New System.Windows.Forms.Label()
        Me.txtSearchFile = New System.Windows.Forms.TextBox()
        Me.btnDefaultTableSet = New System.Windows.Forms.Button()
        Me.btnTableSetDelete = New System.Windows.Forms.Button()
        Me.btnTableSetClear = New System.Windows.Forms.Button()
        Me.lblReportsFile = New System.Windows.Forms.Label()
        Me.txtReportsFile = New System.Windows.Forms.TextBox()
        Me.btnTableSetAdd = New System.Windows.Forms.Button()
        Me.btnTableSetDefault = New System.Windows.Forms.Button()
        Me.lblTableSetName = New System.Windows.Forms.Label()
        Me.txtTableSetName = New System.Windows.Forms.TextBox()
        Me.lblOutputPath = New System.Windows.Forms.Label()
        Me.lblReportsSetName = New System.Windows.Forms.Label()
        Me.lblTablesFile = New System.Windows.Forms.Label()
        Me.txtOutputPath = New System.Windows.Forms.TextBox()
        Me.txtReportsSetName = New System.Windows.Forms.TextBox()
        Me.txtTablesFile = New System.Windows.Forms.TextBox()
        Me.btnTableSetsShow = New System.Windows.Forms.Button()
        Me.lblSelectedTableSet = New System.Windows.Forms.Label()
        Me.tvwTableSet = New System.Windows.Forms.TreeView()
        Me.lblTableSets = New System.Windows.Forms.Label()
        Me.lvwTableSets = New System.Windows.Forms.ListView()
        Me.colTableSetName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTablesFile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tpgTables = New System.Windows.Forms.TabPage()
        Me.pnlFieldSettings = New System.Windows.Forms.Panel()
        Me.chkRelatedField = New System.Windows.Forms.CheckBox()
        Me.lblFieldName = New System.Windows.Forms.Label()
        Me.lblIdentity = New System.Windows.Forms.Label()
        Me.txtFieldName = New System.Windows.Forms.TextBox()
        Me.chkIdentity = New System.Windows.Forms.CheckBox()
        Me.txtFieldAlias = New System.Windows.Forms.TextBox()
        Me.lblPrimaryKey = New System.Windows.Forms.Label()
        Me.lblFieldAlias = New System.Windows.Forms.Label()
        Me.chkPrimaryKey = New System.Windows.Forms.CheckBox()
        Me.lblDataType = New System.Windows.Forms.Label()
        Me.lblRelatedField = New System.Windows.Forms.Label()
        Me.cbxDataType = New System.Windows.Forms.ComboBox()
        Me.txtRelatedField = New System.Windows.Forms.TextBox()
        Me.txtFieldWidth = New System.Windows.Forms.TextBox()
        Me.txtDefaultButton = New System.Windows.Forms.TextBox()
        Me.lblFieldWidth = New System.Windows.Forms.Label()
        Me.lblDefaultButton = New System.Windows.Forms.Label()
        Me.lblRelations = New System.Windows.Forms.Label()
        Me.chkDefaultButton = New System.Windows.Forms.CheckBox()
        Me.txtControlField = New System.Windows.Forms.TextBox()
        Me.btnRelationAdd = New System.Windows.Forms.Button()
        Me.lblControlField = New System.Windows.Forms.Label()
        Me.txtControlValue = New System.Windows.Forms.TextBox()
        Me.lblControlValue = New System.Windows.Forms.Label()
        Me.btnRelationRemove = New System.Windows.Forms.Button()
        Me.chkControlUpdate = New System.Windows.Forms.CheckBox()
        Me.cbxRelations = New System.Windows.Forms.ComboBox()
        Me.lblControlUpdate = New System.Windows.Forms.Label()
        Me.chkControlMode = New System.Windows.Forms.CheckBox()
        Me.lblControlMode = New System.Windows.Forms.Label()
        Me.chkFieldList = New System.Windows.Forms.CheckBox()
        Me.lblFieldList = New System.Windows.Forms.Label()
        Me.txtFieldListOrder = New System.Windows.Forms.TextBox()
        Me.chkFieldVisible = New System.Windows.Forms.CheckBox()
        Me.txtFieldListWidth = New System.Windows.Forms.TextBox()
        Me.lblFieldVisible = New System.Windows.Forms.Label()
        Me.lblFieldListOrder = New System.Windows.Forms.Label()
        Me.chkFieldSearch = New System.Windows.Forms.CheckBox()
        Me.lblFieldListWidth = New System.Windows.Forms.Label()
        Me.lblFieldSearch = New System.Windows.Forms.Label()
        Me.btnFieldDelete = New System.Windows.Forms.Button()
        Me.chkFieldSearchList = New System.Windows.Forms.CheckBox()
        Me.btnFieldClear = New System.Windows.Forms.Button()
        Me.lblFieldSearchList = New System.Windows.Forms.Label()
        Me.btnFieldAddOrUpdate = New System.Windows.Forms.Button()
        Me.chkFieldUpdate = New System.Windows.Forms.CheckBox()
        Me.lblFieldUpdate = New System.Windows.Forms.Label()
        Me.lstTables = New System.Windows.Forms.ListBox()
        Me.chkImportAllTables = New System.Windows.Forms.CheckBox()
        Me.lblTableDelete = New System.Windows.Forms.Label()
        Me.chkTableDelete = New System.Windows.Forms.CheckBox()
        Me.btnColumnsImport = New System.Windows.Forms.Button()
        Me.btnCrawlTables = New System.Windows.Forms.Button()
        Me.lblTableSearch = New System.Windows.Forms.Label()
        Me.chkTableSearch = New System.Windows.Forms.CheckBox()
        Me.lblTableUpdate = New System.Windows.Forms.Label()
        Me.chkTableUpdate = New System.Windows.Forms.CheckBox()
        Me.lblTableInsert = New System.Windows.Forms.Label()
        Me.chkTableInsert = New System.Windows.Forms.CheckBox()
        Me.lblTableVisible = New System.Windows.Forms.Label()
        Me.chkTableVisible = New System.Windows.Forms.CheckBox()
        Me.btnTableDelete = New System.Windows.Forms.Button()
        Me.btnTableClear = New System.Windows.Forms.Button()
        Me.btnTableAddOrUpdate = New System.Windows.Forms.Button()
        Me.btnTableDefault = New System.Windows.Forms.Button()
        Me.lblTableName = New System.Windows.Forms.Label()
        Me.txtTableName = New System.Windows.Forms.TextBox()
        Me.lblTableAlias = New System.Windows.Forms.Label()
        Me.txtTableAlias = New System.Windows.Forms.TextBox()
        Me.btnTablesShow = New System.Windows.Forms.Button()
        Me.lblSelectedTable = New System.Windows.Forms.Label()
        Me.tvwTable = New System.Windows.Forms.TreeView()
        Me.lblTables = New System.Windows.Forms.Label()
        Me.lvwTables = New System.Windows.Forms.ListView()
        Me.colTableName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTableAlias = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tpgTableTemplates = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnUseTemplate = New System.Windows.Forms.Button()
        Me.btnLoadTemplate = New System.Windows.Forms.Button()
        Me.lblSelectedTemplate = New System.Windows.Forms.Label()
        Me.tvwSelectedTemplate = New System.Windows.Forms.TreeView()
        Me.btnloadTemplates = New System.Windows.Forms.Button()
        Me.txtSearchTemplate = New System.Windows.Forms.TextBox()
        Me.btnSearchTemplate = New System.Windows.Forms.Button()
        Me.lblAvailableTemplates = New System.Windows.Forms.Label()
        Me.lstAvailableTemplates = New System.Windows.Forms.ListBox()
        Me.rtbTableTemplates = New System.Windows.Forms.RichTextBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.tabConfiguration.SuspendLayout()
        Me.tpgConnections.SuspendLayout()
        Me.tpgTableSets.SuspendLayout()
        Me.tpgTables.SuspendLayout()
        Me.pnlFieldSettings.SuspendLayout()
        Me.tpgTableTemplates.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabConfiguration
        '
        Me.tabConfiguration.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabConfiguration.Controls.Add(Me.tpgConnections)
        Me.tabConfiguration.Controls.Add(Me.tpgTableSets)
        Me.tabConfiguration.Controls.Add(Me.tpgTables)
        Me.tabConfiguration.Controls.Add(Me.tpgTableTemplates)
        Me.tabConfiguration.Location = New System.Drawing.Point(12, 16)
        Me.tabConfiguration.Name = "tabConfiguration"
        Me.tabConfiguration.SelectedIndex = 0
        Me.tabConfiguration.Size = New System.Drawing.Size(776, 785)
        Me.tabConfiguration.TabIndex = 0
        '
        'tpgConnections
        '
        Me.tpgConnections.Controls.Add(Me.btnShowDatabasePassword)
        Me.tpgConnections.Controls.Add(Me.btnDefaultTableSetFile)
        Me.tpgConnections.Controls.Add(Me.btnTableSetFileBrowse)
        Me.tpgConnections.Controls.Add(Me.lblBackupPath)
        Me.tpgConnections.Controls.Add(Me.btnBackupLocation)
        Me.tpgConnections.Controls.Add(Me.lblBackupLocation)
        Me.tpgConnections.Controls.Add(Me.txtBackupLocation)
        Me.tpgConnections.Controls.Add(Me.btnBackup)
        Me.tpgConnections.Controls.Add(Me.lstDatabases)
        Me.tpgConnections.Controls.Add(Me.lstServers)
        Me.tpgConnections.Controls.Add(Me.btnCrawlServers)
        Me.tpgConnections.Controls.Add(Me.btnCrawlDatabases)
        Me.tpgConnections.Controls.Add(Me.btnConnectionDelete)
        Me.tpgConnections.Controls.Add(Me.btnConnectionClear)
        Me.tpgConnections.Controls.Add(Me.lblTableSetsFile)
        Me.tpgConnections.Controls.Add(Me.txtTableSetsFile)
        Me.tpgConnections.Controls.Add(Me.cbxDataProvider)
        Me.tpgConnections.Controls.Add(Me.cbxLoginMethod)
        Me.tpgConnections.Controls.Add(Me.btnConnectionAddOrUpdate)
        Me.tpgConnections.Controls.Add(Me.btnConnectionDefault)
        Me.tpgConnections.Controls.Add(Me.lblConnectionName)
        Me.tpgConnections.Controls.Add(Me.txtConnectionName)
        Me.tpgConnections.Controls.Add(Me.lblDataLocation)
        Me.tpgConnections.Controls.Add(Me.lblLoginName)
        Me.tpgConnections.Controls.Add(Me.lblPassword)
        Me.tpgConnections.Controls.Add(Me.lblDataProvider)
        Me.tpgConnections.Controls.Add(Me.lblLoginMethod)
        Me.tpgConnections.Controls.Add(Me.lblTimeOut)
        Me.tpgConnections.Controls.Add(Me.lblDataBaseName)
        Me.tpgConnections.Controls.Add(Me.txtDataLocation)
        Me.tpgConnections.Controls.Add(Me.txtLoginName)
        Me.tpgConnections.Controls.Add(Me.txtPassword)
        Me.tpgConnections.Controls.Add(Me.txtTimeOut)
        Me.tpgConnections.Controls.Add(Me.txtDataBaseName)
        Me.tpgConnections.Controls.Add(Me.btnConnectionsShow)
        Me.tpgConnections.Controls.Add(Me.lblSelectedConnection)
        Me.tpgConnections.Controls.Add(Me.tvwConnection)
        Me.tpgConnections.Controls.Add(Me.lblConnections)
        Me.tpgConnections.Controls.Add(Me.lvwConnections)
        Me.tpgConnections.Location = New System.Drawing.Point(4, 22)
        Me.tpgConnections.Name = "tpgConnections"
        Me.tpgConnections.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgConnections.Size = New System.Drawing.Size(768, 759)
        Me.tpgConnections.TabIndex = 0
        Me.tpgConnections.Text = "Connections"
        Me.tpgConnections.UseVisualStyleBackColor = True
        '
        'btnShowDatabasePassword
        '
        Me.btnShowDatabasePassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowDatabasePassword.Image = Global.Sequenchel.My.Resources.Resources.eye
        Me.btnShowDatabasePassword.Location = New System.Drawing.Point(714, 155)
        Me.btnShowDatabasePassword.Name = "btnShowDatabasePassword"
        Me.btnShowDatabasePassword.Size = New System.Drawing.Size(23, 23)
        Me.btnShowDatabasePassword.TabIndex = 111
        Me.btnShowDatabasePassword.UseVisualStyleBackColor = True
        '
        'btnDefaultTableSetFile
        '
        Me.btnDefaultTableSetFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefaultTableSetFile.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnDefaultTableSetFile.Location = New System.Drawing.Point(527, 228)
        Me.btnDefaultTableSetFile.Name = "btnDefaultTableSetFile"
        Me.btnDefaultTableSetFile.Size = New System.Drawing.Size(23, 23)
        Me.btnDefaultTableSetFile.TabIndex = 110
        '
        'btnTableSetFileBrowse
        '
        Me.btnTableSetFileBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTableSetFileBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTableSetFileBrowse.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnTableSetFileBrowse.Location = New System.Drawing.Point(714, 228)
        Me.btnTableSetFileBrowse.Name = "btnTableSetFileBrowse"
        Me.btnTableSetFileBrowse.Size = New System.Drawing.Size(23, 23)
        Me.btnTableSetFileBrowse.TabIndex = 15
        '
        'lblBackupPath
        '
        Me.lblBackupPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBackupPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBackupPath.Location = New System.Drawing.Point(450, 668)
        Me.lblBackupPath.Name = "lblBackupPath"
        Me.lblBackupPath.Size = New System.Drawing.Size(289, 31)
        Me.lblBackupPath.TabIndex = 108
        Me.lblBackupPath.Text = "This must be a valid (network) path on the PC or Server where the database reside" & _
    "s"
        '
        'btnBackupLocation
        '
        Me.btnBackupLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBackupLocation.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnBackupLocation.Location = New System.Drawing.Point(714, 644)
        Me.btnBackupLocation.Name = "btnBackupLocation"
        Me.btnBackupLocation.Size = New System.Drawing.Size(23, 23)
        Me.btnBackupLocation.TabIndex = 20
        Me.btnBackupLocation.Text = "..."
        '
        'lblBackupLocation
        '
        Me.lblBackupLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBackupLocation.Location = New System.Drawing.Point(450, 647)
        Me.lblBackupLocation.Name = "lblBackupLocation"
        Me.lblBackupLocation.Size = New System.Drawing.Size(96, 16)
        Me.lblBackupLocation.TabIndex = 106
        Me.lblBackupLocation.Text = "Backup Location"
        Me.lblBackupLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtBackupLocation
        '
        Me.txtBackupLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBackupLocation.Location = New System.Drawing.Point(554, 643)
        Me.txtBackupLocation.Name = "txtBackupLocation"
        Me.txtBackupLocation.Size = New System.Drawing.Size(154, 20)
        Me.txtBackupLocation.TabIndex = 19
        '
        'btnBackup
        '
        Me.btnBackup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBackup.Location = New System.Drawing.Point(453, 705)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(131, 20)
        Me.btnBackup.TabIndex = 21
        Me.btnBackup.Text = "Backup"
        '
        'lstDatabases
        '
        Me.lstDatabases.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstDatabases.FormattingEnabled = True
        Me.lstDatabases.Location = New System.Drawing.Point(554, 84)
        Me.lstDatabases.Name = "lstDatabases"
        Me.lstDatabases.Size = New System.Drawing.Size(147, 95)
        Me.lstDatabases.TabIndex = 101
        Me.lstDatabases.TabStop = False
        Me.lstDatabases.Visible = False
        '
        'lstServers
        '
        Me.lstServers.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstServers.FormattingEnabled = True
        Me.lstServers.Location = New System.Drawing.Point(554, 60)
        Me.lstServers.Name = "lstServers"
        Me.lstServers.Size = New System.Drawing.Size(147, 95)
        Me.lstServers.TabIndex = 103
        Me.lstServers.TabStop = False
        Me.lstServers.Visible = False
        '
        'btnCrawlServers
        '
        Me.btnCrawlServers.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCrawlServers.BackgroundImage = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnCrawlServers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCrawlServers.Location = New System.Drawing.Point(707, 61)
        Me.btnCrawlServers.Name = "btnCrawlServers"
        Me.btnCrawlServers.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlServers.TabIndex = 6
        Me.btnCrawlServers.Text = "..."
        '
        'btnCrawlDatabases
        '
        Me.btnCrawlDatabases.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCrawlDatabases.BackgroundImage = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnCrawlDatabases.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCrawlDatabases.Location = New System.Drawing.Point(707, 84)
        Me.btnCrawlDatabases.Name = "btnCrawlDatabases"
        Me.btnCrawlDatabases.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlDatabases.TabIndex = 8
        Me.btnCrawlDatabases.Text = "..."
        '
        'btnConnectionDelete
        '
        Me.btnConnectionDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnectionDelete.Location = New System.Drawing.Point(606, 256)
        Me.btnConnectionDelete.Name = "btnConnectionDelete"
        Me.btnConnectionDelete.Size = New System.Drawing.Size(131, 20)
        Me.btnConnectionDelete.TabIndex = 17
        Me.btnConnectionDelete.Text = "Delete Connection"
        '
        'btnConnectionClear
        '
        Me.btnConnectionClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnectionClear.Location = New System.Drawing.Point(453, 282)
        Me.btnConnectionClear.Name = "btnConnectionClear"
        Me.btnConnectionClear.Size = New System.Drawing.Size(131, 20)
        Me.btnConnectionClear.TabIndex = 18
        Me.btnConnectionClear.Text = "Clear"
        '
        'lblTableSetsFile
        '
        Me.lblTableSetsFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableSetsFile.Location = New System.Drawing.Point(450, 231)
        Me.lblTableSetsFile.Name = "lblTableSetsFile"
        Me.lblTableSetsFile.Size = New System.Drawing.Size(96, 16)
        Me.lblTableSetsFile.TabIndex = 45
        Me.lblTableSetsFile.Text = "TableSets File*"
        Me.lblTableSetsFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTableSetsFile
        '
        Me.txtTableSetsFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTableSetsFile.Location = New System.Drawing.Point(554, 230)
        Me.txtTableSetsFile.Name = "txtTableSetsFile"
        Me.txtTableSetsFile.Size = New System.Drawing.Size(154, 20)
        Me.txtTableSetsFile.TabIndex = 14
        '
        'cbxDataProvider
        '
        Me.cbxDataProvider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxDataProvider.FormattingEnabled = True
        Me.cbxDataProvider.Items.AddRange(New Object() {"SQL"})
        Me.cbxDataProvider.Location = New System.Drawing.Point(554, 181)
        Me.cbxDataProvider.Name = "cbxDataProvider"
        Me.cbxDataProvider.Size = New System.Drawing.Size(183, 21)
        Me.cbxDataProvider.TabIndex = 12
        '
        'cbxLoginMethod
        '
        Me.cbxLoginMethod.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxLoginMethod.FormattingEnabled = True
        Me.cbxLoginMethod.Items.AddRange(New Object() {"SQL", "Windows"})
        Me.cbxLoginMethod.Location = New System.Drawing.Point(554, 108)
        Me.cbxLoginMethod.Name = "cbxLoginMethod"
        Me.cbxLoginMethod.Size = New System.Drawing.Size(183, 21)
        Me.cbxLoginMethod.TabIndex = 9
        '
        'btnConnectionAddOrUpdate
        '
        Me.btnConnectionAddOrUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnectionAddOrUpdate.Location = New System.Drawing.Point(453, 256)
        Me.btnConnectionAddOrUpdate.Name = "btnConnectionAddOrUpdate"
        Me.btnConnectionAddOrUpdate.Size = New System.Drawing.Size(131, 20)
        Me.btnConnectionAddOrUpdate.TabIndex = 16
        Me.btnConnectionAddOrUpdate.Text = "Add/Update Connection"
        '
        'btnConnectionDefault
        '
        Me.btnConnectionDefault.Location = New System.Drawing.Point(25, 256)
        Me.btnConnectionDefault.Name = "btnConnectionDefault"
        Me.btnConnectionDefault.Size = New System.Drawing.Size(164, 20)
        Me.btnConnectionDefault.TabIndex = 1
        Me.btnConnectionDefault.Text = "Set as Default Connection"
        '
        'lblConnectionName
        '
        Me.lblConnectionName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConnectionName.Location = New System.Drawing.Point(450, 37)
        Me.lblConnectionName.Name = "lblConnectionName"
        Me.lblConnectionName.Size = New System.Drawing.Size(96, 16)
        Me.lblConnectionName.TabIndex = 36
        Me.lblConnectionName.Text = "Connection Name*"
        Me.lblConnectionName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtConnectionName
        '
        Me.txtConnectionName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConnectionName.Location = New System.Drawing.Point(554, 36)
        Me.txtConnectionName.Name = "txtConnectionName"
        Me.txtConnectionName.Size = New System.Drawing.Size(183, 20)
        Me.txtConnectionName.TabIndex = 4
        '
        'lblDataLocation
        '
        Me.lblDataLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDataLocation.Location = New System.Drawing.Point(450, 63)
        Me.lblDataLocation.Name = "lblDataLocation"
        Me.lblDataLocation.Size = New System.Drawing.Size(96, 16)
        Me.lblDataLocation.TabIndex = 29
        Me.lblDataLocation.Text = "Database Server*"
        Me.lblDataLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLoginName
        '
        Me.lblLoginName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLoginName.Location = New System.Drawing.Point(450, 134)
        Me.lblLoginName.Name = "lblLoginName"
        Me.lblLoginName.Size = New System.Drawing.Size(96, 16)
        Me.lblLoginName.TabIndex = 30
        Me.lblLoginName.Text = "Login Name"
        Me.lblLoginName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPassword
        '
        Me.lblPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPassword.Location = New System.Drawing.Point(450, 158)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(96, 16)
        Me.lblPassword.TabIndex = 31
        Me.lblPassword.Text = "Password"
        Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDataProvider
        '
        Me.lblDataProvider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDataProvider.Location = New System.Drawing.Point(450, 182)
        Me.lblDataProvider.Name = "lblDataProvider"
        Me.lblDataProvider.Size = New System.Drawing.Size(96, 16)
        Me.lblDataProvider.TabIndex = 32
        Me.lblDataProvider.Text = "Data Provider"
        Me.lblDataProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLoginMethod
        '
        Me.lblLoginMethod.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLoginMethod.Location = New System.Drawing.Point(450, 109)
        Me.lblLoginMethod.Name = "lblLoginMethod"
        Me.lblLoginMethod.Size = New System.Drawing.Size(96, 16)
        Me.lblLoginMethod.TabIndex = 33
        Me.lblLoginMethod.Text = "Login Method"
        Me.lblLoginMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTimeOut
        '
        Me.lblTimeOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTimeOut.Location = New System.Drawing.Point(450, 207)
        Me.lblTimeOut.Name = "lblTimeOut"
        Me.lblTimeOut.Size = New System.Drawing.Size(96, 16)
        Me.lblTimeOut.TabIndex = 34
        Me.lblTimeOut.Text = "Time Out"
        Me.lblTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDataBaseName
        '
        Me.lblDataBaseName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDataBaseName.Location = New System.Drawing.Point(450, 86)
        Me.lblDataBaseName.Name = "lblDataBaseName"
        Me.lblDataBaseName.Size = New System.Drawing.Size(96, 16)
        Me.lblDataBaseName.TabIndex = 28
        Me.lblDataBaseName.Text = "Database Name*"
        Me.lblDataBaseName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDataLocation
        '
        Me.txtDataLocation.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataLocation.Location = New System.Drawing.Point(554, 60)
        Me.txtDataLocation.Name = "txtDataLocation"
        Me.txtDataLocation.Size = New System.Drawing.Size(147, 20)
        Me.txtDataLocation.TabIndex = 5
        '
        'txtLoginName
        '
        Me.txtLoginName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLoginName.Enabled = False
        Me.txtLoginName.Location = New System.Drawing.Point(554, 133)
        Me.txtLoginName.Name = "txtLoginName"
        Me.txtLoginName.Size = New System.Drawing.Size(183, 20)
        Me.txtLoginName.TabIndex = 10
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(554, 157)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(157, 20)
        Me.txtPassword.TabIndex = 11
        Me.txtPassword.Tag = "<Keep Current Password>"
        '
        'txtTimeOut
        '
        Me.txtTimeOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTimeOut.Location = New System.Drawing.Point(554, 206)
        Me.txtTimeOut.Name = "txtTimeOut"
        Me.txtTimeOut.Size = New System.Drawing.Size(183, 20)
        Me.txtTimeOut.TabIndex = 13
        Me.txtTimeOut.Text = "7200"
        '
        'txtDataBaseName
        '
        Me.txtDataBaseName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataBaseName.Location = New System.Drawing.Point(554, 84)
        Me.txtDataBaseName.Name = "txtDataBaseName"
        Me.txtDataBaseName.Size = New System.Drawing.Size(147, 20)
        Me.txtDataBaseName.TabIndex = 7
        '
        'btnConnectionsShow
        '
        Me.btnConnectionsShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnConnectionsShow.Location = New System.Drawing.Point(25, 705)
        Me.btnConnectionsShow.Name = "btnConnectionsShow"
        Me.btnConnectionsShow.Size = New System.Drawing.Size(164, 20)
        Me.btnConnectionsShow.TabIndex = 3
        Me.btnConnectionsShow.Text = "Show All Connections"
        '
        'lblSelectedConnection
        '
        Me.lblSelectedConnection.AutoSize = True
        Me.lblSelectedConnection.Location = New System.Drawing.Point(25, 286)
        Me.lblSelectedConnection.Name = "lblSelectedConnection"
        Me.lblSelectedConnection.Size = New System.Drawing.Size(106, 13)
        Me.lblSelectedConnection.TabIndex = 20
        Me.lblSelectedConnection.Text = "Selected Connection"
        Me.lblSelectedConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tvwConnection
        '
        Me.tvwConnection.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwConnection.Location = New System.Drawing.Point(25, 302)
        Me.tvwConnection.Name = "tvwConnection"
        Me.tvwConnection.Size = New System.Drawing.Size(392, 397)
        Me.tvwConnection.TabIndex = 2
        '
        'lblConnections
        '
        Me.lblConnections.AutoSize = True
        Me.lblConnections.Location = New System.Drawing.Point(25, 20)
        Me.lblConnections.Name = "lblConnections"
        Me.lblConnections.Size = New System.Drawing.Size(66, 13)
        Me.lblConnections.TabIndex = 18
        Me.lblConnections.Text = "Connections"
        Me.lblConnections.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvwConnections
        '
        Me.lvwConnections.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwConnections.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colConnectionName, Me.colDataBaseName, Me.colDataLocation})
        Me.lvwConnections.FullRowSelect = True
        Me.lvwConnections.Location = New System.Drawing.Point(25, 36)
        Me.lvwConnections.Name = "lvwConnections"
        Me.lvwConnections.Size = New System.Drawing.Size(392, 214)
        Me.lvwConnections.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvwConnections.TabIndex = 0
        Me.lvwConnections.UseCompatibleStateImageBehavior = False
        Me.lvwConnections.View = System.Windows.Forms.View.Details
        '
        'colConnectionName
        '
        Me.colConnectionName.Text = "Connection"
        Me.colConnectionName.Width = 99
        '
        'colDataBaseName
        '
        Me.colDataBaseName.Text = "Database"
        Me.colDataBaseName.Width = 140
        '
        'colDataLocation
        '
        Me.colDataLocation.Text = "Data location"
        Me.colDataLocation.Width = 145
        '
        'tpgTableSets
        '
        Me.tpgTableSets.Controls.Add(Me.btnDefaultPathSet)
        Me.tpgTableSets.Controls.Add(Me.btnDefaultPathBrowse)
        Me.tpgTableSets.Controls.Add(Me.lblDefaultPath)
        Me.tpgTableSets.Controls.Add(Me.txtDefaultPath)
        Me.tpgTableSets.Controls.Add(Me.chkDefaultValues)
        Me.tpgTableSets.Controls.Add(Me.lblSearchFile)
        Me.tpgTableSets.Controls.Add(Me.txtSearchFile)
        Me.tpgTableSets.Controls.Add(Me.btnDefaultTableSet)
        Me.tpgTableSets.Controls.Add(Me.btnTableSetDelete)
        Me.tpgTableSets.Controls.Add(Me.btnTableSetClear)
        Me.tpgTableSets.Controls.Add(Me.lblReportsFile)
        Me.tpgTableSets.Controls.Add(Me.txtReportsFile)
        Me.tpgTableSets.Controls.Add(Me.btnTableSetAdd)
        Me.tpgTableSets.Controls.Add(Me.btnTableSetDefault)
        Me.tpgTableSets.Controls.Add(Me.lblTableSetName)
        Me.tpgTableSets.Controls.Add(Me.txtTableSetName)
        Me.tpgTableSets.Controls.Add(Me.lblOutputPath)
        Me.tpgTableSets.Controls.Add(Me.lblReportsSetName)
        Me.tpgTableSets.Controls.Add(Me.lblTablesFile)
        Me.tpgTableSets.Controls.Add(Me.txtOutputPath)
        Me.tpgTableSets.Controls.Add(Me.txtReportsSetName)
        Me.tpgTableSets.Controls.Add(Me.txtTablesFile)
        Me.tpgTableSets.Controls.Add(Me.btnTableSetsShow)
        Me.tpgTableSets.Controls.Add(Me.lblSelectedTableSet)
        Me.tpgTableSets.Controls.Add(Me.tvwTableSet)
        Me.tpgTableSets.Controls.Add(Me.lblTableSets)
        Me.tpgTableSets.Controls.Add(Me.lvwTableSets)
        Me.tpgTableSets.Location = New System.Drawing.Point(4, 22)
        Me.tpgTableSets.Name = "tpgTableSets"
        Me.tpgTableSets.Size = New System.Drawing.Size(768, 759)
        Me.tpgTableSets.TabIndex = 2
        Me.tpgTableSets.Text = "Table Sets"
        Me.tpgTableSets.UseVisualStyleBackColor = True
        '
        'btnDefaultPathSet
        '
        Me.btnDefaultPathSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefaultPathSet.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnDefaultPathSet.Location = New System.Drawing.Point(539, 60)
        Me.btnDefaultPathSet.Name = "btnDefaultPathSet"
        Me.btnDefaultPathSet.Size = New System.Drawing.Size(23, 23)
        Me.btnDefaultPathSet.TabIndex = 107
        '
        'btnDefaultPathBrowse
        '
        Me.btnDefaultPathBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefaultPathBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDefaultPathBrowse.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnDefaultPathBrowse.Location = New System.Drawing.Point(714, 60)
        Me.btnDefaultPathBrowse.Name = "btnDefaultPathBrowse"
        Me.btnDefaultPathBrowse.Size = New System.Drawing.Size(23, 23)
        Me.btnDefaultPathBrowse.TabIndex = 6
        '
        'lblDefaultPath
        '
        Me.lblDefaultPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDefaultPath.Location = New System.Drawing.Point(450, 66)
        Me.lblDefaultPath.Name = "lblDefaultPath"
        Me.lblDefaultPath.Size = New System.Drawing.Size(96, 16)
        Me.lblDefaultPath.TabIndex = 105
        Me.lblDefaultPath.Text = "Default Path"
        Me.lblDefaultPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDefaultPath
        '
        Me.txtDefaultPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDefaultPath.Location = New System.Drawing.Point(567, 62)
        Me.txtDefaultPath.Name = "txtDefaultPath"
        Me.txtDefaultPath.Size = New System.Drawing.Size(145, 20)
        Me.txtDefaultPath.TabIndex = 5
        '
        'chkDefaultValues
        '
        Me.chkDefaultValues.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDefaultValues.AutoSize = True
        Me.chkDefaultValues.Checked = True
        Me.chkDefaultValues.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDefaultValues.Location = New System.Drawing.Point(567, 220)
        Me.chkDefaultValues.Name = "chkDefaultValues"
        Me.chkDefaultValues.Size = New System.Drawing.Size(129, 17)
        Me.chkDefaultValues.TabIndex = 12
        Me.chkDefaultValues.Text = "Autofill Default Values"
        Me.chkDefaultValues.UseVisualStyleBackColor = True
        '
        'lblSearchFile
        '
        Me.lblSearchFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearchFile.Location = New System.Drawing.Point(450, 198)
        Me.lblSearchFile.Name = "lblSearchFile"
        Me.lblSearchFile.Size = New System.Drawing.Size(96, 16)
        Me.lblSearchFile.TabIndex = 102
        Me.lblSearchFile.Text = "Search File"
        Me.lblSearchFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSearchFile
        '
        Me.txtSearchFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearchFile.Location = New System.Drawing.Point(567, 194)
        Me.txtSearchFile.Name = "txtSearchFile"
        Me.txtSearchFile.Size = New System.Drawing.Size(170, 20)
        Me.txtSearchFile.TabIndex = 11
        '
        'btnDefaultTableSet
        '
        Me.btnDefaultTableSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefaultTableSet.Image = Global.Sequenchel.My.Resources.Resources.TSfavicon
        Me.btnDefaultTableSet.Location = New System.Drawing.Point(539, 34)
        Me.btnDefaultTableSet.Name = "btnDefaultTableSet"
        Me.btnDefaultTableSet.Size = New System.Drawing.Size(23, 23)
        Me.btnDefaultTableSet.TabIndex = 100
        '
        'btnTableSetDelete
        '
        Me.btnTableSetDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTableSetDelete.Location = New System.Drawing.Point(606, 256)
        Me.btnTableSetDelete.Name = "btnTableSetDelete"
        Me.btnTableSetDelete.Size = New System.Drawing.Size(131, 20)
        Me.btnTableSetDelete.TabIndex = 14
        Me.btnTableSetDelete.Text = "Delete TableSet"
        '
        'btnTableSetClear
        '
        Me.btnTableSetClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTableSetClear.Location = New System.Drawing.Point(453, 282)
        Me.btnTableSetClear.Name = "btnTableSetClear"
        Me.btnTableSetClear.Size = New System.Drawing.Size(131, 20)
        Me.btnTableSetClear.TabIndex = 15
        Me.btnTableSetClear.Text = "Clear"
        '
        'lblReportsFile
        '
        Me.lblReportsFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReportsFile.Location = New System.Drawing.Point(450, 172)
        Me.lblReportsFile.Name = "lblReportsFile"
        Me.lblReportsFile.Size = New System.Drawing.Size(96, 16)
        Me.lblReportsFile.TabIndex = 73
        Me.lblReportsFile.Text = "Reports File"
        Me.lblReportsFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtReportsFile
        '
        Me.txtReportsFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReportsFile.Location = New System.Drawing.Point(567, 168)
        Me.txtReportsFile.Name = "txtReportsFile"
        Me.txtReportsFile.Size = New System.Drawing.Size(170, 20)
        Me.txtReportsFile.TabIndex = 10
        '
        'btnTableSetAdd
        '
        Me.btnTableSetAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTableSetAdd.Location = New System.Drawing.Point(453, 256)
        Me.btnTableSetAdd.Name = "btnTableSetAdd"
        Me.btnTableSetAdd.Size = New System.Drawing.Size(131, 20)
        Me.btnTableSetAdd.TabIndex = 13
        Me.btnTableSetAdd.Text = "Add/Update TableSet"
        '
        'btnTableSetDefault
        '
        Me.btnTableSetDefault.Location = New System.Drawing.Point(25, 256)
        Me.btnTableSetDefault.Name = "btnTableSetDefault"
        Me.btnTableSetDefault.Size = New System.Drawing.Size(164, 20)
        Me.btnTableSetDefault.TabIndex = 1
        Me.btnTableSetDefault.Text = "Set as Default Tableset"
        '
        'lblTableSetName
        '
        Me.lblTableSetName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableSetName.Location = New System.Drawing.Point(450, 40)
        Me.lblTableSetName.Name = "lblTableSetName"
        Me.lblTableSetName.Size = New System.Drawing.Size(96, 16)
        Me.lblTableSetName.TabIndex = 64
        Me.lblTableSetName.Text = "TableSet Name*"
        Me.lblTableSetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTableSetName
        '
        Me.txtTableSetName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTableSetName.Location = New System.Drawing.Point(567, 36)
        Me.txtTableSetName.Name = "txtTableSetName"
        Me.txtTableSetName.Size = New System.Drawing.Size(170, 20)
        Me.txtTableSetName.TabIndex = 4
        '
        'lblOutputPath
        '
        Me.lblOutputPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOutputPath.Location = New System.Drawing.Point(450, 122)
        Me.lblOutputPath.Name = "lblOutputPath"
        Me.lblOutputPath.Size = New System.Drawing.Size(96, 16)
        Me.lblOutputPath.TabIndex = 57
        Me.lblOutputPath.Text = "Output Path"
        Me.lblOutputPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblReportsSetName
        '
        Me.lblReportsSetName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReportsSetName.Location = New System.Drawing.Point(450, 146)
        Me.lblReportsSetName.Name = "lblReportsSetName"
        Me.lblReportsSetName.Size = New System.Drawing.Size(96, 16)
        Me.lblReportsSetName.TabIndex = 58
        Me.lblReportsSetName.Text = "Reports Set Name"
        Me.lblReportsSetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTablesFile
        '
        Me.lblTablesFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTablesFile.Location = New System.Drawing.Point(450, 98)
        Me.lblTablesFile.Name = "lblTablesFile"
        Me.lblTablesFile.Size = New System.Drawing.Size(96, 16)
        Me.lblTablesFile.TabIndex = 56
        Me.lblTablesFile.Text = "Tables File*"
        Me.lblTablesFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOutputPath
        '
        Me.txtOutputPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutputPath.Location = New System.Drawing.Point(567, 118)
        Me.txtOutputPath.Name = "txtOutputPath"
        Me.txtOutputPath.Size = New System.Drawing.Size(170, 20)
        Me.txtOutputPath.TabIndex = 8
        '
        'txtReportsSetName
        '
        Me.txtReportsSetName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtReportsSetName.Location = New System.Drawing.Point(567, 142)
        Me.txtReportsSetName.Name = "txtReportsSetName"
        Me.txtReportsSetName.Size = New System.Drawing.Size(170, 20)
        Me.txtReportsSetName.TabIndex = 9
        '
        'txtTablesFile
        '
        Me.txtTablesFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTablesFile.Location = New System.Drawing.Point(567, 94)
        Me.txtTablesFile.Name = "txtTablesFile"
        Me.txtTablesFile.Size = New System.Drawing.Size(170, 20)
        Me.txtTablesFile.TabIndex = 7
        '
        'btnTableSetsShow
        '
        Me.btnTableSetsShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTableSetsShow.Location = New System.Drawing.Point(25, 705)
        Me.btnTableSetsShow.Name = "btnTableSetsShow"
        Me.btnTableSetsShow.Size = New System.Drawing.Size(164, 20)
        Me.btnTableSetsShow.TabIndex = 3
        Me.btnTableSetsShow.Text = "Show All TableSets"
        '
        'lblSelectedTableSet
        '
        Me.lblSelectedTableSet.AutoSize = True
        Me.lblSelectedTableSet.Location = New System.Drawing.Point(25, 286)
        Me.lblSelectedTableSet.Name = "lblSelectedTableSet"
        Me.lblSelectedTableSet.Size = New System.Drawing.Size(93, 13)
        Me.lblSelectedTableSet.TabIndex = 50
        Me.lblSelectedTableSet.Text = "Selected Tableset"
        Me.lblSelectedTableSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tvwTableSet
        '
        Me.tvwTableSet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwTableSet.Location = New System.Drawing.Point(25, 302)
        Me.tvwTableSet.Name = "tvwTableSet"
        Me.tvwTableSet.Size = New System.Drawing.Size(392, 397)
        Me.tvwTableSet.TabIndex = 2
        '
        'lblTableSets
        '
        Me.lblTableSets.AutoSize = True
        Me.lblTableSets.Location = New System.Drawing.Point(25, 20)
        Me.lblTableSets.Name = "lblTableSets"
        Me.lblTableSets.Size = New System.Drawing.Size(58, 13)
        Me.lblTableSets.TabIndex = 48
        Me.lblTableSets.Text = "Table Sets"
        Me.lblTableSets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvwTableSets
        '
        Me.lvwTableSets.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwTableSets.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTableSetName, Me.colTablesFile})
        Me.lvwTableSets.FullRowSelect = True
        Me.lvwTableSets.Location = New System.Drawing.Point(25, 36)
        Me.lvwTableSets.Name = "lvwTableSets"
        Me.lvwTableSets.Size = New System.Drawing.Size(392, 214)
        Me.lvwTableSets.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvwTableSets.TabIndex = 0
        Me.lvwTableSets.UseCompatibleStateImageBehavior = False
        Me.lvwTableSets.View = System.Windows.Forms.View.Details
        '
        'colTableSetName
        '
        Me.colTableSetName.Text = "Tables Set"
        Me.colTableSetName.Width = 120
        '
        'colTablesFile
        '
        Me.colTablesFile.Text = "Tables File"
        Me.colTablesFile.Width = 263
        '
        'tpgTables
        '
        Me.tpgTables.Controls.Add(Me.pnlFieldSettings)
        Me.tpgTables.Controls.Add(Me.lstTables)
        Me.tpgTables.Controls.Add(Me.chkImportAllTables)
        Me.tpgTables.Controls.Add(Me.lblTableDelete)
        Me.tpgTables.Controls.Add(Me.chkTableDelete)
        Me.tpgTables.Controls.Add(Me.btnColumnsImport)
        Me.tpgTables.Controls.Add(Me.btnCrawlTables)
        Me.tpgTables.Controls.Add(Me.lblTableSearch)
        Me.tpgTables.Controls.Add(Me.chkTableSearch)
        Me.tpgTables.Controls.Add(Me.lblTableUpdate)
        Me.tpgTables.Controls.Add(Me.chkTableUpdate)
        Me.tpgTables.Controls.Add(Me.lblTableInsert)
        Me.tpgTables.Controls.Add(Me.chkTableInsert)
        Me.tpgTables.Controls.Add(Me.lblTableVisible)
        Me.tpgTables.Controls.Add(Me.chkTableVisible)
        Me.tpgTables.Controls.Add(Me.btnTableDelete)
        Me.tpgTables.Controls.Add(Me.btnTableClear)
        Me.tpgTables.Controls.Add(Me.btnTableAddOrUpdate)
        Me.tpgTables.Controls.Add(Me.btnTableDefault)
        Me.tpgTables.Controls.Add(Me.lblTableName)
        Me.tpgTables.Controls.Add(Me.txtTableName)
        Me.tpgTables.Controls.Add(Me.lblTableAlias)
        Me.tpgTables.Controls.Add(Me.txtTableAlias)
        Me.tpgTables.Controls.Add(Me.btnTablesShow)
        Me.tpgTables.Controls.Add(Me.lblSelectedTable)
        Me.tpgTables.Controls.Add(Me.tvwTable)
        Me.tpgTables.Controls.Add(Me.lblTables)
        Me.tpgTables.Controls.Add(Me.lvwTables)
        Me.tpgTables.Location = New System.Drawing.Point(4, 22)
        Me.tpgTables.Name = "tpgTables"
        Me.tpgTables.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgTables.Size = New System.Drawing.Size(768, 759)
        Me.tpgTables.TabIndex = 1
        Me.tpgTables.Text = "Tables"
        Me.tpgTables.UseVisualStyleBackColor = True
        '
        'pnlFieldSettings
        '
        Me.pnlFieldSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFieldSettings.AutoScroll = True
        Me.pnlFieldSettings.Controls.Add(Me.chkRelatedField)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldName)
        Me.pnlFieldSettings.Controls.Add(Me.lblIdentity)
        Me.pnlFieldSettings.Controls.Add(Me.txtFieldName)
        Me.pnlFieldSettings.Controls.Add(Me.chkIdentity)
        Me.pnlFieldSettings.Controls.Add(Me.txtFieldAlias)
        Me.pnlFieldSettings.Controls.Add(Me.lblPrimaryKey)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldAlias)
        Me.pnlFieldSettings.Controls.Add(Me.chkPrimaryKey)
        Me.pnlFieldSettings.Controls.Add(Me.lblDataType)
        Me.pnlFieldSettings.Controls.Add(Me.lblRelatedField)
        Me.pnlFieldSettings.Controls.Add(Me.cbxDataType)
        Me.pnlFieldSettings.Controls.Add(Me.txtRelatedField)
        Me.pnlFieldSettings.Controls.Add(Me.txtFieldWidth)
        Me.pnlFieldSettings.Controls.Add(Me.txtDefaultButton)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldWidth)
        Me.pnlFieldSettings.Controls.Add(Me.lblDefaultButton)
        Me.pnlFieldSettings.Controls.Add(Me.lblRelations)
        Me.pnlFieldSettings.Controls.Add(Me.chkDefaultButton)
        Me.pnlFieldSettings.Controls.Add(Me.txtControlField)
        Me.pnlFieldSettings.Controls.Add(Me.btnRelationAdd)
        Me.pnlFieldSettings.Controls.Add(Me.lblControlField)
        Me.pnlFieldSettings.Controls.Add(Me.txtControlValue)
        Me.pnlFieldSettings.Controls.Add(Me.lblControlValue)
        Me.pnlFieldSettings.Controls.Add(Me.btnRelationRemove)
        Me.pnlFieldSettings.Controls.Add(Me.chkControlUpdate)
        Me.pnlFieldSettings.Controls.Add(Me.cbxRelations)
        Me.pnlFieldSettings.Controls.Add(Me.lblControlUpdate)
        Me.pnlFieldSettings.Controls.Add(Me.chkControlMode)
        Me.pnlFieldSettings.Controls.Add(Me.lblControlMode)
        Me.pnlFieldSettings.Controls.Add(Me.chkFieldList)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldList)
        Me.pnlFieldSettings.Controls.Add(Me.txtFieldListOrder)
        Me.pnlFieldSettings.Controls.Add(Me.chkFieldVisible)
        Me.pnlFieldSettings.Controls.Add(Me.txtFieldListWidth)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldVisible)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldListOrder)
        Me.pnlFieldSettings.Controls.Add(Me.chkFieldSearch)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldListWidth)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldSearch)
        Me.pnlFieldSettings.Controls.Add(Me.btnFieldDelete)
        Me.pnlFieldSettings.Controls.Add(Me.chkFieldSearchList)
        Me.pnlFieldSettings.Controls.Add(Me.btnFieldClear)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldSearchList)
        Me.pnlFieldSettings.Controls.Add(Me.btnFieldAddOrUpdate)
        Me.pnlFieldSettings.Controls.Add(Me.chkFieldUpdate)
        Me.pnlFieldSettings.Controls.Add(Me.lblFieldUpdate)
        Me.pnlFieldSettings.Location = New System.Drawing.Point(444, 304)
        Me.pnlFieldSettings.Name = "pnlFieldSettings"
        Me.pnlFieldSettings.Size = New System.Drawing.Size(318, 453)
        Me.pnlFieldSettings.TabIndex = 162
        '
        'chkRelatedField
        '
        Me.chkRelatedField.AutoSize = True
        Me.chkRelatedField.Location = New System.Drawing.Point(107, 142)
        Me.chkRelatedField.Name = "chkRelatedField"
        Me.chkRelatedField.Size = New System.Drawing.Size(15, 14)
        Me.chkRelatedField.TabIndex = 162
        Me.chkRelatedField.UseVisualStyleBackColor = True
        '
        'lblFieldName
        '
        Me.lblFieldName.Location = New System.Drawing.Point(3, 10)
        Me.lblFieldName.Name = "lblFieldName"
        Me.lblFieldName.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldName.TabIndex = 108
        Me.lblFieldName.Text = "Field Name*"
        Me.lblFieldName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblIdentity
        '
        Me.lblIdentity.Location = New System.Drawing.Point(173, 246)
        Me.lblIdentity.Name = "lblIdentity"
        Me.lblIdentity.Size = New System.Drawing.Size(96, 16)
        Me.lblIdentity.TabIndex = 161
        Me.lblIdentity.Text = "Identity"
        Me.lblIdentity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFieldName
        '
        Me.txtFieldName.Location = New System.Drawing.Point(107, 6)
        Me.txtFieldName.Name = "txtFieldName"
        Me.txtFieldName.Size = New System.Drawing.Size(183, 20)
        Me.txtFieldName.TabIndex = 0
        '
        'chkIdentity
        '
        Me.chkIdentity.AutoSize = True
        Me.chkIdentity.Location = New System.Drawing.Point(277, 248)
        Me.chkIdentity.Name = "chkIdentity"
        Me.chkIdentity.Size = New System.Drawing.Size(15, 14)
        Me.chkIdentity.TabIndex = 18
        Me.chkIdentity.UseVisualStyleBackColor = True
        '
        'txtFieldAlias
        '
        Me.txtFieldAlias.Location = New System.Drawing.Point(107, 32)
        Me.txtFieldAlias.Name = "txtFieldAlias"
        Me.txtFieldAlias.Size = New System.Drawing.Size(183, 20)
        Me.txtFieldAlias.TabIndex = 1
        '
        'lblPrimaryKey
        '
        Me.lblPrimaryKey.Location = New System.Drawing.Point(173, 272)
        Me.lblPrimaryKey.Name = "lblPrimaryKey"
        Me.lblPrimaryKey.Size = New System.Drawing.Size(96, 16)
        Me.lblPrimaryKey.TabIndex = 159
        Me.lblPrimaryKey.Text = "Primary Key"
        Me.lblPrimaryKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFieldAlias
        '
        Me.lblFieldAlias.Location = New System.Drawing.Point(3, 36)
        Me.lblFieldAlias.Name = "lblFieldAlias"
        Me.lblFieldAlias.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldAlias.TabIndex = 110
        Me.lblFieldAlias.Text = "Field Alias*"
        Me.lblFieldAlias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkPrimaryKey
        '
        Me.chkPrimaryKey.AutoSize = True
        Me.chkPrimaryKey.Location = New System.Drawing.Point(277, 274)
        Me.chkPrimaryKey.Name = "chkPrimaryKey"
        Me.chkPrimaryKey.Size = New System.Drawing.Size(15, 14)
        Me.chkPrimaryKey.TabIndex = 19
        Me.chkPrimaryKey.UseVisualStyleBackColor = True
        '
        'lblDataType
        '
        Me.lblDataType.Location = New System.Drawing.Point(3, 62)
        Me.lblDataType.Name = "lblDataType"
        Me.lblDataType.Size = New System.Drawing.Size(96, 16)
        Me.lblDataType.TabIndex = 112
        Me.lblDataType.Text = "Data Type*"
        Me.lblDataType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRelatedField
        '
        Me.lblRelatedField.Location = New System.Drawing.Point(3, 142)
        Me.lblRelatedField.Name = "lblRelatedField"
        Me.lblRelatedField.Size = New System.Drawing.Size(96, 16)
        Me.lblRelatedField.TabIndex = 157
        Me.lblRelatedField.Text = "Related Field (List)"
        Me.lblRelatedField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbxDataType
        '
        Me.cbxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxDataType.FormattingEnabled = True
        Me.cbxDataType.Location = New System.Drawing.Point(107, 58)
        Me.cbxDataType.Name = "cbxDataType"
        Me.cbxDataType.Size = New System.Drawing.Size(183, 21)
        Me.cbxDataType.TabIndex = 2
        '
        'txtRelatedField
        '
        Me.txtRelatedField.Location = New System.Drawing.Point(128, 138)
        Me.txtRelatedField.Name = "txtRelatedField"
        Me.txtRelatedField.Size = New System.Drawing.Size(162, 20)
        Me.txtRelatedField.TabIndex = 8
        '
        'txtFieldWidth
        '
        Me.txtFieldWidth.Location = New System.Drawing.Point(107, 85)
        Me.txtFieldWidth.Name = "txtFieldWidth"
        Me.txtFieldWidth.Size = New System.Drawing.Size(183, 20)
        Me.txtFieldWidth.TabIndex = 3
        '
        'txtDefaultButton
        '
        Me.txtDefaultButton.Location = New System.Drawing.Point(128, 164)
        Me.txtDefaultButton.Name = "txtDefaultButton"
        Me.txtDefaultButton.Size = New System.Drawing.Size(163, 20)
        Me.txtDefaultButton.TabIndex = 10
        '
        'lblFieldWidth
        '
        Me.lblFieldWidth.Location = New System.Drawing.Point(3, 89)
        Me.lblFieldWidth.Name = "lblFieldWidth"
        Me.lblFieldWidth.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldWidth.TabIndex = 115
        Me.lblFieldWidth.Tag = "Field Width"
        Me.lblFieldWidth.Text = "Field Width"
        Me.lblFieldWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDefaultButton
        '
        Me.lblDefaultButton.Location = New System.Drawing.Point(3, 165)
        Me.lblDefaultButton.Name = "lblDefaultButton"
        Me.lblDefaultButton.Size = New System.Drawing.Size(96, 16)
        Me.lblDefaultButton.TabIndex = 154
        Me.lblDefaultButton.Text = "Default Button"
        Me.lblDefaultButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRelations
        '
        Me.lblRelations.Location = New System.Drawing.Point(3, 115)
        Me.lblRelations.Name = "lblRelations"
        Me.lblRelations.Size = New System.Drawing.Size(96, 16)
        Me.lblRelations.TabIndex = 117
        Me.lblRelations.Text = "Relations"
        Me.lblRelations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkDefaultButton
        '
        Me.chkDefaultButton.AutoSize = True
        Me.chkDefaultButton.Location = New System.Drawing.Point(107, 167)
        Me.chkDefaultButton.Name = "chkDefaultButton"
        Me.chkDefaultButton.Size = New System.Drawing.Size(15, 14)
        Me.chkDefaultButton.TabIndex = 9
        Me.chkDefaultButton.UseVisualStyleBackColor = True
        '
        'txtControlField
        '
        Me.txtControlField.Location = New System.Drawing.Point(107, 190)
        Me.txtControlField.Name = "txtControlField"
        Me.txtControlField.Size = New System.Drawing.Size(183, 20)
        Me.txtControlField.TabIndex = 11
        '
        'btnRelationAdd
        '
        Me.btnRelationAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRelationAdd.Location = New System.Drawing.Point(249, 112)
        Me.btnRelationAdd.Name = "btnRelationAdd"
        Me.btnRelationAdd.Size = New System.Drawing.Size(20, 20)
        Me.btnRelationAdd.TabIndex = 6
        Me.btnRelationAdd.Text = "V"
        Me.btnRelationAdd.UseVisualStyleBackColor = True
        '
        'lblControlField
        '
        Me.lblControlField.Location = New System.Drawing.Point(3, 194)
        Me.lblControlField.Name = "lblControlField"
        Me.lblControlField.Size = New System.Drawing.Size(96, 16)
        Me.lblControlField.TabIndex = 119
        Me.lblControlField.Text = "Control Field"
        Me.lblControlField.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtControlValue
        '
        Me.txtControlValue.Location = New System.Drawing.Point(107, 216)
        Me.txtControlValue.Name = "txtControlValue"
        Me.txtControlValue.Size = New System.Drawing.Size(183, 20)
        Me.txtControlValue.TabIndex = 12
        '
        'lblControlValue
        '
        Me.lblControlValue.Location = New System.Drawing.Point(3, 220)
        Me.lblControlValue.Name = "lblControlValue"
        Me.lblControlValue.Size = New System.Drawing.Size(96, 16)
        Me.lblControlValue.TabIndex = 121
        Me.lblControlValue.Text = "Control Value"
        Me.lblControlValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRelationRemove
        '
        Me.btnRelationRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRelationRemove.Location = New System.Drawing.Point(271, 112)
        Me.btnRelationRemove.Name = "btnRelationRemove"
        Me.btnRelationRemove.Size = New System.Drawing.Size(20, 20)
        Me.btnRelationRemove.TabIndex = 7
        Me.btnRelationRemove.Text = "X"
        Me.btnRelationRemove.UseVisualStyleBackColor = True
        '
        'chkControlUpdate
        '
        Me.chkControlUpdate.AutoSize = True
        Me.chkControlUpdate.Location = New System.Drawing.Point(107, 248)
        Me.chkControlUpdate.Name = "chkControlUpdate"
        Me.chkControlUpdate.Size = New System.Drawing.Size(15, 14)
        Me.chkControlUpdate.TabIndex = 13
        Me.chkControlUpdate.UseVisualStyleBackColor = True
        '
        'cbxRelations
        '
        Me.cbxRelations.FormattingEnabled = True
        Me.cbxRelations.Location = New System.Drawing.Point(107, 111)
        Me.cbxRelations.Name = "cbxRelations"
        Me.cbxRelations.Size = New System.Drawing.Size(137, 21)
        Me.cbxRelations.TabIndex = 4
        '
        'lblControlUpdate
        '
        Me.lblControlUpdate.Location = New System.Drawing.Point(3, 246)
        Me.lblControlUpdate.Name = "lblControlUpdate"
        Me.lblControlUpdate.Size = New System.Drawing.Size(96, 16)
        Me.lblControlUpdate.TabIndex = 123
        Me.lblControlUpdate.Text = "Control Update"
        Me.lblControlUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkControlMode
        '
        Me.chkControlMode.AutoSize = True
        Me.chkControlMode.Location = New System.Drawing.Point(107, 274)
        Me.chkControlMode.Name = "chkControlMode"
        Me.chkControlMode.Size = New System.Drawing.Size(15, 14)
        Me.chkControlMode.TabIndex = 14
        Me.chkControlMode.UseVisualStyleBackColor = True
        '
        'lblControlMode
        '
        Me.lblControlMode.Location = New System.Drawing.Point(3, 272)
        Me.lblControlMode.Name = "lblControlMode"
        Me.lblControlMode.Size = New System.Drawing.Size(96, 16)
        Me.lblControlMode.TabIndex = 125
        Me.lblControlMode.Text = "Control Mode"
        Me.lblControlMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkFieldList
        '
        Me.chkFieldList.AutoSize = True
        Me.chkFieldList.Location = New System.Drawing.Point(107, 298)
        Me.chkFieldList.Name = "chkFieldList"
        Me.chkFieldList.Size = New System.Drawing.Size(15, 14)
        Me.chkFieldList.TabIndex = 15
        Me.chkFieldList.UseVisualStyleBackColor = True
        '
        'lblFieldList
        '
        Me.lblFieldList.Location = New System.Drawing.Point(3, 296)
        Me.lblFieldList.Name = "lblFieldList"
        Me.lblFieldList.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldList.TabIndex = 127
        Me.lblFieldList.Text = "Field List"
        Me.lblFieldList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFieldListOrder
        '
        Me.txtFieldListOrder.Enabled = False
        Me.txtFieldListOrder.Location = New System.Drawing.Point(104, 349)
        Me.txtFieldListOrder.Name = "txtFieldListOrder"
        Me.txtFieldListOrder.Size = New System.Drawing.Size(44, 20)
        Me.txtFieldListOrder.TabIndex = 17
        '
        'chkFieldVisible
        '
        Me.chkFieldVisible.AutoSize = True
        Me.chkFieldVisible.Location = New System.Drawing.Point(277, 299)
        Me.chkFieldVisible.Name = "chkFieldVisible"
        Me.chkFieldVisible.Size = New System.Drawing.Size(15, 14)
        Me.chkFieldVisible.TabIndex = 20
        Me.chkFieldVisible.UseVisualStyleBackColor = True
        '
        'txtFieldListWidth
        '
        Me.txtFieldListWidth.Enabled = False
        Me.txtFieldListWidth.Location = New System.Drawing.Point(104, 322)
        Me.txtFieldListWidth.Name = "txtFieldListWidth"
        Me.txtFieldListWidth.Size = New System.Drawing.Size(44, 20)
        Me.txtFieldListWidth.TabIndex = 16
        '
        'lblFieldVisible
        '
        Me.lblFieldVisible.Location = New System.Drawing.Point(173, 297)
        Me.lblFieldVisible.Name = "lblFieldVisible"
        Me.lblFieldVisible.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldVisible.TabIndex = 129
        Me.lblFieldVisible.Text = "Field Visible"
        Me.lblFieldVisible.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFieldListOrder
        '
        Me.lblFieldListOrder.Location = New System.Drawing.Point(3, 350)
        Me.lblFieldListOrder.Name = "lblFieldListOrder"
        Me.lblFieldListOrder.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldListOrder.TabIndex = 141
        Me.lblFieldListOrder.Text = "Field List Order"
        Me.lblFieldListOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkFieldSearch
        '
        Me.chkFieldSearch.AutoSize = True
        Me.chkFieldSearch.Location = New System.Drawing.Point(277, 324)
        Me.chkFieldSearch.Name = "chkFieldSearch"
        Me.chkFieldSearch.Size = New System.Drawing.Size(15, 14)
        Me.chkFieldSearch.TabIndex = 21
        Me.chkFieldSearch.UseVisualStyleBackColor = True
        '
        'lblFieldListWidth
        '
        Me.lblFieldListWidth.Location = New System.Drawing.Point(3, 323)
        Me.lblFieldListWidth.Name = "lblFieldListWidth"
        Me.lblFieldListWidth.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldListWidth.TabIndex = 140
        Me.lblFieldListWidth.Text = "Field List Width"
        Me.lblFieldListWidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFieldSearch
        '
        Me.lblFieldSearch.Location = New System.Drawing.Point(173, 322)
        Me.lblFieldSearch.Name = "lblFieldSearch"
        Me.lblFieldSearch.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldSearch.TabIndex = 131
        Me.lblFieldSearch.Text = "Field Search"
        Me.lblFieldSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFieldDelete
        '
        Me.btnFieldDelete.Location = New System.Drawing.Point(161, 402)
        Me.btnFieldDelete.Name = "btnFieldDelete"
        Me.btnFieldDelete.Size = New System.Drawing.Size(131, 20)
        Me.btnFieldDelete.TabIndex = 25
        Me.btnFieldDelete.Text = "Delete Field"
        '
        'chkFieldSearchList
        '
        Me.chkFieldSearchList.AutoSize = True
        Me.chkFieldSearchList.Location = New System.Drawing.Point(277, 351)
        Me.chkFieldSearchList.Name = "chkFieldSearchList"
        Me.chkFieldSearchList.Size = New System.Drawing.Size(15, 14)
        Me.chkFieldSearchList.TabIndex = 22
        Me.chkFieldSearchList.UseVisualStyleBackColor = True
        '
        'btnFieldClear
        '
        Me.btnFieldClear.Location = New System.Drawing.Point(11, 428)
        Me.btnFieldClear.Name = "btnFieldClear"
        Me.btnFieldClear.Size = New System.Drawing.Size(131, 20)
        Me.btnFieldClear.TabIndex = 26
        Me.btnFieldClear.Text = "Clear"
        '
        'lblFieldSearchList
        '
        Me.lblFieldSearchList.Location = New System.Drawing.Point(173, 349)
        Me.lblFieldSearchList.Name = "lblFieldSearchList"
        Me.lblFieldSearchList.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldSearchList.TabIndex = 133
        Me.lblFieldSearchList.Text = "Field Search List"
        Me.lblFieldSearchList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnFieldAddOrUpdate
        '
        Me.btnFieldAddOrUpdate.Location = New System.Drawing.Point(11, 402)
        Me.btnFieldAddOrUpdate.Name = "btnFieldAddOrUpdate"
        Me.btnFieldAddOrUpdate.Size = New System.Drawing.Size(131, 20)
        Me.btnFieldAddOrUpdate.TabIndex = 24
        Me.btnFieldAddOrUpdate.Text = "Add/Update Field"
        '
        'chkFieldUpdate
        '
        Me.chkFieldUpdate.AutoSize = True
        Me.chkFieldUpdate.Location = New System.Drawing.Point(277, 378)
        Me.chkFieldUpdate.Name = "chkFieldUpdate"
        Me.chkFieldUpdate.Size = New System.Drawing.Size(15, 14)
        Me.chkFieldUpdate.TabIndex = 23
        Me.chkFieldUpdate.UseVisualStyleBackColor = True
        '
        'lblFieldUpdate
        '
        Me.lblFieldUpdate.Location = New System.Drawing.Point(173, 376)
        Me.lblFieldUpdate.Name = "lblFieldUpdate"
        Me.lblFieldUpdate.Size = New System.Drawing.Size(96, 16)
        Me.lblFieldUpdate.TabIndex = 135
        Me.lblFieldUpdate.Text = "Field Update"
        Me.lblFieldUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lstTables
        '
        Me.lstTables.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstTables.FormattingEnabled = True
        Me.lstTables.HorizontalScrollbar = True
        Me.lstTables.Location = New System.Drawing.Point(554, 35)
        Me.lstTables.Name = "lstTables"
        Me.lstTables.Size = New System.Drawing.Size(147, 95)
        Me.lstTables.TabIndex = 145
        Me.lstTables.TabStop = False
        Me.lstTables.Visible = False
        '
        'chkImportAllTables
        '
        Me.chkImportAllTables.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkImportAllTables.AutoSize = True
        Me.chkImportAllTables.Location = New System.Drawing.Point(453, 209)
        Me.chkImportAllTables.Name = "chkImportAllTables"
        Me.chkImportAllTables.Size = New System.Drawing.Size(72, 17)
        Me.chkImportAllTables.TabIndex = 12
        Me.chkImportAllTables.Text = "All Tables"
        Me.chkImportAllTables.UseVisualStyleBackColor = True
        '
        'lblTableDelete
        '
        Me.lblTableDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableDelete.Location = New System.Drawing.Point(450, 185)
        Me.lblTableDelete.Name = "lblTableDelete"
        Me.lblTableDelete.Size = New System.Drawing.Size(96, 16)
        Me.lblTableDelete.TabIndex = 148
        Me.lblTableDelete.Text = "Table Delete"
        Me.lblTableDelete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTableDelete
        '
        Me.chkTableDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTableDelete.AutoSize = True
        Me.chkTableDelete.Location = New System.Drawing.Point(554, 187)
        Me.chkTableDelete.Name = "chkTableDelete"
        Me.chkTableDelete.Size = New System.Drawing.Size(15, 14)
        Me.chkTableDelete.TabIndex = 11
        Me.chkTableDelete.UseVisualStyleBackColor = True
        '
        'btnColumnsImport
        '
        Me.btnColumnsImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnColumnsImport.Location = New System.Drawing.Point(554, 207)
        Me.btnColumnsImport.Name = "btnColumnsImport"
        Me.btnColumnsImport.Size = New System.Drawing.Size(180, 20)
        Me.btnColumnsImport.TabIndex = 13
        Me.btnColumnsImport.Text = "Import Columns for this table"
        '
        'btnCrawlTables
        '
        Me.btnCrawlTables.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCrawlTables.BackgroundImage = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnCrawlTables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCrawlTables.Location = New System.Drawing.Point(707, 35)
        Me.btnCrawlTables.Name = "btnCrawlTables"
        Me.btnCrawlTables.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlTables.TabIndex = 5
        Me.btnCrawlTables.Text = "..."
        '
        'lblTableSearch
        '
        Me.lblTableSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableSearch.Location = New System.Drawing.Point(450, 112)
        Me.lblTableSearch.Name = "lblTableSearch"
        Me.lblTableSearch.Size = New System.Drawing.Size(96, 16)
        Me.lblTableSearch.TabIndex = 106
        Me.lblTableSearch.Text = "Table Search"
        Me.lblTableSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTableSearch
        '
        Me.chkTableSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTableSearch.AutoSize = True
        Me.chkTableSearch.Checked = True
        Me.chkTableSearch.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTableSearch.Location = New System.Drawing.Point(554, 114)
        Me.chkTableSearch.Name = "chkTableSearch"
        Me.chkTableSearch.Size = New System.Drawing.Size(15, 14)
        Me.chkTableSearch.TabIndex = 8
        Me.chkTableSearch.UseVisualStyleBackColor = True
        '
        'lblTableUpdate
        '
        Me.lblTableUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableUpdate.Location = New System.Drawing.Point(450, 136)
        Me.lblTableUpdate.Name = "lblTableUpdate"
        Me.lblTableUpdate.Size = New System.Drawing.Size(96, 16)
        Me.lblTableUpdate.TabIndex = 104
        Me.lblTableUpdate.Text = "Table Update"
        Me.lblTableUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTableUpdate
        '
        Me.chkTableUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTableUpdate.AutoSize = True
        Me.chkTableUpdate.Location = New System.Drawing.Point(554, 138)
        Me.chkTableUpdate.Name = "chkTableUpdate"
        Me.chkTableUpdate.Size = New System.Drawing.Size(15, 14)
        Me.chkTableUpdate.TabIndex = 9
        Me.chkTableUpdate.UseVisualStyleBackColor = True
        '
        'lblTableInsert
        '
        Me.lblTableInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableInsert.Location = New System.Drawing.Point(450, 160)
        Me.lblTableInsert.Name = "lblTableInsert"
        Me.lblTableInsert.Size = New System.Drawing.Size(96, 16)
        Me.lblTableInsert.TabIndex = 102
        Me.lblTableInsert.Text = "Table Insert"
        Me.lblTableInsert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTableInsert
        '
        Me.chkTableInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTableInsert.AutoSize = True
        Me.chkTableInsert.Location = New System.Drawing.Point(554, 162)
        Me.chkTableInsert.Name = "chkTableInsert"
        Me.chkTableInsert.Size = New System.Drawing.Size(15, 14)
        Me.chkTableInsert.TabIndex = 10
        Me.chkTableInsert.UseVisualStyleBackColor = True
        '
        'lblTableVisible
        '
        Me.lblTableVisible.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableVisible.Location = New System.Drawing.Point(450, 88)
        Me.lblTableVisible.Name = "lblTableVisible"
        Me.lblTableVisible.Size = New System.Drawing.Size(96, 16)
        Me.lblTableVisible.TabIndex = 100
        Me.lblTableVisible.Text = "Table Visible"
        Me.lblTableVisible.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkTableVisible
        '
        Me.chkTableVisible.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTableVisible.AutoSize = True
        Me.chkTableVisible.Checked = True
        Me.chkTableVisible.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTableVisible.Location = New System.Drawing.Point(554, 90)
        Me.chkTableVisible.Name = "chkTableVisible"
        Me.chkTableVisible.Size = New System.Drawing.Size(15, 14)
        Me.chkTableVisible.TabIndex = 7
        Me.chkTableVisible.UseVisualStyleBackColor = True
        '
        'btnTableDelete
        '
        Me.btnTableDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTableDelete.Location = New System.Drawing.Point(603, 256)
        Me.btnTableDelete.Name = "btnTableDelete"
        Me.btnTableDelete.Size = New System.Drawing.Size(131, 20)
        Me.btnTableDelete.TabIndex = 15
        Me.btnTableDelete.Text = "Delete Table"
        '
        'btnTableClear
        '
        Me.btnTableClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTableClear.Location = New System.Drawing.Point(453, 282)
        Me.btnTableClear.Name = "btnTableClear"
        Me.btnTableClear.Size = New System.Drawing.Size(131, 20)
        Me.btnTableClear.TabIndex = 16
        Me.btnTableClear.Text = "Clear"
        '
        'btnTableAddOrUpdate
        '
        Me.btnTableAddOrUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTableAddOrUpdate.Location = New System.Drawing.Point(453, 256)
        Me.btnTableAddOrUpdate.Name = "btnTableAddOrUpdate"
        Me.btnTableAddOrUpdate.Size = New System.Drawing.Size(131, 20)
        Me.btnTableAddOrUpdate.TabIndex = 14
        Me.btnTableAddOrUpdate.Text = "Add/Update Table"
        '
        'btnTableDefault
        '
        Me.btnTableDefault.Location = New System.Drawing.Point(25, 256)
        Me.btnTableDefault.Name = "btnTableDefault"
        Me.btnTableDefault.Size = New System.Drawing.Size(164, 20)
        Me.btnTableDefault.TabIndex = 1
        Me.btnTableDefault.Text = "Set as Default Table"
        '
        'lblTableName
        '
        Me.lblTableName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableName.Location = New System.Drawing.Point(450, 40)
        Me.lblTableName.Name = "lblTableName"
        Me.lblTableName.Size = New System.Drawing.Size(96, 16)
        Me.lblTableName.TabIndex = 89
        Me.lblTableName.Text = "Table Name"
        Me.lblTableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTableName
        '
        Me.txtTableName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTableName.Location = New System.Drawing.Point(554, 36)
        Me.txtTableName.Name = "txtTableName"
        Me.txtTableName.Size = New System.Drawing.Size(147, 20)
        Me.txtTableName.TabIndex = 4
        '
        'lblTableAlias
        '
        Me.lblTableAlias.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTableAlias.Location = New System.Drawing.Point(450, 64)
        Me.lblTableAlias.Name = "lblTableAlias"
        Me.lblTableAlias.Size = New System.Drawing.Size(96, 16)
        Me.lblTableAlias.TabIndex = 85
        Me.lblTableAlias.Text = "Table Alias"
        Me.lblTableAlias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTableAlias
        '
        Me.txtTableAlias.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTableAlias.Location = New System.Drawing.Point(554, 60)
        Me.txtTableAlias.Name = "txtTableAlias"
        Me.txtTableAlias.Size = New System.Drawing.Size(183, 20)
        Me.txtTableAlias.TabIndex = 6
        '
        'btnTablesShow
        '
        Me.btnTablesShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnTablesShow.Location = New System.Drawing.Point(25, 705)
        Me.btnTablesShow.Name = "btnTablesShow"
        Me.btnTablesShow.Size = New System.Drawing.Size(164, 20)
        Me.btnTablesShow.TabIndex = 3
        Me.btnTablesShow.Text = "Show All Tables"
        '
        'lblSelectedTable
        '
        Me.lblSelectedTable.AutoSize = True
        Me.lblSelectedTable.Location = New System.Drawing.Point(25, 286)
        Me.lblSelectedTable.Name = "lblSelectedTable"
        Me.lblSelectedTable.Size = New System.Drawing.Size(79, 13)
        Me.lblSelectedTable.TabIndex = 81
        Me.lblSelectedTable.Text = "Selected Table"
        Me.lblSelectedTable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tvwTable
        '
        Me.tvwTable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwTable.Location = New System.Drawing.Point(25, 302)
        Me.tvwTable.Name = "tvwTable"
        Me.tvwTable.Size = New System.Drawing.Size(392, 397)
        Me.tvwTable.TabIndex = 2
        '
        'lblTables
        '
        Me.lblTables.AutoSize = True
        Me.lblTables.Location = New System.Drawing.Point(25, 20)
        Me.lblTables.Name = "lblTables"
        Me.lblTables.Size = New System.Drawing.Size(39, 13)
        Me.lblTables.TabIndex = 79
        Me.lblTables.Text = "Tables"
        Me.lblTables.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvwTables
        '
        Me.lvwTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwTables.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTableName, Me.colTableAlias})
        Me.lvwTables.FullRowSelect = True
        Me.lvwTables.Location = New System.Drawing.Point(25, 36)
        Me.lvwTables.Name = "lvwTables"
        Me.lvwTables.Size = New System.Drawing.Size(392, 214)
        Me.lvwTables.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvwTables.TabIndex = 0
        Me.lvwTables.UseCompatibleStateImageBehavior = False
        Me.lvwTables.View = System.Windows.Forms.View.Details
        '
        'colTableName
        '
        Me.colTableName.Text = "Table Name"
        Me.colTableName.Width = 120
        '
        'colTableAlias
        '
        Me.colTableAlias.Text = "Table Alias"
        Me.colTableAlias.Width = 263
        '
        'tpgTableTemplates
        '
        Me.tpgTableTemplates.Controls.Add(Me.Button1)
        Me.tpgTableTemplates.Controls.Add(Me.btnUseTemplate)
        Me.tpgTableTemplates.Controls.Add(Me.btnLoadTemplate)
        Me.tpgTableTemplates.Controls.Add(Me.lblSelectedTemplate)
        Me.tpgTableTemplates.Controls.Add(Me.tvwSelectedTemplate)
        Me.tpgTableTemplates.Controls.Add(Me.btnloadTemplates)
        Me.tpgTableTemplates.Controls.Add(Me.txtSearchTemplate)
        Me.tpgTableTemplates.Controls.Add(Me.btnSearchTemplate)
        Me.tpgTableTemplates.Controls.Add(Me.lblAvailableTemplates)
        Me.tpgTableTemplates.Controls.Add(Me.lstAvailableTemplates)
        Me.tpgTableTemplates.Controls.Add(Me.rtbTableTemplates)
        Me.tpgTableTemplates.Location = New System.Drawing.Point(4, 22)
        Me.tpgTableTemplates.Name = "tpgTableTemplates"
        Me.tpgTableTemplates.Size = New System.Drawing.Size(768, 759)
        Me.tpgTableTemplates.TabIndex = 3
        Me.tpgTableTemplates.Text = "Table Templates"
        Me.tpgTableTemplates.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(171, 704)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(140, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Search Sequenchel.com"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnUseTemplate
        '
        Me.btnUseTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUseTemplate.Location = New System.Drawing.Point(335, 704)
        Me.btnUseTemplate.Name = "btnUseTemplate"
        Me.btnUseTemplate.Size = New System.Drawing.Size(140, 23)
        Me.btnUseTemplate.TabIndex = 6
        Me.btnUseTemplate.Text = "Use Selected Template"
        Me.btnUseTemplate.UseVisualStyleBackColor = True
        '
        'btnLoadTemplate
        '
        Me.btnLoadTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoadTemplate.Location = New System.Drawing.Point(481, 705)
        Me.btnLoadTemplate.Name = "btnLoadTemplate"
        Me.btnLoadTemplate.Size = New System.Drawing.Size(140, 23)
        Me.btnLoadTemplate.TabIndex = 7
        Me.btnLoadTemplate.Text = "Load Template From File"
        Me.btnLoadTemplate.UseVisualStyleBackColor = True
        '
        'lblSelectedTemplate
        '
        Me.lblSelectedTemplate.AutoSize = True
        Me.lblSelectedTemplate.Location = New System.Drawing.Point(335, 101)
        Me.lblSelectedTemplate.Name = "lblSelectedTemplate"
        Me.lblSelectedTemplate.Size = New System.Drawing.Size(96, 13)
        Me.lblSelectedTemplate.TabIndex = 82
        Me.lblSelectedTemplate.Text = "Selected Template"
        '
        'tvwSelectedTemplate
        '
        Me.tvwSelectedTemplate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvwSelectedTemplate.Location = New System.Drawing.Point(335, 117)
        Me.tvwSelectedTemplate.Name = "tvwSelectedTemplate"
        Me.tvwSelectedTemplate.Size = New System.Drawing.Size(392, 547)
        Me.tvwSelectedTemplate.TabIndex = 5
        '
        'btnloadTemplates
        '
        Me.btnloadTemplates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnloadTemplates.Location = New System.Drawing.Point(25, 732)
        Me.btnloadTemplates.Name = "btnloadTemplates"
        Me.btnloadTemplates.Size = New System.Drawing.Size(286, 23)
        Me.btnloadTemplates.TabIndex = 4
        Me.btnloadTemplates.Text = "Load All Local Templates"
        Me.btnloadTemplates.UseVisualStyleBackColor = True
        '
        'txtSearchTemplate
        '
        Me.txtSearchTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSearchTemplate.Location = New System.Drawing.Point(25, 678)
        Me.txtSearchTemplate.Name = "txtSearchTemplate"
        Me.txtSearchTemplate.Size = New System.Drawing.Size(286, 20)
        Me.txtSearchTemplate.TabIndex = 1
        '
        'btnSearchTemplate
        '
        Me.btnSearchTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSearchTemplate.Location = New System.Drawing.Point(25, 705)
        Me.btnSearchTemplate.Name = "btnSearchTemplate"
        Me.btnSearchTemplate.Size = New System.Drawing.Size(140, 23)
        Me.btnSearchTemplate.TabIndex = 2
        Me.btnSearchTemplate.Text = "Search Local"
        Me.btnSearchTemplate.UseVisualStyleBackColor = True
        '
        'lblAvailableTemplates
        '
        Me.lblAvailableTemplates.AutoSize = True
        Me.lblAvailableTemplates.Location = New System.Drawing.Point(27, 101)
        Me.lblAvailableTemplates.Name = "lblAvailableTemplates"
        Me.lblAvailableTemplates.Size = New System.Drawing.Size(102, 13)
        Me.lblAvailableTemplates.TabIndex = 2
        Me.lblAvailableTemplates.Text = "Available Templates"
        '
        'lstAvailableTemplates
        '
        Me.lstAvailableTemplates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstAvailableTemplates.FormattingEnabled = True
        Me.lstAvailableTemplates.Location = New System.Drawing.Point(25, 117)
        Me.lstAvailableTemplates.Name = "lstAvailableTemplates"
        Me.lstAvailableTemplates.Size = New System.Drawing.Size(286, 537)
        Me.lstAvailableTemplates.TabIndex = 0
        '
        'rtbTableTemplates
        '
        Me.rtbTableTemplates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbTableTemplates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbTableTemplates.Location = New System.Drawing.Point(25, 33)
        Me.rtbTableTemplates.Name = "rtbTableTemplates"
        Me.rtbTableTemplates.ReadOnly = True
        Me.rtbTableTemplates.Size = New System.Drawing.Size(702, 46)
        Me.rtbTableTemplates.TabIndex = 0
        Me.rtbTableTemplates.Text = resources.GetString("rtbTableTemplates.Text")
        '
        'btnTest
        '
        Me.btnTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTest.Location = New System.Drawing.Point(367, 803)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(135, 23)
        Me.btnTest.TabIndex = 7
        Me.btnTest.TabStop = False
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        Me.btnTest.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(12, 808)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 18)
        Me.lblStatus.TabIndex = 8
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(649, 803)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(135, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(508, 803)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(135, 23)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        Me.btnCancel.Visible = False
        '
        'frmConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(800, 842)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.tabConfiguration)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(713, 575)
        Me.Name = "frmConfiguration"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configuration"
        Me.tabConfiguration.ResumeLayout(False)
        Me.tpgConnections.ResumeLayout(False)
        Me.tpgConnections.PerformLayout()
        Me.tpgTableSets.ResumeLayout(False)
        Me.tpgTableSets.PerformLayout()
        Me.tpgTables.ResumeLayout(False)
        Me.tpgTables.PerformLayout()
        Me.pnlFieldSettings.ResumeLayout(False)
        Me.pnlFieldSettings.PerformLayout()
        Me.tpgTableTemplates.ResumeLayout(False)
        Me.tpgTableTemplates.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tabConfiguration As System.Windows.Forms.TabControl
    Friend WithEvents tpgConnections As System.Windows.Forms.TabPage
    Friend WithEvents tpgTables As System.Windows.Forms.TabPage
    Friend WithEvents lblSelectedConnection As System.Windows.Forms.Label
    Friend WithEvents tvwConnection As System.Windows.Forms.TreeView
    Friend WithEvents lblConnections As System.Windows.Forms.Label
    Friend WithEvents lvwConnections As System.Windows.Forms.ListView
    Friend WithEvents colConnectionName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDataBaseName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDataLocation As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnConnectionsShow As System.Windows.Forms.Button
    Friend WithEvents cbxDataProvider As System.Windows.Forms.ComboBox
    Friend WithEvents cbxLoginMethod As System.Windows.Forms.ComboBox
    Friend WithEvents btnConnectionAddOrUpdate As System.Windows.Forms.Button
    Friend WithEvents btnConnectionDefault As System.Windows.Forms.Button
    Friend WithEvents lblConnectionName As System.Windows.Forms.Label
    Friend WithEvents txtConnectionName As System.Windows.Forms.TextBox
    Friend WithEvents lblDataLocation As System.Windows.Forms.Label
    Friend WithEvents lblLoginName As System.Windows.Forms.Label
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblDataProvider As System.Windows.Forms.Label
    Friend WithEvents lblLoginMethod As System.Windows.Forms.Label
    Friend WithEvents lblTimeOut As System.Windows.Forms.Label
    Friend WithEvents lblDataBaseName As System.Windows.Forms.Label
    Friend WithEvents txtDataLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtLoginName As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtTimeOut As System.Windows.Forms.TextBox
    Friend WithEvents txtDataBaseName As System.Windows.Forms.TextBox
    Friend WithEvents lblTableSetsFile As System.Windows.Forms.Label
    Friend WithEvents txtTableSetsFile As System.Windows.Forms.TextBox
    Friend WithEvents tpgTableSets As System.Windows.Forms.TabPage
    Friend WithEvents btnTableSetAdd As System.Windows.Forms.Button
    Friend WithEvents btnTableSetDefault As System.Windows.Forms.Button
    Friend WithEvents lblTableSetName As System.Windows.Forms.Label
    Friend WithEvents txtTableSetName As System.Windows.Forms.TextBox
    Friend WithEvents lblOutputPath As System.Windows.Forms.Label
    Friend WithEvents lblReportsSetName As System.Windows.Forms.Label
    Friend WithEvents lblTablesFile As System.Windows.Forms.Label
    Friend WithEvents txtOutputPath As System.Windows.Forms.TextBox
    Friend WithEvents txtReportsSetName As System.Windows.Forms.TextBox
    Friend WithEvents txtTablesFile As System.Windows.Forms.TextBox
    Friend WithEvents btnTableSetsShow As System.Windows.Forms.Button
    Friend WithEvents lblSelectedTableSet As System.Windows.Forms.Label
    Friend WithEvents tvwTableSet As System.Windows.Forms.TreeView
    Friend WithEvents lblTableSets As System.Windows.Forms.Label
    Friend WithEvents lvwTableSets As System.Windows.Forms.ListView
    Friend WithEvents colTableSetName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTablesFile As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblReportsFile As System.Windows.Forms.Label
    Friend WithEvents txtReportsFile As System.Windows.Forms.TextBox
    Friend WithEvents btnConnectionClear As System.Windows.Forms.Button
    Friend WithEvents btnTableSetClear As System.Windows.Forms.Button
    Friend WithEvents btnTableClear As System.Windows.Forms.Button
    Friend WithEvents btnTableAddOrUpdate As System.Windows.Forms.Button
    Friend WithEvents btnTableDefault As System.Windows.Forms.Button
    Friend WithEvents lblTableName As System.Windows.Forms.Label
    Friend WithEvents txtTableName As System.Windows.Forms.TextBox
    Friend WithEvents lblTableAlias As System.Windows.Forms.Label
    Friend WithEvents txtTableAlias As System.Windows.Forms.TextBox
    Friend WithEvents btnTablesShow As System.Windows.Forms.Button
    Friend WithEvents lblSelectedTable As System.Windows.Forms.Label
    Friend WithEvents tvwTable As System.Windows.Forms.TreeView
    Friend WithEvents lblTables As System.Windows.Forms.Label
    Friend WithEvents lvwTables As System.Windows.Forms.ListView
    Friend WithEvents colTableName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTableAlias As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnConnectionDelete As System.Windows.Forms.Button
    Friend WithEvents btnTableSetDelete As System.Windows.Forms.Button
    Friend WithEvents btnTableDelete As System.Windows.Forms.Button
    Friend WithEvents lblTableSearch As System.Windows.Forms.Label
    Friend WithEvents chkTableSearch As System.Windows.Forms.CheckBox
    Friend WithEvents lblTableUpdate As System.Windows.Forms.Label
    Friend WithEvents chkTableUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents lblTableInsert As System.Windows.Forms.Label
    Friend WithEvents chkTableInsert As System.Windows.Forms.CheckBox
    Friend WithEvents lblTableVisible As System.Windows.Forms.Label
    Friend WithEvents chkTableVisible As System.Windows.Forms.CheckBox
    Friend WithEvents btnFieldDelete As System.Windows.Forms.Button
    Friend WithEvents btnFieldClear As System.Windows.Forms.Button
    Friend WithEvents btnFieldAddOrUpdate As System.Windows.Forms.Button
    Friend WithEvents lblFieldUpdate As System.Windows.Forms.Label
    Friend WithEvents chkFieldUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents lblFieldSearchList As System.Windows.Forms.Label
    Friend WithEvents chkFieldSearchList As System.Windows.Forms.CheckBox
    Friend WithEvents lblFieldSearch As System.Windows.Forms.Label
    Friend WithEvents chkFieldSearch As System.Windows.Forms.CheckBox
    Friend WithEvents lblFieldVisible As System.Windows.Forms.Label
    Friend WithEvents chkFieldVisible As System.Windows.Forms.CheckBox
    Friend WithEvents lblFieldList As System.Windows.Forms.Label
    Friend WithEvents chkFieldList As System.Windows.Forms.CheckBox
    Friend WithEvents lblControlMode As System.Windows.Forms.Label
    Friend WithEvents chkControlMode As System.Windows.Forms.CheckBox
    Friend WithEvents lblControlUpdate As System.Windows.Forms.Label
    Friend WithEvents chkControlUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents lblControlValue As System.Windows.Forms.Label
    Friend WithEvents txtControlValue As System.Windows.Forms.TextBox
    Friend WithEvents lblControlField As System.Windows.Forms.Label
    Friend WithEvents txtControlField As System.Windows.Forms.TextBox
    Friend WithEvents lblRelations As System.Windows.Forms.Label
    Friend WithEvents lblFieldWidth As System.Windows.Forms.Label
    Friend WithEvents txtFieldWidth As System.Windows.Forms.TextBox
    Friend WithEvents cbxDataType As System.Windows.Forms.ComboBox
    Friend WithEvents lblDataType As System.Windows.Forms.Label
    Friend WithEvents lblFieldAlias As System.Windows.Forms.Label
    Friend WithEvents txtFieldAlias As System.Windows.Forms.TextBox
    Friend WithEvents lblFieldName As System.Windows.Forms.Label
    Friend WithEvents txtFieldName As System.Windows.Forms.TextBox
    Friend WithEvents txtFieldListOrder As System.Windows.Forms.TextBox
    Friend WithEvents txtFieldListWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblFieldListOrder As System.Windows.Forms.Label
    Friend WithEvents lblFieldListWidth As System.Windows.Forms.Label
    Friend WithEvents lstDatabases As System.Windows.Forms.ListBox
    Friend WithEvents btnCrawlDatabases As System.Windows.Forms.Button
    Friend WithEvents btnDefaultTableSet As System.Windows.Forms.Button
    Friend WithEvents lstTables As System.Windows.Forms.ListBox
    Friend WithEvents btnCrawlTables As System.Windows.Forms.Button
    Friend WithEvents btnColumnsImport As System.Windows.Forms.Button
    Friend WithEvents lblTableDelete As System.Windows.Forms.Label
    Friend WithEvents chkTableDelete As System.Windows.Forms.CheckBox
    Friend WithEvents btnCrawlServers As System.Windows.Forms.Button
    Friend WithEvents lstServers As System.Windows.Forms.ListBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents lblBackupPath As System.Windows.Forms.Label
    Friend WithEvents btnBackupLocation As System.Windows.Forms.Button
    Friend WithEvents lblBackupLocation As System.Windows.Forms.Label
    Friend WithEvents txtBackupLocation As System.Windows.Forms.TextBox
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents cbxRelations As System.Windows.Forms.ComboBox
    Friend WithEvents btnRelationRemove As System.Windows.Forms.Button
    Friend WithEvents chkImportAllTables As System.Windows.Forms.CheckBox
    Friend WithEvents lblSearchFile As System.Windows.Forms.Label
    Friend WithEvents txtSearchFile As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents btnRelationAdd As System.Windows.Forms.Button
    Friend WithEvents tpgTableTemplates As System.Windows.Forms.TabPage
    Friend WithEvents lblAvailableTemplates As System.Windows.Forms.Label
    Friend WithEvents lstAvailableTemplates As System.Windows.Forms.ListBox
    Friend WithEvents rtbTableTemplates As System.Windows.Forms.RichTextBox
    Friend WithEvents btnloadTemplates As System.Windows.Forms.Button
    Friend WithEvents txtSearchTemplate As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchTemplate As System.Windows.Forms.Button
    Friend WithEvents btnUseTemplate As System.Windows.Forms.Button
    Friend WithEvents btnLoadTemplate As System.Windows.Forms.Button
    Friend WithEvents lblSelectedTemplate As System.Windows.Forms.Label
    Friend WithEvents tvwSelectedTemplate As System.Windows.Forms.TreeView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtDefaultButton As System.Windows.Forms.TextBox
    Friend WithEvents lblDefaultButton As System.Windows.Forms.Label
    Friend WithEvents chkDefaultButton As System.Windows.Forms.CheckBox
    Friend WithEvents lblRelatedField As System.Windows.Forms.Label
    Friend WithEvents txtRelatedField As System.Windows.Forms.TextBox
    Friend WithEvents lblPrimaryKey As System.Windows.Forms.Label
    Friend WithEvents chkPrimaryKey As System.Windows.Forms.CheckBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkDefaultValues As System.Windows.Forms.CheckBox
    Friend WithEvents btnDefaultPathBrowse As System.Windows.Forms.Button
    Friend WithEvents lblDefaultPath As System.Windows.Forms.Label
    Friend WithEvents txtDefaultPath As System.Windows.Forms.TextBox
    Friend WithEvents lblIdentity As System.Windows.Forms.Label
    Friend WithEvents chkIdentity As System.Windows.Forms.CheckBox
    Friend WithEvents btnDefaultPathSet As System.Windows.Forms.Button
    Friend WithEvents btnDefaultTableSetFile As System.Windows.Forms.Button
    Friend WithEvents btnTableSetFileBrowse As System.Windows.Forms.Button
    Friend WithEvents pnlFieldSettings As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chkRelatedField As System.Windows.Forms.CheckBox
    Friend WithEvents btnShowDatabasePassword As System.Windows.Forms.Button
End Class
