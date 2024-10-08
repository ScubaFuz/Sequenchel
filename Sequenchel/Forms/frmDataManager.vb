﻿Imports System.IO
Imports System.Xml

Public Class frmDataManager
    Private dtsTable As New DataSet

    Private Sub frmSequenchel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        'dgvTable1.BackgroundImageLayout = ImageLayout.Center

        basCode.SetDefaults()
        If My.Application.CommandLineArgs.Count > 0 Then basCode.ParseCommands(My.Application.CommandLineArgs)
        Me.Text = My.Application.Info.Title
        If basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch Then Me.Text &= " ControlMode"
        If basCode.curVar.DebugMode Then Me.Text &= " Debug"
        If basCode.curVar.DevMode Then Me.Text &= " Development"
        If basCode.curVar.Encryption = False Then Me.Text &= " NoEncryption"

        DebugSettings()
        LoadLicense(lblStatusText)
        Me.Hide()
        frmAbout.Show()
        frmAbout.Refresh()

        lblLicense.Text = "Licensed to: " & basCode.curVar.LicenseName
        lblLicense.Left = Me.Width - lblLicense.Width - (basCode.curVar.BuildMargin * 5)

        If basCode.LoadSDBASettingsXml(basCode.xmlSDBASettings) = False Then
            If basCode.SaveSDBASettingsXml(basCode.xmlSDBASettings) = False Then WriteStatus("There was an error loading the main settings. please check the logfile", 1, lblStatusText)
        End If
        SetTimer()
        SecuritySet()
        If basCode.LoadGeneralSettingsXml(basCode.xmlGeneralSettings) = False Then WriteStatus("There was an error loading the settings. please check the logfile", 1, lblStatusText)
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
                    basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
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
                    basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
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
                    LoadTable(basCode.curStatus.Table)
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
        'MessageBox.Show(My.Application.Info.AssemblyName)

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
            basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
            basCode.curStatus.TableSet = ""
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
        If cbxTableSet.SelectedIndex > -1 Then
            CursorControl("Wait")
            basCode.curStatus.TableSet = cbxTableSet.SelectedItem
            basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
            basCode.curStatus.Table = ""
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
        If cbxTable.SelectedIndex > -1 Then
            CursorControl("Wait")
            basCode.curStatus.Table = cbxTable.SelectedItem
            LoadTable(basCode.curStatus.Table)
            SortColumns()
            SearchListLoad()
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

    Private Sub btnDefault_Click(sender As Object, e As EventArgs)
        WriteStatus("", 0, lblStatusText)
        Dim strDefault As String = sender.name.ToString.Substring(3, sender.name.ToString.Length - 3)
        LoadDefaultValue(strDefault)
    End Sub

