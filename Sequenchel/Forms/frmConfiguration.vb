Imports System.IO
Imports System.Xml

Public Class frmConfiguration

    Private Sub frmConfiguration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        If basCode.curVar.DebugMode Then btnTest.Visible = True
        cbxDataProvider.SelectedItem = "SQL"
        cbxLoginMethod.SelectedItem = "Windows"
        SecuritySet()
        DataTypesLoad()
        'SetDefaultText(txtPassword)
        ConnectionsLoad()
        ConnectionSelect()
        ConnectionShow()
        'ConnectionLoad()

        TableSetsLoad()
        TableSetSelect()
        TableSetShow()

        TablesLoad()
        TableSelect()
        TableShow()
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
        If basCode.curVar.AllowUpdate = False Then chkTableUpdate.Enabled = False
        If basCode.curVar.AllowInsert = False Then chkTableInsert.Enabled = False
        If basCode.curVar.AllowDelete = False Then chkTableDelete.Enabled = False
    End Sub

    Private Sub DefaultsSet()
        basCode.curStatus.SuspendActions = True
        chkDefaultValues.Checked = True
        basCode.curStatus.SuspendActions = False
    End Sub

    Private Sub ConfigurationSave()
        Try
            If basCode.curStatus.ConnectionChanged = True Then
                basCode.dhdText.SaveXmlFile(basCode.xmlConnections, basCode.dhdText.PathConvert(basCode.CheckFilePath(basCode.curVar.ConnectionsFile)), True)
                basCode.curStatus.ConnectionChanged = False
            End If
            If basCode.curStatus.TableSetChanged = True Then
                basCode.dhdText.SaveXmlFile(basCode.xmlTableSets, basCode.dhdText.PathConvert(basCode.CheckFilePath(basCode.curVar.TableSetsFile)), True)
                basCode.curStatus.TableSetChanged = False
            End If
            If basCode.curStatus.TableChanged = True Then
                basCode.dhdText.SaveXmlFile(basCode.xmlTables, basCode.dhdText.PathConvert(basCode.CheckFilePath(basCode.curVar.TablesFile)), True)
                basCode.curStatus.TableChanged = False
            End If
        Catch ex As Exception
            WriteStatus("There was an error saving the configuration. Check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error saving the configuration. " & ex.Message, 1)
        End Try
    End Sub

    Private Sub DataTypesLoad()
        cbxDataType.Items.Clear()
        Dim lstDataTypes As List(Of String) = basCode.GetDataTypes()
        For Each DataType In lstDataTypes
            cbxDataType.Items.Add(DataType)
        Next
    End Sub

#Region "Connections"
#Region "Controls"

    Private Sub lvwConnections_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwConnections.SelectedIndexChanged
        CursorControl("Wait")
        If lvwConnections.SelectedItems.Count = 1 Then
            If Not basCode.curStatus.Connection = lvwConnections.SelectedItems.Item(0).Name Then
                basCode.curStatus.Connection = lvwConnections.SelectedItems.Item(0).Name
                AllClear(3, True)
                ConnectionLoad()
                ConnectionShow()
            End If
        End If
        CursorControl()
    End Sub

    Private Sub txtConnectionName_TextChanged(sender As Object, e As EventArgs) Handles txtConnectionName.TextChanged
        txtTableSetsFile.Text = basCode.curVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
    End Sub

    Private Sub btnDefaultTableSetFile_Click(sender As Object, e As EventArgs) Handles btnDefaultTableSetFile.Click
        txtTableSetsFile.Text = basCode.curVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
    End Sub

    Private Sub btnTableSetFileBrowse_Click(sender As Object, e As EventArgs) Handles btnTableSetFileBrowse.Click
        Dim loadFile1 As New OpenFileDialog
        loadFile1.InitialDirectory = basCode.curVar.DefaultConfigFilePath
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
            'txtPassword.Text = basCode.dhdMainDB.Password
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

        basCode.dhdConnection.DatabaseName = "master"
        basCode.dhdConnection.DataLocation = txtDataLocation.Text
        basCode.dhdConnection.DataProvider = cbxDataProvider.SelectedItem
        basCode.dhdConnection.LoginMethod = cbxLoginMethod.SelectedItem
        basCode.dhdConnection.LoginName = txtLoginName.Text
        basCode.dhdConnection.Password = txtPassword.Text

        If CheckSqlVersion(basCode.dhdConnection) = False Then Exit Sub

        strQuery = "SELECT name FROM sys.databases WHERE database_id <> 2"
        Dim dtsData As DataSet = basCode.QueryDb(basCode.dhdConnection, strQuery, True)

        If dtsData Is Nothing Then
            CursorControl()
            WriteStatus(basCode.ErrorMessage, 1, lblStatusText)
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
        DisplayXmlFile(basCode.xmlConnections, tvwConnection)
        CursorControl()
    End Sub

    Private Sub btnConnectionDefault_Click(sender As Object, e As EventArgs) Handles btnConnectionDefault.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If lvwConnections.SelectedItems.Count = 1 Then
            Dim strSelection As String = lvwConnections.SelectedItems.Item(0).Name

            'Get the ParentNode
            Dim xNode0 As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlConnections, "Connections")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlConnections, "Connection", "ConnectionName", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"
            basCode.curStatus.ConnectionChanged = True
            ConfigurationSave()
            'ConnectionLoad()
            ConnectionShow()
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

    Private Sub AllClear(intLevel As Integer, blnClearAllFields As Boolean)
        If intLevel >= 4 Then
            'Reload all connections
            basCode.xmlConnections.RemoveAll()
            lvwConnections.Items.Clear()
            basCode.curStatus.ConnectionsReload = True
            basCode.curStatus.Connection = ""
            'basCode.curVar.ConnectionsFile = ""
        End If
        If intLevel >= 3 Then
            'Reload 1 connection and all tablesets
            basCode.xmlTableSets.RemoveAll()
            basCode.curStatus.ConnectionReload = True
            ConnectionClear()
            tvwConnection.Nodes.Clear()

            basCode.curStatus.TableSetsReload = True
            basCode.curStatus.TableSet = ""
            basCode.curVar.TableSetsFile = ""
            lvwTableSets.Items.Clear()
        End If
        If intLevel >= 2 Then
            'Reload 1 tableset and all tables
            basCode.xmlTables.RemoveAll()
            basCode.xmlSearch.RemoveAll()
            basCode.xmlReports.RemoveAll()
            TableSetClear()
            tvwTableSet.Nodes.Clear()
            basCode.curStatus.TableSetReload = True

            basCode.curStatus.ReportsReload = True
            basCode.curStatus.SearchesReload = True
            basCode.curStatus.TablesReload = True
            basCode.curStatus.Table = ""
            basCode.curVar.TablesFile = ""
            lvwTables.Items.Clear()
        End If
        If intLevel >= 1 Then
            'Reload 1 table
            basCode.curStatus.TableReload = True
            TableClear()
            tvwTable.Nodes.Clear()
            FieldClear(blnClearAllFields)
        End If
    End Sub

    Private Sub ConnectionsLoad()
        Dim lstXml As XmlNodeList
        lvwConnections.Items.Clear()
        lstXml = basCode.dhdText.FindXmlNodes(basCode.xmlConnections, "Connection")
        If lstXml Is Nothing Then Exit Sub
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Name = xNode.Item("ConnectionName").InnerText
            lsvItem.Text = xNode.Item("ConnectionName").InnerText
            lsvItem.SubItems.Add(xNode.Item("DataBaseName").InnerText)
            lsvItem.SubItems.Add(xNode.Item("DataLocation").InnerText)
            lvwConnections.Items.Add(lsvItem)

            If xNode.Item("ConnectionName").InnerText = basCode.curStatus.Connection Then
                basCode.dhdConnection.DatabaseName = xNode.Item("DataBaseName").InnerText
                basCode.dhdConnection.DataLocation = xNode.Item("DataLocation").InnerText
                basCode.dhdConnection.DataProvider = xNode.Item("DataProvider").InnerText
                basCode.dhdConnection.LoginMethod = xNode.Item("LoginMethod").InnerText
                basCode.dhdConnection.LoginName = xNode.Item("LoginName").InnerText
                If basCode.dhdText.CheckElement(xNode, "Password") Then basCode.dhdConnection.Password = DataHandler.txt.DecryptText(xNode.Item("Password").InnerText)
            End If
        Next
    End Sub

    Private Sub ConnectionSelect()
        For Each lstItem In lvwConnections.Items
            If lstItem.Name = basCode.curStatus.Connection Then
                lstItem.Selected = True
            End If
        Next
    End Sub

    Private Sub ConnectionLoad()
        basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
        basCode.LoadTableSets(basCode.xmlTableSets)
        basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
        TableSetsLoad()
        TableSetSelect()
        TableSetShow()
        basCode.LoadTables(basCode.xmlTables, False)
        TablesLoad()
        TableSelect()
        TableShow()
    End Sub

    Private Sub ConnectionShow()
        txtConnection.Text = basCode.curStatus.Connection
        If basCode.curStatus.Connection.Length > 0 Then
            Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlConnections, "Connection", "ConnectionName", basCode.curStatus.Connection)
            tvwConnection.Nodes.Clear()
            If xNode Is Nothing Then Exit Sub
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
            Dim strConnection As String = lvwConnections.SelectedItems.Item(0).Name

            Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlConnections, "Connection", "ConnectionName", strConnection)
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
            txtConnectionName.Text = basCode.dhdConnection.DatabaseName
            txtConnectionName.Tag = basCode.dhdConnection.DatabaseName
            txtDataBaseName.Text = basCode.dhdConnection.DatabaseName
            txtDataLocation.Text = basCode.dhdConnection.DataLocation
            txtLoginName.Tag = basCode.dhdConnection.LoginName
            txtLoginName.Text = basCode.dhdConnection.LoginName
            'txtPassword.Text = DataHandler.txt.DecryptText(xNode.Item("Password").InnerText)
            cbxDataProvider.Text = basCode.dhdConnection.DataProvider
            cbxLoginMethod.Text = basCode.dhdConnection.LoginMethod
            txtTimeOut.Text = basCode.dhdConnection.ConnectionTimeout
            txtTableSetsFile.Text = basCode.curVar.DefaultConfigFilePath & "\" & txtConnectionName.Text.Replace(" ", "_") & "TableSets.xml"
        End If
    End Sub

    Private Sub ConnectionAddOrUpdate()
        'dhdText.RemoveNode(basCode.xmlConnections, "Connections", "ConnectionName", txtConnectionName.Text)
        Dim root As XmlElement = basCode.xmlConnections.DocumentElement
        If root Is Nothing Then
            basCode.xmlConnections = basCode.dhdText.CreateRootDocument(basCode.xmlConnections, "Sequenchel", "Connections", True)
        End If

        If txtConnectionName.Text.Length < 2 Or txtDataLocation.Text.Length < 2 Or txtDataBaseName.Text.Length < 2 Or txtTableSetsFile.Text.Length < 2 Then
            WriteStatus(basCode.Message.strAllData, 2, lblStatusText)
            Exit Sub
        End If

        Dim xCNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlConnections, "Connection", "ConnectionName", txtConnectionName.Text)
        If xCNode Is Nothing Then
            xCNode = basCode.dhdText.CreateAppendElement(basCode.xmlConnections.Item("Sequenchel").Item("Connections"), "Connection", Nothing, False)
        End If

        basCode.curStatus.Connection = txtConnectionName.Text
        basCode.curVar.TableSetsFile = txtTableSetsFile.Text

        basCode.dhdText.CreateAppendAttribute(xCNode, "Default", "False", True)
        basCode.dhdText.CreateAppendElement(xCNode, "ConnectionName", txtConnectionName.Text, True)
        basCode.dhdText.CreateAppendElement(xCNode, "DataBaseName", txtDataBaseName.Text, True)
        basCode.dhdText.CreateAppendElement(xCNode, "DataLocation", txtDataLocation.Text, True)
        basCode.dhdText.CreateAppendElement(xCNode, "LoginName", txtLoginName.Text, True)
        If txtPassword.Text <> txtPassword.Tag Then basCode.dhdText.CreateAppendElement(xCNode, "Password", DataHandler.txt.EncryptText(txtPassword.Text), True)
        basCode.dhdText.CreateAppendElement(xCNode, "DataProvider", cbxDataProvider.Text, True)
        basCode.dhdText.CreateAppendElement(xCNode, "LoginMethod", cbxLoginMethod.Text, True)
        basCode.dhdText.CreateAppendElement(xCNode, "Timeout", txtTimeOut.Text, True)
        basCode.dhdText.CreateAppendElement(xCNode, "TableSets", txtTableSetsFile.Text, True)

        basCode.curStatus.ConnectionChanged = True
        ConfigurationSave()
        AllClear(3, True)
        ConnectionsLoad()
        'txtConnection.Text = basCode.curStatus.Connection
        ConnectionLoad()
        ConnectionSelect()
        ConnectionShow()
        WriteStatus("Connection Saved", 0, lblStatusText)
    End Sub

    Private Sub ConnectionDelete()
        Dim strSelection As String = txtConnectionName.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlConnections, "Connection", "ConnectionName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            basCode.curStatus.ConnectionChanged = True
            ConfigurationSave()

            basCode.curStatus.Connection = ""
            AllClear(4, True)
            basCode.LoadConnections(basCode.xmlConnections)
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
        txtConnection.Text = ""
    End Sub

