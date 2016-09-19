<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReAlias
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReAlias))
        Me.grpTableAlias = New System.Windows.Forms.GroupBox()
        Me.cbxNewTableAlias = New System.Windows.Forms.ComboBox()
        Me.txtCurrentTableAlias = New System.Windows.Forms.TextBox()
        Me.txtTableName = New System.Windows.Forms.TextBox()
        Me.lblTableName = New System.Windows.Forms.Label()
        Me.lblCurrentTableAlias = New System.Windows.Forms.Label()
        Me.lblNewTableAlias = New System.Windows.Forms.Label()
        Me.txtFieldName = New System.Windows.Forms.TextBox()
        Me.lblFieldName = New System.Windows.Forms.Label()
        Me.grpFieldAlias = New System.Windows.Forms.GroupBox()
        Me.lblNewFieldAlias = New System.Windows.Forms.Label()
        Me.txtNewFieldAlias = New System.Windows.Forms.TextBox()
        Me.lblCurrentFieldAlias = New System.Windows.Forms.Label()
        Me.txtCurrentFieldAlias = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.grpTableAlias.SuspendLayout()
        Me.grpFieldAlias.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpTableAlias
        '
        Me.grpTableAlias.Controls.Add(Me.cbxNewTableAlias)
        Me.grpTableAlias.Controls.Add(Me.txtCurrentTableAlias)
        Me.grpTableAlias.Controls.Add(Me.txtTableName)
        Me.grpTableAlias.Controls.Add(Me.lblTableName)
        Me.grpTableAlias.Controls.Add(Me.lblCurrentTableAlias)
        Me.grpTableAlias.Controls.Add(Me.lblNewTableAlias)
        Me.grpTableAlias.Location = New System.Drawing.Point(6, 8)
        Me.grpTableAlias.Name = "grpTableAlias"
        Me.grpTableAlias.Size = New System.Drawing.Size(342, 102)
        Me.grpTableAlias.TabIndex = 1
        Me.grpTableAlias.TabStop = False
        Me.grpTableAlias.Text = "Table Alias"
        '
        'cbxNewTableAlias
        '
        Me.cbxNewTableAlias.FormattingEnabled = True
        Me.cbxNewTableAlias.Location = New System.Drawing.Point(120, 71)
        Me.cbxNewTableAlias.Name = "cbxNewTableAlias"
        Me.cbxNewTableAlias.Size = New System.Drawing.Size(216, 21)
        Me.cbxNewTableAlias.TabIndex = 10
        '
        'txtCurrentTableAlias
        '
        Me.txtCurrentTableAlias.Location = New System.Drawing.Point(120, 45)
        Me.txtCurrentTableAlias.Name = "txtCurrentTableAlias"
        Me.txtCurrentTableAlias.ReadOnly = True
        Me.txtCurrentTableAlias.Size = New System.Drawing.Size(216, 20)
        Me.txtCurrentTableAlias.TabIndex = 9
        '
        'txtTableName
        '
        Me.txtTableName.Location = New System.Drawing.Point(120, 19)
        Me.txtTableName.Name = "txtTableName"
        Me.txtTableName.ReadOnly = True
        Me.txtTableName.Size = New System.Drawing.Size(216, 20)
        Me.txtTableName.TabIndex = 8
        '
        'lblTableName
        '
        Me.lblTableName.AutoSize = True
        Me.lblTableName.Location = New System.Drawing.Point(6, 22)
        Me.lblTableName.Name = "lblTableName"
        Me.lblTableName.Size = New System.Drawing.Size(65, 13)
        Me.lblTableName.TabIndex = 3
        Me.lblTableName.Text = "Table Name"
        '
        'lblCurrentTableAlias
        '
        Me.lblCurrentTableAlias.AutoSize = True
        Me.lblCurrentTableAlias.Location = New System.Drawing.Point(6, 48)
        Me.lblCurrentTableAlias.Name = "lblCurrentTableAlias"
        Me.lblCurrentTableAlias.Size = New System.Drawing.Size(96, 13)
        Me.lblCurrentTableAlias.TabIndex = 2
        Me.lblCurrentTableAlias.Text = "Current Table Alias"
        '
        'lblNewTableAlias
        '
        Me.lblNewTableAlias.AutoSize = True
        Me.lblNewTableAlias.Location = New System.Drawing.Point(6, 74)
        Me.lblNewTableAlias.Name = "lblNewTableAlias"
        Me.lblNewTableAlias.Size = New System.Drawing.Size(84, 13)
        Me.lblNewTableAlias.TabIndex = 1
        Me.lblNewTableAlias.Text = "New Table Alias"
        '
        'txtFieldName
        '
        Me.txtFieldName.Location = New System.Drawing.Point(120, 19)
        Me.txtFieldName.Name = "txtFieldName"
        Me.txtFieldName.ReadOnly = True
        Me.txtFieldName.Size = New System.Drawing.Size(216, 20)
        Me.txtFieldName.TabIndex = 2
        '
        'lblFieldName
        '
        Me.lblFieldName.AutoSize = True
        Me.lblFieldName.Location = New System.Drawing.Point(6, 22)
        Me.lblFieldName.Name = "lblFieldName"
        Me.lblFieldName.Size = New System.Drawing.Size(60, 13)
        Me.lblFieldName.TabIndex = 3
        Me.lblFieldName.Text = "Field Name"
        '
        'grpFieldAlias
        '
        Me.grpFieldAlias.Controls.Add(Me.lblNewFieldAlias)
        Me.grpFieldAlias.Controls.Add(Me.txtNewFieldAlias)
        Me.grpFieldAlias.Controls.Add(Me.lblCurrentFieldAlias)
        Me.grpFieldAlias.Controls.Add(Me.txtCurrentFieldAlias)
        Me.grpFieldAlias.Controls.Add(Me.lblFieldName)
        Me.grpFieldAlias.Controls.Add(Me.txtFieldName)
        Me.grpFieldAlias.Location = New System.Drawing.Point(6, 119)
        Me.grpFieldAlias.Name = "grpFieldAlias"
        Me.grpFieldAlias.Size = New System.Drawing.Size(342, 102)
        Me.grpFieldAlias.TabIndex = 4
        Me.grpFieldAlias.TabStop = False
        Me.grpFieldAlias.Text = "Field Alias"
        '
        'lblNewFieldAlias
        '
        Me.lblNewFieldAlias.AutoSize = True
        Me.lblNewFieldAlias.Location = New System.Drawing.Point(6, 74)
        Me.lblNewFieldAlias.Name = "lblNewFieldAlias"
        Me.lblNewFieldAlias.Size = New System.Drawing.Size(79, 13)
        Me.lblNewFieldAlias.TabIndex = 7
        Me.lblNewFieldAlias.Text = "New Field Alias"
        '
        'txtNewFieldAlias
        '
        Me.txtNewFieldAlias.Location = New System.Drawing.Point(120, 71)
        Me.txtNewFieldAlias.Name = "txtNewFieldAlias"
        Me.txtNewFieldAlias.Size = New System.Drawing.Size(216, 20)
        Me.txtNewFieldAlias.TabIndex = 6
        '
        'lblCurrentFieldAlias
        '
        Me.lblCurrentFieldAlias.AutoSize = True
        Me.lblCurrentFieldAlias.Location = New System.Drawing.Point(6, 48)
        Me.lblCurrentFieldAlias.Name = "lblCurrentFieldAlias"
        Me.lblCurrentFieldAlias.Size = New System.Drawing.Size(91, 13)
        Me.lblCurrentFieldAlias.TabIndex = 5
        Me.lblCurrentFieldAlias.Text = "Current Field Alias"
        '
        'txtCurrentFieldAlias
        '
        Me.txtCurrentFieldAlias.Location = New System.Drawing.Point(120, 45)
        Me.txtCurrentFieldAlias.Name = "txtCurrentFieldAlias"
        Me.txtCurrentFieldAlias.ReadOnly = True
        Me.txtCurrentFieldAlias.Size = New System.Drawing.Size(216, 20)
        Me.txtCurrentFieldAlias.TabIndex = 4
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(267, 229)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(186, 229)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 6
        Me.btnOk.Text = "OK"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'frmReAlias
        '
        Me.AcceptButton = Me.btnOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(354, 261)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.grpFieldAlias)
        Me.Controls.Add(Me.grpTableAlias)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmReAlias"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ReAlias"
        Me.TopMost = True
        Me.grpTableAlias.ResumeLayout(False)
        Me.grpTableAlias.PerformLayout()
        Me.grpFieldAlias.ResumeLayout(False)
        Me.grpFieldAlias.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpTableAlias As System.Windows.Forms.GroupBox
    Friend WithEvents lblNewTableAlias As System.Windows.Forms.Label
    Friend WithEvents txtFieldName As System.Windows.Forms.TextBox
    Friend WithEvents lblFieldName As System.Windows.Forms.Label
    Friend WithEvents grpFieldAlias As System.Windows.Forms.GroupBox
    Friend WithEvents lblTableName As System.Windows.Forms.Label
    Friend WithEvents lblCurrentTableAlias As System.Windows.Forms.Label
    Friend WithEvents lblNewFieldAlias As System.Windows.Forms.Label
    Friend WithEvents txtNewFieldAlias As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrentFieldAlias As System.Windows.Forms.Label
    Friend WithEvents txtCurrentFieldAlias As System.Windows.Forms.TextBox
    Friend WithEvents cbxNewTableAlias As System.Windows.Forms.ComboBox
    Friend WithEvents txtCurrentTableAlias As System.Windows.Forms.TextBox
    Friend WithEvents txtTableName As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
