Public Class Servers
    Inherits System.Collections.CollectionBase

    Public Overloads Sub Add(ByVal dblClientId As Double)
        Dim aNewClient As New Server
        Me.List.Add(aNewClient)
        aNewClient.ID = dblClientId
    End Sub

    Public Overloads Sub Add(ByVal AddClient As Server)
        Me.List.Add(AddClient)
    End Sub

    Public Sub Remove(ByVal removeClient As Server)
        Me.List.Remove(removeClient)
    End Sub

    Default Public Property Item(ByVal index As Double) As Server
        Get
            Return Me.List.Item(index)
        End Get
        Set(ByVal Value As Server)
            Me.List.Item(index) = Value
        End Set
    End Property
End Class
