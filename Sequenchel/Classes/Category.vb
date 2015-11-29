Public Class Category
    Private _ID As Integer
    Private _ControlField As String
    Private _ControlValue As Boolean
    Private _ControlUpdate As Boolean
    Private _ControlMode As Boolean
    Private _List As Boolean
    Private _Visible As Boolean
    Private _Search As Boolean
    Private _SearchList As Boolean
    Private _Update As Boolean

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal Value As Integer)
            _ID = Value
        End Set
    End Property

    Public Property ControlField() As String
        Get
            Return _ControlField
        End Get
        Set(ByVal Value As String)
            _ControlField = Value
        End Set
    End Property

    Public Property ControlValue() As Boolean
        Get
            Return _ControlValue
        End Get
        Set(ByVal Value As Boolean)
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

    Public Property List() As Boolean
        Get
            Return _List
        End Get
        Set(ByVal Value As Boolean)
            _List = Value
        End Set
    End Property

    Public Property Visible() As Boolean
        Get
            Return _Visible
        End Get
        Set(ByVal Value As Boolean)
            _Visible = Value
        End Set
    End Property

    Public Property Search() As Boolean
        Get
            Return _Search
        End Get
        Set(ByVal Value As Boolean)
            _Search = Value
        End Set
    End Property

    Public Property SearchList() As Boolean
        Get
            Return _SearchList
        End Get
        Set(ByVal Value As Boolean)
            _SearchList = Value
        End Set
    End Property

    Public Property Update() As Boolean
        Get
            Return _Update
        End Get
        Set(ByVal Value As Boolean)
            _Update = Value
        End Set
    End Property


End Class