#End Region

    Private Sub AllClear(intLevel As Integer)
        If intLevel >= 4 Then
            If basCode.curStatus.ConnectionsReload = True Then basCode.xmlConnections.RemoveAll()
            basCode.curVar.TableSetsFile = ""
            cbxConnection.Items.Clear()
            cbxConnection.Text = ""
        End If
        If intLevel >= 3 Then
            If basCode.curStatus.TableSetsReload = True Then basCode.xmlTableSets.RemoveAll()
            basCode.curVar.TablesFile = ""
            cbxTableSet.Items.Clear()
            cbxTableSet.Text = ""
        End If
        If intLevel >= 2 Then
            If basCode.curStatus.TablesReload = True Then basCode.xmlTables.RemoveAll()
            cbxTable.Items.Clear()
            cbxTable.Text = ""
        End If
        If intLevel >= 1 Then
            FieldsDispose()
        End If
    End Sub

    Private Sub FieldsDispose()
        basCode.curStatus.SuspendActions = True

        'TableClear()
        basCode.TableClear()
        'arrLabels.Clear()
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
        Dim lstConnections As List(Of String) = basCode.LoadConnections(basCode.xmlConnections)
        If lstConnections Is Nothing OrElse lstConnections.Count = 0 Then
            basCode.UseMainDB()
            Exit Sub
        End If
        For Each lstItem As String In lstConnections
            cbxConnection.Items.Add(lstItem)
        Next
        cbxConnection.SelectedItem = basCode.curStatus.Connection
    End Sub

    Private Sub LoadTableSets()
        AllClear(3)
        Dim lstTableSets As List(Of String) = basCode.LoadTableSets(basCode.xmlTableSets)
        If lstTableSets Is Nothing Then Exit Sub
        For Each lstItem As String In lstTableSets
            cbxTableSet.Items.Add(lstItem)
        Next
        cbxTableSet.SelectedItem = basCode.curStatus.TableSet
    End Sub

    Private Sub LoadTables()
        AllClear(2)
        Dim lstTables As List(Of String) = basCode.LoadTables(basCode.xmlTables, False)
        If lstTables Is Nothing Then Exit Sub
        For Each lstItem As String In lstTables
            cbxTable.Items.Add(lstItem)
        Next
        cbxTable.SelectedItem = basCode.curStatus.Table
    End Sub

    Friend Sub LoadTable(strTable As String)
        Try
            FieldsDispose()
            FieldsClearAll(True)
            If basCode.LoadTable(basCode.xmlTables, strTable) = False Then
                WriteStatus("Loading the table " & strTable & " failed. Check your configuration", 2, lblStatusText)
                Exit Sub
            End If

            For intCount = 0 To basCode.basTable.Count - 1
                Dim fldField As Object = Nothing
                Select Case basCode.basTable.Item(intCount).Category
                    Case 1
                        fldField = New TextField
                        Dim txtTemp As TextField = TryCast(fldField, TextField)
                        If txtTemp IsNot Nothing Then AddHandler txtTemp.TextChanged, AddressOf TextHandler
                        fldField.Tag = ""
                        fldField.Field = basCode.basTable.Item(intCount)
                    Case 2
                        fldField = New CheckField
                        Dim chkTemp As CheckField = TryCast(fldField, CheckField)
                        If chkTemp IsNot Nothing Then AddHandler chkTemp.CheckedChanged, AddressOf TextHandler
                        fldField.Tag = chkTemp.Checked.ToString
                        fldField.Field = basCode.basTable.Item(intCount)
                    Case 5
                        fldField = New ManagedSelectField
                        fldField.DataConn.DataLocation = basCode.dhdConnection.DataLocation
                        fldField.DataConn.DatabaseName = basCode.dhdConnection.DatabaseName
                        fldField.DataConn.DataProvider = basCode.dhdConnection.DataProvider
                        fldField.DataConn.LoginMethod = basCode.dhdConnection.LoginMethod
                        fldField.DataConn.LoginName = basCode.dhdConnection.LoginName
                        fldField.DataConn.Password = basCode.dhdConnection.Password
                        fldField.Table = basCode.basTable.TableName
                        fldField.SearchField = basCode.basTable.Item(intCount).Name
                        fldField.SearchFieldAlias = basCode.basTable.Item(intCount).FieldAlias
                        Dim mslTemp As ManagedSelectField = TryCast(fldField, ManagedSelectField)
                        If mslTemp IsNot Nothing Then AddHandler mslTemp.TextChanged, AddressOf TextHandler
                        If mslTemp IsNot Nothing Then AddHandler mslTemp.ValueChanged, AddressOf TextHandler
                        fldField.Tag = ""
                        fldField.Field = basCode.basTable.Item(intCount)
                    Case 6
                        fldField = New ManagedSelectField
                End Select
                fldField.Name = basCode.basTable.Item(intCount).Name


                If basCode.basTable.Item(intCount).FieldVisible = True Then
                    sptFields1.Panel2.Controls.Add(fldField)
                    'fldField.Top = ((sptFields1.Panel2.Controls.Count - 1) * basCode.curVar.FieldHeight) + (sptFields1.Panel2.Controls.Count * basCode.curVar.BuildMargin)
                    If sptFields1.Panel2.Controls.Count > 1 Then
                        fldField.Top = sptFields1.Panel2.Controls(sptFields1.Panel2.Controls.Count - 2).Top + basCode.curVar.FieldHeight + basCode.curVar.BuildMargin
                    Else
                        fldField.top = basCode.curVar.BuildMargin
                    End If
                    fldField.Width = basCode.basTable.Item(intCount).FieldWidth

                    If fldField.top > sptFields1.Panel2.Height And fldField.Width >= sptFields1.Panel2.Width - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth Then
                        fldField.width = sptFields1.Panel2.Width - (basCode.curVar.BuildMargin * 3) - SystemInformation.VerticalScrollBarWidth
                    End If
                    Dim lblLabel As New LabelField
                    lblLabel.Field = New SeqCore.BaseField
                    'arrLabels.Add(lblLabel)
                    AddHandler lblLabel.Click, AddressOf LabelClickHandler

                    lblLabel.Name = "lbl" & fldField.Name
                    lblLabel.Field.Name = lblLabel.Name
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

                    If basCode.basTable.Item(intCount).DefaultButton = True Then
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
                        For intRel As Integer = 0 To basCode.basTable.Item(intCount).Count - 1
                            If basCode.basTable.Item(intCount).Item(intRel).RelatedFieldName.ToString.Length > 0 Then
                                Dim msfRelatedField As New ManagedSelectField
                                If msfRelatedField IsNot Nothing Then AddHandler msfRelatedField.ValueChanged, AddressOf TextHandler
                                msfRelatedField.Name = basCode.basTable.Item(intCount).Item(intRel).RelationTableAlias & "." & basCode.basTable.Item(intCount).Item(intRel).RelationField
                                msfRelatedField.Tag = ""

                                msfRelatedField.DataConn.DataLocation = basCode.dhdConnection.DataLocation
                                msfRelatedField.DataConn.DatabaseName = basCode.dhdConnection.DatabaseName
                                msfRelatedField.DataConn.DataProvider = basCode.dhdConnection.DataProvider
                                msfRelatedField.DataConn.LoginMethod = basCode.dhdConnection.LoginMethod
                                msfRelatedField.DataConn.LoginName = basCode.dhdConnection.LoginName
                                msfRelatedField.DataConn.Password = basCode.dhdConnection.Password
                                msfRelatedField.Table = basCode.basTable.Item(intCount).Item(intRel).RelationTable
                                msfRelatedField.SearchField = basCode.basTable.Item(intCount).Item(intRel).RelatedFieldName
                                msfRelatedField.SearchFieldAlias = basCode.basTable.Item(intCount).Item(intRel).RelatedFieldAlias
                                msfRelatedField.IdentifierField = basCode.basTable.Item(intCount).Item(intRel).RelationField

                                msfRelatedField.Field = New SeqCore.BaseField
                                msfRelatedField.Field.Category = 6
                                msfRelatedField.Field.Name = msfRelatedField.Name
                                msfRelatedField.Field.FieldAlias = basCode.basTable.Item(intCount).FieldAlias
                                msfRelatedField.Field.FieldName = msfRelatedField.IdentifierField
                                msfRelatedField.Field.FieldTableName = msfRelatedField.Table
                                msfRelatedField.Field.FieldTableAlias = basCode.GetTableAliasFromName(basCode.xmlTables, msfRelatedField.Field.FieldTableName)
                                msfRelatedField.Field.FieldDataType = basCode.basTable.Item(intCount).FieldDataType
                                msfRelatedField.Field.FieldVisible = basCode.basTable.Item(intCount).FieldVisible
                                msfRelatedField.Field.FieldSearch = basCode.basTable.Item(intCount).FieldSearch
                                msfRelatedField.Field.FieldUpdate = basCode.basTable.Item(intCount).FieldUpdate

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
                                Exit For
                            End If
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
            ButtonHandle()

        Catch ex As Exception
            WriteStatus("There was an error reading the Table file. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error reading the Table file. Please check the file for Table: " & Environment.NewLine & strTable & Environment.NewLine & ex.Message, 1)
        End Try
    End Sub

    Public Sub TextHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        FieldTextHandler(sender)
    End Sub

    Public Sub LabelClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lblTemp As Label = sender
        For Each ctrl As Object In sptFields1.Panel2.Controls
            If ctrl.Name = lblTemp.Name.Substring(3, lblTemp.Name.Length - 3) Then
                If ctrl.Enabled = True Then
                    If ctrl.BackColor = clrMarked Then
                        If ctrl.Field.FieldDataType = "BIT" Then
                            ctrl.backColor = clrControl
                        Else
                            ctrl.backColor = clrOriginal
                        End If
                    Else
                        ctrl.BackColor = clrMarked
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub SortColumns()
        Dim intColumns As Integer = dgvTable1.Columns.Count
        Dim intColumn As Integer = 0
        For intField As Integer = 0 To basCode.basTable.Count - 1
            If basCode.basTable.Item(intField).FieldList = True Or basCode.basTable.Item(intField).Identity = True Or basCode.basTable.Item(intField).PrimaryKey = True Then
                For Each column In dgvTable1.Columns
                    If column.name = basCode.basTable.Item(intField).FieldName Then
                        Dim intColumnIndex As Integer = basCode.basTable.Item(intField).FieldListOrder
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
        If basCode.basTable.TableName.Length < 1 Then Exit Sub
        If cbxSearch.Text.Length < 1 Then
            WriteStatus("The Name of the Search must be at least 1 character long", 2, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        SearchDelete(True)
        SearchAdd()
        If SaveXmlFile(basCode.xmlSearch, basCode.curVar.SearchFile, True) = False Then
            WriteStatus("The file " & basCode.curVar.SearchFile & " was not saved.", 1, lblStatusText)
        Else
            If cbxSearch.Items.Contains(cbxSearch.Text) = False Then cbxSearch.Items.Add(cbxSearch.Text)
            basCode.curStatus.SearchesReload = True
            WriteStatus("Search saved", 0, lblStatusText)
        End If
        CursorControl()

        'SearchListLoad(tblTable.TableName)
    End Sub

    Private Sub btnDeleteSearch_Click(sender As Object, e As EventArgs) Handles btnDeleteSearch.Click
        WriteStatus("", 0, lblStatusText)
        If basCode.basTable.TableName.Length < 1 Then Exit Sub
        If cbxSearch.Text.Length < 1 Then
            WriteStatus("Select a Search to delete.", 2, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        Dim blnDeleted As Boolean = SearchDelete(False)
        If blnDeleted = False Then
            WriteStatus("Search " & cbxSearch.Text & " was not deleted", 2, lblStatusText)
            CursorControl()
            Exit Sub
        End If
        If SaveXmlFile(basCode.xmlSearch, basCode.curVar.SearchFile, True) = False Then
            WriteStatus("The file " & basCode.curVar.SearchFile & " was not saved.", 1, lblStatusText)
        Else
            WriteStatus("Search deleted", 0, lblStatusText)
        End If
        SearchListLoad()
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
            ButtonHandle()
            CursorControl()
        End If
    End Sub

#End Region

    Private Sub ButtonHandle()
        btnSearch.Enabled = False
        btnUpdate.Enabled = False
        btnAdd.Enabled = False
        btnDelete.Enabled = False

        'If basCode.dhdConnection.DataBaseOnline = True Then
        If basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search And basCode.basTable.TableSearch = True Then
            btnSearch.Enabled = True
        End If
        If dgvTable1.SelectedRows.Count = 1 And basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.Edit And basCode.basTable.TableUpdate = True And (basCode.curVar.AllowUpdate = True Or basCode.curVar.SecurityOverride = True) Then
            btnUpdate.Enabled = True
        End If
        If basCode.basTable.TableInsert = True And (basCode.curVar.AllowInsert = True Or basCode.curVar.SecurityOverride = True) Then
            btnAdd.Enabled = True
        End If
        If basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch Then
            btnSearch.Enabled = True
        End If
        If dgvTable1.SelectedRows.Count = 1 And basCode.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlEdit And (basCode.curVar.AllowUpdate = True Or basCode.curVar.SecurityOverride = True) Then
            btnUpdate.Enabled = True
        End If
        If dgvTable1.SelectedRows.Count = 1 And basCode.basTable.TableDelete = True And (basCode.curVar.AllowDelete = True Or basCode.curVar.SecurityOverride = True) Then
            btnDelete.Enabled = True
        End If
        'End If
    End Sub

    Private Sub FieldsClearAll(Optional ClearSearch As Boolean = False)
        If ClearSearch = True Then
            basCode.curStatus.SuspendActions = True
            cbxSearch.SelectedIndex = -1
            cbxSearch.Text = ""
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
        For Each ctrl As Object In sptFields1.Panel2.Controls
            FieldEnableHandler(ctrl, False)
        Next
    End Sub

    Private Sub FieldsEnable(Optional blnEnableIdentity As Boolean = True)

        Select Case basCode.curStatus.Status
            Case SeqCore.CurrentStatus.StatusList.Search, SeqCore.CurrentStatus.StatusList.ControlSearch
                For Each ctrl As Object In sptFields1.Panel2.Controls
                    FieldEnableHandler(ctrl, ctrl.Field.FieldSearch)
                Next
            Case SeqCore.CurrentStatus.StatusList.Edit
                FieldsDisable()
                If basCode.basTable.TableUpdate = True Then
                    For Each ctrl As Object In sptFields1.Panel2.Controls
                        Dim strControl As String = ctrl.Field.ControlField
                        Dim strControlValue As String = ctrl.Field.ControlValue
                        If strControl.Length > 0 Then
                            For Each ctrl2 As Object In sptFields1.Panel2.Controls
                                If ctrl2.Field.FieldName = strControl Then
                                    Select Case ctrl2.Field.Category
                                        Case 1, 3, 4, 5, 6
                                            If ctrl2.Text = strControlValue Then
                                                FieldEnableHandler(ctrl, ctrl.Field.ControlUpdate)
                                            End If
                                        Case 2
                                            If ctrl2.Checked = strControlValue Then
                                                FieldEnableHandler(ctrl, ctrl.Field.ControlUpdate)
                                            End If
                                    End Select

                                    Exit For
                                End If
                            Next
                        Else
                            FieldEnableHandler(ctrl, ctrl.Field.FieldUpdate)
                        End If
                    Next
                End If
            Case SeqCore.CurrentStatus.StatusList.Add, SeqCore.CurrentStatus.StatusList.ControlAdd
                For Each ctrl As Object In sptFields1.Panel2.Controls
                    If ctrl.Field.Identity = False Or blnEnableIdentity = True Then
                        FieldEnableHandler(ctrl, ctrl.Field.FieldSearch)
                    End If
                Next
            Case SeqCore.CurrentStatus.StatusList.ControlEdit
                For Each ctrl As Object In sptFields1.Panel2.Controls
                    FieldEnableHandler(ctrl, ctrl.Field.ControlMode)
                Next
        End Select
        ColorReset()
    End Sub

    Private Sub FieldsClear(Optional blnIdentityOnly As Boolean = False)
        TagsClear()
        For intControl As Integer = sptFields1.Panel2.Controls.Count - 1 To 0 Step -1

            Dim fldField As Object = sptFields1.Panel2.Controls(intControl)
            Dim intCategory As Integer = basCode.GetCategory(fldField)

            Select Case intCategory
                Case 1, 3, 4
                    If blnIdentityOnly = False Or fldField.Field.Identity = True Or fldField.Field.PrimaryKey = True Then
                        fldField.Text = ""
                    End If
                Case 2
                    fldField.Checked = False
                Case 5, 6
                    If blnIdentityOnly = False Or fldField.Field.Identity = True Or fldField.Field.PrimaryKey = True Then
                        fldField.DropDown(2)
                        fldField.Text = ""
                        fldField.Value = ""
                    End If
            End Select
            '    End If
            'Next
        Next
        btnAdd.Text = "New Item"
        btnAdd.BackColor = clrControl
        lblMultipleRows.Visible = False
    End Sub

    Private Sub TagsClear()
        For intControl As Integer = sptFields1.Panel2.Controls.Count - 1 To 0 Step -1

            Dim fldField As Object = sptFields1.Panel2.Controls(intControl)
            Dim intCategory As Integer = basCode.GetCategory(fldField)

            Select Case intCategory
                Case 1, 3, 4, 5, 6
                    fldField.Tag = ""
                Case 2
                    fldField.Tag = False
            End Select
        Next
    End Sub

    Friend Sub LoadList(blnRefine As Boolean)
        'If basCode.dhdConnection.DataBaseOnline = False Then Exit Sub
        basCode.curStatus.SuspendActions = True
        dgvTable1.Rows.Clear()
        lblListCountNumber.Text = 0

        Dim intTop As Integer = -1
        If chkUseTop.Checked = True AndAlso IsNumeric(txtUseTop.Text) Then intTop = txtUseTop.Text
        Dim strQuerySelect As String = basCode.MainSelectBuild(intTop)
        Dim strQueryOrder As String = basCode.MainOrderBuild(chkReversedSortOrder.Checked)

        strQuery = strQuerySelect
        strQuery &= " FROM [" & basCode.basTable.TableName.Replace(".", "].[") & "] [" & basCode.basTable.TableAlias & "] "
        strQuery &= " WHERE 1=1 "

        If blnRefine = True Then
            For intField As Integer = 0 To sptFields1.Panel2.Controls.Count - 1
                Dim fldField As Object = sptFields1.Panel2.Controls(intField)
                If sptFields1.Panel2.Controls(intField).BackColor = clrMarked Then
                    'Dim strWhere As String = Nothing
                    Dim strValue As String = Nothing
                    If TypeOf fldField Is CheckField Then
                        Dim chkTemp As CheckField = TryCast(fldField, CheckField)
                        If chkTemp IsNot Nothing Then strValue = chkTemp.CheckState
                    Else
                        strValue = fldField.Text
                    End If
                    Dim strName As String = fldField.Name
                    strQuery &= " AND " & basCode.FormatFieldWhere(fldField.Field.FieldName, fldField.Field.FieldTableAlias, fldField.Field.FieldWidth, fldField.Field.FieldDataType, strValue)
                End If
            Next
        End If

        If strQueryOrder.Length > 8 Then strQuery = strQuery & " " & strQueryOrder

        dtsTable = Nothing
        dtsTable = basCode.QueryDb(basCode.dhdConnection, strQuery, True)
        If basCode.dhdText.DatasetCheck(dtsTable) = False Then Exit Sub
        If DataSet2DataGridView(dtsTable, 0, dgvTable1, False, 0) = False Then Exit Sub

        dgvTable1.ClearSelection()
        lblListCountNumber.Text = dtsTable.Tables(0).Rows.Count
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

    Private Sub ItemSelect()
        If basCode.curStatus.SuspendActions = False Then
            FieldsClear()
            If dgvTable1.SelectedRows.Count = 1 Then
                LoadItem(dgvTable1.SelectedRows(0))
            End If
            ButtonHandle()
        End If
    End Sub

    Private Sub LoadItem(dgrSelection As DataGridViewRow)
        Dim strQueryWhere As String = " WHERE 1=1 "
        Dim strQueryWhere2 As String = ""
        Dim strQueryFrom As String = " FROM [" & basCode.basTable.TableName.Replace(".", "].[") & "] [" & basCode.basTable.TableAlias & "] "
        'strQuery = "SELECT TOP 1 ,"
        strQuery = "SELECT ,"
        For intField As Integer = 0 To basCode.basTable.Count - 1
            'strQuery &= ",COALESCE([" & tblTable.Item(intField).FieldName & "],'') AS [" & tblTable.Item(intField).FieldName & "]"
            'If basCode.basTable.Item(intField).TableName = basCode.basTable.TableName Then

            strQuery &= ", " & basCode.FormatField(basCode.basTable.TableName, basCode.basTable.TableAlias, basCode.basTable.Item(intField).FieldName, basCode.basTable.Item(intField).FieldAlias, basCode.basTable.Item(intField).FieldDataType, basCode.basTable.Item(intField).FieldWidth, Nothing, True, True, basCode.curVar.DateTimeStyle)

            If basCode.basTable.Item(intField).Count > 0 Then
                'Relations exist
                For intRel As Integer = 0 To basCode.basTable.Item(intField).Count - 1
                    If basCode.basTable.Item(intField).Item(intRel).RelatedFieldName.Length > 0 Then
                        Dim strRelation As String = basCode.basTable.Item(intField).Item(intRel).RelationField
                        strQuery &= ",[" & basCode.basTable.Item(intField).Item(intRel).RelationTableAlias & "].[" & basCode.basTable.Item(intField).Item(intRel).RelatedFieldName & "] AS [" & basCode.basTable.Item(intField).Item(intRel).RelatedFieldAlias & "]"
                        strQueryFrom &= " LEFT OUTER JOIN " & basCode.basTable.Item(intField).Item(intRel).RelationTable & " " & basCode.basTable.Item(intField).Item(intRel).RelationTableAlias & " ON " & "[" & basCode.basTable.TableAlias & "]." & basCode.basTable.Item(intField).FieldName & " = " & basCode.basTable.Item(intField).Item(intRel).RelationTableAlias & "." & basCode.basTable.Item(intField).Item(intRel).RelationField
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
            For Each ctrl As Object In sptFields1.Panel2.Controls
                Dim intCategory As Integer
                Select Case ctrl.[GetType]().Name
                    Case "TextField"
                        intCategory = ctrl.Field.Category
                    Case "CheckField"
                        intCategory = ctrl.Field.Category
                    Case "ComboField"
                        'not used
                    Case "ManagedSelectField"
                        intCategory = ctrl.Field.Category
                    Case Else
                        'Not a type with a category.
                        intCategory = 0
                End Select

                If intCategory > 0 Then
                    If objData.Tables.Item(0).Rows(0).Item(ctrl.Field.FieldAlias).GetType().ToString = "System.DBNull" Then
                        ctrl.BackColor = clrEmpty
                        ctrl.Tag = ""
                    Else
                        Select Case intCategory
                            Case 1, 3, 4
                                ctrl.Text = objData.Tables.Item(0).Rows(0).Item(ctrl.Field.FieldAlias)
                            Case 2
                                Dim chkTemp As CheckBox = TryCast(ctrl, CheckBox)
                                If chkTemp IsNot Nothing Then chkTemp.Checked = objData.Tables.Item(0).Rows(0).Item(ctrl.Field.FieldAlias)
                            Case 5
                                Dim msfTemp As ManagedSelectField = TryCast(ctrl, ManagedSelectField)
                                msfTemp.SuspendActions = True
                                msfTemp.Text = objData.Tables.Item(0).Rows(0).Item(ctrl.Field.FieldAlias)
                                msfTemp.SuspendActions = False
                            Case 6
                                Dim msfTemp As ManagedSelectField = TryCast(ctrl, ManagedSelectField)
                                msfTemp.SuspendActions = True
                                msfTemp.Text = objData.Tables.Item(0).Rows(0).Item(ctrl.SearchFieldAlias)
                                msfTemp.SuspendActions = False
                        End Select
                        ctrl.Tag = objData.Tables.Item(0).Rows(0).Item(ctrl.Field.FieldAlias).ToString
                    End If
                End If
            Next

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

    Private Sub LoadDefaultValue(strFieldName As String)
        For Each ctrl As Object In sptFields1.Panel2.Controls
            If ctrl.Field.FieldTableName & "." & ctrl.Field.FieldName = strFieldName Then
                Dim strValue As String = ctrl.Field.DefaultValue
                If strValue.Substring(0, 2) = "v:" Then
                    strValue = strValue.Replace("v:", "")
                    strValue = basCode.ProcessDefaultValue(strValue)
                End If

                Select Case ctrl.[GetType]().Name
                    Case "CheckField", "CheckBox"
                        ctrl.Checked = basCode.CheckBooleanValue(strValue)
                    Case Else
                        ctrl.Text = strValue
                End Select
            End If
        Next
    End Sub

    Private Sub ColorReset()
        For Each ctrl As Object In sptFields1.Panel2.Controls
            If FieldEnabledCheck(ctrl) = True Then
                Select Case ctrl.[GetType]().Name
                    Case "CheckField", "CheckBox"
                        ctrl.BackColor = clrControl
                    Case Else
                        ctrl.BackColor = clrOriginal
                End Select
            Else
                ctrl.BackColor = clrDisabled
            End If
        Next

    End Sub

    Private Sub ColorSet()
        For Each ctrl As Control In sptFields1.Panel2.Controls
            If FieldEnabledCheck(ctrl) = True Then
                FieldTextHandler(ctrl)
            Else
                ctrl.Text = ""
            End If
        Next
    End Sub

    Private Sub ItemAdd()
        strQuery = ""
        Dim strQuery1 As String = "", strQuery2 As String = ""
        strQuery = " INSERT INTO [" & basCode.basTable.TableName.Replace(".", "].[") & "] ("

        For Each ctrl As Object In sptFields1.Panel2.Controls
            If ctrl.BackColor = clrMarked And FieldEnabledCheck(ctrl) = True Then
                Select Case ctrl.Field.FieldDataType.ToUpper
                    Case "BIT"
                        strQuery1 &= ", [" & ctrl.Field.FieldName & "]"
                        strQuery2 &= ", " & ctrl.CheckState
                    Case "INTEGER"
                        If Not (ctrl.Text = "NULL" Or ctrl.Text = "") Then
                            strQuery1 &= ", [" & ctrl.Field.FieldName & "]"
                            strQuery2 &= ", " & ctrl.Text
                        End If
                    Case "BINARY", "IMAGE", "TIMESTAMP"
                        'do nothing
                    Case Else
                        If Not (ctrl.Text = "NULL" Or ctrl.Text = "") Then
                            strQuery1 &= ", [" & ctrl.Field.FieldName & "]"
                            strQuery2 &= ", N'" & ctrl.Text & "'"
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
        strQuery = " UPDATE [" & basCode.basTable.TableName.Replace(".", "].[") & "] SET ,"

        For Each ctrl As Object In sptFields1.Panel2.Controls
            If ctrl.BackColor = clrMarked And FieldEnabledCheck(ctrl) = True Then
                Select Case ctrl.Field.FieldDataType.ToUpper
                    Case "BIT"
                        strQueryUpdate &= ", [" & ctrl.Field.FieldName & "] = " & ctrl.CheckState
                    Case "INTEGER"
                        If ctrl.Text = "NULL" Or ctrl.Text = "" Then
                            strQueryUpdate &= ", [" & ctrl.Field.FieldName & "] = NULL "
                        Else
                            strQueryUpdate &= ", [" & ctrl.Field.FieldName & "] = " & ctrl.Text
                        End If
                    Case "BINARY", "IMAGE", "TIMESTAMP"
                        'do nothing
                    Case Else
                        If ctrl.Text = "NULL" Or ctrl.Text = "" Then
                            strQueryUpdate &= ", [" & ctrl.Field.FieldName & "] = NULL "
                        Else
                            strQueryUpdate &= ", [" & ctrl.Field.FieldName & "] = N'" & ctrl.Text & "'"
                        End If
                End Select
            End If

            If ctrl.Field.Identity = True Or ctrl.Field.PrimaryKey = True Then
                For Each cell In dgvTable1.SelectedRows(0).Cells
                    If cell.OwningColumn.Name = ctrl.Field.FieldName Then
                        If cell.Value = Nothing And ctrl.Field.FieldDataType = "CHAR" Then
                            strQueryWhere &= " AND COALESCE([" & ctrl.Field.FieldName & "],'') = " & basCode.SetDelimiters(cell.Value, ctrl.Field.FieldDataType, "=")
                        Else
                            strQueryWhere &= " AND [" & ctrl.Field.FieldName & "] = " & basCode.SetDelimiters(cell.Value, ctrl.Field.FieldDataType, "=")
                        End If
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
        strQuery = " DELETE FROM [" & basCode.basTable.TableName.Replace(".", "].[") & "] "
        Dim strColumn As String = "", strValue As String = ""

        For intField As Integer = 0 To basCode.basTable.Count - 1
            If basCode.basTable.Item(intField).Identity = True Or basCode.basTable.Item(intField).PrimaryKey = True Then
                For Each cell In dgvTable1.SelectedRows(0).Cells
                    If cell.OwningColumn.Name = basCode.basTable.Item(intField).FieldName Then
                        strColumn = basCode.basTable.Item(intField).FieldName
                        strValue = basCode.SetDelimiters(cell.Value, basCode.basTable.Item(intField).FieldDataType, "=")
                        strQueryWhere &= " AND [" & strColumn & "] = " & strValue
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
        'basCode.DataRowRemove(dtsTable, dtsTable.Tables(0).TableName, strColumn, strValue)
        dgvTable1.Rows.Remove(dgvTable1.SelectedRows(0))
        ColorReset()
        ButtonHandle()
    End Sub

    Private Sub SearchAdd()

        Dim root As XmlElement = basCode.xmlSearch.DocumentElement
        If root Is Nothing Then
            basCode.xmlTables = basCode.dhdText.CreateRootDocument(basCode.xmlSearch, "Sequenchel", "Searches", True)
        End If

        Dim NewNode As XmlNode = basCode.dhdText.CreateAppendElement(basCode.xmlSearch.Item("Sequenchel").Item("Searches"), "Search")
        basCode.dhdText.CreateAppendElement(NewNode, "TableName", basCode.basTable.TableName)
        basCode.dhdText.CreateAppendElement(NewNode, "SearchName", cbxSearch.Text)

        basCode.dhdText.CreateAppendElement(NewNode, "UseTop", chkUseTop.Checked)
        basCode.dhdText.CreateAppendElement(NewNode, "UseTopCount", txtUseTop.Text)
        basCode.dhdText.CreateAppendElement(NewNode, "ReversedOrder", chkReversedSortOrder.Checked)

        Dim FieldsNode As XmlNode = basCode.dhdText.CreateAppendElement(NewNode, "Fields", "")

        For Each ctrl As Object In sptFields1.Panel2.Controls
            If ctrl.BackColor = clrMarked Then

                Dim newAttribute1 As XmlAttribute = basCode.xmlSearch.CreateAttribute("FieldName")
                newAttribute1.Value = ctrl.Field.FieldName

                Select Case ctrl.Field.FieldDataType.ToUpper
                    Case "BIT"
                        Dim NewFieldNode As XmlNode = basCode.dhdText.CreateAppendElement(FieldsNode, "Field", ctrl.CheckState)
                        NewFieldNode.Attributes.Append(newAttribute1)
                    Case Else
                        Dim NewFieldNode As XmlNode = basCode.dhdText.CreateAppendElement(FieldsNode, "Field", ctrl.Text)
                        NewFieldNode.Attributes.Append(newAttribute1)
                End Select
            End If
        Next
    End Sub

    Private Sub SearchListLoad()
        cbxSearch.Text = ""
        cbxSearch.Items.Clear()
        Dim lstSearches As List(Of String) = basCode.LoadSearches(basCode.xmlSearch, basCode.basTable.TableName)
        If lstSearches Is Nothing Then Exit Sub
        For Each lstItem As String In lstSearches
            cbxSearch.Items.Add(lstItem)
        Next
    End Sub

    Private Function SearchDelete(Optional UpdateMode As Boolean = False) As Boolean
        Dim strSelection As String = cbxSearch.Text

        If strSelection.Length = 0 Then Return False
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlSearch, "Search", "SearchName", strSelection)
        If Not xNode Is Nothing Then
            If UpdateMode = False Then
                If MessageBox.Show("This will permanently remove the Item: " & strSelection & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Return False
            End If
            xNode.ParentNode.RemoveChild(xNode)
        End If
        Return True
    End Function

    Private Sub SearchLoad()
        Dim strSelection As String = cbxSearch.Text
        Dim strFieldName As String = "", strFieldValue As String = ""

        If strSelection.Length = 0 Then Exit Sub
        Dim xPNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlSearch, "Search", "SearchName", strSelection)
        If Not xPNode Is Nothing Then

            If basCode.dhdText.CheckElement(xPNode, "UseTop") Then chkUseTop.Checked = xPNode.Item("UseTop").InnerText
            If basCode.dhdText.CheckElement(xPNode, "UseTopCount") Then txtUseTop.Text = xPNode.Item("UseTopCount").InnerText
            If basCode.dhdText.CheckElement(xPNode, "ReversedOrder") Then chkReversedSortOrder.Checked = xPNode.Item("ReversedOrder").InnerText

            For Each xNode As XmlNode In xPNode.SelectNodes(".//Field")
                strFieldName = xNode.Attributes("FieldName").Value
                strFieldValue = xNode.InnerText

                For Each ctrl As Object In sptFields1.Panel2.Controls
                    If ctrl.Field.FieldName = strFieldName Then
                        'ctrl.BackColor = clrMarked
                        Select Case ctrl.Field.Category
                            Case 1, 3, 4
                                ctrl.Text = strFieldValue
                            Case 2
                                Dim chkTemp As CheckBox = TryCast(ctrl, CheckBox)
                                If chkTemp IsNot Nothing Then chkTemp.Checked = strFieldValue
                            Case 5
                                Dim msfTemp As ManagedSelectField = TryCast(ctrl, ManagedSelectField)
                                msfTemp.SuspendActions = True
                                msfTemp.Text = strFieldValue
                                msfTemp.SuspendActions = False
                                'msfTemp.DropDown(2)
                        End Select

                    End If
                Next
            Next
        End If
    End Sub

    Public Sub FieldTextHandler(sender As Object)
        If basCode.curStatus.SuspendActions = False Then

            Select Case sender.[GetType]().Name
                Case "TextField"
                    If sender.Text <> sender.Tag.ToString Then
                        sender.BackColor = clrMarked
                    Else
                        If sender.ReadOnly = False Then
                            sender.BackColor = clrOriginal
                        Else
                            sender.BackColor = clrDisabled
                        End If
                    End If
                Case "CheckField"
                    If sender.Checked.ToString <> sender.Tag.ToString Then
                        sender.BackColor = clrMarked
                    Else
                        If sender.Enabled = True Then
                            sender.BackColor = clrControl
                        Else
                            sender.BackColor = clrDisabled
                        End If
                    End If
                Case "ComboField"
                    'not used
                Case "ManagedSelectField"
                    Select Case sender.Field.Category
                        Case 5
                            'as main field
                            If sender.Text.ToString <> sender.Tag.ToString Then
                                sender.BackColor = clrMarked
                            Else
                                If sender.Enabled = True Then
                                    sender.BackColor = clrOriginal
                                Else
                                    sender.BackColor = clrDisabled
                                End If
                            End If
                        Case 6
                            'As related field
                            For Each ctrl In sptFields1.Panel2.Controls
                                'For intField As Integer = 0 To tblTable.Count - 1
                                If ctrl.Field.FieldAlias = sender.Field.FieldAlias Then
                                    Select Case ctrl.Field.Category
                                        Case 1, 3, 4, 5
                                            If Not sender.value = Nothing Then ctrl.Text = sender.Value
                                        Case 2
                                            ctrl.Checked = sender.Value
                                    End Select
                                End If
                            Next
                    End Select
            End Select
        End If
    End Sub
#End Region

End Class
