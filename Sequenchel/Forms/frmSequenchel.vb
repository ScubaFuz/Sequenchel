﻿Imports System.IO
Imports System.Xml

Public Class frmSequenchel

    Private Sub frmSequenchel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        'dgvTable1.BackgroundImageLayout = ImageLayout.Center

        If My.Application.CommandLineArgs.Count > 0 Then basCode.ParseCommands(My.Application.CommandLineArgs)
        Me.Text = My.Application.Info.Title
        If basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch Then Me.Text &= " ControlMode"
        If basCode.curVar.DebugMode Then Me.Text &= " Debug"
        If basCode.curVar.DevMode Then Me.Text &= " Development"
        If basCode.curVar.Encryption = False Then Me.Text &= " NoEncryption"

        DebugSettings()
        basCode.SetDefaults()
        'basCode.SetDefaults()
        LoadLicense(lblStatusText)
        Me.Hide()
        frmAbout.Show()
        frmAbout.Refresh()

        lblLicense.Text = "Licensed to: " & basCode.curVar.LicenseName
        lblLicense.Left = Me.Width - lblLicense.Width - (basCode.curVar.BuildMargin * 5)

        If basCode.LoadSDBASettingsXml(xmlSDBASettings) = False Then
            If basCode.SaveSDBASettingsXml(xmlSDBASettings) = False Then WriteStatus("There was an error loading the main settings. please check the logfile", 1, lblStatusText)
        End If
        SetTimer()
        SecuritySet()
        If basCode.LoadGeneralSettingsXml(xmlGeneralSettings) = False Then WriteStatus("There was an error loading the settings. please check the logfile", 1, lblStatusText)
        LoadConnections()

        If basCode.curVar.SecurityOverride = True Then Me.Text &= " SecurityOverride"

        Me.Show()
        If SeqCore.LicenseValidated Then
            frmAbout.Hide()
        Else
            frmAbout.TopMost = True
        End If
        btnSearch.Focus()
    End Sub

    Private Sub frmSequenchel_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        SecuritySet()
        If basCode.curStatus.ConnectionReload = True Then
            LoadConnections()
            basCode.curStatus.ConnectionReload = False
            basCode.curStatus.TableSetReload = False
            basCode.curStatus.TableReload = False
        Else
            If Not cbxConnection.SelectedItem Is Nothing Then
                If basCode.curStatus.Connection <> cbxConnection.SelectedItem Then
                    basCode.curStatus.Connection = cbxConnection.SelectedItem
                    basCode.LoadConnection(xmlConnections, basCode.curStatus.Connection)
                End If
            End If
        End If
        If basCode.CurStatus.TableSetReload = True Then
            LoadTableSets()
            basCode.CurStatus.TableSetReload = False
            basCode.CurStatus.TableReload = False
        Else
            If Not cbxTableSet.SelectedItem Is Nothing Then
                If basCode.curStatus.TableSet <> cbxTableSet.SelectedItem Then
                    basCode.curStatus.TableSet = cbxTableSet.SelectedItem
                    basCode.LoadTableSet(xmlTableSets, basCode.curStatus.TableSet)
                End If
            End If
        End If
        If basCode.CurStatus.TableReload = True Then
            LoadTables()
            basCode.CurStatus.TableReload = False
        Else
            If Not cbxTable.SelectedItem Is Nothing Then
                If basCode.CurStatus.Table <> cbxTable.SelectedItem Then
                    basCode.CurStatus.Table = cbxTable.SelectedItem
                    LoadOneTable(basCode.curStatus.Table)
                    'LoadTable(basCode.curStatus.Table)
                End If
            End If
        End If
    End Sub

#Region "Common routines"

    Private Sub DebugSettings()
        If basCode.curVar.DebugMode Then
            btnTest.Visible = True
        End If
        If basCode.curVar.DevMode Then
            'mnuMain.Visible = True
        End If
    End Sub

    Private Sub SecuritySet()
        If basCode.curVar.AllowSettingsChange = False And basCode.curVar.SecurityOverride = False Then mnuMainEditSettings.Enabled = False
        If basCode.curVar.AllowConfiguration = False And basCode.curVar.SecurityOverride = False Then mnuMainEditConfiguration.Enabled = False
        If basCode.curVar.AllowLinkedServers = False And basCode.curVar.SecurityOverride = False Then mnuMainEditLinkedServers.Enabled = False
        If basCode.curVar.AllowDataImport = False And basCode.curVar.SecurityOverride = False Then mnuMainToolsImport.Enabled = False
        If basCode.curVar.AllowSmartUpdate = False And basCode.curVar.SecurityOverride = False Then mnuMainToolsSmartUpdate.Enabled = False
        If basCode.curVar.AllowUpdate = False And basCode.curVar.SecurityOverride = False Then btnUpdate.Enabled = False
        If basCode.curVar.AllowInsert = False And basCode.curVar.SecurityOverride = False Then btnAdd.Enabled = False
        If basCode.curVar.AllowDelete = False And basCode.curVar.SecurityOverride = False Then btnDelete.Enabled = False
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        'MessageBox.Show(basCode.TestPath(1))
        'MessageBox.Show(basCode.TestPath(2))
        'MessageBox.Show(basCode.TestPath(3))
        'MessageBox.Show(basCode.TestPath(4))
        'MessageBox.Show(basCode.TestPath(5))
    End Sub

#End Region

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
        WriteStatus("", 0, lblStatusText)
        ShowSettingsForm()
        CursorControl()
    End Sub

    Private Sub mnuMainEditLinkedServers_Click(sender As Object, e As EventArgs) Handles mnuMainEditLinkedServers.Click
        WriteStatus("", 0, lblStatusText)
        ShowLinkedServerForm()
    End Sub

    Private Sub mnuMainEditConfiguration_Click(sender As Object, e As EventArgs) Handles mnuMainEditConfiguration.Click
        WriteStatus("", 0, lblStatusText)
        ShowConfigurationForm()
    End Sub

    Private Sub mnuMainToolsReports_Click(sender As Object, e As EventArgs) Handles mnuMainToolsReports.Click
        WriteStatus("", 0, lblStatusText)
        ShowReportsForm()
    End Sub

    Private Sub mnuMainToolsImport_Click(sender As Object, e As EventArgs) Handles mnuMainToolsImport.Click
        WriteStatus("", 0, lblStatusText)
        ShowImportForm()
    End Sub

    Private Sub mnuMainToolsSmartUpdate_Click(sender As Object, e As EventArgs) Handles mnuMainToolsSmartUpdate.Click
        WriteStatus("", 0, lblStatusText)
        ShowSmartUpdateForm()
    End Sub

    Private Sub mnuMainHelpManual_Click(sender As Object, e As EventArgs) Handles mnuMainHelpManual.Click
        WriteStatus("", 0, lblStatusText)
        System.Diagnostics.Process.Start("http://sequenchel.com/service/manual")
    End Sub

    Private Sub mnuMainHelpAbout_Click(sender As Object, e As EventArgs) Handles mnuMainHelpAbout.Click
        WriteStatus("", 0, lblStatusText)
        basCode.curVar.CallSplash = True
        Dim frmAboutForm As New frmAbout
        frmAboutForm.Show(Me)
    End Sub

#End Region

    Private Sub ShowReportsForm()
        Dim frmReportsForm As New frmReports
        frmReportsForm.Show()
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

#Region "Definitions Load"

