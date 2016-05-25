Public Class BaseTable
    Inherits System.Collections.CollectionBase

#Region "Properties"
    Private _TableName As String = ""
    Private _TableAlias As String = ""
    Private _TableVisible As Boolean = False
    Private _TableSearch As Boolean = False
    Private _TableUpdate As Boolean = False
    Private _TableInsert As Boolean = False
    Private _TableDelete As Boolean = False

    Public Property TableName() As String
        Get
            Return _TableName
        End Get
        Set(ByVal Value As String)
            _TableName = Value
        End Set
    End Property

    Public Property TableAlias() As String
        Get
            Return _TableAlias
        End Get
        Set(ByVal Value As String)
            _TableAlias = Value
        End Set
    End Property

    Public Property TableVisible() As Boolean
        Get
            Return _TableVisible
        End Get
        Set(ByVal Value As Boolean)
            _TableVisible = Value
        End Set
    End Property

    Public Property TableSearch() As Boolean
        Get
            Return _TableSearch
        End Get
        Set(ByVal Value As Boolean)
            _TableSearch = Value
        End Set
    End Property

    Public Property TableUpdate() As Boolean
        Get
            Return _TableUpdate
        End Get
        Set(ByVal Value As Boolean)
            _TableUpdate = Value
        End Set
    End Property

    Public Property TableInsert() As Boolean
        Get
            Return _TableInsert
        End Get
        Set(ByVal Value As Boolean)
            _TableInsert = Value
        End Set
    End Property

    Public Property TableDelete() As Boolean
        Get
            Return _TableDelete
        End Get
        Set(ByVal Value As Boolean)
            _TableDelete = Value
        End Set
    End Property
#End Region

    Public Overloads Sub Add(ByVal AddField As BaseField)
        Me.List.Add(AddField)
    End Sub

    Public Overloads Sub Remove(ByVal removeField As BaseField)
        Me.List.Remove(removeField)
    End Sub

    Public Overloads Sub Remove(ByVal strFieldName As String)
        Me.List.Remove(strFieldName)
    End Sub

    Default Public Property Item(ByVal index As Double) As Object
        Get
            Return Me.List.Item(index)
        End Get
        Set(ByVal Value As Object)
            Me.List.Item(index) = Value
        End Set
    End Property

End Class
