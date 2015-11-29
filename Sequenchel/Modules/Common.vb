Imports System.Xml
Imports System.Text
Imports System.Collections.Specialized
Imports System.Data.Sql
Imports System.Security.Cryptography
Imports System.Environment.SpecialFolder

Module Common
    Friend DebugMode As Boolean = False
    Friend ErrorMode As Boolean = False
    Friend DevMode As Boolean = False

    Friend strLicenseName As String = "Thicor Services Demo License"
    Friend strLicenseKey As String
    Friend blnDatabaseOnLine As Boolean = False
    Friend strErrorMessage As String = ""
    Friend strQuery As String = Nothing

    Friend dhdText As New DataHandler.txt
    Friend dhdReg As New DataHandler.reg
    Friend dhdDatabase As New DataHandler.db
    Friend dhdConnection As New DataHandler.db
    Friend xmlDoc As New XmlDocument
    Friend xmlSDBASettings As New XmlDocument
    Friend xmlGeneralSettings As New XmlDocument
    Friend xmlConnections As New XmlDocument
    Friend xmlTableSets As New XmlDocument
    Friend xmlTables As New XmlDocument
    Friend xmlReports As New XmlDocument
    Friend xmlSearch As New XmlDocument

    Friend tblTable As New Table
    Friend arrLabels As New LabelArray
    Friend dtsTable As New DataSet
    Friend dtsReport As New DataSet

    Friend strMessages As New Messages
    Friend CurVar As New Variables
    Friend CurStatus As New CurrentStatus

    Friend clrOriginal As Color = System.Drawing.SystemColors.Window
    Friend clrControl As Color = System.Drawing.SystemColors.Control
    Friend clrDisabled As Color = System.Drawing.SystemColors.ControlLight
    Friend clrMarked As Color = System.Drawing.Color.LightSkyBlue
    Friend clrWarning As Color = System.Drawing.Color.IndianRed
    Friend clrEmpty As Color = System.Drawing.Color.LemonChiffon

    Friend strReport As String = "Sequenchel " & vbTab & " version: " & Application.ProductVersion & "  Licensed by: " & strLicenseName
    Friend dtmElapsedTime As DateTime
    Friend tmsElapsedTime As TimeSpan

    Friend Sub ParseCommands()
        Dim intLength As Integer = 0

        For Each Command As String In My.Application.CommandLineArgs
            Dim intPosition As Integer = Command.IndexOf(":")
            If intPosition < 1 Then intPosition = Command.Length
            Dim strCommand As String = Command.ToLower.Substring(0, intPosition)
            Select Case strCommand
                Case "/debug"
                    DebugMode = True
                Case "/control"
                    CurStatus.Status = CurrentStatus.StatusList.ControlSearch
                Case "/dev"
                    DevMode = True
                Case "/noencryption"
                    CurVar.Encryption = False
                Case "/securityoverride"
                    If Command.Length > intPosition + 1 Then
                        CurVar.OverridePassword = Encrypt(Command.Substring(intPosition + 1, Command.Length - (intPosition + 1)))
                    End If
                Case "/report"
                    'Open Report Tab dirctly
                Case "/connection"
                    'Open the chosen connection
                Case "/TableSet"
                    'Open the chosen TableSet
                Case "/reportname"
                    'Open the report if found
                Case "/exportfile"
                    'Export the report to the chosen file
            End Select
        Next

        'If My.Application.CommandLineArgs.Contains("/debug") Then
        '    DebugMode = True
        '    MessageBox.Show("Running in Debug Mode")
        'End If
        'If My.Application.CommandLineArgs.Contains("/control") Then
        '    CurStatus.Status = CurrentStatus.StatusList.ControlSearch
        'End If
        'If My.Application.CommandLineArgs.Contains("/dev") Then
        '    DevMode = True
        'End If
        'If My.Application.CommandLineArgs.Contains("/noencryption") Then
        '    blnEncryption = False
        'End If

    End Sub

    Public Function GetVersion(strPart As String) As String
        If (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) Then
            Dim ver As Version
            ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
            Select Case strPart
                Case "M"
                    Return String.Format("{0}", ver.Major)
                Case "m"
                    Return String.Format("{0}.{1}", ver.Major, ver.Minor)
                Case "B"
                    Return String.Format("{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build)
                Case "R"
                    Return String.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision)
                Case Else
                    Return String.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision)
            End Select
        Else
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
        End If
    End Function

    Friend Sub SetDefaults()
        dhdReg.RegistryPath = "Software\Thicor\Sequenchel\3.0"

        dhdText.InputFile = "SequenchelDBA.xml"
        dhdText.LogFileName = "Sequenchel.Log"
        dhdText.LogLevel = 5
        dhdText.LogLocation = CurVar.DefaultConfigFilePath & "\LOG"
        dhdText.OutputFile = Environment.SpecialFolder.MyDocuments

        dhdDatabase.LoginMethod = "WINDOWS"
        dhdDatabase.LoginName = "SDBAUser"
        dhdDatabase.Password = "SDBAPassword"
        dhdDatabase.DataLocation = Environment.MachineName & "\SQLEXPRESS"
        dhdDatabase.DatabaseName = "Sequenchel"
        dhdDatabase.DataProvider = "SQL"
        If CurStatus.Status > 3 Then
            CurStatus.Status = CurrentStatus.StatusList.ControlSearch
        Else
            CurStatus.Status = CurrentStatus.StatusList.Search
        End If

    End Sub

    Friend Sub LoadLicense()
        Try
            Dim strLicense As String
            strLicense = dhdReg.ReadAnyRegKey("LicenseName", dhdReg.RegistryPath)
            If strLicense = "-1" Then WriteLog(dhdReg.RegMessage, 1)
            If strLicense <> "-1" Then strLicenseName = strLicense
            strLicenseKey = dhdReg.ReadAnyRegKey("LicenseKey", dhdReg.RegistryPath)
            If strLicenseKey = "-1" Then WriteLog(dhdReg.RegMessage, 1)
        Catch ex As Exception
            If DebugMode = True Then MessageBox.Show(ex.Message)
            WriteLog(ex.Message, 1)
            blnLicenseValidated = False
        End Try

        'If DebugMode Then MessageBox.Show(strLicenseName & Environment.NewLine & dtmExpiryDate.ToString & Environment.NewLine & My.Application.Info.Version.Major.ToString & Environment.NewLine & strLicenseKey)
        If strLicenseKey <> "-1" Then
            If CheckLicenseKey(strLicenseKey, strLicenseName, GetVersion("M"), Nothing) = False Then
                'MessageBox.Show(strMessages.strLicenseError)
                blnLicenseValidated = False
            Else
                blnLicenseValidated = True
                'If DebugMode Then MessageBox.Show(lanStrings.strLicenseValidated)
            End If
        Else
            blnLicenseValidated = False

        End If

        strReport = "Sequenchel " & vbTab & " version: " & Application.ProductVersion & vbTab & "  Licensed to: " & strLicenseName

    End Sub

    Public Sub FieldTextHandler(sender As Object)
        If CurStatus.SuspendActions = False Then
            Select Case sender.FieldCategory
                Case 1
                    If sender.Text <> sender.Tag.ToString Then
                        sender.BackColor = clrMarked
                    Else
                        If sender.ReadOnly = False Then
                            sender.BackColor = clrOriginal
                        Else
                            sender.BackColor = clrDisabled
                        End If
                    End If
                Case 2
                    If sender.Checked.ToString <> sender.Tag.ToString Then
                        sender.BackColor = clrMarked
                    Else
                        If sender.Enabled = True Then
                            sender.BackColor = clrControl
                        Else
                            sender.BackColor = clrDisabled
                        End If
                    End If
                    'Case 3
                    '    If tblTable.TableName = sender.Name.Substring(0, sender.Name.LastIndexOf(".")) Then
                    '        If sender.SelectedItem Is Nothing Then
                    '            LoadLookupList(sender, True, False)
                    '            If sender.Text.ToString <> sender.Tag.ToString Then
                    '                sender.BackColor = clrMarked
                    '            Else
                    '                If sender.Enabled = True Then
                    '                    sender.BackColor = clrOriginal
                    '                Else
                    '                    sender.BackColor = clrDisabled
                    '                End If
                    '            End If
                    '        Else
                    '            sender.DropDownStyle = ComboBoxStyle.DropDown
                    '            If sender.SelectedItem.ToString <> sender.Tag.ToString And sender.Text.ToString <> sender.Tag.ToString Then
                    '                sender.BackColor = clrMarked
                    '            Else
                    '                If sender.Enabled = True Then
                    '                    sender.BackColor = clrOriginal
                    '                Else
                    '                    sender.BackColor = clrDisabled
                    '                End If
                    '            End If
                    '        End If
                    '    End If
                    'Case 4
                    '    'related Lookup with default list
                    '    If sender.SelectedItem Is Nothing Then
                    '        LoadLookupList(sender, True, False)
                    '    Else
                    '        sender.DropDownStyle = ComboBoxStyle.DropDown

                    '    End If

                Case 5
                    'ManagedSelectField 
                    For intField As Integer = 0 To tblTable.Count - 1
                        If tblTable.Item(intField).Name = sender.Name Then
                            If sender.Text.ToString <> sender.Tag.ToString Then
                                sender.BackColor = clrMarked
                            Else
                                If sender.Enabled = True Then
                                    sender.BackColor = clrOriginal
                                Else
                                    sender.BackColor = clrDisabled
                                End If
                            End If
                        End If
                    Next
                Case 6
                    'ManagedSelectField as related field
                    For intField As Integer = 0 To tblTable.Count - 1
                        If tblTable.Item(intField).FieldName = sender.FieldRelatedField Then
                            Select Case tblTable.Item(intField).FieldCategory
                                Case 1, 3, 4, 5
                                    tblTable.Item(intField).Text = sender.Value
                                Case 2
                                    tblTable.Item(intField).Checked = sender.Value
                            End Select
                            'If sender.Text.ToString <> sender.Tag.ToString Then
                            '    sender.BackColor = clrMarked
                            'Else
                            '    If sender.Enabled = True Then
                            '        sender.BackColor = clrOriginal
                            '    Else
                            '        sender.BackColor = clrDisabled
                            '    End If
                            'End If

                        End If
                    Next
            End Select
        End If
    End Sub

    Friend Sub FieldEnableHandler(sender As Object, blnEnabled As Boolean)
        Select Case sender.FieldCategory
            Case 1
                If blnEnabled = True Then
                    sender.ReadOnly = False
                    sender.BackColor = clrOriginal
                Else
                    sender.ReadOnly = True
                    sender.BackColor = clrDisabled
                End If
            Case 2, 3, 4, 5, 6
                sender.Enabled = blnEnabled
        End Select
    End Sub

    Friend Function FieldEnabledCheck(sender As Object) As Boolean
        Dim blnReturn As Boolean = False
        Select Case sender.FieldCategory
            Case 1
                If sender.ReadOnly = False Then
                    blnReturn = True
                Else
                    blnReturn = False
                End If
            Case 2, 3, 4, 5, 6
                blnReturn = sender.Enabled
            Case Else
                blnReturn = False
        End Select
        Return blnReturn
    End Function

    Public Sub LabelClickHandler(sender As Object)
        Dim lblTemp As Label = sender
        For intField As Integer = 0 To tblTable.Count - 1
            If tblTable.Item(intField).Name = lblTemp.Name.Substring(3, lblTemp.Name.Length - 3) Then
                If tblTable.Item(intField).Enabled = True Then
                    If tblTable.Item(intField).BackColor = clrMarked Then
                        If tblTable.Item(intField).FieldDataType = "BIT" Then
                            tblTable.Item(intField).backColor = clrControl
                        Else
                            tblTable.Item(intField).backColor = clrOriginal
                        End If
                    Else
                        tblTable.Item(intField).BackColor = clrMarked
                    End If
                End If
            End If
        Next
    End Sub

    Friend Sub TableClear()
        tblTable.Clear()
        tblTable.TableName = ""
        tblTable.TableAlias = ""
        tblTable.TableVisible = False
        tblTable.TableSearch = False
        tblTable.TableUpdate = False
        tblTable.TableInsert = False
        tblTable.TableDelete = False
    End Sub

    Public Sub DeleteOldLogs(blnUseAutoDelete As Boolean)
        If blnUseAutoDelete = False Or dhdText.AutoDelete = True Then
            'delete old logs
        End If
    End Sub

