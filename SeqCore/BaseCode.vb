Imports System.Xml

Public Class BaseCode
    Public curVar As New Variables
    Public curStatus As New CurrentStatus
    Public dhdText As New DataHandler.txt
    Public dhdReg As New DataHandler.reg
    Public dhdMainDB As New DataHandler.db
    Public dhdConnection As New DataHandler.db
    Public Message As New Messages

#Region "Errors"
    Private _ErrorLevel As Integer = 0
    Private _ErrorMessage As String = ""

    Public Property ErrorLevel() As Integer
        Get
            Return _ErrorLevel
        End Get
        Set(ByVal Value As Integer)
            _ErrorLevel = Value
        End Set
    End Property

    Public Property ErrorMessage() As String
        Get
            Return _ErrorMessage
        End Get
        Set(ByVal Value As String)
            _ErrorMessage = Value
        End Set
    End Property

    Public Sub ResetError()
        ErrorLevel = 0
        ErrorMessage = ""
    End Sub
#End Region

    Public Sub ParseCommands(StringCollection As System.Collections.ObjectModel.ReadOnlyCollection(Of String))
        Dim intLength As Integer = 0

        For Each Command As String In StringCollection
            Dim intPosition As Integer = Command.IndexOf(":")
            Dim strInput As String = ""
            If intPosition < 0 Then
                intPosition = Command.Length
            Else
                strInput = Command.Substring(intPosition + 1, Command.Length - (intPosition + 1))
            End If
            Dim strCommand As String = Command.ToLower.Substring(0, intPosition)
            Select Case strCommand
                Case "/silent"
                    'Start wihtout any windows / forms
                Case "/debug"
                    curVar.DebugMode = True
                    'Case "/control"
                    '    CurStatus.Status = CurrentStatus.StatusList.ControlSearch
                Case "/dev"
                    curVar.DevMode = True
                    'Case "/noencryption"
                    '    CurVar.Encryption = False
                Case "/securityoverride"
                    If Command.Length > intPosition + 1 Then
                        curVar.OverridePassword = dhdText.MD5Encrypt(Command.Substring(intPosition + 1, Command.Length - (intPosition + 1)))
                    End If
                Case "/control"
                    curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
                Case "/noencryption"
                    curVar.Encryption = False
                Case "/report"
                    'Run Report
                    curStatus.RunReport = True
                Case "/connection"
                    'Use the chosen connection
                    curStatus.Connection = strInput
                Case "/tableset"
                    'Use the chosen TableSet
                    curStatus.TableSet = strInput
                Case "/table"
                    'Use the chosen Table
                    curStatus.Table = strInput
                    'ImportTable = strInput
                Case "/reportname"
                    'Open the report if found
                    curStatus.Report = strInput
                Case "/exportfile"
                    'Export the report to the chosen file
                    dhdText.ExportFile = strInput
                Case "/import"
                    'Run Import
                    curStatus.RunImport = True
                Case "/importfile"
                    'Export the report to the chosen file
                    dhdText.ImportFile = strInput
                Case "/converttotext"
                    'Export the report to the chosen file
                    curVar.ConvertToText = strInput
                Case "/converttonull"
                    'Export the report to the chosen file
                    curVar.ConvertToNull = strInput
                Case "/hasheaders"
                    'Export the report to the chosen file
                    curVar.HasHeaders = strInput
                Case "/delimiter"
                    'Export the report to the chosen file
                    curVar.Delimiter = strInput
                Case "/emailrecipient"
                    'Export the report to the chosen file
                    dhdText.SmtpRecipient = strInput
            End Select

        Next

    End Sub

    Public Sub SetDefaults()
        dhdReg.RegistryPath = "Software\Thicor\Sequenchel\3.0"

        dhdText.InputFile = "SequenchelDBA.xml"
        dhdText.LogFileName = "Sequenchel.Log"
        dhdText.LogLevel = 5
        dhdText.LogLocation = System.AppDomain.CurrentDomain.BaseDirectory & "LOG"
        dhdText.OutputFile = Environment.SpecialFolder.MyDocuments

        dhdMainDB.LoginMethod = "WINDOWS"
        dhdMainDB.LoginName = "SDBAUser"
        dhdMainDB.Password = "SDBAPassword"
        dhdMainDB.DataLocation = Environment.MachineName & "\SQLEXPRESS"
        dhdMainDB.DatabaseName = "Sequenchel"
        dhdMainDB.DataProvider = "SQL"

        dhdConnection.LoginMethod = "WINDOWS"
        dhdConnection.LoginName = "SDBAUser"
        dhdConnection.Password = "SDBAPassword"
        dhdConnection.DataLocation = Environment.MachineName & "\SQLEXPRESS"
        dhdConnection.DatabaseName = "Sequenchel"
        dhdConnection.DataProvider = "SQL"

        If curStatus.Status > 3 Then
            curStatus.Status = SeqCore.CurrentStatus.StatusList.ControlSearch
        Else
            curStatus.Status = SeqCore.CurrentStatus.StatusList.Search
        End If

    End Sub

