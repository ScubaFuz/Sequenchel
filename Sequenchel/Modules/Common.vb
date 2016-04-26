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

    Friend Core As New SeqCore.Core
    Friend Excel As New SeqCore.Excel
    Friend SeqData As New SeqCore.Data

    Friend xmlDoc As New XmlDocument
    Friend xmlSDBASettings As New XmlDocument
    Friend xmlGeneralSettings As New XmlDocument
    Friend xmlConnections As New XmlDocument
    Friend xmlTableSets As New XmlDocument
    Friend xmlTables As New XmlDocument
    Friend xmlReports As New XmlDocument
    Friend xmlSearch As New XmlDocument

    Friend tblTable As New Table
    Friend arrLabels As New LabelArray
    Friend dtsTable As New DataSet
    Friend dtsReport As New DataSet
    Friend dtsImport As New DataSet

    Friend clrOriginal As Color = System.Drawing.SystemColors.Window
    Friend clrControl As Color = System.Drawing.SystemColors.Control
    Friend clrDisabled As Color = System.Drawing.SystemColors.ControlLight
    Friend clrMarked As Color = System.Drawing.Color.LightSkyBlue
    Friend clrWarning As Color = System.Drawing.Color.IndianRed
    Friend clrEmpty As Color = System.Drawing.Color.LemonChiffon

    Friend strReport As String = "Sequenchel " & vbTab & " version: " & Core.GetVersion("B") & "  Licensed by: " & Core.LicenseName
    Friend dtmElapsedTime As DateTime
    Friend tmsElapsedTime As TimeSpan
    Friend tmrShutdown As New Timers.Timer

    Private _SelectedItem As DataGridViewRow

    Public Property SelectedItem() As System.Windows.Forms.DataGridViewRow
        Get
            Return _SelectedItem
        End Get
        Set(ByVal Value As System.Windows.Forms.DataGridViewRow)
            _SelectedItem = Value
        End Set
    End Property

    Friend Sub ParseCommands()
        Dim intLength As Integer = 0

        For Each Command As String In My.Application.CommandLineArgs
            Dim intPosition As Integer = Command.IndexOf(":")
            If intPosition < 0 Then intPosition = Command.Length
            Dim strCommand As String = Command.ToLower.Substring(0, intPosition)
            Select Case strCommand
                Case "/silent"
                    'Start wihtout any windows / forms
                Case "/debug"
                    SeqData.curVar.DebugMode = True
                Case "/control"
                    SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
                Case "/dev"
                    SeqData.curVar.DevMode = True
                Case "/noencryption"
                    SeqData.curVar.Encryption = False
                Case "/securityoverride"
                    If Command.Length > intPosition + 1 Then
                        SeqData.curVar.OverridePassword = SeqData.dhdText.MD5Encrypt(Command.Substring(intPosition + 1, Command.Length - (intPosition + 1)))
                    End If
                Case "/report"
                    'Open Report window directly
                Case "/connection"
                    'Use the chosen connection
                Case "/tableset"
                    'Use the chosen TableSet
                Case "/table"
                    'Use the chosen Table
                Case "/reportname"
                    'Open the report if found
                Case "/exportfile"
                    'Export the report to the chosen file
                Case "/import"
                    'open the Import window
                Case "/importfile"
                    'Export the report to the chosen file
            End Select
        Next

    End Sub

    Friend Sub SetTimer()
        AddHandler tmrShutdown.Elapsed, AddressOf TimedShutdown
        Dim intShutdown As Integer = SeqData.curVar.TimedShutdown
        If intShutdown > 60000 Then intShutdown -= 60000
        If intShutdown > 0 Then tmrShutdown.Interval = intShutdown
        If intShutdown <= 60000 Then
            tmrShutdown.Enabled = False
        ElseIf intShutdown > 0 Then
            tmrShutdown.Enabled = True
            tmrShutdown.Start()
        End If
    End Sub

    Friend Sub ShutdownDelay()
        If tmrShutdown.Enabled = False And tmrShutdown.Interval > 60000 Then
            tmrShutdown.Enabled = True
            tmrShutdown.Start()
        ElseIf tmrShutdown.Enabled = True And tmrShutdown.Interval > 60000 Then
            tmrShutdown.Stop()
            tmrShutdown.Enabled = False
            tmrShutdown.Enabled = True
            tmrShutdown.Start()
        Else
            tmrShutdown.Enabled = False
        End If
    End Sub

    Friend Sub TimedShutdown()
        ShutdownDelay()
        ShowShutdownForm()
    End Sub

    Friend Sub ShowShutdownForm()
        Application.Run(frmShutdown)
    End Sub

    Friend Sub LoadLicense(lblTarget As Label)
        If Core.LoadLicense = False Then
            WriteStatus(Core.Message.ErrorMessage, 1, lblTarget)
        Else
            WriteStatus("License loaded succesfully", 2, lblTarget)
        End If
        strReport = "Sequenchel " & vbTab & " version: " & Core.GetVersion("B") & vbTab & "  Licensed to: " & Core.LicenseName

    End Sub

    Public Sub FieldTextHandler(sender As Object)
        If SeqData.curStatus.SuspendActions = False Then
            Select Case sender.FieldCategory
                Case 1
                    If sender.Text <> sender.Tag.ToString Then
                        sender.BackColor = clrMarked
                    Else
                        If sender.ReadOnly = False Then
                            sender.BackColor = clrOriginal
                        Else
                            sender.BackColor = clrDisabled
                        End If
                    End If
                Case 2
                    If sender.Checked.ToString <> sender.Tag.ToString Then
                        sender.BackColor = clrMarked
                    Else
                        If sender.Enabled = True Then
                            sender.BackColor = clrControl
                        Else
                            sender.BackColor = clrDisabled
                        End If
                    End If
                    'Case 3
                    '    If tblTable.TableName = sender.Name.Substring(0, sender.Name.LastIndexOf(".")) Then
                    '        If sender.SelectedItem Is Nothing Then
                    '            LoadLookupList(sender, True, False)
                    '            If sender.Text.ToString <> sender.Tag.ToString Then
                    '                sender.BackColor = clrMarked
                    '            Else
                    '                If sender.Enabled = True Then
                    '                    sender.BackColor = clrOriginal
                    '                Else
                    '                    sender.BackColor = clrDisabled
                    '                End If
                    '            End If
                    '        Else
                    '            sender.DropDownStyle = ComboBoxStyle.DropDown
                    '            If sender.SelectedItem.ToString <> sender.Tag.ToString And sender.Text.ToString <> sender.Tag.ToString Then
                    '                sender.BackColor = clrMarked
                    '            Else
                    '                If sender.Enabled = True Then
                    '                    sender.BackColor = clrOriginal
                    '                Else
                    '                    sender.BackColor = clrDisabled
                    '                End If
                    '            End If
                    '        End If
                    '    End If
                    'Case 4
                    '    'related Lookup with default list
                    '    If sender.SelectedItem Is Nothing Then
                    '        LoadLookupList(sender, True, False)
                    '    Else
                    '        sender.DropDownStyle = ComboBoxStyle.DropDown

                    '    End If

                Case 5
                    'ManagedSelectField 
                    For intField As Integer = 0 To tblTable.Count - 1
                        If tblTable.Item(intField).Name = sender.Name Then
                            If sender.Text.ToString <> sender.Tag.ToString Then
                                sender.BackColor = clrMarked
                            Else
                                If sender.Enabled = True Then
                                    sender.BackColor = clrOriginal
                                Else
                                    sender.BackColor = clrDisabled
                                End If
                            End If
                        End If
                    Next
                Case 6
                    'ManagedSelectField as related field
                    For intField As Integer = 0 To tblTable.Count - 1
                        If tblTable.Item(intField).FieldName = sender.FieldRelatedField Then
                            Select Case tblTable.Item(intField).FieldCategory
                                Case 1, 3, 4, 5
                                    tblTable.Item(intField).Text = sender.Value
                                Case 2
                                    tblTable.Item(intField).Checked = sender.Value
                            End Select
                        End If
                    Next
            End Select
        End If
    End Sub

    Friend Sub FieldEnableHandler(sender As Object, blnEnabled As Boolean)
        Select Case sender.FieldCategory
            Case 1
                If blnEnabled = True Then
                    sender.ReadOnly = False
                    sender.BackColor = clrOriginal
                Else
                    sender.ReadOnly = True
                    sender.BackColor = clrDisabled
                End If
            Case 2, 3, 4, 5, 6
                sender.Enabled = blnEnabled
        End Select
    End Sub

    Friend Function FieldEnabledCheck(sender As Object) As Boolean
        Dim blnReturn As Boolean = False
        Select Case sender.FieldCategory
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

    Public Sub LabelClickHandler(sender As Object)
        Dim lblTemp As Label = sender
        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).Name = lblTemp.Name.Substring(3, lblTemp.Name.Length - 3) Then
                If tblTable.Item(intField).Enabled = True Then
                    If tblTable.Item(intField).BackColor = clrMarked Then
                        If tblTable.Item(intField).FieldDataType = "BIT" Then
                            tblTable.Item(intField).backColor = clrControl
                        Else
                            tblTable.Item(intField).backColor = clrOriginal
                        End If
                    Else
                        tblTable.Item(intField).BackColor = clrMarked
                    End If
                End If
            End If
        Next
    End Sub

    Friend Sub TableClear()
        tblTable.Clear()
        tblTable.TableName = ""
        tblTable.TableAlias = ""
        tblTable.TableVisible = False
        tblTable.TableSearch = False
        tblTable.TableUpdate = False
        tblTable.TableInsert = False
        tblTable.TableDelete = False
    End Sub

    Friend Function GetSaveFileName(strFileName As String) As String
        If SeqData.curVar.IncludeDate = True Then
            strFileName = strFileName & "_" & SeqData.FormatFileDate(Now)
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
                SeqData.curVar.ConvertToText = False
                SeqData.curVar.ConvertToNull = False
            Case 2
                SeqData.curVar.ConvertToText = True
                SeqData.curVar.ConvertToNull = False
            Case 3
                SeqData.curVar.ConvertToText = True
                SeqData.curVar.ConvertToNull = False
            Case 4
                SeqData.curVar.ConvertToText = True
                SeqData.curVar.ConvertToNull = False
            Case Else
                'unknown selection, do not try to save or show file
                SeqData.curVar.ShowFile = False
                Return Nothing
        End Select

        Return strTargetFile
    End Function

    Friend Function ExportFile(dtsInput As DataSet, strFileName As String) As Boolean
        CursorControl("Wait")
        Try
            SeqData.ExportFile(dtsInput, strFileName, SeqData.curVar.ConvertToText, SeqData.curVar.ConvertToNull, SeqData.curVar.ShowFile, SeqData.curVar.HasHeaders, SeqData.curVar.Delimiter, SeqData.curVar.QuoteValues, SeqData.curVar.CreateDir)
        Catch ex As Exception
            SeqData.curVar.ShowFile = False
            SeqData.WriteLog("An error occured while creating the file." & Environment.NewLine & ex.Message, 1)
            Return False
        End Try
        CursorControl()
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
        Dim tvNode As TreeNode = nodes.Add(xmlnode.Name, xmlnode.Name)
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
        If SeqData.dhdText.CheckDir(SeqData.dhdText.PathConvert(SeqData.CheckFilePath(FilePathName)).Substring(0, SeqData.dhdText.PathConvert(SeqData.CheckFilePath(FilePathName)).LastIndexOf("\")), False) = False Then
            If CreateDir = True Then
                If MessageBox.Show("The folder " & SeqData.dhdText.PathConvert(SeqData.CheckFilePath(FilePathName)).Substring(0, SeqData.dhdText.PathConvert(SeqData.CheckFilePath(FilePathName)).LastIndexOf("\")) & " does not exist." & Environment.NewLine & "do you wish to create it?", "Folder does not exist", MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1) = DialogResult.No Then
                    Return False
                End If
            Else
                Return False
            End If
        End If
        Try
            SeqData.dhdText.SaveXmlFile(xmlDoc, SeqData.dhdText.PathConvert(SeqData.CheckFilePath(FilePathName)), True)
        Catch ex As Exception
            SeqData.WriteLog("There was an error saving the XML file: " & SeqData.dhdText.PathConvert(SeqData.CheckFilePath(FilePathName)) & Environment.NewLine & ex.Message, 1)
            Return False
        End Try
        Return True
    End Function


#End Region

#Region "Database"

    Friend Function DatabaseTest(dhdConnect As DataHandler.db) As Boolean
        If SeqData.curVar.DebugMode Then
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
            Dim intSqlVersion As Integer = SeqData.GetSqlVersion(dhdConnect)
            Select Case intSqlVersion
                Case 0
                    MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
                    SeqData.WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
                    Return False
                Case 7, 8
                    MessageBox.Show("SQL Server 2000 or older is not supported")
                    SeqData.WriteLog("SQL Server 2000 or older is not supported", 1)
                    Return False
                Case 9, 10, 11, 12, 13, 14
                    SeqData.WriteLog("SQL Version " & intSqlVersion & " detected.", 3)
                    'Just do it
                    Return True
                Case Else
                    MessageBox.Show("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion)
                    SeqData.WriteLog("SQL Server version is not recognised" & Environment.NewLine & "Version detected = " & intSqlVersion, 1)
                    Return False
            End Select
        Catch ex As Exception
            MessageBox.Show("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings")
            SeqData.WriteLog("SQL Server not found or not accessible" & Environment.NewLine & "Please check your settings", 1)
            Return False
        End Try
    End Function

    Friend Sub BackupDatabase(ByVal dhdConnect As DataHandler.db, ByVal strPath As String)

        Dim strDateTime As String
        'strDateTime = FormatDateTime(Now()).Replace(":", "").Replace(" ", "_").Replace("-", "").Replace("/", "")
        strDateTime = Now.ToString("yyyyMMdd_HHmmss")
        strQuery = ""
        strQuery = "exec usp_BackupHandle 'CREATE','" & dhdConnect.DatabaseName & "','" & strPath & "','" & strDateTime & "'"
        If SeqData.curVar.DebugMode Then MessageBox.Show("DatabaseName = " & dhdConnect.DatabaseName & Environment.NewLine & _
                                            "strPath = " & strPath & Environment.NewLine & _
                                            "strDateTime = " & strDateTime & Environment.NewLine & _
                                            strQuery)
        Try
            SeqData.QueryDb(dhdConnect, strQuery, False)
        Catch ex As Exception
            SeqData.dhdText.LogLocation = ""
            SeqData.WriteLog(ex.Message, 1)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Friend Sub SaveConfigSetting(ByVal strCategory As String, ByVal strConfigName As String, ByVal strConfigValue As String, Optional ByVal strRemarks As String = Nothing)
        strQuery = "exec usp_ConfigHandle 'Smt','" & strCategory & "',NULL,'" & strConfigName & "','" & strConfigValue & "','" & Now.ToString("yyyyMMdd HH:mm") & "'"
        If strRemarks = Nothing Then
            'Do nothing
        Else
            strQuery &= ",'" & strRemarks & "'"
        End If
        SeqData.QueryDb(SeqData.dhdMainDB, strQuery, False)
    End Sub

    Friend Function LoadConfigSetting(ByVal strCategory As String, ByVal strConfigName As String) As String
        strQuery = "exec usp_ConfigHandle 'Get','" & strCategory & "',NULL,'" & strConfigName & "'"
        Dim objData As DataSet
        objData = SeqData.QueryDb(SeqData.dhdMainDB, strQuery, True)
        Dim strReturn As String = Nothing

        If objData Is Nothing Then
            'blnDatabaseOnLine = False
            Return ""
        End If
        If objData.Tables.Count = 0 Then Return ""
        If objData.Tables(0).Rows.Count = 0 Then Return ""

        For intRowCount = 0 To objData.Tables(0).Rows.Count - 1
            'If objData.Tables.Item(0).Rows(intRowCount).Item(0).GetType().ToString = "System.DBNull" Then
            'MessageBox.Show("Cell Must be empty")
            'Else
            If objData.Tables.Item(0).Rows(intRowCount).Item("Category") = strCategory Then
                If objData.Tables.Item(0).Rows(intRowCount).Item("ConfigName") = strConfigName Then
                    strReturn = objData.Tables.Item(0).Rows(intRowCount).Item("ConfigValue")
                End If
            End If
            'End If
        Next
        Return strReturn
    End Function

    Friend Function ClearDBLog(ByVal dtmDate As Date) As Boolean
        strQuery = ""
        strQuery = "exec usp_LoggingHandle 'Del','" & FormatDate(dtmDate) & "'"

        Try
            SeqData.QueryDb(SeqData.dhdMainDB, strQuery, False)
        Catch ex As Exception
            SeqData.dhdText.LogLocation = ""
            SeqData.WriteLog("Error deleting old logs: " & ex.Message, 1)
            Return False
        End Try
        Return True
    End Function

    Friend Function LoadTablesList(ByVal dhdConnect As DataHandler.db, Optional blnCrawlViews As Boolean = True) As List(Of String)
        strQuery = "SELECT "
        strQuery &= " sch.[name] + '.' + "
        strQuery &= " tbl.[name] AS TableName FROM sys.tables tbl"
        strQuery &= " INNER JOIN sys.schemas sch"
        strQuery &= " ON tbl.[schema_id] = sch.[schema_id]"
        If blnCrawlViews = True Then
            strQuery &= " UNION SELECT "
            strQuery &= " sch.[name] + '.' + "
            strQuery &= " vw.[name] AS TableName FROM sys.views vw"
            strQuery &= " INNER JOIN sys.schemas sch"
            strQuery &= " ON vw.[schema_id] = sch.[schema_id]"
        End If
        strQuery &= " ORDER BY TableName"

        CursorControl("Wait")

        Dim dtsData As DataSet = SeqData.QueryDb(dhdConnect, strQuery, True)
        If SeqData.dhdText.DatasetCheck(dtsData) = False Then Return Nothing

        Dim ReturnValue As New List(Of String)
        For intRowCount = 0 To dtsData.Tables(0).Rows.Count - 1
            ReturnValue.Add(dtsData.Tables.Item(0).Rows(intRowCount).Item("TableName"))
        Next
        Return ReturnValue
    End Function

    Friend Function ScheduleCreate(JobName As String, SqlCommand As String, FreqType As Integer, FreqInterval As Integer, FreqSubType As Integer, FreqSubTypeInt As Integer, StartTime As Integer, EndTime As Integer, OutputPath As String) As Boolean
        CursorControl("Wait")

        Dim strStartDate As String = Now.ToString("yyyyMMdd")
        If OutputPath.LastIndexOf("\") = OutputPath.Length - 1 Then OutputPath = OutputPath.Substring(0, OutputPath.Length - 1)

        strQuery = "EXECUTE [dbo].[usp_ScheduleCreate] "
        strQuery &= "@JobName = '" & JobName & "'"
        strQuery &= ",@SqlCommand = '" & SqlCommand & "'"
        strQuery &= ",@DatabaseName = '" & SeqData.dhdMainDB.DatabaseName & "'"
        strQuery &= ",@OutputPath = '" & OutputPath & "'"
        strQuery &= ",@FreqType = " & FreqType
        strQuery &= ",@FreqInterval = " & FreqInterval
        strQuery &= ",@FreqSubType = " & FreqSubType
        strQuery &= ",@FreqSubTypeInt = " & FreqSubTypeInt
        strQuery &= ",@StartDate = " & strStartDate
        strQuery &= ",@StartTime = " & StartTime
        strQuery &= ",@EndTime = " & EndTime

        SeqData.QueryDb(SeqData.dhdMainDB, strQuery, False)
        If SeqData.dhdMainDB.ErrorLevel = -1 Then
            Return False
        End If
        Return True
    End Function

    Friend Function GetDefaultLogPath(dhdConnect As DataHandler.db) As String
        strQuery = "SELECT coalesce(serverproperty('InstanceDefaultLogPath'),LEFT(physical_name,LEN(physical_name) - charindex('\',reverse(physical_name)))) AS InstanceDefaultLogPath FROM sys.master_files where name = 'mastlog' "

        Dim objData As DataSet
        objData = SeqData.QueryDb(dhdConnect, strQuery, True)
        Dim strReturn As String = ""

        If SeqData.dhdText.DatasetCheck(objData) = False Then
            strReturn = ""
        Else
            strReturn = objData.Tables.Item(0).Rows(0).Item("InstanceDefaultLogPath")
        End If

        Return strReturn
    End Function

    Friend Function GetJobName(dhdConnect As DataHandler.db, strPattern As String) As String

        strQuery = "select [name] AS JobName from msdb.dbo.sysjobs where name like '%" & strPattern & "%'"

        Dim objData As DataSet
        objData = SeqData.QueryDb(dhdConnect, strQuery, True)
        Dim strReturn As String = ""

        If SeqData.dhdText.DatasetCheck(objData) = False Then
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
            objData = SeqData.QueryDb(dhdConnect, strQuery, True)

            If SeqData.dhdText.DatasetCheck(objData) = False Then
                intReturn = -1
            Else
                intReturn = objData.Tables.Item(0).Rows(0).Item("StepsCount")
            End If
        Catch ex As Exception
            SeqData.WriteLog("Error retrieving Job Information" & Environment.NewLine & ex.Message, 1)
            Return -1
        End Try

        Return intReturn
    End Function

#End Region

#Region "DataTypes"

    Friend Function FormatFieldWhere(strFieldName As String, strTableName As String, strFieldWidth As String, strFieldType As String, strFieldValue As String) As String
        Dim strOutput As String = ""
        Select Case strFieldType.ToUpper
            Case "CHAR"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],'') = '') "
                Else
                    strOutput = " ([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "] IN ('" & Replace(strFieldValue, ",", "','") & "') OR [" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] LIKE '%" & strFieldValue & "%')"
                End If
            Case "INTEGER"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],0) = 0) "
                Else
                    strOutput = " ([" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] IN (" & strFieldValue & ") OR [" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] LIKE '%" & strFieldValue & "%')"
                End If
            Case "DATETIME"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],0) = 0) "
                Else
                    strOutput = " ((CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "], " & SeqData.curVar.DateTimeStyle & ")) IN ('" & strFieldValue.Replace(",", "','") & "') OR (CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "], " & SeqData.curVar.DateTimeStyle & ")) LIKE '%" & strFieldValue & "%')"
                End If
            Case "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],'') = '') "
                Else
                    strOutput = " ((CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "])) IN (" & strFieldValue.Replace(",", "','") & ") OR (CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "])) LIKE '%" & strFieldValue & "%')"
                End If
            Case "BIT"
                If strFieldValue = 0 Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],0) = 0) "
                Else
                    strOutput = " [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "] = " & strFieldValue
                End If
            Case "IMAGE"
                'do nothing. cannot search on an image data type.
            Case Else
                'try the default CHAR action
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],'') = '') "
                Else
                    strOutput = " ([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "] IN ('" & Replace(strFieldValue, ",", "','") & "') OR [" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] LIKE '%" & strFieldValue & "%')"
                End If
        End Select
        Return strOutput
    End Function

    Friend Function FormatFieldWhere1(strFieldName As String, strTableName As String, strFieldWidth As String, strFieldType As String, strFieldValue As String) As String
        Dim strOutput As String = ""
        Dim strTableField As String = " [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "]"

        If strFieldValue = "NULL" Or strFieldValue = "" Then
            Select Case strFieldType.ToUpper
                Case "CHAR", "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                    strOutput = " (COALESCE(" & strTableField & ",'') = '') " & Environment.NewLine
                Case "INTEGER", "DATETIME", "BIT"
                    strOutput = " (COALESCE(" & strTableField & ",0) = 0) " & Environment.NewLine
                Case "IMAGE"
                    'do nothing. cannot search on an image data type.
                Case Else
                    'try the default CHAR action
                    strOutput = " (COALESCE(" & strTableField & ",'') = '') " & Environment.NewLine
            End Select
            Return strOutput
        Else
            If strFieldValue.Contains(",") Then
                strFieldValue = strFieldValue.Trim(",")
                If strFieldValue.Length = 0 Then Return strOutput
                Select Case strFieldType.ToUpper
                    Case "CHAR", "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                        strOutput = " (" & strTableField & " IN ('" & Replace(strFieldValue, ",", "','") & "'))" & Environment.NewLine
                    Case "INTEGER", "BIT"
                        strOutput = " (" & strTableField & " IN (" & strFieldValue & "))" & Environment.NewLine
                    Case "DATETIME"
                        strOutput = " ((CONVERT([nvarchar](" & strFieldWidth & "), " & strTableField & ", " & SeqData.curVar.DateTimeStyle & ")) IN ('" & strFieldValue.Replace(",", "','") & "'))" & Environment.NewLine
                    Case "IMAGE"
                        'do nothing. cannot search on an image data type.
                    Case Else
                        'try the default CHAR action
                        strOutput = " (" & strTableField & " IN ('" & Replace(strFieldValue, ",", "','") & "'))" & Environment.NewLine
                End Select
                Return strOutput
            Else
                If strFieldValue.Trim().Contains(" ") Then
                    Dim strArgs As String() = strFieldValue.Trim().Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                    For Each strArg As String In strArgs
                        If strArg IsNot Nothing AndAlso strArg.Trim().Length > 0 Then
                            strOutput &= " AND (" & strTableField & " LIKE ('%" & strArg.Trim() & "%'))" & Environment.NewLine
                        End If
                    Next
                    Dim strTest As String = strOutput.Substring(0, 4)
                    If strOutput.Substring(0, 4) = " AND" Then
                        'if the value starts with AND, remove it.
                        strOutput = strOutput.Remove(0, 4)
                    End If
                    Return strOutput
                Else

                    Select Case strFieldType.ToUpper
                        Case "CHAR"
                            strOutput = " (" & strTableField & " LIKE '%" & strFieldValue & "%')"
                        Case "INTEGER"
                            strOutput = " (" & strTableField & " LIKE '%" & strFieldValue & "%')"
                        Case "DATETIME"
                            strOutput = " (CONVERT([nvarchar](" & strFieldWidth & "), " & strTableField & ", " & SeqData.curVar.DateTimeStyle & ")) LIKE '%" & strFieldValue & "%')"
                        Case "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                            strOutput = " (CONVERT([nvarchar](" & strFieldWidth & "), " & strTableField & ")) LIKE '%" & strFieldValue & "%')"
                        Case "BIT"
                            strOutput = " (COALESCE(" & strTableField & ",0) = " & strFieldValue & ") "
                        Case "IMAGE"
                            'do nothing. cannot search on an image data type.
                        Case Else
                            'try the default CHAR action
                            strOutput = " (" & strTableField & " LIKE '%" & strFieldValue & "%')"
                    End Select
                    Return strOutput

                End If
            End If
        End If

        Return strOutput
    End Function

