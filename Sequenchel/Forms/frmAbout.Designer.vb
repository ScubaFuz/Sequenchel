<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
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
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblCopyright As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAbout))
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblCopyright = New System.Windows.Forms.Label()
        Me.pnlMain = New System.Windows.Forms.Panel()
        Me.lblLicenseText = New System.Windows.Forms.Label()
        Me.lblLicenseName = New System.Windows.Forms.Label()
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblVersion
        '
        Me.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.Color.White
        Me.lblVersion.Location = New System.Drawing.Point(341, 260)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(130, 20)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "Version {0}.{1:00}"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCopyright
        '
        Me.lblCopyright.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblCopyright.BackColor = System.Drawing.Color.Transparent
        Me.lblCopyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyright.ForeColor = System.Drawing.Color.White
        Me.lblCopyright.Location = New System.Drawing.Point(-2, 260)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(313, 20)
        Me.lblCopyright.TabIndex = 2
        Me.lblCopyright.Text = "Copyright"
        '
        'pnlMain
        '
        Me.pnlMain.BackgroundImage = CType(resources.GetObject("pnlMain.BackgroundImage"), System.Drawing.Image)
        Me.pnlMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pnlMain.Controls.Add(Me.lblLicenseText)
        Me.pnlMain.Controls.Add(Me.lblLicenseName)
        Me.pnlMain.Controls.Add(Me.lblCopyright)
        Me.pnlMain.Controls.Add(Me.lblVersion)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(468, 275)
        Me.pnlMain.TabIndex = 3
        '
        'lblLicenseText
        '
        Me.lblLicenseText.BackColor = System.Drawing.Color.Transparent
        Me.lblLicenseText.ForeColor = System.Drawing.Color.Black
        Me.lblLicenseText.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblLicenseText.Location = New System.Drawing.Point(12, 11)
        Me.lblLicenseText.Name = "lblLicenseText"
        Me.lblLicenseText.Size = New System.Drawing.Size(452, 91)
        Me.lblLicenseText.TabIndex = 6
        Me.lblLicenseText.Text = "Sequenchel is free for personal use. Companies are required to purchase a license" & _
    ". Having a license will automatically close this screen at startup. Thank you fo" & _
    "r playing fair."
        Me.lblLicenseText.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblLicenseName
        '
        Me.lblLicenseName.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblLicenseName.BackColor = System.Drawing.Color.Transparent
        Me.lblLicenseName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseName.ForeColor = System.Drawing.Color.White
        Me.lblLicenseName.Location = New System.Drawing.Point(-2, 240)
        Me.lblLicenseName.Name = "lblLicenseName"
        Me.lblLicenseName.Size = New System.Drawing.Size(313, 20)
        Me.lblLicenseName.TabIndex = 5
        Me.lblLicenseName.Text = "Licensed to"
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.ClientSize = New System.Drawing.Size(468, 275)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.pnlMain.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents lblLicenseName As System.Windows.Forms.Label
    Friend WithEvents lblLicenseText As System.Windows.Forms.Label

End Class
