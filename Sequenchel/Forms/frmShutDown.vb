Public Class frmShutDown

    Friend intTimeLeft As Integer = 60

    Private Sub frmShutDown_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tmrShuttingDown.Interval = 1000
        tmrShuttingDown.Enabled = True
        tmrShuttingDown.Start()
    End Sub

    Private Sub frmShutDown_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Me.TopMost = True
        Me.Activate()
    End Sub

    Private Sub tmrShuttingDown_Tick(sender As Object, e As EventArgs) Handles tmrShuttingDown.Tick
        If intTimeLeft > 0 Then
            intTimeLeft -= 1
            lblCounter.Text = intTimeLeft
            Me.Invalidate()
        End If
        If intTimeLeft <= 0 Then Application.Exit()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Application.Exit()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        tmrShuttingDown.Stop()
        tmrShuttingDown.Enabled = False
        ShutdownDelay()
        Me.Close()
    End Sub

End Class