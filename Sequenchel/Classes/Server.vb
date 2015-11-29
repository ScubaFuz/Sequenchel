Public Class Server
    Private _Name As String = Nothing
    Private _ID As Double = 0
    Private _Checked As Boolean = True

    Private _LargeNumber As Double = 0
    Private _PropertyName As String = Nothing
    Private _SmallNumber As Integer = 0
    Private _SomeColor As System.Drawing.Color = Nothing

    Public Function Clone() As Server
        Return DirectCast(Me.MemberwiseClone(), Server)
    End Function

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Public Property ID() As Double
        Get
            Return _ID
        End Get
        Set(ByVal value As Double)
            _ID = value
        End Set
    End Property

    Public Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
        End Set
    End Property

    Public Property LargeNumber() As Double
        Get
            Return _LargeNumber
        End Get
        Set(ByVal value As Double)
            _LargeNumber = value
        End Set
    End Property

    Public Property PropertyName() As String
        Get
            Return _PropertyName
        End Get
        Set(ByVal value As String)
            _PropertyName = value
        End Set
    End Property

    Public Property SmallNumber() As Integer
        Get
            Return _SmallNumber
        End Get
        Set(ByVal value As Integer)
            _SmallNumber = value
        End Set
    End Property

    Public Property SomeColor() As System.Drawing.Color
        Get
            Return _SomeColor
        End Get
        Set(ByVal value As System.Drawing.Color)
            _SomeColor = value
        End Set
    End Property

End Class
