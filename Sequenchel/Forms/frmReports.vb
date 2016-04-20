Imports System.Xml

Public Class frmReports
    Private mousePath As System.Drawing.Drawing2D.GraphicsPath
    Private fontSize As Integer = 20

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblLicense.Text = "Licensed to: " & Core.LicenseName
        lblLicense.Left = Me.Width - lblLicense.Width - (SeqData.curVar.BuildMargin * 5)

        mousePath = New System.Drawing.Drawing2D.GraphicsPath()

        DebugSettings()
        SecuritySet()
        'ReportFieldsDispose(True)
        LoadConnections()
        cbxEmailResults.SelectedIndex = 0
        'LoadTableFields()
        'ReportsLoad()
    End Sub

    Private Sub frmReports_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If cbxConnection.SelectedIndex <> -1 Then
            SeqData.curStatus.Connection = cbxConnection.SelectedItem
            If Not SeqData.curStatus.Connection = Nothing Then
                SeqData.LoadConnection(xmlConnections, SeqData.curStatus.Connection)
                If cbxTableSet.SelectedIndex <> -1 Then
                    SeqData.curStatus.TableSet = cbxTableSet.SelectedItem
                    If Not SeqData.curStatus.TableSet = Nothing Then
                        SeqData.LoadTableSet(xmlTableSets, SeqData.curStatus.TableSet)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        MessageBox.Show(pnlReportLabel.Controls.Count)
    End Sub

    Private Sub DebugSettings()
        If SeqData.curVar.DebugMode Then
            btnTest.Visible = True
        End If
        If SeqData.curVar.DevMode Then
            'mnuMain.Visible = True
        End If
    End Sub

    Private Sub SecuritySet()
        If SeqData.curVar.AllowQueryEdit = False And SeqData.curVar.SecurityOverride = False Then
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
            CursorControl("Wait")
            SeqData.curStatus.Connection = cbxConnection.SelectedItem
            SeqData.LoadConnection(xmlConnections, SeqData.curStatus.Connection)
            LoadTableSets()
            CursorControl()
        End If
    End Sub

    Private Sub btnTableSetsReload_Click(sender As Object, e As EventArgs) Handles btnTableSetsReload.Click
        LoadTableSets()
    End Sub

    Private Sub cbxTableSet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxTableSet.SelectedIndexChanged
        If cbxTableSet.SelectedIndex >= -1 Then
            CursorControl("Wait")
            SeqData.curStatus.TableSet = cbxTableSet.SelectedItem
            SeqData.LoadTableSet(xmlTableSets, SeqData.curStatus.TableSet)
            LoadTables()
            LoadTableFields()
            ReportsLoad()
            CursorControl()
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

        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        Dim xCNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
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
        cbxConnection.SelectedItem = SeqData.CurStatus.Connection
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
        'cbxTable.SelectedItem = SeqData.CurStatus.Table
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

    Friend Sub PanelsSuspendLayout()
        pnlReportLabel.SuspendLayout()
        pnlReportDisplay.SuspendLayout()
        pnlReportShowMode.SuspendLayout()
        pnlReportSort.SuspendLayout()
        pnlReportSortOrder.SuspendLayout()
        pnlReportFilter.SuspendLayout()
        pnlReportFilterMode.SuspendLayout()
        pnlReportFilterType.SuspendLayout()
        pnlReportFilterText.SuspendLayout()

        pnlRelationsLabel.SuspendLayout()
        pnlRelationsUse.SuspendLayout()
        pnlRelationsField.SuspendLayout()
        pnlRelationsRelation.SuspendLayout()
        pnlRelationsJoinType.SuspendLayout()
        pnlSelectedFieldsMain.SuspendLayout()
    End Sub

    Friend Sub PanelsResumeLayout()
        pnlReportLabel.ResumeLayout()
        pnlReportDisplay.ResumeLayout()
        pnlReportShowMode.ResumeLayout()
        pnlReportSort.ResumeLayout()
        pnlReportSortOrder.ResumeLayout()
        pnlReportFilter.ResumeLayout()
        pnlReportFilterMode.ResumeLayout()
        pnlReportFilterType.ResumeLayout()
        pnlReportFilterText.ResumeLayout()

        pnlRelationsLabel.ResumeLayout()
        pnlRelationsUse.ResumeLayout()
        pnlRelationsField.ResumeLayout()
        pnlRelationsRelation.ResumeLayout()
        pnlRelationsJoinType.ResumeLayout()
        pnlSelectedFieldsMain.ResumeLayout()
    End Sub

    Private Sub FieldAdd(strFieldName As String, strFieldAlias As String, strTableAlias As String)
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        Dim xCNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
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
        lblNew.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count) * SeqData.curVar.BuildMargin)

        If lblNew.Width > SeqData.CurStatus.ReportLabelWidth Then
            SeqData.CurStatus.ReportLabelWidth = lblNew.Width
        End If

        Dim cbxNewSort As New ComboField
        cbxNewSort.Name = pnlReportSort.Name & "1" & strFieldName
        cbxNewSort.Tag = strFieldName
        cbxNewSort.Width = 50
        pnlReportSort.Controls.Add(cbxNewSort)
        cbxNewSort.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewSort.Left = SeqData.CurVar.BuildMargin
        ComboBoxFill(cbxNewSort, "Sort")

        Dim txtNewSortOrder As New TextField
        txtNewSortOrder.Name = pnlReportSortOrder.Name & "1" & strFieldName
        txtNewSortOrder.Tag = strFieldName
        txtNewSortOrder.Width = 20
        pnlReportSortOrder.Controls.Add(txtNewSortOrder)
        txtNewSortOrder.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        txtNewSortOrder.Left = SeqData.CurVar.BuildMargin

    End Sub

    Private Sub FieldCheckShowAdd(strFieldName As String, strFieldType As String)
        Dim chkNewShow As New CheckField
        chkNewShow.Name = pnlReportDisplay.Name & "1" & strFieldName
        chkNewShow.Tag = strFieldName
        'chkNewShow.ThreeState = True
        pnlReportDisplay.Controls.Add(chkNewShow)
        chkNewShow.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        chkNewShow.Left = SeqData.CurVar.BuildMargin

        Dim cbxNewShowMode As New ComboField
        cbxNewShowMode.Name = pnlReportShowMode.Name & "1" & strFieldName
        cbxNewShowMode.FieldDataType = strFieldType
        cbxNewShowMode.Tag = strFieldName
        cbxNewShowMode.Width = 75
        pnlReportShowMode.Controls.Add(cbxNewShowMode)
        cbxNewShowMode.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewShowMode.Left = SeqData.CurVar.BuildMargin
        'Dim strDataType As String = 
        ComboBoxFill(cbxNewShowMode, "ShowMode")
    End Sub

    Private Sub FieldCheckFilterAdd(strFieldName As String, strFieldType As String, Optional intCount As Integer = 0)

        'Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        'Dim xCNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
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
        'chkNewFilter.Top = ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        chkNewFilter.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        chkNewFilter.Left = SeqData.CurVar.BuildMargin

        Dim cbxNewFilterMode As New ComboField
        cbxNewFilterMode.Name = pnlReportFilterMode.Name & intCount.ToString & strFieldName
        cbxNewFilterMode.Tag = strFieldName
        cbxNewFilterMode.Width = 75
        pnlReportFilterMode.Controls.Add(cbxNewFilterMode)
        'cbxNewFilterMode.Top = ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewFilterMode.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewFilterMode.Left = SeqData.CurVar.BuildMargin
        ComboBoxFill(cbxNewFilterMode, "FilterMode")

        Dim cbxNewFilterType As New ComboField
        cbxNewFilterType.Name = pnlReportFilterType.Name & intCount.ToString & strFieldName
        cbxNewFilterType.Tag = strFieldName
        cbxNewFilterType.Width = 75
        pnlReportFilterType.Controls.Add(cbxNewFilterType)
        'cbxNewFilterType.Top = ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewFilterType.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewFilterType.Left = SeqData.CurVar.BuildMargin
        ComboBoxFill(cbxNewFilterType, "FilterType")

        Dim txtNewFilter As New TextField
        txtNewFilter.Name = pnlReportFilterText.Name & intCount.ToString & strFieldName
        txtNewFilter.FieldDataType = strFieldType
        txtNewFilter.Tag = strFieldName
        txtNewFilter.Width = 190
        pnlReportFilterText.Controls.Add(txtNewFilter)
        'txtNewFilter.Top = ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        txtNewFilter.Top = ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        txtNewFilter.Left = SeqData.CurVar.BuildMargin

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
                Dim lstFields As List(Of String) = SeqData.dhdText.LoadItemList(xmlTables, "Table", "Name", strSource, "Field", "FldName")
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
        pnlReportLabel.Width = SeqData.CurStatus.ReportLabelWidth + SeqData.CurVar.BuildMargin
        pnlReportDisplay.Left = pnlReportLabel.Left + pnlReportLabel.Width
        chkReportShow.Left = pnlReportDisplay.Left + SeqData.CurVar.BuildMargin - 20

        pnlReportShowMode.Left = pnlReportDisplay.Left + pnlReportDisplay.Width
        lblReportShowMode.Left = pnlReportShowMode.Left + SeqData.CurVar.BuildMargin

        pnlReportSort.Left = pnlReportShowMode.Left + pnlReportShowMode.Width
        lblReportSort.Left = pnlReportSort.Left + SeqData.CurVar.BuildMargin

        pnlReportSortOrder.Left = pnlReportSort.Left + pnlReportSort.Width
        lblReportSortOrder.Left = pnlReportSortOrder.Left + SeqData.CurVar.BuildMargin

        pnlReportFilter.Left = pnlReportSortOrder.Left + pnlReportSortOrder.Width
        lblReportFilter.Left = pnlReportFilter.Left + SeqData.CurVar.BuildMargin

        pnlReportFilterMode.Left = pnlReportFilter.Left + pnlReportFilter.Width
        lblReportFilterMode.Left = pnlReportFilterMode.Left + SeqData.CurVar.BuildMargin

        pnlReportFilterType.Left = pnlReportFilterMode.Left + pnlReportFilterMode.Width
        lblReportFilterType.Left = pnlReportFilterType.Left + SeqData.CurVar.BuildMargin

        pnlReportFilterText.Left = pnlReportFilterType.Left + pnlReportFilterType.Width
        lblReportFilterText.Left = pnlReportFilterText.Left + SeqData.CurVar.BuildMargin
        If pnlReportFilterText.Left + pnlReportFilterText.Width + (SeqData.CurVar.BuildMargin * 2) > pnlSelectedFields.Width Then pnlSelectedFields.Width = pnlReportFilterText.Left + pnlReportFilterText.Width + (SeqData.CurVar.BuildMargin * 2)
    End Sub

    Private Sub PanelRelationWidthSet()
        pnlRelationsLabel.Width = SeqData.CurStatus.RelationLabelWidth + SeqData.CurVar.BuildMargin
        pnlRelationsUse.Left = pnlRelationsLabel.Left + pnlRelationsLabel.Width
        lblRelationUse.Left = pnlRelationsUse.Left

        pnlRelationsField.Left = pnlRelationsUse.Left + pnlRelationsUse.Width
        lblSourceField.Left = pnlRelationsField.Left + SeqData.curVar.BuildMargin

        pnlRelationsRelation.Left = pnlRelationsField.Left + pnlRelationsField.Width
        lblRelationsRelation.Left = pnlRelationsRelation.Left + SeqData.curVar.BuildMargin

        pnlRelationsJoinType.Left = pnlRelationsRelation.Left + pnlRelationsRelation.Width
        lblRelationsJoinType.Left = pnlRelationsJoinType.Left + SeqData.curVar.BuildMargin

        pnlRelations.Width = pnlRelationsJoinType.Left + pnlRelationsJoinType.Width + (SeqData.curVar.BuildMargin * 2)
    End Sub

    Private Sub PanelFieldHeightSet()
        If (pnlReportFilterText.Controls.Count + 1) * (SeqData.CurVar.BuildMargin + SeqData.CurVar.FieldHeight) > pnlReportFilterText.Height Then
            pnlReportFilterText.Height = (pnlReportFilterText.Controls.Count + 1) * (SeqData.CurVar.BuildMargin + SeqData.CurVar.FieldHeight)
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
        If (pnlRelationsUse.Controls.Count + 1) * (SeqData.CurVar.BuildMargin + SeqData.CurVar.FieldHeight) > pnlRelationsUse.Height Then
            pnlRelationsUse.Height = (pnlRelationsUse.Controls.Count + 1) * (SeqData.CurVar.BuildMargin + SeqData.CurVar.FieldHeight)
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
        SeqData.CurStatus.ReportMaxTop = SeqData.CurVar.BuildMargin
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
                        'MessageBox.Show(CurStatus.ReportMaxTop & Environment.NewLine & ((intControlNumber - 1) * SeqData.CurVar.FieldHeight) & Environment.NewLine & ((intControlNumber - 1) * SeqData.CurVar.BuildMargin))
                        ctrControl.Top = SeqData.CurStatus.ReportMaxTop + ((intControlNumber - 1) * SeqData.CurVar.FieldHeight) + ((intControlNumber - 1) * SeqData.CurVar.BuildMargin)
                    End If

                End If
            Next
            SeqData.CurStatus.ReportMaxTop += intMaxNumber * SeqData.CurVar.FieldHeight + intMaxNumber * SeqData.CurVar.BuildMargin
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
        lblNew.Top = ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count) * SeqData.CurVar.BuildMargin)

        If lblNew.Width > SeqData.CurStatus.RelationLabelWidth Then
            SeqData.CurStatus.RelationLabelWidth = lblNew.Width
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
        chkNewShow.Top = ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        chkNewShow.Left = SeqData.CurVar.BuildMargin

        Dim cbxNewField As New ComboField
        cbxNewField.Name = pnlRelationsField.Name & intCount.ToString & strTableName
        cbxNewField.Tag = strTableName
        cbxNewField.Width = 140
        pnlRelationsField.Controls.Add(cbxNewField)
        cbxNewField.Top = ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewField.Left = SeqData.CurVar.BuildMargin
        ComboBoxFill(cbxNewField, "RelationField", strTableName)

        Dim txtNewFilter As New TextField
        txtNewFilter.Name = pnlRelationsRelation.Name & intCount.ToString & strTableName
        txtNewFilter.Tag = strTableName
        txtNewFilter.Width = 200
        pnlRelationsRelation.Controls.Add(txtNewFilter)
        txtNewFilter.Top = ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        txtNewFilter.Left = SeqData.CurVar.BuildMargin

        Dim cbxNewJoinType As New ComboField
        cbxNewJoinType.Name = pnlRelationsJoinType.Name & intCount.ToString & strTableName
        cbxNewJoinType.Tag = strTableName
        cbxNewJoinType.Width = 110
        pnlRelationsJoinType.Controls.Add(cbxNewJoinType)
        cbxNewJoinType.Top = ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * SeqData.CurVar.BuildMargin)
        cbxNewJoinType.Left = SeqData.CurVar.BuildMargin
        ComboBoxFill(cbxNewJoinType, "JoinType")

        PanelsRelationSort()
    End Sub

    Private Sub RelationsLoad(strTable As String)
        Dim xPNode As System.Xml.XmlNode = SeqData.dhdText.FindXmlNode(xmlTables, "Table", "Name", strTable)
        Dim xmlFieldList As System.Xml.XmlNodeList = SeqData.dhdText.FindXmlChildNodes(xPNode, "Fields/Field/Relations", "Relation")
        Dim intCount As Integer = 0
        For Each xmlCNode As System.Xml.XmlNode In xmlFieldList
            Dim xmlRelationList As System.Xml.XmlNodeList = SeqData.dhdText.FindXmlChildNodes(xmlCNode, "Relation")
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
        lblStatus.Text = ""
        CursorControl()
    End Sub

    Private Sub btnReportCreate_Click(sender As Object, e As EventArgs) Handles btnReportCreate.Click
        CursorControl("Wait")
        If QueryShow() = True Then
            QueryExecute()
        End If
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
        If strQuery.Length > 0 Then QueryExecute()
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

    Private Function QueryShow() As Boolean
        ReportClear(True)
        Dim xmlReportShow As XmlDocument = SeqData.dhdText.CreateRootDocument(Nothing, Nothing, Nothing)
        Dim strReportName As String = cbxReportName.Text
        If strReportName = "" Then strReportName = "TempReport"
        'ReportAdd(xmlReportShow, strReportName)
        ReportToXML(xmlReportShow, strReportName)
        strQuery = SeqData.ReportQueryBuild(xmlReportShow, xmlTables, strReportName, SeqData.curVar.DateTimeStyle)
        If strQuery = Nothing Then
            WriteStatus("No fields were selected. Select at least 1 field to be shown in your report", 0, lblStatus)
            Return False
        End If
        'strQuery = ReportQueryBuild2()
        rtbQuery.Text = strQuery
        tabReports.SelectedTab = tpgReportResult
        Return True
    End Function

    Private Sub QueryExecute()
        ReportClear(False)
        dtmElapsedTime = Now()
        tmrElapsedTime.Enabled = True
        tmrElapsedTime.Start()
        dtsReport = Nothing
        dtsReport = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True)
        ReportShow(dtsReport)
        tmrElapsedTime.Stop()
        tmrElapsedTime.Enabled = False
        tmsElapsedTime = Now() - dtmElapsedTime
        lblElapsedTime.Text = tmsElapsedTime.ToString
    End Sub

    Private Sub btnSaveQuery_Click(sender As Object, e As EventArgs) Handles btnSaveQuery.Click
        CursorControl("Wait")
        SaveQuery()
        CursorControl()
    End Sub

    Private Sub btnLoadQuery_Click(sender As Object, e As EventArgs) Handles btnLoadQuery.Click
        CursorControl("Wait")
        LoadQuery()
        CursorControl()
    End Sub

    Private Sub btnReportAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnReportAddOrUpdate.Click
        CursorControl("Wait")
        If cbxReportName.Text.Length < 3 Then
            'MessageBox.Show("Report Name must be at least 3 characters long")
            WriteStatus("Report Name must be at least 3 characters long.", 0, lblStatus)
            Exit Sub
        End If
        If lvwSelectedFields.Items.Count = 0 Then
            WriteStatus("No fields have been selected for this report. Aborting save.", 0, lblStatus)
            'MessageBox.Show("No fields have been selected for this report. Aborting save.", "No fields selected", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Sub
        End If
        ReportDelete(cbxReportName.Text)
        ReportToXML(xmlReports, cbxReportName.Text)
        'ReportAdd(xmlReports, cbxReportName.Text)
        If Not cbxReportName.Items.Contains(cbxReportName.Text) Then cbxReportName.Items.Add(cbxReportName.Text)

        If SaveXmlFile(xmlReports, SeqData.curVar.ReportSetFile, True) = False Then
            WriteStatus("The file " & SeqData.curVar.ReportSetFile & " was not saved.", 0, lblStatus)
            'MessageBox.Show("The file " & SeqData.curVar.ReportSetFile & " was not saved.")
        End If
        CursorControl()
    End Sub

    Private Sub btnReportDelete_Click(sender As Object, e As EventArgs) Handles btnReportDelete.Click
        If MessageBox.Show("This will permanently remove the Item: " & cbxReportName.Text & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        CursorControl("Wait")
        ReportDelete(cbxReportName.Text)
        If cbxReportName.Items.Contains(cbxReportName.Text) Then cbxReportName.Items.Remove(cbxReportName.Text)
        cbxReportName.SelectedIndex = -1
        cbxReportName.Text = ""
        ReportFieldsDispose(False)

        If SaveXmlFile(xmlReports, SeqData.curVar.ReportSetFile, True) = False Then
            MessageBox.Show("The file " & SeqData.curVar.ReportSetFile & " was not saved.")
        End If
        CursorControl()
    End Sub

    Private Sub cbxReportName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxReportName.SelectedIndexChanged
        CursorControl("Wait")
        PanelsSuspendLayout()
        If cbxReportName.SelectedIndex >= 0 Then
            ReportFieldsDispose(False)
            ReportLoad(xmlReports, cbxReportName.Text)
        End If
        pnlSelectedFieldsMain.Focus()
        pnlSelectedFieldsMain.Invalidate()
        PanelsResumeLayout()
        CursorControl()
    End Sub

    Private Sub btnRevertChanges_Click(sender As Object, e As EventArgs) Handles btnRevertChanges.Click
        CursorControl("Wait")
        ReportFieldsDispose(False)
        ReportLoad(xmlReports, cbxReportName.Text)
        CursorControl()
    End Sub

    Private Sub btnExportToFile_Click(sender As Object, e As EventArgs) Handles btnExportToFile.Click
        If dgvReport.RowCount = 0 Then Exit Sub
        CursorControl("Wait")
        Dim strReportName As String = ""
        If cbxReportName.Text.Length > 0 Then
            strReportName = cbxReportName.Text
        Else
            strReportName = SeqData.curStatus.Connection
        End If
        Dim strFileName As String = GetSaveFileName(strReportName)
        ExportFile(dtsReport, strFileName)
        CursorControl()
    End Sub

#End Region

    Private Sub ReportsLoad()
        cbxReportName.Items.Clear()
        cbxReportName.Text = ""
        Dim lstReports As List(Of String) = SeqData.LoadReportsXml(xmlReports)
        If lstReports Is Nothing Then
            xmlReports.RemoveAll()
            Exit Sub
        End If
        For Each lstItem As String In lstReports
            cbxReportName.Items.Add(lstItem)
        Next
    End Sub

    Private Sub ReportLoad(xmlReports As XmlDocument, strReportName As String)
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strReportName)
        If Not xNode Is Nothing Then
            Dim xFNode As XmlNode = SeqData.dhdText.FindXmlChildNode(xNode, "Tables/Table")
            If xFNode Is Nothing Then
                XmlToReport(xmlReports, strReportName)
            Else
                XmlToReport_old(xmlReports, strReportName)
            End If
        End If
    End Sub

    Private Sub XmlToReport_old(xmlReports As XmlDocument, strReportName As String)
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strReportName)
        Dim strTable As String = "", strField As String = "", intRelationNumber As Integer = 0

        If Not xNode Is Nothing Then
            If SeqData.dhdText.CheckNodeElement(xNode, "Description") Then txtDescription.Text = xNode.Item("Description").InnerText
            chkTop.Checked = xNode.Item("UseTop").InnerText
            txtTop.Text = xNode.Item("UseTopNumber").InnerText
            chkDistinct.Checked = xNode.Item("UseDistinct").InnerText
            For Each xCNode As XmlNode In SeqData.dhdText.FindXmlChildNodes(xNode, "Tables/Table/Fields/Field")
                strTable = xCNode.ParentNode.ParentNode.Item("TableName").InnerText
                If strTable.IndexOf(".") = -1 Then strTable = "dbo." & strTable
                strField = xCNode.Item("FieldName").InnerText
                Try
                    lvwAvailableFields.Items(strTable & "." & strField).Selected = True
                    btnReportFieldAdd_Click(Nothing, Nothing)
                    SetCtrText(pnlReportDisplay, strTable & "." & strField, xCNode.Item("FieldShow").InnerText)
                    SetCtrText(pnlReportShowMode, strTable & "." & strField, xCNode.Item("FieldShowMode").InnerText)
                    SetCtrText(pnlReportSort, strTable & "." & strField, xCNode.Item("FieldSort").InnerText)
                    SetCtrText(pnlReportSortOrder, strTable & "." & strField, xCNode.Item("FieldSortOrder").InnerText)

                    For Each xFnode In SeqData.dhdText.FindXmlChildNodes(xCNode, "Filters/Filter")
                        SetCtrText(pnlReportFilter, strTable & "." & strField, xFnode.Item("FilterEnabled").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterMode, strTable & "." & strField, xFnode.Item("FilterMode").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterType, strTable & "." & strField, xFnode.Item("FilterType").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterText, strTable & "." & strField, xFnode.Item("FilterText").InnerText, xFnode.Item("FilterNumber").InnerText)
                    Next
                Catch ex As Exception
                    If MessageBox.Show("Unable to load the Field " & strTable & "." & strField & Environment.NewLine & "Do you wish to keep loading the report?", "Error Loading Report", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
                End Try
            Next

            For Each xRNode As XmlNode In SeqData.dhdText.FindXmlChildNodes(xNode, "Tables/Table/Relations/Relation")
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

    Private Sub XmlToReport(xmlReports As XmlDocument, strReportName As String)
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strReportName)
        Dim strTable As String = "", strField As String = "", intRelationNumber As Integer = 0

        If Not xNode Is Nothing Then
            If SeqData.dhdText.CheckNodeElement(xNode, "Description") Then txtDescription.Text = xNode.Item("Description").InnerText
            chkTop.Checked = xNode.Item("UseTop").InnerText
            txtTop.Text = xNode.Item("UseTopNumber").InnerText
            chkDistinct.Checked = xNode.Item("UseDistinct").InnerText
            For Each xCNode As XmlNode In SeqData.dhdText.FindXmlChildNodes(xNode, "Fields/Field")
                strTable = xCNode.Item("TableName").InnerText
                If strTable.IndexOf(".") = -1 Then strTable = "dbo." & strTable
                strField = xCNode.Item("FieldName").InnerText
                Try
                    lvwAvailableFields.Items(strTable & "." & strField).Selected = True
                    btnReportFieldAdd_Click(Nothing, Nothing)
                    SetCtrText(pnlReportDisplay, strTable & "." & strField, xCNode.Item("FieldShow").InnerText)
                    SetCtrText(pnlReportShowMode, strTable & "." & strField, xCNode.Item("FieldShowMode").InnerText)
                    SetCtrText(pnlReportSort, strTable & "." & strField, xCNode.Item("FieldSort").InnerText)
                    SetCtrText(pnlReportSortOrder, strTable & "." & strField, xCNode.Item("FieldSortOrder").InnerText)

                    For Each xFnode In SeqData.dhdText.FindXmlChildNodes(xCNode, "Filters/Filter")
                        SetCtrText(pnlReportFilter, strTable & "." & strField, xFnode.Item("FilterEnabled").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterMode, strTable & "." & strField, xFnode.Item("FilterMode").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterType, strTable & "." & strField, xFnode.Item("FilterType").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterText, strTable & "." & strField, xFnode.Item("FilterText").InnerText, xFnode.Item("FilterNumber").InnerText)
                    Next
                Catch ex As Exception
                    If MessageBox.Show("Unable to load the Field " & strTable & "." & strField & Environment.NewLine & "Do you wish to keep loading the report?", "Error Loading Report", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
                End Try
            Next

            For Each xRNode As XmlNode In SeqData.dhdText.FindXmlChildNodes(xNode, "Relations/Relation")
                strTable = xRNode.Item("TableName").InnerText
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
                    SeqData.WriteLog("unable to add extra Filtercriterium for: " & strFieldName, 1)
                Else
                    lblShowField_DoubleClick(lblTarget, Nothing)
                    SetCtrText(pnlInput, strFieldName, strValue, intControlNumber)
                End If
            ElseIf pnlInput.Parent Is pnlRelations Then
                Dim lblTarget As Label = GetLabel(pnlRelationsLabel, strFieldName)
                If lblTarget Is Nothing Then
                    SeqData.WriteLog("unable to add extra Relationcriterium for: " & strFieldName, 1)
                Else
                    lblShowRelation_DoubleClick(lblTarget, Nothing)
                    SetCtrText(pnlInput, strFieldName, strValue, intControlNumber)
                End If
            Else
                SeqData.WriteLog("The Field: " & strFieldName & " on Panel: " & pnlInput.Name & " was not found." & "Unable to load the Value: " & strValue, 1)
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
        If SeqData.dhdText.DatasetCheck(dtsData) = False Then Exit Sub

        Try
            If DataSet2DataGridView(dtsData, 0, dgvReport, True) = False Then
                MessageBox.Show("There was an error loading the report")
            End If
            'dgvReport.DataSource = dtsData.Tables(0)
            lblListCountNumber.Text = dtsData.Tables(0).Rows.Count.ToString
            'DataGridViewColumnSize(dgvReport)
            lblErrorMessage.Text = "Command completed succesfully"
        Catch ex As Exception
            MessageBox.Show("There was an error loading the report" & Environment.NewLine & ex.Message)
        End Try

        'If strErrorMessage = "" Then lblErrorMessage.Text = "Command completed succesfully"
        dgvReport.ClearSelection()

    End Sub

    'Private Sub ReportAdd(xmlDocReport As XmlDocument, strReportName As String)
    '    Dim strTable As String = ""
    '    Dim strField As String = ""
    '    Dim intControlNumber As Integer = 0

    '    Dim root As XmlElement = xmlDocReport.DocumentElement
    '    If root Is Nothing Then
    '        xmlDocReport = SeqData.dhdText.CreateRootDocument(xmlDocReport, "Sequenchel", "Reports", True)
    '    End If

    '    Dim NewReportNode As XmlNode = SeqData.dhdText.CreateAppendElement(xmlDocReport.Item("Sequenchel").Item("Reports"), "Report")
    '    SeqData.dhdText.CreateAppendElement(NewReportNode, "ReportName", strReportName)
    '    SeqData.dhdText.CreateAppendElement(NewReportNode, "Description", txtDescription.Text)
    '    SeqData.dhdText.CreateAppendElement(NewReportNode, "UseTop", chkTop.Checked.ToString)
    '    SeqData.dhdText.CreateAppendElement(NewReportNode, "UseTopNumber", txtTop.Text)
    '    SeqData.dhdText.CreateAppendElement(NewReportNode, "UseDistinct", chkDistinct.Checked.ToString)
    '    Dim NewTablesNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewReportNode, "Tables")
    '    For intTableCount As Integer = 0 To lvwSelectedTables.Items.Count - 1
    '        strTable = lvwSelectedTables.Items.Item(intTableCount).Name
    '        Dim NewTableNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewTablesNode, "Table")
    '        SeqData.dhdText.CreateAppendElement(NewTableNode, "TableName", strTable)
    '        Dim NewFieldsNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewTableNode, "Fields")
    '        For intFieldCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
    '            strField = lvwSelectedFields.Items.Item(intFieldCount).Name
    '            If strField.Length > strTable.Length Then
    '                If strField.Substring(0, strField.LastIndexOf(".")) = strTable Then
    '                    Dim strFieldName As String = strField.Substring(strField.LastIndexOf(".") + 1, strField.Length - strField.LastIndexOf(".") - 1)
    '                    Dim NewFieldNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewFieldsNode, "Field")
    '                    SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldName", strFieldName)
    '                    SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldShow", GetCtrText(pnlReportDisplay, strField))
    '                    SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldShowMode", GetCtrText(pnlReportShowMode, strField))
    '                    SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldSort", GetCtrText(pnlReportSort, strField))
    '                    SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldSortOrder", GetCtrText(pnlReportSortOrder, strField))

    '                    Dim NewFiltersNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewFieldNode, "Filters")
    '                    For Each ctrFilter In pnlReportFilter.Controls
    '                        If ctrFilter.Tag = strField Then
    '                            intControlNumber = ctrFilter.Name.ToString.Substring(ctrFilter.Name.ToString.Length - strField.Length - 1, 1)
    '                            Dim NewFilterNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewFiltersNode, "Filter")
    '                            SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterNumber", intControlNumber)
    '                            SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterEnabled", ctrFilter.Checkstate.ToString)
    '                            SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterMode", GetCtrText(pnlReportFilterMode, strField, intControlNumber))
    '                            SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterType", GetCtrText(pnlReportFilterType, strField, intControlNumber))
    '                            SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterText", GetCtrText(pnlReportFilterText, strField, intControlNumber))
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Next
    '        Dim NewRelationsNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewTableNode, "Relations")
    '        For Each ctrRelation In pnlRelationsUse.Controls
    '            If ctrRelation.Tag = strTable Then
    '                Dim NewRelationNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewRelationsNode, "Relation")
    '                intControlNumber = ctrRelation.Name.ToString.Substring(ctrRelation.Name.ToString.Length - strTable.Length - 1, 1)
    '                SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationNumber", intControlNumber)
    '                SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationEnabled", ctrRelation.Checked.ToString)
    '                SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationSource", GetCtrText(pnlRelationsField, strTable, intControlNumber))
    '                SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationTarget", GetCtrText(pnlRelationsRelation, strTable, intControlNumber))
    '                SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationJoinType", GetCtrText(pnlRelationsJoinType, strTable, intControlNumber))
    '            End If
    '        Next

    '    Next
    'End Sub

    Private Function ReportToXML(xmlReport As XmlDocument, strReportName As String) As XmlDocument
        Dim strTable As String = ""
        Dim strField As String = ""
        Dim intControlNumber As Integer = 0

        Dim root As XmlElement = xmlReport.DocumentElement
        If root Is Nothing Then
            xmlReport = SeqData.dhdText.CreateRootDocument(xmlReport, "Sequenchel", "Reports", True)
        End If
        Dim NewReportNode As XmlNode = SeqData.dhdText.CreateAppendElement(xmlReport.Item("Sequenchel").Item("Reports"), "Report")
        SeqData.dhdText.CreateAppendElement(NewReportNode, "ReportName", strReportName)
        SeqData.dhdText.CreateAppendElement(NewReportNode, "Description", txtDescription.Text)
        SeqData.dhdText.CreateAppendElement(NewReportNode, "UseTop", chkTop.Checked.ToString)
        SeqData.dhdText.CreateAppendElement(NewReportNode, "UseTopNumber", txtTop.Text)
        SeqData.dhdText.CreateAppendElement(NewReportNode, "UseDistinct", chkDistinct.Checked.ToString)

        Dim NewFieldsNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewReportNode, "Fields")
        For intFieldCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
            strField = lvwSelectedFields.Items.Item(intFieldCount).Name
            strTable = strField.Substring(0, strField.LastIndexOf("."))
            'If strField.Length > strTable.Length Then

            Dim strFieldName As String = strField.Substring(strField.LastIndexOf(".") + 1, strField.Length - strField.LastIndexOf(".") - 1)
            Dim NewFieldNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewFieldsNode, "Field")
            SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldName", strFieldName)
            SeqData.dhdText.CreateAppendElement(NewFieldNode, "TableName", strTable)
            SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldShow", GetCtrText(pnlReportDisplay, strField))
            SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldShowMode", GetCtrText(pnlReportShowMode, strField))
            SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldSort", GetCtrText(pnlReportSort, strField))
            SeqData.dhdText.CreateAppendElement(NewFieldNode, "FieldSortOrder", GetCtrText(pnlReportSortOrder, strField))

            Dim NewFiltersNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewFieldNode, "Filters")
            For Each ctrFilter In pnlReportFilter.Controls
                If ctrFilter.Tag = strField Then
                    intControlNumber = ctrFilter.Name.ToString.Substring(ctrFilter.Name.ToString.Length - strField.Length - 1, 1)
                    Dim NewFilterNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewFiltersNode, "Filter")
                    SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterNumber", intControlNumber)
                    SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterEnabled", ctrFilter.Checkstate.ToString)
                    SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterMode", GetCtrText(pnlReportFilterMode, strField, intControlNumber))
                    SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterType", GetCtrText(pnlReportFilterType, strField, intControlNumber))
                    SeqData.dhdText.CreateAppendElement(NewFilterNode, "FilterText", GetCtrText(pnlReportFilterText, strField, intControlNumber))
                End If
            Next

            'End If
        Next

        intControlNumber = 0
        Dim NewRelationsNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewReportNode, "Relations")
        For intTableCount As Integer = 0 To lvwSelectedTables.Items.Count - 1
            strTable = lvwSelectedTables.Items.Item(intTableCount).Name

            For Each ctrRelation In pnlRelationsUse.Controls
                If ctrRelation.Tag = strTable Then
                    Dim NewRelationNode As XmlNode = SeqData.dhdText.CreateAppendElement(NewRelationsNode, "Relation")
                    intControlNumber = ctrRelation.Name.ToString.Substring(ctrRelation.Name.ToString.Length - strTable.Length - 1, 1)
                    SeqData.dhdText.CreateAppendElement(NewRelationNode, "TableName", strTable)
                    SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationNumber", intControlNumber)
                    SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationEnabled", ctrRelation.Checked.ToString)
                    SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationSource", GetCtrText(pnlRelationsField, strTable, intControlNumber))
                    SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationTarget", GetCtrText(pnlRelationsRelation, strTable, intControlNumber))
                    SeqData.dhdText.CreateAppendElement(NewRelationNode, "RelationJoinType", GetCtrText(pnlRelationsJoinType, strTable, intControlNumber))
                End If
            Next
        Next
        Return xmlReport
    End Function

    Private Sub ReportDelete(strSelection As String)
        SeqData.dhdText.RemoveNode(xmlReports, "Report", "ReportName", strSelection)
    End Sub

    Private Sub SaveQuery()
        If rtbQuery.Text.Length = 0 Then Exit Sub
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
        CursorControl("Wait")
        tabReports.SelectedTab = tpgReportDefinition
        CursorControl()
    End Sub