#End Region

#Region "TableSets"
#Region "Controls"

    Private Sub lvwTableSets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwTableSets.SelectedIndexChanged
        CursorControl("Wait")
        If lvwTableSets.SelectedItems.Count = 1 Then
            If basCode.curStatus.TableSet <> lvwTableSets.SelectedItems.Item(0).Name Then
                AllClear(2, True)
                basCode.curStatus.TableSet = lvwTableSets.SelectedItems.Item(0).Name
                TableSetLoad()
                TableSetShow()
            End If
        End If
        CursorControl()
    End Sub

    Private Sub txtTableSetName_TextChanged(sender As Object, e As EventArgs) Handles txtTableSetName.TextChanged
        If chkDefaultValues.Checked = True Then
            AutoFillDefaults()
        End If
    End Sub

    Private Sub btnDefaultTableSet_Click(sender As Object, e As EventArgs) Handles btnDefaultTableSet.Click
        txtTableSetName.Text = basCode.curVar.TableSetName
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
        txtDefaultPath.Text = basCode.curVar.DefaultConfigFilePath
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
            Dim strSelection As String = lvwTableSets.SelectedItems.Item(0).Name

            'Get the ParentNode
            Dim xNode0 As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTableSets, "TableSets")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTableSets, "TableSet", "TableSetName", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"

            basCode.curStatus.TableSetChanged = True
            basCode.curStatus.TableSetsReload = True
            ConfigurationSave()
            'TableSetLoad()
            TableSetShow()
            WriteStatus("Default TableSet Set", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnTableSetsShow_Click(sender As Object, e As EventArgs) Handles btnTableSetsShow.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        DisplayXmlFile(basCode.xmlTableSets, tvwTableSet)
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
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTableSets, "TableSet", "TableSetName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            basCode.curStatus.TableSetChanged = True
            basCode.curStatus.TableSetsReload = True
            basCode.curStatus.TablesReload = True
            ConfigurationSave()
            AllClear(2, True)
            basCode.LoadTableSets(basCode.xmlTableSets)
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
        If basCode.xmlTableSets.OuterXml.Length = 0 Then Exit Sub

        Dim lstXml As XmlNodeList
        lstXml = basCode.dhdText.FindXmlNodes(basCode.xmlTableSets, "TableSet")
        If lstXml Is Nothing Then Exit Sub
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Name = xNode.Item("TableSetName").InnerText
            lsvItem.Text = xNode.Item("TableSetName").InnerText
            lsvItem.SubItems.Add(xNode.Item("TablesFile").InnerText)
            lvwTableSets.Items.Add(lsvItem)
        Next
    End Sub

    Private Sub TableSetSelect()
        For Each lvwItem In lvwTableSets.Items
            If lvwItem.Name = basCode.curStatus.TableSet Then
                lvwItem.Selected = True
            End If
        Next
    End Sub

    Private Sub TableSetLoad()
        basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
        basCode.LoadTables(basCode.xmlTables, False)
        TablesLoad()
        TableSelect()
        TableShow()
    End Sub

    Private Sub TableSetShow()
        tvwTableSet.Nodes.Clear()
        If basCode.curStatus.TableSet.Length > 0 Then
            Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTableSets, "TableSet", "TableSetName", basCode.curStatus.TableSet)
            If xNode Is Nothing Then Exit Sub
            DisplayXmlNode(xNode, tvwTableSet.Nodes)
            tvwTableSet.ExpandAll()
            TableSetEdit()
            lblTableSet.Text = basCode.curStatus.TableSet
        End If
    End Sub

    Private Sub DefaultPathBrowse()
        Dim fbdBrowse As New FolderBrowserDialog
        If (fbdBrowse.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (fbdBrowse.SelectedPath.Length) > 0 Then
            txtDefaultPath.Text = fbdBrowse.SelectedPath
        End If
    End Sub

    Private Sub AutoFillDefaults()
        If basCode.curStatus.SuspendActions = False Then
            If txtConnection.Text.Length > 0 And txtTableSetName.Text.Length > 0 Then
                Dim strSeparator As String = "\"
                If txtDefaultPath.Text.Length = 0 Then
                    strSeparator = ""
                    txtOutputPath.Text = "."
                Else
                    txtOutputPath.Text = txtDefaultPath.Text
                End If
                txtTablesFile.Text = txtDefaultPath.Text & strSeparator & basCode.curStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Tables.xml"
                txtReportsSetName.Text = txtTableSetName.Text.Replace(" ", "_") & "_Reports"
                txtReportsFile.Text = txtDefaultPath.Text & strSeparator & basCode.curStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Reports.xml"
                txtSearchFile.Text = txtDefaultPath.Text & strSeparator & basCode.curStatus.Connection.Replace(" ", "_") & txtTableSetName.Text.Replace(" ", "_") & "Search.xml"
            End If
        End If
    End Sub

    Private Sub TableSetEdit()
        'If lvwTableSets.SelectedItems.Count = 1 Then
        'Dim strSelection As String = lvwTableSets.SelectedItems.Item(0).Name
        Dim strSelection As String = basCode.curStatus.TableSet

        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTableSets, "TableSet", "TableSetName", strSelection)
        txtTableSetName.Text = xNode.Item("TableSetName").InnerText
        txtTableSetName.Tag = xNode.Item("TableSetName").InnerText
        txtTablesFile.Text = xNode.Item("TablesFile").InnerText
        txtOutputPath.Text = xNode.Item("OutputPath").InnerText
        txtReportsSetName.Text = xNode.Item("ReportSet").Attributes("Name").InnerText
        txtReportsFile.Text = xNode.Item("ReportSet").InnerText
        If basCode.dhdText.CheckElement(xNode, "Search") Then txtSearchFile.Text = xNode.Item("Search").InnerText
        'End If
    End Sub

    Private Sub TableSetAddOrUpdate()
        'dhdText.RemoveNode(basCode.xmlConnections, "Connections", "ConnectionName", txtConnectionName.Text)
        If txtConnection.Text.Length = 0 Then
            WriteStatus("No connection is selected", 2, lblStatusText)
            Exit Sub
        End If
        If txtTableSetName.Text.Length < 2 Or txtTablesFile.Text.Length < 2 Then
            WriteStatus(basCode.Message.strAllData, 2, lblStatusText)
            Exit Sub
        End If

        Dim root As XmlElement = basCode.xmlTableSets.DocumentElement
        If root Is Nothing Then
            basCode.xmlTableSets = basCode.dhdText.CreateRootDocument(basCode.xmlTableSets, "Sequenchel", "TableSets", True)
        End If

        Dim xTNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTableSets, "TableSet", "TableSetName", txtTableSetName.Text)
        If xTNode Is Nothing Then
            xTNode = basCode.dhdText.CreateAppendElement(basCode.xmlTableSets.Item("Sequenchel").Item("TableSets"), "TableSet", Nothing, False)
        End If

        basCode.dhdText.CreateAppendAttribute(xTNode, "Default", "False", True)
        basCode.dhdText.CreateAppendElement(xTNode, "TableSetName", txtTableSetName.Text, True)
        basCode.dhdText.CreateAppendElement(xTNode, "TablesFile", txtTablesFile.Text, True)
        basCode.dhdText.CreateAppendElement(xTNode, "OutputPath", txtOutputPath.Text, True)
        basCode.dhdText.CreateAppendElement(xTNode, "ReportSet", txtReportsFile.Text, True)
        basCode.dhdText.CreateAppendAttribute(xTNode.Item("ReportSet"), "Name", txtReportsSetName.Text, True)
        basCode.dhdText.CreateAppendElement(xTNode, "Search", txtSearchFile.Text, True)
        basCode.curStatus.TableSet = txtTableSetName.Text
        basCode.curVar.TablesFile = txtTablesFile.Text

        basCode.curStatus.TableSetChanged = True
        ConfigurationSave()

        AllClear(2, True)
        'basCode.curStatus.TableSetsReload = True
        TableSetsLoad()
        lblTableSet.Text = basCode.curStatus.TableSet
        TableSetLoad()
        TableSetSelect()
        TableSetShow()
        WriteStatus("TableSet Saved", 0, lblStatusText)

    End Sub

    Private Sub TableSetClear()
        basCode.curStatus.SuspendActions = True
        txtTableSetName.Text = ""
        txtDefaultPath.Text = ""
        txtTablesFile.Text = ""
        txtOutputPath.Text = ""
        txtReportsSetName.Text = ""
        txtReportsFile.Text = ""
        txtSearchFile.Text = ""
        lblTableSet.Text = ""
        basCode.curStatus.SuspendActions = False
    End Sub

