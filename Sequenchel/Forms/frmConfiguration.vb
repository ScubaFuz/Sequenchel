Imports System.IO
Imports System.Xml

Public Class frmConfiguration

    Private Sub frmConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        If SeqData.curVar.DebugMode Then btnTest.Visible = True
        cbxDataProvider.SelectedItem = "SQL"
        cbxLoginMethod.SelectedItem = "Windows"
        SecuritySet()
        DataTypesLoad()
        'SetDefaultText(txtPassword)
        ConnectionsLoad()
        TableSetsLoad()
        TablesLoad()
        txtDefaultPath.Text = SeqData.curVar.DefaultConfigFilePath
        txtTableSetName.Text = SeqData.curVar.TableSetName
        If lvwConnections.Items.Count = 0 Then ConnectionEdit()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        WriteStatus("", 0, lblStatusText)
        If lstServers.Visible = True Then
            lstServers.Visible = False
        End If
        If lstDatabases.Visible = True Then
            lstDatabases.Visible = False
        End If
        If lstTables.Visible = True Then
            lstTables.Visible = False
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub SecuritySet()
        If SeqData.curVar.AllowUpdate = False Then chkTableUpdate.Enabled = False
        If SeqData.curVar.AllowInsert = False Then chkTableInsert.Enabled = False
        If SeqData.curVar.AllowDelete = False Then chkTableDelete.Enabled = False
    End Sub

    Private Sub ConfigurationSave()
        If SeqData.curStatus.ConnectionChanged = True Then
            SeqData.curStatus.ConnectionReload = True
            SeqData.dhdText.SaveXmlFile(xmlConnections, SeqData.dhdText.PathConvert(SeqData.CheckFilePath(SeqData.curVar.ConnectionsFile)), True)
            SeqData.curStatus.ConnectionChanged = False
        End If
        If SeqData.curStatus.TableSetChanged = True Then
            SeqData.curStatus.TableSetReload = True
            SeqData.dhdText.SaveXmlFile(xmlTableSets, SeqData.dhdText.PathConvert(SeqData.CheckFilePath(SeqData.curVar.TableSetsFile)), True)
            SeqData.curStatus.TableSetChanged = False
        End If
        If SeqData.curStatus.TableChanged = True Then
            SeqData.curStatus.TableReload = True
            SeqData.dhdText.SaveXmlFile(xmlTables, SeqData.dhdText.PathConvert(SeqData.CheckFilePath(SeqData.curVar.TablesFile)), True)
            SeqData.curStatus.TableChanged = False
        End If
    End Sub

    Private Sub DataTypesLoad()
        cbxDataType.Items.Clear()
        Dim lstDataTypes As List(Of String) = SeqData.GetDataTypes()
        For Each DataType In lstDataTypes
            cbxDataType.Items.Add(DataType)
        Next
    End Sub

