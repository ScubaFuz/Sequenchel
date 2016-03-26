﻿
'Imports System.IO
'Imports System.Data
'Imports DocumentFormat.OpenXml.Packaging
'Imports DocumentFormat.OpenXml.Spreadsheet
'Imports System.Data.OleDb

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
        UploadFile()
    End Sub

    Private Sub btnUploadTable_Click(sender As Object, e As EventArgs) Handles btnUploadTable.Click
        Try
            Dim dtsUpload As New DataSet
            dtsUpload.Tables.Add(dgvImport.DataSource.Copy)
            UploadToDatabase(dtsUpload)
        Catch ex As Exception
            MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & ex.Message)
            lblStatusText.Text = "0 rows uploaded"
            Exit Sub
        End Try
    End Sub

    Private Sub LoadDefaults()
        txtServer.Text = dhdConnection.DataLocation
        txtDatabase.Text = dhdConnection.DatabaseName
        txtTable.Text = CurStatus.Table
        If dhdConnection.LoginMethod = "Windows" Then
            chkWinAuth.Checked = True
        Else
            chkWinAuth.Checked = False
        End If
        txtUser.Text = dhdConnection.LoginName
        txtPassword.Text = dhdConnection.Password
    End Sub

    Private Sub SelectFile()
        Dim ofdFile As New OpenFileDialog
        'sfdFile.FileName = strFileName
        'sfdFile.Filter = "XML File (*.xml)|*.xml|XML Text File(*.xml)|*.xml|Excel 2007 file (*.xlsx)|*.xlsx|Excel 2007 Text file(*.xlsx)|*.xlsx"
        ofdFile.Filter = "Excel file (*.xls, *.xlsx)|*.xls;*.xlsx"
        ofdFile.FilterIndex = 1
        ofdFile.RestoreDirectory = True
        'sfdFile.OverwritePrompt = True

        If (ofdFile.ShowDialog() <> DialogResult.OK) Then
            Return
        End If

        dhdText.ImportFile = ofdFile.FileName
        txtCurrentFile.Text = dhdText.ImportFile
        ImportFile()
    End Sub

    Private Sub ImportFile()
        dtsImport = Excel.ImportExcelFile(dhdText.ImportFile)
        Try
            If dtsImport.Tables.Count > 0 Then
                If chkScreen.Checked Then
                    DisplayData(0)
                End If
                UploadFile()
            End If
        Catch ex As Exception
            MessageBox.Show("there was an error importing the file" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub DisplayData(Optional intTable As Integer = 0)
        dgvImport.DataSource = Nothing
        If SeqData.curVar.ConvertToText = True Then
            Dim dttConvert As DataTable = dhdDatabase.ConvertToText(dtsImport.Tables(intTable))
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

    Private Sub UploadFile()
        If chkFile.Checked = True Then
            ExportToFile()
        End If
        If chkDatabase.Checked = True Then
            UploadToDatabase(dtsImport)
        End If
    End Sub

    Private Sub ExportToFile()
        Try
            Dim strExtension As String = txtFileName.Text.Substring(txtFileName.Text.LastIndexOf(".") + 1, txtFileName.Text.Length - (txtFileName.Text.LastIndexOf(".") + 1))
            If strExtension.ToLower = "xml" Then
                Dim dtsUpload As DataSet = dtsImport
                If SeqData.curVar.ConvertToText = True Then
                    dtsUpload = dhdConnection.ConvertToText(dtsUpload)
                    'Dim dtsConvert As DataSet = dhdConnection.ConvertToText(dtsImport)
                    'Else
                    '    dhdText.ExportDataSetToXML(dtsImport, txtFileName.Text, False)
                End If
                If SeqData.curVar.ConvertToNull = True Then
                    dtsUpload = dhdConnection.EmptyToNull(dtsUpload)
                End If
                dhdText.ExportDataSetToXML(dtsUpload, txtFileName.Text, False)
            Else
                MessageBox.Show("Only files with XML extension are allowed at this time.")
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

    'Private Sub SaveToDatabase(dtsInput As DataSet)
    '    Dim intRecordsAffected As Integer = 0
    '    Dim intReturn As Integer = 0

    '    Try
    '        For Each Table In dtsInput.Tables
    '            Dim blnExport As Boolean = True
    '            If Table.ExtendedProperties.Count > 0 Then
    '                If Table.ExtendedProperties.ContainsKey("ExportTable") = True Then
    '                    If Table.ExtendedProperties("ExportTable") = "False" Then
    '                        blnExport = False
    '                    End If
    '                End If
    '            End If
    '            If blnExport = True Then
    '                intReturn = UploadTable(Table)
    '                If intReturn = -1 Then
    '                    MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & "If you are importing more than 1 table, make sure they have identical columns")
    '                    Exit Sub
    '                Else
    '                    intRecordsAffected += intReturn
    '                End If
    '            End If
    '        Next
    '        'intRecordsAffected = dhdDB.UploadSqlData(dgvImport.DataSource)
    '        lblStatusText.Text = intRecordsAffected & " rows uploaded"
    '    Catch ex As Exception
    '        MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & "If you are importing more than 1 table, make sure they have identical columns" & Environment.NewLine & ex.Message)
    '    End Try
    'End Sub

    'Private Function UploadTable(dttInput As DataTable) As Integer
    '    Dim dhdDB As New DataHandler.db
    '    Dim intRecordsAffected As Integer = 0

    '    Try
    '        dhdDB.DataLocation = txtServer.Text
    '        dhdDB.DatabaseName = txtDatabase.Text
    '        dhdDB.DataTableName = txtTable.Text
    '        dhdDB.DataProvider = "SQL"
    '        If chkWinAuth.Checked = True Then
    '            dhdDB.LoginMethod = "Windows"
    '        Else
    '            dhdDB.LoginMethod = "SQL"
    '        End If
    '        dhdDB.LoginName = txtUser.Text
    '        dhdDB.Password = txtPassword.Text

    '        If SeqData.curVar.ConvertToText = True Then
    '            Dim dttConvert As DataTable = dhdConnection.ConvertToText(dttInput)
    '            intRecordsAffected = dhdDB.UploadSqlData(dttConvert)
    '        Else
    '            intRecordsAffected = dhdDB.UploadSqlData(dttInput)
    '        End If

    '    Catch ex As Exception
    '        WriteLog("Uploading Table failed. " & ex.Message, 1)
    '        Return -1
    '    End Try
    '    Return intRecordsAffected
    'End Function

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
End Class