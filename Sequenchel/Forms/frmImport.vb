
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
        ImportFile()
    End Sub

    Private Sub btnUploadFile_Click(sender As Object, e As EventArgs) Handles btnUploadFile.Click
        UploadFile()
    End Sub

    Private Sub btnUploadTable_Click(sender As Object, e As EventArgs) Handles btnUploadTable.Click
        Dim intRecordsAffected As Integer = 0
        Try
            intRecordsAffected = UploadTable(dgvImport.DataSource)
            If intRecordsAffected = -1 Then
                MessageBox.Show("Export to database failed. Check if the columns match and try again")
                lblStatusText.Text = "0 rows uploaded"
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & ex.Message)
            lblStatusText.Text = "0 rows uploaded"
            Exit Sub
        End Try
        lblStatusText.Text = intRecordsAffected & " rows uploaded"

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
        dgvImport.DataSource = dtsImport.Tables(intTable)
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
        chkUploadTable.Checked = DataTableGetExtendedPorperty(dtsImport.Tables(intTable))
    End Sub

    Private Sub UploadFile()
        If chkFile.Checked = True Then
            Try
                Dim strExtension As String = txtFileName.Text.Substring(txtFileName.Text.LastIndexOf(".") + 1, txtFileName.Text.Length - (txtFileName.Text.LastIndexOf(".") + 1))
                If strExtension.ToLower = "xml" Then
                    dhdText.ExportDataSetToXML(dtsImport, txtFileName.Text, False)
                    'Dim xmlDoc As New StreamWriter(txtFileName.Text, False)
                    'dtsImport.WriteXml(xmlDoc)
                    'xmlDoc.Close()
                Else
                    MessageBox.Show("Only files with XML extension are allowed at this time.")
                End If
            Catch ex As Exception
                MessageBox.Show("There was an error writng to " & txtFileName.Text & Environment.NewLine & ex.Message)
            End Try
        End If
        If chkDatabase.Checked = True Then
            SaveToDatabase(dtsImport)
        End If
    End Sub

    'Protected Function ImportExcel(strFilePath As String) As DataSet
    '    Dim dstOutput As New DataSet

    '    'Open the Excel file in Read Mode using OpenXml.
    '    Using doc As SpreadsheetDocument = SpreadsheetDocument.Open(strFilePath, False)
    '        'Read the first Sheet from Excel file.
    '        For Each excelSheet As Sheet In doc.WorkbookPart.Workbook.Sheets

    '            'Dim sheet As Sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild(Of Sheet)()

    '            'Get the Worksheet instance.
    '            Dim worksheet As Worksheet = TryCast(doc.WorkbookPart.GetPartById(excelSheet.Id.Value), WorksheetPart).Worksheet

    '            'Fetch all the rows present in the Worksheet.
    '            Dim rows As IEnumerable(Of Row) = worksheet.GetFirstChild(Of SheetData)().Descendants(Of Row)()

    '            'Create a new DataTable.
    '            Dim dt As New DataTable()
    '            dt.TableName = excelSheet.Name
    '            'Loop through the Worksheet rows.
    '            For Each row As Row In rows
    '                'Use the first row to add columns to DataTable.
    '                If row.RowIndex.Value = 1 Then
    '                    For Each cell As Cell In row.Descendants(Of Cell)()
    '                        dt.Columns.Add(GetValue(doc, cell))
    '                    Next
    '                Else
    '                    'Add rows to DataTable.
    '                    dt.Rows.Add()
    '                    Dim i As Integer = 0
    '                    For Each cell As Cell In row.Descendants(Of Cell)()
    '                        dt.Rows(dt.Rows.Count - 1)(i) = GetValue(doc, cell)
    '                        i += 1
    '                    Next
    '                End If
    '            Next
    '            dstOutput.Tables.Add(dt)
    '        Next

    '    End Using

    '    Return dstOutput
    'End Function

    'Private Function ImportExcel2003(ByVal StrFilePath As String) As DataSet
    '    Dim dstOutput As New DataSet

    '    Dim objExcel As New Microsoft.Office.Interop.Excel.Application
    '    'Dim objWorkbook As New Microsoft.Office.Interop.Excel.Workbook

    '    Try
    '        'objExcel = objExcel.Workbooks.Open(StrFilePath, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
    '        objExcel = objExcel.Workbooks.Open(StrFilePath)
    '        Dim sheet As Sheet = objExcel.WorkbookPart.Workbook.Sheets.GetFirstChild(Of Sheet)()

    '        'Get the Worksheet instance.
    '        Dim worksheet As Worksheet = TryCast(objExcel.WorkbookPart.GetPartById(sheet.Id.Value), WorksheetPart).Worksheet

    '        'Fetch all the rows present in the Worksheet.
    '        Dim rows As IEnumerable(Of Row) = worksheet.GetFirstChild(Of SheetData)().Descendants(Of Row)()

    '        'Create a new DataTable.
    '        Dim dt As New DataTable()

    '        'Loop through the Worksheet rows.
    '        For Each row As Row In rows
    '            'Use the first row to add columns to DataTable.
    '            If row.RowIndex.Value = 1 Then
    '                For Each cell As Cell In row.Descendants(Of Cell)()
    '                    dt.Columns.Add(GetValue(objExcel, cell))
    '                Next
    '            Else
    '                'Add rows to DataTable.
    '                dt.Rows.Add()
    '                Dim i As Integer = 0
    '                For Each cell As Cell In row.Descendants(Of Cell)()
    '                    dt.Rows(dt.Rows.Count - 1)(i) = GetValue(objExcel, cell)
    '                    i += 1
    '                Next
    '            End If
    '        Next
    '        dstOutput.Tables.Add(dt)
    '        Return dstOutput
    '    Catch ex As Exception
    '        MessageBox.Show("An error has occured importing the data" & Environment.NewLine & ex.Message)
    '        Return Nothing
    '    End Try
    'End Function

    'Private Function ReadExcelFile(ByVal StrFilePath As String) As DataSet
    '    Dim ExcelCon As New OleDbConnection
    '    Dim ExcelAdp As OleDbDataAdapter
    '    Dim ExcelComm As OleDbCommand
    '    'Dim Col1 As DataColumn
    '    Dim StrSql As String
    '    Dim dstOutput As New DataSet

    '    Try
    '        ExcelCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
    '            "Data Source= " & StrFilePath & _
    '            ";Extended Properties=""Excel 8.0;"""
    '        ExcelCon.Open()

    '        Dim dtSheets As DataTable =
    '          ExcelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
    '        Dim listSheet As New List(Of String)
    '        Dim drSheet As DataRow

    '        For Each drSheet In dtSheets.Rows
    '            listSheet.Add(drSheet("TABLE_NAME").ToString())
    '        Next

    '        '//show sheetname in textbox where multiline is true
    '        For Each sheet As String In listSheet
    '            StrSql = "Select * From [" & sheet & "]"
    '            ExcelComm = New OleDbCommand(StrSql, ExcelCon)
    '            ExcelAdp = New OleDbDataAdapter(ExcelComm)
    '            Dim objdt = New DataTable()
    '            ExcelAdp.Fill(objdt)
    '            dstOutput.Tables.Add(objdt)
    '        Next

    '        ExcelCon.Close()
    '        Return dstOutput
    '    Catch ex As Exception
    '        MessageBox.Show("An error has occured importing the data" & Environment.NewLine & ex.Message)
    '        Return Nothing
    '    Finally
    '        ExcelCon = Nothing
    '        ExcelAdp = Nothing
    '        ExcelComm = Nothing
    '    End Try
    'End Function

    'Private Function GetValue(doc As SpreadsheetDocument, cell As Cell) As String
    '    Dim value As String = Nothing
    '    If cell.CellValue IsNot Nothing Then
    '        value = cell.CellValue.InnerText
    '        If cell.DataType IsNot Nothing AndAlso cell.DataType.Value = CellValues.SharedString Then
    '            Return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(Integer.Parse(value)).InnerText
    '        End If
    '    End If
    '    Return value
    'End Function

    Private Sub SaveToDatabase(dtsInput As DataSet)
        Dim intRecordsAffected As Integer = 0
        Dim intReturn As Integer = 0

        Try
            For Each Table In dtsInput.Tables
                Dim blnExport As Boolean = True
                If Table.ExtendedProperties.Count > 0 Then
                    If Table.ExtendedProperties.ContainsKey("ExportTable") = True Then
                        If Table.ExtendedProperties("ExportTable") = "False" Then
                            blnExport = False
                        End If
                    End If
                End If
                If blnExport = True Then
                    intReturn = UploadTable(Table)
                    If intReturn = -1 Then
                        MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & "If you are importing more than 1 table, make sure they have identical columns")
                        Exit Sub
                    Else
                        intRecordsAffected += intReturn
                    End If
                End If
            Next
            'intRecordsAffected = dhdDB.UploadSqlData(dgvImport.DataSource)
            lblStatusText.Text = intRecordsAffected & " rows uploaded"
        Catch ex As Exception
            MessageBox.Show("Export to database failed. Check if the columns match and try again" & Environment.NewLine & "If you are importing more than 1 table, make sure they have identical columns" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Function UploadTable(dttInput As DataTable) As Integer
        Dim dhdDB As New DataHandler.db
        Dim intRecordsAffected As Integer = 0

        Try
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

            intRecordsAffected = dhdDB.UploadSqlData(dttInput)

        Catch ex As Exception
            WriteLog("Uploading Table failed. " & ex.Message, 1)
            Return -1
        End Try
        Return intRecordsAffected
    End Function

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
        DataTableSetExtendedPorperty(dgvImport.DataSource, chkUploadTable.Checked)
    End Sub

    Private Sub DataTableSetExtendedPorperty(dttInput As DataTable, blnExportTable As Boolean)
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

    Private Function DataTableGetExtendedPorperty(dttInput As DataTable) As Boolean
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

End Class