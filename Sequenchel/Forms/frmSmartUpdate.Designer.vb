<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSmartUpdate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSmartUpdate))
        Me.lblSourceTable = New System.Windows.Forms.Label()
        Me.lblTargetTable = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxConnection = New System.Windows.Forms.ComboBox()
        Me.chkCreateTargetTable = New System.Windows.Forms.CheckBox()
        Me.chkCreateAuditTable = New System.Windows.Forms.CheckBox()
        Me.chkUseAuditing = New System.Windows.Forms.CheckBox()
        Me.chkRemoveNonSourceData = New System.Windows.Forms.CheckBox()
        Me.chkUseTargetCollation = New System.Windows.Forms.CheckBox()
        Me.txtSourceTable = New System.Windows.Forms.TextBox()
        Me.txtTargetTable = New System.Windows.Forms.TextBox()
        Me.pnlSourceTable = New System.Windows.Forms.Panel()
        Me.pnlSourcePrimaryKey = New System.Windows.Forms.Panel()
        Me.pnlCompareColumn = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pnlSourceDataType = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lstSourceTables = New System.Windows.Forms.ListBox()
        Me.btnCrawlSourceTables = New System.Windows.Forms.Button()
        Me.lblStatusText = New System.Windows.Forms.Label()
        Me.lstTargetTables = New System.Windows.Forms.ListBox()
        Me.btnCrawlTargetTables = New System.Windows.Forms.Button()
        Me.btnImportTables = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.pnlTargetTable = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlTargetDataType = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.pnlTargetPrimaryKey = New System.Windows.Forms.Panel()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.rbnSourceConfig = New System.Windows.Forms.RadioButton()
        Me.rbnTargetConfig = New System.Windows.Forms.RadioButton()
        Me.btnSaveConfiguration = New System.Windows.Forms.Button()
        Me.lblStatusTitle = New System.Windows.Forms.Label()
        Me.btnCreateSmartUpdateTable = New System.Windows.Forms.Button()
        Me.btnCreateSmartUpdateProcedure = New System.Windows.Forms.Button()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dtpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.chkNoEndDate = New System.Windows.Forms.CheckBox()
        Me.lblSmartUpdateCommand = New System.Windows.Forms.Label()
        Me.txtSmartUpdateCommand = New System.Windows.Forms.TextBox()
        Me.btnAddSmartUpdateSchedule = New System.Windows.Forms.Button()
        Me.lblLicenseRequired = New System.Windows.Forms.Label()
        Me.grpConfiguration = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grpCommand = New System.Windows.Forms.GroupBox()
        Me.chkClearTargetTable = New System.Windows.Forms.CheckBox()
        Me.grpCreateSQL = New System.Windows.Forms.GroupBox()
        Me.grpCreateLocalView = New System.Windows.Forms.GroupBox()
        Me.rtbInfoSourceDatabase = New System.Windows.Forms.RichTextBox()
        Me.rtbInfoLinkedServer = New System.Windows.Forms.RichTextBox()
        Me.lblInfoLocalView = New System.Windows.Forms.Label()
        Me.lblInfoLocalSchema = New System.Windows.Forms.Label()
        Me.lblInfoSourceDatabase = New System.Windows.Forms.Label()
        Me.lblInfoLinkedServer = New System.Windows.Forms.Label()
        Me.btnCreateLocalView = New System.Windows.Forms.Button()
        Me.lblLocalView = New System.Windows.Forms.Label()
        Me.txtLocalView = New System.Windows.Forms.TextBox()
        Me.txtLocalSchema = New System.Windows.Forms.TextBox()
        Me.lblLocalSchema = New System.Windows.Forms.Label()
        Me.lblSourceTableOrView = New System.Windows.Forms.Label()
        Me.txtSourceTableOrView = New System.Windows.Forms.TextBox()
        Me.txtSourceSchema = New System.Windows.Forms.TextBox()
        Me.lblSourceSchema = New System.Windows.Forms.Label()
        Me.txtSourceDatabase = New System.Windows.Forms.TextBox()
        Me.lblSourceDatabase = New System.Windows.Forms.Label()
        Me.lblLinkedServer = New System.Windows.Forms.Label()
        Me.cbxLinkedServer = New System.Windows.Forms.ComboBox()
        Me.rtbInfoLocalSchema = New System.Windows.Forms.RichTextBox()
        Me.rtbLocalView = New System.Windows.Forms.RichTextBox()
        Me.pnlMain.SuspendLayout()
        Me.grpConfiguration.SuspendLayout()
        Me.grpCommand.SuspendLayout()
        Me.grpCreateSQL.SuspendLayout()
        Me.grpCreateLocalView.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblSourceTable
        '
        Me.lblSourceTable.AutoSize = True
        Me.lblSourceTable.Location = New System.Drawing.Point(53, 233)
        Me.lblSourceTable.Name = "lblSourceTable"
        Me.lblSourceTable.Size = New System.Drawing.Size(109, 13)
        Me.lblSourceTable.TabIndex = 0
        Me.lblSourceTable.Text = "Source Table or View"
        '
        'lblTargetTable
        '
        Me.lblTargetTable.AutoSize = True
        Me.lblTargetTable.Location = New System.Drawing.Point(394, 233)
        Me.lblTargetTable.Name = "lblTargetTable"
        Me.lblTargetTable.Size = New System.Drawing.Size(68, 13)
        Me.lblTargetTable.TabIndex = 1
        Me.lblTargetTable.Text = "Target Table"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Connection"
        '
        'cbxConnection
        '
        Me.cbxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxConnection.FormattingEnabled = True
        Me.cbxConnection.Location = New System.Drawing.Point(108, 21)
        Me.cbxConnection.Name = "cbxConnection"
        Me.cbxConnection.Size = New System.Drawing.Size(227, 21)
        Me.cbxConnection.TabIndex = 0
        '
        'chkCreateTargetTable
        '
        Me.chkCreateTargetTable.AutoSize = True
        Me.chkCreateTargetTable.Location = New System.Drawing.Point(6, 19)
        Me.chkCreateTargetTable.Name = "chkCreateTargetTable"
        Me.chkCreateTargetTable.Size = New System.Drawing.Size(171, 17)
        Me.chkCreateTargetTable.TabIndex = 0
        Me.chkCreateTargetTable.Text = "Create Target Table if not exist"
        Me.chkCreateTargetTable.UseVisualStyleBackColor = True
        '
        'chkCreateAuditTable
        '
        Me.chkCreateAuditTable.AutoSize = True
        Me.chkCreateAuditTable.Checked = True
        Me.chkCreateAuditTable.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCreateAuditTable.Location = New System.Drawing.Point(6, 65)
        Me.chkCreateAuditTable.Name = "chkCreateAuditTable"
        Me.chkCreateAuditTable.Size = New System.Drawing.Size(164, 17)
        Me.chkCreateAuditTable.TabIndex = 2
        Me.chkCreateAuditTable.Text = "Create Audit Table if not exist"
        Me.chkCreateAuditTable.UseVisualStyleBackColor = True
        '
        'chkUseAuditing
        '
        Me.chkUseAuditing.AutoSize = True
        Me.chkUseAuditing.Checked = True
        Me.chkUseAuditing.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseAuditing.Location = New System.Drawing.Point(6, 42)
        Me.chkUseAuditing.Name = "chkUseAuditing"
        Me.chkUseAuditing.Size = New System.Drawing.Size(86, 17)
        Me.chkUseAuditing.TabIndex = 1
        Me.chkUseAuditing.Text = "Use Auditing"
        Me.chkUseAuditing.UseVisualStyleBackColor = True
        '
        'chkRemoveNonSourceData
        '
        Me.chkRemoveNonSourceData.Checked = True
        Me.chkRemoveNonSourceData.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRemoveNonSourceData.Location = New System.Drawing.Point(6, 88)
        Me.chkRemoveNonSourceData.Name = "chkRemoveNonSourceData"
        Me.chkRemoveNonSourceData.Size = New System.Drawing.Size(213, 17)
        Me.chkRemoveNonSourceData.TabIndex = 3
        Me.chkRemoveNonSourceData.Text = "Remove NON-Source Data from Target"
        Me.chkRemoveNonSourceData.UseVisualStyleBackColor = True
        '
        'chkUseTargetCollation
        '
        Me.chkUseTargetCollation.AutoSize = True
        Me.chkUseTargetCollation.Checked = True
        Me.chkUseTargetCollation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUseTargetCollation.Location = New System.Drawing.Point(6, 111)
        Me.chkUseTargetCollation.Name = "chkUseTargetCollation"
        Me.chkUseTargetCollation.Size = New System.Drawing.Size(171, 17)
        Me.chkUseTargetCollation.TabIndex = 4
        Me.chkUseTargetCollation.Text = "Use Target Database Collation"
        Me.chkUseTargetCollation.UseVisualStyleBackColor = True
        '
        'txtSourceTable
        '
        Me.txtSourceTable.Location = New System.Drawing.Point(56, 254)
        Me.txtSourceTable.Name = "txtSourceTable"
        Me.txtSourceTable.ReadOnly = True
        Me.txtSourceTable.Size = New System.Drawing.Size(249, 20)
        Me.txtSourceTable.TabIndex = 2
        '
        'txtTargetTable
        '
        Me.txtTargetTable.Location = New System.Drawing.Point(397, 254)
        Me.txtTargetTable.Name = "txtTargetTable"
        Me.txtTargetTable.Size = New System.Drawing.Size(249, 20)
        Me.txtTargetTable.TabIndex = 5
        '
        'pnlSourceTable
        '
        Me.pnlSourceTable.Location = New System.Drawing.Point(166, 21)
        Me.pnlSourceTable.Name = "pnlSourceTable"
        Me.pnlSourceTable.Size = New System.Drawing.Size(186, 85)
        Me.pnlSourceTable.TabIndex = 19
        '
        'pnlSourcePrimaryKey
        '
        Me.pnlSourcePrimaryKey.Location = New System.Drawing.Point(3, 21)
        Me.pnlSourcePrimaryKey.Name = "pnlSourcePrimaryKey"
        Me.pnlSourcePrimaryKey.Size = New System.Drawing.Size(28, 85)
        Me.pnlSourcePrimaryKey.TabIndex = 19
        '
        'pnlCompareColumn
        '
        Me.pnlCompareColumn.Location = New System.Drawing.Point(351, 21)
        Me.pnlCompareColumn.Name = "pnlCompareColumn"
        Me.pnlCompareColumn.Size = New System.Drawing.Size(28, 85)
        Me.pnlCompareColumn.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(170, 5)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(143, 13)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Source Column / Field Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(21, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "PK"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(352, 5)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(21, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "CC"
        '
        'pnlSourceDataType
        '
        Me.pnlSourceDataType.Location = New System.Drawing.Point(30, 21)
        Me.pnlSourceDataType.Name = "pnlSourceDataType"
        Me.pnlSourceDataType.Size = New System.Drawing.Size(137, 85)
        Me.pnlSourceDataType.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(37, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Data Type"
        '
        'lstSourceTables
        '
        Me.lstSourceTables.FormattingEnabled = True
        Me.lstSourceTables.HorizontalScrollbar = True
        Me.lstSourceTables.Location = New System.Drawing.Point(56, 254)
        Me.lstSourceTables.Name = "lstSourceTables"
        Me.lstSourceTables.Size = New System.Drawing.Size(242, 82)
        Me.lstSourceTables.TabIndex = 1
        Me.lstSourceTables.TabStop = False
        Me.lstSourceTables.Visible = False
        '
        'btnCrawlSourceTables
        '
        Me.btnCrawlSourceTables.BackgroundImage = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnCrawlSourceTables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCrawlSourceTables.Location = New System.Drawing.Point(311, 255)
        Me.btnCrawlSourceTables.Name = "btnCrawlSourceTables"
        Me.btnCrawlSourceTables.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlSourceTables.TabIndex = 3
        Me.btnCrawlSourceTables.Text = "..."
        '
        'lblStatusText
        '
        Me.lblStatusText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusText.Location = New System.Drawing.Point(70, 712)
        Me.lblStatusText.Name = "lblStatusText"
        Me.lblStatusText.Size = New System.Drawing.Size(692, 16)
        Me.lblStatusText.TabIndex = 148
        '
        'lstTargetTables
        '
        Me.lstTargetTables.FormattingEnabled = True
        Me.lstTargetTables.HorizontalScrollbar = True
        Me.lstTargetTables.Location = New System.Drawing.Point(397, 254)
        Me.lstTargetTables.Name = "lstTargetTables"
        Me.lstTargetTables.Size = New System.Drawing.Size(242, 82)
        Me.lstTargetTables.TabIndex = 4
        Me.lstTargetTables.TabStop = False
        Me.lstTargetTables.Visible = False
        '
        'btnCrawlTargetTables
        '
        Me.btnCrawlTargetTables.BackgroundImage = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnCrawlTargetTables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCrawlTargetTables.Location = New System.Drawing.Point(651, 255)
        Me.btnCrawlTargetTables.Name = "btnCrawlTargetTables"
        Me.btnCrawlTargetTables.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlTargetTables.TabIndex = 6
        Me.btnCrawlTargetTables.Text = "..."
        '
        'btnImportTables
        '
        Me.btnImportTables.Location = New System.Drawing.Point(276, 295)
        Me.btnImportTables.Name = "btnImportTables"
        Me.btnImportTables.Size = New System.Drawing.Size(164, 23)
        Me.btnImportTables.TabIndex = 7
        Me.btnImportTables.Text = "Import Table Structure(s)"
        Me.btnImportTables.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(382, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 13)
        Me.Label4.TabIndex = 153
        Me.Label4.Text = "Target Column / Field Name"
        '
        'pnlTargetTable
        '
        Me.pnlTargetTable.Location = New System.Drawing.Point(378, 21)
        Me.pnlTargetTable.Name = "pnlTargetTable"
        Me.pnlTargetTable.Size = New System.Drawing.Size(186, 85)
        Me.pnlTargetTable.TabIndex = 152
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(570, 5)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 155
        Me.Label9.Text = "Data Type"
        '
        'pnlTargetDataType
        '
        Me.pnlTargetDataType.Location = New System.Drawing.Point(563, 21)
        Me.pnlTargetDataType.Name = "pnlTargetDataType"
        Me.pnlTargetDataType.Size = New System.Drawing.Size(137, 85)
        Me.pnlTargetDataType.TabIndex = 154
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(700, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(21, 13)
        Me.Label10.TabIndex = 157
        Me.Label10.Text = "PK"
        '
        'pnlTargetPrimaryKey
        '
        Me.pnlTargetPrimaryKey.Location = New System.Drawing.Point(699, 21)
        Me.pnlTargetPrimaryKey.Name = "pnlTargetPrimaryKey"
        Me.pnlTargetPrimaryKey.Size = New System.Drawing.Size(28, 85)
        Me.pnlTargetPrimaryKey.TabIndex = 156
        '
        'pnlMain
        '
        Me.pnlMain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlMain.AutoScroll = True
        Me.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlMain.Controls.Add(Me.Label8)
        Me.pnlMain.Controls.Add(Me.pnlSourceTable)
        Me.pnlMain.Controls.Add(Me.Label10)
        Me.pnlMain.Controls.Add(Me.pnlSourcePrimaryKey)
        Me.pnlMain.Controls.Add(Me.pnlTargetPrimaryKey)
        Me.pnlMain.Controls.Add(Me.pnlCompareColumn)
        Me.pnlMain.Controls.Add(Me.Label9)
        Me.pnlMain.Controls.Add(Me.Label5)
        Me.pnlMain.Controls.Add(Me.pnlTargetDataType)
        Me.pnlMain.Controls.Add(Me.Label6)
        Me.pnlMain.Controls.Add(Me.Label4)
        Me.pnlMain.Controls.Add(Me.Label7)
        Me.pnlMain.Controls.Add(Me.pnlTargetTable)
        Me.pnlMain.Controls.Add(Me.pnlSourceDataType)
        Me.pnlMain.Location = New System.Drawing.Point(12, 358)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(750, 321)
        Me.pnlMain.TabIndex = 158
        '
        'rbnSourceConfig
        '
        Me.rbnSourceConfig.AutoSize = True
        Me.rbnSourceConfig.Checked = True
        Me.rbnSourceConfig.Location = New System.Drawing.Point(10, 19)
        Me.rbnSourceConfig.Name = "rbnSourceConfig"
        Me.rbnSourceConfig.Size = New System.Drawing.Size(176, 17)
        Me.rbnSourceConfig.TabIndex = 0
        Me.rbnSourceConfig.TabStop = True
        Me.rbnSourceConfig.Text = "Use Source Table Configuration"
        Me.rbnSourceConfig.UseVisualStyleBackColor = True
        '
        'rbnTargetConfig
        '
        Me.rbnTargetConfig.AutoSize = True
        Me.rbnTargetConfig.Enabled = False
        Me.rbnTargetConfig.Location = New System.Drawing.Point(10, 42)
        Me.rbnTargetConfig.Name = "rbnTargetConfig"
        Me.rbnTargetConfig.Size = New System.Drawing.Size(173, 17)
        Me.rbnTargetConfig.TabIndex = 1
        Me.rbnTargetConfig.Text = "Use Target Table Configuration"
        Me.rbnTargetConfig.UseVisualStyleBackColor = True
        '
        'btnSaveConfiguration
        '
        Me.btnSaveConfiguration.Location = New System.Drawing.Point(10, 161)
        Me.btnSaveConfiguration.Name = "btnSaveConfiguration"
        Me.btnSaveConfiguration.Size = New System.Drawing.Size(171, 23)
        Me.btnSaveConfiguration.TabIndex = 5
        Me.btnSaveConfiguration.Text = "Save Configuration"
        Me.btnSaveConfiguration.UseVisualStyleBackColor = True
        '
        'lblStatusTitle
        '
        Me.lblStatusTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatusTitle.AutoSize = True
        Me.lblStatusTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusTitle.Location = New System.Drawing.Point(9, 712)
        Me.lblStatusTitle.Name = "lblStatusTitle"
        Me.lblStatusTitle.Size = New System.Drawing.Size(55, 16)
        Me.lblStatusTitle.TabIndex = 162
        Me.lblStatusTitle.Text = "Status:"
        '
        'btnCreateSmartUpdateTable
        '
        Me.btnCreateSmartUpdateTable.Location = New System.Drawing.Point(10, 17)
        Me.btnCreateSmartUpdateTable.Name = "btnCreateSmartUpdateTable"
        Me.btnCreateSmartUpdateTable.Size = New System.Drawing.Size(171, 23)
        Me.btnCreateSmartUpdateTable.TabIndex = 0
        Me.btnCreateSmartUpdateTable.Text = "Create SmartUpdate Table"
        Me.btnCreateSmartUpdateTable.UseVisualStyleBackColor = True
        '
        'btnCreateSmartUpdateProcedure
        '
        Me.btnCreateSmartUpdateProcedure.Enabled = False
        Me.btnCreateSmartUpdateProcedure.Location = New System.Drawing.Point(10, 48)
        Me.btnCreateSmartUpdateProcedure.Name = "btnCreateSmartUpdateProcedure"
        Me.btnCreateSmartUpdateProcedure.Size = New System.Drawing.Size(171, 23)
        Me.btnCreateSmartUpdateProcedure.TabIndex = 1
        Me.btnCreateSmartUpdateProcedure.Text = "Create SmartUpdate Procedure"
        Me.btnCreateSmartUpdateProcedure.UseVisualStyleBackColor = True
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CustomFormat = "yyyy-MM-dd"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(85, 88)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpStartDate.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(10, 94)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 13)
        Me.Label12.TabIndex = 166
        Me.Label12.Text = "Start Date"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 117)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 167
        Me.Label13.Text = "End Date"
        '
        'dtpEndDate
        '
        Me.dtpEndDate.CustomFormat = " "
        Me.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndDate.Location = New System.Drawing.Point(85, 111)
        Me.dtpEndDate.Name = "dtpEndDate"
        Me.dtpEndDate.Size = New System.Drawing.Size(96, 20)
        Me.dtpEndDate.TabIndex = 3
        '
        'chkNoEndDate
        '
        Me.chkNoEndDate.AutoSize = True
        Me.chkNoEndDate.Checked = True
        Me.chkNoEndDate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNoEndDate.Location = New System.Drawing.Point(85, 137)
        Me.chkNoEndDate.Name = "chkNoEndDate"
        Me.chkNoEndDate.Size = New System.Drawing.Size(88, 17)
        Me.chkNoEndDate.TabIndex = 4
        Me.chkNoEndDate.Text = "No End Date"
        Me.chkNoEndDate.UseVisualStyleBackColor = True
        '
        'lblSmartUpdateCommand
        '
        Me.lblSmartUpdateCommand.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSmartUpdateCommand.AutoSize = True
        Me.lblSmartUpdateCommand.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmartUpdateCommand.Location = New System.Drawing.Point(9, 689)
        Me.lblSmartUpdateCommand.Name = "lblSmartUpdateCommand"
        Me.lblSmartUpdateCommand.Size = New System.Drawing.Size(122, 13)
        Me.lblSmartUpdateCommand.TabIndex = 171
        Me.lblSmartUpdateCommand.Text = "SmartUpdate Command:"
        '
        'txtSmartUpdateCommand
        '
        Me.txtSmartUpdateCommand.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSmartUpdateCommand.Location = New System.Drawing.Point(137, 686)
        Me.txtSmartUpdateCommand.Name = "txtSmartUpdateCommand"
        Me.txtSmartUpdateCommand.ReadOnly = True
        Me.txtSmartUpdateCommand.Size = New System.Drawing.Size(625, 20)
        Me.txtSmartUpdateCommand.TabIndex = 8
        '
        'btnAddSmartUpdateSchedule
        '
        Me.btnAddSmartUpdateSchedule.Location = New System.Drawing.Point(6, 157)
        Me.btnAddSmartUpdateSchedule.Name = "btnAddSmartUpdateSchedule"
        Me.btnAddSmartUpdateSchedule.Size = New System.Drawing.Size(171, 23)
        Me.btnAddSmartUpdateSchedule.TabIndex = 5
        Me.btnAddSmartUpdateSchedule.Text = "Add to SmartUpdate Schedule"
        Me.btnAddSmartUpdateSchedule.UseVisualStyleBackColor = True
        '
        'lblLicenseRequired
        '
        Me.lblLicenseRequired.AutoSize = True
        Me.lblLicenseRequired.Location = New System.Drawing.Point(10, 74)
        Me.lblLicenseRequired.Name = "lblLicenseRequired"
        Me.lblLicenseRequired.Size = New System.Drawing.Size(185, 13)
        Me.lblLicenseRequired.TabIndex = 174
        Me.lblLicenseRequired.Text = "A License is required for SmartUpdate"
        '
        'grpConfiguration
        '
        Me.grpConfiguration.Controls.Add(Me.Label11)
        Me.grpConfiguration.Controls.Add(Me.rbnSourceConfig)
        Me.grpConfiguration.Controls.Add(Me.rbnTargetConfig)
        Me.grpConfiguration.Controls.Add(Me.btnSaveConfiguration)
        Me.grpConfiguration.Controls.Add(Me.dtpStartDate)
        Me.grpConfiguration.Controls.Add(Me.Label12)
        Me.grpConfiguration.Controls.Add(Me.chkNoEndDate)
        Me.grpConfiguration.Controls.Add(Me.Label13)
        Me.grpConfiguration.Controls.Add(Me.dtpEndDate)
        Me.grpConfiguration.Location = New System.Drawing.Point(768, 169)
        Me.grpConfiguration.Name = "grpConfiguration"
        Me.grpConfiguration.Size = New System.Drawing.Size(226, 196)
        Me.grpConfiguration.TabIndex = 175
        Me.grpConfiguration.TabStop = False
        Me.grpConfiguration.Text = "Configuration"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 62)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(152, 13)
        Me.Label11.TabIndex = 170
        Me.Label11.Text = "Source has priority over Target"
        '
        'grpCommand
        '
        Me.grpCommand.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCommand.Controls.Add(Me.chkClearTargetTable)
        Me.grpCommand.Controls.Add(Me.btnAddSmartUpdateSchedule)
        Me.grpCommand.Controls.Add(Me.chkCreateTargetTable)
        Me.grpCommand.Controls.Add(Me.chkCreateAuditTable)
        Me.grpCommand.Controls.Add(Me.chkUseAuditing)
        Me.grpCommand.Controls.Add(Me.chkRemoveNonSourceData)
        Me.grpCommand.Controls.Add(Me.chkUseTargetCollation)
        Me.grpCommand.Location = New System.Drawing.Point(768, 528)
        Me.grpCommand.Name = "grpCommand"
        Me.grpCommand.Size = New System.Drawing.Size(226, 190)
        Me.grpCommand.TabIndex = 176
        Me.grpCommand.TabStop = False
        Me.grpCommand.Text = "SmartUpdateCommand"
        '
        'chkClearTargetTable
        '
        Me.chkClearTargetTable.AutoSize = True
        Me.chkClearTargetTable.Location = New System.Drawing.Point(6, 134)
        Me.chkClearTargetTable.Name = "chkClearTargetTable"
        Me.chkClearTargetTable.Size = New System.Drawing.Size(189, 17)
        Me.chkClearTargetTable.TabIndex = 6
        Me.chkClearTargetTable.Text = "Clear Target Table before Transfer"
        Me.chkClearTargetTable.UseVisualStyleBackColor = True
        '
        'grpCreateSQL
        '
        Me.grpCreateSQL.Controls.Add(Me.btnCreateSmartUpdateTable)
        Me.grpCreateSQL.Controls.Add(Me.btnCreateSmartUpdateProcedure)
        Me.grpCreateSQL.Controls.Add(Me.lblLicenseRequired)
        Me.grpCreateSQL.Location = New System.Drawing.Point(768, 58)
        Me.grpCreateSQL.Name = "grpCreateSQL"
        Me.grpCreateSQL.Size = New System.Drawing.Size(226, 105)
        Me.grpCreateSQL.TabIndex = 177
        Me.grpCreateSQL.TabStop = False
        Me.grpCreateSQL.Text = "Create SQL Components"
        '
        'grpCreateLocalView
        '
        Me.grpCreateLocalView.Controls.Add(Me.rtbInfoSourceDatabase)
        Me.grpCreateLocalView.Controls.Add(Me.rtbInfoLinkedServer)
        Me.grpCreateLocalView.Controls.Add(Me.lblInfoLocalView)
        Me.grpCreateLocalView.Controls.Add(Me.lblInfoLocalSchema)
        Me.grpCreateLocalView.Controls.Add(Me.lblInfoSourceDatabase)
        Me.grpCreateLocalView.Controls.Add(Me.lblInfoLinkedServer)
        Me.grpCreateLocalView.Controls.Add(Me.btnCreateLocalView)
        Me.grpCreateLocalView.Controls.Add(Me.lblLocalView)
        Me.grpCreateLocalView.Controls.Add(Me.txtLocalView)
        Me.grpCreateLocalView.Controls.Add(Me.txtLocalSchema)
        Me.grpCreateLocalView.Controls.Add(Me.lblLocalSchema)
        Me.grpCreateLocalView.Controls.Add(Me.lblSourceTableOrView)
        Me.grpCreateLocalView.Controls.Add(Me.txtSourceTableOrView)
        Me.grpCreateLocalView.Controls.Add(Me.txtSourceSchema)
        Me.grpCreateLocalView.Controls.Add(Me.lblSourceSchema)
        Me.grpCreateLocalView.Controls.Add(Me.txtSourceDatabase)
        Me.grpCreateLocalView.Controls.Add(Me.lblSourceDatabase)
        Me.grpCreateLocalView.Controls.Add(Me.lblLinkedServer)
        Me.grpCreateLocalView.Controls.Add(Me.cbxLinkedServer)
        Me.grpCreateLocalView.Location = New System.Drawing.Point(12, 58)
        Me.grpCreateLocalView.Name = "grpCreateLocalView"
        Me.grpCreateLocalView.Size = New System.Drawing.Size(750, 159)
        Me.grpCreateLocalView.TabIndex = 178
        Me.grpCreateLocalView.TabStop = False
        Me.grpCreateLocalView.Text = "Create Local View on Remote Table"
        '
        'rtbInfoSourceDatabase
        '
        Me.rtbInfoSourceDatabase.BackColor = System.Drawing.SystemColors.Info
        Me.rtbInfoSourceDatabase.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbInfoSourceDatabase.Location = New System.Drawing.Point(329, 47)
        Me.rtbInfoSourceDatabase.Name = "rtbInfoSourceDatabase"
        Me.rtbInfoSourceDatabase.ReadOnly = True
        Me.rtbInfoSourceDatabase.Size = New System.Drawing.Size(164, 82)
        Me.rtbInfoSourceDatabase.TabIndex = 185
        Me.rtbInfoSourceDatabase.Text = "If the database name is embedded in the Linked Server, leave this field blank. If" & _
    " you want to connect to a database on the local server, enter the name of that d" & _
    "atabase."
        Me.rtbInfoSourceDatabase.Visible = False
        '
        'rtbInfoLinkedServer
        '
        Me.rtbInfoLinkedServer.BackColor = System.Drawing.SystemColors.Info
        Me.rtbInfoLinkedServer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbInfoLinkedServer.Location = New System.Drawing.Point(330, 20)
        Me.rtbInfoLinkedServer.Name = "rtbInfoLinkedServer"
        Me.rtbInfoLinkedServer.ReadOnly = True
        Me.rtbInfoLinkedServer.Size = New System.Drawing.Size(152, 54)
        Me.rtbInfoLinkedServer.TabIndex = 184
        Me.rtbInfoLinkedServer.Text = "You need a Linked Server to connect to a remote database. If your database is on " & _
    "the local server, leave this field blank."
        Me.rtbInfoLinkedServer.Visible = False
        '
        'lblInfoLocalView
        '
        Me.lblInfoLocalView.Image = Global.Sequenchel.My.Resources.Resources.info
        Me.lblInfoLocalView.Location = New System.Drawing.Point(718, 98)
        Me.lblInfoLocalView.Name = "lblInfoLocalView"
        Me.lblInfoLocalView.Size = New System.Drawing.Size(20, 20)
        Me.lblInfoLocalView.TabIndex = 183
        '
        'lblInfoLocalSchema
        '
        Me.lblInfoLocalSchema.Image = Global.Sequenchel.My.Resources.Resources.info
        Me.lblInfoLocalSchema.Location = New System.Drawing.Point(718, 72)
        Me.lblInfoLocalSchema.Name = "lblInfoLocalSchema"
        Me.lblInfoLocalSchema.Size = New System.Drawing.Size(20, 20)
        Me.lblInfoLocalSchema.TabIndex = 182
        '
        'lblInfoSourceDatabase
        '
        Me.lblInfoSourceDatabase.Image = Global.Sequenchel.My.Resources.Resources.info
        Me.lblInfoSourceDatabase.Location = New System.Drawing.Point(328, 47)
        Me.lblInfoSourceDatabase.Name = "lblInfoSourceDatabase"
        Me.lblInfoSourceDatabase.Size = New System.Drawing.Size(20, 20)
        Me.lblInfoSourceDatabase.TabIndex = 181
        '
        'lblInfoLinkedServer
        '
        Me.lblInfoLinkedServer.Image = Global.Sequenchel.My.Resources.Resources.info
        Me.lblInfoLinkedServer.Location = New System.Drawing.Point(328, 20)
        Me.lblInfoLinkedServer.Name = "lblInfoLinkedServer"
        Me.lblInfoLinkedServer.Size = New System.Drawing.Size(20, 20)
        Me.lblInfoLinkedServer.TabIndex = 180
        '
        'btnCreateLocalView
        '
        Me.btnCreateLocalView.Location = New System.Drawing.Point(264, 129)
        Me.btnCreateLocalView.Name = "btnCreateLocalView"
        Me.btnCreateLocalView.Size = New System.Drawing.Size(164, 23)
        Me.btnCreateLocalView.TabIndex = 179
        Me.btnCreateLocalView.Text = "Create Local View"
        Me.btnCreateLocalView.UseVisualStyleBackColor = True
        '
        'lblLocalView
        '
        Me.lblLocalView.AutoSize = True
        Me.lblLocalView.Location = New System.Drawing.Point(395, 101)
        Me.lblLocalView.Name = "lblLocalView"
        Me.lblLocalView.Size = New System.Drawing.Size(59, 13)
        Me.lblLocalView.TabIndex = 11
        Me.lblLocalView.Text = "Local View"
        '
        'txtLocalView
        '
        Me.txtLocalView.Location = New System.Drawing.Point(541, 98)
        Me.txtLocalView.Name = "txtLocalView"
        Me.txtLocalView.Size = New System.Drawing.Size(171, 20)
        Me.txtLocalView.TabIndex = 10
        '
        'txtLocalSchema
        '
        Me.txtLocalSchema.Location = New System.Drawing.Point(541, 72)
        Me.txtLocalSchema.Name = "txtLocalSchema"
        Me.txtLocalSchema.Size = New System.Drawing.Size(171, 20)
        Me.txtLocalSchema.TabIndex = 9
        '
        'lblLocalSchema
        '
        Me.lblLocalSchema.AutoSize = True
        Me.lblLocalSchema.Location = New System.Drawing.Point(395, 75)
        Me.lblLocalSchema.Name = "lblLocalSchema"
        Me.lblLocalSchema.Size = New System.Drawing.Size(75, 13)
        Me.lblLocalSchema.TabIndex = 8
        Me.lblLocalSchema.Text = "Local Schema"
        '
        'lblSourceTableOrView
        '
        Me.lblSourceTableOrView.AutoSize = True
        Me.lblSourceTableOrView.Location = New System.Drawing.Point(6, 101)
        Me.lblSourceTableOrView.Name = "lblSourceTableOrView"
        Me.lblSourceTableOrView.Size = New System.Drawing.Size(116, 13)
        Me.lblSourceTableOrView.TabIndex = 7
        Me.lblSourceTableOrView.Text = "Source Table or View *"
        '
        'txtSourceTableOrView
        '
        Me.txtSourceTableOrView.Location = New System.Drawing.Point(152, 98)
        Me.txtSourceTableOrView.Name = "txtSourceTableOrView"
        Me.txtSourceTableOrView.Size = New System.Drawing.Size(171, 20)
        Me.txtSourceTableOrView.TabIndex = 6
        '
        'txtSourceSchema
        '
        Me.txtSourceSchema.Location = New System.Drawing.Point(152, 72)
        Me.txtSourceSchema.Name = "txtSourceSchema"
        Me.txtSourceSchema.Size = New System.Drawing.Size(171, 20)
        Me.txtSourceSchema.TabIndex = 5
        '
        'lblSourceSchema
        '
        Me.lblSourceSchema.AutoSize = True
        Me.lblSourceSchema.Location = New System.Drawing.Point(6, 75)
        Me.lblSourceSchema.Name = "lblSourceSchema"
        Me.lblSourceSchema.Size = New System.Drawing.Size(90, 13)
        Me.lblSourceSchema.TabIndex = 4
        Me.lblSourceSchema.Text = "Source Schema *"
        '
        'txtSourceDatabase
        '
        Me.txtSourceDatabase.Location = New System.Drawing.Point(152, 46)
        Me.txtSourceDatabase.Name = "txtSourceDatabase"
        Me.txtSourceDatabase.Size = New System.Drawing.Size(171, 20)
        Me.txtSourceDatabase.TabIndex = 3
        '
        'lblSourceDatabase
        '
        Me.lblSourceDatabase.AutoSize = True
        Me.lblSourceDatabase.Location = New System.Drawing.Point(6, 49)
        Me.lblSourceDatabase.Name = "lblSourceDatabase"
        Me.lblSourceDatabase.Size = New System.Drawing.Size(90, 13)
        Me.lblSourceDatabase.TabIndex = 2
        Me.lblSourceDatabase.Text = "Source Database"
        '
        'lblLinkedServer
        '
        Me.lblLinkedServer.AutoSize = True
        Me.lblLinkedServer.Location = New System.Drawing.Point(6, 20)
        Me.lblLinkedServer.Name = "lblLinkedServer"
        Me.lblLinkedServer.Size = New System.Drawing.Size(73, 13)
        Me.lblLinkedServer.TabIndex = 1
        Me.lblLinkedServer.Text = "Linked Server"
        '
        'cbxLinkedServer
        '
        Me.cbxLinkedServer.FormattingEnabled = True
        Me.cbxLinkedServer.Location = New System.Drawing.Point(152, 19)
        Me.cbxLinkedServer.Name = "cbxLinkedServer"
        Me.cbxLinkedServer.Size = New System.Drawing.Size(171, 21)
        Me.cbxLinkedServer.TabIndex = 0
        '
        'rtbInfoLocalSchema
        '
        Me.rtbInfoLocalSchema.BackColor = System.Drawing.SystemColors.Info
        Me.rtbInfoLocalSchema.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbInfoLocalSchema.Location = New System.Drawing.Point(732, 131)
        Me.rtbInfoLocalSchema.Name = "rtbInfoLocalSchema"
        Me.rtbInfoLocalSchema.ReadOnly = True
        Me.rtbInfoLocalSchema.Size = New System.Drawing.Size(164, 82)
        Me.rtbInfoLocalSchema.TabIndex = 186
        Me.rtbInfoLocalSchema.Text = "The schema where the new View will be created. If you leave this field blank, the" & _
    " source schema name will be used. Make sure the schema exists before creating th" & _
    "e view."
        Me.rtbInfoLocalSchema.Visible = False
        '
        'rtbLocalView
        '
        Me.rtbLocalView.BackColor = System.Drawing.SystemColors.Info
        Me.rtbLocalView.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbLocalView.Location = New System.Drawing.Point(732, 156)
        Me.rtbLocalView.Name = "rtbLocalView"
        Me.rtbLocalView.ReadOnly = True
        Me.rtbLocalView.Size = New System.Drawing.Size(164, 82)
        Me.rtbLocalView.TabIndex = 187
        Me.rtbLocalView.Text = "The name of the View to be created. If you leave this field blank the name will b" & _
    "e ""vw_"" with the name of the source Table or View appended."
        Me.rtbLocalView.Visible = False
        '
        'frmSmartUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 738)
        Me.Controls.Add(Me.rtbLocalView)
        Me.Controls.Add(Me.rtbInfoLocalSchema)
        Me.Controls.Add(Me.grpCreateLocalView)
        Me.Controls.Add(Me.grpCreateSQL)
        Me.Controls.Add(Me.grpCommand)
        Me.Controls.Add(Me.grpConfiguration)
        Me.Controls.Add(Me.txtSmartUpdateCommand)
        Me.Controls.Add(Me.lblSmartUpdateCommand)
        Me.Controls.Add(Me.lblStatusTitle)
        Me.Controls.Add(Me.lstTargetTables)
        Me.Controls.Add(Me.lstSourceTables)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.btnImportTables)
        Me.Controls.Add(Me.btnCrawlTargetTables)
        Me.Controls.Add(Me.lblStatusText)
        Me.Controls.Add(Me.btnCrawlSourceTables)
        Me.Controls.Add(Me.txtTargetTable)
        Me.Controls.Add(Me.txtSourceTable)
        Me.Controls.Add(Me.cbxConnection)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTargetTable)
        Me.Controls.Add(Me.lblSourceTable)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(1025, 5000)
        Me.MinimumSize = New System.Drawing.Size(1025, 595)
        Me.Name = "frmSmartUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SmartUpdate"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.grpConfiguration.ResumeLayout(False)
        Me.grpConfiguration.PerformLayout()
        Me.grpCommand.ResumeLayout(False)
        Me.grpCommand.PerformLayout()
        Me.grpCreateSQL.ResumeLayout(False)
        Me.grpCreateSQL.PerformLayout()
        Me.grpCreateLocalView.ResumeLayout(False)
        Me.grpCreateLocalView.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSourceTable As System.Windows.Forms.Label
    Friend WithEvents lblTargetTable As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbxConnection As System.Windows.Forms.ComboBox
    Friend WithEvents chkCreateTargetTable As System.Windows.Forms.CheckBox
    Friend WithEvents chkCreateAuditTable As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseAuditing As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemoveNonSourceData As System.Windows.Forms.CheckBox
    Friend WithEvents chkUseTargetCollation As System.Windows.Forms.CheckBox
    Friend WithEvents txtSourceTable As System.Windows.Forms.TextBox
    Friend WithEvents txtTargetTable As System.Windows.Forms.TextBox
    Friend WithEvents pnlSourceTable As System.Windows.Forms.Panel
    Friend WithEvents pnlSourcePrimaryKey As System.Windows.Forms.Panel
    Friend WithEvents pnlCompareColumn As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents pnlSourceDataType As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lstSourceTables As System.Windows.Forms.ListBox
    Friend WithEvents btnCrawlSourceTables As System.Windows.Forms.Button
    Friend WithEvents lblStatusText As System.Windows.Forms.Label
    Friend WithEvents lstTargetTables As System.Windows.Forms.ListBox
    Friend WithEvents btnCrawlTargetTables As System.Windows.Forms.Button
    Friend WithEvents btnImportTables As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents pnlTargetTable As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlTargetDataType As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnlTargetPrimaryKey As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents rbnSourceConfig As System.Windows.Forms.RadioButton
    Friend WithEvents rbnTargetConfig As System.Windows.Forms.RadioButton
    Friend WithEvents btnSaveConfiguration As System.Windows.Forms.Button
    Friend WithEvents lblStatusTitle As System.Windows.Forms.Label
    Friend WithEvents btnCreateSmartUpdateTable As System.Windows.Forms.Button
    Friend WithEvents btnCreateSmartUpdateProcedure As System.Windows.Forms.Button
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkNoEndDate As System.Windows.Forms.CheckBox
    Friend WithEvents lblSmartUpdateCommand As System.Windows.Forms.Label
    Friend WithEvents txtSmartUpdateCommand As System.Windows.Forms.TextBox
    Friend WithEvents btnAddSmartUpdateSchedule As System.Windows.Forms.Button
    Friend WithEvents lblLicenseRequired As System.Windows.Forms.Label
    Friend WithEvents grpConfiguration As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents grpCommand As System.Windows.Forms.GroupBox
    Friend WithEvents grpCreateSQL As System.Windows.Forms.GroupBox
    Friend WithEvents chkClearTargetTable As System.Windows.Forms.CheckBox
    Friend WithEvents grpCreateLocalView As System.Windows.Forms.GroupBox
    Friend WithEvents btnCreateLocalView As System.Windows.Forms.Button
    Friend WithEvents lblLocalView As System.Windows.Forms.Label
    Friend WithEvents txtLocalView As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalSchema As System.Windows.Forms.TextBox
    Friend WithEvents lblLocalSchema As System.Windows.Forms.Label
    Friend WithEvents lblSourceTableOrView As System.Windows.Forms.Label
    Friend WithEvents txtSourceTableOrView As System.Windows.Forms.TextBox
    Friend WithEvents txtSourceSchema As System.Windows.Forms.TextBox
    Friend WithEvents lblSourceSchema As System.Windows.Forms.Label
    Friend WithEvents txtSourceDatabase As System.Windows.Forms.TextBox
    Friend WithEvents lblSourceDatabase As System.Windows.Forms.Label
    Friend WithEvents lblLinkedServer As System.Windows.Forms.Label
    Friend WithEvents cbxLinkedServer As System.Windows.Forms.ComboBox
    Friend WithEvents lblInfoLinkedServer As System.Windows.Forms.Label
    Friend WithEvents rtbInfoSourceDatabase As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbInfoLinkedServer As System.Windows.Forms.RichTextBox
    Friend WithEvents lblInfoLocalView As System.Windows.Forms.Label
    Friend WithEvents lblInfoLocalSchema As System.Windows.Forms.Label
    Friend WithEvents lblInfoSourceDatabase As System.Windows.Forms.Label
    Friend WithEvents rtbInfoLocalSchema As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbLocalView As System.Windows.Forms.RichTextBox
End Class
