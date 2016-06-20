Imports System.Xml

Public Class BaseCode
    Public curVar As New Variables
    Public curStatus As New CurrentStatus

    Public dhdText As New DataHandler.txt
    Public dhdReg As New DataHandler.reg
    Public dhdMainDB As New DataHandler.db
    Public dhdConnection As New DataHandler.db

    Public Excel As New Excel
    Public Message As New Messages
    Public basTable As New BaseTable

    Public xmlTemplates As New XmlDocument
    Public xmlSDBASettings As New XmlDocument
    Public xmlGeneralSettings As New XmlDocument
    Public xmlConnections As New XmlDocument
    Public xmlTableSets As New XmlDocument
    Public xmlTables As New XmlDocument
    Public xmlReports As New XmlDocument
    Public xmlSearch As New XmlDocument

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
        dhdReg.RegistryPath = "Software\Thicor\Sequenchel\4.0"

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

#Region "BaseObjects"
    Public Function GetBaseField(strFieldName As String) As BaseField
        Dim tmpField As BaseField = Nothing
        For intCount As Integer = 0 To basTable.Count - 1
            If basTable.Item(intCount).Name = strFieldName Then
                tmpField = basTable.Item(intCount)
            End If
        Next
        Return tmpField
    End Function

    Public Function GetBaseRelation(strFieldName As String, strRelationName As String) As BaseRelation
        Dim tmpField As BaseField = GetBaseField(strFieldName)
        Dim tmpRelation As BaseRelation = Nothing
        If Not tmpField Is Nothing Then
            tmpRelation = GetBaseRelation(tmpField, strRelationName)
        End If
        Return tmpRelation
    End Function

    Public Function GetBaseRelation(basField As BaseField, strRelationName As String) As BaseRelation
        Dim tmpRelation As BaseRelation = Nothing
        For intCount As Integer = 0 To basField.Count - 1
            If basField.Item(intCount).Name = strRelationName Then
                tmpRelation = basField.Item(intCount)
            End If
        Next
        Return tmpRelation
    End Function

    Public Function GetCategory(fldField As Object) As Integer
        Dim intCategory As Integer = 0
        Try
            Select Case fldField.[GetType]().Name
                Case "TextField"
                    intCategory = fldField.Field.Category
                Case "CheckField"
                    intCategory = fldField.Field.Category
                Case "ComboField"
                    'not used
                Case "ManagedSelectField"
                    intCategory = fldField.Field.Category
                Case Else
                    'Not a type with a category.
            End Select
        Catch ex As Exception
            ErrorLevel = -1
            ErrorMessage = "Retrieving the catagory failed. " & ex.Message
            intCategory = 0
        End Try
        Return intCategory
    End Function

#End Region

#Region "Formatting"
    Public Function FormatDate(ByVal dtmInput As Date) As String
        If dtmInput = Nothing Then
            FormatDate = ""
        Else
            FormatDate = dtmInput.ToString("yyyy-MM-dd")
        End If
    End Function

    'Public Function FormatDateTime(ByVal dtmInput As Date) As String
    '    If dtmInput = Nothing Then
    '        FormatDateTime = ""
    '    Else
    '        FormatDateTime = dtmInput.ToString("yyyyMMdd_HHmm")
    '    End If
    'End Function
