Imports System.Xml

Public Class Data
    Public dhdText As New DataHandler.txt
    Public dhdMainDB As New DataHandler.db
    Public dhdConnection As New DataHandler.db
    Public curVar As New Variables
    Public curStatus As New CurrentStatus
    Public Excel As New Excel

    Private _ErrorLevel As Integer = 0
    Private _ErrorMessage As String = ""

    Public Property ErrorLevel() As Integer
        Get
            Return _ErrorLevel
        End Get
        Set(ByVal Value As Integer)
            _ErrorLevel = Value
        End Set
    End Property

    Public Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
        Set(ByVal Value As String)
            _ErrorMessage = Value
        End Set
    End Property

    Public DataBaseOnline As Boolean = False

    Public Function TestPath(intInput As Integer) As String
        Select Case intInput
            Case 1
                Return System.AppDomain.CurrentDomain.BaseDirectory
            Case 2
                Return System.Reflection.Assembly.GetCallingAssembly.Location
            Case 3
                Return Reflection.Assembly.GetExecutingAssembly().Location
            Case 4
                Return Reflection.Assembly.GetEntryAssembly().Location
            Case 5
                Return IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName)
        End Select
        Return ""
    End Function

#Region "General"
    Public Sub WriteLog(ByVal strLogtext As String, ByVal intLogLevel As Integer)
        Dim blnLogSucces As Boolean = True
        Try
            If dhdText.LogLocation.ToLower = "database" Then
                If dhdMainDB.WriteLog(strLogtext, intLogLevel, dhdText.LogLevel) = False Then
                    blnLogSucces = False
                End If
            Else
                If dhdText.WriteLog(strLogtext, intLogLevel) = False Then
                    blnLogSucces = False
                End If
                'If DevMode Then MessageBox.Show(dhdText.LogFileName & Environment.NewLine & dhdText.LogLocation & Environment.NewLine & dhdText.LogLevel)
            End If
        Catch ex As Exception
            blnLogSucces = False
            'MessageBox.Show("there was an error writing to the logfile: " & Environment.NewLine & ex.Message)
        End Try

        If blnLogSucces = False Then
            Dim strMyDir As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            Dim strOrgDir As String = dhdText.LogLocation
            If dhdText.CheckDir(strMyDir & "\Sequenchel", True) = False Then dhdText.CreateDir(strMyDir & "\Sequenchel")
            If dhdText.CheckDir(strMyDir & "\Sequenchel\LOG", True) = False Then dhdText.CreateDir(strMyDir & "\Sequenchel\LOG")
            dhdText.LogFileName = "Sequenchel.Log"
            dhdText.LogLocation = strMyDir & "\Sequenchel\LOG"
            dhdText.WriteLog("There was an error writng to the logfile at: " & strOrgDir & Environment.NewLine & dhdText.Errormessage, 1)
        End If
    End Sub

#End Region

