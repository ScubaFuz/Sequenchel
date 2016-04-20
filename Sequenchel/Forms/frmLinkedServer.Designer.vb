<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLinkedServer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLinkedServer))
        Me.lvwLinkedServers = New System.Windows.Forms.ListView()
        Me.colServer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colInstance = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDomain = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPort = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblServer = New System.Windows.Forms.Label()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.lblInstance = New System.Windows.Forms.Label()
        Me.txtInstance = New System.Windows.Forms.TextBox()
        Me.lblTcpPport = New System.Windows.Forms.Label()
        Me.txtTcpPort = New System.Windows.Forms.TextBox()
        Me.lblDomain = New System.Windows.Forms.Label()
        Me.txtDomain = New System.Windows.Forms.TextBox()
        Me.btnLinkedServerDelete = New System.Windows.Forms.Button()
        Me.btnLinkedServerClear = New System.Windows.Forms.Button()
        Me.btnLinkedServerAdd = New System.Windows.Forms.Button()
        Me.btnLinkedServerUpdate = New System.Windows.Forms.Button()
        Me.btnColumnsImport = New System.Windows.Forms.Button()
        Me.lblHostServer = New System.Windows.Forms.Label()
        Me.txtHostServer = New System.Windows.Forms.TextBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblLinkedServer = New System.Windows.Forms.Label()
        Me.txtLinkedServer = New System.Windows.Forms.TextBox()
        Me.chkLinkedServer = New System.Windows.Forms.CheckBox()
        Me.chkInstance = New System.Windows.Forms.CheckBox()
        Me.chkTcpPort = New System.Windows.Forms.CheckBox()
        Me.chkDomain = New System.Windows.Forms.CheckBox()
        Me.chkDataSource = New System.Windows.Forms.CheckBox()
        Me.lblDataSource = New System.Windows.Forms.Label()
        Me.txtDataSource = New System.Windows.Forms.TextBox()
        Me.lblProvider = New System.Windows.Forms.Label()
        Me.txtProvider = New System.Windows.Forms.TextBox()
        Me.lblServerProduct = New System.Windows.Forms.Label()
        Me.txtServerProduct = New System.Windows.Forms.TextBox()
        Me.chkCollationCompatible = New System.Windows.Forms.CheckBox()
        Me.chkDataAccess = New System.Windows.Forms.CheckBox()
        Me.chkRpcOut = New System.Windows.Forms.CheckBox()
        Me.chkRpc = New System.Windows.Forms.CheckBox()
        Me.chkLazySchema = New System.Windows.Forms.CheckBox()
        Me.chkRemoteCollation = New System.Windows.Forms.CheckBox()
        Me.chkRPTPromotion = New System.Windows.Forms.CheckBox()
        Me.chkDistributor = New System.Windows.Forms.CheckBox()
        Me.chkPublisher = New System.Windows.Forms.CheckBox()
        Me.chkSubscriber = New System.Windows.Forms.CheckBox()
        Me.lblConnectionTimeout = New System.Windows.Forms.Label()
        Me.txtConnectionTimeout = New System.Windows.Forms.TextBox()
        Me.lblCollationName = New System.Windows.Forms.Label()
        Me.txtCollationName = New System.Windows.Forms.TextBox()
        Me.lblQueryTimeout = New System.Windows.Forms.Label()
        Me.txtQueryTimeout = New System.Windows.Forms.TextBox()
        Me.lblRemoteLoginName = New System.Windows.Forms.Label()
        Me.txtRemoteLoginName = New System.Windows.Forms.TextBox()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblLocalLoginName = New System.Windows.Forms.Label()
        Me.txtLocalLoginName = New System.Windows.Forms.TextBox()
        Me.grpUpdatable = New System.Windows.Forms.GroupBox()
        Me.lblLinkedServerName = New System.Windows.Forms.Label()
        Me.txtLinkedServerName = New System.Windows.Forms.TextBox()
        Me.grpUpdatable.SuspendLayout()
        Me.SuspendLayout()
        '
        'lvwLinkedServers
        '
        Me.lvwLinkedServers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwLinkedServers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colServer, Me.colInstance, Me.colDomain, Me.colPort})
        Me.lvwLinkedServers.FullRowSelect = True
        Me.lvwLinkedServers.Location = New System.Drawing.Point(12, 18)
        Me.lvwLinkedServers.MultiSelect = False
        Me.lvwLinkedServers.Name = "lvwLinkedServers"
        Me.lvwLinkedServers.Size = New System.Drawing.Size(394, 323)
        Me.lvwLinkedServers.TabIndex = 0
        Me.lvwLinkedServers.UseCompatibleStateImageBehavior = False
        Me.lvwLinkedServers.View = System.Windows.Forms.View.Details
        '
        'colServer
        '
        Me.colServer.Text = "Machine Name"
        Me.colServer.Width = 132
        '
        'colInstance
        '
        Me.colInstance.Text = "Instance"
        Me.colInstance.Width = 95
        '
        'colDomain
        '
        Me.colDomain.Text = "Domain"
        Me.colDomain.Width = 98
        '
        'colPort
        '
        Me.colPort.Text = "Port"
        Me.colPort.Width = 43
        '
        'lblServer
        '
        Me.lblServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServer.Location = New System.Drawing.Point(441, 111)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(96, 16)
        Me.lblServer.TabIndex = 117
        Me.lblServer.Text = "Machine Name"
        Me.lblServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtServer
        '
        Me.txtServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtServer.Location = New System.Drawing.Point(546, 110)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(183, 20)
        Me.txtServer.TabIndex = 2
        '
        'lblInstance
        '
        Me.lblInstance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInstance.Location = New System.Drawing.Point(441, 137)
        Me.lblInstance.Name = "lblInstance"
        Me.lblInstance.Size = New System.Drawing.Size(96, 16)
        Me.lblInstance.TabIndex = 119
        Me.lblInstance.Text = "Instance"
        Me.lblInstance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInstance
        '
        Me.txtInstance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInstance.Location = New System.Drawing.Point(546, 136)
        Me.txtInstance.Name = "txtInstance"
        Me.txtInstance.Size = New System.Drawing.Size(183, 20)
        Me.txtInstance.TabIndex = 4
        '
        'lblTcpPport
        '
        Me.lblTcpPport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTcpPport.Location = New System.Drawing.Point(441, 163)
        Me.lblTcpPport.Name = "lblTcpPport"
        Me.lblTcpPport.Size = New System.Drawing.Size(96, 16)
        Me.lblTcpPport.TabIndex = 121
        Me.lblTcpPport.Text = "TCP Port"
        Me.lblTcpPport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTcpPort
        '
        Me.txtTcpPort.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTcpPort.Location = New System.Drawing.Point(546, 162)
        Me.txtTcpPort.Name = "txtTcpPort"
        Me.txtTcpPort.Size = New System.Drawing.Size(183, 20)
        Me.txtTcpPort.TabIndex = 6
        '
        'lblDomain
        '
        Me.lblDomain.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDomain.Location = New System.Drawing.Point(441, 189)
        Me.lblDomain.Name = "lblDomain"
        Me.lblDomain.Size = New System.Drawing.Size(96, 16)
        Me.lblDomain.TabIndex = 123
        Me.lblDomain.Text = "Domain"
        Me.lblDomain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDomain
        '
        Me.txtDomain.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDomain.Location = New System.Drawing.Point(546, 188)
        Me.txtDomain.Name = "txtDomain"
        Me.txtDomain.Size = New System.Drawing.Size(183, 20)
        Me.txtDomain.TabIndex = 8
        '
        'btnLinkedServerDelete
        '
        Me.btnLinkedServerDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLinkedServerDelete.Location = New System.Drawing.Point(458, 544)
        Me.btnLinkedServerDelete.Name = "btnLinkedServerDelete"
        Me.btnLinkedServerDelete.Size = New System.Drawing.Size(131, 20)
        Me.btnLinkedServerDelete.TabIndex = 18
        Me.btnLinkedServerDelete.Text = "Delete Linked Server"
        '
        'btnLinkedServerClear
        '
        Me.btnLinkedServerClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLinkedServerClear.Location = New System.Drawing.Point(595, 544)
        Me.btnLinkedServerClear.Name = "btnLinkedServerClear"
        Me.btnLinkedServerClear.Size = New System.Drawing.Size(131, 20)
        Me.btnLinkedServerClear.TabIndex = 19
        Me.btnLinkedServerClear.Text = "Clear"
        '
        'btnLinkedServerAdd
        '
        Me.btnLinkedServerAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLinkedServerAdd.Location = New System.Drawing.Point(458, 518)
        Me.btnLinkedServerAdd.Name = "btnLinkedServerAdd"
        Me.btnLinkedServerAdd.Size = New System.Drawing.Size(131, 20)
        Me.btnLinkedServerAdd.TabIndex = 16
        Me.btnLinkedServerAdd.Text = "Add Linked Server"
        '
        'btnLinkedServerUpdate
        '
        Me.btnLinkedServerUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLinkedServerUpdate.Location = New System.Drawing.Point(595, 518)
        Me.btnLinkedServerUpdate.Name = "btnLinkedServerUpdate"
        Me.btnLinkedServerUpdate.Size = New System.Drawing.Size(131, 20)
        Me.btnLinkedServerUpdate.TabIndex = 17
        Me.btnLinkedServerUpdate.Text = "Update Linked Server"
        '
        'btnColumnsImport
        '
        Me.btnColumnsImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnColumnsImport.Location = New System.Drawing.Point(447, 44)
        Me.btnColumnsImport.Name = "btnColumnsImport"
        Me.btnColumnsImport.Size = New System.Drawing.Size(282, 20)
        Me.btnColumnsImport.TabIndex = 1
        Me.btnColumnsImport.Text = "Import Linked Servers from Host Server"
        '
        'lblHostServer
        '
        Me.lblHostServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHostServer.Location = New System.Drawing.Point(444, 19)
        Me.lblHostServer.Name = "lblHostServer"
        Me.lblHostServer.Size = New System.Drawing.Size(96, 16)
        Me.lblHostServer.TabIndex = 149
        Me.lblHostServer.Text = "Host Server"
        Me.lblHostServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtHostServer
        '
        Me.txtHostServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtHostServer.Location = New System.Drawing.Point(546, 18)
        Me.txtHostServer.Name = "txtHostServer"
        Me.txtHostServer.ReadOnly = True
        Me.txtHostServer.Size = New System.Drawing.Size(183, 20)
        Me.txtHostServer.TabIndex = 148
        Me.txtHostServer.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(595, 570)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(131, 23)
        Me.btnClose.TabIndex = 20
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblLinkedServer
        '
        Me.lblLinkedServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLinkedServer.Location = New System.Drawing.Point(441, 215)
        Me.lblLinkedServer.Name = "lblLinkedServer"
        Me.lblLinkedServer.Size = New System.Drawing.Size(96, 16)
        Me.lblLinkedServer.TabIndex = 152
        Me.lblLinkedServer.Text = "Linked Server"
        Me.lblLinkedServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLinkedServer
        '
        Me.txtLinkedServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLinkedServer.Enabled = False
        Me.txtLinkedServer.Location = New System.Drawing.Point(546, 214)
        Me.txtLinkedServer.Name = "txtLinkedServer"
        Me.txtLinkedServer.Size = New System.Drawing.Size(183, 20)
        Me.txtLinkedServer.TabIndex = 10
        '
        'chkLinkedServer
        '
        Me.chkLinkedServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkLinkedServer.AutoSize = True
        Me.chkLinkedServer.Location = New System.Drawing.Point(525, 217)
        Me.chkLinkedServer.Name = "chkLinkedServer"
        Me.chkLinkedServer.Size = New System.Drawing.Size(15, 14)
        Me.chkLinkedServer.TabIndex = 9
        Me.chkLinkedServer.UseVisualStyleBackColor = True
        '
        'chkInstance
        '
        Me.chkInstance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkInstance.AutoSize = True
        Me.chkInstance.Checked = True
        Me.chkInstance.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkInstance.Location = New System.Drawing.Point(525, 139)
        Me.chkInstance.Name = "chkInstance"
        Me.chkInstance.Size = New System.Drawing.Size(15, 14)
        Me.chkInstance.TabIndex = 3
        Me.chkInstance.UseVisualStyleBackColor = True
        '
        'chkTcpPort
        '
        Me.chkTcpPort.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTcpPort.AutoSize = True
        Me.chkTcpPort.Checked = True
        Me.chkTcpPort.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTcpPort.Location = New System.Drawing.Point(525, 165)
        Me.chkTcpPort.Name = "chkTcpPort"
        Me.chkTcpPort.Size = New System.Drawing.Size(15, 14)
        Me.chkTcpPort.TabIndex = 5
        Me.chkTcpPort.UseVisualStyleBackColor = True
        '
        'chkDomain
        '
        Me.chkDomain.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDomain.AutoSize = True
        Me.chkDomain.Checked = True
        Me.chkDomain.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDomain.Location = New System.Drawing.Point(525, 191)
        Me.chkDomain.Name = "chkDomain"
        Me.chkDomain.Size = New System.Drawing.Size(15, 14)
        Me.chkDomain.TabIndex = 7
        Me.chkDomain.UseVisualStyleBackColor = True
        '
        'chkDataSource
        '
        Me.chkDataSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkDataSource.AutoSize = True
        Me.chkDataSource.Location = New System.Drawing.Point(525, 243)
        Me.chkDataSource.Name = "chkDataSource"
        Me.chkDataSource.Size = New System.Drawing.Size(15, 14)
        Me.chkDataSource.TabIndex = 11
        Me.chkDataSource.UseVisualStyleBackColor = True
        '
        'lblDataSource
        '
        Me.lblDataSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDataSource.Location = New System.Drawing.Point(441, 241)
        Me.lblDataSource.Name = "lblDataSource"
        Me.lblDataSource.Size = New System.Drawing.Size(96, 16)
        Me.lblDataSource.TabIndex = 159
        Me.lblDataSource.Text = "Data Source"
        Me.lblDataSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDataSource
        '
        Me.txtDataSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDataSource.Enabled = False
        Me.txtDataSource.Location = New System.Drawing.Point(546, 240)
        Me.txtDataSource.Name = "txtDataSource"
        Me.txtDataSource.Size = New System.Drawing.Size(183, 20)
        Me.txtDataSource.TabIndex = 12
        '
        'lblProvider
        '
        Me.lblProvider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProvider.Location = New System.Drawing.Point(429, 20)
        Me.lblProvider.Name = "lblProvider"
        Me.lblProvider.Size = New System.Drawing.Size(96, 16)
        Me.lblProvider.TabIndex = 162
        Me.lblProvider.Text = "Provider"
        Me.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtProvider
        '
        Me.txtProvider.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtProvider.Location = New System.Drawing.Point(531, 19)
        Me.txtProvider.Name = "txtProvider"
        Me.txtProvider.Size = New System.Drawing.Size(183, 20)
        Me.txtProvider.TabIndex = 10
        Me.txtProvider.Text = "SQLNCLI"
        '
        'lblServerProduct
        '
        Me.lblServerProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblServerProduct.Location = New System.Drawing.Point(429, 46)
        Me.lblServerProduct.Name = "lblServerProduct"
        Me.lblServerProduct.Size = New System.Drawing.Size(96, 16)
        Me.lblServerProduct.TabIndex = 164
        Me.lblServerProduct.Text = "Server Product"
        Me.lblServerProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtServerProduct
        '
        Me.txtServerProduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtServerProduct.Location = New System.Drawing.Point(531, 45)
        Me.txtServerProduct.Name = "txtServerProduct"
        Me.txtServerProduct.Size = New System.Drawing.Size(183, 20)
        Me.txtServerProduct.TabIndex = 11
        Me.txtServerProduct.Text = "SQL_Server"
        '
        'chkCollationCompatible
        '
        Me.chkCollationCompatible.AutoSize = True
        Me.chkCollationCompatible.Location = New System.Drawing.Point(6, 21)
        Me.chkCollationCompatible.Name = "chkCollationCompatible"
        Me.chkCollationCompatible.Size = New System.Drawing.Size(121, 17)
        Me.chkCollationCompatible.TabIndex = 0
        Me.chkCollationCompatible.Text = "Collation Compatible"
        Me.chkCollationCompatible.UseVisualStyleBackColor = True
        '
        'chkDataAccess
        '
        Me.chkDataAccess.AutoSize = True
        Me.chkDataAccess.Checked = True
        Me.chkDataAccess.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDataAccess.Location = New System.Drawing.Point(6, 47)
        Me.chkDataAccess.Name = "chkDataAccess"
        Me.chkDataAccess.Size = New System.Drawing.Size(87, 17)
        Me.chkDataAccess.TabIndex = 1
        Me.chkDataAccess.Text = "Data Access"
        Me.chkDataAccess.UseVisualStyleBackColor = True
        '
        'chkRpcOut
        '
        Me.chkRpcOut.AutoSize = True
        Me.chkRpcOut.Checked = True
        Me.chkRpcOut.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRpcOut.Location = New System.Drawing.Point(260, 127)
        Me.chkRpcOut.Name = "chkRpcOut"
        Me.chkRpcOut.Size = New System.Drawing.Size(68, 17)
        Me.chkRpcOut.TabIndex = 9
        Me.chkRpcOut.Text = "RPC Out"
        Me.chkRpcOut.UseVisualStyleBackColor = True
        '
        'chkRpc
        '
        Me.chkRpc.AutoSize = True
        Me.chkRpc.Checked = True
        Me.chkRpc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRpc.Location = New System.Drawing.Point(260, 101)
        Me.chkRpc.Name = "chkRpc"
        Me.chkRpc.Size = New System.Drawing.Size(48, 17)
        Me.chkRpc.TabIndex = 8
        Me.chkRpc.Text = "RPC"
        Me.chkRpc.UseVisualStyleBackColor = True
        '
        'chkLazySchema
        '
        Me.chkLazySchema.AutoSize = True
        Me.chkLazySchema.Location = New System.Drawing.Point(6, 73)
        Me.chkLazySchema.Name = "chkLazySchema"
        Me.chkLazySchema.Size = New System.Drawing.Size(139, 17)
        Me.chkLazySchema.TabIndex = 2
        Me.chkLazySchema.Text = "Lazy Schema Validation"
        Me.chkLazySchema.UseVisualStyleBackColor = True
        '
        'chkRemoteCollation
        '
        Me.chkRemoteCollation.AutoSize = True
        Me.chkRemoteCollation.Checked = True
        Me.chkRemoteCollation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRemoteCollation.Location = New System.Drawing.Point(6, 99)
        Me.chkRemoteCollation.Name = "chkRemoteCollation"
        Me.chkRemoteCollation.Size = New System.Drawing.Size(128, 17)
        Me.chkRemoteCollation.TabIndex = 3
        Me.chkRemoteCollation.Text = "Use Remote Collation"
        Me.chkRemoteCollation.UseVisualStyleBackColor = True
        '
        'chkRPTPromotion
        '
        Me.chkRPTPromotion.AutoSize = True
        Me.chkRPTPromotion.Location = New System.Drawing.Point(6, 125)
        Me.chkRPTPromotion.Name = "chkRPTPromotion"
        Me.chkRPTPromotion.Size = New System.Drawing.Size(224, 17)
        Me.chkRPTPromotion.TabIndex = 4
        Me.chkRPTPromotion.Text = "Remote Procedure Transaction Promotion"
        Me.chkRPTPromotion.UseVisualStyleBackColor = True
        '
        'chkDistributor
        '
        Me.chkDistributor.AutoSize = True
        Me.chkDistributor.Location = New System.Drawing.Point(260, 23)
        Me.chkDistributor.Name = "chkDistributor"
        Me.chkDistributor.Size = New System.Drawing.Size(73, 17)
        Me.chkDistributor.TabIndex = 5
        Me.chkDistributor.Text = "Distributor"
        Me.chkDistributor.UseVisualStyleBackColor = True
        '
        'chkPublisher
        '
        Me.chkPublisher.AutoSize = True
        Me.chkPublisher.Location = New System.Drawing.Point(260, 49)
        Me.chkPublisher.Name = "chkPublisher"
        Me.chkPublisher.Size = New System.Drawing.Size(69, 17)
        Me.chkPublisher.TabIndex = 6
        Me.chkPublisher.Text = "Publisher"
        Me.chkPublisher.UseVisualStyleBackColor = True
        '
        'chkSubscriber
        '
        Me.chkSubscriber.AutoSize = True
        Me.chkSubscriber.Location = New System.Drawing.Point(260, 75)
        Me.chkSubscriber.Name = "chkSubscriber"
        Me.chkSubscriber.Size = New System.Drawing.Size(76, 17)
        Me.chkSubscriber.TabIndex = 7
        Me.chkSubscriber.Text = "Subscriber"
        Me.chkSubscriber.UseVisualStyleBackColor = True
        '
        'lblConnectionTimeout
        '
        Me.lblConnectionTimeout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConnectionTimeout.Location = New System.Drawing.Point(429, 72)
        Me.lblConnectionTimeout.Name = "lblConnectionTimeout"
        Me.lblConnectionTimeout.Size = New System.Drawing.Size(116, 16)
        Me.lblConnectionTimeout.TabIndex = 176
        Me.lblConnectionTimeout.Text = "Connection Timeout"
        Me.lblConnectionTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtConnectionTimeout
        '
        Me.txtConnectionTimeout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtConnectionTimeout.Location = New System.Drawing.Point(531, 71)
        Me.txtConnectionTimeout.Name = "txtConnectionTimeout"
        Me.txtConnectionTimeout.Size = New System.Drawing.Size(183, 20)
        Me.txtConnectionTimeout.TabIndex = 12
        Me.txtConnectionTimeout.Text = "0"
        '
        'lblCollationName
        '
        Me.lblCollationName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCollationName.Location = New System.Drawing.Point(429, 98)
        Me.lblCollationName.Name = "lblCollationName"
        Me.lblCollationName.Size = New System.Drawing.Size(96, 16)
        Me.lblCollationName.TabIndex = 178
        Me.lblCollationName.Text = "Collation Name"
        Me.lblCollationName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCollationName
        '
        Me.txtCollationName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCollationName.Location = New System.Drawing.Point(531, 97)
        Me.txtCollationName.Name = "txtCollationName"
        Me.txtCollationName.Size = New System.Drawing.Size(183, 20)
        Me.txtCollationName.TabIndex = 13
        '
        'lblQueryTimeout
        '
        Me.lblQueryTimeout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblQueryTimeout.Location = New System.Drawing.Point(429, 124)
        Me.lblQueryTimeout.Name = "lblQueryTimeout"
        Me.lblQueryTimeout.Size = New System.Drawing.Size(96, 16)
        Me.lblQueryTimeout.TabIndex = 180
        Me.lblQueryTimeout.Text = "Query Timeout"
        Me.lblQueryTimeout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtQueryTimeout
        '
        Me.txtQueryTimeout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQueryTimeout.Location = New System.Drawing.Point(531, 123)
        Me.txtQueryTimeout.Name = "txtQueryTimeout"
        Me.txtQueryTimeout.Size = New System.Drawing.Size(183, 20)
        Me.txtQueryTimeout.TabIndex = 14
        Me.txtQueryTimeout.Text = "0"
        '
        'lblRemoteLoginName
        '
        Me.lblRemoteLoginName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRemoteLoginName.Location = New System.Drawing.Point(441, 301)
        Me.lblRemoteLoginName.Name = "lblRemoteLoginName"
        Me.lblRemoteLoginName.Size = New System.Drawing.Size(106, 16)
        Me.lblRemoteLoginName.TabIndex = 182
        Me.lblRemoteLoginName.Text = "Remote Login Name"
        Me.lblRemoteLoginName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRemoteLoginName
        '
        Me.txtRemoteLoginName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemoteLoginName.Enabled = False
        Me.txtRemoteLoginName.Location = New System.Drawing.Point(546, 300)
        Me.txtRemoteLoginName.Name = "txtRemoteLoginName"
        Me.txtRemoteLoginName.Size = New System.Drawing.Size(183, 20)
        Me.txtRemoteLoginName.TabIndex = 14
        '
        'lblPassword
        '
        Me.lblPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPassword.Location = New System.Drawing.Point(441, 327)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(96, 16)
        Me.lblPassword.TabIndex = 184
        Me.lblPassword.Text = "Remote Password"
        Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.Enabled = False
        Me.txtPassword.Location = New System.Drawing.Point(546, 326)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(183, 20)
        Me.txtPassword.TabIndex = 15
        '
        'lblLocalLoginName
        '
        Me.lblLocalLoginName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLocalLoginName.Location = New System.Drawing.Point(441, 275)
        Me.lblLocalLoginName.Name = "lblLocalLoginName"
        Me.lblLocalLoginName.Size = New System.Drawing.Size(96, 16)
        Me.lblLocalLoginName.TabIndex = 186
        Me.lblLocalLoginName.Text = "Local Login Name"
        Me.lblLocalLoginName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLocalLoginName
        '
        Me.txtLocalLoginName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLocalLoginName.Enabled = False
        Me.txtLocalLoginName.Location = New System.Drawing.Point(546, 274)
        Me.txtLocalLoginName.Name = "txtLocalLoginName"
        Me.txtLocalLoginName.Size = New System.Drawing.Size(183, 20)
        Me.txtLocalLoginName.TabIndex = 13
        '
        'grpUpdatable
        '
        Me.grpUpdatable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpUpdatable.Controls.Add(Me.chkCollationCompatible)
        Me.grpUpdatable.Controls.Add(Me.chkDataAccess)
        Me.grpUpdatable.Controls.Add(Me.chkLazySchema)
        Me.grpUpdatable.Controls.Add(Me.chkRemoteCollation)
        Me.grpUpdatable.Controls.Add(Me.chkRPTPromotion)
        Me.grpUpdatable.Controls.Add(Me.chkDistributor)
        Me.grpUpdatable.Controls.Add(Me.chkRpcOut)
        Me.grpUpdatable.Controls.Add(Me.txtQueryTimeout)
        Me.grpUpdatable.Controls.Add(Me.txtCollationName)
        Me.grpUpdatable.Controls.Add(Me.lblQueryTimeout)
        Me.grpUpdatable.Controls.Add(Me.txtConnectionTimeout)
        Me.grpUpdatable.Controls.Add(Me.chkRpc)
        Me.grpUpdatable.Controls.Add(Me.txtServerProduct)
        Me.grpUpdatable.Controls.Add(Me.chkPublisher)
        Me.grpUpdatable.Controls.Add(Me.txtProvider)
        Me.grpUpdatable.Controls.Add(Me.lblCollationName)
        Me.grpUpdatable.Controls.Add(Me.chkSubscriber)
        Me.grpUpdatable.Controls.Add(Me.lblConnectionTimeout)
        Me.grpUpdatable.Controls.Add(Me.lblProvider)
        Me.grpUpdatable.Controls.Add(Me.lblServerProduct)
        Me.grpUpdatable.Location = New System.Drawing.Point(12, 349)
        Me.grpUpdatable.Name = "grpUpdatable"
        Me.grpUpdatable.Size = New System.Drawing.Size(723, 158)
        Me.grpUpdatable.TabIndex = 187
        Me.grpUpdatable.TabStop = False
        Me.grpUpdatable.Text = "Updatable Properties"
        '
        'lblLinkedServerName
        '
        Me.lblLinkedServerName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLinkedServerName.Location = New System.Drawing.Point(441, 85)
        Me.lblLinkedServerName.Name = "lblLinkedServerName"
        Me.lblLinkedServerName.Size = New System.Drawing.Size(96, 16)
        Me.lblLinkedServerName.TabIndex = 189
        Me.lblLinkedServerName.Text = "Linked Server Name"
        Me.lblLinkedServerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLinkedServerName
        '
        Me.txtLinkedServerName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLinkedServerName.Location = New System.Drawing.Point(546, 84)
        Me.txtLinkedServerName.Name = "txtLinkedServerName"
        Me.txtLinkedServerName.ReadOnly = True
        Me.txtLinkedServerName.Size = New System.Drawing.Size(183, 20)
        Me.txtLinkedServerName.TabIndex = 188
        Me.txtLinkedServerName.TabStop = False
        '
        'frmLinkedServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(741, 606)
        Me.Controls.Add(Me.lblLinkedServerName)
        Me.Controls.Add(Me.txtLinkedServerName)
        Me.Controls.Add(Me.grpUpdatable)
        Me.Controls.Add(Me.txtRemoteLoginName)
        Me.Controls.Add(Me.lblLocalLoginName)
        Me.Controls.Add(Me.txtLocalLoginName)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblRemoteLoginName)
        Me.Controls.Add(Me.chkDataSource)
        Me.Controls.Add(Me.lblDataSource)
        Me.Controls.Add(Me.txtDataSource)
        Me.Controls.Add(Me.chkDomain)
        Me.Controls.Add(Me.chkTcpPort)
        Me.Controls.Add(Me.chkInstance)
        Me.Controls.Add(Me.chkLinkedServer)
        Me.Controls.Add(Me.lblLinkedServer)
        Me.Controls.Add(Me.txtLinkedServer)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblHostServer)
        Me.Controls.Add(Me.txtHostServer)
        Me.Controls.Add(Me.btnColumnsImport)
        Me.Controls.Add(Me.btnLinkedServerDelete)
        Me.Controls.Add(Me.btnLinkedServerClear)
        Me.Controls.Add(Me.btnLinkedServerAdd)
        Me.Controls.Add(Me.btnLinkedServerUpdate)
        Me.Controls.Add(Me.lblDomain)
        Me.Controls.Add(Me.txtDomain)
        Me.Controls.Add(Me.lblTcpPport)
        Me.Controls.Add(Me.txtTcpPort)
        Me.Controls.Add(Me.lblInstance)
        Me.Controls.Add(Me.txtInstance)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.lvwLinkedServers)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(757, 644)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(757, 644)
        Me.Name = "frmLinkedServer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Linked Servers"
        Me.grpUpdatable.ResumeLayout(False)
        Me.grpUpdatable.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvwLinkedServers As System.Windows.Forms.ListView
    Friend WithEvents colServer As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPort As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDomain As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents lblInstance As System.Windows.Forms.Label
    Friend WithEvents txtInstance As System.Windows.Forms.TextBox
    Friend WithEvents lblTcpPport As System.Windows.Forms.Label
    Friend WithEvents txtTcpPort As System.Windows.Forms.TextBox
    Friend WithEvents lblDomain As System.Windows.Forms.Label
    Friend WithEvents txtDomain As System.Windows.Forms.TextBox
    Friend WithEvents btnLinkedServerDelete As System.Windows.Forms.Button
    Friend WithEvents btnLinkedServerClear As System.Windows.Forms.Button
    Friend WithEvents btnLinkedServerAdd As System.Windows.Forms.Button
    Friend WithEvents btnLinkedServerUpdate As System.Windows.Forms.Button
    Friend WithEvents btnColumnsImport As System.Windows.Forms.Button
    Friend WithEvents lblHostServer As System.Windows.Forms.Label
    Friend WithEvents txtHostServer As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblLinkedServer As System.Windows.Forms.Label
    Friend WithEvents txtLinkedServer As System.Windows.Forms.TextBox
    Friend WithEvents chkLinkedServer As System.Windows.Forms.CheckBox
    Friend WithEvents chkInstance As System.Windows.Forms.CheckBox
    Friend WithEvents chkTcpPort As System.Windows.Forms.CheckBox
    Friend WithEvents chkDomain As System.Windows.Forms.CheckBox
    Friend WithEvents chkDataSource As System.Windows.Forms.CheckBox
    Friend WithEvents lblDataSource As System.Windows.Forms.Label
    Friend WithEvents txtDataSource As System.Windows.Forms.TextBox
    Friend WithEvents colInstance As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblProvider As System.Windows.Forms.Label
    Friend WithEvents txtProvider As System.Windows.Forms.TextBox
    Friend WithEvents lblServerProduct As System.Windows.Forms.Label
    Friend WithEvents txtServerProduct As System.Windows.Forms.TextBox
    Friend WithEvents chkCollationCompatible As System.Windows.Forms.CheckBox
    Friend WithEvents chkDataAccess As System.Windows.Forms.CheckBox
    Friend WithEvents chkRpcOut As System.Windows.Forms.CheckBox
    Friend WithEvents chkRpc As System.Windows.Forms.CheckBox
    Friend WithEvents chkLazySchema As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemoteCollation As System.Windows.Forms.CheckBox
    Friend WithEvents chkRPTPromotion As System.Windows.Forms.CheckBox
    Friend WithEvents chkDistributor As System.Windows.Forms.CheckBox
    Friend WithEvents chkPublisher As System.Windows.Forms.CheckBox
    Friend WithEvents chkSubscriber As System.Windows.Forms.CheckBox
    Friend WithEvents lblConnectionTimeout As System.Windows.Forms.Label
    Friend WithEvents txtConnectionTimeout As System.Windows.Forms.TextBox
    Friend WithEvents lblCollationName As System.Windows.Forms.Label
    Friend WithEvents txtCollationName As System.Windows.Forms.TextBox
    Friend WithEvents lblQueryTimeout As System.Windows.Forms.Label
    Friend WithEvents txtQueryTimeout As System.Windows.Forms.TextBox
    Friend WithEvents lblRemoteLoginName As System.Windows.Forms.Label
    Friend WithEvents txtRemoteLoginName As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblLocalLoginName As System.Windows.Forms.Label
    Friend WithEvents txtLocalLoginName As System.Windows.Forms.TextBox
    Friend WithEvents grpUpdatable As System.Windows.Forms.GroupBox
    Friend WithEvents lblLinkedServerName As System.Windows.Forms.Label
    Friend WithEvents txtLinkedServerName As System.Windows.Forms.TextBox
End Class
