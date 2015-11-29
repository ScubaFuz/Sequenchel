Public Class Table
    Inherits System.Collections.CollectionBase
    Private ReadOnly HostForm As System.Windows.Forms.Form

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

    Public Overloads Sub Add(ByVal AddField As TextField)
        Me.List.Add(AddField)
        AddHandler AddField.TextChanged, AddressOf TextHandler
    End Sub

    Public Overloads Sub Add(ByVal AddField As CheckField)
        Me.List.Add(AddField)
        AddHandler AddField.CheckedChanged, AddressOf TextHandler
    End Sub

    Public Overloads Sub Add(ByVal AddField As ComboField)
        Me.List.Add(AddField)
        AddHandler AddField.TextChanged, AddressOf TextHandler
        AddHandler AddField.SelectedIndexChanged, AddressOf TextHandler
    End Sub

    Public Overloads Sub Add(ByVal AddField As ManagedSelectField)
        Me.List.Add(AddField)
        AddHandler AddField.TextChanged, AddressOf TextHandler
        AddHandler AddField.ValueChanged, AddressOf TextHandler
        AddHandler AddField.LostFocus, AddressOf LostFocusHandler
    End Sub

    Public Overloads Sub Remove(ByVal removeField As TextField)
        Me.List.Remove(removeField)
    End Sub

    Public Overloads Sub Remove(ByVal removeField As CheckField)
        Me.List.Remove(removeField)
    End Sub

    Public Overloads Sub Remove(ByVal removeField As ComboField)
        Me.List.Remove(removeField)
    End Sub

    Public Overloads Sub Remove(ByVal strFieldAlias As String)
        Me.List.Remove(strFieldAlias)
    End Sub

    Default Public Property Item(ByVal index As Double) As Object
        Get
            Return Me.List.Item(index)
        End Get
        Set(ByVal Value As Object)
            Me.List.Item(index) = Value
        End Set
    End Property

    Public Sub TextHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        FieldTextHandler(sender)
    End Sub

    Public Sub LostFocusHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        sender.DropDown(2)
    End Sub

End Class