#End Region

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
            WriteLog("There was an error creating or saving the Settings file. " & Environment.NewLine & ex.Message, 1)
            ErrorMessage = "There was an error creating or saving the Settings file. Please check the log "
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
                ErrorMessage = "There was an error reading the XML Settings file. Please check the log."
                WriteLog("There was an error reading the XML Settings file. Please check the file" & Environment.NewLine & curVar.GeneralSettings & Environment.NewLine & ex.Message, 1)
                Return False
            End Try
        Else
            If SaveGeneralSettingsXml(xmlLoadDoc) = False Then
                ErrorMessage = "The XML Settings file was not found. Please check the file: " & curVar.GeneralSettings
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
            'WriteLog(Message.strXmlError & Environment.NewLine & ex.Message, 1)
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
        curStatus.TableSetsReload = True

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
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CheckFilePath(curVar.TableSetsFile)) & Environment.NewLine & ex.Message, 1)
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
        curStatus.TablesReload = True
        dhdText.OutputFile = xmlTSNode.Item("OutputPath").InnerText
        curVar.ReportSetFile = xmlTSNode.Item("ReportSet").InnerText
        If dhdText.CheckNodeElement(xmlTSNode, "Search") Then curVar.SearchFile = xmlTSNode.Item("Search").InnerText
        curStatus.SearchesReload = True
    End Sub

    Public Function LoadTables(xmlTables As XmlDocument, blnUseFullName As Boolean) As List(Of String)
        ResetError()
        If curStatus.TablesReload = True Then
            xmlTables = LoadTablesXml(xmlTables)
        End If
        Dim ReturnValue As New List(Of String)
        Try
            Dim blnTableExists As Boolean = False
            curVar.TableDefault = ""

            Dim TableNode As XmlNode
            For Each TableNode In xmlTables.SelectNodes("//Table")
                If TableNode.Item("Name").InnerText = curStatus.Table Then blnTableExists = True
                If blnUseFullName = True Then
                    ReturnValue.Add(TableNode.Item("Alias").InnerText & " (" & TableNode.Item("Name").InnerText & ")")
                Else
                    ReturnValue.Add(TableNode.Item("Alias").InnerText)
                End If
                If TableNode.Attributes("Default").Value = "True" Then
                    curVar.TableDefault = TableNode.Item("Alias").InnerText
                End If
            Next
            If blnTableExists = False And curStatus.Table = "" Then curStatus.Table = curVar.TableDefault
        Catch ex As Exception
            ErrorMessage = "There was an error loading the tables list. Please check the log."
            WriteLog("There was an error loading the tables list." & Environment.NewLine & ex.Message, 1)
            Return Nothing
        End Try
        Return ReturnValue
    End Function

    Public Function LoadTable(xmlTables As XmlDocument, strTable As String) As Boolean
        Dim xPNode As System.Xml.XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Alias", strTable)
        If xPNode Is Nothing Then Return False

        If dhdText.CheckNodeElement(xPNode, "Name") Then basTable.TableName = xPNode.Item("Name").InnerText
        If dhdText.CheckNodeElement(xPNode, "Alias") Then basTable.TableAlias = xPNode.Item("Alias").InnerText.Replace(".", "_")
        If dhdText.CheckNodeElement(xPNode, "Visible") Then basTable.TableVisible = xPNode.Item("Visible").InnerText
        If dhdText.CheckNodeElement(xPNode, "Update") Then basTable.TableUpdate = xPNode.Item("Update").InnerText
        If dhdText.CheckNodeElement(xPNode, "Search") Then basTable.TableSearch = xPNode.Item("Search").InnerText
        If dhdText.CheckNodeElement(xPNode, "Insert") Then basTable.TableInsert = xPNode.Item("Insert").InnerText
        If dhdText.CheckNodeElement(xPNode, "Delete") Then basTable.TableDelete = xPNode.Item("Delete").InnerText

        Dim xNode As System.Xml.XmlNode
        For Each xNode In xPNode.SelectNodes(".//Field")
            Dim basField As BaseField = LoadField(xNode, basTable.TableName, basTable.TableAlias)
            basTable.Add(basField)
            basField.Index = basTable.Count
        Next

        Return True
    End Function

    Public Function LoadTablesXml(xmlTables As XmlDocument) As XmlDocument
        ResetError()
        If dhdText.CheckFile(dhdText.PathConvert(CheckFilePath(curVar.TablesFile))) = True Then
            Try
                xmlTables.Load(dhdText.PathConvert(CheckFilePath(curVar.TablesFile)))
                curStatus.TablesReload = False
            Catch ex As Exception
                ErrorMessage = "There was an error reading the XML file. Please check the log."
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CheckFilePath(curVar.TablesFile)) & Environment.NewLine & ex.Message, 1)
                Return Nothing
            End Try
        Else
            ErrorLevel = 5
            ErrorMessage = "Invalid file path: " & dhdText.PathConvert(CheckFilePath(curVar.TablesFile))
        End If
        Return xmlTables
    End Function

    Public Function LoadField(xNode As XmlNode, strTableName As String, strTableAlias As String) As BaseField
        Dim basfield As New BaseField
        If xNode.Item("FldSearchList").InnerText = True And xNode.Item("DataType").InnerText.ToUpper <> "BIT" Then
            basfield.Category = 5
        ElseIf xNode.Item("DataType").InnerText.ToUpper = "BIT" Then
            basfield.Category = 2
        Else
            basfield.Category = 1
        End If

        basfield.FieldTableName = strTableName
        basfield.FieldTableAlias = strTableAlias
        If dhdText.CheckNodeElement(xNode, "FldName") Then basfield.FieldName = xNode.Item("FldName").InnerText
        If dhdText.CheckNodeElement(xNode, "FldAlias") Then basfield.FieldAlias = xNode.Item("FldAlias").InnerText
        If basfield.FieldAlias = "" Then basfield.FieldAlias = basfield.FieldName
        basfield.Name = strTableName & "." & basfield.FieldAlias
        If dhdText.CheckNodeElement(xNode, "FldName") Then basfield.Name = strTableName & "." & xNode.Item("FldName").InnerText
        If dhdText.CheckNodeElement(xNode, "DataType") Then basfield.FieldDataType = xNode.Item("DataType").InnerText
        If dhdText.CheckNodeElement(xNode, "Identity") Then basfield.Identity = xNode.Item("Identity").InnerText
        If dhdText.CheckNodeElement(xNode, "PrimaryKey") Then basfield.PrimaryKey = xNode.Item("PrimaryKey").InnerText
        If dhdText.CheckNodeElement(xNode, "FldWidth") Then basfield.FieldWidth = xNode.Item("FldWidth").InnerText

        If basfield.Category = 5 Then
            basfield.FieldTableName = strTableName
            'basField.RelationField = basField.Name
        End If

        If dhdText.CheckNodeElement(xNode, "Relations") Then
            If xNode.Item("Relations").ChildNodes.Count > 0 Then
                For Each xRnode As XmlNode In xNode.Item("Relations").ChildNodes
                    If xRnode.ChildNodes.Count > 1 Then
                        Dim basRelation As BaseRelation = LoadRelation(xRnode)
                        basfield.Add(basRelation)
                        basRelation.Index = basfield.Count
                    End If
                Next
            End If
        End If

        If dhdText.CheckNodeElement(xNode, "DefaultButton") Then basfield.DefaultButton = xNode.Item("DefaultButton").InnerText
        If dhdText.CheckNodeElement(xNode, "DefaultValue") Then basfield.DefaultValue = xNode.Item("DefaultValue").InnerText

        If dhdText.CheckNodeElement(xNode, "FldList") Then basfield.FieldList = xNode.Item("FldList").InnerText
        If basfield.FieldList = True Then
            Try
                basfield.FieldListOrder = xNode.Item("FldList").Attributes("Order").Value
                basfield.FieldListWidth = xNode.Item("FldList").Attributes("Width").Value
            Catch ex As Exception
                basfield.FieldListOrder = 0
                basfield.FieldListWidth = 0
                WriteLog("there was an error setting the value for ListOrder or ListWidth: " & Environment.NewLine & ex.Message, 1)
            End Try
        End If

        If dhdText.CheckNodeElement(xNode, "FldSearch") Then basfield.FieldSearch = xNode.Item("FldSearch").InnerText
        If dhdText.CheckNodeElement(xNode, "FldSearchList") Then basfield.FieldSearchList = xNode.Item("FldSearchList").InnerText
        If dhdText.CheckNodeElement(xNode, "FldUpdate") Then basfield.FieldUpdate = xNode.Item("FldUpdate").InnerText
        If dhdText.CheckNodeElement(xNode, "FldVisible") Then basfield.FieldVisible = xNode.Item("FldVisible").InnerText

        If dhdText.CheckNodeElement(xNode, "ControlField") Then basfield.ControlField = xNode.Item("ControlField").InnerText
        If dhdText.CheckNodeElement(xNode, "ControlValue") Then basfield.ControlValue = xNode.Item("ControlValue").InnerText
        If dhdText.CheckNodeElement(xNode, "ControlUpdate") Then basfield.ControlUpdate = xNode.Item("ControlUpdate").InnerText
        If dhdText.CheckNodeElement(xNode, "ControlMode") Then basfield.ControlMode = xNode.Item("ControlMode").InnerText
        Return basfield
    End Function

    Public Function LoadRelation(XRNode As XmlNode) As BaseRelation
        Dim basRelation As New BaseRelation
        If dhdText.CheckNodeElement(XRNode, "RelationTable") Then basRelation.RelationTable = XRNode.Item("RelationTable").InnerText
        If dhdText.CheckNodeElement(XRNode, "RelationTableAlias") Then basRelation.RelationTableAlias = XRNode.Item("RelationTableAlias").InnerText
        If String.IsNullOrEmpty(basRelation.RelationTableAlias) Then basRelation.RelationTableAlias = basRelation.RelationTable.ToString.Replace(".", "_")
        If dhdText.CheckNodeElement(XRNode, "RelationField") Then basRelation.RelationField = XRNode.Item("RelationField").InnerText
        If dhdText.CheckNodeElement(XRNode, "RelatedFieldName") Then basRelation.RelatedFieldName = XRNode.Item("RelatedFieldName").InnerText
        If dhdText.CheckNodeElement(XRNode, "RelatedFieldAlias") Then basRelation.RelatedFieldAlias = XRNode.Item("RelatedFieldAlias").InnerText
        If dhdText.CheckNodeElement(XRNode, "RelatedFieldList") Then basRelation.RelatedFieldList = XRNode.Item("RelatedFieldList").InnerText
        basRelation.Name = basRelation.RelationTableAlias & "_" & basRelation.RelationField
        Return basRelation
    End Function

    Public Function GetFieldNode(xmlTables As XmlDocument, strTableName As String, strFieldName As String) As XmlNode
        Dim xTNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        Dim xFNode As XmlNode = dhdText.FindXmlChildNode(xTNode, "Fields/Field", "FldName", strFieldName)
        Return xFNode
    End Function

    Public Function GetRelationNode(xmlTables As XmlDocument, strTableName As String, strFieldName As String, strRelationTable As String, strRelationField As String) As XmlNode
        Dim xFNode As XmlNode = GetFieldNode(xmlTables, strTableName, strFieldName)
        Dim xRelNode As XmlNode = GetRelationNode(xFNode, strRelationTable, strRelationField)
        Return xRelNode
    End Function

    Public Function GetRelationNode(FieldNode As XmlNode, strRelationTable As String, strRelationField As String) As XmlNode
        Dim xRNode As XmlNode = dhdText.FindXmlChildNode(FieldNode, "Relations", Nothing, Nothing)
        For Each xRelNode In xRNode.ChildNodes
            If xRelNode.item("RelationTable").innertext = strRelationTable And xRelNode.item("RelationField").innertext = strRelationField Then
                Return xRelNode
            End If
        Next
        Return Nothing
    End Function

    Public Function LoadReports(xmlReports As XmlDocument) As List(Of String)
        ResetError()
        If curStatus.ReportsReload = True Then
            xmlReports = LoadReportsXml(xmlReports)
        End If
        Dim ReturnValue As New List(Of String)
        Try
            Dim xNode As XmlNode
            For Each xNode In xmlReports.SelectNodes("//Report")
                ReturnValue.Add(xNode.Item("ReportName").InnerText)
            Next
        Catch ex As Exception
            ErrorLevel = -1
            ErrorMessage = "There was an error reading the Reports list. Please check the log."
            WriteLog("There was an error reading the Reports list." & Environment.NewLine & ex.Message, 1)
            Return Nothing
        End Try
        Return ReturnValue
    End Function

    Public Function LoadReportsXml(xmlReports As XmlDocument) As XmlDocument
        ResetError()
        If dhdText.CheckFile(dhdText.PathConvert(CheckFilePath(curVar.ReportSetFile))) = True Then
            Try
                xmlReports.Load(dhdText.PathConvert(CheckFilePath(curVar.ReportSetFile)))
                curStatus.ReportsReload = False
            Catch ex As Exception
                ErrorLevel = -1
                ErrorMessage = "There was an error reading the XML file. Please check the log."
                WriteLog("There was an error reading the XML file. Please check the file:" & Environment.NewLine & dhdText.PathConvert(CheckFilePath(curVar.ReportSetFile)) & Environment.NewLine & ex.Message, 1)
                Return Nothing
            End Try
        Else
            ErrorLevel = 5
            ErrorMessage = "Invalid file path: " & dhdText.PathConvert(CheckFilePath(curVar.ReportSetFile))
        End If
        Return xmlReports
    End Function

    Public Function LoadSearches(xmlSearch As XmlDocument, strTable As String) As List(Of String)
        ResetError()
        If curStatus.SearchesReload = True Then
            xmlSearch = LoadSearchXml(xmlSearch)
        End If
        Dim ReturnValue As New List(Of String)
        Try
            Dim xNode As XmlNode
            For Each xNode In dhdText.FindXmlNodes(xmlSearch, "Searches/Search", "TableName", strTable)
                ReturnValue.Add(xNode.Item("SearchName").InnerText)
            Next
        Catch ex As Exception
            ErrorLevel = -1
            ErrorMessage = "There was an error loading the searches. Please check the log."
            WriteLog("There was an error loading the searches:" & Environment.NewLine & ex.Message, 1)
            Return Nothing
        End Try
        Return ReturnValue
    End Function

    Public Function LoadSearchXml(xmlSearch As XmlDocument) As XmlDocument
        ResetError()
        If dhdText.CheckFile(dhdText.PathConvert(CheckFilePath(curVar.SearchFile))) = True Then
            Try
                xmlSearch.Load(dhdText.PathConvert(CheckFilePath(curVar.SearchFile)))
                curStatus.SearchesReload = False
            Catch ex As Exception
                ErrorLevel = -1
                ErrorMessage = "There was an error reading the XML file. Please check the log."
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CheckFilePath(curVar.SearchFile)) & Environment.NewLine & ex.Message, 1)
                Return Nothing
            End Try
        Else
            ErrorLevel = 5
            ErrorMessage = "Invalid file path: " & dhdText.PathConvert(CheckFilePath(curVar.SearchFile))
            xmlSearch.RemoveAll()
        End If
        Return xmlSearch
    End Function

    Public Sub TableClear()
        basTable.Clear()
        basTable.TableName = ""
        basTable.TableAlias = ""
        basTable.TableVisible = False
        basTable.TableSearch = False
        basTable.TableUpdate = False
        basTable.TableInsert = False
        basTable.TableDelete = False
    End Sub

    Public Function GetFieldDataType(xmlTables As XmlDocument, strTableName As String, strFieldName As String) As String
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName)
        Dim strFieldDataType As String = xCNode.Item("DataType").InnerText
        Return strFieldDataType
    End Function

    Public Function FormatFieldXML(xmlTablesDoc As XmlDocument, strTableName As String, strTableAlias As String, strFieldName As String, strShowMode As String, blnUseAlias As Boolean, blnSelect As Boolean, blnConvert As Boolean, DateTimeStyle As Integer) As String
        Dim strOutput As String = ""
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTablesDoc, "Table", "Name", strTableName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName)
        Dim strFieldType As String = xCNode.Item("DataType").InnerText
        Dim strFieldWidth As String = xCNode.Item("FldWidth").InnerText
        Dim strFieldAlias As String = xCNode.Item("FldAlias").InnerText
        If blnUseAlias = False Then strFieldAlias = Nothing

        strOutput = FormatField(strTableName, strTableAlias, strFieldName, strFieldAlias, strFieldType, strFieldWidth, strShowMode, blnSelect, blnConvert, DateTimeStyle)
        Return strOutput
    End Function

    Public Function FormatField(strTableName As String, strTableAlias As String, strFieldName As String, strFieldAlias As String, strFieldType As String, strFieldWidth As String, strShowMode As String, blnSelect As Boolean, blnConvert As Boolean, DateTimeStyle As Integer) As String
        Dim strOutput As String = ""
        If String.IsNullOrEmpty(strTableAlias) Then strTableAlias = strTableName.Replace(".", "_")
        'Dim strFQDN As String = "[" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "]"
        Dim strFQDN As String = "[" & strTableAlias & "].[" & strFieldName & "]"

        If Not strShowMode Is Nothing Then
            Select Case strShowMode.ToUpper
                Case "SUM", "AVG", "COUNT"
                    strFQDN = strShowMode & "(" & strFQDN & ")"
                    strFieldType = "INTEGER"
                Case "MIN", "MAX"
                    strFQDN = strShowMode & "(" & strFQDN & ")"
                Case "YEAR", "MONTH", "DAY", "HOUR", "MINUTE", "SECOND"
                    strFQDN = "DATEPART(" & strShowMode & "," & strFQDN & ")"
                    strFieldType = "INTEGER"
                Case "DATE"
                    strFQDN = "CAST(" & strFQDN & " AS DATE)"
                    strFieldType = "DATETIME"
                Case "TIME"
                    strFQDN = "CAST(" & strFQDN & " AS TIME)"
                    strFieldType = "TIME"
                Case Else
                    'Do Nothing
            End Select
            If Not strFieldAlias Is Nothing Then
                strFieldAlias = strShowMode.ToLower & strFieldAlias
            Else
                strFieldAlias = strShowMode.ToLower & strFieldName
            End If
        End If

        If blnConvert = True Or (strFieldType.ToUpper = "DATETIME" And Not DateTimeStyle = Nothing) Then
            Select Case strFieldType.ToUpper
                Case "IMAGE"
                    strOutput = "(CONVERT([varchar](" & strFieldWidth & "), " & strFQDN & "))"
                Case "BINARY", "GEO", "TEXT", "GUID"
                    strOutput = "(CONVERT([nvarchar](" & strFieldWidth & "), " & strFQDN & "))"
                Case "TIME", "TIMESTAMP"
                    Dim intFieldWidth As Integer = 8
                    If IsNumeric(strFieldWidth) = 1 And strFieldWidth < intFieldWidth Then intFieldWidth = strFieldWidth
                    Select Case DateTimeStyle
                        Case 101, 100
                            strOutput = "(CONVERT([nvarchar](7), " & strFQDN & ", 100))"
                        Case 105, 102
                            strOutput = "(CONVERT([nvarchar](8), " & strFQDN & ", 120))"
                        Case Else
                            strOutput = "(CONVERT([nvarchar](13), " & strFQDN & ", " & DateTimeStyle & "))"
                    End Select
                Case "XML"
                    strOutput = "(CONVERT([nvarchar](max), " & strFQDN & "))"
                Case "DATETIME"
                    strOutput = "(CONVERT([nvarchar](" & strFieldWidth & "), " & strFQDN & ", " & DateTimeStyle & "))"
                Case Else
                    'CHAR: no need to convert char or int values to char.
                    strOutput = strFQDN
            End Select
        Else
            strOutput = strFQDN
        End If

        If blnSelect = True Then
            If Not strFieldAlias Is Nothing Then
                If strFieldAlias.Length > 0 Then
                    strOutput &= " AS [" & strFieldAlias & "]"
                End If
            End If
        End If

        Return strOutput
    End Function

    Public Function FormatFieldWhere(strFieldName As String, strTableAlias As String, strFieldWidth As String, strFieldType As String, strFieldValue As String) As String
        Dim strOutput As String = ""
        Dim strTableField As String = " [" & strTableAlias.Replace(".", "].[") & "].[" & strFieldName & "]"

        If strFieldValue = "NULL" Or strFieldValue = "" Then
            Select Case strFieldType.ToUpper
                Case "CHAR", "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                    strOutput = " (COALESCE(" & strTableField & ",'') = '') " & Environment.NewLine
                Case "INTEGER", "DATETIME", "BIT"
                    strOutput = " (COALESCE(" & strTableField & ",0) = 0) " & Environment.NewLine
                Case "IMAGE"
                    'do nothing. cannot search on an image data type.
                Case Else
                    'try the default CHAR action
                    strOutput = " (COALESCE(" & strTableField & ",'') = '') " & Environment.NewLine
            End Select
            Return strOutput
        Else
            If strFieldValue.Contains(",") Then
                strFieldValue = strFieldValue.Trim(",")
                If strFieldValue.Length = 0 Then Return strOutput
                Select Case strFieldType.ToUpper
                    Case "CHAR", "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                        strOutput = " (" & strTableField & " IN ('" & Replace(strFieldValue, ",", "','") & "'))" & Environment.NewLine
                    Case "INTEGER", "BIT"
                        strOutput = " (" & strTableField & " IN (" & strFieldValue & "))" & Environment.NewLine
                    Case "DATETIME"
                        strOutput = " ((CONVERT([nvarchar](" & strFieldWidth & "), " & strTableField & ", " & curVar.DateTimeStyle & ")) IN ('" & strFieldValue.Replace(",", "','") & "'))" & Environment.NewLine
                    Case "IMAGE"
                        'do nothing. cannot search on an image data type.
                    Case Else
                        'try the default CHAR action
                        strOutput = " (" & strTableField & " IN ('" & Replace(strFieldValue, ",", "','") & "'))" & Environment.NewLine
                End Select
                Return strOutput
            Else
                If strFieldValue.Trim().Contains(" ") Then
                    Dim strArgs As String() = strFieldValue.Trim().Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                    For Each strArg As String In strArgs
                        If strArg IsNot Nothing AndAlso strArg.Trim().Length > 0 Then
                            strOutput &= " AND (" & strTableField & " LIKE ('%" & strArg.Trim() & "%'))" & Environment.NewLine
                        End If
                    Next
                    Dim strTest As String = strOutput.Substring(0, 4)
                    If strOutput.Substring(0, 4) = " AND" Then
                        'if the value starts with AND, remove it.
                        strOutput = strOutput.Remove(0, 4)
                    End If
                    Return strOutput
                Else

                    Select Case strFieldType.ToUpper
                        Case "CHAR"
                            strOutput = " (" & strTableField & " LIKE '%" & strFieldValue & "%')"
                        Case "INTEGER"
                            strOutput = " (" & strTableField & " LIKE '%" & strFieldValue & "%')"
                        Case "DATETIME"
                            strOutput = " (CONVERT([nvarchar](" & strFieldWidth & "), " & strTableField & ", " & curVar.DateTimeStyle & ")) LIKE '%" & strFieldValue & "%')"
                        Case "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                            strOutput = " (CONVERT([nvarchar](" & strFieldWidth & "), " & strTableField & ")) LIKE '%" & strFieldValue & "%')"
                        Case "BIT"
                            strOutput = " (COALESCE(" & strTableField & ",0) = " & strFieldValue & ") "
                        Case "IMAGE"
                            'do nothing. cannot search on an image data type.
                        Case Else
                            'try the default CHAR action
                            strOutput = " (" & strTableField & " LIKE '%" & strFieldValue & "%')"
                    End Select
                    Return strOutput

                End If
            End If
        End If

        Return strOutput
    End Function

    Public Function ReportQueryBuild(xmlQueryDoc As XmlDocument, xmlTables As XmlDocument, strReportName As String, DateTimeStyle As Integer) As String
        Dim strTableName As String = "", strTableAlias As String = ""
        Dim strFieldName As String = ""
        Dim strShowMode As String = Nothing
        'Dim strHavingMode As String = Nothing
        Dim strQueryFrom As String = ""
        Dim strQueryWhere As String = "WHERE "
        Dim strWhereClause As String = ""
        Dim strWhereMode As String = ""
        Dim intSort As Integer = 0
        Dim intMaxSort As Integer = 0

        Dim strHavingField As String = ""
        Dim strQueryHaving As String = "HAVING "
        Dim strHavingMode As String = Nothing, strHavingType As String = Nothing, strHavingClause As String = Nothing

        Dim strQueryGroup As String = "GROUP BY ", blnGroup As Boolean = False
        Dim strQueryOrder As String = ""

        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlQueryDoc, "Report", "ReportName", strReportName)

        Dim strQuery As String = "SELECT "
        'iterate through all fields

        Dim xmlCNodelist As XmlNodeList = dhdText.FindXmlChildNodes(xNode, "Fields/Field")
        For Each xCNode As XmlNode In xmlCNodelist
            strTableName = xCNode.Item("TableName").InnerText
            If dhdText.CheckNodeElement(xCNode, "TableAlias") Then strTableAlias = xCNode.Item("TableAlias").InnerText Else strTableAlias = strTableName.Replace(".", "_")
            If strTableName.Contains(".") = False Then strTableName = "dbo." & strTableName
            strFieldName = xCNode.Item("FieldName").InnerText
            If IsNumeric(xCNode.Item("FieldSortOrder").InnerText) Then
                intSort = xCNode.Item("FieldSortOrder").InnerText
                If intSort > intMaxSort Then intMaxSort = intSort
            End If
            Try
                If xCNode.Item("FieldShow").InnerText = 1 Then
                    strShowMode = xCNode.Item("FieldShowMode").InnerText
                    Dim strQueryField As String = FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, True, True, True, DateTimeStyle)
                    strQuery &= ", " & strQueryField
                    Select Case strShowMode
                        Case Nothing
                            strQueryGroup &= ", " & FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, False, False, True, DateTimeStyle)
                        Case "DATE", "YEAR", "MONTH", "DAY", "TIME", "HOUR", "MINUTE", "SECOND"
                            strQueryGroup &= ", " & FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, False, False, True, DateTimeStyle)
                        Case Else
                            blnGroup = True
                    End Select
                End If

                'xCNode.Item("FieldSort").InnerText)
                'xCNode.Item("FieldSortOrder").InnerText)

                For Each xFnode In dhdText.FindXmlChildNodes(xCNode, "Filters/Filter")
                    If xFnode.Item("FilterEnabled").InnerText = "Indeterminate" Then
                        blnGroup = True
                        strHavingMode = xFnode.Item("FilterMode").InnerText
                        If strHavingMode = "" Then strHavingMode = "AND"
                        If strHavingMode.Contains("AND") Then strHavingMode = ") " & strHavingMode & " ("
                        strHavingType = xFnode.Item("FilterType").InnerText
                        strHavingClause = SetDelimiters(xFnode.Item("FilterText").InnerText, GetFieldDataType(xmlTables, strTableName, strFieldName), strHavingType, strShowMode)
                        strHavingField = " " & FormatFieldXML(xmlTables, strTableName, strTableAlias, strFieldName, strShowMode, False, False, True, DateTimeStyle)
                        If strHavingType.Contains("LIKE") And strHavingClause.Contains("*") Then strHavingClause = strHavingClause.Replace("*", "%")

                        If Not strHavingType = Nothing And Not strHavingClause = Nothing Then
                            'If strHavingMode = Nothing Then
                            '    strQueryHaving &= " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            'Else
                            strQueryHaving &= " " & strHavingMode & " " & strHavingField & " " & strHavingType & " " & strHavingClause
                            'End If
                        End If
                    End If

                    If xFnode.Item("FilterEnabled").InnerText = "Checked" Then
                        strWhereMode = xFnode.Item("FilterMode").InnerText
                        If strWhereMode = "" Then strWhereMode = "AND"
                        If strWhereMode.Contains("AND") Then strWhereMode = ") " & strWhereMode & " ("
                        strWhereClause = SetDelimiters(xFnode.Item("FilterText").InnerText, GetFieldDataType(xmlTables, strTableName, strFieldName), xFnode.Item("FilterType").InnerText)

                        If xFnode.Item("FilterType").InnerText.Contains("LIKE") And strWhereClause.Contains("*") Then strWhereClause = strWhereClause.Replace("*", "%")
                        If xFnode.Item("FilterType").InnerText <> "" And strWhereClause <> "" Then
                            strQueryWhere &= " " & strWhereMode & " " & GetTableAliasFromString(strTableAlias) & "." & strFieldName & " " & xFnode.Item("FilterType").InnerText & " " & strWhereClause
                        End If
                    End If

                Next
            Catch ex As Exception
                'Skip this field
            End Try
        Next


        If strQuery = "SELECT " Then
            Return Nothing
        End If
        strQuery = strQuery.Replace("SELECT ,", "SELECT ")
        'Check for top x 
        If xNode.Item("UseTop").InnerText = True And IsNumeric(xNode.Item("UseTopNumber").InnerText) = True Then strQuery = strQuery.Replace("SELECT ", "SELECT TOP " & xNode.Item("UseTopNumber").InnerText & " ")
        'Check for distinct
        If xNode.Item("UseDistinct").InnerText = True Then strQuery = strQuery.Replace("SELECT ", "SELECT DISTINCT ")
        strQueryGroup = strQueryGroup.Replace("GROUP BY ,", "GROUP BY ")
        If strQueryGroup = "GROUP BY " Then blnGroup = False

        strQueryFrom = FromClauseGet(xNode)
        strQuery &= Environment.NewLine & strQueryFrom
        strQueryWhere &= ")"
        strQueryWhere = strQueryWhere.Replace("WHERE  ) AND", "WHERE ").Replace("WHERE  OR", "WHERE (").Replace("WHERE  ) AND NOT", "WHERE NOT (")
        strQueryHaving &= ")"
        strQueryHaving = strQueryHaving.Replace("HAVING  ) AND", "HAVING ").Replace("HAVING  OR", "HAVING (").Replace("HAVING  ) AND NOT", "HAVING NOT (")
        'strQueryHaving = strQueryHaving..Replace("HAVING  ) OR", "HAVING ")

        If strQueryWhere.Length > 10 Then strQuery &= Environment.NewLine & strQueryWhere
        If blnGroup = True Then strQuery &= Environment.NewLine & strQueryGroup
        If strQueryHaving.Length > 11 Then strQuery &= Environment.NewLine & strQueryHaving
        strQueryOrder = OrderClauseGet(xmlCNodelist, intMaxSort)
        If strQueryOrder.Length > 10 Then strQuery &= Environment.NewLine & strQueryOrder

        Return strQuery
    End Function

    Private Function FromClauseGet(XNode As XmlNode) As String
        Dim strFromClause As String = "FROM "
        Dim strFromSource As String = Nothing, strFromType As String = Nothing, strFromRelation As String = Nothing, strTargetTable As String = Nothing, strTargetTableAlias As String = Nothing, strTargetField As String = Nothing

        Dim intCount As Integer = 0
        For Each xTNode As XmlNode In dhdText.FindXmlChildNodes(XNode, "Relations/Relation")
            Dim strTableName As String = xTNode.Item("TableName").InnerText
            Dim strTableAlias As String = ""
            If dhdText.CheckNodeElement(xTNode, "TableAlias") Then strTableAlias = GetTableAliasFromString(xTNode.Item("TableAlias").InnerText)
            If String.IsNullOrEmpty(strTableAlias) Then strTableAlias = GetTableAliasFromString(xTNode.Item("TableName").InnerText)

            If intCount = 0 Then strFromClause &= strTableName & " " & strTableAlias
            If xTNode.Item("RelationEnabled").InnerText = "True" Then

                strFromSource = xTNode.Item("RelationSource").InnerText
                strFromType = xTNode.Item("RelationJoinType").InnerText

                If dhdText.CheckNodeElement(xTNode, "RelationTarget") Then
                    strFromRelation = xTNode.Item("RelationTarget").InnerText
                    strTargetTable = GetTableNameFromString(strFromRelation)
                    strTargetTableAlias = GetTableAliasFromString(strFromRelation)
                    strTargetField = strFromRelation.Substring(strFromRelation.LastIndexOf(".") + 1, strFromRelation.Length - (strFromRelation.LastIndexOf(".") + 1))
                End If
                If dhdText.CheckNodeElement(xTNode, "RelationTargetTable") Then strTargetTable = xTNode.Item("RelationTargetTable").InnerText
                If dhdText.CheckNodeElement(xTNode, "RelationTargetAlias") Then strTargetTableAlias = xTNode.Item("RelationTargetAlias").InnerText
                If dhdText.CheckNodeElement(xTNode, "RelationTargetField") Then strTargetField = xTNode.Item("RelationTargetField").InnerText

                'strTargetTable = strFromRelation.Substring(0, strFromRelation.LastIndexOf("."))
                'strTargetTable = strTargetTable.Substring(strTargetTable.LastIndexOf("(") + 1, strTargetTable.Length - (strTargetTable.LastIndexOf("(") + 1))

                If strFromClause.Contains(strTargetTable) = True And strFromClause.Contains(strTableName) = False Then
                    strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTableName & " " & strTableAlias & " ON " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                ElseIf strFromClause.Contains(strTargetTable) = True And strFromClause.Contains(strTableName) = True Then
                    strFromClause &= Environment.NewLine & " AND " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                ElseIf strFromClause.Contains(strTargetTable) = False And strFromClause.Contains(strTableName) = False Then
                    strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTargetTable & " " & strTargetTableAlias & " ON " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                ElseIf strFromClause.Contains(strTargetTable) = False And strFromClause.Contains(strTableName) = True Then
                    strFromClause &= Environment.NewLine & strFromType & " JOIN " & strTargetTable & " " & strTargetTableAlias & " ON " & strTableAlias & "." & strFromSource & " = " & strTargetTableAlias & "." & strTargetField
                End If
            End If

            intCount += 1
        Next
        Return strFromClause
    End Function

    Private Function OrderClauseGet(xmlCNodelist As XmlNodeList, intMaxSort As Integer) As String
        Dim strQueryOrder As String = "ORDER BY "
        Dim strTableAlias As String = ""
        Dim strFieldName As String = ""
        Dim strFieldSort As String = ""
        Dim intOrder As Integer = 0

        For intCount As Integer = 1 To intMaxSort
            For Each xCNode As XmlNode In xmlCNodelist
                If IsNumeric(xCNode.Item("FieldSortOrder").InnerText) Then
                    intOrder = xCNode.Item("FieldSortOrder").InnerText
                    If intCount = intOrder Or (intCount > intMaxSort And intOrder > intMaxSort) Then
                        If dhdText.CheckNodeElement(xCNode, "TableAlias") Then strTableAlias = xCNode.Item("TableAlias").InnerText
                        If String.IsNullOrEmpty(strTableAlias) Then strTableAlias = xCNode.Item("TableName").InnerText.Replace(".", "_")
                        'If strTableName.IndexOf(".") = -1 Then strTableName = "dbo." & strTableName
                        strFieldName = xCNode.Item("FieldName").InnerText
                        strQueryOrder &= ", " & strTableAlias & "." & strFieldName
                        strFieldSort = xCNode.Item("FieldSort").InnerText
                        If strFieldSort = "ASC" Or strFieldSort = "DESC" Then strQueryOrder &= " " & strFieldSort
                    End If
                End If
            Next
        Next

        strQueryOrder = strQueryOrder.Replace("ORDER BY ,", "ORDER BY ")
        Return strQueryOrder
    End Function

    Public Function GetTableNameFromAlias(xmlTables As XmlDocument, strInput As String) As String
        Dim strReturn As String = strInput
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Alias", strInput)
        If Not xNode Is Nothing Then
            If dhdText.CheckNodeElement(xNode, "Name") = True Then strReturn = xNode.Item("Name").InnerText
        End If
        Return strReturn
    End Function

    Public Function GetTableNameFromString(strInput As String) As String
        If String.IsNullOrEmpty(strInput) Then Return strInput
        Dim strReturn As String = strInput
        If strInput.Contains("(") Then
            strReturn = strInput.Substring(strInput.IndexOf("(") + 1, strInput.Length - (strInput.IndexOf("(") + 1) - 1)
        Else
            If strInput.IndexOf(".") <> strInput.LastIndexOf(".") Then
                strReturn = strInput.Substring(0, strInput.LastIndexOf("."))
            End If
        End If
        Return strReturn
    End Function

    Public Function GetTableAliasFromString(strInput As String) As String
        If String.IsNullOrEmpty(strInput) Then Return strInput
        Dim strReturn As String = strInput
        If strInput.Contains("(") Then
            strReturn = strInput.Substring(0, strInput.IndexOf("(") - 1)
        End If
        If strInput.IndexOf(".") <> strInput.LastIndexOf(".") Then
            strReturn = strInput.Substring(0, strInput.LastIndexOf("."))
        End If
        If strReturn.IndexOf(".") > -1 Then
            strReturn = strReturn.Replace(".", "_")
        End If
        Return strReturn
    End Function

    Public Function GetFieldNameFromAlias(xmlTables As XmlDocument, strTableName As String, strFieldAlias As String) As String
        Dim strFieldName As String = strFieldAlias
        If String.IsNullOrEmpty(strTableName) = False Then
            Dim xPNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
            If xPNode IsNot Nothing Then
                Dim xNode As XmlNode = dhdText.FindXmlChildNode(xPNode, "Fields/Field", "FldAlias", strFieldAlias)
                If xNode IsNot Nothing Then
                    If dhdText.CheckNodeElement(xNode, "FldName") Then strFieldName = xNode.Item("FldName").InnerText
                End If
            End If
        End If
        Return strFieldName
    End Function

    Public Function GetFieldAliasFromName(xmlTables As XmlDocument, strTableName As String, strFieldName As String) As String
        Dim strFieldAlias As String = strFieldName
        If String.IsNullOrEmpty(strTableName) = False Then
            Dim xPNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
            If xPNode IsNot Nothing Then
                Dim xNode As XmlNode = dhdText.FindXmlChildNode(xPNode, "Fields/Field", "FldName", strFieldName)
                If xNode IsNot Nothing Then
                    If dhdText.CheckNodeElement(xNode, "FldAlias") Then strFieldAlias = xNode.Item("FldAlias").InnerText
                End If
            End If
        End If
        Return strFieldAlias
    End Function

