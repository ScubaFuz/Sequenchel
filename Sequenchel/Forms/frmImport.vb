
Imports System.Xml

Public Class frmImport
    Private dtsImport As New DataSet

    Private Sub frmImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDefaults()
    End Sub

#Region "Controls"

    Private Sub btnSelectFile_Click(sender As Object, e As EventArgs) Handles btnSelectFile.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        SelectFile()
        CursorControl()
    End Sub

    Private Sub btnImportFile_Click(sender As Object, e As EventArgs) Handles btnImportFile.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If txtCurrentFile.Text.Length > 3 And txtCurrentFile.Text.Contains(".") Then
            basCode.dhdText.ImportFile = txtCurrentFile.Text
            ImportFile()
        Else
            WriteStatus("Please enter a valid path and filename before pressing this button", 2, lblStatusText)
        End If
        CursorControl()
    End Sub

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        If basCode.dhdText.DatasetCheck(dtsImport) = False Then Exit Sub
        UploadFile(dtsImport)
        CursorControl()
    End Sub

    Private Sub btnUploadTable_Click(sender As Object, e As EventArgs) Handles btnUploadTable.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        Try
            If dgvImport.DataSource Is Nothing Then Exit Sub
            Dim dtsUpload As New DataSet
            dtsUpload.Tables.Add(dgvImport.DataSource.Copy)
            UploadFile(dtsUpload)
        Catch ex As Exception
            WriteStatus("Table Upload failed. Check the log for more information.", 1, lblStatusText)
            basCode.WriteLog("Table Upload failed. Check if the columns match and try again" & Environment.NewLine & ex.Message, 1)
            Exit Sub
        End Try
        CursorControl()
    End Sub

    Private Sub chkDatabase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDatabase.CheckedChanged
        Checkfields()
    End Sub

    Private Sub chkFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFile.CheckedChanged
        Checkfields()
    End Sub

    Private Sub chkWinAuth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWinAuth.CheckedChanged
        Checkfields()
    End Sub

    Private Sub btnPreviousTable_Click(sender As Object, e As EventArgs) Handles btnPreviousTable.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        DisplayData(btnPreviousTable.Tag)
        CursorControl()
    End Sub

    Private Sub btnNextTable_Click(sender As Object, e As EventArgs) Handles btnNextTable.Click
        CursorControl("Wait")
        WriteStatus("", 0, lblStatusText)
        DisplayData(btnNextTable.Tag)
        CursorControl()
    End Sub

    Private Sub chkImportTable_CheckedChanged(sender As Object, e As EventArgs) Handles chkUploadTable.CheckedChanged
        DataTableSetExtendedProperty(dgvImport.DataSource, chkUploadTable.Checked)
    End Sub

    Private Sub chkCovertToText_CheckedChanged(sender As Object, e As EventArgs) Handles chkCovertToText.CheckedChanged
        basCode.curVar.ConvertToText = chkCovertToText.Checked
    End Sub

    Private Sub chkCovertToNull_CheckedChanged(sender As Object, e As EventArgs) Handles chkCovertToNull.CheckedChanged
        basCode.curVar.ConvertToNull = chkCovertToNull.Checked
    End Sub

    Private Sub txtDelimiter_GotFocus(sender As Object, e As EventArgs) Handles txtDelimiter.GotFocus
        txtDelimiter.SelectAll()
    End Sub

    Private Sub txtDelimiter_MouseHover(sender As Object, e As EventArgs) Handles txtDelimiter.MouseHover
        txtDelimiterShow.Text = txtDelimiter.Text
        txtDelimiterShow.Visible = True
        txtDelimiter.SelectAll()
    End Sub

    Private Sub txtDelimiter_MouseLeave(sender As Object, e As EventArgs) Handles txtDelimiter.MouseLeave
        txtDelimiterShow.Visible = False
        txtDelimiter.SelectAll()
    End Sub

    Private Sub txtDelimiter_TextChanged(sender As Object, e As EventArgs) Handles txtDelimiter.TextChanged
        txtDelimiterShow.Text = txtDelimiter.Text
    End Sub

    Private Sub btnUploadFolder_Click(sender As Object, e As EventArgs) Handles btnUploadFolder.Click
        SelectFolder()
    End Sub