#Region "DataBase"
    Public Function QueryDb(ByVal dhdConnect As DataHandler.db, ByVal strQueryData As String, ByVal ReturnValue As Boolean, Optional ByVal LogLevel As Integer = 5) As DataSet
        If String.IsNullOrWhiteSpace(strQueryData) Then
            WriteLog("No query was created, aborting querying database", LogLevel)
            Return Nothing
        End If
        WriteLog(strQueryData, LogLevel)
        ErrorMessage = ""
        dhdConnect.CheckDB()
        If dhdConnect.DataBaseOnline = False Then
            ErrorMessage = "The database was not found. Please check your settings"
            Return Nothing
        End If
        Dim dtsData As DataSet
        Try
            dtsData = dhdConnect.QueryDatabase(strQueryData, ReturnValue)
            If dhdConnect.DataBaseOnline = False Then
                ErrorMessage = dhdConnect.ErrorMessage
                Return Nothing
            End If
            Return dtsData
        Catch ex As Exception
            WriteLog(ex.Message, 1)
            ErrorMessage = ex.Message
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
        If strInput = Nothing Then strInput = ""
        If strInput.Length > 2 Then
            If strInput.Substring(0, 2) = "f:" Then
                Return "(" & strInput.Replace("f:", "") & ")"
            End If
            If strInput.Substring(0, 2) = "v:" Then
                strInput = strInput.Replace("v:", "")
                strInput = ProcessDefaultValue(strInput)
            End If
        End If
        If strCompare = Nothing Then Return strInput
        If strCompare = "IS" Or strCompare = "IS NOT" Then
            Return strInput
        End If
        If Not strShowMode = Nothing Then
            If strShowMode = "COUNT" Or _
                strShowMode = "SUM" Or _
                strShowMode = "AVG" Then
                strDataType = "INTEGER"
            End If
        End If
        If Not strDataType = Nothing Then
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

    Public Function SaveToDatabase(ByVal dhdConnect As DataHandler.db, dtsInput As DataSet, Optional ConvertToText As Boolean = False, Optional ConvertToNull As Boolean = False) As Integer
        Dim intRecordsAffected As Integer = 0
        Dim intReturn As Integer = 0

        Try
            For Each dttInput As DataTable In dtsInput.Tables
                Dim blnExport As Boolean = True
                If dttInput.ExtendedProperties.Count > 0 Then
                    If dttInput.ExtendedProperties.ContainsKey("ExportTable") = True Then
                        If dttInput.ExtendedProperties("ExportTable") = "False" Then
                            blnExport = False
                        End If
                    End If
                End If
                If blnExport = True Then
                    intReturn = UploadSqlData(dhdConnect, dttInput, ConvertToText, ConvertToNull)
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
            ErrorLevel = -1
            ErrorMessage = "Export to database failed. Check if the columns match and try again. If you are importing more than 1 table, make sure they have identical columns" & ex.Message
            Return ErrorLevel
        End Try
        Return intRecordsAffected
    End Function

    Public Function UploadSqlData(ByVal dhdConnect As DataHandler.db, ByVal dttInput As DataTable, Optional ConvertToText As Boolean = False, Optional ConvertToNull As Boolean = False) As Integer
        Dim intRecordsAffected As Integer = 0
        intRecordsAffected = dttInput.Rows.Count
        Try
            If dhdConnect.SqlConnection.State = ConnectionState.Open Then dhdConnect.SqlConnection.Close()
            Using bcp As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(dhdConnect.SqlConnection)
                If dhdConnect.SqlConnection.State = ConnectionState.Closed Then dhdConnect.SqlConnection.Open()
                bcp.DestinationTableName = dhdConnect.DataTableName
                If ConvertToText = True Then dttInput = dhdConnect.ConvertToText(dttInput)
                If ConvertToNull = True Then dttInput = dhdConnect.EmptyToNull(dttInput)
                Dim reader As DataTableReader = dttInput.CreateDataReader()
                bcp.WriteToServer(reader)
            End Using
        Catch ex As Exception
            dhdConnect.ErrorMessage = ex.Message
            dhdConnect.ErrorLevel = -1
            intRecordsAffected = -1
        End Try
        'Try
        '    'If dhdConnect.SqlConnection.State = ConnectionState.Open Then dhdConnect.SqlConnection.Close()
        'Catch ex As Exception
        '    dhdConnect.ErrorMessage = ex.Message
        '    dhdConnect.ErrorLevel = -1
        'End Try
        Return intRecordsAffected
    End Function
#End Region

