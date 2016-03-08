Public Class frmSettings

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.CurrentCulture = New System.Globalization.CultureInfo("EN-US")
        txtLicenseName.Text = strLicenseName
        DatabaseShow()
        dhdDatabase.CheckDB()
        blnDatabaseOnLine = dhdDatabase.DataBaseOnline
        If blnDatabaseOnLine = True Then
            If VersionLoad() = True Then
                tabSettings.SelectTab(tpgDatabase)
                btnUpgradeDatabase.BackColor = clrMarked
            End If
            MonitorDataspacesLoad()
        End If
        If blnDatabaseOnLine = False Then
            tabSettings.SelectTab(tpgDatabase)
        End If
        LogSettingsShow()
        GeneralSettingsShow()
        ProceduresLoad()
        HoursLoad()
        MinutesLoad()
        DateFormatsLoad()
        txtErrorlogPath.Text = dhdText.LogLocation
        ResetColors()
        SetFtpDefaults()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

#Region "General"

    Private Sub GeneralSettingsShow()
        txtDefaultConfigFilePath.Tag = CurVar.DefaultConfigFilePath
        txtDefaultConfigFilePath.Text = CurVar.DefaultConfigFilePath
        txtSettingsFile.Tag = CurVar.GeneralSettings
        txtSettingsFile.Text = CurVar.GeneralSettings
        txtConnectionsFile.Tag = CurVar.ConnectionsFile
        txtConnectionsFile.Text = CurVar.ConnectionsFile
        chkAllowQueryEdit.Tag = CurVar.AllowQueryEdit
        chkAllowQueryEdit.Checked = CurVar.AllowQueryEdit
        chkAllowConfiguration.Tag = CurVar.AllowConfiguration
        chkAllowConfiguration.Checked = CurVar.AllowConfiguration
        chkAllowUpdate.Tag = CurVar.AllowUpdate
        chkAllowUpdate.Checked = CurVar.AllowUpdate
        chkAllowInsert.Tag = CurVar.AllowInsert
        chkAllowInsert.Checked = CurVar.AllowInsert
        chkAllowDelete.Tag = CurVar.AllowDelete
        chkAllowDelete.Checked = CurVar.AllowDelete
        chkAllowLinkedServers.Tag = CurVar.AllowLinkedServers
        chkAllowLinkedServers.Checked = CurVar.AllowLinkedServers
        chkAllowDataImport.Tag = CurVar.AllowDataImport
        chkAllowDataImport.Checked = CurVar.AllowDataImport
        chkAllowSettingsChange.Tag = CurVar.AllowSettingsChange
        chkAllowSettingsChange.Checked = CurVar.AllowSettingsChange

        chkLimitLookupLists.Tag = CurVar.LimitLookupLists
        chkLimitLookupLists.Checked = CurVar.LimitLookupLists
        txtLimitLookupLists.Tag = CurVar.LimitLookupListsCount
        txtLimitLookupLists.Text = CurVar.LimitLookupListsCount

        chkIncludeDateInExportFiles.Tag = CurVar.IncludeDate
        chkIncludeDateInExportFiles.Checked = CurVar.IncludeDate

    End Sub

    Private Sub btnDefaultConfigFilePath_Click(sender As Object, e As EventArgs) Handles btnDefaultConfigFilePath.Click
        txtDefaultConfigFilePath.Text = Application.StartupPath & "\Config"
    End Sub

    Private Sub btnSettingsFileSystem_Click(sender As Object, e As EventArgs) Handles btnSettingsFileSystem.Click
        txtSettingsFile.Text = Application.StartupPath & "\" & CurVar.MainSettingsFile
    End Sub

    Private Sub btnSettingsFileDefault_Click(sender As Object, e As EventArgs) Handles btnSettingsFileDefault.Click
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtSettingsFile.Text = txtDefaultConfigFilePath.Text & "\" & CurVar.MainSettingsFile
        Else
            txtSettingsFile.Text = CurVar.MainSettingsFile
        End If
    End Sub

    Private Sub btnConnectionsFileSystem_Click(sender As Object, e As EventArgs) Handles btnConnectionsFileSystem.Click
        txtConnectionsFile.Text = CurVar.ConnectionsFile
    End Sub

    Private Sub btnConnectionsFileDefault_Click(sender As Object, e As EventArgs) Handles btnConnectionsFileDefault.Click
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtConnectionsFile.Text = txtDefaultConfigFilePath.Text & "\" & CurVar.ConnectionsFileName
        Else
            txtConnectionsFile.Text = CurVar.ConnectionsFileName
        End If
    End Sub

    Private Sub txtDefaultConfigFilePath_TextChanged(sender As Object, e As EventArgs) Handles txtDefaultConfigFilePath.TextChanged
        If txtDefaultConfigFilePath.Text = txtDefaultConfigFilePath.Tag Then
            txtDefaultConfigFilePath.BackColor = clrOriginal
        Else
            txtDefaultConfigFilePath.BackColor = clrMarked
        End If
        If txtDefaultConfigFilePath.Text.Length > 0 Then
            txtSettingsFile.Text = txtDefaultConfigFilePath.Text & "\" & CurVar.GeneralSettings.Substring(CurVar.GeneralSettings.LastIndexOf("\") + 1, CurVar.GeneralSettings.Length - (CurVar.GeneralSettings.LastIndexOf("\") + 1))
            txtConnectionsFile.Text = txtDefaultConfigFilePath.Text & "\" & CurVar.ConnectionsFile.Substring(CurVar.ConnectionsFile.LastIndexOf("\") + 1, CurVar.ConnectionsFile.Length - (CurVar.ConnectionsFile.LastIndexOf("\") + 1))
            txtErrorlogPath.Text = txtDefaultConfigFilePath.Text
        Else
            txtSettingsFile.Text = CurVar.GeneralSettings.Substring(CurVar.GeneralSettings.LastIndexOf("\") + 1, CurVar.GeneralSettings.Length - (CurVar.GeneralSettings.LastIndexOf("\") + 1))
            txtConnectionsFile.Text = CurVar.ConnectionsFile.Substring(CurVar.ConnectionsFile.LastIndexOf("\") + 1, CurVar.ConnectionsFile.Length - (CurVar.ConnectionsFile.LastIndexOf("\") + 1))
            txtErrorlogPath.Text = dhdText.LogLocation
        End If

    End Sub

    Private Sub txtSettingsFile_TextChanged(sender As Object, e As EventArgs) Handles txtSettingsFile.TextChanged
        If txtSettingsFile.Text = txtSettingsFile.Tag Then
            txtSettingsFile.BackColor = clrOriginal
        Else
            txtSettingsFile.BackColor = clrMarked
        End If
    End Sub

    Private Sub txtConnectionsFile_TextChanged(sender As Object, e As EventArgs) Handles txtConnectionsFile.TextChanged
        If txtConnectionsFile.Text = txtConnectionsFile.Tag Then
            txtConnectionsFile.BackColor = clrOriginal
        Else
            txtConnectionsFile.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowQueryEdit_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowQueryEdit.CheckedChanged
        If chkAllowQueryEdit.Checked = chkAllowQueryEdit.Tag Then
            chkAllowQueryEdit.BackColor = clrControl
        Else
            chkAllowQueryEdit.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowConfiguration_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowConfiguration.CheckedChanged
        If chkAllowConfiguration.Checked = chkAllowConfiguration.Tag Then
            chkAllowConfiguration.BackColor = clrControl
        Else
            chkAllowConfiguration.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowUpdate_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowUpdate.CheckedChanged
        If chkAllowUpdate.Checked = chkAllowUpdate.Tag Then
            chkAllowUpdate.BackColor = clrControl
        Else
            chkAllowUpdate.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowInsert_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowInsert.CheckedChanged
        If chkAllowInsert.Checked = chkAllowInsert.Tag Then
            chkAllowInsert.BackColor = clrControl
        Else
            chkAllowInsert.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowDelete_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowDelete.CheckedChanged
        If chkAllowDelete.Checked = chkAllowDelete.Tag Then
            chkAllowDelete.BackColor = clrControl
        Else
            chkAllowDelete.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowLinkedServers_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowLinkedServers.CheckedChanged
        If chkAllowLinkedServers.Checked = chkAllowLinkedServers.Tag Then
            chkAllowLinkedServers.BackColor = clrControl
        Else
            chkAllowLinkedServers.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowDataImport_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowDataImport.CheckedChanged
        If chkAllowDataImport.Checked = chkAllowDataImport.Tag Then
            chkAllowDataImport.BackColor = clrControl
        Else
            chkAllowDataImport.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkAllowSettingsChange_CheckedChanged(sender As Object, e As EventArgs) Handles chkAllowSettingsChange.CheckedChanged
        If chkAllowSettingsChange.Checked = chkAllowSettingsChange.Tag Then
            chkAllowSettingsChange.BackColor = clrControl
        Else
            chkAllowSettingsChange.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkLimitLookupLists_CheckedChanged(sender As Object, e As EventArgs) Handles chkLimitLookupLists.CheckedChanged
        If chkLimitLookupLists.Checked = chkLimitLookupLists.Tag Then
            chkLimitLookupLists.BackColor = clrControl
        Else
            chkLimitLookupLists.BackColor = clrMarked
        End If
    End Sub

    Private Sub txtLimitLookupLists_TextChanged(sender As Object, e As EventArgs) Handles txtLimitLookupLists.TextChanged
        If txtLimitLookupLists.Text = txtLimitLookupLists.Tag Then
            txtLimitLookupLists.BackColor = clrOriginal
        Else
            txtLimitLookupLists.BackColor = clrMarked
        End If
    End Sub

    Private Sub chkIncludeDateInExportFiles_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncludeDateInExportFiles.CheckedChanged
        If chkIncludeDateInExportFiles.Checked = chkIncludeDateInExportFiles.Tag Then
            chkIncludeDateInExportFiles.BackColor = clrControl
        Else
            chkIncludeDateInExportFiles.BackColor = clrMarked
        End If
    End Sub


    Private Sub btnSettingsGeneralSave_Click(sender As Object, e As EventArgs) Handles btnSettingsGeneralSave.Click
        Dim blnSettingsChanged As Boolean = False
        Dim blnGeneralSettingsChanged As Boolean = False

        If txtDefaultConfigFilePath.BackColor = clrMarked Then
            CurVar.DefaultConfigFilePath = txtDefaultConfigFilePath.Text
            txtDefaultConfigFilePath.Tag = CurVar.DefaultConfigFilePath
            txtDefaultConfigFilePath.BackColor = clrOriginal
            dhdText.CheckDir(CurVar.DefaultConfigFilePath, True)
            blnSettingsChanged = True
        End If
        If txtSettingsFile.BackColor = clrMarked Then
            CurVar.GeneralSettings = txtSettingsFile.Text
            txtSettingsFile.Tag = CurVar.GeneralSettings
            txtSettingsFile.BackColor = clrOriginal
            'dhdText.CheckDir(CurVar.GeneralSettings, True)
            blnSettingsChanged = True
        End If
        If chkAllowQueryEdit.BackColor = clrMarked Then
            CurVar.AllowQueryEdit = chkAllowQueryEdit.Checked
            chkAllowQueryEdit.Tag = CurVar.AllowQueryEdit
            chkAllowQueryEdit.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If chkAllowConfiguration.BackColor = clrMarked Then
            CurVar.AllowConfiguration = chkAllowConfiguration.Checked
            chkAllowConfiguration.Tag = CurVar.AllowConfiguration
            chkAllowConfiguration.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If chkAllowUpdate.BackColor = clrMarked Then
            CurVar.AllowUpdate = chkAllowUpdate.Checked
            chkAllowUpdate.Tag = CurVar.AllowUpdate
            chkAllowUpdate.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If chkAllowInsert.BackColor = clrMarked Then
            CurVar.AllowInsert = chkAllowInsert.Checked
            chkAllowInsert.Tag = CurVar.AllowInsert
            chkAllowInsert.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If chkAllowDelete.BackColor = clrMarked Then
            CurVar.AllowDelete = chkAllowDelete.Checked
            chkAllowDelete.Tag = CurVar.AllowDelete
            chkAllowDelete.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If chkAllowLinkedServers.BackColor = clrMarked Then
            CurVar.AllowLinkedServers = chkAllowLinkedServers.Checked
            chkAllowLinkedServers.Tag = CurVar.AllowLinkedServers
            chkAllowLinkedServers.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If chkAllowDataImport.BackColor = clrMarked Then
            CurVar.AllowDataImport = chkAllowDataImport.Checked
            chkAllowDataImport.Tag = CurVar.AllowDataImport
            chkAllowDataImport.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If chkAllowSettingsChange.BackColor = clrMarked Then
            CurVar.AllowSettingsChange = chkAllowSettingsChange.Checked
            chkAllowSettingsChange.Tag = CurVar.AllowSettingsChange
            chkAllowSettingsChange.BackColor = clrOriginal
            blnSettingsChanged = True
        End If
        If txtOverridePassword.Text.Length > 0 Then
            CurVar.OverridePassword = Encrypt(txtOverridePassword.Text)
            blnSettingsChanged = True
        End If

        If chkLimitLookupLists.BackColor = clrMarked Then
            CurVar.LimitLookupLists = chkLimitLookupLists.Checked
            chkLimitLookupLists.Tag = CurVar.LimitLookupLists
            chkLimitLookupLists.BackColor = clrOriginal
            blnGeneralSettingsChanged = True
        End If
        If txtLimitLookupLists.BackColor = clrMarked Then
            CurVar.LimitLookupListsCount = txtLimitLookupLists.Text
            txtLimitLookupLists.Tag = CurVar.LimitLookupListsCount
            txtLimitLookupLists.BackColor = clrOriginal
            blnSettingsChanged = True
        End If

        If txtConnectionsFile.BackColor = clrMarked Then
            CurVar.ConnectionsFile = txtConnectionsFile.Text
            txtConnectionsFile.Tag = CurVar.ConnectionsFile
            blnGeneralSettingsChanged = True
            txtConnectionsFile.BackColor = clrOriginal
            'dhdText.CheckDir(CurVar.ConnectionsFile, True)
        End If
        If cbxDateFormats.BackColor = clrMarked Then
            CurVar.DateTimeStyle = cbxDateFormats.SelectedValue.ToString
            blnGeneralSettingsChanged = True
            cbxDateFormats.BackColor = clrOriginal
        End If
        If chkIncludeDateInExportFiles.BackColor = clrMarked Then
            CurVar.IncludeDate = chkIncludeDateInExportFiles.Checked
            chkIncludeDateInExportFiles.Tag = CurVar.IncludeDate
            chkIncludeDateInExportFiles.BackColor = clrOriginal
            blnGeneralSettingsChanged = True
        End If

        If blnSettingsChanged = True Then
            SaveSDBASettingsXml()
            blnSettingsChanged = False
        End If
        If blnGeneralSettingsChanged = True Then
            SaveGeneralSettingsXml()
            blnGeneralSettingsChanged = False
        End If
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

        cbxDateFormats.SelectedValue = CurVar.DateTimeStyle
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
        If cbxDateFormats.SelectedValue.ToString = CurVar.DateTimeStyle Then
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

#End Region

#Region "License"

    Private Sub btnSaveLicense_Click(sender As Object, e As EventArgs) Handles btnSaveLicense.Click
        Dim strLocation As String = ""
        If CheckLicenseKey(txtLicenseKey.Text, txtLicenseName.Text, GetVersion("M"), Nothing) = False Then
            'MessageBox.Show(strMessages.strLicenseError)
            blnLicenseValidated = False
            MessageBox.Show("Your license validated: " & blnLicenseValidated)
        Else
            blnLicenseValidated = True
            'If DebugMode Then MessageBox.Show(lanStrings.strLicenseValidated)
        End If
        If blnLicenseValidated = True Then
            Try
                dhdReg.AddLMRegKey("LicenseName", txtLicenseName.Text)
                If dhdReg.ErrorLevel = -1 Then
                    dhdReg.AddCURRegKey("LicenseName", txtLicenseName.Text)
                    strLocation = "HK Current User"
                Else
                    strLocation = "HKLM"
                End If
                If DebugMode And dhdReg.ErrorLevel = -1 Then MessageBox.Show(dhdReg.RegMessage)
                dhdReg.AddLMRegKey("LicenseKey", txtLicenseKey.Text)
                If dhdReg.ErrorLevel = -1 Then
                    dhdReg.AddCURRegKey("LicenseKey", txtLicenseKey.Text)
                    strLocation = "HK Current User"
                Else
                    strLocation = "HKLM"
                End If
                If DebugMode And dhdReg.ErrorLevel = -1 Then MessageBox.Show(dhdReg.RegMessage)
                MessageBox.Show("Your License information has been saved to " & strLocation)
            Catch ex As Exception
                MessageBox.Show("There ws an errror saving you license information" & Environment.NewLine & ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnValidateLicense_Click(sender As Object, e As EventArgs) Handles btnValidateLicense.Click
        If CheckLicenseKey(txtLicenseKey.Text, txtLicenseName.Text, GetVersion("M"), Nothing) = False Then
            'MessageBox.Show(strMessages.strLicenseError)
            blnLicenseValidated = False
        Else
            blnLicenseValidated = True
            'If DebugMode Then MessageBox.Show(lanStrings.strLicenseValidated)
        End If
        MessageBox.Show("Your license validated: " & blnLicenseValidated)
    End Sub

#End Region

#Region "Logging"

    Private Sub LogSettingsShow()
        Select Case dhdText.LogLevel
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
        txtLogfileName.Text = dhdText.LogFileName
        txtLogfileLocation.Text = dhdText.LogLocation
        'If TxtHandle.LogLocation.ToLower = "database" Then grpLogsToKeep.Visible = True

        chkAutoDeleteOldLogs.Checked = dhdText.AutoDelete
        Select Case dhdText.Retenion
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
        dhdText.LogFileName = txtLogfileName.Text
        dhdText.LogLevel = 1
        dhdText.LogLocation = txtLogfileLocation.Text
        dhdText.CheckDir(dhdText.LogLocation, True)

        If rbtLoggingLevel0.Checked Then
            dhdText.LogLevel = 0
        ElseIf rbtLoggingLevel1.Checked Then
            dhdText.LogLevel = 1
        ElseIf rbtLoggingLevel2.Checked Then
            dhdText.LogLevel = 2
        ElseIf rbtLoggingLevel3.Checked Then
            dhdText.LogLevel = 3
        ElseIf rbtLoggingLevel4.Checked Then
            dhdText.LogLevel = 4
        ElseIf rbtLoggingLevel5.Checked Then
            dhdText.LogLevel = 5
        End If

        If chkAutoDeleteOldLogs.Checked = True Then
            dhdText.AutoDelete = True
            If rbtKeepLogDay.Checked = True Then dhdText.Retenion = "Day"
            If rbtKeepLogWeek.Checked = True Then dhdText.Retenion = "Week"
            If rbtKeepLogMonth.Checked = True Then dhdText.Retenion = "Month"
        Else
            dhdText.AutoDelete = False
        End If
        SaveGeneralSettingsXml()
    End Sub

    Private Sub btnClearOldLogs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearOldLogs.Click
        If dhdText.LogLocation.ToLower = "database" Then
            Dim dtmDate As Date = Today

            If rbtKeepLogDay.Checked = True Then dtmDate = dtmDate.AddDays(-1)
            If rbtKeepLogWeek.Checked = True Then dtmDate = dtmDate.AddDays(-7)
            If rbtKeepLogMonth.Checked = True Then dtmDate = dtmDate.AddMonths(-1)
            If DebugMode = True Then MessageBox.Show(dtmDate.ToString)
            If dhdText.LogLocation.ToLower = "database" Then
                ClearDBLog(dtmDate)
            Else
                MessageBox.Show("You can only delete database logging as of yet.")
            End If
        End If
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
        txtLogfileLocation.Text = CurVar.DefaultConfigFilePath & "\LOG"
    End Sub

    Private Sub btnLogLocationBrowse_Click(sender As Object, e As EventArgs) Handles btnLogLocationBrowse.Click
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = CurVar.DefaultConfigFilePath

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
        CreateDatabase(True)
    End Sub

    Private Sub btnRefreshDatabase_Click(sender As Object, e As EventArgs) Handles btnRefreshDatabase.Click
        If MessageBox.Show("This will update all standard Sequenchel Views, Stored Procedures and Functions to their latest version. " & Environment.NewLine _
            & "Your Tables will not be changed." & Environment.NewLine _
            & strMessages.strAreYouSure, strMessages.strWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub
        CreateDatabase(False)
    End Sub

    Friend Sub CreateDatabase(Optional ByVal blnIncludeTables As Boolean = True)
        Dim strDBName As String
        Dim strSQL As String
        Dim intLogLevel As Integer = 6
        If CurVar.Encryption = False Then intLogLevel = 5

        btnCreateExtraProcs.Visible = False
        lblStatusDatabase.Visible = True
        prbCreateDatabase.Visible = True

        If blnIncludeTables = True Then
            dhdDatabase.DataProvider = cbxDataProvider.SelectedItem
            dhdDatabase.DataLocation = txtDatabaseLocation.Text
            dhdDatabase.LoginMethod = cbxLoginMethod.SelectedItem
            dhdDatabase.LoginName = txtLoginName.Text
            dhdDatabase.Password = txtPassword.Text
            dhdDatabase.DatabaseName = "master"
            strDBName = txtDatabaseName.Text
            If DebugMode Then
                MessageBox.Show("Sequenchel v " & Application.ProductVersion & " Database Creation" & Environment.NewLine _
                 & "   DatabaseServer = " & dhdDatabase.DataLocation & Environment.NewLine _
                 & "   Database Context = " & dhdDatabase.DatabaseName & Environment.NewLine _
                 & "   Database to create: = " & strDBName & Environment.NewLine _
                 & "   DataProvider = " & dhdDatabase.DataProvider & Environment.NewLine _
                 & "   LoginMethod = " & dhdDatabase.LoginMethod & Environment.NewLine _
                 & "   Running in Debug Mode")
            End If
        Else
            strDBName = dhdDatabase.DatabaseName
        End If

        Try
            dhdDatabase.CheckDB()
            If dhdDatabase.DataBaseOnline = False Then
                MessageBox.Show("The server was not found. Please check your settings")
                dhdDatabase.DatabaseName = strDBName
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error connecting to the server. please check your settings")
            dhdDatabase.DatabaseName = strDBName
            Exit Sub
        End Try

        Try
            Dim MydbRef As New SDBA.DBRef
            Dim arrScripts(100) As String
            arrScripts = MydbRef.GetList(blnIncludeTables)
            prbCreateDatabase.Maximum = arrScripts.GetUpperBound(0)

            strSQL = MydbRef.GetScript(arrScripts(0))
            strSQL = strSQL.Replace("Sequenchel", strDBName)
            If CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If DevMode Then MessageBox.Show(strSQL)
            QueryDb(dhdDatabase, strSQL, False, 10)
            dhdDatabase.DatabaseName = strDBName
            txtJobNamePrefix.Text = dhdDatabase.DatabaseName
            prbCreateDatabase.PerformStep()

            For i = 1 To arrScripts.GetUpperBound(0)
                strSQL = MydbRef.GetScript(arrScripts(i))
                If Not strSQL = "-1" Then
                    'strSQL = Replace(strSQL, "Sequenchel", strDBName)
                    strSQL = strSQL.Replace("Sequenchel", strDBName)
                    If CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
                    If blnIncludeTables = False Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
                    If DevMode Then MessageBox.Show(strSQL)
                    QueryDb(dhdDatabase, strSQL, False, 5)
                Else
                    If DebugMode Then MessageBox.Show("The script: " & arrScripts(i) & " returned: " & strSQL)
                End If
                prbCreateDatabase.PerformStep()
            Next
            prbCreateDatabase.PerformStep()

            SaveConfigSetting("Database", "Version", My.Application.Info.Version.ToString)
            'btnCreateDemoData.Visible = True

        Catch ex As Exception
            dhdDatabase.DatabaseName = strDBName
            txtJobNamePrefix.Text = dhdDatabase.DatabaseName
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
            MessageBox.Show(strMessages.strUpdateDatabase)
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
        txtPassword.Text = ""
    End Sub

    Private Sub btnSaveSettingsDatabase_Click(sender As Object, e As EventArgs) Handles btnSaveSettingsDatabase.Click
        If cbxDataProvider.Items.Contains(cbxDataProvider.Text) = False Or cbxLoginMethod.Items.Contains(cbxLoginMethod.Text) = False Then
            MessageBox.Show(strMessages.strPreconfigured & Environment.NewLine & strMessages.strCheckSettings)
            Exit Sub
        End If
        'If dhdDatabase.DataLocation <> txtDatabaseLocation.Text Or _
        ' dhdDatabase.DatabaseName <> txtDatabaseName.Text Or _
        ' dhdDatabase.DataProvider <> cbxDataProvider.SelectedItem Then
        '    If MessageBox.Show(strMessages.strSettingReload & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Cancel Then Exit Sub
        'End If
        dhdDatabase.DataLocation = txtDatabaseLocation.Text
        dhdDatabase.DatabaseName = txtDatabaseName.Text
        dhdDatabase.DataProvider = cbxDataProvider.SelectedItem
        dhdDatabase.LoginMethod = cbxLoginMethod.SelectedItem
        dhdDatabase.LoginName = txtLoginName.Text
        dhdDatabase.Password = txtPassword.Text
        DatabaseTest(dhdDatabase)
        Try
            If blnDatabaseOnLine = False Then
                If MessageBox.Show("The database specified was not found." & Environment.NewLine & "Do you wish to save anyway?", "Database not found", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            SaveGeneralSettingsXml()
            txtJobNamePrefix.Text = dhdDatabase.DatabaseName
            'btnSaveSettingsDatabase.BackColor = clrOriginal
        Catch ex As Exception
            MessageBox.Show(strMessages.strDataError & Environment.NewLine & strMessages.strCheckSettings)
            LoadGeneralSettingsXml()
            DatabaseShow()
        End Try

    End Sub

    Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
        dhdDatabase.DataProvider = cbxDataProvider.SelectedItem
        dhdDatabase.DataLocation = txtDatabaseLocation.Text
        dhdDatabase.LoginMethod = cbxLoginMethod.SelectedItem
        dhdDatabase.LoginName = txtLoginName.Text
        dhdDatabase.Password = txtPassword.Text
        dhdDatabase.DatabaseName = txtDatabaseName.Text

        DatabaseTest(dhdDatabase)
        MessageBox.Show("Database Connection tested: " & blnDatabaseOnLine)
    End Sub

    Private Sub DatabaseShow()
        cbxDataProvider.SelectedItem = dhdDatabase.DataProvider
        txtDatabaseLocation.Text = dhdDatabase.DataLocation
        txtDatabaseName.Text = dhdDatabase.DatabaseName
        cbxLoginMethod.SelectedItem = dhdDatabase.LoginMethod
        txtLoginName.Text = dhdDatabase.LoginName
        txtPassword.Text = dhdDatabase.Password
        cbxLoginMethod_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub cbxLoginMethod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxLoginMethod.SelectedIndexChanged
        If cbxLoginMethod.SelectedItem = "SQL" Then
            txtLoginName.Enabled = True
            txtPassword.Enabled = True
            txtLoginName.Text = dhdDatabase.LoginName
            txtPassword.Text = dhdDatabase.Password
        ElseIf cbxLoginMethod.SelectedItem = "Windows" Then
            txtLoginName.Enabled = False
            txtPassword.Enabled = False
            txtLoginName.Text = ""
            txtPassword.Text = ""
        End If
    End Sub

    Private Sub btnUpgradeDatabase_Click(sender As Object, e As EventArgs) Handles btnUpgradeDatabase.Click
        Dim strSQL As String
        Dim strMode As String
        Dim MydbRef As New SDBA.DBRef

        btnUpgradeDatabase.Enabled = False

        Try
            Dim arrScripts(100, 1) As String
            arrScripts = MydbRef.GetNewList(txtUpgradeDatabase.Tag)

            strSQL = MydbRef.GetScript(arrScripts(0, 0))
            If DebugMode Then MessageBox.Show("<debug>Number of Scripts: " & arrScripts.GetUpperBound(1) + 1)

            strMode = arrScripts(1, 0)
            If strSQL = "-1" Then
                MessageBox.Show("Error retrieving SQL Script " & arrScripts(0, 0) & ", please contact your vendor")
                Exit Sub
            End If
            strSQL = Replace(strSQL, "Sequenchel", dhdDatabase.DatabaseName)
            If CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If strMode = "ALTER" Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
            QueryDb(dhdDatabase, strSQL, False)

            For i = 1 To arrScripts.GetUpperBound(1)
                strSQL = MydbRef.GetScript(arrScripts(0, i))
                If Not strSQL = "-1" Then
                    strSQL = Replace(strSQL, "Sequenchel", dhdDatabase.DatabaseName)
                    strMode = arrScripts(1, i)
                    If strMode = "ALTER" Then strSQL = Replace(strSQL, "CREATE", "ALTER", 1, 1)
                    QueryDb(dhdDatabase, strSQL, False)
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
        MessageBox.Show("Database Update successfull")
        VersionLoad()
    End Sub

    Private Sub btnBackupDatabase_Click(sender As Object, e As EventArgs) Handles btnBackupDatabase.Click
        Try
            BackupDatabase(dhdDatabase, txtBackupDatabase.Text)
            SaveConfigSetting("Database", "BackupLocation", txtBackupDatabase.Text, "A valid location on the server")
        Catch ex As Exception
            MessageBox.Show("While saving the database, the following error occured: " & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub btnCreateExtraProcs_Click(sender As Object, e As EventArgs) Handles btnCreateExtraProcs.Click
        If MessageBox.Show("This will create extra procedures with the power to damage your data or database. Create these procedures in a secure database." & Environment.NewLine & strMessages.strContinue, strMessages.strWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.No Then Exit Sub

        Dim strSQL As String
        Dim MydbRef As New SDBA.DBRef

        Try
            Dim arrScripts(100) As String
            arrScripts = MydbRef.GetDBAList()

            strSQL = MydbRef.GetScript(arrScripts(0))
            strSQL = strSQL.Replace("Sequenchel", dhdDatabase.DatabaseName)
            If CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
            If DevMode Then MessageBox.Show(strSQL)
            QueryDb(dhdDatabase, strSQL, False, 10)
            prbCreateDatabase.PerformStep()

            For i = 1 To arrScripts.GetUpperBound(0)
                strSQL = MydbRef.GetScript(arrScripts(i))
                If Not strSQL = "-1" Then
                    'strSQL = Replace(strSQL, "Sequenchel", strDBName)
                    strSQL = strSQL.Replace("Sequenchel", dhdDatabase.DatabaseName)
                    If CurVar.Encryption = False Then strSQL = strSQL.Replace("WITH ENCRYPTION", "")
                    'If DevMode Then MessageBox.Show(strSQL)
                    QueryDb(dhdDatabase, strSQL, False, 10)
                Else
                    If DevMode Then MessageBox.Show("The script: " & arrScripts(i) & " returned: " & strSQL)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        MessageBox.Show("Procedures added successfully")

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
        txtJobNamePrefix.Text = dhdDatabase.DatabaseName
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
        strQuery = "select InstanceDefaultLogPath = serverproperty('InstanceDefaultLogPath')"

        Dim objData As DataSet
        objData = QueryDb(dhdDatabase, strQuery, True)
        Dim strReturn As String = ""

        If objData Is Nothing Then
            'blnDatabaseOnLine = False
            Exit Sub
        End If
        If objData.Tables.Count = 0 Then Exit Sub
        If objData.Tables(0).Rows.Count = 0 Then Exit Sub

        For intRowCount = 0 To objData.Tables(0).Rows.Count - 1
            strReturn = objData.Tables.Item(0).Rows(intRowCount).Item("InstanceDefaultLogPath")
        Next
        txtErrorlogPath.Text = strReturn
    End Sub

    Private Sub btnErrorlogPathSystem_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathSystem.Click
        txtErrorlogPath.Text = Application.StartupPath & "\" & "LOG"
    End Sub

    Private Sub btnErrorlogPathDefault_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathDefault.Click
        txtErrorlogPath.Text = CurVar.DefaultConfigFilePath & "\" & "LOG"
    End Sub

    Private Sub btnErrorlogPathBrowse_Click(sender As Object, e As EventArgs) Handles btnErrorlogPathBrowse.Click
        Dim DefaultFolder As New FolderBrowserDialog
        DefaultFolder.SelectedPath = CurVar.DefaultConfigFilePath

        If (DefaultFolder.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (DefaultFolder.SelectedPath.Length) > 0 Then
            txtLogfileLocation.Text = DefaultFolder.SelectedPath
        End If
    End Sub

    Private Sub btnCreateScheduledJob_Click(sender As Object, e As EventArgs) Handles btnCreateScheduledJob.Click
        If cbxProcedures.SelectedIndex = -1 Then
            MessageBox.Show("Please select a procedure to schedule first")
            Exit Sub
        End If
        If cbxProcedures.SelectedItem.ToString.Length = 0 Then
            Exit Sub
        End If
        ScheduleAdd()
    End Sub

    Private Sub ScheduleAdd()
        Dim strJobName As String = ProcedureNameGet(cbxProcedures.SelectedItem)
        Dim strSqlCommand As String = ScheduleCommandBuild(strJobName)
        Dim strSTartTime As String = cbxStartHour.Text & cbxStartMinute.Text & "00"
        Dim strEndTime As String = cbxEndHour.Text & cbxEndMinute.Text & "00"
        Dim strErrorlogPath As String = dhdText.LogLocation

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
        If strValue.Length > 0 And IsNumeric(strValue) Then CurVar.MinPercGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "MinFreeSpace")
        If strValue.Length > 0 Then CurVar.MinFreeSpace = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "LowerLimit")
        If strValue.Length > 0 Then CurVar.LowerLimit = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "UpperLimit")
        If strValue.Length > 0 Then CurVar.UpperLimit = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "SmallGrowth")
        If strValue.Length > 0 Then CurVar.SmallGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "MediumGrowth")
        If strValue.Length > 0 Then CurVar.MediumGrowth = strValue
        strValue = ""
        strValue = LoadConfigSetting("MonitorDataspaces", "LargeGrowth")
        If strValue.Length > 0 Then CurVar.LargeGrowth = strValue
    End Sub

    Private Sub MonitorDataspacesShow()
        txtMinPercGrowth.Text = CurVar.MinPercGrowth
        txtMinFreeSpace.Text = CurVar.MinFreeSpace
        txtLowerLimit.Text = CurVar.LowerLimit
        txtUpperLimit.Text = CurVar.UpperLimit
        txtSmallGrowth.Text = CurVar.SmallGrowth
        txtMediumGrowth.Text = CurVar.MediumGrowth
        txtLargeGrowth.Text = CurVar.LargeGrowth
    End Sub

    Private Sub btnMonitorDataSpacesLoad_Click(sender As Object, e As EventArgs) Handles btnMonitorDataSpacesLoad.Click
        CursorControl("Wait")
        MonitorDataspacesLoad()
        MonitorDataspacesShow()
        CursorControl()
    End Sub

    Private Sub btnMonitorDataSpacesSave_Click(sender As Object, e As EventArgs) Handles btnMonitorDataSpacesSave.Click
        If IsNumeric(txtMinPercGrowth.Text) Then CurVar.MinPercGrowth = txtMinPercGrowth.Text
        If IsNumeric(txtMinFreeSpace.Text) Then CurVar.MinFreeSpace = txtMinFreeSpace.Text
        If IsNumeric(txtLowerLimit.Text) Then CurVar.LowerLimit = txtLowerLimit.Text
        If IsNumeric(txtUpperLimit.Text) Then CurVar.UpperLimit = txtUpperLimit.Text
        If IsNumeric(txtSmallGrowth.Text) Then CurVar.SmallGrowth = txtSmallGrowth.Text
        If IsNumeric(txtMediumGrowth.Text) Then CurVar.MediumGrowth = txtMediumGrowth.Text
        If IsNumeric(txtLargeGrowth.Text) Then CurVar.LargeGrowth = txtLargeGrowth.Text

        SaveConfigSetting("MonitorDataspaces", "MinPercGrowth", CurVar.MinPercGrowth)
        SaveConfigSetting("MonitorDataspaces", "MinFreeSpace", CurVar.MinFreeSpace)
        SaveConfigSetting("MonitorDataspaces", "LowerLimit", CurVar.LowerLimit)
        SaveConfigSetting("MonitorDataspaces", "UpperLimit", CurVar.UpperLimit)
        SaveConfigSetting("MonitorDataspaces", "SmallGrowth", CurVar.SmallGrowth)
        SaveConfigSetting("MonitorDataspaces", "MediumGrowth", CurVar.MediumGrowth)
        SaveConfigSetting("MonitorDataspaces", "LargeGrowth", CurVar.LargeGrowth)

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

    Private Sub SetDefaultText(objTextBox As TextBox)
        If objTextBox.Text = "" Then
            objTextBox.Text = objTextBox.Tag
            objTextBox.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub RemoveDefaultText(objTextBox As TextBox)
        If objTextBox.Text = objTextBox.Tag Then
            objTextBox.Text = ""
            objTextBox.ForeColor = SystemColors.WindowText
        End If
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
                QueryDb(dhdDatabase, strSQL, False, 10)
                lblFtpStatus.Text = "Procedure PutFTPfiles was created succesfully"
            Catch ex As Exception
                MessageBox.Show("There was an error creating the procedure" & Environment.NewLine & ex.Message)
            End Try
        Else
            If DebugMode Then MessageBox.Show("The script: 01 dbo.usp_PutFTPfiles.sql returned: " & strSQL)
        End If

    End Sub

    Private Sub btnCreateDownloadProcedure_Click(sender As Object, e As EventArgs) Handles btnCreateDownloadProcedure.Click
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
                QueryDb(dhdDatabase, strSQL, False, 10)
                lblFtpStatus.Text = "Procedure GetFTPfiles was created succesfully"
            Catch ex As Exception
                MessageBox.Show("There was an error creating the procedure" & Environment.NewLine & ex.Message)
            End Try
        Else
            If DebugMode Then MessageBox.Show("The script: 01 dbo.usp_GetFTPfiles.sql returned: " & strSQL)
        End If
    End Sub

#End Region

End Class