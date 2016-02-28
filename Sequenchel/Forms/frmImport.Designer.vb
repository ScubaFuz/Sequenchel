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
        Me.btnExportFile = New System.Windows.Forms.Button()
        Me.dgvImport = New Sequenchel.usrDataGridView()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblStatusText = New System.Windows.Forms.Label()
        CType(Me.dgvImport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnImportFile
        '
        Me.btnImportFile.Location = New System.Drawing.Point(607, 29)
        Me.btnImportFile.Name = "btnImportFile"
        Me.btnImportFile.Size = New System.Drawing.Size(135, 23)
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
        Me.txtCurrentFile.Size = New System.Drawing.Size(323, 20)
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
        Me.lblPassword.Location = New System.Drawing.Point(580, 159)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(53, 13)
        Me.lblPassword.TabIndex = 38
        Me.lblPassword.Text = "Password"
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(580, 136)
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
        Me.chkWinAuth.Location = New System.Drawing.Point(582, 113)
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
        Me.lblTable.Location = New System.Drawing.Point(238, 159)
        Me.lblTable.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(65, 13)
        Me.lblTable.TabIndex = 35
        Me.lblTable.Text = "Table Name"
        '
        'lblDatabaseServerName
        '
        Me.lblDatabaseServerName.AutoSize = True
        Me.lblDatabaseServerName.Location = New System.Drawing.Point(238, 114)
        Me.lblDatabaseServerName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDatabaseServerName.Name = "lblDatabaseServerName"
        Me.lblDatabaseServerName.Size = New System.Drawing.Size(118, 13)
        Me.lblDatabaseServerName.TabIndex = 34
        Me.lblDatabaseServerName.Text = "Database Server Name"
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.Location = New System.Drawing.Point(238, 136)
        Me.lblDatabaseName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Size = New System.Drawing.Size(82, 13)
        Me.lblDatabaseName.TabIndex = 33
        Me.lblDatabaseName.Text = "Database name"
        '
        'lblFileName
        '
        Me.lblFileName.AutoSize = True
        Me.lblFileName.Location = New System.Drawing.Point(174, 88)
        Me.lblFileName.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(54, 13)
        Me.lblFileName.TabIndex = 32
        Me.lblFileName.Text = "File Name"
        '
        'txtFileName
        '
        Me.txtFileName.Enabled = False
        Me.txtFileName.Location = New System.Drawing.Point(240, 86)
        Me.txtFileName.Margin = New System.Windows.Forms.Padding(2)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(322, 20)
        Me.txtFileName.TabIndex = 31
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(644, 157)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(2)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(98, 20)
        Me.txtPassword.TabIndex = 30
        '
        'txtUser
        '
        Me.txtUser.Enabled = False
        Me.txtUser.Location = New System.Drawing.Point(644, 134)
        Me.txtUser.Margin = New System.Windows.Forms.Padding(2)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(98, 20)
        Me.txtUser.TabIndex = 29
        '
        'txtTable
        '
        Me.txtTable.Enabled = False
        Me.txtTable.Location = New System.Drawing.Point(360, 157)
        Me.txtTable.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTable.Name = "txtTable"
        Me.txtTable.Size = New System.Drawing.Size(203, 20)
        Me.txtTable.TabIndex = 28
        '
        'txtDatabase
        '
        Me.txtDatabase.Enabled = False
        Me.txtDatabase.Location = New System.Drawing.Point(360, 134)
        Me.txtDatabase.Margin = New System.Windows.Forms.Padding(2)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(203, 20)
        Me.txtDatabase.TabIndex = 27
        '
        'txtServer
        '
        Me.txtServer.Enabled = False
        Me.txtServer.Location = New System.Drawing.Point(360, 111)
        Me.txtServer.Margin = New System.Windows.Forms.Padding(2)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(203, 20)
        Me.txtServer.TabIndex = 26
        '
        'chkFile
        '
        Me.chkFile.AutoSize = True
        Me.chkFile.Location = New System.Drawing.Point(29, 88)
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
        Me.chkDatabase.Location = New System.Drawing.Point(29, 113)
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
        Me.chkScreen.Location = New System.Drawing.Point(29, 63)
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
        Me.chkCreateTable.Location = New System.Drawing.Point(29, 158)
        Me.chkCreateTable.Margin = New System.Windows.Forms.Padding(2)
        Me.chkCreateTable.Name = "chkCreateTable"
        Me.chkCreateTable.Size = New System.Drawing.Size(87, 17)
        Me.chkCreateTable.TabIndex = 39
        Me.chkCreateTable.Text = "Create Table"
        Me.chkCreateTable.UseVisualStyleBackColor = True
        '
        'btnExportFile
        '
        Me.btnExportFile.Location = New System.Drawing.Point(607, 83)
        Me.btnExportFile.Name = "btnExportFile"
        Me.btnExportFile.Size = New System.Drawing.Size(135, 23)
        Me.btnExportFile.TabIndex = 40
        Me.btnExportFile.Text = "Export File"
        Me.btnExportFile.UseVisualStyleBackColor = True
        '
        'dgvImport
        '
        Me.dgvImport.AllowUserToOrderColumns = True
        Me.dgvImport.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvImport.BackgroundColor = System.Drawing.SystemColors.Control
        Me.dgvImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.dgvImport.BackImage = CType(resources.GetObject("dgvImport.BackImage"), System.Drawing.Image)
        Me.dgvImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvImport.Location = New System.Drawing.Point(29, 193)
        Me.dgvImport.Name = "dgvImport"
        Me.dgvImport.Size = New System.Drawing.Size(713, 519)
        Me.dgvImport.TabIndex = 1
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
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 753)
        Me.Controls.Add(Me.lblStatusText)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnExportFile)
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
    Friend WithEvents btnExportFile As System.Windows.Forms.Button
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblStatusText As System.Windows.Forms.Label
End Class