#Region "XML"
    Public Function CheckFilePath(strFilePathName As String, Optional blnPersonal As Boolean = False) As String
        If strFilePathName.Contains("\") Then
            Return strFilePathName
        ElseIf blnPersonal = True Then
            Return dhdText.PathConvert("%Documents%\" & strFilePathName)
        ElseIf curVar.DefaultConfigFilePath.Length > 0 Then
            Return curVar.DefaultConfigFilePath & "\" & strFilePathName
        Else
            Return System.AppDomain.CurrentDomain.BaseDirectory & strFilePathName
        End If
    End Function



    Public Function LoadTablesXml(xmlTables As XmlDocument) As List(Of String)
        If dhdText.CheckFile(CheckFilePath(curVar.TablesFile)) = True Then
            Try
                xmlTables.Load(dhdText.PathConvert(CheckFilePath(curVar.TablesFile)))
                Dim ReturnValue As List(Of String) = LoadTablesListXml(xmlTables, False)
                Return ReturnValue
            Catch ex As Exception
                Return Nothing
                ErrorMessage = "There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TablesFile) & Environment.NewLine & ex.Message
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TablesFile) & Environment.NewLine & ex.Message, 1)
            End Try
        End If
        Return Nothing
    End Function

    Public Function LoadTablesListXml(xmlTables As XmlDocument, blnUseFullName As Boolean) As List(Of String)
        Try
            Dim blnTableExists As Boolean = False
            curVar.TableDefault = ""

            Dim TableNode As XmlNode
            Dim ReturnValue As New List(Of String)
            For Each TableNode In xmlTables.SelectNodes("//Table")
                If TableNode.Item("Name").InnerText = curStatus.Table Then blnTableExists = True
                If blnUseFullName = True Then
                    ReturnValue.Add(TableNode.Item("Alias").InnerText & " (" & TableNode.Item("Name").InnerText & ")")
                Else
                    ReturnValue.Add(TableNode.Item("Alias").InnerText)
                End If
                If TableNode.Attributes("Default").Value = "True" Then
                    curVar.TableDefault = TableNode.Item("Alias").InnerText
                End If
            Next
            If blnTableExists = False And curStatus.Table = "" Then curStatus.Table = curVar.TableDefault
            Return ReturnValue
        Catch ex As Exception
            Return Nothing
            ErrorMessage = "There was an error loading the tables list. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TablesFile) & Environment.NewLine & ex.Message
            WriteLog("There was an error loading the tables list. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TablesFile) & Environment.NewLine & ex.Message, 1)
        End Try

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
                ErrorMessage = "There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.ReportSetFile) & Environment.NewLine & ex.Message
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.ReportSetFile) & Environment.NewLine & ex.Message, 1)
            End Try
        End If
        Return Nothing
    End Function

    Public Function LoadSearchXml(xmlSearch As XmlDocument, strTable As String) As List(Of String)
        If dhdText.CheckFile(CheckFilePath(curVar.SearchFile)) = True Then
            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlSearch.Load(dhdText.PathConvert(CheckFilePath(curVar.SearchFile)))
                'Dim blnConnectionExists As Boolean = False
                'CurVar.ConnectionDefault = ""

                Dim xNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each xNode In dhdText.FindXmlNodes(xmlSearch, "Searches/Search", "TableName", strTable)
                    ReturnValue.Add(xNode.Item("SearchName").InnerText)
                Next
                Return ReturnValue
            Catch ex As Exception
                ErrorMessage = "There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.SearchFile) & Environment.NewLine & ex.Message
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.SearchFile) & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            xmlSearch.RemoveAll()
        End If
        Return Nothing
    End Function

    Public Function GetFieldDataType(xmlTables As XmlDocument, strTableName As String, strFieldName As String) As String
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName)
        Dim strFieldDataType As String = xCNode.Item("DataType").InnerText
        Return strFieldDataType
    End Function

    Public Function FormatFieldXML(xmlTablesDoc As XmlDocument, strTableName As String, strTableAlias As String, strFieldName As String, strShowMode As String, blnUseAlias As Boolean, blnSelect As Boolean, blnConvert As Boolean, DateTimeStyle As Integer) As String
        Dim strOutput As String = ""
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTablesDoc, "Table", "Name", strTableName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName)
        Dim strFieldType As String = xCNode.Item("DataType").InnerText
        Dim strFieldWidth As String = xCNode.Item("FldWidth").InnerText
        Dim strFieldAlias As String = xCNode.Item("FldAlias").InnerText
        If blnUseAlias = False Then strFieldAlias = Nothing

        strOutput = FormatField(strTableName, strTableAlias, strFieldName, strFieldAlias, strFieldType, strFieldWidth, strShowMode, blnSelect, blnConvert, DateTimeStyle)
        Return strOutput
    End Function

    Public Function FormatField(strTableName As String, strTableAlias As String, strFieldName As String, strFieldAlias As String, strFieldType As String, strFieldWidth As String, strShowMode As String, blnSelect As Boolean, blnConvert As Boolean, DateTimeStyle As Integer) As String
        Dim strOutput As String = ""
        If String.IsNullOrEmpty(strTableAlias) Then strTableAlias = strTableName.Replace(".", "_")
        'Dim strFQDN As String = "[" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "]"
        Dim strFQDN As String = "[" & strTableAlias & "].[" & strFieldName & "]"

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

        If blnConvert = True Or (strFieldType.ToUpper = "DATETIME" And Not DateTimeStyle = Nothing) Then
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
        Else
            strOutput = strFQDN
        End If

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
        Dim strTableName As String = "", strTableAlias As String = ""
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
            If dhdText.CheckNodeElement(xCNode, "TableAlias") Then strTableAlias = xCNode.Item("TableAlias").InnerText Else strTableAlias = strTableName.Replace(".", "_")
            If strTableName.Contains(".") = False Then strTableName = "dbo." & strTableName
            strFieldName = xCNode.Item("FieldName").InnerText
            If IsNumeric(xCNode.Item("FieldSortOrder").InnerText) Then
                intSort = xCNode.Item("FieldSortOrder").InnerText
                If intSort > intMaxSort Then intMaxSort = intSort
            End If
            Try
                If xCNode.Item("FieldShow").InnerText = 1 Then
                    strShowMode = xCNode.Item("FieldShowMode").InnerText
                    Dim strQueryField As String = FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, True, True, True, DateTimeStyle)
                    strQuery &= ", " & strQueryField
                    Select Case strShowMode
                        Case Nothing
                            strQueryGroup &= ", " & FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, False, False, True, DateTimeStyle)
                        Case "DATE", "YEAR", "MONTH", "DAY", "TIME", "HOUR", "MINUTE", "SECOND"
                            strQueryGroup &= ", " & FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, False, False, True, DateTimeStyle)
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
                        strHavingClause = SetDelimiters(xFnode.Item("FilterText").InnerText, GetFieldDataType(xmlTables, strTableName, strFieldName), strHavingType, strShowMode)
                        strHavingField = " " & FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, False, False, True, DateTimeStyle)
                        If strHavingType.Contains("LIKE") And strHavingClause.Contains("*") Then strHavingClause = strHavingClause.Replace("*", "%")

                        If Not strHavingType = Nothing And Not strHavingClause = Nothing Then
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
                        strWhereClause = SetDelimiters(xFnode.Item("FilterText").InnerText, GetFieldDataType(xmlTables, strTableName, strFieldName), xFnode.Item("FilterType").InnerText)

                        If xFnode.Item("FilterType").InnerText.Contains("LIKE") And strWhereClause.Contains("*") Then strWhereClause = strWhereClause.Replace("*", "%")
                        If xFnode.Item("FilterType").InnerText <> "" And strWhereClause <> "" Then
                            strQueryWhere &= " " & strWhereMode & " " & GetTableAliasFromString(strTableAlias) & "." & strFieldName & " " & xFnode.Item("FilterType").InnerText & " " & strWhereClause
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

    Private Function FromClauseGet(XNode As XmlNode) As String
        Dim strFromClause As String = "FROM "
        Dim strFromSource As String = Nothing, strFromType As String = Nothing, strFromRelation As String = Nothing, strTargetTable As String = Nothing, strTargetTableAlias As String = Nothing, strTargetField As String = Nothing

        Dim intCount As Integer = 0
        For Each xTNode As XmlNode In dhdText.FindXmlChildNodes(XNode, "Relations/Relation")
            Dim strTableName As String = xTNode.Item("TableName").InnerText
            Dim strTableAlias As String = ""
            If dhdText.CheckNodeElement(xTNode, "TableAlias") Then strTableAlias = GetTableAliasFromString(xTNode.Item("TableAlias").InnerText)
            If String.IsNullOrEmpty(strTableAlias) Then strTableAlias = GetTableAliasFromString(xTNode.Item("TableName").InnerText)

            If intCount = 0 Then strFromClause &= strTableName & " " & strTableAlias
            If xTNode.Item("RelationEnabled").InnerText = "True" Then

                strFromSource = xTNode.Item("RelationSource").InnerText
                strFromType = xTNode.Item("RelationJoinType").InnerText

                If dhdText.CheckNodeElement(xTNode, "RelationTarget") Then
                    strFromRelation = xTNode.Item("RelationTarget").InnerText
                    strTargetTable = GetTableNameFromString(strFromRelation)
                    strTargetTableAlias = GetTableAliasFromString(strFromRelation)
                    strTargetField = strFromRelation.Substring(strFromRelation.LastIndexOf(".") + 1, strFromRelation.Length - (strFromRelation.LastIndexOf(".") + 1))
                End If
                If dhdText.CheckNodeElement(xTNode, "RelationTargetTable") Then strTargetTable = xTNode.Item("RelationTargetTable").InnerText
                If dhdText.CheckNodeElement(xTNode, "RelationTargetAlias") Then strTargetTableAlias = xTNode.Item("RelationTargetAlias").InnerText
                If dhdText.CheckNodeElement(xTNode, "RelationTargetField") Then strTargetField = xTNode.Item("RelationTargetField").InnerText

                'strTargetTable = strFromRelation.Substring(0, strFromRelation.LastIndexOf("."))
                'strTargetTable = strTargetTable.Substring(strTargetTable.LastIndexOf("(") + 1, strTargetTable.Length - (strTargetTable.LastIndexOf("(") + 1))

                If strFromClause.Contains(strTargetTable) = True And strFromClause.Contains(strTableName) = False Then
                    strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTableName & " " & strTableAlias & " ON " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                ElseIf strFromClause.Contains(strTargetTable) = True And strFromClause.Contains(strTableName) = True Then
                    strFromClause &= Environment.NewLine & " AND " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                ElseIf strFromClause.Contains(strTargetTable) = False And strFromClause.Contains(strTableName) = False Then
                    strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTargetTable & " " & strTargetTableAlias & " ON " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                ElseIf strFromClause.Contains(strTargetTable) = False And strFromClause.Contains(strTableName) = True Then
                    strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTargetTable & " " & strTargetTableAlias & " ON " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                End If
            End If

            intCount += 1
        Next
        Return strFromClause
    End Function

    Private Function OrderClauseGet(xmlCNodelist As XmlNodeList, intMaxSort As Integer) As String
        Dim strQueryOrder As String = "ORDER BY "
        Dim strTableAlias As String = ""
        Dim strFieldName As String = ""
        Dim strFieldSort As String = ""
        Dim intOrder As Integer = 0

        For intCount As Integer = 1 To intMaxSort
            For Each xCNode As XmlNode In xmlCNodelist
                If IsNumeric(xCNode.Item("FieldSortOrder").InnerText) Then
                    intOrder = xCNode.Item("FieldSortOrder").InnerText
                    If intCount = intOrder Or (intCount > intMaxSort And intOrder > intMaxSort) Then
                        If dhdText.CheckNodeElement(xCNode, "TableAlias") Then strTableAlias = xCNode.Item("TableAlias").InnerText
                        If String.IsNullOrEmpty(strTableAlias) Then strTableAlias = xCNode.Item("TableName").InnerText.Replace(".", "_")
                        'If strTableName.IndexOf(".") = -1 Then strTableName = "dbo." & strTableName
                        strFieldName = xCNode.Item("FieldName").InnerText
                        strQueryOrder &= ", " & strTableAlias & "." & strFieldName
                        strFieldSort = xCNode.Item("FieldSort").InnerText
                        If strFieldSort = "ASC" Or strFieldSort = "DESC" Then strQueryOrder &= " " & strFieldSort
                    End If
                End If
            Next
        Next

        strQueryOrder = strQueryOrder.Replace("ORDER BY ,", "ORDER BY ")
        Return strQueryOrder
    End Function

    Public Function GetTableNameFromAlias(xmlTables As XmlDocument, strInput As String) As String
        Dim strReturn As String = strInput
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Alias", strInput)
        If Not xNode Is Nothing Then
            If dhdText.CheckNodeElement(xNode, "Name") = True Then strReturn = xNode.Item("Name").InnerText
        End If
        Return strReturn
    End Function

    Public Function GetTableNameFromString(strInput As String) As String
        If String.IsNullOrEmpty(strInput) Then Return strInput
        Dim strReturn As String = strInput
        If strInput.Contains("(") Then
            strReturn = strInput.Substring(strInput.IndexOf("(") + 1, strInput.Length - (strInput.IndexOf("(") + 1) - 1)
        Else
            If strInput.IndexOf(".") <> strInput.LastIndexOf(".") Then
                strReturn = strInput.Substring(0, strInput.LastIndexOf("."))
            End If
        End If
        Return strReturn
    End Function

    Public Function GetTableAliasFromString(strInput As String) As String
        If String.IsNullOrEmpty(strInput) Then Return strInput
        Dim strReturn As String = strInput
        If strInput.Contains("(") Then
            strReturn = strInput.Substring(0, strInput.IndexOf("(") - 1)
        End If
        If strInput.IndexOf(".") <> strInput.LastIndexOf(".") Then
            strReturn = strInput.Substring(0, strInput.LastIndexOf("."))
        End If
        If strReturn.IndexOf(".") > -1 Then
            strReturn = strReturn.Replace(".", "_")
        End If
        Return strReturn
    End Function