#Region "XML"
    Friend Sub LoadSDBASettingsXml()
        If dhdText.CheckFile(Application.StartupPath & "\" & dhdText.InputFile) = True Then
            'LoadXmlFile
            Try
                xmlSDBASettings.Load(Application.StartupPath & "\" & dhdText.InputFile)
                If dhdText.CheckElement(xmlSDBASettings, "DefaultConfigFilePath") Then CurVar.DefaultConfigFilePath = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("DefaultConfigFilePath").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "SettingsFile") Then CurVar.GeneralSettings = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("SettingsFile").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "AllowReportQueryEdit") Then CurVar.AllowQueryEdit = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("AllowReportQueryEdit").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "AllowConfigurationChange") Then CurVar.AllowConfiguration = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("AllowConfigurationChange").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "AllowUpdate") Then CurVar.AllowUpdate = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("AllowUpdate").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "AllowInsert") Then CurVar.AllowInsert = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("AllowInsert").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "AllowDelete") Then CurVar.AllowDelete = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("AllowDelete").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "AllowLinkedServersChange") Then CurVar.AllowLinkedServers = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("AllowLinkedServersChange").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "AllowSettingsChange") Then CurVar.AllowSettingsChange = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("AllowSettingsChange").InnerText
                If dhdText.CheckElement(xmlSDBASettings, "OverridePassword") Then
                    If xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("OverridePassword").InnerText.Length > 0 Then
                        If CurVar.OverridePassword = xmlSDBASettings.Item("Sequenchel").Item("Settings").Item("OverridePassword").InnerText Then
                            CurVar.SecurityOverride = True
                        End If
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & Application.StartupPath & "\" & dhdText.InputFile & Environment.NewLine & Environment.NewLine & ex.Message)
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & Application.StartupPath & "\" & dhdText.InputFile & Environment.NewLine & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            If SaveSDBASettingsXml() = False Then
                WriteLog(strMessages.strXmlError, 1)
                MessageBox.Show(strMessages.strXmlError)
            End If
        End If

    End Sub

    Friend Function SaveSDBASettingsXml() As Boolean
        '** Create or update the xml inputdata.
        Dim strXmlText As String
        strXmlText = "<?xml version=""1.0"" standalone=""yes""?>" & Environment.NewLine
        strXmlText &= "<Sequenchel>" & Environment.NewLine
        strXmlText &= "	<Settings>" & Environment.NewLine
        strXmlText &= "		<DefaultConfigFilePath>" & CurVar.DefaultConfigFilePath & "</DefaultConfigFilePath>" & Environment.NewLine
        strXmlText &= "		<SettingsFile>" & CurVar.GeneralSettings & "</SettingsFile>" & Environment.NewLine
        strXmlText &= "		<AllowReportQueryEdit>" & CurVar.AllowQueryEdit & "</AllowReportQueryEdit>" & Environment.NewLine
        strXmlText &= "		<AllowConfigurationChange>" & CurVar.AllowConfiguration & "</AllowConfigurationChange>" & Environment.NewLine
        strXmlText &= "		<AllowUpdate>" & CurVar.AllowUpdate & "</AllowUpdate>" & Environment.NewLine
        strXmlText &= "		<AllowInsert>" & CurVar.AllowInsert & "</AllowInsert>" & Environment.NewLine
        strXmlText &= "		<AllowDelete>" & CurVar.AllowDelete & "</AllowDelete>" & Environment.NewLine
        strXmlText &= "		<AllowLinkedServersChange>" & CurVar.AllowLinkedServers & "</AllowLinkedServersChange>" & Environment.NewLine
        strXmlText &= "		<AllowSettingsChange>" & CurVar.AllowSettingsChange & "</AllowSettingsChange>" & Environment.NewLine
        strXmlText &= "		<OverridePassword>" & CurVar.OverridePassword & "</OverridePassword>" & Environment.NewLine
        strXmlText &= "	</Settings>" & Environment.NewLine
        strXmlText &= "</Sequenchel>" & Environment.NewLine
        Try
            xmlSDBASettings.LoadXml(strXmlText)
            SaveSDBASettingsXml = dhdText.CreateFile(strXmlText, Application.StartupPath & "\" & dhdText.InputFile)
            If SaveSDBASettingsXml = False Then WriteLog("There was an error saving the Settings file" & Environment.NewLine & dhdText.Errormessage, 1)
        Catch ex As Exception
            WriteLog(strMessages.strXmlError & Environment.NewLine & ex.Message, 1)
            SaveSDBASettingsXml = Nothing
        End Try
    End Function

    'Public Function XMLToDataSet(ByVal xmlStr As String) As DataSet
    '    'Convert xmlData to a Dataset
    '    Dim sr As New IO.StringReader(xmlStr)
    '    Dim ds As New DataSet

    '    ds.ReadXml(sr)

    '    For Each relation As DataRelation In ds.Relations
    '        For Each c As DataColumn In relation.ParentColumns
    '            If Not relation.ChildTable.Columns.Contains(c.ColumnName) Then
    '                relation.ChildTable.Columns.Add(c)
    '            End If
    '            For Each dr As DataRow In relation.ChildTable.Rows
    '                dr(c.ColumnName) = dr.GetParentRow(relation)(c.ColumnName)
    '            Next
    '        Next
    '    Next

    '    Return ds
    'End Function

    Friend Sub LoadGeneralSettingsXml()
        If dhdText.CheckFile(CurVar.GeneralSettings) = True Then
            'LoadXmlFile
            Try
                xmlGeneralSettings.Load(dhdText.PathConvert(CurVar.GeneralSettings))
                If dhdText.CheckElement(xmlGeneralSettings, "DataLocation") Then dhdDatabase.DataLocation = xmlGeneralSettings.Item("Sequenchel").Item("DataBase").Item("DataLocation").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "DatabaseName") Then dhdDatabase.DatabaseName = xmlGeneralSettings.Item("Sequenchel").Item("DataBase").Item("DatabaseName").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "DataProvider") Then dhdDatabase.DataProvider = xmlGeneralSettings.Item("Sequenchel").Item("DataBase").Item("DataProvider").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "LoginMethod") Then dhdDatabase.LoginMethod = xmlGeneralSettings.Item("Sequenchel").Item("DataBase").Item("LoginMethod").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "LoginName") Then dhdDatabase.LoginName = xmlGeneralSettings.Item("Sequenchel").Item("DataBase").Item("LoginName").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "Password") Then dhdDatabase.Password = xmlGeneralSettings.Item("Sequenchel").Item("DataBase").Item("Password").InnerText

                If dhdText.CheckElement(xmlGeneralSettings, "LogFileName") Then dhdText.LogFileName = xmlGeneralSettings.Item("Sequenchel").Item("LogSettings").Item("LogFileName").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "LogLevel") Then dhdText.LogLevel = xmlGeneralSettings.Item("Sequenchel").Item("LogSettings").Item("LogLevel").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "LogLocation") Then dhdText.LogLocation = xmlGeneralSettings.Item("Sequenchel").Item("LogSettings").Item("LogLocation").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "Retenion") Then dhdText.Retenion = xmlGeneralSettings.Item("Sequenchel").Item("LogSettings").Item("Retenion").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "AutoDelete") Then dhdText.AutoDelete = xmlGeneralSettings.Item("Sequenchel").Item("LogSettings").Item("AutoDelete").InnerText

                If dhdText.CheckElement(xmlGeneralSettings, "Connections") Then CurVar.ConnectionsFile = xmlGeneralSettings.Item("Sequenchel").Item("Settings").Item("Connections").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "UsersetLocation") Then CurVar.UsersetLocation = xmlGeneralSettings.Item("Sequenchel").Item("Settings").Item("UsersetLocation").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "LimitLookupLists") Then CurVar.LimitLookupLists = xmlGeneralSettings.Item("Sequenchel").Item("Settings").Item("LimitLookupLists").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "LimitLookupListsCount") Then CurVar.LimitLookupListsCount = xmlGeneralSettings.Item("Sequenchel").Item("Settings").Item("LimitLookupListsCount").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "DateTimeStyle") Then CurVar.DateTimeStyle = xmlGeneralSettings.Item("Sequenchel").Item("Settings").Item("DateTimeStyle").InnerText
                If dhdText.CheckElement(xmlGeneralSettings, "IncludeDate") Then CurVar.IncludeDate = xmlGeneralSettings.Item("Sequenchel").Item("Settings").Item("IncludeDate").InnerText

            Catch ex As Exception
                MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & CurVar.GeneralSettings & Environment.NewLine & ex.Message)
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & CurVar.GeneralSettings & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            If SaveGeneralSettingsXml() = False Then
                WriteLog(strMessages.strXmlError, 1)
            End If
        End If

        'dhdDatabase.CheckDB()

        If DebugMode Then
            MessageBox.Show("Sequenchel v " & Application.ProductVersion & Environment.NewLine _
             & "   GeneralSettings = " & CurVar.GeneralSettings & Environment.NewLine _
             & "   DatabaseServer = " & dhdDatabase.DataLocation & Environment.NewLine _
             & "   Database Name = " & dhdDatabase.DatabaseName & Environment.NewLine _
             & "   DataProvider = " & dhdDatabase.DataProvider & Environment.NewLine _
             & "   LoginMethod = " & dhdDatabase.LoginMethod & Environment.NewLine _
             & "   LoginName = " & dhdDatabase.LoginName & Environment.NewLine _
             & "   Password = " & dhdDatabase.Password & Environment.NewLine _
             & "   Main Database online = " & dhdDatabase.DataBaseOnline & Environment.NewLine _
             & Environment.NewLine _
             & "   LogFileName = " & dhdText.LogFileName & Environment.NewLine _
             & "   LogLevel = " & dhdText.LogLevel & Environment.NewLine _
             & "   LogLocation = " & dhdText.LogLocation & Environment.NewLine _
             & "   Retenion = " & dhdText.Retenion & Environment.NewLine _
             & "   AutoDelete = " & dhdText.AutoDelete & Environment.NewLine _
             & Environment.NewLine _
             & "   ConnectionsFile = " & CurVar.ConnectionsFile & Environment.NewLine _
             & "   UsersetLocation = " & CurVar.UsersetLocation & Environment.NewLine _
             & "   DateTimeStyle = " & CurVar.DateTimeStyle & Environment.NewLine _
             & "   IncludeDate = " & CurVar.IncludeDate & Environment.NewLine _
             & "   Running in Debug Mode")
        End If

    End Sub

    Friend Function SaveGeneralSettingsXml() As Boolean
        '** Create or update the xml inputdata.
        Dim strXmlText As String
        strXmlText = "<?xml version=""1.0"" standalone=""yes""?>" & Environment.NewLine
        strXmlText &= "<Sequenchel>" & Environment.NewLine
        strXmlText &= "	<DataBase>" & Environment.NewLine
        strXmlText &= "		<DataLocation>" & dhdDatabase.DataLocation & "</DataLocation>" & Environment.NewLine
        strXmlText &= "		<DatabaseName>" & dhdDatabase.DatabaseName & "</DatabaseName>" & Environment.NewLine
        strXmlText &= "		<DataProvider>" & dhdDatabase.DataProvider & "</DataProvider>" & Environment.NewLine
        strXmlText &= "		<LoginMethod>" & dhdDatabase.LoginMethod & "</LoginMethod>" & Environment.NewLine
        strXmlText &= "		<LoginName>" & dhdDatabase.LoginName & "</LoginName>" & Environment.NewLine
        strXmlText &= "		<Password>" & dhdDatabase.Password & "</Password>" & Environment.NewLine
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
        strXmlText &= "		<Connections>" & CurVar.ConnectionsFile & "</Connections>" & Environment.NewLine
        strXmlText &= "		<UsersetLocation>REGISTRY</UsersetLocation>" & Environment.NewLine
        strXmlText &= "		<LimitLookupLists>" & CurVar.LimitLookupLists & "</LimitLookupLists>" & Environment.NewLine
        strXmlText &= "		<LimitLookupListsCount>" & CurVar.LimitLookupListsCount & "</LimitLookupListsCount>" & Environment.NewLine
        strXmlText &= "		<DateTimeStyle>" & CurVar.DateTimeStyle & "</DateTimeStyle>" & Environment.NewLine
        strXmlText &= "		<IncludeDate>" & CurVar.IncludeDate & "</IncludeDate>" & Environment.NewLine
        strXmlText &= "	</Settings>" & Environment.NewLine
        strXmlText &= "</Sequenchel>" & Environment.NewLine

        Try
            xmlGeneralSettings.LoadXml(strXmlText)
            SaveGeneralSettingsXml = dhdText.CreateFile(strXmlText, CurVar.GeneralSettings)
            If SaveGeneralSettingsXml = False Then WriteLog("There was an error saving the General Settings file" & Environment.NewLine & dhdText.Errormessage, 1)
        Catch ex As Exception
            WriteLog(strMessages.strXmlError & Environment.NewLine & ex.Message, 1)
            SaveGeneralSettingsXml = Nothing
        End Try
    End Function

    Friend Function LoadConnectionsXml() As List(Of String)
        If dhdText.CheckFile(CurVar.ConnectionsFile) = True Then
            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlConnections.Load(dhdText.PathConvert(CurVar.ConnectionsFile))
                Dim blnConnectionExists As Boolean = False
                CurVar.ConnectionDefault = ""

                Dim xNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each xNode In xmlConnections.SelectNodes("//Connection")
                    If xNode.Item("ConnectionName").InnerText = CurStatus.Connection Then blnConnectionExists = True
                    ReturnValue.Add(xNode.Item("ConnectionName").InnerText)
                    If xNode.Attributes("Default").Value = "True" Then
                        CurVar.ConnectionDefault = xNode.Item("ConnectionName").InnerText
                    End If
                Next
                If blnConnectionExists = False Then CurStatus.Connection = CurVar.ConnectionDefault
                Return ReturnValue
            Catch ex As Exception
                MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.ConnectionsFile) & Environment.NewLine & ex.Message)
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.ConnectionsFile) & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            xmlConnections.RemoveAll()
            xmlTableSets.RemoveAll()
            CurVar.TableSetsFile = ""
            xmlTables.RemoveAll()
            CurVar.TablesFile = ""
            TableClear()
            dhdConnection = dhdDatabase
        End If
        Return Nothing
    End Function

    Friend Sub LoadConnection(strConnection As String)
        Dim xmlConnNode As XmlNode = dhdText.FindXmlNode(xmlConnections, "Connection", "ConnectionName", strConnection)

        dhdConnection.DataLocation = xmlConnNode.Item("DataLocation").InnerText
        dhdConnection.DatabaseName = xmlConnNode.Item("DataBaseName").InnerText
        dhdConnection.DataProvider = xmlConnNode.Item("DataProvider").InnerText
        dhdConnection.LoginMethod = xmlConnNode.Item("LoginMethod").InnerText
        dhdConnection.LoginName = xmlConnNode.Item("LoginName").InnerText
        dhdConnection.Password = DataHandler.txt.DecryptText(xmlConnNode.Item("Password").InnerText)
        CurVar.TableSetsFile = xmlConnNode.Item("TableSets").InnerText

        dhdConnection.CheckDB()

        'If DebugMode Then
        '    MessageBox.Show(xmlConnNode.OuterXml & Environment.NewLine & _
        '    dhdConnection.DataLocation & Environment.NewLine & _
        '    dhdConnection.DatabaseName & Environment.NewLine & _
        '    dhdConnection.DataProvider & Environment.NewLine & _
        '    dhdConnection.LoginMethod & Environment.NewLine & _
        '    dhdConnection.LoginName & Environment.NewLine & _
        '    dhdConnection.Password & Environment.NewLine & _
        '    dhdConnection.DataBaseOnline & Environment.NewLine & _
        '    CurVar.TableSetsFile)
        'End If
        'Dim xmlConnNode As XmlNode = xmlConnections.SelectSingleNode("\\Connection", "descendant::Connection[DataBaseName='" & strConnection & "']")

    End Sub

    Friend Function LoadTableSetsXml() As List(Of String)
        If dhdText.CheckFile(CurVar.TableSetsFile) = True Then

            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlTableSets.Load(dhdText.PathConvert(CurVar.TableSetsFile))
                CurVar.TableSetDefault = ""
                Dim blnTableSetExists As Boolean = False

                Dim TableSetNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each TableSetNode In xmlTableSets.SelectNodes("//TableSet")
                    If TableSetNode.Item("TableSetName").InnerText = CurStatus.TableSet Then blnTableSetExists = True
                    ReturnValue.Add(TableSetNode.Item("TableSetName").InnerText)
                    If TableSetNode.Attributes("Default").Value = "True" Then
                        CurVar.TableSetDefault = TableSetNode.Item("TableSetName").InnerText
                    End If
                Next
                If blnTableSetExists = False Then CurStatus.TableSet = CurVar.TableSetDefault
                Return ReturnValue
            Catch ex As Exception
                MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.TableSetsFile) & Environment.NewLine & ex.Message)
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.TableSetsFile) & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            xmlTableSets.RemoveAll()
            xmlTables.RemoveAll()
            CurVar.TablesFile = ""
            TableClear()
        End If
        Return Nothing
    End Function

    Friend Sub LoadTableSet(strTableSet As String)
        Dim xmlTSNode As XmlNode = dhdText.FindXmlNode(xmlTableSets, "TableSet", "TableSetName", strTableSet)
        If xmlTSNode Is Nothing Then Exit Sub
        CurStatus.TableSet = xmlTSNode.Item("TableSetName").InnerText
        CurVar.TablesFile = xmlTSNode.Item("TablesFile").InnerText
        dhdText.OutputFile = xmlTSNode.Item("OutputPath").InnerText
        CurVar.ReportSetFile = xmlTSNode.Item("ReportSet").InnerText
        If dhdText.CheckNodeElement(xmlTSNode, "Search") Then CurVar.SearchFile = xmlTSNode.Item("Search").InnerText

        'If DebugMode Then
        '    MessageBox.Show(xmlTSNode.OuterXml & Environment.NewLine & _
        '        CurVar.TableSet & Environment.NewLine & _
        '        CurVar.TablesFile & Environment.NewLine & _
        '        dhdText.OutputFile & Environment.NewLine & _
        '        CurVar.ReportSet)
        'End If

    End Sub

    Friend Function LoadTablesXml() As List(Of String)
        If dhdText.CheckFile(CurVar.TablesFile) = True Then

            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlTables.Load(dhdText.PathConvert(CurVar.TablesFile))
                Dim blnTableExists As Boolean = False
                CurVar.TableDefault = ""

                Dim TableNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each TableNode In xmlTables.SelectNodes("//Table")
                    If TableNode.Item("Name").InnerText = CurStatus.Table Then blnTableExists = True
                    ReturnValue.Add(TableNode.Item("Alias").InnerText)
                    If TableNode.Attributes("Default").Value = "True" Then
                        CurVar.TableDefault = TableNode.Item("Alias").InnerText
                    End If
                Next
                If blnTableExists = False Then CurStatus.Table = CurVar.TableDefault
                Return ReturnValue
            Catch ex As Exception
                MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.TablesFile) & Environment.NewLine & ex.Message)
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.TablesFile) & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            xmlTables.RemoveAll()
            Dim root As XmlElement = xmlTables.DocumentElement
            If root Is Nothing Then
                xmlTables = dhdText.CreateRootDocument(xmlTables, "Sequenchel", "Tables", True)
            End If
        End If
        Return Nothing
    End Function

    Friend Function LoadReportsXml() As List(Of String)
        If dhdText.CheckFile(CurVar.ReportSetFile) = True Then
            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlReports.Load(dhdText.PathConvert(CurVar.ReportSetFile))
                'Dim blnConnectionExists As Boolean = False
                'CurVar.ConnectionDefault = ""

                Dim xNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each xNode In xmlReports.SelectNodes("//Report")
                    'If xNode.Item("ReportName").InnerText = CurStatus.Connection Then blnConnectionExists = True
                    ReturnValue.Add(xNode.Item("ReportName").InnerText)
                    'If xNode.Attributes("Default").Value = "True" Then
                    '    CurVar.ConnectionDefault = xNode.Item("ConnectionName").InnerText
                    'End If
                Next
                ' If blnConnectionExists = False Then CurStatus.Connection = CurVar.ConnectionDefault
                Return ReturnValue
            Catch ex As Exception
                MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.ReportSetFile) & Environment.NewLine & ex.Message)
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.ReportSetFile) & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            xmlReports.RemoveAll()
        End If
        Return Nothing
    End Function

    Friend Function LoadSearchXml(strTable As String) As List(Of String)
        If dhdText.CheckFile(CurVar.SearchFile) = True Then
            'LoadXmlFile
            'Dim lstXml As XmlNodeList
            Try
                xmlSearch.Load(dhdText.PathConvert(CurVar.SearchFile))
                'Dim blnConnectionExists As Boolean = False
                'CurVar.ConnectionDefault = ""

                Dim xNode As XmlNode
                Dim ReturnValue As New List(Of String)
                For Each xNode In dhdText.FindXmlNodes(xmlSearch, "Searches/Search", "TableName", strTable)
                    ReturnValue.Add(xNode.Item("SearchName").InnerText)
                Next
                Return ReturnValue
            Catch ex As Exception
                MessageBox.Show("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.SearchFile) & Environment.NewLine & ex.Message)
                WriteLog("There was an error reading the XML file. Please check the file" & Environment.NewLine & dhdText.PathConvert(CurVar.SearchFile) & Environment.NewLine & ex.Message, 1)
            End Try
        Else
            xmlSearch.RemoveAll()
        End If
        Return Nothing
    End Function

    Friend Sub XmlExportListView(lvwInput As ListView)
        'Create xml file
        Dim xmlDoc As New XmlDocument
        Dim xmlDec As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes")
        Dim RootNode As XmlElement = xmlDoc.CreateElement("Sequenchel")
        xmlDoc.InsertBefore(xmlDec, xmlDoc.DocumentElement)
        xmlDoc.AppendChild(RootNode)

        'Add parent Node
        Dim ParentNode As XmlElement = xmlDoc.CreateElement("Servers")
        'ParentNode.SetAttribute("Name", cbxLanguage.Text)
        xmlDoc.DocumentElement.AppendChild(ParentNode)

        'Add Items to parentnode
        For Each lanItem In lvwInput.Items
            Dim ItemNode As XmlElement = xmlDoc.CreateElement("Server")

            For intHeader As Integer = 0 To lvwInput.Columns.Count - 1
                Dim Childnode As XmlElement = xmlDoc.CreateElement(lvwInput.Columns(intHeader).Text)
                Dim ItemText As XmlText = xmlDoc.CreateTextNode(lanItem.SubItems(intHeader).Text)
                ItemNode.AppendChild(Childnode)
                Childnode.AppendChild(ItemText)
            Next
            ParentNode.AppendChild(ItemNode)
        Next

        Using sw As New System.IO.StringWriter()
            ' Make the XmlTextWriter to format the XML.
            Using xml_writer As New XmlTextWriter(sw)
                xml_writer.Formatting = Formatting.Indented
                xmlDoc.WriteTo(xml_writer)
                xml_writer.Flush()

                Try
                    'get the filename and location to write to
                    Dim sfdFile As New SaveFileDialog
                    sfdFile.Filter = "Sequenchel File (*.xml)|*.xml"
                    sfdFile.ShowDialog()

                    'Write the XML to disk
                    dhdText.CreateFile(sw.ToString(), sfdFile.FileName)
                Catch ex As Exception
                    WriteLog(ex.Message, 1)
                End Try
            End Using
        End Using

    End Sub

    Friend Sub ExportFile(dtsInput As DataSet, strFileName As String, blnShowFile As Boolean)
        If CurVar.IncludeDate = True Then
            strFileName = strFileName & "_" & FormatFileDate(Now)
        End If

        Dim sfdFile As New SaveFileDialog
        sfdFile.FileName = strFileName
        sfdFile.Filter = "XML File (*.xml)|*.xml|Excel 2007 file (*.xlsx)|*.xlsx"
        sfdFile.FilterIndex = 1
        sfdFile.RestoreDirectory = True
        sfdFile.OverwritePrompt = False

        If (sfdFile.ShowDialog() <> DialogResult.OK) Then
            Return
        End If

        Dim strTargetFile As String = sfdFile.FileName
        Dim strExtension As String = sfdFile.FileName.Substring(sfdFile.FileName.LastIndexOf(".") + 1, sfdFile.FileName.Length - (sfdFile.FileName.LastIndexOf(".") + 1))

        Select Case strExtension
            Case "xml"
                'MessageBox.Show("xml file detected")
                ExportXML(dtsInput, strTargetFile)
            Case "xlsx"
                'MessageBox.Show("excel file detected")
                ExportExcel(dtsInput, strTargetFile)
            Case Else
                Return
                'unknown filetype, do nothing
        End Select

        If blnShowFile = True Then
            Dim p As New Process
            p.StartInfo = New ProcessStartInfo(strTargetFile)
            p.Start()
        End If

    End Sub

    Friend Sub ExportXML(dtsInput As DataSet, strFileName As String)
        Try
            Dim strXMl As String = dtsInput.GetXml()
            dhdText.CreateFile(strXMl, strFileName)
        Catch ex As Exception
            WriteLog(ex.Message, 1)
        End Try
    End Sub

    Friend Sub XmlExportDatagridView(dgvInput As DataGridView, Optional ByVal strRootNode As String = "Sequenchel", Optional ByVal strParentNode As String = "Table", Optional ByVal strItemNode As String = "Row")
        'Create xml file
        Dim xmlDoc As New XmlDocument
        Dim xmlDec As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes")
        Dim RootNode As XmlElement = xmlDoc.CreateElement(strRootNode)
        xmlDoc.InsertBefore(xmlDec, xmlDoc.DocumentElement)
        xmlDoc.AppendChild(RootNode)
        strRootNode = strRootNode.Replace(" ", "")
        strParentNode = strParentNode.Replace(" ", "")
        strItemNode = strItemNode.Replace(" ", "")

        'Add parent Node
        Dim ParentNode As XmlElement = xmlDoc.CreateElement(strParentNode)
        'ParentNode.SetAttribute("Name", cbxLanguage.Text)
        xmlDoc.DocumentElement.AppendChild(ParentNode)
        'Add Items to parentnode
        For Each rowItem In dgvInput.Rows
            If rowItem.index = dgvInput.Rows.Count - 1 Then Exit For
            Dim ItemNode As XmlElement = xmlDoc.CreateElement(strItemNode)
            For Each colItem In dgvInput.Columns
                Dim Childnode As XmlElement = xmlDoc.CreateElement(colItem.Name.ToString.Replace(" ", "_"))
                Dim ItemText As XmlText = xmlDoc.CreateTextNode(If(String.IsNullOrEmpty(Trim(CStr(rowItem.Cells(colItem.Index).Value))), String.Empty, rowItem.Cells(colItem.Index).Value.ToString))
                ItemNode.AppendChild(Childnode)
                Childnode.AppendChild(ItemText)
            Next

            'For intHeader As Integer = 0 To dgvInput.Columns.Count - 1
            '    Dim Childnode As XmlElement = xmlDoc.CreateElement(dgvInput.Columns(intHeader).Name)
            '    Dim ItemText As XmlText = xmlDoc.CreateTextNode(rowItem.Cells(intHeader).Value)
            '    ItemNode.AppendChild(Childnode)
            '    Childnode.AppendChild(ItemText)
            '    Dim newAttribute As XmlAttribute = xmlDoc.CreateAttribute("Alias")
            '    Childnode.Attributes.Append(newAttribute)
            '    newAttribute.Value = dgvInput.Columns(intHeader).HeaderCell.Value
            'Next
            ParentNode.AppendChild(ItemNode)
        Next

        Using sw As New System.IO.StringWriter()
            ' Make the XmlTextWriter to format the XML.
            Using xml_writer As New XmlTextWriter(sw)
                xml_writer.Formatting = Formatting.Indented
                xmlDoc.WriteTo(xml_writer)
                xml_writer.Flush()

                Try
                    'get the filename and location to write to
                    Dim sfdFile As New SaveFileDialog
                    sfdFile.Filter = "Sequenchel File (*.xml)|*.xml"
                    sfdFile.ShowDialog()

                    'Write the XML to disk
                    dhdText.CreateFile(sw.ToString(), sfdFile.FileName)
                Catch ex As Exception
                    WriteLog(ex.Message, 1)
                End Try
            End Using
        End Using

    End Sub

    Friend Sub ExportExcel(dtsInput As DataSet, strTargetFile As String)
        Try
            CreateExcelFile.CreateExcelDocument(dtsInput, strTargetFile)
        Catch ex As Exception
            WriteLog("Couldn't create Excel file.\r\nException: " + ex.Message, 1)
            Return
        End Try

    End Sub

    Friend Function CheckElement(ByVal xmlDoc As XDocument, ByVal name As XName) As Boolean
        Return xmlDoc.Descendants(name).Any()
    End Function

    Friend Function LoadItemList(xmlInput As XmlDocument, strSearchItem As String, strSearchField As String, strSearchValue As String, strTargetItem As String, strDisplayItem As String)
        Dim xPNode As System.Xml.XmlNode = dhdText.FindXmlNode(xmlInput, strSearchItem, strSearchField, strSearchValue)
        Dim blnSearchValueExists As Boolean = False
        If Not xPNode Is Nothing Then
            Dim ReturnValue As New List(Of String)
            Dim xNode As System.Xml.XmlNode
            For Each xNode In xPNode.SelectNodes(".//" & strTargetItem)
                ReturnValue.Add(xNode.Item(strDisplayItem).InnerText)
                'If xNode.Item("ConnectionName").InnerText = CurStatus.Connection Then blnSearchValueExists = True
            Next
            'If blnSearchValueExists = False Then CurStatus.Connection = CurVar.ConnectionDefault
            Return ReturnValue
        End If
        Return Nothing
    End Function

    Friend Sub DisplayXmlFile(ByVal xmlDoc As Xml.XmlDocument, ByVal tvw As TreeView)
        tvw.Nodes.Clear()
        DisplayXmlNode(xmlDoc, tvw.Nodes)
        tvw.Nodes(0).Expand()
    End Sub

    Friend Sub DisplayXmlNode(ByVal xmlnode As XmlNode, ByVal nodes As TreeNodeCollection)
        ' Add a TreeView node for this XmlNode.
        ' (Using the node's Name is OK for most XmlNode types.)
        Dim tvNode As TreeNode = nodes.Add(xmlnode.Name, xmlnode.Name)
        Select Case xmlnode.NodeType
            Case XmlNodeType.Element
                '	' This is an element: Check whether there are attributes.
                If xmlnode.Attributes.Count > 0 Then
                    '		' Create an ATTRIBUTES node.
                    Dim attrNode As TreeNode = tvNode.Nodes.Add("(ATTRIBUTES)")
                    '		' Add all the attributes as children of the new node.
                    Dim xmlAttr As XmlAttribute
                    For Each xmlAttr In xmlnode.Attributes
                        '			' Each node shows name and value.
                        attrNode.Nodes.Add(xmlAttr.Name & " = '" & xmlAttr.Value & _
                           "'")
                    Next
                End If
            Case XmlNodeType.Text, XmlNodeType.CDATA
                ' For these node types we display the value
                tvNode.Text = xmlnode.Value
            Case XmlNodeType.Comment
                tvNode.Text = "<!--" & xmlnode.Value & "-->"
            Case XmlNodeType.ProcessingInstruction, XmlNodeType.XmlDeclaration
                tvNode.Text = "<?" & xmlnode.Name & " " & xmlnode.Value & "?>"
            Case Else
                ' ignore other node types.
        End Select

        Dim xmlChild As XmlNode = xmlnode.FirstChild
        Do Until xmlChild Is Nothing
            DisplayXmlNode(xmlChild, tvNode.Nodes)
            xmlChild = xmlChild.NextSibling
        Loop
    End Sub

    'Friend Function PathConvert(strPath As String) As String
    '    If strPath.Contains("%Documents%") Then
    '        strPath = strPath.Replace("%Documents%", Environment.GetFolderPath(MyDocuments))
    '    End If
    '    If strPath.Contains("%ProgramFiles%") Then
    '        strPath = strPath.Replace("%ProgramFiles%", Environment.GetFolderPath(ProgramFiles))
    '    End If
    '    If strPath.Contains("%ApplicationData%") Then
    '        strPath = strPath.Replace("%ApplicationData%", Environment.GetFolderPath(ApplicationData))
    '    End If
    '    If strPath.Contains("%CommonApplicationData%") Then
    '        strPath = strPath.Replace("%CommonApplicationData%", Environment.GetFolderPath(CommonApplicationData))
    '    End If
    '    If strPath.Contains("%Desktop%") Then
    '        strPath = strPath.Replace("%Desktop%", Environment.GetFolderPath(Desktop))
    '    End If
    '    Return strPath
    'End Function

