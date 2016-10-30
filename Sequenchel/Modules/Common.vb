Imports System.Xml
Imports System.Text
Imports System.Collections.Specialized
Imports System.Data.Sql
Imports System.Security.Cryptography
Imports System.Environment.SpecialFolder

Module Common
    Friend ErrorMode As Boolean = False
    Friend strErrorMessage As String = ""
    Friend strQuery As String = Nothing

    Friend Sub FieldEnableHandler(sender As Object, blnEnabled As Boolean)
        Select Case sender.Field.Category
            Case 1
                If blnEnabled = True Then
                    sender.ReadOnly = False
                    sender.BackColor = clrOriginal
                Else
                    sender.ReadOnly = True
                    sender.BackColor = clrDisabled
                End If
            Case 2, 3, 4
                sender.Enabled = blnEnabled
            Case 5, 6
                sender.Enabled = blnEnabled
                If blnEnabled = True Then
                    sender.BackColor = clrOriginal
                Else
                    sender.BackColor = clrDisabled
                End If
        End Select
    End Sub

    Friend Function FieldEnabledCheck(sender As Object) As Boolean
        Dim blnReturn As Boolean = False
        Select Case sender.Field.Category
            Case 1
                If sender.ReadOnly = False Then
                    blnReturn = True
                Else
                    blnReturn = False
                End If
            Case 2, 3, 4, 5, 6
                blnReturn = sender.Enabled
            Case Else
                blnReturn = False
        End Select
        Return blnReturn
    End Function

    Friend Function GetSaveFileName(strFileName As String) As String
        If basCode.curVar.IncludeDate = True Then
            strFileName = strFileName & "_" & basCode.FormatFileDate(Now)
        End If

        Dim sfdFile As New SaveFileDialog
        sfdFile.FileName = strFileName
        sfdFile.Filter = "Excel 2007 file (*.xlsx)|*.xlsx|Excel 2007 Text file(*.xlsx)|*.xlsx|XML File (*.xml)|*.xml|Text File (*.csv, *.txt)|*.csv;*.txt"
        sfdFile.FilterIndex = 1
        sfdFile.RestoreDirectory = True
        sfdFile.OverwritePrompt = True

        If (sfdFile.ShowDialog() <> DialogResult.OK) Then
            Return Nothing
        End If

        Dim strTargetFile As String = sfdFile.FileName
        Select Case sfdFile.FilterIndex
            Case 1
                basCode.curVar.ConvertToText = False
                basCode.curVar.ConvertToNull = False
            Case 2
                basCode.curVar.ConvertToText = True
                basCode.curVar.ConvertToNull = False
            Case 3
                basCode.curVar.ConvertToText = True
                basCode.curVar.ConvertToNull = False
            Case 4
                basCode.curVar.ConvertToText = True
                basCode.curVar.ConvertToNull = False
            Case Else
                'unknown selection, do not try to save or show file
                basCode.curVar.ShowFile = False
                Return Nothing
        End Select

        Return strTargetFile
    End Function

    Friend Function ExportFile(dtsInput As DataSet, strFileName As String) As Boolean
        Try
            If basCode.ExportFile(dtsInput, strFileName, basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull, basCode.curVar.ShowFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter, basCode.curVar.QuoteValues, basCode.curVar.CreateDir) = False Then
                Return False
            End If
        Catch ex As Exception
            basCode.curVar.ShowFile = False
            basCode.WriteLog("An error occured while creating the file." & Environment.NewLine & ex.Message, 1)
            Return False
        End Try
        Return True
    End Function

