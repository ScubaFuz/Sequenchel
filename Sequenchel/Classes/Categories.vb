Public Class Categories
    Inherits System.Collections.CollectionBase

    Public Function AddNewCategory(ByVal cat As Category) As Integer
        Me.List.Add(cat)
        Return Me.Count - 1
    End Function

    Default Public ReadOnly Property Item(ByVal Index As Integer) As Category
        Get
            Return CType(Me.List.Item(Index), Category)
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal ID As Integer) As Category
        Get
            For index As Integer = 0 To Me.List.Count - 1
                If Me(index).ID = ID Then
                    Return CType(Me.List.Item(index), Category)
                End If
            Next
            Return Nothing
        End Get
    End Property

    Public Function Remove(ByVal Index As Integer) As Boolean
        If Me.Count > 0 Then
            Try
                Me.List.RemoveAt(Index)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

    Public Function Remove(ByVal ID As String) As Boolean
        If Me.Count > 0 Then
            Try
                For index As Integer = 0 To Me.List.Count - 1
                    If Me(index).ID = ID Then
                        Remove(index)
                        Return True
                    End If
                Next
            Catch ex As Exception
                Return False
            End Try
        End If
        Return False
    End Function

End Class
