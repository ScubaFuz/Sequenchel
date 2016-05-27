Public Class BaseField

#Region "General"
    Private _Category As Integer = 0
    Private _ControlName As String = ""
    Private _CurrentValue As String = ""
    Private _Index As Integer = 0
    Private _Name As String = ""

    Public Property Category() As Integer
        Get
            Return _Category
        End Get
        Set(ByVal Value As Integer)
            _Category = Value
        End Set
    End Property

    Public Property ControlName() As String
        Get
            Return _ControlName
        End Get
        Set(ByVal Value As String)
            _ControlName = Value
        End Set
    End Property

    Public Property CurrentValue() As String
        Get
            Return _CurrentValue
        End Get
        Set(ByVal Value As String)
            _CurrentValue = Value
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

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            _Name = Value
        End Set
    End Property
#End Region

#Region "Base"
    Private _FieldName As String = ""
    Private _FieldAlias As String = ""
    Private _FieldTableName As String = ""
    Private _FieldTableAlias As String = ""
    Private _FieldDataType As String = ""
    Private _Identity As Boolean = False
    Private _PrimaryKey As Boolean = False

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

    Public Property FieldTableName() As String
        Get
            Return _FieldTableName
        End Get
        Set(ByVal Value As String)
            _FieldTableName = Value
        End Set
    End Property

    Public Property FieldTableAlias() As String
        Get
            Return _FieldTableAlias
        End Get
        Set(ByVal Value As String)
            _FieldTableAlias = Value
        End Set
    End Property

    Public Property FieldDataType() As String
        Get
            Return _FieldDataType
        End Get
        Set(ByVal Value As String)
            _FieldDataType = Value
        End Set
    End Property

    Public Property Identity() As Boolean
        Get
            Return _Identity
        End Get
        Set(ByVal Value As Boolean)
            _Identity = Value
        End Set
    End Property

    Public Property PrimaryKey() As Boolean
        Get
            Return _PrimaryKey
        End Get
        Set(ByVal Value As Boolean)
            _PrimaryKey = Value
        End Set
    End Property
#End Region

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

#Region "Display"
    Private _ControlField As String = ""
    Private _ControlValue As String = ""
    Private _ControlUpdate As Boolean = False
    Private _ControlMode As Boolean = False
    Private _DefaultButton As Boolean = False
    Private _DefaultValue As String = ""
    Private _FieldWidth As Integer = 0
    Private _FieldList As Boolean = False
    Private _FieldListOrder As Integer = 0
    Private _FieldListWidth As Integer = 0
    Private _FieldVisible As Boolean = False
    Private _FieldSearch As Boolean = False
    Private _FieldSearchList As Boolean = False
    Private _FieldUpdate As Boolean = False

    Public Property ControlField() As String
        Get
            Return _ControlField
        End Get
        Set(ByVal Value As String)
            _ControlField = Value
        End Set
    End Property

    Public Property ControlValue() As String
        Get
            Return _ControlValue
        End Get
        Set(ByVal Value As String)
            _ControlValue = Value
        End Set
    End Property

    Public Property ControlUpdate() As Boolean
        Get
            Return _ControlUpdate
        End Get
        Set(ByVal Value As Boolean)
            _ControlUpdate = Value
        End Set
    End Property

    Public Property ControlMode() As Boolean
        Get
            Return _ControlMode
        End Get
        Set(ByVal Value As Boolean)
            _ControlMode = Value
        End Set
    End Property

    Public Property DefaultButton() As Boolean
        Get
            Return _DefaultButton
        End Get
        Set(ByVal Value As Boolean)
            _DefaultButton = Value
        End Set
    End Property

    Public Property DefaultValue() As String
        Get
            Return _DefaultValue
        End Get
        Set(ByVal Value As String)
            _DefaultValue = Value
        End Set
    End Property

    Public Property FieldWidth() As Integer
        Get
            Return _FieldWidth
        End Get
        Set(ByVal Value As Integer)
            _FieldWidth = Value
        End Set
    End Property

    Public Property FieldList() As Boolean
        Get
            Return _FieldList
        End Get
        Set(ByVal Value As Boolean)
            _FieldList = Value
        End Set
    End Property

    Public Property FieldListOrder() As Integer
        Get
            Return _FieldListOrder
        End Get
        Set(ByVal Value As Integer)
            _FieldListOrder = Value
        End Set
    End Property

    Public Property FieldListWidth() As Integer
        Get
            Return _FieldListWidth
        End Get
        Set(ByVal Value As Integer)
            _FieldListWidth = Value
        End Set
    End Property

    Public Property FieldVisible() As Boolean
        Get
            Return _FieldVisible
        End Get
        Set(ByVal Value As Boolean)
            _FieldVisible = Value
        End Set
    End Property

    Public Property FieldSearch() As Boolean
        Get
            Return _FieldSearch
        End Get
        Set(ByVal Value As Boolean)
            _FieldSearch = Value
        End Set
    End Property

    Public Property FieldSearchList() As Boolean
        Get
            Return _FieldSearchList
        End Get
        Set(ByVal Value As Boolean)
            _FieldSearchList = Value
        End Set
    End Property

    Public Property FieldUpdate() As Boolean
        Get
            Return _FieldUpdate
        End Get
        Set(ByVal Value As Boolean)
            _FieldUpdate = Value
        End Set
    End Property
#End Region

End Class
