﻿Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet
Imports DocumentFormat.OpenXml
Imports System.Data.OleDb
Imports System.Text.RegularExpressions

Public Class Excel

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

    Public Function CreateExcelDocument(ByVal dt As DataTable, ByVal Filename As String) As Boolean
        Try
            Dim dtsInput As New DataSet
            dtsInput.Tables.Add(dt)
            Return CreateExcelDocument(dtsInput, Filename)
        Catch ex As Exception
            'WriteLog("Failed to create Excel Document: " & ex.Message, 1)
            Return False
        End Try
    End Function

    Public Function CreateExcelDocument(ByVal ds As DataSet, ByVal excelFilename As String) As Boolean
        Try
            'Dim document As SpreadsheetDocument = CreateWorkbook(excelFilename)
            Using document As SpreadsheetDocument = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook)
                Dim workbook As WorkbookPart = document.AddWorkbookPart

                document.WorkbookPart.Workbook = New DocumentFormat.OpenXml.Spreadsheet.Workbook()
                document.WorkbookPart.Workbook.Append(New BookViews(New WorkbookView()))

                Dim workbookStylesPart As WorkbookStylesPart = document.WorkbookPart.AddNewPart(Of WorkbookStylesPart)("rIdStyles")

                Dim stylesheet As New Stylesheet
                workbookStylesPart.Stylesheet = stylesheet
                workbookStylesPart.Stylesheet.Save()

                CreateParts(ds, document)

            End Using
            Trace.WriteLine("Successfully created: " + excelFilename)
            Return True

        Catch ex As Exception
            Trace.WriteLine("Failed, exception thrown: " + ex.Message)
            Return False
        End Try

    End Function

    Private Sub CreateParts(ByVal ds As DataSet, ByVal spreadsheet As SpreadsheetDocument)

        '  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
        Dim worksheetNumber As Integer = 1
        If Not spreadsheet.WorkbookPart.Workbook.Sheets Is Nothing AndAlso spreadsheet.WorkbookPart.Workbook.Sheets.Count > 0 Then
            worksheetNumber = spreadsheet.WorkbookPart.Workbook.Sheets.Count
        End If
        For Each dt As DataTable In ds.Tables
            AddWorksheet(spreadsheet, dt, worksheetNumber)
            worksheetNumber += 1
        Next
    End Sub

    Private Function AddWorksheet(ByVal spreadsheet As SpreadsheetDocument, ByVal dt As DataTable, intWorkSheetNumber As Integer, Optional SheetName As String = Nothing) As SpreadsheetDocument
        Try
            '  For each worksheet you want to create
            'Dim workSheetID As String = "rId" + worksheetNumber.ToString()

            If SheetName = Nothing Then SheetName = dt.TableName
            If SheetName = Nothing Then SheetName = "Sheet1"

            Dim newWorksheetPart As WorksheetPart = spreadsheet.WorkbookPart.AddNewPart(Of WorksheetPart)()
            newWorksheetPart.Worksheet = New DocumentFormat.OpenXml.Spreadsheet.Worksheet()

            Dim columnWidthSize As Integer = 20     ' Replace the following line with your desired Column Width for column # col
            Dim columns As New Columns

            For colInx As Integer = 0 To dt.Columns.Count
                Dim column As Column = CustomColumnWidth(colInx, columnWidthSize)
                columns.Append(column)
            Next
            newWorksheetPart.Worksheet.Append(columns)

            ' create sheet data
            newWorksheetPart.Worksheet.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.SheetData())

            ' save worksheet
            WriteDataTableToExcelWorksheet(dt, newWorksheetPart)
            newWorksheetPart.Worksheet.Save()

            ' create the worksheet to workbook relation
            If (intWorkSheetNumber = 1) Then
                spreadsheet.WorkbookPart.Workbook.AppendChild(New DocumentFormat.OpenXml.Spreadsheet.Sheets())
            End If

            Dim sheet As DocumentFormat.OpenXml.Spreadsheet.Sheet = New DocumentFormat.OpenXml.Spreadsheet.Sheet
            sheet.Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart)
            sheet.SheetId = intWorkSheetNumber
            sheet.Name = SheetName
            '            Sheets.Append(sheet)

            spreadsheet.WorkbookPart.Workbook.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.Sheets).Append(sheet)
            ' AppendChild(new DocumentFormat.OpenXml.Spreadsheet.Sheet()

            Return spreadsheet
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            'WriteLog("Failed to create Excel Sheet: " & ex.Message, 1)
            Return Nothing
        End Try

    End Function

    Private Sub WriteDataTableToExcelWorksheet(ByVal dt As DataTable, ByVal worksheetPart As WorksheetPart)
        Try

            Dim worksheet As Worksheet = worksheetPart.Worksheet
            Dim sheetData As SheetData = worksheet.GetFirstChild(Of SheetData)()

            Dim cellValue As String = ""
            Dim cellType As CellValues = CellValues.String

            Dim numberOfColumns As Integer = dt.Columns.Count
            'Dim IsNumericColumn(numberOfColumns) As Boolean
            Dim ColumnTypes(numberOfColumns) As CellValues

            Dim excelColumnNames([numberOfColumns]) As String

            For n As Integer = 0 To numberOfColumns
                excelColumnNames(numberOfColumns) = ColumnNameFromIndex(n)
            Next n

            Dim rowIndex As Integer = 1

            Dim headerRow As Row = New Row
            headerRow.RowIndex = rowIndex
            sheetData.Append(headerRow)

            For colInx As Integer = 0 To numberOfColumns - 1
                Dim col As DataColumn = dt.Columns(colInx)
                AppendCell(excelColumnNames(colInx) + "1", CellValues.String, FormatValue(col.ColumnName, CellValues.String), headerRow)
                'IsNumericColumn(colInx) = (col.DataType.FullName = "System.Decimal") Or (col.DataType.FullName = "System.Int32")
                Select Case col.DataType.FullName
                    Case "System.Boolean"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Boolean
                    Case "System.DateTime", "System.TimeSpan"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Date
                    Case "System.Double", "System.Int16", "System.Int32", "System.Int64", "System.Single", "System.UInt16", "System.UInt32", "System.UInt64", "System.Decimal"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number
                    Case "System.String", "System.Byte", "System.Char", "System.SByte", "System.Guid"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
                End Select
                ColumnTypes(colInx) = cellType
            Next

            Dim cellNumericValue As Double = 0

            For Each dr As DataRow In dt.Rows
                rowIndex = rowIndex + 1
                Dim newExcelRow As New Row
                newExcelRow.RowIndex = rowIndex
                sheetData.Append(newExcelRow)

                For colInx As Integer = 0 To numberOfColumns - 1
                    cellValue = FormatValue(dr.ItemArray(colInx), ColumnTypes(colInx))
                    AppendCell(excelColumnNames(colInx) + rowIndex.ToString(), ColumnTypes(colInx), FormatValue(cellValue, ColumnTypes(colInx)), newExcelRow)
                Next

            Next
        Catch ex As Exception
            'MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Function CustomColumnWidth(ByVal columnIndex As Integer, ByVal columnWidth As Double) As Column
        ' This creates a Column variable for a zero-based column-index (eg 0 = Excel Column A), with a particular column width.
        Dim column As New Column
        column.Min = columnIndex + 1
        column.Max = columnIndex + 1
        column.Width = columnWidth
        column.CustomWidth = True
        Return column
    End Function

    'Public Function GetExcelColumnName(ByVal columnIndex As Integer) As String
    '    If (columnIndex < 26) Then
    '        Return Chr(Asc("A") + columnIndex)
    '    End If

    '    Dim firstChar As Char,
    '        secondChar As Char

    '    firstChar = Chr(Asc("A") + (columnIndex \ 26) - 1)
    '    secondChar = Chr(Asc("A") + (columnIndex Mod 26))

    '    Return firstChar + secondChar
    'End Function

    Public Function ColumnNameFromIndex(columnIndex As Integer) As String
        Dim remainder As Integer
        Dim columnName As String = ""

        While (columnIndex > 0)
            remainder = (columnIndex - 1) Mod 26
            columnName = System.Convert.ToChar(65 + remainder).ToString() + columnName
            columnIndex = ((columnIndex - remainder) / 26)
        End While

        Return columnName
    End Function

    Public Sub AppendCell(ByVal cellReference As String, cellType As CellValues, ByVal cellStringValue As String, ByVal excelRow As Row)
        '/  Add a new Excel Cell to our Row 
        Dim cell As New Cell
        cell.CellReference = cellReference
        cell.DataType = cellType

        Dim cellValue As New CellValue
        Try
            cellValue.Text = cellStringValue
        Catch ex As Exception

        End Try

        cell.Append(cellValue)

        excelRow.Append(cell)
    End Sub

    Public Function FormatValue(value As Object, Style As DocumentFormat.OpenXml.Spreadsheet.CellValues) As String
        Try

            Dim strValue As String = ""
            If IsDBNull(value) Then
                strValue = ""
                Return strValue
            End If
            If Style = Spreadsheet.CellValues.String Then
                strValue = value
            ElseIf Style = Spreadsheet.CellValues.Boolean Then
                Select Case value.ToString.Trim.ToLower
                    Case "1", "true", "yes"
                        strValue = "1"
                    Case "0", "false", "no"
                        strValue = "0"
                    Case Else
                        strValue = ""
                End Select
            ElseIf Style = Spreadsheet.CellValues.Date Then