#End Region

#Region "Tables"
#Region "Controls"

    Private Sub lvwTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwTables.SelectedIndexChanged
        CursorControl("Wait")
        If lvwTables.SelectedItems.Count = 1 Then
            If basCode.curStatus.Table <> lvwTables.SelectedItems.Item(0).Name Then
                AllClear(1, True)
                basCode.curStatus.Table = lvwTables.SelectedItems.Item(0).Name
                basCode.curStatus.TableReload = True
                TableShow()
            End If
        End If
        CursorControl()
    End Sub

    Private Sub btnCrawlTables_Click(sender As Object, e As EventArgs) Handles btnCrawlTables.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        'Application.DoEvents()

        If CheckSqlVersion(basCode.dhdConnection) = False Then Exit Sub

        Dim lstNewTables As List(Of String) = basCode.LoadTablesList(basCode.dhdConnection)

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
        If lblTableSet.Text.Length = 0 Then
            WriteStatus("No tableset was selected", 2, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)

        If CheckSqlVersion(basCode.dhdConnection) = False Then Exit Sub

        If txtTableName.Text.Length < 2 And chkImportAllTables.Checked = False Then
            WriteStatus("Please enter a schema name with a table name.", 2, lblStatusText)
            Exit Sub
        End If

        If chkImportAllTables.Checked = True Then
            ImportAllTables()
        Else
            ImportOneTable(txtTableName.Text, txtTableAlias.Text, True)
        End If

        basCode.curStatus.TableChanged = True
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
            Dim strSelection As String = lvwTables.SelectedItems.Item(0).Name

            'Get the ParentNode
            Dim xNode0 As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Tables")
            'Update all attribuites to False
            Dim xNode As XmlNode
            For Each xNode In xNode0
                xNode.Attributes("Default").InnerText = "False"
            Next

            'Slect the correct Node
            Dim xNode2 As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Alias", strSelection)
            'Update this attribute to true
            xNode2.Attributes("Default").InnerText = "True"
            basCode.curStatus.TableChanged = True
            ConfigurationSave()
            TableShow()
            WriteStatus("Default Table Set", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnTablesShow_Click(sender As Object, e As EventArgs) Handles btnTablesShow.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        DisplayXmlFile(basCode.xmlTables, tvwTable)
        CursorControl()
    End Sub

    Private Sub btnTableAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnTableAddOrUpdate.Click
        If lblTableSet.Text.Length = 0 Then
            WriteStatus("No tableset was selected", 2, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        TableAddOrUpdate(txtTableName.Text, txtTableAlias.Text, chkTableVisible.Checked, chkTableSearch.Checked, chkTableUpdate.Checked, chkTableInsert.Checked, chkTableDelete.Checked, True)
        basCode.curStatus.TableChanged = True
        ConfigurationSave()
        WriteStatus("Table Saved", 0, lblStatusText)
        CursorControl()
    End Sub

    Private Sub btnTableClear_Click(sender As Object, e As EventArgs) Handles btnTableClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        basCode.curStatus.Table = ""
        TableClear()
        tvwTable.Nodes.Clear()
        FieldClear(True)
        CursorControl()
    End Sub

    Private Sub btnTableDelete_Click(sender As Object, e As EventArgs) Handles btnTableDelete.Click
        If lblTableSet.Text.Length = 0 Then
            WriteStatus("No tableset was selected", 2, lblStatusText)
            Exit Sub
        End If
        Dim strSelection As String = txtTableName.Text
        If strSelection.Length = 0 Then
            WriteStatus("No table was selected", 2, lblStatusText)
            Exit Sub
        End If

        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            basCode.curStatus.TableChanged = True
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

    Private Sub sptTable_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles sptTable.SplitterMoved
        pnlTableSettings.Height = sptTable.Panel1.Height
        pnlFieldSettings.Top = sptTable.Panel2.Top
        pnlFieldSettings.Height = sptTable.Panel2.Height
    End Sub

#End Region

    Private Sub TablesLoad()
        lvwTables.Items.Clear()
        If basCode.xmlTables.OuterXml.Length = 0 Then Exit Sub

        Dim lstXml As XmlNodeList
        lstXml = basCode.dhdText.FindXmlNodes(basCode.xmlTables, "Table")
        If lstXml Is Nothing Then Exit Sub
        Dim xNode As XmlNode
        For Each xNode In lstXml
            Dim lsvItem As New ListViewItem
            lsvItem.Name = xNode.Item("Alias").InnerText
            lsvItem.Text = xNode.Item("Name").InnerText
            lsvItem.SubItems.Add(xNode.Item("Alias").InnerText)
            lvwTables.Items.Add(lsvItem)
        Next
    End Sub

    Private Sub TableSelect()
        For Each lvwItem In lvwTables.Items
            If lvwItem.Name = basCode.curStatus.Table Then
                lvwItem.Selected = True
            End If
        Next
    End Sub

    Private Sub TableShow()
        tvwTable.Nodes.Clear()
        If basCode.curStatus.Table.Length > 0 Then
            Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Alias", basCode.curStatus.Table)
            If xNode Is Nothing Then Exit Sub
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
        TableAddOrUpdate(strTableName, strTableAlias, chkTableVisible.Checked, chkTableSearch.Checked, chkTableUpdate.Checked, chkTableInsert.Checked, chkTableDelete.Checked, blnReload)

        Dim strSchemaName As String = strTableName.Substring(0, strTableName.IndexOf("."))
        Dim TableName As String = strTableName.Substring(strTableName.IndexOf(".") + 1, strTableName.Length - (strTableName.IndexOf(".") + 1))
        ColumnsImport(strSchemaName, TableName, blnReload)
    End Sub

    Private Sub ImportAllTables()
        Dim lstNewTables As List(Of String) = basCode.LoadTablesList(basCode.dhdConnection)

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

        Dim dtsData As DataSet = basCode.QueryDb(basCode.dhdConnection, strQuery, True)
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

        Dim dtsRelations As DataSet = basCode.QueryDb(basCode.dhdConnection, strQuery, True)

        strQuery = " SELECT KU.TABLE_SCHEMA as SchemaName, KU.TABLE_NAME as TableName,KU.COLUMN_NAME as PrimaryKeyColumn"
        strQuery &= " FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC"
        strQuery &= " INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU"
        strQuery &= " ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' "
        strQuery &= " AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME"
        strQuery &= " WHERE KU.TABLE_SCHEMA = '" & strSchemaName & "'"
        strQuery &= " AND KU.TABLE_NAME = '" & strTableName & "'"
        strQuery &= " ORDER BY KU.TABLE_NAME, KU.ORDINAL_POSITION;"

        Dim dtsPrimaryKeys As DataSet = basCode.QueryDb(basCode.dhdConnection, strQuery, True)

        Dim blnReload As Boolean = False
        Dim strDataType As String = ""
        Dim intWidth As Integer = 0, intColCount As Integer = 0, intWidthList As Integer = 0, blnShowField As Boolean = False, blnIdentity As Boolean = False, blnPrimaryKey As Boolean = False
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

            strDataType = basCode.GetDataType(dtsData.Tables.Item(0).Rows(intRowCount).Item("DataType"))
            blnIdentity = dtsData.Tables.Item(0).Rows(intRowCount).Item("is_identity")
            intWidth = basCode.GetWidth(strDataType, dtsData.Tables.Item(0).Rows(intRowCount).Item("MaxLength"), 0, False)
            intWidthList = basCode.GetWidth(strDataType, dtsData.Tables.Item(0).Rows(intRowCount).Item("MaxLength"), dtsData.Tables.Item(0).Rows(intRowCount).Item("colName").replace(".", "_").length, True)
            If intRowCount = dtsData.Tables(0).Rows.Count - 1 And blnReloadAll = True Then blnReload = True
            FieldAddOrUpdate(strSchemaName & "." & strTableName, dtsData.Tables.Item(0).Rows(intRowCount).Item("colName"), dtsData.Tables.Item(0).Rows(intRowCount).Item("colName").replace(".", "_"), _
                     strDataType, blnIdentity, blnPrimaryKey, intWidth, "", "", "", "", "", False, txtControlField.Text, txtControlValue.Text, chkControlUpdate.Checked, chkControlMode.Checked, False, "", _
                     blnShowField, intColCount, intWidthList, True, True, chkFieldSearchList.Checked, chkFieldUpdate.Checked, blnReload)

            If Not dtsRelations Is Nothing Then
                basCode.curStatus.SuspendActions = True
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
                                    dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Schema") & "_" & dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Table"), _
                                    dtsRelations.Tables.Item(0).Rows(intRowCountRel).Item("PK_Column"), "", "", False)
                                'Add Relations for this column
                            End If
                        Next
                    End If
                End If
                basCode.curStatus.SuspendActions = False
            End If

            'End If
        Next

    End Sub

    Private Sub TableEdit()
        'If lvwTables.SelectedItems.Count = 1 Then
        'Dim strSelection As String = lvwTables.SelectedItems.Item(0).Name
        Dim strSelection As String = basCode.curStatus.Table
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Alias", strSelection)
        If basCode.dhdText.CheckElement(xNode, "Name") Then txtTableName.Tag = xNode.Item("Name").InnerText
        If basCode.dhdText.CheckElement(xNode, "Name") Then txtTableName.Text = xNode.Item("Name").InnerText
        If basCode.dhdText.CheckElement(xNode, "Alias") Then txtTableAlias.Text = xNode.Item("Alias").InnerText
        If basCode.dhdText.CheckElement(xNode, "Visible") Then chkTableVisible.Checked = xNode.Item("Visible").InnerText
        If basCode.dhdText.CheckElement(xNode, "Search") Then chkTableSearch.Checked = xNode.Item("Search").InnerText
        If basCode.dhdText.CheckElement(xNode, "Update") Then chkTableUpdate.Checked = xNode.Item("Update").InnerText
        If basCode.dhdText.CheckElement(xNode, "Insert") Then chkTableInsert.Checked = xNode.Item("Insert").InnerText
        If basCode.dhdText.CheckElement(xNode, "Delete") Then chkTableDelete.Checked = xNode.Item("Delete").InnerText
        'End If
    End Sub

    Private Sub TableClear()
        txtTableName.Tag = ""
        txtTableName.Text = ""
        txtTableAlias.Text = ""
        chkTableVisible.Checked = True
        chkTableSearch.Checked = True
        chkTableUpdate.Checked = False
        chkTableInsert.Checked = False
        chkTableDelete.Checked = False
    End Sub

    Private Sub TableAddOrUpdate(strTableName As String, strAlias As String, blnVisible As Boolean, blnSearch As Boolean, blnUpdate As Boolean, blnInsert As Boolean, blnDelete As Boolean, Optional blnReload As Boolean = True)
        Dim root As XmlElement = basCode.xmlTables.DocumentElement
        If root Is Nothing Then
            basCode.xmlTables = basCode.dhdText.CreateRootDocument(basCode.xmlTables, "Sequenchel", "Tables", True)
        End If

        If strAlias.Length = 0 Then strAlias = strTableName.Replace(".", "_")

        Dim xTNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strTableName)
        If xTNode Is Nothing Then xTNode = basCode.dhdText.CreateAppendElement(basCode.xmlTables.Item("Sequenchel").Item("Tables"), "Table")
        Dim blnAliasChanged As Boolean = False
        If basCode.dhdText.CheckElement(xTNode, "Alias") Then
            If xTNode.Item("Alias").InnerText <> strAlias Then
                blnAliasChanged = True
            End If
        End If
        basCode.dhdText.CreateAppendAttribute(xTNode, "Default", "False", True)
        basCode.dhdText.CreateAppendElement(xTNode, "Name", strTableName, True)
        basCode.dhdText.CreateAppendElement(xTNode, "Alias", strAlias, True)
        basCode.dhdText.CreateAppendElement(xTNode, "Visible", blnVisible, True)
        basCode.dhdText.CreateAppendElement(xTNode, "Search", blnSearch, True)
        basCode.dhdText.CreateAppendElement(xTNode, "Update", blnUpdate, True)
        basCode.dhdText.CreateAppendElement(xTNode, "Insert", blnInsert, True)
        basCode.dhdText.CreateAppendElement(xTNode, "Delete", blnDelete, True)

        If blnAliasChanged = True Then
            Dim lstAlias As XmlNodeList = basCode.dhdText.FindXmlNodes(basCode.xmlTables, "Relation", "RelationTable", strTableName)
            For Each xNode As XmlNode In lstAlias
                If basCode.dhdText.CheckElement(xNode, "RelationTableAlias") Then xNode.Item("RelationTableAlias").InnerText = strAlias
            Next
        End If

        basCode.curStatus.Table = strAlias
        basCode.curStatus.TableReload = True

        If blnReload = True Then
            AllClear(1, False)
            TablesLoad()
            TableSelect()
            TableShow()
        End If
    End Sub

