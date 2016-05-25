Public Class BaseCode
    Public curVar As New Variables
    Public curStatus As New CurrentStatus
    Public dhdText As New DataHandler.txt

    Friend Sub ParseCommands(StringCollection As System.Collections.ReadOnlyCollectionBase)
        Dim intLength As Integer = 0

        For Each Command As String In StringCollection
            Dim intPosition As Integer = Command.IndexOf(":")
            Dim strInput As String = ""
            If intPosition < 0 Then
                intPosition = Command.Length
            Else
                strInput = Command.Substring(intPosition + 1, Command.Length - (intPosition + 1))
            End If
            Dim strCommand As String = Command.ToLower.Substring(0, intPosition)
            Select Case strCommand
                Case "/silent"
                    'Start wihtout any windows / forms
                Case "/debug"
                    curVar.DebugMode = True
                    'Case "/control"
                    '    CurStatus.Status = CurrentStatus.StatusList.ControlSearch
                Case "/dev"
                    curVar.DevMode = True
                    'Case "/noencryption"
                    '    CurVar.Encryption = False
                Case "/securityoverride"
                    If Command.Length > intPosition + 1 Then
                        curVar.OverridePassword = dhdText.MD5Encrypt(Command.Substring(intPosition + 1, Command.Length - (intPosition + 1)))
                    End If
                Case "/control"
                    curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
                Case "/noencryption"
                    curVar.Encryption = False
                Case "/report"
                    'Run Report
                    curStatus.RunReport = True
                Case "/connection"
                    'Use the chosen connection
                    curStatus.Connection = strInput
                Case "/tableset"
                    'Use the chosen TableSet
                    curStatus.TableSet = strInput
                Case "/table"
                    'Use the chosen Table
                    curStatus.Table = strInput
                    'ImportTable = strInput
                Case "/reportname"
                    'Open the report if found
                    curStatus.Report = strInput
                Case "/exportfile"
                    'Export the report to the chosen file
                    dhdText.ExportFile = strInput
                Case "/import"
                    'Run Import
                    curStatus.RunImport = True
                Case "/importfile"
                    'Export the report to the chosen file
                    dhdText.ImportFile = strInput
                Case "/converttotext"
                    'Export the report to the chosen file
                    curVar.ConvertToText = strInput
                Case "/converttonull"
                    'Export the report to the chosen file
                    curVar.ConvertToNull = strInput
                Case "/hasheaders"
                    'Export the report to the chosen file
                    curVar.HasHeaders = strInput
                Case "/delimiter"
                    'Export the report to the chosen file
                    curVar.Delimiter = strInput
                Case "/emailrecipient"
                    'Export the report to the chosen file
                    dhdText.SmtpRecipient = strInput
            End Select

        Next

    End Sub

End Class