#End Region

#Region "Import & Export"
    Public Function ImportFile(strFileName As String, Optional blnHasHeaders As Boolean = True, Optional Delimiter As String = ",") As DataSet
        Dim dtsImport As New DataSet
        Try
            Dim strExtension As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1, strFileName.Length - (strFileName.LastIndexOf(".") + 1))
            Select Case strExtension.ToLower
                Case "xml"
                    dtsImport = dhdText.LoadXmlToDataset(strFileName)
                Case "xls", "xlsx"
                    dtsImport = Excel.ImportExcelFile(strFileName)
                Case "csv", "txt"
                    If Delimiter.Length = 0 Then
                        Return Nothing
                    End If
                    dtsImport = dhdText.CsvToDataSet(strFileName, blnHasHeaders, Delimiter)
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            'Error importing file
            ErrorLevel = -1
            ErrorMessage = "The file import failed. Check the file and try again."
            dtsImport = Nothing
        End Try
        Return dtsImport
    End Function

    Public Function GetExportFileName(strFileName As String) As String
        Dim strExtension As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1, strFileName.Length - (strFileName.LastIndexOf(".") + 1))
        Dim strTargetFile As String = strFileName.Substring(0, strFileName.LastIndexOf("."))
        If curVar.IncludeDate = True Then
            strTargetFile = strTargetFile & "_" & FormatFileDate(Now)
        End If
        Dim strExportFile As String = strTargetFile & "." & strExtension
        Return strExportFile
    End Function

    Public Function ExportFile(dtsInput As DataSet, strFileName As String, Optional blnConvertToText As Boolean = False, Optional blnConvertToNull As Boolean = False, Optional blnShowFile As Boolean = False, Optional blnHasHeaders As Boolean = True, Optional Delimiter As String = ",", Optional QuoteValues As Boolean = False, Optional CreateDir As Boolean = False) As Boolean
        Dim strExtension As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1, strFileName.Length - (strFileName.LastIndexOf(".") + 1))

        Try
            If blnConvertToText = True Then dtsInput = dhdConnection.ConvertToText(dtsInput)
            If blnConvertToNull = True Then dtsInput = dhdConnection.EmptyToNull(dtsInput)

            Select Case strExtension.ToLower
                Case "xml"
                    dtsInput = dhdConnection.ConvertToText(dtsInput)
                    dhdText.ExportDataSetToXML(dtsInput, strFileName, CreateDir)
                Case "xlsx", "xls"
                    Excel.CreateExcelDocument(dtsInput, strFileName)
                Case "txt", "csv"
                    dtsInput = dhdConnection.ConvertToText(dtsInput)
                    dhdText.DataSetToCsv(dtsInput.Tables(0), strFileName, blnHasHeaders, Delimiter, QuoteValues)
                Case Else
                    'unknown filetype, do nothing
                    blnShowFile = False
                    WriteLog("Unknown File extension. Unable to export file.", 2)
                    Return False
            End Select
        Catch ex As Exception
            blnShowFile = False
            WriteLog("There was an error exporting the file: " + ex.Message, 1)
            Return False
        End Try

        If blnShowFile = True Then
            Dim p As New Process
            p.StartInfo = New ProcessStartInfo(strFileName)
            p.Start()
        End If

        Return True
    End Function

#End Region

End Class
