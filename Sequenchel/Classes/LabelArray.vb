Public Class LabelArray
    Inherits System.Collections.CollectionBase
    Private ReadOnly HostForm As System.Windows.Forms.Form
    Private _strName As String

    Public Overloads Sub Add(ByVal AddLabel As Label)
        Me.List.Add(AddLabel)
        AddHandler AddLabel.Click, AddressOf ClickHandler
    End Sub

    Default Friend ReadOnly Property Item(ByVal Index As Integer) As Label
        Get
            Return CType(Me.List.Item(Index), Label)
        End Get
    End Property

    Friend Sub Remove()
        If Me.Count > 1 Then
            HostForm.Controls.Remove(Me(Me.List.Count - 1))
            Me.List.RemoveAt(Me.List.Count - 1)
        End If
    End Sub

    Friend Sub ClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        LabelClickHandler(sender)
    End Sub

    Friend Sub DoubleClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        'LabelClickHandler(sender)
    End Sub
End Class
