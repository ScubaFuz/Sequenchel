Imports System.Xml
Imports System.IO

Module SeqCmd
    Friend basCode As New SeqCore.BaseCode
    'Friend ConvertToText As Boolean = False
    Friend ImportTable As String = ""

    Dim lstConnections As New List(Of String)
    Dim lstTableSets As List(Of String)
    Dim lstTables As List(Of String)
    Dim lstReports As List(Of String)

    Sub Main()
        Console.WriteLine("Program Start " & Now().ToString)
        basCode.SetDefaults()
        If My.Application.CommandLineArgs.Count > 0 Then basCode.ParseCommands(My.Application.CommandLineArgs)
        If basCode.curStatus.QuitApplication = True Then End
        ImportTable = basCode.curStatus.Table
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Default Settings Loaded")
        Dim strReturn As String = basCode.LoadSDBASettingsXml(basCode.xmlSDBASettings)
        If strReturn = False Then
            Console.WriteLine(basCode.ErrorMessage)
            Console.ReadLine()
            Exit Sub
        End If
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Base Settings Loaded")
        Dim strReturnGen As String = basCode.LoadGeneralSettingsXml(basCode.xmlGeneralSettings)
        If strReturnGen = False Then
            Console.WriteLine(basCode.ErrorMessage)
            Console.ReadLine()
            Exit Sub
        End If
        If basCode.curVar.DebugMode = True Then Console.WriteLine("General Settings Loaded")
        basCode.dhdText.LogLocation = "console"

        LoadConnections()
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Connections Loaded")
        basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Connection " & basCode.curStatus.Connection & " loaded")
        LoadTableSets()
        If basCode.curVar.DebugMode = True Then Console.WriteLine("TableSets Loaded")
        basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
        If basCode.curVar.DebugMode = True Then Console.WriteLine("TableSet " & basCode.curStatus.TableSet & " loaded")
        Dim blnTableExists As Boolean = basCode.CheckTable(basCode.dhdConnection, ImportTable)
        LoadTables(blnTableExists, basCode.curStatus.CreateTargetTable)
        If ImportTable <> basCode.curStatus.Table And (blnTableExists = True Or basCode.curStatus.CreateTargetTable = True) Then basCode.curStatus.Table = ImportTable
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Tables Loaded")

        If basCode.curVar.DebugMode = True Then Console.WriteLine("Importmode enabled = " & basCode.curStatus.RunImport)
        If basCode.curStatus.RunImport = True AndAlso basCode.dhdText.ImportFile.Length > 0 AndAlso basCode.curStatus.Table.Length > 0 Then
            ImportRun()
        End If

        If basCode.curVar.DebugMode = True Then Console.WriteLine("Exportmode enabled = " & basCode.curStatus.RunReport)
        If Not basCode.dhdText.ExportFile Is Nothing AndAlso basCode.curStatus.RunReport = True AndAlso basCode.dhdText.ExportFile.Length > 0 Then
            ExportRun()
        End If

        If basCode.curVar.CreateSmartView = True Then
            SmartViewCreate()
        End If
        Console.WriteLine("Program End " & Now().ToString)
    End Sub

    Friend Sub LoadConnections()
        Console.WriteLine("Loading Connection: " & basCode.curStatus.Connection)
        lstConnections = basCode.LoadConnections(basCode.xmlConnections)
        If lstConnections Is Nothing Then Environment.Exit(0)
        If lstConnections.Contains(basCode.curStatus.Connection) = False Or basCode.curStatus.Connection = "" Then
            If basCode.curVar.ConnectionDefault.Length > 0 Then
                basCode.curStatus.Connection = basCode.curVar.ConnectionDefault
                Console.WriteLine("Connection Loaded: " & basCode.curStatus.Connection)
            Else
                Console.WriteLine("The specified Connection was not found. please check your settings")
                'Console.ReadLine()
                Environment.Exit(0)
            End If
        End If
    End Sub

    Friend Sub LoadTableSets()
        Console.WriteLine("Loading TableSet: " & basCode.curStatus.TableSet)
        lstTableSets = basCode.LoadTableSets(basCode.xmlTableSets)
        If lstTableSets Is Nothing Then Exit Sub
        If lstTableSets.Contains(basCode.curStatus.TableSet) = False Or basCode.curStatus.TableSet = "" Then
            If basCode.curVar.TableSetDefault.Length > 0 Then
                basCode.curStatus.TableSet = basCode.curVar.TableSetDefault
                Console.WriteLine("TableSet Loaded: " & basCode.curStatus.TableSet)
            Else
                Console.WriteLine("The specified TableSet was not found. please check your settings")
                'Console.ReadLine()
                Environment.Exit(0)
            End If
        End If
    End Sub

    Friend Sub LoadTables(blnTableExists As Boolean, blnCreateTargetTable As Boolean)
        Console.WriteLine("Loading Table: " & basCode.curStatus.Table)
        lstTables = basCode.LoadTables(basCode.xmlTables, False)
        If lstTables Is Nothing And blnTableExists = False And blnCreateTargetTable = False Then
            Console.WriteLine("The specified Table was not found. please check your settings")
            Environment.Exit(0)
        End If
        If basCode.curVar.TableDefault = basCode.curStatus.Table And ImportTable <> basCode.curStatus.Table And ImportTable <> "" And blnTableExists = False And blnCreateTargetTable = False Then
            Console.WriteLine("The specified Table was not found. please check your settings")
            Environment.Exit(0)
        End If
    End Sub

    Friend Sub ImportRun()
        If basCode.dhdText.ImportFile.Contains("\") = False Then
            Console.WriteLine("You need to provide a valid filepath and filename for import.")
            Console.WriteLine("Press Enter to Exit.")
            Console.ReadLine()
            Environment.Exit(0)
        End If
        Dim dtsInput As DataSet = Nothing

        If basCode.curStatus.ClearTargetTable = 1 Then
            'Clear target table before inporting data
            If ImportTable <> "" And ImportTable = basCode.curStatus.Table Then
                basCode.dhdConnection.DataTableName = basCode.GetTableNameFromAlias(basCode.xmlTables, basCode.curStatus.Table)
                If basCode.curVar.DebugMode = True Then Console.WriteLine("Table Name set to: " & basCode.dhdConnection.DataTableName)
                Console.WriteLine("Clearing Target Table")
                basCode.ClearTargetTable(basCode.dhdConnection)
            End If
        End If

        If basCode.dhdText.ImportFile.Contains("*") Or basCode.dhdText.ImportFile.Contains("?") Then
            If ImportTable <> "" And ImportTable = basCode.curStatus.Table Then
                Console.WriteLine("Importing multiple files")
                ImportFiles(basCode.dhdText.ImportFile)
            End If
        Else
            If basCode.curVar.DebugMode = True Then Console.WriteLine("File to Import: " & basCode.dhdText.ImportFile)
            dtsInput = ImportFile(basCode.CheckFilePath(basCode.dhdText.ImportFile))
            If basCode.curVar.DebugMode = True Then Console.WriteLine("Imported file is OK: " & basCode.dhdText.DatasetCheck(dtsInput))
            Dim intRecords As Integer = 0
            If ImportTable <> "" And ImportTable = basCode.curStatus.Table Then
                If basCode.curVar.DebugMode = True Then Console.WriteLine("Import to Table: " & basCode.curStatus.Table)
                intRecords = UploadFile(dtsInput, basCode.dhdText.ImportFile)
                'Console.WriteLine(intRecords & " Records Uploaded")
            Else
                If basCode.curVar.DebugMode = True Then Console.WriteLine("No import: ImportTable = " & ImportTable & " StatusTable = " & basCode.curStatus.Table)
            End If
            If Not basCode.dhdText.ExportFile Is Nothing AndAlso basCode.curVar.LargeFile = False AndAlso basCode.curStatus.RunReport = False AndAlso basCode.dhdText.ExportFile.Length > 0 Then
                If basCode.curVar.DebugMode = True Then Console.WriteLine("Imported file export to: " & basCode.dhdText.ExportFile)
                'export imported file
                basCode.ExportFile(dtsInput, basCode.dhdText.ExportFile, basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull, basCode.curVar.ShowFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter, basCode.curVar.QuoteValues, basCode.curVar.CreateDir)
            End If
        End If
    End Sub

    Friend Sub ExportRun()
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Exportfile = " & basCode.dhdText.ExportFile)
        'Export Report
        Dim strExportFile As String = basCode.GetExportFileName(basCode.dhdText.ExportFile)
        Console.WriteLine("Exporting Report: " & basCode.curStatus.Report & " to " & strExportFile)
        Dim strQuery As String = ""
        If LoadReports() = True Then
            strQuery = basCode.ReportQueryBuild(basCode.xmlReports, basCode.xmlTables, basCode.curStatus.Report, basCode.curVar.DateTimeStyle)
        Else
            If basCode.curVar.LargeFile = True Then
                basCode.dhdText.TableToCsv(basCode.dhdConnection.SqlConnection, basCode.curStatus.Table, strExportFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter, basCode.curVar.QuoteValues)
                Exit Sub
            End If
            strQuery = "SELECT * FROM " & basCode.curStatus.Table
        End If
        Dim dtsData As DataSet = basCode.QueryDb(basCode.dhdConnection, strQuery, True, 5)
        For Each table As Data.DataTable In dtsData.Tables
            Console.WriteLine("Exporting " & dtsData.Tables(0).Rows.Count & " rows from " & table.TableName)
        Next
        'If dtsData Is Nothing Then Environment.Exit(0)
        basCode.ExportFile(dtsData, strExportFile, basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull, basCode.curVar.ShowFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter, basCode.curVar.QuoteValues, basCode.curVar.CreateDir)
        If basCode.dhdText.SmtpRecipient.Length > 0 AndAlso basCode.dhdText.SmtpRecipient.Contains("@") Then
            'Email Report
            Console.WriteLine("Emailing Report: " & basCode.curStatus.Report & " to " & basCode.dhdText.SmtpRecipient)
            Dim strRecepientName As String = basCode.dhdText.SmtpRecipient.Substring(0, basCode.dhdText.SmtpRecipient.IndexOf("@"))
            Dim strSenderName As String = basCode.dhdText.SmtpReply.Substring(0, basCode.dhdText.SmtpReply.IndexOf("@"))
            Dim strBody As String = "Sequenchel Report: " & basCode.curStatus.Report
            basCode.dhdText.SendSMTP(basCode.dhdText.SmtpReply, strSenderName, basCode.dhdText.SmtpRecipient, strRecepientName, basCode.dhdText.SmtpReply, strSenderName, basCode.curStatus.Report, strBody, strExportFile)
        End If
    End Sub

    Friend Function LoadReports() As Boolean
        lstReports = basCode.LoadReports(basCode.xmlReports)
        If (lstReports Is Nothing OrElse lstReports.Contains(basCode.curStatus.Report) = False OrElse basCode.curStatus.Report = "") Then
            If basCode.curStatus.Report <> basCode.curStatus.Table Then
                Console.WriteLine("The specified Report was not found. please check your settings")
                'Console.ReadLine()
                Environment.Exit(0)
            Else
                Return False
            End If
        End If
        Return True
    End Function

    Friend Sub ImportFiles(strImportFiles As String)
        Dim strFolder As String = strImportFiles.Substring(0, strImportFiles.LastIndexOf("\") + 1)
        Dim strFileFilter As String = strImportFiles.Substring(strImportFiles.LastIndexOf("\") + 1, strImportFiles.Length - (strImportFiles.LastIndexOf("\") + 1))
        Dim FilesArray As ArrayList = basCode.dhdText.GetFiles(strFolder, strFileFilter)
        For Each strFile As String In FilesArray
            Dim dtsInput As DataSet = ImportFile(basCode.CheckFilePath(strFile))
            Dim intRecords As Integer = UploadFile(dtsInput, strFile)
        Next
    End Sub

    Friend Function ImportFile(strImportFile As String) As DataSet
        Console.WriteLine("Importing file: " & strImportFile)
        Dim dtsImport As DataSet = basCode.ImportFile(strImportFile)
        If basCode.ErrorLevel = -1 Then Console.WriteLine("Error: " & basCode.ErrorMessage)
        Return dtsImport
    End Function

    Friend Function UploadFile(dtsInput As DataSet, strFileName As String) As Integer
        Try
            Dim dhdDB As New DataHandler.db
            dhdDB.DataLocation = basCode.dhdConnection.DataLocation
            dhdDB.DatabaseName = basCode.dhdConnection.DatabaseName
            dhdDB.DataTableName = basCode.GetTableNameFromAlias(basCode.xmlTables, basCode.curStatus.Table)
            dhdDB.DataProvider = basCode.dhdConnection.DataProvider
            dhdDB.LoginMethod = basCode.dhdConnection.LoginMethod
            dhdDB.LoginName = basCode.dhdConnection.LoginName
            dhdDB.Password = basCode.dhdConnection.Password

            If basCode.curVar.DebugMode = True Then Console.WriteLine("Check if table exists or can be created")
            Dim blnTargetTableOK As Boolean = basCode.CreateTargetTable(dhdDB, dtsInput)
            If blnTargetTableOK = False Then
                Console.WriteLine("The table was not found or created: " & dhdDB.ErrorMessage)
                Environment.Exit(0)
            End If

            If basCode.curStatus.ClearTargetTable = 2 Then
                'Clear target table before uploading data
                If ImportTable <> "" And ImportTable = basCode.curStatus.Table Then
                    If basCode.curVar.DebugMode = True Then Console.WriteLine("Table Name set to: " & dhdDB.DataTableName)
                    Console.WriteLine("Clearing Target Table")
                    basCode.ClearTargetTable(dhdDB)
                End If
            End If

            Dim intRecords As Integer = 0
            If basCode.curStatus.ImportAsXml = True Then
                intRecords = basCode.SaveXmlToDatabase(dhdDB, dtsInput, strFileName)
            ElseIf basCode.curVar.LargeFile = True Then
                intRecords = basCode.SaveLargeFileToDatabase(dhdDB, strFileName)
            Else
                intRecords = basCode.SaveToDatabase(dhdDB, dtsInput)
            End If

            If intRecords = -1 Then
                Console.WriteLine("No records uploaded. Error: " & dhdDB.ErrorMessage)
                'Console.ReadLine()
                Environment.Exit(0)
            End If
            Console.WriteLine(intRecords & " Records Uploaded")
            Return intRecords
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            'Console.ReadLine()
            Environment.Exit(0)
            Return 0
        End Try
    End Function

    Friend Function SmartViewCreate()
        If basCode.curVar.TargetSchema.Length = 0 Then basCode.curVar.TargetSchema = basCode.curVar.SourceSchema
        If basCode.curVar.TargetView.Length = 0 Then basCode.curVar.TargetView = "vw_" & basCode.curVar.SourceTable
        If basCode.curVar.SourceSchema.Length = 0 Or basCode.curVar.SourceTable.Length = 0 Then
            Console.WriteLine("Source schema and table/view are required.")
            Return -1
        End If
        If basCode.curVar.LinkedServer.Length = 0 And basCode.curVar.SourceDatabase.Length = 0 Then
            Console.WriteLine("Linked Server or Database name is required.")
            Return -1
        End If

        Dim errorlevel As Integer = basCode.CreateLocalView(basCode.dhdConnection, basCode.curVar.LinkedServer, basCode.curVar.SourceDatabase, basCode.curVar.SourceSchema, basCode.curVar.SourceTable, basCode.curVar.TargetSchema, basCode.curVar.TargetView, basCode.curVar.SourceSystem)

        If errorlevel <> 0 Then
            Console.WriteLine(basCode.ErrorMessage)
            Return -1
        End If
        Console.WriteLine("View " & basCode.curVar.TargetSchema & "." & basCode.curVar.TargetView & " created")
        Return 0
    End Function

End Module