#Region "Connections"
#Region "Controls"

    Private Sub lvwConnections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwConnections.SelectedIndexChanged
        CursorControl("Wait")
        ConnectionLoad()
        CursorControl()
    End Sub

    Private Sub txtConnectionName_TextChanged(sender As Object, e As EventArgs) Handles txtConnectionName.TextChanged
        txtTableSetsFile.Text = SeqData.curVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
    End Sub

    Private Sub btnDefaultTableSetFile_Click(sender As Object, e As EventArgs) Handles btnDefaultTableSetFile.Click
        txtTableSetsFile.Text = SeqData.curVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
    End Sub

    Private Sub btnTableSetFileBrowse_Click(sender As Object, e As EventArgs) Handles btnTableSetFileBrowse.Click
        Dim loadFile1 As New OpenFileDialog
        loadFile1.InitialDirectory = SeqData.curVar.DefaultConfigFilePath
        loadFile1.Title = "Tablesets File"
        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Sequenchel Tablesets Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            txtTableSetsFile.Text = loadFile1.FileName
        End If
    End Sub

    Private Sub btnCrawlServers_Click(sender As Object, e As EventArgs) Handles btnCrawlServers.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        GetSqlInstances()
        CursorControl()
    End Sub

    Private Sub cbxLoginMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxLoginMethod.SelectedIndexChanged
        If cbxLoginMethod.SelectedItem = "SQL" Then
            txtLoginName.Enabled = True
            txtPassword.Enabled = True
            txtLoginName.Text = txtLoginName.Tag
            'txtPassword.Text = SeqData.dhdMainDB.Password
            SetDefaultText(txtPassword)
            PasswordCharSet(txtPassword)
        ElseIf cbxLoginMethod.SelectedItem = "Windows" Then
            txtLoginName.Enabled = False
            txtPassword.Enabled = False
            txtLoginName.Text = ""
            'txtPassword.Text = ""
            RemoveDefaultText(txtPassword)
            PasswordCharSet(txtPassword)
        End If
    End Sub

    Private Sub lstServers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstServers.SelectedIndexChanged
        txtDataLocation.Text = lstServers.SelectedItem
        lstServers.Visible = False
    End Sub

    Private Sub lstServers_LostFocus(sender As Object, e As EventArgs) Handles lstServers.LostFocus
        lstServers.Visible = False
    End Sub

    Private Sub btnCrawlDatabases_Click(sender As Object, e As EventArgs) Handles btnCrawlDatabases.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        'Application.DoEvents()

        SeqData.dhdConnection.DatabaseName = "master"
        SeqData.dhdConnection.DataLocation = txtDataLocation.Text
        SeqData.dhdConnection.DataProvider = cbxDataProvider.SelectedItem
        SeqData.dhdConnection.LoginMethod = cbxLoginMethod.SelectedItem
        SeqData.dhdConnection.LoginName = txtLoginName.Text
        SeqData.dhdConnection.Password = txtPassword.Text

        If CheckSqlVersion(SeqData.dhdConnection) = False Then Exit Sub

        strQuery = "SELECT name FROM sys.databases WHERE database_id <> 2"
        Dim dtsData As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)

        If dtsData Is Nothing Then
            CursorControl()
            WriteStatus(SeqData.ErrorMessage, 1, lblStatusText)
            Exit Sub
        End If
        If dtsData.Tables.Count = 0 Then Exit Sub
        If dtsData.Tables(0).Rows.Count = 0 Then Exit Sub

        lstDatabases.Items.Clear()
        lstDatabases.Width = 147
        lstDatabases.Items.Add("")
        For intRowCount = 0 To dtsData.Tables(0).Rows.Count - 1
            'If dtsData.Tables.Item(0).Rows(intRowCount).Item(0).GetType().ToString = "System.DBNull" Then
            'MessageBox.Show("Cell Must be empty")
            'Else
            lstDatabases.Items.Add(dtsData.Tables.Item(0).Rows(intRowCount).Item("name"))
            'End If
        Next

        lstDatabases.AutoSize = True
        lstDatabases.Height = lstDatabases.Items.Count * 15
        lstDatabases.Visible = True
        CursorControl()
        lstDatabases.Focus()
    End Sub

    Private Sub lstDatabases_LostFocus(sender As Object, e As EventArgs) Handles lstDatabases.LostFocus
        lstDatabases.Visible = False
    End Sub

    Private Sub lstDatabases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDatabases.SelectedIndexChanged
        txtDataBaseName.Text = lstDatabases.SelectedItem
        lstDatabases.Visible = False
    End Sub

    Private Sub btnConnectionsShow_Click(sender As Object, e As EventArgs) Handles btnConnectionsShow.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        DisplayXmlFile(xmlConnections, tvwConnection)
        CursorControl()
    End Sub

    Private Sub btnConnectionDefault_Click(sender As Object, e As EventArgs) Handles btnConnectionDefault.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If lvwConnections.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwConnections.SelectedItems.Item(0).Tag

            'Get the ParentNode
            Dim xNode0 As XmlNode = SeqData.dhdText.FindXmlNode(xmlConnections, "Connections")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = SeqData.dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"
            SeqData.curStatus.ConnectionChanged = True
            ConfigurationSave()
            ConnectionLoad()
            WriteStatus("Default connection set", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnConnectionClear_Click(sender As Object, e As EventArgs) Handles btnConnectionClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        ConnectionClear()
        CursorControl()
    End Sub

    Private Sub btnConnectionDelete_Click(sender As Object, e As EventArgs) Handles btnConnectionDelete.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        ConnectionDelete()
        CursorControl()
    End Sub

    Private Sub btnConnectionAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnConnectionAddOrUpdate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        ConnectionAddOrUpdate()
        CursorControl()
    End Sub

    Private Sub txtPassword_GotFocus(sender As Object, e As EventArgs) Handles txtPassword.GotFocus
        RemoveDefaultText(sender)
        PasswordCharSet(sender)
    End Sub

    Private Sub txtPassword_LostFocus(sender As Object, e As EventArgs) Handles txtPassword.LostFocus
        SetDefaultText(sender)
        PasswordCharSet(sender)
    End Sub

    Private Sub btnShowDatabasePassword_MouseDown(sender As Object, e As MouseEventArgs) Handles btnShowDatabasePassword.MouseDown
        txtPassword.PasswordChar = Nothing
    End Sub

    Private Sub btnShowDatabasePassword_MouseLeave(sender As Object, e As EventArgs) Handles btnShowDatabasePassword.MouseLeave
        PasswordCharSet(txtPassword)
    End Sub

    Private Sub btnShowDatabasePassword_MouseUp(sender As Object, e As MouseEventArgs) Handles btnShowDatabasePassword.MouseUp
        PasswordCharSet(txtPassword)
    End Sub

#End Region

    Private Sub ConnectionsLoad()
        lvwConnections.Items.Clear()
        Dim lstXml As XmlNodeList
        lstXml = SeqData.dhdText.FindXmlNodes(xmlConnections, "//Connection")
        If lstXml Is Nothing Then Exit Sub
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Tag = xNode.Item("ConnectionName").InnerText
            lsvItem.Text = xNode.Item("ConnectionName").InnerText
            lsvItem.SubItems.Add(xNode.Item("DataBaseName").InnerText)
            lsvItem.SubItems.Add(xNode.Item("DataLocation").InnerText)
            lvwConnections.Items.Add(lsvItem)

            If xNode.Item("ConnectionName").InnerText = SeqData.curStatus.Connection Then
                SeqData.dhdConnection.DatabaseName = xNode.Item("DataBaseName").InnerText
                SeqData.dhdConnection.DataLocation = xNode.Item("DataLocation").InnerText
                SeqData.dhdConnection.DataProvider = xNode.Item("DataProvider").InnerText
                SeqData.dhdConnection.LoginMethod = xNode.Item("LoginMethod").InnerText
                SeqData.dhdConnection.LoginName = xNode.Item("LoginName").InnerText
                If SeqData.dhdText.CheckNodeElement(xNode, "Password") Then SeqData.dhdConnection.Password = DataHandler.txt.DecryptText(xNode.Item("Password").InnerText)
            End If
        Next
        For Each lstItem In lvwConnections.Items
            If lstItem.Tag = SeqData.curStatus.Connection Then
                lstItem.Selected = True
            End If
        Next
        'lvwConnections.SelectedItems(0)
    End Sub

    Private Sub ConnectionLoad()
        If lvwConnections.SelectedItems.Count = 1 Then
            'If CurStatus.Connection <> lvwConnections.SelectedItems.Item(0).Tag Then
            SeqData.curStatus.ConnectionReload = True
            SeqData.curStatus.Connection = lvwConnections.SelectedItems.Item(0).Tag
            SeqData.LoadConnection(xmlConnections, SeqData.curStatus.Connection)
            TableSetClear()
            SeqData.LoadTableSetsXml(xmlTableSets)
            'If lstTableSets Is Nothing Then
            '    xmlTableSets.RemoveAll()
            '    xmlTables.RemoveAll()
            '    SeqData.curVar.TablesFile = ""
            '    TableClear()
            '    Exit Sub
            'End If
            SeqData.LoadTableSet(xmlTableSets, SeqData.curStatus.TableSet)
            TableSetsLoad()
            SeqData.LoadTablesXml(xmlTables)
            TablesLoad()
            'End If

            Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", SeqData.curStatus.Connection)
            tvwConnection.Nodes.Clear()
            DisplayXmlNode(xNode, tvwConnection.Nodes)
            tvwConnection.ExpandAll()
            ConnectionEdit()
        End If

    End Sub

    Friend Sub GetSqlInstances()
        Dim strResult As String = ""

        Using dtSqlSources As DataTable = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources()
            If dtSqlSources.Rows.Count > 0 Then
                lstServers.Items.Clear()
                lstServers.Width = 147
                lstServers.Items.Add("")
                For Each objDR As DataRow In dtSqlSources.Rows
                    strResult = objDR("ServerName")
                    If objDR("InstanceName").ToString.Length > 0 Then strResult &= "\" & objDR("InstanceName")
                    lstServers.Items.Add(strResult)
                Next

                lstServers.AutoSize = True
                lstServers.Height = lstServers.Items.Count * 15
                lstServers.Visible = True
                lstServers.Focus()
            Else
                MessageBox.Show("No servers found")
            End If
        End Using
    End Sub

    Private Sub ConnectionEdit()
        If lvwConnections.SelectedItems.Count = 1 Then
            Dim strConnection As String = lvwConnections.SelectedItems.Item(0).Tag

            Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strConnection)
            txtConnectionName.Text = xNode.Item("ConnectionName").InnerText
            txtConnectionName.Tag = xNode.Item("ConnectionName").InnerText
            txtDataBaseName.Text = xNode.Item("DataBaseName").InnerText
            txtDataLocation.Text = xNode.Item("DataLocation").InnerText
            txtLoginName.Tag = xNode.Item("LoginName").InnerText
            txtLoginName.Text = xNode.Item("LoginName").InnerText
            'txtPassword.Text = DataHandler.txt.DecryptText(xNode.Item("Password").InnerText)
            cbxDataProvider.Text = xNode.Item("DataProvider").InnerText
            cbxLoginMethod.Text = xNode.Item("LoginMethod").InnerText
            txtTimeOut.Text = xNode.Item("Timeout").InnerText
            txtTableSetsFile.Text = xNode.Item("TableSets").InnerText
        ElseIf lvwConnections.Items.Count = 0 Then
            txtConnectionName.Text = SeqData.dhdConnection.DatabaseName
            txtConnectionName.Tag = SeqData.dhdConnection.DatabaseName
            txtDataBaseName.Text = SeqData.dhdConnection.DatabaseName
            txtDataLocation.Text = SeqData.dhdConnection.DataLocation
            txtLoginName.Tag = SeqData.dhdConnection.LoginName
            txtLoginName.Text = SeqData.dhdConnection.LoginName
            'txtPassword.Text = DataHandler.txt.DecryptText(xNode.Item("Password").InnerText)
            cbxDataProvider.Text = SeqData.dhdConnection.DataProvider
            cbxLoginMethod.Text = SeqData.dhdConnection.LoginMethod
            txtTimeOut.Text = SeqData.dhdConnection.ConnectionTimeout
            txtTableSetsFile.Text = SeqData.curVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
        End If
    End Sub

    Private Sub ConnectionAddOrUpdate()
        'dhdText.RemoveNode(xmlConnections, "Connections", "ConnectionName", txtConnectionName.Text)
        Dim root As XmlElement = xmlConnections.DocumentElement
        If root Is Nothing Then
            xmlConnections = SeqData.dhdText.CreateRootDocument(xmlConnections, "Sequenchel", "Connections", True)
        End If

        If txtConnectionName.Text.Length < 2 Or txtDataLocation.Text.Length < 2 Or txtDataBaseName.Text.Length < 2 Or txtTableSetsFile.Text.Length < 2 Then
            WriteStatus(Core.Message.strAllData, 2, lblStatusText)
            Exit Sub
        End If

        Dim xCNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", txtConnectionName.Text)
        If xCNode Is Nothing Then
            xCNode = SeqData.dhdText.CreateAppendElement(xmlConnections.Item("Sequenchel").Item("Connections"), "Connection", Nothing, False)
        End If

        SeqData.dhdText.CreateAppendAttribute(xCNode, "Default", "False", True)
        SeqData.dhdText.CreateAppendElement(xCNode, "ConnectionName", txtConnectionName.Text, True)
        SeqData.dhdText.CreateAppendElement(xCNode, "DataBaseName", txtDataBaseName.Text, True)
        SeqData.dhdText.CreateAppendElement(xCNode, "DataLocation", txtDataLocation.Text, True)
        SeqData.dhdText.CreateAppendElement(xCNode, "LoginName", txtLoginName.Text, True)
        If txtPassword.Text <> txtPassword.Tag Then SeqData.dhdText.CreateAppendElement(xCNode, "Password", DataHandler.txt.EncryptText(txtPassword.Text), True)
        SeqData.dhdText.CreateAppendElement(xCNode, "DataProvider", cbxDataProvider.Text, True)
        SeqData.dhdText.CreateAppendElement(xCNode, "LoginMethod", cbxLoginMethod.Text, True)
        SeqData.dhdText.CreateAppendElement(xCNode, "Timeout", txtTimeOut.Text, True)
        SeqData.dhdText.CreateAppendElement(xCNode, "TableSets", txtTableSetsFile.Text, True)
        SeqData.curStatus.Connection = txtConnectionName.Text
        SeqData.curVar.TableSetsFile = txtTableSetsFile.Text
        SeqData.curStatus.TableSet = ""
        SeqData.curStatus.Table = ""
        SeqData.curStatus.ConnectionChanged = True
        ConfigurationSave()
        ConnectionsLoad()
        WriteStatus("Connection Saved", 0, lblStatusText)
    End Sub

    Private Sub ConnectionDelete()
        Dim strSelection As String = txtConnectionName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            SeqData.curStatus.ConnectionChanged = True
            ConfigurationSave()
            ConnectionClear()
            ConnectionsLoad()
            WriteStatus("Connection Deleted", 0, lblStatusText)
        End If
    End Sub

    Private Sub ConnectionClear()
        txtConnectionName.Text = ""
        txtDataBaseName.Text = ""
        txtDataLocation.Text = ""
        txtLoginName.Text = ""
        txtPassword.Text = ""
        cbxDataProvider.Text = ""
        cbxLoginMethod.Text = ""
        txtTimeOut.Text = ""
        txtTableSetsFile.Text = ""
    End Sub

#End Region