#End Region

#Region "Fields"
#Region "Controls"

    Private Sub tvwTable_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvwTable.AfterSelect
        WriteStatus("", 0, lblStatusText)
        Dim strFieldName As String = ""
        Dim strFieldValue As String = ""
        Dim trnNode As TreeNode = Nothing

        Try
            If tvwTable.SelectedNode.Parent Is Nothing Then
                trnNode = tvwTable.SelectedNode.Nodes(8).FirstNode
            Else
                If tvwTable.SelectedNode.Parent.Parent Is Nothing Then
                    trnNode = tvwTable.SelectedNode.Parent.Nodes(8).FirstNode
                Else
                    If tvwTable.SelectedNode.Text = "Fields" Then
                        trnNode = tvwTable.SelectedNode.FirstNode
                    ElseIf tvwTable.SelectedNode.Text.Substring(0, 6) = "Field " Then
                        trnNode = tvwTable.SelectedNode
                    ElseIf tvwTable.SelectedNode.Parent.Text.Substring(0, 6) = "Field " Then
                        trnNode = tvwTable.SelectedNode.Parent
                    ElseIf tvwTable.SelectedNode.Parent.Parent.Text.Substring(0, 6) = "Field " Then
                        trnNode = tvwTable.SelectedNode.Parent.Parent
                    Else
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            WriteStatus("Unable to select a field. Check the log.", 1, lblStatusText)
            basCode.WriteLog("Unable to select a field. " & Environment.NewLine & ex.Message, 1)
        End Try

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
        WriteStatus("", 0, lblStatusText)
        If txtFieldName.Text.Length = 0 Or txtFieldAlias.Text.Length = 0 Or cbxDataType.SelectedIndex = -1 Then
            WriteStatus(basCode.Message.strAllData, 2, lblStatusText)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtFieldWidth.Text) = False And IsNumeric(txtFieldWidth.Text) = False Then
            WriteStatus("The Field Width must be a numerical value", 2, lblStatusText)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtFieldListWidth.Text) = False And IsNumeric(txtFieldListWidth.Text) = False Then
            WriteStatus("The Field List Width must be a numerical value", 2, lblStatusText)
            Exit Sub
        End If
        If String.IsNullOrEmpty(txtFieldListOrder.Text) = False And IsNumeric(txtFieldListOrder.Text) = False Then
            WriteStatus("The Field List Order must be a numerical value", 2, lblStatusText)
            Exit Sub
        End If

        CursorControl("Wait")
        Dim strTableName As String = "", strTableAlias As String = ""
        If cbxRelationTables.Text.IndexOf("(") > 0 Then
            strTableName = cbxRelationTables.Text.Substring(cbxRelationTables.Text.IndexOf("(") + 1, cbxRelationTables.Text.Length - (cbxRelationTables.Text.IndexOf("(") + 1) - 1)
            strTableAlias = cbxRelationTables.Text.Substring(0, cbxRelationTables.Text.IndexOf("(") - 1)
        Else
            strTableName = cbxRelationTables.Text
            strTableAlias = cbxRelationTables.Text
        End If
        If basCode.FieldAliasExists(basCode.xmlTables, txtTableName.Text, txtFieldName.Text, txtFieldAlias.Text) = True Then
            WriteStatus("There already is a different field with the same alias.", 2, lblStatusText)
            Exit Sub
        End If
        FieldAddOrUpdate(txtTableName.Text, txtFieldName.Text, txtFieldAlias.Text, cbxDataType.SelectedItem, chkIdentity.Checked, chkPrimaryKey.Checked, txtFieldWidth.Text, strTableName, strTableAlias, cbxRelationFields.Text, txtRelatedField.Text, txtRelatedFieldAlias.Text, chkRelatedField.Checked, txtControlField.Text, txtControlValue.Text, chkControlUpdate.Checked, _
                         chkControlMode.Checked, chkDefaultButton.Checked, txtDefaultButton.Text, chkFieldList.Checked, txtFieldListOrder.Text, txtFieldListWidth.Text, chkFieldVisible.Checked, chkFieldSearch.Checked, chkFieldSearchList.Checked, chkFieldUpdate.Checked, True)
        CursorControl()
    End Sub

    Private Sub btnFieldClear_Click(sender As Object, e As EventArgs) Handles btnFieldClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        FieldClear(True)
        CursorControl()
    End Sub

    Private Sub btnFieldDelete_Click(sender As Object, e As EventArgs) Handles btnFieldDelete.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim strSelection As String = txtFieldName.Text
        If strSelection.Length = 0 Then
            WriteStatus("No Field is selected.", 2, lblStatusText)
            Exit Sub
        End If

        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Field", "FldName", strSelection)
        If Not xNode Is Nothing Then
            If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            xNode.ParentNode.RemoveChild(xNode)

            basCode.curStatus.TableChanged = True
            ConfigurationSave()
            FieldClear(True)
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

    Private Sub btnNodeUp_Click(sender As Object, e As EventArgs) Handles btnNodeUp.Click
        WriteStatus("", 0, lblStatusText)
        MoveNodeUpOrDown("Up")
    End Sub

    Private Sub btnNodeDown_Click(sender As Object, e As EventArgs) Handles btnNodeDown.Click
        WriteStatus("", 0, lblStatusText)
        MoveNodeUpOrDown("Down")
    End Sub

