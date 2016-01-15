Imports System.IO
Imports System.Xml

Public Class frmConfiguration

    Private Sub frmConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        If DebugMode Then btnTest.Visible = True
        cbxDataProvider.SelectedItem = "SQL"
        cbxLoginMethod.SelectedItem = "Windows"
        SecuritySet()
        DataTypesLoad()
        ConnectionsLoad()
        TableSetsLoad()
        TablesLoad()
        txtDefaultPath.Text = CurVar.DefaultConfigFilePath
        txtTableSetName.Text = CurVar.TableSetName

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
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
        If CurVar.AllowUpdate = False Then chkTableUpdate.Enabled = False
        If CurVar.AllowInsert = False Then chkTableInsert.Enabled = False
        If CurVar.AllowDelete = False Then chkTableDelete.Enabled = False
    End Sub

    Private Sub ConfigurationSave()
        If CurStatus.ConnectionChanged = True Then
            CurStatus.ConnectionReload = True
            dhdText.SaveXmlFile(xmlConnections, CurVar.ConnectionsFile)
            CurStatus.ConnectionChanged = False
        End If
        If CurStatus.TableSetChanged = True Then
            CurStatus.TableSetReload = True
            dhdText.SaveXmlFile(xmlTableSets, CurVar.TableSetsFile)
            CurStatus.TableSetChanged = False
        End If
        If CurStatus.TableChanged = True Then
            CurStatus.TableReload = True
            dhdText.SaveXmlFile(xmlTables, CurVar.TablesFile)
            CurStatus.TableChanged = False
        End If
    End Sub

    Private Sub DataTypesLoad()
        cbxDataType.Items.Clear()
        Dim lstDataTypes As List(Of String) = GetDataTypes()
        For Each DataType In lstDataTypes
            cbxDataType.Items.Add(DataType)
        Next
    End Sub



