Public Class BaseRelation
    Private _Name As String = ""
    Private _Index As Integer = 0

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property

    Public Property Index() As Integer
        Get
            Return _Index
        End Get
        Set(ByVal Value As Integer)
            _Index = Value
        End Set
    End Property

#Region "Relations"
    Private _RelationTable As String = ""
    Private _RelationTableAlias As String = ""
    Private _RelationField As String = ""
    Private _RelatedFieldName As String = ""
    Private _RelatedFieldAlias As String = ""
    Private _RelatedFieldList As Boolean = False

    Public Property RelationTable() As String
        Get
            Return _RelationTable
        End Get
        Set(ByVal Value As String)
            _RelationTable = Value
        End Set
    End Property

    Public Property RelationTableAlias() As String
        Get
            Return _RelationTableAlias
        End Get
        Set(ByVal Value As String)
            _RelationTableAlias = Value
        End Set
    End Property

    Public Property RelationField() As String
        Get
            Return _RelationField
        End Get
        Set(ByVal Value As String)
            _RelationField = Value
        End Set
    End Property

    Public Property RelatedFieldName() As String
        Get
            Return _RelatedFieldName
        End Get
        Set(ByVal Value As String)
            _RelatedFieldName = Value
        End Set
    End Property

    Public Property RelatedFieldAlias() As String
        Get
            Return _RelatedFieldAlias
        End Get
        Set(ByVal Value As String)
            _RelatedFieldAlias = Value
        End Set
    End Property

    Public Property RelatedFieldList() As Boolean
        Get
            Return _RelatedFieldList
        End Get
        Set(ByVal Value As Boolean)
            _RelatedFieldList = Value
        End Set
    End Property
#End Region

End Class
