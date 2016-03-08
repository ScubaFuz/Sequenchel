Public Class frmSmartUpdate

    Private Sub frmSmartUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadConnections()
    End Sub

    Private Sub LoadConnections()
        'AllClear(4)
        Dim lstConnections As List(Of String) = LoadConnectionsXml()
        If lstConnections Is Nothing Then Exit Sub
        For Each lstItem As String In lstConnections
            cbxConnection.Items.Add(lstItem)
        Next
        cbxConnection.SelectedItem = CurStatus.Connection
    End Sub

    Private Sub cbxConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxConnection.SelectedIndexChanged
        If cbxConnection.SelectedIndex >= -1 Then
            CurStatus.Connection = cbxConnection.SelectedItem
            LoadConnection(CurStatus.Connection)
            'dhdText.FindXmlNode(xmlConnections, "Connection", "DatabasdeName", strConnection)
            'Dim xmlConnNode As xmlnode = xmlConnections.SelectSingleNode("\\Connection", "descendant::Connection[DataBaseName='" & strConnection & "']")
            'dhdConnection.DatabaseName = strConnection
        End If
    End Sub

    Private Sub btnCrawlSourceTables_Click(sender As Object, e As EventArgs) Handles btnCrawlSourceTables.Click
        CursorControl("Wait")
        CrawlTables(True, lstSourceTables)
        CursorControl()
    End Sub

    Private Sub btnCrawlTargetTables_Click(sender As Object, e As EventArgs) Handles btnCrawlTargetTables.Click
        CursorControl("Wait")
        CrawlTables(False, lstTargetTables)
        CursorControl()
    End Sub

    Private Sub lstSourceTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSourceTables.SelectedIndexChanged
        txtSourceTable.Text = lstSourceTables.SelectedItem
        lstSourceTables.Visible = False
    End Sub

    Private Sub lstSourceTables_LostFocus(sender As Object, e As EventArgs) Handles lstSourceTables.LostFocus
        lstSourceTables.Visible = False
    End Sub

    Private Sub lstTargetTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTargetTables.SelectedIndexChanged
        txtTargetTable.Text = lstTargetTables.SelectedItem
        lstTargetTables.Visible = False
    End Sub

    Private Sub lstTargetTables_LostFocus(sender As Object, e As EventArgs) Handles lstTargetTables.LostFocus
        lstTargetTables.Visible = False
    End Sub

    Private Sub btnImportTables_Click(sender As Object, e As EventArgs) Handles btnImportTables.Click
        If txtSourceTable.Text.Length = 0 Then
            MessageBox.Show("You need to select at least a source table")
            Exit Sub
        End If

        Dim strSourceSchema As String = ""
        Dim strSourceTable As String = ""
        Dim strTargetSchema As String = ""
        Dim strTargetTable As String = ""
        If txtSourceTable.Text.LastIndexOf(".") > 0 Then
            strSourceSchema = txtSourceTable.Text.Substring(0, txtSourceTable.Text.LastIndexOf("."))
            strSourceTable = txtSourceTable.Text.Substring(txtSourceTable.Text.LastIndexOf(".") + 1, txtSourceTable.Text.Length - (txtSourceTable.Text.LastIndexOf(".") + 1))
        Else
            strSourceSchema = "dbo"
            strSourceTable = txtSourceTable.Text
        End If
        If txtTargetTable.Text.LastIndexOf(".") > 0 Then
            strTargetSchema = txtTargetTable.Text.Substring(0, txtTargetTable.Text.LastIndexOf("."))
            strTargetTable = txtTargetTable.Text.Substring(txtTargetTable.Text.LastIndexOf(".") + 1, txtTargetTable.Text.Length - (txtTargetTable.Text.LastIndexOf(".") + 1))
        Else
            strTargetSchema = "dbo"
            strTargetTable = txtTargetTable.Text
        End If
        GetColumns(strSourceSchema, strSourceTable, strTargetSchema, strTargetTable)
    End Sub

    Private Sub CrawlTables(blnCrawlViews As Boolean, lstTarget As ListBox)
        If CheckSqlVersion(dhdConnection) = False Then Exit Sub

        Dim lstNewTables As List(Of String) = LoadTablesList(dhdConnection, blnCrawlViews)

        If lstNewTables Is Nothing Then
            CursorControl()
            lblStatus.Text = "No tables found"
            Exit Sub
        End If

        lstTarget.Items.Clear()
        lstTarget.Items.Add("")

        For Each TableName In lstNewTables
            lstTarget.Items.Add(TableName)
        Next

        CursorControl()
        If lstTarget.Items.Count < 15 Then
            lstTarget.Height = lstSourceTables.Items.Count * 15
        Else
            lstTarget.Height = 15 * 15
        End If
        lstTarget.Visible = True
        lstTarget.Focus()
    End Sub

    Private Sub GetColumns(strSourceSchema As String, strSourceTable As String, strTargetSchema As String, strTargetTable As String)

        If strTargetSchema Is Nothing Then strTargetSchema = ""
        If strTargetTable Is Nothing Then strTargetTable = ""

        Dim strSQL As String = ""
        strSQL &= ";WITH ColSource As ("
        strSQL &= " SELECT scm.name AS SchemaName "
        strSQL &= "	,col.name As colName"
        strSQL &= "	,typ.name as DataType"
        strSQL &= "	,col.[object_id]"
        strSQL &= "	,col.is_identity"
        strSQL &= "	,case when pky.PrimaryKeyColumn is null then 0 else 1 end as pk"
        strSQL &= "	,200 As srcValue"
        strSQL &= " FROM sys.columns col"
        strSQL &= " INNER JOIN sys.types typ"
        strSQL &= "	ON col.system_type_id = typ.system_type_id"
        strSQL &= "	AND col.user_type_id = typ.user_type_id"
        strSQL &= " INNER JOIN sys.objects obj"
        strSQL &= "	ON col.[object_id] = obj.[object_id]"
        strSQL &= " INNER JOIN sys.schemas scm"
        strSQL &= "	ON obj.[schema_id] = scm.[schema_id]"
        strSQL &= " LEFT OUTER JOIN (SELECT KU.TABLE_SCHEMA as SchemaName"
        strSQL &= "					, KU.TABLE_NAME as TableName"
        strSQL &= "					,KU.COLUMN_NAME as PrimaryKeyColumn"
        strSQL &= "				FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC"
        strSQL &= "				INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU"
        strSQL &= "					ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' "
        strSQL &= "					AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME"
        strSQL &= "				WHERE KU.TABLE_SCHEMA = '" & strSourceSchema & "'"
        strSQL &= "					AND KU.TABLE_NAME = '" & strSourceTable & "') pky"
        strSQL &= "	ON col.name = pky.PrimaryKeyColumn"
        strSQL &= " WHERE obj.name = '" & strSourceTable & "'"
        strSQL &= "	AND scm.name = '" & strSourceSchema & "'"
        strSQL &= "),"
        strSQL &= " ColTarget as ("
        strSQL &= " SELECT scm.name AS SchemaName "
        strSQL &= "	,col.name As colName"
        strSQL &= "	,typ.name as DataType"
        strSQL &= "	,col.[object_id]"
        strSQL &= "	,col.is_identity"
        strSQL &= "	,case when pky.PrimaryKeyColumn is null then 0 else 1 end as pk"
        strSQL &= "	,100 As tgtValue"
        strSQL &= " FROM sys.columns col"
        strSQL &= " INNER JOIN sys.types typ"
        strSQL &= "	ON col.system_type_id = typ.system_type_id"
        strSQL &= "	AND col.user_type_id = typ.user_type_id"
        strSQL &= " INNER JOIN sys.objects obj"
        strSQL &= "	ON col.[object_id] = obj.[object_id]"
        strSQL &= " INNER JOIN sys.schemas scm"
        strSQL &= "	ON obj.[schema_id] = scm.[schema_id]"
        strSQL &= " LEFT OUTER JOIN (SELECT KU.TABLE_SCHEMA as SchemaName"
        strSQL &= "					, KU.TABLE_NAME as TableName"
        strSQL &= "					,KU.COLUMN_NAME as PrimaryKeyColumn"
        strSQL &= "				FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC"
        strSQL &= "				INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU"
        strSQL &= "					ON TC.CONSTRAINT_TYPE = 'PRIMARY KEY' "
        strSQL &= "					AND TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME"
        strSQL &= "				WHERE KU.TABLE_SCHEMA = '" & strTargetSchema & "'"
        strSQL &= "					AND KU.TABLE_NAME = '" & strTargetTable & "') pky"
        strSQL &= "	ON col.name = pky.PrimaryKeyColumn"
        strSQL &= " WHERE obj.name = '" & strTargetTable & "'"
        strSQL &= "	AND scm.name = '" & strTargetSchema & "'"
        strSQL &= ")"
        strSQL &= " SELECT src.SchemaName AS srcSchemaName "
        strSQL &= "	,src.colName AS srcColName"
        strSQL &= "	,src.DataType AS srcDataType"
        strSQL &= "	,tgt.SchemaName AS tgtSchemaName "
        strSQL &= "	,tgt.colName AS tgtColName"
        strSQL &= "	,tgt.DataType AS tgtDataType"
        strSQL &= "	,src.is_identity As srcIdentity"
        strSQL &= "	,src.pk AS srcPK"
        strSQL &= "	,tgt.is_identity As tgtIdentity"
        strSQL &= "	,tgt.pk AS tgtPK"
        strSQL &= "	,src.srcValue + tgt.tgtValue AS OrderValue"
        strSQL &= "	,src.srcValue "
        strSQL &= "	,tgt.tgtValue"
        strSQL &= " FROM ColSource src"
        strSQL &= " FULL OUTER JOIN ColTarget tgt"
        strSQL &= "	ON src.colName = tgt.colName"
        strSQL &= "	AND src.DataType = tgt.DataType"
        strSQL &= " ORDER BY OrderValue desc"
        strSQL &= "	,src.pk desc"
        strSQL &= "	,src.is_identity desc"
        strSQL &= "	,src.srcValue desc"
        strSQL &= "	,tgt.pk desc"
        strSQL &= "	,tgt.is_identity desc"
        strSQL &= "	,tgt.tgtValue desc"
        strSQL &= "	,src.colName"
        strSQL &= "	,tgt.colName"

        Dim dtsTables As New DataSet
        dtsTables = QueryDb(dhdConnection, strSQL, True, 5)
        If DatasetCheck(dtsTables) = False Then Exit Sub
        PanelsClear()
        For intRowCount1 As Integer = 0 To dtsTables.Tables(0).Rows.Count - 1
            If dtsTables.Tables.Item(0).Rows(intRowCount1).Item("tgtSchemaName").GetType().ToString = "System.DBNull" Then
                'create source row only
                SourceFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), False)
                SourceDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), False)
                SourcePkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcIdentity"), False)
            ElseIf dtsTables.Tables.Item(0).Rows(intRowCount1).Item("srcSchemaName").GetType().ToString = "System.DBNull" Then
                'create target row only
                TargetFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), False)
                TargetDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType"), False)
                TargetPkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtIdentity"), False)
            Else
                'create CC row
                SourceFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), True)
                TargetFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), True)
                SourceDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), True)
                TargetDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType"), True)
                SourcePkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcIdentity"), True)
                TargetPkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtIdentity"), True)
                CompareColumnAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), True)
            End If

        Next

        CheckPrimaryKey(pnlSourcePrimaryKey)
        CheckPrimaryKey(pnlTargetPrimaryKey)

        Dim intPercent As Integer = pnlCompareColumn.Controls.Count / dtsTables.Tables(0).Rows.Count * 200
        If intPercent < 50 Then
            lblStatus.Text = "Matching columns is " & intPercent & " percent. Are you sure you have the correct tables selected?"
        Else
            lblStatus.Text = ""
        End If
    End Sub

    Private Sub PanelsClear()
        pnlSourceTable.Controls.Clear()
        pnlSourceTable.Height = 50
        pnlTargetTable.Controls.Clear()
        pnlTargetTable.Height = 50

        pnlSourceDataType.Controls.Clear()
        pnlSourceDataType.Height = 50
        pnlTargetDataType.Controls.Clear()
        pnlTargetDataType.Height = 50

        pnlSourcePrimaryKey.Controls.Clear()
        pnlSourcePrimaryKey.Height = 50
        pnlTargetPrimaryKey.Controls.Clear()
        pnlTargetPrimaryKey.Height = 50

        pnlCompareColumn.Controls.Clear()
        pnlCompareColumn.Height = 50
    End Sub

    Private Sub SourceFieldAdd(strFieldName As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlSourceTable.Name & strFieldName
        txtNew.Text = strFieldName
        txtNew.Tag = strFieldName
        txtNew.Enabled = blnEnableCC
        txtNew.Width = 166
        pnlSourceTable.Controls.Add(txtNew)
        txtNew.Top = ((pnlSourceTable.Controls.Count - 1) * CurVar.FieldHeight)
        If pnlSourceTable.Height < txtNew.Top + txtNew.Height Then pnlSourceTable.Height = txtNew.Top + txtNew.Height
        txtNew.Left = CurVar.BuildMargin
    End Sub

    Private Sub SourceDataTypeAdd(strFieldName As String, strDataType As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlSourceDataType.Name & strFieldName
        txtNew.Text = strDataType
        txtNew.Tag = strDataType
        txtNew.Enabled = blnEnableCC
        txtNew.Width = 117
        pnlSourceDataType.Controls.Add(txtNew)
        txtNew.Top = ((pnlSourceTable.Controls.Count - 1) * CurVar.FieldHeight)
        pnlSourceDataType.Height = pnlSourceTable.Height
        txtNew.Left = CurVar.BuildMargin
    End Sub

    Private Sub SourcePkAdd(strFieldName As String, blnFieldPK As Boolean, blnFieldIdentity As Boolean, blnEnableCC As Boolean)
        Dim chkNew As New CheckBox
        chkNew.Name = pnlSourcePrimaryKey.Name & strFieldName
        chkNew.Checked = blnFieldPK
        chkNew.Tag = blnFieldIdentity
        chkNew.Enabled = blnEnableCC
        pnlSourcePrimaryKey.Controls.Add(chkNew)
        chkNew.Top = ((pnlSourceTable.Controls.Count - 1) * CurVar.FieldHeight)
        pnlSourcePrimaryKey.Height = pnlSourceTable.Height
        chkNew.Left = CurVar.BuildMargin
    End Sub

    Private Sub TargetFieldAdd(strFieldName As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlTargetTable.Name & strFieldName
        txtNew.Text = strFieldName
        txtNew.Tag = strFieldName
        txtNew.Enabled = blnEnableCC
        txtNew.Width = 166
        pnlTargetTable.Controls.Add(txtNew)
        txtNew.Top = ((pnlTargetTable.Controls.Count - 1) * CurVar.FieldHeight)
        If pnlTargetTable.Height < txtNew.Top + txtNew.Height Then pnlTargetTable.Height = txtNew.Top + txtNew.Height
        txtNew.Left = CurVar.BuildMargin

    End Sub

    Private Sub TargetDataTypeAdd(strFieldName As String, strDataType As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlTargetDataType.Name & strFieldName
        txtNew.Text = strDataType
        txtNew.Tag = strDataType
        txtNew.Enabled = blnEnableCC
        txtNew.Width = 117
        pnlTargetDataType.Controls.Add(txtNew)
        txtNew.Top = ((pnlTargetDataType.Controls.Count - 1) * CurVar.FieldHeight)
        pnlTargetDataType.Height = pnlTargetTable.Height
        txtNew.Left = CurVar.BuildMargin
    End Sub

    Private Sub TargetPkAdd(strFieldName As String, blnFieldPK As Boolean, blnFieldIdentity As Boolean, blnEnableCC As Boolean)
        Dim chkNew As New CheckBox
        chkNew.Name = pnlTargetPrimaryKey.Name & strFieldName
        chkNew.Checked = blnFieldPK
        chkNew.Tag = blnFieldIdentity
        chkNew.Enabled = blnEnableCC
        pnlTargetPrimaryKey.Controls.Add(chkNew)
        chkNew.Top = ((pnlTargetTable.Controls.Count - 1) * CurVar.FieldHeight)
        pnlTargetPrimaryKey.Height = pnlTargetTable.Height
        chkNew.Left = CurVar.BuildMargin
    End Sub

    Private Sub CompareColumnAdd(strFieldName As String, blnEnableCC As Boolean)
        Dim chkNew As New CheckBox
        chkNew.Name = pnlCompareColumn.Name & strFieldName
        chkNew.Checked = blnEnableCC
        chkNew.Tag = blnEnableCC
        chkNew.Enabled = blnEnableCC
        pnlCompareColumn.Controls.Add(chkNew)
        chkNew.Top = ((pnlSourceTable.Controls.Count - 1) * CurVar.FieldHeight)
        pnlCompareColumn.Height = pnlSourceTable.Height
        chkNew.Left = CurVar.BuildMargin
    End Sub

    Private Sub CheckPrimaryKey(pnlInput As Panel)
        Dim blnChecked As Boolean = False
        For Each Control In pnlInput.Controls
            If Control.checked = True Then blnChecked = True
        Next
        If blnChecked = False Then
            For Each Control In pnlInput.Controls
                Control.checked = Control.tag
            Next
        End If
    End Sub

End Class