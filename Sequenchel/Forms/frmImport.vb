
Imports System.IO
Imports System.Data
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.Data.OleDb

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

    Private Sub btnExportFile_Click(sender As Object, e As EventArgs) Handles btnExportFile.Click
        ExportFile()
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

        txtCurrentFile.Text = ofdFile.FileName
        ImportFile()
    End Sub

    Private Sub ImportFile()
        Dim Ext As String = txtCurrentFile.Text.Substring(txtCurrentFile.Text.LastIndexOf(".") + 1)
        Dim strFilePath As String = txtCurrentFile.Text
        Dim dteExcel As New DataTable

        If Ext = "xls" Then
            'ImportExcel2003(strFilePath)
            ReadExcelFile(dteExcel, strFilePath)
        ElseIf Ext = "xlsx" Then
            ImportExcel(strFilePath)
        End If
    End Sub

    Private Sub ExportFile()
        If chkDatabase.Checked = True Then
            SaveToDatabase()
        End If
    End Sub

    Protected Sub ImportExcel(strFilePath As String)

        'Open the Excel file in Read Mode using OpenXml.
        Using doc As SpreadsheetDocument = SpreadsheetDocument.Open(strFilePath, False)
            'Read the first Sheet from Excel file.
            Dim sheet As Sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild(Of Sheet)()

            'Get the Worksheet instance.
            Dim worksheet As Worksheet = TryCast(doc.WorkbookPart.GetPartById(sheet.Id.Value), WorksheetPart).Worksheet

            'Fetch all the rows present in the Worksheet.
            Dim rows As IEnumerable(Of Row) = worksheet.GetFirstChild(Of SheetData)().Descendants(Of Row)()

            'Create a new DataTable.
            Dim dt As New DataTable()

            'Loop through the Worksheet rows.
            For Each row As Row In rows
                'Use the first row to add columns to DataTable.
                If row.RowIndex.Value = 1 Then
                    For Each cell As Cell In row.Descendants(Of Cell)()
                        dt.Columns.Add(GetValue(doc, cell))
                    Next
                Else
                    'Add rows to DataTable.
                    dt.Rows.Add()
                    Dim i As Integer = 0
                    For Each cell As Cell In row.Descendants(Of Cell)()
                        dt.Rows(dt.Rows.Count - 1)(i) = GetValue(doc, cell)
                        i += 1
                    Next
                End If
            Next
            dgvImport.DataSource = dt
            'dgvImport.DataBind()
        End Using
    End Sub

    Private Sub ImportExcel2003(ByVal StrFilePath As String)
        Dim objExcel As New Microsoft.Office.Interop.Excel.Application
        'Dim objWorkbook As New Microsoft.Office.Interop.Excel.Workbook

        Try
            'objExcel = objExcel.Workbooks.Open(StrFilePath, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            objExcel = objExcel.Workbooks.Open(StrFilePath)
            Dim sheet As Sheet = objExcel.WorkbookPart.Workbook.Sheets.GetFirstChild(Of Sheet)()

            'Get the Worksheet instance.
            Dim worksheet As Worksheet = TryCast(objExcel.WorkbookPart.GetPartById(sheet.Id.Value), WorksheetPart).Worksheet

            'Fetch all the rows present in the Worksheet.
            Dim rows As IEnumerable(Of Row) = worksheet.GetFirstChild(Of SheetData)().Descendants(Of Row)()

            'Create a new DataTable.
            Dim dt As New DataTable()

            'Loop through the Worksheet rows.
            For Each row As Row In rows
                'Use the first row to add columns to DataTable.
                If row.RowIndex.Value = 1 Then
                    For Each cell As Cell In row.Descendants(Of Cell)()
                        dt.Columns.Add(GetValue(objExcel, cell))
                    Next
                Else
                    'Add rows to DataTable.
                    dt.Rows.Add()
                    Dim i As Integer = 0
                    For Each cell As Cell In row.Descendants(Of Cell)()
                        dt.Rows(dt.Rows.Count - 1)(i) = GetValue(objExcel, cell)
                        i += 1
                    Next
                End If
            Next

            dgvImport.DataSource = dt

        Catch ex As Exception
            MessageBox.Show("An error has occured importing the data" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub ReadExcelFile(ByRef objdt As DataTable, ByVal StrFilePath As String)
        Dim ExcelCon As New OleDbConnection
        Dim ExcelAdp As OleDbDataAdapter
        Dim ExcelComm As OleDbCommand
        'Dim Col1 As DataColumn
        Dim StrSql As String

        Try
            ExcelCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                "Data Source= " & StrFilePath & _
                ";Extended Properties=""Excel 8.0;"""
            ExcelCon.Open()

            Dim dtSheets As DataTable =
              ExcelCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim listSheet As New List(Of String)
            Dim drSheet As DataRow

            For Each drSheet In dtSheets.Rows
                listSheet.Add(drSheet("TABLE_NAME").ToString())
            Next

            '//show sheetname in textbox where multiline is true
            For Each sheet As String In listSheet
                StrSql = "Select * From [" & sheet & "]"
                ExcelComm = New OleDbCommand(StrSql, ExcelCon)
                ExcelAdp = New OleDbDataAdapter(ExcelComm)
                objdt = New DataTable()
                ExcelAdp.Fill(objdt)
                Exit For
            Next

            dgvImport.DataSource = objdt
            ExcelCon.Close()
        Catch ex As Exception
            MessageBox.Show("An error has occured importing the data" & Environment.NewLine & ex.Message)
        Finally
            ExcelCon = Nothing
            ExcelAdp = Nothing
            ExcelComm = Nothing
        End Try
    End Sub

    Private Function GetValue(doc As SpreadsheetDocument, cell As Cell) As String
        Dim value As String = Nothing
        If cell.CellValue IsNot Nothing Then
            value = cell.CellValue.InnerText
            If cell.DataType IsNot Nothing AndAlso cell.DataType.Value = CellValues.SharedString Then
                Return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(Integer.Parse(value)).InnerText
            End If
        End If
        Return value
    End Function

    Private Sub SaveToDatabase()
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

        Try
            intRecordsAffected = dhdDB.UploadSqlData(dgvImport.DataSource)
            lblStatusText.Text = intRecordsAffected & " rows uploaded"
        Catch ex As Exception
            MessageBox.Show("Import failed. Check if the columns match and try again" & Environment.NewLine & ex.Message)
        End Try
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

End Class