#End Region

    Private Sub LoadDefaults()
        txtServer.Text = basCode.dhdConnection.DataLocation
        txtDatabase.Text = basCode.dhdConnection.DatabaseName
        txtTable.Text = basCode.curStatus.Table
        cbxTextEncoding.SelectedIndex = 0
        If basCode.dhdConnection.LoginMethod.ToLower = "windows" Then
            chkWinAuth.Checked = True
        Else
            chkWinAuth.Checked = False
        End If
        txtUser.Text = basCode.dhdConnection.LoginName
        txtPassword.Text = basCode.dhdConnection.Password
    End Sub

    Private Sub SelectFile()
        Dim ofdFile As New OpenFileDialog
        ofdFile.Filter = "All supported file types (*.xls, *.xlsx, *.xml, *.csv, *.txt, *.*)|*.xls;*.xlsx;*.xml;*.csv;*.txt;*.*|Excel file (*.xls, *.xlsx)|*.xls;*.xlsx|XML File (*.xml)|*.xml|Text File (*.csv, *.txt, *.*)|*.csv;*.txt;*.*"
        ofdFile.FilterIndex = 1
        ofdFile.RestoreDirectory = True

        If (ofdFile.ShowDialog() <> DialogResult.OK) Then
            Return
        End If

        basCode.dhdText.ImportFile = ofdFile.FileName
        txtCurrentFile.Text = basCode.dhdText.ImportFile
        WriteStatus("File Selected for Import", 0, lblStatusText)
        'ImportFile()
    End Sub

    Private Sub SelectFolder()
        Dim sfdFile As New SaveFileDialog

        sfdFile.Filter = "Excel file (*.xlsx)|*.xlsx|XML File (*.xml)|*.xml|Comma Separated File (*.csv)|*.csv|Text File (*.txt)|*.txt"
        sfdFile.FilterIndex = 1
        sfdFile.RestoreDirectory = True
        sfdFile.OverwritePrompt = True
        sfdFile.FileName = "UploadFile"
        sfdFile.CheckFileExists = False
        sfdFile.AddExtension = True
        sfdFile.DefaultExt = "xlsx"
        sfdFile.CreatePrompt = False

        sfdFile.Title = "Select Folder and enter File Name"

        If (sfdFile.ShowDialog() <> DialogResult.OK) Then
            Return
        End If

        txtUploadFile.Text = sfdFile.FileName
        WriteStatus("Folder Selected for upload", 0, lblStatusText)
        'ImportFile()
    End Sub

    Private Sub ImportFile()
        If basCode.dhdText.CheckFile(basCode.dhdText.PathConvert(basCode.CheckFilePath(basCode.dhdText.ImportFile))) = False Then
            WriteStatus("The file was not found. Check the file path and name", 2, lblStatusText)
            Exit Sub
        End If
        If txtDelimiter.Text.Length = 0 Then txtDelimiter.Text = ","
        basCode.curVar.HasHeaders = chkHasHeaders.Checked
        basCode.curVar.Delimiter = txtDelimiter.Text
        basCode.curVar.QuoteValues = chkQuotedValues.Checked
        basCode.curVar.LargeFile = chkLargeFile.Checked
        basCode.curVar.TextEncoding = cbxTextEncoding.SelectedItem
        If chkFilterRows.Checked = True Then
            basCode.curVar.RowFilter = txtFilterRows.Text
        Else
            basCode.curVar.RowFilter = ""
        End If
        If chkSkipRows.Checked = True Then
            basCode.curVar.SkipRows = nudSkipRows.Value
        Else
            basCode.curVar.SkipRows = 0
        End If
        If chkArchive.Checked = True And txtArchive.Text.Length > 0 Then
            If basCode.dhdText.CheckDir(txtArchive.Text) = True Then
                basCode.curVar.Archive = txtArchive.Text
            End If
        End If
        If IsNumeric(txtBatchSize.Text) Then basCode.curVar.BatchSize = txtBatchSize.Text
        dtsImport = basCode.ImportFile(basCode.dhdText.PathConvert(basCode.CheckFilePath(basCode.dhdText.ImportFile)))

        If basCode.ErrorLevel = -1 Then
            WriteStatus(basCode.ErrorMessage, 2, lblStatusText)
        Else
            WriteStatus("File loaded.", 0, lblStatusText)
        End If

        If basCode.dhdText.DatasetCheck(dtsImport) = False Then
            If basCode.ErrorLevel = -1 Then
                WriteStatus(basCode.ErrorMessage, 2, lblStatusText)
            Else
                WriteStatus("File extension or delimiter not recognised or unable to load file.", 2, lblStatusText)
            End If
            Exit Sub
        End If

        Try
            If dtsImport.Tables.Count > 0 Then
                If chkScreen.Checked And chkLargeFile.Checked = False Then
                    DisplayData(0)
                End If
                UploadFile(dtsImport)
            Else
                WriteStatus("No data was loaded from the file.", 2, lblStatusText)
            End If
        Catch ex As Exception
            WriteStatus("There was an error displaying or uploading the file. Please check the log", 1, lblStatusText)
            basCode.WriteLog("There was an error displaying or uploading the file." & Environment.NewLine & ex.Message, 1)
        End Try
    End Sub

    Private Sub ClearDisplayData()
        dgvImport.DataSource = Nothing
        dtsImport = Nothing
        WriteStatus("Data cleared.", 0, lblStatusText)
    End Sub

    Private Sub DisplayData(Optional intTable As Integer = 0)
        If basCode.dhdText.DatasetCheck(dtsImport) = False Then Exit Sub
        dgvImport.DataSource = Nothing
        If basCode.curVar.ConvertToText = True Then
            Dim dttConvert As DataTable = basCode.dhdMainDB.ConvertToText(dtsImport.Tables(intTable))
            dgvImport.DataSource = dttConvert
        Else
            dgvImport.DataSource = dtsImport.Tables(intTable)
        End If
        lblTableNameText.Text = dtsImport.Tables(intTable).TableName
        If intTable > 0 Then
            btnPreviousTable.Tag = intTable - 1
            btnPreviousTable.Enabled = True
        Else
            btnPreviousTable.Enabled = False
        End If
        If intTable < dtsImport.Tables.Count - 1 Then
            btnNextTable.Tag = intTable + 1
            btnNextTable.Enabled = True
        Else
            btnNextTable.Enabled = False
        End If
        lblTableNumber.Text = "Table " & intTable + 1 & " of " & dtsImport.Tables.Count
        chkUploadTable.Checked = DataTableGetExtendedProperty(dtsImport.Tables(intTable))
    End Sub

    Private Sub UploadFile(dtsUpload As DataSet)
        If chkFile.Checked = True Then
            ExportToFile(dtsUpload)
        End If
        If chkDatabase.Checked = True Then
            UploadToDatabase(dtsUpload)
        End If
    End Sub

    Private Sub ExportToFile(dtsUpload As DataSet)
        Try
            For intCount As Integer = dtsUpload.Tables.Count - 1 To 0 Step -1
                If dtsUpload.Tables(intCount).ExtendedProperties.Count > 0 Then
                    If dtsUpload.Tables(intCount).ExtendedProperties.ContainsKey("ExportTable") = True Then
                        If dtsUpload.Tables(intCount).ExtendedProperties("ExportTable") = "False" Then
                            dtsUpload.Tables.Remove(dtsUpload.Tables(intCount))
                        End If
                    End If
                End If
            Next
            If dtsUpload.Tables.Count > 0 Then
                If basCode.ExportFile(dtsUpload, basCode.CheckFilePath(txtUploadFile.Text, True), basCode.curVar.ConvertToText, basCode.curVar.ConvertToNull, basCode.curVar.ShowFile, chkHasHeaders.Checked, txtDelimiter.Text, basCode.curVar.QuoteValues, basCode.curVar.CreateDir) = False Then
                    WriteStatus("There was an error exporting the file. PLease check the log.", 1, lblStatusText)
                Else
                    WriteStatus("File uploaded.", 0, lblStatusText)
                End If
            End If
        Catch ex As Exception
            WriteStatus("There was an error writng to " & txtUploadFile.Text & " Please check the log", 1, lblStatusText)
            basCode.WriteLog("There was an error writng to " & txtUploadFile.Text & Environment.NewLine & ex.Message, 1)
        End Try
    End Sub

    Private Sub UploadToDatabase(dtsUpload As DataSet)
        Dim dhdDB As New DataHandler.db
        Dim intRecordsAffected As Integer = 0

        dhdDB.DataLocation = txtServer.Text
        dhdDB.DatabaseName = txtDatabase.Text
        dhdDB.DataTableName = basCode.GetTableNameFromAlias(basCode.xmlTables, txtTable.Text)
        dhdDB.DataProvider = "SQL"
        If chkWinAuth.Checked = True Then
            dhdDB.LoginMethod = "Windows"
        Else
            dhdDB.LoginMethod = "SQL"
        End If
        dhdDB.LoginName = txtUser.Text
        dhdDB.Password = txtPassword.Text

        basCode.curStatus.CreateTargetTable = chkCreateTable.Checked
        basCode.curStatus.ImportAsXml = chkUploadAsXml.Checked
        Dim blnTargetTableOK As Boolean = basCode.CreateTargetTable(dhdDB, dtsUpload)
        If blnTargetTableOK = False Then
            WriteStatus("The specified table was not found.", 1, lblStatusText)
            basCode.WriteLog("Export to database failed. The specified table was not found.", 1)
            Exit Sub
        End If

        If chkClearTarget.Checked = True Then
            'Clear target table
            basCode.ClearTargetTable(dhdDB)
            If basCode.ErrorLevel = -1 Then
                'Data removal failed, probably because of a constraint. Try removing every row.
                lblStatusText.Text = basCode.ErrorMessage
            End If
        End If
        If chkSqlCommand.Checked = True And txtSqlCommand.Text.Length > 0 Then
            basCode.curVar.SqlCommand = txtSqlCommand.Text
        End If

        If basCode.curStatus.ImportAsXml = True Then
            intRecordsAffected = basCode.SaveXmlToDatabase(dhdDB, dtsUpload, basCode.dhdText.ImportFile)
        ElseIf basCode.curVar.LargeFile = True Then
            intRecordsAffected = basCode.SaveLargeFileToDatabase(dhdDB, basCode.dhdText.ImportFile)
        Else
            intRecordsAffected = basCode.SaveToDatabase(dhdDB, dtsUpload)
        End If
        If intRecordsAffected = -1 Then
            WriteStatus("Export to database failed.", 1, lblStatusText)
            basCode.WriteLog("Export to database failed. Check if the columns match and try again" & Environment.NewLine & "If you are importing more than 1 table, make sure they have identical columns" & Environment.NewLine & dhdDB.ErrorMessage, 1)
            MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & "If you are importing more than 1 table, make sure they have identical columns" & Environment.NewLine & dhdDB.ErrorMessage)
            Exit Sub
        Else
            WriteStatus(intRecordsAffected & " row(s) uploaded", 0, lblStatusText)
        End If
    End Sub

    Private Sub Checkfields()
        If chkDatabase.Checked = True Then
            grpDatabase.Enabled = True
            If chkWinAuth.Checked = False Then
                txtUser.Enabled = True
                txtPassword.Enabled = True
            Else
                txtUser.Enabled = False
                txtPassword.Enabled = False
            End If
        Else
            grpDatabase.Enabled = False
        End If
        If chkFile.Checked = True Then
            grpUploadFile.Enabled = True
        Else
            grpUploadFile.Enabled = False
        End If
    End Sub

    Private Sub DataTableSetExtendedProperty(dttInput As DataTable, blnExportTable As Boolean)
        If dttInput Is Nothing Then Exit Sub
        If dttInput.ExtendedProperties.Count = 0 Then
            dttInput.ExtendedProperties.Add("ExportTable", blnExportTable.ToString)
        Else
            For intCount As Integer = 1 To dttInput.ExtendedProperties.Count
                If dttInput.ExtendedProperties.ContainsKey("ExportTable") = True Then
                    dttInput.ExtendedProperties("ExportTable") = blnExportTable.ToString
                Else
                    dttInput.ExtendedProperties.Add("ExportTable", blnExportTable.ToString)
                End If
            Next
        End If
    End Sub

    Private Function DataTableGetExtendedProperty(dttInput As DataTable) As Boolean
        If dttInput.ExtendedProperties.Count > 0 Then
            If dttInput.ExtendedProperties.ContainsKey("ExportTable") = True Then
                Return dttInput.ExtendedProperties("ExportTable")
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Private Sub btnClearData_Click(sender As Object, e As EventArgs) Handles btnClearData.Click
        ClearDisplayData()
    End Sub

    Private Sub chkClearTarget_CheckedChanged(sender As Object, e As EventArgs) Handles chkClearTarget.CheckedChanged
        If chkClearTarget.Checked = True Then
            If MessageBox.Show("This will delete all data from the target table before importing new data." & Environment.NewLine & "Are you sure?", "All data will be deleted.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                chkClearTarget.Checked = False
            End If
        End If
    End Sub

End Class