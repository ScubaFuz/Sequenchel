Public Class frmSettings

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        txtLicenseName.Text = basCode.curVar.LicenseName
        DatabaseShow()
        basCode.dhdMainDB.CheckDB()
        If basCode.dhdMainDB.DataBaseOnline = True Then
            If VersionLoad() = True Then
                tabSettings.SelectTab(tpgDatabase)
                btnUpgradeDatabase.BackColor = clrMarked
            End If
            MonitorDataspacesLoad()
        End If
        If basCode.dhdMainDB.DataBaseOnline = False Then
            tabSettings.SelectTab(tpgDatabase)
        End If
        LogSettingsShow()
        GeneralSettingsShow()
        ProceduresLoad()
        HoursLoad()
        MinutesLoad()
        DateFormatsLoad()
        txtErrorlogPath.Text = basCode.dhdText.LogLocation
        ResetColors()
        SetFtpDefaults()
        SetEmailDefaults()
        EmailSettingsShow()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

#Region "General"

    Private Sub GeneralSettingsShow()
        txtDefaultConfigFilePath.Tag = basCode.curVar.DefaultConfigFilePath
        txtDefaultConfigFilePath.Text = basCode.curVar.DefaultConfigFilePath
        txtSettingsFile.Tag = basCode.curVar.GeneralSettings
        txtSettingsFile.Text = basCode.curVar.GeneralSettings
        txtConnectionsFile.Tag = basCode.curVar.ConnectionsFile
        txtConnectionsFile.Text = basCode.curVar.ConnectionsFile

        chkAllowSettingsChange.Tag = basCode.curVar.AllowSettingsChange
        chkAllowSettingsChange.Checked = basCode.curVar.AllowSettingsChange
        chkAllowConfiguration.Tag = basCode.curVar.AllowConfiguration
        chkAllowConfiguration.Checked = basCode.curVar.AllowConfiguration
        chkAllowLinkedServers.Tag = basCode.curVar.AllowLinkedServers
        chkAllowLinkedServers.Checked = basCode.curVar.AllowLinkedServers

        chkAllowQueryEdit.Tag = basCode.curVar.AllowQueryEdit
        chkAllowQueryEdit.Checked = basCode.curVar.AllowQueryEdit
        chkAllowDataImport.Tag = basCode.curVar.AllowDataImport
        chkAllowDataImport.Checked = basCode.curVar.AllowDataImport
        chkAllowSmartUpdate.Tag = basCode.curVar.AllowSmartUpdate
        chkAllowSmartUpdate.Checked = basCode.curVar.AllowSmartUpdate

        chkAllowUpdate.Tag = basCode.curVar.AllowUpdate
        chkAllowUpdate.Checked = basCode.curVar.AllowUpdate
        chkAllowInsert.Tag = basCode.curVar.AllowInsert
        chkAllowInsert.Checked = basCode.curVar.AllowInsert
        chkAllowDelete.Tag = basCode.curVar.AllowDelete
        chkAllowDelete.Checked = basCode.curVar.AllowDelete

        txtTimerHours.Tag = basCode.curVar.TimedShutdown / 60 / 60 / 1000
        txtTimerHours.Text = basCode.curVar.TimedShutdown / 60 / 60 / 1000

        chkLimitLookupLists.Tag = basCode.curVar.LimitLookupLists
        chkLimitLookupLists.Checked = basCode.curVar.LimitLookupLists
        txtLimitLookupLists.Tag = basCode.curVar.LimitLookupListsCount
        txtLimitLookupLists.Text = basCode.curVar.LimitLookupListsCount

        chkIncludeDateInExportFiles.Tag = basCode.curVar.IncludeDate
        chkIncludeDateInExportFiles.Checked = basCode.curVar.IncludeDate

        SetDefaultText(txtOverridePassword)
        PasswordCharSet(txtOverridePassword)
    End Sub

    Private Sub btnDefaultConfigFilePath_Click(sender As Object, e As EventArgs) Handles btnDefaultConfigFilePath.Click
        WriteStatus("", 0, lblStatusText)
        txtDefaultConfigFilePath.Text = Application.StartupPath & "\Config"
    End Sub

    Private Sub btnSettingsFileSystem_Click(sender As Object, e As EventArgs) Handles btnSettingsFileSystem.Click
        txtSettingsFile.Text = Application.StartupPath & "\" & basCode.curVar.MainSettingsFile
    End Sub

    Private Sub btnSettingsFileDefault_Click(sender As Object, e As EventArgs) Handles btnSettingsFileDefault.Click
        WriteStatus("", 0, lblStatusText)
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtSettingsFile.Text = txtDefaultConfigFilePath.Text & "\" & basCode.curVar.MainSettingsFile
        Else
            txtSettingsFile.Text = basCode.curVar.MainSettingsFile
        End If
    End Sub

    Private Sub btnConnectionsFileSystem_Click(sender As Object, e As EventArgs) Handles btnConnectionsFileSystem.Click
        txtConnectionsFile.Text = basCode.curVar.ConnectionsFile
    End Sub

    Private Sub btnConnectionsFileDefault_Click(sender As Object, e As EventArgs) Handles btnConnectionsFileDefault.Click
        WriteStatus("", 0, lblStatusText)
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtConnectionsFile.Text = txtDefaultConfigFilePath.Text & "\" & basCode.curVar.ConnectionsFileName
        Else
            txtConnectionsFile.Text = basCode.curVar.ConnectionsFileName
        End If
    End Sub

    Private Sub txtDefaultConfigFilePath_TextChanged(sender As Object, e As EventArgs) Handles txtDefaultConfigFilePath.TextChanged
        SetBackColor(txtDefaultConfigFilePath)
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtSettingsFile.Text = txtDefaultConfigFilePath.Text & "\" & basCode.curVar.GeneralSettings.Substring(basCode.curVar.GeneralSettings.LastIndexOf("\") + 1, basCode.curVar.GeneralSettings.Length - (basCode.curVar.GeneralSettings.LastIndexOf("\") + 1))
            txtConnectionsFile.Text = txtDefaultConfigFilePath.Text & "\" & basCode.curVar.ConnectionsFile.Substring(basCode.curVar.ConnectionsFile.LastIndexOf("\") + 1, basCode.curVar.ConnectionsFile.Length - (basCode.curVar.ConnectionsFile.LastIndexOf("\") + 1))
            txtErrorlogPath.Text = txtDefaultConfigFilePath.Text
        Else
            txtSettingsFile.Text = basCode.curVar.GeneralSettings.Substring(basCode.curVar.GeneralSettings.LastIndexOf("\") + 1, basCode.curVar.GeneralSettings.Length - (basCode.curVar.GeneralSettings.LastIndexOf("\") + 1))
            txtConnectionsFile.Text = basCode.curVar.ConnectionsFile.Substring(basCode.curVar.ConnectionsFile.LastIndexOf("\") + 1, basCode.curVar.ConnectionsFile.Length - (basCode.curVar.ConnectionsFile.LastIndexOf("\") + 1))
            txtErrorlogPath.Text = basCode.dhdText.LogLocation
        End If

    End Sub

    Private Sub txtSettingsFile_TextChanged(sender As Object, e As EventArgs) Handles txtSettingsFile.TextChanged
        SetBackColor(txtSettingsFile)
    End Sub

    Private Sub txtConnectionsFile_TextChanged(sender As Object, e As EventArgs) Handles txtConnectionsFile.TextChanged
        SetBackColor(txtConnectionsFile)
    End Sub

    Private Sub chkAllowSettingsChange_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowSettingsChange.CheckedChanged
        SetBackColor(chkAllowSettingsChange)
    End Sub

    Private Sub chkAllowConfiguration_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowConfiguration.CheckedChanged
        SetBackColor(chkAllowConfiguration)
    End Sub

    Private Sub chkAllowLinkedServers_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowLinkedServers.CheckedChanged
        SetBackColor(chkAllowLinkedServers)
    End Sub

    Private Sub chkAllowQueryEdit_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowQueryEdit.CheckedChanged
        SetBackColor(chkAllowQueryEdit)
    End Sub

    Private Sub chkAllowDataImport_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowDataImport.CheckedChanged
        SetBackColor(chkAllowDataImport)
    End Sub

    Private Sub chkAllowSmartUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowSmartUpdate.CheckedChanged
        SetBackColor(chkAllowSmartUpdate)
    End Sub

    Private Sub chkAllowUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowUpdate.CheckedChanged
        SetBackColor(chkAllowUpdate)
    End Sub

    Private Sub chkAllowInsert_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowInsert.CheckedChanged
        SetBackColor(chkAllowInsert)
    End Sub

    Private Sub chkAllowDelete_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowDelete.CheckedChanged
        SetBackColor(chkAllowDelete)
    End Sub

    Private Sub txtTimerHours_TextChanged(sender As Object, e As EventArgs) Handles txtTimerHours.TextChanged
        SetBackColor(txtTimerHours)
    End Sub

    Private Sub chkLimitLookupLists_CheckedChanged(sender As Object, e As EventArgs) Handles chkLimitLookupLists.CheckedChanged
        SetBackColor(chkLimitLookupLists)
    End Sub

    Private Sub txtLimitLookupLists_TextChanged(sender As Object, e As EventArgs) Handles txtLimitLookupLists.TextChanged
        SetBackColor(txtLimitLookupLists)
    End Sub

    Private Sub chkIncludeDateInExportFiles_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeDateInExportFiles.CheckedChanged
        SetBackColor(chkIncludeDateInExportFiles)
    End Sub

    Private Sub btnSettingsGeneralSave_Click(sender As Object, e As EventArgs) Handles btnSettingsGeneralSave.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim blnSettingsChanged As Boolean = False
        Dim blnGeneralSettingsChanged As Boolean = False

        If txtDefaultConfigFilePath.BackColor = clrMarked Then
            basCode.curVar.DefaultConfigFilePath = txtDefaultConfigFilePath.Text
            txtDefaultConfigFilePath.Tag = basCode.curVar.DefaultConfigFilePath
            txtDefaultConfigFilePath.BackColor = clrOriginal
            basCode.dhdText.CheckDir(basCode.curVar.DefaultConfigFilePath, True)
            blnSettingsChanged = True
        End If
        If txtSettingsFile.BackColor = clrMarked Then
            basCode.curVar.GeneralSettings = txtSettingsFile.Text
            txtSettingsFile.Tag = basCode.curVar.GeneralSettings
            txtSettingsFile.BackColor = clrOriginal
            'dhdText.CheckDir(basCode.CurVar.GeneralSettings, True)
            blnSettingsChanged = True
        End If
        If chkAllowSettingsChange.BackColor = clrMarked Then
            basCode.curVar.AllowSettingsChange = chkAllowSettingsChange.Checked
            chkAllowSettingsChange.Tag = basCode.curVar.AllowSettingsChange
            chkAllowSettingsChange.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowConfiguration.BackColor = clrMarked Then
            basCode.curVar.AllowConfiguration = chkAllowConfiguration.Checked
            chkAllowConfiguration.Tag = basCode.curVar.AllowConfiguration
            chkAllowConfiguration.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowLinkedServers.BackColor = clrMarked Then
            basCode.curVar.AllowLinkedServers = chkAllowLinkedServers.Checked
            chkAllowLinkedServers.Tag = basCode.curVar.AllowLinkedServers
            chkAllowLinkedServers.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowQueryEdit.BackColor = clrMarked Then
            basCode.curVar.AllowQueryEdit = chkAllowQueryEdit.Checked
            chkAllowQueryEdit.Tag = basCode.curVar.AllowQueryEdit
            chkAllowQueryEdit.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowDataImport.BackColor = clrMarked Then
            basCode.curVar.AllowDataImport = chkAllowDataImport.Checked
            chkAllowDataImport.Tag = basCode.curVar.AllowDataImport
            chkAllowDataImport.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowSmartUpdate.BackColor = clrMarked Then
            basCode.curVar.AllowSmartUpdate = chkAllowSmartUpdate.Checked
            chkAllowSmartUpdate.Tag = basCode.curVar.AllowSmartUpdate
            chkAllowSmartUpdate.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowUpdate.BackColor = clrMarked Then
            basCode.curVar.AllowUpdate = chkAllowUpdate.Checked
            chkAllowUpdate.Tag = basCode.curVar.AllowUpdate
            chkAllowUpdate.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowInsert.BackColor = clrMarked Then
            basCode.curVar.AllowInsert = chkAllowInsert.Checked
            chkAllowInsert.Tag = basCode.curVar.AllowInsert
            chkAllowInsert.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowDelete.BackColor = clrMarked Then
            basCode.curVar.AllowDelete = chkAllowDelete.Checked
            chkAllowDelete.Tag = basCode.curVar.AllowDelete
            chkAllowDelete.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If txtTimerHours.BackColor = clrMarked Then
            If IsNumeric(txtTimerHours.Text) = True Then
                Dim intTimer As Integer = txtTimerHours.Text
                If intTimer > 576 Then
                    MessageBox.Show("The timer cannot exceed 576 hours (24 days). please choose a value between 0 and 576", "Value to high", MessageBoxButtons.OK)
                    txtTimerHours.Text = 0
                End If
            Else
                txtTimerHours.Text = 0
            End If
            basCode.curVar.TimedShutdown = txtTimerHours.Text * 60 * 60 * 1000
            txtTimerHours.Tag = basCode.curVar.TimedShutdown / 60 / 60 / 1000
            txtTimerHours.BackColor = clrOriginal
            SetTimer()
            blnSettingsChanged = True
        End If

        If txtOverridePassword.Text.Length > 0 And txtOverridePassword.Text <> txtOverridePassword.Tag Then
            basCode.curVar.OverridePassword = basCode.dhdText.MD5Encrypt(txtOverridePassword.Text)
            blnSettingsChanged = True
        End If

        If chkLimitLookupLists.BackColor = clrMarked Then
            basCode.curVar.LimitLookupLists = chkLimitLookupLists.Checked
            chkLimitLookupLists.Tag = basCode.curVar.LimitLookupLists
            chkLimitLookupLists.BackColor = clrControl
            blnGeneralSettingsChanged = True
        End If
        If txtLimitLookupLists.BackColor = clrMarked Then
            basCode.curVar.LimitLookupListsCount = txtLimitLookupLists.Text
            txtLimitLookupLists.Tag = basCode.curVar.LimitLookupListsCount
            txtLimitLookupLists.BackColor = clrOriginal
            blnSettingsChanged = True
        End If

        If txtConnectionsFile.BackColor = clrMarked Then
            basCode.curVar.ConnectionsFile = txtConnectionsFile.Text
            txtConnectionsFile.Tag = basCode.curVar.ConnectionsFile
            blnGeneralSettingsChanged = True
            txtConnectionsFile.BackColor = clrOriginal
            'dhdText.CheckDir(basCode.CurVar.ConnectionsFile, True)
        End If
        If cbxDateFormats.BackColor = clrMarked Then
            basCode.curVar.DateTimeStyle = cbxDateFormats.SelectedValue.ToString
            blnGeneralSettingsChanged = True
            cbxDateFormats.BackColor = clrOriginal
        End If
        If chkIncludeDateInExportFiles.BackColor = clrMarked Then
            basCode.curVar.IncludeDate = chkIncludeDateInExportFiles.Checked
            chkIncludeDateInExportFiles.Tag = basCode.curVar.IncludeDate
            chkIncludeDateInExportFiles.BackColor = clrOriginal
            blnGeneralSettingsChanged = True
        End If

        If blnSettingsChanged = True Then
            If basCode.SaveSDBASettingsXml(xmlSDBASettings) = False Then
                WriteStatus("Error saving settings file. please check the log.", 1, lblStatusText)
            Else
                WriteStatus("Settings file saved", 0, lblStatusText)
            End If
            blnSettingsChanged = False

        End If
        If blnGeneralSettingsChanged = True Then
            If basCode.SaveGeneralSettingsXml(xmlGeneralSettings) = False Then
                WriteStatus("Error saving settings file. please check the log.", 1, lblStatusText)
            Else
                WriteStatus("Settings file saved", 0, lblStatusText)
            End If
            blnGeneralSettingsChanged = False
        End If
        CursorControl()
    End Sub

    Private Sub btnConfigFilePath_Click(sender As Object, e As EventArgs) Handles btnConfigFilePath.Click
        WriteStatus("", 0, lblStatusText)
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = Application.StartupPath

        If (DefaultFolder.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (DefaultFolder.SelectedPath.Length) > 0 Then
            txtDefaultConfigFilePath.Text = DefaultFolder.SelectedPath
        End If
    End Sub

    Private Sub btnSettingsFile_Click(sender As Object, e As EventArgs) Handles btnSettingsFile.Click
        WriteStatus("", 0, lblStatusText)
        Dim loadFile1 As New OpenFileDialog
        loadFile1.InitialDirectory = txtDefaultConfigFilePath.Text
        loadFile1.Title = "Settings File"
        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Sequenchel Config Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            txtSettingsFile.Text = loadFile1.FileName
        End If
    End Sub

    Private Sub btnConnectionsFile_Click(sender As Object, e As EventArgs) Handles btnConnectionsFile.Click
        WriteStatus("", 0, lblStatusText)
        Dim loadFile1 As New OpenFileDialog
        loadFile1.InitialDirectory = txtDefaultConfigFilePath.Text
        loadFile1.Title = "Connections File"
        loadFile1.DefaultExt = "*.xml"
        loadFile1.Filter = "Sequenchel Config Files|*.xml"

        If (loadFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (loadFile1.FileName.Length) > 0 Then
            txtConnectionsFile.Text = loadFile1.FileName
        End If
    End Sub

    Private Sub DateFormatsLoad()
        Dim dtFormats As DataTable = New DataTable()
        dtFormats.Columns.Add("ID", GetType(Integer))
        dtFormats.Columns.Add("Name", GetType(String))
        dtFormats.Columns.Add("Format", GetType(String))

        dtFormats.Rows.Add(120, "Universal", "yyyy-mm-dd hh:mi:ss(24h)")
        dtFormats.Rows.Add(100, "SQL Default", "mon dd yyyy hh:miAM (or PM)")
        dtFormats.Rows.Add(101, "US", "mm/dd/yyyy")
        dtFormats.Rows.Add(105, "Italian", "dd-mm-yyyy")
        dtFormats.Rows.Add(109, "SQL Default + millsec.", "mon dd yyyy hh:mi:ss:mmmAM (or PM)")
        dtFormats.Rows.Add(113, "EU Default + millsec.", "dd mon yyyy hh:mi:ss:mmm(24h)")

        ' Bind the ComboBox to the DataTable
        Me.cbxDateFormats.DataSource = dtFormats
        'Me.cbxDateFormats.DisplayMember = "Name"
        Me.cbxDateFormats.DisplayMember = "Format"
        Me.cbxDateFormats.ValueMember = "ID"

        ' Enable the owner draw on the ComboBox.
        Me.cbxDateFormats.DrawMode = DrawMode.OwnerDrawFixed
        ' Handle the DrawItem event to draw the items.

        cbxDateFormats.SelectedValue = basCode.curVar.DateTimeStyle
    End Sub

    Private Sub cbxDateFormats_DrawItem(ByVal sender As System.Object, _
                               ByVal e As System.Windows.Forms.DrawItemEventArgs) _
                               Handles cbxDateFormats.DrawItem
        ' Draw the default background
        e.DrawBackground()

        ' The ComboBox is bound to a DataTable,
        ' so the items are DataRowView objects.
        Dim drv As DataRowView = CType(cbxDateFormats.Items(e.Index), DataRowView)

        ' Retrieve the value of each column.
        Dim id As Integer = drv("ID").ToString()
        Dim name As String = drv("Name").ToString()
        Dim format As String = drv("Format").ToString()

        ' Get the bounds for the first column
        Dim r1 As Rectangle = e.Bounds
        r1.Width = r1.Width / (100 / 7)

        ' Draw the text on the first column
        Using sb As SolidBrush = New SolidBrush(e.ForeColor)
            e.Graphics.DrawString(id, e.Font, sb, r1)
        End Using

        ' Draw a line to isolate the columns 
        Using p As Pen = New Pen(Color.Black)
            e.Graphics.DrawLine(p, r1.Right, 0, r1.Right, r1.Bottom)
        End Using

        ' Get the bounds for the second column
        Dim r2 As Rectangle = e.Bounds
        r2.X = e.Bounds.Width / (100 / 7)
        r2.Width = r2.Width / (100 / 35)

        ' Draw the text on the second column
        Using sb As SolidBrush = New SolidBrush(e.ForeColor)
            e.Graphics.DrawString(name, e.Font, sb, r2)
        End Using

        ' Draw a line to isolate the columns 
        Using p As Pen = New Pen(Color.Black)
            e.Graphics.DrawLine(p, r2.Right, 0, r2.Right, r2.Bottom)
        End Using

        ' Get the bounds for the third column
        Dim r3 As Rectangle = e.Bounds
        r3.X = e.Bounds.Width / (100 / 42)
        r3.Width = r3.Width / (100 / 58)

        ' Draw the text on the second column
        Using sb As SolidBrush = New SolidBrush(e.ForeColor)
            e.Graphics.DrawString(format, e.Font, sb, r3)
        End Using

    End Sub

    Private Sub cbxDateFormats_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxDateFormats.SelectedIndexChanged
        If cbxDateFormats.SelectedValue.ToString = basCode.curVar.DateTimeStyle Then
            cbxDateFormats.BackColor = clrOriginal
        Else
            cbxDateFormats.BackColor = clrMarked
        End If
    End Sub

    Private Sub ResetColors()
        cbxDateFormats.BackColor = clrOriginal
        txtSettingsFile.BackColor = clrOriginal
        txtConnectionsFile.BackColor = clrOriginal

    End Sub

    Private Sub txtOverridePassword_GotFocus(sender As Object, e As EventArgs) Handles txtOverridePassword.GotFocus
        RemoveDefaultText(txtOverridePassword)
        PasswordCharSet(txtOverridePassword)
    End Sub

    Private Sub txtOverridePassword_LostFocus(sender As Object, e As EventArgs) Handles txtOverridePassword.LostFocus
        SetDefaultText(txtOverridePassword)
        PasswordCharSet(txtOverridePassword)
    End Sub

    Private Sub btnShowOverridePassword_MouseDown(sender As Object, e As MouseEventArgs) Handles btnShowOverridePassword.MouseDown
        txtOverridePassword.PasswordChar = Nothing
    End Sub

    Private Sub btnShowOverridePassword_MouseLeave(sender As Object, e As EventArgs) Handles btnShowOverridePassword.MouseLeave
        PasswordCharSet(txtOverridePassword)
    End Sub

    Private Sub btnShowOverridePassword_MouseUp(sender As Object, e As MouseEventArgs) Handles btnShowOverridePassword.MouseUp
        PasswordCharSet(txtOverridePassword)
    End Sub

#End Region

#Region "License"

    Private Sub btnSaveLicense_Click(sender As Object, e As EventArgs) Handles btnSaveLicense.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim strLocation As String = ""
        If SeqCore.CheckLicenseKey(txtLicenseKey.Text, txtLicenseName.Text, basCode.GetVersion("M"), Nothing) = False Then
            'MessageBox.Show(strMessages.strLicenseError)
            WriteStatus("Your license validated: " & SeqCore.LicenseValidated, 2, lblStatusText)
        End If
        If SeqCore.LicenseValidated = True Then
            Try
                basCode.dhdReg.AddLMRegKey("LicenseName", txtLicenseName.Text)
                If basCode.dhdReg.ErrorLevel = -1 Then
                    basCode.dhdReg.AddCURRegKey("LicenseName", txtLicenseName.Text)
                    strLocation = "HK Current User"
                Else
                    strLocation = "HKLM"
                End If
                If basCode.curVar.DebugMode And basCode.dhdReg.ErrorLevel = -1 Then MessageBox.Show(basCode.dhdReg.RegMessage)
                basCode.dhdReg.AddLMRegKey("LicenseKey", txtLicenseKey.Text)
                If basCode.dhdReg.ErrorLevel = -1 Then
                    basCode.dhdReg.AddCURRegKey("LicenseKey", txtLicenseKey.Text)
                    strLocation = "HK Current User"
                Else
                    strLocation = "HKLM"
                End If
                If basCode.curVar.DebugMode And basCode.dhdReg.ErrorLevel = -1 Then MessageBox.Show(basCode.dhdReg.RegMessage)
                WriteStatus("Your License information has been saved to " & strLocation, 0, lblStatusText)
            Catch ex As Exception
                WriteStatus("Errror saving license. Please check the log. ", 1, lblStatusText)
                basCode.WriteLog("There ws an errror saving your license information: " & ex.Message, 1)
            End Try
        End If
        CursorControl()
    End Sub

    Private Sub btnValidateLicense_Click(sender As Object, e As EventArgs) Handles btnValidateLicense.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        SeqCore.CheckLicenseKey(txtLicenseKey.Text, txtLicenseName.Text, basCode.GetVersion("M"), Nothing)
        WriteStatus("Your license validated: " & SeqCore.LicenseValidated, 2, lblStatusText)
        CursorControl()
    End Sub

#End Region

#Region "Logging"

    Private Sub LogSettingsShow()
        Select Case basCode.dhdText.LogLevel
            Case 0
                rbtLoggingLevel0.Checked = True
            Case 1
                rbtLoggingLevel1.Checked = True
            Case 2
                rbtLoggingLevel2.Checked = True
            Case 3
                rbtLoggingLevel3.Checked = True
            Case 4
                rbtLoggingLevel4.Checked = True
            Case 5
                rbtLoggingLevel5.Checked = True
        End Select
        txtLogfileName.Text = basCode.dhdText.LogFileName
        txtLogfileLocation.Text = basCode.dhdText.LogLocation
        'If TxtHandle.LogLocation.ToLower = "database" Then grpLogsToKeep.Visible = True

        chkAutoDeleteOldLogs.Checked = basCode.dhdText.AutoDelete
        Select Case basCode.dhdText.Retenion
            Case "Day"
                rbtKeepLogDay.Checked = True
            Case "Week"
                rbtKeepLogWeek.Checked = True
            Case "Month"
                rbtKeepLogMonth.Checked = True
            Case Else
                'Exit Sub
        End Select
    End Sub

    Private Sub btnSaveSettingsLog_Click(sender As Object, e As EventArgs) Handles btnSaveSettingsLog.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        basCode.dhdText.LogFileName = txtLogfileName.Text
        basCode.dhdText.LogLevel = 1
        basCode.dhdText.LogLocation = txtLogfileLocation.Text
        basCode.dhdText.CheckDir(basCode.dhdText.LogLocation, True)

        If rbtLoggingLevel0.Checked Then
            basCode.dhdText.LogLevel = 0
        ElseIf rbtLoggingLevel1.Checked Then
            basCode.dhdText.LogLevel = 1
        ElseIf rbtLoggingLevel2.Checked Then
            basCode.dhdText.LogLevel = 2
        ElseIf rbtLoggingLevel3.Checked Then
            basCode.dhdText.LogLevel = 3
        ElseIf rbtLoggingLevel4.Checked Then
            basCode.dhdText.LogLevel = 4
        ElseIf rbtLoggingLevel5.Checked Then
            basCode.dhdText.LogLevel = 5
        End If

        If chkAutoDeleteOldLogs.Checked = True Then
            basCode.dhdText.AutoDelete = True
            If rbtKeepLogDay.Checked = True Then basCode.dhdText.Retenion = "Day"
            If rbtKeepLogWeek.Checked = True Then basCode.dhdText.Retenion = "Week"
            If rbtKeepLogMonth.Checked = True Then basCode.dhdText.Retenion = "Month"
        Else
            basCode.dhdText.AutoDelete = False
        End If
        If basCode.SaveGeneralSettingsXml(xmlGeneralSettings) = False Then
            WriteStatus("General Settings file not saved. Please check the log.", 1, lblStatusText)
        Else
            WriteStatus("General Settings file saved.", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnClearOldLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearOldLogs.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If basCode.dhdText.LogLocation.ToLower = "database" Then
            Dim dtmDate As Date = Today

            If rbtKeepLogDay.Checked = True Then dtmDate = dtmDate.AddDays(-1)
            If rbtKeepLogWeek.Checked = True Then dtmDate = dtmDate.AddDays(-7)
            If rbtKeepLogMonth.Checked = True Then dtmDate = dtmDate.AddMonths(-1)
            If basCode.curVar.DebugMode = True Then MessageBox.Show(dtmDate.ToString)
            If basCode.dhdText.LogLocation.ToLower = "database" Then
                If ClearDBLog(dtmDate) = False Then
                    WriteStatus("Error deleting old logs. Please check the log.", 1, lblStatusText)
                Else
                    WriteStatus("old logs deleted.", 0, lblStatusText)
                End If
            Else
                WriteStatus("Log clearance only for database logging.", 2, lblStatusText)
            End If
        End If
        CursorControl()
    End Sub

    Private Sub btnLogfileNameDefault_Click(sender As Object, e As EventArgs) Handles btnLogfileNameDefault.Click
        WriteStatus("", 0, lblStatusText)
        txtLogfileName.Text = "Sequenchel.log"
    End Sub

    Private Sub btnLogLocationDatabase_Click(sender As Object, e As EventArgs) Handles btnLogLocationDatabase.Click
        WriteStatus("", 0, lblStatusText)
        txtLogfileLocation.Text = "Database"
    End Sub

    Private Sub btnLogLocationSystem_Click(sender As Object, e As EventArgs) Handles btnLogLocationSystem.Click
        WriteStatus("", 0, lblStatusText)
        txtLogfileLocation.Text = Application.StartupPath & "\LOG"
    End Sub

    Private Sub btnLogLocationDefault_Click(sender As Object, e As EventArgs) Handles btnLogLocationDefault.Click
        WriteStatus("", 0, lblStatusText)
        txtLogfileLocation.Text = basCode.curVar.DefaultConfigFilePath & "\LOG"
    End Sub

    Private Sub btnLogLocationBrowse_Click(sender As Object, e As EventArgs) Handles btnLogLocationBrowse.Click
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = basCode.CurVar.DefaultConfigFilePath

        If (DefaultFolder.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (DefaultFolder.SelectedPath.Length) > 0 Then
            txtLogfileLocation.Text = DefaultFolder.SelectedPath
        End If
    End Sub

    Private Sub txtLogfileLocation_TextChanged(sender As Object, e As EventArgs) Handles txtLogfileLocation.TextChanged
        If txtLogfileLocation.Text.ToLower = "database" Then
            grpLogsToKeep.Visible = True
        Else
            grpLogsToKeep.Visible = False
        End If

    End Sub

#End Region

#Region "Database"
    Private Sub btnCreateDatabase_Click(sender As Object, e As EventArgs) Handles btnCreateDatabase.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        CreateDatabase(True)
        CursorControl()
    End Sub

    Private Sub btnRefreshDatabase_Click(sender As Object, e As EventArgs) Handles btnRefreshDatabase.Click
        WriteStatus("", 0, lblStatusText)
        If MessageBox.Show("This will update all standard Sequenchel Views, Stored Procedures and Functions to their latest version. " & Environment.NewLine _
            & "Your Tables will not be changed." & Environment.NewLine _
            & basCode.Message.strAreYouSure, basCode.Message.strWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
        CursorControl("Wait")
        CreateDatabase(False)
        CursorControl()
    End Sub

    Friend Sub CreateDatabase(Optional ByVal blnIncludeTables As Boolean = True)
        Dim strDBName As String
        Dim strSQL As String
        Dim intLogLevel As Integer = 6
        If basCode.CurVar.Encryption = False Then intLogLevel = 5

        btnCreateExtraProcs.Visible = False
        lblStatusDatabase.Visible = True
        prbCreateDatabase.Visible = True

        If blnIncludeTables = True Then
            basCode.dhdMainDB.DataProvider = cbxDataProvider.SelectedItem
            basCode.dhdMainDB.DataLocation = txtDatabaseLocation.Text
            basCode.dhdMainDB.LoginMethod = cbxLoginMethod.SelectedItem
            basCode.dhdMainDB.LoginName = txtLoginName.Text
            If txtPassword.Text.Length > 0 And txtPassword.Text <> txtPassword.Tag Then basCode.dhdMainDB.Password = txtPassword.Text
            basCode.dhdMainDB.DatabaseName = "master"
            strDBName = txtDatabaseName.Text
            If basCode.CurVar.DebugMode Then
                MessageBox.Show("Sequenchel v " & Application.ProductVersion & " Database Creation" & Environment.NewLine _
                 & "   DatabaseServer = " & basCode.dhdMainDB.DataLocation & Environment.NewLine _
                 & "   Database Context = " & basCode.dhdMainDB.DatabaseName & Environment.NewLine _
                 & "   Database to create: = " & strDBName & Environment.NewLine _
                 & "   DataProvider = " & basCode.dhdMainDB.DataProvider & Environment.NewLine _
                 & "   LoginMethod = " & basCode.dhdMainDB.LoginMethod & Environment.NewLine _
                 & "   Running in Debug Mode")
            End If
        Else
            strDBName = basCode.dhdMainDB.DatabaseName
        End If

        Try
            basCode.dhdMainDB.CheckDB()
            If basCode.dhdMainDB.DataBaseOnline = False Then
                WriteStatus("The server was not found. Please check your settings", 2, lblStatusText)
                basCode.dhdMainDB.DatabaseName = strDBName
                Exit Sub
            End If
        Catch ex As Exception
            WriteStatus("There was an error connecting to the server. please check your settings", 2, lblStatusText)
            basCode.dhdMainDB.DatabaseName = strDBName
            Exit Sub
        End Try

        Try
            Dim MydbRef As New SDBA.DBRef
            Dim arrScripts(100) As String
            arrScripts = MydbRef.GetList(blnIncludeTables)
            prbCreateDatabase.Maximum = arrScripts.GetUpperBound(0)

            strSQL = MydbRef.GetScript(arrScripts(0))
            strSQL = strSQL.Replace("Sequenchel", strDBName)
            If basCode.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If basCode.CurVar.DevMode Then MessageBox.Show(strSQL)
            basCode.QueryDb(basCode.dhdMainDB, strSQL, False, 10)
            basCode.dhdMainDB.DatabaseName = strDBName
            txtJobNamePrefix.Text = basCode.dhdMainDB.DatabaseName
            prbCreateDatabase.PerformStep()

            For i = 1 To arrScripts.GetUpperBound(0)
                strSQL = MydbRef.GetScript(arrScripts(i))
                If Not strSQL = "-1" Then
                    'strSQL = Replace(strSQL, "Sequenchel", strDBName)
                    strSQL = strSQL.Replace("Sequenchel", strDBName)
                    If basCode.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
                    If blnIncludeTables = False Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
                    If basCode.CurVar.DevMode Then MessageBox.Show(strSQL)
                    basCode.QueryDb(basCode.dhdMainDB, strSQL, False, 5)
                Else
                    If basCode.CurVar.DebugMode Then MessageBox.Show("The script: " & arrScripts(i) & " returned: " & strSQL)
                End If
                prbCreateDatabase.PerformStep()
            Next
            prbCreateDatabase.PerformStep()

            SaveConfigSetting("Database", "Version", My.Application.Info.Version.ToString)
            'btnCreateDemoData.Visible = True

            WriteStatus("Database created/updated.", 0, lblStatusText)
        Catch ex As Exception
            basCode.dhdMainDB.DatabaseName = strDBName
            txtJobNamePrefix.Text = basCode.dhdMainDB.DatabaseName
            basCode.WriteLog("An error occured creating the database: " & ex.Message, 1)
            WriteStatus("Error creating the database. Please check the log.", 1, lblStatusText)
        End Try

        lblStatusDatabase.Visible = False
        prbCreateDatabase.Visible = False
        btnCreateExtraProcs.Visible = True

        VersionLoad()
    End Sub

    Private Function VersionLoad() As Boolean
        Dim strVersion As String, strTarget As String

        strVersion = LoadConfigSetting("Database", "Version")
        If strVersion = Nothing Then
            SaveConfigSetting("Database", "Version", My.Application.Info.Version.ToString)
            Return False
        End If
        txtUpgradeDatabase.Text = strVersion
        txtUpgradeDatabase.Tag = strVersion

        Dim MydbRef As New SDBA.DBRef
        strTarget = MydbRef.GetVersion(strVersion)

        If Not (strTarget = Nothing Or strTarget = "") Then
            btnUpgradeDatabase.Enabled = True
            txtUpgradeDatabase.Text &= " -> " & strTarget
            txtUpgradeDatabase.Tag = strTarget
            tabSettings.SelectTab(tpgDatabase)
            btnUpgradeDatabase.BackColor = clrWarning
            MessageBox.Show(basCode.Message.strUpdateDatabase)
            Return True
        Else
            btnUpgradeDatabase.Enabled = False
            Return False
        End If

    End Function

    Private Sub btnDatabaseDefaultsUse_Click(sender As Object, e As EventArgs) Handles btnDatabaseDefaultsUse.Click
        WriteStatus("", 0, lblStatusText)
        txtDatabaseLocation.Text = Environment.MachineName & "\SQLEXPRESS"
        txtDatabaseName.Text = "Sequenchel"
        cbxDataProvider.SelectedItem = "SQL"
        cbxLoginMethod.SelectedItem = "WINDOWS"
        txtLoginName.Text = ""
        SetDefaultText(txtPassword)
    End Sub

    Private Sub btnSaveSettingsDatabase_Click(sender As Object, e As EventArgs) Handles btnSaveSettingsDatabase.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If cbxDataProvider.Items.Contains(cbxDataProvider.Text) = False Or cbxLoginMethod.Items.Contains(cbxLoginMethod.Text) = False Then
            MessageBox.Show(basCode.Message.strPreconfigured & Environment.NewLine & basCode.Message.strCheckSettings)
            Exit Sub
        End If
        'If basCode.dhdMainDB.DataLocation <> txtDatabaseLocation.Text Or _
        ' basCode.dhdMainDB.DatabaseName <> txtDatabaseName.Text Or _
        ' basCode.dhdMainDB.DataProvider <> cbxDataProvider.SelectedItem Then
        '    If MessageBox.Show(strMessages.strSettingReload & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'End If
        Try
            basCode.dhdMainDB.DataLocation = txtDatabaseLocation.Text
            basCode.dhdMainDB.DatabaseName = txtDatabaseName.Text
            basCode.dhdMainDB.DataProvider = cbxDataProvider.SelectedItem
            basCode.dhdMainDB.LoginMethod = cbxLoginMethod.SelectedItem
            basCode.dhdMainDB.LoginName = txtLoginName.Text
            If txtPassword.Text.Length > 0 And txtPassword.Text <> txtPassword.Tag Then basCode.dhdMainDB.Password = txtPassword.Text
            DatabaseTest(basCode.dhdMainDB)
            If basCode.dhdMainDB.DataBaseOnline = False Then
                If MessageBox.Show("The database specified was not found." & Environment.NewLine & "Do you wish to save anyway?", "Database not found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    WriteStatus("Database not found. Aborting save.", 0, lblStatusText)
                    Exit Sub
                End If
            End If
            If basCode.SaveGeneralSettingsXml(xmlGeneralSettings) = False Then
                WriteStatus("General Settings file not saved. Please check the log.", 1, lblStatusText)
            Else
                WriteStatus("General Settings file saved.", 0, lblStatusText)
            End If
            txtJobNamePrefix.Text = basCode.dhdMainDB.DatabaseName
            'btnSaveSettingsDatabase.BackColor = clrOriginal
        Catch ex As Exception
            basCode.WriteLog("An error occured saving database settings: " & ex.Message, 1)
            WriteStatus("Error saving database settings. Please check the log.", 1, lblStatusText)
            basCode.LoadGeneralSettingsXml(xmlGeneralSettings)
            DatabaseShow()
            WriteStatus("Error saving database settings. Please check the log.", 1, lblStatusText)
        End Try
        CursorControl()
    End Sub

    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        WriteStatus("", 0, lblStatusText)
        basCode.dhdMainDB.DataProvider = cbxDataProvider.SelectedItem
        basCode.dhdMainDB.DataLocation = txtDatabaseLocation.Text
        basCode.dhdMainDB.LoginMethod = cbxLoginMethod.SelectedItem
        basCode.dhdMainDB.LoginName = txtLoginName.Text
        basCode.dhdMainDB.Password = txtPassword.Text
        basCode.dhdMainDB.DatabaseName = txtDatabaseName.Text

        DatabaseTest(basCode.dhdMainDB)
        WriteStatus("Database Connection tested: " & basCode.dhdMainDB.DataBaseOnline, 0, lblStatusText)
    End Sub

    Private Sub DatabaseShow()
        SetDefaultText(txtPassword)
        PasswordCharSet(txtPassword)
        cbxDataProvider.SelectedItem = basCode.dhdMainDB.DataProvider
        txtDatabaseLocation.Text = basCode.dhdMainDB.DataLocation
        txtDatabaseName.Text = basCode.dhdMainDB.DatabaseName
        cbxLoginMethod.SelectedItem = basCode.dhdMainDB.LoginMethod
        txtLoginName.Text = basCode.dhdMainDB.LoginName
        'txtPassword.Text = basCode.dhdMainDB.Password
        cbxLoginMethod_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub cbxLoginMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxLoginMethod.SelectedIndexChanged
        If cbxLoginMethod.SelectedItem = "SQL" Then
            txtLoginName.Enabled = True
            txtPassword.Enabled = True
            txtLoginName.Text = basCode.dhdMainDB.LoginName
            'txtPassword.Text = basCode.dhdMainDB.Password
            SetDefaultText(txtPassword)
            btnShowDatabasePassword.Enabled = True
        ElseIf cbxLoginMethod.SelectedItem = "Windows" Then
            txtLoginName.Enabled = False
            txtPassword.Enabled = False
            txtLoginName.Text = ""
            RemoveDefaultText(txtPassword)
            btnShowDatabasePassword.Enabled = False
        End If
    End Sub

    Private Sub btnUpgradeDatabase_Click(sender As Object, e As EventArgs) Handles btnUpgradeDatabase.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim strSQL As String
        Dim strMode As String
        Dim MydbRef As New SDBA.DBRef

        btnUpgradeDatabase.Enabled = False

        Try
            Dim arrScripts(100, 1) As String
            arrScripts = MydbRef.GetNewList(txtUpgradeDatabase.Tag)

            strSQL = MydbRef.GetScript(arrScripts(0, 0))
            If basCode.CurVar.DebugMode Then MessageBox.Show("<debug>Number of Scripts: " & arrScripts.GetUpperBound(1) + 1)

            strMode = arrScripts(1, 0)
            If strSQL = "-1" Then
                MessageBox.Show("Error retrieving SQL Script " & arrScripts(0, 0) & ", please contact support")
                Exit Sub
            End If
            strSQL = Replace(strSQL, "Sequenchel", basCode.dhdMainDB.DatabaseName)
            If basCode.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If strMode = "ALTER" Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
            basCode.QueryDb(basCode.dhdMainDB, strSQL, False)

            For i = 1 To arrScripts.GetUpperBound(1)
                strSQL = MydbRef.GetScript(arrScripts(0, i))
                If Not strSQL = "-1" Then
                    strSQL = Replace(strSQL, "Sequenchel", basCode.dhdMainDB.DatabaseName)
                    strMode = arrScripts(1, i)
                    If strMode = "ALTER" Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
                    basCode.QueryDb(basCode.dhdMainDB, strSQL, False)
                Else
                    MessageBox.Show("Error retrieving SQL Script " & arrScripts(0, i) & ", please contact support")
                    Exit Sub
                End If
            Next
            SaveConfigSetting("Database", "Version", txtUpgradeDatabase.Tag)
        Catch ex As Exception
            basCode.WriteLog("An error occured upgrading the database: " & ex.Message, 1)
            WriteStatus("Error upgrading database. Please check the log.", 1, lblStatusText)
            Exit Sub
        End Try
        btnUpgradeDatabase.BackColor = clrOriginal
        txtUpgradeDatabase.Text = txtUpgradeDatabase.Tag
        txtUpgradeDatabase.Tag = ""
        CursorControl()
        WriteStatus("Database Update successfull", 0, lblStatusText)
        VersionLoad()
    End Sub

    Private Sub btnBackupDatabase_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Try
            BackupDatabase(basCode.dhdMainDB, txtBackupDatabase.Text)
            SaveConfigSetting("Database", "BackupLocation", txtBackupDatabase.Text, "A valid location on the server")
            WriteStatus("Database backup created", 0, lblStatusText)
        Catch ex As Exception
            CursorControl()
            basCode.WriteLog("While saving the database, the following error occured: " & Environment.NewLine & ex.Message, 1)
            WriteStatus("Error creating backup. Please check the log.", 1, lblStatusText)
        End Try
        CursorControl()
    End Sub

    Private Sub btnCreateExtraProcs_Click(sender As Object, e As EventArgs) Handles btnCreateExtraProcs.Click
        If MessageBox.Show("This will create extra procedures with the power to damage your data or database. Create these procedures in a secure database." & Environment.NewLine & basCode.Message.strContinue, basCode.Message.strWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)

        Dim strSQL As String
        Dim MydbRef As New SDBA.DBRef

        Try
            Dim arrScripts(100) As String
            arrScripts = MydbRef.GetDBAList()

            strSQL = MydbRef.GetScript(arrScripts(0))
            strSQL = strSQL.Replace("Sequenchel", basCode.dhdMainDB.DatabaseName)
            If basCode.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If basCode.CurVar.DevMode Then MessageBox.Show(strSQL)
            basCode.QueryDb(basCode.dhdMainDB, strSQL, False, 10)
            prbCreateDatabase.PerformStep()

            For i = 1 To arrScripts.GetUpperBound(0)
                strSQL = MydbRef.GetScript(arrScripts(i))
                If Not strSQL = "-1" Then
                    'strSQL = Replace(strSQL, "Sequenchel", strDBName)
                    strSQL = strSQL.Replace("Sequenchel", basCode.dhdMainDB.DatabaseName)
                    If basCode.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
                    'If basCode.CurVar.DevMode Then MessageBox.Show(strSQL)
                    basCode.QueryDb(basCode.dhdMainDB, strSQL, False, 10)
                Else
                    If basCode.CurVar.DevMode Then MessageBox.Show("The script: " & arrScripts(i) & " returned: " & strSQL)
                End If
            Next
            WriteStatus("Procedures added successfully", 0, lblStatusText)
        Catch ex As Exception
            basCode.WriteLog("An error occured creating the procedures: " & ex.Message, 1)
            WriteStatus("Error creating procedures. Please check the log.", 1, lblStatusText)
            Exit Sub
        End Try
        CursorControl()

    End Sub

    Private Sub txtPassword_GotFocus(sender As Object, e As EventArgs) Handles txtPassword.GotFocus
        RemoveDefaultText(txtPassword)
        PasswordCharSet(txtPassword)
    End Sub

    Private Sub txtPassword_LostFocus(sender As Object, e As EventArgs) Handles txtPassword.LostFocus
        SetDefaultText(txtPassword)
        PasswordCharSet(txtPassword)
    End Sub

    Private Sub btnShowDatabasePassword_MouseDown(sender As Object, e As MouseEventArgs) Handles btnShowDatabasePassword.MouseDown
        txtPassword.PasswordChar = Nothing
    End Sub

    Private Sub btnShowDatabasePassword_MouseLeave(sender As Object, e As EventArgs) Handles btnShowDatabasePassword.MouseLeave
        PasswordCharSet(txtPassword)
    End Sub

    Private Sub btnShowDatabasePassword_MouseUp(sender As Object, e As MouseEventArgs) Handles btnShowDatabasePassword.MouseUp
        PasswordCharSet(txtPassword)
    End Sub

#End Region

#Region "Scheduler"
    Private Sub ProceduresLoad()
        cbxProcedures.Items.Add("Monitor Servers")
        cbxProcedures.Items.Add("Monitor Databases")
        cbxProcedures.Items.Add("Monitor Backups")
        cbxProcedures.Items.Add("Monitor Dataspaces")
        cbxProcedures.Items.Add("Monitor Diskspaces")
        cbxProcedures.Items.Add("Monitor Logins")
        cbxProcedures.Items.Add("Monitor Object Owners")
        cbxProcedures.Items.Add("Monitor Jobs")
        cbxProcedures.Items.Add("Monitor Errorlogs")

        cbxProcedures.Items.Add("Cycle Error logs")
        cbxProcedures.Items.Add("Defragment Indexes")
        cbxProcedures.Items.Add("SmartUpdate")
        txtJobNamePrefix.Text = basCode.dhdMainDB.DatabaseName
    End Sub

    Private Sub HoursLoad()
        For intCount As Integer = 0 To 23
            cbxStartHour.Items.Add(Format(intCount, "00"))
            cbxEndHour.Items.Add(Format(intCount, "00"))
        Next
    End Sub

    Private Sub MinutesLoad()
        For intCount As Integer = 0 To 59
            cbxStartMinute.Items.Add(Format(intCount, "00"))
            cbxEndMinute.Items.Add(Format(intCount, "00"))
        Next
    End Sub

    Private Sub cbxOccurence_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbxOccurence.SelectedIndexChanged
        If cbxOccurence.SelectedItem = "Every" Then
            nudTimeSpan.Visible = True
            cbxTimespan.Visible = True
            lblEndTime.Visible = True
            cbxEndHour.Visible = True
            cbxEndMinute.Visible = True
            lblEndTimeColon.Visible = True
        Else
            nudTimeSpan.Visible = False
            cbxTimespan.Visible = False
            lblEndTime.Visible = False
            cbxEndHour.Visible = False
            cbxEndMinute.Visible = False
            lblEndTimeColon.Visible = False
        End If
    End Sub

    Private Sub btnErrorlogPathDatabase_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathDatabase.Click
        WriteStatus("", 0, lblStatusText)
        txtErrorlogPath.Text = GetDefaultLogPath(basCode.dhdMainDB)
    End Sub

    Private Sub btnErrorlogPathSystem_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathSystem.Click
        WriteStatus("", 0, lblStatusText)
        txtErrorlogPath.Text = Application.StartupPath & "\" & "LOG"
    End Sub

    Private Sub btnErrorlogPathDefault_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathDefault.Click
        WriteStatus("", 0, lblStatusText)
        txtErrorlogPath.Text = basCode.curVar.DefaultConfigFilePath & "\" & "LOG"
    End Sub

    Private Sub btnErrorlogPathBrowse_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathBrowse.Click
        WriteStatus("", 0, lblStatusText)
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = basCode.CurVar.DefaultConfigFilePath

        If (DefaultFolder.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (DefaultFolder.SelectedPath.Length) > 0 Then
            txtLogfileLocation.Text = DefaultFolder.SelectedPath
        End If
    End Sub

    Private Sub btnCreateScheduledJob_Click(sender As Object, e As EventArgs) Handles btnCreateScheduledJob.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If cbxProcedures.SelectedIndex = -1 Then
            CursorControl()
            WriteStatus("Please select a procedure to schedule first", 2, lblStatusText)
            Exit Sub
        End If
        If cbxProcedures.SelectedItem.ToString.Length = 0 Then
            WriteStatus("Please select a procedure to schedule first", 2, lblStatusText)
            Exit Sub
        End If
        ScheduleAdd()
        CursorControl()
    End Sub

    Private Sub ScheduleAdd()
        Dim strJobName As String = ProcedureNameGet(cbxProcedures.SelectedItem)
        Dim strSqlCommand As String = ScheduleCommandBuild(strJobName)
        Dim strSTartTime As String = cbxStartHour.Text & cbxStartMinute.Text & "00"
        Dim strEndTime As String = cbxEndHour.Text & cbxEndMinute.Text & "00"
        Dim strErrorlogPath As String = basCode.dhdText.LogLocation

        Dim FreqInterval As Integer = 0
        Dim FreqType As Integer = 0
        Dim FreqSubType As Integer = 0
        Dim FreqSubTypeInt As Integer = 0

        If chkSunday.Checked = True Then FreqInterval += 1
        If chkMonday.Checked = True Then FreqInterval += 2
        If chkTeusday.Checked = True Then FreqInterval += 4
        If chkWednesday.Checked = True Then FreqInterval += 8
        If chkThursday.Checked = True Then FreqInterval += 16
        If chkFriday.Checked = True Then FreqInterval += 32
        If chkSaturday.Checked = True Then FreqInterval += 64

        Select Case FreqInterval
            Case 0
                FreqType = 1
            Case 127
                FreqType = 4
                FreqInterval = 1
            Case Else
                FreqType = 8
        End Select

        If cbxOccurence.Text = "One Time" Then
            FreqSubType = 1
            If FreqType = 1 And cbxStartHour.Text < Now.Hour Or (cbxStartHour.Text = Now.Hour And cbxStartMinute.Text <= Now.Minute) Then
                MessageBox.Show("The start time must be greater than the current time." & Environment.NewLine & "please select a time later today", "One time execution impossible")
                Exit Sub
            End If
        Else
            FreqSubTypeInt = nudTimeSpan.Value
            If FreqSubTypeInt = 0 Then
                FreqSubType = 1
            Else
                If cbxTimespan.Text = "Minute(s)" Then FreqSubType = 4
                If cbxTimespan.Text = "Hour(s)" Then FreqSubType = 8
            End If
        End If

        If txtErrorlogPath.Text.Length > 0 Then
            strErrorlogPath = txtErrorlogPath.Text
        End If

        If ScheduleCreate(Trim(txtJobNamePrefix.Text & " " & strJobName), strSqlCommand, FreqType, FreqInterval, FreqSubType, FreqSubTypeInt, strSTartTime, strEndTime, strErrorlogPath) = False Then
            WriteStatus("Schedule creation failed.", 1, lblStatusText)
        Else
            WriteStatus("Schedule created.", 0, lblStatusText)
        End If
    End Sub

    Private Function ProcedureNameGet(strSelection As String) As String
        Dim strReturn As String = ""
        Select Case strSelection
            Case "Monitor Servers"
                strReturn = "Report_Servers"
            Case "Monitor Databases"
                strReturn = "Report_Databases"
            Case "Monitor Backups"
                strReturn = "Report_Backups"
            Case "Monitor Dataspaces"
                strReturn = "Report_DataSpaces"
            Case "Monitor Diskspaces"
                strReturn = "Report_DiskSpaces"
            Case "Monitor Logins"
                strReturn = "Report_Logins"
            Case "Monitor Object Owners"
                strReturn = "Report_Object_Owners"
            Case "Monitor Jobs"
                strReturn = "Report_Jobs"
            Case "Monitor Errorlogs"
                strReturn = "Report_Errorlogs"
            Case "Cycle Error logs"
                strReturn = "CycleErrorlogs"
            Case "Defragment Indexes"
                strReturn = "DefragIndexes"
            Case "SmartUpdate"
                strReturn = "SmartUpdate"
            Case Else
                strReturn = ""
        End Select

        Return strReturn
    End Function

    Private Function ScheduleCommandBuild(strProcedure As String) As String
        Dim strCommand As String = "", strsql As String
        Dim strRecipient As String = txtRecipient.Text
        Dim strExceptionList As String = txtExceptionList.Text
        Dim strSeparator As String = txtSeparator.Text
        Dim blnMailStats As Boolean = chkMailStatistics.Checked
        Dim intSqlVersion As Integer = cbxSqlVersion.SelectedItem
        Dim blnIncludeHigherSqlVersions As Integer = chkIncludeHigherSqlVersions.Checked

        'Select Case strProcedure
        '    Case "Report_Servers"
        '    Case "Report_Databases"
        '    Case "Report_Backups"
        '    Case "Report_DataSpaces"
        '    Case "Report_DiskSpaces"
        '    Case "Report_Logins"
        '    Case "Report_Object_Owners"
        '    Case "Report_Jobs"
        '    Case "Report_Errorlogs"
        '    Case "CycleErrorlogs"
        '    Case "DefragIndexes"
        'End Select

        Dim MydbRef As New SDBA.DBRef
        strsql = MydbRef.GetScript("Exec_" & strProcedure & ".sql")
        strsql = strsql.Replace("'", "''")
        strsql = strsql.Replace("<Recipient>", strRecipient)
        strsql = strsql.Replace("<ExceptionList>", strExceptionList)
        strsql = strsql.Replace("<Separator>", strSeparator)
        strsql = strsql.Replace("<MailStats>", Convert.ToInt32(blnMailStats))
        strsql = strsql.Replace("<SqlVersion>", intSqlVersion)
        strsql = strsql.Replace("<IncludeHigherSqlVersions>", Convert.ToInt32(blnIncludeHigherSqlVersions))

        Return strsql
    End Function

#End Region

#Region "MonitorDataspaces"

    Private Sub MonitorDataspacesLoad()
        Dim strValue As String = 0

        strValue = LoadConfigSetting("MonitorDataspaces", "MinPercGrowth")
        If strValue.Length > 0 And IsNumeric(strValue) Then basCode.CurVar.MinPercGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "MinFreeSpace")
        If strValue.Length > 0 Then basCode.CurVar.MinFreeSpace = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "LowerLimit")
        If strValue.Length > 0 Then basCode.CurVar.LowerLimit = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "UpperLimit")
        If strValue.Length > 0 Then basCode.CurVar.UpperLimit = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "SmallGrowth")
        If strValue.Length > 0 Then basCode.CurVar.SmallGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "MediumGrowth")
        If strValue.Length > 0 Then basCode.CurVar.MediumGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "LargeGrowth")
        If strValue.Length > 0 Then basCode.CurVar.LargeGrowth = strValue
    End Sub

    Private Sub MonitorDataspacesShow()
        txtMinPercGrowth.Text = basCode.CurVar.MinPercGrowth
        txtMinFreeSpace.Text = basCode.CurVar.MinFreeSpace
        txtLowerLimit.Text = basCode.CurVar.LowerLimit
        txtUpperLimit.Text = basCode.CurVar.UpperLimit
        txtSmallGrowth.Text = basCode.CurVar.SmallGrowth
        txtMediumGrowth.Text = basCode.CurVar.MediumGrowth
        txtLargeGrowth.Text = basCode.CurVar.LargeGrowth
    End Sub

    Private Sub btnMonitorDataSpacesLoad_Click(sender As Object, e As EventArgs) Handles btnMonitorDataSpacesLoad.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        MonitorDataspacesLoad()
        MonitorDataspacesShow()
        CursorControl()
    End Sub

    Private Sub btnMonitorDataSpacesSave_Click(sender As Object, e As EventArgs) Handles btnMonitorDataSpacesSave.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim intMinPercGrowth As Integer = basCode.curVar.MinPercGrowth
        Dim intMinFreeSpace As Integer = basCode.curVar.MinFreeSpace
        Dim intLowerLimit As Integer = basCode.curVar.LowerLimit
        Dim intUpperlimit As Integer = basCode.curVar.UpperLimit
        Dim intSmallGrowth As Integer = basCode.curVar.SmallGrowth
        Dim intMediumGrowth As Integer = basCode.curVar.MediumGrowth
        Dim intLargeGrowth As Integer = basCode.curVar.LargeGrowth

        If IsNumeric(txtMinPercGrowth.Text) Then intMinPercGrowth = txtMinPercGrowth.Text
        If IsNumeric(txtMinFreeSpace.Text) Then intMinFreeSpace = txtMinFreeSpace.Text
        If IsNumeric(txtLowerLimit.Text) Then intLowerLimit = txtLowerLimit.Text
        If IsNumeric(txtUpperLimit.Text) Then intUpperlimit = txtUpperLimit.Text
        If IsNumeric(txtSmallGrowth.Text) Then intSmallGrowth = txtSmallGrowth.Text
        If IsNumeric(txtMediumGrowth.Text) Then intMediumGrowth = txtMediumGrowth.Text
        If IsNumeric(txtLargeGrowth.Text) Then intLargeGrowth = txtLargeGrowth.Text

        If Not intLowerLimit < intUpperlimit Then
            WriteStatus("The Lower Limit should be smaller than the Upper Limit", 2, lblStatusText)
            CursorControl()
            Exit Sub
        End If
        If Not intSmallGrowth < intMediumGrowth Then
            WriteStatus("Small growth should be smaller than Medium Growth.", 2, lblStatusText)
            CursorControl()
            Exit Sub
        End If
        If Not intMediumGrowth < intLargeGrowth Then
            WriteStatus("Medium Growth should be smaller than Large Growth.", 2, lblStatusText)
            CursorControl()
            Exit Sub
        End If
        If Not intLargeGrowth < intUpperlimit Then
            WriteStatus("large Growth should be smaller than Upper Limit.", 2, lblStatusText)
            CursorControl()
            Exit Sub
        End If
        basCode.curVar.MinPercGrowth = intMinPercGrowth
        basCode.curVar.MinFreeSpace = intMinFreeSpace
        basCode.curVar.LowerLimit = intLowerLimit
        basCode.curVar.UpperLimit = intUpperlimit
        basCode.curVar.SmallGrowth = intSmallGrowth
        basCode.curVar.MediumGrowth = intMediumGrowth
        basCode.curVar.LargeGrowth = intLargeGrowth

        SaveConfigSetting("MonitorDataspaces", "MinPercGrowth", basCode.curVar.MinPercGrowth)
        SaveConfigSetting("MonitorDataspaces", "MinFreeSpace", basCode.curVar.MinFreeSpace)
        SaveConfigSetting("MonitorDataspaces", "LowerLimit", basCode.curVar.LowerLimit)
        SaveConfigSetting("MonitorDataspaces", "UpperLimit", basCode.curVar.UpperLimit)
        SaveConfigSetting("MonitorDataspaces", "SmallGrowth", basCode.curVar.SmallGrowth)
        SaveConfigSetting("MonitorDataspaces", "MediumGrowth", basCode.curVar.MediumGrowth)
        SaveConfigSetting("MonitorDataspaces", "LargeGrowth", basCode.curVar.LargeGrowth)

        CursorControl()
    End Sub

#End Region

#Region "FTP"

    Private Sub txtFtpServer_GotFocus(sender As Object, e As EventArgs) Handles txtFtpServer.GotFocus
        RemoveDefaultText(sender)
    End Sub

    Private Sub txtFtpServer_LostFocus(sender As Object, e As EventArgs) Handles txtFtpServer.LostFocus
        SetDefaultText(sender)
    End Sub

    Private Sub txtUploadSource_GotFocus(sender As Object, e As EventArgs) Handles txtUploadSource.GotFocus
        RemoveDefaultText(sender)
    End Sub

    Private Sub txtUploadSource_LostFocus(sender As Object, e As EventArgs) Handles txtUploadSource.LostFocus
        SetDefaultText(sender)
    End Sub

    Private Sub txtFtpLocation_GotFocus(sender As Object, e As EventArgs) Handles txtFtpLocation.GotFocus
        RemoveDefaultText(sender)
    End Sub

    Private Sub txtFtpLocation_LostFocus(sender As Object, e As EventArgs) Handles txtFtpLocation.LostFocus
        SetDefaultText(sender)
    End Sub

    Private Sub txtDownloadDestination_GotFocus(sender As Object, e As EventArgs) Handles txtDownloadDestination.GotFocus
        RemoveDefaultText(sender)
    End Sub

    Private Sub txtDownloadDestination_LostFocus(sender As Object, e As EventArgs) Handles txtDownloadDestination.LostFocus
        SetDefaultText(sender)
    End Sub

    Private Sub txtTargetFiles_GotFocus(sender As Object, e As EventArgs) Handles txtTargetFiles.GotFocus
        RemoveDefaultText(sender)
    End Sub

    Private Sub txtTargetFiles_LostFocus(sender As Object, e As EventArgs) Handles txtTargetFiles.LostFocus
        SetDefaultText(sender)
    End Sub

    Private Sub SetFtpDefaults()
        SetDefaultText(txtFtpServer)
        SetDefaultText(txtUploadSource)
        SetDefaultText(txtFtpLocation)
        SetDefaultText(txtDownloadDestination)
        SetDefaultText(txtTargetFiles)
        cbxFtpMode.SelectedIndex = 1
    End Sub

    Private Sub btnCreateUploadProcedure_Click(sender As Object, e As EventArgs) Handles btnCreateUploadProcedure.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim MydbRef As New SDBA.DBRef
        Dim strSQL As String

        strSQL = MydbRef.GetScript("01 dbo.usp_PutFTPfiles.sql")
        If Not strSQL = "-1" Then
            If chkEncryptProcedure.Checked = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If txtFtpServer.Text <> txtFtpServer.Tag Then strSQL = strSQL.Replace("<FtpServer>", txtFtpServer.Text)
            If txtFtpUserName.Text.Length > 0 Then strSQL = strSQL.Replace("<FtpUser>", txtFtpUserName.Text)
            If txtFtpPassword.Text.Length > 0 Then strSQL = strSQL.Replace("<FtpPassword>", txtFtpPassword.Text)
            If txtUploadSource.Text <> txtUploadSource.Tag Then strSQL = strSQL.Replace("<SourcePath>", txtUploadSource.Text)
            If txtFtpLocation.Text <> txtFtpLocation.Tag Then strSQL = strSQL.Replace("<DestPath>", txtFtpLocation.Text)
            If txtTargetFiles.Text <> txtTargetFiles.Tag Then strSQL = strSQL.Replace("<SourceFiles>", txtTargetFiles.Text)
            If cbxFtpMode.SelectedIndex <> -1 Then strSQL = strSQL.Replace("<FtpMode>", cbxFtpMode.SelectedItem)
            If chkCmdshell.Checked = True Then
                strSQL = strSQL.Replace("--exec sp_configure", "exec sp_configure")
                strSQL = strSQL.Replace("--reconfigure", "reconfigure")
            End If
            Try
                basCode.QueryDb(basCode.dhdMainDB, strSQL, False, 10)
                WriteStatus("Procedure PutFTPfiles was created succesfully", 0, lblStatusText)
            Catch ex As Exception
                basCode.WriteLog("There was an error creating the FTP procedure: " & ex.Message, 1)
                WriteStatus("Error creating FTP procedure. Please check the log.", 1, lblStatusText)
            End Try
        Else
            basCode.WriteLog("Error reading FTP script file.", 1)
            WriteStatus("Error reading FTP script file. Please contact support.", 1, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnCreateDownloadProcedure_Click(sender As Object, e As EventArgs) Handles btnCreateDownloadProcedure.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Dim MydbRef As New SDBA.DBRef
        Dim strSQL As String

        strSQL = MydbRef.GetScript("01 dbo.usp_GetFTPfiles.sql")
        If Not strSQL = "-1" Then
            If chkEncryptProcedure.Checked = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If txtFtpServer.Text <> txtFtpServer.Tag Then strSQL = strSQL.Replace("<FtpServer>", txtFtpServer.Text)
            If txtFtpUserName.Text.Length > 0 Then strSQL = strSQL.Replace("<FtpUser>", txtFtpUserName.Text)
            If txtFtpPassword.Text.Length > 0 Then strSQL = strSQL.Replace("<FtpPassword>", txtFtpPassword.Text)
            If txtFtpLocation.Text <> txtFtpLocation.Tag Then strSQL = strSQL.Replace("<SourcePath>", txtFtpLocation.Text)
            If txtDownloadDestination.Text <> txtDownloadDestination.Tag Then strSQL = strSQL.Replace("<DestPath>", txtDownloadDestination.Text)
            If txtTargetFiles.Text <> txtTargetFiles.Tag Then strSQL = strSQL.Replace("<SourceFiles>", txtTargetFiles.Text)
            If cbxFtpMode.SelectedIndex <> -1 Then strSQL = strSQL.Replace("<FtpMode>", cbxFtpMode.SelectedItem)
            If chkRemoveFiles.Checked = True Then strSQL = strSQL.Replace("@Remove bit = 0", "@Remove bit = 1")
            If chkCmdshell.Checked = True Then
                strSQL = strSQL.Replace("--exec sp_configure", "exec sp_configure")
                strSQL = strSQL.Replace("--reconfigure", "reconfigure")
            End If
            Try
                basCode.QueryDb(basCode.dhdMainDB, strSQL, False, 10)
                WriteStatus("Procedure PutFTPfiles was created succesfully", 0, lblStatusText)
            Catch ex As Exception
                basCode.WriteLog("There was an error creating the FTP procedure: " & ex.Message, 1)
                WriteStatus("Error creating FTP procedure. Please check the log.", 1, lblStatusText)
            End Try
        Else
            basCode.WriteLog("Error reading FTP script file.", 1)
            WriteStatus("Error reading FTP script file. Please contact support.", 1, lblStatusText)
        End If
        CursorControl()
    End Sub

#End Region

#Region "Email"
    Private Sub SetEmailDefaults()
        SetDefaultText(txtSmtpServerPassword)
    End Sub

    Private Sub EmailSettingsShow()
        txtSmtpServer.Text = basCode.dhdText.SmtpServer
        chkSmtpCredentials.Checked = basCode.dhdText.SmtpCredentials
        txtSmtpServerUsername.Text = basCode.dhdText.SmtpUser
        'DataHandler.txt.EncryptText(txtSmtpServerPassword.Text) = basCode.dhdText.SmtpPassword
        txtSmtpReply.Text = basCode.dhdText.SmtpReply
        txtSmtpPortNumber.Text = basCode.dhdText.SmtpPort
        chkUseSslEncryption.Checked = basCode.dhdText.SmtpSsl
    End Sub

    Private Sub btnSettingsEmailSave_Click(sender As Object, e As EventArgs) Handles btnSettingsEmailSave.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        basCode.dhdText.SmtpServer = txtSmtpServer.Text
        basCode.dhdText.SmtpCredentials = chkSmtpCredentials.Checked
        basCode.dhdText.SmtpUser = txtSmtpServerUsername.Text
        If txtSmtpServerPassword.Text.Length > 0 And txtSmtpServerPassword.Text <> txtSmtpServerPassword.Tag Then basCode.dhdText.SmtpPassword = DataHandler.txt.EncryptText(txtSmtpServerPassword.Text)
        basCode.dhdText.SmtpReply = txtSmtpReply.Text
        basCode.dhdText.SmtpPort = txtSmtpPortNumber.Text
        basCode.dhdText.SmtpSsl = chkUseSslEncryption.Checked
        If basCode.SaveGeneralSettingsXml(xmlGeneralSettings) = False Then
            WriteStatus("Error saving Settings file. Please check the log.", 1, lblStatusText)
        Else
            WriteStatus("Settings file saved.", 0, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub chkSmtpCredentials_CheckedChanged(sender As Object, e As EventArgs) Handles chkSmtpCredentials.CheckedChanged
        If chkSmtpCredentials.Checked = True Then
            txtSmtpServerUsername.Enabled = False
            txtSmtpServerUsername.Text = ""
            txtSmtpServerPassword.Enabled = False
            txtSmtpServerPassword.Text = ""
        Else
            txtSmtpServerUsername.Enabled = True
            txtSmtpServerUsername.Text = basCode.dhdText.SmtpUser
            txtSmtpServerPassword.Enabled = True
            'txtSmtpServerPassword.Text = basCode.dhdText.SmtpPassword
            btnShowEmailPassword.Enabled = True
        End If
    End Sub

    Private Sub txtSmtpServerPassword_GotFocus(sender As Object, e As EventArgs) Handles txtSmtpServerPassword.GotFocus
        RemoveDefaultText(sender)
        PasswordCharSet(txtSmtpServerPassword)
    End Sub

    Private Sub txtSmtpServerPassword_LostFocus(sender As Object, e As EventArgs) Handles txtSmtpServerPassword.LostFocus
        SetDefaultText(sender)
        PasswordCharSet(txtSmtpServerPassword)
    End Sub

    Private Sub txtSmtpServerPassword_TextChanged(sender As Object, e As EventArgs) Handles txtSmtpServerPassword.TextChanged
        PasswordCharSet(txtSmtpServerPassword)
    End Sub

    Private Sub btnShowPassword_Click(sender As Object, e As EventArgs) Handles btnShowEmailPassword.MouseDown
        txtSmtpServerPassword.PasswordChar = Nothing
    End Sub

    Private Sub btnShowPassword_MouseLeave(sender As Object, e As EventArgs) Handles btnShowEmailPassword.MouseLeave
        PasswordCharSet(txtSmtpServerPassword)
    End Sub

    Private Sub btnShowPassword_MouseUp(sender As Object, e As MouseEventArgs) Handles btnShowEmailPassword.MouseUp
        PasswordCharSet(txtSmtpServerPassword)
    End Sub

#End Region

End Class