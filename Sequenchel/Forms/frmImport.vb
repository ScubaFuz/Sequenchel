
Public Class frmImport

    Private Sub frmImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDefaults()
    End Sub

    Private Sub btnSelectFile_Click(sender As Object, e As EventArgs) Handles btnSelectFile.Click
        SelectFile()
    End Sub

    Private Sub btnImportFile_Click(sender As Object, e As EventArgs) Handles btnImportFile.Click
        If txtCurrentFile.Text.Length > 3 And txtCurrentFile.Text.Contains(".") Then
            ImportFile()
        Else
            MessageBox.Show("Please enter a valid path and filename before pressing this button")
        End If
    End Sub

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click
        UploadFile(dtsImport)
    End Sub

    Private Sub btnUploadTable_Click(sender As Object, e As EventArgs) Handles btnUploadTable.Click
        Try
            Dim dtsUpload As New DataSet
            dtsUpload.Tables.Add(dgvImport.DataSource.Copy)
            UploadFile(dtsUpload)
        Catch ex As Exception
            MessageBox.Show("Table Upload failed. Check if the columns match and try again" & Environment.NewLine & ex.Message)
            lblStatusText.Text = "0 rows uploaded"
            Exit Sub
        End Try
    End Sub

    Private Sub LoadDefaults()
        txtServer.Text = SeqData.dhdConnection.DataLocation
        txtDatabase.Text = SeqData.dhdConnection.DatabaseName
        txtTable.Text = SeqData.curStatus.Table
        If SeqData.dhdConnection.LoginMethod = "Windows" Then
            chkWinAuth.Checked = True
        Else
            chkWinAuth.Checked = False
        End If
        txtUser.Text = SeqData.dhdConnection.LoginName
        txtPassword.Text = SeqData.dhdConnection.Password
    End Sub

    Private Sub SelectFile()
        Dim ofdFile As New OpenFileDialog
        'sfdFile.FileName = strFileName
        'sfdFile.Filter = "XML File (*.xml)|*.xml|Excel 2007 file (*.xlsx)|*.xlsx|"
        ofdFile.Filter = "All supported file types (*.xls, *.xlsx, *.xml, *.csv, *.txt)|*.xls;*.xlsx;*.xml;*.csv;*.txt|Excel file (*.xls, *.xlsx)|*.xls;*.xlsx|XML File (*.xml)|*.xml|Text File (*.csv, *.txt)|*.csv;*.txt"
        ofdFile.FilterIndex = 1
        ofdFile.RestoreDirectory = True
        'sfdFile.OverwritePrompt = True

        If (ofdFile.ShowDialog() <> DialogResult.OK) Then
            Return
        End If

        SeqData.dhdText.ImportFile = ofdFile.FileName
        txtCurrentFile.Text = SeqData.dhdText.ImportFile
        ImportFile()
    End Sub

    Private Sub ImportFile()
        dtsImport = SeqData.ImportFile(SeqData.CheckFilePath(SeqData.dhdText.ImportFile), chkHasHeaders.Checked, txtDelimiter.Text)

        If dtsImport Is Nothing Then
            MessageBox.Show("File extension or delimiter not recognised." & Environment.NewLine & "Please try again or select a different file.")
            Exit Sub
        End If

        Try
            If dtsImport.Tables.Count > 0 Then
                If chkScreen.Checked Then
                    DisplayData(0)
                End If
                UploadFile(dtsImport)
            Else
                MessageBox.Show("No data was loaded from the file." & Environment.NewLine & "Please check the file before trying again.")
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error displaying or uploading the file." & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub DisplayData(Optional intTable As Integer = 0)
        dgvImport.DataSource = Nothing
        If SeqData.curVar.ConvertToText = True Then
            Dim dttConvert As DataTable = SeqData.dhdMainDB.ConvertToText(dtsImport.Tables(intTable))
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
                SeqData.ExportFile(dtsUpload, SeqData.CheckFilePath(txtFileName.Text, True), SeqData.curVar.ConvertToText, SeqData.curVar.ConvertToNull, SeqData.curVar.ShowFile, chkHasHeaders.Checked, txtDelimiter.Text, SeqData.curVar.QuoteValues, SeqData.curVar.CreateDir)
            End If
        Catch ex As Exception
            MessageBox.Show("There was an error writng to " & txtFileName.Text & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub UploadToDatabase(dtsUpload As DataSet)
        Dim dhdDB As New DataHandler.db
        Dim intRecordsAffected As Integer = 0

        dhdDB.DataLocation = txtServer.Text
        dhdDB.DatabaseName = txtDatabase.Text
        dhdDB.DataTableName = txtTable.Text
        dhdDB.DataProvider = "SQL"
        If chkWinAuth.Checked = True Then
            dhdDB.LoginMethod = "Windows"
        Else
            dhdDB.LoginMethod = "SQL"
        End If
        dhdDB.LoginName = txtUser.Text
        dhdDB.Password = txtPassword.Text
        intRecordsAffected = SeqData.SaveToDatabase(dhdDB, dtsUpload, SeqData.curVar.ConvertToText, SeqData.curVar.ConvertToNull)
        If intRecordsAffected = -1 Then
            MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & "If you are importing more than 1 table, make sure they have identical columns" & Environment.NewLine & dhdDB.ErrorMessage)
            Exit Sub
        Else
            lblStatusText.Text = intRecordsAffected & " rows uploaded"
        End If
    End Sub

    Private Sub Checkfields()
        If chkDatabase.Checked = True Then
            txtServer.Enabled = True
            txtDatabase.Enabled = True
            txtTable.Enabled = True
            chkWinAuth.Enabled = True
            If chkWinAuth.Checked = False Then
                txtUser.Enabled = True
                txtPassword.Enabled = True
            Else
                txtUser.Enabled = False
                txtPassword.Enabled = False
            End If
        Else
            txtServer.Enabled = False
            txtDatabase.Enabled = False
            txtTable.Enabled = False
            chkWinAuth.Enabled = False
            txtUser.Enabled = False
            txtPassword.Enabled = False
        End If
        If chkFile.Checked = True Then
            txtFileName.Enabled = True
        Else
            txtFileName.Enabled = False
        End If
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
        DisplayData(btnPreviousTable.Tag)
    End Sub

    Private Sub btnNextTable_Click(sender As Object, e As EventArgs) Handles btnNextTable.Click
        DisplayData(btnNextTable.Tag)
    End Sub

    Private Sub chkImportTable_CheckedChanged(sender As Object, e As EventArgs) Handles chkUploadTable.CheckedChanged
        DataTableSetExtendedProperty(dgvImport.DataSource, chkUploadTable.Checked)
    End Sub

    Private Sub DataTableSetExtendedProperty(dttInput As DataTable, blnExportTable As Boolean)
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

    Private Sub chkCovertToText_CheckedChanged(sender As Object, e As EventArgs) Handles chkCovertToText.CheckedChanged
        SeqData.curVar.ConvertToText = chkCovertToText.Checked
    End Sub

    Private Sub chkCovertToNull_CheckedChanged(sender As Object, e As EventArgs) Handles chkCovertToNull.CheckedChanged
        SeqData.curVar.ConvertToNull = chkCovertToNull.Checked
    End Sub

    Private Sub txtDelimiter_MouseHover(sender As Object, e As EventArgs) Handles txtDelimiter.MouseHover
        txtDelimiterShow.Text = txtDelimiter.Text
        txtDelimiterShow.Visible = True
    End Sub

    Private Sub txtDelimiter_MouseLeave(sender As Object, e As EventArgs) Handles txtDelimiter.MouseLeave
        txtDelimiterShow.Visible = False
    End Sub

    Private Sub txtDelimiter_TextChanged(sender As Object, e As EventArgs) Handles txtDelimiter.TextChanged
        txtDelimiterShow.Text = txtDelimiter.Text
    End Sub
End Class