#Region "XML"
    Friend Sub DisplayXmlFile(ByVal xmlDoc As Xml.XmlDocument, ByVal tvw As TreeView)
        tvw.Nodes.Clear()
        DisplayXmlNode(xmlDoc, tvw.Nodes)
        tvw.Nodes(0).Expand()
    End Sub

    Friend Sub DisplayXmlNode(ByVal xmlnode As XmlNode, ByVal nodes As TreeNodeCollection)
        ' Add a TreeView node for this XmlNode.
        ' (Using the node's Name is OK for most XmlNode types.)
        If xmlnode Is Nothing Then Exit Sub
        Dim strDisplayName As String = xmlnode.Name
        If strDisplayName = "Field" Then strDisplayName &= " " & xmlnode.Item("FldAlias").InnerText
        Dim tvNode As TreeNode = nodes.Add(xmlnode.Name, strDisplayName)
        Select Case xmlnode.NodeType
            Case XmlNodeType.Element
                '	' This is an element: Check whether there are attributes.
                If xmlnode.Attributes.Count > 0 Then
                    '		' Create an ATTRIBUTES node.
                    Dim attrNode As TreeNode = tvNode.Nodes.Add("(ATTRIBUTES)")
                    '		' Add all the attributes as children of the new node.
                    Dim xmlAttr As XmlAttribute
                    For Each xmlAttr In xmlnode.Attributes
                        '			' Each node shows name and value.
                        attrNode.Nodes.Add(xmlAttr.Name & " = '" & xmlAttr.Value & _
                           "'")
                    Next
                End If
            Case XmlNodeType.Text, XmlNodeType.CDATA
                ' For these node types we display the value
                tvNode.Text = xmlnode.Value
            Case XmlNodeType.Comment
                tvNode.Text = "<!--" & xmlnode.Value & "-->"
            Case XmlNodeType.ProcessingInstruction, XmlNodeType.XmlDeclaration
                tvNode.Text = "<?" & xmlnode.Name & " " & xmlnode.Value & "?>"
            Case Else
                ' ignore other node types.
        End Select

        Dim xmlChild As XmlNode = xmlnode.FirstChild
        Do Until xmlChild Is Nothing
            DisplayXmlNode(xmlChild, tvNode.Nodes)
            xmlChild = xmlChild.NextSibling
        Loop
    End Sub

    Friend Function SaveXmlFile(xmlDoc As XmlDocument, FilePathName As String, CreateDir As Boolean) As Boolean
        If basCode.dhdText.CheckDir(basCode.dhdText.PathConvert(basCode.CheckFilePath(FilePathName)).Substring(0, basCode.dhdText.PathConvert(basCode.CheckFilePath(FilePathName)).LastIndexOf("\")), False) = False Then
            If CreateDir = True Then
                If MessageBox.Show("The folder " & basCode.dhdText.PathConvert(basCode.CheckFilePath(FilePathName)).Substring(0, basCode.dhdText.PathConvert(basCode.CheckFilePath(FilePathName)).LastIndexOf("\")) & " does not exist." & Environment.NewLine & "do you wish to create it?", "Folder does not exist", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                    Return False
                End If
            Else
                Return False
            End If
        End If
        Try
            basCode.dhdText.SaveXmlFile(xmlDoc, basCode.dhdText.PathConvert(basCode.CheckFilePath(FilePathName)), True)
        Catch ex As Exception
            basCode.WriteLog("There was an error saving the XML file: " & basCode.dhdText.PathConvert(basCode.CheckFilePath(FilePathName)) & Environment.NewLine & ex.Message, 1)
            Return False
        End Try
        Return True
    End Function

#End Region

#Region "Database"

    Friend Function DatabaseTest(dhdConnect As DataHandler.db) As Boolean
        If basCode.curVar.DebugMode Then
            MessageBox.Show("Sequenchel v " & Application.ProductVersion & " Database Settings" & Environment.NewLine _
             & "   DatabaseServer = " & dhdConnect.DataLocation & Environment.NewLine _
             & "   Database Name = " & dhdConnect.DatabaseName & Environment.NewLine _
             & "   DataProvider = " & dhdConnect.DataProvider & Environment.NewLine _
             & "   LoginMethod = " & dhdConnect.LoginMethod & Environment.NewLine _
             & "   LoginName = " & dhdConnect.LoginName & Environment.NewLine _
             & "   Password = " & dhdConnect.Password & Environment.NewLine _
             & "   Running in Debug Mode")
        End If
        Try
            dhdConnect.CheckDB()
            Return dhdConnect.DataBaseOnline
        Catch ex As Exception
            MessageBox.Show("While testing the database connection, the following error occured: " & Environment.NewLine & ex.Message)
            Return False
        End Try
    End Function

    Friend Function CheckSqlVersion(dhdConnect As DataHandler.db) As Boolean
        Try
            Dim intSqlVersion As Integer = basCode.GetSqlVersion(dhdConnect)
            Select Case intSqlVersion
                Case 0
                    MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings" & Environment.NewLine & "Error: " & dhdConnect.ErrorMessage)
                    basCode.WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings" & Environment.NewLine & "Error: " & dhdConnect.ErrorMessage, 1)
                    Return False
                Case 7, 8
                    MessageBox.Show("SQL Server 2000 or older is not supported")
                    basCode.WriteLog("SQL Server 2000 or older is not supported", 1)
                    Return False
                Case 9, 10, 11, 12, 13, 14
                    basCode.WriteLog("SQL Version " & intSqlVersion & " detected.", 3)
                    'Just do it
                    Return True
                Case Else
                    MessageBox.Show("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion)
                    basCode.WriteLog("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion, 1)
                    Return False
            End Select
        Catch ex As Exception
            MessageBox.Show("SQL Server not found or not accessible." & Environment.NewLine & "Please check the log.")
            basCode.WriteLog("SQL Server not found or not accessible." & Environment.NewLine & "Please check your settings." & Environment.NewLine & ex.Message, 1)
            Return False
        End Try
    End Function

    Friend Sub BackupDatabase(ByVal dhdConnect As DataHandler.db, ByVal strPath As String)

        Dim strDateTime As String
        'strDateTime = FormatDateTime(Now()).Replace(":", "").Replace(" ", "_").Replace("-", "").Replace("/", "")
        strDateTime = Now.ToString("yyyyMMdd_HHmmss")
        strQuery = ""
        strQuery = "exec usp_BackupHandle 'CREATE','" & dhdConnect.DatabaseName & "','" & strPath & "','" & strDateTime & "'"
        If basCode.curVar.DebugMode Then MessageBox.Show("DatabaseName = " & dhdConnect.DatabaseName & Environment.NewLine & _
                                            "strPath = " & strPath & Environment.NewLine & _
                                            "strDateTime = " & strDateTime & Environment.NewLine & _
                                            strQuery)
        Try
            basCode.QueryDb(dhdConnect, strQuery, False)
        Catch ex As Exception
            basCode.dhdText.LogLocation = ""
            basCode.WriteLog(ex.Message, 1)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Friend Function ClearDBLog(ByVal dtmDate As Date) As Boolean
        strQuery = ""
        strQuery = "exec usp_LoggingHandle 'Del','" & basCode.FormatDate(dtmDate) & "'"

        Try
            basCode.QueryDb(basCode.dhdMainDB, strQuery, False)
        Catch ex As Exception
            basCode.dhdText.LogLocation = ""
            basCode.WriteLog("Error deleting old logs: " & ex.Message, 1)
            Return False
        End Try
        Return True
    End Function

    Friend Function ScheduleCreate(JobName As String, SqlCommand As String, FreqType As Integer, FreqInterval As Integer, FreqSubType As Integer, FreqSubTypeInt As Integer, StartTime As Integer, EndTime As Integer, OutputPath As String) As Boolean
        CursorControl("Wait")

        Dim strStartDate As String = Now.ToString("yyyyMMdd")
        If OutputPath.LastIndexOf("\") = OutputPath.Length - 1 Then OutputPath = OutputPath.Substring(0, OutputPath.Length - 1)

        strQuery = "EXECUTE [dbo].[usp_ScheduleCreate] "
        strQuery &= "@JobName = '" & JobName & "'"
        strQuery &= ",@SqlCommand = '" & SqlCommand & "'"
        strQuery &= ",@DatabaseName = '" & basCode.dhdMainDB.DatabaseName & "'"
        strQuery &= ",@OutputPath = '" & OutputPath & "'"
        strQuery &= ",@FreqType = " & FreqType
        strQuery &= ",@FreqInterval = " & FreqInterval
        strQuery &= ",@FreqSubType = " & FreqSubType
        strQuery &= ",@FreqSubTypeInt = " & FreqSubTypeInt
        strQuery &= ",@StartDate = " & strStartDate
        strQuery &= ",@StartTime = " & StartTime
        strQuery &= ",@EndTime = " & EndTime

        basCode.QueryDb(basCode.dhdMainDB, strQuery, False)
        If basCode.dhdMainDB.ErrorLevel = -1 Then
            Return False
        End If
        Return True
    End Function

    Friend Function GetDefaultLogPath(dhdConnect As DataHandler.db) As String
        strQuery = "SELECT coalesce(serverproperty('InstanceDefaultLogPath'),LEFT(physical_name,LEN(physical_name) - charindex('\',reverse(physical_name)))) AS InstanceDefaultLogPath FROM sys.master_files where name = 'mastlog' "

        Dim objData As DataSet
        objData = basCode.QueryDb(dhdConnect, strQuery, True)
        Dim strReturn As String = ""

        If basCode.dhdText.DatasetCheck(objData) = False Then
            strReturn = ""
        Else
            strReturn = objData.Tables.Item(0).Rows(0).Item("InstanceDefaultLogPath")
        End If

        Return strReturn
    End Function

    Friend Function GetJobName(dhdConnect As DataHandler.db, strPattern As String) As String

        strQuery = "select [name] AS JobName from msdb.dbo.sysjobs where name like '%" & strPattern & "%'"

        Dim objData As DataSet
        objData = basCode.QueryDb(dhdConnect, strQuery, True)
        Dim strReturn As String = ""

        If basCode.dhdText.DatasetCheck(objData) = False Then
            strReturn = ""
        Else
            strReturn = objData.Tables.Item(0).Rows(0).Item("JobName")
        End If

        Return strReturn
    End Function

    Friend Function GetJobStepCount(dhdConnect As DataHandler.db, strJobName As String) As Integer
        Dim intReturn As Integer = 0
        Try
            strQuery = "select COUNT(*) AS StepsCount FROM [msdb].[dbo].[sysjobs] AS [job] INNER JOIN [msdb].[dbo].[sysjobsteps] AS [stp] ON [job].[job_id] = [stp].[job_id] WHERE [job].name = 'Sequenchel SmartUpdate'"
            Dim objData As DataSet
            objData = basCode.QueryDb(dhdConnect, strQuery, True)

            If basCode.dhdText.DatasetCheck(objData) = False Then
                intReturn = -1
            Else
                intReturn = objData.Tables.Item(0).Rows(0).Item("StepsCount")
            End If
        Catch ex As Exception
            basCode.WriteLog("Error retrieving Job Information" & Environment.NewLine & ex.Message, 1)
            Return -1
        End Try

        Return intReturn
    End Function

#End Region

#Region "Formatting"

    Friend Sub CursorControl(Optional strStyle As String = "Default")
        ShutdownDelay()
        Select Case strStyle
            Case "Default"
                Cursor.Current = Cursors.Default
            Case "Wait"
                Cursor.Current = Cursors.WaitCursor
            Case Else
                Cursor.Current = Cursors.Default
        End Select
    End Sub

    Friend Sub SetBackColor(ctrl As Control)
        Select Case TypeName(ctrl)
            Case "TextBox", "ComboBox"
                If ctrl.Text Is Nothing Then
                    ctrl.BackColor = clrOriginal
                    Exit Sub
                End If

                If ctrl.Tag Is Nothing Then
                    If ctrl.Text = "" Then
                        ctrl.BackColor = clrOriginal
                    ElseIf ctrl.Text.Length = 0 Then
                        ctrl.BackColor = clrOriginal
                    Else
                        ctrl.BackColor = clrMarked
                    End If
                ElseIf ctrl.Text.ToString = ctrl.Tag.ToString Then
                    ctrl.BackColor = clrOriginal
                Else
                    ctrl.BackColor = clrMarked
                End If
            Case "CheckBox"
                Dim chk As CheckBox = TryCast(ctrl, CheckBox)
                If chk IsNot Nothing Then
                    If chk.Tag Is Nothing Then
                        If chk.Checked = 0 Then
                            chk.BackColor = clrControl
                        Else
                            chk.BackColor = clrMarked
                        End If
                    ElseIf chk.Checked.ToString = chk.Tag.ToString Then
                        chk.BackColor = clrControl
                    Else
                        chk.BackColor = clrMarked
                    End If
                End If
        End Select

    End Sub
#End Region

    Friend Function DataSet2DataGridView(dtsSource As DataSet, SourceTable As Integer, dgvTarget As DataGridView, RebuildColumns As Boolean, intPage As Integer) As Boolean
        Dim blnSucces As Boolean = False

        If RebuildColumns = True Then
            For Each colColumn As DataColumn In dtsSource.Tables(SourceTable).Columns
                Dim dgvColumn As New DataGridViewTextBoxColumn
                dgvColumn.Name = colColumn.ColumnName
                dgvColumn.HeaderText = colColumn.ColumnName
                dgvColumn.ValueType = colColumn.DataType
                dgvColumn.Visible = True
                dgvTarget.Columns.Add(dgvColumn)
            Next
        End If

        Dim intTotal As Integer = dtsSource.Tables(SourceTable).Rows.Count
        Dim intStart As Integer = 0, intStop As Integer = 0
        If intPage > 0 Then
            intStart = intPage * 1000 - 999
            If intStart > intTotal Then intStart = (Math.Floor(intTotal / 1000)) * 1000 + 1
            intStop = intPage * 1000
            If intStop > intTotal Then intStop = intTotal
        Else
            intStart = 1
            intStop = intTotal
        End If

        For intRowCount1 As Integer = intStart - 1 To intStop - 1
            Dim dgvRow As New DataGridViewRow
            dgvRow.CreateCells(dgvTarget)
            For Each dgvColumn As DataGridViewTextBoxColumn In dgvTarget.Columns
                If dtsSource.Tables.Item(0).Rows(intRowCount1).Item(dgvColumn.HeaderText).GetType().ToString = "System.DBNull" Then
                    dgvRow.Cells.Item(dgvTarget.Columns.Item(dgvColumn.Name).Index).Style.BackColor = clrEmpty
                Else
                    dgvRow.Cells.Item(dgvTarget.Columns.Item(dgvColumn.Name).Index).Value = dtsSource.Tables(SourceTable).Rows(intRowCount1).Item(dgvColumn.HeaderText)
                End If
            Next
            dgvTarget.Rows.Add(dgvRow)
        Next
        'If RebuildColumns = True Then DataGridViewColumnSize(dgvTarget)
        blnSucces = True
        Return blnSucces
    End Function

    Friend Sub DataGridViewColumnSize(dgvTarget As DataGridView)
        'grd.DataSource = DT

        'set autosize mode
        For Each dgvColumn As DataGridViewTextBoxColumn In dgvTarget.Columns
            dgvColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
        'dgvTarget.Columns(dgvTarget.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        'datagrid has calculated it's widths so we can store them
        For i = 0 To dgvTarget.Columns.Count - 1
            'store autosized widths
            Dim colw As Integer = dgvTarget.Columns(i).Width
            If colw > basCode.curVar.MaxColumnWidth Then colw = basCode.curVar.MaxColumnWidth
            'remove autosizing
            dgvTarget.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            'set width to calculated by autosize
            dgvTarget.Columns(i).Width = colw
        Next
    End Sub

    Friend Sub SetDefaultText(objTextBox As TextBox)
        If objTextBox.Text = "" Then
            objTextBox.Text = objTextBox.Tag
            objTextBox.ForeColor = Color.Gray
        End If
    End Sub

    Friend Sub RemoveDefaultText(objTextBox As TextBox)
        If objTextBox.Text = objTextBox.Tag Then
            objTextBox.Text = ""
            objTextBox.ForeColor = SystemColors.WindowText
        End If
    End Sub

    Friend Sub PasswordCharSet(objTextBox As TextBox)
        If objTextBox.Text.Length = 0 Then
            objTextBox.PasswordChar = "*"
        ElseIf objTextBox.Text = objTextBox.Tag Then
            objTextBox.PasswordChar = Nothing
        Else
            objTextBox.PasswordChar = "*"
        End If
    End Sub

End Module
