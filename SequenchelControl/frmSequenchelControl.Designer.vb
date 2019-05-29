<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSequenchelControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSequenchelControl))
        Me.btnDataManager = New System.Windows.Forms.Button()
        Me.btnReporting = New System.Windows.Forms.Button()
        Me.btnDataImport = New System.Windows.Forms.Button()
        Me.btnDataExport = New System.Windows.Forms.Button()
        Me.btnSmartUpdate = New System.Windows.Forms.Button()
        Me.btnSettings = New System.Windows.Forms.Button()
        Me.btnLinkedServers = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnConnections = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnDataManager
        '
        Me.btnDataManager.Location = New System.Drawing.Point(34, 30)
        Me.btnDataManager.Name = "btnDataManager"
        Me.btnDataManager.Size = New System.Drawing.Size(115, 90)
        Me.btnDataManager.TabIndex = 0
        Me.btnDataManager.Text = "Data Manager"
        Me.btnDataManager.UseVisualStyleBackColor = True
        '
        'btnReporting
        '
        Me.btnReporting.Location = New System.Drawing.Point(160, 30)
        Me.btnReporting.Name = "btnReporting"
        Me.btnReporting.Size = New System.Drawing.Size(115, 90)
        Me.btnReporting.TabIndex = 1
        Me.btnReporting.Text = "Reporting"
        Me.btnReporting.UseVisualStyleBackColor = True
        '
        'btnDataImport
        '
        Me.btnDataImport.Location = New System.Drawing.Point(34, 132)
        Me.btnDataImport.Name = "btnDataImport"
        Me.btnDataImport.Size = New System.Drawing.Size(115, 90)
        Me.btnDataImport.TabIndex = 3
        Me.btnDataImport.Text = "Data Import"
        Me.btnDataImport.UseVisualStyleBackColor = True
        '
        'btnDataExport
        '
        Me.btnDataExport.Location = New System.Drawing.Point(160, 132)
        Me.btnDataExport.Name = "btnDataExport"
        Me.btnDataExport.Size = New System.Drawing.Size(115, 90)
        Me.btnDataExport.TabIndex = 4
        Me.btnDataExport.Text = "Data Export"
        Me.btnDataExport.UseVisualStyleBackColor = True
        '
        'btnSmartUpdate
        '
        Me.btnSmartUpdate.Location = New System.Drawing.Point(285, 30)
        Me.btnSmartUpdate.Name = "btnSmartUpdate"
        Me.btnSmartUpdate.Size = New System.Drawing.Size(115, 90)
        Me.btnSmartUpdate.TabIndex = 2
        Me.btnSmartUpdate.Text = "SmartUpdate Data Warehousing"
        Me.btnSmartUpdate.UseVisualStyleBackColor = True
        '
        'btnSettings
        '
        Me.btnSettings.Location = New System.Drawing.Point(34, 234)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(115, 90)
        Me.btnSettings.TabIndex = 6
        Me.btnSettings.Text = "Settings"
        Me.btnSettings.UseVisualStyleBackColor = True
        '
        'btnLinkedServers
        '
        Me.btnLinkedServers.Location = New System.Drawing.Point(285, 132)
        Me.btnLinkedServers.Name = "btnLinkedServers"
        Me.btnLinkedServers.Size = New System.Drawing.Size(115, 90)
        Me.btnLinkedServers.TabIndex = 5
        Me.btnLinkedServers.Text = "Linked Servers"
        Me.btnLinkedServers.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(285, 234)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(115, 90)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnConnections
        '
        Me.btnConnections.Location = New System.Drawing.Point(160, 234)
        Me.btnConnections.Name = "btnConnections"
        Me.btnConnections.Size = New System.Drawing.Size(115, 90)
        Me.btnConnections.TabIndex = 7
        Me.btnConnections.Text = "Connections"
        Me.btnConnections.UseVisualStyleBackColor = True
        '
        'frmSequenchelControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(438, 349)
        Me.Controls.Add(Me.btnConnections)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnLinkedServers)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.btnSmartUpdate)
        Me.Controls.Add(Me.btnDataExport)
        Me.Controls.Add(Me.btnDataImport)
        Me.Controls.Add(Me.btnReporting)
        Me.Controls.Add(Me.btnDataManager)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmSequenchelControl"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sequenchel Control"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnDataManager As Button
    Friend WithEvents btnReporting As Button
    Friend WithEvents btnDataImport As Button
    Friend WithEvents btnDataExport As Button
    Friend WithEvents btnSmartUpdate As Button
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnLinkedServers As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents btnConnections As Button
End Class
