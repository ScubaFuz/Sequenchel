Imports System.Xml

Public Class frmReports
    Private dtsReport As New DataSet
    Private dtmElapsedTime As DateTime
    Private tmsElapsedTime As TimeSpan

    Private mousePath As System.Drawing.Drawing2D.GraphicsPath
    Private fontSize As Integer = 20

    Private Sub frmReports_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblLicense.Text = "Licensed to: " & basCode.curVar.LicenseName
        lblLicense.Left = Me.Width - lblLicense.Width - (basCode.curVar.BuildMargin * 5)

        mousePath = New System.Drawing.Drawing2D.GraphicsPath()

        DebugSettings()
        SecuritySet()
        LoadConnections()
        cbxEmailResults.SelectedIndex = 0
        Me.lvwAvailableFields.ListViewItemSorter = New ListViewItemComparer(1)
        Me.lvwAvailableFields.ListViewItemSorter = New ListViewItemComparer(0)

    End Sub

    Private Sub frmReports_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If cbxConnection.SelectedIndex <> -1 Then
            basCode.curStatus.Connection = cbxConnection.SelectedItem
            If Not basCode.curStatus.Connection = Nothing Then
                basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
                If cbxTableSet.SelectedIndex <> -1 Then
                    basCode.curStatus.TableSet = cbxTableSet.SelectedItem
                    If Not basCode.curStatus.TableSet = Nothing Then
                        basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub mnuReportsFileClose_Click(sender As Object, e As EventArgs) Handles mnuReportsFileClose.Click
        Me.Close()
    End Sub

    Private Sub mnuReportsHelpManual_Click(sender As Object, e As EventArgs) Handles mnuReportsHelpManual.Click
        WriteStatus("", 0, lblStatusText)
        System.Diagnostics.Process.Start("http://www.sequenchel.com/service/manual/reports/report-definition/")
    End Sub

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        MessageBox.Show(pnlReportLabel.Controls.Count)
    End Sub

    Private Sub DebugSettings()
        If basCode.curVar.DebugMode Then
            btnTest.Visible = True
        End If
        If basCode.curVar.DevMode Then
            'mnuMain.Visible = True
        End If
    End Sub

    Private Sub SecuritySet()
        If basCode.curVar.AllowQueryEdit = False And basCode.curVar.SecurityOverride = False Then
            btnLoadQuery.Enabled = False
            rtbQuery.ReadOnly = True
        End If
    End Sub

#Region "Report Definitions Load"

