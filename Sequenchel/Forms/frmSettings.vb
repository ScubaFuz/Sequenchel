Public Class frmSettings

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        txtLicenseName.Text = Core.LicenseName
        DatabaseShow()
        SeqData.dhdMainDB.CheckDB()
        If SeqData.dhdMainDB.DataBaseOnline = True Then
            If VersionLoad() = True Then
                tabSettings.SelectTab(tpgDatabase)
                btnUpgradeDatabase.BackColor = clrMarked
            End If
            MonitorDataspacesLoad()
        End If
        If SeqData.dhdMainDB.DataBaseOnline = False Then
            tabSettings.SelectTab(tpgDatabase)
        End If
        LogSettingsShow()
        GeneralSettingsShow()
        ProceduresLoad()
        HoursLoad()
        MinutesLoad()
        DateFormatsLoad()
        txtErrorlogPath.Text = SeqData.dhdText.LogLocation
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
        txtDefaultConfigFilePath.Tag = SeqData.curVar.DefaultConfigFilePath
        txtDefaultConfigFilePath.Text = SeqData.curVar.DefaultConfigFilePath
        txtSettingsFile.Tag = SeqData.curVar.GeneralSettings
        txtSettingsFile.Text = SeqData.curVar.GeneralSettings
        txtConnectionsFile.Tag = SeqData.curVar.ConnectionsFile
        txtConnectionsFile.Text = SeqData.curVar.ConnectionsFile

        chkAllowSettingsChange.Tag = SeqData.curVar.AllowSettingsChange
        chkAllowSettingsChange.Checked = SeqData.curVar.AllowSettingsChange
        chkAllowConfiguration.Tag = SeqData.curVar.AllowConfiguration
        chkAllowConfiguration.Checked = SeqData.curVar.AllowConfiguration
        chkAllowLinkedServers.Tag = SeqData.curVar.AllowLinkedServers
        chkAllowLinkedServers.Checked = SeqData.curVar.AllowLinkedServers

        chkAllowQueryEdit.Tag = SeqData.curVar.AllowQueryEdit
        chkAllowQueryEdit.Checked = SeqData.curVar.AllowQueryEdit
        chkAllowDataImport.Tag = SeqData.curVar.AllowDataImport
        chkAllowDataImport.Checked = SeqData.curVar.AllowDataImport
        chkAllowSmartUpdate.Tag = SeqData.curVar.AllowSmartUpdate
        chkAllowSmartUpdate.Checked = SeqData.curVar.AllowSmartUpdate

        chkAllowUpdate.Tag = SeqData.curVar.AllowUpdate
        chkAllowUpdate.Checked = SeqData.curVar.AllowUpdate
        chkAllowInsert.Tag = SeqData.curVar.AllowInsert
        chkAllowInsert.Checked = SeqData.curVar.AllowInsert
        chkAllowDelete.Tag = SeqData.curVar.AllowDelete
        chkAllowDelete.Checked = SeqData.curVar.AllowDelete

        txtTimerHours.Tag = SeqData.curVar.TimedShutdown / 60 / 60 / 1000
        txtTimerHours.Text = SeqData.curVar.TimedShutdown / 60 / 60 / 1000

        chkLimitLookupLists.Tag = SeqData.curVar.LimitLookupLists
        chkLimitLookupLists.Checked = SeqData.curVar.LimitLookupLists
        txtLimitLookupLists.Tag = SeqData.curVar.LimitLookupListsCount
        txtLimitLookupLists.Text = SeqData.curVar.LimitLookupListsCount

        chkIncludeDateInExportFiles.Tag = SeqData.curVar.IncludeDate
        chkIncludeDateInExportFiles.Checked = SeqData.curVar.IncludeDate

        SetDefaultText(txtOverridePassword)
        PasswordCharSet(txtOverridePassword)
    End Sub

    Private Sub btnDefaultConfigFilePath_Click(sender As Object, e As EventArgs) Handles btnDefaultConfigFilePath.Click
        txtDefaultConfigFilePath.Text = Application.StartupPath & "\Config"
    End Sub

    Private Sub btnSettingsFileSystem_Click(sender As Object, e As EventArgs) Handles btnSettingsFileSystem.Click
        txtSettingsFile.Text = Application.StartupPath & "\" & SeqData.curVar.MainSettingsFile
    End Sub

    Private Sub btnSettingsFileDefault_Click(sender As Object, e As EventArgs) Handles btnSettingsFileDefault.Click
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtSettingsFile.Text = txtDefaultConfigFilePath.Text & "\" & SeqData.curVar.MainSettingsFile
        Else
            txtSettingsFile.Text = SeqData.curVar.MainSettingsFile
        End If
    End Sub

    Private Sub btnConnectionsFileSystem_Click(sender As Object, e As EventArgs) Handles btnConnectionsFileSystem.Click
        txtConnectionsFile.Text = SeqData.curVar.ConnectionsFile
    End Sub

    Private Sub btnConnectionsFileDefault_Click(sender As Object, e As EventArgs) Handles btnConnectionsFileDefault.Click
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtConnectionsFile.Text = txtDefaultConfigFilePath.Text & "\" & SeqData.curVar.ConnectionsFileName
        Else
            txtConnectionsFile.Text = SeqData.curVar.ConnectionsFileName
        End If
    End Sub

    Private Sub txtDefaultConfigFilePath_TextChanged(sender As Object, e As EventArgs) Handles txtDefaultConfigFilePath.TextChanged
        SetBackColor(txtDefaultConfigFilePath)
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtSettingsFile.Text = txtDefaultConfigFilePath.Text & "\" & SeqData.curVar.GeneralSettings.Substring(SeqData.curVar.GeneralSettings.LastIndexOf("\") + 1, SeqData.curVar.GeneralSettings.Length - (SeqData.curVar.GeneralSettings.LastIndexOf("\") + 1))
            txtConnectionsFile.Text = txtDefaultConfigFilePath.Text & "\" & SeqData.curVar.ConnectionsFile.Substring(SeqData.curVar.ConnectionsFile.LastIndexOf("\") + 1, SeqData.curVar.ConnectionsFile.Length - (SeqData.curVar.ConnectionsFile.LastIndexOf("\") + 1))
            txtErrorlogPath.Text = txtDefaultConfigFilePath.Text
        Else
            txtSettingsFile.Text = SeqData.curVar.GeneralSettings.Substring(SeqData.curVar.GeneralSettings.LastIndexOf("\") + 1, SeqData.curVar.GeneralSettings.Length - (SeqData.curVar.GeneralSettings.LastIndexOf("\") + 1))
            txtConnectionsFile.Text = SeqData.curVar.ConnectionsFile.Substring(SeqData.curVar.ConnectionsFile.LastIndexOf("\") + 1, SeqData.curVar.ConnectionsFile.Length - (SeqData.curVar.ConnectionsFile.LastIndexOf("\") + 1))
            txtErrorlogPath.Text = SeqData.dhdText.LogLocation
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
        Dim blnSettingsChanged As Boolean = False
        Dim blnGeneralSettingsChanged As Boolean = False

        If txtDefaultConfigFilePath.BackColor = clrMarked Then
            SeqData.curVar.DefaultConfigFilePath = txtDefaultConfigFilePath.Text
            txtDefaultConfigFilePath.Tag = SeqData.curVar.DefaultConfigFilePath
            txtDefaultConfigFilePath.BackColor = clrOriginal
            SeqData.dhdText.CheckDir(SeqData.curVar.DefaultConfigFilePath, True)
            blnSettingsChanged = True
        End If
        If txtSettingsFile.BackColor = clrMarked Then
            SeqData.curVar.GeneralSettings = txtSettingsFile.Text
            txtSettingsFile.Tag = SeqData.curVar.GeneralSettings
            txtSettingsFile.BackColor = clrOriginal
            'dhdText.CheckDir(SeqData.CurVar.GeneralSettings, True)
            blnSettingsChanged = True
        End If
        If chkAllowSettingsChange.BackColor = clrMarked Then
            SeqData.curVar.AllowSettingsChange = chkAllowSettingsChange.Checked
            chkAllowSettingsChange.Tag = SeqData.curVar.AllowSettingsChange
            chkAllowSettingsChange.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowConfiguration.BackColor = clrMarked Then
            SeqData.curVar.AllowConfiguration = chkAllowConfiguration.Checked
            chkAllowConfiguration.Tag = SeqData.curVar.AllowConfiguration
            chkAllowConfiguration.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowLinkedServers.BackColor = clrMarked Then
            SeqData.curVar.AllowLinkedServers = chkAllowLinkedServers.Checked
            chkAllowLinkedServers.Tag = SeqData.curVar.AllowLinkedServers
            chkAllowLinkedServers.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowQueryEdit.BackColor = clrMarked Then
            SeqData.curVar.AllowQueryEdit = chkAllowQueryEdit.Checked
            chkAllowQueryEdit.Tag = SeqData.curVar.AllowQueryEdit
            chkAllowQueryEdit.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowDataImport.BackColor = clrMarked Then
            SeqData.curVar.AllowDataImport = chkAllowDataImport.Checked
            chkAllowDataImport.Tag = SeqData.curVar.AllowDataImport
            chkAllowDataImport.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowSmartUpdate.BackColor = clrMarked Then
            SeqData.curVar.AllowSmartUpdate = chkAllowSmartUpdate.Checked
            chkAllowSmartUpdate.Tag = SeqData.curVar.AllowSmartUpdate
            chkAllowSmartUpdate.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowUpdate.BackColor = clrMarked Then
            SeqData.curVar.AllowUpdate = chkAllowUpdate.Checked
            chkAllowUpdate.Tag = SeqData.curVar.AllowUpdate
            chkAllowUpdate.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowInsert.BackColor = clrMarked Then
            SeqData.curVar.AllowInsert = chkAllowInsert.Checked
            chkAllowInsert.Tag = SeqData.curVar.AllowInsert
            chkAllowInsert.BackColor = clrControl
            blnSettingsChanged = True
        End If
        If chkAllowDelete.BackColor = clrMarked Then
            SeqData.curVar.AllowDelete = chkAllowDelete.Checked
            chkAllowDelete.Tag = SeqData.curVar.AllowDelete
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
            SeqData.curVar.TimedShutdown = txtTimerHours.Text * 60 * 60 * 1000
            txtTimerHours.Tag = SeqData.curVar.TimedShutdown / 60 / 60 / 1000
            txtTimerHours.BackColor = clrOriginal
            blnSettingsChanged = True
        End If

        If txtOverridePassword.Text.Length > 0 And txtOverridePassword.Text <> txtOverridePassword.Tag Then
            SeqData.curVar.OverridePassword = SeqData.dhdText.MD5Encrypt(txtOverridePassword.Text)
            blnSettingsChanged = True
        End If

        If chkLimitLookupLists.BackColor = clrMarked Then
            SeqData.curVar.LimitLookupLists = chkLimitLookupLists.Checked
            chkLimitLookupLists.Tag = SeqData.curVar.LimitLookupLists
            chkLimitLookupLists.BackColor = clrControl
            blnGeneralSettingsChanged = True
        End If
        If txtLimitLookupLists.BackColor = clrMarked Then
            SeqData.curVar.LimitLookupListsCount = txtLimitLookupLists.Text
            txtLimitLookupLists.Tag = SeqData.curVar.LimitLookupListsCount
            txtLimitLookupLists.BackColor = clrOriginal
            blnSettingsChanged = True
        End If

        If txtConnectionsFile.BackColor = clrMarked Then
            SeqData.curVar.ConnectionsFile = txtConnectionsFile.Text
            txtConnectionsFile.Tag = SeqData.curVar.ConnectionsFile
            blnGeneralSettingsChanged = True
            txtConnectionsFile.BackColor = clrOriginal
            'dhdText.CheckDir(SeqData.CurVar.ConnectionsFile, True)
        End If
        If cbxDateFormats.BackColor = clrMarked Then
            SeqData.curVar.DateTimeStyle = cbxDateFormats.SelectedValue.ToString
            blnGeneralSettingsChanged = True
            cbxDateFormats.BackColor = clrOriginal
        End If
        If chkIncludeDateInExportFiles.BackColor = clrMarked Then
            SeqData.curVar.IncludeDate = chkIncludeDateInExportFiles.Checked
            chkIncludeDateInExportFiles.Tag = SeqData.curVar.IncludeDate
            chkIncludeDateInExportFiles.BackColor = clrOriginal
            blnGeneralSettingsChanged = True
        End If

        If blnSettingsChanged = True Then
            SeqData.SaveSDBASettingsXml(xmlSDBASettings)
            blnSettingsChanged = False
        End If
        If blnGeneralSettingsChanged = True Then
            SeqData.SaveGeneralSettingsXml(xmlGeneralSettings)
            blnGeneralSettingsChanged = False
        End If
        CursorControl()
    End Sub

    Private Sub btnConfigFilePath_Click(sender As Object, e As EventArgs) Handles btnConfigFilePath.Click
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = Application.StartupPath

        If (DefaultFolder.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (DefaultFolder.SelectedPath.Length) > 0 Then
            txtDefaultConfigFilePath.Text = DefaultFolder.SelectedPath
        End If
    End Sub

    Private Sub btnSettingsFile_Click(sender As Object, e As EventArgs) Handles btnSettingsFile.Click
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

        cbxDateFormats.SelectedValue = SeqData.curVar.DateTimeStyle
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
        If cbxDateFormats.SelectedValue.ToString = SeqData.curVar.DateTimeStyle Then
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
        Dim strLocation As String = ""
        If Core.CheckLicense(txtLicenseKey.Text, txtLicenseName.Text, Core.GetVersion("M"), Nothing) = False Then
            'MessageBox.Show(strMessages.strLicenseError)
            MessageBox.Show("Your license validated: " & Core.LicenseValidated)
        End If
        If Core.LicenseValidated = True Then
            Try
                Core.dhdReg.AddLMRegKey("LicenseName", txtLicenseName.Text)
                If Core.dhdReg.ErrorLevel = -1 Then
                    Core.dhdReg.AddCURRegKey("LicenseName", txtLicenseName.Text)
                    strLocation = "HK Current User"
                Else
                    strLocation = "HKLM"
                End If
                If SeqData.curVar.DebugMode And Core.dhdReg.ErrorLevel = -1 Then MessageBox.Show(Core.dhdReg.RegMessage)
                Core.dhdReg.AddLMRegKey("LicenseKey", txtLicenseKey.Text)
                If Core.dhdReg.ErrorLevel = -1 Then
                    Core.dhdReg.AddCURRegKey("LicenseKey", txtLicenseKey.Text)
                    strLocation = "HK Current User"
                Else
                    strLocation = "HKLM"
                End If
                If SeqData.curVar.DebugMode And Core.dhdReg.ErrorLevel = -1 Then MessageBox.Show(Core.dhdReg.RegMessage)
                MessageBox.Show("Your License information has been saved to " & strLocation)
            Catch ex As Exception
                MessageBox.Show("There ws an errror saving you license information" & Environment.NewLine & ex.Message)
            End Try
        End If
        CursorControl()
    End Sub

    Private Sub btnValidateLicense_Click(sender As Object, e As EventArgs) Handles btnValidateLicense.Click
        CursorControl("Wait")
        Core.CheckLicense(txtLicenseKey.Text, txtLicenseName.Text, Core.GetVersion("M"), Nothing)
        MessageBox.Show("Your license validated: " & Core.LicenseValidated)
        CursorControl()
    End Sub

#End Region

#Region "Logging"

    Private Sub LogSettingsShow()
        Select Case SeqData.dhdText.LogLevel
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
        txtLogfileName.Text = SeqData.dhdText.LogFileName
        txtLogfileLocation.Text = SeqData.dhdText.LogLocation
        'If TxtHandle.LogLocation.ToLower = "database" Then grpLogsToKeep.Visible = True

        chkAutoDeleteOldLogs.Checked = SeqData.dhdText.AutoDelete
        Select Case SeqData.dhdText.Retenion
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
        SeqData.dhdText.LogFileName = txtLogfileName.Text
        SeqData.dhdText.LogLevel = 1
        SeqData.dhdText.LogLocation = txtLogfileLocation.Text
        SeqData.dhdText.CheckDir(SeqData.dhdText.LogLocation, True)

        If rbtLoggingLevel0.Checked Then
            SeqData.dhdText.LogLevel = 0
        ElseIf rbtLoggingLevel1.Checked Then
            SeqData.dhdText.LogLevel = 1
        ElseIf rbtLoggingLevel2.Checked Then
            SeqData.dhdText.LogLevel = 2
        ElseIf rbtLoggingLevel3.Checked Then
            SeqData.dhdText.LogLevel = 3
        ElseIf rbtLoggingLevel4.Checked Then
            SeqData.dhdText.LogLevel = 4
        ElseIf rbtLoggingLevel5.Checked Then
            SeqData.dhdText.LogLevel = 5
        End If

        If chkAutoDeleteOldLogs.Checked = True Then
            SeqData.dhdText.AutoDelete = True
            If rbtKeepLogDay.Checked = True Then SeqData.dhdText.Retenion = "Day"
            If rbtKeepLogWeek.Checked = True Then SeqData.dhdText.Retenion = "Week"
            If rbtKeepLogMonth.Checked = True Then SeqData.dhdText.Retenion = "Month"
        Else
            SeqData.dhdText.AutoDelete = False
        End If
        SeqData.SaveGeneralSettingsXml(xmlGeneralSettings)
        CursorControl()
    End Sub

    Private Sub btnClearOldLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearOldLogs.Click
        CursorControl("Wait")
        If SeqData.dhdText.LogLocation.ToLower = "database" Then
            Dim dtmDate As Date = Today

            If rbtKeepLogDay.Checked = True Then dtmDate = dtmDate.AddDays(-1)
            If rbtKeepLogWeek.Checked = True Then dtmDate = dtmDate.AddDays(-7)
            If rbtKeepLogMonth.Checked = True Then dtmDate = dtmDate.AddMonths(-1)
            If SeqData.curVar.DebugMode = True Then MessageBox.Show(dtmDate.ToString)
            If SeqData.dhdText.LogLocation.ToLower = "database" Then
                ClearDBLog(dtmDate)
            Else
                MessageBox.Show("You can only delete database logging as of yet.")
            End If
        End If
        CursorControl()
    End Sub

    Private Sub btnLogfileNameDefault_Click(sender As Object, e As EventArgs) Handles btnLogfileNameDefault.Click
        txtLogfileName.Text = "Sequenchel.log"
    End Sub

    Private Sub btnLogLocationDatabase_Click(sender As Object, e As EventArgs) Handles btnLogLocationDatabase.Click
        txtLogfileLocation.Text = "Database"
    End Sub

    Private Sub btnLogLocationSystem_Click(sender As Object, e As EventArgs) Handles btnLogLocationSystem.Click
        txtLogfileLocation.Text = Application.StartupPath & "\LOG"
    End Sub

    Private Sub btnLogLocationDefault_Click(sender As Object, e As EventArgs) Handles btnLogLocationDefault.Click
        txtLogfileLocation.Text = SeqData.CurVar.DefaultConfigFilePath & "\LOG"
    End Sub

    Private Sub btnLogLocationBrowse_Click(sender As Object, e As EventArgs) Handles btnLogLocationBrowse.Click
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = SeqData.CurVar.DefaultConfigFilePath

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
        CreateDatabase(True)
        CursorControl()
    End Sub

    Private Sub btnRefreshDatabase_Click(sender As Object, e As EventArgs) Handles btnRefreshDatabase.Click
        If MessageBox.Show("This will update all standard Sequenchel Views, Stored Procedures and Functions to their latest version. " & Environment.NewLine _
            & "Your Tables will not be changed." & Environment.NewLine _
            & Core.Message.strAreYouSure, Core.Message.strWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
        CursorControl("Wait")
        CreateDatabase(False)
        CursorControl()
    End Sub

    Friend Sub CreateDatabase(Optional ByVal blnIncludeTables As Boolean = True)
        Dim strDBName As String
        Dim strSQL As String
        Dim intLogLevel As Integer = 6
        If SeqData.CurVar.Encryption = False Then intLogLevel = 5

        btnCreateExtraProcs.Visible = False
        lblStatusDatabase.Visible = True
        prbCreateDatabase.Visible = True

        If blnIncludeTables = True Then
            SeqData.dhdMainDB.DataProvider = cbxDataProvider.SelectedItem
            SeqData.dhdMainDB.DataLocation = txtDatabaseLocation.Text
            SeqData.dhdMainDB.LoginMethod = cbxLoginMethod.SelectedItem
            SeqData.dhdMainDB.LoginName = txtLoginName.Text
            If txtPassword.Text.Length > 0 And txtPassword.Text <> txtPassword.Tag Then SeqData.dhdMainDB.Password = txtPassword.Text
            SeqData.dhdMainDB.DatabaseName = "master"
            strDBName = txtDatabaseName.Text
            If SeqData.CurVar.DebugMode Then
                MessageBox.Show("Sequenchel v " & Application.ProductVersion & " Database Creation" & Environment.NewLine _
                 & "   DatabaseServer = " & SeqData.dhdMainDB.DataLocation & Environment.NewLine _
                 & "   Database Context = " & SeqData.dhdMainDB.DatabaseName & Environment.NewLine _
                 & "   Database to create: = " & strDBName & Environment.NewLine _
                 & "   DataProvider = " & SeqData.dhdMainDB.DataProvider & Environment.NewLine _
                 & "   LoginMethod = " & SeqData.dhdMainDB.LoginMethod & Environment.NewLine _
                 & "   Running in Debug Mode")
            End If
        Else
            strDBName = SeqData.dhdMainDB.DatabaseName
        End If

        Try
            SeqData.dhdMainDB.CheckDB()
            If SeqData.dhdMainDB.DataBaseOnline = False Then
                MessageBox.Show("The server was not found. Please check your settings")
                SeqData.dhdMainDB.DatabaseName = strDBName
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error connecting to the server. please check your settings")
            SeqData.dhdMainDB.DatabaseName = strDBName
            Exit Sub
        End Try

        Try
            Dim MydbRef As New SDBA.DBRef
            Dim arrScripts(100) As String
            arrScripts = MydbRef.GetList(blnIncludeTables)
            prbCreateDatabase.Maximum = arrScripts.GetUpperBound(0)

            strSQL = MydbRef.GetScript(arrScripts(0))
            strSQL = strSQL.Replace("Sequenchel", strDBName)
            If SeqData.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If SeqData.CurVar.DevMode Then MessageBox.Show(strSQL)
            SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False, 10)
            SeqData.dhdMainDB.DatabaseName = strDBName
            txtJobNamePrefix.Text = SeqData.dhdMainDB.DatabaseName
            prbCreateDatabase.PerformStep()

            For i = 1 To arrScripts.GetUpperBound(0)
                strSQL = MydbRef.GetScript(arrScripts(i))
                If Not strSQL = "-1" Then
                    'strSQL = Replace(strSQL, "Sequenchel", strDBName)
                    strSQL = strSQL.Replace("Sequenchel", strDBName)
                    If SeqData.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
                    If blnIncludeTables = False Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
                    If SeqData.CurVar.DevMode Then MessageBox.Show(strSQL)
                    SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False, 5)
                Else
                    If SeqData.CurVar.DebugMode Then MessageBox.Show("The script: " & arrScripts(i) & " returned: " & strSQL)
                End If
                prbCreateDatabase.PerformStep()
            Next
            prbCreateDatabase.PerformStep()

            SaveConfigSetting("Database", "Version", My.Application.Info.Version.ToString)
            'btnCreateDemoData.Visible = True

        Catch ex As Exception
            SeqData.dhdMainDB.DatabaseName = strDBName
            txtJobNamePrefix.Text = SeqData.dhdMainDB.DatabaseName
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
            MessageBox.Show(Core.Message.strUpdateDatabase)
            Return True
        Else
            btnUpgradeDatabase.Enabled = False
            Return False
        End If

    End Function

    Private Sub btnDatabaseDefaultsUse_Click(sender As Object, e As EventArgs) Handles btnDatabaseDefaultsUse.Click
        txtDatabaseLocation.Text = Environment.MachineName & "\SQLEXPRESS"
        txtDatabaseName.Text = "Sequenchel"
        cbxDataProvider.SelectedItem = "SQL"
        cbxLoginMethod.SelectedItem = "WINDOWS"
        txtLoginName.Text = ""
        SetDefaultText(txtPassword)
    End Sub

    Private Sub btnSaveSettingsDatabase_Click(sender As Object, e As EventArgs) Handles btnSaveSettingsDatabase.Click
        CursorControl("Wait")
        If cbxDataProvider.Items.Contains(cbxDataProvider.Text) = False Or cbxLoginMethod.Items.Contains(cbxLoginMethod.Text) = False Then
            MessageBox.Show(Core.Message.strPreconfigured & Environment.NewLine & Core.Message.strCheckSettings)
            Exit Sub
        End If
        'If SeqData.dhdMainDB.DataLocation <> txtDatabaseLocation.Text Or _
        ' SeqData.dhdMainDB.DatabaseName <> txtDatabaseName.Text Or _
        ' SeqData.dhdMainDB.DataProvider <> cbxDataProvider.SelectedItem Then
        '    If MessageBox.Show(strMessages.strSettingReload & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'End If
        SeqData.dhdMainDB.DataLocation = txtDatabaseLocation.Text
        SeqData.dhdMainDB.DatabaseName = txtDatabaseName.Text
        SeqData.dhdMainDB.DataProvider = cbxDataProvider.SelectedItem
        SeqData.dhdMainDB.LoginMethod = cbxLoginMethod.SelectedItem
        SeqData.dhdMainDB.LoginName = txtLoginName.Text
        If txtPassword.Text.Length > 0 And txtPassword.Text <> txtPassword.Tag Then SeqData.dhdMainDB.Password = txtPassword.Text
        DatabaseTest(SeqData.dhdMainDB)
        Try
            If SeqData.dhdMainDB.DataBaseOnline = False Then
                If MessageBox.Show("The database specified was not found." & Environment.NewLine & "Do you wish to save anyway?", "Database not found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            SeqData.SaveGeneralSettingsXml(xmlGeneralSettings)
            txtJobNamePrefix.Text = SeqData.dhdMainDB.DatabaseName
            'btnSaveSettingsDatabase.BackColor = clrOriginal
        Catch ex As Exception
            MessageBox.Show(Core.Message.strDataError & Environment.NewLine & Core.Message.strCheckSettings)
            SeqData.LoadGeneralSettingsXml(xmlGeneralSettings)
            DatabaseShow()
        End Try
        CursorControl()
    End Sub

    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        SeqData.dhdMainDB.DataProvider = cbxDataProvider.SelectedItem
        SeqData.dhdMainDB.DataLocation = txtDatabaseLocation.Text
        SeqData.dhdMainDB.LoginMethod = cbxLoginMethod.SelectedItem
        SeqData.dhdMainDB.LoginName = txtLoginName.Text
        SeqData.dhdMainDB.Password = txtPassword.Text
        SeqData.dhdMainDB.DatabaseName = txtDatabaseName.Text

        DatabaseTest(SeqData.dhdMainDB)
        MessageBox.Show("Database Connection tested: " & SeqData.dhdMainDB.DataBaseOnline)
    End Sub

    Private Sub DatabaseShow()
        SetDefaultText(txtPassword)
        PasswordCharSet(txtPassword)
        cbxDataProvider.SelectedItem = SeqData.dhdMainDB.DataProvider
        txtDatabaseLocation.Text = SeqData.dhdMainDB.DataLocation
        txtDatabaseName.Text = SeqData.dhdMainDB.DatabaseName
        cbxLoginMethod.SelectedItem = SeqData.dhdMainDB.LoginMethod
        txtLoginName.Text = SeqData.dhdMainDB.LoginName
        'txtPassword.Text = SeqData.dhdMainDB.Password
        cbxLoginMethod_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub cbxLoginMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxLoginMethod.SelectedIndexChanged
        If cbxLoginMethod.SelectedItem = "SQL" Then
            txtLoginName.Enabled = True
            txtPassword.Enabled = True
            txtLoginName.Text = SeqData.dhdMainDB.LoginName
            'txtPassword.Text = SeqData.dhdMainDB.Password
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
        Dim strSQL As String
        Dim strMode As String
        Dim MydbRef As New SDBA.DBRef

        btnUpgradeDatabase.Enabled = False

        Try
            Dim arrScripts(100, 1) As String
            arrScripts = MydbRef.GetNewList(txtUpgradeDatabase.Tag)

            strSQL = MydbRef.GetScript(arrScripts(0, 0))
            If SeqData.CurVar.DebugMode Then MessageBox.Show("<debug>Number of Scripts: " & arrScripts.GetUpperBound(1) + 1)

            strMode = arrScripts(1, 0)
            If strSQL = "-1" Then
                MessageBox.Show("Error retrieving SQL Script " & arrScripts(0, 0) & ", please contact your vendor")
                Exit Sub
            End If
            strSQL = Replace(strSQL, "Sequenchel", SeqData.dhdMainDB.DatabaseName)
            If SeqData.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If strMode = "ALTER" Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
            SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False)

            For i = 1 To arrScripts.GetUpperBound(1)
                strSQL = MydbRef.GetScript(arrScripts(0, i))
                If Not strSQL = "-1" Then
                    strSQL = Replace(strSQL, "Sequenchel", SeqData.dhdMainDB.DatabaseName)
                    strMode = arrScripts(1, i)
                    If strMode = "ALTER" Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
                    SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False)
                Else
                    MessageBox.Show("Error retrieving SQL Script " & arrScripts(0, 0) & ", please contact your vendor")
                    Exit Sub
                End If
            Next
            SaveConfigSetting("Database", "Version", txtUpgradeDatabase.Tag)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        btnUpgradeDatabase.BackColor = clrOriginal
        txtUpgradeDatabase.Text = txtUpgradeDatabase.Tag
        txtUpgradeDatabase.Tag = ""
        CursorControl()
        MessageBox.Show("Database Update successfull")
        VersionLoad()
    End Sub

    Private Sub btnBackupDatabase_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase.Click
        CursorControl("Wait")
        Try
            BackupDatabase(SeqData.dhdMainDB, txtBackupDatabase.Text)
            SaveConfigSetting("Database", "BackupLocation", txtBackupDatabase.Text, "A valid location on the server")
        Catch ex As Exception
            CursorControl()
            MessageBox.Show("While saving the database, the following error occured: " & Environment.NewLine & ex.Message)
        End Try
        CursorControl()
    End Sub

    Private Sub btnCreateExtraProcs_Click(sender As Object, e As EventArgs) Handles btnCreateExtraProcs.Click
        If MessageBox.Show("This will create extra procedures with the power to damage your data or database. Create these procedures in a secure database." & Environment.NewLine & Core.Message.strContinue, Core.Message.strWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
        CursorControl("Wait")

        Dim strSQL As String
        Dim MydbRef As New SDBA.DBRef

        Try
            Dim arrScripts(100) As String
            arrScripts = MydbRef.GetDBAList()

            strSQL = MydbRef.GetScript(arrScripts(0))
            strSQL = strSQL.Replace("Sequenchel", SeqData.dhdMainDB.DatabaseName)
            If SeqData.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If SeqData.CurVar.DevMode Then MessageBox.Show(strSQL)
            SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False, 10)
            prbCreateDatabase.PerformStep()

            For i = 1 To arrScripts.GetUpperBound(0)
                strSQL = MydbRef.GetScript(arrScripts(i))
                If Not strSQL = "-1" Then
                    'strSQL = Replace(strSQL, "Sequenchel", strDBName)
                    strSQL = strSQL.Replace("Sequenchel", SeqData.dhdMainDB.DatabaseName)
                    If SeqData.CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
                    'If SeqData.CurVar.DevMode Then MessageBox.Show(strSQL)
                    SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False, 10)
                Else
                    If SeqData.CurVar.DevMode Then MessageBox.Show("The script: " & arrScripts(i) & " returned: " & strSQL)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        CursorControl()
        MessageBox.Show("Procedures added successfully")

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
        txtJobNamePrefix.Text = SeqData.dhdMainDB.DatabaseName
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
        txtErrorlogPath.Text = GetDefaultLogPath(SeqData.dhdMainDB)
    End Sub

    Private Sub btnErrorlogPathSystem_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathSystem.Click
        txtErrorlogPath.Text = Application.StartupPath & "\" & "LOG"
    End Sub

    Private Sub btnErrorlogPathDefault_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathDefault.Click
        txtErrorlogPath.Text = SeqData.CurVar.DefaultConfigFilePath & "\" & "LOG"
    End Sub

    Private Sub btnErrorlogPathBrowse_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathBrowse.Click
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = SeqData.CurVar.DefaultConfigFilePath

        If (DefaultFolder.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (DefaultFolder.SelectedPath.Length) > 0 Then
            txtLogfileLocation.Text = DefaultFolder.SelectedPath
        End If
    End Sub

    Private Sub btnCreateScheduledJob_Click(sender As Object, e As EventArgs) Handles btnCreateScheduledJob.Click
        CursorControl("Wait")
        If cbxProcedures.SelectedIndex = -1 Then
            CursorControl()
            MessageBox.Show("Please select a procedure to schedule first")
            Exit Sub
        End If
        If cbxProcedures.SelectedItem.ToString.Length = 0 Then
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
        Dim strErrorlogPath As String = SeqData.dhdText.LogLocation

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

        ScheduleCreate(Trim(txtJobNamePrefix.Text & " " & strJobName), strSqlCommand, FreqType, FreqInterval, FreqSubType, FreqSubTypeInt, strSTartTime, strEndTime, strErrorlogPath)
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
        If strValue.Length > 0 And IsNumeric(strValue) Then SeqData.CurVar.MinPercGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "MinFreeSpace")
        If strValue.Length > 0 Then SeqData.CurVar.MinFreeSpace = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "LowerLimit")
        If strValue.Length > 0 Then SeqData.CurVar.LowerLimit = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "UpperLimit")
        If strValue.Length > 0 Then SeqData.CurVar.UpperLimit = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "SmallGrowth")
        If strValue.Length > 0 Then SeqData.CurVar.SmallGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "MediumGrowth")
        If strValue.Length > 0 Then SeqData.CurVar.MediumGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "LargeGrowth")
        If strValue.Length > 0 Then SeqData.CurVar.LargeGrowth = strValue
    End Sub

    Private Sub MonitorDataspacesShow()
        txtMinPercGrowth.Text = SeqData.CurVar.MinPercGrowth
        txtMinFreeSpace.Text = SeqData.CurVar.MinFreeSpace
        txtLowerLimit.Text = SeqData.CurVar.LowerLimit
        txtUpperLimit.Text = SeqData.CurVar.UpperLimit
        txtSmallGrowth.Text = SeqData.CurVar.SmallGrowth
        txtMediumGrowth.Text = SeqData.CurVar.MediumGrowth
        txtLargeGrowth.Text = SeqData.CurVar.LargeGrowth
    End Sub

    Private Sub btnMonitorDataSpacesLoad_Click(sender As Object, e As EventArgs) Handles btnMonitorDataSpacesLoad.Click
        CursorControl("Wait")
        MonitorDataspacesLoad()
        MonitorDataspacesShow()
        CursorControl()
    End Sub

    Private Sub btnMonitorDataSpacesSave_Click(sender As Object, e As EventArgs) Handles btnMonitorDataSpacesSave.Click
        CursorControl("Wait")
        Dim intMinPercGrowth As Integer = SeqData.curVar.MinPercGrowth
        Dim intMinFreeSpace As Integer = SeqData.curVar.MinFreeSpace
        Dim intLowerLimit As Integer = SeqData.curVar.LowerLimit
        Dim intUpperlimit As Integer = SeqData.curVar.UpperLimit
        Dim intSmallGrowth As Integer = SeqData.curVar.SmallGrowth
        Dim intMediumGrowth As Integer = SeqData.curVar.MediumGrowth
        Dim intLargeGrowth As Integer = SeqData.curVar.LargeGrowth

        If IsNumeric(txtMinPercGrowth.Text) Then intMinPercGrowth = txtMinPercGrowth.Text
        If IsNumeric(txtMinFreeSpace.Text) Then intMinFreeSpace = txtMinFreeSpace.Text
        If IsNumeric(txtLowerLimit.Text) Then intLowerLimit = txtLowerLimit.Text
        If IsNumeric(txtUpperLimit.Text) Then intUpperlimit = txtUpperLimit.Text
        If IsNumeric(txtSmallGrowth.Text) Then intSmallGrowth = txtSmallGrowth.Text
        If IsNumeric(txtMediumGrowth.Text) Then intMediumGrowth = txtMediumGrowth.Text
        If IsNumeric(txtLargeGrowth.Text) Then intLargeGrowth = txtLargeGrowth.Text

        If Not intLowerLimit < intUpperlimit Then
            MessageBox.Show("The Lower Limit should be smaller than the Upper Limit")
            CursorControl()
            Exit Sub
        End If
        If Not (intSmallGrowth < intMediumGrowth And intMediumGrowth < intLargeGrowth And intLargeGrowth < intUpperlimit) Then
            MessageBox.Show("Small growth should be smaller than Medium Growth." & Environment.NewLine & "Medium Growth should be smaller than Large Growth" & Environment.NewLine & "Large Growth should be smaller than Upper Growth")
            CursorControl()
            Exit Sub
        End If
        SeqData.curVar.MinPercGrowth = intMinPercGrowth
        SeqData.curVar.MinFreeSpace = intMinFreeSpace
        SeqData.curVar.LowerLimit = intLowerLimit
        SeqData.curVar.UpperLimit = intUpperlimit
        SeqData.curVar.SmallGrowth = intSmallGrowth
        SeqData.curVar.MediumGrowth = intMediumGrowth
        SeqData.curVar.LargeGrowth = intLargeGrowth

        SaveConfigSetting("MonitorDataspaces", "MinPercGrowth", SeqData.curVar.MinPercGrowth)
        SaveConfigSetting("MonitorDataspaces", "MinFreeSpace", SeqData.curVar.MinFreeSpace)
        SaveConfigSetting("MonitorDataspaces", "LowerLimit", SeqData.curVar.LowerLimit)
        SaveConfigSetting("MonitorDataspaces", "UpperLimit", SeqData.curVar.UpperLimit)
        SaveConfigSetting("MonitorDataspaces", "SmallGrowth", SeqData.curVar.SmallGrowth)
        SaveConfigSetting("MonitorDataspaces", "MediumGrowth", SeqData.curVar.MediumGrowth)
        SaveConfigSetting("MonitorDataspaces", "LargeGrowth", SeqData.curVar.LargeGrowth)

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
                SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False, 10)
                lblFtpStatus.Text = "Procedure PutFTPfiles was created succesfully"
            Catch ex As Exception
                MessageBox.Show("There was an error creating the procedure" & Environment.NewLine & ex.Message)
            End Try
        Else
            If SeqData.CurVar.DebugMode Then MessageBox.Show("The script: 01 dbo.usp_PutFTPfiles.sql returned: " & strSQL)
        End If
        CursorControl()
    End Sub

    Private Sub btnCreateDownloadProcedure_Click(sender As Object, e As EventArgs) Handles btnCreateDownloadProcedure.Click
        CursorControl("Wait")
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
                SeqData.QueryDb(SeqData.dhdMainDB, strSQL, False, 10)
                lblFtpStatus.Text = "Procedure GetFTPfiles was created succesfully"
            Catch ex As Exception
                MessageBox.Show("There was an error creating the procedure" & Environment.NewLine & ex.Message)
            End Try
        Else
            If SeqData.CurVar.DebugMode Then MessageBox.Show("The script: 01 dbo.usp_GetFTPfiles.sql returned: " & strSQL)
        End If
        CursorControl()
    End Sub

