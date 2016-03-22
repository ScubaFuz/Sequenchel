Imports System.Xml

Public Class Data

    Public dhdReg As New DataHandler.reg
    Public dhdText As New DataHandler.txt
    Public dhdConnection As New DataHandler.db
    Public curVar As New Variables
    Public curStatus As New CurrentStatus
    Public Excel As New Excel

#Region "DataBase"
    Public Function QueryDb(ByVal dhdConnect As DataHandler.db, ByVal strQueryData As String, ByVal ReturnValue As Boolean, Optional ByVal LogLevel As Integer = 5) As DataSet
        'WriteLog(strQueryData, LogLevel)
        'strErrorMessage = ""
        dhdConnect.CheckDB()
        If dhdConnect.DataBaseOnline = False Then
            'MessageBox.Show("The database was not found." & Environment.NewLine & "Please check your settings")
            'strErrorMessage = "The database was not found. Please check your settings"
            Return Nothing
        End If
        Dim dtsData As DataSet
        Try
            dtsData = dhdConnect.QueryDatabase(strQueryData, ReturnValue)
            Return dtsData
        Catch ex As Exception
            'WriteLog(ex.Message, 1)
            'strErrorMessage = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetSqlVersion(dhdConnect As DataHandler.db) As Integer
        Dim strDatabase As String = dhdConnect.DatabaseName
        dhdConnect.DatabaseName = "master"
        Dim intSqlVersion As Integer = dhdConnect.GetSqlVersion()
        dhdConnect.DatabaseName = strDatabase

        Return intSqlVersion
    End Function

    Public Function GetDataType(strInput As String) As String
        Select Case strInput
            Case "varchar"
                Return "CHAR"
            Case "char"
                Return "CHAR"
            Case "nvarchar"
                Return "CHAR"
            Case "nchar"
                Return "CHAR"
            Case "bit"
                Return "BIT"
            Case "tinyint"
                Return "INTEGER"
            Case "smallint"
                Return "INTEGER"
            Case "int"
                Return "INTEGER"
            Case "bigint"
                Return "INTEGER"
            Case "date"
                Return "DATETIME"
            Case "time"
                Return "TIME"
            Case "datetime"
                Return "DATETIME"
            Case "smalldatetime"
                Return "DATETIME"
            Case "datetime2"
                Return "DATETIME"
            Case "datetimeoffset"
                Return "CHAR"
            Case "timestamp"
                Return "TIMESTAMP"
            Case "decimal"
                Return "INTEGER"
            Case "numeric"
                Return "INTEGER"
            Case "real"
                Return "INTEGER"
            Case "float"
                Return "INTEGER"
            Case "smallmoney"
                Return "INTEGER"
            Case "money"
                Return "INTEGER"
            Case "uniqueidentifier"
                Return "GUID"
            Case "image"
                Return "IMAGE"
            Case "sql_variant"
                Return "BINARY"
            Case "hierarchyid"
                Return "CHAR"
            Case "geometry"
                Return "GEO"
            Case "geography"
                Return "GEO"
            Case "varbinary"
                Return "BINARY"
            Case "binary"
                Return "BINARY"
            Case "text"
                Return "TEXT"
            Case "ntext"
                Return "TEXT"
            Case "xml"
                Return "XML"
            Case "sysname"
                Return "CHAR"
        End Select
        Return ""
    End Function

    Public Function GetDataTypes()
        Dim ReturnValue As New List(Of String)
        ReturnValue.Add("CHAR")
        ReturnValue.Add("INTEGER")
        ReturnValue.Add("BIT")
        ReturnValue.Add("BINARY")
        ReturnValue.Add("GUID")
        ReturnValue.Add("IMAGE")
        ReturnValue.Add("TEXT")
        ReturnValue.Add("DATETIME")
        ReturnValue.Add("TIME")
        ReturnValue.Add("TIMESTAMP")
        ReturnValue.Add("XML")
        ReturnValue.Add("GEO")
        Return ReturnValue
    End Function

    Public Function SetDelimiters(strInput As String, strDataType As String, strCompare As String, Optional strShowMode As String = Nothing) As String
        If strInput.Length > 2 Then
            If strInput.Substring(0, 2) = "f:" Then
                Return "(" & strInput.Replace("f:", "") & ")"
            End If
            If strInput.Substring(0, 2) = "v:" Then
                strInput = strInput.Replace("v:", "")
                strInput = ProcessDefaultValue(strInput)
            End If
        End If
        If strCompare = "IS" Or strCompare = "IS NOT" Then
            Return strInput
        End If
        If strShowMode = "COUNT" Or _
            strShowMode = "SUM" Or _
            strShowMode = "AVG" Then
            strDataType = "INTEGER"
        End If
        If strDataType = "CHAR" Or _
                strDataType = "BINARY" Or _
                strDataType = "GUID" Or _
                strDataType = "TEXT" Or _
                strDataType = "IMAGE" Or _
                strDataType = "DATETIME" Or _
                strDataType = "TIME" Or _
                strDataType = "TIMESTAMP" Or _
                strDataType = "XML" Or _
                strDataType = "GEO" Then
            strInput = strInput.Replace("'", "''")
            strInput = "N'" & strInput & "'"
            If (strCompare = "IN" Or strCompare = "NOT IN") Then
                strInput = strInput.Replace(",", "','")
            End If
        End If
        If (strCompare = "IN" Or strCompare = "NOT IN") Then
            strInput = "(" & strInput & ")"
        End If
        Return strInput
    End Function

    Public Function GetWidth(strDataType As String, intMaxLength As Integer) As Integer
        Select Case strDataType
            Case "CHAR", "BINARY"
                If intMaxLength < 50 Then
                    Return 50
                ElseIf intMaxLength < 100 Then
                    Return 100
                ElseIf intMaxLength < 150 Then
                    Return 150
                Else
                    Return 200
                End If
            Case "INTEGER"
                Return intMaxLength * 10
            Case "BIT"
                Return 25
            Case "GUID", "XML", "TEXT", "IMAGE"
                Return 200
            Case "TIMESTAMP", "GEO", "DATETIME", "TIME"
                Return 100
            Case Else
                Return 50
        End Select
    End Function

    Public Function CompareDataType(strDataType As String) As Boolean
        Select Case strDataType.ToLower
            Case "text", "ntext", "image"
                Return False
            Case Else
                Return True
        End Select
    End Function

    Public Function ProcessDefaultValue(strValue As String) As String
        Dim strReturn As String = strValue
        Dim intFirstBracket As Integer = strValue.IndexOf("(")
        Dim intLastBracket As Integer = strValue.LastIndexOf(")")
        Dim strInput As String = ""

        If intFirstBracket > 0 And intLastBracket > 0 Then

            If intLastBracket - intFirstBracket > 1 Then
                strInput = strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1))
            End If

            Select Case strValue.Substring(0, strValue.IndexOf("(") + 1).ToLower
                Case "now("
                    Dim dtmOutput As DateTime = DateTime.Now
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "HOUR")
                    Catch ex As Exception
                    End Try
                    strReturn = dtmOutput.ToString("yyyy-MM-dd HH:mm:ss")
                Case "date("
                    Dim dtmOutput As DateTime = Date.Today
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "DAY")
                    Catch ex As Exception
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "time("
                    Dim dtmOutput As DateTime = DateTime.Now
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "MINUTE")
                        strReturn = dtmOutput.ToString("HH:mm:ss")
                    Catch ex As Exception
                        strReturn = TimeOfDay.ToString("HH:mm:ss")
                    End Try
                Case "yearstart("
                    Dim dtmOutput As New DateTime(DateTime.Now.Year, 1, 1)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "YEAR")
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "yearend("
                    Dim dtmOutput As New DateTime(DateTime.Now.Year, 1, 1)
                    dtmOutput = dtmOutput.AddYears(1).AddDays(-1)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "YEAR")
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "monthstart("
                    Dim dtmOutput As DateTime = Date.Today.AddDays(-Date.Today.Day + 1)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "MONTH")
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "monthend("
                    Dim dtmOutput As DateTime = Date.Today.AddMonths(1).AddDays(-Date.Today.Day)
                    Dim IntAddMonths As Integer = 1

                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "MONTH")
                        dtmOutput = dtmOutput.AddMonths(1).AddDays(-dtmOutput.Day)
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)

                Case "weekstart("
                    Dim IntAddWeeks As Integer = 0
                    Dim dtmOutput As DateTime = Date.Today
                    Dim dayIndex As Integer = dtmOutput.DayOfWeek
                    If dayIndex < DayOfWeek.Monday Then
                        dayIndex += 7 'Monday is first day of week, no day of week should have a smaller index
                    End If
                    Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
                    dtmOutput = dtmOutput.AddDays(-dayDiff)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "WEEK")
                    Catch ex As Exception
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "weekend("
                    Dim IntAddWeeks As Integer = 0
                    Dim dtmOutput As DateTime = Date.Today
                    Dim dayIndex As Integer = dtmOutput.DayOfWeek
                    If dayIndex < DayOfWeek.Monday Then
                        dayIndex += 7 'Monday is first day of week, no day of week should have a smaller index
                    End If
                    Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
                    dtmOutput = dtmOutput.AddDays(-dayDiff).AddDays(6)

                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "WEEK")
                    Catch ex As Exception
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "pi("
                    strReturn = "3.1415926535897932384626433832795"
                Case Else
                    'nothing
            End Select
        End If
        Return strReturn
    End Function

    Public Function AlterDate(dtmInput As DateTime, strInput As String, strDefault As String) As DateTime
        Dim intComma As Integer = 0
        Dim strStep As String = ""
        Dim intStep As Integer = 0

        strInput = strInput.Trim(",")

        If strInput.Contains(",") Then
            Try
                intComma = strInput.IndexOf(",")
            Catch ex As Exception
                'WriteLog("An error occured processing the date enhancer" & strInput & ": " & ex.Message, 1)
            End Try
        End If

        If intComma > 0 Then
            Try
                strStep = strInput.Substring(0, intComma)
                intStep = strInput.Substring(intComma + 1, strInput.Length - (intComma + 1))
            Catch ex As Exception
                'WriteLog("Unable to get the identifiers for the date enhancer" & strInput & ": " & ex.Message, 1)
            End Try
        End If

        If IsNumeric(strInput) Then
            intStep = strInput
            strStep = strDefault
        End If

        Try
            If intStep <> 0 Then
                Select Case strStep.ToUpper
                    Case "MILLISECOND"
                        dtmInput = dtmInput.AddMilliseconds(intStep)
                    Case "SECOND"
                        dtmInput = dtmInput.AddSeconds(intStep)
                    Case "MINUTE"
                        dtmInput = dtmInput.AddMinutes(intStep)
                    Case "HOUR"
                        dtmInput = dtmInput.AddHours(intStep)
                    Case "DAY"
                        dtmInput = dtmInput.AddDays(intStep)
                    Case "WEEK"
                        dtmInput = dtmInput.AddDays(intStep * 7)
                    Case "MONTH"
                        dtmInput = dtmInput.AddMonths(intStep)
                    Case "YEAR"
                        dtmInput = dtmInput.AddYears(intStep)
                End Select
            End If
        Catch ex As Exception
            'WriteLog("An error occured applying the date enhancer" & strInput & ": " & ex.Message, 1)
        End Try

        Return dtmInput
    End Function

    Public Function FormatFileDate(ByVal dtmInput As Date, Optional strFormatStyle As String = Nothing) As String
        If dtmInput = Nothing Then
            FormatFileDate = ""
        Else
            If strFormatStyle = Nothing Then strFormatStyle = 120
            FormatFileDate = dtmInput.ToString("yyyy-MM-dd")
            Select Case strFormatStyle
                Case 120
                    FormatFileDate = dtmInput.ToString("yyyy-MM-dd")
                Case 100
                    FormatFileDate = dtmInput.ToString("MMM dd yyyy")
                Case 101
                    FormatFileDate = dtmInput.ToString("MM/dd/yyyy")
                Case 105
                    FormatFileDate = dtmInput.ToString("dd-MM-yyyy")
                Case 109
                    FormatFileDate = dtmInput.ToString("MMM dd yyyy")
                Case 113
                    FormatFileDate = dtmInput.ToString("dd MMM yyyy")
                Case Else
                    FormatFileDate = dtmInput.ToString("yyyy-MM-dd")
            End Select
        End If
        Return FormatFileDate
    End Function

    Public Function SaveToDatabase(dtsInput As DataSet, Optional ConvertToText As Boolean = False) As Integer
        Dim intRecordsAffected As Integer = 0
        Dim intReturn As Integer = 0

        Try
            For Each Table As DataTable In dtsInput.Tables
                Dim blnExport As Boolean = True
                If Table.ExtendedProperties.Count > 0 Then
                    If Table.ExtendedProperties.ContainsKey("ExportTable") = True Then
                        If Table.ExtendedProperties("ExportTable") = "False" Then
                            blnExport = False
                        End If
                    End If
                End If
                If blnExport = True Then
                    If ConvertToText = True Then Table = dhdConnection.ConvertToText(Table)
                    intReturn = UploadSqlData(Table)
                    If intReturn = -1 Then
                        Return -1
                    Else
                        intRecordsAffected += intReturn
                    End If
                End If
            Next
            'intRecordsAffected = dhdDB.UploadSqlData(dgvImport.DataSource)
            Return intRecordsAffected
        Catch ex As Exception
            dhdConnection.dbMessage = "Export to database failed. Check if the columns match and try again. If you are importing more than 1 table, make sure they have identical columns" & ex.Message
            Return -1
        End Try
        Return intRecordsAffected
    End Function

    Public Function UploadSqlData(ByVal objDataTable As DataTable) As Integer
        Dim intRecordsAffected As Integer = 0
        Dim bcp As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(dhdConnection.SqlConnection)
        If dhdConnection.SqlConnection.State = ConnectionState.Closed Then dhdConnection.SqlConnection.Open()
        bcp.DestinationTableName = dhdConnection.DataTableName
        Dim reader As DataTableReader = objDataTable.CreateDataReader()
        intRecordsAffected = objDataTable.Rows.Count
        Try
            bcp.WriteToServer(reader)
        Catch ex As Exception
            dhdConnection.ErrorMessage = ex.Message
            dhdConnection.ErrorLevel = -1
            intRecordsAffected = -1
        End Try
        'bcp.Close()
        Try
            If dhdConnection.SqlConnection.State = ConnectionState.Open Then dhdConnection.SqlConnection.Close()
        Catch ex As Exception
        End Try
        Return intRecordsAffected
    End Function