#Region "Controls"
    Private Sub btnConnectionsReload_Click(sender As Object, e As EventArgs) Handles btnConnectionsReload.Click
        WriteStatus("", 0, lblStatusText)
        LoadConnections()
    End Sub

    Private Sub cbxConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxConnection.SelectedIndexChanged
        WriteStatus("", 0, lblStatusText)
        If cbxConnection.SelectedIndex >= -1 Then
            CursorControl("Wait")
            basCode.curStatus.Connection = cbxConnection.SelectedItem
            basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
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
            basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
            LoadTables()
            LoadTableFields()
            ReportsLoad()
            CursorControl()
        End If
    End Sub

    Private Sub btnTablesReload_Click(sender As Object, e As EventArgs) Handles btnTablesReload.Click
        WriteStatus("", 0, lblStatusText)
        LoadTables()
    End Sub

    Private Sub btnReportFieldAdd_Click(sender As Object, e As EventArgs) Handles btnReportFieldAdd.Click
        WriteStatus("", 0, lblStatusText)
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
        WriteStatus("", 0, lblStatusText)
        For Each lvwItem As ListViewItem In lvwSelectedFields.SelectedItems
            'Dim lvwItem As ListViewItem = lvwSelectedFields.SelectedItems(0)
            lvwSelectedFields.Items.Remove(lvwItem)
            lvwAvailableFields.Items.Add(lvwItem)

            MasterPanelControlsDispose(pnlSelectedFields, lvwItem.Name)
            PanelsFieldSort()
            ReportTableRemove(lvwItem.Text)

            ResizeColumns()
        Next
    End Sub

    Private Sub btnReportFieldUp_Click(sender As Object, e As EventArgs) Handles btnReportFieldUp.Click
        WriteStatus("", 0, lblStatusText)
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
        WriteStatus("", 0, lblStatusText)
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

        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        Dim xCNode As XmlNode = basCode.dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
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
        WriteStatus("", 0, lblStatusText)
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
        WriteStatus("", 0, lblStatusText)
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
        Dim lstConnections As List(Of String) = basCode.LoadConnections(basCode.xmlConnections)
        If lstConnections Is Nothing Then
            basCode.xmlConnections.RemoveAll()
            basCode.xmlTableSets.RemoveAll()
            basCode.curVar.TableSetsFile = ""
            basCode.xmlTables.RemoveAll()
            basCode.curVar.TablesFile = ""
            basCode.TableClear()
            basCode.dhdConnection = basCode.dhdMainDB
            Exit Sub
        End If
        For Each lstItem As String In lstConnections
            cbxConnection.Items.Add(lstItem)
        Next
        cbxConnection.SelectedItem = basCode.CurStatus.Connection
    End Sub

    Private Sub LoadTableSets()
        AllClear(3)
        Dim lstTableSets As List(Of String) = basCode.LoadTableSets(basCode.xmlTableSets)
        If lstTableSets Is Nothing Then
            basCode.xmlTableSets.RemoveAll()
            basCode.xmlTables.RemoveAll()
            basCode.curVar.TablesFile = ""
            basCode.TableClear()
            Exit Sub
        End If
        For Each lstItem As String In lstTableSets
            cbxTableSet.Items.Add(lstItem)
        Next
        cbxTableSet.SelectedItem = basCode.curStatus.TableSet
    End Sub

    Private Sub LoadTables()
        AllClear(2)
        Dim lstTables As List(Of String) = basCode.LoadTables(basCode.xmlTables, False)
        If lstTables Is Nothing Then
            basCode.xmlTables.RemoveAll()
            Exit Sub
        End If
        For Each lstItem As String In lstTables
            cbxTable.Items.Add(lstItem)
        Next
        'cbxTable.SelectedItem = basCode.CurStatus.Table
    End Sub

    Private Sub LoadTableFields()
        For Each TableNode As Xml.XmlNode In basCode.xmlTables.SelectNodes("//Field")
            Dim lsvItem As New ListViewItem
            Dim strAlias As String = TableNode.ParentNode.ParentNode.Item("Alias").InnerText.Replace(".", "_")
            If GroupExists(strAlias) = False Then lvwAvailableFields.Groups.Add(strAlias, strAlias)
            lsvItem.Tag = TableNode.Item("DataType").InnerText
            'lsvItem.Tag = TableNode.ParentNode.ParentNode.Item("Name").InnerText
            lsvItem.Name = TableNode.ParentNode.ParentNode.Item("Name").InnerText & "." & TableNode.Item("FldName").InnerText
            lsvItem.Text = strAlias
            lsvItem.SubItems.Add(TableNode.Item("FldAlias").InnerText)
            lvwAvailableFields.Items.Add(lsvItem)
            lsvItem.Group = lvwAvailableFields.Groups(strAlias)
        Next
        ResizeColumns()
    End Sub

    Private Function GroupExists(strGroupName As String) As Boolean
        Dim blnResult As Boolean = False
        For Each group As ListViewGroup In lvwAvailableFields.Groups
            If group.Name = strGroupName Then blnResult = True
        Next
        Return blnResult
    End Function

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
        PanelControlsDispose(pnlRelationsTargetTable)
        PanelControlsDispose(pnlRelationsTargetField)
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
        pnlRelationsTargetTable.SuspendLayout()
        pnlRelationsTargetField.SuspendLayout()
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
        pnlRelationsTargetTable.ResumeLayout()
        pnlRelationsTargetField.ResumeLayout()
        pnlRelationsJoinType.ResumeLayout()
        pnlSelectedFieldsMain.ResumeLayout()
    End Sub

    Private Sub FieldAdd(strFieldName As String, strFieldAlias As String, strTableAlias As String)
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        Dim xCNode As XmlNode = basCode.dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
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
        lblNew.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count) * basCode.curVar.BuildMargin)

        If lblNew.Width > basCode.CurStatus.ReportLabelWidth Then
            basCode.CurStatus.ReportLabelWidth = lblNew.Width
        End If

        Dim cbxNewSort As New ComboField
        cbxNewSort.Name = pnlReportSort.Name & "1" & strFieldName
        cbxNewSort.Tag = strFieldName
        cbxNewSort.Width = 50
        pnlReportSort.Controls.Add(cbxNewSort)
        cbxNewSort.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewSort.Left = basCode.CurVar.BuildMargin
        ComboBoxFill(cbxNewSort, "Sort")

        Dim txtNewSortOrder As New TextField
        txtNewSortOrder.Name = pnlReportSortOrder.Name & "1" & strFieldName
        txtNewSortOrder.Tag = strFieldName
        txtNewSortOrder.Width = 20
        pnlReportSortOrder.Controls.Add(txtNewSortOrder)
        txtNewSortOrder.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        txtNewSortOrder.Left = basCode.CurVar.BuildMargin

    End Sub

    Private Sub FieldCheckShowAdd(strFieldName As String, strFieldType As String)
        Dim chkNewShow As New CheckField
        chkNewShow.Field = New SeqCore.BaseField
        chkNewShow.Name = pnlReportDisplay.Name & "1" & strFieldName
        chkNewShow.Field.FieldDataType = strFieldType
        chkNewShow.Tag = strFieldName
        'chkNewShow.ThreeState = True
        pnlReportDisplay.Controls.Add(chkNewShow)
        chkNewShow.Checked = True
        chkNewShow.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        chkNewShow.Left = basCode.CurVar.BuildMargin
        AddHandler chkNewShow.CheckedChanged, AddressOf Me.chkShowField_CheckChanged

        Dim cbxNewShowMode As New ComboField
        cbxNewShowMode.Field = New SeqCore.BaseField
        cbxNewShowMode.Name = pnlReportShowMode.Name & "1" & strFieldName
        cbxNewShowMode.Field.FieldDataType = strFieldType
        cbxNewShowMode.Tag = strFieldName
        cbxNewShowMode.Width = 75
        pnlReportShowMode.Controls.Add(cbxNewShowMode)
        cbxNewShowMode.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewShowMode.Left = basCode.CurVar.BuildMargin
        'Dim strDataType As String = 
        ComboBoxFill(cbxNewShowMode, "ShowMode")
    End Sub

    Private Sub FieldCheckFilterAdd(strFieldName As String, strFieldType As String, Optional intCount As Integer = 0)

        'Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xmlTables, "Table", "Name", strFieldName.Substring(0, strFieldName.LastIndexOf(".")))
        'Dim xCNode As XmlNode = basCode.dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName.Substring(strFieldName.LastIndexOf(".") + 1, strFieldName.Length - (strFieldName.LastIndexOf(".") + 1)))
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
            basCode.WriteLog("Maximum number of filters reached. No filter created", 2)
            WriteStatus("Maximum number of filters reached.", 2, lblStatusText)
            Exit Sub
        End If

        Dim chkNewFilter As New CheckField
        chkNewFilter.Field = New SeqCore.BaseField
        chkNewFilter.Name = pnlReportFilter.Name & intCount.ToString & strFieldName
        chkNewFilter.Field.FieldDataType = strFieldType
        chkNewFilter.Tag = strFieldName
        chkNewFilter.ThreeState = True
        pnlReportFilter.Controls.Add(chkNewFilter)
        'chkNewFilter.Top = ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        chkNewFilter.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        chkNewFilter.Left = basCode.CurVar.BuildMargin

        Dim cbxNewFilterMode As New ComboField
        cbxNewFilterMode.Field = New SeqCore.BaseField
        cbxNewFilterMode.Name = pnlReportFilterMode.Name & intCount.ToString & strFieldName
        cbxNewFilterMode.Tag = strFieldName
        cbxNewFilterMode.Width = 75
        pnlReportFilterMode.Controls.Add(cbxNewFilterMode)
        'cbxNewFilterMode.Top = ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewFilterMode.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewFilterMode.Left = basCode.CurVar.BuildMargin
        ComboBoxFill(cbxNewFilterMode, "FilterMode")

        Dim cbxNewFilterType As New ComboField
        cbxNewFilterType.Field = New SeqCore.BaseField
        cbxNewFilterType.Name = pnlReportFilterType.Name & intCount.ToString & strFieldName
        cbxNewFilterType.Tag = strFieldName
        cbxNewFilterType.Width = 75
        pnlReportFilterType.Controls.Add(cbxNewFilterType)
        'cbxNewFilterType.Top = ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewFilterType.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewFilterType.Left = basCode.CurVar.BuildMargin
        ComboBoxFill(cbxNewFilterType, "FilterType")

        Dim txtNewFilter As New TextField
        txtNewFilter.Field = New SeqCore.BaseField
        txtNewFilter.Name = pnlReportFilterText.Name & intCount.ToString & strFieldName
        txtNewFilter.Field.FieldDataType = strFieldType
        txtNewFilter.Tag = strFieldName
        txtNewFilter.Width = 190
        pnlReportFilterText.Controls.Add(txtNewFilter)
        'txtNewFilter.Top = ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lstSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        txtNewFilter.Top = ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedFields.Items.Count - 1) * basCode.CurVar.BuildMargin)
        txtNewFilter.Left = basCode.CurVar.BuildMargin

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
                If cbxTarget.Field.FieldDataType = "DATETIME" Then
                    cbxTarget.Items.Add("DATE")
                    cbxTarget.Items.Add("YEAR")
                    cbxTarget.Items.Add("MONTH")
                    cbxTarget.Items.Add("DAY")
                End If
                If cbxTarget.Field.FieldDataType = "DATETIME" Or cbxTarget.Field.FieldDataType = "TIME" Or cbxTarget.Field.FieldDataType = "TIMESTAMP" Then
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
                Dim lstFields As List(Of String) = basCode.dhdText.LoadItemList(basCode.xmlTables, "Table", "Name", strSource, "Field", "FldName")
                If lstFields Is Nothing Then Exit Sub
                For Each lstItem As String In lstFields
                    cbxTarget.Items.Add(lstItem)
                Next
                'cbxTarget.SelectedItem = "INNER"
            Case "RelationTargetTable"
                For Each lvwItem As ListViewItem In lvwSelectedTables.Items
                    If cbxTarget.Items.Contains(lvwItem.Text & " (" & lvwItem.Tag & ")") = False Then
                        cbxTarget.Items.Add(lvwItem.Text & " (" & lvwItem.Tag & ")")
                    End If
                Next
            Case "RelationTargetfield"
                cbxTarget.SelectedIndex = -1
                cbxTarget.Items.Clear()
                cbxTarget.Items.Add("")
                For Each lvwItem As ListViewItem In lvwAvailableFields.Items
                    If lvwItem.Text = strSource Then
                        cbxTarget.Items.Add(lvwItem.SubItems(1).Text)
                    End If
                Next
                For Each lvwItem As ListViewItem In lvwSelectedFields.Items
                    If lvwItem.Text = strSource Then
                        cbxTarget.Items.Add(lvwItem.SubItems(1).Text)
                    End If
                Next
                cbxTarget.Sorted = True
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
        pnlReportLabel.Width = basCode.CurStatus.ReportLabelWidth + basCode.CurVar.BuildMargin
        pnlReportDisplay.Left = pnlReportLabel.Left + pnlReportLabel.Width
        chkReportShow.Left = pnlReportDisplay.Left + basCode.CurVar.BuildMargin - 20

        pnlReportShowMode.Left = pnlReportDisplay.Left + pnlReportDisplay.Width
        lblReportShowMode.Left = pnlReportShowMode.Left + basCode.CurVar.BuildMargin

        pnlReportSort.Left = pnlReportShowMode.Left + pnlReportShowMode.Width
        lblReportSort.Left = pnlReportSort.Left + basCode.CurVar.BuildMargin

        pnlReportSortOrder.Left = pnlReportSort.Left + pnlReportSort.Width
        lblReportSortOrder.Left = pnlReportSortOrder.Left + basCode.CurVar.BuildMargin

        pnlReportFilter.Left = pnlReportSortOrder.Left + pnlReportSortOrder.Width
        lblReportFilter.Left = pnlReportFilter.Left + basCode.CurVar.BuildMargin

        pnlReportFilterMode.Left = pnlReportFilter.Left + pnlReportFilter.Width
        lblReportFilterMode.Left = pnlReportFilterMode.Left + basCode.CurVar.BuildMargin

        pnlReportFilterType.Left = pnlReportFilterMode.Left + pnlReportFilterMode.Width
        lblReportFilterType.Left = pnlReportFilterType.Left + basCode.CurVar.BuildMargin

        pnlReportFilterText.Left = pnlReportFilterType.Left + pnlReportFilterType.Width
        lblReportFilterText.Left = pnlReportFilterText.Left + basCode.CurVar.BuildMargin
        If pnlReportFilterText.Left + pnlReportFilterText.Width + (basCode.CurVar.BuildMargin * 2) > pnlSelectedFields.Width Then pnlSelectedFields.Width = pnlReportFilterText.Left + pnlReportFilterText.Width + (basCode.CurVar.BuildMargin * 2)
    End Sub

    Private Sub PanelRelationWidthSet()
        pnlRelationsLabel.Width = basCode.CurStatus.RelationLabelWidth + basCode.CurVar.BuildMargin
        pnlRelationsUse.Left = pnlRelationsLabel.Left + pnlRelationsLabel.Width
        lblRelationUse.Left = pnlRelationsUse.Left

        pnlRelationsField.Left = pnlRelationsUse.Left + pnlRelationsUse.Width
        lblSourceField.Left = pnlRelationsField.Left + basCode.curVar.BuildMargin

        pnlRelationsTargetTable.Left = pnlRelationsField.Left + pnlRelationsField.Width
        lblRelationsTargetTable.Left = pnlRelationsTargetTable.Left + basCode.curVar.BuildMargin

        pnlRelationsTargetField.Left = pnlRelationsTargetTable.Left + pnlRelationsTargetTable.Width
        lblRelationsTargetField.Left = pnlRelationsTargetField.Left + basCode.curVar.BuildMargin

        pnlRelationsJoinType.Left = pnlRelationsTargetField.Left + pnlRelationsTargetField.Width
        lblRelationsJoinType.Left = pnlRelationsJoinType.Left + basCode.curVar.BuildMargin

        pnlRelations.Width = pnlRelationsJoinType.Left + pnlRelationsJoinType.Width + (basCode.curVar.BuildMargin * 2)
    End Sub

    Private Sub PanelFieldHeightSet()
        If (pnlReportFilterText.Controls.Count + 1) * (basCode.CurVar.BuildMargin + basCode.CurVar.FieldHeight) > pnlReportFilterText.Height Then
            pnlReportFilterText.Height = (pnlReportFilterText.Controls.Count + 1) * (basCode.CurVar.BuildMargin + basCode.CurVar.FieldHeight)
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
        If (pnlRelationsUse.Controls.Count + 1) * (basCode.CurVar.BuildMargin + basCode.CurVar.FieldHeight) > pnlRelationsUse.Height Then
            pnlRelationsUse.Height = (pnlRelationsUse.Controls.Count + 1) * (basCode.CurVar.BuildMargin + basCode.CurVar.FieldHeight)
        End If
        pnlRelationsField.Height = pnlRelationsUse.Height
        pnlRelationsTargetTable.Height = pnlRelationsUse.Height
        pnlRelationsTargetField.Height = pnlRelationsUse.Height
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
        FieldSort(lvwSelectedTables, pnlRelationsTargetTable, False, pnlRelationsUse)
        FieldSort(lvwSelectedTables, pnlRelationsTargetField, False, pnlRelationsUse)
        FieldSort(lvwSelectedTables, pnlRelationsJoinType, False, pnlRelationsUse)
        FieldSort(lvwSelectedTables, pnlRelationsLabel, True, pnlRelationsUse)
        PanelRelationHeightSet()
    End Sub

    Private Sub FieldSort(lstSource As ListView, pnlTarget As Panel, SinglePanel As Boolean, ComparePanel As Panel)
        Dim strFieldName As String = Nothing
        Dim intControlNumber As Integer = 0
        Dim intMaxNumber As Integer = 0
        basCode.CurStatus.ReportMaxTop = basCode.CurVar.BuildMargin
        For incCount As Integer = 0 To lstSource.Items.Count - 1
            Dim strColumnName As String = lstSource.Columns(1).Text
            If strColumnName = "Table Name" Then
                strFieldName = lstSource.Items.Item(incCount).SubItems(1).Text
            Else
                strFieldName = lstSource.Items.Item(incCount).Name
            End If
            intMaxNumber = 0
            For Each ctrControl In pnlTarget.Controls
                'Dim strControl As String = ctrControl.Tag
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
                        ctrControl.Top = basCode.curStatus.ReportMaxTop + ((intControlNumber - 1) * basCode.curVar.FieldHeight) + ((intControlNumber - 1) * basCode.curVar.BuildMargin)
                    End If

                End If
            Next
            basCode.curStatus.ReportMaxTop += intMaxNumber * basCode.curVar.FieldHeight + intMaxNumber * basCode.curVar.BuildMargin
        Next
    End Sub

    Private Sub ReportTableAdd(strTable As String, strAlias As String)
        'Dim strTable As String = strFieldName.Substring(0, strFieldName.LastIndexOf("."))
        Dim blnTableFound As Boolean = False
        For Each lvwitem In lvwSelectedTables.Items
            If lvwitem.Name = strTable & "_" & strAlias Then blnTableFound = True
        Next
        If blnTableFound = False Then
            Dim lvwAddItem As New ListViewItem
            lvwAddItem.Name = strTable & "_" & strAlias
            lvwAddItem.Tag = strTable
            lvwAddItem.Text = strAlias
            lvwAddItem.SubItems.Add(strTable)
            lvwSelectedTables.Items.Add(lvwAddItem)
            RelationLabelAdd(strTable, strAlias)
            RelationUseAdd(strTable, 0)
            RelationsLoad(strTable)
            'PanelsRelationSort()
            For Each cbxControl In pnlRelationsTargetTable.Controls
                Dim strName As String = cbxControl.[GetType]().Name
                If cbxControl.[GetType]().Name = "ComboField" Then
                    Dim cbxAfterFill As ComboField = TryCast(cbxControl, ComboField)
                    ComboBoxFill(cbxAfterFill, "RelationTargetTable", cbxAfterFill.Tag)
                End If
            Next

            PanelRelationWidthSet()
        End If
    End Sub

    Private Sub ReportTableRemove(strTableAlias As String)
        'Dim strTabel As String = strFieldName.Substring(0, strFieldName.LastIndexOf("."))
        Dim strTableRemove As String = ""
        Dim intCount As Integer = 0
        'Dim lvwItem As ListViewItem = Nothing
        For Each lvwFindItem As ListViewItem In lvwSelectedFields.Items
            If strTableAlias = lvwFindItem.Text Then
                intCount += 1
            End If
        Next
        If intCount = 0 Then
            For Each lvwRemoveItem In lvwSelectedTables.Items
                If lvwRemoveItem.Text = strTableAlias Then
                    lvwSelectedTables.Items.RemoveByKey(lvwRemoveItem)
                    MasterPanelControlsDispose(pnlRelations, strTableAlias)
                End If
            Next
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
        lblNew.Top = ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count) * basCode.CurVar.BuildMargin)

        If lblNew.Width > basCode.CurStatus.RelationLabelWidth Then
            basCode.CurStatus.RelationLabelWidth = lblNew.Width
        End If

    End Sub

    Private Sub RelationUseAdd(strTableName As String, Optional intCount As Integer = 0)
        If intCount = 0 Then
            intCount += 1
            For Each ctrControl In pnlRelationsUse.Controls
                If ctrControl.Name.ToString.Length > strTableName.Length Then
                    If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTableName.Length, strTableName.Length) = strTableName Then intCount += 1
                End If
            Next
        End If

        Dim chkNewShow As New CheckField
        chkNewShow.Field = New SeqCore.BaseField
        chkNewShow.Name = pnlRelationsUse.Name & intCount.ToString & strTableName
        chkNewShow.Tag = strTableName
        pnlRelationsUse.Controls.Add(chkNewShow)
        chkNewShow.Top = ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.BuildMargin)
        chkNewShow.Left = basCode.CurVar.BuildMargin

        Dim cbxNewField As New ComboField
        cbxNewField.Field = New SeqCore.BaseField
        cbxNewField.Name = pnlRelationsField.Name & intCount.ToString & strTableName
        cbxNewField.Tag = strTableName
        cbxNewField.Width = pnlRelationsField.Width - basCode.curVar.BuildMargin
        pnlRelationsField.Controls.Add(cbxNewField)
        cbxNewField.Top = ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewField.Left = basCode.CurVar.BuildMargin
        ComboBoxFill(cbxNewField, "RelationField", strTableName)

        'Dim txtNewFilter As New TextField
        'txtNewFilter.Name = pnlRelationsRelation.Name & intCount.ToString & strTableName
        'txtNewFilter.Tag = strTableName
        'txtNewFilter.Width = pnlRelationsRelation.Width - basCode.curVar.BuildMargin
        'pnlRelationsRelation.Controls.Add(txtNewFilter)
        'txtNewFilter.Top = ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.BuildMargin)
        'txtNewFilter.Left = basCode.CurVar.BuildMargin

        Dim cbxNewTargetTable As New ComboField
        cbxNewTargetTable.Field = New SeqCore.BaseField
        cbxNewTargetTable.Name = pnlRelationsTargetTable.Name & intCount.ToString & strTableName
        cbxNewTargetTable.Tag = strTableName
        cbxNewTargetTable.Width = pnlRelationsTargetTable.Width - basCode.curVar.BuildMargin
        pnlRelationsTargetTable.Controls.Add(cbxNewTargetTable)
        cbxNewTargetTable.Top = ((lvwSelectedTables.Items.Count - 1) * basCode.curVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * basCode.curVar.BuildMargin)
        cbxNewTargetTable.Left = basCode.curVar.BuildMargin
        AddHandler cbxNewTargetTable.SelectedIndexChanged, AddressOf Me.cbxRelationTargetTable_SelectedIndexChanged
        cbxNewTargetTable.Items.Add("")
        ComboBoxFill(cbxNewTargetTable, "RelationTargetTable", strTableName)

        Dim cbxNewTargetField As New ComboField
        cbxNewTargetField.Field = New SeqCore.BaseField
        cbxNewTargetField.Name = pnlRelationsTargetField.Name & intCount.ToString & strTableName
        cbxNewTargetField.Tag = strTableName
        cbxNewTargetField.Width = pnlRelationsTargetField.Width - basCode.curVar.BuildMargin
        pnlRelationsTargetField.Controls.Add(cbxNewTargetField)
        cbxNewTargetField.Top = ((lvwSelectedTables.Items.Count - 1) * basCode.curVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * basCode.curVar.BuildMargin)
        cbxNewTargetField.Left = basCode.curVar.BuildMargin

        Dim cbxNewJoinType As New ComboField
        cbxNewJoinType.Field = New SeqCore.BaseField
        cbxNewJoinType.Name = pnlRelationsJoinType.Name & intCount.ToString & strTableName
        cbxNewJoinType.Tag = strTableName
        cbxNewJoinType.Width = pnlRelationsJoinType.Width - basCode.curVar.BuildMargin
        pnlRelationsJoinType.Controls.Add(cbxNewJoinType)
        cbxNewJoinType.Top = ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.FieldHeight) + ((lvwSelectedTables.Items.Count - 1) * basCode.CurVar.BuildMargin)
        cbxNewJoinType.Left = basCode.CurVar.BuildMargin
        ComboBoxFill(cbxNewJoinType, "JoinType")

        PanelsRelationSort()
    End Sub

    Private Sub RelationsLoad(strTable As String)
        Dim xPNode As System.Xml.XmlNode = basCode.dhdText.FindXmlNode(basCode.xmlTables, "Table", "Name", strTable)
        Dim xmlFieldList As System.Xml.XmlNodeList = basCode.dhdText.FindXmlChildNodes(xPNode, "Fields/Field/Relations", "Relation")
        Dim intCount As Integer = 0
        For Each xmlCNode As System.Xml.XmlNode In xmlFieldList
            Dim xmlRelationList As System.Xml.XmlNodeList = basCode.dhdText.FindXmlChildNodes(xmlCNode, "Relation")
            For Each xRNode As System.Xml.XmlNode In xmlRelationList
                Dim strRelationTable As String = "", strRelationTableAlias As String = "", strRelationField As String = ""
                If basCode.dhdText.CheckNodeElement(xRNode, "RelationField") = True Then
                    strRelationTable = xRNode.Item("RelationTable").InnerText
                    strRelationTableAlias = xRNode.Item("RelationTableAlias").InnerText
                    strRelationField = xRNode.Item("RelationField").InnerText
                Else
                    Dim strRelation As String = xRNode.InnerText
                    If strRelation.Length > 0 Then
                        If strRelation.IndexOf(".") > 0 Then
                            strRelationTable = strRelation.Substring(0, strRelation.LastIndexOf(".")) & " (" & strRelation.Substring(0, strRelation.LastIndexOf(".")) & ")"
                            strRelationTableAlias = strRelation.Substring(0, strRelation.LastIndexOf(".")) & " (" & strRelation.Substring(0, strRelation.LastIndexOf(".")) & ")"
                            strRelationField = strRelation.Substring(strRelation.LastIndexOf(".") + 1, strRelation.Length - (strRelation.LastIndexOf(".") + 1))
                        Else
                            strRelationTable = strRelation
                            strRelationTableAlias = strRelation
                            strRelationField = strRelation
                        End If
                    End If
                End If

                If xRNode.InnerText.Length > 0 Then
                    intCount += 1
                    If intCount > 1 Then RelationUseAdd(strTable)
                    For Each ctrControl In pnlRelationsField.Controls
                        If strTable.Length < ctrControl.Name.ToString.Length Then
                            If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                                If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                                    'ctrControl.Text = xmlCNode.Item("FldName").InnerText
                                    ctrControl.Text = xRNode.ParentNode.ParentNode.Item("FldName").InnerText
                                End If
                            End If
                        End If
                    Next
                    'For Each ctrControl In pnlRelationsRelation.Controls
                    '    If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                    '        If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                    '            ctrControl.Text = xRNode.InnerText
                    '        End If
                    '    End If
                    'Next
                    For Each ctrControl In pnlRelationsTargetTable.Controls
                        If strTable.Length < ctrControl.Name.ToString.Length Then
                            If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                                If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                                    ctrControl.Text = strRelationTableAlias & " (" & strRelationTable & ")"
                                End If
                            End If
                        End If
                    Next
                    For Each ctrControl In pnlRelationsTargetField.Controls
                        If strTable.Length < ctrControl.Name.ToString.Length Then
                            If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                                If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                                    ctrControl.Text = strRelationField
                                End If
                            End If
                        End If
                    Next
                    For Each ctrControl In pnlRelationsJoinType.Controls
                        If strTable.Length < ctrControl.Name.ToString.Length Then
                            If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length, strTable.Length) = strTable Then
                                If ctrControl.Name.ToString.Substring(ctrControl.Name.ToString.Length - strTable.Length - 1, 1) = intCount Then
                                    ctrControl.SelectedIndex = 1
                                End If
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
        WriteStatus("", 0, lblStatusText)
        'If cbxReportName.SelectedIndex = -1 Then
        ReportFieldsDispose(False)
        'End If
        cbxReportName.SelectedIndex = -1
        cbxReportName.Text = ""
        ReportClear(True)
        CursorControl()
    End Sub

    Private Sub btnReportCreate_Click(sender As Object, e As EventArgs) Handles btnReportCreate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If QueryShow() = True Then
            QueryExecute()
        End If
        CursorControl()
    End Sub

    Private Sub btnQueryShow_Click(sender As Object, e As EventArgs) Handles btnQueryShow.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        QueryShow()
        CursorControl()
    End Sub

    Private Sub btnExecuteQuery_Click(sender As Object, e As EventArgs) Handles btnExecuteQuery.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        strQuery = rtbQuery.Text
        If strQuery.Length > 0 Then QueryExecute()
        CursorControl()
    End Sub

    Private Sub ReportClear(blnAll As Boolean)
        strErrorMessage = ""
        lblErrorMessage.Text = ""
        lblListCountNumber.Text = "0"
        dgvReport.Columns.Clear()
        If blnAll = True Then
            rtbQuery.Text = ""
        End If
    End Sub

    Private Function QueryShow() As Boolean
        ReportClear(True)
        Dim xmlReportShow As XmlDocument = basCode.dhdText.CreateRootDocument(Nothing, Nothing, Nothing)
        Dim strReportName As String = cbxReportName.Text
        If strReportName = "" Then strReportName = "TempReport"
        'ReportAdd(xmlReportShow, strReportName)
        ReportToXML(xmlReportShow, strReportName)
        strQuery = basCode.ReportQueryBuild(xmlReportShow, basCode.xmlTables, strReportName, basCode.curVar.DateTimeStyle)
        If strQuery = Nothing Then
            WriteStatus("No fields were selected. Select at least 1 field to be shown in your report", 2, lblStatusText)
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
        dtsReport = basCode.QueryDb(basCode.dhdConnection, strQuery, True, 1)
        If basCode.dhdConnection.ErrorLevel = -1 Then
            WriteStatus(basCode.dhdConnection.ErrorMessage, 1, lblStatusText)
            lblErrorMessage.Text = basCode.ErrorMessage
        End If
        ReportShow(dtsReport, 1)
        tmrElapsedTime.Stop()
        tmrElapsedTime.Enabled = False
        tmsElapsedTime = Now() - dtmElapsedTime
        lblElapsedTime.Text = tmsElapsedTime.ToString
    End Sub

    Private Sub btnSaveQuery_Click(sender As Object, e As EventArgs) Handles btnSaveQuery.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        SaveQuery()
        CursorControl()
    End Sub

    Private Sub btnLoadQuery_Click(sender As Object, e As EventArgs) Handles btnLoadQuery.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        LoadQuery()
        CursorControl()
    End Sub

    Private Sub btnReportAddOrUpdate_Click(sender As Object, e As EventArgs) Handles btnReportAddOrUpdate.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If cbxReportName.Text.Length < 3 Then
            WriteStatus("Report Name must be at least 3 characters long.", 2, lblStatusText)
            Exit Sub
        End If
        If lvwSelectedFields.Items.Count = 0 Then
            WriteStatus("No fields have been selected for this report. Aborting save.", 2, lblStatusText)
            Exit Sub
        End If
        ReportDelete(cbxReportName.Text)
        ReportToXML(basCode.xmlReports, cbxReportName.Text)
        'ReportAdd(xmlReports, cbxReportName.Text)
        If Not cbxReportName.Items.Contains(cbxReportName.Text) Then cbxReportName.Items.Add(cbxReportName.Text)

        If SaveXmlFile(basCode.xmlReports, basCode.curVar.ReportSetFile, True) = False Then
            WriteStatus("The file " & basCode.curVar.ReportSetFile & " was not saved. please check the log.", 1, lblStatusText)
        Else
            WriteStatus("Report Saved", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnReportDelete_Click(sender As Object, e As EventArgs) Handles btnReportDelete.Click
        WriteStatus("", 0, lblStatusText)
        If cbxReportName.SelectedIndex < 0 Then
            WriteStatus("No report is selected. Select a report before deleting it.", 2, lblStatusText)
            Exit Sub
        End If
        If MessageBox.Show("This will permanently remove the Item: " & cbxReportName.SelectedItem & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then
            WriteStatus("Report deletion aborted.", 0, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        ReportDelete(cbxReportName.SelectedItem)
        If cbxReportName.Items.Contains(cbxReportName.Text) Then cbxReportName.Items.Remove(cbxReportName.Text)
        cbxReportName.SelectedIndex = -1
        cbxReportName.Text = ""
        ReportFieldsDispose(False)

        If SaveXmlFile(basCode.xmlReports, basCode.curVar.ReportSetFile, True) = False Then
            WriteStatus("The file " & basCode.curVar.ReportSetFile & " was not saved. please check the log.", 1, lblStatusText)
        Else
            WriteStatus("Report Deleted", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub cbxReportName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxReportName.SelectedIndexChanged
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        PanelsSuspendLayout()
        If cbxReportName.SelectedIndex >= 0 Then
            ReportFieldsDispose(False)
            ReportLoad(basCode.xmlReports, cbxReportName.SelectedItem)
        End If
        pnlSelectedFieldsMain.Focus()
        pnlSelectedFieldsMain.Invalidate()
        PanelsResumeLayout()
        CursorControl()
    End Sub

    Private Sub btnRevertChanges_Click(sender As Object, e As EventArgs) Handles btnRevertChanges.Click
        WriteStatus("", 0, lblStatusText)
        If cbxReportName.SelectedIndex < 0 Then
            WriteStatus("No report is selected to revert to.", 2, lblStatusText)
            Exit Sub
        End If
        CursorControl("Wait")
        ReportFieldsDispose(False)
        ReportLoad(basCode.xmlReports, cbxReportName.SelectedItem)
        CursorControl()
    End Sub

    Private Sub btnExportToFile_Click(sender As Object, e As EventArgs) Handles btnExportToFile.Click
        WriteStatus("", 0, lblStatusText)
        If dgvReport.RowCount = 0 Then Exit Sub
        CursorControl("Wait")
        Dim strReportName As String = ""
        If cbxReportName.Text.Length > 0 Then
            strReportName = cbxReportName.Text
        Else
            strReportName = basCode.curStatus.Connection
        End If
        Dim strFileName As String = GetSaveFileName(strReportName)
        If ExportFile(dtsReport, strFileName) = False Then
            WriteStatus("There was an error saving the file. Please check the log.", 1, lblStatusText)
        Else
            WriteStatus("File Saved", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

#End Region

    Private Sub ReportsLoad()
        cbxReportName.Items.Clear()
        cbxReportName.Text = ""
        Dim lstReports As List(Of String) = basCode.LoadReports(basCode.xmlReports)
        If lstReports Is Nothing Then
            basCode.xmlReports.RemoveAll()
            Exit Sub
        End If
        For Each lstItem As String In lstReports
            cbxReportName.Items.Add(lstItem)
        Next
    End Sub

    Private Sub ReportLoad(xmlReports As XmlDocument, strReportName As String)
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strReportName)
        If Not xNode Is Nothing Then
            XmlToReport(xmlReports, strReportName)
            'Dim xFNode As XmlNode = basCode.dhdText.FindXmlChildNode(xNode, "Tables/Table")
            'If xFNode Is Nothing Then
            'Else
            '    XmlToReport_old(xmlReports, strReportName)
            'End If
            WriteStatus("Report loaded.", 0, lblStatusText)
        Else
            WriteStatus("The selected report was not found.", 2, lblStatusText)
        End If
    End Sub

    'Private Sub XmlToReport_old(xmlReports As XmlDocument, strReportName As String)
    '    Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strReportName)
    '    Dim strTable As String = "", strField As String = "", intRelationNumber As Integer = 0

    '    If Not xNode Is Nothing Then
    '        If basCode.dhdText.CheckNodeElement(xNode, "Description") Then txtDescription.Text = xNode.Item("Description").InnerText
    '        chkTop.Checked = xNode.Item("UseTop").InnerText
    '        txtTop.Text = xNode.Item("UseTopNumber").InnerText
    '        chkDistinct.Checked = xNode.Item("UseDistinct").InnerText
    '        For Each xCNode As XmlNode In basCode.dhdText.FindXmlChildNodes(xNode, "Tables/Table/Fields/Field")
    '            strTable = xCNode.ParentNode.ParentNode.Item("TableName").InnerText
    '            If strTable.IndexOf(".") = -1 Then strTable = "dbo." & strTable
    '            strField = xCNode.Item("FieldName").InnerText
    '            Try
    '                lvwAvailableFields.Items(strTable & "." & strField).Selected = True
    '                btnReportFieldAdd_Click(Nothing, Nothing)
    '                SetCtrText(pnlReportDisplay, strTable & "." & strField, xCNode.Item("FieldShow").InnerText)
    '                SetCtrText(pnlReportShowMode, strTable & "." & strField, xCNode.Item("FieldShowMode").InnerText)
    '                SetCtrText(pnlReportSort, strTable & "." & strField, xCNode.Item("FieldSort").InnerText)
    '                SetCtrText(pnlReportSortOrder, strTable & "." & strField, xCNode.Item("FieldSortOrder").InnerText)

    '                For Each xFnode In basCode.dhdText.FindXmlChildNodes(xCNode, "Filters/Filter")
    '                    SetCtrText(pnlReportFilter, strTable & "." & strField, xFnode.Item("FilterEnabled").InnerText, xFnode.Item("FilterNumber").InnerText)
    '                    SetCtrText(pnlReportFilterMode, strTable & "." & strField, xFnode.Item("FilterMode").InnerText, xFnode.Item("FilterNumber").InnerText)
    '                    SetCtrText(pnlReportFilterType, strTable & "." & strField, xFnode.Item("FilterType").InnerText, xFnode.Item("FilterNumber").InnerText)
    '                    SetCtrText(pnlReportFilterText, strTable & "." & strField, xFnode.Item("FilterText").InnerText, xFnode.Item("FilterNumber").InnerText)
    '                Next
    '            Catch ex As Exception
    '                If MessageBox.Show("Unable to load the Field " & strTable & "." & strField & Environment.NewLine & "Do you wish to keep loading the report?", "Error Loading Report", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
    '                    WriteStatus("Report loading aborted.", 0, lblStatusText)
    '                    Exit Sub
    '                End If
    '            End Try
    '        Next

    '        For Each xRNode As XmlNode In basCode.dhdText.FindXmlChildNodes(xNode, "Relations/Relation")
    '            strTable = xRNode.Item("TableName").InnerText
    '            'strTableAlias = xRNode.Item("TableAlias").InnerText
    '            intRelationNumber = xRNode.Item("RelationNumber").InnerText
    '            'lstAvailableFields.SelectedItem = strTable & "." & strField
    '            'btnReportFieldAdd_Click(Nothing, Nothing)
    '            SetCtrText(pnlRelationsUse, strTable, xRNode.Item("RelationEnabled").InnerText, intRelationNumber)
    '            SetCtrText(pnlRelationsField, strTable, xRNode.Item("RelationSource").InnerText, intRelationNumber)
    '            If basCode.dhdText.CheckNodeElement(xRNode, "RelationTarget") Then
    '                Dim strRelationTarget As String = xRNode.Item("RelationTarget").InnerText
    '                SetCtrText(pnlRelationsTargetField, strTable, strRelationTarget.Substring(strRelationTarget.LastIndexOf("."), strRelationTarget.Length - strRelationTarget.LastIndexOf(".")), intRelationNumber)
    '                SetCtrText(pnlRelationsTargetTable, strTable, basCode.GetTableAliasFromString(strRelationTarget) & " (" & basCode.GetTableNameFromString(strRelationTarget) & ")", intRelationNumber)
    '            End If
    '            If basCode.dhdText.CheckNodeElement(xRNode, "RelationTargetTable") Then
    '                Dim strTargetTable As String = xRNode.Item("RelationTargetAlias").InnerText & " (" & xRNode.Item("RelationTargetTable").InnerText & ")"
    '                If strTargetTable = " ()" Then strTargetTable = ""
    '                SetCtrText(pnlRelationsTargetTable, strTable, strTargetTable, intRelationNumber)
    '            End If
    '            If basCode.dhdText.CheckNodeElement(xRNode, "RelationTargetField") Then SetCtrText(pnlRelationsTargetField, strTable, xRNode.Item("RelationTargetField").InnerText, intRelationNumber)
    '            SetCtrText(pnlRelationsJoinType, strTable, xRNode.Item("RelationJoinType").InnerText, intRelationNumber)

    '        Next
    '        WriteStatus("Report loading completed.", 0, lblStatusText)
    '    Else
    '        WriteStatus("Selected report not found.", 0, lblStatusText)
    '    End If
    'End Sub

    Private Sub XmlToReport(xmlReports As XmlDocument, strReportName As String)
        Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xmlReports, "Report", "ReportName", strReportName)
        Dim strTable As String = "", strTableAlias As String = "", strField As String = "", intRelationNumber As Integer = 0

        If Not xNode Is Nothing Then
            If basCode.dhdText.CheckNodeElement(xNode, "Description") Then txtDescription.Text = xNode.Item("Description").InnerText
            chkTop.Checked = xNode.Item("UseTop").InnerText
            txtTop.Text = xNode.Item("UseTopNumber").InnerText
            chkDistinct.Checked = xNode.Item("UseDistinct").InnerText
            For Each xCNode As XmlNode In basCode.dhdText.FindXmlChildNodes(xNode, "Fields/Field")
                strTable = xCNode.Item("TableName").InnerText
                If strTable.IndexOf(".") = -1 Then strTable = "dbo." & strTable
                If basCode.dhdText.CheckNodeElement(xCNode, "TableAlias") Then strTableAlias = xCNode.Item("TableAlias").InnerText
                If String.IsNullOrEmpty(strTableAlias) Then strTableAlias = strTable.Replace(".", "_")
                strField = xCNode.Item("FieldName").InnerText
                Try
                    lvwAvailableFields.Items(strTable & "." & strField).Selected = True
                    btnReportFieldAdd_Click(Nothing, Nothing)
                    SetCtrText(pnlReportDisplay, strTable & "." & strField, xCNode.Item("FieldShow").InnerText)
                    SetCtrText(pnlReportShowMode, strTable & "." & strField, xCNode.Item("FieldShowMode").InnerText)
                    SetCtrText(pnlReportSort, strTable & "." & strField, xCNode.Item("FieldSort").InnerText)
                    SetCtrText(pnlReportSortOrder, strTable & "." & strField, xCNode.Item("FieldSortOrder").InnerText)

                    For Each xFnode In basCode.dhdText.FindXmlChildNodes(xCNode, "Filters/Filter")
                        SetCtrText(pnlReportFilter, strTable & "." & strField, xFnode.Item("FilterEnabled").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterMode, strTable & "." & strField, xFnode.Item("FilterMode").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterType, strTable & "." & strField, xFnode.Item("FilterType").InnerText, xFnode.Item("FilterNumber").InnerText)
                        SetCtrText(pnlReportFilterText, strTable & "." & strField, xFnode.Item("FilterText").InnerText, xFnode.Item("FilterNumber").InnerText)
                    Next
                Catch ex As Exception
                    basCode.WriteLog("Unable to load the Field " & strTable & "." & strField & Environment.NewLine & ex.Message, 1)
                    If MessageBox.Show("Unable to load the Field " & strTable & "." & strField & Environment.NewLine & "Do you wish to keep loading the report?", "Error Loading Report", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                        WriteStatus("Report loading aborted.", 0, lblStatusText)
                        Exit Sub
                    End If
                End Try
            Next

            Dim intRelationCount As Integer = 0
            For Each xRNode As XmlNode In basCode.dhdText.FindXmlChildNodes(xNode, "Relations/Relation")
                strTable = xRNode.Item("TableName").InnerText
                If basCode.dhdText.CheckNodeElement(xRNode, "TableAlias") Then strTableAlias = xRNode.Item("TableAlias").InnerText
                intRelationNumber = xRNode.Item("RelationNumber").InnerText
                If intRelationNumber = 1 Then intRelationCount += 1
                SetCtrText(pnlRelationsUse, strTable, xRNode.Item("RelationEnabled").InnerText, intRelationNumber)
                SetCtrText(pnlRelationsField, strTable, xRNode.Item("RelationSource").InnerText, intRelationNumber)

                Dim strTargetTable As String = "", strTargetTableAlias As String = "", strTargetField As String = ""
                If basCode.dhdText.CheckNodeElement(xRNode, "RelationTarget") Then
                    Dim strRelationTarget As String = xRNode.Item("RelationTarget").InnerText
                    If String.IsNullOrEmpty(strRelationTarget) = False Then
                        strTargetField = strRelationTarget.Substring(strRelationTarget.LastIndexOf(".") + 1, strRelationTarget.Length - strRelationTarget.LastIndexOf(".") - 1)
                        strTargetTableAlias = basCode.GetTableAliasFromString(strRelationTarget)
                        strTargetTable = basCode.GetTableNameFromString(strRelationTarget)
                    End If
                End If
                If basCode.dhdText.CheckNodeElement(xRNode, "RelationTargetTable") Then strTargetTable = xRNode.Item("RelationTargetTable").InnerText
                If basCode.dhdText.CheckNodeElement(xRNode, "RelationTargetAlias") Then strTargetTableAlias = xRNode.Item("RelationTargetAlias").InnerText
                If basCode.dhdText.CheckNodeElement(xRNode, "RelationTargetField") Then strTargetField = basCode.GetFieldAliasFromName(basCode.xmlTables, strTargetTable, xRNode.Item("RelationTargetField").InnerText)
                If strTargetTableAlias = "" Then strTargetTableAlias = strTargetTable

                If intRelationNumber = 1 Then
                    For Each lvwSortItem As ListViewItem In lvwSelectedTables.Items
                        If lvwSortItem.Text = strTableAlias Then
                            lvwSelectedTables.SelectedItems.Clear()
                            lvwSelectedTables.Items(lvwSortItem.Index).Selected = True
                            Try
                                While lvwSortItem.Index + 1 > intRelationCount
                                    btnReportTableUp_Click(lvwSelectedTables, Nothing)
                                End While
                                While lvwSortItem.Index + 1 < intRelationCount
                                    btnReportTableDown_Click(lvwSelectedTables, Nothing)
                                End While
                            Catch ex As Exception
                                WriteStatus("An error occured sorting the tables in the report. Please check the log", 1, lblStatusText)
                                basCode.WriteLog("An error occured sorting the tables in the report. " & Environment.NewLine & ex.Message, 1)
                            End Try
                        End If
                    Next
                End If
                SetCtrText(pnlRelationsTargetField, strTable, strTargetField, intRelationNumber)
                If strTargetTable.Length > 0 Then SetCtrText(pnlRelationsTargetTable, strTable, strTargetTableAlias & " (" & strTargetTable & ")", intRelationNumber)
                SetCtrText(pnlRelationsJoinType, strTable, xRNode.Item("RelationJoinType").InnerText, intRelationNumber)

            Next
            WriteStatus("Report loading completed.", 0, lblStatusText)
        Else
            WriteStatus("Selected report not found.", 0, lblStatusText)
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
                    If ctrControl.[GetType]().Name = "CheckField" Then
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
                    Select Case ctrControl.[GetType]().Name
                        Case "CheckField"
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
                        Case Else
                            ctrControl.Text = strValue
                    End Select
                End If
            End If
        Next
        If blnControlFound = False And intControlNumber > 1 Then
            If pnlInput.Parent Is pnlSelectedFields Then
                Dim lblTarget As Label = GetLabel(pnlReportLabel, strFieldName)
                If lblTarget Is Nothing Then
                    basCode.WriteLog("unable to add extra Filtercriterium for: " & strFieldName, 1)
                Else
                    lblShowField_DoubleClick(lblTarget, Nothing)
                    SetCtrText(pnlInput, strFieldName, strValue, intControlNumber)
                End If
            ElseIf pnlInput.Parent Is pnlRelations Then
                Dim lblTarget As Label = GetLabel(pnlRelationsLabel, strFieldName)
                If lblTarget Is Nothing Then
                    basCode.WriteLog("unable to add extra Relationcriterium for: " & strFieldName, 1)
                Else
                    lblShowRelation_DoubleClick(lblTarget, Nothing)
                    SetCtrText(pnlInput, strFieldName, strValue, intControlNumber)
                End If
            Else
                basCode.WriteLog("The Field: " & strFieldName & " on Panel: " & pnlInput.Name & " was not found." & "Unable to load the Value: " & strValue, 1)
            End If
        End If
    End Sub

    Private Function GetLabel(pnlSource As Panel, strFieldName As String) As Label
        For Each lblTarget As Label In pnlSource.Controls
            If lblTarget.Tag = strFieldName Then Return lblTarget
        Next
        Return Nothing
    End Function

    Private Sub ReportShow(dtsData As DataSet, Optional intPage As Integer = 0)

        'strReportText &= vbCrLf
        lblListCountNumber.Text = "0"
        lblErrorMessage.Text = strErrorMessage
        If basCode.dhdText.DatasetCheck(dtsData) = False Then Exit Sub

        Try
            If DataSet2DataGridView(dtsData, 0, dgvReport, True, intPage) = False Then
                WriteStatus("There was an error loading the report", 1, lblStatusText)
            End If
            'dgvReport.DataSource = dtsData.Tables(0)
            Dim intTotal As Integer = dtsData.Tables(0).Rows.Count.ToString
            Dim intStart As Integer = 0, intStop As Integer = 0
            If intPage > 0 Then
                intStart = intPage * 1000 - 999
                If intStart > intTotal Then intStart = (Math.Floor(intTotal / 1000)) * 1000 + 1
                intStop = intPage * 1000
                If intStop > intTotal Then intStop = intTotal
                lblListCountNumber.Text = intStart & " to " & intStop & " from " & intTotal
            Else
                intStart = 1
                intStop = intTotal
                'lblListCountNumber.Text = intTotal
                lblListCountNumber.Text = intStart & " to " & intStop & " from " & intTotal
            End If

            'DataGridViewColumnSize(dgvReport)
            lblErrorMessage.Text = "Command completed succesfully"
        Catch ex As Exception
            basCode.WriteLog("There was an error loading the report" & Environment.NewLine & ex.Message, 1)
            WriteStatus("There was an error loading the report. Please check the log", 1, lblStatusText)
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
    '        xmlDocReport = basCode.dhdText.CreateRootDocument(xmlDocReport, "Sequenchel", "Reports", True)
    '    End If

    '    Dim NewReportNode As XmlNode = basCode.dhdText.CreateAppendElement(xmlDocReport.Item("Sequenchel").Item("Reports"), "Report")
    '    basCode.dhdText.CreateAppendElement(NewReportNode, "ReportName", strReportName)
    '    basCode.dhdText.CreateAppendElement(NewReportNode, "Description", txtDescription.Text)
    '    basCode.dhdText.CreateAppendElement(NewReportNode, "UseTop", chkTop.Checked.ToString)
    '    basCode.dhdText.CreateAppendElement(NewReportNode, "UseTopNumber", txtTop.Text)
    '    basCode.dhdText.CreateAppendElement(NewReportNode, "UseDistinct", chkDistinct.Checked.ToString)
    '    Dim NewTablesNode As XmlNode = basCode.dhdText.CreateAppendElement(NewReportNode, "Tables")
    '    For intTableCount As Integer = 0 To lvwSelectedTables.Items.Count - 1
    '        strTable = lvwSelectedTables.Items.Item(intTableCount).Name
    '        Dim NewTableNode As XmlNode = basCode.dhdText.CreateAppendElement(NewTablesNode, "Table")
    '        basCode.dhdText.CreateAppendElement(NewTableNode, "TableName", strTable)
    '        Dim NewFieldsNode As XmlNode = basCode.dhdText.CreateAppendElement(NewTableNode, "Fields")
    '        For intFieldCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
    '            strField = lvwSelectedFields.Items.Item(intFieldCount).Name
    '            If strField.Length > strTable.Length Then
    '                If strField.Substring(0, strField.LastIndexOf(".")) = strTable Then
    '                    Dim strFieldName As String = strField.Substring(strField.LastIndexOf(".") + 1, strField.Length - strField.LastIndexOf(".") - 1)
    '                    Dim NewFieldNode As XmlNode = basCode.dhdText.CreateAppendElement(NewFieldsNode, "Field")
    '                    basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldName", strFieldName)
    '                    basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldShow", GetCtrText(pnlReportDisplay, strField))
    '                    basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldShowMode", GetCtrText(pnlReportShowMode, strField))
    '                    basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldSort", GetCtrText(pnlReportSort, strField))
    '                    basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldSortOrder", GetCtrText(pnlReportSortOrder, strField))

    '                    Dim NewFiltersNode As XmlNode = basCode.dhdText.CreateAppendElement(NewFieldNode, "Filters")
    '                    For Each ctrFilter In pnlReportFilter.Controls
    '                        If ctrFilter.Tag = strField Then
    '                            intControlNumber = ctrFilter.Name.ToString.Substring(ctrFilter.Name.ToString.Length - strField.Length - 1, 1)
    '                            Dim NewFilterNode As XmlNode = basCode.dhdText.CreateAppendElement(NewFiltersNode, "Filter")
    '                            basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterNumber", intControlNumber)
    '                            basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterEnabled", ctrFilter.Checkstate.ToString)
    '                            basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterMode", GetCtrText(pnlReportFilterMode, strField, intControlNumber))
    '                            basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterType", GetCtrText(pnlReportFilterType, strField, intControlNumber))
    '                            basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterText", GetCtrText(pnlReportFilterText, strField, intControlNumber))
    '                        End If
    '                    Next
    '                End If
    '            End If
    '        Next
    '        Dim NewRelationsNode As XmlNode = basCode.dhdText.CreateAppendElement(NewTableNode, "Relations")
    '        For Each ctrRelation In pnlRelationsUse.Controls
    '            If ctrRelation.Tag = strTable Then
    '                Dim NewRelationNode As XmlNode = basCode.dhdText.CreateAppendElement(NewRelationsNode, "Relation")
    '                intControlNumber = ctrRelation.Name.ToString.Substring(ctrRelation.Name.ToString.Length - strTable.Length - 1, 1)
    '                basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationNumber", intControlNumber)
    '                basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationEnabled", ctrRelation.Checked.ToString)
    '                basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationSource", GetCtrText(pnlRelationsField, strTable, intControlNumber))
    '                basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationTarget", GetCtrText(pnlRelationsRelation, strTable, intControlNumber))
    '                basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationJoinType", GetCtrText(pnlRelationsJoinType, strTable, intControlNumber))
    '            End If
    '        Next

    '    Next
    'End Sub

    Private Function ReportToXML(xmlReport As XmlDocument, strReportName As String) As XmlDocument
        Dim strField As String = ""
        Dim strFieldAlias As String = ""
        Dim strTableName As String = ""
        Dim strTableAlias As String = ""
        Dim intControlNumber As Integer = 0

        Dim root As XmlElement = xmlReport.DocumentElement
        If root Is Nothing Then
            xmlReport = basCode.dhdText.CreateRootDocument(xmlReport, "Sequenchel", "Reports", True)
        End If
        Dim NewReportNode As XmlNode = basCode.dhdText.CreateAppendElement(xmlReport.Item("Sequenchel").Item("Reports"), "Report")
        basCode.dhdText.CreateAppendElement(NewReportNode, "ReportName", strReportName)
        basCode.dhdText.CreateAppendElement(NewReportNode, "Description", txtDescription.Text)
        basCode.dhdText.CreateAppendElement(NewReportNode, "UseTop", chkTop.Checked.ToString)
        basCode.dhdText.CreateAppendElement(NewReportNode, "UseTopNumber", txtTop.Text)
        basCode.dhdText.CreateAppendElement(NewReportNode, "UseDistinct", chkDistinct.Checked.ToString)

        Dim NewFieldsNode As XmlNode = basCode.dhdText.CreateAppendElement(NewReportNode, "Fields")
        For intFieldCount As Integer = 0 To lvwSelectedFields.Items.Count - 1
            strField = lvwSelectedFields.Items.Item(intFieldCount).Name
            strFieldAlias = lvwSelectedFields.Items.Item(intFieldCount).SubItems(1).Text
            strTableName = strField.Substring(0, strField.LastIndexOf("."))
            strTableAlias = lvwSelectedFields.Items.Item(intFieldCount).Text
            'If strField.Length > strTable.Length Then

            Dim strFieldName As String = strField.Substring(strField.LastIndexOf(".") + 1, strField.Length - strField.LastIndexOf(".") - 1)
            Dim NewFieldNode As XmlNode = basCode.dhdText.CreateAppendElement(NewFieldsNode, "Field")
            basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldName", strFieldName)
            basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldAlias", strFieldAlias)
            basCode.dhdText.CreateAppendElement(NewFieldNode, "TableName", strTableName)
            basCode.dhdText.CreateAppendElement(NewFieldNode, "TableAlias", strTableAlias)
            basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldShow", GetCtrText(pnlReportDisplay, strField))
            basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldShowMode", GetCtrText(pnlReportShowMode, strField))
            basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldSort", GetCtrText(pnlReportSort, strField))
            basCode.dhdText.CreateAppendElement(NewFieldNode, "FieldSortOrder", GetCtrText(pnlReportSortOrder, strField))

            Dim NewFiltersNode As XmlNode = basCode.dhdText.CreateAppendElement(NewFieldNode, "Filters")
            For Each ctrFilter In pnlReportFilter.Controls
                If ctrFilter.Tag = strField Then
                    intControlNumber = ctrFilter.Name.ToString.Substring(ctrFilter.Name.ToString.Length - strField.Length - 1, 1)
                    Dim NewFilterNode As XmlNode = basCode.dhdText.CreateAppendElement(NewFiltersNode, "Filter")
                    basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterNumber", intControlNumber)
                    basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterEnabled", ctrFilter.Checkstate.ToString)
                    basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterMode", GetCtrText(pnlReportFilterMode, strField, intControlNumber))
                    basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterType", GetCtrText(pnlReportFilterType, strField, intControlNumber))
                    basCode.dhdText.CreateAppendElement(NewFilterNode, "FilterText", GetCtrText(pnlReportFilterText, strField, intControlNumber))
                End If
            Next

            'End If
        Next

        intControlNumber = 0
        Dim NewRelationsNode As XmlNode = basCode.dhdText.CreateAppendElement(NewReportNode, "Relations")
        For intTableCount As Integer = 0 To lvwSelectedTables.Items.Count - 1
            strTableName = lvwSelectedTables.Items.Item(intTableCount).Tag
            strTableAlias = lvwSelectedTables.Items.Item(intTableCount).Text

            For Each ctrRelation In pnlRelationsUse.Controls
                If ctrRelation.Tag = strTableName Then
                    Dim NewRelationNode As XmlNode = basCode.dhdText.CreateAppendElement(NewRelationsNode, "Relation")
                    intControlNumber = ctrRelation.Name.ToString.Substring(ctrRelation.Name.ToString.Length - strTableName.Length - 1, 1)
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "TableName", strTableName)
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "TableAlias", strTableAlias)
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationNumber", intControlNumber)
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationEnabled", ctrRelation.Checked.ToString)
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationSource", GetCtrText(pnlRelationsField, strTableName, intControlNumber))
                    Dim strTable As String = basCode.GetTableNameFromString(GetCtrText(pnlRelationsTargetTable, strTableName, intControlNumber))
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationTargetTable", strTable)
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationTargetAlias", basCode.GetTableAliasFromString(GetCtrText(pnlRelationsTargetTable, strTableName, intControlNumber)))
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationTargetField", basCode.GetFieldNameFromAlias(basCode.xmlTables, strTable, GetCtrText(pnlRelationsTargetField, strTableName, intControlNumber)))
                    basCode.dhdText.CreateAppendElement(NewRelationNode, "RelationJoinType", GetCtrText(pnlRelationsJoinType, strTableName, intControlNumber))
                End If
            Next
        Next
        Return xmlReport
    End Function

    Private Sub ReportDelete(strSelection As String)
        basCode.dhdText.RemoveNode(basCode.xmlReports, "Report", "ReportName", strSelection)
    End Sub

    Private Sub SaveQuery()
        If rtbQuery.Text.Length = 0 Then Exit Sub
        Dim saveFile1 As New SaveFileDialog()

        saveFile1.DefaultExt = "*.sql"
        saveFile1.Filter = "Query Files|*.sql"

        If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (saveFile1.FileName.Length) > 0 Then
            Try
                rtbQuery.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText)
                WriteStatus("Query Saved.", 0, lblStatusText)
            Catch ex As Exception
                basCode.WriteLog("There was an error saving the query. " & ex.Message, 1)
                WriteStatus("There was an error saving the query. Please check the log.", 1, lblStatusText)
            End Try
        End If
    End Sub

    Private Sub LoadQuery()
        Dim loadFile1 As New OpenFileDialog

        loadFile1.DefaultExt = "*.sql"
        loadFile1.Filter = "Query Files|*.sql"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            Try
                rtbQuery.LoadFile(loadFile1.FileName, RichTextBoxStreamType.PlainText)
            Catch ex As Exception
                basCode.WriteLog("There was an error loading the query. " & ex.Message, 1)
                WriteStatus("There was an error loading the query. Please check the log.", 1, lblStatusText)
            End Try
        End If
    End Sub

    Private Sub btnDefinition_Click(sender As Object, e As EventArgs) Handles btnDefinition.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
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
        WriteStatus("", 0, lblStatusText)
        Dim xmlExport As XmlDocument = basCode.dhdText.CreateRootDocument(Nothing, Nothing, Nothing)
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
                    basCode.dhdText.CreateFile(sw.ToString(), strTargetFile)
                End Using
            End Using
            WriteStatus("Report exported.", 0, lblStatusText)
        Catch ex As Exception
            basCode.WriteLog(ex.Message, 1)
            WriteStatus("An error occured exporting the report definition. Please check the log.", 1, lblStatusText)
        End Try
        CursorControl()
    End Sub

    Private Sub btnReportImport_Click(sender As Object, e As EventArgs) Handles btnReportImport.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim xmlImport As New XmlDocument
        Dim strReportName As String
        Dim loadFile1 As New OpenFileDialog

        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Report Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            Try
                xmlImport.Load(loadFile1.FileName)
            Catch ex As Exception
                basCode.WriteLog("Error importing Report: " & ex.Message, 1)
                WriteStatus("An error occured importing the report definition. Please check the log.", 1, lblStatusText)
                CursorControl()
                Exit Sub
            End Try
        End If
        If loadFile1.FileName = "" Then Exit Sub
        Try
            Dim xNode As XmlNode = basCode.dhdText.FindXmlNode(xmlImport, "ReportName")
            strReportName = xNode.InnerText
            ReportFieldsDispose(False)
            ReportLoad(xmlImport, strReportName)
            'XmlToReport(xmlImport, strReportName)
            If cbxReportName.Items.Contains(strReportName) Then
                strReportName &= "_" & FormatDateTime(Now())
            End If
            cbxReportName.Text = strReportName
            WriteStatus("Report definition imported", 0, lblStatusText)
        Catch ex As Exception
            basCode.WriteLog("Error importing Report: " & ex.Message, 1)
            WriteStatus("An error occured importing the report definition. Please check the log.", 1, lblStatusText)
        End Try
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
    '        pnlReportName.Left = cbxTable.Left + cbxTable.Width + basCode.CurVar.BuildMargin
    '    Else
    '        pnlReportName.Anchor = AnchorStyles.Top Or AnchorStyles.Right
    '    End If
    'End Sub

    'Private Sub chkShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkReportShow.CheckedChanged
    '    For Each chkBox As CheckBox In pnlReportDisplay.Controls
    '        chkBox.Checked = chkReportShow.Checked
    '    Next
    'End Sub

    Private Sub chkShow_CheckedStateChanged(sender As Object, e As EventArgs) Handles chkReportShow.CheckStateChanged
        If basCode.curStatus.SuspendActions = False Then
            basCode.curStatus.SuspendActions = True
            If chkReportShow.CheckState = CheckState.Indeterminate Then chkReportShow.CheckState = CheckState.Unchecked
            For Each chkBox As CheckBox In pnlReportDisplay.Controls
                chkBox.Checked = chkReportShow.Checked
            Next
            basCode.curStatus.SuspendActions = False
        End If
    End Sub

    Private Sub chkShowField_CheckChanged(sender As Object, e As EventArgs)
        If basCode.curStatus.SuspendActions = False Then
            basCode.curStatus.SuspendActions = True
            Dim intCount As Integer = 0
            For Each chkBox As CheckBox In pnlReportDisplay.Controls
                If chkBox.Checked = True Then intCount += 1
            Next
            If intCount = 0 Then chkReportShow.CheckState = CheckState.Unchecked
            If intCount > 0 And intCount < pnlReportDisplay.Controls.Count Then chkReportShow.CheckState = CheckState.Indeterminate
            If intCount = pnlReportDisplay.Controls.Count Then chkReportShow.CheckState = CheckState.Checked
            basCode.curStatus.SuspendActions = False
        End If
    End Sub

    Private Sub btnEmailResults_Click(sender As Object, e As EventArgs) Handles btnEmailResults.Click
        WriteStatus("", 0, lblStatusText)
        If basCode.dhdText.SmtpServer.Length = 0 Then
            WriteStatus("Email is not configured. No email was sent", 2, lblErrorMessage)
            Exit Sub
        End If
        If dgvReport.RowCount = 0 Then Exit Sub
        basCode.dhdText.SmtpRecipient = InputBox("Please enter a valid Email Address", "Email", basCode.dhdText.SmtpRecipient)
        If basCode.dhdText.SmtpRecipient = "" Or basCode.dhdText.EmailAddressCheck(basCode.dhdText.SmtpRecipient) = False Then
            WriteStatus("Email address not valid. No email was sent", 2, lblErrorMessage)
            Exit Sub
        End If
        CursorControl("Wait")
        Dim strRecepientName As String = basCode.dhdText.SmtpRecipient.Substring(0, basCode.dhdText.SmtpRecipient.IndexOf("@"))
        Dim strSenderName As String = basCode.dhdText.SmtpReply.Substring(0, basCode.dhdText.SmtpReply.IndexOf("@"))

        Dim strReportName As String = "", strTargetName As String
        If cbxReportName.Text.Length > 0 Then
            strReportName = cbxReportName.Text
        Else
            strReportName = basCode.curStatus.Connection
        End If
        strTargetName = strReportName
        If basCode.curVar.IncludeDate = True Then
            strTargetName = strTargetName & "_" & basCode.FormatFileDate(Now)
        End If

        Dim strBody As String = "Sequenchel Report: " & strReportName
        Try
            Select Case cbxEmailResults.SelectedItem
                Case "HTML"
                    strTargetName = Nothing
                    strBody &= Environment.NewLine & basCode.dhdText.DataSetToHtml(dtsReport)
                Case "Excel"
                    strTargetName &= ".xlsx"
                    basCode.Excel.CreateExcelDocument(dtsReport, strTargetName)
                Case "XML"
                    strTargetName &= ".xml"
                    basCode.dhdText.ExportDataSetToXML(basCode.dhdMainDB.ConvertToText(dtsReport), strTargetName)
                Case "CSV"
                    strTargetName &= ".csv"
                    basCode.dhdText.DataSetToCsv(dtsReport.Tables(0), strTargetName, True, ",", False)
                Case Else
                    'unknown filetype, do nothing
            End Select
            basCode.dhdText.SendSMTP(basCode.dhdText.SmtpReply, strSenderName, basCode.dhdText.SmtpRecipient, strRecepientName, basCode.dhdText.SmtpReply, strSenderName, strReportName, strBody, strTargetName)
            basCode.dhdText.DeleteFile(strTargetName)
            WriteStatus("Email was sent.", 0, lblStatusText)
        Catch ex As Exception
            basCode.WriteLog("An error occured sending your email. " & ex.Message, 1)
            WriteStatus("An error occured sending your email. Please check the log.", 1, lblStatusText)
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

    Private Sub lblRelationsRelation_MouseHover(sender As Object, e As EventArgs)
        sptReport.BackColor = Color.LightGray
        sptReportTables.BackColor = Color.LightGray
    End Sub

    Private Sub lblRelationsRelation_MouseLeave(sender As Object, e As EventArgs)
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

    Private Sub lvwAvailableFields_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lvwAvailableFields.ColumnClick
        Me.lvwAvailableFields.ListViewItemSorter = New ListViewItemComparer(e.Column)
    End Sub

    Private Sub dgvReport_DoubleClick(sender As Object, e As EventArgs) Handles dgvReport.DoubleClick

    End Sub

    Private Sub cbxRelationTargetTable_SelectedIndexChanged(sender As Object, e As EventArgs)
        If sender.[GetType]().Name = "ComboField" Then
            Dim cbxSource As ComboField = TryCast(sender, ComboField)
            If Not cbxSource Is Nothing Then
                'Find Table Alias for collecting Fields
                Dim strtable As String = cbxSource.SelectedItem
                If String.IsNullOrEmpty(strtable) Then Exit Sub
                Dim strTargetTable As String = basCode.GetTableAliasFromString(strtable)

                'Find correct combobox to fill...
                Dim strSourceTable As String = sender.tag
                Dim strName As String = sender.name
                Dim strCounter As String = strName.Substring(pnlRelationsTargetTable.Name.Length, 1)
                Dim strTargetBox As String = pnlRelationsTargetField.Name & strCounter & strSourceTable
                For Each cbxTarget As ComboField In pnlRelationsTargetField.Controls
                    If cbxTarget.Name = strTargetBox Then
                        ComboBoxFill(cbxTarget, "RelationTargetfield", strTargetTable)
                    End If
                Next
            End If
        End If

    End Sub


End Class