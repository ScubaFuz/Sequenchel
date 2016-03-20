Public Class CurrentStatus
    Private _Connection As String = ""
    Private _TableSet As String = ""
    Private _Table As String = ""
    Private _Status As Short
    Private _SuspendActions As Boolean = False
    'Private _SelectedItem As DataGridViewRow
    Private _SelectedValue As String = ""
    Private _ConnectionChanged As Boolean = False
    Private _ConnectionReload As Boolean = False
    Private _TableSetChanged As Boolean = False
    Private _TableSetReload As Boolean = False
    Private _TableChanged As Boolean = False
    Private _TableReload As Boolean = False
    Private _ReportLabelWidth As Integer = 0
    Private _ReportComboboxWidth As Integer = 0
    Private _ReportTextboxWidth As Integer = 0
    Private _ReportMaxTop As Integer = 0
    Private _RelationLabelWidth As Integer = 0
    Private _RelationMaxTop As Integer = 0

    Enum StatusList As Short
        Search = 1
        Edit = 2
        Add = 3
        ControlSearch = 4
        ControlEdit = 5
        ControlAdd = 6
    End Enum

    Public Property Connection() As String
        Get
            Return _Connection
        End Get
        Set(ByVal Value As String)
            _Connection = Value
        End Set
    End Property

    Public Property TableSet() As String
        Get
            Return _TableSet
        End Get
        Set(ByVal Value As String)
            _TableSet = Value
        End Set
    End Property

    Public Property Table() As String
        Get
            Return _Table
        End Get
        Set(ByVal Value As String)
            _Table = Value
        End Set
    End Property

    Property Status() As StatusList

        Get
            Return CType(_Status, StatusList)
        End Get

        Set(ByVal value As StatusList)
            _Status = value
        End Set
    End Property

    Public Property SuspendActions() As Boolean
        Get
            Return _SuspendActions
        End Get
        Set(ByVal Value As Boolean)
            _SuspendActions = Value
        End Set
    End Property

    'Public Property SelectedItem() As System.Windows.forms.DataGridViewRow
    '    Get
    '        Return _SelectedItem
    '    End Get
    '    Set(ByVal Value As System.Windows.forms.DataGridViewRow)
    '        _SelectedItem = Value
    '    End Set
    'End Property

    Public Property SelectedValue() As String
        Get
            Return _SelectedValue
        End Get
        Set(ByVal Value As String)
            _SelectedValue = Value
        End Set
    End Property

    Public Property ConnectionChanged() As Boolean
        Get
            Return _ConnectionChanged
        End Get
        Set(ByVal Value As Boolean)
            _ConnectionChanged = Value
        End Set
    End Property

    Public Property ConnectionReload() As Boolean
        Get
            Return _ConnectionReload
        End Get
        Set(ByVal Value As Boolean)
            _ConnectionReload = Value
        End Set
    End Property

    Public Property TableSetChanged() As Boolean
        Get
            Return _TableSetChanged
        End Get
        Set(ByVal Value As Boolean)
            _TableSetChanged = Value
        End Set
    End Property

    Public Property TableSetReload() As Boolean
        Get
            Return _TableSetReload
        End Get
        Set(ByVal Value As Boolean)
            _TableSetReload = Value
        End Set
    End Property

    Public Property TableChanged() As Boolean
        Get
            Return _TableChanged
        End Get
        Set(ByVal Value As Boolean)
            _TableChanged = Value
        End Set
    End Property

    Public Property TableReload() As Boolean
        Get
            Return _TableReload
        End Get
        Set(ByVal Value As Boolean)
            _TableReload = Value
        End Set
    End Property

    Public Property ReportLabelWidth() As Integer
        Get
            Return _ReportLabelWidth
        End Get
        Set(ByVal Value As Integer)
            _ReportLabelWidth = Value
        End Set
    End Property

    Public Property ReportMaxTop() As Integer
        Get
            Return _ReportMaxTop
        End Get
        Set(ByVal Value As Integer)
            _ReportMaxTop = Value
        End Set
    End Property

    Public Property RelationLabelWidth() As Integer
        Get
            Return _RelationLabelWidth
        End Get
        Set(ByVal Value As Integer)
            _RelationLabelWidth = Value
        End Set
    End Property

    Public Property RelationMaxTop() As Integer
        Get
            Return _RelationMaxTop
        End Get
        Set(ByVal Value As Integer)
            _RelationMaxTop = Value
        End Set
    End Property

End Class
