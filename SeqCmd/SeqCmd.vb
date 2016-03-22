Imports System.Xml

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
    Friend ConvertToText As Boolean = False
    Friend ImportTable As String = ""

    Dim lstConnections As New List(Of String)
    Dim lstTableSets As List(Of String)
    Dim lstTables As List(Of String)
    Dim lstReports As List(Of String)

    Sub Main()
        ParseCommands()
        SetDefaults()
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
            Dim dtsImport As DataSet = SeqData.Excel.ImportExcelFile(SeqData.dhdText.ImportFile)
            If dtsImport Is Nothing Then Environment.Exit(0)
            SeqData.dhdConnection.DataTableName = SeqData.curStatus.Table
            Dim intRecords As Integer = SeqData.SaveToDatabase(dtsImport, ConvertToText)
            If intRecords = -1 Then
                Console.WriteLine(SeqData.dhdConnection.errormessage)
                Console.ReadLine()
                Exit Sub
            End If
        End If
        If Not SeqData.dhdText.ExportFile Is Nothing AndAlso RunReport = True AndAlso SeqData.dhdText.ExportFile.Length > 0 Then
            LoadReports()
            Dim strQuery As String = SeqData.ReportQueryBuild(xmlReports, xmlTables, SeqData.curStatus.Report, SeqData.curVar.DateTimeStyle)
            Dim dtsData As DataSet = SeqData.QueryDb(SeqData.dhdConnection, strQuery, True, 5)
            If dtsData Is Nothing Then Environment.Exit(0)
            SeqData.ExportFile(dtsData, SeqData.dhdText.ExportFile, False)
        End If
    End Sub

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
                        SeqData.curVar.OverridePassword = Core.Encrypt(Command.Substring(intPosition + 1, Command.Length - (intPosition + 1)))
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
                    ConvertToText = strInput
            End Select
        Next

    End Sub

    Friend Sub SetDefaults()
        SeqData.dhdReg.RegistryPath = "Software\Thicor\Sequenchel\3.0"

        SeqData.dhdText.InputFile = "SequenchelDBA.xml"
        SeqData.dhdText.LogFileName = "Sequenchel.Log"
        SeqData.dhdText.LogLevel = 5
        SeqData.dhdText.LogLocation = System.AppDomain.CurrentDomain.BaseDirectory & "LOG"
        SeqData.dhdText.OutputFile = Environment.SpecialFolder.MyDocuments

        SeqData.dhdConnection.LoginMethod = "WINDOWS"
        SeqData.dhdConnection.LoginName = "SDBAUser"
        SeqData.dhdConnection.Password = "SDBAPassword"
        SeqData.dhdConnection.DataLocation = Environment.MachineName & "\SQLEXPRESS"
        SeqData.dhdConnection.DatabaseName = "Sequenchel"
        SeqData.dhdConnection.DataProvider = "SQL"

        SeqData.curStatus.Status = SeqCore.CurrentStatus.StatusList.Search
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

End Module
