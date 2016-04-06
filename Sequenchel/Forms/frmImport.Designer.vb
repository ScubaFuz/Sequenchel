<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Me.txtFileName = New System.Windows.Forms.TextBox()
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
        Me.dgvImport = New Sequenchel.usrDataGridView()
        CType(Me.dgvImport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnImportFile
        '
        Me.btnImportFile.Location = New System.Drawing.Point(571, 29)
        Me.btnImportFile.Name = "btnImportFile"
        Me.btnImportFile.Size = New System.Drawing.Size(171, 23)
        Me.btnImportFile.TabIndex = 5
        Me.btnImportFile.Text = "Import File"
        Me.btnImportFile.UseVisualStyleBackColor = True
        '
        'lblCurrentFile
        '
        Me.lblCurrentFile.AutoSize = True
        Me.lblCurrentFile.Location = New System.Drawing.Point(174, 34)
        Me.lblCurrentFile.Name = "lblCurrentFile"
        Me.lblCurrentFile.Size = New System.Drawing.Size(60, 13)
        Me.lblCurrentFile.TabIndex = 6
        Me.lblCurrentFile.Text = "Current File"
        '
        'txtCurrentFile
        '
        Me.txtCurrentFile.Location = New System.Drawing.Point(240, 31)
        Me.txtCurrentFile.Name = "txtCurrentFile"
        Me.txtCurrentFile.Size = New System.Drawing.Size(313, 20)
        Me.txtCurrentFile.TabIndex = 7
        '
        'btnSelectFile
        '
        Me.btnSelectFile.Location = New System.Drawing.Point(29, 29)
        Me.btnSelectFile.Name = "btnSelectFile"
        Me.btnSelectFile.Size = New System.Drawing.Size(126, 23)
        Me.btnSelectFile.TabIndex = 8
        Me.btnSelectFile.Text = "Select File"
        Me.btnSelectFile.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(568, 183)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 38
        Me.lblPassword.Text = "Password"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(568, 160)
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
        Me.chkWinAuth.Enabled = False
        Me.chkWinAuth.Location = New System.Drawing.Point(570, 137)
        Me.chkWinAuth.Margin = New System.Windows.Forms.Padding(2)
        Me.chkWinAuth.Name = "chkWinAuth"
        Me.chkWinAuth.Size = New System.Drawing.Size(160, 17)
        Me.chkWinAuth.TabIndex = 36
        Me.chkWinAuth.Text = "Use windows Authentication"
        Me.chkWinAuth.UseVisualStyleBackColor = True
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(238, 183)
        Me.lblTable.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(65, 13)
        Me.lblTable.TabIndex = 35
        Me.lblTable.Text = "Table Name"
        '
        'lblDatabaseServerName
        '
        Me.lblDatabaseServerName.AutoSize = True
        Me.lblDatabaseServerName.Location = New System.Drawing.Point(238, 138)
        Me.lblDatabaseServerName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDatabaseServerName.Name = "lblDatabaseServerName"
        Me.lblDatabaseServerName.Size = New System.Drawing.Size(118, 13)
        Me.lblDatabaseServerName.TabIndex = 34
        Me.lblDatabaseServerName.Text = "Database Server Name"
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.Location = New System.Drawing.Point(238, 160)
        Me.lblDatabaseName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Size = New System.Drawing.Size(82, 13)
        Me.lblDatabaseName.TabIndex = 33
        Me.lblDatabaseName.Text = "Database name"
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Location = New System.Drawing.Point(174, 112)
        Me.lblFileName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(54, 13)
        Me.lblFileName.TabIndex = 32
        Me.lblFileName.Text = "File Name"
        '
        'txtFileName
        '
        Me.txtFileName.Enabled = False
        Me.txtFileName.Location = New System.Drawing.Point(240, 110)
        Me.txtFileName.Margin = New System.Windows.Forms.Padding(2)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(312, 20)
        Me.txtFileName.TabIndex = 31
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(632, 181)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(110, 20)
        Me.txtPassword.TabIndex = 30
        '
        'txtUser
        '
        Me.txtUser.Enabled = False
        Me.txtUser.Location = New System.Drawing.Point(632, 158)
        Me.txtUser.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(110, 20)
        Me.txtUser.TabIndex = 29
        '
        'txtTable
        '
        Me.txtTable.Enabled = False
        Me.txtTable.Location = New System.Drawing.Point(360, 181)
        Me.txtTable.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTable.Name = "txtTable"
        Me.txtTable.Size = New System.Drawing.Size(193, 20)
        Me.txtTable.TabIndex = 28
        '
        'txtDatabase
        '
        Me.txtDatabase.Enabled = False
        Me.txtDatabase.Location = New System.Drawing.Point(360, 158)
        Me.txtDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(193, 20)
        Me.txtDatabase.TabIndex = 27
        '
        'txtServer
        '
        Me.txtServer.Enabled = False
        Me.txtServer.Location = New System.Drawing.Point(360, 135)
        Me.txtServer.Margin = New System.Windows.Forms.Padding(2)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(193, 20)
        Me.txtServer.TabIndex = 26
        '
        'chkFile
        '
        Me.chkFile.AutoSize = True
        Me.chkFile.Location = New System.Drawing.Point(29, 112)
        Me.chkFile.Margin = New System.Windows.Forms.Padding(2)
        Me.chkFile.Name = "chkFile"
        Me.chkFile.Size = New System.Drawing.Size(89, 17)
        Me.chkFile.TabIndex = 25
        Me.chkFile.Text = "Output to File"
        Me.chkFile.UseVisualStyleBackColor = True
        '
        'chkDatabase
        '
        Me.chkDatabase.AutoSize = True
        Me.chkDatabase.Location = New System.Drawing.Point(29, 137)
        Me.chkDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.chkDatabase.Name = "chkDatabase"
        Me.chkDatabase.Size = New System.Drawing.Size(119, 17)
        Me.chkDatabase.TabIndex = 24
        Me.chkDatabase.Text = "Output to Database"
        Me.chkDatabase.UseVisualStyleBackColor = True
        '
        'chkScreen
        '
        Me.chkScreen.AutoSize = True
        Me.chkScreen.Checked = True
        Me.chkScreen.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkScreen.Location = New System.Drawing.Point(29, 87)
        Me.chkScreen.Margin = New System.Windows.Forms.Padding(2)
        Me.chkScreen.Name = "chkScreen"
        Me.chkScreen.Size = New System.Drawing.Size(107, 17)
        Me.chkScreen.TabIndex = 23
        Me.chkScreen.Text = "Output to Screen"
        Me.chkScreen.UseVisualStyleBackColor = True
        '
        'chkCreateTable
        '
        Me.chkCreateTable.AutoSize = True
        Me.chkCreateTable.Enabled = False
        Me.chkCreateTable.Location = New System.Drawing.Point(29, 161)
        Me.chkCreateTable.Margin = New System.Windows.Forms.Padding(2)
        Me.chkCreateTable.Name = "chkCreateTable"
        Me.chkCreateTable.Size = New System.Drawing.Size(87, 17)
        Me.chkCreateTable.TabIndex = 39
        Me.chkCreateTable.Text = "Create Table"
        Me.chkCreateTable.UseVisualStyleBackColor = True
        '
        'btnUploadFile
        '
        Me.btnUploadFile.Location = New System.Drawing.Point(571, 107)
        Me.btnUploadFile.Name = "btnUploadFile"
        Me.btnUploadFile.Size = New System.Drawing.Size(171, 23)
        Me.btnUploadFile.TabIndex = 40
        Me.btnUploadFile.Text = "Upload File"
        Me.btnUploadFile.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(26, 731)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblStatus.TabIndex = 41
        Me.lblStatus.Text = "Status"
        '
        'lblStatusText
        '
        Me.lblStatusText.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatusText.AutoSize = True
        Me.lblStatusText.Location = New System.Drawing.Point(79, 731)
        Me.lblStatusText.Name = "lblStatusText"
        Me.lblStatusText.Size = New System.Drawing.Size(0, 13)
        Me.lblStatusText.TabIndex = 42
        '
        'btnPreviousTable
        '
        Me.btnPreviousTable.Location = New System.Drawing.Point(29, 209)
        Me.btnPreviousTable.Name = "btnPreviousTable"
        Me.btnPreviousTable.Size = New System.Drawing.Size(34, 23)
        Me.btnPreviousTable.TabIndex = 43
        Me.btnPreviousTable.Text = "<"
        Me.btnPreviousTable.UseVisualStyleBackColor = True
        '
        'btnNextTable
        '
        Me.btnNextTable.Location = New System.Drawing.Point(177, 209)
        Me.btnNextTable.Name = "btnNextTable"
        Me.btnNextTable.Size = New System.Drawing.Size(34, 23)
        Me.btnNextTable.TabIndex = 44
        Me.btnNextTable.Text = ">"
        Me.btnNextTable.UseVisualStyleBackColor = True
        '
        'lblTableNumber
        '
        Me.lblTableNumber.AutoSize = True
        Me.lblTableNumber.Location = New System.Drawing.Point(84, 214)
        Me.lblTableNumber.Name = "lblTableNumber"
        Me.lblTableNumber.Size = New System.Drawing.Size(64, 13)
        Me.lblTableNumber.TabIndex = 45
        Me.lblTableNumber.Text = "Table 0 of 0"
        '
        'lblTableName
        '
        Me.lblTableName.AutoSize = True
        Me.lblTableName.Location = New System.Drawing.Point(238, 214)
        Me.lblTableName.Name = "lblTableName"
        Me.lblTableName.Size = New System.Drawing.Size(37, 13)
        Me.lblTableName.TabIndex = 46
        Me.lblTableName.Text = "Table:"
        '
        'lblTableNameText
        '
        Me.lblTableNameText.AutoSize = True
        Me.lblTableNameText.Location = New System.Drawing.Point(281, 214)
        Me.lblTableNameText.Name = "lblTableNameText"
        Me.lblTableNameText.Size = New System.Drawing.Size(0, 13)
        Me.lblTableNameText.TabIndex = 47
        '
        'chkUploadTable
        '
        Me.chkUploadTable.AutoSize = True
        Me.chkUploadTable.Location = New System.Drawing.Point(360, 214)
        Me.chkUploadTable.Name = "chkUploadTable"
        Me.chkUploadTable.Size = New System.Drawing.Size(174, 17)
        Me.chkUploadTable.TabIndex = 48
        Me.chkUploadTable.Text = "Upload This Table to Database"
        Me.chkUploadTable.UseVisualStyleBackColor = True
        '
        'btnUploadTable
        '
        Me.btnUploadTable.Location = New System.Drawing.Point(571, 210)
        Me.btnUploadTable.Name = "btnUploadTable"
        Me.btnUploadTable.Size = New System.Drawing.Size(171, 23)
        Me.btnUploadTable.TabIndex = 49
        Me.btnUploadTable.Text = "Upload currently selected Table"
        Me.btnUploadTable.UseVisualStyleBackColor = True
        '
        'chkCovertToText
        '
        Me.chkCovertToText.AutoSize = True
        Me.chkCovertToText.Location = New System.Drawing.Point(240, 87)
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
        Me.chkCovertToNull.Location = New System.Drawing.Point(386, 87)
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
        Me.chkHasHeaders.Location = New System.Drawing.Point(240, 56)
        Me.chkHasHeaders.Margin = New System.Windows.Forms.Padding(2)
        Me.chkHasHeaders.Name = "chkHasHeaders"
        Me.chkHasHeaders.Size = New System.Drawing.Size(153, 17)
        Me.chkHasHeaders.TabIndex = 52
        Me.chkHasHeaders.Text = "Text File Contains Headers"
        Me.chkHasHeaders.UseVisualStyleBackColor = True
        '
        'lblTextDelimiter
        '
        Me.lblTextDelimiter.AutoSize = True
        Me.lblTextDelimiter.Location = New System.Drawing.Point(455, 57)
        Me.lblTextDelimiter.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTextDelimiter.Name = "lblTextDelimiter"
        Me.lblTextDelimiter.Size = New System.Drawing.Size(71, 13)
        Me.lblTextDelimiter.TabIndex = 54
        Me.lblTextDelimiter.Text = "Text Delimiter"
        '
        'txtDelimiter
        '
        Me.txtDelimiter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelimiter.Location = New System.Drawing.Point(530, 54)
        Me.txtDelimiter.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDelimiter.Name = "txtDelimiter"
        Me.txtDelimiter.Size = New System.Drawing.Size(22, 20)
        Me.txtDelimiter.TabIndex = 53
        Me.txtDelimiter.Text = ","
        Me.txtDelimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDelimiterShow
        '
        Me.txtDelimiterShow.BackColor = System.Drawing.SystemColors.Control
        Me.txtDelimiterShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDelimiterShow.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelimiterShow.Location = New System.Drawing.Point(551, 45)
        Me.txtDelimiterShow.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDelimiterShow.Name = "txtDelimiterShow"
        Me.txtDelimiterShow.Size = New System.Drawing.Size(39, 38)
        Me.txtDelimiterShow.TabIndex = 55
        Me.txtDelimiterShow.Text = ","
        Me.txtDelimiterShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtDelimiterShow.Visible = False
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
        Me.dgvImport.Location = New System.Drawing.Point(29, 239)
        Me.dgvImport.Name = "dgvImport"
        Me.dgvImport.Size = New System.Drawing.Size(713, 473)
        Me.dgvImport.TabIndex = 1
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 753)
        Me.Controls.Add(Me.txtDelimiterShow)
        Me.Controls.Add(Me.lblTextDelimiter)
        Me.Controls.Add(Me.txtDelimiter)
        Me.Controls.Add(Me.chkHasHeaders)
        Me.Controls.Add(Me.chkCovertToNull)
        Me.Controls.Add(Me.chkCovertToText)
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
        Me.Controls.Add(Me.chkCreateTable)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.lblUser)
        Me.Controls.Add(Me.chkWinAuth)
        Me.Controls.Add(Me.lblTable)
        Me.Controls.Add(Me.lblDatabaseServerName)
        Me.Controls.Add(Me.lblDatabaseName)
        Me.Controls.Add(Me.lblFileName)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtTable)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.chkFile)
        Me.Controls.Add(Me.chkDatabase)
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
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
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
End Class
