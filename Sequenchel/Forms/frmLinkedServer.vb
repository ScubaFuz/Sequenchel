Public Class frmLinkedServer

#Region "Controls"

    Private Sub frmLinkedServer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtHostServer.Text = SeqData.dhdConnection.DataLocation
    End Sub

    Private Sub txtServer_TextChanged(sender As Object, e As EventArgs) Handles txtServer.TextChanged
        SetLinkedServer()
        SetDataSource()
    End Sub

    Private Sub chkLinkedServer_CheckedChanged(sender As Object, e As EventArgs) Handles chkLinkedServer.CheckedChanged
        txtLinkedServer.Enabled = chkLinkedServer.Checked
        If chkLinkedServer.Checked = False Then SetLinkedServer()
    End Sub

    Private Sub chkDataSource_CheckedChanged(sender As Object, e As EventArgs) Handles chkDataSource.CheckedChanged
        txtDataSource.Enabled = chkDataSource.Checked
        If chkDataSource.Checked = False Then SetDataSource()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub chkInstance_CheckedChanged(sender As Object, e As EventArgs) Handles chkInstance.CheckedChanged
        txtInstance.Enabled = chkInstance.Checked
        If chkInstance.Checked Then
            txtInstance.Text = txtInstance.Tag
        Else
            txtInstance.Tag = txtInstance.Text
            txtInstance.Text = ""
        End If
    End Sub

    Private Sub chkTcpPort_CheckedChanged(sender As Object, e As EventArgs) Handles chkTcpPort.CheckedChanged
        txtTcpPort.Enabled = chkTcpPort.Checked
        If chkTcpPort.Checked Then
            txtTcpPort.Text = txtTcpPort.Tag
        Else
            txtTcpPort.Tag = txtTcpPort.Text
            txtTcpPort.Text = ""
        End If
    End Sub

    Private Sub chkDomain_CheckedChanged(sender As Object, e As EventArgs) Handles chkDomain.CheckedChanged
        txtDomain.Enabled = chkDomain.Checked
        If chkDomain.Checked Then
            txtDomain.Text = txtDomain.Tag
        Else
            txtDomain.Tag = txtDomain.Text
            txtDomain.Text = ""
        End If
    End Sub

    Private Sub txtInstance_TextChanged(sender As Object, e As EventArgs) Handles txtInstance.TextChanged
        SetLinkedServer()
        SetDataSource()
    End Sub

    Private Sub txtTcpPort_TextChanged(sender As Object, e As EventArgs) Handles txtTcpPort.TextChanged
        SetDataSource()
    End Sub

    Private Sub txtDomain_TextChanged(sender As Object, e As EventArgs) Handles txtDomain.TextChanged
        SetDataSource()
    End Sub

    Private Sub btnLinkedServerClear_Click(sender As Object, e As EventArgs) Handles btnLinkedServerClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        ClearAll()
        CursorControl()
    End Sub

    Private Sub btnColumnsImport_Click(sender As Object, e As EventArgs) Handles btnColumnsImport.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If LinkedServersLoad() = True Then WriteStatus("Linked Servers loaded", 0, lblStatusText)
        CursorControl()
    End Sub

    Private Sub lvwLinkedServers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwLinkedServers.SelectedIndexChanged
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        LinkedServerSelect()
        CursorControl()
    End Sub

    Private Sub btnLinkedServerAdd_Click(sender As Object, e As EventArgs) Handles btnLinkedServerAdd.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        LinkedServerAdd(True)
        CursorControl()
    End Sub

    Private Sub btnLinkedServerDelete_Click(sender As Object, e As EventArgs) Handles btnLinkedServerDelete.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        LinkedServerDelete()
        CursorControl()
    End Sub

    Private Sub btnLinkedServerUpdate_Click(sender As Object, e As EventArgs) Handles btnLinkedServerUpdate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        LinkedServerAdd(False)
        CursorControl()
    End Sub

