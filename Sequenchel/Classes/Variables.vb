Public Class Variables

#Region "General"
    Private _UsersetLocation As String = "REGISTRY"
    Private _BuildMargin As Integer = 5
    Private _FieldHeight As Integer = 20
    Private _MaxSearch As Integer = 1000
    Private _MaxColumnSort As Integer = 3
    Private _MaxColumnWidth As Integer = 300
    Private _CallSplash As Boolean = False
    Private _DateTimeStyle As String = "120"
    Private _LimitLookupLists As Boolean = True
    Private _LimitLookupListsCount As Integer = 100
    Private _IncludeDate As Boolean = False

    Public Property UsersetLocation() As String
        Get
            Return _UsersetLocation
        End Get
        Set(ByVal Value As String)
            _UsersetLocation = Value
        End Set
    End Property

    Public Property FieldHeight() As Integer
        Get
            Return _FieldHeight
        End Get
        Set(ByVal Value As Integer)
            _FieldHeight = Value
        End Set
    End Property

    Public Property BuildMargin() As Integer
        Get
            Return _BuildMargin
        End Get
        Set(ByVal Value As Integer)
            _BuildMargin = Value
        End Set
    End Property

    Public Property MaxSearch() As Integer
        Get
            Return _MaxSearch
        End Get
        Set(ByVal Value As Integer)
            _MaxSearch = Value
        End Set
    End Property

    Public Property MaxColumnSort() As Integer
        Get
            Return _MaxColumnSort
        End Get
        Set(ByVal Value As Integer)
            _MaxColumnSort = Value
        End Set
    End Property

    Public Property MaxColumnWidth() As Integer
        Get
            Return _MaxColumnWidth
        End Get
        Set(ByVal Value As Integer)
            _MaxColumnWidth = Value
        End Set
    End Property

    Public Property CallSplash() As Boolean
        Get
            Return _CallSplash
        End Get
        Set(ByVal Value As Boolean)
            _CallSplash = Value
        End Set
    End Property

    Public Property DateTimeStyle() As String
        Get
            Return _DateTimeStyle
        End Get
        Set(ByVal Value As String)
            _DateTimeStyle = Value
        End Set
    End Property

    Public Property LimitLookupLists() As Boolean
        Get
            Return _LimitLookupLists
        End Get
        Set(ByVal Value As Boolean)
            _LimitLookupLists = Value
        End Set
    End Property

    Public Property LimitLookupListsCount() As Integer
        Get
            Return _LimitLookupListsCount
        End Get
        Set(ByVal Value As Integer)
            _LimitLookupListsCount = Value
        End Set
    End Property

    Public Property IncludeDate() As Boolean
        Get
            Return _IncludeDate
        End Get
        Set(ByVal Value As Boolean)
            _IncludeDate = Value
        End Set
    End Property


#End Region

#Region "Security"
    Private _Encryption As Boolean = True
    Private _SecurityOverride As Boolean = False
    Private _OverridePassword As String = ""

    Private _AllowSettingsChange As Boolean = True
    Private _AllowConfiguration As Boolean = True
    Private _AllowLinkedServers As Boolean = True
    Private _AllowQueryEdit As Boolean = True
    Private _AllowDataImport As Boolean = True
    Private _AllowSmartUpdate As Boolean = True
    Private _AllowUpdate As Boolean = True
    Private _AllowInsert As Boolean = True
    Private _AllowDelete As Boolean = True

    Public Property Encryption() As Boolean
        Get
            Return _Encryption
        End Get
        Set(ByVal Value As Boolean)
            _Encryption = Value
        End Set
    End Property

    Public Property SecurityOverride() As Boolean
        Get
            Return _SecurityOverride
        End Get
        Set(ByVal Value As Boolean)
            _SecurityOverride = Value
        End Set
    End Property

    Public Property OverridePassword() As String
        Get
            Return _OverridePassword
        End Get
        Set(ByVal Value As String)
            _OverridePassword = Value
        End Set
    End Property

    Public Property AllowSettingsChange() As Boolean
        Get
            Return _AllowSettingsChange
        End Get
        Set(ByVal Value As Boolean)
            _AllowSettingsChange = Value
        End Set
    End Property

    Public Property AllowConfiguration() As Boolean
        Get
            Return _AllowConfiguration
        End Get
        Set(ByVal Value As Boolean)
            _AllowConfiguration = Value
        End Set
    End Property

    Public Property AllowLinkedServers() As Boolean
        Get
            Return _AllowLinkedServers
        End Get
        Set(ByVal Value As Boolean)
            _AllowLinkedServers = Value
        End Set
    End Property

    Public Property AllowQueryEdit() As Boolean
        Get
            Return _AllowQueryEdit
        End Get
        Set(ByVal Value As Boolean)
            _AllowQueryEdit = Value
        End Set
    End Property

    Public Property AllowDataImport() As Boolean
        Get
            Return _AllowDataImport
        End Get
        Set(ByVal Value As Boolean)
            _AllowDataImport = Value
        End Set
    End Property

    Public Property AllowSmartUpdate() As Boolean
        Get
            Return _AllowSmartUpdate
        End Get
        Set(ByVal Value As Boolean)
            _AllowSmartUpdate = Value
        End Set
    End Property

    Public Property AllowUpdate() As Boolean
        Get
            Return _AllowUpdate
        End Get
        Set(ByVal Value As Boolean)
            _AllowUpdate = Value
        End Set
    End Property

    Public Property AllowInsert() As Boolean
        Get
            Return _AllowInsert
        End Get
        Set(ByVal Value As Boolean)
            _AllowInsert = Value
        End Set
    End Property

    Public Property AllowDelete() As Boolean
        Get
            Return _AllowDelete
        End Get
        Set(ByVal Value As Boolean)
            _AllowDelete = Value
        End Set
    End Property