#Region "Connections"
    Private Sub ConnectionsLoad()
        lvwConnections.Items.Clear()
        Dim lstXml As XmlNodeList
        lstXml = dhdText.FindXmlNodes(xmlConnections, "//Connection")
        If lstXml Is Nothing Then Exit Sub
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Tag = xNode.Item("ConnectionName").InnerText
            lsvItem.Text = xNode.Item("ConnectionName").InnerText
            lsvItem.SubItems.Add(xNode.Item("DataBaseName").InnerText)
            lsvItem.SubItems.Add(xNode.Item("DataLocation").InnerText)
            lvwConnections.Items.Add(lsvItem)

            If xNode.Item("ConnectionName").InnerText = CurStatus.Connection Then
                dhdConnection.DatabaseName = xNode.Item("DataBaseName").InnerText
                dhdConnection.DataLocation = xNode.Item("DataLocation").InnerText
                dhdConnection.DataProvider = xNode.Item("DataProvider").InnerText
                dhdConnection.LoginMethod = xNode.Item("LoginMethod").InnerText
                dhdConnection.LoginName = xNode.Item("LoginName").InnerText
                dhdConnection.Password = xNode.Item("Password").InnerText
            End If
        Next
        For Each lstItem In lvwConnections.Items
            If lstItem.Tag = CurStatus.Connection Then
                lstItem.Selected = True
            End If
        Next
        'lvwConnections.SelectedItems(0)
    End Sub

    Private Sub lvwConnections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwConnections.SelectedIndexChanged
        CursorControl("Wait")
        ConnectionLoad()
        CursorControl()
    End Sub

    Private Sub ConnectionLoad()
        If lvwConnections.SelectedItems.Count = 1 Then
            'If CurStatus.Connection <> lvwConnections.SelectedItems.Item(0).Tag Then
            CurStatus.ConnectionReload = True
            CurStatus.Connection = lvwConnections.SelectedItems.Item(0).Tag
            LoadConnection(CurStatus.Connection)
            TableSetClear()
            LoadTableSetsXml()
            LoadTableSet(CurStatus.TableSet)
            TableSetsLoad()
            LoadTablesXml()
            TablesLoad()
            'End If

            Dim xNode As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", CurStatus.Connection)
            tvwConnection.Nodes.Clear()
            DisplayXmlNode(xNode, tvwConnection.Nodes)
            tvwConnection.ExpandAll()
            ConnectionEdit()
        End If

    End Sub

    Private Sub txtConnectionName_TextChanged(sender As Object, e As EventArgs) Handles txtConnectionName.TextChanged
        txtTableSetsFile.Text = CurVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
    End Sub

    Private Sub btnDefaultTableSetFile_Click(sender As Object, e As EventArgs) Handles btnDefaultTableSetFile.Click
        txtTableSetsFile.Text = CurVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
    End Sub

    Private Sub btnTableSetFileBrowse_Click(sender As Object, e As EventArgs) Handles btnTableSetFileBrowse.Click
        Dim loadFile1 As New OpenFileDialog
        loadFile1.InitialDirectory = CurVar.DefaultConfigFilePath
        loadFile1.Title = "Tablesets File"
        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Sequenchel Tablesets Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            txtTableSetsFile.Text = loadFile1.FileName
        End If
    End Sub

    'Private Sub AddNode(ByVal inXmlNode As XmlNode, ByVal inTreeNode As TreeNode)
    '    Dim xNode As XmlNode
    '    Dim tNode As TreeNode
    '    Dim nodeList As XmlNodeList
    '    Dim i As Integer
    '    If inXmlNode.HasChildNodes Then
    '        nodeList = inXmlNode.ChildNodes
    '        For i = 0 To nodeList.Count - 1
    '            xNode = inXmlNode.ChildNodes(i)
    '            inTreeNode.Nodes.Add(New TreeNode(xNode.Name))
    '            tNode = inTreeNode.Nodes(i)
    '            AddNode(xNode, tNode)
    '        Next
    '    Else
    '        inTreeNode.Text = inXmlNode.InnerText.ToString
    '    End If
    'End Sub

    Private Sub btnCrawlServers_Click(sender As Object, e As EventArgs) Handles btnCrawlServers.Click
        CursorControl("Wait")
        GetSqlInstances()
        CursorControl()
    End Sub

    Friend Sub GetSqlInstances()
        Dim strResult As String = ""

        Using dtSqlSources As DataTable = System.Data.Sql.SqlDataSourceEnumerator.Instance.GetDataSources()
            If dtSqlSources.Rows.Count > 0 Then
                lstServers.Items.Clear()
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

    Private Sub cbxLoginMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxLoginMethod.SelectedIndexChanged
        If cbxLoginMethod.SelectedItem = "SQL" Then
            txtLoginName.Enabled = True
            txtPassword.Enabled = True
            txtLoginName.Text = dhdDatabase.LoginName
            txtPassword.Text = dhdDatabase.Password
        ElseIf cbxLoginMethod.SelectedItem = "Windows" Then
            txtLoginName.Enabled = False
            txtPassword.Enabled = False
            txtLoginName.Text = ""
            txtPassword.Text = ""
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
        'Application.DoEvents()

        dhdConnection.DatabaseName = "master"
        dhdConnection.DataLocation = txtDataLocation.Text
        dhdConnection.DataProvider = cbxDataProvider.SelectedItem
        dhdConnection.LoginMethod = cbxLoginMethod.SelectedItem
        dhdConnection.LoginName = txtLoginName.Text
        dhdConnection.Password = txtPassword.Text

        Try
            Dim intSqlVersion As Integer = GetSqlVersion(dhdConnection)
            Select Case intSqlVersion
                Case 0
                    MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
                    WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
                    Exit Sub
                Case 7, 8
                    MessageBox.Show("SQL Server 2000 or older is not supported")
                    WriteLog("SQL Server 2000 or older is not supported", 1)
                    Exit Sub
                Case 9, 10, 11, 12, 13
                    WriteLog("SQL Version " & intSqlVersion & " detected.", 3)
                    'Just do it
                Case Else
                    MessageBox.Show("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion)
                    WriteLog("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion, 1)
                    Exit Sub
            End Select
        Catch ex As Exception
            MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
            WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
            Exit Sub
        End Try

        strQuery = "SELECT name FROM sys.databases WHERE database_id <> 2"
        Dim dtsData As DataSet = QueryDb(dhdConnection, strQuery, True)


        If dtsData Is Nothing Then
            CursorControl()
            lblStatus.Text = "No databases found"
            Exit Sub
        End If
        If dtsData.Tables.Count = 0 Then Exit Sub
        If dtsData.Tables(0).Rows.Count = 0 Then Exit Sub

        lstDatabases.Items.Clear()
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
        DisplayXmlFile(xmlConnections, tvwConnection)
        CursorControl()
    End Sub

    Private Sub ConnectionEdit()
        If lvwConnections.SelectedItems.Count = 1 Then
            Dim strConnection As String = lvwConnections.SelectedItems.Item(0).Tag

            Dim xNode As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strConnection)
            txtConnectionName.Text = xNode.Item("ConnectionName").InnerText
            txtConnectionName.Tag = xNode.Item("ConnectionName").InnerText
            txtDataBaseName.Text = xNode.Item("DataBaseName").InnerText
            txtDataLocation.Text = xNode.Item("DataLocation").InnerText
            txtLoginName.Text = xNode.Item("LoginName").InnerText
            txtPassword.Text = DataHandler.txt.DecryptText(xNode.Item("Password").InnerText)
            cbxDataProvider.Text = xNode.Item("DataProvider").InnerText
            cbxLoginMethod.Text = xNode.Item("LoginMethod").InnerText
            txtTimeOut.Text = xNode.Item("Timeout").InnerText
            txtTableSetsFile.Text = xNode.Item("TableSets").InnerText
        End If
    End Sub

    Private Sub btnConnectionAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnConnectionAddOrUpdate.Click
        CursorControl("Wait")
        ConnectionAddOrUpdate()
        CursorControl()
    End Sub

    Private Sub ConnectionAddOrUpdate()
        'dhdText.RemoveNode(xmlConnections, "Connections", "ConnectionName", txtConnectionName.Text)
        Dim root As XmlElement = xmlConnections.DocumentElement
        If root Is Nothing Then
            xmlConnections = dhdText.CreateRootDocument(xmlConnections, "Sequenchel", "Connections", True)
        End If

        If txtConnectionName.Text.Length < 2 Or txtDataLocation.Text.Length < 2 Or txtDataBaseName.Text.Length < 2 Or txtTableSetsFile.Text.Length < 2 Then
            lblStatus.Text = strMessages.strAllData
            Exit Sub
        End If

        Dim xCNode As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", txtConnectionName.Text)
        If xCNode Is Nothing Then
            xCNode = dhdText.CreateAppendElement(xmlConnections.Item("Sequenchel").Item("Connections"), "Connection", Nothing, False)
        End If

        dhdText.CreateAppendAttribute(xCNode, "Default", "False", True)
        dhdText.CreateAppendElement(xCNode, "ConnectionName", txtConnectionName.Text, True)
        dhdText.CreateAppendElement(xCNode, "DataBaseName", txtDataBaseName.Text, True)
        dhdText.CreateAppendElement(xCNode, "DataLocation", txtDataLocation.Text, True)
        dhdText.CreateAppendElement(xCNode, "LoginName", txtLoginName.Text, True)
        dhdText.CreateAppendElement(xCNode, "Password", DataHandler.txt.EncryptText(txtPassword.Text), True)
        dhdText.CreateAppendElement(xCNode, "DataProvider", cbxDataProvider.Text, True)
        dhdText.CreateAppendElement(xCNode, "LoginMethod", cbxLoginMethod.Text, True)
        dhdText.CreateAppendElement(xCNode, "Timeout", txtTimeOut.Text, True)
        dhdText.CreateAppendElement(xCNode, "TableSets", txtTableSetsFile.Text, True)
        CurStatus.Connection = txtConnectionName.Text
        CurVar.TableSetsFile = txtTableSetsFile.Text
        CurStatus.TableSet = ""
        CurStatus.Table = ""
        CurStatus.ConnectionChanged = True
        ConfigurationSave()
        ConnectionsLoad()
    End Sub

    Private Sub btnConnectionDefault_Click(sender As Object, e As EventArgs) Handles btnConnectionDefault.Click
        If lvwConnections.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwConnections.SelectedItems.Item(0).Tag

            'Get the ParentNode
            Dim xNode0 As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connections")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"
            CurStatus.ConnectionChanged = True
            ConfigurationSave()
            ConnectionLoad()
        End If

    End Sub

    Private Sub btnConnectionClear_Click(sender As Object, e As EventArgs) Handles btnConnectionClear.Click
        ConnectionClear
    End Sub

    Private Sub btnConnectionDelete_Click(sender As Object, e As EventArgs) Handles btnConnectionDelete.Click
        ConnectionDelete()
    End Sub

    Private Sub ConnectionDelete()
        Dim strSelection As String = txtConnectionName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            CurStatus.ConnectionChanged = True
            ConfigurationSave()
            ConnectionClear()
            ConnectionsLoad()
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
    Private Sub TableSetsLoad()
        lvwTableSets.Items.Clear()
        tvwTableSet.Nodes.Clear()
        If xmlTableSets.OuterXml.Length = 0 Then Exit Sub

        Dim lstXml As XmlNodeList
        lstXml = dhdText.FindXmlNodes(xmlTableSets, "//TableSet")
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Tag = xNode.Item("TableSetName").InnerText
            lsvItem.Text = xNode.Item("TableSetName").InnerText
            lsvItem.SubItems.Add(xNode.Item("TablesFile").InnerText)
            lvwTableSets.Items.Add(lsvItem)
        Next
        For Each lstItemn In lvwTableSets.Items
            If lstItemn.Tag = CurStatus.TableSet Then
                lstItemn.Selected = True
            End If
        Next

    End Sub

    Private Sub lvwTableSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwTableSets.SelectedIndexChanged
        CursorControl("Wait")
        TableSetLoad()
        CursorControl()
    End Sub

    Private Sub TableSetLoad()
        TableSetClear()
        If lvwTableSets.SelectedItems.Count = 1 Then
            If CurStatus.TableSet <> lvwTableSets.SelectedItems.Item(0).Tag Then
                CurStatus.TableSetReload = True
                CurStatus.TableSet = lvwTableSets.SelectedItems.Item(0).Tag
                LoadTableSet(CurStatus.TableSet)
                LoadTablesXml()
                TablesLoad()
            End If
            Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", CurStatus.TableSet)
            tvwTableSet.Nodes.Clear()
            DisplayXmlNode(xNode, tvwTableSet.Nodes)
            tvwTableSet.ExpandAll()
            TableSetEdit()
        End If
    End Sub

    Private Sub txtTableSetName_TextChanged(sender As Object, e As EventArgs) Handles txtTableSetName.TextChanged
        If chkDefaultValues.Checked = True Then
            AutoFillDefaults()
        End If
    End Sub

    Private Sub btnDefaultTableSet_Click(sender As Object, e As EventArgs) Handles btnDefaultTableSet.Click
        txtTableSetName.Text = CurVar.TableSetName
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
        txtDefaultPath.Text = CurVar.DefaultConfigFilePath
    End Sub

    Private Sub btnDefaultPathBrowse_Click(sender As Object, e As EventArgs) Handles btnDefaultPathBrowse.Click
        DefaultPathBrowse()
    End Sub

    Private Sub DefaultPathBrowse()
        Dim fbdBrowse As New FolderBrowserDialog
        If (fbdBrowse.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (fbdBrowse.SelectedPath.Length) > 0 Then
            txtDefaultPath.Text = fbdBrowse.SelectedPath
        End If
    End Sub

    Private Sub chkDefaultValues_CheckedChanged(sender As Object, e As EventArgs) Handles chkDefaultValues.CheckedChanged
        If chkDefaultValues.Checked = True Then
            AutoFillDefaults()
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
        txtTablesFile.Text = txtDefaultPath.Text & strSeparator & CurStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Tables.xml"
        txtReportsSetName.Text = txtTableSetName.Text.Replace(" ", "_") & "_Reports"
        txtReportsFile.Text = txtDefaultPath.Text & strSeparator & CurStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Reports.xml"
        txtSearchFile.Text = txtDefaultPath.Text & strSeparator & CurStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Search.xml"
    End Sub

    Private Sub TableSetEdit()
        If lvwTableSets.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTableSets.SelectedItems.Item(0).Tag

            Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strSelection)
            txtTableSetName.Text = xNode.Item("TableSetName").InnerText
            txtTableSetName.Tag = xNode.Item("TableSetName").InnerText
            txtTablesFile.Text = xNode.Item("TablesFile").InnerText
            txtOutputPath.Text = xNode.Item("OutputPath").InnerText
            txtReportsSetName.Text = xNode.Item("ReportSet").Attributes("Name").InnerText
            txtReportsFile.Text = xNode.Item("ReportSet").InnerText
            If dhdText.CheckNodeElement(xNode, "Search") Then txtSearchFile.Text = xNode.Item("Search").InnerText
        End If
    End Sub

    Private Sub btnTableSetDefault_Click(sender As Object, e As EventArgs) Handles btnTableSetDefault.Click
        If lvwTableSets.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTableSets.SelectedItems.Item(0).Tag

            'Get the ParentNode
            Dim xNode0 As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSets")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"

            CurStatus.TableSetChanged = True
            ConfigurationSave()
            TableSetLoad()
        End If
    End Sub

    Private Sub btnTableSetsShow_Click(sender As Object, e As EventArgs) Handles btnTableSetsShow.Click
        DisplayXmlFile(xmlTableSets, tvwTableSet)
    End Sub

    Private Sub btnTableSetAdd_Click(sender As Object, e As EventArgs) Handles btnTableSetAdd.Click
        CursorControl("Wait")
        TableSetAddOrUpdate()
        CursorControl()
    End Sub

    Private Sub TableSetAddOrUpdate()
        'dhdText.RemoveNode(xmlConnections, "Connections", "ConnectionName", txtConnectionName.Text)
        If txtTableSetName.Text.Length < 2 Or txtTablesFile.Text.Length < 2 Then
            lblStatus.Text = strMessages.strAllData
            Exit Sub
        End If

        Dim root As XmlElement = xmlTableSets.DocumentElement
        If root Is Nothing Then
            xmlTableSets = dhdText.CreateRootDocument(xmlTableSets, "Sequenchel", "TableSets", True)
        End If

        Dim xTNode As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", txtTableSetName.Text)
        If xTNode Is Nothing Then
            xTNode = dhdText.CreateAppendElement(xmlTableSets.Item("Sequenchel").Item("TableSets"), "TableSet", Nothing, False)
        End If

        dhdText.CreateAppendAttribute(xTNode, "Default", "False", True)
        dhdText.CreateAppendElement(xTNode, "TableSetName", txtTableSetName.Text, True)
        dhdText.CreateAppendElement(xTNode, "TablesFile", txtTablesFile.Text, True)
        dhdText.CreateAppendElement(xTNode, "OutputPath", txtOutputPath.Text, True)
        dhdText.CreateAppendElement(xTNode, "ReportSet", txtReportsFile.Text, True)
        dhdText.CreateAppendAttribute(xTNode.Item("ReportSet"), "Name", txtReportsSetName.Text, True)
        dhdText.CreateAppendElement(xTNode, "Search", txtSearchFile.Text, True)
        CurStatus.TableSet = txtTableSetName.Text
        CurVar.TablesFile = txtTablesFile.Text
        CurStatus.Table = ""
        CurStatus.TableSetReload = True
        CurStatus.TableSetChanged = True
        ConfigurationSave()
        TableSetsLoad()
    End Sub

    Private Sub btnTableSetClear_Click(sender As Object, e As EventArgs) Handles btnTableSetClear.Click
        TableSetClear()
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

    Private Sub btnTableSetDelete_Click(sender As Object, e As EventArgs) Handles btnTableSetDelete.Click
        Dim strSelection As String = txtTableSetName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            CurStatus.TableSetChanged = True
            ConfigurationSave()
            btnTableSetClear_Click(Nothing, Nothing)
            TableSetsLoad()
        End If
    End Sub

#End Region

#Region "Tables"

    Private Sub TablesLoad()
        lvwTables.Items.Clear()
        tvwTable.Nodes.Clear()
        If xmlTables.OuterXml.Length = 0 Then Exit Sub

        Dim lstXml As XmlNodeList
        lstXml = dhdText.FindXmlNodes(xmlTables, "//Table")
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
            If lstItemn.Tag = CurStatus.Table Then
                lstItemn.Selected = True
            End If
        Next
    End Sub

    Private Sub lvwTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwTables.SelectedIndexChanged
        CursorControl("Wait")
        TableLoad()
        CursorControl()
    End Sub

    Private Sub TableLoad()
        If lvwTables.SelectedItems.Count = 1 Then
            If CurStatus.Table <> lvwTables.SelectedItems.Item(0).Tag Then
                CurStatus.Table = lvwTables.SelectedItems.Item(0).Tag
                CurStatus.TableReload = True
            End If

            Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", CurStatus.Table)
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

    Private Sub btnCrawlTables_Click(sender As Object, e As EventArgs) Handles btnCrawlTables.Click
        CursorControl("Wait")
        'Application.DoEvents()

        Try
            Dim intSqlVersion As Integer = GetSqlVersion(dhdConnection)
            Select Case intSqlVersion
                Case 0
                    MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
                    WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
                    Exit Sub
                Case 7, 8
                    MessageBox.Show("SQL Server 2000 or older is not supported")
                    WriteLog("SQL Server 2000 or older is not supported", 1)
                    Exit Sub
                Case 9, 10, 11, 12, 13
                    WriteLog("SQL Version " & intSqlVersion & " detected.", 3)
                    'Just do it
                Case Else
                    MessageBox.Show("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion)
                    WriteLog("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion, 1)
                    Exit Sub
            End Select
        Catch ex As Exception
            MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
            WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
            Exit Sub
        End Try

        Dim lstNewTables As List(Of String) = LoadTablesList(dhdConnection)

        If lstNewTables Is Nothing Then
            CursorControl()
            lblStatus.Text = "No tables found"
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
        txtTableAlias.Text = lstTables.SelectedItem
        chkTableVisible.Checked = True
        chkTableSearch.Checked = True
        lstTables.Visible = False
    End Sub

    Private Sub lstTables_LostFocus(sender As Object, e As EventArgs) Handles lstTables.LostFocus
        lstTables.Visible = False
    End Sub

    Private Sub btnColumnsImport_Click(sender As Object, e As EventArgs) Handles btnColumnsImport.Click
        CursorControl("Wait")

        Try
            Dim intSqlVersion As Integer = GetSqlVersion(dhdConnection)
            Select Case intSqlVersion
                Case 0
                    MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
                    WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
                    Exit Sub
                Case 7, 8
                    MessageBox.Show("SQL Server 2000 or older is not supported")
                    WriteLog("SQL Server 2000 or older is not supported", 1)
                    Exit Sub
                Case 9, 10, 11, 12, 13
                    WriteLog("SQL Version " & intSqlVersion & " detected.", 3)
                    'Just do it
                Case Else
                    MessageBox.Show("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion)
                    WriteLog("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion, 1)
                    Exit Sub
            End Select
        Catch ex As Exception
            MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
            WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
            Exit Sub
        End Try

        If txtTableName.Text.Length < 2 And chkImportAllTables.Checked = False Then
            lblStatus.Text = "Please enter a schema name with a table name first"
            Exit Sub
        End If

        If chkImportAllTables.Checked = True Then
            ImportAllTables()
        Else
            ImportOneTable(txtTableName.Text, txtTableAlias.Text, True)
        End If

        CurStatus.TableChanged = True
        ConfigurationSave()
        CursorControl()
    End Sub

    Private Sub ImportOneTable(strTableName As String, strTableAlias As String, blnReload As Boolean)
        TableAddOrUpdate(strTableName, strTableAlias, chkTableVisible.Checked, chkTableSearch.Checked, chkTableUpdate.Checked, chkTableInsert.Checked, chkTableDelete.Checked)

        Dim strSchemaName As String = strTableName.Substring(0, strTableName.IndexOf("."))
        Dim TableName As String = strTableName.Substring(strTableName.IndexOf(".") + 1, strTableName.Length - (strTableName.IndexOf(".") + 1))
        ColumnsImport(strSchemaName, TableName, blnReload)

    End Sub

    Private Sub ImportAllTables()
        Dim lstNewTables As List(Of String) = LoadTablesList(dhdConnection)

        If lstNewTables Is Nothing Then
            lblStatus.Text = "No tables found"
            Exit Sub
        End If

        Dim blnReload As Boolean = False
        For Each TableName In lstNewTables
            If lstNewTables.Last = TableName Then
                blnReload = True
            End If
            ImportOneTable(TableName, TableName, blnReload)
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

        Dim dtsData As DataSet = QueryDb(dhdConnection, strQuery, True)
        If dtsData Is Nothing Then
            lblStatus.Text = "No columns found for table " & strSchemaName & "." & strTableName
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

        Dim dtsRelations As DataSet = QueryDb(dhdConnection, strQuery, True)

        strQuery = " SELECT KU.TABLE_SCHEMA as SchemaName, KU.TABLE_NAME as TableName,KU.COLUMN_NAME as PrimaryKeyColumn"
        strQuery &= " FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC"
        strQuery &= " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU"
        strQuery &= " ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' "
        strQuery &= " AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME"
        strQuery &= " WHERE KU.TABLE_SCHEMA = '" & strSchemaName & "'"
        strQuery &= " AND KU.TABLE_NAME = '" & strTableName & "'"
        strQuery &= " ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION;"

        Dim dtsPrimaryKeys As DataSet = QueryDb(dhdConnection, strQuery, True)

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

            strDataType = GetDataType(dtsData.Tables.Item(0).Rows(intRowCount).Item("DataType"))
            blnIdentity = dtsData.Tables.Item(0).Rows(intRowCount).Item("is_identity")
            intWidth = GetWidth(strDataType, dtsData.Tables.Item(0).Rows(intRowCount).Item("MaxLength"))
            If intRowCount = dtsData.Tables(0).Rows.Count - 1 And blnReloadAll = True Then blnReload = True
            FieldAddOrUpdate(strSchemaName & "." & strTableName, dtsData.Tables.Item(0).Rows(intRowCount).Item("colName"), dtsData.Tables.Item(0).Rows(intRowCount).Item("colName"), _
                     strDataType, blnIdentity, blnPrimaryKey, intWidth, "", "", False, txtControlField.Text, txtControlValue.Text, chkControlUpdate.Checked, chkControlMode.Checked, False, "", _
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
                                    dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Schema") & "." _
                                    & dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Table") & "." _
                                    & dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Column"), "", False)
                                'Add Relations for this column
                            End If
                        Next
                    End If
                End If
            End If

            'End If
        Next

    End Sub

    Private Sub chkImportAllTables_CheckedChanged(sender As Object, e As EventArgs) Handles chkImportAllTables.CheckedChanged
        If chkImportAllTables.Checked Then
            btnColumnsImport.Text = "Import Columns for all tables"
        Else
            btnColumnsImport.Text = "Import Columns for this table"
        End If
    End Sub

    Private Sub TableEdit()
        If lvwTables.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTables.SelectedItems.Item(0).Tag

            Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strSelection)
            If dhdText.CheckNodeElement(xNode, "Name") Then txtTableName.Tag = xNode.Item("Name").InnerText
            If dhdText.CheckNodeElement(xNode, "Name") Then txtTableName.Text = xNode.Item("Name").InnerText
            If dhdText.CheckNodeElement(xNode, "Alias") Then txtTableAlias.Text = xNode.Item("Alias").InnerText
            If dhdText.CheckNodeElement(xNode, "Visible") Then chkTableVisible.Checked = xNode.Item("Visible").InnerText
            If dhdText.CheckNodeElement(xNode, "Search") Then chkTableSearch.Checked = xNode.Item("Search").InnerText
            If dhdText.CheckNodeElement(xNode, "Update") Then chkTableUpdate.Checked = xNode.Item("Update").InnerText
            If dhdText.CheckNodeElement(xNode, "Insert") Then chkTableInsert.Checked = xNode.Item("Insert").InnerText
            If dhdText.CheckNodeElement(xNode, "Delete") Then chkTableDelete.Checked = xNode.Item("Delete").InnerText
        End If
    End Sub

    Private Sub btnTableDefault_Click(sender As Object, e As EventArgs) Handles btnTableDefault.Click
        If lvwTables.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwTables.SelectedItems.Item(0).Tag

            'Get the ParentNode
            Dim xNode0 As XmlNode = dhdText.FindXmlNode(xmlTables, "Tables")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"
            CurStatus.TableChanged = True
            ConfigurationSave()
            TableLoad()
        End If
    End Sub

    Private Sub btnTablesShow_Click(sender As Object, e As EventArgs) Handles btnTablesShow.Click
        DisplayXmlFile(xmlTables, tvwTable)
    End Sub

    Private Sub btnTableAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnTableAddOrUpdate.Click
        CursorControl("Wait")
        TableAddOrUpdate(txtTableName.Text, txtTableAlias.Text, chkTableVisible.Checked, chkTableSearch.Checked, chkTableUpdate.Checked, chkTableInsert.Checked, chkTableDelete.Checked, True)
        CurStatus.TableChanged = True
        ConfigurationSave()
        CursorControl()
    End Sub

    Private Sub TableAddOrUpdate(strTableName As String, strAlias As String, blnVisible As Boolean, blnSearch As Boolean, blnUpdate As Boolean, blnInsert As Boolean, blnDelete As Boolean, Optional blnReload As Boolean = True)
        Dim root As XmlElement = xmlTables.DocumentElement
        If root Is Nothing Then
            xmlTables = dhdText.CreateRootDocument(xmlTables, "Sequenchel", "Tables", True)
        End If

        If strAlias.Length = 0 Then strAlias = strTableName

        Dim xTNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        If xTNode Is Nothing Then xTNode = dhdText.CreateAppendElement(xmlTables.Item("Sequenchel").Item("Tables"), "Table")

        dhdText.CreateAppendAttribute(xTNode, "Default", "False", True)
        dhdText.CreateAppendElement(xTNode, "Name", strTableName, True)
        dhdText.CreateAppendElement(xTNode, "Alias", strAlias, True)
        dhdText.CreateAppendElement(xTNode, "Visible", blnVisible, True)
        dhdText.CreateAppendElement(xTNode, "Search", blnSearch, True)
        dhdText.CreateAppendElement(xTNode, "Update", blnUpdate, True)
        dhdText.CreateAppendElement(xTNode, "Insert", blnInsert, True)
        dhdText.CreateAppendElement(xTNode, "Delete", blnDelete, True)

        CurStatus.Table = strTableName
        CurStatus.TableReload = True

        If blnReload = True Then TablesLoad()
    End Sub

    Private Sub btnTableClear_Click(sender As Object, e As EventArgs) Handles btnTableClear.Click
        txtTableName.Text = ""
        txtTableAlias.Text = ""
        chkTableVisible.Checked = False
        chkTableSearch.Checked = False
        chkTableUpdate.Checked = False
        chkTableInsert.Checked = False
    End Sub

    Private Sub btnTableDelete_Click(sender As Object, e As EventArgs) Handles btnTableDelete.Click
        Dim strSelection As String = txtTableName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            CurStatus.TableChanged = True
            ConfigurationSave()
            btnTableClear_Click(Nothing, Nothing)
            TablesLoad()
            tvwTable.Nodes.Clear()
        End If
    End Sub

#End Region

#Region "Fields"
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

    Private Sub NodeDisplay(PNode As TreeNode)
        FieldsClear()
        Dim strFieldName As String = ""
        Dim strFieldValue As String = ""

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
                        If xRnode.Nodes(0).Text = "(ATTRIBUTES)" Then
                            'MessageBox.Show(xNode.Nodes(0).Nodes.Count)
                            Try
                                txtRelatedField.Text = Replace(Replace(xRnode.Nodes(0).Nodes(0).Text, "RelatedField = '", ""), "'", "")
                            Catch ex As Exception
                                txtRelatedField.Text = ""
                            End Try
                            Try
                                chkRelatedField.Checked = Replace(Replace(xRnode.Nodes(0).Nodes(1).Text, "RelatedFieldList = '", ""), "'", "")
                            Catch ex As Exception
                                chkRelatedField.Checked = False
                            End Try
                            strFieldValue = xRnode.Nodes(1).Text
                        Else
                            strFieldValue = xRnode.Nodes(0).Text
                        End If

                        If xRnode.Nodes.Count > 0 Then
                            cbxRelations.Items.Add(strFieldValue)
                        End If
                    Next
                    If cbxRelations.Items.Count > 0 Then cbxRelations.SelectedIndex = 0
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
        FieldAddOrUpdate(CurStatus.Table, txtFieldName.Text, txtFieldAlias.Text, cbxDataType.SelectedItem, chkIdentity.Checked, chkPrimaryKey.Checked, txtFieldWidth.Text, cbxRelations.Text, txtRelatedField.Text, chkRelatedField.Checked, txtControlField.Text, txtControlValue.Text, chkControlUpdate.Checked, _
                         chkControlMode.Checked, chkDefaultButton.Checked, txtDefaultButton.Text, chkFieldList.Checked, txtFieldListOrder.Text, txtFieldListWidth.Text, chkFieldVisible.Checked, chkFieldSearch.Checked, chkFieldSearchList.Checked, chkFieldUpdate.Checked, True)
        CursorControl()
    End Sub

    Private Sub btnFieldClear_Click(sender As Object, e As EventArgs) Handles btnFieldClear.Click
        FieldsClear()
    End Sub

    Private Sub FieldsClear()
        txtFieldName.Tag = ""
        txtFieldName.Text = ""
        txtFieldAlias.Text = ""
        cbxDataType.SelectedIndex = -1
        chkIdentity.Checked = False
        chkPrimaryKey.Checked = False
        txtFieldWidth.Text = ""
        'txtRelations.Text = ""
        cbxRelations.Items.Clear()
        txtRelatedField.Text = ""
        chkRelatedField.Checked = False
        cbxRelations.Text = ""
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

    Private Sub FieldAddOrUpdate(TableName As String, FldName As String, FldAlias As String, DataType As String, Identity As Boolean, PrimaryKey As Boolean, FldWidth As String, Relations As String, RelatedField As String, RelatedFieldList As String, ControlField As String, _
                         ControlValue As String, ControlUpdate As Boolean, ControlMode As Boolean, DefaultButton As Boolean, DefaultValue As String, FldList As Boolean, Order As String, Width As String, _
                         FldVisible As Boolean, FldSearch As Boolean, FldSearchList As Boolean, FldUpdate As Boolean, Optional Reload As Boolean = False)

        Dim xTNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", TableName)
        If xTNode Is Nothing Then
            lblStatus.Text = "The table to which this field belongs was not found"
            Exit Sub
        End If

        Dim xPNode As XmlNode = dhdText.CreateAppendElement(xTNode, "Fields", Nothing, True)
        Dim xFNode As XmlNode = dhdText.FindXmlChildNode(xPNode, "Field", "FldName", FldName)
        If xFNode Is Nothing Then
            xFNode = dhdText.CreateAppendElement(xPNode, "Field", Nothing, False)
            'FieldAdd(TableName, FldName, FldAlias, DataType, FldWidth, Relations, RelatedField, ControlField, ControlValue, ControlUpdate, ControlMode, DefaultButton, DefaultValue, FldList, Order, Width, FldVisible, FldSearch, FldSearchList, FldUpdate, Reload)
            'Else
            '    FieldUpdate(TableName, FldName, FldAlias, DataType, FldWidth, Relations, RelatedField, ControlField, ControlValue, ControlUpdate, ControlMode, DefaultButton, DefaultValue, FldList, Order, Width, FldVisible, FldSearch, FldSearchList, FldUpdate, Reload)
        End If

        dhdText.CreateAppendElement(xFNode, "FldName", FldName, True)
        dhdText.CreateAppendElement(xFNode, "FldAlias", FldAlias, True)
        dhdText.CreateAppendElement(xFNode, "DataType", DataType, True)
        dhdText.CreateAppendElement(xFNode, "Identity", Identity, True)
        dhdText.CreateAppendElement(xFNode, "PrimaryKey", PrimaryKey, True)
        dhdText.CreateAppendElement(xFNode, "FldWidth", FldWidth, True)
        dhdText.CreateAppendElement(xFNode, "ControlField", ControlField, True)
        dhdText.CreateAppendElement(xFNode, "ControlValue", ControlValue, True)
        dhdText.CreateAppendElement(xFNode, "ControlUpdate", ControlUpdate, True)
        dhdText.CreateAppendElement(xFNode, "ControlMode", ControlMode, True)

        Dim xDNode As XmlNode = dhdText.CreateAppendElement(xFNode, "DefaultButton", DefaultButton, True)
        dhdText.CreateAppendAttribute(xDNode, "DefaultValue", DefaultValue, True)

        Dim xLNode As XmlNode = dhdText.CreateAppendElement(xFNode, "FldList", FldList, True)
        dhdText.CreateAppendAttribute(xLNode, "Order", Order, True)
        dhdText.CreateAppendAttribute(xLNode, "Width", Width, True)

        dhdText.CreateAppendElement(xFNode, "FldVisible", FldVisible, True)
        dhdText.CreateAppendElement(xFNode, "FldSearch", FldSearch, True)
        dhdText.CreateAppendElement(xFNode, "FldSearchList", FldSearchList, True)
        dhdText.CreateAppendElement(xFNode, "FldUpdate", FldUpdate, True)

        RelationAdd(TableName, FldName, Relations, RelatedField, RelatedFieldList)

        If Reload = True Then
            tvwTable.Nodes.Clear()
            DisplayXmlNode(xTNode, tvwTable.Nodes)
            tvwTable.Nodes(0).Expand()
            tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
            tvwTable.SelectedNode.Expand()
        End If
        CurStatus.TableChanged = True
        ConfigurationSave()

    End Sub

    Private Sub btnFieldDelete_Click(sender As Object, e As EventArgs) Handles btnFieldDelete.Click
        Dim strSelection As String = txtFieldName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Field", "FldName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            CurStatus.TableChanged = True
            ConfigurationSave()
            FieldsClear()
            lvwTables_SelectedIndexChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub chkFieldVisible_CheckedChanged(sender As Object, e As EventArgs) Handles chkFieldVisible.CheckedChanged
        If chkFieldVisible.Checked = True Then
            lblFieldWidth.Text = lblFieldWidth.Tag & "*"
        Else
            lblFieldWidth.Text = lblFieldWidth.Tag
        End If
    End Sub

#End Region

#Region "Table Templates"
    Private Sub btnloadTemplates_Click(sender As Object, e As EventArgs) Handles btnloadTemplates.Click
        TemplatesGet()
    End Sub

    Private Sub btnSearchTemplate_Click(sender As Object, e As EventArgs) Handles btnSearchTemplate.Click
        TemplatesGet(txtSearchTemplate.Text)
    End Sub


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
            WriteLog(ex.Message, 1)
        End Try

    End Sub

    Private Sub lstAvailableTemplates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAvailableTemplates.SelectedIndexChanged
        CursorControl("Wait")
        Dim MydbRef As New SDBA.DBRef
        xmlDoc.LoadXml(MydbRef.GetScript(lstAvailableTemplates.SelectedItem))
        TemplateShow()
        CursorControl()
    End Sub

    Private Sub TemplateShow()
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlDoc, "Tables")
        tvwSelectedTemplate.Nodes.Clear()
        If xNode Is Nothing Then Exit Sub
        DisplayXmlNode(xNode, tvwSelectedTemplate.Nodes)
        tvwSelectedTemplate.Nodes(0).Expand()
    End Sub

    Private Sub btnUseTemplate_Click(sender As Object, e As EventArgs) Handles btnUseTemplate.Click
        CursorControl("Wait")
        xmlTables = xmlDoc
        CurStatus.TableChanged = True
        ConfigurationSave()
        tabConfiguration.SelectedTab = tpgTables
        TablesLoad()
        CursorControl()
    End Sub

    Private Sub btnLoadTemplate_Click(sender As Object, e As EventArgs) Handles btnLoadTemplate.Click
        CursorControl("Wait")
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

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        'nothing here
    End Sub

#Region "Backup"

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        If txtBackupLocation.Text.Length = 0 Then
            lblStatus.Text = "A backup location is required, aborting action"
            Exit Sub
        End If
        Try
            BackupDatabase(txtBackupLocation.Text)
            SaveConfigSetting("Database", "BackupLocation", txtBackupLocation.Text, "A valid location on the server")
        Catch ex As Exception
            MessageBox.Show("While saving the database, the following error occured: " & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Friend Sub BackupDatabase(ByVal strPath As String)

        Dim dtmNow As Date = Now()
        Dim strFormat As String = "yyyyMMdd_HHmm"
        Dim strDateTime As String = dtmNow.ToString(strFormat)

        strQuery = "BACKUP DATABASE [" & dhdConnection.DatabaseName & "] TO  DISK = N'" & strPath & "\" & dhdConnection.DatabaseName & "_" & strDateTime & ".bak' WITH NOFORMAT, NOINIT,  NAME = N'" & dhdConnection.DatabaseName & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"

        If DebugMode Then MessageBox.Show(strQuery)
        Try
            QueryDb(dhdConnection, strQuery, False)
        Catch ex As Exception
            WriteLog(ex.Message, 1)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnBackupLocation_Click(sender As Object, e As EventArgs) Handles btnBackupLocation.Click
        Dim fbdBackup As New FolderBrowserDialog
        fbdBackup.ShowDialog()
        txtBackupLocation.Text = fbdBackup.SelectedPath
    End Sub

#End Region

#Region "Relations"

    Private Sub btnRelationRemove_Click(sender As Object, e As EventArgs) Handles btnRelationRemove.Click
        If cbxRelations.Text.Length < 1 Then Exit Sub
        Dim strFieldName As String = txtFieldName.Tag
        Dim strSelection As String = cbxRelations.Text
        Dim xPNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", CurStatus.Table)
        Dim xNode As XmlNode = dhdText.FindXmlChildNode(xPNode, "Fields/Field", "FldName", strFieldName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Relations")
        Dim xRNode As XmlNode = dhdText.FindXmlChildNode(xCNode, "Relation", "Relation", strSelection)

        If Not xRNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xRNode.ParentNode.RemoveChild(xRNode)

            tvwTable.Nodes.Clear()
            DisplayXmlNode(xPNode, tvwTable.Nodes)
            tvwTable.Nodes(0).Expand()
            tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
            tvwTable.SelectedNode.Expand()

            CurStatus.TableChanged = True
            ConfigurationSave()
            'If cbxRelations.Items.Contains(cbxRelations.Text) Then cbxRelations.Items.Remove(cbxRelations.Text)
            'cbxRelations.Text = ""
        End If

    End Sub

    Private Sub RelationAdd(strTableName As String, strFieldName As String, strRelation As String, strRelatedField As String, blnRelatedFieldList As Boolean)
        If strRelation.Length = 0 Then Exit Sub
        Dim xPNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        If xPNode Is Nothing Then
            lblStatus.Text = "The Table " & strTableName & " was not found. Aborting action"
            Exit Sub
        End If
        Dim xNode As XmlNode = dhdText.FindXmlChildNode(xPNode, "Fields/Field", "FldName", strFieldName)
        If xNode Is Nothing Then
            lblStatus.Text = "The Field " & strFieldName & " was not found. Aborting action"
            Exit Sub
        End If
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Relations")
        If xCNode Is Nothing Then xCNode = dhdText.CreateAppendElement(xNode, "Relations", Nothing, False)
        Dim xRNode As XmlNode = dhdText.CreateAppendElement(xCNode, "Relation", strRelation, False)
        If strRelatedField.Length > 0 Then
            dhdText.CreateAppendAttribute(xRNode, "RelatedField", strRelatedField, True)
            dhdText.CreateAppendAttribute(xRNode, "RelatedFieldList", blnRelatedFieldList, True)
        End If
        lblStatus.Text = "Updating Relation completed succesfully"

        tvwTable.Nodes.Clear()
        DisplayXmlNode(xPNode, tvwTable.Nodes)
        tvwTable.Nodes(0).Expand()
        tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
        tvwTable.SelectedNode.Expand()

        CurStatus.TableChanged = True
        ConfigurationSave()
    End Sub

    Private Sub btnRelationAdd_Click(sender As Object, e As EventArgs) Handles btnRelationAdd.Click
        If cbxRelations.Text.Length = 0 Then Exit Sub
        RelationAdd(CurStatus.Table, txtFieldName.Tag, cbxRelations.Text, txtRelatedField.Text, chkRelatedField.Checked)

        'If Not cbxRelations.Items.Contains(cbxRelations.Text) Then cbxRelations.Items.Add(cbxRelations.Text)
    End Sub

#End Region

End Class