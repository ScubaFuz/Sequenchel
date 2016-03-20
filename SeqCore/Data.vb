Imports System.Xml

Public Class Data

    Public dhdText As New DataHandler.txt

#Region "DataBase"

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

#End Region

#Region "XML"

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

        Dim strHavingField As String = ""
        Dim strQueryHaving As String = "HAVING "
        Dim strHavingMode As String = Nothing, strHavingType As String = Nothing, strHavingClause As String = Nothing

        Dim strQueryGroup As String = "GROUP BY ", blnGroup As Boolean = False
        Dim strQueryOrder As String = ""

        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlQueryDoc, "Report", "ReportName", strReportName)

        Dim strQuery As String = "SELECT "
        'iterate through all fields

        For Each xCNode As XmlNode In dhdText.FindXmlChildNodes(xNode, "Fields/Field")
            strTableName = xCNode.Item("TableName").InnerText
            If strTableName.IndexOf(".") = -1 Then strTableName = "dbo." & strTableName
            strFieldName = xCNode.Item("FieldName").InnerText
            Try
                If xCNode.Item("FieldShow").InnerText = 1 Then
                    strShowMode = xCNode.Item("FieldShowMode").InnerText
                    strQuery &= ", " & FormatFieldXML(xmlTables, strTableName & "." & strFieldName, strShowMode, True, True, DateTimeStyle)
                    Select Case strShowMode
                        Case Nothing
                            strQueryGroup &= ", " & strFieldName
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
                            If strHavingMode = Nothing Then
                                strQueryHaving &= " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            Else
                                strQueryHaving &= " " & strHavingMode & " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            End If
                        End If
                    End If

                    If xFnode.Item("FilterEnabled").InnerText = "Checked" Then
                        strWhereMode = xFnode.Item("FilterMode").InnerText
                        If strWhereMode = "" Then strWhereMode = "AND"
                        If strWhereMode.Contains("AND") Then strWhereMode = ") " & strWhereMode & " ("
                        strWhereClause = SetDelimiters(xFnode.Item("FilterText").InnerText, GetFieldDataType(xmlTables, strTableName & "." & strFieldName), xFnode.Item("FilterType").InnerText)

                        If xFnode.Item("FilterType").InnerText.Contains("LIKE") And strWhereClause.Contains("*") Then strWhereClause = strWhereClause.Replace("*", "%")
                        If xFnode.Item("FilterType").InnerText <> "" And strWhereClause <> "" Then
                            If strWhereMode = "" Then
                                strQueryWhere &= " " & strFieldName & " " & xFnode.Item("FilterType").InnerText & " " & strWhereClause
                            Else
                                strQueryWhere &= " " & strWhereMode & " " & strFieldName & " " & xFnode.Item("FilterType").InnerText & " " & strWhereClause
                            End If
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
        'strQueryOrder = OrderClauseGet()
        'If strQueryOrder.Length > 10 Then strQuery &= Environment.NewLine & strQueryOrder

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
#End Region

End Class
