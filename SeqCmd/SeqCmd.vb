Imports System.Xml
Imports System.IO

Module SeqCmd
    Friend basCode As SeqCore.BaseCode

    Friend RunReport As Boolean = False
    Friend RunImport As Boolean = False
    'Friend ConvertToText As Boolean = False
    Friend ImportTable As String = ""

    Dim lstConnections As New List(Of String)
    Dim lstTableSets As List(Of String)
    Dim lstTables As List(Of String)
    Dim lstReports As List(Of String)

    Sub Main()
        basCode.ParseCommands(My.Application.CommandLineArgs)
        basCode.SetDefaults()
        Dim strReturn As String = basCode.LoadSDBASettingsXml(basCode.xmlSDBASettings)
        If strReturn = False Then
            Console.WriteLine(basCode.ErrorMessage)
            Console.ReadLine()
            Exit Sub
        End If
        Dim strReturnGen As String = basCode.LoadGeneralSettingsXml(basCode.xmlGeneralSettings)
        If strReturnGen = False Then
            Console.WriteLine(basCode.ErrorMessage)
            Console.ReadLine()
            Exit Sub
        End If

        LoadConnections()
        basCode.LoadConnection(basCode.xmlConnections, basCode.curStatus.Connection)
        LoadTableSets()
        basCode.LoadTableSet(basCode.xmlTableSets, basCode.curStatus.TableSet)
        LoadTables()

        If RunImport = True AndAlso basCode.dhdText.ImportFile.Length > 0 AndAlso basCode.curStatus.Table.Length > 0 Then
            'Import file
            If basCode.dhdText.ImportFile.Contains("\") = False Then
                Console.WriteLine("You need to provide a valid filepath and filename")
                Console.ReadLine()
                Environment.Exit(0)
            End If
            Dim dtsInput As DataSet = Nothing
            If basCode.dhdText.ImportFile.Contains("*") Then
                If ImportTable <> "" And ImportTable = basCode.curStatus.Table Then
                    Console.WriteLine("Importing multiple files")
                    ImportFiles(basCode.dhdText.ImportFile)
                End If
            Else
                dtsInput = ImportFile(basCode.CheckFilePath(basCode.dhdText.ImportFile))
                Dim intRecords As Integer = 0
                If ImportTable <> "" And ImportTable = basCode.curStatus.Table Then
                    intRecords = UploadFile(dtsInput)
                    Console.WriteLine(intRecords & " Records Uploaded")
                End If
                If Not basCode.dhdText.ExportFile Is Nothing AndAlso RunReport = False AndAlso basCode.dhdText.ExportFile.Length > 0 Then
                    'export imported file
                    basCode.ExportFile(dtsInput, basCode.dhdText.ExportFile, basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull, basCode.curVar.ShowFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter, basCode.curVar.QuoteValues, basCode.curVar.CreateDir)
                End If
            End If
        End If
        If Not basCode.dhdText.ExportFile Is Nothing AndAlso RunReport = True AndAlso basCode.dhdText.ExportFile.Length > 0 Then
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
                Console.ReadLine()
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
                Console.ReadLine()
                Environment.Exit(0)
            End If
        End If
    End Sub

    Friend Sub LoadTables()
        Console.WriteLine("Loading Table: " & basCode.curStatus.Table)
        lstTables = basCode.LoadTables(basCode.xmlTables, False)
        If lstTables Is Nothing Then Exit Sub
        If basCode.curVar.TableDefault = basCode.curStatus.Table And ImportTable <> basCode.curStatus.Table And ImportTable <> "" Then
            Console.WriteLine("The specified Table was not found. please check your settings")
            Console.ReadLine()
            Environment.Exit(0)
        End If
    End Sub

    Friend Sub LoadReports()
        lstReports = basCode.LoadReports(basCode.xmlReports)
        If lstReports Is Nothing Then Environment.Exit(0)
        If lstReports.Contains(basCode.curStatus.Report) = False Or basCode.curStatus.Report = "" Then
            Console.WriteLine("The specified Report was not found. please check your settings")
            Console.ReadLine()
            Environment.Exit(0)
        End If
    End Sub

    Friend Sub ImportFiles(strImportFiles As String)
        Dim strFolder As String = strImportFiles.Substring(0, strImportFiles.LastIndexOf("\") + 1)
        Dim strFileFilter As String = strImportFiles.Substring(strImportFiles.LastIndexOf("\") + 1, strImportFiles.Length - (strImportFiles.LastIndexOf("\") + 1))
        Dim FilesArray As ArrayList = basCode.dhdText.GetFiles(strFolder, strFileFilter)
        For Each strFile As String In FilesArray
            Dim dtsInput As DataSet = ImportFile(basCode.CheckFilePath(strFile))
            Dim intRecords As Integer = UploadFile(dtsInput)
        Next
    End Sub

    Friend Function ImportFile(strImportFile As String) As DataSet
        Console.WriteLine("Importing file: " & strImportFile)
        Dim dtsImport As DataSet = basCode.ImportFile(strImportFile, basCode.curVar.HasHeaders, basCode.curVar.Delimiter)
        Return dtsImport
    End Function

    Friend Function UploadFile(dtsInput As DataSet) As Integer
        Try
            Dim dhdDB As New DataHandler.db
            dhdDB.DataLocation = basCode.dhdConnection.DataLocation
            dhdDB.DatabaseName = basCode.dhdConnection.DatabaseName
            dhdDB.DataTableName = basCode.dhdConnection.DataTableName
            dhdDB.DataProvider = basCode.dhdConnection.DataProvider
            dhdDB.LoginMethod = basCode.dhdConnection.LoginMethod
            dhdDB.LoginName = basCode.dhdConnection.LoginName
            dhdDB.Password = basCode.dhdConnection.Password

            If dhdDB.DataTableName <> basCode.curStatus.Table Then dhdDB.DataTableName = basCode.curStatus.Table
            Dim intRecords As Integer = basCode.SaveToDatabase(dhdDB, dtsInput, basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull)
            If intRecords = -1 Then
                Console.WriteLine(basCode.dhdConnection.ErrorMessage)
                Console.ReadLine()
                Environment.Exit(0)
            End If
            Console.WriteLine(intRecords & " Records Uploaded")
            Return intRecords
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
            Environment.Exit(0)
            Return 0
        End Try
    End Function
End Module