#End Region

    Private Sub tmrElapsedTime_Tick(sender As Object, e As EventArgs) Handles tmrElapsedTime.Tick
        tmsElapsedTime = Now() - dtmElapsedTime
        lblElapsedTime.Text = tmsElapsedTime.ToString
    End Sub

    Private Sub btnReportExport_Click(sender As Object, e As EventArgs) Handles btnReportExport.Click
        CursorControl("Wait")
        Dim xmlExport As XmlDocument = SeqData.dhdText.CreateRootDocument(Nothing, Nothing, Nothing)
        'ReportAdd(xmlExport, cbxReportName.Text)
        ReportToXML(xmlExport, cbxReportName.Text)

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
                    SeqData.dhdText.CreateFile(sw.ToString(), strTargetFile)
                End Using
            End Using

        Catch ex As Exception
            SeqData.WriteLog(ex.Message, 1)
        End Try
        CursorControl()
    End Sub

    Private Sub btnReportImport_Click(sender As Object, e As EventArgs) Handles btnReportImport.Click
        CursorControl("Wait")
        Dim xmlImport As New XmlDocument
        Dim strReportName As String
        Dim loadFile1 As New OpenFileDialog

        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Report Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            Try
                xmlImport.Load(loadFile1.FileName)
            Catch ex As Exception
                SeqData.WriteLog("Error importing Report: " & ex.Message, 1)
                CursorControl()
                Exit Sub
            End Try
        End If
        If loadFile1.FileName = "" Then Exit Sub
        Dim xNode As XmlNode = SeqData.dhdText.FindXmlNode(xmlImport, "ReportName")
        strReportName = xNode.InnerText
        ReportFieldsDispose(False)
        ReportLoad(xmlImport, strReportName)
        'XmlToReport(xmlImport, strReportName)
        If cbxReportName.Items.Contains(strReportName) Then
            strReportName &= "_" & FormatDateTime(Now())
        End If
        cbxReportName.Text = strReportName
        CursorControl()
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

    'Private Sub tpgReportDefinition_Resize(sender As Object, e As EventArgs) Handles tpgReportDefinition.Resize
    '    If tpgReportDefinition.Width < 1200 Then
    '        pnlReportName.Anchor = AnchorStyles.Top Or AnchorStyles.Left
    '        pnlReportName.Left = cbxTable.Left + cbxTable.Width + SeqData.CurVar.BuildMargin
    '    Else
    '        pnlReportName.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    '    End If
    'End Sub

    Private Sub chkShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkReportShow.CheckedChanged
        For Each chkBox As CheckBox In pnlReportDisplay.Controls
            chkBox.Checked = chkReportShow.Checked
        Next
    End Sub

    Private Sub btnEmailResults_Click(sender As Object, e As EventArgs) Handles btnEmailResults.Click
        If SeqData.dhdText.SmtpServer.Length = 0 Then
            WriteStatus("Email is not configured. No email was sent", 0, lblErrorMessage)
            Exit Sub
        End If
        If dgvReport.RowCount = 0 Then Exit Sub
        SeqData.dhdText.SmtpRecipient = InputBox("Please enter a valid Email Address", "Email", SeqData.dhdText.SmtpRecipient)
        If SeqData.dhdText.SmtpRecipient = "" Or SeqData.dhdText.EmailAddressCheck(SeqData.dhdText.SmtpRecipient) = False Then
            WriteStatus("Email address not valid. No email was sent", 0, lblErrorMessage)
            Exit Sub
        End If
        CursorControl("Wait")
        Dim strRecepientName As String = SeqData.dhdText.SmtpRecipient.Substring(0, SeqData.dhdText.SmtpRecipient.IndexOf("@"))
        Dim strSenderName As String = SeqData.dhdText.SmtpReply.Substring(0, SeqData.dhdText.SmtpReply.IndexOf("@"))

        Dim strReportName As String = "", strTargetName As String
        If cbxReportName.Text.Length > 0 Then
            strReportName = cbxReportName.Text
        Else
            strReportName = SeqData.curStatus.Connection
        End If
        strTargetName = strReportName
        If SeqData.curVar.IncludeDate = True Then
            strTargetName = strTargetName & "_" & SeqData.FormatFileDate(Now)
        End If

        Dim strBody As String = "Sequenchel Report: " & strReportName
        Try
            Select Case cbxEmailResults.SelectedItem
                Case "HTML"
                    strTargetName = Nothing
                    strBody &= Environment.NewLine & SeqData.dhdText.DataSetToHtml(dtsReport)
                Case "Excel"
                    strTargetName &= ".xlsx"
                    Excel.CreateExcelDocument(dtsReport, strTargetName)
                Case "XML"
                    strTargetName &= ".xml"
                    SeqData.dhdText.ExportDataSetToXML(SeqData.dhdMainDB.ConvertToText(dtsReport), strTargetName)
                Case "CSV"
                    strTargetName &= ".csv"
                    SeqData.dhdText.DataSetToCsv(dtsReport.Tables(0), strTargetName, True, ",", False)
                Case Else
                    'unknown filetype, do nothing
            End Select
            SeqData.dhdText.SendSMTP(SeqData.dhdText.SmtpReply, strSenderName, SeqData.dhdText.SmtpRecipient, strRecepientName, SeqData.dhdText.SmtpReply, strSenderName, strReportName, strBody, strTargetName)
            SeqData.dhdText.DeleteFile(strTargetName)
        Catch ex As Exception
            MessageBox.Show("An error occured sending your email" & Environment.NewLine & Environment.NewLine & ex.Message)
        End Try
        CursorControl()
    End Sub

    Private Sub sptReports_MouseHover(sender As Object, e As EventArgs) Handles sptReports.MouseHover
        sptReports.BackColor = Color.LightGray
    End Sub

    Private Sub sptReports_MouseLeave(sender As Object, e As EventArgs) Handles sptReports.MouseLeave
        sptReports.BackColor = clrControl
    End Sub

    Private Sub pnlSplitFields_MouseHover(sender As Object, e As EventArgs) Handles pnlSplitFields.MouseHover
        sptReports.BackColor = Color.LightGray
    End Sub

    Private Sub pnlSplitFields_MouseLeave(sender As Object, e As EventArgs) Handles pnlSplitFields.MouseLeave
        sptReports.BackColor = clrControl
    End Sub

    Private Sub pnlSplitFields2_MouseHover(sender As Object, e As EventArgs) Handles pnlSplitFields2.MouseHover
        sptReports.BackColor = Color.LightGray
    End Sub

    Private Sub pnlSplitFields2_MouseLeave(sender As Object, e As EventArgs) Handles pnlSplitFields2.MouseLeave
        sptReports.BackColor = clrControl
    End Sub


    Private Sub sptReportFields_MouseHover(sender As Object, e As EventArgs) Handles sptReportFields.MouseHover
        sptReportFields.BackColor = Color.LightGray
    End Sub

    Private Sub sptReportFields_MouseLeave(sender As Object, e As EventArgs) Handles sptReportFields.MouseLeave
        sptReportFields.BackColor = clrControl
    End Sub

    Private Sub pnlSplitSelectedFields_MouseHover(sender As Object, e As EventArgs) Handles pnlSplitSelectedFields.MouseHover
        sptReportFields.BackColor = Color.LightGray
    End Sub

    Private Sub pnlSplitSelectedFields_MouseLeave(sender As Object, e As EventArgs) Handles pnlSplitSelectedFields.MouseLeave
        sptReportFields.BackColor = clrControl
    End Sub

    Private Sub pnlSplitSelectedTables_MouseHover(sender As Object, e As EventArgs) Handles pnlSplitSelectedTables.MouseHover
        sptReportFields.BackColor = Color.LightGray
    End Sub

    Private Sub pnlSplitSelectedTables_MouseLeave(sender As Object, e As EventArgs) Handles pnlSplitSelectedTables.MouseLeave
        sptReportFields.BackColor = clrControl
    End Sub


    Private Sub sptReport_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles sptReport.SplitterMoved
        If sptReportTables.SplitterDistance <> sptReport.SplitterDistance Then sptReportTables.SplitterDistance = sptReport.SplitterDistance
    End Sub

    Private Sub sptReport_MouseHover(sender As Object, e As EventArgs) Handles sptReport.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub sptReport_MouseLeave(sender As Object, e As EventArgs) Handles sptReport.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub pnlRelationHeaders_MouseHover(sender As Object, e As EventArgs) Handles pnlRelationHeaders.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub pnlRelationHeaders_MouseLeave(sender As Object, e As EventArgs) Handles pnlRelationHeaders.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub sptReportTables_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles sptReportTables.SplitterMoved
        If sptReport.SplitterDistance <> sptReportTables.SplitterDistance Then sptReport.SplitterDistance = sptReportTables.SplitterDistance
    End Sub

    Private Sub sptReportTables_MouseHover(sender As Object, e As EventArgs) Handles sptReportTables.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub sptReportTables_MouseLeave(sender As Object, e As EventArgs) Handles sptReportTables.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub lblRelationsSelectedTable_MouseHover(sender As Object, e As EventArgs) Handles lblRelationsSelectedTable.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub lblRelationsSelectedTable_MouseLeave(sender As Object, e As EventArgs) Handles lblRelationsSelectedTable.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub lblRelationUse_MouseHover(sender As Object, e As EventArgs) Handles lblRelationUse.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub lblRelationUse_MouseLeave(sender As Object, e As EventArgs) Handles lblRelationUse.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub lblSourceField_MouseHover(sender As Object, e As EventArgs) Handles lblSourceField.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub lblSourceField_MouseLeave(sender As Object, e As EventArgs) Handles lblSourceField.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub lblRelationsRelation_MouseHover(sender As Object, e As EventArgs) Handles lblRelationsRelation.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub lblRelationsRelation_MouseLeave(sender As Object, e As EventArgs) Handles lblRelationsRelation.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub lblRelationsJoinType_MouseHover(sender As Object, e As EventArgs) Handles lblRelationsJoinType.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub lblRelationsJoinType_MouseLeave(sender As Object, e As EventArgs) Handles lblRelationsJoinType.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub pnlSplitTables_MouseHover(sender As Object, e As EventArgs) Handles pnlSplitTables.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub pnlSplitTables_MouseLeave(sender As Object, e As EventArgs) Handles pnlSplitTables.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub lblSelectedTables_MouseHover(sender As Object, e As EventArgs) Handles lblSelectedTables.MouseHover
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub lblSelectedTables_MouseLeave(sender As Object, e As EventArgs) Handles lblSelectedTables.MouseLeave
        sptReport.BackColor = clrControl
        sptReportTables.BackColor = clrControl
    End Sub

    Private Sub dgvReport_DoubleClick(sender As Object, e As EventArgs) Handles dgvReport.DoubleClick

    End Sub
End Class