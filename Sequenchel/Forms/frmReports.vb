Imports System.Xml

Public Class frmReports
    Private mousePath As System.Drawing.Drawing2D.GraphicsPath
    Private fontSize As Integer = 20

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblLicense.Text = "Licensed to: " & strLicenseName
        lblLicense.Left = Me.Width - lblLicense.Width - (CurVar.BuildMargin * 5)

        mousePath = New System.Drawing.Drawing2D.GraphicsPath()

        DebugSettings()
        SecuritySet()
        'ReportFieldsDispose(True)
        LoadConnections()
        'LoadTableFields()
        'ReportsLoad()
    End Sub

    Private Sub frmReports_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        CurStatus.Connection = cbxConnection.SelectedItem
        LoadConnection(CurStatus.Connection)
        CurStatus.TableSet = cbxTableSet.SelectedItem
        LoadTableSet(CurStatus.TableSet)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        MessageBox.Show(pnlReportLabel.Controls.Count)
    End Sub

    Private Sub DebugSettings()
        If DebugMode Then
            btnTest.Visible = True
        End If
        If DevMode Then
            'mnuMain.Visible = True
        End If
    End Sub

    Private Sub SecuritySet()
        If CurVar.AllowQueryEdit = False And CurVar.SecurityOverride = False Then
            btnLoadQuery.Enabled = False
            rtbQuery.ReadOnly = True
        End If
    End Sub

#Region "Report Definitions Load"

