<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSequenchel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSequenchel))
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblStatusText = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.cbxConnection = New System.Windows.Forms.ComboBox()
        Me.lblConnection = New System.Windows.Forms.Label()
        Me.lblTableSet = New System.Windows.Forms.Label()
        Me.cbxTableSet = New System.Windows.Forms.ComboBox()
        Me.lblTable = New System.Windows.Forms.Label()
        Me.cbxTable = New System.Windows.Forms.ComboBox()
        Me.btnConnectionsReload = New System.Windows.Forms.Button()
        Me.btnTableSetsReload = New System.Windows.Forms.Button()
        Me.btnTablesReload = New System.Windows.Forms.Button()
        Me.mnuMain = New System.Windows.Forms.MenuStrip()
        Me.mnuMainFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainFileExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainEditSettings = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainEditConfiguration = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainEditLinkedServers = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainTools = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainToolsSmartUpdate = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpManual = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMainHelpAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblLicense = New System.Windows.Forms.Label()
        Me.sptTable1 = New System.Windows.Forms.SplitContainer()
        Me.sptFields1 = New System.Windows.Forms.SplitContainer()
        Me.lblListCount = New System.Windows.Forms.Label()
        Me.lblListCountNumber = New System.Windows.Forms.Label()
        Me.pnlFooterControls = New System.Windows.Forms.Panel()
        Me.lblSavedSearches = New System.Windows.Forms.Label()
        Me.cbxSearch = New System.Windows.Forms.ComboBox()
        Me.btnDeleteSearch = New System.Windows.Forms.Button()
        Me.btnSearchAddOrUpdate = New System.Windows.Forms.Button()
        Me.chkReversedSortOrder = New System.Windows.Forms.CheckBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.txtUseTop = New System.Windows.Forms.TextBox()
        Me.chkUseTop = New System.Windows.Forms.CheckBox()
        Me.btnLoadList = New System.Windows.Forms.Button()
        Me.btnExportList = New System.Windows.Forms.Button()
        Me.btnLoadSearchCriteria = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblMultipleRows = New System.Windows.Forms.Label()
        Me.pnlListCount = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.dgvTable1 = New Sequenchel.usrDataGridView()
        Me.mnuMain.SuspendLayout()
        CType(Me.sptTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptTable1.Panel1.SuspendLayout()
        Me.sptTable1.Panel2.SuspendLayout()
        Me.sptTable1.SuspendLayout()
        CType(Me.sptFields1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.sptFields1.SuspendLayout()
        Me.pnlFooterControls.SuspendLayout()
        Me.pnlListCount.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.dgvTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(6, 36)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(135, 21)
        Me.btnTest.TabIndex = 4
        Me.btnTest.TabStop = False
        Me.btnTest.Text = "Test"
        Me.btnTest.UseVisualStyleBackColor = True
        Me.btnTest.Visible = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(1127, 775)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(135, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        Me.btnClose.Visible = False
        '
        'lblStatusText
        '
        Me.lblStatusText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusText.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusText.Location = New System.Drawing.Point(86, 778)
        Me.lblStatusText.Name = "lblStatusText"
        Me.lblStatusText.Size = New System.Drawing.Size(777, 25)
        Me.lblStatusText.TabIndex = 83
        Me.lblStatusText.Text = "Program Start"
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(12, 778)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(56, 20)
        Me.lblStatus.TabIndex = 82
        Me.lblStatus.Text = "Status"
        '
        'cbxConnection
        '
        Me.cbxConnection.FormattingEnabled = True
        Me.cbxConnection.Location = New System.Drawing.Point(53, 60)
        Me.cbxConnection.Name = "cbxConnection"
        Me.cbxConnection.Size = New System.Drawing.Size(200, 21)
        Me.cbxConnection.Sorted = True
        Me.cbxConnection.TabIndex = 91
        '
        'lblConnection
        '
        Me.lblConnection.AutoSize = True
        Me.lblConnection.Location = New System.Drawing.Point(53, 44)
        Me.lblConnection.Name = "lblConnection"
        Me.lblConnection.Size = New System.Drawing.Size(61, 13)
        Me.lblConnection.TabIndex = 92
        Me.lblConnection.Text = "Connection"
        '
        'lblTableSet
        '
        Me.lblTableSet.AutoSize = True
        Me.lblTableSet.Location = New System.Drawing.Point(284, 44)
        Me.lblTableSet.Name = "lblTableSet"
        Me.lblTableSet.Size = New System.Drawing.Size(50, 13)
        Me.lblTableSet.TabIndex = 94
        Me.lblTableSet.Text = "TableSet"
        '
        'cbxTableSet
        '
        Me.cbxTableSet.FormattingEnabled = True
        Me.cbxTableSet.Location = New System.Drawing.Point(286, 60)
        Me.cbxTableSet.Name = "cbxTableSet"
        Me.cbxTableSet.Size = New System.Drawing.Size(200, 21)
        Me.cbxTableSet.Sorted = True
        Me.cbxTableSet.TabIndex = 93
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(520, 44)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(34, 13)
        Me.lblTable.TabIndex = 96
        Me.lblTable.Text = "Table"
        '
        'cbxTable
        '
        Me.cbxTable.FormattingEnabled = True
        Me.cbxTable.Location = New System.Drawing.Point(519, 60)
        Me.cbxTable.Name = "cbxTable"
        Me.cbxTable.Size = New System.Drawing.Size(200, 21)
        Me.cbxTable.Sorted = True
        Me.cbxTable.TabIndex = 95
        '
        'btnConnectionsReload
        '
        Me.btnConnectionsReload.Image = CType(resources.GetObject("btnConnectionsReload.Image"), System.Drawing.Image)
        Me.btnConnectionsReload.Location = New System.Drawing.Point(26, 56)
        Me.btnConnectionsReload.Name = "btnConnectionsReload"
        Me.btnConnectionsReload.Size = New System.Drawing.Size(25, 25)
        Me.btnConnectionsReload.TabIndex = 97
        Me.btnConnectionsReload.UseVisualStyleBackColor = True
        '
        'btnTableSetsReload
        '
        Me.btnTableSetsReload.Image = CType(resources.GetObject("btnTableSetsReload.Image"), System.Drawing.Image)
        Me.btnTableSetsReload.Location = New System.Drawing.Point(259, 56)
        Me.btnTableSetsReload.Name = "btnTableSetsReload"
        Me.btnTableSetsReload.Size = New System.Drawing.Size(25, 25)
        Me.btnTableSetsReload.TabIndex = 98
        Me.btnTableSetsReload.UseVisualStyleBackColor = True
        '
        'btnTablesReload
        '
        Me.btnTablesReload.Image = CType(resources.GetObject("btnTablesReload.Image"), System.Drawing.Image)
        Me.btnTablesReload.Location = New System.Drawing.Point(492, 56)
        Me.btnTablesReload.Name = "btnTablesReload"
        Me.btnTablesReload.Size = New System.Drawing.Size(25, 25)
        Me.btnTablesReload.TabIndex = 99
        Me.btnTablesReload.UseVisualStyleBackColor = True
        '
        'mnuMain
        '
        Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainFile, Me.mnuMainEdit, Me.mnuMainTools, Me.mnuMainHelp})
        Me.mnuMain.Location = New System.Drawing.Point(0, 0)
        Me.mnuMain.Name = "mnuMain"
        Me.mnuMain.Size = New System.Drawing.Size(1284, 24)
        Me.mnuMain.TabIndex = 100
        Me.mnuMain.Text = "MenuStrip1"
        '
        'mnuMainFile
        '
        Me.mnuMainFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainFileExit})
        Me.mnuMainFile.Name = "mnuMainFile"
        Me.mnuMainFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuMainFile.Text = "File"
        '
        'mnuMainFileExit
        '
        Me.mnuMainFileExit.Name = "mnuMainFileExit"
        Me.mnuMainFileExit.Size = New System.Drawing.Size(92, 22)
        Me.mnuMainFileExit.Text = "&Exit"
        '
        'mnuMainEdit
        '
        Me.mnuMainEdit.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainEditSettings, Me.mnuMainEditConfiguration, Me.mnuMainEditLinkedServers})
        Me.mnuMainEdit.Name = "mnuMainEdit"
        Me.mnuMainEdit.Size = New System.Drawing.Size(39, 20)
        Me.mnuMainEdit.Text = "&Edit"
        '
        'mnuMainEditSettings
        '
        Me.mnuMainEditSettings.Name = "mnuMainEditSettings"
        Me.mnuMainEditSettings.Size = New System.Drawing.Size(149, 22)
        Me.mnuMainEditSettings.Text = "&Settings"
        '
        'mnuMainEditConfiguration
        '
        Me.mnuMainEditConfiguration.Name = "mnuMainEditConfiguration"
        Me.mnuMainEditConfiguration.Size = New System.Drawing.Size(149, 22)
        Me.mnuMainEditConfiguration.Text = "&Configuration"
        '
        'mnuMainEditLinkedServers
        '
        Me.mnuMainEditLinkedServers.Name = "mnuMainEditLinkedServers"
        Me.mnuMainEditLinkedServers.Size = New System.Drawing.Size(149, 22)
        Me.mnuMainEditLinkedServers.Text = "&Linked Servers"
        '
        'mnuMainTools
        '
        Me.mnuMainTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainToolsReports, Me.mnuMainToolsImport, Me.mnuMainToolsSmartUpdate})
        Me.mnuMainTools.Name = "mnuMainTools"
        Me.mnuMainTools.Size = New System.Drawing.Size(47, 20)
        Me.mnuMainTools.Text = "Tools"
        '
        'mnuMainToolsReports
        '
        Me.mnuMainToolsReports.Name = "mnuMainToolsReports"
        Me.mnuMainToolsReports.Size = New System.Drawing.Size(143, 22)
        Me.mnuMainToolsReports.Text = "&Reports"
        '
        'mnuMainToolsImport
        '
        Me.mnuMainToolsImport.Name = "mnuMainToolsImport"
        Me.mnuMainToolsImport.Size = New System.Drawing.Size(143, 22)
        Me.mnuMainToolsImport.Text = "&Import"
        '
        'mnuMainToolsSmartUpdate
        '
        Me.mnuMainToolsSmartUpdate.Name = "mnuMainToolsSmartUpdate"
        Me.mnuMainToolsSmartUpdate.Size = New System.Drawing.Size(143, 22)
        Me.mnuMainToolsSmartUpdate.Text = "&SmartUpdate"
        '
        'mnuMainHelp
        '
        Me.mnuMainHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMainHelpManual, Me.mnuMainHelpAbout})
        Me.mnuMainHelp.Name = "mnuMainHelp"
        Me.mnuMainHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuMainHelp.Text = "Help"
        '
        'mnuMainHelpManual
        '
        Me.mnuMainHelpManual.Name = "mnuMainHelpManual"
        Me.mnuMainHelpManual.Size = New System.Drawing.Size(171, 22)
        Me.mnuMainHelpManual.Text = "Manual"
        '
        'mnuMainHelpAbout
        '
        Me.mnuMainHelpAbout.Name = "mnuMainHelpAbout"
        Me.mnuMainHelpAbout.Size = New System.Drawing.Size(171, 22)
        Me.mnuMainHelpAbout.Text = "About Sequenchel"
        '
        'lblLicense
        '
        Me.lblLicense.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLicense.AutoSize = True
        Me.lblLicense.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicense.Location = New System.Drawing.Point(977, 0)
        Me.lblLicense.Name = "lblLicense"
        Me.lblLicense.Size = New System.Drawing.Size(307, 24)
        Me.lblLicense.TabIndex = 105
        Me.lblLicense.Text = "Licensed to: Gemeente Amsterdam"
        '
        'sptTable1
        '
        Me.sptTable1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.sptTable1.Location = New System.Drawing.Point(19, 87)
        Me.sptTable1.Name = "sptTable1"
        '
        'sptTable1.Panel1
        '
        Me.sptTable1.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.sptTable1.Panel1.Controls.Add(Me.dgvTable1)
        '
        'sptTable1.Panel2
        '
        Me.sptTable1.Panel2.Controls.Add(Me.sptFields1)
        Me.sptTable1.Size = New System.Drawing.Size(1246, 593)
        Me.sptTable1.SplitterDistance = 713
        Me.sptTable1.TabIndex = 106
        '
        'sptFields1
        '
        Me.sptFields1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sptFields1.Location = New System.Drawing.Point(0, 0)
        Me.sptFields1.Name = "sptFields1"
        '
        'sptFields1.Panel2
        '
        Me.sptFields1.Panel2.AutoScroll = True
        Me.sptFields1.Size = New System.Drawing.Size(529, 593)
        Me.sptFields1.SplitterDistance = 196
        Me.sptFields1.TabIndex = 0
        '
        'lblListCount
        '
        Me.lblListCount.AutoSize = True
        Me.lblListCount.Location = New System.Drawing.Point(3, 9)
        Me.lblListCount.Name = "lblListCount"
        Me.lblListCount.Size = New System.Drawing.Size(57, 13)
        Me.lblListCount.TabIndex = 107
        Me.lblListCount.Text = "List Count:"
        '
        'lblListCountNumber
        '
        Me.lblListCountNumber.Location = New System.Drawing.Point(66, 9)
        Me.lblListCountNumber.Name = "lblListCountNumber"
        Me.lblListCountNumber.Size = New System.Drawing.Size(54, 13)
        Me.lblListCountNumber.TabIndex = 108
        Me.lblListCountNumber.Text = "0"
        '
        'pnlFooterControls
        '
        Me.pnlFooterControls.AutoScroll = True
        Me.pnlFooterControls.Controls.Add(Me.lblSavedSearches)
        Me.pnlFooterControls.Controls.Add(Me.cbxSearch)
        Me.pnlFooterControls.Controls.Add(Me.btnDeleteSearch)
        Me.pnlFooterControls.Controls.Add(Me.btnSearchAddOrUpdate)
        Me.pnlFooterControls.Controls.Add(Me.chkReversedSortOrder)
        Me.pnlFooterControls.Controls.Add(Me.btnDelete)
        Me.pnlFooterControls.Controls.Add(Me.txtUseTop)
        Me.pnlFooterControls.Controls.Add(Me.chkUseTop)
        Me.pnlFooterControls.Controls.Add(Me.btnLoadList)
        Me.pnlFooterControls.Controls.Add(Me.btnExportList)
        Me.pnlFooterControls.Controls.Add(Me.btnLoadSearchCriteria)
        Me.pnlFooterControls.Controls.Add(Me.btnSearch)
        Me.pnlFooterControls.Controls.Add(Me.btnUpdate)
        Me.pnlFooterControls.Controls.Add(Me.btnClear)
        Me.pnlFooterControls.Controls.Add(Me.btnAdd)
        Me.pnlFooterControls.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlFooterControls.Location = New System.Drawing.Point(203, 3)
        Me.pnlFooterControls.Name = "pnlFooterControls"
        Me.pnlFooterControls.Size = New System.Drawing.Size(1037, 81)
        Me.pnlFooterControls.TabIndex = 124
        '
        'lblSavedSearches
        '
        Me.lblSavedSearches.AutoSize = True
        Me.lblSavedSearches.Location = New System.Drawing.Point(5, 11)
        Me.lblSavedSearches.Name = "lblSavedSearches"
        Me.lblSavedSearches.Size = New System.Drawing.Size(86, 13)
        Me.lblSavedSearches.TabIndex = 139
        Me.lblSavedSearches.Text = "Saved Searches"
        '
        'cbxSearch
        '
        Me.cbxSearch.FormattingEnabled = True
        Me.cbxSearch.Location = New System.Drawing.Point(8, 35)
        Me.cbxSearch.Name = "cbxSearch"
        Me.cbxSearch.Size = New System.Drawing.Size(135, 21)
        Me.cbxSearch.TabIndex = 138
        '
        'btnDeleteSearch
        '
        Me.btnDeleteSearch.Location = New System.Drawing.Point(149, 35)
        Me.btnDeleteSearch.Name = "btnDeleteSearch"
        Me.btnDeleteSearch.Size = New System.Drawing.Size(135, 23)
        Me.btnDeleteSearch.TabIndex = 136
        Me.btnDeleteSearch.Text = "Delete Search"
        Me.btnDeleteSearch.UseVisualStyleBackColor = True
        '
        'btnSearchAddOrUpdate
        '
        Me.btnSearchAddOrUpdate.Location = New System.Drawing.Point(149, 6)
        Me.btnSearchAddOrUpdate.Name = "btnSearchAddOrUpdate"
        Me.btnSearchAddOrUpdate.Size = New System.Drawing.Size(135, 23)
        Me.btnSearchAddOrUpdate.TabIndex = 135
        Me.btnSearchAddOrUpdate.Text = "Add/Update Search"
        Me.btnSearchAddOrUpdate.UseVisualStyleBackColor = True
        '
        'chkReversedSortOrder
        '
        Me.chkReversedSortOrder.AutoSize = True
        Me.chkReversedSortOrder.Location = New System.Drawing.Point(334, 39)
        Me.chkReversedSortOrder.Name = "chkReversedSortOrder"
        Me.chkReversedSortOrder.Size = New System.Drawing.Size(123, 17)
        Me.chkReversedSortOrder.TabIndex = 134
        Me.chkReversedSortOrder.Text = "Reversed Sort Order"
        Me.chkReversedSortOrder.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(894, 35)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(135, 23)
        Me.btnDelete.TabIndex = 131
        Me.btnDelete.Text = "Delete Item"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'txtUseTop
        '
        Me.txtUseTop.Location = New System.Drawing.Point(404, 8)
        Me.txtUseTop.Name = "txtUseTop"
        Me.txtUseTop.Size = New System.Drawing.Size(61, 20)
        Me.txtUseTop.TabIndex = 133
        Me.txtUseTop.Text = "1000"
        '
        'chkUseTop
        '
        Me.chkUseTop.AutoSize = True
        Me.chkUseTop.Checked = True
        Me.chkUseTop.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseTop.Location = New System.Drawing.Point(334, 10)
        Me.chkUseTop.Name = "chkUseTop"
        Me.chkUseTop.Size = New System.Drawing.Size(70, 17)
        Me.chkUseTop.TabIndex = 132
        Me.chkUseTop.Text = "Use TOP"
        Me.chkUseTop.UseVisualStyleBackColor = True
        '
        'btnLoadList
        '
        Me.btnLoadList.Location = New System.Drawing.Point(471, 6)
        Me.btnLoadList.Name = "btnLoadList"
        Me.btnLoadList.Size = New System.Drawing.Size(135, 23)
        Me.btnLoadList.TabIndex = 124
        Me.btnLoadList.Text = "(Re)Load Complete List"
        Me.btnLoadList.UseVisualStyleBackColor = True
        '
        'btnExportList
        '
        Me.btnExportList.Location = New System.Drawing.Point(471, 35)
        Me.btnExportList.Name = "btnExportList"
        Me.btnExportList.Size = New System.Drawing.Size(135, 23)
        Me.btnExportList.TabIndex = 125
        Me.btnExportList.Text = "Export List to File"
        Me.btnExportList.UseVisualStyleBackColor = True
        '
        'btnLoadSearchCriteria
        '
        Me.btnLoadSearchCriteria.Location = New System.Drawing.Point(612, 6)
        Me.btnLoadSearchCriteria.Name = "btnLoadSearchCriteria"
        Me.btnLoadSearchCriteria.Size = New System.Drawing.Size(135, 23)
        Me.btnLoadSearchCriteria.TabIndex = 126
        Me.btnLoadSearchCriteria.Text = "(Re)Load Search Criteria"
        Me.btnLoadSearchCriteria.UseVisualStyleBackColor = True
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(753, 6)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(135, 23)
        Me.btnSearch.TabIndex = 127
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(753, 35)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(135, 23)
        Me.btnUpdate.TabIndex = 128
        Me.btnUpdate.Text = "Update Item"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClear.Location = New System.Drawing.Point(894, 6)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(135, 23)
        Me.btnClear.TabIndex = 129
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(612, 35)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(135, 23)
        Me.btnAdd.TabIndex = 130
        Me.btnAdd.Text = "New Item"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblMultipleRows
        '
        Me.lblMultipleRows.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMultipleRows.AutoSize = True
        Me.lblMultipleRows.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMultipleRows.Location = New System.Drawing.Point(969, 68)
        Me.lblMultipleRows.Name = "lblMultipleRows"
        Me.lblMultipleRows.Size = New System.Drawing.Size(209, 16)
        Me.lblMultipleRows.TabIndex = 125
        Me.lblMultipleRows.Text = "Warming: Multiple rows found"
        Me.lblMultipleRows.Visible = False
        '
        'pnlListCount
        '
        Me.pnlListCount.Controls.Add(Me.lblListCount)
        Me.pnlListCount.Controls.Add(Me.btnTest)
        Me.pnlListCount.Controls.Add(Me.lblListCountNumber)
        Me.pnlListCount.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlListCount.Location = New System.Drawing.Point(3, 3)
        Me.pnlListCount.Name = "pnlListCount"
        Me.pnlListCount.Size = New System.Drawing.Size(187, 70)
        Me.pnlListCount.TabIndex = 125
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.AutoScroll = True
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.6074!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.3926!))
        Me.TableLayoutPanel1.Controls.Add(Me.pnlFooterControls, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.pnlListCount, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(19, 683)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1243, 87)
        Me.TableLayoutPanel1.TabIndex = 126
        '
        'dgvTable1
        '
        Me.dgvTable1.AllowUserToAddRows = False
        Me.dgvTable1.AllowUserToDeleteRows = False
        Me.dgvTable1.AllowUserToOrderColumns = True
        Me.dgvTable1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvTable1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvTable1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.dgvTable1.BackImage = CType(resources.GetObject("dgvTable1.BackImage"), System.Drawing.Image)
        Me.dgvTable1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTable1.Location = New System.Drawing.Point(3, 3)
        Me.dgvTable1.MultiSelect = False
        Me.dgvTable1.Name = "dgvTable1"
        Me.dgvTable1.ReadOnly = True
        Me.dgvTable1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTable1.Size = New System.Drawing.Size(707, 587)
        Me.dgvTable1.TabIndex = 0
        '
        'frmSequenchel
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClear
        Me.ClientSize = New System.Drawing.Size(1284, 812)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.lblMultipleRows)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.sptTable1)
        Me.Controls.Add(Me.lblLicense)
        Me.Controls.Add(Me.btnTablesReload)
        Me.Controls.Add(Me.btnTableSetsReload)
        Me.Controls.Add(Me.btnConnectionsReload)
        Me.Controls.Add(Me.lblTable)
        Me.Controls.Add(Me.cbxTable)
        Me.Controls.Add(Me.lblTableSet)
        Me.Controls.Add(Me.cbxTableSet)
        Me.Controls.Add(Me.lblConnection)
        Me.Controls.Add(Me.cbxConnection)
        Me.Controls.Add(Me.lblStatusText)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.mnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnuMain
        Me.MinimumSize = New System.Drawing.Size(900, 600)
        Me.Name = "frmSequenchel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sequenchel"
        Me.mnuMain.ResumeLayout(False)
        Me.mnuMain.PerformLayout()
        Me.sptTable1.Panel1.ResumeLayout(False)
        Me.sptTable1.Panel2.ResumeLayout(False)
        CType(Me.sptTable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptTable1.ResumeLayout(False)
        CType(Me.sptFields1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.sptFields1.ResumeLayout(False)
        Me.pnlFooterControls.ResumeLayout(False)
        Me.pnlFooterControls.PerformLayout()
        Me.pnlListCount.ResumeLayout(False)
        Me.pnlListCount.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.dgvTable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblStatusText As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents cbxConnection As System.Windows.Forms.ComboBox
    Friend WithEvents lblConnection As System.Windows.Forms.Label
    Friend WithEvents lblTableSet As System.Windows.Forms.Label
    Friend WithEvents cbxTableSet As System.Windows.Forms.ComboBox
    Friend WithEvents lblTable As System.Windows.Forms.Label
    Friend WithEvents cbxTable As System.Windows.Forms.ComboBox
    Friend WithEvents btnConnectionsReload As System.Windows.Forms.Button
    Friend WithEvents btnTableSetsReload As System.Windows.Forms.Button
    Friend WithEvents btnTablesReload As System.Windows.Forms.Button
    Friend WithEvents mnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuMainFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblLicense As System.Windows.Forms.Label
    Friend WithEvents mnuMainHelpAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents sptTable1 As System.Windows.Forms.SplitContainer
    Friend WithEvents sptFields1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblListCount As System.Windows.Forms.Label
    Friend WithEvents lblListCountNumber As System.Windows.Forms.Label
    Friend WithEvents mnuMainToolsReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainFileExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlFooterControls As System.Windows.Forms.Panel
    Friend WithEvents cbxSearch As System.Windows.Forms.ComboBox
    Friend WithEvents btnDeleteSearch As System.Windows.Forms.Button
    Friend WithEvents btnSearchAddOrUpdate As System.Windows.Forms.Button
    Friend WithEvents chkReversedSortOrder As System.Windows.Forms.CheckBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtUseTop As System.Windows.Forms.TextBox
    Friend WithEvents chkUseTop As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoadList As System.Windows.Forms.Button
    Friend WithEvents btnExportList As System.Windows.Forms.Button
    Friend WithEvents btnLoadSearchCriteria As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents dgvTable1 As Sequenchel.usrDataGridView
    Friend WithEvents lblMultipleRows As System.Windows.Forms.Label
    Friend WithEvents lblSavedSearches As System.Windows.Forms.Label
    Friend WithEvents pnlListCount As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents mnuMainHelpManual As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsImport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainToolsSmartUpdate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainEdit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainEditSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainEditLinkedServers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMainEditConfiguration As System.Windows.Forms.ToolStripMenuItem

End Class