#End Region

#Region "DataBase"
    Public Function QueryDb(ByVal dhdConnect As DataHandler.db, ByVal strQueryData As String, ByVal ReturnValue As Boolean, Optional ByVal LogLevel As Integer = 5) As DataSet
        If String.IsNullOrWhiteSpace(strQueryData) Then
            WriteLog("No query was created, aborting querying database", LogLevel)
            Return Nothing
        End If
        WriteLog(strQueryData, LogLevel)
        ErrorMessage = ""
        dhdConnect.CheckDB()
        If dhdConnect.DataBaseOnline = False Then
            ErrorMessage = "The database was not found. Please check your settings"
            Return Nothing
        End If
        Dim dtsData As DataSet
        Try
            dtsData = dhdConnect.QueryDatabase(strQueryData, ReturnValue)
            ErrorLevel = dhdConnect.ErrorLevel
            If ErrorLevel <> 0 Then
                ErrorMessage = dhdConnect.ErrorMessage
                Return Nothing
            End If
            Return dtsData
        Catch ex As Exception
            WriteLog(ex.Message, 1)
            ErrorMessage = ex.Message
            Return Nothing
        End Try
    End Function

    Public Function GetSqlVersion(dhdConnect As DataHandler.db) As Integer
        Dim strDatabase As String = dhdConnect.DatabaseName
        dhdConnect.DatabaseName = "master"
        Dim intSqlVersion As Integer = dhdConnect.GetSqlVersion()
        dhdConnect.DatabaseName = strDatabase

        Return intSqlVersion
    End Function

    Public Function GetDataType(strInput As String) As String
        Select Case strInput
            Case "varchar"
                Return "CHAR"
            Case "char"
                Return "CHAR"
            Case "nvarchar"
                Return "CHAR"
            Case "nchar"
                Return "CHAR"
            Case "bit"
                Return "BIT"
            Case "tinyint"
                Return "INTEGER"
            Case "smallint"
                Return "INTEGER"
            Case "int"
                Return "INTEGER"
            Case "bigint"
                Return "INTEGER"
            Case "date"
                Return "DATETIME"
            Case "time"
                Return "TIME"
            Case "datetime"
                Return "DATETIME"
            Case "smalldatetime"
                Return "DATETIME"
            Case "datetime2"
                Return "DATETIME"
            Case "datetimeoffset"
                Return "CHAR"
            Case "timestamp"
                Return "TIMESTAMP"
            Case "decimal"
                Return "INTEGER"
            Case "numeric"
                Return "INTEGER"
            Case "real"
                Return "INTEGER"
            Case "float"
                Return "INTEGER"
            Case "smallmoney"
                Return "INTEGER"
            Case "money"
                Return "INTEGER"
            Case "uniqueidentifier"
                Return "GUID"
            Case "image"
                Return "IMAGE"
            Case "sql_variant"
                Return "BINARY"
            Case "hierarchyid"
                Return "CHAR"
            Case "geometry"
                Return "GEO"
            Case "geography"
                Return "GEO"
            Case "varbinary"
                Return "BINARY"
            Case "binary"
                Return "BINARY"
            Case "text"
                Return "TEXT"
            Case "ntext"
                Return "TEXT"
            Case "xml"
                Return "XML"
            Case "sysname"
                Return "CHAR"
        End Select
        Return ""
    End Function

    Public Function GetDataTypes()
        Dim ReturnValue As New List(Of String)
        ReturnValue.Add("CHAR")
        ReturnValue.Add("INTEGER")
        ReturnValue.Add("BIT")
        ReturnValue.Add("BINARY")
        ReturnValue.Add("GUID")
        ReturnValue.Add("IMAGE")
        ReturnValue.Add("TEXT")
        ReturnValue.Add("DATETIME")
        ReturnValue.Add("TIME")
        ReturnValue.Add("TIMESTAMP")
        ReturnValue.Add("XML")
        ReturnValue.Add("GEO")
        Return ReturnValue
    End Function

    Public Function SetDelimiters(strInput As String, strDataType As String, strCompare As String, Optional strShowMode As String = Nothing) As String
        If strInput = Nothing Then strInput = ""
        If strInput.Length > 2 Then
            If strInput.Substring(0, 2) = "f:" Then
                Return "(" & strInput.Replace("f:", "") & ")"
            End If
            If strInput.Substring(0, 2) = "v:" Then
                strInput = strInput.Replace("v:", "")
                strInput = ProcessDefaultValue(strInput)
            End If
        End If
        If strCompare = Nothing Then Return strInput
        If strCompare = "IS" Or strCompare = "IS NOT" Then
            Return strInput
        End If
        If Not strShowMode = Nothing Then
            If strShowMode = "COUNT" Or _
                strShowMode = "SUM" Or _
                strShowMode = "AVG" Then
                strDataType = "INTEGER"
            End If
        End If
        If Not strDataType = Nothing Then
            If strDataType = "CHAR" Or _
                    strDataType = "BINARY" Or _
                    strDataType = "GUID" Or _
                    strDataType = "TEXT" Or _
                    strDataType = "IMAGE" Or _
                    strDataType = "DATETIME" Or _
                    strDataType = "TIME" Or _
                    strDataType = "TIMESTAMP" Or _
                    strDataType = "XML" Or _
                    strDataType = "GEO" Then
                strInput = strInput.Replace("'", "''")
                strInput = "N'" & strInput & "'"
                If (strCompare = "IN" Or strCompare = "NOT IN") Then
                    strInput = strInput.Replace(",", "','")
                End If
            End If
        End If
        If (strCompare = "IN" Or strCompare = "NOT IN") Then
            strInput = "(" & strInput & ")"
        End If
        Return strInput
    End Function

    Public Function GetWidth(strDataType As String, intMaxLength As Integer) As Integer
        Select Case strDataType
            Case "CHAR", "BINARY"
                If intMaxLength < 50 Then
                    Return 50
                ElseIf intMaxLength < 100 Then
                    Return 100
                ElseIf intMaxLength < 150 Then
                    Return 150
                Else
                    Return 200
                End If
            Case "INTEGER"
                Return Math.Max(intMaxLength, 5) * 10
            Case "BIT"
                Return 25
            Case "GUID", "XML", "TEXT", "IMAGE"
                Return 200
            Case "TIMESTAMP", "GEO", "DATETIME", "TIME"
                Return 100
            Case Else
                Return 50
        End Select
    End Function

    Public Function CompareDataType(strDataType As String) As Boolean
        Select Case strDataType.ToLower
            Case "text", "ntext", "image"
                Return False
            Case Else
                Return True
        End Select
    End Function

    Public Function ProcessDefaultValue(strValue As String) As String
        Dim strReturn As String = strValue
        Dim intFirstBracket As Integer = strValue.IndexOf("(")
        Dim intLastBracket As Integer = strValue.LastIndexOf(")")
        Dim strInput As String = ""

        If intFirstBracket > 0 And intLastBracket > 0 Then

            If intLastBracket - intFirstBracket > 1 Then
                strInput = strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1))
            End If

            Select Case strValue.Substring(0, strValue.IndexOf("(") + 1).ToLower
                Case "now("
                    Dim dtmOutput As DateTime = DateTime.Now
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "HOUR")
                    Catch ex As Exception
                    End Try
                    strReturn = dtmOutput.ToString("yyyy-MM-dd HH:mm:ss")
                Case "date("
                    Dim dtmOutput As DateTime = Date.Today
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "DAY")
                    Catch ex As Exception
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "time("
                    Dim dtmOutput As DateTime = DateTime.Now
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "MINUTE")
                        strReturn = dtmOutput.ToString("HH:mm:ss")
                    Catch ex As Exception
                        strReturn = TimeOfDay.ToString("HH:mm:ss")
                    End Try
                Case "yearstart("
                    Dim dtmOutput As New DateTime(DateTime.Now.Year, 1, 1)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "YEAR")
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "yearend("
                    Dim dtmOutput As New DateTime(DateTime.Now.Year, 1, 1)
                    dtmOutput = dtmOutput.AddYears(1).AddDays(-1)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "YEAR")
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "monthstart("
                    Dim dtmOutput As DateTime = Date.Today.AddDays(-Date.Today.Day + 1)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "MONTH")
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "monthend("
                    Dim dtmOutput As DateTime = Date.Today.AddMonths(1).AddDays(-Date.Today.Day)
                    Dim IntAddMonths As Integer = 1

                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "MONTH")
                        dtmOutput = dtmOutput.AddMonths(1).AddDays(-dtmOutput.Day)
                    Catch ex As Exception
                        strReturn = FormatFileDate(dtmOutput, 120)
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)

                Case "weekstart("
                    Dim IntAddWeeks As Integer = 0
                    Dim dtmOutput As DateTime = Date.Today
                    Dim dayIndex As Integer = dtmOutput.DayOfWeek
                    If dayIndex < DayOfWeek.Monday Then
                        dayIndex += 7 'Monday is first day of week, no day of week should have a smaller index
                    End If
                    Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
                    dtmOutput = dtmOutput.AddDays(-dayDiff)
                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "WEEK")
                    Catch ex As Exception
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "weekend("
                    Dim IntAddWeeks As Integer = 0
                    Dim dtmOutput As DateTime = Date.Today
                    Dim dayIndex As Integer = dtmOutput.DayOfWeek
                    If dayIndex < DayOfWeek.Monday Then
                        dayIndex += 7 'Monday is first day of week, no day of week should have a smaller index
                    End If
                    Dim dayDiff As Integer = dayIndex - DayOfWeek.Monday
                    dtmOutput = dtmOutput.AddDays(-dayDiff).AddDays(6)

                    Try
                        dtmOutput = AlterDate(dtmOutput, strInput, "WEEK")
                    Catch ex As Exception
                    End Try
                    strReturn = FormatFileDate(dtmOutput, 120)
                Case "pi("
                    strReturn = "3.1415926535897932384626433832795"
                Case Else
                    'nothing
            End Select
        End If
        Return strReturn
    End Function

    Public Function AlterDate(dtmInput As DateTime, strInput As String, strDefault As String) As DateTime
        Dim intComma As Integer = 0
        Dim strStep As String = ""
        Dim intStep As Integer = 0

        strInput = strInput.Trim(",")

        If strInput.Contains(",") Then
            Try
                intComma = strInput.IndexOf(",")
            Catch ex As Exception
                'WriteLog("An error occured processing the date enhancer" & strInput & ": " & ex.Message, 1)
            End Try
        End If

        If intComma > 0 Then
            Try
                strStep = strInput.Substring(0, intComma)
                intStep = strInput.Substring(intComma + 1, strInput.Length - (intComma + 1))
            Catch ex As Exception
                'WriteLog("Unable to get the identifiers for the date enhancer" & strInput & ": " & ex.Message, 1)
            End Try
        End If

        If IsNumeric(strInput) Then
            intStep = strInput
            strStep = strDefault
        End If

        Try
            If intStep <> 0 Then
                Select Case strStep.ToUpper
                    Case "MILLISECOND"
                        dtmInput = dtmInput.AddMilliseconds(intStep)
                    Case "SECOND"
                        dtmInput = dtmInput.AddSeconds(intStep)
                    Case "MINUTE"
                        dtmInput = dtmInput.AddMinutes(intStep)
                    Case "HOUR"
                        dtmInput = dtmInput.AddHours(intStep)
                    Case "DAY"
                        dtmInput = dtmInput.AddDays(intStep)
                    Case "WEEK"
                        dtmInput = dtmInput.AddDays(intStep * 7)
                    Case "MONTH"
                        dtmInput = dtmInput.AddMonths(intStep)
                    Case "YEAR"
                        dtmInput = dtmInput.AddYears(intStep)
                End Select
            End If
        Catch ex As Exception
            'WriteLog("An error occured applying the date enhancer" & strInput & ": " & ex.Message, 1)
        End Try

        Return dtmInput
    End Function

    Public Function FormatFileDate(ByVal dtmInput As Date, Optional strFormatStyle As String = Nothing) As String
        If dtmInput = Nothing Then
            FormatFileDate = ""
        Else
            If strFormatStyle = Nothing Then strFormatStyle = 120
            FormatFileDate = dtmInput.ToString("yyyy-MM-dd")
            Select Case strFormatStyle
                Case 120
                    FormatFileDate = dtmInput.ToString("yyyy-MM-dd")
                Case 100
                    FormatFileDate = dtmInput.ToString("MMM dd yyyy")
                Case 101
                    FormatFileDate = dtmInput.ToString("MM/dd/yyyy")
                Case 105
                    FormatFileDate = dtmInput.ToString("dd-MM-yyyy")
                Case 109
                    FormatFileDate = dtmInput.ToString("MMM dd yyyy")
                Case 113
                    FormatFileDate = dtmInput.ToString("dd MMM yyyy")
                Case Else
                    FormatFileDate = dtmInput.ToString("yyyy-MM-dd")
            End Select
        End If
        Return FormatFileDate
    End Function

    Public Function SaveToDatabase(ByVal dhdConnect As DataHandler.db, dtsInput As DataSet, Optional ConvertToText As Boolean = False, Optional ConvertToNull As Boolean = False) As Integer
        Dim intRecordsAffected As Integer = 0
        Dim intReturn As Integer = 0

        Try
            For Each dttInput As DataTable In dtsInput.Tables
                Dim blnExport As Boolean = True
                If dttInput.ExtendedProperties.Count > 0 Then
                    If dttInput.ExtendedProperties.ContainsKey("ExportTable") = True Then
                        If dttInput.ExtendedProperties("ExportTable") = "False" Then
                            blnExport = False
                        End If
                    End If
                End If
                If blnExport = True Then
                    intReturn = UploadSqlData(dhdConnect, dttInput, ConvertToText, ConvertToNull)
                    If intReturn = -1 Then
                        Return -1
                    Else
                        intRecordsAffected += intReturn
                    End If
                End If
            Next
            'intRecordsAffected = dhdDB.UploadSqlData(dgvImport.DataSource)
            Return intRecordsAffected
        Catch ex As Exception
            ErrorLevel = -1
            ErrorMessage = "Export to database failed. Check if the columns match and try again. If you are importing more than 1 table, make sure they have identical columns" & ex.Message
            Return ErrorLevel
        End Try
        Return intRecordsAffected
    End Function

    Public Function UploadSqlData(ByVal dhdConnect As DataHandler.db, ByVal dttInput As DataTable, Optional ConvertToText As Boolean = False, Optional ConvertToNull As Boolean = False) As Integer
        Dim intRecordsAffected As Integer = 0
        intRecordsAffected = dttInput.Rows.Count
        Try
            If dhdConnect.SqlConnection.State = ConnectionState.Open Then dhdConnect.SqlConnection.Close()
            Using bcp As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(dhdConnect.SqlConnection)
                If dhdConnect.SqlConnection.State = ConnectionState.Closed Then dhdConnect.SqlConnection.Open()
                bcp.DestinationTableName = dhdConnect.DataTableName
                If ConvertToText = True Then dttInput = dhdConnect.ConvertToText(dttInput)
                If ConvertToNull = True Then dttInput = dhdConnect.EmptyToNull(dttInput)
                Dim reader As DataTableReader = dttInput.CreateDataReader()
                bcp.WriteToServer(reader)
            End Using
        Catch ex As Exception
            dhdConnect.ErrorMessage = ex.Message
            dhdConnect.ErrorLevel = -1
            intRecordsAffected = -1
        End Try
        'Try
        '    'If dhdConnect.SqlConnection.State = ConnectionState.Open Then dhdConnect.SqlConnection.Close()
        'Catch ex As Exception
        '    dhdConnect.ErrorMessage = ex.Message
        '    dhdConnect.ErrorLevel = -1
        'End Try
        Return intRecordsAffected
    End Function

    Public Function MainSelectBuild(intTop As Integer) As String
        Dim strQuery As String = "SELECT "
        Dim strQuery2 As String = ""
        Dim strQuery3 As String = ""

        For intField As Integer = 0 To basTable.Count - 1
            If basTable.Item(intField).FieldList = True And (basTable.Item(intField).FieldListOrder < 1 Or basTable.Item(intField).FieldListOrder > basTable.Count) Then
                strQuery2 &= ", " & FormatField(basTable.TableName, basTable.TableAlias, basTable.Item(intField).FieldName, basTable.Item(intField).FieldAlias, basTable.Item(intField).FieldDataType, basTable.Item(intField).FieldWidth, Nothing, True, True, curVar.DateTimeStyle)
            End If
            If basTable.Item(intField).FieldList = False And (basTable.Item(intField).Identity = True Or basTable.Item(intField).PrimaryKey = True) Then
                strQuery3 &= ", " & FormatField(basTable.TableName, basTable.TableAlias, basTable.Item(intField).FieldName, basTable.Item(intField).FieldAlias, basTable.Item(intField).FieldDataType, basTable.Item(intField).FieldWidth, Nothing, True, True, curVar.DateTimeStyle)
            End If
            For intField2 As Integer = 0 To basTable.Count - 1
                If basTable.Item(intField2).FieldList = True And basTable.Item(intField2).FieldListOrder = intField + 1 Then
                    strQuery &= ", " & FormatField(basTable.TableName, basTable.TableAlias, basTable.Item(intField2).FieldName, basTable.Item(intField2).FieldAlias, basTable.Item(intField2).FieldDataType, basTable.Item(intField2).FieldWidth, Nothing, True, True, curVar.DateTimeStyle)
                End If
            Next
        Next
        strQuery = strQuery & strQuery2 & strQuery3
        strQuery = strQuery.Replace("SELECT ,", "SELECT ")
        If IsNumeric(intTop) AndAlso intTop >= 0 Then strQuery = strQuery.Replace("SELECT ", "SELECT TOP " & intTop & " ")

        Return strQuery
    End Function

    Public Function MainOrderBuild(blnReverseOrder As Boolean) As String
        Dim intColumnCount As Integer = 0
        Dim strQuery As String = "ORDER BY "
        For intCount As Integer = 1 To curVar.MaxColumnSort
            For intField As Integer = 0 To basTable.Count - 1
                If basTable.Item(intField).FieldList = True And basTable.Item(intField).FieldListOrder = intCount Then
                    Select Case basTable.Item(intField).FieldDataType
                        Case "BINARY", "XML", "GEO", "TEXT", "IMAGE"
                            'No sort order
                        Case Else
                            strQuery &= ", " & FormatField(basTable.TableName, basTable.TableAlias, basTable.Item(intField).FieldName, Nothing, basTable.Item(intField).FieldDataType, basTable.Item(intField).FieldWidth, Nothing, False, False, curVar.DateTimeStyle)
                            If blnReverseOrder = True Then
                                strQuery &= " DESC"
                            End If
                    End Select
                End If
            Next
        Next

        strQuery = strQuery.Replace("ORDER BY ,", "ORDER BY ")
        Return strQuery
    End Function

    Public Function LoadTablesList(ByVal dhdConnect As DataHandler.db, Optional blnCrawlViews As Boolean = True) As List(Of String)
        Dim strQuery As String = "SELECT "
        strQuery &= " sch.[name] + '.' + "
        strQuery &= " tbl.[name] AS TableName FROM sys.tables tbl"
        strQuery &= " INNER JOIN sys.schemas sch"
        strQuery &= " ON tbl.[schema_id] = sch.[schema_id]"
        If blnCrawlViews = True Then
            strQuery &= " UNION SELECT "
            strQuery &= " sch.[name] + '.' + "
            strQuery &= " vw.[name] AS TableName FROM sys.views vw"
            strQuery &= " INNER JOIN sys.schemas sch"
            strQuery &= " ON vw.[schema_id] = sch.[schema_id]"
        End If
        strQuery &= " ORDER BY TableName"

        Dim dtsData As DataSet = QueryDb(dhdConnect, strQuery, True)
        If dhdText.DatasetCheck(dtsData) = False Then Return Nothing

        Dim ReturnValue As New List(Of String)
        For intRowCount = 0 To dtsData.Tables(0).Rows.Count - 1
            ReturnValue.Add(dtsData.Tables.Item(0).Rows(intRowCount).Item("TableName"))
        Next
        Return ReturnValue
    End Function