#End Region

#Region "XML"
    Private _DefaultConfigFilePath As String = Application.StartupPath & "\Config"
    Private _GeneralSettings As String = _DefaultConfigFilePath & "\" & "SequenchelSettings.xml"
    Private _ConnectionsFile As String = _DefaultConfigFilePath & "\" & "SDBAConnections.xml"
    Private _ConnectionDefault As String = ""
    Private _TableSetsFile As String = _DefaultConfigFilePath & "\" & "SDBATableSets.xml"
    Private _TableSetDefault As String = ""
    Private _TablesFile As String = _DefaultConfigFilePath & "\" & "SDBATablesFile.xml"
    Private _TableDefault As String = ""
    Private _ReportSetFile As String = _DefaultConfigFilePath & "\" & "SDBAReportSet.xml"
    Private _ReportDefault As String = ""
    Private _SearchFile As String = _DefaultConfigFilePath & "\" & "SDBASearch.xml"

    Public Property DefaultConfigFilePath() As String
        Get
            Return _DefaultConfigFilePath
        End Get
        Set(ByVal Value As String)
            _DefaultConfigFilePath = Value
        End Set
    End Property

    Public Property GeneralSettings() As String
        Get
            Return _GeneralSettings
        End Get
        Set(ByVal Value As String)
            _GeneralSettings = Value
        End Set
    End Property

    Public Property ConnectionsFile() As String
        Get
            Return _ConnectionsFile
        End Get
        Set(ByVal Value As String)
            _ConnectionsFile = Value
        End Set
    End Property

    Public Property ConnectionDefault() As String
        Get
            Return _ConnectionDefault
        End Get
        Set(ByVal Value As String)
            _ConnectionDefault = Value
        End Set
    End Property

    Public Property TableSetsFile() As String
        Get
            Return _TableSetsFile
        End Get
        Set(ByVal Value As String)
            _TableSetsFile = Value
        End Set
    End Property

    Public Property TableSetDefault() As String
        Get
            Return _TableSetDefault
        End Get
        Set(ByVal Value As String)
            _TableSetDefault = Value
        End Set
    End Property

    Public Property TablesFile() As String
        Get
            Return _TablesFile
        End Get
        Set(ByVal Value As String)
            _TablesFile = Value
        End Set
    End Property

    Public Property TableDefault() As String
        Get
            Return _TableDefault
        End Get
        Set(ByVal Value As String)
            _TableDefault = Value
        End Set
    End Property

    Public Property ReportSetFile() As String
        Get
            Return _ReportSetFile
        End Get
        Set(ByVal Value As String)
            _ReportSetFile = Value
        End Set
    End Property

    Public Property ReportDefault() As String
        Get
            Return _ReportDefault
        End Get
        Set(ByVal Value As String)
            _ReportDefault = Value
        End Set
    End Property

    Public Property SearchFile() As String
        Get
            Return _SearchFile
        End Get
        Set(ByVal Value As String)
            _SearchFile = Value
        End Set
    End Property

#End Region

#Region "MonitorDataSpaces"
    Private _MinPercGrowth As Integer = 10
    Private _MinFreeSpace As Integer = 0.5
    Private _SmallGrowth As Integer = 100
    Private _MediumGrowth As Integer = 500
    Private _LargeGrowth As Integer = 1000
    Private _LowerLimit As Integer = 5000
    Private _UpperLimit As Integer = 10000

    Public Property MinPercGrowth() As Integer
        Get
            Return _MinPercGrowth
        End Get
        Set(ByVal Value As Integer)
            _MinPercGrowth = Value
        End Set
    End Property

    Public Property MinFreeSpace() As Integer
        Get
            Return _MinFreeSpace
        End Get
        Set(ByVal Value As Integer)
            _MinFreeSpace = Value
        End Set
    End Property

    Public Property SmallGrowth() As Integer
        Get
            Return _SmallGrowth
        End Get
        Set(ByVal Value As Integer)
            _SmallGrowth = Value
        End Set
    End Property

    Public Property MediumGrowth() As Integer
        Get
            Return _MediumGrowth
        End Get
        Set(ByVal Value As Integer)
            _MediumGrowth = Value
        End Set
    End Property

    Public Property LargeGrowth() As Integer
        Get
            Return _LargeGrowth
        End Get
        Set(ByVal Value As Integer)
            _LargeGrowth = Value
        End Set
    End Property

    Public Property LowerLimit() As Integer
        Get
            Return _LowerLimit
        End Get
        Set(ByVal Value As Integer)
            _LowerLimit = Value
        End Set
    End Property

    Public Property UpperLimit() As Integer
        Get
            Return _UpperLimit
        End Get
        Set(ByVal Value As Integer)
            _UpperLimit = Value
        End Set
    End Property

#End Region

#Region "Defaults"
    Private ReadOnly _MainSettingsFile As String = "SequenchelSettings.xml"
    Private ReadOnly _ConnectionsFileName As String = "SDBAConnections.xml"
    Private ReadOnly _TableSetName As String = "General"

    Public ReadOnly Property MainSettingsFile() As String
        Get
            Return _MainSettingsFile
        End Get
    End Property

    Public ReadOnly Property ConnectionsFileName() As String
        Get
            Return _ConnectionsFileName
        End Get
    End Property

    Public ReadOnly Property TableSetName() As String
        Get
            Return _TableSetName
        End Get
    End Property

#End Region

End Class
