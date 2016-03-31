Imports System.Xml
Imports System.IO

Module SeqCmd
    Friend Core As New SeqCore.Core
    Friend SeqData As New SeqCore.Data

    Friend xmlSDBASettings As New XmlDocument
    Friend xmlGeneralSettings As New XmlDocument
    Friend xmlConnections As New XmlDocument
    Friend xmlTableSets As New XmlDocument
    Friend xmlTables As New XmlDocument
    Friend xmlReports As New XmlDocument

    Friend RunReport As Boolean = False
    Friend RunImport As Boolean = False
    'Friend ConvertToText As Boolean = False
    Friend ImportTable As String = ""

    Dim lstConnections As New List(Of String)
    Dim lstTableSets As List(Of String)
    Dim lstTables As List(Of String)
    Dim lstReports As List(Of String)

    Sub Main()
        ParseCommands()
        SeqData.SetDefaults()
        Dim strReturn As String = SeqData.LoadSDBASettingsXml(xmlSDBASettings)
        If strReturn.Length > 0 Then
            Console.WriteLine(strReturn)
            Console.ReadLine()
            Exit Sub
        End If
        Dim strReturnGen As String = SeqData.LoadGeneralSettingsXml(xmlGeneralSettings)
        If strReturnGen.Length > 0 Then
            Console.WriteLine(strReturnGen)
            Console.ReadLine()
            Exit Sub
        End If

        LoadConnections()
        SeqData.LoadConnection(xmlConnections, SeqData.curStatus.Connection)
        LoadTableSets()
        SeqData.LoadTableSet(xmlTableSets, SeqData.curStatus.TableSet)
        LoadTables()

        If RunImport = True And SeqData.dhdText.ImportFile.Length > 0 And SeqData.curStatus.Table.Length > 0 Then
            'Import file
            If SeqData.dhdText.ImportFile.Contains("\") = False Then
                Console.WriteLine("You need to provide a valid filepath and filename")
                Console.ReadLine()
                Environment.Exit(0)
            End If
            Dim dtsInput As DataSet = Nothing
            If SeqData.dhdText.ImportFile.Contains("*") Then
                If ImportTable <> "" And ImportTable = SeqData.curStatus.Table Then
                    ImportFiles(SeqData.dhdText.ImportFile)
                End If
            Else
                dtsInput = ImportFile(SeqData.dhdText.ImportFile)
                Dim intRecords As Integer = 0
                If ImportTable <> "" And ImportTable = SeqData.curStatus.Table Then
                    intRecords = UploadFile(dtsInput)
                    Console.WriteLine(intRecords & " Records Uploaded")
                End If
                If Not SeqData.dhdText.ExportFile Is Nothing AndAlso RunReport = False AndAlso SeqData.dhdText.ExportFile.Length > 0 Then
                    'export imported file
                    SeqData.ExportFile(dtsInput, SeqData.dhdText.ExportFile, SeqData.curVar.ConvertToText, SeqData.curVar.ConvertToNull, SeqData.curVar.ShowFile, SeqData.curVar.HasHeaders, SeqData.curVar.Delimiter, SeqData.curVar.QuoteValues, SeqData.curVar.CreateDir)
                End If
            End If
        End If
        If Not SeqData.dhdText.ExportFile Is Nothing AndAlso RunReport = True AndAlso SeqData.dhdText.ExportFile.Length > 0 Then
            'Export Report
            LoadReports()
            Dim strQuery As String = SeqData.ReportQueryBuild(xmlReports, xmlTables, SeqData.curStatus.Report, SeqData.curVar.DateTimeStyle)
            Dim dtsData As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True, 5)
            If dtsData Is Nothing Then Environment.Exit(0)
            SeqData.ExportFile(dtsData, SeqData.dhdText.ExportFile, SeqData.curVar.ConvertToText, SeqData.curVar.ConvertToNull, SeqData.curVar.ShowFile, SeqData.curVar.HasHeaders, SeqData.curVar.Delimiter, SeqData.curVar.QuoteValues, SeqData.curVar.CreateDir)
            If SeqData.dhdText.SmtpRecipient.Length > 0 AndAlso SeqData.dhdText.SmtpRecipient.Contains("@") Then
                'Email Report
                Dim strRecepientName As String = SeqData.dhdText.SmtpRecipient.Substring(0, SeqData.dhdText.SmtpRecipient.IndexOf("@"))
                Dim strSenderName As String = SeqData.dhdText.SmtpReply.Substring(0, SeqData.dhdText.SmtpReply.IndexOf("@"))
                Dim strBody As String = "Sequenchel Report: " & SeqData.curStatus.Report
                SeqData.dhdText.SendSMTP(SeqData.dhdText.SmtpReply, strSenderName, SeqData.dhdText.SmtpRecipient, strRecepientName, SeqData.dhdText.SmtpReply, strSenderName, SeqData.curStatus.Report, strBody, SeqData.dhdText.ExportFile)
            End If
        End If
    End Sub

    Public Function GetVersion(strPart As String) As String
        If (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) Then
            Dim ver As Version
            ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
            Select Case strPart
                Case "M"
                    Return String.Format("{0}", ver.Major)
                Case "m"
                    Return String.Format("{0}.{1}", ver.Major, ver.Minor)
                Case "B"
                    Return String.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build)
                Case "R"
                    Return String.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision)
                Case Else
                    Return String.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision)
            End Select
        Else
            Select Case strPart
                Case "M"
                    Return My.Application.Info.Version.Major
                Case "m"
                    Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor
                Case "B"
                    Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
                Case "R"
                    Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
                Case Else
                    Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
            End Select
        End If
    End Function

    Friend Sub ParseCommands()
        Dim intLength As Integer = 0

        For Each Command As String In My.Application.CommandLineArgs
            Dim intPosition As Integer = Command.IndexOf(":")
            Dim strInput As String = ""
            If intPosition < 1 Then
                intPosition = Command.Length
            Else
                strInput = Command.Substring(intPosition + 1, Command.Length - (intPosition + 1))
            End If
            Dim strCommand As String = Command.ToLower.Substring(0, intPosition)
            Select Case strCommand
                Case "/silent"
                    'Start wihtout any windows / forms
                Case "/debug"
                    SeqData.curVar.DebugMode = True
                    'Case "/control"
                    '    CurStatus.Status = CurrentStatus.StatusList.ControlSearch
                Case "/dev"
                    SeqData.curVar.DevMode = True
                    'Case "/noencryption"
                    '    CurVar.Encryption = False
                Case "/securityoverride"
                    If Command.Length > intPosition + 1 Then
                        SeqData.curVar.OverridePassword = SeqData.dhdText.MD5Encrypt(Command.Substring(intPosition + 1, Command.Length - (intPosition + 1)))
                    End If
                Case "/report"
                    'Run Report
                    RunReport = True
                Case "/connection"
                    'Use the chosen connection
                    SeqData.curStatus.Connection = strInput
                Case "/tableset"
                    'Use the chosen TableSet
                    SeqData.curStatus.TableSet = strInput
                Case "/table"
                    'Use the chosen Table
                    SeqData.curStatus.Table = strInput
                    ImportTable = strInput
                Case "/reportname"
                    'Open the report if found
                    SeqData.curStatus.Report = strInput
                Case "/exportfile"
                    'Export the report to the chosen file
                    SeqData.dhdText.ExportFile = strInput
                Case "/import"
                    'Run Import
                    RunImport = True
                Case "/importfile"
                    'Export the report to the chosen file
                    SeqData.dhdText.ImportFile = strInput
                Case "/converttotext"
                    'Export the report to the chosen file
                    SeqData.curVar.ConvertToText = strInput
                Case "/converttonull"
                    'Export the report to the chosen file
                    SeqData.curVar.ConvertToNull = strInput
                Case "/hasheaders"
                    'Export the report to the chosen file
                    SeqData.curVar.HasHeaders = strInput
                Case "/delimiter"
                    'Export the report to the chosen file
                    SeqData.curVar.Delimiter = strInput
                Case "/EmailRecepient"
                    'Export the report to the chosen file
                    SeqData.dhdText.SmtpRecipient = strInput
            End Select
        Next

    End Sub

    Friend Sub LoadConnections()
        lstConnections = SeqData.LoadConnectionsXml(xmlConnections)
        If lstConnections Is Nothing Then Environment.Exit(0)
        If lstConnections.Contains(SeqData.curStatus.Connection) = False Or SeqData.curStatus.Connection = "" Then
            If SeqData.curVar.ConnectionDefault.Length > 0 Then
                SeqData.curStatus.Connection = SeqData.curVar.ConnectionDefault
            Else
                Console.WriteLine("The specified Connection was not found. please check your settings")
                Console.ReadLine()
                Environment.Exit(0)
            End If
        End If
    End Sub

    Friend Sub LoadTableSets()
        lstTableSets = SeqData.LoadTableSetsXml(xmlTableSets)
        If lstTableSets Is Nothing Then Exit Sub
        If lstTableSets.Contains(SeqData.curStatus.TableSet) = False Or SeqData.curStatus.TableSet = "" Then
            If SeqData.curVar.TableSetDefault.Length > 0 Then
                SeqData.curStatus.TableSet = SeqData.curVar.TableSetDefault
            Else
                Console.WriteLine("The specified TableSet was not found. please check your settings")
                Console.ReadLine()
                Environment.Exit(0)
            End If
        End If
    End Sub

    Friend Sub LoadTables()
        lstTables = SeqData.LoadTablesXml(xmlTables)
        If lstTables Is Nothing Then Exit Sub
        If SeqData.curVar.TableDefault = SeqData.curStatus.Table And ImportTable <> SeqData.curStatus.Table And ImportTable <> "" Then
            Console.WriteLine("The specified Table was not found. please check your settings")
            Console.ReadLine()
            Environment.Exit(0)
        End If
    End Sub

    Friend Sub LoadReports()
        lstReports = SeqData.LoadReportsXml(xmlReports)
        If lstReports Is Nothing Then Environment.Exit(0)
        If lstReports.Contains(SeqData.curStatus.Report) = False Or SeqData.curStatus.Report = "" Then
            Console.WriteLine("The specified Report was not found. please check your settings")
            Console.ReadLine()
            Environment.Exit(0)
        End If
    End Sub

    Friend Sub ImportFiles(strImportFiles As String)
        Dim strFolder As String = strImportFiles.Substring(0, strImportFiles.LastIndexOf("\") + 1)
        Dim strFileFilter As String = strImportFiles.Substring(strImportFiles.LastIndexOf("\") + 1, strImportFiles.Length - (strImportFiles.LastIndexOf("\") + 1))
        Dim FilesArray As ArrayList = SeqData.dhdText.GetFiles(strFolder, strFileFilter)
        For Each strFile As String In FilesArray
            Console.WriteLine(strFile)
            Dim dtsInput As DataSet = ImportFile(strFile)
            Dim intRecords As Integer = UploadFile(dtsInput)
            Console.WriteLine(intRecords & " Records Uploaded")
        Next
    End Sub

    Friend Function ImportFile(strImportFile As String) As DataSet
        Dim dtsImport As DataSet = SeqData.ImportFile(strImportFile, SeqData.curVar.HasHeaders, SeqData.curVar.Delimiter)
        Return dtsImport
    End Function

    Friend Function UploadFile(dtsInput As DataSet) As Integer
        SeqData.dhdConnection.DataTableName = SeqData.curStatus.Table
        Dim intRecords As Integer = SeqData.SaveToDatabase(SeqData.dhdConnection, dtsInput, SeqData.curVar.ConvertToText)
        If intRecords = -1 Then
            Console.WriteLine(SeqData.dhdConnection.ErrorMessage)
            Console.ReadLine()
            Environment.Exit(0)
        End If
        Return intRecords
    End Function
End Module
