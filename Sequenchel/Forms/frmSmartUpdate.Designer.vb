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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.grpCreateSQL = New System.Windows.Forms.GroupBox()
        Me.pnlMain.SuspendLayout()
        Me.grpConfiguration.SuspendLayout()
        Me.grpCommand.SuspendLayout()
        Me.grpCreateSQL.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(53, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Source Table or View"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(394, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Target Table"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(54, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Connection"
        '
        'cbxConnection
        '
        Me.cbxConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxConnection.FormattingEnabled = True
        Me.cbxConnection.Location = New System.Drawing.Point(121, 21)
        Me.cbxConnection.Name = "cbxConnection"
        Me.cbxConnection.Size = New System.Drawing.Size(185, 21)
        Me.cbxConnection.TabIndex = 3
        '
        'chkCreateTargetTable
        '
        Me.chkCreateTargetTable.AutoSize = True
        Me.chkCreateTargetTable.Location = New System.Drawing.Point(6, 19)
        Me.chkCreateTargetTable.Name = "chkCreateTargetTable"
        Me.chkCreateTargetTable.Size = New System.Drawing.Size(171, 17)
        Me.chkCreateTargetTable.TabIndex = 5
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
        Me.chkCreateAuditTable.TabIndex = 6
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
        Me.chkUseAuditing.TabIndex = 7
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
        Me.chkRemoveNonSourceData.TabIndex = 8
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
        Me.chkUseTargetCollation.TabIndex = 9
        Me.chkUseTargetCollation.Text = "Use Target Database Collation"
        Me.chkUseTargetCollation.UseVisualStyleBackColor = True
        '
        'txtSourceTable
        '
        Me.txtSourceTable.Location = New System.Drawing.Point(56, 74)
        Me.txtSourceTable.Name = "txtSourceTable"
        Me.txtSourceTable.ReadOnly = True
        Me.txtSourceTable.Size = New System.Drawing.Size(249, 20)
        Me.txtSourceTable.TabIndex = 10
        '
        'txtTargetTable
        '
        Me.txtTargetTable.Location = New System.Drawing.Point(397, 74)
        Me.txtTargetTable.Name = "txtTargetTable"
        Me.txtTargetTable.Size = New System.Drawing.Size(249, 20)
        Me.txtTargetTable.TabIndex = 11
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
        Me.lstSourceTables.Location = New System.Drawing.Point(56, 74)
        Me.lstSourceTables.Name = "lstSourceTables"
        Me.lstSourceTables.Size = New System.Drawing.Size(242, 82)
        Me.lstSourceTables.TabIndex = 147
        Me.lstSourceTables.TabStop = False
        Me.lstSourceTables.Visible = False
        '
        'btnCrawlSourceTables
        '
        Me.btnCrawlSourceTables.BackgroundImage = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnCrawlSourceTables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCrawlSourceTables.Location = New System.Drawing.Point(311, 75)
        Me.btnCrawlSourceTables.Name = "btnCrawlSourceTables"
        Me.btnCrawlSourceTables.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlSourceTables.TabIndex = 146
        Me.btnCrawlSourceTables.Text = "..."
        '
        'lblStatusText
        '
        Me.lblStatusText.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatusText.AutoSize = True
        Me.lblStatusText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusText.Location = New System.Drawing.Point(103, 532)
        Me.lblStatusText.Name = "lblStatusText"
        Me.lblStatusText.Size = New System.Drawing.Size(0, 16)
        Me.lblStatusText.TabIndex = 148
        '
        'lstTargetTables
        '
        Me.lstTargetTables.FormattingEnabled = True
        Me.lstTargetTables.HorizontalScrollbar = True
        Me.lstTargetTables.Location = New System.Drawing.Point(397, 74)
        Me.lstTargetTables.Name = "lstTargetTables"
        Me.lstTargetTables.Size = New System.Drawing.Size(242, 82)
        Me.lstTargetTables.TabIndex = 150
        Me.lstTargetTables.TabStop = False
        Me.lstTargetTables.Visible = False
        '
        'btnCrawlTargetTables
        '
        Me.btnCrawlTargetTables.BackgroundImage = Global.Sequenchel.My.Resources.Resources.button_down
        Me.btnCrawlTargetTables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCrawlTargetTables.Location = New System.Drawing.Point(651, 75)
        Me.btnCrawlTargetTables.Name = "btnCrawlTargetTables"
        Me.btnCrawlTargetTables.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlTargetTables.TabIndex = 149
        Me.btnCrawlTargetTables.Text = "..."
        '
        'btnImportTables
        '
        Me.btnImportTables.Location = New System.Drawing.Point(276, 115)
        Me.btnImportTables.Name = "btnImportTables"
        Me.btnImportTables.Size = New System.Drawing.Size(164, 23)
        Me.btnImportTables.TabIndex = 151
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
        Me.pnlMain.Location = New System.Drawing.Point(12, 147)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(750, 352)
        Me.pnlMain.TabIndex = 158
        '
        'rbnSourceConfig
        '
        Me.rbnSourceConfig.AutoSize = True
        Me.rbnSourceConfig.Checked = True
        Me.rbnSourceConfig.Location = New System.Drawing.Point(10, 19)
        Me.rbnSourceConfig.Name = "rbnSourceConfig"
        Me.rbnSourceConfig.Size = New System.Drawing.Size(176, 17)
        Me.rbnSourceConfig.TabIndex = 159
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
        Me.rbnTargetConfig.TabIndex = 160
        Me.rbnTargetConfig.Text = "Use Target Table Configuration"
        Me.rbnTargetConfig.UseVisualStyleBackColor = True
        '
        'btnSaveConfiguration
        '
        Me.btnSaveConfiguration.Location = New System.Drawing.Point(10, 161)
        Me.btnSaveConfiguration.Name = "btnSaveConfiguration"
        Me.btnSaveConfiguration.Size = New System.Drawing.Size(171, 23)
        Me.btnSaveConfiguration.TabIndex = 161
        Me.btnSaveConfiguration.Text = "Save Configuration"
        Me.btnSaveConfiguration.UseVisualStyleBackColor = True
        '
        'lblStatusTitle
        '
        Me.lblStatusTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatusTitle.AutoSize = True
        Me.lblStatusTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusTitle.Location = New System.Drawing.Point(36, 532)
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
        Me.btnCreateSmartUpdateTable.TabIndex = 163
        Me.btnCreateSmartUpdateTable.Text = "Create SmartUpdate Table"
        Me.btnCreateSmartUpdateTable.UseVisualStyleBackColor = True
        '
        'btnCreateSmartUpdateProcedure
        '
        Me.btnCreateSmartUpdateProcedure.Enabled = False
        Me.btnCreateSmartUpdateProcedure.Location = New System.Drawing.Point(10, 48)
        Me.btnCreateSmartUpdateProcedure.Name = "btnCreateSmartUpdateProcedure"
        Me.btnCreateSmartUpdateProcedure.Size = New System.Drawing.Size(171, 23)
        Me.btnCreateSmartUpdateProcedure.TabIndex = 164
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
        Me.dtpStartDate.TabIndex = 165
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
        Me.dtpEndDate.TabIndex = 168
        '
        'chkNoEndDate
        '
        Me.chkNoEndDate.AutoSize = True
        Me.chkNoEndDate.Checked = True
        Me.chkNoEndDate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNoEndDate.Location = New System.Drawing.Point(85, 137)
        Me.chkNoEndDate.Name = "chkNoEndDate"
        Me.chkNoEndDate.Size = New System.Drawing.Size(88, 17)
        Me.chkNoEndDate.TabIndex = 169
        Me.chkNoEndDate.Text = "No End Date"
        Me.chkNoEndDate.UseVisualStyleBackColor = True
        '
        'lblSmartUpdateCommand
        '
        Me.lblSmartUpdateCommand.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSmartUpdateCommand.AutoSize = True
        Me.lblSmartUpdateCommand.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSmartUpdateCommand.Location = New System.Drawing.Point(9, 509)
        Me.lblSmartUpdateCommand.Name = "lblSmartUpdateCommand"
        Me.lblSmartUpdateCommand.Size = New System.Drawing.Size(122, 13)
        Me.lblSmartUpdateCommand.TabIndex = 171
        Me.lblSmartUpdateCommand.Text = "SmartUpdate Command:"
        '
        'txtSmartUpdateCommand
        '
        Me.txtSmartUpdateCommand.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtSmartUpdateCommand.Location = New System.Drawing.Point(137, 506)
        Me.txtSmartUpdateCommand.Name = "txtSmartUpdateCommand"
        Me.txtSmartUpdateCommand.ReadOnly = True
        Me.txtSmartUpdateCommand.Size = New System.Drawing.Size(625, 20)
        Me.txtSmartUpdateCommand.TabIndex = 172
        '
        'btnAddSmartUpdateSchedule
        '
        Me.btnAddSmartUpdateSchedule.Location = New System.Drawing.Point(10, 130)
        Me.btnAddSmartUpdateSchedule.Name = "btnAddSmartUpdateSchedule"
        Me.btnAddSmartUpdateSchedule.Size = New System.Drawing.Size(171, 23)
        Me.btnAddSmartUpdateSchedule.TabIndex = 173
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
        Me.grpCommand.Controls.Add(Me.btnAddSmartUpdateSchedule)
        Me.grpCommand.Controls.Add(Me.chkCreateTargetTable)
        Me.grpCommand.Controls.Add(Me.chkCreateAuditTable)
        Me.grpCommand.Controls.Add(Me.chkUseAuditing)
        Me.grpCommand.Controls.Add(Me.chkRemoveNonSourceData)
        Me.grpCommand.Controls.Add(Me.chkUseTargetCollation)
        Me.grpCommand.Location = New System.Drawing.Point(768, 373)
        Me.grpCommand.Name = "grpCommand"
        Me.grpCommand.Size = New System.Drawing.Size(226, 163)
        Me.grpCommand.TabIndex = 176
        Me.grpCommand.TabStop = False
        Me.grpCommand.Text = "SmartUpdateCommand"
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
        'frmSmartUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 557)
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
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1020, 595)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
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
End Class