#End Region

    Friend Sub WriteStatus(strStatusText As String, intStatusLevel As Integer, lblTarget As Label)
        Dim clrStatus As Color = clrControl
        Select Case intStatusLevel
            Case 0
                clrStatus = clrControl
            Case 1
                clrStatus = clrWarning
            Case 2
                clrStatus = clrMarked
            Case 3, 4, 5
                clrStatus = clrControl
        End Select
        lblTarget.Text = strStatusText
        lblTarget.BackColor = clrStatus
    End Sub

    Friend Sub MasterPanelControlsDispose(pnlMaster As Panel, Optional strTag As String = "")
        For Each pnlTarget In pnlMaster.Controls
            If pnlTarget.Name.Substring(0, 3) = "pnl" Then
                PanelControlsDispose(pnlTarget, strTag)
            End If
        Next
    End Sub

    Friend Sub PanelControlsDispose(pnlTarget As Panel, Optional strTag As String = "")
        For intCount As Integer = 1 To pnlTarget.Controls.Count
            If pnlTarget.Controls.Count > 0 Then
                For Each ctrDispose In pnlTarget.Controls
                    'MessageBox.Show(ctrDispose.Name)
                    If ctrDispose.Tag Is Nothing Then
                        ctrDispose.Dispose()
                    ElseIf strTag.ToString = "" Then
                        ctrDispose.Dispose()
                    Else
                        If strTag.ToString = ctrDispose.Tag.ToString Then
                            ctrDispose.Dispose()
                        End If
                    End If
                Next
            Else
                Exit Sub
            End If
        Next
        pnlTarget.Height = 30
    End Sub