#End Region

#Region "XML"
    Public Function LoadSDBASettingsXml(xmlLoadDoc As XmlDocument) As String
        If dhdText.CheckFile(System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile) = True Then
            'LoadXmlFile
            Try
                xmlLoadDoc.Load(System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile)
                If dhdText.CheckElement(xmlLoadDoc, "DefaultConfigFilePath") Then curVar.DefaultConfigFilePath = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("DefaultConfigFilePath").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SettingsFile") Then curVar.GeneralSettings = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("SettingsFile").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowSettingsChange") Then curVar.AllowSettingsChange = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowSettingsChange").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowConfigurationChange") Then curVar.AllowConfiguration = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowConfigurationChange").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowLinkedServersChange") Then curVar.AllowLinkedServers = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowLinkedServersChange").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowReportQueryEdit") Then curVar.AllowQueryEdit = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowReportQueryEdit").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowDataImport") Then curVar.AllowDataImport = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowDataImport").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowSmartUpdate") Then curVar.AllowSmartUpdate = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowSmartUpdate").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowUpdate") Then curVar.AllowUpdate = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowUpdate").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowInsert") Then curVar.AllowInsert = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowInsert").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowDelete") Then curVar.AllowDelete = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowDelete").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "OverridePassword") Then
                    If xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("OverridePassword").InnerText.Length > 0 Then
                        If curVar.OverridePassword = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("OverridePassword").InnerText Then
                            curVar.SecurityOverride = True
                        End If
                    End If
                End If
            Catch ex As Exception
                Return "There was an error reading the XML file. Please check the file" & Environment.NewLine & System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile & Environment.NewLine & Environment.NewLine & ex.Message
                'WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & Application.StartupPath & "\" & dhdText.InputFile & Environment.NewLine & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            Return "The XML file was not found. Please check the file" & Environment.NewLine & System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile
        End If
        Return ""
    End Function

    Public Function LoadGeneralSettingsXml(xmlLoadDoc As XmlDocument) As String
        If dhdText.CheckFile(CheckFilePath(curVar.GeneralSettings)) = True Then
            'LoadXmlFile
            Try
                xmlLoadDoc.Load(dhdText.PathConvert(CheckFilePath(curVar.GeneralSettings)))
                'If dhdText.CheckElement(xmlLoadDoc, "DataLocation") Then dhdDatabase.DataLocation = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("DataLocation").InnerText
                'If dhdText.CheckElement(xmlLoadDoc, "DatabaseName") Then dhdDatabase.DatabaseName = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("DatabaseName").InnerText
                'If dhdText.CheckElement(xmlLoadDoc, "DataProvider") Then dhdDatabase.DataProvider = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("DataProvider").InnerText
                'If dhdText.CheckElement(xmlLoadDoc, "LoginMethod") Then dhdDatabase.LoginMethod = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("LoginMethod").InnerText
                'If dhdText.CheckElement(xmlLoadDoc, "LoginName") Then dhdDatabase.LoginName = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("LoginName").InnerText
                'If dhdText.CheckElement(xmlLoadDoc, "Password") Then dhdDatabase.Password = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("Password").InnerText

                If dhdText.CheckElement(xmlLoadDoc, "LogFileName") Then dhdText.LogFileName = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("LogFileName").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LogLevel") Then dhdText.LogLevel = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("LogLevel").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LogLocation") Then dhdText.LogLocation = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("LogLocation").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "Retenion") Then dhdText.Retenion = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("Retenion").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AutoDelete") Then dhdText.AutoDelete = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("AutoDelete").InnerText

                If dhdText.CheckElement(xmlLoadDoc, "Connections") Then curVar.ConnectionsFile = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("Connections").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "UsersetLocation") Then curVar.UsersetLocation = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("UsersetLocation").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LimitLookupLists") Then curVar.LimitLookupLists = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("LimitLookupLists").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LimitLookupListsCount") Then curVar.LimitLookupListsCount = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("LimitLookupListsCount").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "DateTimeStyle") Then curVar.DateTimeStyle = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("DateTimeStyle").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "IncludeDate") Then curVar.IncludeDate = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("IncludeDate").InnerText

            Catch ex As Exception
                Return "There was an error reading the XML file. Please check the file" & Environment.NewLine & curVar.GeneralSettings & Environment.NewLine & ex.Message
                'WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & CurVar.GeneralSettings & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            Return "The XML file was not found. Please check the file" & Environment.NewLine & curVar.GeneralSettings
        End If
        Return ""
    End Function

    Public Function CheckFilePath(strFilePathName As String) As String
        If strFilePathName.Contains("\") Then
            Return strFilePathName
        ElseIf CurVar.DefaultConfigFilePath.Length > 0 Then
            Return CurVar.DefaultConfigFilePath & "\" & strFilePathName
        Else
            Return System.AppDomain.CurrentDomain.BaseDirectory & strFilePathName
        End If
    End Function

    Public Function LoadConnectionsXml(xmlConnections As XmlDocument) As List(Of String)
        If dhdText.CheckFile(CheckFilePath(curVar.ConnectionsFile)) = True Then
            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlConnections.Load(dhdText.PathConvert(CheckFilePath(curVar.ConnectionsFile)))
                Dim blnConnectionExists As Boolean = False
                curVar.ConnectionDefault = ""

                Dim xNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each xNode In xmlConnections.SelectNodes("//Connection")
                    If xNode.Item("ConnectionName").InnerText = CurStatus.Connection Then blnConnectionExists = True
                    ReturnValue.Add(xNode.Item("ConnectionName").InnerText)
                    If xNode.Attributes("Default").Value = "True" Then
                        curVar.ConnectionDefault = xNode.Item("ConnectionName").InnerText
                    End If
                Next
                If blnConnectionExists = False Then CurStatus.Connection = curVar.ConnectionDefault
                Return ReturnValue
            Catch ex As Exception
                Return Nothing
                '"There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.ConnectionsFile) & Environment.NewLine & ex.Message)
                'WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.ConnectionsFile) & Environment.NewLine & ex.Message, 1)
            End Try
        End If
        Return Nothing
    End Function

    Public Sub LoadConnection(xmlConnections As XmlDocument, strConnection As String)
        Dim xmlConnNode As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strConnection)

        dhdConnection.DataLocation = xmlConnNode.Item("DataLocation").InnerText
        dhdConnection.DatabaseName = xmlConnNode.Item("DataBaseName").InnerText
        dhdConnection.DataProvider = xmlConnNode.Item("DataProvider").InnerText
        dhdConnection.LoginMethod = xmlConnNode.Item("LoginMethod").InnerText
        dhdConnection.LoginName = xmlConnNode.Item("LoginName").InnerText
        dhdConnection.Password = DataHandler.txt.DecryptText(xmlConnNode.Item("Password").InnerText)
        curVar.TableSetsFile = xmlConnNode.Item("TableSets").InnerText

        dhdConnection.CheckDB()
    End Sub

    Public Function LoadTableSetsXml(xmlTableSets As XmlDocument) As List(Of String)
        If dhdText.CheckFile(CheckFilePath(curVar.TableSetsFile)) = True Then

            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlTableSets.Load(dhdText.PathConvert(CheckFilePath(curVar.TableSetsFile)))
                curVar.TableSetDefault = ""
                Dim blnTableSetExists As Boolean = False

                Dim TableSetNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each TableSetNode In xmlTableSets.SelectNodes("//TableSet")
                    If TableSetNode.Item("TableSetName").InnerText = curStatus.TableSet Then blnTableSetExists = True
                    ReturnValue.Add(TableSetNode.Item("TableSetName").InnerText)
                    If TableSetNode.Attributes("Default").Value = "True" Then
                        curVar.TableSetDefault = TableSetNode.Item("TableSetName").InnerText
                    End If
                Next
                If blnTableSetExists = False Then curStatus.TableSet = curVar.TableSetDefault
                Return ReturnValue
            Catch ex As Exception
                Return Nothing
                'MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TableSetsFile) & Environment.NewLine & ex.Message)
                'WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TableSetsFile) & Environment.NewLine & ex.Message, 1)
            End Try
        End If
        Return Nothing
    End Function

    Public Sub LoadTableSet(xmlTableSets As XmlDocument, strTableSet As String)
        Dim xmlTSNode As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strTableSet)
        If xmlTSNode Is Nothing Then Exit Sub
        'curStatus.TableSet = xmlTSNode.Item("TableSetName").InnerText
        curVar.TablesFile = xmlTSNode.Item("TablesFile").InnerText
        dhdText.OutputFile = xmlTSNode.Item("OutputPath").InnerText
        curVar.ReportSetFile = xmlTSNode.Item("ReportSet").InnerText
        If dhdText.CheckNodeElement(xmlTSNode, "Search") Then curVar.SearchFile = xmlTSNode.Item("Search").InnerText
    End Sub

    Public Function LoadTablesXml(xmlTables As XmlDocument) As List(Of String)
        If dhdText.CheckFile(CheckFilePath(curVar.TablesFile)) = True Then
            Try
                xmlTables.Load(dhdText.PathConvert(CheckFilePath(curVar.TablesFile)))
                Dim blnTableExists As Boolean = False
                curVar.TableDefault = ""

                Dim TableNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each TableNode In xmlTables.SelectNodes("//Table")
                    If TableNode.Item("Name").InnerText = curStatus.Table Then blnTableExists = True
                    ReturnValue.Add(TableNode.Item("Alias").InnerText)
                    If TableNode.Attributes("Default").Value = "True" Then
                        curVar.TableDefault = TableNode.Item("Alias").InnerText
                    End If
                Next
                If blnTableExists = False Then curStatus.Table = curVar.TableDefault
                Return ReturnValue
            Catch ex As Exception
                Return Nothing
                'MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TablesFile) & Environment.NewLine & ex.Message)
                'WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TablesFile) & Environment.NewLine & ex.Message, 1)
            End Try
        End If
        Return Nothing
    End Function

    Public Function LoadReportsXml(xmlReports As XmlDocument) As List(Of String)
        If dhdText.CheckFile(CheckFilePath(curVar.ReportSetFile)) = True Then
            Try
                xmlReports.Load(dhdText.PathConvert(CheckFilePath(curVar.ReportSetFile)))

                Dim xNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each xNode In xmlReports.SelectNodes("//Report")
                    ReturnValue.Add(xNode.Item("ReportName").InnerText)
                Next
                Return ReturnValue
            Catch ex As Exception
                Return Nothing
                'MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.ReportSetFile) & Environment.NewLine & ex.Message)
                'WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.ReportSetFile) & Environment.NewLine & ex.Message, 1)
            End Try
        End If
        Return Nothing
    End Function

    Public Function GetFieldDataType(xmlTables As XmlDocument, strFullFieldName As String) As String
        Dim strTableName As String = strFullFieldName.Substring(0, strFullFieldName.LastIndexOf("."))
        Dim strFieldName As String = strFullFieldName.Substring(strFullFieldName.LastIndexOf(".") + 1, strFullFieldName.Length - (strFullFieldName.LastIndexOf(".") + 1))
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName)
        Dim strFieldDataType As String = xCNode.Item("DataType").InnerText
        Return strFieldDataType
    End Function

    Public Function FormatFieldXML(xmlTablesDoc As XmlDocument, strFullFieldName As String, strShowMode As String, blnUseAlias As Boolean, blnSelect As Boolean, DateTimeStyle As Integer) As String
        Dim strOutput As String = ""
        Dim strTableName As String = strFullFieldName.Substring(0, strFullFieldName.LastIndexOf("."))
        Dim strFieldName As String = strFullFieldName.Substring(strFullFieldName.LastIndexOf(".") + 1, strFullFieldName.Length - (strFullFieldName.LastIndexOf(".") + 1))
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTablesDoc, "Table", "Name", strTableName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName)
        Dim strFieldType As String = xCNode.Item("DataType").InnerText
        Dim strFieldWidth As String = xCNode.Item("FldWidth").InnerText
        Dim strFieldAlias As String = xCNode.Item("FldAlias").InnerText
        If blnUseAlias = False Then strFieldAlias = Nothing

        strOutput = FormatField(strFieldName, strTableName, strFieldWidth, strFieldType, strFieldAlias, strShowMode, blnSelect, DateTimeStyle)
        Return strOutput
    End Function

    Public Function FormatField(strFieldName As String, strTableName As String, strFieldWidth As String, strFieldType As String, strFieldAlias As String, strShowMode As String, blnSelect As Boolean, DateTimeStyle As Integer) As String
        Dim strOutput As String = ""
        Dim strFQDN As String = "[" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "]"

        If Not strShowMode Is Nothing Then
            Select Case strShowMode.ToUpper
                Case "SUM", "AVG", "COUNT"
                    strFQDN = strShowMode & "(" & strFQDN & ")"
                    strFieldType = "INTEGER"
                Case "MIN", "MAX"
                    strFQDN = strShowMode & "(" & strFQDN & ")"
                Case "YEAR", "MONTH", "DAY", "HOUR", "MINUTE", "SECOND"
                    strFQDN = "DATEPART(" & strShowMode & "," & strFQDN & ")"
                    strFieldType = "INTEGER"
                Case "DATE"
                    strFQDN = "CAST(" & strFQDN & " AS DATE)"
                    strFieldType = "DATETIME"
                Case "TIME"
                    strFQDN = "CAST(" & strFQDN & " AS TIME)"
                    strFieldType = "TIME"
                Case Else
                    'Do Nothing
            End Select
            If Not strFieldAlias Is Nothing Then
                strFieldAlias = strShowMode.ToLower & strFieldAlias
            Else
                strFieldAlias = strShowMode.ToLower & strFieldName
            End If
        End If

        Select Case strFieldType.ToUpper
            Case "IMAGE"
                strOutput = "(CONVERT([varchar](" & strFieldWidth & "), " & strFQDN & "))"
            Case "BINARY", "GEO", "TEXT", "GUID"
                strOutput = "(CONVERT([nvarchar](" & strFieldWidth & "), " & strFQDN & "))"
            Case "TIME", "TIMESTAMP"
                Dim intFieldWidth As Integer = 8
                If IsNumeric(strFieldWidth) = 1 And strFieldWidth < intFieldWidth Then intFieldWidth = strFieldWidth
                Select Case DateTimeStyle
                    Case 101, 100
                        strOutput = "(CONVERT([nvarchar](7), " & strFQDN & ", 100))"
                    Case 105, 102
                        strOutput = "(CONVERT([nvarchar](8), " & strFQDN & ", 120))"
                    Case Else
                        strOutput = "(CONVERT([nvarchar](13), " & strFQDN & ", " & DateTimeStyle & "))"
                End Select
            Case "XML"
                strOutput = "(CONVERT([nvarchar](max), " & strFQDN & "))"
            Case "DATETIME"
                strOutput = "(CONVERT([nvarchar](" & strFieldWidth & "), " & strFQDN & ", " & DateTimeStyle & "))"
            Case Else
                'CHAR: no need to convert char or int values to char.
                strOutput = strFQDN
        End Select

        If blnSelect = True Then
            If Not strFieldAlias Is Nothing Then
                If strFieldAlias.Length > 0 Then
                    strOutput &= " AS [" & strFieldAlias & "]"
                End If
            End If
        End If

        Return strOutput
    End Function

    Public Function ReportQueryBuild(xmlQueryDoc As XmlDocument, xmlTables As XmlDocument, strReportName As String, DateTimeStyle As Integer) As String
        Dim strTableName As String = ""
        Dim strFieldName As String = ""
        Dim strShowMode As String = Nothing
        'Dim strHavingMode As String = Nothing
        Dim strQueryFrom As String = ""
        Dim strQueryWhere As String = "WHERE "
        Dim strWhereClause As String = ""
        Dim strWhereMode As String = ""
        Dim intSort As Integer = 0
        Dim intMaxSort As Integer = 0

        Dim strHavingField As String = ""
        Dim strQueryHaving As String = "HAVING "
        Dim strHavingMode As String = Nothing, strHavingType As String = Nothing, strHavingClause As String = Nothing

        Dim strQueryGroup As String = "GROUP BY ", blnGroup As Boolean = False
        Dim strQueryOrder As String = ""

        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlQueryDoc, "Report", "ReportName", strReportName)

        Dim strQuery As String = "SELECT "
        'iterate through all fields

        Dim xmlCNodelist As XmlNodeList = dhdText.FindXmlChildNodes(xNode, "Fields/Field")
        For Each xCNode As XmlNode In xmlCNodelist
            strTableName = xCNode.Item("TableName").InnerText
            If strTableName.IndexOf(".") = -1 Then strTableName = "dbo." & strTableName
            strFieldName = xCNode.Item("FieldName").InnerText
            If IsNumeric(xCNode.Item("FieldSortOrder").InnerText) Then
                intSort = xCNode.Item("FieldSortOrder").InnerText
                If intSort > intMaxSort Then intMaxSort = intSort
            End If
            Try
                If xCNode.Item("FieldShow").InnerText = 1 Then
                    strShowMode = xCNode.Item("FieldShowMode").InnerText
                    strQuery &= ", " & FormatFieldXML(xmlTables, strTableName & "." & strFieldName, strShowMode, True, True, DateTimeStyle)
                    Select Case strShowMode
                        Case Nothing
                            strQueryGroup &= ", " & strTableName & "." & strFieldName
                        Case "DATE", "YEAR", "MONTH", "DAY", "TIME", "HOUR", "MINUTE", "SECOND"
                            strQueryGroup &= ", " & strFieldName
                        Case Else
                            blnGroup = True
                    End Select
                End If

                'xCNode.Item("FieldSort").InnerText)
                'xCNode.Item("FieldSortOrder").InnerText)

                For Each xFnode In dhdText.FindXmlChildNodes(xCNode, "Filters/Filter")
                    If xFnode.Item("FilterEnabled").InnerText = "Indeterminate" Then
                        blnGroup = True
                        strHavingMode = xFnode.Item("FilterMode").InnerText
                        If strHavingMode = "" Then strHavingMode = "AND"
                        If strHavingMode.Contains("AND") Then strHavingMode = ") " & strHavingMode & " ("
                        strHavingType = xFnode.Item("FilterType").InnerText
                        strHavingClause = SetDelimiters(xFnode.Item("FilterText").InnerText, GetFieldDataType(xmlTables, strTableName & "." & strFieldName), strHavingType, strShowMode)
                        strHavingField = " " & FormatFieldXML(xmlTables, strTableName & "." & strFieldName, strShowMode, False, False, DateTimeStyle)
                        If strHavingType.Contains("LIKE") And strHavingClause.Contains("*") Then strHavingClause = strHavingClause.Replace("*", "%")

                        If strHavingType <> Nothing And strHavingClause <> Nothing Then
                            'If strHavingMode = Nothing Then
                            '    strQueryHaving &= " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            'Else
                            strQueryHaving &= " " & strHavingMode & " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            'End If
                        End If
                    End If

                    If xFnode.Item("FilterEnabled").InnerText = "Checked" Then
                        strWhereMode = xFnode.Item("FilterMode").InnerText
                        If strWhereMode = "" Then strWhereMode = "AND"
                        If strWhereMode.Contains("AND") Then strWhereMode = ") " & strWhereMode & " ("
                        strWhereClause = SetDelimiters(xFnode.Item("FilterText").InnerText, GetFieldDataType(xmlTables, strTableName & "." & strFieldName), xFnode.Item("FilterType").InnerText)

                        If xFnode.Item("FilterType").InnerText.Contains("LIKE") And strWhereClause.Contains("*") Then strWhereClause = strWhereClause.Replace("*", "%")
                        If xFnode.Item("FilterType").InnerText <> "" And strWhereClause <> "" Then
                            strQueryWhere &= " " & strWhereMode & " " & strTableName & "." & strFieldName & " " & xFnode.Item("FilterType").InnerText & " " & strWhereClause
                        End If
                    End If

                Next
            Catch ex As Exception
                'Skip this field
            End Try
        Next


        If strQuery = "SELECT " Then
            Return Nothing
        End If
        strQuery = strQuery.Replace("SELECT ,", "SELECT ")
        'Check for top x 
        If xNode.Item("UseTop").InnerText = True And IsNumeric(xNode.Item("UseTopNumber").InnerText) = True Then strQuery = strQuery.Replace("SELECT ", "SELECT TOP " & xNode.Item("UseTopNumber").InnerText & " ")
        'Check for distinct
        If xNode.Item("UseDistinct").InnerText = True Then strQuery = strQuery.Replace("SELECT ", "SELECT DISTINCT ")
        strQueryGroup = strQueryGroup.Replace("GROUP BY ,", "GROUP BY ")
        If strQueryGroup = "GROUP BY " Then blnGroup = False

        strQueryFrom = FromClauseGet(xNode)
        strQuery &= Environment.NewLine & strQueryFrom
        strQueryWhere &= ")"
        strQueryWhere = strQueryWhere.Replace("WHERE  ) AND", "WHERE ").Replace("WHERE  OR", "WHERE (").Replace("WHERE  ) AND NOT", "WHERE NOT (")
        strQueryHaving &= ")"
        strQueryHaving = strQueryHaving.Replace("HAVING  ) AND", "HAVING ").Replace("HAVING  OR", "HAVING (").Replace("HAVING  ) AND NOT", "HAVING NOT (")
        'strQueryHaving = strQueryHaving..Replace("HAVING  ) OR", "HAVING ")

        If strQueryWhere.Length > 10 Then strQuery &= Environment.NewLine & strQueryWhere
        If blnGroup = True Then strQuery &= Environment.NewLine & strQueryGroup
        If strQueryHaving.Length > 11 Then strQuery &= Environment.NewLine & strQueryHaving
        strQueryOrder = OrderClauseGet(xmlCNodelist, intMaxSort)
        If strQueryOrder.Length > 10 Then strQuery &= Environment.NewLine & strQueryOrder

        Return strQuery
    End Function

    Public Function FromClauseGet(XNode As XmlNode) As String
        Dim strFromClause As String = "FROM "
        Dim strFromSource As String = Nothing, strFromType As String = Nothing, strFromRelation As String = Nothing, strTargetTable As String = Nothing

        Dim intCount As Integer = 0
        For Each xTNode As XmlNode In dhdText.FindXmlChildNodes(XNode, "Relations/Relation")
            Dim strTableName As String = xTNode.Item("TableName").InnerText
            If intCount = 0 Then strFromClause &= strTableName
            If xTNode.Item("RelationEnabled").InnerText = "True" Then

                strFromSource = xTNode.Item("RelationSource").InnerText
                strFromRelation = xTNode.Item("RelationTarget").InnerText
                strFromType = xTNode.Item("RelationJoinType").InnerText
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

            intCount += 1
        Next
        Return strFromClause
    End Function

    Private Function OrderClauseGet(xmlCNodelist As XmlNodeList, intMaxSort As Integer) As String
        Dim strQueryOrder As String = "ORDER BY "
        Dim strTableName As String = ""
        Dim strFieldName As String = ""
        Dim strFieldSort As String = ""
        Dim intOrder As Integer = 0

        For intCount As Integer = 1 To intMaxSort
            For Each xCNode As XmlNode In xmlCNodelist
                If IsNumeric(xCNode.Item("FieldSortOrder").InnerText) Then
                    intOrder = xCNode.Item("FieldSortOrder").InnerText
                    If intCount = intOrder Or (intCount > intMaxSort And intOrder > intMaxSort) Then
                        strTableName = xCNode.Item("TableName").InnerText
                        If strTableName.IndexOf(".") = -1 Then strTableName = "dbo." & strTableName
                        strFieldName = xCNode.Item("FieldName").InnerText
                        strQueryOrder &= ", " & strTableName & "." & strFieldName
                        strFieldSort = xCNode.Item("FieldSort").InnerText
                        If strFieldSort = "ASC" Or strFieldSort = "DESC" Then strQueryOrder &= " " & strFieldSort
                    End If
                End If
            Next
        Next

        strQueryOrder = strQueryOrder.Replace("ORDER BY ,", "ORDER BY ")
        Return strQueryOrder
    End Function

