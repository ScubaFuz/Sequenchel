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
        Me.pnlReportName = New System.Windows.Forms.Panel()
        Me.lblReport = New System.Windows.Forms.Label()
        Me.cbxReportName = New System.Windows.Forms.ComboBox()
        Me.btnReportAddOrUpdate = New System.Windows.Forms.Button()
        Me.btnReportDelete = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.btnReportExport = New System.Windows.Forms.Button()
        Me.btnReportImport = New System.Windows.Forms.Button()
        Me.sptReports = New System.Windows.Forms.SplitContainer()
        Me.lvwAvailableFields = New System.Windows.Forms.ListView()
        Me.colAvTableName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAvFieldAlias = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblAvailableFields = New System.Windows.Forms.Label()
        Me.sptReport = New System.Windows.Forms.SplitContainer()
        Me.lvwSelectedFields = New System.Windows.Forms.ListView()
        Me.colSeltableName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colSelFieldAlias = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pnlReportHeaders = New System.Windows.Forms.Panel()
        Me.lblReportSortOrder = New System.Windows.Forms.Label()
        Me.lblReportSort = New System.Windows.Forms.Label()
        Me.lblReportShowMode = New System.Windows.Forms.Label()
        Me.lblReportFilterMode = New System.Windows.Forms.Label()
        Me.lblReportFilterText = New System.Windows.Forms.Label()
        Me.lblReportFilterType = New System.Windows.Forms.Label()
        Me.lblReportFilter = New System.Windows.Forms.Label()
        Me.lblReportShow = New System.Windows.Forms.Label()
        Me.lblReportSelectedFields = New System.Windows.Forms.Label()
        Me.btnReportFieldRemove = New System.Windows.Forms.Button()
        Me.btnReportFieldAdd = New System.Windows.Forms.Button()
        Me.btnReportFieldDown = New System.Windows.Forms.Button()
        Me.btnReportFieldUp = New System.Windows.Forms.Button()
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
        Me.lblSelectedFields = New System.Windows.Forms.Label()
        Me.lvwSelectedTables = New System.Windows.Forms.ListView()
        Me.colTable = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnReportTableDown = New System.Windows.Forms.Button()
        Me.pnlRelationsMain = New System.Windows.Forms.Panel()
        Me.pnlRelations = New System.Windows.Forms.Panel()
        Me.pnlRelationsField = New System.Windows.Forms.Panel()
        Me.pnlRelationsJoinType = New System.Windows.Forms.Panel()
        Me.pnlRelationsRelation = New System.Windows.Forms.Panel()
        Me.pnlRelationsUse = New System.Windows.Forms.Panel()
        Me.pnlRelationsLabel = New System.Windows.Forms.Panel()
        Me.btnReportTableUp = New System.Windows.Forms.Button()
        Me.pnlRelationHeaders = New System.Windows.Forms.Panel()
        Me.lblSourceField = New System.Windows.Forms.Label()
        Me.lblRelationUse = New System.Windows.Forms.Label()
        Me.lblRelationsJoinType = New System.Windows.Forms.Label()
        Me.lblRelationsRelation = New System.Windows.Forms.Label()
        Me.lblRelationsSelectedTable = New System.Windows.Forms.Label()
        Me.lblSelectedTables = New System.Windows.Forms.Label()
        Me.btnRevertChanges = New System.Windows.Forms.Button()
        Me.btnQueryShow = New System.Windows.Forms.Button()
        Me.txtTop = New System.Windows.Forms.TextBox()
        Me.chkTop = New System.Windows.Forms.CheckBox()
        Me.chkDistinct = New System.Windows.Forms.CheckBox()
        Me.btnReportCreate = New System.Windows.Forms.Button()
        Me.btnReportClear = New System.Windows.Forms.Button()
        Me.btnTablesReload = New System.Windows.Forms.Button()
        Me.btnTableSetsReload = New System.Windows.Forms.Button()
        Me.btnConnectionsReload = New System.Windows.Forms.Button()
        Me.lblTable = New System.Windows.Forms.Label()
        Me.cbxTable = New System.Windows.Forms.ComboBox()
        Me.lblTableSet = New System.Windows.Forms.Label()
        Me.cbxTableSet = New System.Windows.Forms.ComboBox()
        Me.lblConnection = New System.Windows.Forms.Label()
        Me.cbxConnection = New System.Windows.Forms.ComboBox()
        Me.tpgReportResult = New System.Windows.Forms.TabPage()
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
        Me.btnClose = New System.Windows.Forms.Button()
        Me.tmrElapsedTime = New System.Windows.Forms.Timer(Me.components)
        Me.tabReports.SuspendLayout()
        Me.tpgReportDefinition.SuspendLayout()
        Me.pnlReportName.SuspendLayout()
        CType(Me.sptReports, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptReports.Panel1.SuspendLayout()
        Me.sptReports.Panel2.SuspendLayout()
        Me.sptReports.SuspendLayout()
        CType(Me.sptReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptReport.Panel1.SuspendLayout()
        Me.sptReport.Panel2.SuspendLayout()
        Me.sptReport.SuspendLayout()
        Me.pnlReportHeaders.SuspendLayout()
        Me.pnlSelectedFieldsMain.SuspendLayout()
        Me.pnlSelectedFields.SuspendLayout()
        Me.pnlRelationsMain.SuspendLayout()
        Me.pnlRelations.SuspendLayout()
        Me.pnlRelationHeaders.SuspendLayout()
        Me.tpgReportResult.SuspendLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tabReports.Location = New System.Drawing.Point(12, 9)
        Me.tabReports.Name = "tabReports"
        Me.tabReports.SelectedIndex = 0
        Me.tabReports.Size = New System.Drawing.Size(1260, 763)
        Me.tabReports.TabIndex = 0
        '
        'tpgReportDefinition
        '
        Me.tpgReportDefinition.Controls.Add(Me.pnlReportName)
        Me.tpgReportDefinition.Controls.Add(Me.lblDescription)
        Me.tpgReportDefinition.Controls.Add(Me.txtDescription)
        Me.tpgReportDefinition.Controls.Add(Me.btnReportExport)
        Me.tpgReportDefinition.Controls.Add(Me.btnReportImport)
        Me.tpgReportDefinition.Controls.Add(Me.sptReports)
        Me.tpgReportDefinition.Controls.Add(Me.btnRevertChanges)
        Me.tpgReportDefinition.Controls.Add(Me.btnQueryShow)
        Me.tpgReportDefinition.Controls.Add(Me.txtTop)
        Me.tpgReportDefinition.Controls.Add(Me.chkTop)
        Me.tpgReportDefinition.Controls.Add(Me.chkDistinct)
        Me.tpgReportDefinition.Controls.Add(Me.btnReportCreate)
        Me.tpgReportDefinition.Controls.Add(Me.btnReportClear)
        Me.tpgReportDefinition.Controls.Add(Me.btnTablesReload)
        Me.tpgReportDefinition.Controls.Add(Me.btnTableSetsReload)
        Me.tpgReportDefinition.Controls.Add(Me.btnConnectionsReload)
        Me.tpgReportDefinition.Controls.Add(Me.lblTable)
        Me.tpgReportDefinition.Controls.Add(Me.cbxTable)
        Me.tpgReportDefinition.Controls.Add(Me.lblTableSet)
        Me.tpgReportDefinition.Controls.Add(Me.cbxTableSet)
        Me.tpgReportDefinition.Controls.Add(Me.lblConnection)
        Me.tpgReportDefinition.Controls.Add(Me.cbxConnection)
        Me.tpgReportDefinition.Location = New System.Drawing.Point(4, 22)
        Me.tpgReportDefinition.Name = "tpgReportDefinition"
        Me.tpgReportDefinition.Padding = New System.Windows.Forms.Padding(3)
        Me.tpgReportDefinition.Size = New System.Drawing.Size(1252, 737)
        Me.tpgReportDefinition.TabIndex = 0
        Me.tpgReportDefinition.Text = "Report Definition"
        Me.tpgReportDefinition.UseVisualStyleBackColor = True
        '
        'pnlReportName
        '
        Me.pnlReportName.Controls.Add(Me.lblReport)
        Me.pnlReportName.Controls.Add(Me.cbxReportName)
        Me.pnlReportName.Controls.Add(Me.btnReportAddOrUpdate)
        Me.pnlReportName.Controls.Add(Me.btnReportDelete)
        Me.pnlReportName.Location = New System.Drawing.Point(760, 10)
        Me.pnlReportName.Name = "pnlReportName"
        Me.pnlReportName.Size = New System.Drawing.Size(486, 45)
        Me.pnlReportName.TabIndex = 197
        '
        'lblReport
        '
        Me.lblReport.AutoSize = True
        Me.lblReport.Location = New System.Drawing.Point(7, 0)
        Me.lblReport.Name = "lblReport"
        Me.lblReport.Size = New System.Drawing.Size(39, 13)
        Me.lblReport.TabIndex = 171
        Me.lblReport.Text = "Report"
        '
        'cbxReportName
        '
        Me.cbxReportName.FormattingEnabled = True
        Me.cbxReportName.Location = New System.Drawing.Point(8, 16)
        Me.cbxReportName.Name = "cbxReportName"
        Me.cbxReportName.Size = New System.Drawing.Size(197, 21)
        Me.cbxReportName.Sorted = True
        Me.cbxReportName.TabIndex = 3
        '
        'btnReportAddOrUpdate
        '
        Me.btnReportAddOrUpdate.Location = New System.Drawing.Point(211, 15)
        Me.btnReportAddOrUpdate.Name = "btnReportAddOrUpdate"
        Me.btnReportAddOrUpdate.Size = New System.Drawing.Size(131, 20)
        Me.btnReportAddOrUpdate.TabIndex = 4
        Me.btnReportAddOrUpdate.Text = "Add/Update Report"
        '
        'btnReportDelete
        '
        Me.btnReportDelete.Location = New System.Drawing.Point(348, 15)
        Me.btnReportDelete.Name = "btnReportDelete"
        Me.btnReportDelete.Size = New System.Drawing.Size(131, 20)
        Me.btnReportDelete.TabIndex = 5
        Me.btnReportDelete.Text = "Delete Report"
        '
        'lblDescription
        '
        Me.lblDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(186, 664)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(95, 13)
        Me.lblDescription.TabIndex = 168
        Me.lblDescription.Text = "Report Description"
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Location = New System.Drawing.Point(189, 677)
        Me.txtDescription.Multiline = True
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(541, 46)
        Me.txtDescription.TabIndex = 196
        '
        'btnReportExport
        '
        Me.btnReportExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReportExport.Location = New System.Drawing.Point(9, 677)
        Me.btnReportExport.Name = "btnReportExport"
        Me.btnReportExport.Size = New System.Drawing.Size(164, 20)
        Me.btnReportExport.TabIndex = 194
        Me.btnReportExport.Text = "Export Report Definition"
        '
        'btnReportImport
        '
        Me.btnReportImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnReportImport.Location = New System.Drawing.Point(9, 703)
        Me.btnReportImport.Name = "btnReportImport"
        Me.btnReportImport.Size = New System.Drawing.Size(164, 20)
        Me.btnReportImport.TabIndex = 195
        Me.btnReportImport.Text = "Import Report Definition"
        '
        'sptReports
        '
        Me.sptReports.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sptReports.Location = New System.Drawing.Point(6, 54)
        Me.sptReports.Name = "sptReports"
        '
        'sptReports.Panel1
        '
        Me.sptReports.Panel1.Controls.Add(Me.lvwAvailableFields)
        Me.sptReports.Panel1.Controls.Add(Me.lblAvailableFields)
        '
        'sptReports.Panel2
        '
        Me.sptReports.Panel2.Controls.Add(Me.sptReport)
        Me.sptReports.Size = New System.Drawing.Size(1240, 605)
        Me.sptReports.SplitterDistance = 176
        Me.sptReports.TabIndex = 193
        '
        'lvwAvailableFields
        '
        Me.lvwAvailableFields.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwAvailableFields.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colAvTableName, Me.colAvFieldAlias})
        Me.lvwAvailableFields.FullRowSelect = True
        Me.lvwAvailableFields.Location = New System.Drawing.Point(3, 27)
        Me.lvwAvailableFields.Name = "lvwAvailableFields"
        Me.lvwAvailableFields.Size = New System.Drawing.Size(170, 575)
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
        'sptReport
        '
        Me.sptReport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sptReport.Location = New System.Drawing.Point(0, 0)
        Me.sptReport.Name = "sptReport"
        Me.sptReport.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'sptReport.Panel1
        '
        Me.sptReport.Panel1.Controls.Add(Me.lvwSelectedFields)
        Me.sptReport.Panel1.Controls.Add(Me.pnlReportHeaders)
        Me.sptReport.Panel1.Controls.Add(Me.btnReportFieldRemove)
        Me.sptReport.Panel1.Controls.Add(Me.btnReportFieldAdd)
        Me.sptReport.Panel1.Controls.Add(Me.btnReportFieldDown)
        Me.sptReport.Panel1.Controls.Add(Me.btnReportFieldUp)
        Me.sptReport.Panel1.Controls.Add(Me.pnlSelectedFieldsMain)
        Me.sptReport.Panel1.Controls.Add(Me.lblSelectedFields)
        '
        'sptReport.Panel2
        '
        Me.sptReport.Panel2.Controls.Add(Me.lvwSelectedTables)
        Me.sptReport.Panel2.Controls.Add(Me.btnReportTableDown)
        Me.sptReport.Panel2.Controls.Add(Me.pnlRelationsMain)
        Me.sptReport.Panel2.Controls.Add(Me.btnReportTableUp)
        Me.sptReport.Panel2.Controls.Add(Me.pnlRelationHeaders)
        Me.sptReport.Panel2.Controls.Add(Me.lblSelectedTables)
        Me.sptReport.Size = New System.Drawing.Size(1060, 602)
        Me.sptReport.SplitterDistance = 428
        Me.sptReport.TabIndex = 181
        '
        'lvwSelectedFields
        '
        Me.lvwSelectedFields.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lvwSelectedFields.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colSeltableName, Me.colSelFieldAlias})
        Me.lvwSelectedFields.FullRowSelect = True
        Me.lvwSelectedFields.Location = New System.Drawing.Point(38, 25)
        Me.lvwSelectedFields.Name = "lvwSelectedFields"
        Me.lvwSelectedFields.Size = New System.Drawing.Size(169, 400)
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
        'pnlReportHeaders
        '
        Me.pnlReportHeaders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportSortOrder)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportSort)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportShowMode)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilterMode)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilterText)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilterType)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportFilter)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportShow)
        Me.pnlReportHeaders.Controls.Add(Me.lblReportSelectedFields)
        Me.pnlReportHeaders.Location = New System.Drawing.Point(245, -3)
        Me.pnlReportHeaders.Name = "pnlReportHeaders"
        Me.pnlReportHeaders.Size = New System.Drawing.Size(810, 24)
        Me.pnlReportHeaders.TabIndex = 154
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
        'lblReportShow
        '
        Me.lblReportShow.AutoSize = True
        Me.lblReportShow.Location = New System.Drawing.Point(173, 8)
        Me.lblReportShow.Name = "lblReportShow"
        Me.lblReportShow.Size = New System.Drawing.Size(34, 13)
        Me.lblReportShow.TabIndex = 118
        Me.lblReportShow.Text = "Show"
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
        'btnReportFieldRemove
        '
        Me.btnReportFieldRemove.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldRemove.Image = Global.Sequenchel.My.Resources.Resources.button_leftt
        Me.btnReportFieldRemove.Location = New System.Drawing.Point(3, 142)
        Me.btnReportFieldRemove.Name = "btnReportFieldRemove"
        Me.btnReportFieldRemove.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldRemove.TabIndex = 2
        Me.btnReportFieldRemove.UseVisualStyleBackColor = True
        '
        'btnReportFieldAdd
        '
        Me.btnReportFieldAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldAdd.Image = Global.Sequenchel.My.Resources.Resources.button_right
        Me.btnReportFieldAdd.Location = New System.Drawing.Point(3, 113)
        Me.btnReportFieldAdd.Name = "btnReportFieldAdd"
        Me.btnReportFieldAdd.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldAdd.TabIndex = 1
        Me.btnReportFieldAdd.UseVisualStyleBackColor = True
        '
        'btnReportFieldDown
        '
        Me.btnReportFieldDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldDown.Image = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnReportFieldDown.Location = New System.Drawing.Point(213, 142)
        Me.btnReportFieldDown.Name = "btnReportFieldDown"
        Me.btnReportFieldDown.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldDown.TabIndex = 2
        Me.btnReportFieldDown.UseVisualStyleBackColor = True
        '
        'btnReportFieldUp
        '
        Me.btnReportFieldUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportFieldUp.Image = Global.Sequenchel.My.Resources.Resources.button_up
        Me.btnReportFieldUp.Location = New System.Drawing.Point(213, 113)
        Me.btnReportFieldUp.Name = "btnReportFieldUp"
        Me.btnReportFieldUp.Size = New System.Drawing.Size(30, 23)
        Me.btnReportFieldUp.TabIndex = 1
        Me.btnReportFieldUp.UseVisualStyleBackColor = True
        '
        'pnlSelectedFieldsMain
        '
        Me.pnlSelectedFieldsMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSelectedFieldsMain.AutoScroll = True
        Me.pnlSelectedFieldsMain.AutoScrollMargin = New System.Drawing.Size(5, 5)
        Me.pnlSelectedFieldsMain.BackgroundImage = Global.Sequenchel.My.Resources.Resources.Background_Sequenchel_reliefsquaretransparant30down
        Me.pnlSelectedFieldsMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pnlSelectedFieldsMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlSelectedFieldsMain.Controls.Add(Me.pnlSelectedFields)
        Me.pnlSelectedFieldsMain.Location = New System.Drawing.Point(246, 21)
        Me.pnlSelectedFieldsMain.Name = "pnlSelectedFieldsMain"
        Me.pnlSelectedFieldsMain.Size = New System.Drawing.Size(809, 405)
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
        Me.pnlSelectedFields.Location = New System.Drawing.Point(0, 0)
        Me.pnlSelectedFields.Name = "pnlSelectedFields"
        Me.pnlSelectedFields.Size = New System.Drawing.Size(804, 31)
        Me.pnlSelectedFields.TabIndex = 5
        '
        'pnlReportFilterText
        '
        Me.pnlReportFilterText.Location = New System.Drawing.Point(578, 0)
        Me.pnlReportFilterText.Name = "pnlReportFilterText"
        Me.pnlReportFilterText.Size = New System.Drawing.Size(223, 30)
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
        'lblSelectedFields
        '
        Me.lblSelectedFields.AutoSize = True
        Me.lblSelectedFields.Location = New System.Drawing.Point(45, 5)
        Me.lblSelectedFields.Name = "lblSelectedFields"
        Me.lblSelectedFields.Size = New System.Drawing.Size(79, 13)
        Me.lblSelectedFields.TabIndex = 150
        Me.lblSelectedFields.Text = "Selected Fields"
        '
        'lvwSelectedTables
        '
        Me.lvwSelectedTables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lvwSelectedTables.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colTable})
        Me.lvwSelectedTables.FullRowSelect = True
        Me.lvwSelectedTables.Location = New System.Drawing.Point(38, 22)
        Me.lvwSelectedTables.Name = "lvwSelectedTables"
        Me.lvwSelectedTables.Size = New System.Drawing.Size(169, 140)
        Me.lvwSelectedTables.TabIndex = 0
        Me.lvwSelectedTables.UseCompatibleStateImageBehavior = False
        Me.lvwSelectedTables.View = System.Windows.Forms.View.Details
        '
        'colTable
        '
        Me.colTable.Text = "Table"
        Me.colTable.Width = 50
        '
        'btnReportTableDown
        '
        Me.btnReportTableDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportTableDown.Image = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnReportTableDown.Location = New System.Drawing.Point(213, 76)
        Me.btnReportTableDown.Name = "btnReportTableDown"
        Me.btnReportTableDown.Size = New System.Drawing.Size(30, 23)
        Me.btnReportTableDown.TabIndex = 156
        Me.btnReportTableDown.UseVisualStyleBackColor = True
        '
        'pnlRelationsMain
        '
        Me.pnlRelationsMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationsMain.AutoScroll = True
        Me.pnlRelationsMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlRelationsMain.Controls.Add(Me.pnlRelations)
        Me.pnlRelationsMain.Location = New System.Drawing.Point(245, 21)
        Me.pnlRelationsMain.Name = "pnlRelationsMain"
        Me.pnlRelationsMain.Size = New System.Drawing.Size(809, 141)
        Me.pnlRelationsMain.TabIndex = 156
        '
        'pnlRelations
        '
        Me.pnlRelations.Controls.Add(Me.pnlRelationsField)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsJoinType)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsRelation)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsUse)
        Me.pnlRelations.Controls.Add(Me.pnlRelationsLabel)
        Me.pnlRelations.Location = New System.Drawing.Point(0, 0)
        Me.pnlRelations.Name = "pnlRelations"
        Me.pnlRelations.Size = New System.Drawing.Size(706, 48)
        Me.pnlRelations.TabIndex = 7
        '
        'pnlRelationsField
        '
        Me.pnlRelationsField.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationsField.Location = New System.Drawing.Point(203, 0)
        Me.pnlRelationsField.Name = "pnlRelationsField"
        Me.pnlRelationsField.Size = New System.Drawing.Size(150, 48)
        Me.pnlRelationsField.TabIndex = 10
        '
        'pnlRelationsJoinType
        '
        Me.pnlRelationsJoinType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationsJoinType.Location = New System.Drawing.Point(504, 0)
        Me.pnlRelationsJoinType.Name = "pnlRelationsJoinType"
        Me.pnlRelationsJoinType.Size = New System.Drawing.Size(120, 48)
        Me.pnlRelationsJoinType.TabIndex = 9
        '
        'pnlRelationsRelation
        '
        Me.pnlRelationsRelation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationsRelation.Location = New System.Drawing.Point(353, 0)
        Me.pnlRelationsRelation.Name = "pnlRelationsRelation"
        Me.pnlRelationsRelation.Size = New System.Drawing.Size(150, 48)
        Me.pnlRelationsRelation.TabIndex = 9
        '
        'pnlRelationsUse
        '
        Me.pnlRelationsUse.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationsUse.Location = New System.Drawing.Point(173, 0)
        Me.pnlRelationsUse.Name = "pnlRelationsUse"
        Me.pnlRelationsUse.Size = New System.Drawing.Size(30, 48)
        Me.pnlRelationsUse.TabIndex = 9
        '
        'pnlRelationsLabel
        '
        Me.pnlRelationsLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationsLabel.Location = New System.Drawing.Point(0, 0)
        Me.pnlRelationsLabel.Name = "pnlRelationsLabel"
        Me.pnlRelationsLabel.Size = New System.Drawing.Size(173, 48)
        Me.pnlRelationsLabel.TabIndex = 8
        '
        'btnReportTableUp
        '
        Me.btnReportTableUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReportTableUp.Image = Global.Sequenchel.My.Resources.Resources.button_up
        Me.btnReportTableUp.Location = New System.Drawing.Point(213, 47)
        Me.btnReportTableUp.Name = "btnReportTableUp"
        Me.btnReportTableUp.Size = New System.Drawing.Size(30, 23)
        Me.btnReportTableUp.TabIndex = 155
        Me.btnReportTableUp.UseVisualStyleBackColor = True
        '
        'pnlRelationHeaders
        '
        Me.pnlRelationHeaders.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRelationHeaders.Controls.Add(Me.lblSourceField)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationUse)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationsJoinType)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationsRelation)
        Me.pnlRelationHeaders.Controls.Add(Me.lblRelationsSelectedTable)
        Me.pnlRelationHeaders.Location = New System.Drawing.Point(245, -4)
        Me.pnlRelationHeaders.Name = "pnlRelationHeaders"
        Me.pnlRelationHeaders.Size = New System.Drawing.Size(809, 24)
        Me.pnlRelationHeaders.TabIndex = 155
        '
        'lblSourceField
        '
        Me.lblSourceField.AutoSize = True
        Me.lblSourceField.Location = New System.Drawing.Point(203, 8)
        Me.lblSourceField.Name = "lblSourceField"
        Me.lblSourceField.Size = New System.Drawing.Size(66, 13)
        Me.lblSourceField.TabIndex = 121
        Me.lblSourceField.Text = "Source Field"
        '
        'lblRelationUse
        '
        Me.lblRelationUse.AutoSize = True
        Me.lblRelationUse.Location = New System.Drawing.Point(171, 8)
        Me.lblRelationUse.Name = "lblRelationUse"
        Me.lblRelationUse.Size = New System.Drawing.Size(26, 13)
        Me.lblRelationUse.TabIndex = 120
        Me.lblRelationUse.Text = "Use"
        '
        'lblRelationsJoinType
        '
        Me.lblRelationsJoinType.AutoSize = True
        Me.lblRelationsJoinType.Location = New System.Drawing.Point(501, 7)
        Me.lblRelationsJoinType.Name = "lblRelationsJoinType"
        Me.lblRelationsJoinType.Size = New System.Drawing.Size(53, 13)
        Me.lblRelationsJoinType.TabIndex = 119
        Me.lblRelationsJoinType.Text = "Join Type"
        '
        'lblRelationsRelation
        '
        Me.lblRelationsRelation.AutoSize = True
        Me.lblRelationsRelation.Location = New System.Drawing.Point(351, 8)
        Me.lblRelationsRelation.Name = "lblRelationsRelation"
        Me.lblRelationsRelation.Size = New System.Drawing.Size(46, 13)
        Me.lblRelationsRelation.TabIndex = 118
        Me.lblRelationsRelation.Text = "Relation"
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
        'lblSelectedTables
        '
        Me.lblSelectedTables.AutoSize = True
        Me.lblSelectedTables.Location = New System.Drawing.Point(45, 4)
        Me.lblSelectedTables.Name = "lblSelectedTables"
        Me.lblSelectedTables.Size = New System.Drawing.Size(84, 13)
        Me.lblSelectedTables.TabIndex = 153
        Me.lblSelectedTables.Text = "Selected Tables"
        '
        'btnRevertChanges
        '
        Me.btnRevertChanges.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRevertChanges.Location = New System.Drawing.Point(1076, 703)
        Me.btnRevertChanges.Name = "btnRevertChanges"
        Me.btnRevertChanges.Size = New System.Drawing.Size(164, 20)
        Me.btnRevertChanges.TabIndex = 12
        Me.btnRevertChanges.Text = "Revert Changes"
        '
        'btnQueryShow
        '
        Me.btnQueryShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQueryShow.Location = New System.Drawing.Point(1076, 677)
        Me.btnQueryShow.Name = "btnQueryShow"
        Me.btnQueryShow.Size = New System.Drawing.Size(164, 20)
        Me.btnQueryShow.TabIndex = 10
        Me.btnQueryShow.Text = "Show Query"
        '
        'txtTop
        '
        Me.txtTop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTop.Location = New System.Drawing.Point(822, 704)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(39, 20)
        Me.txtTop.TabIndex = 8
        Me.txtTop.Text = "1000"
        '
        'chkTop
        '
        Me.chkTop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTop.AutoSize = True
        Me.chkTop.Checked = True
        Me.chkTop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTop.Location = New System.Drawing.Point(746, 706)
        Me.chkTop.Name = "chkTop"
        Me.chkTop.Size = New System.Drawing.Size(70, 17)
        Me.chkTop.TabIndex = 7
        Me.chkTop.Text = "Use TOP"
        Me.chkTop.UseVisualStyleBackColor = True
        '
        'chkDistinct
        '
        Me.chkDistinct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDistinct.AutoSize = True
        Me.chkDistinct.Location = New System.Drawing.Point(746, 680)
        Me.chkDistinct.Name = "chkDistinct"
        Me.chkDistinct.Size = New System.Drawing.Size(98, 17)
        Me.chkDistinct.TabIndex = 6
        Me.chkDistinct.Text = "Use DISTINCT"
        Me.chkDistinct.UseVisualStyleBackColor = True
        '
        'btnReportCreate
        '
        Me.btnReportCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReportCreate.Location = New System.Drawing.Point(906, 677)
        Me.btnReportCreate.Name = "btnReportCreate"
        Me.btnReportCreate.Size = New System.Drawing.Size(164, 20)
        Me.btnReportCreate.TabIndex = 9
        Me.btnReportCreate.Text = "Create Report"
        '
        'btnReportClear
        '
        Me.btnReportClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReportClear.Location = New System.Drawing.Point(906, 703)
        Me.btnReportClear.Name = "btnReportClear"
        Me.btnReportClear.Size = New System.Drawing.Size(164, 20)
        Me.btnReportClear.TabIndex = 11
        Me.btnReportClear.Text = "Clear All"
        '
        'btnTablesReload
        '
        Me.btnTablesReload.Enabled = False
        Me.btnTablesReload.Image = CType(resources.GetObject("btnTablesReload.Image"), System.Drawing.Image)
        Me.btnTablesReload.Location = New System.Drawing.Point(480, 23)
        Me.btnTablesReload.Name = "btnTablesReload"
        Me.btnTablesReload.Size = New System.Drawing.Size(25, 25)
        Me.btnTablesReload.TabIndex = 180
        Me.btnTablesReload.UseVisualStyleBackColor = True
        '
        'btnTableSetsReload
        '
        Me.btnTableSetsReload.Image = CType(resources.GetObject("btnTableSetsReload.Image"), System.Drawing.Image)
        Me.btnTableSetsReload.Location = New System.Drawing.Point(247, 23)
        Me.btnTableSetsReload.Name = "btnTableSetsReload"
        Me.btnTableSetsReload.Size = New System.Drawing.Size(25, 25)
        Me.btnTableSetsReload.TabIndex = 179
        Me.btnTableSetsReload.UseVisualStyleBackColor = True
        '
        'btnConnectionsReload
        '
        Me.btnConnectionsReload.Image = CType(resources.GetObject("btnConnectionsReload.Image"), System.Drawing.Image)
        Me.btnConnectionsReload.Location = New System.Drawing.Point(14, 23)
        Me.btnConnectionsReload.Name = "btnConnectionsReload"
        Me.btnConnectionsReload.Size = New System.Drawing.Size(25, 25)
        Me.btnConnectionsReload.TabIndex = 178
        Me.btnConnectionsReload.UseVisualStyleBackColor = True
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(508, 11)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(34, 13)
        Me.lblTable.TabIndex = 177
        Me.lblTable.Text = "Table"
        '
        'cbxTable
        '
        Me.cbxTable.Enabled = False
        Me.cbxTable.FormattingEnabled = True
        Me.cbxTable.Location = New System.Drawing.Point(507, 27)
        Me.cbxTable.Name = "cbxTable"
        Me.cbxTable.Size = New System.Drawing.Size(200, 21)
        Me.cbxTable.Sorted = True
        Me.cbxTable.TabIndex = 2
        '
        'lblTableSet
        '
        Me.lblTableSet.AutoSize = True
        Me.lblTableSet.Location = New System.Drawing.Point(272, 11)
        Me.lblTableSet.Name = "lblTableSet"
        Me.lblTableSet.Size = New System.Drawing.Size(50, 13)
        Me.lblTableSet.TabIndex = 175
        Me.lblTableSet.Text = "TableSet"
        '
        'cbxTableSet
        '
        Me.cbxTableSet.FormattingEnabled = True
        Me.cbxTableSet.Location = New System.Drawing.Point(274, 27)
        Me.cbxTableSet.Name = "cbxTableSet"
        Me.cbxTableSet.Size = New System.Drawing.Size(200, 21)
        Me.cbxTableSet.Sorted = True
        Me.cbxTableSet.TabIndex = 1
        '
        'lblConnection
        '
        Me.lblConnection.AutoSize = True
        Me.lblConnection.Location = New System.Drawing.Point(41, 11)
        Me.lblConnection.Name = "lblConnection"
        Me.lblConnection.Size = New System.Drawing.Size(61, 13)
        Me.lblConnection.TabIndex = 173
        Me.lblConnection.Text = "Connection"
        '
        'cbxConnection
        '
        Me.cbxConnection.FormattingEnabled = True
        Me.cbxConnection.Location = New System.Drawing.Point(41, 27)
        Me.cbxConnection.Name = "cbxConnection"
        Me.cbxConnection.Size = New System.Drawing.Size(200, 21)
        Me.cbxConnection.Sorted = True
        Me.cbxConnection.TabIndex = 0
        '
        'tpgReportResult
        '
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
        Me.tpgReportResult.Size = New System.Drawing.Size(1252, 737)
        Me.tpgReportResult.TabIndex = 1
        Me.tpgReportResult.Text = "Report Result"
        Me.tpgReportResult.UseVisualStyleBackColor = True
        '
        'lblElapsedTime
        '
        Me.lblElapsedTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblElapsedTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblElapsedTime.Location = New System.Drawing.Point(1173, 570)
        Me.lblElapsedTime.Name = "lblElapsedTime"
        Me.lblElapsedTime.Size = New System.Drawing.Size(73, 22)
        Me.lblElapsedTime.TabIndex = 178
        '
        'btnDefinition
        '
        Me.btnDefinition.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDefinition.Location = New System.Drawing.Point(1105, 703)
        Me.btnDefinition.Name = "btnDefinition"
        Me.btnDefinition.Size = New System.Drawing.Size(135, 23)
        Me.btnDefinition.TabIndex = 5
        Me.btnDefinition.Text = "Back to Definition"
        Me.btnDefinition.UseVisualStyleBackColor = True
        '
        'btnExportToFile
        '
        Me.btnExportToFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportToFile.Location = New System.Drawing.Point(442, 703)
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
        Me.lblErrorMessage.Location = New System.Drawing.Point(161, 572)
        Me.lblErrorMessage.Name = "lblErrorMessage"
        Me.lblErrorMessage.Size = New System.Drawing.Size(1002, 22)
        Me.lblErrorMessage.TabIndex = 175
        '
        'lblListCount
        '
        Me.lblListCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblListCount.AutoSize = True
        Me.lblListCount.Location = New System.Drawing.Point(6, 572)
        Me.lblListCount.Name = "lblListCount"
        Me.lblListCount.Size = New System.Drawing.Size(57, 13)
        Me.lblListCount.TabIndex = 173
        Me.lblListCount.Text = "List Count:"
        '
        'lblListCountNumber
        '
        Me.lblListCountNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblListCountNumber.Location = New System.Drawing.Point(83, 572)
        Me.lblListCountNumber.Name = "lblListCountNumber"
        Me.lblListCountNumber.Size = New System.Drawing.Size(54, 13)
        Me.lblListCountNumber.TabIndex = 174
        Me.lblListCountNumber.Text = "0"
        '
        'btnLoadQuery
        '
        Me.btnLoadQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoadQuery.Location = New System.Drawing.Point(301, 703)
        Me.btnLoadQuery.Name = "btnLoadQuery"
        Me.btnLoadQuery.Size = New System.Drawing.Size(135, 23)
        Me.btnLoadQuery.TabIndex = 3
        Me.btnLoadQuery.Text = "Load Query from File"
        Me.btnLoadQuery.UseVisualStyleBackColor = True
        '
        'btnSaveQuery
        '
        Me.btnSaveQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveQuery.Location = New System.Drawing.Point(160, 703)
        Me.btnSaveQuery.Name = "btnSaveQuery"
        Me.btnSaveQuery.Size = New System.Drawing.Size(135, 23)
        Me.btnSaveQuery.TabIndex = 2
        Me.btnSaveQuery.Text = "Save Query to File"
        Me.btnSaveQuery.UseVisualStyleBackColor = True
        '
        'btnExecuteQuery
        '
        Me.btnExecuteQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExecuteQuery.Location = New System.Drawing.Point(19, 703)
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
        Me.rtbQuery.Location = New System.Drawing.Point(6, 597)
        Me.rtbQuery.Name = "rtbQuery"
        Me.rtbQuery.Size = New System.Drawing.Size(1240, 100)
        Me.rtbQuery.TabIndex = 0
        Me.rtbQuery.Text = ""
        '
        'dgvReport
        '
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
        Me.dgvReport.Size = New System.Drawing.Size(1243, 563)
        Me.dgvReport.TabIndex = 0
        '
        'btnTest
        '
        Me.btnTest.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTest.Location = New System.Drawing.Point(12, 778)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(135, 23)
        Me.btnTest.TabIndex = 170
        Me.btnTest.TabStop = False
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        Me.btnTest.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(1121, 778)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(135, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'tmrElapsedTime
        '
        Me.tmrElapsedTime.Interval = 1000
        '
        'frmReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1284, 812)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.lblLicense)
        Me.Controls.Add(Me.tabReports)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "frmReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reports"
        Me.tabReports.ResumeLayout(False)
        Me.tpgReportDefinition.ResumeLayout(False)
        Me.tpgReportDefinition.PerformLayout()
        Me.pnlReportName.ResumeLayout(False)
        Me.pnlReportName.PerformLayout()
        Me.sptReports.Panel1.ResumeLayout(False)
        Me.sptReports.Panel1.PerformLayout()
        Me.sptReports.Panel2.ResumeLayout(False)
        CType(Me.sptReports, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptReports.ResumeLayout(False)
        Me.sptReport.Panel1.ResumeLayout(False)
        Me.sptReport.Panel1.PerformLayout()
        Me.sptReport.Panel2.ResumeLayout(False)
        Me.sptReport.Panel2.PerformLayout()
        CType(Me.sptReport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptReport.ResumeLayout(False)
        Me.pnlReportHeaders.ResumeLayout(False)
        Me.pnlReportHeaders.PerformLayout()
        Me.pnlSelectedFieldsMain.ResumeLayout(False)
        Me.pnlSelectedFields.ResumeLayout(False)
        Me.pnlRelationsMain.ResumeLayout(False)
        Me.pnlRelations.ResumeLayout(False)
        Me.pnlRelationHeaders.ResumeLayout(False)
        Me.pnlRelationHeaders.PerformLayout()
        Me.tpgReportResult.ResumeLayout(False)
        Me.tpgReportResult.PerformLayout()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblReportShow As System.Windows.Forms.Label
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
    Friend WithEvents pnlRelationsRelation As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationsUse As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationsLabel As System.Windows.Forms.Panel
    Friend WithEvents pnlRelationHeaders As System.Windows.Forms.Panel
    Friend WithEvents lblSourceField As System.Windows.Forms.Label
    Friend WithEvents lblRelationUse As System.Windows.Forms.Label
    Friend WithEvents lblRelationsJoinType As System.Windows.Forms.Label
    Friend WithEvents lblRelationsRelation As System.Windows.Forms.Label
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
    Friend WithEvents btnClose As System.Windows.Forms.Button
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
    Friend WithEvents colTable As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblElapsedTime As System.Windows.Forms.Label
    Friend WithEvents tmrElapsedTime As System.Windows.Forms.Timer
    Friend WithEvents dgvReport As Sequenchel.usrDataGridView
    Friend WithEvents btnReportExport As System.Windows.Forms.Button
    Friend WithEvents btnReportImport As System.Windows.Forms.Button
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents pnlReportName As System.Windows.Forms.Panel
End Class
