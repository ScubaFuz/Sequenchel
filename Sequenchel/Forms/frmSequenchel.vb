Imports System.IO
Imports System.Xml

Public Class frmSequenchel

    Private Sub frmSequenchel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        'dgvTable1.BackgroundImageLayout = ImageLayout.Center

        ParseCommands()
        Me.Text = My.Application.Info.Title
        If SeqData.CurStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch Then Me.Text &= " ControlMode"
        If SeqData.curVar.DebugMode Then Me.Text &= " Debug"
        If SeqData.CurVar.DevMode Then Me.Text &= " Development"
        If SeqData.CurVar.Encryption = False Then Me.Text &= " NoEncryption"

        DebugSettings()
        Core.SetDefaults()
        SeqData.SetDefaults()
        LoadLicense(lblStatusText)
        Me.Hide()
        frmAbout.Show()
        frmAbout.Refresh()

        lblLicense.Text = "Licensed to: " & Core.LicenseName
        lblLicense.Left = Me.Width - lblLicense.Width - (SeqData.CurVar.BuildMargin * 5)

        If SeqData.LoadSDBASettingsXml(xmlSDBASettings) = False Then
            If SeqData.SaveSDBASettingsXml(xmlSDBASettings) = False Then
                SeqData.WriteLog(Core.Message.strXmlError, 1)
                MessageBox.Show(Core.Message.strXmlError)
            End If

        End If
        SecuritySet()
        SeqData.LoadGeneralSettingsXml(xmlGeneralSettings)
        'Core.DeleteOldLogs()
        LoadConnections()

        If SeqData.CurVar.SecurityOverride = True Then Me.Text &= " SecurityOverride"

        Me.Show()
        If Core.LicenseValidated Then
            frmAbout.Hide()
        Else
            frmAbout.TopMost = True
        End If
        btnSearch.Focus()
    End Sub

    Private Sub frmSequenchel_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        SecuritySet()
        If SeqData.curStatus.ConnectionReload = True Then
            LoadConnections()
            SeqData.CurStatus.ConnectionReload = False
            SeqData.CurStatus.TableSetReload = False
            SeqData.CurStatus.TableReload = False
        Else
            If Not cbxConnection.SelectedItem Is Nothing Then
                If SeqData.curStatus.Connection <> cbxConnection.SelectedItem Then
                    SeqData.curStatus.Connection = cbxConnection.SelectedItem
                    SeqData.LoadConnection(xmlConnections, SeqData.curStatus.Connection)
                End If
            End If
        End If
        If SeqData.CurStatus.TableSetReload = True Then
            LoadTableSets()
            SeqData.CurStatus.TableSetReload = False
            SeqData.CurStatus.TableReload = False
        Else
            If Not cbxTableSet.SelectedItem Is Nothing Then
                If SeqData.curStatus.TableSet <> cbxTableSet.SelectedItem Then
                    SeqData.curStatus.TableSet = cbxTableSet.SelectedItem
                    SeqData.LoadTableSet(xmlTableSets, SeqData.curStatus.TableSet)
                End If
            End If
        End If
        If SeqData.CurStatus.TableReload = True Then
            LoadTables()
            SeqData.CurStatus.TableReload = False
        Else
            If Not cbxTable.SelectedItem Is Nothing Then
                If SeqData.CurStatus.Table <> cbxTable.SelectedItem Then
                    SeqData.CurStatus.Table = cbxTable.SelectedItem
                    LoadTable(SeqData.CurStatus.Table)
                End If
            End If
        End If
    End Sub

    Private Sub DebugSettings()
        If SeqData.CurVar.DebugMode Then
            btnTest.Visible = True
        End If
        If SeqData.CurVar.DevMode Then
            'mnuMain.Visible = True
        End If
    End Sub

    Private Sub SecuritySet()
        If SeqData.CurVar.AllowSettingsChange = False And SeqData.CurVar.SecurityOverride = False Then mnuMainEditSettings.Enabled = False
        If SeqData.CurVar.AllowConfiguration = False And SeqData.CurVar.SecurityOverride = False Then mnuMainEditConfiguration.Enabled = False
        If SeqData.CurVar.AllowLinkedServers = False And SeqData.CurVar.SecurityOverride = False Then mnuMainEditLinkedServers.Enabled = False
        If SeqData.CurVar.AllowDataImport = False And SeqData.CurVar.SecurityOverride = False Then mnuMainToolsImport.Enabled = False
        If SeqData.CurVar.AllowSmartUpdate = False And SeqData.CurVar.SecurityOverride = False Then mnuMainToolsSmartUpdate.Enabled = False
        If SeqData.CurVar.AllowUpdate = False And SeqData.CurVar.SecurityOverride = False Then btnUpdate.Enabled = False
        If SeqData.CurVar.AllowInsert = False And SeqData.CurVar.SecurityOverride = False Then btnAdd.Enabled = False
        If SeqData.CurVar.AllowDelete = False And SeqData.CurVar.SecurityOverride = False Then btnDelete.Enabled = False
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        'MessageBox.Show(FormatFieldWhere2("SearchField", "dbo.TableName", "15", "CHAR", "Pirates AND Pearl NOT Dead OR Caribbean"))
    End Sub

#Region "Navigation"

#Region "Controls"
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub mnuMainFileExit_Click(sender As Object, e As EventArgs) Handles mnuMainFileExit.Click
        Application.Exit()
    End Sub

    Private Sub mnuMainEditSettings_Click(sender As Object, e As EventArgs) Handles mnuMainEditSettings.Click
        CursorControl("Wait")
        ShowSettingsForm()
        CursorControl()
    End Sub

    Private Sub mnuMainEditLinkedServers_Click(sender As Object, e As EventArgs) Handles mnuMainEditLinkedServers.Click
        ShowLinkedServerForm()
    End Sub

    Private Sub mnuMainEditConfiguration_Click(sender As Object, e As EventArgs) Handles mnuMainEditConfiguration.Click
        ShowConfigurationForm()
    End Sub

    Private Sub mnuMainToolsReports_Click(sender As Object, e As EventArgs) Handles mnuMainToolsReports.Click
        ShowReportsForm()
    End Sub

    Private Sub mnuMainToolsImport_Click(sender As Object, e As EventArgs) Handles mnuMainToolsImport.Click
        ShowImportForm()
    End Sub

    Private Sub mnuMainToolsSmartUpdate_Click(sender As Object, e As EventArgs) Handles mnuMainToolsSmartUpdate.Click
        ShowSmartUpdateForm()
    End Sub

    Private Sub mnuMainHelpManual_Click(sender As Object, e As EventArgs) Handles mnuMainHelpManual.Click
        System.Diagnostics.Process.Start("http://sequenchel.com/service/manual")
    End Sub

    Private Sub mnuMainHelpAbout_Click(sender As Object, e As EventArgs) Handles mnuMainHelpAbout.Click
        SeqData.CurVar.CallSplash = True
        Dim frmAboutForm As New frmAbout
        frmAboutForm.Show(Me)
    End Sub

#End Region

    Private Sub ShowReportsForm()
        Dim frmReportsForm As New frmReports
        frmReportsForm.Show(Me)
    End Sub

    Private Sub ShowImportForm()
        Dim frmImportForm As New frmImport
        frmImportForm.Show()
    End Sub

    Private Sub ShowSmartUpdateForm()
        Dim frmSmartUpdateForm As New frmSmartUpdate
        frmSmartUpdateForm.Show(Me)
    End Sub

    Private Sub ShowLinkedServerForm()
        Dim frmLinkedServerForm As New frmLinkedServer
        frmLinkedServerForm.ShowDialog(Me)
    End Sub

    Private Sub ShowConfigurationForm()
        Dim frmConfigurationForm As New frmConfiguration
        frmConfigurationForm.ShowDialog(Me)
    End Sub

    Private Sub ShowSettingsForm()
        Dim frmSettingsForm As New frmSettings
        frmSettingsForm.ShowDialog(Me)
    End Sub

#End Region

#Region "Common routines"


#End Region

#Region "Definitions Load"

