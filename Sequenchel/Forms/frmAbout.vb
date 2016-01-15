Public NotInheritable Class frmAbout

    Private Sub frmAbout_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblVersion.Text = "Version: " & GetVersion("R")
        lblCopyright.Text = My.Application.Info.Copyright
        lblLicenseName.Text = "Licensed to: " & strLicenseName
        lblLicenseText.Text = "Sequenchel is free for personal use. Companies are required to purchase a license. " _
            + Environment.NewLine + "Having a license will automatically close this screen at startup. Thank you for playing fair." _
            + Environment.NewLine + "Click anywhere on this screen to close it. Click on the Version number to see the version log."

    End Sub

    Private Sub pnlMain_Click(sender As Object, e As EventArgs) Handles pnlMain.Click
        Me.Close()
    End Sub

    Private Sub lblCopyright_Click(sender As Object, e As EventArgs) Handles lblCopyright.Click
        Me.Close()
    End Sub

    Private Sub lblVersion_Click(sender As Object, e As EventArgs) Handles lblVersion.Click
        If dhdText.CheckFile(Application.StartupPath & "\Resources\SequenchelVersionLog.txt") Then
            System.Diagnostics.Process.Start(Application.StartupPath & "\Resources\SequenchelVersionLog.txt")
            Me.Close()
        Else
            MessageBox.Show("The Version Log was not found" & Environment.NewLine & Application.StartupPath & "\Resources\SequenchelVersionLog.txt")
            Me.Close()
        End If

    End Sub

    Private Sub lblLicenseName_Click(sender As Object, e As EventArgs) Handles lblLicenseName.Click
        Me.Close()
    End Sub

    Private Sub frmAbout_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        If CurVar.CallSplash = True Then
            Me.Close()
        End If
    End Sub

    Private Sub lblLicenseText_Click(sender As Object, e As EventArgs) Handles lblLicenseText.Click
        Me.Close()
    End Sub
End Class