#If EN_US_CULTURE Then
            strvalue = value.ToOADate().ToString()
#Else
                strValue = value.ToOADate().ToString().Replace(",", ".")
#End If
            ElseIf Style = Spreadsheet.CellValues.Number Then
                Dim cellNumericValue As Double = 0
                strValue = value.ToString()
                If (Double.TryParse(strValue, cellNumericValue)) Then
                    strValue = cellNumericValue.ToString()
                Else
                    strValue = ""
                End If

#If EN_US_CULTURE Then
            strvalue = strValue
#Else
                strValue = strValue.Replace(",", ".")
#End If
            End If
            Return strValue
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function ImportExcelFile(strFilePath As String) As DataSet
        Dim Ext As String = strFilePath.Substring(strFilePath.LastIndexOf(".") + 1)
        Dim dtsExcel As New DataSet

        ErrorLevel = 0
        ErrorMessage = ""

        If Ext = "xls" Then
            'ImportExcel2003(strFilePath)
            'dstImport = ReadExcelFile(strFilePath)
            dtsExcel = ReadXlsFile(strFilePath)
        ElseIf Ext = "xlsx" Then
            dtsExcel = ReadXlsxFile(strFilePath)
        End If
        Return dtsExcel
    End Function

    Public Function ReadXlsFile(ByVal StrFilePath As String) As DataSet
        Dim ExcelCon As New OleDbConnection
        Dim ExcelAdp As OleDbDataAdapter
        Dim ExcelComm As OleDbCommand
        'Dim Col1 As DataColumn
        Dim StrSql As String
        Dim dstOutput As New DataSet

        Try
            ErrorLevel = 0
            ErrorMessage = ""
            ExcelCon.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" & _
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
                Dim objdt = New DataTable()
                ExcelAdp.Fill(objdt)
                dstOutput.Tables.Add(objdt)
            Next

            ExcelCon.Close()
            Return dstOutput
        Catch ex As Exception
            'MessageBox.Show("An error has occured importing the data" & Environment.NewLine & ex.Message)
            ErrorLevel = -1
            ErrorMessage = ex.Message
            Return Nothing
        Finally
            ExcelCon = Nothing
            ExcelAdp = Nothing
            ExcelComm = Nothing
        End Try
    End Function

    Public Function ReadXlsxFile(strFilePath As String) As DataSet
        Dim dstOutput As New DataSet
        Dim ColumnList As New List(Of String)
        Dim ExcelSheetName As String = ""

        'Open the Excel file in Read Mode using OpenXml.
        Using doc As SpreadsheetDocument = SpreadsheetDocument.Open(strFilePath, False)
            Try

                'Read the first Sheet from Excel file.
                Dim intCVount As Integer = doc.WorkbookPart.Workbook.Sheets.Count
                For Each excelSheet As Sheet In doc.WorkbookPart.Workbook.Sheets

                    'Dim sheet As Sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild(Of Sheet)()

                    'Get the Worksheet instance.
                    Dim worksheet As Worksheet = TryCast(doc.WorkbookPart.GetPartById(excelSheet.Id.Value), WorksheetPart).Worksheet

                    'Fetch all the rows present in the Worksheet.
                    Dim rows As IEnumerable(Of Row) = worksheet.GetFirstChild(Of SheetData)().Descendants(Of Row)()

                    'Create a new DataTable.
                    Dim dt As New DataTable()
                    dt.TableName = excelSheet.Name
                    ExcelSheetName = excelSheet.Name
                    'Loop through the Worksheet rows.
                    For Each row As Row In rows
                        'Use the first row to add columns to DataTable.
                        If row.RowIndex.Value = 1 Then
                            For Each cell As Cell In row.Descendants(Of Cell)()
                                dt.Columns.Add(GetValue(doc, cell))
                                ColumnList.Add(GetColumnName(cell.CellReference))
                            Next
                        Else
                            'Add rows to DataTable.
                            dt.Rows.Add()
                            'Dim i As Integer = 0
                            For Each cell As Cell In row.Descendants(Of Cell)()
                                Dim strColReference As String = GetColumnName(cell.CellReference)
                                If ColumnList.Contains(strColReference) Then
                                    Dim intCol As Integer = ColumnList.FindIndex(Function(value As String)
                                                                                     Return value = strColReference
                                                                                 End Function)
                                    dt.Rows(dt.Rows.Count - 1)(intCol) = GetValue(doc, cell)
                                    'dt.Rows(dt.Rows.Count - 1)(i) = GetValue(doc, cell)
                                End If
                                'Dim strCellValue As String = cell.CellValue.InnerText.ToString
                                'Dim strValue As String = GetValue(doc, cell)
                                'i += 1
                            Next
                        End If
                    Next
                    dstOutput.Tables.Add(dt)
                Next
            Catch ex As Exception
                ErrorLevel = -1
                ErrorMessage = "An error occured reading Excel sheet [" & ExcelSheetName & "]: " & ex.Message
            End Try
        End Using

        Return dstOutput
    End Function

    Private Function GetColumnName(strInput As String) As String
        Dim m As Match = Regex.Match(strInput, "^\D+")
        If m.Success Then
            Return m.Value
        End If
        Return Nothing
    End Function

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

    Private Function GetDataType(doc As SpreadsheetDocument, cell As Cell) As String
        Dim value As String = Nothing
        If cell.CellValue IsNot Nothing Then
            value = cell.CellValue.InnerText
            If cell.DataType IsNot Nothing AndAlso cell.DataType.Value = CellValues.SharedString Then
                Return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(Integer.Parse(value)).InnerText
            End If
        End If
        Return value
    End Function

End Class