#Region "Controls"
    Private Sub btnConnectionsReload_Click(sender As Object, e As EventArgs) Handles btnConnectionsReload.Click
        LoadConnections()
    End Sub

    Private Sub cbxConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxConnection.SelectedIndexChanged
        If cbxConnection.SelectedIndex >= -1 Then
            SeqData.CurStatus.Connection = cbxConnection.SelectedItem
            SeqData.LoadConnection(xmlConnections, SeqData.curStatus.Connection)
            LoadTableSets()
            'dhdText.FindXmlNode(xmlConnections, "Connection", "DatabasdeName", strConnection)
            'Dim xmlConnNode As xmlnode = xmlConnections.SelectSingleNode("\\Connection", "descendant::Connection[DataBaseName='" & strConnection & "']")
            'dhdConnection.DatabaseName = strConnection
        End If
    End Sub

    Private Sub btnTableSetsReload_Click(sender As Object, e As EventArgs) Handles btnTableSetsReload.Click
        LoadTableSets()
    End Sub

    Private Sub cbxTableSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxTableSet.SelectedIndexChanged
        If cbxTableSet.SelectedIndex >= -1 Then
            SeqData.CurStatus.TableSet = cbxTableSet.SelectedItem
            SeqData.LoadTableSet(xmlTableSets, SeqData.curStatus.TableSet)
            LoadTables()
        End If

    End Sub

    Private Sub btnTablesReload_Click(sender As Object, e As EventArgs) Handles btnTablesReload.Click
        LoadTables()
    End Sub

    Private Sub cbxTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxTable.SelectedIndexChanged
        If cbxTable.SelectedIndex >= -1 Then
            SeqData.CurStatus.Table = cbxTable.SelectedItem
            LoadTable(SeqData.CurStatus.Table)
        End If
    End Sub

    Private Sub Panel2_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles sptFields1.Panel2.Scroll
        sptFields1.Panel1.VerticalScroll.Minimum = sptFields1.Panel2.VerticalScroll.Minimum
        sptFields1.Panel1.VerticalScroll.Maximum = sptFields1.Panel2.VerticalScroll.Maximum

        If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then
            sptFields1.Panel1.AutoScrollPosition = New Point(sptFields1.Panel1.AutoScrollPosition.X, e.NewValue)
            sptFields1.Panel1.VerticalScroll.Value = e.NewValue
            sptFields1.Panel2.AutoScrollPosition = New Point(sptFields1.Panel2.AutoScrollPosition.X, e.NewValue)
            sptFields1.Panel2.VerticalScroll.Value = e.NewValue
        End If
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs)
        Dim strReload As String = sender.name.ToString.Substring(3, sender.name.ToString.Length - 3)
        LoadSearchCriteria(strReload)
    End Sub

    Private Sub btnDefault_Click(sender As Object, e As EventArgs)
        Dim strDefault As String = sender.name.ToString.Substring(3, sender.name.ToString.Length - 3)
        LoadDefaultValue(strDefault)
    End Sub

    Private Sub cbxRelatedField_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim strRelatedField As String = sender.FieldRelatedField
        Dim strSelectedItem As String = sender.SelectedItem.ToString.Substring(0, sender.SelectedItem.ToString.IndexOf("|") - 1)
        For intFieldCount As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intFieldCount).FieldName = strRelatedField Then
                tblTable.Item(intFieldCount).Text = strSelectedItem
            End If
        Next
    End Sub
