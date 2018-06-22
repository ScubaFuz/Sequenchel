<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmImport
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport))
        Me.btnImportFile = New System.Windows.Forms.Button()
        Me.lblCurrentFile = New System.Windows.Forms.Label()
        Me.txtCurrentFile = New System.Windows.Forms.TextBox()
        Me.btnSelectFile = New System.Windows.Forms.Button()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.chkWinAuth = New System.Windows.Forms.CheckBox()
        Me.lblTable = New System.Windows.Forms.Label()
        Me.lblDatabaseServerName = New System.Windows.Forms.Label()
        Me.lblDatabaseName = New System.Windows.Forms.Label()
        Me.lblFileName = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtTable = New System.Windows.Forms.TextBox()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.chkFile = New System.Windows.Forms.CheckBox()
        Me.chkDatabase = New System.Windows.Forms.CheckBox()
        Me.chkScreen = New System.Windows.Forms.CheckBox()
        Me.chkCreateTable = New System.Windows.Forms.CheckBox()
        Me.btnUploadFile = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblStatusText = New System.Windows.Forms.Label()
        Me.btnPreviousTable = New System.Windows.Forms.Button()
        Me.btnNextTable = New System.Windows.Forms.Button()
        Me.lblTableNumber = New System.Windows.Forms.Label()
        Me.lblTableName = New System.Windows.Forms.Label()
        Me.lblTableNameText = New System.Windows.Forms.Label()
        Me.chkUploadTable = New System.Windows.Forms.CheckBox()
        Me.btnUploadTable = New System.Windows.Forms.Button()
        Me.chkCovertToText = New System.Windows.Forms.CheckBox()
        Me.chkCovertToNull = New System.Windows.Forms.CheckBox()
        Me.chkHasHeaders = New System.Windows.Forms.CheckBox()
        Me.lblTextDelimiter = New System.Windows.Forms.Label()
        Me.txtDelimiter = New System.Windows.Forms.TextBox()
        Me.txtDelimiterShow = New System.Windows.Forms.TextBox()
        Me.btnClearData = New System.Windows.Forms.Button()
        Me.chkClearTarget = New System.Windows.Forms.CheckBox()
        Me.chkUploadAsXml = New System.Windows.Forms.CheckBox()
        Me.chkQuotedValues = New System.Windows.Forms.CheckBox()
        Me.chkLargeFile = New System.Windows.Forms.CheckBox()
        Me.txtBatchSize = New System.Windows.Forms.TextBox()
        Me.grpDatabase = New System.Windows.Forms.GroupBox()
        Me.txtSqlCommand = New System.Windows.Forms.TextBox()
        Me.chkSqlCommand = New System.Windows.Forms.CheckBox()
        Me.grpFileOptions = New System.Windows.Forms.GroupBox()
        Me.txtArchive = New System.Windows.Forms.TextBox()
        Me.chkArchive = New System.Windows.Forms.CheckBox()
        Me.txtFilterRows = New System.Windows.Forms.TextBox()
        Me.chkFilterRows = New System.Windows.Forms.CheckBox()
        Me.nudSkipRows = New System.Windows.Forms.NumericUpDown()
        Me.chkSkipRows = New System.Windows.Forms.CheckBox()
        Me.cbxTextEncoding = New Sequenchel.ComboField()
        Me.lblTextEncoding = New System.Windows.Forms.Label()
        Me.grpUploadFile = New System.Windows.Forms.GroupBox()
        Me.btnUploadFolder = New System.Windows.Forms.Button()
        Me.txtUploadFile = New System.Windows.Forms.TextBox()
        Me.dgvImport = New Sequenchel.usrDataGridView()
        Me.grpDatabase.SuspendLayout()
        Me.grpFileOptions.SuspendLayout()
        CType(Me.nudSkipRows, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpUploadFile.SuspendLayout()
        CType(Me.dgvImport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnImportFile
        '
        Me.btnImportFile.Location = New System.Drawing.Point(571, 14)
        Me.btnImportFile.Name = "btnImportFile"
        Me.btnImportFile.Size = New System.Drawing.Size(171, 23)
        Me.btnImportFile.TabIndex = 4
        Me.btnImportFile.Text = "Import && Upload File"
        Me.btnImportFile.UseVisualStyleBackColor = True
        '
        'lblCurrentFile
        '
        Me.lblCurrentFile.AutoSize = True
        Me.lblCurrentFile.Location = New System.Drawing.Point(26, 19)
        Me.lblCurrentFile.Name = "lblCurrentFile"
        Me.lblCurrentFile.Size = New System.Drawing.Size(54, 13)
        Me.lblCurrentFile.TabIndex = 6
        Me.lblCurrentFile.Text = "File Name"
        '
        'txtCurrentFile
        '
        Me.txtCurrentFile.Location = New System.Drawing.Point(87, 16)
        Me.txtCurrentFile.Name = "txtCurrentFile"
        Me.txtCurrentFile.Size = New System.Drawing.Size(426, 20)
        Me.txtCurrentFile.TabIndex = 1
        '
        'btnSelectFile
        '
        Me.btnSelectFile.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnSelectFile.Location = New System.Drawing.Point(519, 14)
        Me.btnSelectFile.Name = "btnSelectFile"
        Me.btnSelectFile.Size = New System.Drawing.Size(23, 23)
        Me.btnSelectFile.TabIndex = 0
        Me.btnSelectFile.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(327, 62)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 38
        Me.lblPassword.Text = "Password"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(327, 39)
        Me.lblUser.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(60, 13)
        Me.lblUser.TabIndex = 37
        Me.lblUser.Text = "User Name"
        '
        'chkWinAuth
        '
        Me.chkWinAuth.AutoSize = True
        Me.chkWinAuth.Checked = True
        Me.chkWinAuth.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkWinAuth.Location = New System.Drawing.Point(330, 15)
        Me.chkWinAuth.Margin = New System.Windows.Forms.Padding(2)
        Me.chkWinAuth.Name = "chkWinAuth"
        Me.chkWinAuth.Size = New System.Drawing.Size(160, 17)
        Me.chkWinAuth.TabIndex = 12
        Me.chkWinAuth.Text = "Use windows Authentication"
        Me.chkWinAuth.UseVisualStyleBackColor = True
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(5, 61)
        Me.lblTable.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(69, 13)
        Me.lblTable.TabIndex = 35
        Me.lblTable.Text = "Table Name*"
        '
        'lblDatabaseServerName
        '
        Me.lblDatabaseServerName.AutoSize = True
        Me.lblDatabaseServerName.Location = New System.Drawing.Point(5, 16)
        Me.lblDatabaseServerName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDatabaseServerName.Name = "lblDatabaseServerName"
        Me.lblDatabaseServerName.Size = New System.Drawing.Size(122, 13)
        Me.lblDatabaseServerName.TabIndex = 34
        Me.lblDatabaseServerName.Text = "Database Server Name*"
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.Location = New System.Drawing.Point(5, 38)
        Me.lblDatabaseName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Size = New System.Drawing.Size(86, 13)
        Me.lblDatabaseName.TabIndex = 33
        Me.lblDatabaseName.Text = "Database name*"
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Location = New System.Drawing.Point(5, 17)
        Me.lblFileName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(100, 13)
        Me.lblFileName.TabIndex = 32
        Me.lblFileName.Text = "File Path and Name"
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(392, 59)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(108, 20)
        Me.txtPassword.TabIndex = 14
        '
        'txtUser
        '
        Me.txtUser.Enabled = False
        Me.txtUser.Location = New System.Drawing.Point(392, 35)
        Me.txtUser.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(108, 20)
        Me.txtUser.TabIndex = 13
        '
        'txtTable
        '
        Me.txtTable.Location = New System.Drawing.Point(127, 59)
        Me.txtTable.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTable.Name = "txtTable"
        Me.txtTable.Size = New System.Drawing.Size(170, 20)
        Me.txtTable.TabIndex = 11
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(127, 36)
        Me.txtDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(170, 20)
        Me.txtDatabase.TabIndex = 10
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(127, 13)
        Me.txtServer.Margin = New System.Windows.Forms.Padding(2)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(170, 20)
        Me.txtServer.TabIndex = 9
        '
        'chkFile
        '
        Me.chkFile.AutoSize = True
        Me.chkFile.Location = New System.Drawing.Point(34, 274)
        Me.chkFile.Margin = New System.Windows.Forms.Padding(2)
        Me.chkFile.Name = "chkFile"
        Me.chkFile.Size = New System.Drawing.Size(89, 17)
        Me.chkFile.TabIndex = 6
        Me.chkFile.Text = "Output to File"
        Me.chkFile.UseVisualStyleBackColor = True
        '
        'chkDatabase
        '
        Me.chkDatabase.AutoSize = True
        Me.chkDatabase.Location = New System.Drawing.Point(34, 156)
        Me.chkDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.chkDatabase.Name = "chkDatabase"
        Me.chkDatabase.Size = New System.Drawing.Size(119, 17)
        Me.chkDatabase.TabIndex = 8
        Me.chkDatabase.Text = "Output to Database"
        Me.chkDatabase.UseVisualStyleBackColor = True
        '
        'chkScreen
        '
        Me.chkScreen.AutoSize = True
        Me.chkScreen.Checked = True
        Me.chkScreen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkScreen.Location = New System.Drawing.Point(37, 328)
        Me.chkScreen.Margin = New System.Windows.Forms.Padding(2)
        Me.chkScreen.Name = "chkScreen"
        Me.chkScreen.Size = New System.Drawing.Size(107, 17)
        Me.chkScreen.TabIndex = 5
        Me.chkScreen.Text = "Output to Screen"
        Me.chkScreen.UseVisualStyleBackColor = True
        '
        'chkCreateTable
        '
        Me.chkCreateTable.AutoSize = True
        Me.chkCreateTable.Location = New System.Drawing.Point(531, 15)
        Me.chkCreateTable.Margin = New System.Windows.Forms.Padding(2)
        Me.chkCreateTable.Name = "chkCreateTable"
        Me.chkCreateTable.Size = New System.Drawing.Size(121, 17)
        Me.chkCreateTable.TabIndex = 39
        Me.chkCreateTable.Text = "Create Target Table"
        Me.chkCreateTable.UseVisualStyleBackColor = True
        '
        'btnUploadFile
        '
        Me.btnUploadFile.Location = New System.Drawing.Point(570, 317)
        Me.btnUploadFile.Name = "btnUploadFile"
        Me.btnUploadFile.Size = New System.Drawing.Size(171, 23)
        Me.btnUploadFile.TabIndex = 15
        Me.btnUploadFile.Text = "(Re)Upload Imported File"
        Me.btnUploadFile.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(26, 725)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(51, 16)
        Me.lblStatus.TabIndex = 41
        Me.lblStatus.Text = "Status"
        '
        'lblStatusText
        '
        Me.lblStatusText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatusText.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusText.Location = New System.Drawing.Point(82, 725)
        Me.lblStatusText.Name = "lblStatusText"
        Me.lblStatusText.Size = New System.Drawing.Size(528, 15)
        Me.lblStatusText.TabIndex = 42
        '
        'btnPreviousTable
        '
        Me.btnPreviousTable.Location = New System.Drawing.Point(28, 348)
        Me.btnPreviousTable.Name = "btnPreviousTable"
        Me.btnPreviousTable.Size = New System.Drawing.Size(34, 23)
        Me.btnPreviousTable.TabIndex = 16
        Me.btnPreviousTable.Text = "<"
        Me.btnPreviousTable.UseVisualStyleBackColor = True
        '
        'btnNextTable
        '
        Me.btnNextTable.Location = New System.Drawing.Point(176, 348)
        Me.btnNextTable.Name = "btnNextTable"
        Me.btnNextTable.Size = New System.Drawing.Size(34, 23)
        Me.btnNextTable.TabIndex = 17
        Me.btnNextTable.Text = ">"
        Me.btnNextTable.UseVisualStyleBackColor = True
        '
        'lblTableNumber
        '
        Me.lblTableNumber.AutoSize = True
        Me.lblTableNumber.Location = New System.Drawing.Point(83, 353)
        Me.lblTableNumber.Name = "lblTableNumber"
        Me.lblTableNumber.Size = New System.Drawing.Size(64, 13)
        Me.lblTableNumber.TabIndex = 45
        Me.lblTableNumber.Text = "Table 0 of 0"
        '
        'lblTableName
        '
        Me.lblTableName.AutoSize = True
        Me.lblTableName.Location = New System.Drawing.Point(236, 353)
        Me.lblTableName.Name = "lblTableName"
        Me.lblTableName.Size = New System.Drawing.Size(37, 13)
        Me.lblTableName.TabIndex = 46
        Me.lblTableName.Text = "Table:"
        '
        'lblTableNameText
        '
        Me.lblTableNameText.AutoSize = True
        Me.lblTableNameText.Location = New System.Drawing.Point(279, 353)
        Me.lblTableNameText.Name = "lblTableNameText"
        Me.lblTableNameText.Size = New System.Drawing.Size(0, 13)
        Me.lblTableNameText.TabIndex = 47
        '
        'chkUploadTable
        '
        Me.chkUploadTable.AutoSize = True
        Me.chkUploadTable.Location = New System.Drawing.Point(359, 352)
        Me.chkUploadTable.Name = "chkUploadTable"
        Me.chkUploadTable.Size = New System.Drawing.Size(174, 17)
        Me.chkUploadTable.TabIndex = 18
        Me.chkUploadTable.Text = "Upload This Table to Database"
        Me.chkUploadTable.UseVisualStyleBackColor = True
        '
        'btnUploadTable
        '
        Me.btnUploadTable.Location = New System.Drawing.Point(570, 348)
        Me.btnUploadTable.Name = "btnUploadTable"
        Me.btnUploadTable.Size = New System.Drawing.Size(171, 23)
        Me.btnUploadTable.TabIndex = 19
        Me.btnUploadTable.Text = "Upload currently selected Table"
        Me.btnUploadTable.UseVisualStyleBackColor = True
        '
        'chkCovertToText
        '
        Me.chkCovertToText.AutoSize = True
        Me.chkCovertToText.Location = New System.Drawing.Point(531, 18)
        Me.chkCovertToText.Margin = New System.Windows.Forms.Padding(2)
        Me.chkCovertToText.Name = "chkCovertToText"
        Me.chkCovertToText.Size = New System.Drawing.Size(139, 17)
        Me.chkCovertToText.TabIndex = 50
        Me.chkCovertToText.Text = "Convert All Data to Text"
        Me.chkCovertToText.UseVisualStyleBackColor = True
        '
        'chkCovertToNull
        '
        Me.chkCovertToNull.AutoSize = True
        Me.chkCovertToNull.Location = New System.Drawing.Point(531, 39)
        Me.chkCovertToNull.Margin = New System.Windows.Forms.Padding(2)
        Me.chkCovertToNull.Name = "chkCovertToNull"
        Me.chkCovertToNull.Size = New System.Drawing.Size(167, 17)
        Me.chkCovertToNull.TabIndex = 51
        Me.chkCovertToNull.Text = "Convert Empty cells to DBNull"
        Me.chkCovertToNull.UseVisualStyleBackColor = True
        '
        'chkHasHeaders
        '
        Me.chkHasHeaders.AutoSize = True
        Me.chkHasHeaders.Checked = True
        Me.chkHasHeaders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHasHeaders.Location = New System.Drawing.Point(5, 39)
        Me.chkHasHeaders.Margin = New System.Windows.Forms.Padding(2)
        Me.chkHasHeaders.Name = "chkHasHeaders"
        Me.chkHasHeaders.Size = New System.Drawing.Size(153, 17)
        Me.chkHasHeaders.TabIndex = 2
        Me.chkHasHeaders.Text = "Text File Contains Headers"
        Me.chkHasHeaders.UseVisualStyleBackColor = True
        '
        'lblTextDelimiter
        '
        Me.lblTextDelimiter.AutoSize = True
        Me.lblTextDelimiter.Location = New System.Drawing.Point(286, 18)
        Me.lblTextDelimiter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTextDelimiter.Name = "lblTextDelimiter"
        Me.lblTextDelimiter.Size = New System.Drawing.Size(96, 13)
        Me.lblTextDelimiter.TabIndex = 54
        Me.lblTextDelimiter.Text = "Text Field Delimiter"
        '
        'txtDelimiter
        '
        Me.txtDelimiter.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelimiter.Location = New System.Drawing.Point(392, 10)
        Me.txtDelimiter.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDelimiter.Name = "txtDelimiter"
        Me.txtDelimiter.Size = New System.Drawing.Size(50, 26)
        Me.txtDelimiter.TabIndex = 3
        Me.txtDelimiter.Text = ","
        Me.txtDelimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDelimiterShow
        '
        Me.txtDelimiterShow.BackColor = System.Drawing.SystemColors.Control
        Me.txtDelimiterShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDelimiterShow.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelimiterShow.Location = New System.Drawing.Point(443, 8)
        Me.txtDelimiterShow.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDelimiterShow.Name = "txtDelimiterShow"
        Me.txtDelimiterShow.Size = New System.Drawing.Size(50, 38)
        Me.txtDelimiterShow.TabIndex = 55
        Me.txtDelimiterShow.TabStop = False
        Me.txtDelimiterShow.Text = ","
        Me.txtDelimiterShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtDelimiterShow.Visible = False
        '
        'btnClearData
        '
        Me.btnClearData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearData.Location = New System.Drawing.Point(616, 722)
        Me.btnClearData.Name = "btnClearData"
        Me.btnClearData.Size = New System.Drawing.Size(126, 23)
        Me.btnClearData.TabIndex = 56
        Me.btnClearData.Text = "Clear Data"
        Me.btnClearData.UseVisualStyleBackColor = True
        '
        'chkClearTarget
        '
        Me.chkClearTarget.AutoSize = True
        Me.chkClearTarget.Location = New System.Drawing.Point(531, 37)
        Me.chkClearTarget.Margin = New System.Windows.Forms.Padding(2)
        Me.chkClearTarget.Name = "chkClearTarget"
        Me.chkClearTarget.Size = New System.Drawing.Size(114, 17)
        Me.chkClearTarget.TabIndex = 57
        Me.chkClearTarget.Text = "Clear Target Table"
        Me.chkClearTarget.UseVisualStyleBackColor = True
        '
        'chkUploadAsXml
        '
        Me.chkUploadAsXml.AutoSize = True
        Me.chkUploadAsXml.Location = New System.Drawing.Point(531, 60)
        Me.chkUploadAsXml.Margin = New System.Windows.Forms.Padding(2)
        Me.chkUploadAsXml.Name = "chkUploadAsXml"
        Me.chkUploadAsXml.Size = New System.Drawing.Size(99, 17)
        Me.chkUploadAsXml.TabIndex = 58
        Me.chkUploadAsXml.Text = "Upload as XML"
        Me.chkUploadAsXml.UseVisualStyleBackColor = True
        '
        'chkQuotedValues
        '
        Me.chkQuotedValues.AutoSize = True
        Me.chkQuotedValues.Location = New System.Drawing.Point(5, 60)
        Me.chkQuotedValues.Margin = New System.Windows.Forms.Padding(2)
        Me.chkQuotedValues.Name = "chkQuotedValues"
        Me.chkQuotedValues.Size = New System.Drawing.Size(205, 17)
        Me.chkQuotedValues.TabIndex = 59
        Me.chkQuotedValues.Text = "Text File Contains Quoted Values ("" "")"
        Me.chkQuotedValues.UseVisualStyleBackColor = True
        '
        'chkLargeFile
        '
        Me.chkLargeFile.AutoSize = True
        Me.chkLargeFile.Location = New System.Drawing.Point(5, 85)
        Me.chkLargeFile.Margin = New System.Windows.Forms.Padding(2)
        Me.chkLargeFile.Name = "chkLargeFile"
        Me.chkLargeFile.Size = New System.Drawing.Size(164, 17)
        Me.chkLargeFile.TabIndex = 60
        Me.chkLargeFile.Text = "Import large file in batches of:"
        Me.chkLargeFile.UseVisualStyleBackColor = True
        '
        'txtBatchSize
        '
        Me.txtBatchSize.Location = New System.Drawing.Point(237, 83)
        Me.txtBatchSize.Margin = New System.Windows.Forms.Padding(2)
        Me.txtBatchSize.Name = "txtBatchSize"
        Me.txtBatchSize.Size = New System.Drawing.Size(60, 20)
        Me.txtBatchSize.TabIndex = 61
        Me.txtBatchSize.Text = "100000"
        Me.txtBatchSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'grpDatabase
        '
        Me.grpDatabase.Controls.Add(Me.txtSqlCommand)
        Me.grpDatabase.Controls.Add(Me.chkSqlCommand)
        Me.grpDatabase.Controls.Add(Me.lblDatabaseServerName)
        Me.grpDatabase.Controls.Add(Me.txtBatchSize)
        Me.grpDatabase.Controls.Add(Me.chkLargeFile)
        Me.grpDatabase.Controls.Add(Me.txtServer)
        Me.grpDatabase.Controls.Add(Me.txtDatabase)
        Me.grpDatabase.Controls.Add(Me.chkUploadAsXml)
        Me.grpDatabase.Controls.Add(Me.txtTable)
        Me.grpDatabase.Controls.Add(Me.chkClearTarget)
        Me.grpDatabase.Controls.Add(Me.txtUser)
        Me.grpDatabase.Controls.Add(Me.txtPassword)
        Me.grpDatabase.Controls.Add(Me.lblDatabaseName)
        Me.grpDatabase.Controls.Add(Me.lblTable)
        Me.grpDatabase.Controls.Add(Me.chkWinAuth)
        Me.grpDatabase.Controls.Add(Me.lblUser)
        Me.grpDatabase.Controls.Add(Me.lblPassword)
        Me.grpDatabase.Controls.Add(Me.chkCreateTable)
        Me.grpDatabase.Enabled = False
        Me.grpDatabase.Location = New System.Drawing.Point(29, 159)
        Me.grpDatabase.Name = "grpDatabase"
        Me.grpDatabase.Size = New System.Drawing.Size(713, 110)
        Me.grpDatabase.TabIndex = 62
        Me.grpDatabase.TabStop = False
        '
        'txtSqlCommand
        '
        Me.txtSqlCommand.Location = New System.Drawing.Point(505, 81)
        Me.txtSqlCommand.Margin = New System.Windows.Forms.Padding(2)
        Me.txtSqlCommand.Name = "txtSqlCommand"
        Me.txtSqlCommand.Size = New System.Drawing.Size(193, 20)
        Me.txtSqlCommand.TabIndex = 63
        '
        'chkSqlCommand
        '
        Me.chkSqlCommand.AutoSize = True
        Me.chkSqlCommand.Location = New System.Drawing.Point(330, 85)
        Me.chkSqlCommand.Margin = New System.Windows.Forms.Padding(2)
        Me.chkSqlCommand.Name = "chkSqlCommand"
        Me.chkSqlCommand.Size = New System.Drawing.Size(171, 17)
        Me.chkSqlCommand.TabIndex = 62
        Me.chkSqlCommand.Text = "Execute SQL cmd after upload"
        Me.chkSqlCommand.UseVisualStyleBackColor = True
        '
        'grpFileOptions
        '
        Me.grpFileOptions.Controls.Add(Me.txtArchive)
        Me.grpFileOptions.Controls.Add(Me.chkArchive)
        Me.grpFileOptions.Controls.Add(Me.txtFilterRows)
        Me.grpFileOptions.Controls.Add(Me.chkFilterRows)
        Me.grpFileOptions.Controls.Add(Me.nudSkipRows)
        Me.grpFileOptions.Controls.Add(Me.chkSkipRows)
        Me.grpFileOptions.Controls.Add(Me.txtDelimiterShow)
        Me.grpFileOptions.Controls.Add(Me.cbxTextEncoding)
        Me.grpFileOptions.Controls.Add(Me.lblTextEncoding)
        Me.grpFileOptions.Controls.Add(Me.chkHasHeaders)
        Me.grpFileOptions.Controls.Add(Me.lblTextDelimiter)
        Me.grpFileOptions.Controls.Add(Me.chkQuotedValues)
        Me.grpFileOptions.Controls.Add(Me.txtDelimiter)
        Me.grpFileOptions.Controls.Add(Me.chkCovertToNull)
        Me.grpFileOptions.Controls.Add(Me.chkCovertToText)
        Me.grpFileOptions.Location = New System.Drawing.Point(29, 42)
        Me.grpFileOptions.Name = "grpFileOptions"
        Me.grpFileOptions.Size = New System.Drawing.Size(713, 109)
        Me.grpFileOptions.TabIndex = 63
        Me.grpFileOptions.TabStop = False
        Me.grpFileOptions.Text = "File Options"
        '
        'txtArchive
        '
        Me.txtArchive.Location = New System.Drawing.Point(463, 81)
        Me.txtArchive.Margin = New System.Windows.Forms.Padding(2)
        Me.txtArchive.Name = "txtArchive"
        Me.txtArchive.Size = New System.Drawing.Size(235, 20)
        Me.txtArchive.TabIndex = 67
        '
        'chkArchive
        '
        Me.chkArchive.AutoSize = True
        Me.chkArchive.Location = New System.Drawing.Point(289, 83)
        Me.chkArchive.Margin = New System.Windows.Forms.Padding(2)
        Me.chkArchive.Name = "chkArchive"
        Me.chkArchive.Size = New System.Drawing.Size(170, 17)
        Me.chkArchive.TabIndex = 66
        Me.chkArchive.Text = "Archive imported files to folder:"
        Me.chkArchive.UseVisualStyleBackColor = True
        '
        'txtFilterRows
        '
        Me.txtFilterRows.Location = New System.Drawing.Point(463, 58)
        Me.txtFilterRows.Margin = New System.Windows.Forms.Padding(2)
        Me.txtFilterRows.Name = "txtFilterRows"
        Me.txtFilterRows.Size = New System.Drawing.Size(235, 20)
        Me.txtFilterRows.TabIndex = 65
        '
        'chkFilterRows
        '
        Me.chkFilterRows.AutoSize = True
        Me.chkFilterRows.Location = New System.Drawing.Point(289, 60)
        Me.chkFilterRows.Margin = New System.Windows.Forms.Padding(2)
        Me.chkFilterRows.Name = "chkFilterRows"
        Me.chkFilterRows.Size = New System.Drawing.Size(160, 17)
        Me.chkFilterRows.TabIndex = 64
        Me.chkFilterRows.Text = "Ignore Rows that Start With:"
        Me.chkFilterRows.UseVisualStyleBackColor = True
        '
        'nudSkipRows
        '
        Me.nudSkipRows.Location = New System.Drawing.Point(164, 17)
        Me.nudSkipRows.Name = "nudSkipRows"
        Me.nudSkipRows.Size = New System.Drawing.Size(37, 20)
        Me.nudSkipRows.TabIndex = 63
        '
        'chkSkipRows
        '
        Me.chkSkipRows.AutoSize = True
        Me.chkSkipRows.Location = New System.Drawing.Point(5, 18)
        Me.chkSkipRows.Margin = New System.Windows.Forms.Padding(2)
        Me.chkSkipRows.Name = "chkSkipRows"
        Me.chkSkipRows.Size = New System.Drawing.Size(156, 17)
        Me.chkSkipRows.TabIndex = 62
        Me.chkSkipRows.Text = "No of Header Rows to Skip"
        Me.chkSkipRows.UseVisualStyleBackColor = True
        '
        'cbxTextEncoding
        '
        Me.cbxTextEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxTextEncoding.Field = Nothing
        Me.cbxTextEncoding.FormattingEnabled = True
        Me.cbxTextEncoding.Items.AddRange(New Object() {"UTF8", "UTF7", "UTF32", "ASCII", "Unicode", "BigEndianUnicode"})
        Me.cbxTextEncoding.Location = New System.Drawing.Point(392, 37)
        Me.cbxTextEncoding.Name = "cbxTextEncoding"
        Me.cbxTextEncoding.Size = New System.Drawing.Size(108, 21)
        Me.cbxTextEncoding.TabIndex = 61
        '
        'lblTextEncoding
        '
        Me.lblTextEncoding.AutoSize = True
        Me.lblTextEncoding.Location = New System.Drawing.Point(286, 39)
        Me.lblTextEncoding.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTextEncoding.Name = "lblTextEncoding"
        Me.lblTextEncoding.Size = New System.Drawing.Size(76, 13)
        Me.lblTextEncoding.TabIndex = 60
        Me.lblTextEncoding.Text = "Text Encoding"
        '
        'grpUploadFile
        '
        Me.grpUploadFile.Controls.Add(Me.btnUploadFolder)
        Me.grpUploadFile.Controls.Add(Me.txtUploadFile)
        Me.grpUploadFile.Controls.Add(Me.lblFileName)
        Me.grpUploadFile.Enabled = False
        Me.grpUploadFile.Location = New System.Drawing.Point(29, 278)
        Me.grpUploadFile.Name = "grpUploadFile"
        Me.grpUploadFile.Size = New System.Drawing.Size(523, 45)
        Me.grpUploadFile.TabIndex = 64
        Me.grpUploadFile.TabStop = False
        '
        'btnUploadFolder
        '
        Me.btnUploadFolder.Image = Global.Sequenchel.My.Resources.Resources.folder_explore
        Me.btnUploadFolder.Location = New System.Drawing.Point(490, 12)
        Me.btnUploadFolder.Name = "btnUploadFolder"
        Me.btnUploadFolder.Size = New System.Drawing.Size(23, 23)
        Me.btnUploadFolder.TabIndex = 65
        Me.btnUploadFolder.UseVisualStyleBackColor = True
        '
        'txtUploadFile
        '
        Me.txtUploadFile.Location = New System.Drawing.Point(127, 14)
        Me.txtUploadFile.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUploadFile.Name = "txtUploadFile"
        Me.txtUploadFile.Size = New System.Drawing.Size(357, 20)
        Me.txtUploadFile.TabIndex = 33
        '
        'dgvImport
        '
        Me.dgvImport.AllowUserToAddRows = False
        Me.dgvImport.AllowUserToOrderColumns = True
        Me.dgvImport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvImport.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.dgvImport.BackImage = CType(resources.GetObject("dgvImport.BackImage"), System.Drawing.Image)
        Me.dgvImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvImport.Location = New System.Drawing.Point(29, 384)
        Me.dgvImport.Name = "dgvImport"
        Me.dgvImport.Size = New System.Drawing.Size(713, 332)
        Me.dgvImport.TabIndex = 20
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 753)
        Me.Controls.Add(Me.chkFile)
        Me.Controls.Add(Me.grpUploadFile)
        Me.Controls.Add(Me.chkDatabase)
        Me.Controls.Add(Me.grpFileOptions)
        Me.Controls.Add(Me.grpDatabase)
        Me.Controls.Add(Me.btnClearData)
        Me.Controls.Add(Me.btnUploadTable)
        Me.Controls.Add(Me.chkUploadTable)
        Me.Controls.Add(Me.lblTableNameText)
        Me.Controls.Add(Me.lblTableName)
        Me.Controls.Add(Me.lblTableNumber)
        Me.Controls.Add(Me.btnNextTable)
        Me.Controls.Add(Me.btnPreviousTable)
        Me.Controls.Add(Me.lblStatusText)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnUploadFile)
        Me.Controls.Add(Me.chkScreen)
        Me.Controls.Add(Me.btnSelectFile)
        Me.Controls.Add(Me.txtCurrentFile)
        Me.Controls.Add(Me.lblCurrentFile)
        Me.Controls.Add(Me.btnImportFile)
        Me.Controls.Add(Me.dgvImport)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Import"
        Me.grpDatabase.ResumeLayout(False)
        Me.grpDatabase.PerformLayout()
        Me.grpFileOptions.ResumeLayout(False)
        Me.grpFileOptions.PerformLayout()
        CType(Me.nudSkipRows, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpUploadFile.ResumeLayout(False)
        Me.grpUploadFile.PerformLayout()
        CType(Me.dgvImport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvImport As Sequenchel.usrDataGridView
    Friend WithEvents btnImportFile As System.Windows.Forms.Button
    Friend WithEvents lblCurrentFile As System.Windows.Forms.Label
    Friend WithEvents txtCurrentFile As System.Windows.Forms.TextBox
    Friend WithEvents btnSelectFile As System.Windows.Forms.Button
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents chkWinAuth As System.Windows.Forms.CheckBox
    Friend WithEvents lblTable As System.Windows.Forms.Label
    Friend WithEvents lblDatabaseServerName As System.Windows.Forms.Label
    Friend WithEvents lblDatabaseName As System.Windows.Forms.Label
    Friend WithEvents lblFileName As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtTable As System.Windows.Forms.TextBox
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents chkFile As System.Windows.Forms.CheckBox
    Friend WithEvents chkDatabase As System.Windows.Forms.CheckBox
    Friend WithEvents chkScreen As System.Windows.Forms.CheckBox
    Friend WithEvents chkCreateTable As System.Windows.Forms.CheckBox
    Friend WithEvents btnUploadFile As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblStatusText As System.Windows.Forms.Label
    Friend WithEvents btnPreviousTable As System.Windows.Forms.Button
    Friend WithEvents btnNextTable As System.Windows.Forms.Button
    Friend WithEvents lblTableNumber As System.Windows.Forms.Label
    Friend WithEvents lblTableName As System.Windows.Forms.Label
    Friend WithEvents lblTableNameText As System.Windows.Forms.Label
    Friend WithEvents chkUploadTable As System.Windows.Forms.CheckBox
    Friend WithEvents btnUploadTable As System.Windows.Forms.Button
    Friend WithEvents chkCovertToText As System.Windows.Forms.CheckBox
    Friend WithEvents chkCovertToNull As System.Windows.Forms.CheckBox
    Friend WithEvents chkHasHeaders As System.Windows.Forms.CheckBox
    Friend WithEvents lblTextDelimiter As System.Windows.Forms.Label
    Friend WithEvents txtDelimiter As System.Windows.Forms.TextBox
    Friend WithEvents txtDelimiterShow As System.Windows.Forms.TextBox
    Friend WithEvents btnClearData As System.Windows.Forms.Button
    Friend WithEvents chkClearTarget As CheckBox
    Friend WithEvents chkUploadAsXml As CheckBox
    Friend WithEvents chkQuotedValues As System.Windows.Forms.CheckBox
    Friend WithEvents chkLargeFile As CheckBox
    Friend WithEvents txtBatchSize As TextBox
    Friend WithEvents grpDatabase As GroupBox
    Friend WithEvents grpFileOptions As GroupBox
    Friend WithEvents grpUploadFile As GroupBox
    Friend WithEvents cbxTextEncoding As ComboField
    Friend WithEvents lblTextEncoding As Label
    Friend WithEvents txtFilterRows As TextBox
    Friend WithEvents chkFilterRows As CheckBox
    Friend WithEvents nudSkipRows As NumericUpDown
    Friend WithEvents chkSkipRows As CheckBox
    Friend WithEvents txtSqlCommand As TextBox
    Friend WithEvents chkSqlCommand As CheckBox
    Friend WithEvents txtArchive As TextBox
    Friend WithEvents chkArchive As CheckBox
    Friend WithEvents btnUploadFolder As Button
    Friend WithEvents txtUploadFile As TextBox
End Class
