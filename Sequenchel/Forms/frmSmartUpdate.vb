Public Class frmSmartUpdate

    Dim strSourceSchema As String = ""
    Dim strSourceTable As String = ""
    Dim strTargetSchema As String = ""
    Dim strTargetTable As String = ""

    Private Sub frmSmartUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CursorControl("Wait")
        If SeqCore.LicenseValidated = True Then
            btnCreateSmartUpdateProcedure.Enabled = True
            lblLicenseRequired.Visible = False
        End If
        LoadConnections()
        If cbxConnection.SelectedIndex <> -1 Then
            LoadTables()
        End If
        dtpStartDate.Value = Today()
        CursorControl()
    End Sub

#Region "Controls"

    Private Sub cbxConnection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxConnection.SelectedIndexChanged
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If cbxConnection.SelectedIndex >= -1 Then
            basCode.curStatus.Connection = cbxConnection.SelectedItem
            basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
            txtLocalDatabase.Text = basCode.dhdConnection.DatabaseName
            LoadLinkedServers()
            LoadTables()
            PanelsClear()
            txtSourceTable.Text = ""
            txtTargetTable.Text = ""
            'dhdText.FindXmlNode(basCode.xmlConnections, "Connection", "DatabasdeName", strConnection)
            'Dim xmlConnNode As xmlnode = basCode.xmlConnections.SelectSingleNode("\\Connection", "descendant::Connection[DataBaseName='" & strConnection & "']")
            'dhdConnection.DatabaseName = strConnection
        End If
        CursorControl()
    End Sub

    Private Sub btnCreateSmartUpdateTable_Click(sender As Object, e As EventArgs) Handles btnCreateSmartUpdateTable.Click
        Dim strSQL As String = ""
        WriteStatus("", 0, lblStatusText)

        Try
            'check if table exists
            strSQL = "Select TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'SmartUpdate'"
            Dim dtsdata As DataSet = basCode.QueryDb(basCode.dhdConnection, strSQL, True, 5)
            If basCode.dhdText.DatasetCheck(dtsdata) = True Then
                WriteStatus("SmartUpdate table already exists. Please delete the table before (re)creating it.", 2, lblStatusText)
                Exit Sub
            End If
            'create table
            strSQL = ""
            Dim MydbRef As New SDBA.DBRef
            strSQL = MydbRef.GetScript("01 dbo.SmartUpdate.sql")
            strSQL = strSQL.Replace("Sequenchel", basCode.dhdConnection.DatabaseName)
            If basCode.curVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If basCode.curVar.DevMode Then MessageBox.Show(strSQL)
            basCode.QueryDb(basCode.dhdConnection, strSQL, False, 10)
            If basCode.dhdConnection.ErrorLevel = -1 Then
                WriteStatus("There was an error creating the SmartUpdate Table. Please check the log.", 1, lblStatusText)
                basCode.WriteLog("There was an error creating the SmartUpdate Table. " & basCode.dhdConnection.ErrorMessage, 1)
            Else
                WriteStatus("SmartUpdate Table created succesfully.", 0, lblStatusText)
            End If
        Catch ex As Exception
            WriteStatus("There was an error creating the SmartUpdate Table. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error while creating the SmartUpdate Table" & Environment.NewLine & ex.Message, 1)
        End Try
    End Sub

    Private Sub btnCreateSmartUpdateProcedure_Click(sender As Object, e As EventArgs) Handles btnCreateSmartUpdateProcedure.Click
        Dim strSQL As String = ""
        WriteStatus("", 0, lblStatusText)
        Dim blnExists As Boolean = False
        Try
            'Check for procedure
            strSQL = "SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'usp_SmartUpdate' AND ROUTINE_SCHEMA = 'dbo' AND ROUTINE_TYPE = 'PROCEDURE'"
            Dim dtsdata As DataSet = basCode.QueryDb(basCode.dhdConnection, strSQL, True, 5)
            If basCode.dhdText.DatasetCheck(dtsdata) = True Then blnExists = True

            'create procedure
            Dim MydbRef As New SDBA.DBRef
            strSQL = MydbRef.GetScript("01 dbo.usp_SmartUpdate.sql")
            strSQL = strSQL.Replace("Sequenchel", basCode.dhdConnection.DatabaseName)
            If basCode.curVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If blnExists = True Then strSQL = strSQL.Replace("CREATE PROCEDURE", "ALTER PROCEDURE")
            If basCode.curVar.DevMode Then MessageBox.Show(strSQL)
            basCode.QueryDb(basCode.dhdConnection, strSQL, False, 10)
            If basCode.dhdConnection.ErrorLevel = -1 Then
                WriteStatus("There was an error creating the SmartUpdate Procedure. Please check the log.", 1, lblStatusText)
                basCode.WriteLog("There was an error creating the SmartUpdate Procedure. " & basCode.dhdConnection.ErrorMessage, 1)
            Else
                WriteStatus("SmartUpdate Procedure created succesfully.", 0, lblStatusText)
            End If
        Catch ex As Exception
            WriteStatus("There was an error creating the SmartUpdate Procedure. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error while creating the SmartUpdate Procedure" & Environment.NewLine & ex.Message, 1)
        End Try
    End Sub

    Private Sub btnCrawlSourceTables_Click(sender As Object, e As EventArgs) Handles btnCrawlSourceTables.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        lstSourceTables.Visible = True
        lstSourceTables.Focus()
        CursorControl()
    End Sub

    Private Sub btnCrawlTargetTables_Click(sender As Object, e As EventArgs) Handles btnCrawlTargetTables.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        lstTargetTables.Visible = True
        lstTargetTables.Focus()
        CursorControl()
    End Sub

    Private Sub lstSourceTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstSourceTables.SelectedIndexChanged
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        txtSourceTable.Text = lstSourceTables.SelectedItem
        lstSourceTables.Visible = False
        CursorControl()
    End Sub

    Private Sub lstSourceTables_LostFocus(sender As Object, e As EventArgs) Handles lstSourceTables.MouseLeave
        lstSourceTables.Visible = False
    End Sub

    Private Sub lstTargetTables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTargetTables.SelectedIndexChanged
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        txtTargetTable.Text = lstTargetTables.SelectedItem
        lstTargetTables.Visible = False
        CursorControl()
    End Sub

    Private Sub lstTargetTables_LostFocus(sender As Object, e As EventArgs) Handles lstTargetTables.MouseLeave
        lstTargetTables.Visible = False
    End Sub

    Private Sub btnImportTables_Click(sender As Object, e As EventArgs) Handles btnImportTables.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If txtSourceTable.Text.Length = 0 Then
            WriteStatus("You need to select at least a source table", 2, lblStatusText)
            Exit Sub
        End If

        GetTableNames()
        ResetScreen()
        GetColumns(strSourceSchema, strSourceTable, strTargetSchema, strTargetTable)
        CursorControl()
    End Sub

    Private Sub dtpEndDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpEndDate.ValueChanged
        'dtpEndDate.CustomFormat = "yyyy-MM-dd"
        WriteStatus("", 0, lblStatusText)
        chkNoEndDate.Checked = False
    End Sub

    Private Sub chkNoEndDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkNoEndDate.CheckedChanged
        WriteStatus("", 0, lblStatusText)
        If chkNoEndDate.Checked = False Then
            dtpEndDate.CustomFormat = "yyyy-MM-dd"
        Else
            dtpEndDate.CustomFormat = " "
        End If
    End Sub

    Private Sub btnSaveConfiguration_Click(sender As Object, e As EventArgs) Handles btnSaveConfiguration.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        ConfigurationSave(False)
        CursorControl()
    End Sub

    Private Sub btnSavePrimaryKey_Click(sender As Object, e As EventArgs) Handles btnSavePrimaryKey.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        ConfigurationSave(True)
        CursorControl()
    End Sub

    Private Sub ConfigurationSave(blnPrimaryKeyOnly As Boolean)
        If pnlCompareColumn.Controls.Count = 0 Then
            WriteStatus("There is no configuration to save.", 2, lblStatusText)
            Exit Sub
        End If
        Dim blnCheckFound As Boolean = False
        For Each ctrl As CheckBox In pnlCompareColumn.Controls
            If ctrl.Checked = True Then blnCheckFound = True
        Next
        Dim blnCopyFound As Boolean = False
        For Each ctrl As CheckBox In pnlCopyColumn.Controls
            If ctrl.Checked = True Then blnCopyFound = True
        Next
        If blnCheckFound = False And blnCopyFound = False And blnPrimaryKeyOnly = False Then
            WriteStatus("Nothing has been selected for copy and comparison. Nothing is saved.", 2, lblStatusText)
            Exit Sub
        End If
        'check for table dbo.SmartUpdate
        Dim strSQL As String = "Select TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'SmartUpdate'"
        Dim dtsData As DataSet = basCode.QueryDb(basCode.dhdConnection, strSQL, True, 5)
        If basCode.dhdText.DatasetCheck(dtsData) = False Then
            WriteStatus("The SmartUpdate table was not found. Please create the table first.", 2, lblStatusText)
            Exit Sub
        End If

        'get start & end date
        Dim dtmStartDate As Date = dtpStartDate.Value.Date
        Dim dtmEndDate As Date = dtpEndDate.Value.Date
        If dtmEndDate <= dtmStartDate And chkNoEndDate.Checked = False Then
            WriteStatus("End Date is smaller than Start Date. Correct the dates and try again.", 2, lblStatusText)
            Exit Sub
        End If
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
        ElseIf rbnSourceTargetConfig.Checked = True Then
            strSchemaName = strTargetSchema
            strTableName = strTargetTable
            pnlTable = pnlSourceTable
            pnlDataType = pnlSourceDataType
            pnlPrimaryKey = pnlSourcePrimaryKey
        ElseIf rbnTargetConfig.Checked = True Then
            strSchemaName = strTargetSchema
            strTableName = strTargetTable
            pnlTable = pnlTargetTable
            pnlDataType = pnlTargetDataType
            pnlPrimaryKey = pnlTargetPrimaryKey
        Else
            WriteStatus("No config save mode selected", 1, lblStatusText)
            Exit Sub
        End If

        strInsert = InsertString(strSchemaName, strTableName, pnlTable, pnlDataType, pnlPrimaryKey, pnlCopyColumn, pnlCompareColumn, dtmStartDate, dtmEndDate, blnPrimaryKeyOnly)
        strUpdate = "UPDATE dbo.SmartUpdate SET [DateStop] = '" & dtmStartDate.AddDays(-1).ToString("yyyy-MM-dd") & "' WHERE [DataBaseName] = '" & basCode.dhdConnection.DatabaseName & "' AND [SchemaName] = '" & strSchemaName & "' AND [TableName] = '" & strTableName & "' 	AND [DateStart] < '" & dtmStartDate.ToString("yyyy-MM-dd") & "' AND COALESCE([DateStop],'2999-12-31') > '" & dtmStartDate.ToString("yyyy-MM-dd") & "' AND [Active] = 1"
        If dtmEndDate = Nothing Then dtmEndDate = "2999-12-31"
        strDelete = "UPDATE dbo.SmartUpdate SET [Active] = 0 WHERE [DataBaseName] = '" & basCode.dhdConnection.DatabaseName & "' AND [SchemaName] = '" & strSchemaName & "' AND [TableName] = '" & strTableName & "' AND [DateStart] BETWEEN '" & dtmStartDate.ToString("yyyy-MM-dd") & "' AND '" & dtmEndDate.ToString("yyyy-MM-dd") & "' AND [Active] = 1"
        'strDelete = "DELETE FROM dbo.SmartUpdate WHERE [DataBaseName] = '" & dhdConnection.DatabaseName & "' AND [SchemaName] = '" & strSchemaName & "' AND [TableName] = '" & strTableName & "'"

        'get compare columns & PK columns
        If strInsert.Length < 7 Then
            WriteStatus("No config was saved. Check your settings and try again.", 1, lblStatusText)
            Exit Sub
        End If
        strInsert = strInsert.Remove(0, 6)
        strInsert = "INSERT INTO [dbo].[SmartUpdate] ([DataBaseName],[SchemaName],[TableName],[ColumnName],[DataType],[PrimaryKey],[CopyColumn],[CompareColumn],[DateStart],[DateStop],[Active]) " & Environment.NewLine & strInsert

        'save to table dbo.SmartUpdate
        Try
            basCode.QueryDb(basCode.dhdConnection, strUpdate, 0)
            basCode.QueryDb(basCode.dhdConnection, strDelete, 0)
            basCode.QueryDb(basCode.dhdConnection, strInsert, 0)
            If basCode.dhdConnection.ErrorLevel = -1 Then
                WriteStatus("There was an error saving the SmartUpdate configuration. Please check the log.", 1, lblStatusText)
                basCode.WriteLog("There was an error saving the SmartUpdate configuration. " & basCode.dhdConnection.ErrorMessage, 1)
            Else
                WriteStatus("Configuration Saved to SmartUpdate Table for: " & basCode.curStatus.Connection & "." & strSchemaName & "." & strTableName, 0, lblStatusText)
            End If
        Catch ex As Exception
            WriteStatus("There was an error saving the SmartUpdate configuration. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error saving the configuration for: " & basCode.curStatus.Connection & "." & strSchemaName & "." & strTableName & Environment.NewLine & ex.Message, 1)
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

    Private Sub chkClearTargetTable_CheckedChanged(sender As Object, e As EventArgs) Handles chkClearTargetTable.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub chkUseAllColumns_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseAllColumns.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub chkEqualizeText_CheckedChanged(sender As Object, e As EventArgs) Handles chkEqualizeText.CheckedChanged
        SmartUpdateCommand()
    End Sub

    Private Sub btnAddSmartUpdateSchedule_Click(sender As Object, e As EventArgs) Handles btnAddSmartUpdateSchedule.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If txtSmartUpdateCommand.Text.Length = 0 Then
            WriteStatus("There is no command to schedule, aborting action", 2, lblStatusText)
            Exit Sub
        End If
        'get logpath
        Dim strLogPath As String = GetDefaultLogPath(basCode.dhdConnection) & "\SmartUpdate.log"
        strLogPath = strLogPath.Replace("\\", "\")
        'get jobname
        Dim strJobName As String = GetJobName(basCode.dhdConnection, "SmartUpdate")
        If strJobName = "" Then
            WriteStatus("Job SmartUpdate was not found on the server. Create the job first with the scheduler (Settings)", 2, lblStatusText)
            Exit Sub
        End If
        'get JobStepCount
        Dim intJobStepCount As Integer = GetJobStepCount(basCode.dhdConnection, strJobName)
        If intJobStepCount = -1 Then
            WriteStatus("There was an error retrieving information about the SmartUpdate Job, aborting action", 1, lblStatusText)
            Exit Sub
        End If
        Dim intFlags As Integer = 0
        If intJobStepCount > 0 Then
            intFlags = 2
            Try
                Dim strQueryOld As String = "EXEC msdb.dbo.sp_update_jobstep @job_name = '" & strJobName & "', @step_id = " & intJobStepCount & ", @on_success_action=3;"
                basCode.QueryDb(basCode.dhdConnection, strQueryOld, 0)
            Catch ex As Exception

            End Try
        End If

        'get SqlCommand
        Dim strSQL As String = txtSmartUpdateCommand.Text.Replace("'", "''")

        'create jobstep
        strQuery = " EXEC msdb.dbo.sp_add_jobstep "
        strQuery &= " @job_name='" & strJobName & "',"
        strQuery &= " @step_name='SU_" & txtSourceTable.Text & "_" & txtTargetTable.Text & "',"
        strQuery &= " @subsystem=N'TSQL',"
        strQuery &= " @command='" & strSQL & "',"
        strQuery &= " @database_name='" & basCode.dhdConnection.DatabaseName & "',"
        strQuery &= " @output_file_name='" & strLogPath & "',"
        strQuery &= " @flags=" & intFlags & ";"

        basCode.QueryDb(basCode.dhdConnection, strQuery, 0)
        If basCode.dhdConnection.ErrorLevel = -1 Then
            WriteStatus("There was an error saving the SmartUpdate job configuration. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error saving the SmartUpdate job configuration. " & basCode.dhdConnection.ErrorMessage, 1)
        Else
            WriteStatus("Jobstep added to job: " & strJobName & " on database: " & basCode.dhdConnection.DatabaseName, 0, lblStatusText)
        End If

        CursorControl()
    End Sub

    Private Sub btnExecuteNow_Click(sender As Object, e As EventArgs) Handles btnExecuteNow.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If txtSmartUpdateCommand.Text.Length = 0 Then
            WriteStatus("There is no command to schedule, aborting action", 2, lblStatusText)
            Exit Sub
        End If

        Dim strSQL As String = txtSmartUpdateCommand.Text
        basCode.QueryDb(basCode.dhdConnection, strSQL, 0)
        If basCode.dhdConnection.ErrorLevel = -1 Then
            WriteStatus("There was an error executing the SmartUpdate command. Please check the log.", 1, lblStatusText)
            basCode.WriteLog("There was an error executing the SmartUpdate command. " & basCode.dhdConnection.ErrorMessage, 1)
        Else
            WriteStatus("SmartUpdate command executed.", 0, lblStatusText)
        End If

        CursorControl()
    End Sub

#End Region

    Private Sub LoadConnections()
        Dim lstConnections As List(Of String) = basCode.LoadConnections(basCode.xmlConnections)
        If lstConnections Is Nothing Then
            'AllClear(4)
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
        cbxConnection.SelectedItem = basCode.curStatus.Connection
    End Sub

    Private Sub LoadLinkedServers()
        Dim objData As DataSet = basCode.LoadLinkedServers(basCode.dhdConnection)
        If basCode.dhdText.DatasetCheck(objData) = False Then
            Exit Sub
        End If
        cbxLinkedServer.Items.Clear()
        cbxLinkedServer.Items.Add("")
        For intRowCount1 As Integer = 0 To objData.Tables(0).Rows.Count - 1
            Try
                cbxLinkedServer.Items.Add(objData.Tables.Item(0).Rows(intRowCount1).Item("name"))
            Catch

            End Try
        Next
    End Sub

    Private Sub LoadTables()
        CrawlTables(True, lstSourceTables)
        CrawlTables(False, lstTargetTables)
    End Sub

    Private Sub CrawlTables(blnCrawlViews As Boolean, lstTarget As ListBox)
        If CheckSqlVersion(basCode.dhdConnection) = False Then Exit Sub

        Dim lstNewTables As List(Of String) = basCode.LoadTablesList(basCode.dhdConnection, blnCrawlViews)

        If lstNewTables Is Nothing Then
            lblStatusText.Text = "No tables found"
            Exit Sub
        End If

        lstTarget.Items.Clear()
        lstTarget.Items.Add("")

        For Each TableName In lstNewTables
            lstTarget.Items.Add(TableName)
        Next

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
        strSQL &= " SELECT COALESCE(src.SchemaName,'') AS srcSchemaName "
        strSQL &= "	,COALESCE(src.colName,'') AS srcColName"
        strSQL &= "	,COALESCE(src.DataType,'') AS srcDataType"
        strSQL &= "	,COALESCE(tgt.SchemaName,'') AS tgtSchemaName "
        strSQL &= "	,COALESCE(tgt.colName,'') AS tgtColName"
        strSQL &= "	,COALESCE(tgt.DataType,'') AS tgtDataType"
        strSQL &= "	,COALESCE(src.is_identity,0) As srcIdentity"
        strSQL &= "	,COALESCE(src.pk,0) AS srcPK"
        strSQL &= "	,COALESCE(tgt.is_identity,0) As tgtIdentity"
        strSQL &= "	,COALESCE(tgt.pk,0) AS tgtPK"
        strSQL &= "	,COALESCE(src.srcValue,0) + COALESCE(tgt.tgtValue,0) AS OrderValue"
        strSQL &= "	,IIF(src.DataType = tgt.DataType,1,0) AS OrderValue2"
        strSQL &= "	,src.srcValue "
        strSQL &= "	,tgt.tgtValue"
        strSQL &= " FROM ColSource src"
        strSQL &= " FULL OUTER JOIN ColTarget tgt"
        strSQL &= "	ON src.colName = tgt.colName"
        'strSQL &= "	AND src.DataType = tgt.DataType"
        strSQL &= " ORDER BY OrderValue desc"
        strSQL &= " ,OrderValue2 desc"
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
        dtsTables = basCode.QueryDb(basCode.dhdConnection, strSQL, True, 5)
        If basCode.dhdText.DatasetCheck(dtsTables) = False Then Exit Sub
        PanelsClear()
        For intRowCount1 As Integer = 0 To dtsTables.Tables(0).Rows.Count - 1
            If dtsTables.Tables.Item(0).Rows(intRowCount1).Item("tgtSchemaName").GetType().ToString = "System.DBNull" Then
                'create source row only
                If intRowCount1 = 0 And (txtTargetTable.Text = "" Or lstTargetTables.Items.Contains(txtTargetTable.Text) = 0) Then blnSourceOnly = True
                TextFieldAdd(pnlSourceTable, dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), blnSourceOnly)
                TextFieldAdd(pnlSourceDataType, dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), blnSourceOnly)
                'SourceDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), blnSourceOnly)
                SourcePkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcIdentity"), blnSourceOnly)
                CheckColumnAdd(pnlCompareColumn, dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), basCode.CheckDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType")))
                CheckColumnAdd(pnlCopyColumn, dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), True)
                'CopyColumnAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), True)
                If blnSourceOnly = True Then
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
                TextFieldAdd(pnlSourceTable, dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), True)
                TargetFieldAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), True)
                SourceDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), True)
                TargetDataTypeAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType"), True)
                SourcePkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("srcIdentity"), True)
                TargetPkAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("tgtColName"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtPK"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtIdentity"), True)
                CheckColumnAdd(pnlCompareColumn, dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), basCode.CheckDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType")))
                'CompareColumnAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), basCode.CheckDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType")))
                'CopyColumnAdd(dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), True)
                CheckColumnAdd(pnlCopyColumn, dtsTables.Tables(0).Rows(intRowCount1).Item("srcColName"), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), basCode.CompareDataType(dtsTables.Tables(0).Rows(intRowCount1).Item("srcDataType"), dtsTables.Tables(0).Rows(intRowCount1).Item("tgtDataType")), True)
            End If

        Next

        CheckPrimaryKey(pnlSourcePrimaryKey)
        CheckPrimaryKey(pnlTargetPrimaryKey)

        Dim intPercent As Integer = pnlCompareColumn.Controls.Count / dtsTables.Tables(0).Rows.Count * 200
        If intPercent < 50 Then
            WriteStatus("Matching columns is only " & intPercent & " percent. Are you sure you have the correct tables selected?", 2, lblStatusText)
        Else
            WriteStatus("", 0, lblStatusText)
        End If
    End Sub

    Private Sub ResetScreen()
        WriteStatus("", 0, lblStatusText)
        chkCreateTargetTable.Checked = False
        chkClearTargetTable.Checked = False
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

        pnlCopyColumn.Controls.Clear()
        pnlCopyColumn.Height = 50
    End Sub

    Private Sub TextFieldAdd(pnlPanel As Panel, strFieldName As String, strDataType As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlPanel.Name & strFieldName
        txtNew.Text = strDataType
        txtNew.Tag = strDataType
        txtNew.Enabled = True
        Select Case pnlPanel.Name
            Case "PnlSource"
                txtNew.Width = 166
            Case "pnlSourceDataType"
                txtNew.Width = 117
            Case Else
                txtNew.Width = 166
        End Select
        pnlPanel.Controls.Add(txtNew)
        txtNew.Top = ((pnlPanel.Controls.Count - 1) * basCode.curVar.FieldHeight)
        If pnlPanel.Height < txtNew.Top + txtNew.Height Then pnlPanel.Height = txtNew.Top + txtNew.Height
        txtNew.Left = basCode.curVar.BuildMargin
    End Sub

    Private Sub SourceDataTypeAdd(strFieldName As String, strDataType As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlSourceDataType.Name & strFieldName
        txtNew.Text = strDataType
        txtNew.Tag = strDataType
        txtNew.Enabled = True
        txtNew.Width = 117
        pnlSourceDataType.Controls.Add(txtNew)
        txtNew.Top = ((pnlSourceTable.Controls.Count - 1) * basCode.curVar.FieldHeight)
        pnlSourceDataType.Height = pnlSourceTable.Height
        txtNew.Left = basCode.curVar.BuildMargin
    End Sub

    Private Sub SourcePkAdd(strFieldName As String, blnFieldPK As Boolean, blnFieldIdentity As Boolean, blnEnableCC As Boolean)
        Dim chkNew As New CheckBox
        chkNew.Name = pnlSourcePrimaryKey.Name & strFieldName
        chkNew.Checked = blnFieldPK
        chkNew.Tag = blnFieldIdentity
        chkNew.Enabled = True
        pnlSourcePrimaryKey.Controls.Add(chkNew)
        chkNew.Top = ((pnlSourceTable.Controls.Count - 1) * basCode.curVar.FieldHeight)
        pnlSourcePrimaryKey.Height = pnlSourceTable.Height
        chkNew.Left = basCode.curVar.BuildMargin
    End Sub

    Private Sub TargetFieldAdd(strFieldName As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlTargetTable.Name & strFieldName
        txtNew.Text = strFieldName
        txtNew.Tag = strFieldName
        txtNew.Enabled = True
        txtNew.Width = 166
        pnlTargetTable.Controls.Add(txtNew)
        txtNew.Top = ((pnlTargetTable.Controls.Count - 1) * basCode.curVar.FieldHeight)
        If pnlTargetTable.Height < txtNew.Top + txtNew.Height Then pnlTargetTable.Height = txtNew.Top + txtNew.Height
        txtNew.Left = basCode.curVar.BuildMargin

    End Sub

    Private Sub TargetDataTypeAdd(strFieldName As String, strDataType As String, blnEnableCC As Boolean)
        Dim txtNew As New TextField
        txtNew.Name = pnlTargetDataType.Name & strFieldName
        txtNew.Text = strDataType
        txtNew.Tag = strDataType
        txtNew.Enabled = True
        txtNew.Width = 117
        pnlTargetDataType.Controls.Add(txtNew)
        txtNew.Top = ((pnlTargetDataType.Controls.Count - 1) * basCode.curVar.FieldHeight)
        pnlTargetDataType.Height = pnlTargetTable.Height
        txtNew.Left = basCode.curVar.BuildMargin
    End Sub

    Private Sub TargetPkAdd(strFieldName As String, blnFieldPK As Boolean, blnFieldIdentity As Boolean, blnEnableCC As Boolean)
        Dim chkNew As New CheckBox
        chkNew.Name = pnlTargetPrimaryKey.Name & strFieldName
        chkNew.Checked = blnFieldPK
        chkNew.Tag = blnFieldIdentity
        chkNew.Enabled = True
        pnlTargetPrimaryKey.Controls.Add(chkNew)
        chkNew.Top = ((pnlTargetTable.Controls.Count - 1) * basCode.curVar.FieldHeight)
        pnlTargetPrimaryKey.Height = pnlTargetTable.Height
        chkNew.Left = basCode.curVar.BuildMargin
    End Sub

    Private Sub CheckColumnAdd(pnlPanel As Panel, strFieldName As String, blnCheckColumn As Boolean, blnTagColumn As Boolean, blnEnableColumn As Boolean)
        Dim chkNew As New CheckBox
        chkNew.Name = pnlPanel.Name & strFieldName
        chkNew.Checked = blnCheckColumn
        chkNew.Tag = blnTagColumn
        chkNew.Enabled = blnEnableColumn
        pnlPanel.Controls.Add(chkNew)
        chkNew.Top = ((pnlSourceTable.Controls.Count - 1) * basCode.curVar.FieldHeight)
        pnlPanel.Height = pnlSourceTable.Height
        chkNew.Left = basCode.curVar.BuildMargin
    End Sub

    Private Sub CopyColumnAdd(strFieldName As String, blnEnableCopy As Boolean)
        Dim chkNew As New CheckBox
        chkNew.Name = pnlCopyColumn.Name & strFieldName
        chkNew.Checked = blnEnableCopy
        chkNew.Tag = blnEnableCopy
        chkNew.Enabled = True
        pnlCopyColumn.Controls.Add(chkNew)
        chkNew.Top = ((pnlSourceTable.Controls.Count - 1) * basCode.curVar.FieldHeight)
        pnlCopyColumn.Height = pnlSourceTable.Height
        chkNew.Left = basCode.curVar.BuildMargin
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
        WriteStatus("", 0, lblStatusText)
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
            strCommand &= If(chkUseTargetCollation.Checked, 1, 0) & ", "
            strCommand &= If(chkClearTargetTable.Checked, 1, 0) & ", "
            strCommand &= If(chkUseAllColumns.Checked, 1, 0) & ", "
            strCommand &= If(chkEqualizeText.Checked, 1, 0)
        End If
        txtSmartUpdateCommand.Text = strCommand
    End Sub

    Private Function InsertString(strSchema As String, strTable As String, pnlTabel As Panel, pnlDataType As Panel, pnlPrimaryKey As Panel, pnlCopy As Panel, pnlCompare As Panel, dtmStart As Date, dtmEnd As Date, blnPrimaryKeyOnly As Boolean) As String
        Dim strQuery As String = ""
        Dim strColumnName As String = ""
        Dim strDataType As String = ""
        Dim strPrimaryKey As String = ""
        Dim strCopy As String = ""
        Dim strCompare As String = ""

        For Each ctrlColumn In pnlTabel.Controls
            'If ctrlColumn.Enabled = False Then Exit For
            strColumnName = ctrlColumn.Text
            For Each ctrlDataType In pnlDataType.Controls
                If ctrlDataType.Name = pnlDataType.Name & strColumnName Then
                    strDataType = ctrlDataType.Text
                End If
            Next
            For Each ctrlPk In pnlPrimaryKey.Controls
                If ctrlPk.Name = pnlPrimaryKey.Name & strColumnName Then
                    strPrimaryKey = ctrlPk.Checked
                End If
            Next
            For Each ctrlCopy In pnlCopy.Controls
                If ctrlCopy.Name = pnlCopy.Name & strColumnName Then
                    strCopy = ctrlCopy.Checked
                End If
            Next
            For Each ctrlCompare In pnlCompare.Controls
                If ctrlCompare.Name = pnlCompare.Name & strColumnName Then
                    strCompare = ctrlCompare.Checked
                End If
            Next
            If blnPrimaryKeyOnly = True And strPrimaryKey = "True" Then
                strQuery &= "UNION SELECT '" & basCode.dhdConnection.DatabaseName & "', '" & strSchema & "', '" & strTable & "', '" & strColumnName & "', '" & strDataType & "', '" & strPrimaryKey & "', '" & strCopy & "', '" & False & "', '" & dtmStart.ToString("yyyy-MM-dd") & "', " & If(chkNoEndDate.Checked = True, "NULL", "'" & dtmEnd.ToString("yyyy-MM-dd") & "'") & ",1" & Environment.NewLine
            ElseIf strPrimaryKey = "True" Or (strCopy = "True" And blnPrimaryKeyOnly = False) Then
                strQuery &= "UNION SELECT '" & basCode.dhdConnection.DatabaseName & "', '" & strSchema & "', '" & strTable & "', '" & strColumnName & "', '" & strDataType & "', '" & strPrimaryKey & "', '" & strCopy & "', '" & strCompare & "', '" & dtmStart.ToString("yyyy-MM-dd") & "', " & If(chkNoEndDate.Checked = True, "NULL", "'" & dtmEnd.ToString("yyyy-MM-dd") & "'") & ",1" & Environment.NewLine
            End If

        Next

        Return strQuery
    End Function


    Private Sub lblInfoLinkedServer_MouseHover(sender As Object, e As EventArgs) Handles lblInfoLinkedServer.MouseHover
        rtbInfoLinkedServer.Visible = True
        rtbInfoLinkedServer.Focus()
    End Sub

    Private Sub rtbInfoLinkedServer_LostFocus(sender As Object, e As EventArgs) Handles rtbInfoLinkedServer.LostFocus
        rtbInfoLinkedServer.Visible = False
    End Sub

    Private Sub rtbInfoLinkedServer_MouseLeave(sender As Object, e As EventArgs) Handles rtbInfoLinkedServer.MouseLeave
        rtbInfoLinkedServer.Visible = False
    End Sub

    Private Sub lblInfoSourceDatabase_MouseHover(sender As Object, e As EventArgs) Handles lblInfoSourceDatabase.MouseHover
        rtbInfoSourceDatabase.Visible = True
        rtbInfoSourceDatabase.Focus()
    End Sub

    Private Sub rtbInfoSourceDatabase_LostFocus(sender As Object, e As EventArgs) Handles rtbInfoSourceDatabase.LostFocus
        rtbInfoSourceDatabase.Visible = False
    End Sub

    Private Sub rtbInfoSourceDatabase_MouseLeave(sender As Object, e As EventArgs) Handles rtbInfoSourceDatabase.MouseLeave
        rtbInfoSourceDatabase.Visible = False
    End Sub

    Private Sub lblInfoLocalSchema_MouseHover(sender As Object, e As EventArgs) Handles lblInfoLocalSchema.MouseHover
        rtbInfoLocalSchema.Visible = True
        rtbInfoLocalSchema.Focus()
    End Sub

    Private Sub rtbInfoLocalSchema_LostFocus(sender As Object, e As EventArgs) Handles rtbInfoLocalSchema.LostFocus
        rtbInfoLocalSchema.Visible = False
    End Sub

    Private Sub rtbInfoLocalSchema_MouseLeave(sender As Object, e As EventArgs) Handles rtbInfoLocalSchema.MouseLeave
        rtbInfoLocalSchema.Visible = False
    End Sub

    Private Sub lblInfoLocalView_MouseHover(sender As Object, e As EventArgs) Handles lblInfoLocalView.MouseHover
        rtbLocalView.Visible = True
        rtbLocalView.Focus()
    End Sub

    Private Sub rtbLocalView_LostFocus(sender As Object, e As EventArgs) Handles rtbLocalView.LostFocus
        rtbLocalView.Visible = False
    End Sub

    Private Sub rtbLocalView_MouseLeave(sender As Object, e As EventArgs) Handles rtbLocalView.MouseLeave
        rtbLocalView.Visible = False
    End Sub

    Private Sub btnCreateLocalView_Click(sender As Object, e As EventArgs) Handles btnCreateLocalView.Click
        WriteStatus("", 0, lblStatusText)
        BuildView()
    End Sub

    Private Sub BuildView()
        Dim strLinkedServer As String = cbxLinkedServer.Text
        If strLinkedServer.Length > 0 And cbxLinkedServer.Items.Contains(strLinkedServer) = False Then Exit Sub
        Dim strDatabaseSource As String = txtSourceDatabase.Text
        Dim strSchemaSource As String = txtSourceSchema.Text
        Dim strTableSource As String = txtSourceTableOrView.Text
        Dim strSchemaTarget As String = txtLocalSchema.Text
        Dim strViewTarget As String = txtLocalView.Text

        If strSchemaTarget.Length = 0 Then strSchemaTarget = strSchemaSource
        If strViewTarget.Length = 0 Then strViewTarget = "vw_" & strTableSource
        If strSchemaSource.Length = 0 Or strTableSource.Length = 0 Then
            WriteStatus("Source schema and table/view are required.", 2, lblStatusText)
            Exit Sub
        End If
        If strLinkedServer.Length = 0 And strDatabaseSource.Length = 0 Then
            WriteStatus("Linked Server or Database name is required.", 2, lblStatusText)
            Exit Sub
        End If

        Dim errorlevel As Integer = basCode.CreateLocalView(basCode.dhdConnection, strLinkedServer, strDatabaseSource, strSchemaSource, strTableSource, strSchemaTarget, strViewTarget)
        'Dim strSourceQuery As String = ""
        'If strLinkedServer.Length > 0 And strDatabaseSource.Length > 0 Then
        '    strSourceQuery = "SELECT [Star1] FROM OPENQUERY([" & strLinkedServer & "],''SELECT [Star2] FROM " & strDatabaseSource & "." & strSchemaSource & "." & strTableSource & "'')"
        'ElseIf strLinkedServer.Length > 0 Then
        '    strSourceQuery = "SELECT [Star1] FROM OPENQUERY([" & strLinkedServer & "],''SELECT [Star2] FROM " & strSchemaSource & "." & strTableSource & "'')"
        'ElseIf strDatabaseSource.Length > 0 Then
        '    strSourceQuery = "SELECT [Star1] FROM " & strDatabaseSource & "." & strSchemaSource & "." & strTableSource
        'End If

        'If strSourceQuery.Length = 0 Then Exit Sub
        'Dim strInputQuery As String = strSourceQuery.Replace("[Star1]", "*").Replace("[Star2]", "TOP 0 *")

        'Dim strColumnQuery As String = "SELECT name FROM sys.dm_exec_describe_first_result_set('" & strInputQuery & "', NULL, 0) ORDER BY column_ordinal;"
        'Dim dtsData As DataSet = basCode.QueryDb(basCode.dhdConnection, strColumnQuery, True)
        'If basCode.dhdText.DatasetCheck(dtsData) = False Then
        '    WriteStatus("No results were found for this table or View. Check your settings.", 2, lblStatusText)
        '    basCode.WriteLog("No results were found for this table or View. Check your settings.", 3)
        '    Exit Sub
        'End If

        'Dim strColumns As String = ",", strColumnsSource As String = "", strColumnsTarget As String = ""
        'For intRowCount1 As Integer = 0 To dtsData.Tables(0).Rows.Count - 1
        '    If dtsData.Tables.Item(0).Rows(intRowCount1).Item("name").GetType().ToString = "System.DBNull" Then
        '        'No column name was found
        '    Else
        '        strColumns &= "," & dtsData.Tables.Item(0).Rows(intRowCount1).Item("name")
        '    End If
        'Next
        'strColumns = strColumns.Replace(",,", "")
        'If strColumns = "," Then
        '    WriteStatus("No columns were found for this table or View. Check your settings.", 2, lblStatusText)
        '    basCode.WriteLog("No columns were found for this table or View. Check your settings.", 3)
        '    Exit Sub
        'End If

        'Dim strViewQuery As String = ""
        'strColumnsSource = """" & strColumns.Replace(",", """,""") & """"
        'strColumnsTarget = "[" & strColumns.Replace(",", "],[") & "]"

        'If strColumnsSource.Length > 7000 And strLinkedServer.Length > 0 Then
        '    strColumnsSource = "*"
        'End If

        'strViewQuery = strSourceQuery.Replace("[Star1]", strColumnsTarget).Replace("[Star2]", strColumnsSource)

        'Dim strCheckViewQuery As String = "SELECT name FROM sys.dm_exec_describe_first_result_set('" & strViewQuery & "', NULL, 0);"
        'Dim dtsCheckView As DataSet = basCode.QueryDb(basCode.dhdConnection, strCheckViewQuery, True)
        'If basCode.dhdText.DatasetCheck(dtsCheckView) = False Then
        '    strViewQuery = strSourceQuery.Replace("[Star1]", "*").Replace("[Star2]", "*")
        'Else
        '    For intRowCount1 As Integer = 0 To dtsCheckView.Tables(0).Rows.Count - 1
        '        If dtsCheckView.Tables.Item(0).Rows(intRowCount1).Item("name").GetType().ToString = "System.DBNull" Then
        '            'The query does not produce any results, revert to former query
        '            strViewQuery = strSourceQuery.Replace("[Star1]", "*").Replace("[Star2]", "*")
        '        End If
        '    Next
        'End If

        ''Create the View
        'Dim strBuildViewQuery As String = "CREATE VIEW [" & strSchemaTarget & "].[" & strViewTarget & "] AS " & strViewQuery.Replace("''", "'")
        'basCode.QueryDb(basCode.dhdConnection, strBuildViewQuery, False)
        If errorlevel <> 0 Then
            WriteStatus(basCode.ErrorMessage, 1, lblStatusText)
        Else
            WriteStatus("View " & strSchemaTarget & "." & strViewTarget & " created", 0, lblStatusText)
        End If

        lstSourceTables.Items.Add(strSchemaTarget & "." & strViewTarget)
        lstSourceTables.SelectedItem = strSchemaTarget & "." & strViewTarget
        txtTargetTable.Text = strSchemaTarget & "." & strTableSource

        GetTableNames()
        ResetScreen()
        GetColumns(strSourceSchema, strSourceTable, strTargetSchema, strTargetTable)

    End Sub

End Class