#Region "Controls"
    Private Sub btnConnectionsReload_Click(sender As Object, e As EventArgs) Handles btnConnectionsReload.Click
        LoadConnections()
    End Sub

    Private Sub cbxConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxConnection.SelectedIndexChanged
        If cbxConnection.SelectedIndex >= -1 Then
            CurStatus.Connection = cbxConnection.SelectedItem
            LoadConnection(CurStatus.Connection)
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
            CurStatus.TableSet = cbxTableSet.SelectedItem
            LoadTableSet(CurStatus.TableSet)
            LoadTables()
            LoadTableFields()
            ReportsLoad()
        End If
    End Sub

    Private Sub btnTablesReload_Click(sender As Object, e As EventArgs) Handles btnTablesReload.Click
        LoadTables()
    End Sub

    Private Sub btnReportFieldAdd_Click(sender As Object, e As EventArgs) Handles btnReportFieldAdd.Click
        For Each lvwItem As ListViewItem In lvwAvailableFields.SelectedItems
            'Dim lvwItem As ListViewItem = lvwAvailableFields.SelectedItems(intCount)
            lvwAvailableFields.Items.Remove(lvwItem)
            lvwSelectedFields.Items.Add(lvwItem)

            ReportTableAdd(lvwItem.Name.Substring(0, lvwItem.Name.LastIndexOf(".")), lvwItem.Text)
            FieldAdd(lvwItem.Name, lvwItem.SubItems(1).Text, lvwItem.SubItems(0).Text)

            ResizeColumns()
        Next
    End Sub

    Private Sub btnReportFieldRemove_Click(sender As Object, e As EventArgs) Handles btnReportFieldRemove.Click
        For Each lvwItem As ListViewItem In lvwSelectedFields.SelectedItems
            'Dim lvwItem As ListViewItem = lvwSelectedFields.SelectedItems(0)
            lvwSelectedFields.Items.Remove(lvwItem)
            lvwAvailableFields.Items.Add(lvwItem)

            MasterPanelControlsDispose(pnlSelectedFields, lvwItem.Name)
            PanelsFieldSort()
            ReportTableRemove(lvwItem.Tag)

            ResizeColumns()
        Next
    End Sub

    Private Sub btnReportFieldUp_Click(sender As Object, e As EventArgs) Handles btnReportFieldUp.Click
        If lvwSelectedFields.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = lvwSelectedFields.SelectedItems(0)
            If Not item Is Nothing Then
                Dim index As Integer = lvwSelectedFields.Items.IndexOf(item)
                If index <> 0 Then
                    lvwSelectedFields.Items.RemoveAt(index)
                    index -= 1
                    lvwSelectedFields.Items.Insert(index, item)
                    lvwSelectedFields.Items(index).Selected = True
                    PanelsFieldSort()
                End If
            End If
        End If
    End Sub

    Private Sub btnReportFieldDown_Click(sender As Object, e As EventArgs) Handles btnReportFieldDown.Click
        If lvwSelectedFields.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = lvwSelectedFields.SelectedItems(0)
            If Not item Is Nothing Then
                Dim index As Integer = lvwSelectedFields.Items.IndexOf(item)
                If index < lvwSelectedFields.Items.Count - 1 Then
                    lvwSelectedFields.Items.RemoveAt(index)
                    index += 1
                    lvwSelectedFields.Items.Insert(index, item)
                    lvwSelectedFields.Items(index).Selected = True
                    PanelsFieldSort()
                End If
            End If
        End If
    End Sub

    Private Sub lblShowField_Click(sender As Object, e As EventArgs)
        pnlSelectedFieldsMain.Focus()
        pnlSelectedFieldsMain.Invalidate()
    End Sub

    Private Sub lblShowField_DoubleClick(sender As Object, e As EventArgs)
        'Add extra searchcriteria
        Dim strFieldName As String = Nothing
        strFieldName = sender.Tag

        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
        Dim strFieldType As String = xCNode.Item("DataType").InnerText

        FieldCheckFilterAdd(strFieldName, strFieldType, Nothing)
    End Sub

    Private Sub lblShowRelation_DoubleClick(sender As Object, e As EventArgs)
        'Add extra searchcriteria
        Dim strTableName As String = Nothing
        strTableName = sender.Tag
        RelationUseAdd(strTableName)
    End Sub

    Private Sub btnReportTableUp_Click(sender As Object, e As EventArgs) Handles btnReportTableUp.Click
        If lvwSelectedTables.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = lvwSelectedTables.SelectedItems(0)
            If Not item Is Nothing Then
                Dim index As Integer = lvwSelectedTables.Items.IndexOf(item)
                If index <> 0 Then
                    lvwSelectedTables.Items.RemoveAt(index)
                    index -= 1
                    lvwSelectedTables.Items.Insert(index, item)
                    lvwSelectedTables.Items(index).Selected = True
                    PanelsRelationSort()
                End If
            End If
        End If
    End Sub

    Private Sub btnReportTableDown_Click(sender As Object, e As EventArgs) Handles btnReportTableDown.Click
        If lvwSelectedTables.SelectedItems.Count = 1 Then
            Dim item As ListViewItem = lvwSelectedTables.SelectedItems(0)
            If Not item Is Nothing Then
                Dim index As Integer = lvwSelectedTables.Items.IndexOf(item)
                If index < lvwSelectedTables.Items.Count - 1 Then
                    lvwSelectedTables.Items.RemoveAt(index)
                    index += 1
                    lvwSelectedTables.Items.Insert(index, item)
                    lvwSelectedTables.Items(index).Selected = True
                    PanelsRelationSort()
                End If
            End If
        End If
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
            ReportFieldsDispose(True)
        End If
    End Sub

    Private Sub LoadConnections()
        AllClear(4)
        Dim lstConnections As List(Of String) = LoadConnectionsXml()
        If lstConnections Is Nothing Then Exit Sub
        For Each lstItem As String In lstConnections
            cbxConnection.Items.Add(lstItem)
        Next
        cbxConnection.SelectedItem = CurStatus.Connection
    End Sub

    Private Sub LoadTableSets()
        AllClear(3)
        Dim lstTableSets As List(Of String) = LoadTableSetsXml()
        If lstTableSets Is Nothing Then Exit Sub
        For Each lstItem As String In lstTableSets
            cbxTableSet.Items.Add(lstItem)
        Next
        cbxTableSet.SelectedItem = CurStatus.TableSet
    End Sub

    Private Sub LoadTables()
        AllClear(2)
        Dim lstTables As List(Of String) = LoadTablesXml()
        If lstTables Is Nothing Then Exit Sub
        For Each lstItem As String In lstTables
            cbxTable.Items.Add(lstItem)
        Next
        'cbxTable.SelectedItem = CurStatus.Table
    End Sub

    Private Sub LoadTableFields()
        For Each TableNode As Xml.XmlNode In xmlTables.SelectNodes("//Field")
            Dim lsvItem As New ListViewItem
            lsvItem.Tag = TableNode.Item("DataType").InnerText
            'lsvItem.Tag = TableNode.ParentNode.ParentNode.Item("Name").InnerText
            lsvItem.Name = TableNode.ParentNode.ParentNode.Item("Name").InnerText & "." & TableNode.Item("FldName").InnerText
            lsvItem.Text = TableNode.ParentNode.ParentNode.Item("Alias").InnerText
            lsvItem.SubItems.Add(TableNode.Item("FldAlias").InnerText)
            lvwAvailableFields.Items.Add(lsvItem)
        Next
        ResizeColumns()
    End Sub

    Private Sub ResizeColumns()
        lvwAvailableFields.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent)
        lvwAvailableFields.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent)
        lvwSelectedFields.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent)
        lvwSelectedFields.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent)
        lvwSelectedTables.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent)
        If lvwAvailableFields.Columns(0).Width < 50 Then lvwAvailableFields.Columns(0).Width = 50
        If lvwAvailableFields.Columns(1).Width < 50 Then lvwAvailableFields.Columns(1).Width = 50
        If lvwSelectedFields.Columns(0).Width < 50 Then lvwSelectedFields.Columns(0).Width = 50
        If lvwSelectedFields.Columns(1).Width < 50 Then lvwSelectedFields.Columns(1).Width = 50
        If lvwSelectedTables.Columns(0).Width < 50 Then lvwSelectedTables.Columns(0).Width = 50
    End Sub

    Private Sub ReportFieldsDispose(Optional blnAll As Boolean = True)
        txtDescription.Text = ""
        chkDistinct.Checked = False
        chkTop.Checked = True
        txtTop.Text = 1000

        lvwAvailableFields.Items.Clear()
        lvwSelectedFields.Items.Clear()
        lvwSelectedTables.Items.Clear()

        PanelControlsDispose(pnlReportLabel)
        PanelControlsDispose(pnlReportDisplay)
        PanelControlsDispose(pnlReportShowMode)
        PanelControlsDispose(pnlReportSort)
        PanelControlsDispose(pnlReportSortOrder)
        PanelControlsDispose(pnlReportFilter)
        PanelControlsDispose(pnlReportFilterMode)
        PanelControlsDispose(pnlReportFilterType)
        PanelControlsDispose(pnlReportFilterText)

        PanelControlsDispose(pnlRelationsLabel)
        PanelControlsDispose(pnlRelationsUse)
        PanelControlsDispose(pnlRelationsField)
        PanelControlsDispose(pnlRelationsRelation)
        PanelControlsDispose(pnlRelationsJoinType)
        pnlSelectedFields.Height = 31
        If blnAll = False Then
            LoadTableFields()
        End If
    End Sub

    Private Sub FieldAdd(strFieldName As String, strFieldAlias As String, strTableAlias As String)
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
        Dim strFieldType As String = xCNode.Item("DataType").InnerText

        FieldLabelAdd(strFieldName, strFieldAlias, strTableAlias)
        FieldCheckShowAdd(strFieldName, strFieldType)
        FieldCheckFilterAdd(strFieldName, strFieldType)
        PanelFieldWidthSet()
    End Sub

    Private Sub FieldLabelAdd(strFieldName As String, strFieldAlias As String, strTableAlias As String)
        Dim lblNew As New Label
        lblNew.Name = pnlReportLabel.Name & "1" & strFieldName
        lblNew.Tag = strFieldName
        lblNew.Text = strTableAlias & "." & strFieldAlias
        pnlReportLabel.Controls.Add(lblNew)
        AddHandler lblNew.Click, AddressOf Me.lblShowField_Click
        AddHandler lblNew.DoubleClick, AddressOf Me.lblShowField_DoubleClick
        lblNew.AutoSize = True
        lblNew.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count) * CurVar.BuildMargin)

        If lblNew.Width > CurStatus.ReportLabelWidth Then
            CurStatus.ReportLabelWidth = lblNew.Width
        End If

        Dim cbxNewSort As New ComboField
        cbxNewSort.Name = pnlReportSort.Name & "1" & strFieldName
        cbxNewSort.Tag = strFieldName
        cbxNewSort.Width = 50
        pnlReportSort.Controls.Add(cbxNewSort)
        cbxNewSort.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewSort.Left = CurVar.BuildMargin
        ComboBoxFill(cbxNewSort, "Sort")

        Dim txtNewSortOrder As New TextField
        txtNewSortOrder.Name = pnlReportSortOrder.Name & "1" & strFieldName
        txtNewSortOrder.Tag = strFieldName
        txtNewSortOrder.Width = 20
        pnlReportSortOrder.Controls.Add(txtNewSortOrder)
        txtNewSortOrder.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        txtNewSortOrder.Left = CurVar.BuildMargin

    End Sub

    Private Sub FieldCheckShowAdd(strFieldName As String, strFieldType As String)
        Dim chkNewShow As New CheckField
        chkNewShow.Name = pnlReportDisplay.Name & "1" & strFieldName
        chkNewShow.Tag = strFieldName
        'chkNewShow.ThreeState = True
        pnlReportDisplay.Controls.Add(chkNewShow)
        chkNewShow.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        chkNewShow.Left = CurVar.BuildMargin

        Dim cbxNewShowMode As New ComboField
        cbxNewShowMode.Name = pnlReportShowMode.Name & "1" & strFieldName
        cbxNewShowMode.FieldDataType = strFieldType
        cbxNewShowMode.Tag = strFieldName
        cbxNewShowMode.Width = 75
        pnlReportShowMode.Controls.Add(cbxNewShowMode)
        cbxNewShowMode.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewShowMode.Left = CurVar.BuildMargin
        'Dim strDataType As String = 
        ComboBoxFill(cbxNewShowMode, "ShowMode")
    End Sub

    Private Sub FieldCheckFilterAdd(strFieldName As String, strFieldType As String, Optional intCount As Integer = 0)

        'Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        'Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
        'Dim strFieldType As String = xCNode.Item("DataType").InnerText

        If intCount = 0 Then
            intCount += 1
            For Each ctrControl In pnlReportFilter.Controls
                If ctrControl.Name.ToString.Length > strFieldName.Length Then
                    If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strFieldName.Length, strFieldName.Length) = strFieldName Then intCount += 1
                End If
            Next
        End If
        If intCount > 9 Then
            MessageBox.Show("You cannot have more than 9 filters on any given field. Please try to combine your filters.")
            Exit Sub
        End If

        Dim chkNewFilter As New CheckField
        chkNewFilter.Name = pnlReportFilter.Name & intCount.ToString & strFieldName
        chkNewFilter.FieldDataType = strFieldType
        chkNewFilter.Tag = strFieldName
        chkNewFilter.ThreeState = True
        pnlReportFilter.Controls.Add(chkNewFilter)
        'chkNewFilter.Top = ((lstSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        chkNewFilter.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        chkNewFilter.Left = CurVar.BuildMargin

        Dim cbxNewFilterMode As New ComboField
        cbxNewFilterMode.Name = pnlReportFilterMode.Name & intCount.ToString & strFieldName
        cbxNewFilterMode.Tag = strFieldName
        cbxNewFilterMode.Width = 75
        pnlReportFilterMode.Controls.Add(cbxNewFilterMode)
        'cbxNewFilterMode.Top = ((lstSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewFilterMode.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewFilterMode.Left = CurVar.BuildMargin
        ComboBoxFill(cbxNewFilterMode, "FilterMode")

        Dim cbxNewFilterType As New ComboField
        cbxNewFilterType.Name = pnlReportFilterType.Name & intCount.ToString & strFieldName
        cbxNewFilterType.Tag = strFieldName
        cbxNewFilterType.Width = 75
        pnlReportFilterType.Controls.Add(cbxNewFilterType)
        'cbxNewFilterType.Top = ((lstSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewFilterType.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewFilterType.Left = CurVar.BuildMargin
        ComboBoxFill(cbxNewFilterType, "FilterType")

        Dim txtNewFilter As New TextField
        txtNewFilter.Name = pnlReportFilterText.Name & intCount.ToString & strFieldName
        txtNewFilter.FieldDataType = strFieldType
        txtNewFilter.Tag = strFieldName
        txtNewFilter.Width = 190
        pnlReportFilterText.Controls.Add(txtNewFilter)
        'txtNewFilter.Top = ((lstSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        txtNewFilter.Top = ((lvwSelectedFields.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * CurVar.BuildMargin)
        txtNewFilter.Left = CurVar.BuildMargin

        PanelsFieldSort()
    End Sub

    Private Sub ComboBoxFill(cbxTarget As ComboField, strCollection As String, Optional strSource As String = "")
        Select Case strCollection
            Case "ShowMode"
                cbxTarget.Items.Clear()
                cbxTarget.Items.Add("")
                cbxTarget.Items.Add("SUM")
                cbxTarget.Items.Add("MIN")
                cbxTarget.Items.Add("MAX")
                cbxTarget.Items.Add("AVG")
                cbxTarget.Items.Add("COUNT")
                If cbxTarget.FieldDataType = "DATETIME" Then
                    cbxTarget.Items.Add("DATE")
                    cbxTarget.Items.Add("YEAR")
                    cbxTarget.Items.Add("MONTH")
                    cbxTarget.Items.Add("DAY")
                End If
                If cbxTarget.FieldDataType = "DATETIME" Or cbxTarget.FieldDataType = "TIME" Or cbxTarget.FieldDataType = "TIMESTAMP" Then
                    cbxTarget.Items.Add("TIME")
                    cbxTarget.Items.Add("HOUR")
                    cbxTarget.Items.Add("MINUTE")
                    cbxTarget.Items.Add("SECOND")
                End If
            Case "Sort"
                cbxTarget.Items.Clear()
                cbxTarget.Items.Add("")
                cbxTarget.Items.Add("ASC")
                cbxTarget.Items.Add("DESC")
            Case "FilterType"
                cbxTarget.Items.Clear()
                cbxTarget.Items.Add("")
                cbxTarget.Items.Add("=")
                cbxTarget.Items.Add("<>")
                cbxTarget.Items.Add("<")
                cbxTarget.Items.Add("<=")
                cbxTarget.Items.Add(">=")
                cbxTarget.Items.Add(">")
                cbxTarget.Items.Add("LIKE")
                cbxTarget.Items.Add("NOT LIKE")
                cbxTarget.Items.Add("IS")
                cbxTarget.Items.Add("IS NOT")
                cbxTarget.Items.Add("IN")
                cbxTarget.Items.Add("NOT IN")
            Case "FilterMode"
                cbxTarget.Items.Clear()
                cbxTarget.Items.Add("")
                cbxTarget.Items.Add("AND")
                cbxTarget.Items.Add("AND NOT")
                cbxTarget.Items.Add("OR")
                cbxTarget.Items.Add("OR NOT")
            Case "RelationField"
                Dim lstFields As List(Of String) = LoadItemList(xmlTables, "Table", "Name", strSource, "Field", "FldName")
                If lstFields Is Nothing Then Exit Sub
                For Each lstItem As String In lstFields
                    cbxTarget.Items.Add(lstItem)
                Next
                'cbxTarget.SelectedItem = "INNER"
            Case "JoinType"
                cbxTarget.Items.Clear()
                cbxTarget.Items.Add("")
                cbxTarget.Items.Add("INNER")
                cbxTarget.Items.Add("LEFT OUTER")
                cbxTarget.Items.Add("RIGHT OUTER")
                cbxTarget.Items.Add("FULL OUTER")
                cbxTarget.Items.Add("CROSS")
        End Select
    End Sub

    Private Sub PanelFieldWidthSet()
        pnlReportLabel.Width = CurStatus.ReportLabelWidth + CurVar.BuildMargin
        pnlReportDisplay.Left = pnlReportLabel.Left + pnlReportLabel.Width
        lblReportShow.Left = pnlReportDisplay.Left + CurVar.BuildMargin

        pnlReportShowMode.Left = pnlReportDisplay.Left + pnlReportDisplay.Width
        lblReportShowMode.Left = pnlReportShowMode.Left + CurVar.BuildMargin

        pnlReportSort.Left = pnlReportShowMode.Left + pnlReportShowMode.Width
        lblReportSort.Left = pnlReportSort.Left + CurVar.BuildMargin

        pnlReportSortOrder.Left = pnlReportSort.Left + pnlReportSort.Width
        lblReportSortOrder.Left = pnlReportSortOrder.Left + CurVar.BuildMargin

        pnlReportFilter.Left = pnlReportSortOrder.Left + pnlReportSortOrder.Width
        lblReportFilter.Left = pnlReportFilter.Left + CurVar.BuildMargin

        pnlReportFilterMode.Left = pnlReportFilter.Left + pnlReportFilter.Width
        lblReportFilterMode.Left = pnlReportFilterMode.Left + CurVar.BuildMargin

        pnlReportFilterType.Left = pnlReportFilterMode.Left + pnlReportFilterMode.Width
        lblReportFilterType.Left = pnlReportFilterType.Left + CurVar.BuildMargin

        pnlReportFilterText.Left = pnlReportFilterType.Left + pnlReportFilterType.Width
        lblReportFilterText.Left = pnlReportFilterText.Left + CurVar.BuildMargin
        If pnlReportFilterText.Left + pnlReportFilterText.Width + (CurVar.BuildMargin * 2) > pnlSelectedFields.Width Then pnlSelectedFields.Width = pnlReportFilterText.Left + pnlReportFilterText.Width + (CurVar.BuildMargin * 2)
    End Sub

    Private Sub PanelRelationWidthSet()
        pnlRelationsLabel.Width = CurStatus.RelationLabelWidth + CurVar.BuildMargin
        pnlRelationsUse.Left = pnlRelationsLabel.Left + pnlRelationsLabel.Width
        lblRelationUse.Left = pnlRelationsUse.Left

        pnlRelationsField.Left = pnlRelationsUse.Left + pnlRelationsUse.Width
        lblSourceField.Left = pnlRelationsField.Left

        pnlRelationsRelation.Left = pnlRelationsField.Left + pnlRelationsField.Width
        lblRelationsRelation.Left = pnlRelationsRelation.Left

        pnlRelationsJoinType.Left = pnlRelationsRelation.Left + pnlRelationsRelation.Width
        lblRelationsJoinType.Left = pnlRelationsJoinType.Left

        If pnlRelationsJoinType.Left + pnlRelationsJoinType.Width + (CurVar.BuildMargin * 2) > pnlRelations.Width Then pnlRelations.Width = pnlRelationsJoinType.Left + pnlRelationsJoinType.Width + (CurVar.BuildMargin * 2)
    End Sub

    Private Sub PanelFieldHeightSet()
        If (pnlReportFilterText.Controls.Count + 1) * (CurVar.BuildMargin + CurVar.FieldHeight) > pnlReportFilterText.Height Then
            pnlReportFilterText.Height = (pnlReportFilterText.Controls.Count + 1) * (CurVar.BuildMargin + CurVar.FieldHeight)
        End If
        pnlReportLabel.Height = pnlReportFilterText.Height
        pnlReportDisplay.Height = pnlReportFilterText.Height
        pnlReportShowMode.Height = pnlReportFilterText.Height
        pnlReportSort.Height = pnlReportFilterText.Height
        pnlReportSortOrder.Height = pnlReportFilterText.Height
        pnlReportFilter.Height = pnlReportFilterText.Height
        pnlReportFilterMode.Height = pnlReportFilterText.Height
        pnlReportFilterType.Height = pnlReportFilterText.Height
        pnlSelectedFields.Height = pnlReportFilterText.Height
    End Sub

    Private Sub PanelRelationHeightSet()
        If (pnlRelationsUse.Controls.Count + 1) * (CurVar.BuildMargin + CurVar.FieldHeight) > pnlRelationsUse.Height Then
            pnlRelationsUse.Height = (pnlRelationsUse.Controls.Count + 1) * (CurVar.BuildMargin + CurVar.FieldHeight)
        End If
        pnlRelationsField.Height = pnlRelationsUse.Height
        pnlRelationsRelation.Height = pnlRelationsUse.Height
        pnlRelationsJoinType.Height = pnlRelationsUse.Height
        pnlRelationsLabel.Height = pnlRelationsUse.Height
        pnlRelations.Height = pnlRelationsUse.Height
    End Sub

    Private Sub PanelsFieldSort()
        FieldSort(lvwSelectedFields, pnlReportFilter, False, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportFilterMode, False, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportFilterType, False, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportFilterText, False, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportLabel, True, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportDisplay, True, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportShowMode, True, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportSort, True, pnlReportFilterText)
        FieldSort(lvwSelectedFields, pnlReportSortOrder, True, pnlReportFilterText)
        PanelFieldHeightSet()
    End Sub

    Private Sub PanelsRelationSort()
        FieldSort(lvwSelectedTables, pnlRelationsUse, False, pnlRelationsUse)
        FieldSort(lvwSelectedTables, pnlRelationsField, False, pnlRelationsUse)
        FieldSort(lvwSelectedTables, pnlRelationsRelation, False, pnlRelationsUse)
        FieldSort(lvwSelectedTables, pnlRelationsJoinType, False, pnlRelationsUse)
        FieldSort(lvwSelectedTables, pnlRelationsLabel, True, pnlRelationsUse)
        PanelRelationHeightSet()
    End Sub

    Private Sub FieldSort(lstSource As ListView, pnlTarget As Panel, SinglePanel As Boolean, ComparePanel As Panel)
        Dim strFieldName As String = Nothing
        Dim intControlNumber As Integer = 0
        Dim intMaxNumber As Integer = 0
        CurStatus.ReportMaxTop = CurVar.BuildMargin
        For incCount As Integer = 0 To lstSource.Items.Count - 1
            strFieldName = lstSource.Items.Item(incCount).Name
            intMaxNumber = 0
            For Each ctrControl In pnlTarget.Controls
                If ctrControl.Tag = strFieldName Then
                    intControlNumber = ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strFieldName.Length - 1, 1)
                    If SinglePanel = True And intControlNumber = 1 Then
                        For Each ctlControl In ComparePanel.Controls
                            If ctlControl.Tag = strFieldName Then
                                If intControlNumber = ctlControl.Name.ToString.Substring(ctlControl.Name.ToString.Length - strFieldName.Length - 1, 1) Then
                                    ctrControl.top = ctlControl.top
                                End If
                            End If
                        Next
                    Else
                        If intControlNumber > intMaxNumber Then intMaxNumber = intControlNumber
                        'MessageBox.Show(CurStatus.ReportMaxTop & Environment.NewLine & ((intControlNumber - 1) * CurVar.FieldHeight) & Environment.NewLine & ((intControlNumber - 1) * CurVar.BuildMargin))
                        ctrControl.Top = CurStatus.ReportMaxTop + ((intControlNumber - 1) * CurVar.FieldHeight) + ((intControlNumber - 1) * CurVar.BuildMargin)
                    End If

                End If
            Next
            CurStatus.ReportMaxTop += intMaxNumber * CurVar.FieldHeight + intMaxNumber * CurVar.BuildMargin
        Next
    End Sub

    Private Sub ReportTableAdd(strTable As String, strAlias As String)
        'Dim strTable As String = strFieldName.Substring(0, strFieldName.LastIndexOf("."))
        Dim blnTableFound As Boolean = False
        For Each lvwitem In lvwSelectedTables.Items
            If lvwitem.Name = strTable Then blnTableFound = True
        Next
        If blnTableFound = False Then
            Dim lvwAddItem As New ListViewItem
            lvwAddItem.Name = strTable
            lvwAddItem.Tag = strTable
            lvwAddItem.Text = strAlias
            lvwSelectedTables.Items.Add(lvwAddItem)
            RelationLabelAdd(strTable, strAlias)
            RelationUseAdd(strTable, 0)
            RelationsLoad(strTable)
            'PanelsRelationSort()
            PanelRelationWidthSet()
        End If
        '        MessageBox.Show(strTabel)
    End Sub

    Private Sub ReportTableRemove(strTable As String)
        'Dim strTabel As String = strFieldName.Substring(0, strFieldName.LastIndexOf("."))
        Dim strTableRemove As String = ""
        Dim intCount As Integer = 0
        Dim lvwItem As ListViewItem = Nothing
        For Each strItem As ListViewItem In lvwSelectedFields.Items
            If strTable = strItem.Tag Then
                intCount += 1
                'MessageBox.Show(strItem.ToString)
            End If
        Next
        If intCount = 0 Then
            lvwSelectedTables.Items.RemoveByKey(strTable)
            MasterPanelControlsDispose(pnlRelations, strTable)
            PanelsRelationSort()
        End If
    End Sub

    Private Sub RelationLabelAdd(strTableName As String, strAlias As String)
        Dim lblNew As New Label
        lblNew.Name = pnlRelationsLabel.Name & "1" & strTableName
        lblNew.Tag = strTableName
        lblNew.Text = strAlias & " (" & strTableName & ")"
        pnlRelationsLabel.Controls.Add(lblNew)
        AddHandler lblNew.DoubleClick, AddressOf Me.lblShowRelation_DoubleClick
        lblNew.AutoSize = True
        lblNew.Top = ((lvwSelectedTables.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count) * CurVar.BuildMargin)

        If lblNew.Width > CurStatus.RelationLabelWidth Then
            CurStatus.RelationLabelWidth = lblNew.Width
        End If

    End Sub

    Private Sub RelationUseAdd(strTableName As String, Optional intCount As Integer = 0)
        If intCount = 0 Then
            intCount += 1
            For Each ctrControl In pnlRelationsUse.Controls
                If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTableName.Length, strTableName.Length) = strTableName Then intCount += 1
            Next
        End If

        Dim chkNewShow As New CheckField
        chkNewShow.Name = pnlRelationsUse.Name & intCount.ToString & strTableName
        chkNewShow.Tag = strTableName
        pnlRelationsUse.Controls.Add(chkNewShow)
        chkNewShow.Top = ((lvwSelectedTables.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * CurVar.BuildMargin)
        chkNewShow.Left = CurVar.BuildMargin

        Dim cbxNewField As New ComboField
        cbxNewField.Name = pnlRelationsField.Name & intCount.ToString & strTableName
        cbxNewField.Tag = strTableName
        cbxNewField.Width = 140
        pnlRelationsField.Controls.Add(cbxNewField)
        cbxNewField.Top = ((lvwSelectedTables.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewField.Left = CurVar.BuildMargin
        ComboBoxFill(cbxNewField, "RelationField", strTableName)

        Dim txtNewFilter As New TextField
        txtNewFilter.Name = pnlRelationsRelation.Name & intCount.ToString & strTableName
        txtNewFilter.Tag = strTableName
        txtNewFilter.Width = 140
        pnlRelationsRelation.Controls.Add(txtNewFilter)
        txtNewFilter.Top = ((lvwSelectedTables.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * CurVar.BuildMargin)
        txtNewFilter.Left = CurVar.BuildMargin

        Dim cbxNewJoinType As New ComboField
        cbxNewJoinType.Name = pnlRelationsJoinType.Name & intCount.ToString & strTableName
        cbxNewJoinType.Tag = strTableName
        cbxNewJoinType.Width = 110
        pnlRelationsJoinType.Controls.Add(cbxNewJoinType)
        cbxNewJoinType.Top = ((lvwSelectedTables.Items.Count - 1) * CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * CurVar.BuildMargin)
        cbxNewJoinType.Left = CurVar.BuildMargin
        ComboBoxFill(cbxNewJoinType, "JoinType")

        PanelsRelationSort()
    End Sub

    Private Sub RelationsLoad(strTable As String)
        Dim xPNode As System.Xml.XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTable)
        Dim xmlFieldList As System.Xml.XmlNodeList = dhdText.FindXmlChildNodes(xPNode, "Fields/Field/Relations", "Relation")
        Dim intCount As Integer = 0
        For Each xmlCNode As System.Xml.XmlNode In xmlFieldList
            Dim xmlRelationList As System.Xml.XmlNodeList = dhdText.FindXmlChildNodes(xmlCNode, "Relation")
            For Each xmlRNode As System.Xml.XmlNode In xmlRelationList
                'MessageBox.Show(xmlCNode("Relations").Value)
                If xmlRNode.InnerText.Length > 0 Then
                    intCount += 1
                    If intCount > 1 Then RelationUseAdd(strTable)
                    For Each ctrControl In pnlRelationsField.Controls
                        If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                            If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                                'ctrControl.Text = xmlCNode.Item("FldName").InnerText
                                ctrControl.Text = xmlRNode.ParentNode.ParentNode.Item("FldName").InnerText
                            End If
                        End If
                    Next
                    For Each ctrControl In pnlRelationsRelation.Controls
                        If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                            If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                                ctrControl.Text = xmlRNode.InnerText
                            End If
                        End If
                    Next
                    For Each ctrControl In pnlRelationsJoinType.Controls
                        If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                            If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                                ctrControl.SelectedIndex = 1
                            End If
                        End If
                    Next
                End If
            Next
        Next
    End Sub
#End Region

#Region "Report Data Load"

#Region "Controls"
    Private Sub btnReportClear_Click(sender As Object, e As EventArgs) Handles btnReportClear.Click
        CursorControl("Wait")
        If cbxReportName.SelectedIndex = -1 Then
            ReportFieldsDispose(False)
        End If
        cbxReportName.SelectedIndex = -1
        cbxReportName.Text = ""
        CursorControl()
    End Sub

    Private Sub btnReportCreate_Click(sender As Object, e As EventArgs) Handles btnReportCreate.Click
        CursorControl("Wait")
        QueryShow()
        QueryExecute()
        CursorControl()
    End Sub

    Private Sub btnQueryShow_Click(sender As Object, e As EventArgs) Handles btnQueryShow.Click
        CursorControl("Wait")
        QueryShow()
        CursorControl()
    End Sub

    Private Sub btnExecuteQuery_Click(sender As Object, e As EventArgs) Handles btnExecuteQuery.Click
        CursorControl("Wait")
        strQuery = rtbQuery.Text
        QueryExecute()
        CursorControl()
    End Sub

    Private Sub ReportClear(blnAll As Boolean)
        strErrorMessage = ""
        lblErrorMessage.Text = ""
        dgvReport.Columns.Clear()
        If blnAll = True Then
            rtbQuery.Text = ""
        End If
    End Sub

    Private Sub QueryShow()
        ReportClear(True)
        ReportQueryBuild()
        rtbQuery.Text = strQuery
        tabReports.SelectedTab = tpgReportResult
    End Sub

    Private Sub QueryExecute()
        ReportClear(False)
        dtmElapsedTime = Now()
        tmrElapsedTime.Enabled = True
        tmrElapsedTime.Start()
        dtsReport = Nothing
        dtsReport = QueryDb(dhdConnection, strQuery, True)
        ReportShow(dtsReport)
        tmrElapsedTime.Stop()
        tmrElapsedTime.Enabled = False
        tmsElapsedTime = Now() - dtmElapsedTime
        lblElapsedTime.Text = tmsElapsedTime.ToString
    End Sub

    Private Sub btnSaveQuery_Click(sender As Object, e As EventArgs) Handles btnSaveQuery.Click
        SaveQuery()
    End Sub

    Private Sub btnLoadQuery_Click(sender As Object, e As EventArgs) Handles btnLoadQuery.Click
        LoadQuery()
    End Sub

    Private Sub btnReportAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnReportAddOrUpdate.Click
        CursorControl("Wait")
        If cbxReportName.Text.Length < 3 Then
            MessageBox.Show("Report Name must be at least 3 characters long")
            Exit Sub
        End If
        ReportDelete()
        ReportAdd(xmlReports)
        If Not cbxReportName.Items.Contains(cbxReportName.Text) Then cbxReportName.Items.Add(cbxReportName.Text)

        dhdText.SaveXmlFile(xmlReports, CurVar.ReportSetFile)
        CursorControl()
    End Sub

    Private Sub btnReportDelete_Click(sender As Object, e As EventArgs) Handles btnReportDelete.Click
        If MessageBox.Show("This will permanently remove the Item: " & cbxReportName.Text & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        CursorControl("Wait")
        ReportDelete()
        If cbxReportName.Items.Contains(cbxReportName.Text) Then cbxReportName.Items.Remove(cbxReportName.Text)
        cbxReportName.SelectedIndex = -1
        cbxReportName.Text = ""
        ReportFieldsDispose(False)

        dhdText.SaveXmlFile(xmlReports, CurVar.ReportSetFile)
        CursorControl()
    End Sub

    Private Sub cbxReportName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxReportName.SelectedIndexChanged
        If cbxReportName.SelectedIndex >= 0 Then
            ReportFieldsDispose(False)
            ReportLoad(xmlReports, cbxReportName.Text)
        End If
        pnlSelectedFieldsMain.Focus()
        pnlSelectedFieldsMain.Invalidate()

    End Sub

    Private Sub btnRevertChanges_Click(sender As Object, e As EventArgs) Handles btnRevertChanges.Click
        ReportFieldsDispose(False)
        ReportLoad(xmlReports, cbxReportName.Text)
    End Sub

    Private Sub btnExportToFile_Click(sender As Object, e As EventArgs) Handles btnExportToFile.Click

        Dim strReportName As String = ""
        If cbxReportName.Text.Length > 0 Then
            strReportName = cbxReportName.Text
        Else
            strReportName = CurStatus.Connection
        End If
        ExportFile(dtsReport, strReportName, False)
        'XmlExportDatagridView(dgvReport, "Sequenchel", CurStatus.Connection, strReportName)
    End Sub

    'Private Sub btnExportToExcel_Click(sender As Object, e As EventArgs) Handles btnExportToExcel.Click
    '    If Not dtsReport Is Nothing Then
    '        ExcelExportDatagridView(dtsReport)
    '    End If
    'End Sub

#End Region

    Private Sub ReportsLoad()
        cbxReportName.Items.Clear()
        cbxReportName.Text = ""
        Dim lstReports As List(Of String) = LoadReportsXml()
        If lstReports Is Nothing Then Exit Sub
        For Each lstItem As String In lstReports
            cbxReportName.Items.Add(lstItem)
        Next
    End Sub

    Private Sub ReportLoad(xmlReports As XmlDocument, strReportName As String)
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strReportName)
        Dim strTable As String = "", strField As String = "", intRelationNumber As Integer = 0

        If Not xNode Is Nothing Then
            If dhdText.CheckNodeElement(xNode, "Description") Then txtDescription.Text = xNode.Item("Description").InnerText
            chkTop.Checked = xNode.Item("UseTop").InnerText
            txtTop.Text = xNode.Item("UseTopNumber").InnerText
            chkDistinct.Checked = xNode.Item("UseDistinct").InnerText
            For Each xCNode As XmlNode In dhdText.FindXmlChildNodes(xNode, "Tables/Table/Fields/Field")
                strTable = xCNode.ParentNode.ParentNode.Item("TableName").InnerText
                If strTable.IndexOf(".") = -1 Then strTable = "dbo." & strTable
                strField = xCNode.Item("FieldName").InnerText
                lvwAvailableFields.Items(strTable & "." & strField).Selected = True
                btnReportFieldAdd_Click(Nothing, Nothing)
                SetCtrText(pnlReportDisplay, strTable & "." & strField, xCNode.Item("FieldShow").InnerText)
                SetCtrText(pnlReportShowMode, strTable & "." & strField, xCNode.Item("FieldShowMode").InnerText)
                SetCtrText(pnlReportSort, strTable & "." & strField, xCNode.Item("FieldSort").InnerText)
                SetCtrText(pnlReportSortOrder, strTable & "." & strField, xCNode.Item("FieldSortOrder").InnerText)

                For Each xFnode In dhdText.FindXmlChildNodes(xCNode, "Filters/Filter")
                    SetCtrText(pnlReportFilter, strTable & "." & strField, xFnode.Item("FilterEnabled").InnerText, xFnode.Item("FilterNumber").InnerText)
                    SetCtrText(pnlReportFilterMode, strTable & "." & strField, xFnode.Item("FilterMode").InnerText, xFnode.Item("FilterNumber").InnerText)
                    SetCtrText(pnlReportFilterType, strTable & "." & strField, xFnode.Item("FilterType").InnerText, xFnode.Item("FilterNumber").InnerText)
                    SetCtrText(pnlReportFilterText, strTable & "." & strField, xFnode.Item("FilterText").InnerText, xFnode.Item("FilterNumber").InnerText)
                Next
            Next

            For Each xRNode As XmlNode In dhdText.FindXmlChildNodes(xNode, "Tables/Table/Relations/Relation")
                strTable = xRNode.ParentNode.ParentNode.Item("TableName").InnerText
                intRelationNumber = xRNode.Item("RelationNumber").InnerText
                'lstAvailableFields.SelectedItem = strTable & "." & strField
                'btnReportFieldAdd_Click(Nothing, Nothing)
                SetCtrText(pnlRelationsUse, strTable, xRNode.Item("RelationEnabled").InnerText, intRelationNumber)
                SetCtrText(pnlRelationsField, strTable, xRNode.Item("RelationSource").InnerText, intRelationNumber)
                SetCtrText(pnlRelationsRelation, strTable, xRNode.Item("RelationTarget").InnerText, intRelationNumber)
                SetCtrText(pnlRelationsJoinType, strTable, xRNode.Item("RelationJoinType").InnerText, intRelationNumber)

            Next
        End If
    End Sub

    Private Sub ReportQueryBuild()
        Dim strFieldName As String = ""
        Dim strShowMode As String = Nothing
        Dim strHavingMode As String = Nothing
        Dim strQueryFrom As String = ""
        Dim strQueryWhere As String = ""
        Dim strQueryGroup As String = "GROUP BY ", blnGroup As Boolean = False
        Dim strQueryHaving As String = ""
        Dim strQueryOrder As String = ""

        strQuery = "SELECT "
        For incCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
            strFieldName = lvwSelectedFields.Items.Item(incCount).Name
            'intMaxNumber = 0

            For Each ctrSelect In pnlReportDisplay.Controls
                strHavingMode = GetCtrText(pnlReportFilter, strFieldName)
                If strHavingMode = 2 Then blnGroup = True
                If ctrSelect.Checked = True Then
                    If strFieldName = ctrSelect.Tag Then
                        strShowMode = GetCtrText(pnlReportShowMode, strFieldName)

                        strQuery &= ", " & FormatFieldXML(strFieldName, strShowMode, True, True)
                        Select Case strShowMode
                            Case Nothing
                                strQueryGroup &= ", " & strFieldName
                            Case "DATE", "YEAR", "MONTH", "DAY", "TIME", "HOUR", "MINUTE", "SECOND"
                                strQueryGroup &= ", " & strFieldName
                            Case Else
                                blnGroup = True
                        End Select
                    End If
                End If
            Next
        Next

        If strQuery = "SELECT " Then
            MessageBox.Show("You need to select at least 1 Field for display")
            Exit Sub
        End If
        strQuery = strQuery.Replace("SELECT ,", "SELECT ")
        If chkTop.Checked = True And IsNumeric(txtTop.Text) = True Then strQuery = strQuery.Replace("SELECT ", "SELECT TOP " & txtTop.Text & " ")
        If chkDistinct.Checked = True Then strQuery = strQuery.Replace("SELECT ", "SELECT DISTINCT ")
        strQueryGroup = strQueryGroup.Replace("GROUP BY ,", "GROUP BY ")
        If strQueryGroup = "GROUP BY " Then blnGroup = False
        strQueryFrom = FromClauseGet()
        strQuery &= Environment.NewLine & strQueryFrom
        strQueryWhere = WhereClauseGet()
        If strQueryWhere.Length > 10 Then strQuery &= Environment.NewLine & strQueryWhere
        If blnGroup = True Then strQuery &= Environment.NewLine & strQueryGroup
        strQueryHaving = HavingClauseGet()
        If strQueryHaving.Length > 11 Then strQuery &= Environment.NewLine & strQueryHaving
        strQueryOrder = OrderClauseGet()
        If strQueryOrder.Length > 10 Then strQuery &= Environment.NewLine & strQueryOrder

        'MessageBox.Show(strQuery)
    End Sub

    Private Function FieldAliasGet(strFieldName As String) As String
        Dim strAliasName As String = ""
        For Each lvwItem In lvwSelectedFields.Items
            If lvwItem.Name = strFieldName Then
                strAliasName = lvwItem.SubItems(1).Text
            End If
        Next
        If strAliasName = "" Then strAliasName = strFieldName
        Return strAliasName
    End Function

    Private Function FromClauseGet() As String
        Dim strTableName As String = ""
        Dim intControlNumber As Integer = 0
        Dim strFromClause As String = "FROM "
        Dim strFromSource As String = Nothing, strFromType As String = Nothing, strFromRelation As String = Nothing, strTargetTable As String = Nothing

        For incCount As Integer = 0 To lvwSelectedTables.Items.Count - 1
            strTableName = lvwSelectedTables.Items.Item(incCount).Name
            If incCount = 0 Then strFromClause &= strTableName
            For Each ctrFrom In pnlRelationsUse.Controls
                If ctrFrom.Checked = True Then
                    If strTableName = ctrFrom.Tag Then
                        intControlNumber = ctrFrom.Name.ToString.Substring(ctrFrom.Name.ToString.Length - strTableName.Length - 1, 1)
                        strFromSource = GetCtrText(pnlRelationsField, strTableName, intControlNumber)
                        strFromRelation = GetCtrText(pnlRelationsRelation, strTableName, intControlNumber)
                        strFromType = GetCtrText(pnlRelationsJoinType, strTableName, intControlNumber)
                        strTargetTable = strFromRelation.Substring(0, strFromRelation.LastIndexOf("."))
                        strTargetTable = strTargetTable.Substring(strTargetTable.LastIndexOf("(") + 1, strTargetTable.Length - (strTargetTable.LastIndexOf("(") + 1))
                        If strFromClause.Contains(strTargetTable) = True And strFromClause.Contains(strTableName) = False Then
                            strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTableName & " ON " & strTableName & "." & strFromSource & " = " & strFromRelation
                        ElseIf strFromClause.Contains(strTargetTable) = True And strFromClause.Contains(strTableName) = True Then
                            strFromClause &= Environment.NewLine & " AND " & strTableName & "." & strFromSource & " = " & strFromRelation
                        ElseIf strFromClause.Contains(strTargetTable) = False And strFromClause.Contains(strTableName) = False Then
                            strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTargetTable & " ON " & strTableName & "." & strFromSource & " = " & strFromRelation
                        ElseIf strFromClause.Contains(strTargetTable) = False And strFromClause.Contains(strTableName) = True Then
                            strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTargetTable & " ON " & strTableName & "." & strFromSource & " = " & strFromRelation
                        End If
                    End If
                End If
            Next
        Next
        Return strFromClause
    End Function

    Private Function WhereClauseGet() As String
        Dim strFieldName As String = ""
        Dim intControlNumber As Integer = 0
        Dim strQueryWhere As String = "WHERE "
        Dim strWhereMode As String = Nothing, strWhereType As String = Nothing, strWhereClause As String = Nothing

        For incCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
            strFieldName = lvwSelectedFields.Items.Item(incCount).Name
            For Each ctrWhere As CheckField In pnlReportFilter.Controls
                If ctrWhere.Checked = True And ctrWhere.CheckState = CheckState.Checked Then
                    If strFieldName = ctrWhere.Tag Then
                        intControlNumber = ctrWhere.Name.ToString.Substring(ctrWhere.Name.ToString.Length - strFieldName.Length - 1, 1)
                        strWhereMode = GetCtrText(pnlReportFilterMode, strFieldName, intControlNumber)
                        If strWhereMode = "" Then strWhereMode = "AND"
                        If strWhereMode.Contains("AND") Then strWhereMode = ") " & strWhereMode & " ("
                        'If strWhereMode = "AND NOT" Then strWhereMode = ") AND NOT ("
                        strWhereType = GetCtrText(pnlReportFilterType, strFieldName, intControlNumber)
                        strWhereClause = GetCtrText(pnlReportFilterText, strFieldName, intControlNumber)
                        'If IsNumeric(strWhereClause) = False Then strWhereClause = "'" & strWhereClause & "'"
                        strWhereClause = SetDelimiters(strWhereClause, ctrWhere.FieldDataType, strWhereType)

                        If strWhereType = "LIKE" And strWhereClause.Contains("*") Then strWhereClause = strWhereClause.Replace("*", "%")
                        If strWhereType <> Nothing And strWhereClause <> Nothing Then
                            If strWhereMode = Nothing Then
                                strQueryWhere &= " " & strFieldName & " " & strWhereType & " " & strWhereClause
                            Else
                                strQueryWhere &= " " & strWhereMode & " " & strFieldName & " " & strWhereType & " " & strWhereClause
                            End If
                        End If
                    End If
                End If
            Next
        Next
        strQueryWhere &= ")"
        strQueryWhere = strQueryWhere.Replace("WHERE  ) AND", "WHERE ").Replace("WHERE  OR", "WHERE (").Replace("WHERE  ) AND NOT", "WHERE NOT (")

        Return strQueryWhere
    End Function

    Private Function HavingClauseGet() As String
        Dim strFieldName As String = ""
        Dim strHavingField As String = ""
        Dim strShowMode As String = Nothing
        Dim intControlNumber As Integer = 0
        Dim strQueryHaving As String = "HAVING "
        Dim strHavingMode As String = Nothing, strHavingType As String = Nothing, strHavingClause As String = Nothing

        For incCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
            strFieldName = lvwSelectedFields.Items.Item(incCount).Name
            For Each ctrHaving As CheckField In pnlReportFilter.Controls
                If ctrHaving.Checked = True And ctrHaving.CheckState = CheckState.Indeterminate Then
                    If strFieldName = ctrHaving.Tag Then
                        intControlNumber = ctrHaving.Name.ToString.Substring(ctrHaving.Name.ToString.Length - strFieldName.Length - 1, 1)
                        strHavingMode = GetCtrText(pnlReportFilterMode, strFieldName, intControlNumber)
                        If strHavingMode = "" Then strHavingMode = "AND"
                        If strHavingMode.Contains("AND") Then strHavingMode = ") " & strHavingMode & " ("
                        'If strHavingMode = "AND NOT" Then strHavingMode = ") AND NOT ("
                        strHavingType = GetCtrText(pnlReportFilterType, strFieldName, intControlNumber)

                        strHavingClause = GetCtrText(pnlReportFilterText, strFieldName, intControlNumber)

                        strShowMode = GetCtrText(pnlReportShowMode, strFieldName)
                        strHavingField &= ", " & FormatFieldXML(strFieldName, strShowMode, False, False)

                        'If IsNumeric(strHavingClause) = False Then strHavingClause = "'" & strHavingClause & "'"
                        strHavingClause = SetDelimiters(strHavingClause, ctrHaving.FieldDataType, strHavingType)
                        If strHavingType <> Nothing And strHavingClause <> Nothing Then
                            If strHavingMode = Nothing Then
                                strQueryHaving &= " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            Else
                                strQueryHaving &= " " & strHavingMode & " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            End If
                        End If
                    End If
                End If
            Next
        Next
        strQueryHaving &= ")"
        'strQueryHaving = strQueryHaving.Replace("HAVING  ) AND", "HAVING ").Replace("HAVING  OR", "HAVING (").Replace("HAVING  ) AND NOT", "HAVING NOT (")
        strQueryHaving = strQueryHaving.Replace("HAVING  ) AND", "HAVING ").Replace("HAVING  ) OR", "HAVING ")
        strQueryHaving = strQueryHaving.Replace("HAVING  ( ,", "HAVING (").Replace("HAVING  NOT ( ,", "HAVING NOT (")

        Return strQueryHaving
    End Function

    Private Function OrderClauseGet() As String
        Dim strQueryOrder As String = "ORDER BY "

        For intCount = 1 To pnlReportSortOrder.Controls.Count + 1
            For Each ctrOrder In pnlReportSortOrder.Controls
                If ctrOrder.Text = intCount.ToString Then
                    strQueryOrder &= ", " & ctrOrder.Tag
                    For Each ctrSort In pnlReportSort.Controls
                        If ctrOrder.Tag = ctrSort.Tag And ctrSort.Text.Length > 0 Then
                            strQueryOrder &= " " & ctrSort.Text
                        End If
                    Next
                End If
                If intCount > pnlReportSortOrder.Controls.Count And IsNumeric(ctrOrder.Text) = True Then
                    If ctrOrder.text > pnlReportSortOrder.Controls.Count Then
                        strQueryOrder &= ", " & ctrOrder.Tag
                        For Each ctrSort In pnlReportSort.Controls
                            If ctrOrder.Tag = ctrSort.Tag And ctrSort.Text.Length > 0 Then
                                strQueryOrder &= " " & ctrSort.Text
                            End If
                        Next
                    End If
                End If
            Next
        Next

        strQueryOrder = strQueryOrder.Replace("ORDER BY ,", "ORDER BY ")
        Return strQueryOrder
    End Function

    Private Function GetCtrText(pnlInput As Panel, strFieldName As String, Optional intControlNumber As Integer = 0) As String
        For Each ctrControl In pnlInput.Controls
            If strFieldName = ctrControl.Tag Then
                If intControlNumber = 0 Or intControlNumber = ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strFieldName.Length - 1, 1) Then
                    If ctrControl.FieldCategory = 2 Then
                        Return ctrControl.CheckState
                    End If
                    If ctrControl.Text.Length > 0 Then
                        Return ctrControl.Text
                    Else
                        Return Nothing
                    End If
                End If
            End If
        Next
        Return Nothing
    End Function

    Private Sub SetCtrText(pnlInput As Panel, strFieldName As String, strValue As String, Optional intControlNumber As Integer = 0)
        Dim blnControlFound As Boolean = False
        For Each ctrControl In pnlInput.Controls
            If strFieldName = ctrControl.Tag Then
                If intControlNumber = 0 Or intControlNumber = ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strFieldName.Length - 1, 1) Then
                    blnControlFound = True
                    Select Case ctrControl.FieldCategory
                        Case 2
                            Select Case strValue.ToLower
                                Case "true", "checked", "1"
                                    strValue = 1
                                Case "false", "unchecked", "0"
                                    strValue = 0
                                Case "indeterminate", "2"
                                    strValue = 2
                                Case Else
                                    strValue = 0
                            End Select
                            ctrControl.CheckState = strValue
                        Case 1, 3, 4, 5, 6
                            ctrControl.Text = strValue
                    End Select
                End If
            End If
        Next
        If blnControlFound = False And intControlNumber > 1 Then
            If pnlInput.Parent Is pnlSelectedFields Then
                Dim lblTarget As Label = GetLabel(pnlReportLabel, strFieldName)
                If lblTarget Is Nothing Then
                    WriteLog("unable to add extra Filtercriterium for: " & strFieldName, 1)
                Else
                    lblShowField_DoubleClick(lblTarget, Nothing)
                    SetCtrText(pnlInput, strFieldName, strValue, intControlNumber)
                End If
            ElseIf pnlInput.Parent Is pnlRelations Then
                Dim lblTarget As Label = GetLabel(pnlRelationsLabel, strFieldName)
                If lblTarget Is Nothing Then
                    WriteLog("unable to add extra Relationcriterium for: " & strFieldName, 1)
                Else
                    lblShowRelation_DoubleClick(lblTarget, Nothing)
                    SetCtrText(pnlInput, strFieldName, strValue, intControlNumber)
                End If
            Else
                WriteLog("The Field: " & strFieldName & " on Panel: " & pnlInput.Name & " was not found." & "Unable to load the Value: " & strValue, 1)
            End If
        End If
    End Sub

    Private Function GetLabel(pnlSource As Panel, strFieldName As String) As Label
        For Each lblTarget As Label In pnlSource.Controls
            If lblTarget.Tag = strFieldName Then Return lblTarget
        Next
        Return Nothing
    End Function

    Private Sub ReportShow(dtsData As DataSet)

        'strReportText &= vbCrLf
        lblListCountNumber.Text = "0"
        lblErrorMessage.Text = strErrorMessage
        If DatasetCheck(dtsData) = False Then Exit Sub

        'If DataSet2DataGridView(dtsData, 0, dgvReport, True) = False Then
        '    MessageBox.Show("There was an error loading the report")
        'End If
        Try
            dgvReport.DataSource = dtsData.Tables(0)
            lblListCountNumber.Text = dtsData.Tables(0).Rows.Count.ToString
            'DataGridViewColumnSize(dgvReport)
            lblErrorMessage.Text = "Command completed succesfully"
        Catch ex As Exception
            MessageBox.Show("There was an error loading the report" & Environment.NewLine & ex.Message)
        End Try

        'If strErrorMessage = "" Then lblErrorMessage.Text = "Command completed succesfully"
        dgvReport.ClearSelection()

    End Sub

    Private Sub ReportAdd(xmlDocReport As XmlDocument)
        Dim strTable As String = ""
        Dim strField As String = ""
        Dim intControlNumber As Integer = 0

        Dim root As XmlElement = xmlDocReport.DocumentElement
        If root Is Nothing Then
            xmlDocReport = dhdText.CreateRootDocument(xmlDocReport, "Sequenchel", "Reports", True)
        End If

        Dim NewReportNode As XmlNode = dhdText.CreateAppendElement(xmlDocReport.Item("Sequenchel").Item("Reports"), "Report")
        dhdText.CreateAppendElement(NewReportNode, "ReportName", cbxReportName.Text)
        dhdText.CreateAppendElement(NewReportNode, "Description", txtDescription.Text)
        dhdText.CreateAppendElement(NewReportNode, "UseTop", chkTop.Checked.ToString)
        dhdText.CreateAppendElement(NewReportNode, "UseTopNumber", txtTop.Text)
        dhdText.CreateAppendElement(NewReportNode, "UseDistinct", chkDistinct.Checked.ToString)
        Dim NewTablesNode As XmlNode = dhdText.CreateAppendElement(NewReportNode, "Tables")
        For intTableCount As Integer = 0 To lvwSelectedTables.Items.Count - 1
            strTable = lvwSelectedTables.Items.Item(intTableCount).Name
            Dim NewTableNode As XmlNode = dhdText.CreateAppendElement(NewTablesNode, "Table")
            dhdText.CreateAppendElement(NewTableNode, "TableName", strTable)
            Dim NewFieldsNode As XmlNode = dhdText.CreateAppendElement(NewTableNode, "Fields")
            For intFieldCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
                strField = lvwSelectedFields.Items.Item(intFieldCount).Name
                If strField.Length > strTable.Length Then
                    If strField.Substring(0, strTable.Length) = strTable Then
                        Dim strFieldName As String = strField.Substring(strField.LastIndexOf(".") + 1, strField.Length - strField.LastIndexOf(".") - 1)
                        Dim NewFieldNode As XmlNode = dhdText.CreateAppendElement(NewFieldsNode, "Field")
                        dhdText.CreateAppendElement(NewFieldNode, "FieldName", strFieldName)
                        dhdText.CreateAppendElement(NewFieldNode, "FieldShow", GetCtrText(pnlReportDisplay, strField))
                        dhdText.CreateAppendElement(NewFieldNode, "FieldShowMode", GetCtrText(pnlReportShowMode, strField))
                        dhdText.CreateAppendElement(NewFieldNode, "FieldSort", GetCtrText(pnlReportSort, strField))
                        dhdText.CreateAppendElement(NewFieldNode, "FieldSortOrder", GetCtrText(pnlReportSortOrder, strField))

                        Dim NewFiltersNode As XmlNode = dhdText.CreateAppendElement(NewFieldNode, "Filters")
                        For Each ctrFilter In pnlReportFilter.Controls
                            If ctrFilter.Tag = strField Then
                                intControlNumber = ctrFilter.Name.ToString.Substring(ctrFilter.Name.ToString.Length - strField.Length - 1, 1)
                                Dim NewFilterNode As XmlNode = dhdText.CreateAppendElement(NewFiltersNode, "Filter")
                                dhdText.CreateAppendElement(NewFilterNode, "FilterNumber", intControlNumber)
                                dhdText.CreateAppendElement(NewFilterNode, "FilterEnabled", ctrFilter.Checkstate.ToString)
                                dhdText.CreateAppendElement(NewFilterNode, "FilterMode", GetCtrText(pnlReportFilterMode, strField, intControlNumber))
                                dhdText.CreateAppendElement(NewFilterNode, "FilterType", GetCtrText(pnlReportFilterType, strField, intControlNumber))
                                dhdText.CreateAppendElement(NewFilterNode, "FilterText", GetCtrText(pnlReportFilterText, strField, intControlNumber))
                            End If
                        Next
                    End If
                End If
            Next
            Dim NewRelationsNode As XmlNode = dhdText.CreateAppendElement(NewTableNode, "Relations")
            For Each ctrRelation In pnlRelationsUse.Controls
                If ctrRelation.Tag = strTable Then
                    Dim NewRelationNode As XmlNode = dhdText.CreateAppendElement(NewRelationsNode, "Relation")
                    intControlNumber = ctrRelation.Name.ToString.Substring(ctrRelation.Name.ToString.Length - strTable.Length - 1, 1)
                    dhdText.CreateAppendElement(NewRelationNode, "RelationNumber", intControlNumber)
                    dhdText.CreateAppendElement(NewRelationNode, "RelationEnabled", ctrRelation.Checked.ToString)
                    dhdText.CreateAppendElement(NewRelationNode, "RelationSource", GetCtrText(pnlRelationsField, strTable, intControlNumber))
                    dhdText.CreateAppendElement(NewRelationNode, "RelationTarget", GetCtrText(pnlRelationsRelation, strTable, intControlNumber))
                    dhdText.CreateAppendElement(NewRelationNode, "RelationJoinType", GetCtrText(pnlRelationsJoinType, strTable, intControlNumber))
                End If
            Next

        Next
    End Sub

    Private Sub ReportDelete()
        Dim strSelection As String = cbxReportName.Text
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strSelection)

        If Not xNode Is Nothing Then
            xNode.ParentNode.RemoveChild(xNode)
        End If
    End Sub

    Private Sub SaveQuery()
        Dim saveFile1 As New SaveFileDialog()

        saveFile1.DefaultExt = "*.sql"
        saveFile1.Filter = "Query Files|*.sql"

        If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (saveFile1.FileName.Length) > 0 Then
            rtbQuery.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub LoadQuery()
        Dim loadFile1 As New OpenFileDialog

        loadFile1.DefaultExt = "*.sql"
        loadFile1.Filter = "Query Files|*.sql"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            rtbQuery.LoadFile(loadFile1.FileName, RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub btnDefinition_Click(sender As Object, e As EventArgs) Handles btnDefinition.Click
        tabReports.SelectedTab = tpgReportDefinition
    End Sub

#End Region

    Private Sub tmrElapsedTime_Tick(sender As Object, e As EventArgs) Handles tmrElapsedTime.Tick
        tmsElapsedTime = Now() - dtmElapsedTime
        lblElapsedTime.Text = tmsElapsedTime.ToString
    End Sub

    Private Sub btnReportExport_Click(sender As Object, e As EventArgs) Handles btnReportExport.Click
        Dim xmlExport As New XmlDocument
        Dim xmlDec As XmlDeclaration = xmlExport.CreateXmlDeclaration("1.0", "utf-8", "yes")
        xmlExport.InsertBefore(xmlDec, xmlExport.DocumentElement)
        ReportAdd(xmlExport)

        Dim sfdFile As New SaveFileDialog
        sfdFile.FileName = cbxReportName.Text
        sfdFile.Filter = "XML File (*.xml)|*.xml"
        sfdFile.FilterIndex = 1
        sfdFile.RestoreDirectory = True
        sfdFile.OverwritePrompt = False

        If (sfdFile.ShowDialog() <> DialogResult.OK) Then
            Return
        End If

        Dim strTargetFile As String = sfdFile.FileName

        Try
            Using sw As New System.IO.StringWriter()
                ' Make the XmlTextWriter to format the XML.
                Using xml_writer As New XmlTextWriter(sw)
                    xml_writer.Formatting = Formatting.Indented
                    'dtsInput.WriteXml(xml_writer)
                    xmlExport.WriteTo(xml_writer)
                    xml_writer.Flush()

                    'Write the XML to disk
                    dhdText.CreateFile(sw.ToString(), strTargetFile)
                End Using
            End Using

        Catch ex As Exception
            WriteLog(ex.Message, 1)
        End Try
    End Sub

    Private Sub btnReportImport_Click(sender As Object, e As EventArgs) Handles btnReportImport.Click
        Dim xmlImport As New XmlDocument
        Dim strReportName As String
        Dim loadFile1 As New OpenFileDialog

        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Report Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            xmlImport.Load(loadFile1.FileName)
        End If
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlImport, "ReportName")
        strReportName = xNode.InnerText
        ReportLoad(xmlImport, strReportName)
        cbxReportName.Text = strReportName
    End Sub

    Private Sub pnlSelectedFieldsMain_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles pnlSelectedFieldsMain.MouseDown
        pnlSelectedFieldsMain.Focus()
        pnlSelectedFieldsMain.Invalidate()
    End Sub

    Private Sub pnlSelectedFieldsMain_MouseWheel(sender As Object, e As MouseEventArgs) Handles pnlSelectedFieldsMain.MouseWheel

        Dim numberOfTextLinesToMove As Integer = CInt(e.Delta * SystemInformation.MouseWheelScrollLines / 120)
        Dim numberOfPixelsToMove As Integer = numberOfTextLinesToMove * fontSize

        If numberOfPixelsToMove <> 0 Then
            Dim translateMatrix As New System.Drawing.Drawing2D.Matrix()
            translateMatrix.Translate(0, numberOfPixelsToMove)
            mousePath.Transform(translateMatrix)
        End If

        pnlSelectedFieldsMain.Invalidate()
    End Sub

    Private Sub dgvReport_DoubleClick(sender As Object, e As MouseEventArgs) Handles dgvReport.DoubleClick
        Dim args As MouseEventArgs = e
        Dim dgv As DataGridView = sender
        Dim hit As DataGridView.HitTestInfo = dgv.HitTest(args.X, args.Y)
        If (hit.Type = DataGridViewHitTestType.TopLeftHeader) Then
            DataGridViewColumnSize(sender)
        End If

    End Sub

    Private Sub tpgReportDefinition_Resize(sender As Object, e As EventArgs) Handles tpgReportDefinition.Resize
        If tpgReportDefinition.Width < 1200 Then
            pnlReportName.Anchor = AnchorStyles.Top Or AnchorStyles.Left
            pnlReportName.Left = cbxTable.Left + cbxTable.Width + CurVar.BuildMargin
        Else
            pnlReportName.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        End If
    End Sub
End Class