#End Region

    Private Sub NodeDisplay(PNode As TreeNode)
        FieldClear(True)
        If PNode Is Nothing Then Exit Sub
        Dim strFieldName As String = ""
        Dim strFieldValue As String = ""
        Dim strTableName As String = "", strTableAlias As String = "", strRelatedFieldName As String = "", strRelatedFieldAlias As String = ""

        'find field name
        'find xml node
        'display xml node

        For Each tvNode As TreeNode In PNode.Nodes
            strFieldName = tvNode.Text
            strFieldValue = tvNode.Nodes(0).Text

            If tvNode.Text = "FldName" Then
                strFieldValue = tvNode.Nodes(0).Text
                Exit For
            End If
        Next

        If strFieldValue.Length > 0 And basCode.curStatus.Table.Length > 0 Then
            Dim xNode As XmlNode = basCode.GetFieldNode(basCode.xmlTables, basCode.curStatus.Table, strFieldValue)
            If Not xNode Is Nothing Then
                'If basCode.dhdText.CheckElement(xNode, "FldName") Then basfield.FieldName = xNode.Item("FldName").InnerText
                txtFieldName.Text = strFieldValue
                txtFieldAlias.Text = xNode.Item("FldAlias").InnerText

                If basCode.dhdText.CheckElement(xNode, "FldAlias") Then txtFieldAlias.Text = xNode.Item("FldAlias").InnerText
                If txtFieldAlias.Text = "" Then txtFieldAlias.Text = txtFieldName.Text
                If basCode.dhdText.CheckElement(xNode, "DataType") Then cbxDataType.Text = xNode.Item("DataType").InnerText
                If basCode.dhdText.CheckElement(xNode, "Identity") Then chkIdentity.Checked = basCode.CheckBooleanValue(xNode.Item("Identity").InnerText)
                If basCode.dhdText.CheckElement(xNode, "PrimaryKey") Then chkPrimaryKey.Checked = basCode.CheckBooleanValue(xNode.Item("PrimaryKey").InnerText)
                If basCode.dhdText.CheckElement(xNode, "FldWidth") Then txtFieldWidth.Text = basCode.CheckNumericValue(xNode.Item("FldWidth").InnerText)

                If basCode.dhdText.CheckElement(xNode, "DefaultButton") Then chkDefaultButton.Checked = basCode.CheckBooleanValue(xNode.Item("DefaultButton").InnerText)
                If basCode.dhdText.CheckElement(xNode, "DefaultButton/@DefaultValue") Then txtDefaultButton.Text = xNode.Item("DefaultButton").Attributes("DefaultValue").Value
                If basCode.dhdText.CheckElement(xNode, "FldList") Then chkFieldList.Checked = basCode.CheckBooleanValue(xNode.Item("FldList").InnerText)
                If basCode.dhdText.CheckElement(xNode, "FldList/@Order") Then txtFieldListOrder.Text = basCode.CheckNumericValue(xNode.Item("FldList").Attributes("Order").Value)
                If basCode.dhdText.CheckElement(xNode, "FldList/@Width") Then txtFieldListWidth.Text = basCode.CheckNumericValue(xNode.Item("FldList").Attributes("Width").Value)
                If basCode.dhdText.CheckElement(xNode, "FldSearch") Then chkFieldSearch.Checked = basCode.CheckBooleanValue(xNode.Item("FldSearch").InnerText)
                If basCode.dhdText.CheckElement(xNode, "FldSearchList") Then chkFieldSearchList.Checked = basCode.CheckBooleanValue(xNode.Item("FldSearchList").InnerText)
                If basCode.dhdText.CheckElement(xNode, "FldUpdate") Then chkFieldUpdate.Checked = basCode.CheckBooleanValue(xNode.Item("FldUpdate").InnerText)
                If basCode.dhdText.CheckElement(xNode, "FldVisible") Then chkFieldVisible.Checked = basCode.CheckBooleanValue(xNode.Item("FldVisible").InnerText)

                If basCode.dhdText.CheckElement(xNode, "ControlField") Then txtControlField.Text = xNode.Item("ControlField").InnerText
                If basCode.dhdText.CheckElement(xNode, "ControlValue") Then txtControlValue.Text = xNode.Item("ControlValue").InnerText
                If basCode.dhdText.CheckElement(xNode, "ControlUpdate") Then chkControlUpdate.Checked = basCode.CheckBooleanValue(xNode.Item("ControlUpdate").InnerText)
                If basCode.dhdText.CheckElement(xNode, "ControlMode") Then chkControlMode.Checked = basCode.CheckBooleanValue(xNode.Item("ControlMode").InnerText)

                If basCode.dhdText.CheckElement(xNode, "Relations") Then
                    If xNode.Item("Relations").ChildNodes.Count > 0 Then
                        For Each xRnode As XmlNode In xNode.Item("Relations").ChildNodes
                            If xRnode.ChildNodes.Count > 1 Then
                                cbxRelationTables.Items.Add(xRnode.Item("RelationTableAlias").InnerText & " (" & xRnode.Item("RelationTable").InnerText & ")")
                            End If
                        Next
                    End If
                End If
                If cbxRelationTables.Items.Count > 0 Then cbxRelationTables.SelectedIndex = 0
            End If

        End If
    End Sub

    Private Sub FieldClear(blnClearAll As Boolean)
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
        txtRelatedFieldAlias.Text = ""
        chkRelatedField.Checked = False
        chkDefaultButton.Checked = False
        txtDefaultButton.Text = ""
        chkFieldList.Checked = False
        txtFieldListOrder.Text = ""
        txtFieldListWidth.Text = ""
        chkPrimaryKey.Checked = False
        chkFieldVisible.Checked = True

        If blnClearAll = True Then
            txtControlField.Text = ""
            txtControlValue.Text = ""
            chkControlUpdate.Checked = False
            chkControlMode.Checked = False

            chkFieldSearch.Checked = True
            chkFieldSearchList.Checked = False
            chkFieldUpdate.Checked = False
        End If
    End Sub

    Private Sub FieldAddOrUpdate(TableName As String, FldName As String, FldAlias As String, DataType As String, Identity As Boolean, PrimaryKey As Boolean, FldWidth As String, RelationTable As String, RelationTableAlias As String, RelationField As String, RelatedField As String, RelatedFieldAlias As String, RelatedFieldList As String, ControlField As String, _
                         ControlValue As String, ControlUpdate As Boolean, ControlMode As Boolean, DefaultButton As Boolean, DefaultValue As String, FldList As Boolean, Order As String, Width As String, _
                         FldVisible As Boolean, FldSearch As Boolean, FldSearchList As Boolean, FldUpdate As Boolean, Reload As Boolean)

        Dim xTNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", TableName)
        If xTNode Is Nothing Then
            WriteStatus("The table to which this field belongs was not found", 2, lblStatusText)
            Exit Sub
        End If

        Dim xPNode As XmlNode = basCode.dhdText.CreateAppendElement(xTNode, "Fields", Nothing, True)
        Dim xFNode As XmlNode = basCode.dhdText.FindXmlNode(xPNode, "Field", "FldName", FldName)
        If xFNode Is Nothing Then
            xFNode = basCode.dhdText.CreateAppendElement(xPNode, "Field", Nothing, False)
            'FieldAdd(TableName, FldName, FldAlias, DataType, FldWidth, Relations, RelatedField, ControlField, ControlValue, ControlUpdate, ControlMode, DefaultButton, DefaultValue, FldList, Order, Width, FldVisible, FldSearch, FldSearchList, FldUpdate, Reload)
            'Else
            '    FieldUpdate(TableName, FldName, FldAlias, DataType, FldWidth, Relations, RelatedField, ControlField, ControlValue, ControlUpdate, ControlMode, DefaultButton, DefaultValue, FldList, Order, Width, FldVisible, FldSearch, FldSearchList, FldUpdate, Reload)
        End If

        basCode.dhdText.CreateAppendElement(xFNode, "FldName", FldName, True)
        basCode.dhdText.CreateAppendElement(xFNode, "FldAlias", FldAlias, True)
        basCode.dhdText.CreateAppendElement(xFNode, "DataType", DataType, True)
        basCode.dhdText.CreateAppendElement(xFNode, "Identity", Identity, True)
        basCode.dhdText.CreateAppendElement(xFNode, "PrimaryKey", PrimaryKey, True)
        basCode.dhdText.CreateAppendElement(xFNode, "FldWidth", FldWidth, True)
        basCode.dhdText.CreateAppendElement(xFNode, "ControlField", ControlField, True)
        basCode.dhdText.CreateAppendElement(xFNode, "ControlValue", ControlValue, True)
        basCode.dhdText.CreateAppendElement(xFNode, "ControlUpdate", ControlUpdate, True)
        basCode.dhdText.CreateAppendElement(xFNode, "ControlMode", ControlMode, True)

        Dim xDNode As XmlNode = basCode.dhdText.CreateAppendElement(xFNode, "DefaultButton", DefaultButton, True)
        basCode.dhdText.CreateAppendAttribute(xDNode, "DefaultValue", DefaultValue, True)

        Dim xLNode As XmlNode = basCode.dhdText.CreateAppendElement(xFNode, "FldList", FldList, True)
        basCode.dhdText.CreateAppendAttribute(xLNode, "Order", Order, True)
        basCode.dhdText.CreateAppendAttribute(xLNode, "Width", Width, True)

        basCode.dhdText.CreateAppendElement(xFNode, "FldVisible", FldVisible, True)
        basCode.dhdText.CreateAppendElement(xFNode, "FldSearch", FldSearch, True)
        basCode.dhdText.CreateAppendElement(xFNode, "FldSearchList", FldSearchList, True)
        basCode.dhdText.CreateAppendElement(xFNode, "FldUpdate", FldUpdate, True)

        RelationAdd(TableName, FldName, RelationTable, RelationTableAlias, RelationField, RelatedField, RelatedFieldAlias, RelatedFieldList)

        If Reload = True Then
            tvwTable.Nodes.Clear()
            DisplayXmlNode(xTNode, tvwTable.Nodes)
            tvwTable.Nodes(0).Expand()
            tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
            tvwTable.SelectedNode.Expand()
        End If
        basCode.curStatus.TableChanged = True
        ConfigurationSave()
        WriteStatus("Field Saved", 0, lblStatusText)

    End Sub

    Private Sub MoveNodeUpOrDown(Direction As String)
        If txtTableName.Text.Length = 0 Then Exit Sub
        If txtFieldName.Text.Length = 0 Then Exit Sub
        Dim strTable As String = txtTableName.Text
        Dim strField As String = txtFieldName.Text

        Dim xTNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strTable)
        If xTNode Is Nothing Then
            WriteStatus("The table to which this field belongs was not found.", 2, lblStatusText)
            Exit Sub
        End If

        Dim xPNode As XmlNode = basCode.dhdText.CreateAppendElement(xTNode, "Fields", Nothing, True)
        Dim xFNode As XmlNode = basCode.dhdText.FindXmlNode(xPNode, "Field", "FldName", strField)
        If xFNode Is Nothing Then
            WriteStatus("The field to move was not found.", 2, lblStatusText)
            Exit Sub
        End If

        Dim blnMoved As Boolean = MoveNode(xFNode, Direction)

        If blnMoved Then
            tvwTable.Nodes.Clear()
            DisplayXmlNode(xTNode, tvwTable.Nodes)
            tvwTable.Nodes(0).Expand()
            tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
            tvwTable.SelectedNode.Expand()
            'tvwTable.SelectedNode = tvwTable.SelectedNode.Nodes.Find("Field", True)(0)

            For Each tPNode As TreeNode In tvwTable.SelectedNode.Nodes
                For Each tCNode As TreeNode In tPNode.Nodes
                    If tCNode.Text = "FldName" Then
                        If tCNode.Nodes(0).Text = strField Then
                            tvwTable.SelectedNode = tPNode
                            tvwTable.SelectedNode.Collapse()
                            'Exit For
                        End If
                    End If
                Next
            Next

            basCode.curStatus.TableChanged = True
            ConfigurationSave()
            WriteStatus("Field moved", 0, lblStatusText)
        Else
            WriteStatus("Field not moved", 2, lblStatusText)
        End If
    End Sub

    Private Function MoveNode(xNode As XmlNode, Direction As String) As Boolean
        Dim xPNode As XmlNode = xNode.PreviousSibling
        Dim xNNode As XmlNode = xNode.NextSibling
        Dim XMNode As XmlNode = xNode.ParentNode

        If XMNode Is Nothing Then
            Return False
        ElseIf Direction = "Up" And Not xPNode Is Nothing Then
            XMNode.RemoveChild(xNode)
            XMNode.InsertBefore(xNode, xPNode)
        ElseIf Direction = "Down" And Not xNNode Is Nothing Then
            XMNode.RemoveChild(xNode)
            XMNode.InsertAfter(xNode, xNNode)
        Else
            Return False
        End If
        Return True
    End Function

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
        basCode.xmlTemplates.LoadXml(MydbRef.GetScript(lstAvailableTemplates.SelectedItem))
        TemplateShow()
        CursorControl()
    End Sub

    Private Sub btnUseTemplate_Click(sender As Object, e As EventArgs) Handles btnUseTemplate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        basCode.xmlTables = basCode.xmlTemplates
        basCode.curStatus.TableChanged = True
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
            basCode.xmlTemplates.Load(loadFile1.FileName)
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
            basCode.WriteLog(ex.Message, 1)
        End Try

    End Sub

    Private Sub TemplateShow()
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTemplates, "Tables")
        tvwSelectedTemplate.Nodes.Clear()
        If xNode Is Nothing Then Exit Sub
        DisplayXmlNode(xNode, tvwSelectedTemplate.Nodes)
        tvwSelectedTemplate.Nodes(0).Expand()
    End Sub