#Region "TableSets"
#Region "Controls"

    Private Sub lvwTableSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwTableSets.SelectedIndexChanged
        CursorControl("Wait")
        TableSetLoad()
        CursorControl()
    End Sub

    Private Sub txtTableSetName_TextChanged(sender As Object, e As EventArgs) Handles txtTableSetName.TextChanged
        If chkDefaultValues.Checked = True Then
            AutoFillDefaults()
        End If
    End Sub

    Private Sub btnDefaultTableSet_Click(sender As Object, e As EventArgs) Handles btnDefaultTableSet.Click
        txtTableSetName.Text = SeqData.curVar.TableSetName
        If chkDefaultValues.Checked = True Then
            AutoFillDefaults()
        End If
    End Sub

    Private Sub txtDefaultPath_TextChanged(sender As Object, e As EventArgs) Handles txtDefaultPath.TextChanged
        If chkDefaultValues.Checked = True Then
            AutoFillDefaults()
        End If
    End Sub

    Private Sub btnDefaultPathSet_Click(sender As Object, e As EventArgs) Handles btnDefaultPathSet.Click
        txtDefaultPath.Text = SeqData.curVar.DefaultConfigFilePath
    End Sub

    Private Sub btnDefaultPathBrowse_Click(sender As Object, e As EventArgs) Handles btnDefaultPathBrowse.Click
        DefaultPathBrowse()
    End Sub

    Private Sub chkDefaultValues_CheckedChanged(sender As Object, e As EventArgs) Handles chkDefaultValues.CheckedChanged
        If chkDefaultValues.Checked = True Then
            AutoFillDefaults()
        End If
    End Sub

    Private Sub btnTableSetDefault_Click(sender As Object, e As EventArgs) Handles btnTableSetDefault.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If lvwTableSets.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTableSets.SelectedItems.Item(0).Tag

            'Get the ParentNode
            Dim xNode0 As XmlNode = SeqData.dhdText.FindXmlNode(xmlTableSets, "TableSets")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = SeqData.dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"

            SeqData.curStatus.TableSetChanged = True
            ConfigurationSave()
            TableSetLoad()
            WriteStatus("Default TableSet Set", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnTableSetsShow_Click(sender As Object, e As EventArgs) Handles btnTableSetsShow.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        DisplayXmlFile(xmlTableSets, tvwTableSet)
        CursorControl()
    End Sub

    Private Sub btnTableSetAdd_Click(sender As Object, e As EventArgs) Handles btnTableSetAdd.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        TableSetAddOrUpdate()
        CursorControl()
    End Sub

    Private Sub btnTableSetClear_Click(sender As Object, e As EventArgs) Handles btnTableSetClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        TableSetClear()
        CursorControl()
    End Sub

    Private Sub btnTableSetDelete_Click(sender As Object, e As EventArgs) Handles btnTableSetDelete.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim strSelection As String = txtTableSetName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            SeqData.curStatus.TableSetChanged = True
            ConfigurationSave()
            btnTableSetClear_Click(Nothing, Nothing)
            TableSetsLoad()
            WriteStatus("TableSet Deleted", 0, lblStatusText)
        Else
            WriteStatus("Unable to delete TableSet.", 2, lblStatusText)
        End If
        CursorControl()
    End Sub

#End Region

    Private Sub TableSetsLoad()
        lvwTableSets.Items.Clear()
        tvwTableSet.Nodes.Clear()
        If xmlTableSets.OuterXml.Length = 0 Then Exit Sub

        Dim lstXml As XmlNodeList
        lstXml = SeqData.dhdText.FindXmlNodes(xmlTableSets, "//TableSet")
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Tag = xNode.Item("TableSetName").InnerText
            lsvItem.Text = xNode.Item("TableSetName").InnerText
            lsvItem.SubItems.Add(xNode.Item("TablesFile").InnerText)
            lvwTableSets.Items.Add(lsvItem)
        Next
        For Each lstItemn In lvwTableSets.Items
            If lstItemn.Tag = SeqData.curStatus.TableSet Then
                lstItemn.Selected = True
            End If
        Next

    End Sub

    Private Sub TableSetLoad()
        TableSetClear()
        If lvwTableSets.SelectedItems.Count = 1 Then
            If SeqData.curStatus.TableSet <> lvwTableSets.SelectedItems.Item(0).Tag Then
                SeqData.curStatus.TableSetReload = True
                SeqData.curStatus.TableSet = lvwTableSets.SelectedItems.Item(0).Tag
                SeqData.LoadTableSet(xmlTableSets, SeqData.curStatus.TableSet)
                SeqData.LoadTablesXml(xmlTables)
                TablesLoad()
            End If
            Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", SeqData.curStatus.TableSet)
            tvwTableSet.Nodes.Clear()
            DisplayXmlNode(xNode, tvwTableSet.Nodes)
            tvwTableSet.ExpandAll()
            TableSetEdit()
        End If
    End Sub

    Private Sub DefaultPathBrowse()
        Dim fbdBrowse As New FolderBrowserDialog
        If (fbdBrowse.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (fbdBrowse.SelectedPath.Length) > 0 Then
            txtDefaultPath.Text = fbdBrowse.SelectedPath
        End If
    End Sub

    Private Sub AutoFillDefaults()
        Dim strSeparator As String = "\"
        If txtDefaultPath.Text.Length = 0 Then
            strSeparator = ""
            txtOutputPath.Text = "."
        Else
            txtOutputPath.Text = txtDefaultPath.Text
        End If
        txtTablesFile.Text = txtDefaultPath.Text & strSeparator & SeqData.curStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Tables.xml"
        txtReportsSetName.Text = txtTableSetName.Text.Replace(" ", "_") & "_Reports"
        txtReportsFile.Text = txtDefaultPath.Text & strSeparator & SeqData.curStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Reports.xml"
        txtSearchFile.Text = txtDefaultPath.Text & strSeparator & SeqData.curStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Search.xml"
    End Sub

    Private Sub TableSetEdit()
        If lvwTableSets.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTableSets.SelectedItems.Item(0).Tag

            Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strSelection)
            txtTableSetName.Text = xNode.Item("TableSetName").InnerText
            txtTableSetName.Tag = xNode.Item("TableSetName").InnerText
            txtTablesFile.Text = xNode.Item("TablesFile").InnerText
            txtOutputPath.Text = xNode.Item("OutputPath").InnerText
            txtReportsSetName.Text = xNode.Item("ReportSet").Attributes("Name").InnerText
            txtReportsFile.Text = xNode.Item("ReportSet").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Search") Then txtSearchFile.Text = xNode.Item("Search").InnerText
        End If
    End Sub

    Private Sub TableSetAddOrUpdate()
        'dhdText.RemoveNode(xmlConnections, "Connections", "ConnectionName", txtConnectionName.Text)
        If txtTableSetName.Text.Length < 2 Or txtTablesFile.Text.Length < 2 Then
            WriteStatus(Core.Message.strAllData, 2, lblStatusText)
            Exit Sub
        End If

        Dim root As XmlElement = xmlTableSets.DocumentElement
        If root Is Nothing Then
            xmlTableSets = SeqData.dhdText.CreateRootDocument(xmlTableSets, "Sequenchel", "TableSets", True)
        End If

        Dim xTNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", txtTableSetName.Text)
        If xTNode Is Nothing Then
            xTNode = SeqData.dhdText.CreateAppendElement(xmlTableSets.Item("Sequenchel").Item("TableSets"), "TableSet", Nothing, False)
        End If

        SeqData.dhdText.CreateAppendAttribute(xTNode, "Default", "False", True)
        SeqData.dhdText.CreateAppendElement(xTNode, "TableSetName", txtTableSetName.Text, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "TablesFile", txtTablesFile.Text, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "OutputPath", txtOutputPath.Text, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "ReportSet", txtReportsFile.Text, True)
        SeqData.dhdText.CreateAppendAttribute(xTNode.Item("ReportSet"), "Name", txtReportsSetName.Text, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Search", txtSearchFile.Text, True)
        SeqData.curStatus.TableSet = txtTableSetName.Text
        SeqData.curVar.TablesFile = txtTablesFile.Text
        SeqData.curStatus.Table = ""
        SeqData.curStatus.TableSetReload = True
        SeqData.curStatus.TableSetChanged = True
        ConfigurationSave()
        TableSetsLoad()
        WriteStatus("TableSet Saved", 0, lblStatusText)

    End Sub

    Private Sub TableSetClear()
        txtTableSetName.Text = ""
        txtDefaultPath.Text = ""
        txtTablesFile.Text = ""
        txtOutputPath.Text = ""
        txtReportsSetName.Text = ""
        txtReportsFile.Text = ""
        txtSearchFile.Text = ""
    End Sub

#End Region

#Region "Tables"
#Region "Controls"

    Private Sub lvwTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwTables.SelectedIndexChanged
        CursorControl("Wait")
        TableLoad()
        CursorControl()
    End Sub

    Private Sub btnCrawlTables_Click(sender As Object, e As EventArgs) Handles btnCrawlTables.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        'Application.DoEvents()

        If CheckSqlVersion(SeqData.dhdConnection) = False Then Exit Sub

        Dim lstNewTables As List(Of String) = LoadTablesList(SeqData.dhdConnection)

        If lstNewTables Is Nothing Then
            CursorControl()
            WriteStatus("No tables found", 2, lblStatusText)
            Exit Sub
        End If

        lstTables.Items.Clear()
        lstTables.Items.Add("")

        For Each TableName In lstNewTables
            lstTables.Items.Add(TableName)
        Next

        CursorControl()
        If lstTables.Items.Count < 15 Then
            lstTables.Height = lstTables.Items.Count * 15
        Else
            lstTables.Height = 15 * 15
        End If
        lstTables.Visible = True
        lstTables.Focus()

    End Sub

    Private Sub lstTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTables.SelectedIndexChanged
        txtTableName.Text = lstTables.SelectedItem
        txtTableAlias.Text = lstTables.SelectedItem.ToString.Replace(".", "_")
        chkTableVisible.Checked = True
        chkTableSearch.Checked = True
        lstTables.Visible = False
    End Sub

    Private Sub lstTables_LostFocus(sender As Object, e As EventArgs) Handles lstTables.LostFocus
        lstTables.Visible = False
    End Sub

    Private Sub btnColumnsImport_Click(sender As Object, e As EventArgs) Handles btnColumnsImport.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)

        If CheckSqlVersion(SeqData.dhdConnection) = False Then Exit Sub

        If txtTableName.Text.Length < 2 And chkImportAllTables.Checked = False Then
            WriteStatus("Please enter a schema name with a table name.", 2, lblStatusText)
            Exit Sub
        End If

        If chkImportAllTables.Checked = True Then
            ImportAllTables()
        Else
            ImportOneTable(txtTableName.Text, txtTableAlias.Text, True)
        End If

        SeqData.curStatus.TableChanged = True
        ConfigurationSave()
        CursorControl()
    End Sub

    Private Sub chkImportAllTables_CheckedChanged(sender As Object, e As EventArgs) Handles chkImportAllTables.CheckedChanged
        If chkImportAllTables.Checked Then
            btnColumnsImport.Text = "Import Columns for all tables"
        Else
            btnColumnsImport.Text = "Import Columns for this table"
        End If
    End Sub

    Private Sub btnTableDefault_Click(sender As Object, e As EventArgs) Handles btnTableDefault.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If lvwTables.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTables.SelectedItems.Item(0).Tag

            'Get the ParentNode
            Dim xNode0 As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Tables")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"
            SeqData.curStatus.TableChanged = True
            ConfigurationSave()
            TableLoad()
            WriteStatus("Default Table Set", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnTablesShow_Click(sender As Object, e As EventArgs) Handles btnTablesShow.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        DisplayXmlFile(xmlTables, tvwTable)
        CursorControl()
    End Sub

    Private Sub btnTableAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnTableAddOrUpdate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        TableAddOrUpdate(txtTableName.Text, txtTableAlias.Text, chkTableVisible.Checked, chkTableSearch.Checked, chkTableUpdate.Checked, chkTableInsert.Checked, chkTableDelete.Checked, True)
        SeqData.curStatus.TableChanged = True
        ConfigurationSave()
        WriteStatus("Table Saved", 0, lblStatusText)
        CursorControl()
    End Sub

    Private Sub btnTableClear_Click(sender As Object, e As EventArgs) Handles btnTableClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        txtTableName.Text = ""
        txtTableAlias.Text = ""
        chkTableVisible.Checked = False
        chkTableSearch.Checked = False
        chkTableUpdate.Checked = False
        chkTableInsert.Checked = False
        CursorControl()
    End Sub

    Private Sub btnTableDelete_Click(sender As Object, e As EventArgs) Handles btnTableDelete.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim strSelection As String = txtTableName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            SeqData.curStatus.TableChanged = True
            ConfigurationSave()
            btnTableClear_Click(Nothing, Nothing)
            TablesLoad()
            tvwTable.Nodes.Clear()
            WriteStatus("Table Deleted", 0, lblStatusText)
        Else
            WriteStatus("Unable to Delete Table", 2, lblStatusText)
        End If
        CursorControl()
    End Sub

#End Region

    Private Sub TablesLoad()
        lvwTables.Items.Clear()
        tvwTable.Nodes.Clear()
        If xmlTables.OuterXml.Length = 0 Then Exit Sub

        Dim lstXml As XmlNodeList
        lstXml = SeqData.dhdText.FindXmlNodes(xmlTables, "//Table")
        If lstXml Is Nothing Then Exit Sub
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Tag = xNode.Item("Name").InnerText
            lsvItem.Text = xNode.Item("Name").InnerText
            lsvItem.SubItems.Add(xNode.Item("Alias").InnerText)
            lvwTables.Items.Add(lsvItem)
        Next
        For Each lstItemn In lvwTables.Items
            If lstItemn.Tag = SeqData.curStatus.Table Then
                lstItemn.Selected = True
            End If
        Next
    End Sub

    Private Sub TableLoad()
        If lvwTables.SelectedItems.Count = 1 Then
            If SeqData.curStatus.Table <> lvwTables.SelectedItems.Item(0).Tag Then
                SeqData.curStatus.Table = lvwTables.SelectedItems.Item(0).Tag
                SeqData.curStatus.TableReload = True
            End If

            Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", SeqData.curStatus.Table)
            tvwTable.Nodes.Clear()
            DisplayXmlNode(xNode, tvwTable.Nodes)
            tvwTable.Nodes(0).Expand()
            Try
                tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
                tvwTable.SelectedNode.Expand()
            Catch ex As Exception
                tvwTable.Nodes(0).LastNode.Expand()
            End Try
            TableEdit()
        End If
    End Sub

    Private Sub ImportOneTable(strTableName As String, strTableAlias As String, blnReload As Boolean)
        TableAddOrUpdate(strTableName, strTableAlias, chkTableVisible.Checked, chkTableSearch.Checked, chkTableUpdate.Checked, chkTableInsert.Checked, chkTableDelete.Checked)

        Dim strSchemaName As String = strTableName.Substring(0, strTableName.IndexOf("."))
        Dim TableName As String = strTableName.Substring(strTableName.IndexOf(".") + 1, strTableName.Length - (strTableName.IndexOf(".") + 1))
        ColumnsImport(strSchemaName, TableName, blnReload)

    End Sub

    Private Sub ImportAllTables()
        Dim lstNewTables As List(Of String) = LoadTablesList(SeqData.dhdConnection)

        If lstNewTables Is Nothing Then
            WriteStatus("No tables found", 2, lblStatusText)
            Exit Sub
        End If

        Dim blnReload As Boolean = False
        For Each TableName In lstNewTables
            If lstNewTables.Last = TableName Then
                blnReload = True
            End If
            ImportOneTable(TableName, TableName.Replace(".", "_"), blnReload)
        Next
    End Sub

    Private Sub ColumnsImport(strSchemaName As String, strTableName As String, blnReloadAll As Boolean)

        strQuery = "SELECT col.name As colName, col.is_identity, typ.name as DataType, col.max_length as MaxLength FROM sys.columns col"
        strQuery &= " INNER JOIN sys.types typ"
        strQuery &= " ON col.system_type_id = typ.system_type_id"
        strQuery &= " AND col.user_type_id = typ.user_type_id"
        strQuery &= " INNER JOIN sys.objects obj"
        strQuery &= " ON col.object_id = obj.object_id"
        strQuery &= " INNER JOIN sys.schemas scm"
        strQuery &= " ON obj.schema_id = scm.schema_id"
        strQuery &= " WHERE obj.name = '" & strTableName & "'"
        strQuery &= " AND scm.name = '" & strSchemaName & "'"

        Dim dtsData As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)
        If dtsData Is Nothing Then
            WriteStatus("No columns found for table " & strSchemaName & "." & strTableName, 2, lblStatusText)
            Exit Sub
        End If
        If dtsData.Tables.Count = 0 Then Exit Sub
        If dtsData.Tables(0).Rows.Count = 0 Then Exit Sub

        strQuery = "SELECT FK_Schema = FK.TABLE_SCHEMA, FK_Table = FK.TABLE_NAME, FK_Column = CU.COLUMN_NAME,"
        strQuery &= " PK_Schema = PK.TABLE_SCHEMA, PK_Table = PK.TABLE_NAME, PK_Column = PT.COLUMN_NAME, Constraint_Name = C.CONSTRAINT_NAME"
        strQuery &= " FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C"
        strQuery &= " INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME"
        strQuery &= " INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME"
        strQuery &= " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME"
        strQuery &= " INNER JOIN (SELECT i1.TABLE_NAME, i2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1"
        strQuery &= " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME"
        strQuery &= " WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT ON PT.TABLE_NAME = PK.TABLE_NAME"
        strQuery &= " WHERE FK.TABLE_SCHEMA = '" & strSchemaName & "' AND FK.TABLE_NAME = '" & strTableName & "'"

        Dim dtsRelations As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)

        strQuery = " SELECT KU.TABLE_SCHEMA as SchemaName, KU.TABLE_NAME as TableName,KU.COLUMN_NAME as PrimaryKeyColumn"
        strQuery &= " FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC"
        strQuery &= " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU"
        strQuery &= " ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' "
        strQuery &= " AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME"
        strQuery &= " WHERE KU.TABLE_SCHEMA = '" & strSchemaName & "'"
        strQuery &= " AND KU.TABLE_NAME = '" & strTableName & "'"
        strQuery &= " ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION;"

        Dim dtsPrimaryKeys As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)

        Dim blnReload As Boolean = False
        Dim strDataType As String = ""
        Dim intWidth As Integer = 0, intColCount As Integer = 0, blnShowField As Boolean = False, blnIdentity As Boolean = False, blnPrimaryKey As Boolean = False
        For intRowCount = 0 To dtsData.Tables(0).Rows.Count - 1
            'If dtsData.Tables.Item(0).Rows(intRowCount).Item(0).GetType().ToString = "System.DBNull" Then
            'MessageBox.Show("Cell Must be empty")
            'Else
            blnPrimaryKey = False
            If intRowCount < 10 Then
                intColCount = intRowCount + 1
                blnShowField = True
            Else
                intColCount = 0
                blnShowField = False
            End If

            If Not dtsPrimaryKeys Is Nothing Then
                If dtsPrimaryKeys.Tables.Count > 0 Then
                    If dtsPrimaryKeys.Tables(0).Rows.Count > 0 Then
                        For intRowCountPK = 0 To dtsPrimaryKeys.Tables(0).Rows.Count - 1
                            'If dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item(0).GetType().ToString = "System.DBNull" Then
                            'MessageBox.Show("Cell Must be empty")
                            'Else
                            If dtsPrimaryKeys.Tables.Item(0).Rows(intRowCountPK).Item("SchemaName") = strSchemaName _
                            And dtsPrimaryKeys.Tables.Item(0).Rows(intRowCountPK).Item("TableName") = strTableName _
                            And dtsPrimaryKeys.Tables.Item(0).Rows(intRowCountPK).Item("PrimaryKeyColumn") = dtsData.Tables.Item(0).Rows(intRowCount).Item("colName") Then
                                blnPrimaryKey = True
                            End If
                        Next
                    End If
                End If
            End If

            strDataType = SeqData.GetDataType(dtsData.Tables.Item(0).Rows(intRowCount).Item("DataType"))
            blnIdentity = dtsData.Tables.Item(0).Rows(intRowCount).Item("is_identity")
            intWidth = SeqData.GetWidth(strDataType, dtsData.Tables.Item(0).Rows(intRowCount).Item("MaxLength"))
            If intRowCount = dtsData.Tables(0).Rows.Count - 1 And blnReloadAll = True Then blnReload = True
            FieldAddOrUpdate(strSchemaName & "." & strTableName, dtsData.Tables.Item(0).Rows(intRowCount).Item("colName"), dtsData.Tables.Item(0).Rows(intRowCount).Item("colName"), _
                     strDataType, blnIdentity, blnPrimaryKey, intWidth, "", "", "", "", False, txtControlField.Text, txtControlValue.Text, chkControlUpdate.Checked, chkControlMode.Checked, False, "", _
                     blnShowField, intColCount, intWidth, True, True, chkFieldSearchList.Checked, chkFieldUpdate.Checked, blnReload)

            If Not dtsRelations Is Nothing Then
                If dtsRelations.Tables.Count > 0 Then
                    If dtsRelations.Tables(0).Rows.Count > 0 Then
                        For intRowCountRel = 0 To dtsRelations.Tables(0).Rows.Count - 1
                            'If dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item(0).GetType().ToString = "System.DBNull" Then
                            'MessageBox.Show("Cell Must be empty")
                            'Else
                            If dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("FK_Schema") = strSchemaName _
                            And dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("FK_Table") = strTableName _
                            And dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("FK_Column") = dtsData.Tables.Item(0).Rows(intRowCount).Item("colName") Then
                                RelationAdd(strSchemaName & "." & strTableName, dtsData.Tables.Item(0).Rows(intRowCount).Item("colName"), _
                                    dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Schema") & "." & dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Table"), _
                                    dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Schema") & "." & dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Table"), _
                                    dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Column"), "", False)
                                'Add Relations for this column
                            End If
                        Next
                    End If
                End If
            End If

            'End If
        Next

    End Sub

    Private Sub TableEdit()
        If lvwTables.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTables.SelectedItems.Item(0).Tag

            Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strSelection)
            If SeqData.dhdText.CheckNodeElement(xNode, "Name") Then txtTableName.Tag = xNode.Item("Name").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Name") Then txtTableName.Text = xNode.Item("Name").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Alias") Then txtTableAlias.Text = xNode.Item("Alias").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Visible") Then chkTableVisible.Checked = xNode.Item("Visible").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Search") Then chkTableSearch.Checked = xNode.Item("Search").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Update") Then chkTableUpdate.Checked = xNode.Item("Update").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Insert") Then chkTableInsert.Checked = xNode.Item("Insert").InnerText
            If SeqData.dhdText.CheckNodeElement(xNode, "Delete") Then chkTableDelete.Checked = xNode.Item("Delete").InnerText
        End If
    End Sub

    Private Sub TableAddOrUpdate(strTableName As String, strAlias As String, blnVisible As Boolean, blnSearch As Boolean, blnUpdate As Boolean, blnInsert As Boolean, blnDelete As Boolean, Optional blnReload As Boolean = True)
        Dim root As XmlElement = xmlTables.DocumentElement
        If root Is Nothing Then
            xmlTables = SeqData.dhdText.CreateRootDocument(xmlTables, "Sequenchel", "Tables", True)
        End If

        If strAlias.Length = 0 Then strAlias = strTableName

        Dim xTNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        If xTNode Is Nothing Then xTNode = SeqData.dhdText.CreateAppendElement(xmlTables.Item("Sequenchel").Item("Tables"), "Table")

        SeqData.dhdText.CreateAppendAttribute(xTNode, "Default", "False", True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Name", strTableName, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Alias", strAlias, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Visible", blnVisible, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Search", blnSearch, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Update", blnUpdate, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Insert", blnInsert, True)
        SeqData.dhdText.CreateAppendElement(xTNode, "Delete", blnDelete, True)

        SeqData.curStatus.Table = strTableName
        SeqData.curStatus.TableReload = True

        If blnReload = True Then TablesLoad()
    End Sub