#End Region

    Private Sub AllClear(intLevel As Integer)
        If intLevel >= 4 Then
            cbxConnection.Items.Clear()
            cbxConnection.Text = ""
        End If
        If intLevel >= 3 Then
            cbxTableSet.Items.Clear()
            cbxTableSet.Text = ""
        End If
        If intLevel >= 2 Then
            cbxTable.Items.Clear()
            cbxTable.Text = ""
        End If
        If intLevel >= 1 Then
            FieldsDispose()
        End If
    End Sub

    Private Sub FieldsDispose()
        SeqData.curStatus.SuspendActions = True

        tblTable.Clear()
        arrLabels.Clear()
        PanelControlsDispose(sptFields1.Panel1)
        PanelControlsDispose(sptFields1.Panel2)

        sptTable1.SplitterDistance = sptTable1.Width - 404
        sptFields1.SplitterDistance = 150
        dgvTable1.Columns.Clear()
        lblListCountNumber.Text = 0
        SeqData.curStatus.SuspendActions = False

        sptFields1.Panel1.VerticalScroll.Minimum = sptFields1.Panel2.VerticalScroll.Minimum
        sptFields1.Panel1.VerticalScroll.Maximum = sptFields1.Panel2.VerticalScroll.Maximum
        sptFields1.Panel1.AutoScrollPosition = New Point(sptFields1.Panel1.AutoScrollPosition.X, 0)
        sptFields1.Panel1.VerticalScroll.Value = 0
        sptFields1.Panel2.AutoScrollPosition = New Point(sptFields1.Panel2.AutoScrollPosition.X, 0)
        sptFields1.Panel2.VerticalScroll.Value = 0
    End Sub

    Private Sub LoadConnections()
        AllClear(4)
        Dim lstConnections As List(Of String) = SeqData.LoadConnectionsXml(xmlConnections)
        If lstConnections Is Nothing Then
            xmlConnections.RemoveAll()
            xmlTableSets.RemoveAll()
            SeqData.curVar.TableSetsFile = ""
            xmlTables.RemoveAll()
            SeqData.curVar.TablesFile = ""
            TableClear()
            SeqData.dhdConnection = SeqData.dhdMainDB
            Exit Sub
        End If
        For Each lstItem As String In lstConnections
            cbxConnection.Items.Add(lstItem)
        Next
        cbxConnection.SelectedItem = SeqData.curStatus.Connection
    End Sub

    Private Sub LoadTableSets()
        AllClear(3)
        Dim lstTableSets As List(Of String) = SeqData.LoadTableSetsXml(xmlTableSets)
        If lstTableSets Is Nothing Then
            xmlTableSets.RemoveAll()
            xmlTables.RemoveAll()
            SeqData.curVar.TablesFile = ""
            TableClear()
            Exit Sub
        End If
        For Each lstItem As String In lstTableSets
            cbxTableSet.Items.Add(lstItem)
        Next
        cbxTableSet.SelectedItem = SeqData.curStatus.TableSet
    End Sub

    Private Sub LoadTables()
        AllClear(2)
        Dim lstTables As List(Of String) = SeqData.LoadTablesXml(xmlTables)
        If lstTables Is Nothing Then
            xmlTables.RemoveAll()
            Exit Sub
        End If
        For Each lstItem As String In lstTables
            cbxTable.Items.Add(lstItem)
        Next
        cbxTable.SelectedItem = SeqData.curStatus.Table
    End Sub

    Friend Sub LoadTable(strTable As String)
        Try
            FieldsDispose()
            Dim xPNode As System.Xml.XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Alias", strTable)

            If Not xPNode Is Nothing Then
                'If SeqData.dhdText.CheckNodeElement(xPNode, "Alias") Then tabTables.TabPages("tpgTable1").Text = xPNode.Item("Alias").InnerText
                If SeqData.dhdText.CheckNodeElement(xPNode, "Name") Then tblTable.TableName = xPNode.Item("Name").InnerText
                If SeqData.dhdText.CheckNodeElement(xPNode, "Alias") Then tblTable.TableAlias = xPNode.Item("Alias").InnerText
                If SeqData.dhdText.CheckNodeElement(xPNode, "Visible") Then tblTable.TableVisible = xPNode.Item("Visible").InnerText
                If SeqData.dhdText.CheckNodeElement(xPNode, "Update") Then tblTable.TableUpdate = xPNode.Item("Update").InnerText
                If SeqData.dhdText.CheckNodeElement(xPNode, "Search") Then tblTable.TableSearch = xPNode.Item("Search").InnerText
                If SeqData.dhdText.CheckNodeElement(xPNode, "Insert") Then tblTable.TableInsert = xPNode.Item("Insert").InnerText
                If SeqData.dhdText.CheckNodeElement(xPNode, "Delete") Then tblTable.TableDelete = xPNode.Item("Delete").InnerText

                If tblTable.TableVisible = True Then
                    Dim xNode As System.Xml.XmlNode
                    For Each xNode In xPNode.SelectNodes(".//Field")
                        Dim fldField As Object = Nothing
                        If xNode.Item("FldSearchList").InnerText = True And xNode.Item("DataType").InnerText.ToUpper <> "BIT" Then
                            'fldField = New ComboField
                            fldField = New ManagedSelectField
                            fldField.FieldCategory = 5
                            'fldField.AutoCompleteMode = AutoCompleteMode.Suggest
                            'fldField.AutoCompleteSource = AutoCompleteSource.ListItems
                        ElseIf xNode.Item("DataType").InnerText.ToUpper = "BIT" Then
                            fldField = New CheckField
                        Else
                            fldField = New TextField
                        End If
                        tblTable.Add(fldField)
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldName") Then fldField.FieldName = xNode.Item("FldName").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldAlias") Then fldField.FieldAlias = xNode.Item("FldAlias").InnerText
                        If fldField.FieldAlias = "" Then fldField.FieldAlias = fldField.FieldName
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldName") Then fldField.Name = tblTable.TableName & "." & xNode.Item("FldName").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "DataType") Then fldField.FieldDataType = xNode.Item("DataType").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "Identity") Then fldField.Identity = xNode.Item("Identity").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "PrimaryKey") Then fldField.PrimaryKey = xNode.Item("PrimaryKey").InnerText

                        fldField.Left = SeqData.curVar.BuildMargin
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldWidth") Then
                            If xNode.Item("FldWidth").InnerText >= sptFields1.Panel2.Width - SeqData.curVar.BuildMargin * 3 Then
                                fldField.Width = sptFields1.Panel2.Width - SeqData.curVar.BuildMargin * 3
                                fldField.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                            Else
                                fldField.Width = xNode.Item("FldWidth").InnerText
                            End If
                        End If

                        If fldField.FieldCategory = 5 Then
                            fldField.DataConn.DataLocation = SeqData.dhdConnection.DataLocation
                            fldField.DataConn.DatabaseName = SeqData.dhdConnection.DatabaseName
                            fldField.DataConn.DataProvider = SeqData.dhdConnection.DataProvider
                            fldField.DataConn.LoginMethod = SeqData.dhdConnection.LoginMethod
                            fldField.DataConn.LoginName = SeqData.dhdConnection.LoginName
                            fldField.DataConn.Password = SeqData.dhdConnection.Password
                            fldField.Table = tblTable.TableName
                            fldField.SearchField = fldField.FieldName
                        End If

                        If SeqData.dhdText.CheckNodeElement(xNode, "Relations") Then
                            For Each xRnode As XmlNode In xNode.Item("Relations").ChildNodes
                                fldField.FieldRelation = xRnode.InnerText
                                If xRnode.Attributes.Count > 0 Then
                                    If xRnode.Attributes(0).Name = "RelatedField" Then fldField.FieldRelatedField = xRnode.Attributes("RelatedField").InnerText
                                    If xRnode.Attributes.Count > 1 Then
                                        If xRnode.Attributes(1).Name = "RelatedFieldList" Then fldField.FieldRelatedFieldList = xRnode.Attributes("RelatedFieldList").InnerText
                                    End If
                                    'If SeqData.dhdText.CheckNodeElement(xRnode, "RelatedField") Then fldField.FieldRelatedField = xRnode.Attributes("RelatedField").InnerText
                                    Exit For
                                End If
                            Next
                        End If
                        'fldField.FieldRelation = xNode.Item("Relations").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "DefaultButton") Then fldField.DefaultButton = xNode.Item("DefaultButton").InnerText
                        If fldField.DefaultButton = True Then
                            Try
                                fldField.DefaultValue = xNode.Item("DefaultButton").Attributes("DefaultValue").Value
                            Catch ex As Exception
                                fldField.DefaultValue = ""
                                SeqData.WriteLog("there was an error setting the value for DefaultValue: " & Environment.NewLine & ex.Message, 1)
                            End Try
                        End If

                        If SeqData.dhdText.CheckNodeElement(xNode, "FldList") Then fldField.FieldList = xNode.Item("FldList").InnerText
                        If fldField.FieldList = True Then
                            Try
                                fldField.FieldListOrder = xNode.Item("FldList").Attributes("Order").Value
                                fldField.FieldListWidth = xNode.Item("FldList").Attributes("Width").Value
                            Catch ex As Exception
                                fldField.FieldListOrder = 0
                                fldField.FieldListWidth = 0
                                SeqData.WriteLog("there was an error setting the value for ListOrder or ListWidth: " & Environment.NewLine & ex.Message, 1)
                            End Try
                        End If
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldSearch") Then fldField.FieldSearch = xNode.Item("FldSearch").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldSearchList") Then fldField.FieldSearchList = xNode.Item("FldSearchList").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldUpdate") Then fldField.FieldUpdate = xNode.Item("FldUpdate").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "FldVisible") Then fldField.FieldVisible = xNode.Item("FldVisible").InnerText

                        If SeqData.dhdText.CheckNodeElement(xNode, "ControlField") Then fldField.ControlField = xNode.Item("ControlField").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "ControlValue") Then fldField.ControlValue = xNode.Item("ControlValue").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "ControlUpdate") Then fldField.ControlUpdate = xNode.Item("ControlUpdate").InnerText
                        If SeqData.dhdText.CheckNodeElement(xNode, "ControlMode") Then fldField.ControlMode = xNode.Item("ControlMode").InnerText

                        If fldField.FieldVisible = True Then
                            sptFields1.Panel2.Controls.Add(fldField)
                            If tblTable.Count = 1 Then
                                fldField.Top = SeqData.curVar.BuildMargin
                            Else
                                fldField.Top = tblTable(tblTable.Count - 2).Top + fldField.Height + SeqData.curVar.BuildMargin
                            End If
                            If fldField.top > sptFields1.Panel2.Height And fldField.Width >= sptFields1.Panel2.Width - (SeqData.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth Then
                                fldField.width = sptFields1.Panel2.Width - (SeqData.CurVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                            End If
                            Dim lblLabel As New Label
                            arrLabels.Add(lblLabel)
                            lblLabel.Name = "lbl" & fldField.Name
                            sptFields1.Panel1.Controls.Add(lblLabel)
                            lblLabel.Text = fldField.FieldAlias
                            lblLabel.Top = fldField.Top + SeqData.curVar.BuildMargin
                            lblLabel.AutoSize = True
                            lblLabel.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                            lblLabel.Left = sptFields1.Panel1.Width - lblLabel.Width - 25 - SeqData.curVar.BuildMargin
                            If lblLabel.Left < 0 Then
                                sptTable1.SplitterDistance += lblLabel.Left
                                sptFields1.SplitterDistance += (lblLabel.Left * -1)
                            End If
                            If fldField.FieldSearchList = True And fldField.FieldDataType.ToString.ToUpper <> "BIT" Then
                                Dim btnButton As New Button
                                btnButton.Name = "btn" & fldField.FieldName
                                sptFields1.Panel1.Controls.Add(btnButton)
                                btnButton.Text = ""
                                btnButton.Image = My.Resources.reload16
                                btnButton.ImageAlign = ContentAlignment.MiddleCenter
                                btnButton.Size = New System.Drawing.Size(25, 23)
                                btnButton.Top = fldField.Top - SeqData.curVar.BuildMargin / 2
                                btnButton.Left = sptFields1.Panel1.Width - btnButton.Width
                                btnButton.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                                btnButton.UseVisualStyleBackColor = True
                                AddHandler btnButton.Click, AddressOf Me.btnReload_Click
                            End If

                            If fldField.DefaultButton = True And Not (fldField.FieldSearchList = True And fldField.FieldDataType.ToString.ToUpper <> "BIT") Then
                                Dim btnButton As New Button
                                btnButton.Name = "btn" & fldField.FieldName
                                sptFields1.Panel1.Controls.Add(btnButton)
                                btnButton.Text = ""
                                btnButton.Image = My.Resources.TSfavicon
                                btnButton.ImageAlign = ContentAlignment.MiddleCenter
                                btnButton.Size = New System.Drawing.Size(25, 23)
                                btnButton.Top = fldField.Top - SeqData.curVar.BuildMargin / 2
                                btnButton.Left = sptFields1.Panel1.Width - btnButton.Width
                                btnButton.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                                btnButton.UseVisualStyleBackColor = True
                                AddHandler btnButton.Click, AddressOf Me.btnDefault_Click
                            End If

                            If fldField.FieldRelatedField.ToString.Length > 0 And fldField.FieldRelation.ToString.Length > 0 Then
                                'Dim objRelatedField As New ComboField
                                Dim msfRelatedField As New ManagedSelectField
                                'AddHandler msfRelatedField.SelectedIndexChanged, AddressOf Me.cbxRelatedField_SelectedIndexChanged
                                tblTable.Add(msfRelatedField)
                                msfRelatedField.Name = fldField.FieldRelation.Substring(0, fldField.FieldRelation.LastIndexOf(".")) & "." & fldField.FieldRelatedField
                                msfRelatedField.FieldName = fldField.FieldRelatedField
                                msfRelatedField.FieldRelatedField = fldField.FieldName
                                msfRelatedField.FieldSearch = fldField.FieldSearch
                                msfRelatedField.FieldUpdate = fldField.FieldUpdate
                                msfRelatedField.FieldVisible = fldField.FieldVisible
                                msfRelatedField.ControlField = fldField.ControlField
                                msfRelatedField.ControlValue = fldField.ControlValue
                                msfRelatedField.ControlUpdate = fldField.ControlUpdate
                                msfRelatedField.ControlMode = fldField.ControlMode

                                msfRelatedField.DataConn.DataLocation = SeqData.dhdConnection.DataLocation
                                msfRelatedField.DataConn.DatabaseName = SeqData.dhdConnection.DatabaseName
                                msfRelatedField.DataConn.DataProvider = SeqData.dhdConnection.DataProvider
                                msfRelatedField.DataConn.LoginMethod = SeqData.dhdConnection.LoginMethod
                                msfRelatedField.DataConn.LoginName = SeqData.dhdConnection.LoginName
                                msfRelatedField.DataConn.Password = SeqData.dhdConnection.Password
                                msfRelatedField.Table = fldField.FieldRelation.Substring(0, fldField.FieldRelation.LastIndexOf("."))
                                msfRelatedField.SearchField = fldField.FieldRelatedField
                                msfRelatedField.IdentifierField = fldField.FieldRelation.Substring(fldField.FieldRelation.LastIndexOf(".") + 1, fldField.FieldRelation.length - (fldField.FieldRelation.LastIndexOf(".") + 1))

                                sptFields1.Panel2.Controls.Add(msfRelatedField)
                                msfRelatedField.Top = fldField.Top
                                msfRelatedField.Left = fldField.Left + fldField.Width + SeqData.curVar.BuildMargin
                                If fldField.top > sptFields1.Panel2.Height Then
                                    msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (SeqData.CurVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                                Else
                                    msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (SeqData.CurVar.BuildMargin * 3)
                                End If

                                msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (SeqData.CurVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                                msfRelatedField.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                                msfRelatedField.Visible = True
                                If SeqData.dhdConnection.DataBaseOnline = True Then msfRelatedField.RunSearch()
                            End If
                        End If
                        FieldEnableHandler(fldField, fldField.FieldSearch)

                        If fldField.FieldList = True Or fldField.Identity = True Or fldField.PrimaryKey = True Then
                            Dim dgvColumn As New DataGridViewTextBoxColumn
                            dgvColumn.Name = fldField.FieldName
                            dgvColumn.HeaderText = fldField.FieldAlias
                            Select Case fldField.FieldDataType.ToUpper
                                Case "BIT"
                                    dgvColumn.ValueType = GetType(Boolean)
                                Case "INTEGER"
                                    dgvColumn.ValueType = GetType(Integer)
                                Case Else
                                    dgvColumn.ValueType = GetType(String)
                            End Select
                            dgvColumn.Width = fldField.FieldListWidth
                            dgvTable1.Columns.Add(dgvColumn)
                            If fldField.FieldList = True Then
                                dgvColumn.Visible = True
                            Else
                                dgvColumn.Visible = False
                            End If
                            If fldField.Identity = True Or fldField.PrimaryKey = True Then
                                dgvColumn.Tag = "Identity"
                            Else
                                dgvColumn.Tag = ""
                            End If
                        End If

                    Next
                End If
            End If
            FieldsClear()
            SetWidth()
            SortColumns()
            'LoadSearchCriteria()
            'LoadRelatedSearchCriteria()
            SearchListLoad(tblTable.TableName)
            If SeqData.curStatus.Status > 3 Then
                SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
            Else
                SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search
            End If
            ButtonHandle()
        Catch ex As Exception
            MessageBox.Show("There was an error reading the Table file. Please check the file for Table: " & Environment.NewLine & strTable & Environment.NewLine & ex.Message)
            SeqData.WriteLog("There was an error reading the Table file. Please check the file for Table: " & Environment.NewLine & strTable & Environment.NewLine & ex.Message, 1)
        End Try

    End Sub

    Private Sub SortColumns()
        Dim intColumns As Integer = dgvTable1.Columns.Count
        Dim intColumn As Integer = 0
        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).FieldList = True Or tblTable.Item(intField).Identity = True Or tblTable.Item(intField).PrimaryKey = True Then
                For Each column In dgvTable1.Columns
                    If column.name = tblTable.Item(intField).FieldName Then
                        Dim intColumnIndex As Integer = tblTable.Item(intField).FieldListOrder
                        If intColumnIndex = 0 Then intColumnIndex = intColumns
                        intColumn = intColumnIndex
                        If intColumn > intColumns Then intColumn = intColumns - 1
                        column.DisplayIndex = intColumn - 1
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub SetWidth()
        For Each ctrField In sptFields1.Panel2.Controls
            If ctrField.Left + ctrField.Width > sptFields1.Panel2.Width - (SeqData.CurVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth Then
                ctrField.Width = sptFields1.Panel2.Width - ctrField.Left - (SeqData.CurVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
            End If
        Next
    End Sub
#End Region

#Region "Data Load"

#Region "Controls"
    Private Sub dgvTable1_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTable1.SelectionChanged
        ItemSelect()
    End Sub

    Private Sub btnLoadList_Click(sender As Object, e As EventArgs) Handles btnLoadList.Click
        CursorControl("Wait")
        FieldsClearAll(True)
        ColorReset()
        LoadList(False)
        CursorControl()
    End Sub

    Private Sub btnLoadSearchCriteria_Click(sender As Object, e As EventArgs) Handles btnLoadSearchCriteria.Click
        CursorControl("Wait")
        LoadSearchCriteria()
        CursorControl()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CursorControl("Wait")
        LoadList(True)
        CursorControl()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        CursorControl("Wait")
        FieldsClearAll(True)
        CursorControl()
    End Sub

    Private Sub btnExportList_Click(sender As Object, e As EventArgs) Handles btnExportList.Click
        If SeqData.dhdText.DatasetCheck(dtsTable) = False Then Exit Sub
        Dim strReportName As String = ""
        If cbxSearch.Text.Length > 0 Then
            strReportName = cbxSearch.Text
        ElseIf cbxTable.Text.Length > 0 Then
            strReportName = cbxTable.Text
        Else
            strReportName = SeqData.curStatus.Table
        End If
        Dim strFileName As String = GetSaveFileName(strReportName)
        ExportFile(dtsTable, strFileName)
        'XmlExportDatagridView(dgvTable1, "Sequenchel", SeqData.CurStatus.Table, SeqData.CurStatus.Table & "-Item")
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If btnAdd.Text = "New Item" Then
            MessageBox.Show("Your current selection (if any) is duplicated." & Environment.NewLine & _
                               "You can edit the data before you save it to the database" & Environment.NewLine & _
                               "Click 'Clear' to cancel the operation" & Environment.NewLine & _
                               "Click 'Save' to save the new item to the database" _
                               , "How it works", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If SeqData.CurStatus.Status > 3 Then
                SeqData.CurStatus.Status = SeqCore.CurrentStatus.StatusList.ControlAdd
            Else
                SeqData.CurStatus.Status = SeqCore.CurrentStatus.StatusList.Add
            End If
            FieldsClear(True)
            FieldsEnable(False)

            SeqData.curStatus.SuspendActions = True
            dgvTable1.ClearSelection()
            dgvTable1.CurrentCell = Nothing
            SeqData.curStatus.SuspendActions = False

            ButtonHandle()
            TagsClear()
            ColorSet()
            btnAdd.Text = "Save Item"
            btnAdd.BackColor = clrMarked
        ElseIf btnAdd.Text = "Save Item" Then
            btnAdd.Text = "New Item"
            btnAdd.BackColor = clrControl
            If SeqData.curStatus.Status > 3 Then
                SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
            Else
                SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
            End If
            ItemAdd()
            FieldsEnable()
            ButtonHandle()
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ItemUpdate()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvTable1.RowCount = 0 Then Exit Sub
        If dgvTable1.SelectedRows.Count <> 1 Then
            WriteStatus("You need to select 1 item from the list to use this function", 0, lblStatusText)
            Exit Sub
        End If
        If MessageBox.Show("This will permanently delete the selected Item from the database." & Environment.NewLine & Core.Message.strContinue, Core.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            WriteStatus("Delete cancelled", 5, lblStatusText)
            Exit Sub
        End If
        ItemDelete()
    End Sub

    Private Sub btnSearchAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnSearchAddOrUpdate.Click
        If tblTable.TableName.Length < 1 Then Exit Sub
        If cbxSearch.Text.Length < 1 Then
            WriteStatus("The Name of the Search must be at least 1 character long", 0, lblStatusText)
            Exit Sub
        End If
        SearchDelete(True)
        SearchAdd()
        If SaveXmlFile(xmlSearch, SeqData.curVar.SearchFile, True) = False Then
            MessageBox.Show("The file " & SeqData.curVar.SearchFile & " was not saved.")
        Else
            cbxSearch.Items.Add(cbxSearch.Text)
            WriteStatus("Search saved", 0, lblStatusText)
        End If

        'SearchListLoad(tblTable.TableName)
    End Sub

    Private Sub btnDeleteSearch_Click(sender As Object, e As EventArgs) Handles btnDeleteSearch.Click
        If tblTable.TableName.Length < 1 Then Exit Sub
        If cbxSearch.Text.Length < 1 Then
            MessageBox.Show("The Name of the Search must be at least 1 character long")
            Exit Sub
        End If
        SearchDelete(False)
        If SaveXmlFile(xmlSearch, SeqData.curVar.SearchFile, True) = False Then
            MessageBox.Show("The file " & SeqData.curVar.SearchFile & " was not saved.")
        End If
        SearchListLoad(tblTable.TableName)
        btnClear_Click(Nothing, Nothing)
        cbxSearch.SelectedIndex = -1
        cbxSearch.Text = ""
        WriteStatus("Search deleted", 0, lblStatusText)
    End Sub

    Private Sub cbxSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSearch.SelectedIndexChanged
        If SeqData.CurStatus.SuspendActions = False Then
            CursorControl("Wait")
            FieldsClearAll()
            SearchLoad()
            LoadList(True)
            CursorControl()
        End If
    End Sub

#End Region

    Private Sub ButtonHandle()
        btnSearch.Enabled = False
        btnUpdate.Enabled = False
        btnAdd.Enabled = False
        btnDelete.Enabled = False

        If SeqData.dhdConnection.DataBaseOnline = True Then
            If SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search And tblTable.TableSearch = True Then
                btnSearch.Enabled = True
            End If
            If dgvTable1.SelectedRows.Count = 1 And SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit And tblTable.TableUpdate = True And (SeqData.curVar.AllowUpdate = True Or SeqData.curVar.SecurityOverride = True) Then
                btnUpdate.Enabled = True
            End If
            If tblTable.TableInsert = True And (SeqData.curVar.AllowInsert = True Or SeqData.curVar.SecurityOverride = True) Then
                btnAdd.Enabled = True
            End If
            If SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch Then
                btnSearch.Enabled = True
            End If
            If dgvTable1.SelectedRows.Count = 1 And SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit And (SeqData.curVar.AllowUpdate = True Or SeqData.curVar.SecurityOverride = True) Then
                btnUpdate.Enabled = True
            End If
            If dgvTable1.SelectedRows.Count = 1 And tblTable.TableDelete = True And (SeqData.curVar.AllowDelete = True Or SeqData.curVar.SecurityOverride = True) Then
                btnDelete.Enabled = True
            End If
        End If
    End Sub

    Private Sub FieldsClearAll(Optional ClearSearch As Boolean = False)
        If ClearSearch = True Then
            SeqData.curStatus.SuspendActions = True
            cbxSearch.SelectedIndex = -1
            SeqData.curStatus.SuspendActions = False
        End If
        dgvTable1.ClearSelection()
        FieldsClear()
        If SeqData.curStatus.Status > 3 Then
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
        Else
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search
        End If
        FieldsEnable()
        'ColorReset()
        ButtonHandle()
    End Sub

    Private Sub FieldsDisable()
        For intField As Integer = 0 To tblTable.Count - 1
            FieldEnableHandler(tblTable.Item(intField), False)
        Next
    End Sub

    Private Sub FieldsEnable(Optional blnEnableIdentity As Boolean = True)

        Select Case SeqData.curStatus.Status
            Case SeqCore.CurrentStatus.StatusList.Search, SeqCore.CurrentStatus.StatusList.ControlSearch
                For intField As Integer = 0 To tblTable.Count - 1
                    FieldEnableHandler(tblTable.Item(intField), tblTable.Item(intField).FieldSearch)
                Next
            Case SeqCore.CurrentStatus.StatusList.Edit
                FieldsDisable()
                If tblTable.TableUpdate = True Then
                    For intField As Integer = 0 To tblTable.Count - 1
                        Dim strControl As String = tblTable.Item(intField).ControlField
                        Dim strControlValue As String = tblTable.Item(intField).ControlValue
                        If strControl.Length > 0 Then
                            'MessageBox.Show(tblTable.Item(intField).FieldName)
                            For intField2 As Integer = 0 To tblTable.Count - 1
                                If tblTable.Item(intField2).FieldName = strControl Then
                                    Select Case tblTable.Item(intField2).FieldCategory
                                        Case 1, 3, 4, 5, 6
                                            If tblTable.Item(intField2).Text = strControlValue Then
                                                FieldEnableHandler(tblTable.Item(intField), tblTable.Item(intField).ControlUpdate)
                                            End If
                                        Case 2
                                            If tblTable.Item(intField2).Checked = strControlValue Then
                                                FieldEnableHandler(tblTable.Item(intField), tblTable.Item(intField).ControlUpdate)
                                            End If
                                    End Select

                                    Exit For
                                End If
                            Next
                        Else
                            FieldEnableHandler(tblTable.Item(intField), tblTable.Item(intField).FieldUpdate)
                        End If
                    Next
                End If
            Case SeqCore.CurrentStatus.StatusList.Add, SeqCore.CurrentStatus.StatusList.ControlAdd
                For intField As Integer = 0 To tblTable.Count - 1
                    If tblTable.Item(intField).Identity = False Or blnEnableIdentity = True Then
                        FieldEnableHandler(tblTable.Item(intField), tblTable.Item(intField).FieldSearch)
                    End If
                Next
            Case SeqCore.CurrentStatus.StatusList.ControlEdit
                For intField As Integer = 0 To tblTable.Count - 1
                    FieldEnableHandler(tblTable.Item(intField), tblTable.Item(intField).ControlMode)
                Next
        End Select
        ColorReset()
    End Sub

    Private Sub FieldsClear(Optional blnIdentityOnly As Boolean = False)
        For intField As Integer = tblTable.Count - 1 To 0 Step -1
            Select Case tblTable.Item(intField).FieldCategory
                Case 1, 3, 4
                    If blnIdentityOnly = False Or tblTable.Item(intField).Identity = True Or tblTable.Item(intField).PrimaryKey = True Then
                        tblTable.Item(intField).Text = ""
                    End If
                Case 2
                    tblTable.Item(intField).Checked = False
                Case 5, 6
                    If blnIdentityOnly = False Or tblTable.Item(intField).Identity = True Or tblTable.Item(intField).PrimaryKey = True Then
                        tblTable.Item(intField).Text = ""
                        tblTable.Item(intField).DropDown(2)
                    End If
            End Select
        Next
        btnAdd.Text = "New Item"
        btnAdd.BackColor = clrControl
        lblMultipleRows.Visible = False
        TagsClear()
    End Sub

    Private Sub TagsClear()
        For intField As Integer = 0 To tblTable.Count - 1
            Select Case tblTable.Item(intField).FieldCategory
                Case 1, 3, 4, 5, 6
                    tblTable.Item(intField).Tag = ""
                Case 2
                    tblTable.Item(intField).Tag = False
            End Select
        Next
    End Sub

    Friend Sub LoadList(blnRefine As Boolean)
        If SeqData.dhdConnection.DataBaseOnline = False Then Exit Sub
        If tblTable.TableName.Length < 1 Then Exit Sub
        SeqData.curStatus.SuspendActions = True
        Dim strQuerySelect As String = SelectBuild()
        Dim strQueryOrder As String = OrderBuild()

        dgvTable1.Rows.Clear()
        lblListCountNumber.Text = 0

        strQuery = strQuerySelect
        strQuery &= " FROM [" & tblTable.TableName.Replace(".", "].[") & "] "
        strQuery &= " WHERE 1=1 "

        If blnRefine = True Then
            For intField As Integer = 0 To tblTable.Count - 1
                If tblTable.Item(intField).BackColor = clrMarked Then
                    Dim strWhere As String = Nothing
                    Dim strValue As String = Nothing
                    Select Case tblTable.Item(intField).FieldDataType.ToUpper
                        Case "BIT"
                            strValue = tblTable.Item(intField).CheckState
                        Case Else
                            strValue = tblTable.Item(intField).Text
                    End Select
                    strQuery &= " AND " & FormatFieldWhere1(tblTable.Item(intField).FieldName, tblTable.TableName, tblTable.Item(intField).Width, tblTable.Item(intField).FieldDataType, strValue)
                End If
            Next
        End If

        If strQueryOrder.Length > 0 Then strQuery = strQuery & " " & strQueryOrder

        dtsTable = Nothing
        dtsTable = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)
        If SeqData.dhdText.DatasetCheck(dtsTable) = False Then Exit Sub
        If DataSet2DataGridView(dtsTable, 0, dgvTable1, False) = False Then Exit Sub

        dgvTable1.ClearSelection()
        lblListCountNumber.Text = dgvTable1.RowCount - 1
        SeqData.curStatus.SuspendActions = False
    End Sub

    Private Function SelectBuild() As String
        Dim strMode As String = "SELECT "
        'Dim strAddField As String = ""
        strQuery = strMode & " "
        For intOrder As Integer = 0 To dgvTable1.Columns.Count
            For Each column In dgvTable1.Columns
                Dim strColumn As String = column.Name
                If intOrder = column.displayindex Then
                    For intField As Integer = 0 To tblTable.Count - 1
                        If tblTable.Item(intField).FieldName = column.Name Then
                            strQuery &= ", " & SeqData.FormatField(tblTable.Item(intField).FieldName, tblTable.TableName, tblTable.Item(intField).Width, tblTable.Item(intField).FieldDataType, tblTable.Item(intField).FieldName, Nothing, True, True, SeqData.curVar.DateTimeStyle)
                            Exit For
                        End If
                    Next
                End If
            Next
        Next
        strQuery = strQuery.Replace(strMode & " ,", strMode & " ")
        If chkUseTop.Checked = True Then strQuery = strQuery.Replace(strMode, strMode & " TOP " & txtUseTop.Text & " ")

        Return strQuery
    End Function

    Private Function OrderBuild() As String
        Dim strMode As String = "ORDER BY "
        Dim intColumnCount As Integer = 0
        strQuery = strMode & " "
        For intOrder As Integer = 0 To dgvTable1.Columns.Count
            If intColumnCount >= SeqData.curVar.MaxColumnSort Then Exit For

            For Each column In dgvTable1.Columns
                If intOrder = column.displayindex And column.Visible = True Then
                    For intField As Integer = 0 To tblTable.Count - 1
                        If tblTable.Item(intField).FieldName = column.Name Then
                            Select Case tblTable.Item(intField).FieldDataType
                                Case "BINARY", "XML", "GEO", "TEXT", "IMAGE"
                                    'No sort order
                                Case Else
                                    strQuery &= ", " & SeqData.FormatField(tblTable.Item(intField).FieldName, tblTable.TableName, tblTable.Item(intField).Width, tblTable.Item(intField).FieldDataType, Nothing, Nothing, False, False, SeqData.curVar.DateTimeStyle)
                                    If chkReversedSortOrder.Checked = True Then
                                        strQuery &= " DESC "
                                    End If
                            End Select
                            intColumnCount += 1
                        End If
                    Next
                End If
            Next
        Next
        strQuery = strQuery.Replace(strMode & " ,", strMode & " ")
        Return strQuery
    End Function

    Private Sub ItemSelect()
        If SeqData.curStatus.SuspendActions = False Then
            FieldsClear()
            If dgvTable1.SelectedRows.Count = 1 Then
                SelectedItem = dgvTable1.SelectedRows(0)
                LoadItem(SelectedItem)
            End If
            ButtonHandle()
        End If
    End Sub

    Private Sub LoadItem(dgrSelection As DataGridViewRow)
        Dim strQueryWhere As String = " WHERE 1=1 "
        Dim strQueryWhere2 As String = ""
        Dim strQueryFrom As String = " FROM [" & tblTable.TableName.Replace(".", "].[") & "] "
        'strQuery = "SELECT TOP 1 ,"
        strQuery = "SELECT ,"
        For intField As Integer = 0 To tblTable.Count - 1
            'strQuery &= ",COALESCE([" & tblTable.Item(intField).FieldName & "],'') AS [" & tblTable.Item(intField).FieldName & "]"
            If tblTable.Item(intField).Name.Substring(0, tblTable.Item(intField).Name.LastIndexOf(".")) = tblTable.TableName Then

                strQuery &= ", " & SeqData.FormatField(tblTable.Item(intField).FieldName, tblTable.TableName, tblTable.Item(intField).Width, tblTable.Item(intField).FieldDataType, tblTable.Item(intField).FieldName, Nothing, True, True, SeqData.curVar.DateTimeStyle)

                If tblTable.Item(intField).FieldRelatedField.Length > 0 Then
                    Dim strRelation As String = tblTable.Item(intField).FieldRelation
                    strQuery &= ",[" & tblTable.Item(intField).FieldRelation.Substring(0, tblTable.Item(intField).FieldRelation.LastIndexOf(".")).Replace(".", "].[") & "].[" & tblTable.Item(intField).FieldRelatedField & "] AS [" & tblTable.Item(intField).FieldRelatedField & "]"
                    strQueryFrom &= " LEFT OUTER JOIN " & tblTable.Item(intField).FieldRelation.Substring(0, tblTable.Item(intField).FieldRelation.LastIndexOf(".")) & " ON " & "[" & tblTable.TableName.Replace(".", "].[") & "]." & tblTable.Item(intField).FieldName & " = " & tblTable.Item(intField).FieldRelation.Substring(0, tblTable.Item(intField).FieldRelation.LastIndexOf(".")) & "." & tblTable.Item(intField).FieldRelation.Substring(tblTable.Item(intField).FieldRelation.LastIndexOf(".") + 1, tblTable.Item(intField).FieldRelation.Length - tblTable.Item(intField).FieldRelation.LastIndexOf(".") - 1)
                End If

                For Each cell In dgrSelection.Cells
                    If cell.OwningColumn.Name = tblTable.Item(intField).FieldName Then
                        If Not cell.Value Is Nothing Then
                            If tblTable.Item(intField).Identity = True Or tblTable.Item(intField).PrimaryKey = True Then
                                'strQueryWhere &= " AND [" & tblTable.TableName.Replace(".", "].[") & "].[" & tblTable.Item(intField).FieldName & "] = " & SetDelimiters(cell.Value.ToString, tblTable.Item(intField).FieldDataType, "=")
                                strQueryWhere &= " AND " & SeqData.FormatField(tblTable.Item(intField).FieldName, tblTable.TableName, tblTable.Item(intField).Width, tblTable.Item(intField).FieldDataType, Nothing, Nothing, False, False, SeqData.curVar.DateTimeStyle) & " = " & SeqData.SetDelimiters(cell.Value.ToString, tblTable.Item(intField).FieldDataType, "=")

                            End If
                            'strQueryWhere2 &= " AND [" & tblTable.TableName.Replace(".", "].[") & "].[" & tblTable.Item(intField).FieldName & "] = " & SetDelimiters(cell.Value.ToString, tblTable.Item(intField).FieldDataType, "=")
                            strQueryWhere2 &= " AND " & SeqData.FormatField(tblTable.Item(intField).FieldName, tblTable.TableName, tblTable.Item(intField).Width, tblTable.Item(intField).FieldDataType, Nothing, Nothing, False, False, SeqData.curVar.DateTimeStyle) & " = " & SeqData.SetDelimiters(cell.Value.ToString, tblTable.Item(intField).FieldDataType, "=")
                        End If
                    End If
                Next
            End If
        Next
        strQuery &= strQueryFrom
        If strQueryWhere = " WHERE 1=1 " Then strQueryWhere &= strQueryWhere2
        If strQueryWhere = " WHERE 1=1 " Then Exit Sub
        strQuery &= strQueryWhere
        strQuery = strQuery.Replace(",,", " ")
        'If SeqData.CurVar.DebugMode Then MessageBox.Show(strQuery)

        Dim objData As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)
        If objData Is Nothing Then Exit Sub
        If objData.Tables.Count = 0 Then Exit Sub
        If objData.Tables(0).Rows.Count = 0 Then Exit Sub
        SeqData.curStatus.SuspendActions = True
        Try
            'For intRowCount1 As Integer = 0 To objData.Tables(0).Rows.Count - 1
            'If objData.Tables.Item(0).Rows(intRowCount1).Item(0).GetType().ToString = "System.DBNull" Then
            'MessageBox.Show("Cell Must be empty")
            'Else
            'strReportText &= Environment.NewLine
            For intField As Integer = 0 To tblTable.Count - 1
                If objData.Tables.Item(0).Rows(0).Item(tblTable.Item(intField).FieldName).GetType().ToString = "System.DBNull" Then
                    tblTable.Item(intField).BackColor = clrEmpty
                    tblTable.Item(intField).Tag = ""
                Else
                    Select Case tblTable.Item(intField).FieldCategory
                        Case 1, 3, 4
                            tblTable.Item(intField).Text = objData.Tables.Item(0).Rows(0).Item(tblTable.Item(intField).FieldName)
                        Case 2
                            tblTable.Item(intField).Checked = objData.Tables.Item(0).Rows(0).Item(tblTable.Item(intField).FieldName)
                        Case 5, 6
                            tblTable.Item(intField).Text = objData.Tables.Item(0).Rows(0).Item(tblTable.Item(intField).FieldName)
                            tblTable.Item(intField).DropDown(2)
                    End Select
                    tblTable.Item(intField).Tag = objData.Tables.Item(0).Rows(0).Item(tblTable.Item(intField).FieldName).ToString
                End If
            Next
            'End If
            'Next
            If objData.Tables(0).Rows.Count > 1 Then lblMultipleRows.Visible = True

        Catch ex As Exception
            MessageBox.Show("There was an error loading the data." & Environment.NewLine & ex.Message)
            SeqData.WriteLog("There was an error loading the data." & Environment.NewLine & ex.Message, 1)

            SeqData.curStatus.SuspendActions = False
        End Try
        SeqData.curStatus.SuspendActions = False
        If SeqData.curStatus.Status > 3 Then
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
        Else
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
        End If
        'ButtonHandle()
        FieldsEnable()
    End Sub

    Private Sub LoadSearchCriteria(Optional strCriterium As String = "")
        If SeqData.dhdConnection.DataBaseOnline = True Then
            Dim strQuery2 As String = " WHERE 1=1 ", strQuery3 As String = ""
            For intField As Integer = 0 To tblTable.Count - 1
                strQuery = ""
                strQuery2 = " WHERE 1=1 "
                strQuery3 = ""
                If tblTable.Item(intField).FieldSearchList = True Then
                    'If tblTable.Item(intField).FieldName = strCriterium Or strCriterium = "" Then
                    '    strQuery = "SELECT DISTINCT " & " "
                    '    If SeqData.CurVar.LimitLookupLists = True Then strQuery &= "TOP " & SeqData.CurVar.LimitLookupListsCount & " "
                    '    strQuery &= "COALESCE([" & tblTable.Item(intField).FieldName & "],'') AS [" & tblTable.Item(intField).FieldName & "]"
                    '    strQuery &= " FROM [" & tblTable.TableName.Replace(".", "].[") & "] "
                    '    strQuery3 = " ORDER BY [" & tblTable.Item(intField).FieldName & "] "
                    'End If

                    'If strCriterium.Length > 0 And strQuery.Length > 0 Then
                    '    For intField2 As Integer = 0 To tblTable.Count - 1
                    '        If tblTable.Item(intField2).BackColor = clrMarked And tblTable.Item(intField2).FieldName <> strCriterium Then
                    '            strQuery2 &= " AND [" & tblTable.Item(intField2).FieldName & "] = '" & tblTable.Item(intField2).Text & "'"
                    '        End If
                    '    Next
                    'End If
                    'If tblTable.Item(intField).FieldName = strCriterium Or strCriterium = "" Then
                    '    Try
                    '        strQuery = strQuery & strQuery2 & strQuery3
                    '        Dim objData As DataSet = QueryDb(dhdConnection, strQuery, True)
                    '        If objData Is Nothing Then Exit Sub
                    '        If objData.Tables.Count = 0 Then Exit Sub
                    '        If objData.Tables(0).Rows.Count = 0 Then Exit Sub
                    '        tblTable.Item(intField).Items.Clear()
                    '        For intRowCount1 As Integer = 0 To objData.Tables(0).Rows.Count - 1
                    '            'If objData.Tables.Item(0).Rows(intRowCount1).Item(0).GetType().ToString = "System.DBNull" Then
                    '            'MessageBox.Show("Cell Must be empty")
                    '            'Else
                    '            tblTable.Item(intField).Items.Add(objData.Tables.Item(0).Rows(intRowCount1).Item(tblTable.Item(intField).FieldName))
                    '            'End If
                    '        Next
                    '    Catch ex As Exception
                    '        WriteLog(ex.Message, 1)
                    '    End Try
                    'End If
                    tblTable.Item(intField).RunSearch()
                End If
            Next
        End If
    End Sub

    'Private Sub LoadRelatedSearchCriteria(Optional strCriterium As String = "", Optional blnRefine As Boolean = False)
    '    If dhdConnection.DataBaseOnline = True Then
    '        Dim strQuery2 As String = " WHERE 1=1 ", strQuery3 As String = "", strQueryFrom As String = ""
    '        For intField As Integer = 0 To tblTable.Count - 1
    '            strQuery = ""
    '            strQuery2 = " WHERE 1=1 "
    '            strQuery3 = ""
    '            strQueryFrom = ""
    '            'If tblTable.Item(intField).Fieldcategory = 5 Then
    '            '    MessageBox.Show(tblTable.Item(intField).Name)
    '            'End If
    '            If tblTable.TableName <> tblTable.Item(intField).Name.Substring(0, tblTable.Item(intField).Name.LastIndexOf(".")) And tblTable.Item(intField).Fieldcategory = 4 Then
    '                strQuery = "SELECT DISTINCT TOP 100 "
    '                strQuery &= "[" & tblTable.Item(intField).Name.Substring(0, tblTable.Item(intField).Name.LastIndexOf(".")).Replace(".", "].[") & "].[" & tblTable.Item(intField).FieldRelatedField & "]"
    '                strQuery &= ",[" & tblTable.Item(intField).Name.Replace(".", "].[") & "]"
    '                strQuery &= " FROM [" & tblTable.Item(intField).Name.Substring(0, tblTable.Item(intField).Name.LastIndexOf(".")).Replace(".", "].[") & "] "
    '                strQuery3 &= " ORDER BY [" & tblTable.Item(intField).Name.Replace(".", "].[") & "] "
    '                strQuery &= " "


    '                Try
    '                    strQuery = strQuery & strQuery2 & strQuery3
    '                    Dim objData As DataSet = QueryDb(dhdConnection, strQuery, True)
    '                    If objData Is Nothing Then Exit Sub
    '                    If objData.Tables.Count = 0 Then Exit Sub
    '                    If objData.Tables(0).Rows.Count = 0 Then Exit Sub

    '                    'tblTable.Item(intField).Items.Clear()
    '                    For intRowCount1 As Integer = 0 To objData.Tables(0).Rows.Count - 1
    '                        tblTable.Item(intField).Items.Add(objData.Tables.Item(0).Rows(intRowCount1).Item(0) & " | " & objData.Tables.Item(0).Rows(intRowCount1).Item(1))
    '                    Next

    '                    'ComboFieldMultiColumnCreate(tblTable.Item(intField), objData)
    '                Catch ex As Exception
    '                    WriteLog(ex.Message, 1)
    '                End Try

    '            End If

    '            'If strRelation = strCriterium And blnRefine = True Then
    '            ' name = tblTable.Item(intField).FieldRelation.Substring(0, tblTable.Item(intField).FieldRelation.LastIndexOf(".")) & "." & tblTable.Item(intField).FieldRelatedField
    '            'fieldname = fldField.FieldRelatedField
    '            'End If
    '            '    If strCriterium.Length > 0 And strQuery.Length > 0 Then
    '            '        For intField2 As Integer = 0 To tblTable.Count - 1
    '            '            If tblTable.Item(intField2).BackColor = clrMarked And tblTable.Item(intField2).FieldName <> strCriterium Then
    '            '                strQuery2 &= " AND [" & tblTable.Item(intField2).FieldName & "] = '" & tblTable.Item(intField2).Text & "'"
    '            '            End If
    '            '        Next
    '            '    End If
    '            '    If tblTable.Item(intField).FieldName = strCriterium Or strCriterium = "" Then
    '            '    End If
    '            'End If
    '        Next
    '    End If
    'End Sub

    'Private Sub cbxRelatedItems_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
    '    'Handles cbxDateFormats.DrawItem
    '    ' Draw the default background
    '    e.DrawBackground()

    '    ' The ComboBox is bound to a DataTable,
    '    ' so the items are DataRowView objects.
    '    Dim drv As DataRowView = CType(sender.Items(e.Index), DataRowView)

    '    ' Retrieve the value of each column.
    '    Dim id As Integer = drv("ID").ToString()
    '    Dim name As String = drv("Name").ToString()

    '    ' Get the bounds for the first column
    '    Dim r1 As Rectangle = e.Bounds
    '    r1.Width = r1.Width / (100 / 7)

    '    ' Draw the text on the first column
    '    Using sb As SolidBrush = New SolidBrush(e.ForeColor)
    '        e.Graphics.DrawString(id, e.Font, sb, r1)
    '    End Using

    '    ' Draw a line to isolate the columns 
    '    Using p As Pen = New Pen(Color.Black)
    '        e.Graphics.DrawLine(p, r1.Right, 0, r1.Right, r1.Bottom)
    '    End Using

    '    ' Get the bounds for the second column
    '    Dim r2 As Rectangle = e.Bounds
    '    r2.X = e.Bounds.Width / (100 / 7)
    '    r2.Width = r2.Width / (100 / 35)

    '    ' Draw the text on the second column
    '    Using sb As SolidBrush = New SolidBrush(e.ForeColor)
    '        e.Graphics.DrawString(name, e.Font, sb, r2)
    '    End Using

    '    ' Draw a line to isolate the columns 
    '    Using p As Pen = New Pen(Color.Black)
    '        e.Graphics.DrawLine(p, r2.Right, 0, r2.Right, r2.Bottom)
    '    End Using

    '    ' Get the bounds for the third column
    '    Dim r3 As Rectangle = e.Bounds
    '    r3.X = e.Bounds.Width / (100 / 42)
    '    r3.Width = r3.Width / (100 / 58)

    '    ' Draw the text on the second column
    '    Using sb As SolidBrush = New SolidBrush(e.ForeColor)
    '        e.Graphics.DrawString(format, e.Font, sb, r3)
    '    End Using

    'End Sub

    Private Sub LoadDefaultValue(strFieldName As String)
        For Each Field In tblTable
            If Field.FieldName = strFieldName Then
                Dim strValue As String = SeqData.ProcessDefaultValue(Field.DefaultValue)
                Select Case Field.FieldDataType.ToUpper
                    Case "BIT"
                        Field.Checked = strValue
                    Case Else
                        Field.Text = strValue
                End Select
            End If
        Next
    End Sub

    Private Sub ColorReset()
        For intField As Integer = 0 To tblTable.Count - 1
            If FieldEnabledCheck(tblTable.Item(intField)) = True Then
                If tblTable.Item(intField).FieldDataType = "BIT" Then
                    tblTable.Item(intField).BackColor = clrControl
                Else
                    tblTable.Item(intField).BackColor = clrOriginal
                End If
            Else
                tblTable.Item(intField).BackColor = clrDisabled
            End If
        Next

    End Sub

    Private Sub ColorSet()
        For intField As Integer = 0 To tblTable.Count - 1
            If FieldEnabledCheck(tblTable.Item(intField)) = True Then
                FieldTextHandler(tblTable.Item(intField))
            Else
                tblTable.Item(intField).text = ""
            End If
        Next
    End Sub

    Private Sub ItemAdd()
        strQuery = ""
        Dim strQuery1 As String = "", strQuery2 As String = ""
        strQuery = " INSERT INTO [" & tblTable.TableName.Replace(".", "].[") & "] ("

        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).BackColor = clrMarked And FieldEnabledCheck(tblTable.Item(intField)) = True Then
                Select Case tblTable.Item(intField).FieldDataType.ToUpper
                    Case "BIT"
                        strQuery1 &= ", [" & tblTable.Item(intField).FieldName & "]"
                        strQuery2 &= ", " & tblTable.Item(intField).CheckState
                    Case "INTEGER"
                        If Not (tblTable.Item(intField).Text = "NULL" Or tblTable.Item(intField).Text = "") Then
                            strQuery1 &= ", [" & tblTable.Item(intField).FieldName & "]"
                            strQuery2 &= ", " & tblTable.Item(intField).Text
                        End If
                    Case "BINARY", "IMAGE", "TIMESTAMP"
                        'do nothing
                    Case Else
                        If Not (tblTable.Item(intField).Text = "NULL" Or tblTable.Item(intField).Text = "") Then
                            strQuery1 &= ", [" & tblTable.Item(intField).FieldName & "]"
                            strQuery2 &= ", N'" & tblTable.Item(intField).Text & "'"
                        End If
                End Select
            End If
        Next
        If strQuery1.Length = 0 Then
            WriteStatus("Nothing to Insert", 0, lblStatusText)
            Exit Sub
        End If
        strQuery = strQuery & strQuery1 & ") VALUES (" & strQuery2 & ")"
        strQuery = Replace(strQuery, "(,", "(")

        If SeqData.curVar.DebugMode Then
            If MessageBox.Show("The query to be executed is: " & Environment.NewLine & strQuery & Environment.NewLine & Core.Message.strContinue, Core.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                WriteStatus("Insert cancelled", 0, lblStatusText)
                Exit Sub
            End If
        End If
        WriteStatus("Inserting Record", 0, lblStatusText)

        Try
            Dim dtsData As DataSet
            dtsData = SeqData.QueryDb(SeqData.dhdConnection, strQuery, 0)
            MessageBox.Show(SeqData.dhdConnection.DataBaseOnline)

            If SeqData.dhdConnection.ErrorLevel = -1 Then
                WriteStatus(SeqData.dhdConnection.ErrorMessage, 1, lblStatusText)
            Else
                WriteStatus(SeqData.dhdConnection.RowsAffected & " Record(s) Inserted.", 0, lblStatusText)
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error inserting the record: " & Environment.NewLine & ex.Message)
            SeqData.WriteLog("There was an error inserting the record: " & Environment.NewLine & ex.Message, 1)
        End Try

        If SeqData.curStatus.Status > 3 Then
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
        Else
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
        End If
        ColorReset()
        ButtonHandle()

    End Sub

    Private Sub ItemUpdate()
        If dgvTable1.SelectedRows.Count <> 1 Then Exit Sub
        strQuery = ""
        Dim strQueryUpdate As String = ""
        Dim strQueryWhere As String = " WHERE 1=1 "
        strQuery = " UPDATE [" & tblTable.TableName.Replace(".", "].[") & "] SET ,"

        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).BackColor = clrMarked And FieldEnabledCheck(tblTable.Item(intField)) = True Then
                Select Case tblTable.Item(intField).FieldDataType.ToUpper
                    Case "BIT"
                        strQueryUpdate &= ", [" & tblTable.Item(intField).FieldName & "] = " & tblTable.Item(intField).CheckState
                    Case "INTEGER"
                        If tblTable.Item(intField).Text = "NULL" Or tblTable.Item(intField).Text = "" Then
                            strQueryUpdate &= ", [" & tblTable.Item(intField).FieldName & "] = NULL "
                        Else
                            strQueryUpdate &= ", [" & tblTable.Item(intField).FieldName & "] = " & tblTable.Item(intField).Text
                        End If
                    Case "BINARY", "IMAGE", "TIMESTAMP"
                        'do nothing
                    Case Else
                        If tblTable.Item(intField).Text = "NULL" Or tblTable.Item(intField).Text = "" Then
                            strQueryUpdate &= ", [" & tblTable.Item(intField).FieldName & "] = NULL "
                        Else
                            strQueryUpdate &= ", [" & tblTable.Item(intField).FieldName & "] = N'" & tblTable.Item(intField).Text & "'"
                        End If
                End Select
            End If

            If tblTable.Item(intField).Identity = True Or tblTable.Item(intField).PrimaryKey = True Then
                For Each cell In dgvTable1.SelectedRows(0).Cells
                    If cell.OwningColumn.Name = tblTable.Item(intField).FieldName Then
                        strQueryWhere &= " AND [" & tblTable.Item(intField).FieldName & "] = " & SeqData.SetDelimiters(cell.Value, tblTable.Item(intField).FieldDataType, "=")
                    End If
                Next
            End If

        Next
        If strQueryUpdate.Length = 0 Then
            WriteStatus("Nothing to Update", 0, lblStatusText)
            Exit Sub
        End If
        If strQueryWhere = " WHERE 1=1 " Then
            WriteStatus("Item to update not found", 0, lblStatusText)
            Exit Sub
        End If
        strQuery = strQuery & strQueryUpdate & strQueryWhere
        strQuery = Replace(strQuery, ",,", "")

        If SeqData.curVar.DebugMode Then
            If MessageBox.Show("The query to be executed is: " & Environment.NewLine & strQuery & Environment.NewLine & Core.Message.strContinue, Core.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                WriteStatus("Update cancelled", 0, lblStatusText)
                Exit Sub
            End If
        End If
        WriteStatus("Updating Record", 0, lblStatusText)

        Try
            Dim dtsData As DataSet
            dtsData = SeqData.QueryDb(SeqData.dhdConnection, strQuery, 0)
            If SeqData.dhdConnection.ErrorLevel = -1 Then
                WriteStatus(SeqData.dhdConnection.ErrorMessage, 1, lblStatusText)
            Else
                WriteStatus(SeqData.dhdConnection.RowsAffected & " Record(s) Updated.", 0, lblStatusText)
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error updating the record: " & Environment.NewLine & ex.Message)
            SeqData.WriteLog("There was an error updating the record: " & Environment.NewLine & ex.Message, 1)
        End Try

        ColorReset()
        ButtonHandle()
    End Sub

    Private Sub ItemDelete()
        If dgvTable1.SelectedRows.Count <> 1 Then
            WriteStatus("You need to select a single row/item in order to delete an item", 1, lblStatusText)
            Exit Sub
        End If

        strQuery = ""
        Dim strQueryWhere As String = " WHERE 1=1 "
        strQuery = " DELETE FROM [" & tblTable.TableName.Replace(".", "].[") & "] "

        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).Identity = True Or tblTable.Item(intField).PrimaryKey = True Then
                For Each cell In dgvTable1.SelectedRows(0).Cells
                    If cell.OwningColumn.Name = tblTable.Item(intField).FieldName Then
                        strQueryWhere &= " AND [" & tblTable.Item(intField).FieldName & "] = " & SeqData.SetDelimiters(cell.Value, tblTable.Item(intField).FieldDataType, "=")
                    End If
                Next
            End If

        Next
        If strQueryWhere = " WHERE 1=1 " Then
            WriteStatus("You need an AUTOID or Primary Key field in order to delete an item", 1, lblStatusText)
            Exit Sub
        End If
        strQuery = strQuery & strQueryWhere

        If SeqData.curVar.DebugMode Then
            If MessageBox.Show("The query to be executed is: " & Environment.NewLine & strQuery & Environment.NewLine & Core.Message.strContinue, Core.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                WriteStatus("Delete cancelled", 0, lblStatusText)
                Exit Sub
            End If
        End If
        WriteStatus("Deleting Record", 0, lblStatusText)

        Try
            Dim dtsData As DataSet
            dtsData = SeqData.QueryDb(SeqData.dhdConnection, strQuery, 0)
            WriteStatus("Record Deleted", 0, lblStatusText)
        Catch ex As Exception
            MessageBox.Show("There was an error deleting the record: " & Environment.NewLine & ex.Message)
            SeqData.WriteLog("There was an error deleting the record: " & Environment.NewLine & ex.Message, 1)
        End Try

        If SeqData.curStatus.Status > 3 Then
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
        Else
            SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
        End If
        dgvTable1.Rows.Remove(dgvTable1.SelectedRows(0))
        ColorReset()
        ButtonHandle()
    End Sub

    Private Sub SearchAdd()

        Dim root As XmlElement = xmlSearch.DocumentElement
        If root Is Nothing Then
            xmlTables = SeqData.dhdText.CreateRootDocument(xmlSearch, "Sequenchel", "Searches", True)
        End If

        Dim NewNode As XmlNode = SeqData.dhdText.CreateAppendElement(xmlSearch.Item("Sequenchel").Item("Searches"), "Search")
        SeqData.dhdText.CreateAppendElement(NewNode, "TableName", tblTable.TableName)
        SeqData.dhdText.CreateAppendElement(NewNode, "SearchName", cbxSearch.Text)

        SeqData.dhdText.CreateAppendElement(NewNode, "UseTop", chkUseTop.Checked)
        SeqData.dhdText.CreateAppendElement(NewNode, "UseTopCount", txtUseTop.Text)
        SeqData.dhdText.CreateAppendElement(NewNode, "ReversedOrder", chkReversedSortOrder.Checked)

        Dim FieldsNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewNode, "Fields", "")

        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).BackColor = clrMarked Then

                Dim newAttribute1 As XmlAttribute = xmlSearch.CreateAttribute("FieldName")
                newAttribute1.Value = tblTable.Item(intField).FieldName

                Select Case tblTable.Item(intField).FieldDataType.ToUpper
                    Case "BIT"
                        Dim NewFieldNode As XmlNode = SeqData.dhdText.CreateAppendElement(FieldsNode, "Field", tblTable.Item(intField).CheckState)
                        NewFieldNode.Attributes.Append(newAttribute1)
                    Case Else
                        Dim NewFieldNode As XmlNode = SeqData.dhdText.CreateAppendElement(FieldsNode, "Field", tblTable.Item(intField).Text)
                        NewFieldNode.Attributes.Append(newAttribute1)
                End Select
            End If
        Next
    End Sub

    Private Sub SearchListLoad(strTable As String)
        cbxSearch.Text = ""
        cbxSearch.Items.Clear()
        Dim lstTables As List(Of String) = SeqData.LoadSearchXml(xmlSearch, strTable)
        If lstTables Is Nothing Then Exit Sub
        For Each lstItem As String In lstTables
            cbxSearch.Items.Add(lstItem)
        Next
    End Sub

    Private Sub SearchDelete(Optional UpdateMode As Boolean = False)
        Dim strSelection As String = cbxSearch.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlSearch, "Search", "SearchName", strSelection)
        If Not xNode Is Nothing Then
            If UpdateMode = False Then
                If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            End If
            xNode.ParentNode.RemoveChild(xNode)
        End If

    End Sub

    Private Sub SearchLoad()
        Dim strSelection As String = cbxSearch.Text
        Dim strFieldName As String = "", strFieldValue As String = ""

        If strSelection.Length = 0 Then Exit Sub
        Dim xPNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlSearch, "Search", "SearchName", strSelection)
        If Not xPNode Is Nothing Then

            If SeqData.dhdText.CheckNodeElement(xPNode, "UseTop") Then chkUseTop.Checked = xPNode.Item("UseTop").InnerText
            If SeqData.dhdText.CheckNodeElement(xPNode, "UseTopCount") Then txtUseTop.Text = xPNode.Item("UseTopCount").InnerText
            If SeqData.dhdText.CheckNodeElement(xPNode, "ReversedOrder") Then chkReversedSortOrder.Checked = xPNode.Item("ReversedOrder").InnerText

            For Each xNode As XmlNode In xPNode.SelectNodes(".//Field")
                strFieldName = xNode.Attributes("FieldName").Value
                strFieldValue = xNode.InnerText

                For intField As Integer = 0 To tblTable.Count - 1
                    If tblTable.Item(intField).FieldName = strFieldName Then
                        tblTable.Item(intField).BackColor = clrMarked
                        Select Case tblTable.Item(intField).FieldDataType.ToUpper
                            Case "BIT"
                                tblTable.Item(intField).Checked = strFieldValue
                            Case Else
                                tblTable.Item(intField).Text = strFieldValue
                        End Select
                    End If
                Next
            Next
        End If
    End Sub

#End Region

End Class
