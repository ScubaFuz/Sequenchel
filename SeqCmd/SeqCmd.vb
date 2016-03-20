Imports SeqCore

Module SeqCmd
    Friend CurVar As New Variables
    Friend CurStatus As New CurrentStatus
    Friend Core As New SeqCore.Core

    Sub Main()
        ParseCommands()

    End Sub

    Friend Sub ParseCommands()
        Dim intLength As Integer = 0

        For Each Command As String In My.Application.CommandLineArgs
            Dim intPosition As Integer = Command.IndexOf(":")
            If intPosition < 1 Then intPosition = Command.Length
            Dim strCommand As String = Command.ToLower.Substring(0, intPosition)
            Select Case strCommand
                Case "/silent"
                    'Start wihtout any windows / forms
                Case "/debug"
                    CurVar.DebugMode = True
                Case "/control"
                    CurStatus.Status = CurrentStatus.StatusList.ControlSearch
                Case "/dev"
                    CurVar.DevMode = True
                Case "/noencryption"
                    CurVar.Encryption = False
                Case "/securityoverride"
                    If Command.Length > intPosition + 1 Then
                        CurVar.OverridePassword = Core.Encrypt(Command.Substring(intPosition + 1, Command.Length - (intPosition + 1)))
                    End If
                Case "/report"
                    'Open Report window directly
                Case "/connection"
                    'Use the chosen connection
                Case "/tableset"
                    'Use the chosen TableSet
                Case "/table"
                    'Use the chosen Table
                Case "/reportname"
                    'Open the report if found
                Case "/exportfile"
                    'Export the report to the chosen file
                Case "/import"
                    'open the Import window
                Case "/importfile"
                    'Export the report to the chosen file
            End Select
        Next

        'If My.Application.CommandLineArgs.Contains("/debug") Then
        '    DebugMode = True
        '    MessageBox.Show("Running in Debug Mode")
        'End If
        'If My.Application.CommandLineArgs.Contains("/control") Then
        '    CurStatus.Status = CurrentStatus.StatusList.ControlSearch
        'End If
        'If My.Application.CommandLineArgs.Contains("/dev") Then
        '    DevMode = True
        'End If
        'If My.Application.CommandLineArgs.Contains("/noencryption") Then
        '    blnEncryption = False
        'End If

    End Sub


End Module