#End Region

#Region "Fields"
#Region "Controls"

    Private Sub tvwTable_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvwTable.AfterSelect
        Dim strFieldName As String = ""
        Dim strFieldValue As String = ""
        Dim trnNode As TreeNode = Nothing

        If tvwTable.SelectedNode.Parent Is Nothing Then
            trnNode = tvwTable.SelectedNode.Nodes(7).FirstNode
        Else
            If tvwTable.SelectedNode.Parent.Parent Is Nothing Then
                trnNode = tvwTable.SelectedNode.Parent.Nodes(7).FirstNode
            Else
                If tvwTable.SelectedNode.Text = "Fields" Then
                    trnNode = tvwTable.SelectedNode.FirstNode
                ElseIf tvwTable.SelectedNode.Text = "Field" Then
                    trnNode = tvwTable.SelectedNode
                ElseIf tvwTable.SelectedNode.Parent.Text = "Field" Then
                    trnNode = tvwTable.SelectedNode.Parent
                ElseIf tvwTable.SelectedNode.Parent.Parent.Text = "Field" Then
                    trnNode = tvwTable.SelectedNode.Parent.Parent
                Else
                    Exit Sub
                End If

            End If
        End If

        NodeDisplay(trnNode)
    End Sub

    Private Sub chkFieldList_CheckedChanged(sender As Object, e As EventArgs) Handles chkFieldList.CheckedChanged
        If chkFieldList.Checked Then
            txtFieldListWidth.Enabled = True
            txtFieldListOrder.Enabled = True
        Else
            txtFieldListWidth.Enabled = False
            txtFieldListWidth.Text = ""
            txtFieldListOrder.Enabled = False
            txtFieldListOrder.Text = ""
        End If
    End Sub

    Private Sub btnFieldAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnFieldAddOrUpdate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim strTableName As String = "", strTableAlias As String = ""
        If cbxRelationTables.Text.IndexOf("(") > 0 Then
            strTableName = cbxRelationTables.Text.Substring(cbxRelationTables.Text.IndexOf("(") + 1, cbxRelationTables.Text.Length - (cbxRelationTables.Text.IndexOf("(") + 1) - 1)
            strTableAlias = cbxRelationTables.Text.Substring(0, cbxRelationTables.Text.IndexOf("(") - 1)
        Else
            strTableName = cbxRelationTables.Text
            strTableAlias = cbxRelationTables.Text
        End If

        FieldAddOrUpdate(SeqData.curStatus.Table, txtFieldName.Text, txtFieldAlias.Text, cbxDataType.SelectedItem, chkIdentity.Checked, chkPrimaryKey.Checked, txtFieldWidth.Text, strTableName, strTableAlias, cbxRelationFields.Text, txtRelatedField.Text, chkRelatedField.Checked, txtControlField.Text, txtControlValue.Text, chkControlUpdate.Checked, _
                         chkControlMode.Checked, chkDefaultButton.Checked, txtDefaultButton.Text, chkFieldList.Checked, txtFieldListOrder.Text, txtFieldListWidth.Text, chkFieldVisible.Checked, chkFieldSearch.Checked, chkFieldSearchList.Checked, chkFieldUpdate.Checked, True)
        CursorControl()
    End Sub

    Private Sub btnFieldClear_Click(sender As Object, e As EventArgs) Handles btnFieldClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        FieldsClear()
        CursorControl()
    End Sub

    Private Sub btnFieldDelete_Click(sender As Object, e As EventArgs) Handles btnFieldDelete.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim strSelection As String = txtFieldName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Field", "FldName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            SeqData.curStatus.TableChanged = True
            ConfigurationSave()
            FieldsClear()
            lvwTables_SelectedIndexChanged(Nothing, Nothing)
            WriteStatus("Field Deleted", 0, lblStatusText)
        Else
            WriteStatus("Unable to Delete Field", 2, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub chkFieldVisible_CheckedChanged(sender As Object, e As EventArgs) Handles chkFieldVisible.CheckedChanged
        If chkFieldVisible.Checked = True Then
            lblFieldWidth.Text = lblFieldWidth.Tag & "*"
        Else
            lblFieldWidth.Text = lblFieldWidth.Tag
        End If
    End Sub

#End Region

    Private Sub NodeDisplay(PNode As TreeNode)
        FieldsClear()
        Dim strFieldName As String = ""
        Dim strFieldValue As String = ""
        Dim strTableName As String = "", strTableAlias As String = "", strRelatedField As String = ""

        For Each xNode As TreeNode In PNode.Nodes
            strFieldName = xNode.Text

            If xNode.Nodes.Count > 0 Then
                'For iNode = 0 To xNode.nodes.count - 1
                '    MessageBox.Show(strFieldName & Environment.NewLine & xNode.Nodes(iNode).Text)
                'Next
                If strFieldName = "FldList" And xNode.Nodes(0).Text = "(ATTRIBUTES)" Then
                    'MessageBox.Show(xNode.Nodes(0).Nodes.Count)
                    txtFieldListOrder.Text = Replace(Replace(xNode.Nodes(0).Nodes(0).Text, "Order = '", ""), "'", "")
                    txtFieldListWidth.Text = Replace(Replace(xNode.Nodes(0).Nodes(1).Text, "Width = '", ""), "'", "")
                    strFieldValue = xNode.Nodes(1).Text
                ElseIf strFieldName = "DefaultButton" And xNode.Nodes(0).Text = "(ATTRIBUTES)" Then
                    'MessageBox.Show(xNode.Nodes(0).Nodes.Count)
                    txtDefaultButton.Text = Replace(Replace(xNode.Nodes(0).Nodes(0).Text, "DefaultValue = '", ""), "'", "")
                    strFieldValue = xNode.Nodes(1).Text
                Else
                    strFieldValue = xNode.Nodes(0).Text
                End If
            Else
                strFieldValue = ""
            End If
            'Do something...
            'MessageBox.Show(strFieldName & Environment.NewLine & strFieldValue)
            Select Case strFieldName
                Case "FldName"
                    If strFieldValue.Length > 0 Then
                        txtFieldName.Text = strFieldValue
                        txtFieldName.Tag = strFieldValue
                    End If
                Case "FldAlias"
                    If strFieldValue.Length > 0 Then txtFieldAlias.Text = strFieldValue
                Case "DataType"
                    If strFieldValue.Length > 0 Then cbxDataType.SelectedItem = strFieldValue
                Case "Identity"
                    If strFieldValue.Length > 0 Then chkIdentity.Checked = strFieldValue
                Case "PrimaryKey"
                    If strFieldValue.Length > 0 Then chkPrimaryKey.Checked = strFieldValue
                Case "FldWidth"
                    If strFieldValue.Length > 0 Then txtFieldWidth.Text = strFieldValue
                Case "Relations"
                    For Each xRnode As TreeNode In xNode.Nodes
                        If xRnode.Nodes.Count > 0 Then
                            If xRnode.Nodes(0).Text = "(ATTRIBUTES)" Then
                                'MessageBox.Show(xNode.Nodes(0).Nodes.Count)
                                strFieldValue = xRnode.Nodes(1).Text
                            ElseIf xRnode.Nodes(0).Text = "RelationTable" Then
                                'dont know yet
                                strTableName = xRnode.Nodes(0).Nodes(0).Text
                                strTableAlias = xRnode.Nodes(1).Nodes(0).Text
                                strRelatedField = xRnode.Nodes(2).Nodes(0).Text
                                strFieldValue = strTableAlias & " (" & strTableName & ")"
                            Else
                                strFieldValue = xRnode.Nodes(0).Text
                                If strFieldValue.IndexOf(".") > 0 Then
                                    strFieldValue = strFieldValue.Substring(0, strFieldValue.LastIndexOf(".")) & " (" & strFieldValue.Substring(0, strFieldValue.LastIndexOf(".")) & ")"
                                End If
                            End If
                        End If

                        If xRnode.Nodes.Count > 0 Then
                            cbxRelationTables.Items.Add(strFieldValue)
                        End If
                    Next
                    If cbxRelationTables.Items.Count > 0 Then cbxRelationTables.SelectedIndex = 0
                    ''If strFieldValue.Length > 0 Then txtRelations.Text = strFieldValue
                    'If strFieldValue.Length > 0 Then cbxRelations.Items.Add(strFieldValue)
                    'If cbxRelations.Items.Count > 0 Then cbxRelations.SelectedIndex = 0
                Case "ControlField"
                    If strFieldValue.Length > 0 Then txtControlField.Text = strFieldValue
                Case "ControlValue"
                    If strFieldValue.Length > 0 Then txtControlValue.Text = strFieldValue
                Case "ControlUpdate"
                    If strFieldValue.Length > 0 Then chkControlUpdate.Checked = strFieldValue
                Case "ControlMode"
                    If strFieldValue.Length > 0 Then chkControlMode.Checked = strFieldValue
                Case "FldList"
                    If strFieldValue.Length > 0 Then chkFieldList.Checked = strFieldValue
                Case "DefaultButton"
                    If strFieldValue.Length > 0 Then chkDefaultButton.Checked = strFieldValue
                    'Case "Width"
                    '    If strFieldValue.Length > 0 Then txtFieldListWidth.Text = strFieldValue
                    'Case "Order"
                    '    If strFieldValue.Length > 0 Then txtFieldListOrder.Text = strFieldValue
                Case "FldVisible"
                    If strFieldValue.Length > 0 Then chkFieldVisible.Checked = strFieldValue
                Case "FldSearch"
                    If strFieldValue.Length > 0 Then chkFieldSearch.Checked = strFieldValue
                Case "FldSearchList"
                    If strFieldValue.Length > 0 Then chkFieldSearchList.Checked = strFieldValue
                Case "FldUpdate"
                    If strFieldValue.Length > 0 Then chkFieldUpdate.Checked = strFieldValue
                Case Else
                    MessageBox.Show("Unknown Field detected: " & strFieldName)
            End Select
        Next
    End Sub

    Private Sub FieldsClear()
        txtFieldName.Tag = ""
        txtFieldName.Text = ""
        txtFieldAlias.Text = ""
        cbxDataType.SelectedIndex = -1
        chkIdentity.Checked = False
        chkPrimaryKey.Checked = False
        txtFieldWidth.Text = ""
        cbxRelationTables.Items.Clear()
        cbxRelationTables.Text = ""
        cbxRelationFields.Items.Clear()
        cbxRelationFields.Text = ""
        txtRelatedField.Text = ""
        chkRelatedField.Checked = False
        chkDefaultButton.Checked = False
        txtDefaultButton.Text = ""
        txtControlField.Text = ""
        txtControlValue.Text = ""
        chkControlUpdate.Checked = False
        chkControlMode.Checked = False
        chkFieldList.Checked = False
        txtFieldListOrder.Text = ""
        txtFieldListWidth.Text = ""
        chkPrimaryKey.Checked = False
        chkFieldVisible.Checked = False
        chkFieldSearch.Checked = False
        chkFieldSearchList.Checked = False
        chkFieldUpdate.Checked = False
    End Sub

    Private Sub FieldAddOrUpdate(TableName As String, FldName As String, FldAlias As String, DataType As String, Identity As Boolean, PrimaryKey As Boolean, FldWidth As String, RelationTable As String, RelationTableAlias As String, RelationField As String, RelatedField As String, RelatedFieldList As String, ControlField As String, _
                         ControlValue As String, ControlUpdate As Boolean, ControlMode As Boolean, DefaultButton As Boolean, DefaultValue As String, FldList As Boolean, Order As String, Width As String, _
                         FldVisible As Boolean, FldSearch As Boolean, FldSearchList As Boolean, FldUpdate As Boolean, Optional Reload As Boolean = False)

        Dim xTNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", TableName)
        If xTNode Is Nothing Then
            WriteStatus("The table to which this field belongs was not found", 2, lblStatusText)
            Exit Sub
        End If

        Dim xPNode As XmlNode = SeqData.dhdText.CreateAppendElement(xTNode, "Fields", Nothing, True)
        Dim xFNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xPNode, "Field", "FldName", FldName)
        If xFNode Is Nothing Then
            xFNode = SeqData.dhdText.CreateAppendElement(xPNode, "Field", Nothing, False)
            'FieldAdd(TableName, FldName, FldAlias, DataType, FldWidth, Relations, RelatedField, ControlField, ControlValue, ControlUpdate, ControlMode, DefaultButton, DefaultValue, FldList, Order, Width, FldVisible, FldSearch, FldSearchList, FldUpdate, Reload)
            'Else
            '    FieldUpdate(TableName, FldName, FldAlias, DataType, FldWidth, Relations, RelatedField, ControlField, ControlValue, ControlUpdate, ControlMode, DefaultButton, DefaultValue, FldList, Order, Width, FldVisible, FldSearch, FldSearchList, FldUpdate, Reload)
        End If

        SeqData.dhdText.CreateAppendElement(xFNode, "FldName", FldName, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "FldAlias", FldAlias, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "DataType", DataType, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "Identity", Identity, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "PrimaryKey", PrimaryKey, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "FldWidth", FldWidth, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "ControlField", ControlField, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "ControlValue", ControlValue, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "ControlUpdate", ControlUpdate, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "ControlMode", ControlMode, True)

        Dim xDNode As XmlNode = SeqData.dhdText.CreateAppendElement(xFNode, "DefaultButton", DefaultButton, True)
        SeqData.dhdText.CreateAppendAttribute(xDNode, "DefaultValue", DefaultValue, True)

        Dim xLNode As XmlNode = SeqData.dhdText.CreateAppendElement(xFNode, "FldList", FldList, True)
        SeqData.dhdText.CreateAppendAttribute(xLNode, "Order", Order, True)
        SeqData.dhdText.CreateAppendAttribute(xLNode, "Width", Width, True)

        SeqData.dhdText.CreateAppendElement(xFNode, "FldVisible", FldVisible, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "FldSearch", FldSearch, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "FldSearchList", FldSearchList, True)
        SeqData.dhdText.CreateAppendElement(xFNode, "FldUpdate", FldUpdate, True)

        RelationAdd(TableName, FldName, RelationTable, RelationTableAlias, RelationField, RelatedField, RelatedFieldList)

        If Reload = True Then
            tvwTable.Nodes.Clear()
            DisplayXmlNode(xTNode, tvwTable.Nodes)
            tvwTable.Nodes(0).Expand()
            tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
            tvwTable.SelectedNode.Expand()
        End If
        SeqData.curStatus.TableChanged = True
        ConfigurationSave()
        WriteStatus("Field Saved", 0, lblStatusText)

    End Sub

