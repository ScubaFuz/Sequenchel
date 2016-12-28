Public Class CurrentStatus

#Region "Connection"
    Private _Connection As String = ""
    Private _TableSet As String = ""
    Private _Table As String = ""
    Private _TableAlias As String = ""
    Private _Report As String = ""

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

    Public Property TableAlias() As String
        Get
            Return _TableAlias
        End Get
        Set(ByVal Value As String)
            _TableAlias = Value
        End Set
    End Property

    Public Property Report() As String
        Get
            Return _Report
        End Get
        Set(ByVal Value As String)
            _Report = Value
        End Set
    End Property
#End Region

#Region "ConnectionChange"
    Private _ConnectionsReload As Boolean = True
    Private _ConnectionChanged As Boolean = False
    Private _ConnectionReload As Boolean = False
    Private _TableSetsReload As Boolean = True
    Private _TableSetChanged As Boolean = False
    Private _TableSetReload As Boolean = False
    Private _TablesReload As Boolean = True
    Private _TableChanged As Boolean = False
    Private _TableReload As Boolean = False
    Private _ReportsReload As Boolean = True
    Private _SearchesReload As Boolean = True

    Public Property ConnectionsReload() As Boolean
        Get
            Return _ConnectionsReload
        End Get
        Set(ByVal Value As Boolean)
            _ConnectionsReload = Value
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

    Public Property TableSetsReload() As Boolean
        Get
            Return _TableSetsReload
        End Get
        Set(ByVal Value As Boolean)
            _TableSetsReload = Value
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

    Public Property TablesReload() As Boolean
        Get
            Return _TablesReload
        End Get
        Set(ByVal Value As Boolean)
            _TablesReload = Value
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

    Public Property ReportsReload() As Boolean
        Get
            Return _ReportsReload
        End Get
        Set(ByVal Value As Boolean)
            _ReportsReload = Value
        End Set
    End Property

    Public Property SearchesReload() As Boolean
        Get
            Return _SearchesReload
        End Get
        Set(ByVal Value As Boolean)
            _SearchesReload = Value
        End Set
    End Property
#End Region

#Region "Display"
    Private _ReportLabelWidth As Integer = 0
    'Private _ReportComboboxWidth As Integer = 0
    'Private _ReportTextboxWidth As Integer = 0
    Private _ReportMaxTop As Integer = 0
    Private _RelationLabelWidth As Integer = 0
    Private _RelationMaxTop As Integer = 0
    Private _ReportPageShow As Integer = 0

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

    Public Property ReportPageShow() As Integer
        Get
            Return _ReportPageShow
        End Get
        Set(ByVal Value As Integer)
            _ReportPageShow = Value
        End Set
    End Property
#End Region

#Region "Actions"
    Private _SuspendActions As Boolean = False
    Private _RunReport As Boolean = False
    Private _RunImport As Boolean = False
    Private _ClearTargetTable As Boolean = False
    Private _ImportAsXml As Boolean = False

    Public Property SuspendActions() As Boolean
        Get
            Return _SuspendActions
        End Get
        Set(ByVal Value As Boolean)
            _SuspendActions = Value
        End Set
    End Property

    Public Property RunReport() As Boolean
        Get
            Return _RunReport
        End Get
        Set(ByVal Value As Boolean)
            _RunReport = Value
        End Set
    End Property

    Public Property ClearTargetTable() As Boolean
        Get
            Return _ClearTargetTable
        End Get
        Set(ByVal Value As Boolean)
            _ClearTargetTable = Value
        End Set
    End Property

    Public Property ImportAsXml() As Boolean
        Get
            Return _ImportAsXml
        End Get
        Set(ByVal Value As Boolean)
            _ImportAsXml = Value
        End Set
    End Property

    Public Property RunImport() As Boolean
        Get
            Return _RunImport
        End Get
        Set(ByVal Value As Boolean)
            _RunImport = Value
        End Set
    End Property
#End Region

#Region "Status"
    Private _Status As Short

    Enum StatusList As Short
        Search = 1
        Edit = 2
        Add = 3
        ControlSearch = 4
        ControlEdit = 5
        ControlAdd = 6
    End Enum

    Property Status() As StatusList
        Get
            Return CType(_Status, StatusList)
        End Get

        Set(ByVal value As StatusList)
            _Status = value
        End Set
    End Property
#End Region

    Private _SelectedValue As String = ""

    Public Property SelectedValue() As String
        Get
            Return _SelectedValue
        End Get
        Set(ByVal Value As String)
            _SelectedValue = Value
        End Set
    End Property


End Class