#End Region

#Region "Email"
    Private Sub SetEmailDefaults()
        SetDefaultText(txtSmtpServerPassword)
    End Sub

    Private Sub EmailSettingsShow()
        txtSmtpServer.Text = SeqData.dhdText.SmtpServer
        chkSmtpCredentials.Checked = SeqData.dhdText.SmtpCredentials
        txtSmtpServerUsername.Text = SeqData.dhdText.SmtpUser
        'DataHandler.txt.EncryptText(txtSmtpServerPassword.Text) = SeqData.dhdText.SmtpPassword
        txtSmtpReply.Text = SeqData.dhdText.SmtpReply
        txtSmtpPortNumber.Text = SeqData.dhdText.SmtpPort
        chkUseSslEncryption.Checked = SeqData.dhdText.SmtpSsl
    End Sub

    Private Sub btnSettingsEmailSave_Click(sender As Object, e As EventArgs) Handles btnSettingsEmailSave.Click
        CursorControl("Wait")
        SeqData.dhdText.SmtpServer = txtSmtpServer.Text
        SeqData.dhdText.SmtpCredentials = chkSmtpCredentials.Checked
        SeqData.dhdText.SmtpUser = txtSmtpServerUsername.Text
        If txtSmtpServerPassword.Text.Length > 0 And txtSmtpServerPassword.Text <> txtSmtpServerPassword.Tag Then SeqData.dhdText.SmtpPassword = DataHandler.txt.EncryptText(txtSmtpServerPassword.Text)
        SeqData.dhdText.SmtpReply = txtSmtpReply.Text
        SeqData.dhdText.SmtpPort = txtSmtpPortNumber.Text
        SeqData.dhdText.SmtpSsl = chkUseSslEncryption.Checked
        SeqData.SaveGeneralSettingsXml(xmlGeneralSettings)
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
            txtSmtpServerUsername.Text = SeqData.dhdText.SmtpUser
            txtSmtpServerPassword.Enabled = True
            'txtSmtpServerPassword.Text = SeqData.dhdText.SmtpPassword
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