#End Region

#Region "Table Templates"
#Region "Controls"

    Private Sub btnloadTemplates_Click(sender As Object, e As EventArgs) Handles btnloadTemplates.Click
        WriteStatus("", 0, lblStatusText)
        TemplatesGet()
    End Sub

    Private Sub btnSearchTemplate_Click(sender As Object, e As EventArgs) Handles btnSearchTemplate.Click
        WriteStatus("", 0, lblStatusText)
        TemplatesGet(txtSearchTemplate.Text)
    End Sub

    Private Sub lstAvailableTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAvailableTemplates.SelectedIndexChanged
        CursorControl("Wait")
        Dim MydbRef As New SDBA.DBRef
        xmlDoc.LoadXml(MydbRef.GetScript(lstAvailableTemplates.SelectedItem))
        TemplateShow()
        CursorControl()
    End Sub

    Private Sub btnUseTemplate_Click(sender As Object, e As EventArgs) Handles btnUseTemplate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        xmlTables = xmlDoc
        SeqData.curStatus.TableChanged = True
        ConfigurationSave()
        tabConfiguration.SelectedTab = tpgTables
        TablesLoad()
        CursorControl()
    End Sub

    Private Sub btnLoadTemplate_Click(sender As Object, e As EventArgs) Handles btnLoadTemplate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim loadFile1 As New OpenFileDialog
        loadFile1.Title = "Table Template File"
        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Sequenchel Config Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            xmlDoc.Load(loadFile1.FileName)
            TemplateShow()
        Else
            tvwSelectedTemplate.Nodes.Clear()
        End If
        CursorControl()
    End Sub

