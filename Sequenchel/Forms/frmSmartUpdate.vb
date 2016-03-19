Public Class frmSmartUpdate

    Dim strSourceSchema As String = ""
    Dim strSourceTable As String = ""
    Dim strTargetSchema As String = ""
    Dim strTargetTable As String = ""

    Private Sub frmSmartUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If blnLicenseValidated = True Then
            btnCreateSmartUpdateProcedure.Enabled = True
            lblLicenseRequired.Visible = False
        End If
        LoadConnections()
        LoadTables()
        dtpStartDate.Value = Today()
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
            LoadTables()
            PanelsClear()
            txtSourceTable.Text = ""
            txtTargetTable.Text = ""
            'dhdText.FindXmlNode(xmlConnections, "Connection", "DatabasdeName", strConnection)
            'Dim xmlConnNode As xmlnode = xmlConnections.SelectSingleNode("\\Connection", "descendant::Connection[DataBaseName='" & strConnection & "']")
            'dhdConnection.DatabaseName = strConnection
        End If
    End Sub

    Private Sub btnCreateSmartUpdateTable_Click(sender As Object, e As EventArgs) Handles btnCreateSmartUpdateTable.Click
        Dim strSQL As String = ""

        Try
            Dim MydbRef As New SDBA.DBRef

            strSQL = MydbRef.GetScript("01 dbo.SmartUpdate.sql")
            strSQL = strSQL.Replace("Sequenchel", dhdConnection.DatabaseName)
            If CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If DevMode Then MessageBox.Show(strSQL)
            QueryDb(dhdConnection, strSQL, False, 10)
            lblStatusText.Text = "SmartUpdate Table created succesfully"
        Catch ex As Exception
            MessageBox.Show("There was an error while creating the SmartUpdate Table" & Environment.NewLine & ex.Message, "Error Creating Table", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnCreateSmartUpdateProcedure_Click(sender As Object, e As EventArgs) Handles btnCreateSmartUpdateProcedure.Click
        Dim strSQL As String = ""

        Try
            Dim MydbRef As New SDBA.DBRef

            strSQL = MydbRef.GetScript("01 dbo.usp_SmartUpdate.sql")
            strSQL = strSQL.Replace("Sequenchel", dhdConnection.DatabaseName)
            If CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If DevMode Then MessageBox.Show(strSQL)
            QueryDb(dhdConnection, strSQL, False, 10)
            lblStatusText.Text = "SmartUpdate Procedure created succesfully"
        Catch ex As Exception
            MessageBox.Show("There was an error while creating the SmartUpdate Procedure" & Environment.NewLine & ex.Message, "Error Creating Procedure", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnCrawlSourceTables_Click(sender As Object, e As EventArgs) Handles btnCrawlSourceTables.Click
        lstSourceTables.Visible = True
        lstSourceTables.Focus()
    End Sub

    Private Sub btnCrawlTargetTables_Click(sender As Object, e As EventArgs) Handles btnCrawlTargetTables.Click
        lstTargetTables.Visible = True
        lstTargetTables.Focus()
    End Sub

    Private Sub lstSourceTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSourceTables.SelectedIndexChanged
        txtSourceTable.Text = lstSourceTables.SelectedItem
        lstSourceTables.Visible = False
    End Sub

    Private Sub lstSourceTables_LostFocus(sender As Object, e As EventArgs) Handles lstSourceTables.MouseLeave
        lstSourceTables.Visible = False
    End Sub

    Private Sub lstTargetTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTargetTables.SelectedIndexChanged
        txtTargetTable.Text = lstTargetTables.SelectedItem
        lstTargetTables.Visible = False
    End Sub

    Private Sub lstTargetTables_LostFocus(sender As Object, e As EventArgs) Handles lstTargetTables.MouseLeave
        lstTargetTables.Visible = False
    End Sub

    Private Sub btnImportTables_Click(sender As Object, e As EventArgs) Handles btnImportTables.Click
        If txtSourceTable.Text.Length = 0 Then
            MessageBox.Show("You need to select at least a source table")
            Exit Sub
        End If

        GetTableNames()
        ResetScreen()
        GetColumns(strSourceSchema, strSourceTable, strTargetSchema, strTargetTable)
    End Sub

    Private Sub dtpEndDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpEndDate.ValueChanged
        'dtpEndDate.CustomFormat = "yyyy-MM-dd"
        chkNoEndDate.Checked = False
    End Sub

    Private Sub chkNoEndDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoEndDate.CheckedChanged
        If chkNoEndDate.Checked = False Then
            dtpEndDate.CustomFormat = "yyyy-MM-dd"
        Else
            dtpEndDate.CustomFormat = " "
        End If
    End Sub

    Private Sub btnSaveConfiguration_Click(sender As Object, e As EventArgs) Handles btnSaveConfiguration.Click
        'check for table dbo.SmartUpdate
        'Dim strSQL As String = "IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'SmartUpdate') SELECT 1 AS TableExists ELSE SELECT 0 AS TableExists"
        Dim strSQL As String = "SELECT 1 AS TableExists FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'SmartUpdate'"
        Dim dtsData As DataSet = QueryDb(dhdConnection, strSQL, True, 5)
        If DatasetCheck(dtsData) = False Then
            lblStatusText.Text = "The SmartUpdate table was not found. Please create the table first."
            Exit Sub
        End If

        'get start & end date
        Dim dtmStartDate As Date = dtpStartDate.Value.Date
        Dim dtmEndDate As Date = dtpEndDate.Value.Date
        If chkNoEndDate.Checked = True Then dtmEndDate = Nothing

        'set table name (source or target)
        GetTableNames()
        Dim strSchemaName As String = ""
        Dim strTableName As String = ""
        Dim pnlTable As Panel = Nothing
        Dim pnlDataType As Panel = Nothing
        Dim pnlPrimaryKey As Panel = Nothing
        Dim strInsert As String = ""
        Dim strUpdate As String = ""
        Dim strDelete As String = ""
        If rbnSourceConfig.Checked = True Then
            strSchemaName = strSourceSchema
            strTableName = strSourceTable
            pnlTable = pnlSourceTable
            pnlDataType = pnlSourceDataType
            pnlPrimaryKey = pnlSourcePrimaryKey
        ElseIf rbnTargetConfig.Checked = True Then
            strSchemaName = strTargetSchema
            strTableName = strTargetTable
            pnlTable = pnlTargetTable
            pnlDataType = pnlTargetDataType
            pnlPrimaryKey = pnlTargetPrimaryKey
        End If
        strInsert = InsertString(strSchemaName, strTableName, pnlTable, pnlDataType, pnlPrimaryKey, pnlCompareColumn, dtmStartDate, dtmEndDate)
        strUpdate = "UPDATE dbo.SmartUpdate SET [DateStop] = '" & dtmStartDate.AddDays(-1).ToString("yyyy-MM-dd") & "' WHERE [DataBaseName] = '" & dhdConnection.DatabaseName & "' AND [SchemaName] = '" & strSchemaName & "' AND [TableName] = '" & strTableName & "' 	AND [DateStart] < '" & dtmStartDate.ToString("yyyy-MM-dd") & "' AND COALESCE([DateStop],'2999-12-31') > '" & dtmStartDate.ToString("yyyy-MM-dd") & "' AND [Active] = 1"
        If dtmEndDate = Nothing Then dtmEndDate = "2999-12-31"
        strDelete = "UPDATE dbo.SmartUpdate SET [Active] = 0 WHERE [DataBaseName] = '" & dhdConnection.DatabaseName & "' AND [SchemaName] = '" & strSchemaName & "' AND [TableName] = '" & strTableName & "' AND [DateStart] BETWEEN '" & dtmStartDate.ToString("yyyy-MM-dd") & "' AND '" & dtmEndDate.ToString("yyyy-MM-dd") & "' AND [Active] = 1"
        'strDelete = "DELETE FROM dbo.SmartUpdate WHERE [DataBaseName] = '" & dhdConnection.DatabaseName & "' AND [SchemaName] = '" & strSchemaName & "' AND [TableName] = '" & strTableName & "'"

        'get compare columns & PK columns
        strInsert = strInsert.Remove(0, 6)
        strInsert = "INSERT INTO [dbo].[SmartUpdate] ([DataBaseName],[SchemaName],[TableName],[ColumnName],[DataType],[PrimaryKey],[CompareColumn],[DateStart],[DateStop],[Active]) " & Environment.NewLine & strInsert

        'save to table dbo.SmartUpdate
        Try
            QueryDb(dhdConnection, strUpdate, 0)
            QueryDb(dhdConnection, strDelete, 0)
            QueryDb(dhdConnection, strInsert, 0)
            lblStatusText.Text = "Configuration Saved to SmartUpdate Table on connection: " & CurStatus.Connection
        Catch ex As Exception
            lblStatusText.Text = "There was an error saving the configuration. Check the log for more details"
            MessageBox.Show("There was an error saving the configuration: " & Environment.NewLine & ex.Message)
            WriteLog("There was an error saving the configuration: " & Environment.NewLine & ex.Message, 1)
        End Try


    End Sub

    Private Sub txtSourceTable_TextChanged(sender As Object, e As EventArgs) Handles txtSourceTable.TextChanged
        SmartUpdateCommand()
    End Sub

    Private Sub txtTargetTable_TextChanged(sender As Object, e As EventArgs) Handles txtTargetTable.TextChanged
        SmartUpdateCommand()
    End Sub

    Private Sub chkCreateTargetTable_CheckedChanged(sender As Object, e As EventArgs) Handles chkCreateTargetTable.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub chkUseAuditing_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseAuditing.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub chkCreateAuditTable_CheckedChanged(sender As Object, e As EventArgs) Handles chkCreateAuditTable.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub chkRemoveNonSourceData_CheckedChanged(sender As Object, e As EventArgs) Handles chkRemoveNonSourceData.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub chkUseTargetCollation_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseTargetCollation.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub btnAddSmartUpdateSchedule_Click(sender As Object, e As EventArgs) Handles btnAddSmartUpdateSchedule.Click
        'MessageBox.Show("The Scheduler is not yet operational. Please schedule the SmartUpdate Command manually.")

        'get logpath
        Dim strLogPath As String = GetDefaultLogPath(dhdConnection)
        'get jobname
        Dim strJobName As String = GetJobName(dhdConnection, "SmartUpdate")
        If strJobName = "" Then
            lblStatusText.Text = "Job SmartUpdate was not found on the server. Create the job first with the scheduler (Settings)"
            Exit Sub
        End If
        'get JobStepCount
        Dim intJobStepCount As Integer = GetJobStepCount(dhdConnection, strJobName)
        If intJobStepCount = -1 Then
            lblStatusText.Text = "There was an error retrieving information about the SmartUpdate Job, aborting action"
            Exit Sub
        End If
        Dim intFlags As Integer = 0
        If intJobStepCount > 0 Then intFlags = 2

        'get SqlCommand
        Dim strSQL As String = txtSmartUpdateCommand.Text

        'create jobstep
        strQuery = " EXEC msdb.dbo.sp_add_jobstep "
        strQuery &= " @job_name='" & strJobName & "',"
        strQuery &= " @step_name='SU_" & txtSourceTable.Text & "_" & txtTargetTable.Text & "',"
        strQuery &= " @subsystem=N'TSQL',"
        strQuery &= " @command='" & strSQL & "',"
        strQuery &= " @database_name='" & dhdConnection.DatabaseName & "',"
        strQuery &= " @output_file_name='" & strLogPath & "\SmartUpdate.log',"
        strQuery &= " @flags=" & intFlags & ";"

        QueryDb(dhdConnection, strQuery, 0)
        lblStatusText.Text = "Jobstep added to job: " & strJobName & " on database: " & dhdConnection.DatabaseName
    End Sub

    Private Sub LoadTables()
        CrawlTables(True, lstSourceTables)
        CrawlTables(False, lstTargetTables)
    End Sub

    Private Sub CrawlTables(blnCrawlViews As Boolean, lstTarget As ListBox)
        If CheckSqlVersion(dhdConnection) = False Then Exit Sub

        Dim lstNewTables As List(Of String) = LoadTablesList(dhdConnection, blnCrawlViews)

        If lstNewTables Is Nothing Then
            CursorControl()
            lblStatusText.Text = "No tables found"
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
        'lstTarget.Visible = True
        'lstTarget.Focus()
    End Sub

    Private Sub GetTableNames()
        strSourceSchema = ""
        strSourceTable = ""
        strTargetSchema = ""
        strTargetTable = ""
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
        Dim blnSourceOnly As Boolean = False
        dtsTables = QueryDb(dhdConnection, strSQL, True, 5)
        If DatasetCheck(dtsTables) = False Then Exit Sub
        PanelsClear()
        For intRowCount1 As Integer = 0 To dtsTables.Tables(0).Rows.Count - 1
            If dtsTables.Tables.Item(0).Rows(intRowCount1).Item("tgtSchemaName").GetType().ToString = "System.DBNull" Then
                'create source row only
                If intRowCount1 = 0 And (txtTargetTable.Text = "" Or lstTargetTables.Items.Contains(txtTargetTable.Text) = 0) Then blnSourceOnly = True
                SourceFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), blnSourceOnly)
                SourceDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), blnSourceOnly)
                SourcePkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcIdentity"), blnSourceOnly)
                If blnSourceOnly = True Then
                    CompareColumnAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType")))
                    chkCreateTargetTable.Checked = True
                Else
                    chkCreateTargetTable.Checked = False
                End If
            ElseIf dtsTables.Tables.Item(0).Rows(intRowCount1).Item("srcSchemaName").GetType().ToString = "System.DBNull" Then
                'create target row only
                TargetFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), False)
                TargetDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType"), False)
                TargetPkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtIdentity"), False)
            Else
                'create CC row
                chkCreateTargetTable.Checked = False
                rbnTargetConfig.Enabled = True
                SourceFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), True)
                TargetFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), True)
                SourceDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), True)
                TargetDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType"), True)
                SourcePkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcIdentity"), True)
                TargetPkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtIdentity"), True)
                CompareColumnAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType")))
            End If

        Next

        CheckPrimaryKey(pnlSourcePrimaryKey)
        CheckPrimaryKey(pnlTargetPrimaryKey)

        Dim intPercent As Integer = pnlCompareColumn.Controls.Count / dtsTables.Tables(0).Rows.Count * 200
        If intPercent < 50 Then
            lblStatusText.Text = "Matching columns is only " & intPercent & " percent. Are you sure you have the correct tables selected?"
        Else
            lblStatusText.Text = ""
        End If
    End Sub

    Private Sub ResetScreen()
        lblStatusText.Text = ""
        chkCreateTargetTable.Checked = False
        rbnSourceConfig.Checked = True
        rbnTargetConfig.Enabled = False
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

    Private Sub SmartUpdateCommand()
        Dim strCommand As String = ""
        If txtSourceTable.Text.Length > 0 And txtTargetTable.Text.Length > 0 Then
            GetTableNames()
            strCommand = "EXECUTE [dbo].[usp_SmartUpdate] "
            strCommand &= "'" & strSourceSchema & "', "
            strCommand &= "'" & strSourceTable & "', "
            strCommand &= "'" & strTargetSchema & "', "
            strCommand &= "'" & strTargetTable & "', "
            strCommand &= If(chkCreateTargetTable.Checked, 1, 0) & ", "
            strCommand &= If(chkUseAuditing.Checked, 1, 0) & ", "
            strCommand &= If(chkCreateAuditTable.Checked, 1, 0) & ", "
            strCommand &= If(chkRemoveNonSourceData.Checked, 1, 0) & ", "
            strCommand &= If(chkUseTargetCollation.Checked, 1, 0)
        End If
        txtSmartUpdateCommand.Text = strCommand
    End Sub

    Private Function InsertString(strSchema As String, strTable As String, pnlTabel As Panel, pnlDataType As Panel, pnlPrimaryKey As Panel, pnlCompare As Panel, dtmStart As Date, dtmEnd As Date) As String
        Dim strQuery As String = ""
        Dim strColumnName As String = ""
        Dim strDataType As String = ""
        Dim strPrmaryKey As String = ""
        Dim strCompare As String = ""

        For Each ctrlColumn In pnlTabel.Controls
            If ctrlColumn.Enabled = False Then Exit For
            strColumnName = ctrlColumn.Text
            For Each ctrlDataType In pnlDataType.Controls
                If ctrlDataType.Name = pnlDataType.Name & strColumnName Then
                    strDataType = ctrlDataType.Text
                End If
            Next
            For Each ctrlPk In pnlPrimaryKey.Controls
                If ctrlPk.Name = pnlPrimaryKey.Name & strColumnName Then
                    strPrmaryKey = ctrlPk.Checked
                End If
            Next
            For Each ctrlCompare In pnlCompare.Controls
                If ctrlCompare.Name = pnlCompare.Name & strColumnName Then
                    strCompare = ctrlCompare.Checked
                End If
            Next
            If strPrmaryKey = "True" Or strCompare = "True" Then
                strQuery &= "UNION SELECT '" & dhdConnection.DatabaseName & "', '" & strSchema & "', '" & strTable & "', '" & strColumnName & "', '" & strDataType & "', '" & strPrmaryKey & "', '" & strCompare & "', '" & dtmStart.ToString("yyyy-MM-dd") & "', " & If(chkNoEndDate.Checked = True, "NULL", "'" & dtmEnd.ToString("yyyy-MM-dd") & "'") & ",1" & Environment.NewLine
            End If
        Next

        Return strQuery
    End Function

End Class