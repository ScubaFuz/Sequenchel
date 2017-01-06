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
        If My.Application.CommandLineArgs.Count > 0 Then basCode.ParseCommands(My.Application.CommandLineArgs)
        If basCode.curStatus.QuitApplication = True Then End
        ImportTable = basCode.curStatus.Table
        basCode.SetDefaults()
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
        If ImportTable <> basCode.curStatus.Table And (basCode.CheckTable(basCode.dhdConnection, ImportTable) = True Or basCode.curStatus.CreateTargetTable = True) Then basCode.curStatus.Table = ImportTable
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Tables Loaded")

        If basCode.curVar.DebugMode = True Then Console.WriteLine("Importmode enabled = " & basCode.curStatus.RunImport)
        If basCode.curStatus.RunImport = True AndAlso basCode.dhdText.ImportFile.Length > 0 AndAlso basCode.curStatus.Table.Length > 0 Then
            'Import file
            If basCode.dhdText.ImportFile.Contains("\") = False Then
                Console.WriteLine("You need to provide a valid filepath and filename")
                Console.ReadLine()
                Environment.Exit(0)
            End If
            Dim dtsInput As DataSet = Nothing

            If basCode.curStatus.ClearTargetTable = True Then
                If basCode.curVar.DebugMode = True Then Console.WriteLine("Clearing Target Table")
                'Clear target table before inporting data
                If ImportTable <> "" And ImportTable = basCode.curStatus.Table Then
                    basCode.dhdConnection.DataTableName = basCode.GetTableNameFromAlias(basCode.xmlTables, basCode.curStatus.Table)
                    If basCode.curVar.DebugMode = True Then Console.WriteLine("Table Name set to: " & basCode.dhdConnection.DataTableName)
                    basCode.ClearTargetTable(basCode.dhdConnection)
                End If
            End If

            If basCode.dhdText.ImportFile.Contains("*") Then
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
                If Not basCode.dhdText.ExportFile Is Nothing AndAlso basCode.curStatus.RunReport = False AndAlso basCode.dhdText.ExportFile.Length > 0 Then
                    If basCode.curVar.DebugMode = True Then Console.WriteLine("Imported file export to: " & basCode.dhdText.ExportFile)
                    'export imported file
                    basCode.ExportFile(dtsInput, basCode.dhdText.ExportFile, basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull, basCode.curVar.ShowFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter, basCode.curVar.QuoteValues, basCode.curVar.CreateDir)
                End If
            End If
        End If
        If basCode.curVar.DebugMode = True Then Console.WriteLine("Exportmode enabled = " & basCode.curStatus.RunReport)
        If Not basCode.dhdText.ExportFile Is Nothing AndAlso basCode.curStatus.RunReport = True AndAlso basCode.dhdText.ExportFile.Length > 0 Then
            If basCode.curVar.DebugMode = True Then Console.WriteLine("Exportfile = " & basCode.dhdText.ExportFile)
            'Export Report
            Dim strExportFile As String = basCode.GetExportFileName(basCode.dhdText.ExportFile)
            Console.WriteLine("Exporting Report: " & basCode.curStatus.Report & " to " & strExportFile)
            LoadReports()
            Dim strQuery As String = basCode.ReportQueryBuild(basCode.xmlReports, basCode.xmlTables, basCode.curStatus.Report, basCode.curVar.DateTimeStyle)
            Dim dtsData As DataSet = basCode.QueryDb(basCode.dhdConnection, strQuery, True, 5)
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
        End If
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

    Friend Sub LoadReports()
        lstReports = basCode.LoadReports(basCode.xmlReports)
        If lstReports Is Nothing Then Environment.Exit(0)
        If lstReports.Contains(basCode.curStatus.Report) = False Or basCode.curStatus.Report = "" Then
            Console.WriteLine("The specified Report was not found. please check your settings")
            'Console.ReadLine()
            Environment.Exit(0)
        End If
    End Sub

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
        Dim dtsImport As DataSet = basCode.ImportFile(strImportFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter, basCode.curVar.QuoteValues)
        If basCode.ErrorLevel = -1 Then Console.WriteLine(basCode.ErrorMessage)
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
            Dim intRecords As Integer = 0
            If basCode.curStatus.ImportAsXml = True Then
                intRecords = basCode.SaveXmlToDatabase(dhdDB, dtsInput, strFileName)
            Else
                intRecords = basCode.SaveToDatabase(dhdDB, dtsInput, basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull)
            End If

            If intRecords = -1 Then
                Console.WriteLine("No records uploaded: " & dhdDB.ErrorMessage)
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
End Module