#End Region

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        MessageBox.Show(basCode.curStatus.Table)
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
            basCode.WriteLog("While saving the database, the following error occured: " & Environment.NewLine & ex.Message, 1)
            WriteStatus("An error occured saving the database. Please check the log", 1, lblStatusText)
        End Try
        CursorControl()
    End Sub

    Friend Sub BackupDatabase(ByVal strPath As String)

        Dim dtmNow As Date = Now()
        Dim strFormat As String = "yyyyMMdd_HHmm"
        Dim strDateTime As String = dtmNow.ToString(strFormat)

        strQuery = "BACKUP DATABASE [" & basCode.dhdConnection.DatabaseName & "] TO  DISK = N'" & strPath & "\" & basCode.dhdConnection.DatabaseName & "_" & strDateTime & ".bak' WITH NOFORMAT, NOINIT,  NAME = N'" & basCode.dhdConnection.DatabaseName & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"

        If basCode.curVar.DebugMode Then MessageBox.Show(strQuery)
        Try
            basCode.QueryDb(basCode.dhdConnection, strQuery, False)
        Catch ex As Exception
            basCode.WriteLog("Backup error database: " & ex.Message, 1)
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
        Dim lstFindTables As List(Of String) = basCode.LoadTables(basCode.xmlTables, True)

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
            cbxRelationFields.Text = ""
            chkRelatedField.Checked = False
            txtRelatedField.Text = ""
            txtRelatedFieldAlias.Text = ""
        End If
        lstRelationTables.Visible = False
    End Sub

    Private Sub cbxRelationTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxRelationTables.SelectedIndexChanged
        If basCode.curStatus.SuspendActions = False Then
            cbxRelationFields.Text = ""
            chkRelatedField.Checked = False
            txtRelatedField.Text = ""
            txtRelatedFieldAlias.Text = ""
            If cbxRelationTables.Text.Length = 0 Then Exit Sub

            'get table name; get fieldname; get relationname; find relation in basetable/basefield/baserelation; load baserelation to screen.
            Dim strTableName As String = "", strTableAlias As String = ""
            If cbxRelationTables.Text.IndexOf("(") > 0 Then
                'strTableName = cbxRelationTables.Text.Substring(cbxRelationTables.Text.IndexOf("(") + 1, cbxRelationTables.Text.Length - (cbxRelationTables.Text.IndexOf("(") + 1) - 1)
                strTableName = basCode.GetTableNameFromString(cbxRelationTables.Text)
                'strTableAlias = cbxRelationTables.Text.Substring(0, cbxRelationTables.Text.IndexOf("(") - 2)
                strTableAlias = basCode.GetTableAliasFromString(cbxRelationTables.Text)
            Else
                strTableName = cbxRelationTables.Text
                strTableAlias = cbxRelationTables.Text
            End If

            Dim strFieldName As String = txtFieldName.Text
            Dim xMNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", txtTableName.Text)
            If xMNode Is Nothing Then Exit Sub
            Dim xPNode As XmlNode = basCode.dhdText.FindXmlNode(xMNode, "Fields/Field", "FldName", txtFieldName.Text)
            If xPNode Is Nothing Then Exit Sub

            Dim xlmRNodes As XmlNodeList = basCode.dhdText.FindXmlNodes(xPNode, "Relations/Relation", Nothing, Nothing)
            If xlmRNodes Is Nothing Then Exit Sub

            For Each xRNode As XmlNode In xlmRNodes
                If basCode.dhdText.CheckElement(xRNode, "RelationTable") = True Then
                    If xRNode.Item("RelationTable").InnerText = strTableName Then
                        If basCode.dhdText.CheckElement(xRNode, "RelationField") = True Then cbxRelationFields.Text = xRNode.Item("RelationField").InnerText
                        If basCode.dhdText.CheckElement(xRNode, "RelatedFieldList") = True Then chkRelatedField.Checked = xRNode.Item("RelatedFieldList").InnerText
                        If basCode.dhdText.CheckElement(xRNode, "RelatedFieldName") = True Then txtRelatedField.Text = xRNode.Item("RelatedFieldName").InnerText
                        If basCode.dhdText.CheckElement(xRNode, "RelatedFieldAlias") = True Then txtRelatedFieldAlias.Text = xRNode.Item("RelatedFieldAlias").InnerText
                    End If
                    'Else
                    '    'old style. Delete before deployment
                    '    Dim strRelation As String = xRNode.InnerText
                    '    Dim strRelationField As String = ""
                    '    If strRelation.Length > 0 Then
                    '        If strRelation.IndexOf(".") > 0 Then
                    '            strRelationField = strRelation.Substring(strRelation.LastIndexOf(".") + 1, strRelation.Length - (strRelation.LastIndexOf(".") + 1))
                    '            strRelation = strRelation.Substring(0, strRelation.LastIndexOf(".")) & " (" & strRelation.Substring(0, strRelation.LastIndexOf(".")) & ")"
                    '        End If

                    '        If strRelation = cbxRelationTables.SelectedItem Then
                    '            cbxRelationFields.Text = strRelationField
                    '            If xRNode.Attributes.Count > 0 Then
                    '                If xRNode.Attributes(0).Name = "RelatedField" Then txtRelatedField.Text = xRNode.Attributes("RelatedField").InnerText
                    '                If xRNode.Attributes.Count > 1 Then
                    '                    If xRNode.Attributes(1).Name = "RelatedFieldList" Then chkRelatedField.Checked = xRNode.Attributes("RelatedFieldList").InnerText
                    '                End If
                    '                Exit For
                    '            End If
                    '        End If
                    '    End If
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
        ShowTableFields(lstRelationFields)

        'CursorControl("Wait")
        'If cbxRelationTables.Text.Length = 0 Then
        '    CursorControl()
        '    WriteStatus("No table selected", 2, lblStatusText)
        '    Exit Sub
        'End If

        'Dim strTable As String = cbxRelationTables.Text
        'If strTable.IndexOf("(") > 0 Then
        '    strTable = strTable.Substring(strTable.IndexOf("(") + 1, strTable.Length - strTable.IndexOf("(") - 2)
        'End If
        'Dim lstFindFields As New List(Of String)
        'lstFindFields = basCode.dhdText.LoadItemList(xmlTables, "Table", "Name", strTable, "Field", "FldName")

        'If lstFindFields Is Nothing Then
        '    CursorControl()
        '    WriteStatus("No fields found", 2, lblStatusText)
        '    Exit Sub
        'End If

        'lstRelationFields.Items.Clear()
        'lstRelationFields.Items.Add("")

        'For Each lstItem As String In lstFindFields
        '    lstRelationFields.Items.Add(lstItem)
        'Next

        'CursorControl()
        'If lstRelationFields.Items.Count < 15 Then
        '    lstRelationFields.Height = lstRelationFields.Items.Count * 15
        'Else
        '    lstRelationFields.Height = 15 * 15
        'End If
        'lstRelationFields.Visible = True
        'lstRelationFields.Focus()

    End Sub

    Private Sub btnShowRelatedFields_Click(sender As Object, e As EventArgs) Handles btnShowRelatedFields.Click
        ShowTableFields(lstRelatedFields)
    End Sub

    Private Sub ShowTableFields(lstInput As ListBox)
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
        lstFindFields = basCode.dhdText.LoadItemList(basCode.xmlTables, "Table", "Name", strTable, "Field", "FldName")

        If lstFindFields Is Nothing Then
            CursorControl()
            WriteStatus("No fields found", 2, lblStatusText)
            Exit Sub
        End If

        lstInput.Items.Clear()
        lstInput.Items.Add("")

        For Each lstItem As String In lstFindFields
            lstInput.Items.Add(lstItem)
        Next

        CursorControl()
        If lstInput.Items.Count < 15 Then
            lstInput.Height = lstInput.Items.Count * 15
        Else
            lstInput.Height = 15 * 15
        End If
        lstInput.Visible = True
        lstInput.Focus()
    End Sub

    Private Sub lstRelationFields_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRelationFields.SelectedIndexChanged
        cbxRelationFields.Text = lstRelationFields.SelectedItem
        lstRelationFields.Visible = False
    End Sub

    Private Sub lstRelationFields_LostFocus(sender As Object, e As EventArgs) Handles lstRelationFields.LostFocus
        lstRelationFields.Visible = False
    End Sub

    Private Sub lstRelatedFields_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRelatedFields.SelectedIndexChanged
        txtRelatedField.Text = lstRelatedFields.SelectedItem
        lstRelatedFields.Visible = False
    End Sub

    Private Sub lstRelatedFields_LostFocus(sender As Object, e As EventArgs) Handles lstRelatedFields.LostFocus
        lstRelatedFields.Visible = False
    End Sub

    Private Sub btnRelationRemove_Click(sender As Object, e As EventArgs) Handles btnRelationRemove.Click
        CursorControl("Wait")
        If cbxRelationTables.Text.Length < 1 Or cbxRelationFields.Text.Length < 1 Then Exit Sub
        Dim strFieldName As String = txtFieldName.Tag
        Dim strRelatedTable As String = cbxRelationTables.Text
        Dim strRelatedField As String = cbxRelationFields.Text

        If strRelatedTable.Contains("(") Then strRelatedTable = strRelatedTable.Substring(strRelatedTable.IndexOf("(") + 1, strRelatedTable.Length - (strRelatedTable.IndexOf("(") + 1) - 1)

        If RelationRemove(txtTableName.Text, txtFieldName.Text, strRelatedTable, strRelatedField, True) = False Then
            Exit Sub
        End If

        Dim xPNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Alias", basCode.curStatus.Table)

        tvwTable.Nodes.Clear()
        DisplayXmlNode(xPNode, tvwTable.Nodes)
        tvwTable.Nodes(0).Expand()
        tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
        tvwTable.SelectedNode.Expand()

        basCode.curStatus.TableChanged = True
        ConfigurationSave()
        CursorControl()
    End Sub

    Private Function RelationRemove(strTableSource As String, strFieldSource As String, strRelatedTable As String, strRelatedField As String, blnRemoveOnly As Boolean) As Boolean
        Dim xPNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strTableSource)
        If xPNode Is Nothing Then
            WriteStatus("The Table " & strTableSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xPNode, "Fields/Field", "FldName", strFieldSource)
        If xNode Is Nothing Then
            WriteStatus("The Field " & strFieldSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xCNode As XmlNode = basCode.dhdText.FindXmlNode(xNode, "Relations")
        'If relations node does not exist, nothing needs to be deleted.
        If xCNode Is Nothing Then Return True

        Dim xRNode As XmlNode = basCode.dhdText.FindXmlNode(xCNode, "Relation", "RelationTable", strRelatedTable)
        'Find relation old style
        If xRNode Is Nothing Then xRNode = basCode.dhdText.FindXmlNode(xCNode, "Relation", "Relation", strRelatedTable & "." & strRelatedField)
        If xRNode Is Nothing Then
            WriteStatus("The Relation " & strRelatedTable & "." & strRelatedField & " was not found. Nothing is deleted", 2, lblStatusText)
            Return True
        End If

        If blnRemoveOnly = True Then
            If MessageBox.Show("This will permanently remove the relation: " & strRelatedTable & "." & strRelatedField & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Return False
        End If

        'remove existing node
        Try
            'enhance relation detection (table name/table alias/field name)
            If xCNode.ChildNodes.Count > 0 Then
                Dim strRelTable As String = "", strRelField As String = "", strRelation As String = ""

                For Each xXNode As XmlNode In xCNode.ChildNodes
                    If basCode.dhdText.CheckElement(xXNode, "RelationTable") Then strRelTable = xXNode.Item("RelationTable").InnerText
                    If basCode.dhdText.CheckElement(xXNode, "RelationField") Then strRelField = xXNode.Item("RelationField").InnerText
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
            basCode.WriteLog("Error removing (old) relation: " & ex.Message, 1)
            WriteStatus("Error removing (old) relation: " & ex.Message, 1, lblStatusText)
            Return False
        End Try

    End Function

    Private Function RelationAdd(strTableSource As String, strFieldSource As String, strTableRelation As String, strTableAliasRelation As String, strFieldRelation As String, strRelatedField As String, strRelatedFieldAlias As String, blnRelatedFieldList As Boolean) As Boolean
        If strFieldRelation.Length = 0 Then Return False
        If String.IsNullOrEmpty(strRelatedFieldAlias) Then strRelatedFieldAlias = strRelatedField

        If RelationRemove(strTableSource, strFieldSource, strTableRelation, strFieldRelation, False) = False Then
            Return False
        End If

        Dim xPNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strTableSource)
        If xPNode Is Nothing Then
            WriteStatus("The Table " & strTableSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xPNode, "Fields/Field", "FldName", strFieldSource)
        If xNode Is Nothing Then
            WriteStatus("The Field " & strFieldSource & " was not found. Aborting action", 2, lblStatusText)
            Return False
        End If
        Dim xCNode As XmlNode = basCode.dhdText.FindXmlNode(xNode, "Relations")
        If xCNode Is Nothing Then xCNode = basCode.dhdText.CreateAppendElement(xNode, "Relations", Nothing, False)

        Try
            Dim xRNode As XmlNode = basCode.dhdText.CreateAppendElement(xCNode, "Relation", Nothing, False)
            basCode.dhdText.CreateAppendElement(xRNode, "RelationTable", strTableRelation, True)
            basCode.dhdText.CreateAppendElement(xRNode, "RelationTableAlias", strTableAliasRelation, True)
            basCode.dhdText.CreateAppendElement(xRNode, "RelationField", strFieldRelation, True)
            basCode.dhdText.CreateAppendElement(xRNode, "RelatedFieldName", strRelatedField, True)
            basCode.dhdText.CreateAppendElement(xRNode, "RelatedFieldAlias", strRelatedFieldAlias, True)
            basCode.dhdText.CreateAppendElement(xRNode, "RelatedFieldList", blnRelatedFieldList, True)

            WriteStatus("Add/Update Relation completed succesfully", 0, lblStatusText)
            basCode.curStatus.TableChanged = True
            ConfigurationSave()
        Catch ex As Exception
            basCode.WriteLog("Error saving relation: " & ex.Message, 1)
            WriteStatus("Error saving relation: " & ex.Message, 1, lblStatusText)
            Return False
        End Try

        If basCode.curStatus.SuspendActions = False Then
            tvwTable.Nodes.Clear()
            DisplayXmlNode(xPNode, tvwTable.Nodes)
            tvwTable.Nodes(0).Expand()
            tvwTable.SelectedNode = tvwTable.Nodes.Find("Fields", True)(0)
            tvwTable.SelectedNode.Expand()
        End If
        Return True

    End Function

    Private Sub btnRelationAdd_Click(sender As Object, e As EventArgs) Handles btnRelationAdd.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If cbxRelationFields.Text.Length = 0 Then Exit Sub
        Dim strTableName As String = "", strTableAlias As String = ""
        If cbxRelationTables.Text.IndexOf("(") > 0 Then
            strTableName = cbxRelationTables.Text.Substring(cbxRelationTables.Text.IndexOf("(") + 1, cbxRelationTables.Text.Length - (cbxRelationTables.Text.IndexOf("(") + 1) - 1)
            strTableAlias = cbxRelationTables.Text.Substring(0, cbxRelationTables.Text.IndexOf("(") - 1)
        Else
            strTableName = cbxRelationTables.Text
            strTableAlias = cbxRelationTables.Text
        End If
        RelationAdd(txtTableName.Text, txtFieldName.Text, strTableName, strTableAlias, cbxRelationFields.Text, txtRelatedField.Text, txtRelatedFieldAlias.Text, chkRelatedField.Checked)

        'If Not cbxRelations.Items.Contains(cbxRelations.Text) Then cbxRelations.Items.Add(cbxRelations.Text)
        CursorControl()
    End Sub

#End Region

End Class