#Region "License"
    Public Function LoadLicense() As Boolean
        Try
            Dim strLicense As String
            strLicense = dhdReg.ReadAnyRegKey("LicenseName", dhdReg.RegistryPath)
            If strLicense = "-1" Then
                Message.ErrorLevel = 1
                Message.ErrorMessage = "The licensename was not found"
                Return False
            End If
            If strLicense <> "-1" Then curVar.LicenseName = strLicense
            curVar.LicenseKey = dhdReg.ReadAnyRegKey("LicenseKey", dhdReg.RegistryPath)
            If curVar.LicenseKey = "-1" Then
                Message.ErrorLevel = 1
                Message.ErrorMessage = "The Licencenumber was not found"
                Return False
            End If
        Catch ex As Exception
            Message.ErrorLevel = 1
            Message.ErrorMessage = ex.Message
            Return False
        End Try

        'If CurVar.DebugMode Then MessageBox.Show(strLicenseName & Environment.NewLine & dtmExpiryDate.ToString & Environment.NewLine & My.Application.Info.Version.Major.ToString & Environment.NewLine & strLicenseKey)
        If curVar.LicenseKey <> "-1" Then
            If CheckLicenseKey(curVar.LicenseKey, curVar.LicenseName, GetVersion("M"), Nothing) = False Then
                Message.ErrorLevel = 2
                Message.ErrorMessage = "The License could not be validated"
            End If
        Else
            Message.ErrorLevel = 2
            Message.ErrorMessage = "The License could not be loaded"
        End If

        'strReport = "Sequenchel " & vbTab & " version: " & Application.ProductVersion & vbTab & "  Licensed to: " & strLicenseName
        Return LicenseValidated
    End Function

#End Region

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

    Public Sub WriteLog(ByVal strLogtext As String, ByVal intLogLevel As Integer)
        Dim blnLogSucces As Boolean = True
        Try
            If dhdText.LogLocation.ToLower = "database" Then
                If dhdMainDB.WriteLog(strLogtext, intLogLevel, dhdText.LogLevel) = False Then
                    blnLogSucces = False
                End If
            Else
                If dhdText.WriteLog(strLogtext, intLogLevel) = False Then
                    blnLogSucces = False
                End If
                'If DevMode Then MessageBox.Show(dhdText.LogFileName & Environment.NewLine & dhdText.LogLocation & Environment.NewLine & dhdText.LogLevel)
            End If
        Catch ex As Exception
            blnLogSucces = False
            'MessageBox.Show("there was an error writing to the logfile: " & Environment.NewLine & ex.Message)
        End Try

        If blnLogSucces = False Then
            Dim strMyDir As String = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            Dim strOrgDir As String = dhdText.LogLocation
            If dhdText.CheckDir(strMyDir & "\Sequenchel", True) = False Then dhdText.CreateDir(strMyDir & "\Sequenchel")
            If dhdText.CheckDir(strMyDir & "\Sequenchel\LOG", True) = False Then dhdText.CreateDir(strMyDir & "\Sequenchel\LOG")
            dhdText.LogFileName = "Sequenchel.Log"
            dhdText.LogLocation = strMyDir & "\Sequenchel\LOG"
            dhdText.WriteLog("There was an error writng to the logfile at: " & strOrgDir & Environment.NewLine & dhdText.Errormessage, 1)
        End If
    End Sub

