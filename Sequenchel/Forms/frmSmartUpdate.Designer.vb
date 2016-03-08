﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbxConnection = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
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
        Me.lblStatus = New System.Windows.Forms.Label()
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
        Me.pnlMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(79, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Source Table or View"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(420, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Target Table"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(80, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Connection"
        '
        'cbxConnection
        '
        Me.cbxConnection.FormattingEnabled = True
        Me.cbxConnection.Location = New System.Drawing.Point(147, 30)
        Me.cbxConnection.Name = "cbxConnection"
        Me.cbxConnection.Size = New System.Drawing.Size(185, 21)
        Me.cbxConnection.TabIndex = 3
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(810, 398)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(171, 17)
        Me.CheckBox1.TabIndex = 5
        Me.CheckBox1.Text = "Create Target Table if not exist"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(810, 444)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(164, 17)
        Me.CheckBox2.TabIndex = 6
        Me.CheckBox2.Text = "Create Audit Table if not exist"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(810, 421)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(86, 17)
        Me.CheckBox3.TabIndex = 7
        Me.CheckBox3.Text = "Use Auditing"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(810, 467)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(213, 17)
        Me.CheckBox4.TabIndex = 8
        Me.CheckBox4.Text = "Remove NON-Source Data from Target"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(810, 490)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(171, 17)
        Me.CheckBox5.TabIndex = 9
        Me.CheckBox5.Text = "Use Target Database Collation"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'txtSourceTable
        '
        Me.txtSourceTable.Location = New System.Drawing.Point(82, 93)
        Me.txtSourceTable.Name = "txtSourceTable"
        Me.txtSourceTable.ReadOnly = True
        Me.txtSourceTable.Size = New System.Drawing.Size(249, 20)
        Me.txtSourceTable.TabIndex = 10
        '
        'txtTargetTable
        '
        Me.txtTargetTable.Location = New System.Drawing.Point(423, 93)
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
        Me.lstSourceTables.Location = New System.Drawing.Point(82, 93)
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
        Me.btnCrawlSourceTables.Location = New System.Drawing.Point(337, 94)
        Me.btnCrawlSourceTables.Name = "btnCrawlSourceTables"
        Me.btnCrawlSourceTables.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlSourceTables.TabIndex = 146
        Me.btnCrawlSourceTables.Text = "..."
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(12, 573)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 18)
        Me.lblStatus.TabIndex = 148
        '
        'lstTargetTables
        '
        Me.lstTargetTables.FormattingEnabled = True
        Me.lstTargetTables.HorizontalScrollbar = True
        Me.lstTargetTables.Location = New System.Drawing.Point(423, 93)
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
        Me.btnCrawlTargetTables.Location = New System.Drawing.Point(677, 94)
        Me.btnCrawlTargetTables.Name = "btnCrawlTargetTables"
        Me.btnCrawlTargetTables.Size = New System.Drawing.Size(30, 20)
        Me.btnCrawlTargetTables.TabIndex = 149
        Me.btnCrawlTargetTables.Text = "..."
        '
        'btnImportTables
        '
        Me.btnImportTables.Location = New System.Drawing.Point(302, 134)
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
        Me.pnlMain.Location = New System.Drawing.Point(38, 166)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(750, 377)
        Me.pnlMain.TabIndex = 158
        '
        'frmSmartUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1106, 600)
        Me.Controls.Add(Me.lstTargetTables)
        Me.Controls.Add(Me.lstSourceTables)
        Me.Controls.Add(Me.pnlMain)
        Me.Controls.Add(Me.btnImportTables)
        Me.Controls.Add(Me.btnCrawlTargetTables)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnCrawlSourceTables)
        Me.Controls.Add(Me.txtTargetTable)
        Me.Controls.Add(Me.txtSourceTable)
        Me.Controls.Add(Me.CheckBox5)
        Me.Controls.Add(Me.CheckBox4)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.cbxConnection)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmSmartUpdate"
        Me.Text = "frmSmartUpdate"
        Me.pnlMain.ResumeLayout(False)
        Me.pnlMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbxConnection As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
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
    Friend WithEvents lblStatus As System.Windows.Forms.Label
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
End Class
