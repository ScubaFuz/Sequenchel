Public Class frmReAlias

    Private _TableName As String
    Private _TableAlias As String
    Private _FieldName As String
    Private _FieldAlias As String

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

    Public Property FieldName() As String
        Get
            Return _FieldName
        End Get
        Set(ByVal Value As String)
            _FieldName = Value
        End Set
    End Property

    Public Property FieldAlias() As String
        Get
            Return _FieldAlias
        End Get
        Set(ByVal Value As String)
            _FieldAlias = Value
        End Set
    End Property

    Private Sub frmReAlias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowFields()
    End Sub

    Private Sub ShowFields()
        txtFieldName.Text = FieldName
        txtCurrentFieldAlias.Text = FieldAlias
        txtTableName.Text = TableName
        txtCurrentTableAlias.Text = TableAlias

    End Sub
End Class