#End Region

    Private Sub TemplatesGet(Optional strSearch As String = "")
        Try
            lstAvailableTemplates.Items.Clear()
            Dim MydbRef As New SDBA.DBRef
            Dim arrScripts(100) As String
            arrScripts = MydbRef.GetTemplateList()
            For intCount As Integer = 0 To arrScripts.GetUpperBound(0)
                If strSearch = "" Or arrScripts(intCount).ToString.ToLower.Contains(strSearch.ToLower) = True Then
                    lstAvailableTemplates.Items.Add(arrScripts(intCount))
                End If
            Next
        Catch ex As Exception
            SeqData.WriteLog(ex.Message, 1)
        End Try

    End Sub

    Private Sub TemplateShow()
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlDoc, "Tables")
        tvwSelectedTemplate.Nodes.Clear()
        If xNode Is Nothing Then Exit Sub
        DisplayXmlNode(xNode, tvwSelectedTemplate.Nodes)
        tvwSelectedTemplate.Nodes(0).Expand()
    End Sub

#End Region

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        'nothing here
    End Sub

#Region "Backup"

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If txtBackupLocation.Text.Length = 0 Then
            WriteStatus("A backup location is required, aborting action", 2, lblStatusText)
            Exit Sub
        End If
        Try
            BackupDatabase(txtBackupLocation.Text)
            SaveConfigSetting("Database", "BackupLocation", txtBackupLocation.Text, "A valid location on the server")
            WriteStatus("Database backup is created.", 0, lblStatusText)
        Catch ex As Exception
            SeqData.WriteLog("While saving the database, the following error occured: " & Environment.NewLine & ex.Message, 1)
            WriteStatus("An error occured saving the database. Please check the log", 1, lblStatusText)
        End Try
        CursorControl()
    End Sub

    Friend Sub BackupDatabase(ByVal strPath As String)

        Dim dtmNow As Date = Now()
        Dim strFormat As String = "yyyyMMdd_HHmm"
        Dim strDateTime As String = dtmNow.ToString(strFormat)

        strQuery = "BACKUP DATABASE [" & SeqData.dhdConnection.DatabaseName & "] TO  DISK = N'" & strPath & "\" & SeqData.dhdConnection.DatabaseName & "_" & strDateTime & ".bak' WITH NOFORMAT, NOINIT,  NAME = N'" & SeqData.dhdConnection.DatabaseName & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"

        If SeqData.curVar.DebugMode Then MessageBox.Show(strQuery)
        Try
            SeqData.QueryDb(SeqData.dhdConnection, strQuery, False)
        Catch ex As Exception
            SeqData.WriteLog("Backup error database: " & ex.Message, 1)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBackupLocation_Click(sender As Object, e As EventArgs) Handles btnBackupLocation.Click
        WriteStatus("", 0, lblStatusText)
        Dim fbdBackup As New FolderBrowserDialog
        fbdBackup.ShowDialog()
        txtBackupLocation.Text = fbdBackup.SelectedPath
    End Sub

