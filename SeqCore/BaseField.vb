Public Class BaseField
    Private _FieldCategory As Integer = 0

    Public Property FieldCategory() As Integer
        Get
            Return _FieldCategory
        End Get
        Set(ByVal Value As Integer)
            _FieldCategory = Value
        End Set
    End Property

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
    Private _FieldRelationTable As String = ""
    Private _FieldRelationTableAlias As String = ""
    Private _FieldRelationField As String = ""
    Private _FieldRelatedField As String = ""
    Private _FieldRelatedFieldList As Boolean = False

    Public Property FieldRelationTable() As String
        Get
            Return _FieldRelationTable
        End Get
        Set(ByVal Value As String)
            _FieldRelationTable = Value
        End Set
    End Property

    Public Property FieldRelationTableAlias() As String
        Get
            Return _FieldRelationTableAlias
        End Get
        Set(ByVal Value As String)
            _FieldRelationTableAlias = Value
        End Set
    End Property

    Public Property FieldRelationField() As String
        Get
            Return _FieldRelationField
        End Get
        Set(ByVal Value As String)
            _FieldRelationField = Value
        End Set
    End Property

    Public Property FieldRelatedField() As String
        Get
            Return _FieldRelatedField
        End Get
        Set(ByVal Value As String)
            _FieldRelatedField = Value
        End Set
    End Property

    Public Property FieldRelatedFieldList() As Boolean
        Get
            Return _FieldRelatedFieldList
        End Get
        Set(ByVal Value As Boolean)
            _FieldRelatedFieldList = Value
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
