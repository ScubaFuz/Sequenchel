<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReports))
        Me.lblLicense = New System.Windows.Forms.Label()
        Me.tabReports = New System.Windows.Forms.TabControl()
        Me.tpgReportDefinition = New System.Windows.Forms.TabPage()
        Me.sptReports = New System.Windows.Forms.SplitContainer()
        Me.lvwAvailableFields = New System.Windows.Forms.ListView()
        Me.colAvTableName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAvFieldAlias = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblAvailableFields = New System.Windows.Forms.Label()
        Me.sptReportFields = New System.Windows.Forms.SplitContainer()
        Me.sptReportTables = New System.Windows.Forms.SplitContainer()
        Me.pnlSplitFields = New System.Windows.Forms.Panel()
        Me.btnReportFieldAdd = New System.Windows.Forms.Button()
        Me.btnReportFieldRemove = New System.Windows.Forms.Button()
        Me.lvwSelectedFields = New System.Windows.Forms.ListView()
        Me.colSeltableName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSelFieldAlias = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblSelectedFields = New System.Windows.Forms.Label()
        Me.pnlSplitTables = New System.Windows.Forms.Panel()
        Me.lblSelectedTables = New System.Windows.Forms.Label()
        Me.pnlSplitFields2 = New System.Windows.Forms.Panel()
        Me.lvwSelectedTables = New System.Windows.Forms.ListView()
        Me.colTableAlias = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTableName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.sptReport = New System.Windows.Forms.SplitContainer()
        Me.pnlSelectedFieldsMain = New System.Windows.Forms.Panel()
        Me.pnlSelectedFields = New System.Windows.Forms.Panel()
        Me.pnlReportFilterText = New System.Windows.Forms.Panel()
        Me.pnlReportFilterType = New System.Windows.Forms.Panel()
        Me.pnlReportFilterMode = New System.Windows.Forms.Panel()
        Me.pnlReportFilter = New System.Windows.Forms.Panel()
        Me.pnlReportSortOrder = New System.Windows.Forms.Panel()
        Me.pnlReportSort = New System.Windows.Forms.Panel()
        Me.pnlReportShowMode = New System.Windows.Forms.Panel()
        Me.pnlReportDisplay = New System.Windows.Forms.Panel()
        Me.pnlReportLabel = New System.Windows.Forms.Panel()
        Me.pnlReportHeaders = New System.Windows.Forms.Panel()
        Me.chkReportShow = New System.Windows.Forms.CheckBox()
        Me.lblReportSortOrder = New System.Windows.Forms.Label()
        Me.lblReportSort = New System.Windows.Forms.Label()
        Me.lblReportShowMode = New System.Windows.Forms.Label()
        Me.lblReportFilterMode = New System.Windows.Forms.Label()
        Me.lblReportFilterText = New System.Windows.Forms.Label()
        Me.lblReportFilterType = New System.Windows.Forms.Label()
        Me.lblReportFilter = New System.Windows.Forms.Label()
        Me.lblReportSelectedFields = New System.Windows.Forms.Label()
        Me.pnlSplitSelectedFields = New System.Windows.Forms.Panel()
        Me.btnReportFieldDown = New System.Windows.Forms.Button()
        Me.btnReportFieldUp = New System.Windows.Forms.Button()
        Me.pnlRelationsMain = New System.Windows.Forms.Panel()
        Me.pnlRelations = New System.Windows.Forms.Panel()
        Me.pnlRelationsTargetTable = New System.Windows.Forms.Panel()
        Me.pnlRelationsTargetField = New System.Windows.Forms.Panel()
        Me.pnlRelationsJoinType = New System.Windows.Forms.Panel()
        Me.pnlRelationsField = New System.Windows.Forms.Panel()
        Me.pnlRelationsUse = New System.Windows.Forms.Panel()
        Me.pnlRelationsLabel = New System.Windows.Forms.Panel()
        Me.pnlRelationHeaders = New System.Windows.Forms.Panel()
        Me.lblRelationsTargetField = New System.Windows.Forms.Label()
        Me.lblRelationsTargetTable = New System.Windows.Forms.Label()
        Me.lblSourceField = New System.Windows.Forms.Label()
        Me.lblRelationUse = New System.Windows.Forms.Label()
        Me.lblRelationsJoinType = New System.Windows.Forms.Label()
        Me.lblRelationsSelectedTable = New System.Windows.Forms.Label()
        Me.pnlSplitSelectedTables = New System.Windows.Forms.Panel()
        Me.btnReportTableUp = New System.Windows.Forms.Button()
        Me.btnReportTableDown = New System.Windows.Forms.Button()
        Me.pnlReportButtons = New System.Windows.Forms.Panel()
        Me.btnQueryShow = New System.Windows.Forms.Button()
        Me.btnReportClear = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.btnReportCreate = New System.Windows.Forms.Button()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.chkDistinct = New System.Windows.Forms.CheckBox()
        Me.btnReportExport = New System.Windows.Forms.Button()
        Me.chkTop = New System.Windows.Forms.CheckBox()
        Me.btnReportImport = New System.Windows.Forms.Button()
        Me.txtTop = New System.Windows.Forms.TextBox()
        Me.btnRevertChanges = New System.Windows.Forms.Button()
        Me.pnlReportConnection = New System.Windows.Forms.Panel()
        Me.lblReport = New System.Windows.Forms.Label()
        Me.cbxReportName = New System.Windows.Forms.ComboBox()
        Me.btnReportAddOrUpdate = New System.Windows.Forms.Button()
        Me.btnTablesReload = New System.Windows.Forms.Button()
        Me.btnReportDelete = New System.Windows.Forms.Button()
        Me.btnTableSetsReload = New System.Windows.Forms.Button()
        Me.lblConnection = New System.Windows.Forms.Label()
        Me.btnConnectionsReload = New System.Windows.Forms.Button()
        Me.cbxConnection = New System.Windows.Forms.ComboBox()
        Me.lblTable = New System.Windows.Forms.Label()
        Me.cbxTableSet = New System.Windows.Forms.ComboBox()
        Me.cbxTable = New System.Windows.Forms.ComboBox()
        Me.lblTableSet = New System.Windows.Forms.Label()
        Me.tpgReportResult = New System.Windows.Forms.TabPage()
        Me.cbxEmailResults = New System.Windows.Forms.ComboBox()
        Me.btnEmailResults = New System.Windows.Forms.Button()
        Me.lblElapsedTime = New System.Windows.Forms.Label()
        Me.btnDefinition = New System.Windows.Forms.Button()
        Me.btnExportToFile = New System.Windows.Forms.Button()
        Me.lblErrorMessage = New System.Windows.Forms.Label()
        Me.lblListCount = New System.Windows.Forms.Label()
        Me.lblListCountNumber = New System.Windows.Forms.Label()
        Me.btnLoadQuery = New System.Windows.Forms.Button()
        Me.btnSaveQuery = New System.Windows.Forms.Button()
        Me.btnExecuteQuery = New System.Windows.Forms.Button()
        Me.rtbQuery = New System.Windows.Forms.RichTextBox()
        Me.dgvReport = New Sequenchel.usrDataGridView()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.tmrElapsedTime = New System.Windows.Forms.Timer(Me.components)
        Me.lblStatusTitle = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuReportsFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsFileClose = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReportsHelpManual = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblStatusText = New System.Windows.Forms.TextBox()
        Me.tabReports.SuspendLayout()
        Me.tpgReportDefinition.SuspendLayout()
        CType(Me.sptReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptReports.Panel1.SuspendLayout()
        Me.sptReports.Panel2.SuspendLayout()
        Me.sptReports.SuspendLayout()
        CType(Me.sptReportFields, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptReportFields.Panel1.SuspendLayout()
        Me.sptReportFields.Panel2.SuspendLayout()
        Me.sptReportFields.SuspendLayout()
        CType(Me.sptReportTables, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptReportTables.Panel1.SuspendLayout()
        Me.sptReportTables.Panel2.SuspendLayout()
        Me.sptReportTables.SuspendLayout()
        Me.pnlSplitFields.SuspendLayout()
        Me.pnlSplitTables.SuspendLayout()
        CType(Me.sptReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptReport.Panel1.SuspendLayout()
        Me.sptReport.Panel2.SuspendLayout()
        Me.sptReport.SuspendLayout()
        Me.pnlSelectedFieldsMain.SuspendLayout()
        Me.pnlSelectedFields.SuspendLayout()
        Me.pnlReportHeaders.SuspendLayout()
        Me.pnlSplitSelectedFields.SuspendLayout()
        Me.pnlRelationsMain.SuspendLayout()
        Me.pnlRelations.SuspendLayout()
        Me.pnlRelationHeaders.SuspendLayout()
        Me.pnlSplitSelectedTables.SuspendLayout()
        Me.pnlReportButtons.SuspendLayout()
        Me.pnlReportConnection.SuspendLayout()
        Me.tpgReportResult.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblLicense
        '
        Me.lblLicense.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLicense.AutoSize = True
        Me.lblLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicense.Location = New System.Drawing.Point(898, 2)
        Me.lblLicense.Name = "lblLicense"
        Me.lblLicense.Size = New System.Drawing.Size(375, 24)
        Me.lblLicense.TabIndex = 168
        Me.lblLicense.Text = "Licensed to: Thicor Services Demo License"
        '
        'tabReports
        '
        Me.tabReports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabReports.Controls.Add(Me.tpgReportDefinition)
        Me.tabReports.Controls.Add(Me.tpgReportResult)
        Me.tabReports.Location = New System.Drawing.Point(12, 29)
        Me.tabReports.Name = "tabReports"
        Me.tabReports.SelectedIndex = 0
        Me.tabReports.Size = New System.Drawing.Size(1260, 743)
        Me.tabReports.TabIndex = 0
        '
        'tpgReportDefinition
        '
        Me.tpgReportDefinition.Controls.Add(Me.sptReports)
        Me.tpgReportDefinition.Controls.Add(Me.pnlReportButtons)
        Me.tpgReportDefinition.Controls.Add(Me.pnlReportConnection)
        Me.tpgReportDefinition.Location = New System.Drawing.Point(4, 22)
        Me.tpgReportDefinition.Name = "tpgReportDefinition"
        Me.tpgReportDefinition.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgReportDefinition.Size = New System.Drawing.Size(1252, 717)
        Me.tpgReportDefinition.TabIndex = 0
        Me.tpgReportDefinition.Text = "Report Definition"
        Me.tpgReportDefinition.UseVisualStyleBackColor = True
        '
        'sptReports
        '
        Me.sptReports.BackColor = System.Drawing.SystemColors.Control
        Me.sptReports.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sptReports.Location = New System.Drawing.Point(3, 54)
        Me.sptReports.Name = "sptReports"
        '
        'sptReports.Panel1
        '
        Me.sptReports.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.sptReports.Panel1.Controls.Add(Me.lvwAvailableFields)
        Me.sptReports.Panel1.Controls.Add(Me.lblAvailableFields)
        Me.sptReports.Panel1MinSize = 175
        '
        'sptReports.Panel2
        '
        Me.sptReports.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.sptReports.Panel2.Controls.Add(Me.sptReportFields)
        Me.sptReports.Panel2MinSize = 175
        Me.sptReports.Size = New System.Drawing.Size(1246, 602)
        Me.sptReports.SplitterDistance = 175
        Me.sptReports.TabIndex = 193
        '
        'lvwAvailableFields
        '
        Me.lvwAvailableFields.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwAvailableFields.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colAvTableName, Me.colAvFieldAlias})
        Me.lvwAvailableFields.FullRowSelect = True
        Me.lvwAvailableFields.Location = New System.Drawing.Point(0, 25)
        Me.lvwAvailableFields.Name = "lvwAvailableFields"
        Me.lvwAvailableFields.Size = New System.Drawing.Size(174, 576)
        Me.lvwAvailableFields.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvwAvailableFields.TabIndex = 0
        Me.lvwAvailableFields.UseCompatibleStateImageBehavior = False
        Me.lvwAvailableFields.View = System.Windows.Forms.View.Details
        '
        'colAvTableName
        '
        Me.colAvTableName.Text = "Table"
        Me.colAvTableName.Width = 89
        '
        'colAvFieldAlias
        '
        Me.colAvFieldAlias.Text = "Field"
        Me.colAvFieldAlias.Width = 95
        '
        'lblAvailableFields
        '
        Me.lblAvailableFields.AutoSize = True
        Me.lblAvailableFields.Location = New System.Drawing.Point(10, 5)
        Me.lblAvailableFields.Name = "lblAvailableFields"
        Me.lblAvailableFields.Size = New System.Drawing.Size(80, 13)
        Me.lblAvailableFields.TabIndex = 167
        Me.lblAvailableFields.Text = "Available Fields"
        '
        'sptReportFields
        '
        Me.sptReportFields.BackColor = System.Drawing.SystemColors.Control
        Me.sptReportFields.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sptReportFields.Location = New System.Drawing.Point(0, 0)
        Me.sptReportFields.Name = "sptReportFields"
        '
        'sptReportFields.Panel1
        '
        Me.sptReportFields.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.sptReportFields.Panel1.Controls.Add(Me.sptReportTables)
        Me.sptReportFields.Panel1MinSize = 200
        '
        'sptReportFields.Panel2
        '
        Me.sptReportFields.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.sptReportFields.Panel2.Controls.Add(Me.sptReport)
        Me.sptReportFields.Panel2MinSize = 200
        Me.sptReportFields.Size = New System.Drawing.Size(1067, 602)
        Me.sptReportFields.SplitterDistance = 201
        Me.sptReportFields.TabIndex = 182
        '
        'sptReportTables
        '
        Me.sptReportTables.BackColor = System.Drawing.SystemColors.Control
        Me.sptReportTables.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sptReportTables.Location = New System.Drawing.Point(0, 0)
        Me.sptReportTables.Name = "sptReportTables"
        Me.sptReportTables.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'sptReportTables.Panel1
        '
        Me.sptReportTables.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.sptReportTables.Panel1.Controls.Add(Me.pnlSplitFields)
        Me.sptReportTables.Panel1.Controls.Add(Me.lvwSelectedFields)
        Me.sptReportTables.Panel1.Controls.Add(Me.lblSelectedFields)
        Me.sptReportTables.Panel1MinSize = 200
        '
        'sptReportTables.Panel2
        '
        Me.sptReportTables.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.sptReportTables.Panel2.Controls.Add(Me.pnlSplitTables)
        Me.sptReportTables.Panel2.Controls.Add(Me.pnlSplitFields2)
        Me.sptReportTables.Panel2.Controls.Add(Me.lvwSelectedTables)
        Me.sptReportTables.Panel2MinSize = 200
        Me.sptReportTables.Size = New System.Drawing.Size(201, 602)
        Me.sptReportTables.SplitterDistance = 398
        Me.sptReportTables.TabIndex = 0
        '
        'pnlSplitFields
        '
        Me.pnlSplitFields.Controls.Add(Me.btnReportFieldAdd)
        Me.pnlSplitFields.Controls.Add(Me.btnReportFieldRemove)
        Me.pnlSplitFields.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSplitFields.Location = New System.Drawing.Point(0, 0)
        Me.pnlSplitFields.Name = "pnlSplitFields"
        Me.pnlSplitFields.Size = New System.Drawing.Size(38, 398)
        Me.pnlSplitFields.TabIndex = 152
        '
        'btnReportFieldAdd
        '
        Me.btnReportFieldAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldAdd.Image = Global.Sequenchel.My.Resources.Resources.button_right
        Me.btnReportFieldAdd.Location = New System.Drawing.Point(3, 113)
        Me.btnReportFieldAdd.Name = "btnReportFieldAdd"
        Me.btnReportFieldAdd.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldAdd.TabIndex = 0
        Me.btnReportFieldAdd.UseVisualStyleBackColor = True
        '
        'btnReportFieldRemove
        '
        Me.btnReportFieldRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldRemove.Image = Global.Sequenchel.My.Resources.Resources.button_leftt
        Me.btnReportFieldRemove.Location = New System.Drawing.Point(3, 142)
        Me.btnReportFieldRemove.Name = "btnReportFieldRemove"
        Me.btnReportFieldRemove.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldRemove.TabIndex = 1
        Me.btnReportFieldRemove.UseVisualStyleBackColor = True
        '
        'lvwSelectedFields
        '
        Me.lvwSelectedFields.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwSelectedFields.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colSeltableName, Me.colSelFieldAlias})
        Me.lvwSelectedFields.FullRowSelect = True
        Me.lvwSelectedFields.Location = New System.Drawing.Point(38, 25)
        Me.lvwSelectedFields.Name = "lvwSelectedFields"
        Me.lvwSelectedFields.Size = New System.Drawing.Size(162, 372)
        Me.lvwSelectedFields.TabIndex = 0
        Me.lvwSelectedFields.UseCompatibleStateImageBehavior = False
        Me.lvwSelectedFields.View = System.Windows.Forms.View.Details
        '
        'colSeltableName
        '
        Me.colSeltableName.Text = "Table"
        Me.colSeltableName.Width = 50
        '
        'colSelFieldAlias
        '
        Me.colSelFieldAlias.Text = "Field"
        Me.colSelFieldAlias.Width = 50
        '
        'lblSelectedFields
        '
        Me.lblSelectedFields.AutoSize = True
        Me.lblSelectedFields.Location = New System.Drawing.Point(45, 5)
        Me.lblSelectedFields.Name = "lblSelectedFields"
        Me.lblSelectedFields.Size = New System.Drawing.Size(79, 13)
        Me.lblSelectedFields.TabIndex = 150
        Me.lblSelectedFields.Text = "Selected Fields"
        '
        'pnlSplitTables
        '
        Me.pnlSplitTables.Controls.Add(Me.lblSelectedTables)
        Me.pnlSplitTables.Location = New System.Drawing.Point(38, 0)
        Me.pnlSplitTables.Name = "pnlSplitTables"
        Me.pnlSplitTables.Size = New System.Drawing.Size(170, 23)
        Me.pnlSplitTables.TabIndex = 155
        '
        'lblSelectedTables
        '
        Me.lblSelectedTables.AutoSize = True
        Me.lblSelectedTables.Location = New System.Drawing.Point(7, 7)
        Me.lblSelectedTables.Name = "lblSelectedTables"
        Me.lblSelectedTables.Size = New System.Drawing.Size(84, 13)
        Me.lblSelectedTables.TabIndex = 153
        Me.lblSelectedTables.Text = "Selected Tables"
        '
        'pnlSplitFields2
        '
        Me.pnlSplitFields2.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSplitFields2.Location = New System.Drawing.Point(0, 0)
        Me.pnlSplitFields2.Name = "pnlSplitFields2"
        Me.pnlSplitFields2.Size = New System.Drawing.Size(38, 200)
        Me.pnlSplitFields2.TabIndex = 154
        '
        'lvwSelectedTables
        '
        Me.lvwSelectedTables.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwSelectedTables.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTableAlias, Me.colTableName})
        Me.lvwSelectedTables.FullRowSelect = True
        Me.lvwSelectedTables.Location = New System.Drawing.Point(38, 24)
        Me.lvwSelectedTables.Name = "lvwSelectedTables"
        Me.lvwSelectedTables.Size = New System.Drawing.Size(162, 175)
        Me.lvwSelectedTables.TabIndex = 0
        Me.lvwSelectedTables.UseCompatibleStateImageBehavior = False
        Me.lvwSelectedTables.View = System.Windows.Forms.View.Details
        '
        'colTableAlias
        '
        Me.colTableAlias.Text = "Table Alias"
        Me.colTableAlias.Width = 75
        '
        'colTableName
        '
        Me.colTableName.Text = "Table Name"
        Me.colTableName.Width = 75
        '
        'sptReport
        '
        Me.sptReport.BackColor = System.Drawing.SystemColors.Control
        Me.sptReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sptReport.Location = New System.Drawing.Point(0, 0)
        Me.sptReport.Name = "sptReport"
        Me.sptReport.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'sptReport.Panel1
        '
        Me.sptReport.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.sptReport.Panel1.Controls.Add(Me.pnlSelectedFieldsMain)
        Me.sptReport.Panel1.Controls.Add(Me.pnlSplitSelectedFields)
        Me.sptReport.Panel1MinSize = 200
        '
        'sptReport.Panel2
        '
        Me.sptReport.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.sptReport.Panel2.Controls.Add(Me.pnlRelationsMain)
        Me.sptReport.Panel2.Controls.Add(Me.pnlSplitSelectedTables)
        Me.sptReport.Panel2MinSize = 200
        Me.sptReport.Size = New System.Drawing.Size(862, 602)
        Me.sptReport.SplitterDistance = 398
        Me.sptReport.TabIndex = 181
        '
        'pnlSelectedFieldsMain
        '
        Me.pnlSelectedFieldsMain.AutoScroll = True
        Me.pnlSelectedFieldsMain.AutoScrollMargin = New System.Drawing.Size(5, 5)
        Me.pnlSelectedFieldsMain.BackgroundImage = Global.Sequenchel.My.Resources.Resources.Background_Sequenchel_reliefsquaretransparant30down
        Me.pnlSelectedFieldsMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnlSelectedFieldsMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSelectedFieldsMain.Controls.Add(Me.pnlSelectedFields)
        Me.pnlSelectedFieldsMain.Controls.Add(Me.pnlReportHeaders)
        Me.pnlSelectedFieldsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlSelectedFieldsMain.Location = New System.Drawing.Point(38, 0)
        Me.pnlSelectedFieldsMain.Name = "pnlSelectedFieldsMain"
        Me.pnlSelectedFieldsMain.Size = New System.Drawing.Size(824, 398)
        Me.pnlSelectedFieldsMain.TabIndex = 151
        '
        'pnlSelectedFields
        '
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportFilterText)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportFilterType)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportFilterMode)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportFilter)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportSortOrder)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportSort)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportShowMode)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportDisplay)
        Me.pnlSelectedFields.Controls.Add(Me.pnlReportLabel)
        Me.pnlSelectedFields.Location = New System.Drawing.Point(0, 24)
        Me.pnlSelectedFields.Name = "pnlSelectedFields"
        Me.pnlSelectedFields.Size = New System.Drawing.Size(800, 31)
        Me.pnlSelectedFields.TabIndex = 5
        '
        'pnlReportFilterText
        '
        Me.pnlReportFilterText.Location = New System.Drawing.Point(578, 0)
        Me.pnlReportFilterText.Name = "pnlReportFilterText"
        Me.pnlReportFilterText.Size = New System.Drawing.Size(222, 30)
        Me.pnlReportFilterText.TabIndex = 10
        '
        'pnlReportFilterType
        '
        Me.pnlReportFilterType.Location = New System.Drawing.Point(493, 0)
        Me.pnlReportFilterType.Name = "pnlReportFilterType"
        Me.pnlReportFilterType.Size = New System.Drawing.Size(85, 30)
        Me.pnlReportFilterType.TabIndex = 9
        '
        'pnlReportFilterMode
        '
        Me.pnlReportFilterMode.Location = New System.Drawing.Point(408, 0)
        Me.pnlReportFilterMode.Name = "pnlReportFilterMode"
        Me.pnlReportFilterMode.Size = New System.Drawing.Size(85, 30)
        Me.pnlReportFilterMode.TabIndex = 10
        '
        'pnlReportFilter
        '
        Me.pnlReportFilter.Location = New System.Drawing.Point(378, 0)
        Me.pnlReportFilter.Name = "pnlReportFilter"
        Me.pnlReportFilter.Size = New System.Drawing.Size(30, 30)
        Me.pnlReportFilter.TabIndex = 8
        '
        'pnlReportSortOrder
        '
        Me.pnlReportSortOrder.Location = New System.Drawing.Point(348, 0)
        Me.pnlReportSortOrder.Name = "pnlReportSortOrder"
        Me.pnlReportSortOrder.Size = New System.Drawing.Size(30, 30)
        Me.pnlReportSortOrder.TabIndex = 9
        '
        'pnlReportSort
        '
        Me.pnlReportSort.Location = New System.Drawing.Point(288, 0)
        Me.pnlReportSort.Name = "pnlReportSort"
        Me.pnlReportSort.Size = New System.Drawing.Size(60, 30)
        Me.pnlReportSort.TabIndex = 12
        '
        'pnlReportShowMode
        '
        Me.pnlReportShowMode.Location = New System.Drawing.Point(203, 0)
        Me.pnlReportShowMode.Name = "pnlReportShowMode"
        Me.pnlReportShowMode.Size = New System.Drawing.Size(85, 30)
        Me.pnlReportShowMode.TabIndex = 11
        '
        'pnlReportDisplay
        '
        Me.pnlReportDisplay.Location = New System.Drawing.Point(173, 0)
        Me.pnlReportDisplay.Name = "pnlReportDisplay"
        Me.pnlReportDisplay.Size = New System.Drawing.Size(30, 30)
        Me.pnlReportDisplay.TabIndex = 7
        '
        'pnlReportLabel
        '
        Me.pnlReportLabel.Location = New System.Drawing.Point(0, 0)
        Me.pnlReportLabel.Name = "pnlReportLabel"
        Me.pnlReportLabel.Size = New System.Drawing.Size(173, 30)
        Me.pnlReportLabel.TabIndex = 6
        '
        'pnlReportHeaders
        '
        Me.pnlReportHeaders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlReportHeaders.Controls.Add(Me.chkReportShow)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportSortOrder)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportSort)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportShowMode)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilterMode)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilterText)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilterType)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilter)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportSelectedFields)
        Me.pnlReportHeaders.Location = New System.Drawing.Point(0, 0)
        Me.pnlReportHeaders.Name = "pnlReportHeaders"
        Me.pnlReportHeaders.Size = New System.Drawing.Size(816, 24)
        Me.pnlReportHeaders.TabIndex = 154
        '
        'chkReportShow
        '
        Me.chkReportShow.AutoSize = True
        Me.chkReportShow.Location = New System.Drawing.Point(155, 6)
        Me.chkReportShow.Name = "chkReportShow"
        Me.chkReportShow.Size = New System.Drawing.Size(53, 17)
        Me.chkReportShow.TabIndex = 0
        Me.chkReportShow.Text = "Show"
        Me.chkReportShow.UseVisualStyleBackColor = True
        '
        'lblReportSortOrder
        '
        Me.lblReportSortOrder.AutoSize = True
        Me.lblReportSortOrder.Location = New System.Drawing.Point(349, 8)
        Me.lblReportSortOrder.Name = "lblReportSortOrder"
        Me.lblReportSortOrder.Size = New System.Drawing.Size(33, 13)
        Me.lblReportSortOrder.TabIndex = 125
        Me.lblReportSortOrder.Text = "Order"
        '
        'lblReportSort
        '
        Me.lblReportSort.AutoSize = True
        Me.lblReportSort.Location = New System.Drawing.Point(289, 8)
        Me.lblReportSort.Name = "lblReportSort"
        Me.lblReportSort.Size = New System.Drawing.Size(26, 13)
        Me.lblReportSort.TabIndex = 124
        Me.lblReportSort.Text = "Sort"
        '
        'lblReportShowMode
        '
        Me.lblReportShowMode.AutoSize = True
        Me.lblReportShowMode.Location = New System.Drawing.Point(204, 8)
        Me.lblReportShowMode.Name = "lblReportShowMode"
        Me.lblReportShowMode.Size = New System.Drawing.Size(64, 13)
        Me.lblReportShowMode.TabIndex = 123
        Me.lblReportShowMode.Text = "Show Mode"
        '
        'lblReportFilterMode
        '
        Me.lblReportFilterMode.AutoSize = True
        Me.lblReportFilterMode.Location = New System.Drawing.Point(409, 8)
        Me.lblReportFilterMode.Name = "lblReportFilterMode"
        Me.lblReportFilterMode.Size = New System.Drawing.Size(59, 13)
        Me.lblReportFilterMode.TabIndex = 122
        Me.lblReportFilterMode.Text = "Filter Mode"
        '
        'lblReportFilterText
        '
        Me.lblReportFilterText.AutoSize = True
        Me.lblReportFilterText.Location = New System.Drawing.Point(579, 9)
        Me.lblReportFilterText.Name = "lblReportFilterText"
        Me.lblReportFilterText.Size = New System.Drawing.Size(53, 13)
        Me.lblReportFilterText.TabIndex = 121
        Me.lblReportFilterText.Text = "Filter Text"
        '
        'lblReportFilterType
        '
        Me.lblReportFilterType.AutoSize = True
        Me.lblReportFilterType.Location = New System.Drawing.Point(494, 8)
        Me.lblReportFilterType.Name = "lblReportFilterType"
        Me.lblReportFilterType.Size = New System.Drawing.Size(56, 13)
        Me.lblReportFilterType.TabIndex = 120
        Me.lblReportFilterType.Text = "Filter Type"
        '
        'lblReportFilter
        '
        Me.lblReportFilter.AutoSize = True
        Me.lblReportFilter.Location = New System.Drawing.Point(379, 8)
        Me.lblReportFilter.Name = "lblReportFilter"
        Me.lblReportFilter.Size = New System.Drawing.Size(29, 13)
        Me.lblReportFilter.TabIndex = 119
        Me.lblReportFilter.Text = "Filter"
        '
        'lblReportSelectedFields
        '
        Me.lblReportSelectedFields.AutoSize = True
        Me.lblReportSelectedFields.Location = New System.Drawing.Point(3, 8)
        Me.lblReportSelectedFields.Name = "lblReportSelectedFields"
        Me.lblReportSelectedFields.Size = New System.Drawing.Size(79, 13)
        Me.lblReportSelectedFields.TabIndex = 117
        Me.lblReportSelectedFields.Text = "Selected Fields"
        '
        'pnlSplitSelectedFields
        '
        Me.pnlSplitSelectedFields.Controls.Add(Me.btnReportFieldDown)
        Me.pnlSplitSelectedFields.Controls.Add(Me.btnReportFieldUp)
        Me.pnlSplitSelectedFields.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSplitSelectedFields.Location = New System.Drawing.Point(0, 0)
        Me.pnlSplitSelectedFields.Name = "pnlSplitSelectedFields"
        Me.pnlSplitSelectedFields.Size = New System.Drawing.Size(38, 398)
        Me.pnlSplitSelectedFields.TabIndex = 0
        '
        'btnReportFieldDown
        '
        Me.btnReportFieldDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldDown.Image = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnReportFieldDown.Location = New System.Drawing.Point(3, 142)
        Me.btnReportFieldDown.Name = "btnReportFieldDown"
        Me.btnReportFieldDown.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldDown.TabIndex = 1
        Me.btnReportFieldDown.UseVisualStyleBackColor = True
        '
        'btnReportFieldUp
        '
        Me.btnReportFieldUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldUp.Image = Global.Sequenchel.My.Resources.Resources.button_up
        Me.btnReportFieldUp.Location = New System.Drawing.Point(3, 113)
        Me.btnReportFieldUp.Name = "btnReportFieldUp"
        Me.btnReportFieldUp.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldUp.TabIndex = 0
        Me.btnReportFieldUp.UseVisualStyleBackColor = True
        '
        'pnlRelationsMain
        '
        Me.pnlRelationsMain.AutoScroll = True
        Me.pnlRelationsMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRelationsMain.Controls.Add(Me.pnlRelations)
        Me.pnlRelationsMain.Controls.Add(Me.pnlRelationHeaders)
        Me.pnlRelationsMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlRelationsMain.Location = New System.Drawing.Point(38, 0)
        Me.pnlRelationsMain.Name = "pnlRelationsMain"
        Me.pnlRelationsMain.Size = New System.Drawing.Size(824, 200)
        Me.pnlRelationsMain.TabIndex = 156
        '
        'pnlRelations
        '
        Me.pnlRelations.Controls.Add(Me.pnlRelationsTargetTable)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsTargetField)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsJoinType)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsField)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsUse)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsLabel)
        Me.pnlRelations.Location = New System.Drawing.Point(0, 24)
        Me.pnlRelations.Name = "pnlRelations"
        Me.pnlRelations.Size = New System.Drawing.Size(800, 48)
        Me.pnlRelations.TabIndex = 7
        '
        'pnlRelationsTargetTable
        '
        Me.pnlRelationsTargetTable.Location = New System.Drawing.Point(353, 0)
        Me.pnlRelationsTargetTable.Name = "pnlRelationsTargetTable"
        Me.pnlRelationsTargetTable.Size = New System.Drawing.Size(175, 48)
        Me.pnlRelationsTargetTable.TabIndex = 10
        '
        'pnlRelationsTargetField
        '
        Me.pnlRelationsTargetField.Location = New System.Drawing.Point(528, 0)
        Me.pnlRelationsTargetField.Name = "pnlRelationsTargetField"
        Me.pnlRelationsTargetField.Size = New System.Drawing.Size(150, 48)
        Me.pnlRelationsTargetField.TabIndex = 11
        '
        'pnlRelationsJoinType
        '
        Me.pnlRelationsJoinType.Location = New System.Drawing.Point(678, 0)
        Me.pnlRelationsJoinType.Name = "pnlRelationsJoinType"
        Me.pnlRelationsJoinType.Size = New System.Drawing.Size(120, 48)
        Me.pnlRelationsJoinType.TabIndex = 9
        '
        'pnlRelationsField
        '
        Me.pnlRelationsField.Location = New System.Drawing.Point(203, 0)
        Me.pnlRelationsField.Name = "pnlRelationsField"
        Me.pnlRelationsField.Size = New System.Drawing.Size(150, 48)
        Me.pnlRelationsField.TabIndex = 10
        '
        'pnlRelationsUse
        '
        Me.pnlRelationsUse.Location = New System.Drawing.Point(173, 0)
        Me.pnlRelationsUse.Name = "pnlRelationsUse"
        Me.pnlRelationsUse.Size = New System.Drawing.Size(30, 48)
        Me.pnlRelationsUse.TabIndex = 9
        '
        'pnlRelationsLabel
        '
        Me.pnlRelationsLabel.Location = New System.Drawing.Point(0, 0)
        Me.pnlRelationsLabel.Name = "pnlRelationsLabel"
        Me.pnlRelationsLabel.Size = New System.Drawing.Size(173, 48)
        Me.pnlRelationsLabel.TabIndex = 8
        '
        'pnlRelationHeaders
        '
        Me.pnlRelationHeaders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationsTargetField)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationsTargetTable)
        Me.pnlRelationHeaders.Controls.Add(Me.lblSourceField)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationUse)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationsJoinType)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationsSelectedTable)
        Me.pnlRelationHeaders.Location = New System.Drawing.Point(0, 0)
        Me.pnlRelationHeaders.Name = "pnlRelationHeaders"
        Me.pnlRelationHeaders.Size = New System.Drawing.Size(843, 24)
        Me.pnlRelationHeaders.TabIndex = 155
        '
        'lblRelationsTargetField
        '
        Me.lblRelationsTargetField.AutoSize = True
        Me.lblRelationsTargetField.Location = New System.Drawing.Point(525, 8)
        Me.lblRelationsTargetField.Name = "lblRelationsTargetField"
        Me.lblRelationsTargetField.Size = New System.Drawing.Size(63, 13)
        Me.lblRelationsTargetField.TabIndex = 123
        Me.lblRelationsTargetField.Text = "Target Field"
        '
        'lblRelationsTargetTable
        '
        Me.lblRelationsTargetTable.AutoSize = True
        Me.lblRelationsTargetTable.Location = New System.Drawing.Point(350, 8)
        Me.lblRelationsTargetTable.Name = "lblRelationsTargetTable"
        Me.lblRelationsTargetTable.Size = New System.Drawing.Size(68, 13)
        Me.lblRelationsTargetTable.TabIndex = 122
        Me.lblRelationsTargetTable.Text = "Target Table"
        '
        'lblSourceField
        '
        Me.lblSourceField.AutoSize = True
        Me.lblSourceField.Location = New System.Drawing.Point(206, 8)
        Me.lblSourceField.Name = "lblSourceField"
        Me.lblSourceField.Size = New System.Drawing.Size(66, 13)
        Me.lblSourceField.TabIndex = 121
        Me.lblSourceField.Text = "Source Field"
        '
        'lblRelationUse
        '
        Me.lblRelationUse.AutoSize = True
        Me.lblRelationUse.Location = New System.Drawing.Point(174, 8)
        Me.lblRelationUse.Name = "lblRelationUse"
        Me.lblRelationUse.Size = New System.Drawing.Size(26, 13)
        Me.lblRelationUse.TabIndex = 120
        Me.lblRelationUse.Text = "Use"
        '
        'lblRelationsJoinType
        '
        Me.lblRelationsJoinType.AutoSize = True
        Me.lblRelationsJoinType.Location = New System.Drawing.Point(681, 8)
        Me.lblRelationsJoinType.Name = "lblRelationsJoinType"
        Me.lblRelationsJoinType.Size = New System.Drawing.Size(53, 13)
        Me.lblRelationsJoinType.TabIndex = 119
        Me.lblRelationsJoinType.Text = "Join Type"
        '
        'lblRelationsSelectedTable
        '
        Me.lblRelationsSelectedTable.AutoSize = True
        Me.lblRelationsSelectedTable.Location = New System.Drawing.Point(3, 8)
        Me.lblRelationsSelectedTable.Name = "lblRelationsSelectedTable"
        Me.lblRelationsSelectedTable.Size = New System.Drawing.Size(84, 13)
        Me.lblRelationsSelectedTable.TabIndex = 117
        Me.lblRelationsSelectedTable.Text = "Selected Tables"
        '
        'pnlSplitSelectedTables
        '
        Me.pnlSplitSelectedTables.Controls.Add(Me.btnReportTableUp)
        Me.pnlSplitSelectedTables.Controls.Add(Me.btnReportTableDown)
        Me.pnlSplitSelectedTables.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlSplitSelectedTables.Location = New System.Drawing.Point(0, 0)
        Me.pnlSplitSelectedTables.Name = "pnlSplitSelectedTables"
        Me.pnlSplitSelectedTables.Size = New System.Drawing.Size(38, 200)
        Me.pnlSplitSelectedTables.TabIndex = 155
        '
        'btnReportTableUp
        '
        Me.btnReportTableUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportTableUp.Image = Global.Sequenchel.My.Resources.Resources.button_up
        Me.btnReportTableUp.Location = New System.Drawing.Point(3, 70)
        Me.btnReportTableUp.Name = "btnReportTableUp"
        Me.btnReportTableUp.Size = New System.Drawing.Size(30, 23)
        Me.btnReportTableUp.TabIndex = 0
        Me.btnReportTableUp.UseVisualStyleBackColor = True
        '
        'btnReportTableDown
        '
        Me.btnReportTableDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportTableDown.Image = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnReportTableDown.Location = New System.Drawing.Point(3, 99)
        Me.btnReportTableDown.Name = "btnReportTableDown"
        Me.btnReportTableDown.Size = New System.Drawing.Size(30, 23)
        Me.btnReportTableDown.TabIndex = 1
        Me.btnReportTableDown.UseVisualStyleBackColor = True
        '
        'pnlReportButtons
        '
        Me.pnlReportButtons.Controls.Add(Me.btnQueryShow)
        Me.pnlReportButtons.Controls.Add(Me.btnReportClear)
        Me.pnlReportButtons.Controls.Add(Me.lblDescription)
        Me.pnlReportButtons.Controls.Add(Me.btnReportCreate)
        Me.pnlReportButtons.Controls.Add(Me.txtDescription)
        Me.pnlReportButtons.Controls.Add(Me.chkDistinct)
        Me.pnlReportButtons.Controls.Add(Me.btnReportExport)
        Me.pnlReportButtons.Controls.Add(Me.chkTop)
        Me.pnlReportButtons.Controls.Add(Me.btnReportImport)
        Me.pnlReportButtons.Controls.Add(Me.txtTop)
        Me.pnlReportButtons.Controls.Add(Me.btnRevertChanges)
        Me.pnlReportButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlReportButtons.Location = New System.Drawing.Point(3, 656)
        Me.pnlReportButtons.Name = "pnlReportButtons"
        Me.pnlReportButtons.Size = New System.Drawing.Size(1246, 58)
        Me.pnlReportButtons.TabIndex = 198
        '
        'btnQueryShow
        '
        Me.btnQueryShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQueryShow.Location = New System.Drawing.Point(903, 33)
        Me.btnQueryShow.Name = "btnQueryShow"
        Me.btnQueryShow.Size = New System.Drawing.Size(164, 20)
        Me.btnQueryShow.TabIndex = 7
        Me.btnQueryShow.Text = "Show Query"
        '
        'btnReportClear
        '
        Me.btnReportClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReportClear.Location = New System.Drawing.Point(1073, 33)
        Me.btnReportClear.Name = "btnReportClear"
        Me.btnReportClear.Size = New System.Drawing.Size(164, 20)
        Me.btnReportClear.TabIndex = 8
        Me.btnReportClear.Text = "Clear All"
        '
        'lblDescription
        '
        Me.lblDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(185, 7)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(95, 13)
        Me.lblDescription.TabIndex = 168
        Me.lblDescription.Text = "Report Description"
        '
        'btnReportCreate
        '
        Me.btnReportCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReportCreate.Location = New System.Drawing.Point(903, 7)
        Me.btnReportCreate.Name = "btnReportCreate"
        Me.btnReportCreate.Size = New System.Drawing.Size(164, 20)
        Me.btnReportCreate.TabIndex = 6
        Me.btnReportCreate.Text = "Create Report"
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(186, 21)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(541, 32)
        Me.txtDescription.TabIndex = 2
        '
        'chkDistinct
        '
        Me.chkDistinct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDistinct.AutoSize = True
        Me.chkDistinct.Location = New System.Drawing.Point(743, 10)
        Me.chkDistinct.Name = "chkDistinct"
        Me.chkDistinct.Size = New System.Drawing.Size(98, 17)
        Me.chkDistinct.TabIndex = 3
        Me.chkDistinct.Text = "Use DISTINCT"
        Me.chkDistinct.UseVisualStyleBackColor = True
        '
        'btnReportExport
        '
        Me.btnReportExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReportExport.Location = New System.Drawing.Point(6, 7)
        Me.btnReportExport.Name = "btnReportExport"
        Me.btnReportExport.Size = New System.Drawing.Size(164, 20)
        Me.btnReportExport.TabIndex = 0
        Me.btnReportExport.Text = "Export Report Definition"
        '
        'chkTop
        '
        Me.chkTop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTop.AutoSize = True
        Me.chkTop.Checked = True
        Me.chkTop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTop.Location = New System.Drawing.Point(743, 36)
        Me.chkTop.Name = "chkTop"
        Me.chkTop.Size = New System.Drawing.Size(70, 17)
        Me.chkTop.TabIndex = 4
        Me.chkTop.Text = "Use TOP"
        Me.chkTop.UseVisualStyleBackColor = True
        '
        'btnReportImport
        '
        Me.btnReportImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReportImport.Location = New System.Drawing.Point(6, 33)
        Me.btnReportImport.Name = "btnReportImport"
        Me.btnReportImport.Size = New System.Drawing.Size(164, 20)
        Me.btnReportImport.TabIndex = 1
        Me.btnReportImport.Text = "Import Report Definition"
        '
        'txtTop
        '
        Me.txtTop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTop.Location = New System.Drawing.Point(819, 34)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(39, 20)
        Me.txtTop.TabIndex = 5
        Me.txtTop.Text = "1000"
        '
        'btnRevertChanges
        '
        Me.btnRevertChanges.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRevertChanges.Location = New System.Drawing.Point(1073, 7)
        Me.btnRevertChanges.Name = "btnRevertChanges"
        Me.btnRevertChanges.Size = New System.Drawing.Size(164, 20)
        Me.btnRevertChanges.TabIndex = 9
        Me.btnRevertChanges.Text = "Revert Changes"
        '
        'pnlReportConnection
        '
        Me.pnlReportConnection.Controls.Add(Me.lblReport)
        Me.pnlReportConnection.Controls.Add(Me.cbxReportName)
        Me.pnlReportConnection.Controls.Add(Me.btnReportAddOrUpdate)
        Me.pnlReportConnection.Controls.Add(Me.btnTablesReload)
        Me.pnlReportConnection.Controls.Add(Me.btnReportDelete)
        Me.pnlReportConnection.Controls.Add(Me.btnTableSetsReload)
        Me.pnlReportConnection.Controls.Add(Me.lblConnection)
        Me.pnlReportConnection.Controls.Add(Me.btnConnectionsReload)
        Me.pnlReportConnection.Controls.Add(Me.cbxConnection)
        Me.pnlReportConnection.Controls.Add(Me.lblTable)
        Me.pnlReportConnection.Controls.Add(Me.cbxTableSet)
        Me.pnlReportConnection.Controls.Add(Me.cbxTable)
        Me.pnlReportConnection.Controls.Add(Me.lblTableSet)
        Me.pnlReportConnection.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlReportConnection.Location = New System.Drawing.Point(3, 3)
        Me.pnlReportConnection.Name = "pnlReportConnection"
        Me.pnlReportConnection.Size = New System.Drawing.Size(1246, 51)
        Me.pnlReportConnection.TabIndex = 197
        '
        'lblReport
        '
        Me.lblReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblReport.AutoSize = True
        Me.lblReport.Location = New System.Drawing.Point(766, 5)
        Me.lblReport.Name = "lblReport"
        Me.lblReport.Size = New System.Drawing.Size(39, 13)
        Me.lblReport.TabIndex = 171
        Me.lblReport.Text = "Report"
        '
        'cbxReportName
        '
        Me.cbxReportName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbxReportName.FormattingEnabled = True
        Me.cbxReportName.Location = New System.Drawing.Point(768, 21)
        Me.cbxReportName.Name = "cbxReportName"
        Me.cbxReportName.Size = New System.Drawing.Size(197, 21)
        Me.cbxReportName.Sorted = True
        Me.cbxReportName.TabIndex = 6
        '
        'btnReportAddOrUpdate
        '
        Me.btnReportAddOrUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReportAddOrUpdate.Location = New System.Drawing.Point(971, 20)
        Me.btnReportAddOrUpdate.Name = "btnReportAddOrUpdate"
        Me.btnReportAddOrUpdate.Size = New System.Drawing.Size(131, 20)
        Me.btnReportAddOrUpdate.TabIndex = 7
        Me.btnReportAddOrUpdate.Text = "Add/Update Report"
        '
        'btnTablesReload
        '
        Me.btnTablesReload.Enabled = False
        Me.btnTablesReload.Image = CType(resources.GetObject("btnTablesReload.Image"), System.Drawing.Image)
        Me.btnTablesReload.Location = New System.Drawing.Point(471, 17)
        Me.btnTablesReload.Name = "btnTablesReload"
        Me.btnTablesReload.Size = New System.Drawing.Size(25, 25)
        Me.btnTablesReload.TabIndex = 4
        Me.btnTablesReload.UseVisualStyleBackColor = True
        '
        'btnReportDelete
        '
        Me.btnReportDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReportDelete.Location = New System.Drawing.Point(1108, 20)
        Me.btnReportDelete.Name = "btnReportDelete"
        Me.btnReportDelete.Size = New System.Drawing.Size(131, 20)
        Me.btnReportDelete.TabIndex = 8
        Me.btnReportDelete.Text = "Delete Report"
        '
        'btnTableSetsReload
        '
        Me.btnTableSetsReload.Image = CType(resources.GetObject("btnTableSetsReload.Image"), System.Drawing.Image)
        Me.btnTableSetsReload.Location = New System.Drawing.Point(238, 17)
        Me.btnTableSetsReload.Name = "btnTableSetsReload"
        Me.btnTableSetsReload.Size = New System.Drawing.Size(25, 25)
        Me.btnTableSetsReload.TabIndex = 2
        Me.btnTableSetsReload.UseVisualStyleBackColor = True
        '
        'lblConnection
        '
        Me.lblConnection.AutoSize = True
        Me.lblConnection.Location = New System.Drawing.Point(32, 5)
        Me.lblConnection.Name = "lblConnection"
        Me.lblConnection.Size = New System.Drawing.Size(61, 13)
        Me.lblConnection.TabIndex = 173
        Me.lblConnection.Text = "Connection"
        '
        'btnConnectionsReload
        '
        Me.btnConnectionsReload.Image = CType(resources.GetObject("btnConnectionsReload.Image"), System.Drawing.Image)
        Me.btnConnectionsReload.Location = New System.Drawing.Point(5, 17)
        Me.btnConnectionsReload.Name = "btnConnectionsReload"
        Me.btnConnectionsReload.Size = New System.Drawing.Size(25, 25)
        Me.btnConnectionsReload.TabIndex = 0
        Me.btnConnectionsReload.UseVisualStyleBackColor = True
        '
        'cbxConnection
        '
        Me.cbxConnection.FormattingEnabled = True
        Me.cbxConnection.Location = New System.Drawing.Point(32, 21)
        Me.cbxConnection.Name = "cbxConnection"
        Me.cbxConnection.Size = New System.Drawing.Size(200, 21)
        Me.cbxConnection.Sorted = True
        Me.cbxConnection.TabIndex = 1
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(499, 5)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(34, 13)
        Me.lblTable.TabIndex = 177
        Me.lblTable.Text = "Table"
        '
        'cbxTableSet
        '
        Me.cbxTableSet.FormattingEnabled = True
        Me.cbxTableSet.Location = New System.Drawing.Point(265, 21)
        Me.cbxTableSet.Name = "cbxTableSet"
        Me.cbxTableSet.Size = New System.Drawing.Size(200, 21)
        Me.cbxTableSet.Sorted = True
        Me.cbxTableSet.TabIndex = 3
        '
        'cbxTable
        '
        Me.cbxTable.Enabled = False
        Me.cbxTable.FormattingEnabled = True
        Me.cbxTable.Location = New System.Drawing.Point(498, 21)
        Me.cbxTable.Name = "cbxTable"
        Me.cbxTable.Size = New System.Drawing.Size(200, 21)
        Me.cbxTable.Sorted = True
        Me.cbxTable.TabIndex = 5
        '
        'lblTableSet
        '
        Me.lblTableSet.AutoSize = True
        Me.lblTableSet.Location = New System.Drawing.Point(263, 5)
        Me.lblTableSet.Name = "lblTableSet"
        Me.lblTableSet.Size = New System.Drawing.Size(50, 13)
        Me.lblTableSet.TabIndex = 175
        Me.lblTableSet.Text = "TableSet"
        '
        'tpgReportResult
        '
        Me.tpgReportResult.Controls.Add(Me.cbxEmailResults)
        Me.tpgReportResult.Controls.Add(Me.btnEmailResults)
        Me.tpgReportResult.Controls.Add(Me.lblElapsedTime)
        Me.tpgReportResult.Controls.Add(Me.btnDefinition)
        Me.tpgReportResult.Controls.Add(Me.btnExportToFile)
        Me.tpgReportResult.Controls.Add(Me.lblErrorMessage)
        Me.tpgReportResult.Controls.Add(Me.lblListCount)
        Me.tpgReportResult.Controls.Add(Me.lblListCountNumber)
        Me.tpgReportResult.Controls.Add(Me.btnLoadQuery)
        Me.tpgReportResult.Controls.Add(Me.btnSaveQuery)
        Me.tpgReportResult.Controls.Add(Me.btnExecuteQuery)
        Me.tpgReportResult.Controls.Add(Me.rtbQuery)
        Me.tpgReportResult.Controls.Add(Me.dgvReport)
        Me.tpgReportResult.Location = New System.Drawing.Point(4, 22)
        Me.tpgReportResult.Name = "tpgReportResult"
        Me.tpgReportResult.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgReportResult.Size = New System.Drawing.Size(1252, 717)
        Me.tpgReportResult.TabIndex = 1
        Me.tpgReportResult.Text = "Report Result"
        Me.tpgReportResult.UseVisualStyleBackColor = True
        '
        'cbxEmailResults
        '
        Me.cbxEmailResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbxEmailResults.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxEmailResults.FormattingEnabled = True
        Me.cbxEmailResults.Items.AddRange(New Object() {"HTML", "Excel", "XML", "CSV"})
        Me.cbxEmailResults.Location = New System.Drawing.Point(738, 691)
        Me.cbxEmailResults.Name = "cbxEmailResults"
        Me.cbxEmailResults.Size = New System.Drawing.Size(60, 21)
        Me.cbxEmailResults.TabIndex = 6
        '
        'btnEmailResults
        '
        Me.btnEmailResults.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEmailResults.Location = New System.Drawing.Point(602, 690)
        Me.btnEmailResults.Name = "btnEmailResults"
        Me.btnEmailResults.Size = New System.Drawing.Size(135, 23)
        Me.btnEmailResults.TabIndex = 5
        Me.btnEmailResults.Text = "Email Results as:"
        Me.btnEmailResults.UseVisualStyleBackColor = True
        '
        'lblElapsedTime
        '
        Me.lblElapsedTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblElapsedTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblElapsedTime.Location = New System.Drawing.Point(1173, 559)
        Me.lblElapsedTime.Name = "lblElapsedTime"
        Me.lblElapsedTime.Size = New System.Drawing.Size(73, 22)
        Me.lblElapsedTime.TabIndex = 178
        '
        'btnDefinition
        '
        Me.btnDefinition.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefinition.Location = New System.Drawing.Point(1105, 690)
        Me.btnDefinition.Name = "btnDefinition"
        Me.btnDefinition.Size = New System.Drawing.Size(135, 23)
        Me.btnDefinition.TabIndex = 7
        Me.btnDefinition.Text = "Back to Definition"
        Me.btnDefinition.UseVisualStyleBackColor = True
        '
        'btnExportToFile
        '
        Me.btnExportToFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportToFile.Location = New System.Drawing.Point(442, 690)
        Me.btnExportToFile.Name = "btnExportToFile"
        Me.btnExportToFile.Size = New System.Drawing.Size(135, 23)
        Me.btnExportToFile.TabIndex = 4
        Me.btnExportToFile.Text = "Export Results to File"
        Me.btnExportToFile.UseVisualStyleBackColor = True
        '
        'lblErrorMessage
        '
        Me.lblErrorMessage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblErrorMessage.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorMessage.Location = New System.Drawing.Point(161, 560)
        Me.lblErrorMessage.Name = "lblErrorMessage"
        Me.lblErrorMessage.Size = New System.Drawing.Size(1002, 22)
        Me.lblErrorMessage.TabIndex = 175
        '
        'lblListCount
        '
        Me.lblListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblListCount.AutoSize = True
        Me.lblListCount.Location = New System.Drawing.Point(6, 560)
        Me.lblListCount.Name = "lblListCount"
        Me.lblListCount.Size = New System.Drawing.Size(57, 13)
        Me.lblListCount.TabIndex = 173
        Me.lblListCount.Text = "List Count:"
        '
        'lblListCountNumber
        '
        Me.lblListCountNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblListCountNumber.Location = New System.Drawing.Point(83, 560)
        Me.lblListCountNumber.Name = "lblListCountNumber"
        Me.lblListCountNumber.Size = New System.Drawing.Size(54, 13)
        Me.lblListCountNumber.TabIndex = 174
        Me.lblListCountNumber.Text = "0"
        '
        'btnLoadQuery
        '
        Me.btnLoadQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoadQuery.Location = New System.Drawing.Point(301, 690)
        Me.btnLoadQuery.Name = "btnLoadQuery"
        Me.btnLoadQuery.Size = New System.Drawing.Size(135, 23)
        Me.btnLoadQuery.TabIndex = 3
        Me.btnLoadQuery.Text = "Load Query from File"
        Me.btnLoadQuery.UseVisualStyleBackColor = True
        '
        'btnSaveQuery
        '
        Me.btnSaveQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveQuery.Location = New System.Drawing.Point(160, 690)
        Me.btnSaveQuery.Name = "btnSaveQuery"
        Me.btnSaveQuery.Size = New System.Drawing.Size(135, 23)
        Me.btnSaveQuery.TabIndex = 2
        Me.btnSaveQuery.Text = "Save Query to File"
        Me.btnSaveQuery.UseVisualStyleBackColor = True
        '
        'btnExecuteQuery
        '
        Me.btnExecuteQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExecuteQuery.Location = New System.Drawing.Point(19, 690)
        Me.btnExecuteQuery.Name = "btnExecuteQuery"
        Me.btnExecuteQuery.Size = New System.Drawing.Size(135, 23)
        Me.btnExecuteQuery.TabIndex = 1
        Me.btnExecuteQuery.Text = "Execute Query"
        Me.btnExecuteQuery.UseVisualStyleBackColor = True
        '
        'rtbQuery
        '
        Me.rtbQuery.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtbQuery.Location = New System.Drawing.Point(6, 585)
        Me.rtbQuery.Name = "rtbQuery"
        Me.rtbQuery.Size = New System.Drawing.Size(1240, 100)
        Me.rtbQuery.TabIndex = 0
        Me.rtbQuery.Text = ""
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.AllowUserToOrderColumns = True
        Me.dgvReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvReport.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.dgvReport.BackImage = CType(resources.GetObject("dgvReport.BackImage"), System.Drawing.Image)
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Location = New System.Drawing.Point(6, 6)
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.Size = New System.Drawing.Size(1243, 549)
        Me.dgvReport.TabIndex = 0
        '
        'btnTest
        '
        Me.btnTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTest.Location = New System.Drawing.Point(734, 3)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(135, 23)
        Me.btnTest.TabIndex = 170
        Me.btnTest.TabStop = False
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        Me.btnTest.Visible = False
        '
        'tmrElapsedTime
        '
        Me.tmrElapsedTime.Interval = 1000
        '
        'lblStatusTitle
        '
        Me.lblStatusTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatusTitle.AutoSize = True
        Me.lblStatusTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusTitle.Location = New System.Drawing.Point(9, 781)
        Me.lblStatusTitle.Name = "lblStatusTitle"
        Me.lblStatusTitle.Size = New System.Drawing.Size(51, 16)
        Me.lblStatusTitle.TabIndex = 172
        Me.lblStatusTitle.Text = "Status"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportsFile, Me.mnuReportsHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1284, 24)
        Me.MenuStrip1.TabIndex = 174
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuReportsFile
        '
        Me.mnuReportsFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportsFileClose})
        Me.mnuReportsFile.Name = "mnuReportsFile"
        Me.mnuReportsFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuReportsFile.Text = "&File"
        '
        'mnuReportsFileClose
        '
        Me.mnuReportsFileClose.Name = "mnuReportsFileClose"
        Me.mnuReportsFileClose.Size = New System.Drawing.Size(146, 22)
        Me.mnuReportsFileClose.Text = "&Close Reports"
        '
        'mnuReportsHelp
        '
        Me.mnuReportsHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportsHelpManual})
        Me.mnuReportsHelp.Name = "mnuReportsHelp"
        Me.mnuReportsHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuReportsHelp.Text = "&Help"
        '
        'mnuReportsHelpManual
        '
        Me.mnuReportsHelpManual.Name = "mnuReportsHelpManual"
        Me.mnuReportsHelpManual.Size = New System.Drawing.Size(157, 22)
        Me.mnuReportsHelpManual.Text = "Reports &Manual"
        '
        'lblStatusText
        '
        Me.lblStatusText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblStatusText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusText.Location = New System.Drawing.Point(70, 782)
        Me.lblStatusText.Multiline = True
        Me.lblStatusText.Name = "lblStatusText"
        Me.lblStatusText.ReadOnly = True
        Me.lblStatusText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.lblStatusText.Size = New System.Drawing.Size(1202, 20)
        Me.lblStatusText.TabIndex = 175
        '
        'frmReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 812)
        Me.Controls.Add(Me.lblStatusText)
        Me.Controls.Add(Me.lblStatusTitle)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.lblLicense)
        Me.Controls.Add(Me.tabReports)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(1300, 800)
        Me.Name = "frmReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reports"
        Me.tabReports.ResumeLayout(False)
        Me.tpgReportDefinition.ResumeLayout(False)
        Me.sptReports.Panel1.ResumeLayout(False)
        Me.sptReports.Panel1.PerformLayout()
        Me.sptReports.Panel2.ResumeLayout(False)
        CType(Me.sptReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptReports.ResumeLayout(False)
        Me.sptReportFields.Panel1.ResumeLayout(False)
        Me.sptReportFields.Panel2.ResumeLayout(False)
        CType(Me.sptReportFields, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptReportFields.ResumeLayout(False)
        Me.sptReportTables.Panel1.ResumeLayout(False)
        Me.sptReportTables.Panel1.PerformLayout()
        Me.sptReportTables.Panel2.ResumeLayout(False)
        CType(Me.sptReportTables, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptReportTables.ResumeLayout(False)
        Me.pnlSplitFields.ResumeLayout(False)
        Me.pnlSplitTables.ResumeLayout(False)
        Me.pnlSplitTables.PerformLayout()
        Me.sptReport.Panel1.ResumeLayout(False)
        Me.sptReport.Panel2.ResumeLayout(False)
        CType(Me.sptReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptReport.ResumeLayout(False)
        Me.pnlSelectedFieldsMain.ResumeLayout(False)
        Me.pnlSelectedFields.ResumeLayout(False)
        Me.pnlReportHeaders.ResumeLayout(False)
        Me.pnlReportHeaders.PerformLayout()
        Me.pnlSplitSelectedFields.ResumeLayout(False)
        Me.pnlRelationsMain.ResumeLayout(False)
        Me.pnlRelations.ResumeLayout(False)
        Me.pnlRelationHeaders.ResumeLayout(False)
        Me.pnlRelationHeaders.PerformLayout()
        Me.pnlSplitSelectedTables.ResumeLayout(False)
        Me.pnlReportButtons.ResumeLayout(False)
        Me.pnlReportButtons.PerformLayout()
        Me.pnlReportConnection.ResumeLayout(False)
        Me.pnlReportConnection.PerformLayout()
        Me.tpgReportResult.ResumeLayout(False)
        Me.tpgReportResult.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLicense As System.Windows.Forms.Label
    Friend WithEvents tabReports As System.Windows.Forms.TabControl
    Friend WithEvents tpgReportDefinition As System.Windows.Forms.TabPage
    Friend WithEvents txtTop As System.Windows.Forms.TextBox
    Friend WithEvents chkTop As System.Windows.Forms.CheckBox
    Friend WithEvents chkDistinct As System.Windows.Forms.CheckBox
    Friend WithEvents btnReportCreate As System.Windows.Forms.Button
    Friend WithEvents btnReportDelete As System.Windows.Forms.Button
    Friend WithEvents btnReportClear As System.Windows.Forms.Button
    Friend WithEvents btnReportAddOrUpdate As System.Windows.Forms.Button
    Friend WithEvents btnTablesReload As System.Windows.Forms.Button
    Friend WithEvents btnTableSetsReload As System.Windows.Forms.Button
    Friend WithEvents btnConnectionsReload As System.Windows.Forms.Button
    Friend WithEvents lblTable As System.Windows.Forms.Label
    Friend WithEvents cbxTable As System.Windows.Forms.ComboBox
    Friend WithEvents lblTableSet As System.Windows.Forms.Label
    Friend WithEvents cbxTableSet As System.Windows.Forms.ComboBox
    Friend WithEvents lblConnection As System.Windows.Forms.Label
    Friend WithEvents cbxConnection As System.Windows.Forms.ComboBox
    Friend WithEvents lblReport As System.Windows.Forms.Label
    Friend WithEvents cbxReportName As System.Windows.Forms.ComboBox
    Friend WithEvents btnReportFieldRemove As System.Windows.Forms.Button
    Friend WithEvents btnReportFieldAdd As System.Windows.Forms.Button
    Friend WithEvents lblAvailableFields As System.Windows.Forms.Label
    Friend WithEvents sptReport As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlReportHeaders As System.Windows.Forms.Panel
    Friend WithEvents lblReportSortOrder As System.Windows.Forms.Label
    Friend WithEvents lblReportSort As System.Windows.Forms.Label
    Friend WithEvents lblReportShowMode As System.Windows.Forms.Label
    Friend WithEvents lblReportFilterMode As System.Windows.Forms.Label
    Friend WithEvents lblReportFilterText As System.Windows.Forms.Label
    Friend WithEvents lblReportFilterType As System.Windows.Forms.Label
    Friend WithEvents lblReportFilter As System.Windows.Forms.Label
    Friend WithEvents lblReportSelectedFields As System.Windows.Forms.Label
    Friend WithEvents btnReportFieldDown As System.Windows.Forms.Button
    Friend WithEvents btnReportFieldUp As System.Windows.Forms.Button
    Friend WithEvents pnlSelectedFieldsMain As System.Windows.Forms.Panel
    Friend WithEvents pnlSelectedFields As System.Windows.Forms.Panel
    Friend WithEvents pnlReportSortOrder As System.Windows.Forms.Panel
    Friend WithEvents pnlReportSort As System.Windows.Forms.Panel
    Friend WithEvents pnlReportShowMode As System.Windows.Forms.Panel
    Friend WithEvents pnlReportFilterMode As System.Windows.Forms.Panel
    Friend WithEvents pnlReportFilterText As System.Windows.Forms.Panel
    Friend WithEvents pnlReportFilterType As System.Windows.Forms.Panel
    Friend WithEvents pnlReportFilter As System.Windows.Forms.Panel
    Friend WithEvents pnlReportDisplay As System.Windows.Forms.Panel
    Friend WithEvents pnlReportLabel As System.Windows.Forms.Panel
    Friend WithEvents lblSelectedFields As System.Windows.Forms.Label
    Friend WithEvents pnlRelationsMain As System.Windows.Forms.Panel
    Friend WithEvents pnlRelations As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationsField As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationsJoinType As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationsUse As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationsLabel As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationHeaders As System.Windows.Forms.Panel
    Friend WithEvents lblSourceField As System.Windows.Forms.Label
    Friend WithEvents lblRelationUse As System.Windows.Forms.Label
    Friend WithEvents lblRelationsJoinType As System.Windows.Forms.Label
    Friend WithEvents lblRelationsSelectedTable As System.Windows.Forms.Label
    Friend WithEvents lblSelectedTables As System.Windows.Forms.Label
    Friend WithEvents tpgReportResult As System.Windows.Forms.TabPage
    Friend WithEvents rtbQuery As System.Windows.Forms.RichTextBox
    Friend WithEvents btnExecuteQuery As System.Windows.Forms.Button
    Friend WithEvents btnReportTableDown As System.Windows.Forms.Button
    Friend WithEvents btnReportTableUp As System.Windows.Forms.Button
    Friend WithEvents btnLoadQuery As System.Windows.Forms.Button
    Friend WithEvents btnSaveQuery As System.Windows.Forms.Button
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents lblListCount As System.Windows.Forms.Label
    Friend WithEvents lblListCountNumber As System.Windows.Forms.Label
    Friend WithEvents btnQueryShow As System.Windows.Forms.Button
    Friend WithEvents lblErrorMessage As System.Windows.Forms.Label
    Friend WithEvents btnExportToFile As System.Windows.Forms.Button
    Friend WithEvents btnDefinition As System.Windows.Forms.Button
    Friend WithEvents btnRevertChanges As System.Windows.Forms.Button
    Friend WithEvents lvwAvailableFields As System.Windows.Forms.ListView
    Friend WithEvents colAvFieldAlias As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAvTableName As System.Windows.Forms.ColumnHeader
    Friend WithEvents sptReports As System.Windows.Forms.SplitContainer
    Friend WithEvents lvwSelectedFields As System.Windows.Forms.ListView
    Friend WithEvents colSeltableName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colSelFieldAlias As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwSelectedTables As System.Windows.Forms.ListView
    Friend WithEvents colTableAlias As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblElapsedTime As System.Windows.Forms.Label
    Friend WithEvents tmrElapsedTime As System.Windows.Forms.Timer
    Friend WithEvents dgvReport As Sequenchel.usrDataGridView
    Friend WithEvents btnReportExport As System.Windows.Forms.Button
    Friend WithEvents btnReportImport As System.Windows.Forms.Button
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents pnlReportConnection As System.Windows.Forms.Panel
    Friend WithEvents chkReportShow As System.Windows.Forms.CheckBox
    Friend WithEvents cbxEmailResults As System.Windows.Forms.ComboBox
    Friend WithEvents btnEmailResults As System.Windows.Forms.Button
    Friend WithEvents pnlSplitFields As System.Windows.Forms.Panel
    Friend WithEvents sptReportFields As System.Windows.Forms.SplitContainer
    Friend WithEvents sptReportTables As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlSplitFields2 As System.Windows.Forms.Panel
    Friend WithEvents pnlSplitSelectedFields As System.Windows.Forms.Panel
    Friend WithEvents pnlSplitSelectedTables As System.Windows.Forms.Panel
    Friend WithEvents pnlSplitTables As System.Windows.Forms.Panel
    Friend WithEvents pnlReportButtons As System.Windows.Forms.Panel
    Friend WithEvents lblStatusTitle As System.Windows.Forms.Label
    Friend WithEvents pnlRelationsTargetField As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationsTargetTable As System.Windows.Forms.Panel
    Friend WithEvents lblRelationsTargetField As System.Windows.Forms.Label
    Friend WithEvents lblRelationsTargetTable As System.Windows.Forms.Label
    Friend WithEvents colTableName As System.Windows.Forms.ColumnHeader
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuReportsFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsFileClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportsHelpManual As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblStatusText As System.Windows.Forms.TextBox
End Class