#End Region

#Region "Database"

    Friend Function QueryDb(ByVal dhdConnect As DataHandler.db, ByVal strQueryData As String, ByVal ReturnValue As Boolean, Optional ByVal LogLevel As Integer = 5) As DataSet
        WriteLog(strQueryData, LogLevel)
        strErrorMessage = ""
        dhdConnect.CheckDB()
        If dhdConnect.DataBaseOnline = False Then
            MessageBox.Show("The database was not found." & Environment.NewLine & "Please check your settings")
            strErrorMessage = "The database was not found. Please check your settings"
            Return Nothing
        End If
        Dim dtsData As DataSet
        Try
            dtsData = dhdConnect.QueryDatabase(strQueryData, ReturnValue)
            If dhdConnect.DataBaseOnline = False Then
                MessageBox.Show("There was a problem processing your query" & Environment.NewLine & "Please check your settings")
                strErrorMessage = "There was a problem processing your query. Please check your settings"
                Return dtsData
            End If
            Return dtsData
        Catch ex As Exception
            WriteLog(ex.Message, 1)
            strErrorMessage = ex.Message
            Return Nothing
        End Try
    End Function

    Friend Function DatabaseTest(dhdConnect As DataHandler.db) As Boolean
        If DebugMode Then
            MessageBox.Show("Sequenchel v " & Application.ProductVersion & " Database Settings" & Environment.NewLine _
             & "   DatabaseServer = " & dhdConnect.DataLocation & Environment.NewLine _
             & "   Database Name = " & dhdConnect.DatabaseName & Environment.NewLine _
             & "   DataProvider = " & dhdConnect.DataProvider & Environment.NewLine _
             & "   LoginMethod = " & dhdConnect.LoginMethod & Environment.NewLine _
             & "   LoginName = " & dhdConnect.LoginName & Environment.NewLine _
             & "   Password = " & dhdConnect.Password & Environment.NewLine _
             & "   Running in Debug Mode")
        End If
        Try
            dhdConnect.CheckDB()
            blnDatabaseOnLine = dhdConnect.DataBaseOnline
            Return blnDatabaseOnLine
        Catch ex As Exception
            MessageBox.Show("While testing the database connection, the following error occured: " & Environment.NewLine & ex.Message)
            Return False
        End Try
        Return blnDatabaseOnLine
    End Function

    Friend Function GetSqlVersion(dhdConnect As DataHandler.db) As Integer
        Dim strDatabase As String = dhdConnect.DatabaseName
        dhdConnect.DatabaseName = "master"
        Dim intSqlVersion As Integer = dhdConnect.GetSqlVersion()
        dhdConnect.DatabaseName = strDatabase

        Return intSqlVersion
    End Function

    Friend Sub BackupDatabase(ByVal dhdConnect As DataHandler.db, ByVal strPath As String)

        Dim strDateTime As String
        'strDateTime = FormatDateTime(Now()).Replace(":", "").Replace(" ", "_").Replace("-", "").Replace("/", "")
        strDateTime = Now.ToString("yyyyMMdd_HHmmss")
        strQuery = ""
        strQuery = "exec usp_BackupHandle 'CREATE','" & dhdConnect.DatabaseName & "','" & strPath & "','" & strDateTime & "'"
        If DebugMode Then MessageBox.Show("DatabaseName = " & dhdConnect.DatabaseName & Environment.NewLine & _
                                            "strPath = " & strPath & Environment.NewLine & _
                                            "strDateTime = " & strDateTime & Environment.NewLine & _
                                            strQuery)
        Try
            QueryDb(dhdConnect, strQuery, False)
        Catch ex As Exception
            dhdText.LogLocation = ""
            WriteLog(ex.Message, 1)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Friend Sub SaveConfigSetting(ByVal strCategory As String, ByVal strConfigName As String, ByVal strConfigValue As String, Optional ByVal strRemarks As String = Nothing)
        strQuery = "exec usp_ConfigHandle 'Smt','" & strCategory & "',NULL,'" & strConfigName & "','" & strConfigValue & "','" & Now.ToString("yyyyMMdd HH:mm") & "'"
        If strRemarks = Nothing Then
            'Do nothing
        Else
            strQuery &= ",'" & strRemarks & "'"
        End If
        QueryDb(dhdDatabase, strQuery, False)
    End Sub

    Friend Function LoadConfigSetting(ByVal strCategory As String, ByVal strConfigName As String) As String
        strQuery = "exec usp_ConfigHandle 'Get','" & strCategory & "',NULL,'" & strConfigName & "'"
        Dim objData As DataSet
        objData = QueryDb(dhdDatabase, strQuery, True)
        Dim strReturn As String = Nothing

        If objData Is Nothing Then
            'blnDatabaseOnLine = False
            Return ""
        End If
        If objData.Tables.Count = 0 Then Return ""
        If objData.Tables(0).Rows.Count = 0 Then Return ""

        For intRowCount = 0 To objData.Tables(0).Rows.Count - 1
            'If objData.Tables.Item(0).Rows(intRowCount).Item(0).GetType().ToString = "System.DBNull" Then
            'MessageBox.Show("Cell Must be empty")
            'Else
            If objData.Tables.Item(0).Rows(intRowCount).Item("Category") = strCategory Then
                If objData.Tables.Item(0).Rows(intRowCount).Item("ConfigName") = strConfigName Then
                    strReturn = objData.Tables.Item(0).Rows(intRowCount).Item("ConfigValue")
                End If
            End If
            'End If
        Next
        Return strReturn
    End Function

    Friend Sub DisplayData(ByVal table As DataTable)
        Dim strResult As String = ""
        For Each row As DataRow In table.Rows
            For Each col As DataColumn In table.Columns
                strResult &= col.ColumnName & " = " & row(col) & Environment.NewLine
            Next
            'Console.WriteLine("============================")
        Next
        MessageBox.Show(strResult)
    End Sub

    Public Sub GetServerList()
        Dim strResult As String = String.Empty
        Dim Server As String = String.Empty
        Dim instance As Sql.SqlDataSourceEnumerator = Sql.SqlDataSourceEnumerator.Instance
        Dim table As System.Data.DataTable = instance.GetDataSources()
        For Each row As System.Data.DataRow In table.Rows
            Server = String.Empty
            Server = row("ServerName")
            If row("InstanceName").ToString.Length > 0 Then
                Server = Server & "\" & row("InstanceName")
            End If
            strResult &= Server & Environment.NewLine
        Next
        MessageBox.Show(strResult)

    End Sub

    Friend Sub WriteDBLog(ByVal strLogText As String)
        strQuery = ""
        strLogText = Replace(strLogText, "'", "~")
        strQuery = "exec usp_LoggingHandle 'Ins',NULL,'" & strLogText & "','" & Environment.MachineName.ToString & "'"

        Try
            dhdDatabase.QueryDatabase(strQuery, False)
        Catch ex As Exception
            dhdText.LogLocation = ""
            WriteLog(ex.Message, 1)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Friend Sub ClearDBLog(ByVal dtmDate As Date)
        strQuery = ""
        strQuery = "exec usp_LoggingHandle 'Del','" & FormatDate(dtmDate) & "'"

        Try
            QueryDb(dhdDatabase, strQuery, False)
        Catch ex As Exception
            dhdText.LogLocation = ""
            WriteLog(ex.Message, 1)
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Friend Function LoadTablesList(ByVal dhdConnect As DataHandler.db) As List(Of String)
        strQuery = "SELECT "
        strQuery &= " sch.[name] + '.' + "
        strQuery &= " tbl.[name] AS TableName FROM sys.tables tbl"
        strQuery &= " INNER JOIN sys.schemas sch"
        strQuery &= " ON tbl.[schema_id] = sch.[schema_id]"
        strQuery &= " UNION SELECT "
        strQuery &= " sch.[name] + '.' + "
        strQuery &= " vw.[name] AS TableName FROM sys.views vw"
        strQuery &= " INNER JOIN sys.schemas sch"
        strQuery &= " ON vw.[schema_id] = sch.[schema_id]"
        strQuery &= " ORDER BY TableName"

        CursorControl("Wait")
        'Application.DoEvents()

        Dim dtsData As DataSet = QueryDb(dhdConnect, strQuery, True)
        If dtsData Is Nothing Then
            Return Nothing
        End If
        If dtsData.Tables.Count = 0 Then Return Nothing
        If dtsData.Tables(0).Rows.Count = 0 Then Return Nothing

        Dim ReturnValue As New List(Of String)
        For intRowCount = 0 To dtsData.Tables(0).Rows.Count - 1
            'If dtsData.Tables.Item(0).Rows(intRowCount).Item(0).GetType().ToString = "System.DBNull" Then
            'MessageBox.Show("Cell Must be empty")
            'Else
            ReturnValue.Add(dtsData.Tables.Item(0).Rows(intRowCount).Item("TableName"))
            'End If
        Next
        Return ReturnValue
    End Function

    Friend Sub ScheduleCreate(JobName As String, SqlCommand As String, FreqType As Integer, FreqInterval As Integer, FreqSubType As Integer, FreqSubTypeInt As Integer, StartTime As Integer, EndTime As Integer, OutputPath As String)
        CursorControl("Wait")

        Dim strStartDate As String = Now.ToString("yyyyMMdd")
        If OutputPath.LastIndexOf("\") = OutputPath.Length - 1 Then OutputPath = OutputPath.Substring(0, OutputPath.Length - 1)

        strQuery = "EXECUTE [dbo].[usp_ScheduleCreate] "
        strQuery &= "@JobName = '" & JobName & "'"
        strQuery &= ",@SqlCommand = '" & SqlCommand & "'"
        strQuery &= ",@DatabaseName = '" & dhdDatabase.DatabaseName & "'"
        strQuery &= ",@OutputPath = '" & OutputPath & "'"
        strQuery &= ",@FreqType = " & FreqType
        strQuery &= ",@FreqInterval = " & FreqInterval
        strQuery &= ",@FreqSubType = " & FreqSubType
        strQuery &= ",@FreqSubTypeInt = " & FreqSubTypeInt
        strQuery &= ",@StartDate = " & strStartDate
        strQuery &= ",@StartTime = " & StartTime
        strQuery &= ",@EndTime = " & EndTime

        QueryDb(dhdDatabase, strQuery, False)
    End Sub

    'Friend Sub LoadLookupList(cbxField As ComboField, blnRefine As Boolean, blnUseOtherLookups As Boolean)
    '    If dhdConnection.DataBaseOnline = True Then
    '        Dim strQuerySelect As String = "SELECT DISTINCT "
    '        Dim strQueryFrom As String = " FROM "
    '        Dim strQueryWhere As String = " WHERE 1=1 "
    '        Dim strQueryOrder As String = " ORDER BY "
    '        strQuery = ""

    '        If CurVar.LimitLookupLists = True Then
    '            strQuerySelect &= "TOP " & CurVar.LimitLookupListsCount & " "
    '        End If
    '        If cbxField.FieldCategory = 4 Or cbxField.FieldCategory = 5 Then
    '            strQuerySelect &= "[" & cbxField.Name.Substring(0, cbxField.Name.LastIndexOf(".")).Replace(".", "].[") & "].[" & cbxField.FieldRelatedField & "],"
    '        End If

    '        strQuerySelect &= "[" & cbxField.Name.Replace(".", "].[") & "]"
    '        strQueryFrom &= "[" & cbxField.Name.Substring(0, cbxField.Name.LastIndexOf(".")).Replace(".", "].[") & "] "
    '        strQueryOrder &= "[" & cbxField.Name.Replace(".", "].[") & "] "

    '        If blnRefine = True And cbxField.Text.Length > 0 Then
    '            strQueryWhere &= " AND [" & cbxField.FieldName & "] LIKE '%" & cbxField.Text & "%'"
    '        End If
    '        If cbxField.FieldCategory = 3 And blnUseOtherLookups = True Then
    '            For intFieldCount As Integer = 0 To tblTable.Count - 1
    '                If tblTable.Item(intFieldCount).BackColor = clrMarked And tblTable.Item(intFieldCount).FieldName <> cbxField.FieldName Then
    '                    strQueryWhere &= " AND [" & tblTable.Item(intFieldCount).FieldName & "] = '" & tblTable.Item(intFieldCount).Text & "'"
    '                End If
    '            Next
    '        End If


    '        strQuery = strQuerySelect & strQueryFrom & strQueryWhere & strQueryOrder
    '        Try
    '            Dim objData As DataSet = QueryDb(dhdConnection, strQuery, True)
    '            If objData Is Nothing Then Exit Sub
    '            If objData.Tables.Count = 0 Then Exit Sub
    '            If objData.Tables(0).Rows.Count = 0 Then Exit Sub

    '            cbxField.Items.Clear()
    '            For intRowCount1 As Integer = 0 To objData.Tables(0).Rows.Count - 1
    '                If objData.Tables(0).Columns.Count = 1 Then
    '                    cbxField.Items.Add(objData.Tables.Item(0).Rows(intRowCount1).Item(0))
    '                ElseIf objData.Tables(0).Columns.Count = 2 Then
    '                    cbxField.Items.Add(objData.Tables.Item(0).Rows(intRowCount1).Item(0) & " | " & objData.Tables.Item(0).Rows(intRowCount1).Item(1))
    '                End If
    '            Next
    '            cbxField.Focus()
    '            'cbxField.AutoCompleteMode = AutoCompleteMode.Suggest
    '            'cbxField.AutoCompleteSource = AutoCompleteSource.ListItems
    '            cbxField.SelectionStart = cbxField.Text.Length
    '        Catch ex As Exception
    '            WriteLog(ex.Message, 1)
    '        End Try

    '    End If

    'End Sub

#End Region

#Region "DataTypes"

    Friend Function GetDataType(strInput As String) As String
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

    Friend Function GetDataTypes()
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

    Friend Function SetDelimiters(strInput As String, strDataType As String, strCompare As String) As String
        If strInput.Length > 2 Then
            If strInput.Substring(0, 2) = "f:" Then
                Return "(" & strInput.Replace("f:", "") & ")"
            End If
        End If
        If strCompare = "IS" Or strCompare = "IS NOT" Then
            Return strInput
        End If
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
        If (strCompare = "IN" Or strCompare = "NOT IN") Then
            strInput = "(" & strInput & ")"
        End If
        Return strInput
    End Function

    Friend Function GetWidth(strDataType As String, intMaxLength As Integer) As Integer
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
                Return intMaxLength * 10
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

    Friend Function FormatFieldXML(strFullFieldName As String, strShowMode As String, blnUseAlias As Boolean, blnSelect As Boolean) As String
        Dim strOutput As String = ""
        Dim strTableName As String = strFullFieldName.Substring(0, strFullFieldName.LastIndexOf("."))
        Dim strFieldName As String = strFullFieldName.Substring(strFullFieldName.LastIndexOf(".") + 1, strFullFieldName.Length - (strFullFieldName.LastIndexOf(".") + 1))
        Dim xNode As XmlNode = dhdText.FindXmlNode(xmlTables, "Table", "Name", strTableName)
        Dim xCNode As XmlNode = dhdText.FindXmlChildNode(xNode, "Fields/Field", "FldName", strFieldName)
        Dim strFieldType As String = xCNode.Item("DataType").InnerText
        Dim strFieldWidth As String = xCNode.Item("FldWidth").InnerText
        Dim strFieldAlias As String = xCNode.Item("FldAlias").InnerText
        If blnUseAlias = False Then strFieldAlias = Nothing

        strOutput = FormatField(strFieldName, strTableName, strFieldWidth, strFieldType, strFieldAlias, strShowMode, blnSelect)
        Return strOutput
    End Function

    Friend Function FormatField(strFieldName As String, strTableName As String, strFieldWidth As String, strFieldType As String, strFieldAlias As String, strShowMode As String, blnSelect As Boolean) As String
        Dim strOutput As String = ""
        Dim strFQDN As String = "[" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "]"

        If Not strShowMode Is Nothing Then
            Select Case strShowMode.ToUpper
                Case "SUM", "AVG", "COUNT"
                    strFQDN = strShowMode & "(" & strFQDN & ")"
                    strFieldType = "INTEGER"
                Case "MIN", "MAX"
                    strFQDN = strShowMode & "(" & strFQDN & ")"
                Case Else
                    'Do Nothing
            End Select
            If Not strFieldAlias Is Nothing Then
                strFieldAlias = strShowMode.ToLower & strFieldAlias
            Else
                strFieldAlias = strShowMode.ToLower & strFieldName
            End If
        End If

        Select Case strFieldType.ToUpper
            Case "IMAGE"
                strOutput = "(CONVERT([varchar](" & strFieldWidth & "), " & strFQDN & "))"
            Case "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                strOutput = "(CONVERT([nvarchar](" & strFieldWidth & "), " & strFQDN & "))"
            Case "DATETIME"
                strOutput = "(CONVERT([nvarchar](" & strFieldWidth & "), " & strFQDN & ", " & CurVar.DateTimeStyle & "))"
            Case Else
                'CHAR: no need to convert char values to char.
                strOutput = strFQDN
        End Select

        If blnSelect = True Then
            If Not strFieldAlias Is Nothing Then
                If strFieldAlias.Length > 0 Then
                    strOutput &= " AS [" & strFieldAlias & "]"
                End If
            End If
        End If

        Return strOutput
    End Function

    Friend Function FormatFieldWhere(strFieldName As String, strTableName As String, strFieldWidth As String, strFieldType As String, strFieldValue As String) As String
        Dim strOutput As String = ""
        Select Case strFieldType.ToUpper
            Case "CHAR"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],'') = '') "
                Else
                    strOutput = " ([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "] IN ('" & Replace(strFieldValue, ",", "','") & "') OR [" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] LIKE '%" & strFieldValue & "%')"
                End If
            Case "INTEGER"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],0) = 0) "
                Else
                    strOutput = " ([" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] IN (" & strFieldValue & ") OR [" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] LIKE '%" & strFieldValue & "%')"
                End If
            Case "DATETIME"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],0) = 0) "
                Else
                    strOutput = " ((CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "], " & CurVar.DateTimeStyle & ")) IN ('" & strFieldValue.Replace(",", "','") & "') OR (CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "], " & CurVar.DateTimeStyle & ")) LIKE '%" & strFieldValue & "%')"
                End If
            Case "BINARY", "XML", "GEO", "TEXT", "GUID", "TIME", "TIMESTAMP"
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],'') = '') "
                Else
                    strOutput = " ((CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "])) IN (" & strFieldValue.Replace(",", "','") & ") OR (CONVERT([nvarchar](" & strFieldWidth & "), [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "])) LIKE '%" & strFieldValue & "%')"
                End If
            Case "BIT"
                If strFieldValue = 0 Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],0) = 0) "
                Else
                    strOutput = " [" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "] = " & strFieldValue
                End If
            Case "IMAGE"
                'do nothing. cannot search on an image data type.
            Case Else
                'try the default CHAR action
                If strFieldValue = "NULL" Or strFieldValue = "" Then
                    strOutput = " (COALESCE([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "],'') = '') "
                Else
                    strOutput = " ([" & strTableName.Replace(".", "].[") & "].[" & strFieldName & "] IN ('" & Replace(strFieldValue, ",", "','") & "') OR [" & tblTable.TableName.Replace(".", "].[") & "].[" & strFieldName & "] LIKE '%" & strFieldValue & "%')"
                End If
        End Select
        Return strOutput
    End Function