#End Region

#Region "Import & Export"
    Public Function ImportFile(strFileName As String, Optional blnHasHeaders As Boolean = True, Optional Delimiter As String = ",") As DataSet
        Dim dtsImport As New DataSet
        Try
            Dim strExtension As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1, strFileName.Length - (strFileName.LastIndexOf(".") + 1))
            Select Case strExtension.ToLower
                Case "xml"
                    dtsImport = dhdText.LoadXmlToDataset(strFileName)
                Case "xls", "xlsx"
                    dtsImport = Excel.ImportExcelFile(strFileName)
                Case "csv", "txt"
                    If Delimiter.Length = 0 Then
                        Return Nothing
                    End If
                    dtsImport = dhdText.CsvToDataSet(strFileName, blnHasHeaders, Delimiter)
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            'Error importing file
            ErrorLevel = -1
            ErrorMessage = "The file import failed. Check the file and try again."
            dtsImport = Nothing
        End Try
        Return dtsImport
    End Function

    Public Function GetExportFileName(strFileName As String) As String
        Dim strExtension As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1, strFileName.Length - (strFileName.LastIndexOf(".") + 1))
        Dim strTargetFile As String = strFileName.Substring(0, strFileName.LastIndexOf("."))
        If curVar.IncludeDate = True Then
            strTargetFile = strTargetFile & "_" & FormatFileDate(Now)
        End If
        Dim strExportFile As String = strTargetFile & "." & strExtension
        Return strExportFile
    End Function

    Public Function ExportFile(dtsInput As DataSet, strFileName As String, Optional blnConvertToText As Boolean = False, Optional blnConvertToNull As Boolean = False, Optional blnShowFile As Boolean = False, Optional blnHasHeaders As Boolean = True, Optional Delimiter As String = ",", Optional QuoteValues As Boolean = False, Optional CreateDir As Boolean = False) As Boolean
        Dim strExtension As String = strFileName.Substring(strFileName.LastIndexOf(".") + 1, strFileName.Length - (strFileName.LastIndexOf(".") + 1))

        Try
            If blnConvertToText = True Then dtsInput = dhdConnection.ConvertToText(dtsInput)
            If blnConvertToNull = True Then dtsInput = dhdConnection.EmptyToNull(dtsInput)

            Select Case strExtension.ToLower
                Case "xml"
                    dtsInput = dhdConnection.ConvertToText(dtsInput)
                    dhdText.ExportDataSetToXML(dtsInput, strFileName, CreateDir)
                Case "xlsx", "xls"
                    Excel.CreateExcelDocument(dtsInput, strFileName)
                Case "txt", "csv"
                    dtsInput = dhdConnection.ConvertToText(dtsInput)
                    dhdText.DataSetToCsv(dtsInput.Tables(0), strFileName, blnHasHeaders, Delimiter, QuoteValues)
                Case Else
                    'unknown filetype, do nothing
                    blnShowFile = False
                    WriteLog("Unknown File extension. Unable to export file.", 2)
                    Return False
            End Select
        Catch ex As Exception
            blnShowFile = False
            WriteLog("There was an error exporting the file: " + ex.Message, 1)
            Return False
        End Try

        If blnShowFile = True Then
            Dim p As New Process
            p.StartInfo = New ProcessStartInfo(strFileName)
            p.Start()
        End If

        Return True
    End Function

#End Region

End Class