#Region "Controls"
    Private Sub btnConnectionsReload_Click(sender As Object, e As EventArgs) Handles btnConnectionsReload.Click
        WriteStatus("", 0, lblStatusText)
        LoadConnections()
    End Sub

    Private Sub cbxConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxConnection.SelectedIndexChanged
        WriteStatus("", 0, lblStatusText)
        If cbxConnection.SelectedIndex > -1 Then
            CursorControl("Wait")
            basCode.curStatus.Connection = cbxConnection.SelectedItem
            basCode.LoadConnection(xmlConnections, basCode.curStatus.Connection)
            LoadTableSets()
            CursorControl()
        End If
    End Sub

    Private Sub btnTableSetsReload_Click(sender As Object, e As EventArgs) Handles btnTableSetsReload.Click
        WriteStatus("", 0, lblStatusText)
        LoadTableSets()
    End Sub

    Private Sub cbxTableSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxTableSet.SelectedIndexChanged
        WriteStatus("", 0, lblStatusText)
        If cbxTableSet.SelectedIndex >= -1 Then
            CursorControl("Wait")
            basCode.curStatus.TableSet = cbxTableSet.SelectedItem
            basCode.LoadTableSet(xmlTableSets, basCode.curStatus.TableSet)
            LoadTables()
            CursorControl()
        End If
    End Sub

    Private Sub btnTablesReload_Click(sender As Object, e As EventArgs) Handles btnTablesReload.Click
        WriteStatus("", 0, lblStatusText)
        LoadTables()
    End Sub

    Private Sub cbxTable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxTable.SelectedIndexChanged
        WriteStatus("", 0, lblStatusText)
        If cbxTable.SelectedIndex >= -1 Then
            CursorControl("Wait")
            basCode.curStatus.Table = cbxTable.SelectedItem
            LoadOneTable(basCode.curStatus.Table)
            'LoadTable(basCode.curStatus.Table)
            CursorControl()
        End If
    End Sub

    Private Sub sptFields1_Panel2_MouseWheel(sender As Object, e As MouseEventArgs) Handles sptFields1.Panel2.MouseWheel
        sptFields1.Panel1.VerticalScroll.Minimum = sptFields1.Panel2.VerticalScroll.Minimum
        sptFields1.Panel1.VerticalScroll.Maximum = sptFields1.Panel2.VerticalScroll.Maximum

        Dim vScrollPosition As Integer = sptFields1.Panel2.VerticalScroll.Value
        sptFields1.Panel1.AutoScrollPosition = New Point(sptFields1.Panel1.AutoScrollPosition.X, vScrollPosition)
        sptFields1.Panel1.VerticalScroll.Value = vScrollPosition
        sptFields1.Panel2.AutoScrollPosition = New Point(sptFields1.Panel2.AutoScrollPosition.X, vScrollPosition)
        'sptFields1.Panel2.VerticalScroll.Value = vScrollPosition
        sptFields1.Panel1.Invalidate()
        sptFields1.Panel2.Invalidate()
    End Sub

    Private Sub sptFields1_Panel1_MouseWheel(sender As Object, e As MouseEventArgs) Handles sptFields1.Panel1.MouseWheel
        sptFields1.Panel1.VerticalScroll.Minimum = sptFields1.Panel2.VerticalScroll.Minimum
        sptFields1.Panel1.VerticalScroll.Maximum = sptFields1.Panel2.VerticalScroll.Maximum

        Dim vScrollPosition As Integer = sptFields1.Panel2.VerticalScroll.Value - e.Delta
        vScrollPosition = Math.Max(sptFields1.Panel1.VerticalScroll.Minimum, vScrollPosition)
        vScrollPosition = Math.Min(sptFields1.Panel1.VerticalScroll.Maximum, vScrollPosition)
        sptFields1.Panel1.AutoScrollPosition = New Point(sptFields1.Panel1.AutoScrollPosition.X, vScrollPosition)
        sptFields1.Panel1.VerticalScroll.Value = vScrollPosition
        sptFields1.Panel2.AutoScrollPosition = New Point(sptFields1.Panel2.AutoScrollPosition.X, vScrollPosition)
        sptFields1.Panel2.VerticalScroll.Value = vScrollPosition
        sptFields1.Panel1.Invalidate()
        sptFields1.Panel2.Invalidate()
    End Sub

    Private Sub Panel2_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles sptFields1.Panel2.Scroll
        sptFields1.Panel1.VerticalScroll.Minimum = sptFields1.Panel2.VerticalScroll.Minimum
        sptFields1.Panel1.VerticalScroll.Maximum = sptFields1.Panel2.VerticalScroll.Maximum

        If e.ScrollOrientation = ScrollOrientation.VerticalScroll Then
            sptFields1.Panel1.AutoScrollPosition = New Point(sptFields1.Panel1.AutoScrollPosition.X, e.NewValue)
            sptFields1.Panel1.VerticalScroll.Value = e.NewValue
            sptFields1.Panel2.AutoScrollPosition = New Point(sptFields1.Panel2.AutoScrollPosition.X, e.NewValue)
            sptFields1.Panel2.VerticalScroll.Value = e.NewValue
            sptFields1.Panel1.Invalidate()
            sptFields1.Panel2.Invalidate()
        End If
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs)
        WriteStatus("", 0, lblStatusText)
        Dim strReload As String = sender.name.ToString.Substring(3, sender.name.ToString.Length - 3)
        LoadSearchCriteria(strReload)
    End Sub

    Private Sub btnDefault_Click(sender As Object, e As EventArgs)
        WriteStatus("", 0, lblStatusText)
        Dim strDefault As String = sender.name.ToString.Substring(3, sender.name.ToString.Length - 3)
        LoadDefaultValue(strDefault)
    End Sub

    Private Sub cbxRelatedField_SelectedIndexChanged(sender As Object, e As EventArgs)
        WriteStatus("", 0, lblStatusText)
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
            xmlConnections.RemoveAll()
            basCode.curVar.TableSetsFile = ""
            cbxConnection.Items.Clear()
            cbxConnection.Text = ""
        End If
        If intLevel >= 3 Then
            xmlTableSets.RemoveAll()
            basCode.curVar.TablesFile = ""
            cbxTableSet.Items.Clear()
            cbxTableSet.Text = ""
        End If
        If intLevel >= 2 Then
            xmlTables.RemoveAll()
            cbxTable.Items.Clear()
            cbxTable.Text = ""
        End If
        If intLevel >= 1 Then
            FieldsDispose()
        End If
    End Sub

    Private Sub FieldsDispose()
        basCode.curStatus.SuspendActions = True

        TableClear()
        basCode.TableClear()
        arrLabels.Clear()
        PanelControlsDispose(sptFields1.Panel1)
        PanelControlsDispose(sptFields1.Panel2)

        sptTable1.SplitterDistance = sptTable1.Width - 404
        sptFields1.SplitterDistance = 150
        dgvTable1.Columns.Clear()
        lblListCountNumber.Text = 0
        basCode.curStatus.SuspendActions = False

        sptFields1.Panel1.VerticalScroll.Minimum = sptFields1.Panel2.VerticalScroll.Minimum
        sptFields1.Panel1.VerticalScroll.Maximum = sptFields1.Panel2.VerticalScroll.Maximum
        sptFields1.Panel1.AutoScrollPosition = New Point(sptFields1.Panel1.AutoScrollPosition.X, 0)
        sptFields1.Panel1.VerticalScroll.Value = 0
        sptFields1.Panel2.AutoScrollPosition = New Point(sptFields1.Panel2.AutoScrollPosition.X, 0)
        sptFields1.Panel2.VerticalScroll.Value = 0
    End Sub

    Private Sub LoadConnections()
        AllClear(4)
        Dim lstConnections As List(Of String) = basCode.LoadConnections(xmlConnections)
        If lstConnections Is Nothing Then
            basCode.UseMainConnection()
            Exit Sub
        End If
        For Each lstItem As String In lstConnections
            cbxConnection.Items.Add(lstItem)
        Next
        cbxConnection.SelectedItem = basCode.curStatus.Connection
    End Sub

    Private Sub LoadTableSets()
        AllClear(3)
        Dim lstTableSets As List(Of String) = basCode.LoadTableSets(xmlTableSets)
        If lstTableSets Is Nothing Then Exit Sub
        For Each lstItem As String In lstTableSets
            cbxTableSet.Items.Add(lstItem)
        Next
        cbxTableSet.SelectedItem = basCode.curStatus.TableSet
    End Sub

    Private Sub LoadTables()
        AllClear(2)
        Dim lstTables As List(Of String) = basCode.LoadTables(xmlTables, False)
        If lstTables Is Nothing Then Exit Sub
        For Each lstItem As String In lstTables
            cbxTable.Items.Add(lstItem)
        Next
        cbxTable.SelectedItem = basCode.curStatus.Table
    End Sub

    Friend Sub LoadOneTable(strTable As String)
        Try
            FieldsDispose()
            If basCode.LoadTable(xmlTables, strTable) = False Then
                WriteStatus("Loading the table " & strTable & " failed. Check your configuration", 2, lblStatusText)
                Exit Sub
            End If

            For intCount = 0 To basCode.basTable.Count - 1
                Dim fldField As Object = Nothing
                Select Case basCode.basTable.Item(intCount).Category
                    Case 1
                        fldField = New TextBox
                    Case 2
                        fldField = New CheckBox
                    Case 5
                        fldField = New ManagedSelectField
                        fldField.DataConn.DataLocation = basCode.dhdConnection.DataLocation
                        fldField.DataConn.DatabaseName = basCode.dhdConnection.DatabaseName
                        fldField.DataConn.DataProvider = basCode.dhdConnection.DataProvider
                        fldField.DataConn.LoginMethod = basCode.dhdConnection.LoginMethod
                        fldField.DataConn.LoginName = basCode.dhdConnection.LoginName
                        fldField.DataConn.Password = basCode.dhdConnection.Password
                        fldField.Table = basCode.basTable.TableName
                        fldField.SearchField = fldField.FieldName
                    Case 6
                        fldField = New ManagedSelectField
                End Select
                fldField.Name = basCode.basTable.Item(intCount).Name
                fldField.Tag = basCode.basTable.Item(intCount).Name

                If basCode.basTable.Item(intCount).FieldVisible = True Then
                    sptFields1.Panel2.Controls.Add(fldField)
                    fldField.Top = ((sptFields1.Panel2.Controls.Count - 1) * basCode.curVar.FieldHeight) + (sptFields1.Panel2.Controls.Count * basCode.curVar.BuildMargin)
                    fldField.Width = basCode.basTable.Item(intCount).FieldWidth

                    If fldField.top > sptFields1.Panel2.Height And fldField.Width >= sptFields1.Panel2.Width - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth Then
                        fldField.width = sptFields1.Panel2.Width - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                    End If
                    Dim lblLabel As New Label
                    arrLabels.Add(lblLabel)
                    lblLabel.Name = "lbl" & fldField.Name
                    sptFields1.Panel1.Controls.Add(lblLabel)
                    lblLabel.Text = basCode.basTable.Item(intCount).FieldAlias
                    lblLabel.Top = fldField.Top + basCode.curVar.BuildMargin
                    lblLabel.AutoSize = True
                    lblLabel.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                    lblLabel.Left = sptFields1.Panel1.Width - lblLabel.Width - 25 - basCode.curVar.BuildMargin
                    If lblLabel.Left < 0 Then
                        sptTable1.SplitterDistance += lblLabel.Left
                        sptFields1.SplitterDistance += (lblLabel.Left * -1)
                    End If
                    If basCode.basTable.Item(intCount).FieldSearchList = True And basCode.basTable.Item(intCount).FieldDataType.ToString.ToUpper <> "BIT" Then
                        Dim btnButton As New Button
                        btnButton.Name = "btn" & fldField.Name
                        sptFields1.Panel1.Controls.Add(btnButton)
                        btnButton.Text = ""
                        btnButton.Image = My.Resources.reload16
                        btnButton.ImageAlign = ContentAlignment.MiddleCenter
                        btnButton.Size = New System.Drawing.Size(25, 23)
                        btnButton.Top = fldField.Top - basCode.curVar.BuildMargin / 2
                        btnButton.Left = sptFields1.Panel1.Width - btnButton.Width
                        btnButton.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                        btnButton.UseVisualStyleBackColor = True
                        AddHandler btnButton.Click, AddressOf Me.btnReload_Click
                    End If

                    If basCode.basTable.Item(intCount).DefaultButton = True And Not (basCode.basTable.Item(intCount).FieldSearchList = True And basCode.basTable.Item(intCount).FieldDataType.ToString.ToUpper <> "BIT") Then
                        Dim btnButton As New Button
                        btnButton.Name = "btn" & fldField.Name
                        sptFields1.Panel1.Controls.Add(btnButton)
                        btnButton.Text = ""
                        btnButton.Image = My.Resources.TSfavicon
                        btnButton.ImageAlign = ContentAlignment.MiddleCenter
                        btnButton.Size = New System.Drawing.Size(25, 23)
                        btnButton.Top = fldField.Top - basCode.curVar.BuildMargin / 2
                        btnButton.Left = sptFields1.Panel1.Width - btnButton.Width
                        btnButton.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                        btnButton.UseVisualStyleBackColor = True
                        AddHandler btnButton.Click, AddressOf Me.btnDefault_Click
                    End If

                    If basCode.basTable.Item(intCount).Count > 0 Then
                        For intRel As Integer = 1 To basCode.basTable.Item(intCount).Count
                            MessageBox.Show(basCode.basTable.Item(intCount).Item(intRel).Name)

                            'If basCode.basTable.Item(intCount).FieldRelatedField.ToString.Length > 0 And basCode.basTable.Item(intCount).FieldRelation.ToString.Length > 0 Then
                            'End If

                            Dim msfRelatedField As New ManagedSelectField
                            'AddHandler msfRelatedField.SelectedIndexChanged, AddressOf Me.cbxRelatedField_SelectedIndexChanged
                            msfRelatedField.Name = basCode.basTable.Item(intCount).Item(intRel).RelationTableAlias & "." & basCode.basTable.Item(intCount).Item(intRel).RelationField

                            msfRelatedField.DataConn.DataLocation = basCode.dhdConnection.DataLocation
                            msfRelatedField.DataConn.DatabaseName = basCode.dhdConnection.DatabaseName
                            msfRelatedField.DataConn.DataProvider = basCode.dhdConnection.DataProvider
                            msfRelatedField.DataConn.LoginMethod = basCode.dhdConnection.LoginMethod
                            msfRelatedField.DataConn.LoginName = basCode.dhdConnection.LoginName
                            msfRelatedField.DataConn.Password = basCode.dhdConnection.Password
                            msfRelatedField.Table = fldField.FieldRelation.Substring(0, fldField.FieldRelation.LastIndexOf("."))
                            msfRelatedField.SearchField = fldField.FieldRelatedField
                            msfRelatedField.IdentifierField = fldField.FieldRelation.Substring(fldField.FieldRelation.LastIndexOf(".") + 1, fldField.FieldRelation.length - (fldField.FieldRelation.LastIndexOf(".") + 1))

                            sptFields1.Panel2.Controls.Add(msfRelatedField)
                            msfRelatedField.Top = fldField.Top
                            msfRelatedField.Left = fldField.Left + fldField.Width + basCode.curVar.BuildMargin
                            If fldField.top > sptFields1.Panel2.Height Then
                                msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                            Else
                                msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (basCode.curVar.BuildMargin * 3)
                            End If

                            msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                            msfRelatedField.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                            msfRelatedField.Visible = True
                        Next
                    End If

                End If

                If basCode.basTable.Item(intCount).FieldList = True Or basCode.basTable.Item(intCount).Identity = True Or basCode.basTable.Item(intCount).PrimaryKey = True Then
                    Dim dgvColumn As New DataGridViewTextBoxColumn
                    dgvColumn.Name = basCode.basTable.Item(intCount).FieldName
                    dgvColumn.HeaderText = basCode.basTable.Item(intCount).FieldAlias
                    Select Case basCode.basTable.Item(intCount).FieldDataType.ToUpper
                        Case "BIT"
                            dgvColumn.ValueType = GetType(Boolean)
                        Case "INTEGER"
                            dgvColumn.ValueType = GetType(Integer)
                        Case Else
                            dgvColumn.ValueType = GetType(String)
                    End Select
                    dgvColumn.Width = basCode.basTable.Item(intCount).FieldListWidth
                    dgvTable1.Columns.Add(dgvColumn)
                    If basCode.basTable.Item(intCount).FieldList = True Then
                        dgvColumn.Visible = True
                    Else
                        dgvColumn.Visible = False
                    End If
                    If basCode.basTable.Item(intCount).Identity = True Or basCode.basTable.Item(intCount).PrimaryKey = True Then
                        dgvColumn.Tag = "Identity"
                    Else
                        dgvColumn.Tag = ""
                    End If
                End If

            Next


        Catch ex As Exception
            WriteStatus("There was an error reading the Table file. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error reading the Table file. Please check the file for Table: " & Environment.NewLine & strTable & Environment.NewLine & ex.Message, 1)
        End Try
    End Sub

    Friend Sub LoadTable(strTable As String)
        Try
            FieldsDispose()
            Dim xPNode As System.Xml.XmlNode = basCode.dhdText.FindXmlNode(xmlTables, "Table", "Alias", strTable)

            If Not xPNode Is Nothing Then
                'If basCode.dhdText.CheckNodeElement(xPNode, "Alias") Then tabTables.TabPages("tpgTable1").Text = xPNode.Item("Alias").InnerText
                If basCode.dhdText.CheckNodeElement(xPNode, "Name") Then tblTable.TableName = xPNode.Item("Name").InnerText
                If basCode.dhdText.CheckNodeElement(xPNode, "Alias") Then tblTable.TableAlias = xPNode.Item("Alias").InnerText.Replace(".", "_")
                If basCode.dhdText.CheckNodeElement(xPNode, "Visible") Then tblTable.TableVisible = xPNode.Item("Visible").InnerText
                If basCode.dhdText.CheckNodeElement(xPNode, "Update") Then tblTable.TableUpdate = xPNode.Item("Update").InnerText
                If basCode.dhdText.CheckNodeElement(xPNode, "Search") Then tblTable.TableSearch = xPNode.Item("Search").InnerText
                If basCode.dhdText.CheckNodeElement(xPNode, "Insert") Then tblTable.TableInsert = xPNode.Item("Insert").InnerText
                If basCode.dhdText.CheckNodeElement(xPNode, "Delete") Then tblTable.TableDelete = xPNode.Item("Delete").InnerText

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
                        fldField.TabIndex = tblTable.Count
                        If basCode.dhdText.CheckNodeElement(xNode, "FldName") Then fldField.FieldName = xNode.Item("FldName").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "FldAlias") Then fldField.FieldAlias = xNode.Item("FldAlias").InnerText
                        If fldField.FieldAlias = "" Then fldField.FieldAlias = fldField.FieldName
                        If basCode.dhdText.CheckNodeElement(xNode, "FldName") Then fldField.Name = tblTable.TableName & "." & xNode.Item("FldName").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "DataType") Then fldField.FieldDataType = xNode.Item("DataType").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "Identity") Then fldField.Identity = xNode.Item("Identity").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "PrimaryKey") Then fldField.PrimaryKey = xNode.Item("PrimaryKey").InnerText

                        fldField.Left = basCode.curVar.BuildMargin
                        If basCode.dhdText.CheckNodeElement(xNode, "FldWidth") Then
                            If xNode.Item("FldWidth").InnerText >= sptFields1.Panel2.Width - basCode.curVar.BuildMargin * 3 Then
                                fldField.Width = sptFields1.Panel2.Width - basCode.curVar.BuildMargin * 3
                                fldField.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                            Else
                                fldField.Width = xNode.Item("FldWidth").InnerText
                            End If
                        End If

                        If fldField.FieldCategory = 5 Then
                            fldField.DataConn.DataLocation = basCode.dhdConnection.DataLocation
                            fldField.DataConn.DatabaseName = basCode.dhdConnection.DatabaseName
                            fldField.DataConn.DataProvider = basCode.dhdConnection.DataProvider
                            fldField.DataConn.LoginMethod = basCode.dhdConnection.LoginMethod
                            fldField.DataConn.LoginName = basCode.dhdConnection.LoginName
                            fldField.DataConn.Password = basCode.dhdConnection.Password
                            fldField.Table = tblTable.TableName
                            fldField.SearchField = fldField.FieldName
                        End If

                        If basCode.dhdText.CheckNodeElement(xNode, "Relations") Then
                            For Each xRnode As XmlNode In xNode.Item("Relations").ChildNodes

                                'New relation style...
                                Dim strRelTable As String = "", strRelField As String = "", strRelation As String = ""

                                If basCode.dhdText.CheckNodeElement(xRnode, "RelationTable") Then fldField.FieldRelationTable = xRnode.Item("RelationTable").InnerText
                                If basCode.dhdText.CheckNodeElement(xRnode, "RelationTableAlias") Then fldField.FieldRelationTableAlias = xRnode.Item("RelationTableAlias").InnerText
                                If String.IsNullOrEmpty(fldField.FieldRelationTableAlias) Then fldField.FieldRelationTableAlias = fldField.FieldRelationTable.ToString.Replace(".", "_")
                                If basCode.dhdText.CheckNodeElement(xRnode, "RelationField") Then fldField.FieldRelationField = xRnode.Item("RelationField").InnerText
                                fldField.FieldRelation = fldField.FieldRelationTable & "." & fldField.FieldRelationField
                                If basCode.dhdText.CheckNodeElement(xRnode, "RelatedField") Then fldField.FieldRelatedField = xRnode.Item("RelatedField").InnerText
                                If basCode.dhdText.CheckNodeElement(xRnode, "RelatedFieldList") Then fldField.FieldRelatedFieldList = xRnode.Item("RelatedFieldList").InnerText
                                If xRnode.ChildNodes.Count = 1 Then
                                    strRelation = xRnode.InnerText
                                    If strRelation.Length > 0 Then
                                        fldField.FieldRelationTable = strRelation.Substring(0, strRelation.LastIndexOf("."))
                                        fldField.FieldRelationField = strRelation.Substring(strRelation.LastIndexOf(".") + 1, strRelation.Length - (strRelation.LastIndexOf(".") + 1))
                                        If xRnode.Attributes.Count > 0 Then
                                            If xRnode.Attributes(0).Name = "RelatedField" Then fldField.FieldRelatedField = xRnode.Attributes("RelatedField").InnerText
                                            If xRnode.Attributes.Count > 1 Then
                                                If xRnode.Attributes(1).Name = "RelatedFieldList" Then fldField.FieldRelatedFieldList = xRnode.Attributes("RelatedFieldList").InnerText
                                            End If
                                            fldField.FieldRelation = xRnode.InnerText
                                            'If basCode.dhdText.CheckNodeElement(xRnode, "RelatedField") Then fldField.FieldRelatedField = xRnode.Attributes("RelatedField").InnerText
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                        'fldField.FieldRelation = xNode.Item("Relations").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "DefaultButton") Then fldField.DefaultButton = xNode.Item("DefaultButton").InnerText
                        If fldField.DefaultButton = True Then
                            Try
                                fldField.DefaultValue = xNode.Item("DefaultButton").Attributes("DefaultValue").Value
                            Catch ex As Exception
                                fldField.DefaultValue = ""
                                basCode.WriteLog("there was an error setting the value for DefaultValue: " & Environment.NewLine & ex.Message, 1)
                            End Try
                        End If

                        If basCode.dhdText.CheckNodeElement(xNode, "FldList") Then fldField.FieldList = xNode.Item("FldList").InnerText
                        If fldField.FieldList = True Then
                            Try
                                fldField.FieldListOrder = xNode.Item("FldList").Attributes("Order").Value
                                fldField.FieldListWidth = xNode.Item("FldList").Attributes("Width").Value
                            Catch ex As Exception
                                fldField.FieldListOrder = 0
                                fldField.FieldListWidth = 0
                                basCode.WriteLog("there was an error setting the value for ListOrder or ListWidth: " & Environment.NewLine & ex.Message, 1)
                            End Try
                        End If
                        If basCode.dhdText.CheckNodeElement(xNode, "FldSearch") Then fldField.FieldSearch = xNode.Item("FldSearch").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "FldSearchList") Then fldField.FieldSearchList = xNode.Item("FldSearchList").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "FldUpdate") Then fldField.FieldUpdate = xNode.Item("FldUpdate").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "FldVisible") Then fldField.FieldVisible = xNode.Item("FldVisible").InnerText

                        If basCode.dhdText.CheckNodeElement(xNode, "ControlField") Then fldField.ControlField = xNode.Item("ControlField").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "ControlValue") Then fldField.ControlValue = xNode.Item("ControlValue").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "ControlUpdate") Then fldField.ControlUpdate = xNode.Item("ControlUpdate").InnerText
                        If basCode.dhdText.CheckNodeElement(xNode, "ControlMode") Then fldField.ControlMode = xNode.Item("ControlMode").InnerText

                        If fldField.FieldVisible = True Then
                            sptFields1.Panel2.Controls.Add(fldField)
                            If tblTable.Count = 1 Then
                                fldField.Top = basCode.curVar.BuildMargin
                            Else
                                fldField.Top = tblTable(tblTable.Count - 2).Top + fldField.Height + basCode.curVar.BuildMargin
                            End If
                            If fldField.top > sptFields1.Panel2.Height And fldField.Width >= sptFields1.Panel2.Width - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth Then
                                fldField.width = sptFields1.Panel2.Width - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                            End If
                            Dim lblLabel As New Label
                            arrLabels.Add(lblLabel)
                            lblLabel.Name = "lbl" & fldField.Name
                            sptFields1.Panel1.Controls.Add(lblLabel)
                            lblLabel.Text = fldField.FieldAlias
                            lblLabel.Top = fldField.Top + basCode.curVar.BuildMargin
                            lblLabel.AutoSize = True
                            lblLabel.Anchor = AnchorStyles.Right Or AnchorStyles.Top
                            lblLabel.Left = sptFields1.Panel1.Width - lblLabel.Width - 25 - basCode.curVar.BuildMargin
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
                                btnButton.Top = fldField.Top - basCode.curVar.BuildMargin / 2
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
                                btnButton.Top = fldField.Top - basCode.curVar.BuildMargin / 2
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
                                msfRelatedField.Name = fldField.FieldRelationTable & "." & fldField.FieldRelatedField
                                msfRelatedField.FieldName = fldField.FieldRelatedField
                                msfRelatedField.FieldAlias = fldField.FieldRelatedField
                                msfRelatedField.FieldRelatedField = fldField.FieldName
                                msfRelatedField.FieldSearch = fldField.FieldSearch
                                msfRelatedField.FieldUpdate = fldField.FieldUpdate
                                msfRelatedField.FieldVisible = fldField.FieldVisible
                                msfRelatedField.ControlField = fldField.ControlField
                                msfRelatedField.ControlValue = fldField.ControlValue
                                msfRelatedField.ControlUpdate = fldField.ControlUpdate
                                msfRelatedField.ControlMode = fldField.ControlMode

                                msfRelatedField.DataConn.DataLocation = basCode.dhdConnection.DataLocation
                                msfRelatedField.DataConn.DatabaseName = basCode.dhdConnection.DatabaseName
                                msfRelatedField.DataConn.DataProvider = basCode.dhdConnection.DataProvider
                                msfRelatedField.DataConn.LoginMethod = basCode.dhdConnection.LoginMethod
                                msfRelatedField.DataConn.LoginName = basCode.dhdConnection.LoginName
                                msfRelatedField.DataConn.Password = basCode.dhdConnection.Password
                                msfRelatedField.Table = fldField.FieldRelation.Substring(0, fldField.FieldRelation.LastIndexOf("."))
                                msfRelatedField.SearchField = fldField.FieldRelatedField
                                msfRelatedField.IdentifierField = fldField.FieldRelation.Substring(fldField.FieldRelation.LastIndexOf(".") + 1, fldField.FieldRelation.length - (fldField.FieldRelation.LastIndexOf(".") + 1))

                                sptFields1.Panel2.Controls.Add(msfRelatedField)
                                msfRelatedField.Top = fldField.Top
                                msfRelatedField.Left = fldField.Left + fldField.Width + basCode.curVar.BuildMargin
                                If fldField.top > sptFields1.Panel2.Height Then
                                    msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                                Else
                                    msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (basCode.curVar.BuildMargin * 3)
                                End If

                                msfRelatedField.Width = sptFields1.Panel2.Width - msfRelatedField.Left - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                                msfRelatedField.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
                                msfRelatedField.Visible = True
                                'If basCode.dhdConnection.DataBaseOnline = True Then msfRelatedField.RunSearch()
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
            If basCode.curStatus.Status > 3 Then
                basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
            Else
                basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search
            End If
            ButtonHandle()
        Catch ex As Exception
            WriteStatus("There was an error reading the Table file. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error reading the Table file. Please check the file for Table: " & Environment.NewLine & strTable & Environment.NewLine & ex.Message, 1)
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
            If ctrField.Left + ctrField.Width > sptFields1.Panel2.Width - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth Then
                ctrField.Width = sptFields1.Panel2.Width - ctrField.Left - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
            End If
        Next
    End Sub
