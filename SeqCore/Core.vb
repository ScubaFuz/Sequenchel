
Public Class Core

    Public dhdReg As New DataHandler.reg
    Public Message As New Messages

    Private _LicenseName As String = "Thicor Services Demo License"
    Private _LicenseKey As String = ""
    Private _LicenseValidated As Boolean = False

    Public Property LicenseName() As String
        Get
            Return _LicenseName
        End Get
        Set(ByVal Value As String)
            _LicenseName = Value
        End Set
    End Property

    Public Property LicenseKey() As String
        Get
            Return _LicenseKey
        End Get
        Set(ByVal Value As String)
            _LicenseKey = Value
        End Set
    End Property

    Public ReadOnly Property LicenseValidated() As Boolean
        Get
            Return _LicenseValidated
        End Get
    End Property

    Public Sub SetDefaults()
        dhdReg.RegistryPath = "Software\Thicor\Sequenchel\3.0"
    End Sub

    Public Function CheckLicense(ByVal strLicenseKey As String, Optional ByVal strLicensedName As String = "Thicor Services Demo License", Optional ByVal intMajorVersion As Integer = 1, Optional ByVal datExpiryDate As Date = Nothing) As Boolean
        Dim blnReturn As Boolean = CheckLicenseKey(strLicenseKey, strLicensedName, intMajorVersion, datExpiryDate)
        _LicenseValidated = blnReturn
        Return LicenseValidated
    End Function

    Public Function LoadLicense() As Boolean
        Try
            Dim strLicense As String
            strLicense = dhdReg.ReadAnyRegKey("LicenseName", dhdReg.RegistryPath)
            If strLicense = "-1" Then
                Message.ErrorLevel = 1
                Message.ErrorMessage = "The licensename was not found"
                _LicenseValidated = False
                Return False
            End If
            If strLicense <> "-1" Then LicenseName = strLicense
            LicenseKey = dhdReg.ReadAnyRegKey("LicenseKey", dhdReg.RegistryPath)
            If LicenseKey = "-1" Then
                Message.ErrorLevel = 1
                Message.ErrorMessage = "The Licencenumber was not found"
                _LicenseValidated = False
                Return False
            End If
        Catch ex As Exception
            Message.ErrorLevel = 1
            Message.ErrorMessage = ex.Message
            _LicenseValidated = False
            Return False
        End Try

        'If CurVar.DebugMode Then MessageBox.Show(strLicenseName & Environment.NewLine & dtmExpiryDate.ToString & Environment.NewLine & My.Application.Info.Version.Major.ToString & Environment.NewLine & strLicenseKey)
        If LicenseKey <> "-1" Then
            If CheckLicense(LicenseKey, LicenseName, GetVersion("M"), Nothing) = False Then
                Message.ErrorLevel = 2
                Message.ErrorMessage = "The License could not be validated"
            End If
        Else
            Message.ErrorLevel = 2
            Message.ErrorMessage = "The License could not be loaded"
            _LicenseValidated = False
        End If

        'strReport = "Sequenchel " & vbTab & " version: " & Application.ProductVersion & vbTab & "  Licensed to: " & strLicenseName
        Return LicenseValidated
    End Function

    Public Function GetVersion(strPart As String) As String
        Select Case strPart
            Case "M"
                Return My.Application.Info.Version.Major
            Case "m"
                Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor
            Case "B"
                Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
            Case "R"
                Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
            Case Else
                Return My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & "." & My.Application.Info.Version.Revision
        End Select
    End Function

    Public Sub DeleteOldLogs()
        'delete old logs
    End Sub

End Class