#End Region

    Private Sub SetLinkedServer()
        Dim strLinkedServer As String = ""
        If chkLinkedServer.Checked = False Then
            strLinkedServer = txtServer.Text
            If chkInstance.Checked = True And txtInstance.Text.Length > 0 Then
                strLinkedServer &= "\" & txtInstance.Text
            End If
            txtLinkedServer.Text = strLinkedServer
        End If
    End Sub

    Private Sub SetDataSource()
        Dim strDataSource As String = ""
        If chkDataSource.Checked = False Then
            strDataSource = txtServer.Text
            If chkDomain.Checked = True And txtDomain.Text.Length > 0 Then
                strDataSource &= "." & txtDomain.Text
            End If
            If chkInstance.Checked = True And txtInstance.Text.Length > 0 Then
                strDataSource &= "\" & txtInstance.Text
            End If
            If chkTcpPort.Checked = True And txtTcpPort.Text.Length > 0 Then
                strDataSource &= "," & txtTcpPort.Text
            End If
            txtDataSource.Text = strDataSource
        End If
    End Sub

    Private Sub ClearAll()
        txtHostServer.Text = SeqData.dhdConnection.DataLocation
        txtLinkedServerName.Text = ""
        txtServer.Text = ""
        txtInstance.Text = ""
        chkInstance.Checked = True
        txtTcpPort.Text = ""
        chkTcpPort.Checked = True
        txtDomain.Text = ""
        chkDomain.Checked = True
        chkLinkedServer.Checked = False
        chkDataSource.Checked = False
        txtProvider.Text = "SQLNCLI"
        txtServerProduct.Text = "SQL_Server"
        txtConnectionTimeout.Text = "0"
        txtCollationName.Text = ""
        txtQueryTimeout.Text = "0"
        txtLocalLoginName.Text = ""
        txtRemoteLoginName.Text = ""
        txtPassword.Text = ""
        chkCollationCompatible.Checked = False
        chkDataAccess.Checked = True
        chkLazySchema.Checked = False
        chkRemoteCollation.Checked = True
        chkRPTPromotion.Checked = False
        chkDistributor.Checked = False
        chkPublisher.Checked = False
        chkSubscriber.Checked = False
        chkRpc.Checked = True
        chkRpcOut.Checked = True
    End Sub

    Private Function LinkedServersLoad() As Boolean
        If txtHostServer.Text.Length = 0 Then
            WriteStatus("A host server is required for loading Linked Servers.", 2, lblStatusText)
            Return False
        End If
        strQuery = "SELECT server_id,[name] COLLATE DATABASE_DEFAULT as [Name],product,provider,data_source,location,provider_string,[catalog]"
        strQuery &= ",connect_timeout,query_timeout,is_linked,is_remote_login_enabled,is_rpc_out_enabled,is_data_access_enabled,is_collation_compatible"
        strQuery &= ",uses_remote_collation,collation_name,lazy_schema_validation,is_system,is_publisher,is_subscriber,is_distributor"
        strQuery &= ",is_nonsql_subscriber,is_remote_proc_transaction_promotion_enabled,modify_date"
        strQuery &= " FROM sys.servers WHERE is_linked=1 ORDER BY [name] ASC"

        Dim strDataSource As String = ""
        Dim intDomain As Integer = 0, intDomainLength As Integer = 0
        Dim intInstance As Integer = 0, intInstanceLength As Integer = 0
        Dim intPort As Integer = 0, intPortLength As Integer = 0

        Dim objData As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)
        If SeqData.dhdText.DatasetCheck(objData) = False Then
            SeqData.WriteLog("No Linked Servers were found. Please check your settings. Connection:" & strDataSource, 1)
            WriteStatus("No Linked Servers were found. Please check your settings.", 2, lblStatusText)
            Return False
        End If
        lvwLinkedServers.Items.Clear()
        For intRowCount1 As Integer = 0 To objData.Tables(0).Rows.Count - 1
            Try
                If IsDBNull(objData.Tables.Item(0).Rows(intRowCount1).Item("data_source")) = False Then
                    strDataSource = objData.Tables.Item(0).Rows(intRowCount1).Item("data_source")
                Else
                    strDataSource = ""
                End If
                intDomain = strDataSource.IndexOf(".")
                intInstance = strDataSource.IndexOf("\")
                intPort = strDataSource.IndexOf(",")
                If intPort = -1 And intInstance > 0 Then
                    intInstanceLength = strDataSource.Length - intInstance
                ElseIf intPort > 0 And intInstance > 0 Then
                    intInstanceLength = intPort - intInstance
                End If
                If intDomain > 0 Then
                    If intInstance > 0 Then
                        intDomainLength = intInstance - intDomain
                    ElseIf intPort > 0 Then
                        intDomainLength = intPort - intDomain
                    Else
                        intDomainLength = strDataSource.Length - intDomain
                    End If
                End If
                Dim lsvItem As New ListViewItem

                lsvItem.Tag = objData.Tables.Item(0).Rows(intRowCount1).Item("server_id")
                'Add the Server name
                If strDataSource.Length > 0 Then
                    If intDomain > 0 Then
                        lsvItem.Text = strDataSource.Substring(0, intDomain)
                    ElseIf intInstance > 0 Then
                        lsvItem.Text = strDataSource.Substring(0, intInstance)
                    ElseIf intPort > 0 Then
                        lsvItem.Text = strDataSource.Substring(0, intPort)
                    Else
                        lsvItem.Text = strDataSource
                    End If
                Else
                    lsvItem.Text = objData.Tables.Item(0).Rows(intRowCount1).Item("name")
                End If

                'Add Instance Name
                If intInstance > 0 Then
                    lsvItem.SubItems.Add(strDataSource.Substring(intInstance + 1, intInstanceLength - 1))
                Else
                    lsvItem.SubItems.Add("")
                End If
                'Add Domain Name
                If intDomain > 0 Then
                    lsvItem.SubItems.Add(strDataSource.Substring(intDomain + 1, intDomainLength - 1))
                Else
                    lsvItem.SubItems.Add("")
                End If
                'Add port number
                If intPort > 0 Then
                    lsvItem.SubItems.Add(strDataSource.Substring(intPort + 1, strDataSource.Length - intPort - 1))
                Else
                    lsvItem.SubItems.Add("")
                End If
                lvwLinkedServers.Items.Add(lsvItem)
                WriteStatus("Linked Server " & strDataSource & " loaded", 0, lblStatusText)

                'End If
            Catch ex As Exception
                SeqData.WriteLog("There was a problem processing the Linked Server with datasource:" & strDataSource & Environment.NewLine & ex.Message, 1)
                WriteStatus("There was a problem processing the Linked Server with datasource:" & strDataSource & ". Please check the log", 1, lblStatusText)
                If MessageBox.Show("There was a problem processing the Linked Server with datasource:" & strDataSource & Environment.NewLine & ex.Message & Environment.NewLine & Environment.NewLine & "Do you wish to continue loading Linked Servers?", "Error Loading Linked Server", MessageBoxButtons.YesNo, MessageBoxIcon.Error) = Windows.Forms.DialogResult.No Then
                    Return False
                End If
            End Try
        Next
        Return True
    End Function

    Private Sub LinkedServerAdd(Optional blnCreate As Boolean = True)
        If txtDataSource.Text.Length = 0 Then
            WriteStatus("A valid datasource is required for creating or updating a Linked Server", 2, lblStatusText)
            Exit Sub
        End If
        strQuery = "USE [master]; " & Environment.NewLine
        strQuery &= " DECLARE      @ServerName nvarchar(100),@LinkedServerName nvarchar(255),@DataSource nvarchar(255),@Instance nvarchar(100),@Domain nvarchar(100),@Port nvarchar(10)" & Environment.NewLine

        strQuery &= " SET @ServerName = '" & txtServer.Text & "'" & Environment.NewLine
        If txtInstance.Text.Length > 0 Then strQuery &= " SET @Instance = '" & txtInstance.Text & "'" & Environment.NewLine
        If txtDomain.Text.Length > 0 Then strQuery &= " SET @Domain = '" & txtDomain.Text & "'" & Environment.NewLine
        If txtTcpPort.Text.Length > 0 Then strQuery &= " SET @Port = '" & txtTcpPort.Text & "'" & Environment.NewLine

        strQuery &= " SET @LinkedServerName = UPPER(@ServerName + COALESCE('\' + @Instance,''));" & Environment.NewLine
        strQuery &= " SET @DataSource = LOWER(@ServerName + COALESCE('.' + @Domain,'')  + COALESCE('\' + @Instance,'')) + COALESCE(',' + @Port,'');" & Environment.NewLine

        If blnCreate = True Then
            strQuery &= " EXEC master.dbo.sp_addlinkedserver @server = @LinkedServerName, @srvproduct=N'SQL_Server', @provider=N'SQLNCLI', @datasrc=@DataSource;" & Environment.NewLine
            strQuery &= " EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=@LinkedServerName,@useself=N'True',@locallogin=NULL,@rmtuser=NULL,@rmtpassword=NULL;" & Environment.NewLine
        End If


        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'collation compatible', @optvalue=N'" & chkCollationCompatible.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'data access', @optvalue=N'" & chkDataAccess.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'dist', @optvalue=N'" & chkDistributor.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'pub', @optvalue=N'" & chkPublisher.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'rpc', @optvalue=N'" & chkRpc.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'rpc out', @optvalue=N'" & chkRpcOut.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'sub', @optvalue=N'" & chkSubscriber.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'connect timeout', @optvalue=N'" & txtConnectionTimeout.Text & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'collation name', @optvalue="
        If txtCollationName.Text.Length = 0 Then
            strQuery &= "NULL"
        Else
            strQuery &= txtCollationName.Text.Length
        End If
        strQuery &= ";" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'lazy schema validation', @optvalue=N'" & chkLazySchema.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'query timeout', @optvalue=N'" & txtQueryTimeout.Text & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'use remote collation', @optvalue=N'" & chkRemoteCollation.Checked & "';" & Environment.NewLine
        strQuery &= " EXEC master.dbo.sp_serveroption @server=@LinkedServerName, @optname=N'remote proc transaction promotion', @optvalue=N'" & chkRPTPromotion.Checked & "';" & Environment.NewLine

        SeqData.QueryDb(SeqData.dhdConnection, strQuery, False)
        If SeqData.dhdConnection.ErrorLevel = -1 Then
            WriteStatus("There was an error saving the Linked Server. Please check the log.", 1, lblStatusText)
            SeqData.WriteLog("There was an error saving the Linked Server. " & SeqData.dhdConnection.ErrorMessage, 1)
        Else
            WriteStatus("Linked Server saved", 0, lblStatusText)
            LinkedServersLoad()
        End If

    End Sub

    Private Sub LinkedServerDelete()
        Dim strSelection As String = txtLinkedServerName.Text
        If strSelection.Length = 0 Then
            WriteStatus("No Linked Server was selected for deletion.", 2, lblStatusText)
            Exit Sub
        End If
        'If txtInstance.Text.Length > 0 Then strSelection &= "\" & txtInstance.Text
        If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub

        strQuery = "master.dbo.sp_dropserver @server=N'" & strSelection & "', @droplogins='droplogins'"
        SeqData.QueryDb(SeqData.dhdConnection, strQuery, False)
        If SeqData.dhdConnection.ErrorLevel = -1 Then
            WriteStatus("There was an error deleting the Linked Server. Please check the log.", 1, lblStatusText)
            SeqData.WriteLog("There was an error deleting the Linked Server. " & SeqData.dhdConnection.ErrorMessage, 1)
        Else
            WriteStatus("Linked Server deleted", 0, lblStatusText)
            txtLinkedServerName.Text = ""
            LinkedServersLoad()
        End If
    End Sub

    Private Sub LinkedServerSelect()
        If lvwLinkedServers.SelectedItems.Count = 1 Then
            strQuery = "SELECT srv.server_id,srv.[name] COLLATE DATABASE_DEFAULT as [Name],srv.product,srv.provider,srv.data_source,srv.[catalog]"
            strQuery &= ",srv.connect_timeout,srv.query_timeout,srv.is_remote_login_enabled,srv.is_rpc_out_enabled,srv.is_data_access_enabled,srv.is_collation_compatible"
            strQuery &= ",srv.uses_remote_collation,srv.collation_name,srv.lazy_schema_validation,srv.is_system,srv.is_publisher,srv.is_subscriber,srv.is_distributor"
            strQuery &= ",srv.is_remote_proc_transaction_promotion_enabled,srv.modify_date"
            'strQuery &= ",lin.local_principal_id,lin.uses_self_credential,lin.remote_name"
            strQuery &= " FROM sys.servers srv "
            'strQuery &= "  OUTER APPLY (SELECT TOP 1 * FROM sys.linked_logins WHERE server_id = srv.server_id) lin  "
            strQuery &= " WHERE srv.is_linked=1 "
            strQuery &= " AND server_id = " & lvwLinkedServers.SelectedItems(0).Tag
            strQuery &= " ORDER BY [name] ASC"

            Dim objData As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)
            If objData Is Nothing Then Exit Sub
            If objData.Tables.Count = 0 Then Exit Sub
            If objData.Tables(0).Rows.Count = 0 Then Exit Sub
            ClearAll()
            For intRowCount1 As Integer = 0 To objData.Tables(0).Rows.Count - 1
                'If objData.Tables.Item(0).Rows(intRowCount1).Item(0).GetType().ToString = "System.DBNull" Then
                'MessageBox.Show("Cell Must be empty")
                'Else
                txtLinkedServerName.Text = objData.Tables.Item(0).Rows(intRowCount1).Item("Name")
                txtServer.Tag = objData.Tables.Item(0).Rows(intRowCount1).Item("server_id")
                txtServer.Text = lvwLinkedServers.SelectedItems(0).SubItems(0).Text
                txtInstance.Text = lvwLinkedServers.SelectedItems(0).SubItems(1).Text
                txtDomain.Text = lvwLinkedServers.SelectedItems(0).SubItems(2).Text
                txtTcpPort.Text = lvwLinkedServers.SelectedItems(0).SubItems(3).Text
                txtProvider.Text = If(objData.Tables.Item(0).Rows(intRowCount1).Field(Of String)("provider"), "")
                txtServerProduct.Text = If(objData.Tables.Item(0).Rows(intRowCount1).Field(Of String)("product"), "")
                txtConnectionTimeout.Text = objData.Tables.Item(0).Rows(intRowCount1).Item("connect_timeout")
                txtCollationName.Text = If(objData.Tables.Item(0).Rows(intRowCount1).Field(Of String)("collation_name"), "")
                txtQueryTimeout.Text = objData.Tables.Item(0).Rows(intRowCount1).Item("query_timeout")
                'txtLocalLoginName.Text = objData.Tables.Item(0).Rows(intRowCount1).Item("")
                'txtRemoteLoginName.Text = objData.Tables.Item(0).Rows(intRowCount1).Item("")
                'txtPassword.Text = objData.Tables.Item(0).Rows(intRowCount1).Item("")

                chkCollationCompatible.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_collation_compatible")
                chkDataAccess.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_data_access_enabled")
                chkLazySchema.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("lazy_schema_validation")
                chkRemoteCollation.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("uses_remote_collation")
                chkRPTPromotion.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_remote_proc_transaction_promotion_enabled")
                chkDistributor.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_distributor")
                chkPublisher.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_publisher")
                chkSubscriber.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_subscriber")
                chkRpc.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_remote_login_enabled")
                chkRpcOut.Checked = objData.Tables.Item(0).Rows(intRowCount1).Item("is_rpc_out_enabled")

                'End If
            Next
        End If
    End Sub
End Class