#Region "XML"
    Public Function LoadSDBASettingsXml(xmlLoadDoc As XmlDocument) As Boolean
        If dhdText.CheckFile(System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile) = True Then
            'LoadXmlFile
            Try
                xmlLoadDoc.Load(System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile)
                If dhdText.CheckElement(xmlLoadDoc, "DefaultConfigFilePath") Then curVar.DefaultConfigFilePath = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("DefaultConfigFilePath").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SettingsFile") Then curVar.GeneralSettings = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("SettingsFile").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowSettingsChange") Then curVar.AllowSettingsChange = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowSettingsChange").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowConfigurationChange") Then curVar.AllowConfiguration = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowConfigurationChange").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowLinkedServersChange") Then curVar.AllowLinkedServers = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowLinkedServersChange").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowReportQueryEdit") Then curVar.AllowQueryEdit = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowReportQueryEdit").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowDataImport") Then curVar.AllowDataImport = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowDataImport").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowSmartUpdate") Then curVar.AllowSmartUpdate = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowSmartUpdate").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowUpdate") Then curVar.AllowUpdate = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowUpdate").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowInsert") Then curVar.AllowInsert = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowInsert").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AllowDelete") Then curVar.AllowDelete = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("AllowDelete").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "OverridePassword") Then
                    If xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("OverridePassword").InnerText.Length > 0 Then
                        If curVar.OverridePassword = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("OverridePassword").InnerText Then
                            curVar.SecurityOverride = True
                        Else
                            curVar.OverridePassword = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("OverridePassword").InnerText
                        End If
                    End If
                End If
                If dhdText.CheckElement(xmlLoadDoc, "TimedShutdown") Then curVar.TimedShutdown = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("TimedShutdown").InnerText
            Catch ex As Exception
                ErrorMessage = "There was an error reading the XML file. Please check the file" & Environment.NewLine & System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile & Environment.NewLine & Environment.NewLine & ex.Message
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile & Environment.NewLine & Environment.NewLine & ex.Message, 1)
                Return False
            End Try
        Else
            ErrorMessage = "The XML file was not found. Please check the file" & Environment.NewLine & System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile
            WriteLog("The XML file was not found. Please check the file: " & System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile, 1)
            Return False
        End If
        Return True

    End Function

    Public Function SaveSDBASettingsXml(xmlSDBASettings As XmlDocument) As Boolean
        '** Create or update the xml inputdata.
        Dim strXmlText As String
        strXmlText = "<?xml version=""1.0"" standalone=""yes""?>" & Environment.NewLine
        strXmlText &= "<Sequenchel>" & Environment.NewLine
        strXmlText &= "	<Settings>" & Environment.NewLine
        strXmlText &= "		<DefaultConfigFilePath>" & curVar.DefaultConfigFilePath & "</DefaultConfigFilePath>" & Environment.NewLine
        strXmlText &= "		<SettingsFile>" & curVar.GeneralSettings & "</SettingsFile>" & Environment.NewLine
        strXmlText &= "		<AllowSettingsChange>" & curVar.AllowSettingsChange & "</AllowSettingsChange>" & Environment.NewLine
        strXmlText &= "		<AllowConfigurationChange>" & curVar.AllowConfiguration & "</AllowConfigurationChange>" & Environment.NewLine
        strXmlText &= "		<AllowLinkedServersChange>" & curVar.AllowLinkedServers & "</AllowLinkedServersChange>" & Environment.NewLine
        strXmlText &= "		<AllowReportQueryEdit>" & curVar.AllowQueryEdit & "</AllowReportQueryEdit>" & Environment.NewLine
        strXmlText &= "		<AllowDataImport>" & curVar.AllowDataImport & "</AllowDataImport>" & Environment.NewLine
        strXmlText &= "		<AllowSmartUpdate>" & curVar.AllowSmartUpdate & "</AllowSmartUpdate>" & Environment.NewLine
        strXmlText &= "		<AllowUpdate>" & curVar.AllowUpdate & "</AllowUpdate>" & Environment.NewLine
        strXmlText &= "		<AllowInsert>" & curVar.AllowInsert & "</AllowInsert>" & Environment.NewLine
        strXmlText &= "		<AllowDelete>" & curVar.AllowDelete & "</AllowDelete>" & Environment.NewLine
        strXmlText &= "		<OverridePassword>" & curVar.OverridePassword & "</OverridePassword>" & Environment.NewLine
        strXmlText &= "		<TimedShutdown>" & curVar.TimedShutdown & "</TimedShutdown>" & Environment.NewLine
        strXmlText &= "	</Settings>" & Environment.NewLine
        strXmlText &= "</Sequenchel>" & Environment.NewLine
        Try
            xmlSDBASettings.LoadXml(strXmlText)
            If dhdText.CreateFile(strXmlText, System.AppDomain.CurrentDomain.BaseDirectory & dhdText.InputFile) = False Then
                WriteLog("There was an error creating or saving the Settings file. " & dhdText.Errormessage, 1)
                ErrorMessage = "There was an error creating or saving the Settings file: " & dhdText.Errormessage
                Return False
            End If
        Catch ex As Exception
            ''WriteLog(basCode.Message.strXmlError & " " & ex.Message, 1)
            'ErrorMessage = basCode.Message.strXmlError & " " & ex.Message
            Return False
        End Try
        Return True
    End Function

    Public Function CheckFilePath(strFilePathName As String, Optional blnPersonal As Boolean = False) As String
        If strFilePathName.Contains("\") Then
            Return strFilePathName
        ElseIf blnPersonal = True Then
            Return dhdText.PathConvert("%Documents%\" & strFilePathName)
        ElseIf curVar.DefaultConfigFilePath.Length > 0 Then
            Return curVar.DefaultConfigFilePath & "\" & strFilePathName
        Else
            Return System.AppDomain.CurrentDomain.BaseDirectory & strFilePathName
        End If
    End Function

    Public Function LoadGeneralSettingsXml(xmlLoadDoc As XmlDocument) As Boolean
        If dhdText.CheckFile(dhdText.PathConvert(CheckFilePath(curVar.GeneralSettings))) = True Then
            'LoadXmlFile
            Try
                xmlLoadDoc.Load(dhdText.PathConvert(CheckFilePath(curVar.GeneralSettings)))
                If dhdText.CheckElement(xmlLoadDoc, "DataLocation") Then dhdMainDB.DataLocation = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("DataLocation").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "DatabaseName") Then dhdMainDB.DatabaseName = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("DatabaseName").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "DataProvider") Then dhdMainDB.DataProvider = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("DataProvider").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LoginMethod") Then dhdMainDB.LoginMethod = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("LoginMethod").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LoginName") Then dhdMainDB.LoginName = xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("LoginName").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "Password") Then dhdMainDB.Password = DataHandler.txt.DecryptText(xmlLoadDoc.Item("Sequenchel").Item("DataBase").Item("Password").InnerText)

                If dhdText.CheckElement(xmlLoadDoc, "LogFileName") Then dhdText.LogFileName = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("LogFileName").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LogLevel") Then dhdText.LogLevel = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("LogLevel").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LogLocation") Then dhdText.LogLocation = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("LogLocation").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "Retenion") Then dhdText.Retenion = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("Retenion").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "AutoDelete") Then dhdText.AutoDelete = xmlLoadDoc.Item("Sequenchel").Item("LogSettings").Item("AutoDelete").InnerText

                If dhdText.CheckElement(xmlLoadDoc, "Connections") Then curVar.ConnectionsFile = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("Connections").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "UsersetLocation") Then curVar.UsersetLocation = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("UsersetLocation").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LimitLookupLists") Then curVar.LimitLookupLists = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("LimitLookupLists").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "LimitLookupListsCount") Then curVar.LimitLookupListsCount = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("LimitLookupListsCount").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "DateTimeStyle") Then curVar.DateTimeStyle = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("DateTimeStyle").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "IncludeDate") Then curVar.IncludeDate = xmlLoadDoc.Item("Sequenchel").Item("Settings").Item("IncludeDate").InnerText

                If dhdText.CheckElement(xmlLoadDoc, "SmtpServer") Then dhdText.SmtpServer = xmlLoadDoc.Item("Sequenchel").Item("Email").Item("SmtpServer").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SmtpCredentials") Then dhdText.SmtpCredentials = xmlLoadDoc.Item("Sequenchel").Item("Email").Item("SmtpCredentials").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SmtpServerUsername") Then dhdText.SmtpUser = xmlLoadDoc.Item("Sequenchel").Item("Email").Item("SmtpServerUsername").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SmtpServerPassword") Then dhdText.SmtpPassword = xmlLoadDoc.Item("Sequenchel").Item("Email").Item("SmtpServerPassword").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SmtpReply") Then dhdText.SmtpReply = xmlLoadDoc.Item("Sequenchel").Item("Email").Item("SmtpReply").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SmtpPort") Then dhdText.SmtpPort = xmlLoadDoc.Item("Sequenchel").Item("Email").Item("SmtpPort").InnerText
                If dhdText.CheckElement(xmlLoadDoc, "SmtpSsl") Then dhdText.SmtpSsl = xmlLoadDoc.Item("Sequenchel").Item("Email").Item("SmtpSsl").InnerText

            Catch ex As Exception
                ErrorMessage = "There was an error reading the XML file. Please check the file: " & curVar.GeneralSettings & " " & ex.Message
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & curVar.GeneralSettings & Environment.NewLine & ex.Message, 1)
                Return False
            End Try
        Else
            If SaveGeneralSettingsXml(xmlLoadDoc) = False Then
                Return False
            End If
        End If
        Return True

    End Function

    Public Function SaveGeneralSettingsXml(xmlSaveDoc As XmlDocument) As Boolean
        '** Create or update the xml inputdata.
        Dim strXmlText As String
        strXmlText = "<?xml version=""1.0"" standalone=""yes""?>" & Environment.NewLine
        strXmlText &= "<Sequenchel>" & Environment.NewLine
        strXmlText &= "	<DataBase>" & Environment.NewLine
        strXmlText &= "		<DataLocation>" & dhdMainDB.DataLocation & "</DataLocation>" & Environment.NewLine
        strXmlText &= "		<DatabaseName>" & dhdMainDB.DatabaseName & "</DatabaseName>" & Environment.NewLine
        strXmlText &= "		<DataProvider>" & dhdMainDB.DataProvider & "</DataProvider>" & Environment.NewLine
        strXmlText &= "		<LoginMethod>" & dhdMainDB.LoginMethod & "</LoginMethod>" & Environment.NewLine
        strXmlText &= "		<LoginName>" & dhdMainDB.LoginName & "</LoginName>" & Environment.NewLine
        strXmlText &= "		<Password>" & DataHandler.txt.EncryptText(dhdMainDB.Password) & "</Password>" & Environment.NewLine
        strXmlText &= "	</DataBase>" & Environment.NewLine
        strXmlText &= "	<LogSettings>" & Environment.NewLine
        strXmlText &= "		<LogFileName>" & dhdText.LogFileName & "</LogFileName>" & Environment.NewLine
        strXmlText &= "		<LogLevel>" & dhdText.LogLevel & "</LogLevel>" & Environment.NewLine
        strXmlText &= "		<LogLocation>" & dhdText.LogLocation & "</LogLocation>" & Environment.NewLine
        strXmlText &= "		<Retenion>" & dhdText.Retenion & "</Retenion>" & Environment.NewLine
        strXmlText &= "		<AutoDelete>" & dhdText.AutoDelete & "</AutoDelete>" & Environment.NewLine
        strXmlText &= "		<Language>EN</Language>" & Environment.NewLine
        strXmlText &= "		<LanguageOverride>False</LanguageOverride>" & Environment.NewLine
        strXmlText &= "	</LogSettings>" & Environment.NewLine
        strXmlText &= "	<Settings>" & Environment.NewLine
        strXmlText &= "		<Connections>" & curVar.ConnectionsFile & "</Connections>" & Environment.NewLine
        strXmlText &= "		<UsersetLocation>REGISTRY</UsersetLocation>" & Environment.NewLine
        strXmlText &= "		<LimitLookupLists>" & curVar.LimitLookupLists & "</LimitLookupLists>" & Environment.NewLine
        strXmlText &= "		<LimitLookupListsCount>" & curVar.LimitLookupListsCount & "</LimitLookupListsCount>" & Environment.NewLine
        strXmlText &= "		<DateTimeStyle>" & curVar.DateTimeStyle & "</DateTimeStyle>" & Environment.NewLine
        strXmlText &= "		<IncludeDate>" & curVar.IncludeDate & "</IncludeDate>" & Environment.NewLine
        strXmlText &= "	</Settings>" & Environment.NewLine
        strXmlText &= "	<Email>" & Environment.NewLine
        strXmlText &= "		<SmtpServer>" & dhdText.SmtpServer & "</SmtpServer>" & Environment.NewLine
        strXmlText &= "		<SmtpCredentials>" & dhdText.SmtpCredentials & "</SmtpCredentials>" & Environment.NewLine
        strXmlText &= "		<SmtpServerUsername>" & dhdText.SmtpUser & "</SmtpServerUsername>" & Environment.NewLine
        strXmlText &= "		<SmtpServerPassword>" & dhdText.SmtpPassword & "</SmtpServerPassword>" & Environment.NewLine
        strXmlText &= "		<SmtpReply>" & dhdText.SmtpReply & "</SmtpReply>" & Environment.NewLine
        strXmlText &= "		<SmtpPort>" & dhdText.SmtpPort & "</SmtpPort>" & Environment.NewLine
        strXmlText &= "		<SmtpSsl>" & dhdText.SmtpSsl & "</SmtpSsl>" & Environment.NewLine
        strXmlText &= "	</Email>" & Environment.NewLine
        strXmlText &= "</Sequenchel>" & Environment.NewLine

        Try
            xmlSaveDoc.LoadXml(strXmlText)
            If dhdText.CreateFile(strXmlText, dhdText.PathConvert(CheckFilePath(curVar.GeneralSettings))) = False Then
                WriteLog("There was an error saving the General Settings file" & Environment.NewLine & dhdText.Errormessage, 1)
                Return False
            End If
            Return True
        Catch ex As Exception
            'WriteLog(basCode.Message.strXmlError & Environment.NewLine & ex.Message, 1)
            Return False
        End Try
    End Function

    Public Function LoadConnections(xmlConnections As XmlDocument) As List(Of String)
        ResetError()
        If curStatus.ConnectionsReload = True Then
            xmlConnections = LoadConnectionsXml(xmlConnections)
        End If

        Dim ReturnValue As New List(Of String)
        Try
            Dim blnConnectionExists As Boolean = False
            curVar.ConnectionDefault = ""

            Dim xNode As XmlNode
            For Each xNode In xmlConnections.SelectNodes("//Connection")
                If xNode.Item("ConnectionName").InnerText = curStatus.Connection Then blnConnectionExists = True
                ReturnValue.Add(xNode.Item("ConnectionName").InnerText)
                If xNode.Attributes("Default").Value = "True" Then
                    curVar.ConnectionDefault = xNode.Item("ConnectionName").InnerText
                End If
            Next
            If blnConnectionExists = False And curStatus.Connection = "" Then curStatus.Connection = curVar.ConnectionDefault
        Catch ex As Exception
            ErrorLevel = -1
            ErrorMessage = "There was an error loading the connections. Please check the log."
            WriteLog("There was an error loading the connections." & Environment.NewLine & ex.Message, 1)
            Return Nothing
        End Try
        Return ReturnValue

    End Function

    Public Function LoadConnectionsXml(xmlConnections As XmlDocument) As XmlDocument
        ResetError()
        If dhdText.CheckFile(dhdText.PathConvert(CheckFilePath(curVar.ConnectionsFile))) = True Then
            Try
                xmlConnections.Load(dhdText.PathConvert(CheckFilePath(curVar.ConnectionsFile)))
                curStatus.ConnectionsReload = False
            Catch ex As Exception
                ErrorLevel = -1
                ErrorMessage = "There was an error reading the XML file. Please check the log."
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CheckFilePath(curVar.ConnectionsFile)) & Environment.NewLine & ex.Message, 1)
                Return Nothing
            End Try
        Else
            ErrorLevel = 5
            ErrorMessage = "Invalid file path: " & dhdText.PathConvert(CheckFilePath(curVar.ConnectionsFile))
        End If
        Return xmlConnections
    End Function

    Public Sub UseMainConnection()
        dhdMainDB.DataLocation = dhdConnection.DataLocation
        dhdMainDB.DatabaseName = dhdConnection.DatabaseName
        dhdMainDB.DataProvider = dhdConnection.DataProvider
        dhdMainDB.LoginMethod = dhdConnection.LoginMethod
        dhdMainDB.LoginName = dhdConnection.LoginName
        dhdMainDB.Password = dhdConnection.Password
        dhdMainDB.DataTableName = dhdConnection.DataTableName
    End Sub

    Public Sub LoadConnection(xmlConnections As XmlDocument, strConnection As String)
        Dim xmlConnNode As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strConnection)

        dhdConnection.DataLocation = xmlConnNode.Item("DataLocation").InnerText
        dhdConnection.DatabaseName = xmlConnNode.Item("DataBaseName").InnerText
        dhdConnection.DataProvider = xmlConnNode.Item("DataProvider").InnerText
        dhdConnection.LoginMethod = xmlConnNode.Item("LoginMethod").InnerText
        dhdConnection.LoginName = xmlConnNode.Item("LoginName").InnerText
        If dhdText.CheckNodeElement(xmlConnNode, "Password") Then dhdConnection.Password = DataHandler.txt.DecryptText(xmlConnNode.Item("Password").InnerText)
        curVar.TableSetsFile = xmlConnNode.Item("TableSets").InnerText

        dhdConnection.CheckDB()
    End Sub

    Public Function LoadTableSets(xmlTableSets As XmlDocument) As List(Of String)
        ResetError()
        If curStatus.TableSetsReload = True Then
            xmlTableSets = LoadTableSetsXml(xmlTableSets)
        End If
        Dim ReturnValue As New List(Of String)
        Try
            curVar.TableSetDefault = ""
            Dim blnTableSetExists As Boolean = False

            Dim TableSetNode As XmlNode
            For Each TableSetNode In xmlTableSets.SelectNodes("//TableSet")
                If TableSetNode.Item("TableSetName").InnerText = curStatus.TableSet Then blnTableSetExists = True
                ReturnValue.Add(TableSetNode.Item("TableSetName").InnerText)
                If TableSetNode.Attributes("Default").Value = "True" Then
                    curVar.TableSetDefault = TableSetNode.Item("TableSetName").InnerText
                End If
            Next
            If blnTableSetExists = False And curStatus.TableSet = "" Then curStatus.TableSet = curVar.TableSetDefault
        Catch ex As Exception
            ErrorLevel = -1
            ErrorMessage = "There was an error loading the TableSets. Please check the log."
            WriteLog("There was an error loading the TableSets" & Environment.NewLine & ex.Message, 1)
            Return Nothing
        End Try
        Return ReturnValue
    End Function

    Public Function LoadTableSetsXml(xmlTableSets As XmlDocument) As XmlDocument
        ResetError()
        If dhdText.CheckFile(dhdText.PathConvert(CheckFilePath(curVar.TableSetsFile))) = True Then
            Try
                xmlTableSets.Load(dhdText.PathConvert(CheckFilePath(curVar.TableSetsFile)))
                curStatus.TableSetsReload = False
            Catch ex As Exception
                ErrorLevel = -1
                ErrorMessage = "There was an error reading the XML file. Please check the log."
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(curVar.TableSetsFile) & Environment.NewLine & ex.Message, 1)
                Return Nothing
            End Try
        Else
            ErrorLevel = 5
            ErrorMessage = "Invalid file path: " & dhdText.PathConvert(CheckFilePath(curVar.TableSetsFile))
        End If
        Return xmlTableSets
    End Function

    Public Sub LoadTableSet(xmlTableSets As XmlDocument, strTableSet As String)
        Dim xmlTSNode As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strTableSet)
        If xmlTSNode Is Nothing Then Exit Sub
        'curStatus.TableSet = xmlTSNode.Item("TableSetName").InnerText
        curVar.TablesFile = xmlTSNode.Item("TablesFile").InnerText
        dhdText.OutputFile = xmlTSNode.Item("OutputPath").InnerText
        curVar.ReportSetFile = xmlTSNode.Item("ReportSet").InnerText
        If dhdText.CheckNodeElement(xmlTSNode, "Search") Then curVar.SearchFile = xmlTSNode.Item("Search").InnerText
    End Sub


#End Region
End Class