#Region "Formatting"

    Friend Function FormatDate(ByVal dtmInput As Date) As String
        If dtmInput = Nothing Then
            FormatDate = ""
        Else
            FormatDate = dtmInput.ToString("yyyy-MM-dd")
        End If
    End Function

    Friend Function FormatDateTime(ByVal dtmInput As Date) As String
        If dtmInput = Nothing Then
            FormatDateTime = ""
        Else
            FormatDateTime = dtmInput.ToString("yyyyMMdd_HHmm")
        End If
    End Function

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

    Friend Function DataSet2DataGridView(dtsSource As DataSet, SourceTable As Integer, dgvTarget As DataGridView, RebuildColumns As Boolean) As Boolean
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

        For intRowCount1 As Integer = 0 To dtsSource.Tables(SourceTable).Rows.Count - 1
            Dim dgvRow As New DataGridViewRow
            dgvRow.CreateCells(dgvTarget)
            For Each dgvColumn As DataGridViewTextBoxColumn In dgvTarget.Columns
                If dtsSource.Tables.Item(0).Rows(intRowCount1).Item(dgvColumn.Name).GetType().ToString = "System.DBNull" Then
                    dgvRow.Cells.Item(dgvTarget.Columns.Item(dgvColumn.Name).Index).Style.BackColor = clrEmpty
                Else
                    dgvRow.Cells.Item(dgvTarget.Columns.Item(dgvColumn.Name).Index).Value = dtsSource.Tables(SourceTable).Rows(intRowCount1).Item(dgvColumn.Name)
                End If
            Next
            dgvTarget.Rows.Add(dgvRow)
        Next
        If RebuildColumns = True Then DataGridViewColumnSize(dgvTarget)
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
            If colw > SeqData.curVar.MaxColumnWidth Then colw = SeqData.curVar.MaxColumnWidth
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