#End Region

#Region "Export"
    Public Sub ExportFile(dtsInput As DataSet, strFileName As String, blnShowFile As Boolean)
        Dim strTargetFile As String = strFileName.Substring(0, strFileName.LastIndexOf("."))
        If curVar.IncludeDate = True Then
            strTargetFile = strTargetFile & "_" & FormatFileDate(Now)
        End If

        Dim strExtension As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1, strFileName.Length - (strFileName.LastIndexOf(".") + 1))
        Dim strExportFile As String = strTargetFile & "." & strExtension
        Try
            Select Case strExtension.ToLower
                Case "xml"
                    dhdText.ExportDataSetToXML(dhdConnection.ConvertToText(dtsInput), strExportFile)
                Case "xlsx"
                    Excel.CreateExcelDocument(dtsInput, strExportFile)
                Case "xls"
                    Excel.CreateExcelDocument(dhdConnection.ConvertToText(dtsInput), strExportFile)
                Case Else
                    'unknown filetype, do nothing
                    blnShowFile = False
            End Select
        Catch ex As Exception
            blnShowFile = False
            'WriteLog("Couldn't create Excel file.\r\nException: " + ex.Message, 1)
        End Try

        If blnShowFile = True Then
            Dim p As New Process
            p.StartInfo = New ProcessStartInfo(strExportFile)
            p.Start()
        End If

    End Sub

#End Region
End Class
