Module General
    Friend basCode As SeqCore.BaseCode

    Friend tmrShutdown As New Timers.Timer

#Region "General"
    Friend Sub LoadLicense(lblTarget As Label)
        If basCode.LoadLicense = False Then
            WriteStatus(basCode.Message.ErrorMessage, 1, lblTarget)
        Else
            WriteStatus("License loaded succesfully", 2, lblTarget)
        End If
        strReport = "Sequenchel " & vbTab & " version: " & basCode.GetVersion("B") & vbTab & "  Licensed to: " & basCode.curVar.LicenseName

    End Sub

    Friend Sub WriteStatus(strStatusText As String, intStatusLevel As Integer, lblTarget As Label)
        Dim clrStatus As Color = clrControl
        Select Case intStatusLevel
            Case 0
                clrStatus = clrControl
            Case 1
                clrStatus = clrWarning
            Case 2
                clrStatus = clrMarked
            Case 3, 4, 5
                clrStatus = clrControl
        End Select
        lblTarget.Text = strStatusText
        lblTarget.BackColor = clrStatus
    End Sub

#End Region

#Region "Shutdown"
    Friend Sub SetTimer()
        AddHandler tmrShutdown.Elapsed, AddressOf TimedShutdown
        Dim intShutdown As Integer = SeqData.curVar.TimedShutdown
        If intShutdown > 60000 Then intShutdown -= 60000
        If intShutdown > 0 Then tmrShutdown.Interval = intShutdown
        If intShutdown <= 60000 Then
            tmrShutdown.Enabled = False
        ElseIf intShutdown > 0 Then
            tmrShutdown.Enabled = True
            tmrShutdown.Start()
        End If
    End Sub

    Friend Sub ShutdownDelay()
        If tmrShutdown.Enabled = False And tmrShutdown.Interval > 60000 Then
            tmrShutdown.Enabled = True
            tmrShutdown.Start()
        ElseIf tmrShutdown.Enabled = True And tmrShutdown.Interval > 60000 Then
            tmrShutdown.Stop()
            tmrShutdown.Enabled = False
            tmrShutdown.Enabled = True
            tmrShutdown.Start()
        Else
            tmrShutdown.Enabled = False
        End If
    End Sub

    Friend Sub TimedShutdown()
        ShutdownDelay()
        ShowShutdownForm()
    End Sub

    Friend Sub ShowShutdownForm()
        Application.Run(frmShutdown)
    End Sub

#End Region

End Module