#End Region

#Region "Data Load"

#Region "Controls"

    Private Sub dgvTable1_DoubleClick(sender As Object, e As MouseEventArgs) Handles dgvTable1.DoubleClick
        WriteStatus("", 0, lblStatusText)
        Dim args As MouseEventArgs = e
        Dim dgv As DataGridView = sender
        Dim hit As DataGridView.HitTestInfo = dgv.HitTest(args.X, args.Y)
        If (hit.Type = DataGridViewHitTestType.TopLeftHeader) Then
            DataGridViewColumnSize(sender)
        End If
    End Sub

    Private Sub dgvTable1_SelectionChanged(sender As Object, e As EventArgs) Handles dgvTable1.SelectionChanged
        WriteStatus("", 0, lblStatusText)
        ItemSelect()
    End Sub

    Private Sub btnLoadList_Click(sender As Object, e As EventArgs) Handles btnLoadList.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        FieldsClearAll(True)
        ColorReset()
        LoadList(False)
        CursorControl()
    End Sub

    Private Sub btnLoadSearchCriteria_Click(sender As Object, e As EventArgs) Handles btnLoadSearchCriteria.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        LoadSearchCriteria()
        CursorControl()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        LoadList(True)
        CursorControl()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        FieldsClearAll(True)
        CursorControl()
    End Sub

    Private Sub btnExportList_Click(sender As Object, e As EventArgs) Handles btnExportList.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If basCode.dhdText.DatasetCheck(dtsTable) = False Then Exit Sub
        Dim strReportName As String = ""
        If cbxSearch.Text.Length > 0 Then
            strReportName = cbxSearch.Text
        ElseIf cbxTable.Text.Length > 0 Then
            strReportName = cbxTable.Text
        Else
            strReportName = basCode.curStatus.Table
        End If
        Dim strFileName As String = GetSaveFileName(strReportName)
        If ExportFile(dtsTable, strFileName) = False Then WriteStatus("There was an error exporting the file. please check the log.", 1, lblStatusText)
        CursorControl()
        'XmlExportDatagridView(dgvTable1, "Sequenchel", basCode.CurStatus.Table, basCode.CurStatus.Table & "-Item")
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If btnAdd.Text = "New Item" Then
            'MessageBox.Show("Your current selection (if any) is duplicated." & Environment.NewLine & _
            '                   "You can edit the data before you save it to the database" & Environment.NewLine & _
            '                   "Click 'Clear' to cancel the operation" & Environment.NewLine & _
            '                   "Click 'Save' to save the new item to the database" _
            '                   , "How it works", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If basCode.curStatus.Status > 3 Then
                basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlAdd
            Else
                basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Add
            End If
            FieldsDisable()
            FieldsClear(True)
            FieldsEnable(False)

            basCode.curStatus.SuspendActions = True
            dgvTable1.ClearSelection()
            dgvTable1.CurrentCell = Nothing

            ButtonHandle()
            basCode.curStatus.SuspendActions = False
            TagsClear()
            ColorSet()
            btnAdd.Text = "Save Item"
            btnAdd.BackColor = clrMarked
            WriteStatus("Adding mode enabled", 0, lblStatusText)
        ElseIf btnAdd.Text = "Save Item" Then
            btnAdd.Text = "New Item"
            btnAdd.BackColor = clrControl
            If basCode.curStatus.Status > 3 Then
                basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
            Else
                basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
            End If
            ItemAdd()
            FieldsEnable()
            ButtonHandle()
        End If
        CursorControl()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        ItemUpdate()
        CursorControl()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        WriteStatus("", 0, lblStatusText)
        If dgvTable1.RowCount = 0 Then Exit Sub
        If dgvTable1.SelectedRows.Count <> 1 Then
            WriteStatus("You need to select 1 item from the list to use this function", 2, lblStatusText)
            Exit Sub
        End If
        If MessageBox.Show("This will permanently delete the selected Item from the database." & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
            WriteStatus("Delete cancelled", 0, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        ItemDelete()
        CursorControl()
    End Sub

    Private Sub btnSearchAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnSearchAddOrUpdate.Click
        WriteStatus("", 0, lblStatusText)
        If tblTable.TableName.Length < 1 Then Exit Sub
        If cbxSearch.Text.Length < 1 Then
            WriteStatus("The Name of the Search must be at least 1 character long", 2, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        SearchDelete(True)
        SearchAdd()
        If SaveXmlFile(xmlSearch, basCode.curVar.SearchFile, True) = False Then
            WriteStatus("The file " & basCode.curVar.SearchFile & " was not saved.", 1, lblStatusText)
        Else
            If cbxSearch.Items.Contains(cbxSearch.Text) = False Then cbxSearch.Items.Add(cbxSearch.Text)
            WriteStatus("Search saved", 0, lblStatusText)
        End If
        CursorControl()

        'SearchListLoad(tblTable.TableName)
    End Sub

    Private Sub btnDeleteSearch_Click(sender As Object, e As EventArgs) Handles btnDeleteSearch.Click
        WriteStatus("", 0, lblStatusText)
        If tblTable.TableName.Length < 1 Then Exit Sub
        If cbxSearch.Text.Length < 1 Then
            WriteStatus("The Name of the Search must be at least 1 character long", 2, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        SearchDelete(False)
        If SaveXmlFile(xmlSearch, basCode.curVar.SearchFile, True) = False Then
            WriteStatus("The file " & basCode.curVar.SearchFile & " was not saved.", 1, lblStatusText)
        Else
            WriteStatus("Search deleted", 0, lblStatusText)
        End If
        SearchListLoad(tblTable.TableName)
        FieldsClearAll(True)
        cbxSearch.SelectedIndex = -1
        cbxSearch.Text = ""
        CursorControl()
    End Sub

    Private Sub cbxSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxSearch.SelectedIndexChanged
        If basCode.curStatus.SuspendActions = False Then
            CursorControl("Wait")
            WriteStatus("", 0, lblStatusText)
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

        If basCode.dhdConnection.DataBaseOnline = True Then
            If basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search And tblTable.TableSearch = True Then
                btnSearch.Enabled = True
            End If
            If dgvTable1.SelectedRows.Count = 1 And basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit And tblTable.TableUpdate = True And (basCode.curVar.AllowUpdate = True Or basCode.curVar.SecurityOverride = True) Then
                btnUpdate.Enabled = True
            End If
            If tblTable.TableInsert = True And (basCode.curVar.AllowInsert = True Or basCode.curVar.SecurityOverride = True) Then
                btnAdd.Enabled = True
            End If
            If basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch Then
                btnSearch.Enabled = True
            End If
            If dgvTable1.SelectedRows.Count = 1 And basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit And (basCode.curVar.AllowUpdate = True Or basCode.curVar.SecurityOverride = True) Then
                btnUpdate.Enabled = True
            End If
            If dgvTable1.SelectedRows.Count = 1 And tblTable.TableDelete = True And (basCode.curVar.AllowDelete = True Or basCode.curVar.SecurityOverride = True) Then
                btnDelete.Enabled = True
            End If
        End If
    End Sub

    Private Sub FieldsClearAll(Optional ClearSearch As Boolean = False)
        If ClearSearch = True Then
            basCode.curStatus.SuspendActions = True
            cbxSearch.SelectedIndex = -1
            basCode.curStatus.SuspendActions = False
        End If
        dgvTable1.ClearSelection()
        FieldsClear()
        If basCode.curStatus.Status > 3 Then
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
        Else
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search
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

        Select Case basCode.curStatus.Status
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
        For intControl As Integer = sptFields1.Panel2.Controls.Count - 1 To 0 Step -1
            Dim strName As String = sptFields1.Panel2.Controls(intControl).Name
            For intField As Integer = 0 To basCode.basTable.Count - 1
                If basCode.basTable.Item(intField).Name = strName Then
                    Select Case basCode.basTable.Item(intField).Category
                        Case 1, 3, 4
                            If blnIdentityOnly = False Or basCode.basTable.Item(intField).Identity = True Or basCode.basTable.Item(intField).PrimaryKey = True Then
                                sptFields1.Panel2.Controls(intControl).Text = ""
                            End If
                        Case 2
                            Dim chkTemp As CheckBox = TryCast(sptFields1.Panel2.Controls(intControl), CheckBox)
                            If chkTemp IsNot Nothing Then chkTemp.Checked = False
                        Case 5, 6
                            If blnIdentityOnly = False Or basCode.basTable.Item(intField).Identity = True Or basCode.basTable.Item(intField).PrimaryKey = True Then
                                Dim msfTemp As ManagedSelectField = TryCast(sptFields1.Panel2.Controls(intControl), ManagedSelectField)
                                If msfTemp IsNot Nothing Then
                                    msfTemp.DropDown(2)
                                    msfTemp.Text = ""
                                End If
                            End If
                    End Select
                End If
            Next
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
        If basCode.dhdConnection.DataBaseOnline = False Then Exit Sub
        'If tblTable.TableName.Length < 1 Then Exit Sub
        basCode.curStatus.SuspendActions = True
        Dim strQuerySelect As String = SelectBuild()
        Dim strQueryOrder As String = OrderBuild()

        dgvTable1.Rows.Clear()
        lblListCountNumber.Text = 0

        strQuery = strQuerySelect
        strQuery &= " FROM [" & basCode.basTable.TableName.Replace(".", "].[") & "] " & basCode.basTable.TableAlias & " "
        strQuery &= " WHERE 1=1 "

        If blnRefine = True Then
            For intField As Integer = 0 To sptFields1.Panel2.Controls.Count - 1
                If sptFields1.Panel2.Controls(intField).BackColor = clrMarked Then
                    Dim strWhere As String = Nothing
                    Dim strValue As String = Nothing
                    If TypeOf sptFields1.Panel2.Controls(intField) Is CheckBox Then
                        Dim chkTemp As CheckBox = TryCast(sptFields1.Panel2.Controls(intField), CheckBox)
                        If chkTemp IsNot Nothing Then strValue = chkTemp.CheckState
                    Else
                        strValue = sptFields1.Panel2.Controls(intField).Text
                    End If
                    Dim strName As String = sptFields1.Panel2.Controls(intField).Name
                    For intCount As Integer = 0 To basCode.basTable.Count - 1
                        If basCode.basTable.Item(intField).Name = strName Then
                            strQuery &= " AND " & FormatFieldWhere1(basCode.basTable.Item(intField).FieldName, basCode.basTable.Item(intField).TableAlias, basCode.basTable.Item(intField).FieldWidth, basCode.basTable.Item(intField).FieldDataType, strValue)
                        End If
                    Next
                End If
            Next
        End If

        If strQueryOrder.Length > 0 Then strQuery = strQuery & " " & strQueryOrder

        dtsTable = Nothing
        dtsTable = basCode.QueryDb(basCode.dhdConnection, strQuery, True)
        If basCode.dhdText.DatasetCheck(dtsTable) = False Then Exit Sub
        If DataSet2DataGridView(dtsTable, 0, dgvTable1, False) = False Then Exit Sub

        dgvTable1.ClearSelection()
        lblListCountNumber.Text = dgvTable1.RowCount - 1
        basCode.curStatus.SuspendActions = False
    End Sub

    Private Function SelectBuild() As String
        Dim strMode As String = "SELECT "
        'Dim strAddField As String = ""
        strQuery = strMode & " "
        For intOrder As Integer = 0 To dgvTable1.Columns.Count
            For Each column In dgvTable1.Columns
                Dim strColumn As String = column.Name
                If intOrder = column.displayindex Then
                    For intField As Integer = 0 To basCode.basTable.Count - 1
                        If basCode.basTable.Item(intField).FieldName = column.Name Then
                            strQuery &= ", " & basCode.FormatField(basCode.basTable.TableName, basCode.basTable.TableAlias, basCode.basTable.Item(intField).FieldName, basCode.basTable.Item(intField).FieldAlias, basCode.basTable.Item(intField).FieldDataType, basCode.basTable.Item(intField).FieldWidth, Nothing, True, True, basCode.curVar.DateTimeStyle)
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
            If intColumnCount >= basCode.curVar.MaxColumnSort Then Exit For

            For Each column In dgvTable1.Columns
                If intOrder = column.displayindex And column.Visible = True Then
                    For intField As Integer = 0 To basCode.basTable.Count - 1
                        If basCode.basTable.Item(intField).FieldName = column.Name Then
                            Select Case basCode.basTable.Item(intField).FieldDataType
                                Case "BINARY", "XML", "GEO", "TEXT", "IMAGE"
                                    'No sort order
                                Case Else
                                    strQuery &= ", " & basCode.FormatField(basCode.basTable.TableName, basCode.basTable.TableAlias, basCode.basTable.Item(intField).FieldName, Nothing, basCode.basTable.Item(intField).FieldDataType, basCode.basTable.Item(intField).FieldWidth, Nothing, False, False, basCode.curVar.DateTimeStyle)
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
        If basCode.curStatus.SuspendActions = False Then
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
        Dim strQueryFrom As String = " FROM [" & basCode.basTable.TableName.Replace(".", "].[") & "] " & basCode.basTable.TableAlias & " "
        'strQuery = "SELECT TOP 1 ,"
        strQuery = "SELECT ,"
        For intField As Integer = 0 To basCode.basTable.Count - 1
            'strQuery &= ",COALESCE([" & tblTable.Item(intField).FieldName & "],'') AS [" & tblTable.Item(intField).FieldName & "]"
            'If basCode.basTable.Item(intField).TableName = basCode.basTable.TableName Then

            strQuery &= ", " & basCode.FormatField(basCode.basTable.TableName, basCode.basTable.TableAlias, basCode.basTable.Item(intField).FieldName, basCode.basTable.Item(intField).FieldAlias, basCode.basTable.Item(intField).FieldDataType, basCode.basTable.Item(intField).FieldWidth, Nothing, True, True, basCode.curVar.DateTimeStyle)

            If basCode.basTable.Item(intField).Count > 0 Then
                'Relations exist
                For intRel As Integer = 0 To basCode.basTable.Item(intField).Count - 1
                    If basCode.basTable.Item(intField).Item(intRel).RelatedField.Length > 0 Then
                        Dim strRelation As String = basCode.basTable.Item(intField).Item(intRel).RelationField
                        strQuery &= ",[" & basCode.basTable.Item(intField).Item(intRel).RelationTable.Replace(".", "].[") & "].[" & basCode.basTable.Item(intField).Item(intRel).RelatedFieldName & "] AS [" & basCode.basTable.Item(intField).Item(intRel).RelatedFieldAlias & "]"
                        strQueryFrom &= " LEFT OUTER JOIN " & basCode.basTable.Item(intField).Item(intRel).RelationTable & " ON " & "[" & basCode.basTable.TableAlias & "]." & basCode.basTable.Item(intField).FieldName & " = " & basCode.basTable.Item(intField).Item(intRel).RelationTable & "." & basCode.basTable.Item(intField).Item(intRel).RelationField
                    End If
                Next
            End If

            For Each cell In dgrSelection.Cells
                If cell.OwningColumn.HeaderText = basCode.basTable.Item(intField).FieldAlias Then
                    If Not cell.Value Is Nothing Then
                        If basCode.basTable.Item(intField).Identity = True Or basCode.basTable.Item(intField).PrimaryKey = True Then
                            'strQueryWhere &= " AND [" & tblTable.TableName.Replace(".", "].[") & "].[" & tblTable.Item(intField).FieldName & "] = " & SetDelimiters(cell.Value.ToString, tblTable.Item(intField).FieldDataType, "=")
                            strQueryWhere &= " AND " & basCode.FormatField(basCode.basTable.TableName, basCode.basTable.TableAlias, basCode.basTable.Item(intField).FieldName, Nothing, basCode.basTable.Item(intField).FieldDataType, basCode.basTable.Item(intField).FieldWidth, Nothing, False, False, basCode.curVar.DateTimeStyle) & " = " & basCode.SetDelimiters(cell.Value.ToString, basCode.basTable.Item(intField).FieldDataType, "=")

                        End If
                        'strQueryWhere2 &= " AND [" & tblTable.TableName.Replace(".", "].[") & "].[" & tblTable.Item(intField).FieldName & "] = " & SetDelimiters(cell.Value.ToString, tblTable.Item(intField).FieldDataType, "=")
                        strQueryWhere2 &= " AND " & basCode.FormatField(basCode.basTable.TableName, basCode.basTable.TableAlias, basCode.basTable.Item(intField).FieldName, Nothing, basCode.basTable.Item(intField).FieldDataType, basCode.basTable.Item(intField).FieldWidth, Nothing, False, False, basCode.curVar.DateTimeStyle) & " = " & basCode.SetDelimiters(cell.Value.ToString, basCode.basTable.Item(intField).FieldDataType, "=")
                    End If
                End If
            Next
            'End If
        Next
        strQuery &= strQueryFrom
        If strQueryWhere = " WHERE 1=1 " Then strQueryWhere &= strQueryWhere2
        If strQueryWhere = " WHERE 1=1 " Then Exit Sub
        strQuery &= strQueryWhere
        strQuery = strQuery.Replace(",,", " ")
        'If basCode.CurVar.DebugMode Then MessageBox.Show(strQuery)

        Dim objData As DataSet = basCode.QueryDb(basCode.dhdConnection, strQuery, True)
        If objData Is Nothing Then Exit Sub
        If objData.Tables.Count = 0 Then Exit Sub
        If objData.Tables(0).Rows.Count = 0 Then Exit Sub
        basCode.curStatus.SuspendActions = True
        Try
            For intField As Integer = 0 To basCode.basTable.Count - 1
                For Each ctrl As Control In sptFields1.Panel2.Controls
                    If basCode.basTable.Item(intField).Name = ctrl.Name Then
                        If objData.Tables.Item(0).Rows(0).Item(basCode.basTable.Item(intField).FieldAlias).GetType().ToString = "System.DBNull" Then
                            ctrl.BackColor = clrEmpty
                            ctrl.Tag = ""
                        Else
                            Select Case basCode.basTable.Item(intField).Category
                                Case 1, 3, 4
                                    ctrl.Text = objData.Tables.Item(0).Rows(0).Item(basCode.basTable.Item(intField).FieldAlias)
                                Case 2
                                    Dim chkTemp As CheckBox = TryCast(ctrl, CheckBox)
                                    If chkTemp IsNot Nothing Then chkTemp.Checked = objData.Tables.Item(0).Rows(0).Item(basCode.basTable.Item(intField).FieldAlias)
                                Case 5, 6
                                    Dim msfTemp As ManagedSelectField = TryCast(ctrl, ManagedSelectField)
                                    msfTemp.Text = objData.Tables.Item(0).Rows(0).Item(basCode.basTable.Item(intField).FieldAlias)
                                    msfTemp.DropDown(2)
                            End Select
                            ctrl.Tag = objData.Tables.Item(0).Rows(0).Item(basCode.basTable.Item(intField).FieldAlias).ToString
                        End If
                    End If
                Next
            Next

            'End If
            'Next
            If objData.Tables(0).Rows.Count > 1 Then lblMultipleRows.Visible = True

        Catch ex As Exception
            WriteStatus("There was an error loading the data. please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error loading the data." & Environment.NewLine & ex.Message, 1)

            basCode.curStatus.SuspendActions = False
        End Try
        basCode.curStatus.SuspendActions = False
        If basCode.curStatus.Status > 3 Then
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
        Else
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
        End If
        'ButtonHandle()
        FieldsEnable()
    End Sub

    Private Sub LoadSearchCriteria(Optional strCriterium As String = "")
        If basCode.dhdConnection.DataBaseOnline = True Then
            Dim strQuery2 As String = " WHERE 1=1 ", strQuery3 As String = ""
            For intField As Integer = 0 To tblTable.Count - 1
                strQuery = ""
                strQuery2 = " WHERE 1=1 "
                strQuery3 = ""
                If tblTable.Item(intField).FieldSearchList = True Then
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
                Dim strValue As String = basCode.ProcessDefaultValue(Field.DefaultValue)
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

        If basCode.curVar.DebugMode Then
            If MessageBox.Show("The query to be executed is: " & Environment.NewLine & strQuery & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                WriteStatus("Insert cancelled", 0, lblStatusText)
                Exit Sub
            End If
        End If
        WriteStatus("Inserting Record", 0, lblStatusText)

        Try
            Dim dtsData As DataSet
            dtsData = basCode.QueryDb(basCode.dhdConnection, strQuery, 0)
            If basCode.dhdConnection.ErrorLevel = -1 Then
                WriteStatus(basCode.dhdConnection.ErrorMessage, 1, lblStatusText)
            Else
                WriteStatus(basCode.dhdConnection.RowsAffected & " Record(s) Inserted.", 0, lblStatusText)
            End If
        Catch ex As Exception
            basCode.WriteLog("There was an error inserting the record: " & Environment.NewLine & ex.Message, 1)
            WriteStatus("There was an error inserting the record. Please check the logfile.", 0, lblStatusText)
        End Try

        If basCode.curStatus.Status > 3 Then
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
        Else
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
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
                        strQueryWhere &= " AND [" & tblTable.Item(intField).FieldName & "] = " & basCode.SetDelimiters(cell.Value, tblTable.Item(intField).FieldDataType, "=")
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

        If basCode.curVar.DebugMode Then
            If MessageBox.Show("The query to be executed is: " & Environment.NewLine & strQuery & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                WriteStatus("Update cancelled", 0, lblStatusText)
                Exit Sub
            End If
        End If
        WriteStatus("Updating Record", 0, lblStatusText)

        Try
            Dim dtsData As DataSet
            dtsData = basCode.QueryDb(basCode.dhdConnection, strQuery, 0)
            If basCode.dhdConnection.ErrorLevel = -1 Then
                WriteStatus(basCode.dhdConnection.ErrorMessage, 1, lblStatusText)
            Else
                WriteStatus(basCode.dhdConnection.RowsAffected & " Record(s) Updated.", 0, lblStatusText)
            End If
        Catch ex As Exception
            WriteStatus("There was an error updating the record. Please check the logfile.", 0, lblStatusText)
            basCode.WriteLog("There was an error updating the record: " & Environment.NewLine & ex.Message, 1)
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
                        strQueryWhere &= " AND [" & tblTable.Item(intField).FieldName & "] = " & basCode.SetDelimiters(cell.Value, tblTable.Item(intField).FieldDataType, "=")
                    End If
                Next
            End If

        Next
        If strQueryWhere = " WHERE 1=1 " Then
            WriteStatus("You need an AUTOID or Primary Key field in order to delete an item", 2, lblStatusText)
            Exit Sub
        End If
        strQuery = strQuery & strQueryWhere

        If basCode.curVar.DebugMode Then
            If MessageBox.Show("The query to be executed is: " & Environment.NewLine & strQuery & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strAreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                WriteStatus("Delete cancelled", 0, lblStatusText)
                Exit Sub
            End If
        End If
        WriteStatus("Deleting Record", 0, lblStatusText)

        Try
            Dim dtsData As DataSet
            dtsData = basCode.QueryDb(basCode.dhdConnection, strQuery, 0)
            WriteStatus("Record Deleted", 0, lblStatusText)
        Catch ex As Exception
            WriteStatus("There was an error deleting the record. Please check the logfile", 1, lblStatusText)
            basCode.WriteLog("There was an error deleting the record: " & Environment.NewLine & ex.Message, 1)
        End Try

        If basCode.curStatus.Status > 3 Then
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit
        Else
            basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit
        End If
        dgvTable1.Rows.Remove(dgvTable1.SelectedRows(0))
        ColorReset()
        ButtonHandle()
    End Sub

    Private Sub SearchAdd()

        Dim root As XmlElement = xmlSearch.DocumentElement
        If root Is Nothing Then
            xmlTables = basCode.dhdText.CreateRootDocument(xmlSearch, "Sequenchel", "Searches", True)
        End If

        Dim NewNode As XmlNode = basCode.dhdText.CreateAppendElement(xmlSearch.Item("Sequenchel").Item("Searches"), "Search")
        basCode.dhdText.CreateAppendElement(NewNode, "TableName", tblTable.TableName)
        basCode.dhdText.CreateAppendElement(NewNode, "SearchName", cbxSearch.Text)

        basCode.dhdText.CreateAppendElement(NewNode, "UseTop", chkUseTop.Checked)
        basCode.dhdText.CreateAppendElement(NewNode, "UseTopCount", txtUseTop.Text)
        basCode.dhdText.CreateAppendElement(NewNode, "ReversedOrder", chkReversedSortOrder.Checked)

        Dim FieldsNode As XmlNode = basCode.dhdText.CreateAppendElement(NewNode, "Fields", "")

        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).BackColor = clrMarked Then

                Dim newAttribute1 As XmlAttribute = xmlSearch.CreateAttribute("FieldName")
                newAttribute1.Value = tblTable.Item(intField).FieldName

                Select Case tblTable.Item(intField).FieldDataType.ToUpper
                    Case "BIT"
                        Dim NewFieldNode As XmlNode = basCode.dhdText.CreateAppendElement(FieldsNode, "Field", tblTable.Item(intField).CheckState)
                        NewFieldNode.Attributes.Append(newAttribute1)
                    Case Else
                        Dim NewFieldNode As XmlNode = basCode.dhdText.CreateAppendElement(FieldsNode, "Field", tblTable.Item(intField).Text)
                        NewFieldNode.Attributes.Append(newAttribute1)
                End Select
            End If
        Next
    End Sub

    Private Sub SearchListLoad(strTable As String)
        cbxSearch.Text = ""
        cbxSearch.Items.Clear()
        Dim lstSearches As List(Of String) = basCode.LoadSearches(xmlSearch, strTable)
        If lstSearches Is Nothing Then Exit Sub
        For Each lstItem As String In lstSearches
            cbxSearch.Items.Add(lstItem)
        Next
    End Sub

    Private Sub SearchDelete(Optional UpdateMode As Boolean = False)
        Dim strSelection As String = cbxSearch.Text

        If strSelection.Length = 0 Then Exit Sub
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xmlSearch, "Search", "SearchName", strSelection)
        If Not xNode Is Nothing Then
            If UpdateMode = False Then
                If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
            End If
            xNode.ParentNode.RemoveChild(xNode)
        End If

    End Sub

    Private Sub SearchLoad()
        Dim strSelection As String = cbxSearch.Text
        Dim strFieldName As String = "", strFieldValue As String = ""

        If strSelection.Length = 0 Then Exit Sub
        Dim xPNode As XmlNode = basCode.dhdText.FindXmlNode(xmlSearch, "Search", "SearchName", strSelection)
        If Not xPNode Is Nothing Then

            If basCode.dhdText.CheckNodeElement(xPNode, "UseTop") Then chkUseTop.Checked = xPNode.Item("UseTop").InnerText
            If basCode.dhdText.CheckNodeElement(xPNode, "UseTopCount") Then txtUseTop.Text = xPNode.Item("UseTopCount").InnerText
            If basCode.dhdText.CheckNodeElement(xPNode, "ReversedOrder") Then chkReversedSortOrder.Checked = xPNode.Item("ReversedOrder").InnerText

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
