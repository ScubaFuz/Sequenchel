Module General
    Friend basCode As New SeqCore.BaseCode

    Friend tmrShutdown As New Timers.Timer
    'Friend strReport As String = "Sequenchel " & vbTab & " version: " & basCode.GetVersion("B") & "  Licensed by: " & basCode.curVar.LicenseName

    Friend clrOriginal As Color = System.Drawing.SystemColors.Window
    Friend clrControl As Color = System.Drawing.SystemColors.Control
    Friend clrDisabled As Color = System.Drawing.SystemColors.ControlLight
    Friend clrMarked As Color = System.Drawing.Color.LightSkyBlue
    Friend clrWarning As Color = System.Drawing.Color.IndianRed
    Friend clrEmpty As Color = System.Drawing.Color.LemonChiffon

#Region "General"
    Friend Sub LoadLicense(lblTarget As Label)
        If basCode.LoadLicense = False Then
            WriteStatus(basCode.Message.ErrorMessage, 1, lblTarget)
        Else
            WriteStatus("License loaded succesfully", 2, lblTarget)
        End If
        'strReport = "Sequenchel " & vbTab & " version: " & basCode.GetVersion("B") & vbTab & "  Licensed to: " & basCode.curVar.LicenseName
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

    Friend Sub WriteStatus(strStatusText As String, intStatusLevel As Integer, lblTarget As TextBox)
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
        Dim intShutdown As Integer = basCode.curVar.TimedShutdown
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

#Region "ControlDisposal"
    Friend Sub MasterPanelControlsDispose(pnlMaster As Panel, Optional strControlName As String = "")
        For Each pnlTarget In pnlMaster.Controls
            If pnlTarget.Name.Substring(0, 3) = "pnl" Then
                PanelControlsDispose(pnlTarget, strControlName)
            End If
        Next
    End Sub

    Friend Sub PanelControlsDispose(pnlTarget As Panel, Optional strControlName As String = "")
        For intCount As Integer = 1 To pnlTarget.Controls.Count
            If pnlTarget.Controls.Count > 0 Then
                For Each ctrDispose As Object In pnlTarget.Controls
                    'MessageBox.Show(ctrDispose.Name)
                    If ctrDispose.Name Is Nothing Then
                        ctrDispose.Dispose()
                    ElseIf strControlName.ToString = "" Then
                        ctrDispose.Dispose()
                    ElseIf strControlName.ToString = ctrDispose.Name.ToString Then
                        ctrDispose.Dispose()
                    Else
                        Select Case ctrDispose.GetType.ToString
                            Case "CheckField", "ComboField", "LabelField", "TextField", "ManagedSelectField"
                                If strControlName.ToString = ctrDispose.Field.Name.ToString Then
                                    ctrDispose.Dispose()
                                End If
                        End Select
                    End If
                Next
            Else
                Exit Sub
            End If
        Next
        pnlTarget.Height = 30
    End Sub
#End Region

End Module