#End Region

#Region "Relations"

    Private Sub btnShowRelationTables_Click(sender As Object, e As EventArgs) Handles btnShowRelationTables.Click
        CursorControl("Wait")
        Dim lstFindTables As List(Of String) = SeqData.LoadTablesListXml(xmlTables, True)

        If lstFindTables Is Nothing Then
            CursorControl()
            WriteStatus("No tables found", 2, lblStatusText)
            Exit Sub
        End If

        lstRelationTables.Items.Clear()
        lstRelationTables.Items.Add("")

        For Each lstItem As String In lstFindTables
            lstRelationTables.Items.Add(lstItem)
        Next

        CursorControl()
        If lstRelationTables.Items.Count < 15 Then
            lstRelationTables.Height = lstRelationTables.Items.Count * 15
        Else
            lstRelationTables.Height = 15 * 15
        End If
        lstRelationTables.Visible = True
        lstRelationTables.Focus()
    End Sub

    Private Sub lstRelationTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRelationTables.SelectedIndexChanged
        Dim strTable As String = lstRelationTables.SelectedItem
        If cbxRelationTables.Items.Contains(strTable) Then
            cbxRelationTables.SelectedItem = strTable
        Else
            cbxRelationTables.Text = strTable
        End If
        lstRelationTables.Visible = False
    End Sub

    Private Sub cbxRelationTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxRelationTables.SelectedIndexChanged
        If SeqData.curStatus.SuspendActions = False Then
            If cbxRelationTables.Text.Length = 0 Then Exit Sub

            Dim strTableName As String = "", strTableAlias As String = ""
            If cbxRelationTables.Text.IndexOf("(") > 0 Then
                strTableName = cbxRelationTables.Text.Substring(cbxRelationTables.Text.IndexOf("(") + 1, cbxRelationTables.Text.Length - (cbxRelationTables.Text.IndexOf("(") + 1) - 1)
                strTableAlias = cbxRelationTables.Text.Substring(0, cbxRelationTables.Text.IndexOf("(") - 2)
            Else
                strTableName = cbxRelationTables.Text
                strTableAlias = cbxRelationTables.Text
            End If
            cbxRelationFields.Text = ""
            chkRelatedField.Checked = False
            txtRelatedField.Text = ""

            Dim strFieldName As String = txtFieldName.Text
            Dim xMNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", txtTableName.Text)
            If xMNode Is Nothing Then Exit Sub
            Dim xPNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xMNode, "Fields/Field", "FldName", txtFieldName.Text)
            If xPNode Is Nothing Then Exit Sub

            Dim xlmRNodes As XmlNodeList = SeqData.dhdText.FindXmlChildNodes(xPNode, "Relations/Relation", Nothing, Nothing)
            If xlmRNodes Is Nothing Then Exit Sub

            For Each xRNode As XmlNode In xlmRNodes
                If SeqData.dhdText.CheckNodeElement(xRNode, "RelationField") = True Then
                    If xRNode.Item("RelationTable").InnerText = strTableName Then
                        cbxRelationFields.Text = xRNode.Item("RelationField").InnerText
                        chkRelatedField.Checked = xRNode.Item("RelatedFieldList").InnerText
                        txtRelatedField.Text = xRNode.Item("RelatedField").InnerText
                    End If
                Else
                    Dim strRelation As String = xRNode.InnerText
                    Dim strRelationField As String = ""
                    If strRelation.Length > 0 Then
                        If strRelation.IndexOf(".") > 0 Then
                            strRelationField = strRelation.Substring(strRelation.LastIndexOf(".") + 1, strRelation.Length - (strRelation.LastIndexOf(".") + 1))
                            strRelation = strRelation.Substring(0, strRelation.LastIndexOf(".")) & " (" & strRelation.Substring(0, strRelation.LastIndexOf(".")) & ")"
                        End If

                        If strRelation = cbxRelationTables.SelectedItem Then
                            cbxRelationFields.Text = strRelationField
                            If xRNode.Attributes.Count > 0 Then
                                If xRNode.Attributes(0).Name = "RelatedField" Then txtRelatedField.Text = xRNode.Attributes("RelatedField").InnerText
                                If xRNode.Attributes.Count > 1 Then
                                    If xRNode.Attributes(1).Name = "RelatedFieldList" Then chkRelatedField.Checked = xRNode.Attributes("RelatedFieldList").InnerText
                                End If
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next
        End If


        'current selected table, selected field, defined relations
        'find existing relation and display or empty fields

    End Sub

    Private Sub lstRelationTables_LostFocus(sender As Object, e As EventArgs) Handles lstRelationTables.LostFocus
        lstRelationTables.Visible = False
    End Sub

    Private Sub btnShowRelationFields_Click(sender As Object, e As EventArgs) Handles btnShowRelationFields.Click
        CursorControl("Wait")
        If cbxRelationTables.Text.Length = 0 Then
            CursorControl()
            WriteStatus("No table selected", 2, lblStatusText)
            Exit Sub
        End If

        Dim strTable As String = cbxRelationTables.Text
        If strTable.IndexOf("(") > 0 Then
            strTable = strTable.Substring(strTable.IndexOf("(") + 1, strTable.Length - strTable.IndexOf("(") - 2)
        End If
        Dim lstFindFields As New List(Of String)
        lstFindFields = SeqData.dhdText.LoadItemList(xmlTables, "Table", "Name", strTable, "Field", "FldName")

        If lstFindFields Is Nothing Then
            CursorControl()
            WriteStatus("No fields found", 2, lblStatusText)
            Exit Sub
        End If

        lstRelationFields.Items.Clear()
        lstRelationFields.Items.Add("")

        For Each lstItem As String In lstFindFields
            lstRelationFields.Items.Add(lstItem)
        Next

        CursorControl()
        If lstRelationFields.Items.Count < 15 Then
            lstRelationFields.Height = lstRelationFields.Items.Count * 15
        Else
            lstRelationFields.Height = 15 * 15
        End If
        lstRelationFields.Visible = True
        lstRelationFields.Focus()

    End Sub

    Private Sub lstRelationFields_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRelationFields.SelectedIndexChanged
        cbxRelationFields.Text = lstRelationFields.SelectedItem
        lstRelationFields.Visible = False
    End Sub

    Private Sub lstRelationFields_LostFocus(sender As Object, e As EventArgs) Handles lstRelationFields.LostFocus
        lstRelationFields.Visible = False
    End Sub

    Private Sub btnRelationRemove_Click(sender As Object, e As EventArgs) Handles btnRelationRemove.Click
        CursorControl("Wait")
        If cbxRelationTables.Text.Length < 1 Or cbxRelationFields.Text.Length < 1 Then Exit Sub
        Dim strFieldName As String = txtFieldName.Tag
        Dim strRelatedTable As String = cbxRelationTables.Text
        Dim strRelatedField As String = cbxRelationFields.Text

        If strRelatedTable.Contains("(") Then strRelatedTable = strRelatedTable.Substring(strRelatedTable.IndexOf("(") + 1, strRelatedTable.Length - (strRelatedTable.IndexOf("(") + 1) - 1)

        If RelationRemove(SeqData.curStatus.Table, txtFieldName.Tag, strRelatedTable, strRelatedField, True) = False Then
            Exit Sub
        End If

        Dim xPNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", SeqData.curStatus.Table)

        tvwTable.Nodes.Clear()
        DisplayXmlNode(xPNode, tvwTable.Nodes)
        tvwTable.Nodes(0).Expand()
        tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
        tvwTable.SelectedNode.Expand()

        SeqData.curStatus.TableChanged = True
        ConfigurationSave()
        CursorControl()
    End Sub

    Private Function RelationRemove(strTableSource As String, strFieldSource As String, strRelatedTable As String, strRelatedField As String, blnRemoveOnly As Boolean) As Boolean
        Dim xPNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableSource)
        If xPNode Is Nothing Then
            WriteStatus("The Table " & strTableSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xPNode, "Fields/Field", "FldName", strFieldSource)
        If xNode Is Nothing Then
            WriteStatus("The Field " & strFieldSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xCNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xNode, "Relations")
        'If relations node does not exist, nothing needs to be deleted.
        If xCNode Is Nothing Then Return True

        Dim xRNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xCNode, "Relation", "RelationTable", strRelatedTable)
        'Find relation old style
        If xRNode Is Nothing Then xRNode = SeqData.dhdText.FindXmlChildNode(xCNode, "Relation", "Relation", strRelatedTable & "." & strRelatedField)
        If xRNode Is Nothing Then
            WriteStatus("The Relation " & strRelatedTable & "." & strRelatedField & " was not found. Nothing is deleted", 2, lblStatusText)
            Return True
        End If

        If blnRemoveOnly = True Then
            If MessageBox.Show("This will permanently remove the relation: " & strRelatedTable & "." & strRelatedField & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Return False
        End If

        'remove existing node
        Try
            'enhance relation detection (table name/table alias/field name)
            If xCNode.ChildNodes.Count > 0 Then
                Dim strRelTable As String = "", strRelField As String = "", strRelation As String = ""

                For Each xXNode As XmlNode In xCNode.ChildNodes
                    If SeqData.dhdText.CheckNodeElement(xXNode, "RelationTable") Then strRelTable = xXNode.Item("RelationTable").InnerText
                    If SeqData.dhdText.CheckNodeElement(xXNode, "RelationField") Then strRelField = xXNode.Item("RelationField").InnerText
                    If xXNode.ChildNodes.Count = 1 Then
                        strRelation = xXNode.InnerText
                        If strRelation.Length > 0 Then
                            strRelTable = strRelation.Substring(0, strRelation.LastIndexOf("."))
                            strRelField = strRelation.Substring(strRelation.LastIndexOf(".") + 1, strRelation.Length - (strRelation.LastIndexOf(".") + 1))
                        End If
                    End If
                    If strRelTable = strRelatedTable And strRelField = strRelatedField Then
                        xXNode.ParentNode.RemoveChild(xXNode)
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            SeqData.WriteLog("Error removing (old) relation: " & ex.Message, 1)
            WriteStatus("Error removing (old) relation: " & ex.Message, 1, lblStatusText)
            Return False
        End Try

    End Function

    Private Function RelationAdd(strTableSource As String, strFieldSource As String, strTableRelation As String, strTableAliasRelation As String, strFieldRelation As String, strRelatedField As String, blnRelatedFieldList As Boolean) As Boolean
        If strFieldRelation.Length = 0 Then Return False

        If RelationRemove(strTableSource, strFieldSource, strTableRelation, strFieldRelation, False) = False Then
            Return False
        End If

        Dim xPNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableSource)
        If xPNode Is Nothing Then
            WriteStatus("The Table " & strTableSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xPNode, "Fields/Field", "FldName", strFieldSource)
        If xNode Is Nothing Then
            WriteStatus("The Field " & strFieldSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xCNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xNode, "Relations")
        If xCNode Is Nothing Then xCNode = SeqData.dhdText.CreateAppendElement(xNode, "Relations", Nothing, False)

        Try
            Dim xRNode As XmlNode = SeqData.dhdText.CreateAppendElement(xCNode, "Relation", Nothing, False)
            SeqData.dhdText.CreateAppendElement(xRNode, "RelationTable", strTableRelation, True)
            SeqData.dhdText.CreateAppendElement(xRNode, "RelationTableAlias", strTableAliasRelation, True)
            SeqData.dhdText.CreateAppendElement(xRNode, "RelationField", strFieldRelation, True)
            SeqData.dhdText.CreateAppendElement(xRNode, "RelatedField", strRelatedField, True)
            SeqData.dhdText.CreateAppendElement(xRNode, "RelatedFieldList", blnRelatedFieldList, True)

            WriteStatus("Add/Update Relation completed succesfully", 0, lblStatusText)
            SeqData.curStatus.TableChanged = True
            ConfigurationSave()
        Catch ex As Exception
            SeqData.WriteLog("Error saving relation: " & ex.Message, 1)
            WriteStatus("Error saving relation: " & ex.Message, 1, lblStatusText)
            Return False
        End Try

        tvwTable.Nodes.Clear()
        DisplayXmlNode(xPNode, tvwTable.Nodes)
        tvwTable.Nodes(0).Expand()
        tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
        tvwTable.SelectedNode.Expand()
        Return True

    End Function

    Private Sub btnRelationAdd_Click(sender As Object, e As EventArgs) Handles btnRelationAdd.Click
        CursorControl("Wait")
        If cbxRelationFields.Text.Length = 0 Then Exit Sub
        Dim strTableName As String = "", strTableAlias As String = ""
        If cbxRelationTables.Text.IndexOf("(") > 0 Then
            strTableName = cbxRelationTables.Text.Substring(cbxRelationTables.Text.IndexOf("(") + 1, cbxRelationTables.Text.Length - (cbxRelationTables.Text.IndexOf("(") + 1) - 1)
            strTableAlias = cbxRelationTables.Text.Substring(0, cbxRelationTables.Text.IndexOf("(") - 2)
        Else
            strTableName = cbxRelationTables.Text
            strTableAlias = cbxRelationTables.Text
        End If
        RelationAdd(SeqData.curStatus.Table, txtFieldName.Tag, strTableName, strTableAlias, cbxRelationFields.Text, txtRelatedField.Text, chkRelatedField.Checked)

        'If Not cbxRelations.Items.Contains(cbxRelations.Text) Then cbxRelations.Items.Add(cbxRelations.Text)
        CursorControl()
    End Sub

#End Region

End Class