#End Region

    Friend Sub WriteLog(ByVal strLogtext As String, ByVal intLogLevel As Integer)
        Try
            If dhdText.LogLocation.ToLower = "database" Then
                dhdDatabase.WriteLog(strLogtext, intLogLevel, dhdText.LogLevel)
            Else
                dhdText.WriteLog(strLogtext, intLogLevel)
                'If DevMode Then MessageBox.Show(dhdText.LogFileName & Environment.NewLine & dhdText.LogLocation & Environment.NewLine & dhdText.LogLevel)
            End If
        Catch ex As Exception
            Dim strMyDir As String
            strMyDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            If dhdText.CheckDir(strMyDir & "\Sequenchel") = False Then dhdText.CreateDir(strMyDir & "\Sequenchel")
            If dhdText.CheckDir(strMyDir & "\Sequenchel\LOG") = False Then dhdText.CreateDir(strMyDir & "\Sequenchel\LOG")
            dhdText.LogFileName = "Sequenchel.Log"
            dhdText.LogLocation = strMyDir & "\Sequenchel\LOG"
            MessageBox.Show("there was an error writing to the logfile: " & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Friend Sub WriteStatus(strStatusText As String, intStatusLevel As Integer, lblTarget As Label)
        Dim clrStatus As Color = clrControl
        Select Case intStatusLevel
            Case 0
                clrStatus = clrControl
            Case 1
                clrStatus = clrWarning
            Case 2
                clrStatus = clrWarning
            Case 3
                clrStatus = clrMarked
            Case 4
                clrStatus = clrMarked
            Case 5
                clrStatus = clrControl
        End Select
        lblTarget.Text = strStatusText
        lblTarget.BackColor = clrStatus
    End Sub

    Friend Sub MasterPanelControlsDispose(pnlMaster As Panel, Optional strTag As String = "")
        For Each pnlTarget In pnlMaster.Controls
            If pnlTarget.Name.Substring(0, 3) = "pnl" Then
                PanelControlsDispose(pnlTarget, strTag)
            End If
        Next
    End Sub

    Friend Sub PanelControlsDispose(pnlTarget As Panel, Optional strTag As String = "")
        For intCount As Integer = 1 To pnlTarget.Controls.Count
            If pnlTarget.Controls.Count > 0 Then
                For Each ctrDispose In pnlTarget.Controls
                    'MessageBox.Show(ctrDispose.Name)
                    If ctrDispose.Tag Is Nothing Then
                        ctrDispose.Dispose()
                    ElseIf strTag.ToString = "" Then
                        ctrDispose.Dispose()
                    Else
                        If strTag.ToString = ctrDispose.Tag.ToString Then
                            ctrDispose.Dispose()
                        End If
                    End If
                Next
            Else
                Exit Sub
            End If
        Next
        pnlTarget.Height = 30
    End Sub

#Region "Formatting"

    Friend Function FormatDate(ByVal dtmInput As Date) As String
        If dtmInput = Nothing Then
            FormatDate = ""
        Else
            FormatDate = dtmInput.ToString("yyyy-MM-dd")
        End If
    End Function

    Friend Function FormatFileDate(ByVal dtmInput As Date) As String
        If dtmInput = Nothing Then
            FormatFileDate = ""
        Else
            FormatFileDate = dtmInput.ToString("yyyy-MM-dd")
            Select Case CurVar.DateTimeStyle
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

    Friend Sub CursorControl(Optional strStyle As String = "Default")
        Select Case strStyle
            Case "Default"
                Cursor.Current = Cursors.Default
            Case "Wait"
                Cursor.Current = Cursors.WaitCursor
            Case Else
                Cursor.Current = Cursors.Default
        End Select
    End Sub

    Friend Function ProcessDefaultValue(strValue As String) As String
        Dim strReturn As String = strValue
        Dim intFirstBracket As Integer = strValue.IndexOf("(")
        Dim intLastBracket As Integer = strValue.LastIndexOf(")")
        If intFirstBracket > 0 And intLastBracket > 0 Then
            Select Case strValue.Substring(0, strValue.IndexOf("(") + 1).ToLower
                Case "now("
                    If intLastBracket - intFirstBracket > 1 Then
                        If IsNumeric(strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1))) Then
                            Try
                                Dim dt As DateTime = DateTime.Now
                                dt = dt.AddHours(strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1)))
                                strReturn = dt
                            Catch ex As Exception
                                strReturn = Now
                            End Try
                        Else
                            strReturn = Now
                        End If
                    Else
                        strReturn = Now
                    End If
                Case "date("
                    If intLastBracket - intFirstBracket > 1 Then
                        If IsNumeric(strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1))) Then
                            Try
                                Dim dt As DateTime = Date.Today
                                dt = dt.AddDays(strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1)))
                                strReturn = dt
                            Catch ex As Exception
                                strReturn = Date.Today
                            End Try
                        Else
                            strReturn = Date.Today
                        End If
                    Else
                        strReturn = Date.Today
                    End If
                Case "time("
                    If intLastBracket - intFirstBracket > 1 Then
                        If IsNumeric(strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1))) Then
                            Try
                                strReturn = TimeOfDay.AddMinutes(strValue.Substring(intFirstBracket + 1, intLastBracket - (intFirstBracket + 1)))
                            Catch ex As Exception
                                strReturn = TimeOfDay
                            End Try
                        Else
                            strReturn = TimeOfDay
                        End If
                    Else
                        strReturn = TimeOfDay
                    End If
                    strReturn = TimeOfDay
                Case "pi("
                    strReturn = "3.1415926535897932384626433832795"
                Case Else
                    'nothing
            End Select
        End If
        Return strReturn
    End Function

