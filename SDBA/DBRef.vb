Imports System.IO

Public Class DBRef
    Friend dbVersion As String = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
    Friend intCounter As Integer

    Private Function AddOne() As Integer
        intCounter = intCounter + 1
        Return intCounter
    End Function

    Public Function GetList(Optional blnIncludeTables As Boolean = True) As Array
        Dim arrScripts(100) As String
        intCounter = 0

        'Bogus startscript

        If blnIncludeTables = False Then
            'Database
            arrScripts(intCounter) = "01 Use Sequenchel.sql"
        Else
            'Database
            arrScripts(intCounter) = "01 Create Database.sql"

            'Tables
            arrScripts(AddOne()) = "01 dbo.tbl_AccErrors.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_AccOwners.sql"
            'arrScripts(AddOne()) = "01 dbo.tbl_Arguments.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_Backups.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_Configuration.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_Databases.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_DataSpaces.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_DiskSpaces.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_ErrorLogsAll.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_IndexList.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_Jobs.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_Logging.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_Logins.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_ObjectOwners.sql"
            'arrScripts(AddOne()) = "01 dbo.tbl_Reports.sql"
            arrScripts(AddOne()) = "01 dbo.tbl_Servers.sql"
            'arrScripts(AddOne()) = "02 dbo.tbl_ReportArguments.sql"
            'arrScripts(AddOne()) = "02 dbo.tbl_ReportFields.sql"
            arrScripts(AddOne()) = "AcceptableErrors.sql"
            arrScripts(AddOne()) = "AcceptableOwners.sql"
        End If

        'Functions
        arrScripts(AddOne()) = "dbo.udf_CorrectCommand.sql"
        arrScripts(AddOne()) = "dbo.udf_CsvToInt.sql"
        arrScripts(AddOne()) = "dbo.udf_CsvToVarchar.sql"
        arrScripts(AddOne()) = "dbo.udf_DateFormat.sql"
        arrScripts(AddOne()) = "dbo.udf_EmailDomain.sql"
        arrScripts(AddOne()) = "dbo.udf_EmailPrefix.sql"
        arrScripts(AddOne()) = "dbo.udf_FloorDate.sql"
        arrScripts(AddOne()) = "dbo.udf_ValidEmail.sql"

        'Views
        arrScripts(AddOne()) = "dbo.vw_DataSpaces.sql"
        arrScripts(AddOne()) = "dbo.vw_DiskUseDaysLeft.sql"
        arrScripts(AddOne()) = "dbo.vw_ObjectOwners.sql"
        arrScripts(AddOne()) = "dbo.vw_RecentErrors.sql"
        arrScripts(AddOne()) = "dbo.vw_Servers.sql"

        'Stored Procedures
        arrScripts(AddOne()) = "01 dbo.usp_BackupHandle.sql"
        arrScripts(AddOne()) = "01 dbo.usp_ConfigHandle.sql"
        arrScripts(AddOne()) = "01 dbo.usp_LoggingHandle.sql"
        'arrScripts(AddOne()) = "01 dbo.usp_ReportArgumentHandle.sql"
        'arrScripts(AddOne()) = "01 dbo.usp_ReportFieldHandle.sql"
        'arrScripts(AddOne()) = "01 dbo.usp_ReportFieldsGet.sql"
        'arrScripts(AddOne()) = "01 dbo.usp_ReportHandle.sql"
        arrScripts(AddOne()) = "01 dbo.usp_ScheduleCreate.sql"
        arrScripts(AddOne()) = "01 dbo.usp_Test_LinkedServer.sql"

        arrScripts(AddOne()) = "02 dbo.usp_Enum_Servers.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Monitor_Errorlogs.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Report_Databases.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Report_DataSpaces.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Report_Jobs.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Report_Logins.sql"

        arrScripts(AddOne()) = "03 dbo.usp_CycleErrorlogs.sql"
        arrScripts(AddOne()) = "03 dbo.usp_DefragIndexes.sql"
        arrScripts(AddOne()) = "03 dbo.usp_Report_Backups.sql"
        arrScripts(AddOne()) = "03 dbo.usp_Report_DiskSpaces.sql"
        arrScripts(AddOne()) = "03 dbo.usp_Report_Errorlogs.sql"
        arrScripts(AddOne()) = "03 dbo.usp_Report_Object_Owner.sql"
        arrScripts(AddOne()) = "03 dbo.usp_Report_Servers.sql"

        'Logins
        'arrScripts(AddOne()) = "01 Main Login.sql"

        'Data
        'arrScripts(AddOne()) = "Insert Standard Data.sql"
        'arrScripts(AddOne()) = "WeekDays.sql"
        'arrScripts(AddOne()) = "LogFile.sql"

        'Reports
        'arrScripts(AddOne()) = "01 dbo.usp_Report_SomeReport.sql"

        ReDim Preserve arrScripts(intCounter)

        Return arrScripts
        'Return intCounter
    End Function

    Public Function GetDBAList() As Array
        Dim arrScripts(100) As String
        intCounter = 0

        arrScripts(intCounter) = "01 dbo.usp_ConstraintState.sql"
        arrScripts(AddOne()) = "01 dbo.usp_Manage_Jobs.sql"
        arrScripts(AddOne()) = "01 dbo.usp_TriggerState.sql"
        arrScripts(AddOne()) = "01 dbo.usp_TruncateTable.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Disable_all_const_in_db.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Disable_all_Triggers_in_db.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Enable_all_const_in_db.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Enable_all_Triggers_in_db.sql"
        arrScripts(AddOne()) = "02 dbo.usp_Truncate_all_Tables_in_db.sql"
        arrScripts(AddOne()) = "03 dbo.usp_Disable_all_BusinessRules.sql"
        arrScripts(AddOne()) = "03 dbo.usp_Enable_all_BusinessRules.sql"
        arrScripts(AddOne()) = "04 dbo.usp_InitDB.sql"

        ReDim Preserve arrScripts(intCounter)

        Return arrScripts
    End Function

    Public Function GetTemplateList() As Array
        Dim arrScripts(100) As String
        intCounter = 0

        'arrScripts(intCounter) = "Sequenchel.xml"
        'arrScripts(AddOne()) = "TrackManager.xml"

        ReDim Preserve arrScripts(intCounter)

        Return arrScripts
    End Function

    Public Function GetVersion(ByVal strVersion As String) As String

        Dim verDatabase As New Version(strVersion)
        If verDatabase.CompareTo(My.Application.Info.Version) = -1 Then
            'Upgrade database
            Dim verSDBA2001 As New Version("2.0.0.1")
            If verDatabase.CompareTo(verSDBA2001) = -1 Then
                Return "2.0.0.1"
            End If

            Dim verSDBA3011 As New Version("3.0.1.1")
            If verDatabase.CompareTo(verSDBA3011) = -1 Then
                Return "3.0.1.1"
            End If
            Dim verSDBA3030 As New Version("3.0.3.0")
            If verDatabase.CompareTo(verSDBA3030) = -1 Then
                Return "3.0.3.0"
            End If
            Dim verSDBA3040 As New Version("3.0.4.0")
            If verDatabase.CompareTo(verSDBA3040) = -1 Then
                Return "3.0.4.0"
            End If
            Dim verSDBA3100 As New Version("3.1.0.0")
            If verDatabase.CompareTo(verSDBA3100) = -1 Then
                Return "3.1.0.0"
            End If
            Dim verSDBA3120 As New Version("3.1.2.0")
            If verDatabase.CompareTo(verSDBA3120) = -1 Then
                Return "3.1.2.0"
            End If
            Dim verSDBA3130 As New Version("3.1.3.0")
            If verDatabase.CompareTo(verSDBA3130) = -1 Then
                Return "3.1.3.0"
            End If
            Dim verSDBA3200 As New Version("3.2.0.0")
            If verDatabase.CompareTo(verSDBA3200) = -1 Then
                Return "3.2.0.0"
            End If
            Dim verSDBA3300 As New Version("3.3.0.0")
            If verDatabase.CompareTo(verSDBA3300) = -1 Then
                Return "3.3.0.0"
            End If
            Dim verSDBA33015 As New Version("3.3.0.15")
            If verDatabase.CompareTo(verSDBA33015) = -1 Then
                Return "3.3.0.15"
            End If
            Dim verSDBA3408 As New Version("3.4.0.8")
            If verDatabase.CompareTo(verSDBA3408) = -1 Then
                Return "3.4.0.8"
            End If
        Else
            'Return strVersion
        End If

        Return Nothing
        'Return strVersion
    End Function

    Public Function GetNewList(ByVal strVersion As String) As Array
        Dim arrScripts(1, 100) As String
        intCounter = 0

        If strVersion = "2.0.0.1" Then
            'arrScripts(0, intCounter) = "dbo.StoredProcedure1"
            'arrScripts(1, intCounter) = "CREATE"
            'arrScripts(0, AddOne()) = "dbo.StoredProcedure2.sql"
            'arrScripts(1, intCounter) = "ALTER"
            'arrScripts(0, AddOne()) = "dbo.Drops.sql"
            'arrScripts(1, intCounter) = "DROP"
            'arrScripts(0, AddOne()) = "dbo.NewData.sql"
            'arrScripts(1, intCounter) = "INSERT"
            'arrScripts(0, AddOne()) = "dbo.UpdateVersion.sql"
            'arrScripts(1, intCounter) = "SELECT"
        End If

        If strVersion = "3.0.1.1" Then
            arrScripts(0, intCounter) = "03 dbo.usp_Report_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
        End If

        If strVersion = "3.0.3.0" Then
            arrScripts(0, intCounter) = "02 dbo.usp_Report_DataSpaces.sql"
            arrScripts(1, intCounter) = "ALTER"
        End If

        If strVersion = "3.0.4.0" Then
            arrScripts(0, intCounter) = "01 dbo.usp_ScheduleCreate.sql"
            arrScripts(1, intCounter) = "CREATE"
        End If

        If strVersion = "3.1.0.0" Then
            arrScripts(0, intCounter) = "02 dbo.usp_Report_DataSpaces.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_Databases.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_DiskSpaces.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
        End If

        If strVersion = "3.1.2.0" Then
            arrScripts(0, intCounter) = "dbo.udf_CorrectCommand.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "3110.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "01 dbo.tbl_ObjectOwners.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "dbo.vw_DiskUseDaysLeft.sql"
            arrScripts(1, intCounter) = "ALTER"

            arrScripts(0, AddOne()) = "02 dbo.usp_Enum_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "02 dbo.usp_Monitor_Errorlogs.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_Databases.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_DataSpaces.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_Logins.sql"
            arrScripts(1, intCounter) = "ALTER"

            arrScripts(0, AddOne()) = "03 dbo.usp_Report_DiskSpaces.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Object_Owner.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Errorlogs.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Backups.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_CycleErrorlogs.sql"
            arrScripts(1, intCounter) = "ALTER"

        End If

        If strVersion = "3.1.3.0" Then
            arrScripts(0, intCounter) = "3130.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "dbo.vw_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "02 dbo.usp_Monitor_Errorlogs.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Errorlogs.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "dbo.vw_RecentErrors.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_DataSpaces.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "dbo.udf_CorrectCommand.sql"
            arrScripts(1, intCounter) = "ALTER"
        End If

        If strVersion = "3.2.0.0" Then
            arrScripts(0, intCounter) = "dbo.vw_ObjectOwners.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "dbo.vw_DataSpaces.sql"
            arrScripts(1, intCounter) = "CREATE"
        End If

        If strVersion = "3.3.0.0" Then
            arrScripts(0, intCounter) = "3300.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_Databases.sql"
            arrScripts(1, intCounter) = "ALTER"

        End If

        If strVersion = "3.3.0.15" Then
            arrScripts(0, intCounter) = "33015.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_Databases.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_DefragIndexes.sql"
            arrScripts(1, intCounter) = "ALTER"
        End If

        If strVersion = "3.4.0.8" Then
            arrScripts(0, intCounter) = "3400.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "dbo.vw_Servers.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "03 dbo.usp_Report_Errorlogs.sql"
            arrScripts(1, intCounter) = "ALTER"
            arrScripts(0, AddOne()) = "01 dbo.tbl_Jobs.sql"
            arrScripts(1, intCounter) = "CREATE"
            arrScripts(0, AddOne()) = "02 dbo.usp_Report_Jobs.sql"
            arrScripts(1, intCounter) = "CREATE"
        End If

        ReDim Preserve arrScripts(1, intCounter)
        Return arrScripts
        'Return intCounter
    End Function

    Public Function GetScript(ByVal strScriptName As String) As String
        Dim myAssembly As System.Reflection.Assembly
        myAssembly = Me.GetType.Assembly
        'Dim MydbRef As New SDBADB.DBRef
        'myAssembly = System.Reflection.Assembly.GetAssembly(MydbRef.GetType)

        'Dim name As String = "TMDB.01 Create Database.sql"
        strScriptName = "SDBA." & strScriptName
        Dim returnValue As Stream
        returnValue = myAssembly.GetManifestResourceStream(strScriptName)

        If returnValue Is Nothing Then Return "-1"

        Dim stream_reader As New System.IO.StreamReader(returnValue)
        Return (stream_reader.ReadToEnd())
        stream_reader.Close()

    End Function

    Private Function ParseVersion(ByVal strVersion As String) As Version
        Dim dbMajor As String = "0"
        Dim dbMinor As String = "0"
        Dim dbBuild As String = "0"
        Dim dbRevision As String = "0"
        Dim intpoint0 As Integer
        Dim intPoint1 As Integer
        Dim intPoint2 As Integer

        intpoint0 = strVersion.LastIndexOf(".")

        intPoint1 = 0
        intPoint2 = strVersion.IndexOf(".", intPoint1)
        If intPoint2 = -1 Then intPoint2 = strVersion.Length
        dbMajor = strVersion.Substring(intPoint1, intPoint2 - intPoint1)

        intPoint1 = intPoint2 + 1
        If intPoint1 < strVersion.Length Then
            intPoint2 = strVersion.IndexOf(".", intPoint1)
            If intPoint2 = -1 Then intPoint2 = strVersion.Length
            dbMinor = strVersion.Substring(intPoint1, intPoint2 - intPoint1)
        End If

        intPoint1 = intPoint2 + 1
        If intPoint1 < strVersion.Length Then
            intPoint2 = strVersion.IndexOf(".", intPoint1)
            If intPoint2 = -1 Then intPoint2 = strVersion.Length
            dbBuild = strVersion.Substring(intPoint1, intPoint2 - intPoint1)
        End If

        intPoint1 = intPoint2 + 1
        If intPoint1 < strVersion.Length Then
            intPoint2 = strVersion.Length
            If intPoint2 = -1 Then intPoint2 = strVersion.Length
            dbRevision = strVersion.Substring(intPoint1, intPoint2 - intPoint1)
        End If

        Dim verDatabase As New Version(dbMajor, dbMinor, dbBuild, dbRevision)
        Return verDatabase
    End Function
End Class