#End Region

    Friend Function DataSet2DataGridView(dtsSource As DataSet, SourceTable As Integer, dgvTarget As DataGridView, RebuildColumns As Boolean) As Boolean
        Dim blnSucces As Boolean = False

        If RebuildColumns = True Then
            For Each colColumn As DataColumn In dtsSource.Tables(SourceTable).Columns
                Dim dgvColumn As New DataGridViewTextBoxColumn
                dgvColumn.Name = colColumn.ColumnName
                dgvColumn.HeaderText = colColumn.ColumnName
                dgvColumn.ValueType = colColumn.DataType
                dgvColumn.Visible = True
                dgvTarget.Columns.Add(dgvColumn)
            Next
        End If

        For intRowCount1 As Integer = 0 To dtsSource.Tables(SourceTable).Rows.Count - 1
            Dim dgvRow As New DataGridViewRow
            dgvRow.CreateCells(dgvTarget)
            For Each dgvColumn As DataGridViewTextBoxColumn In dgvTarget.Columns
                If dtsSource.Tables.Item(0).Rows(intRowCount1).Item(dgvColumn.Name).GetType().ToString = "System.DBNull" Then
                    dgvRow.Cells.Item(dgvTarget.Columns.Item(dgvColumn.Name).Index).Style.BackColor = clrEmpty
                Else
                    dgvRow.Cells.Item(dgvTarget.Columns.Item(dgvColumn.Name).Index).Value = dtsSource.Tables(SourceTable).Rows(intRowCount1).Item(dgvColumn.Name)
                End If
            Next
            dgvTarget.Rows.Add(dgvRow)
        Next
        If RebuildColumns = True Then DataGridViewColumnSize(dgvTarget)
        blnSucces = True
        Return blnSucces
    End Function

    Friend Function DatasetCheck(dtsInput As DataSet, Optional intTable As Integer = 0) As Boolean
        Dim blnOK As Boolean = True

        If dtsInput Is Nothing Then Return False
        If dtsInput.Tables.Count = 0 Then Return False
        If dtsInput.Tables.Count < intTable + 1 Then Return False
        If dtsInput.Tables(intTable).Rows.Count = 0 Then Return False

        Return blnOK
    End Function

    Friend Sub DataGridViewColumnSize(dgvTarget As DataGridView)
        'grd.DataSource = DT

        'set autosize mode
        For Each dgvColumn As DataGridViewTextBoxColumn In dgvTarget.Columns
            dgvColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next
        'dgvTarget.Columns(dgvTarget.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        'datagrid has calculated it's widths so we can store them
        For i = 0 To dgvTarget.Columns.Count - 1
            'store autosized widths
            Dim colw As Integer = dgvTarget.Columns(i).Width
            If colw > CurVar.MaxColumnWidth Then colw = CurVar.MaxColumnWidth
            'remove autosizing
            dgvTarget.Columns(i).AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            'set width to calculated by autosize
            dgvTarget.Columns(i).Width = colw
        Next
    End Sub

    Friend Function Encrypt(strPassword As String) As String
        Dim x As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim bs As Byte() = System.Text.Encoding.UTF8.GetBytes(strPassword)
        bs = x.ComputeHash(bs)
        Dim s As New System.Text.StringBuilder()
        For Each b As Byte In bs
            s.Append(b.ToString("x2").ToLower())
        Next
        Return s.ToString()
    End Function

    'Friend Function ComboFieldMultiColumnCreate(cbxInput As ComboField, dtsInput As DataSet)
    '    'Dim strName As String = "dt" & cbxInput.Name
    '    Dim dtFormats As DataTable = New DataTable()
    '    dtFormats.Columns.Add("ID", GetType(Integer))
    '    dtFormats.Columns.Add("Name", GetType(String))

    '    'CurStatus.SuspendActions = True
    '    'dtFormats.Rows.Add(1, "Name")
    '    For intRowCount1 As Integer = 0 To dtsInput.Tables(0).Rows.Count - 1
    '        dtFormats.Rows.Add(dtsInput.Tables.Item(0).Rows(intRowCount1).Item(0), dtsInput.Tables.Item(0).Rows(intRowCount1).Item(1))
    '    Next
    '    'CurStatus.SuspendActions = False

    '    cbxInput.DataSource = dtFormats
    '    cbxInput.DisplayMember = "Name"
    '    cbxInput.ValueMember = "ID"


    '    cbxInput.DrawMode = DrawMode.OwnerDrawFixed
    '    Return cbxInput
    'End Function

End Module
