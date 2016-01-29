Option Infer On
Imports System.Linq
Imports DocumentFormat.OpenXml

Public Class Excel

    ''' <summary>
    ''' Creates a basic workbook
    ''' </summary>
    ''' <param name="workbookName">Name of the workbook</param>
    ''' <param name="createStylesInCode">Create the styles in code?</param>
    Public Shared Sub CreateWorkbook(dstInput As DataSet, workbookName As String, createStylesInCode As Boolean)
        Dim spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument
        Dim worksheet As DocumentFormat.OpenXml.Spreadsheet.Worksheet
        Dim styleXml As String

        spreadsheet = Excel.CreateWorkbook(workbookName)
        If (spreadsheet Is Nothing) Then
            Return
        End If

        If (createStylesInCode) Then
            Excel.AddBasicStyles(spreadsheet)
        Else
            Using styleXmlReader As System.IO.StreamReader = New System.IO.StreamReader("PredefinedStyles.xml")
                styleXml = styleXmlReader.ReadToEnd()
                Excel.AddPredefinedStyles(spreadsheet, styleXml)
            End Using
        End If

        'Excel.AddSharedString(spreadsheet, "Shared string")
        'Excel.AddWorksheet(spreadsheet, "Test 1")
        Excel.AddWorksheet(spreadsheet, dstInput.Tables(0).TableName)
        worksheet = spreadsheet.WorkbookPart.WorksheetParts.First().Worksheet

        For Each colColumn As DataColumn In dstInput.Tables(0).Columns
            'For intColCount As Integer = 1 To dstInput.Tables(0).Columns.Count
            Excel.SetStringCellValue(spreadsheet, worksheet, colColumn.Ordinal + 1, 1, colColumn.ColumnName, False, False)
            'Next
        Next
        Dim cellType As DocumentFormat.OpenXml.Spreadsheet.CellValues = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
        Dim strValue As String = ""

        For Each dtrRow As DataRow In dstInput.Tables(0).Rows
            For Each colColumn As DataColumn In dstInput.Tables(0).Columns
                Select Case colColumn.DataType.ToString
                    Case "System.Boolean"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Boolean
                        'strValue = FormatValue(dtrRow.Item(colColumn), cellType)
                        'Excel.SetCellValue(spreadsheet, worksheet, colColumn.Ordinal, dstInput.Tables(0).Rows.IndexOf(dtrRow), cellType, strValue, Nothing, True)
                    Case "System.DateTime", "System.TimeSpan"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Date
                        'Excel.SetDateCellValue(spreadsheet, worksheet, colColumn.Ordinal, dstInput.Tables(0).Rows.IndexOf(dtrRow), ReplaceNull(dtrRow, colColumn), 1, True)
                    Case "System.Double", "System.Int16", "System.Int32", "System.Int64", "System.Single", "System.UInt16", "System.UInt32", "System.UInt64", "System.Decimal"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number
                        'Excel.SetDoubleCellValue(spreadsheet, worksheet, colColumn.Ordinal, dstInput.Tables(0).Rows.IndexOf(dtrRow), ReplaceNull(dtrRow, colColumn), Nothing, True)
                    Case "System.String", "System.Byte", "System.Char", "System.SByte", "System.Guid"
                        cellType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
                        'Excel.SetStringCellValue(spreadsheet, worksheet, colColumn.Ordinal, dstInput.Tables(0).Rows.IndexOf(dtrRow), ReplaceNull(dtrRow, colColumn), False, False)
                End Select
                strValue = FormatValue(dtrRow.Item(colColumn), cellType)
                Excel.SetCellValue(spreadsheet, worksheet, colColumn.Ordinal + 1, dstInput.Tables(0).Rows.IndexOf(dtrRow) + 2, cellType, strValue, Nothing, False)
            Next
        Next

        '' Add shared strings
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 1, "Shared string", True)
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 2, "Shared string", True)
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 3, "Shared string", True)

        '' Add a string
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 5, "Number", False, False)
        '' Add a decimal number
        'Excel.SetDoubleCellValue(spreadsheet, worksheet, 2, 5, 1.23, Nothing, True)

        '' Add a string
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 6, "Integer", False, False)
        '' Add an integer number
        'Excel.SetDoubleCellValue(spreadsheet, worksheet, 2, 6, 1, Nothing, True)

        '' Add a string
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 7, "Currency", False, False)
        '' Add currency
        'Excel.SetDoubleCellValue(spreadsheet, worksheet, 2, 7, 1.23, 2, True)

        '' Add a string
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 8, "Date", False, False)
        '' Add date
        'Excel.SetDateCellValue(spreadsheet, worksheet, 2, 8, System.DateTime.Now, 1, True)

        '' Add a string
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 9, "Percentage", False, False)
        '' Add percentage
        'Excel.SetDoubleCellValue(spreadsheet, worksheet, 2, 9, 0.123, 3, True)

        '' Add a string
        'Excel.SetStringCellValue(spreadsheet, worksheet, 1, 10, "Boolean", False, False)
        '' Add boolean
        'Excel.SetBooleanCellValue(spreadsheet, worksheet, 2, 10, True, Nothing, True)

        ' Set column widths
        Excel.SetColumnWidth(worksheet, 1, 15)

        worksheet.Save()
        spreadsheet.Close()

        System.Diagnostics.Process.Start(workbookName)
    End Sub

    ''' <summary>
    ''' Creates the workbook
    ''' </summary>
    ''' <returns>Spreadsheet created</returns>
    Public Shared Function CreateWorkbook(fileName As String) As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument
        Dim spreadSheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument = Nothing
        Dim sharedStringTablePart As DocumentFormat.OpenXml.Packaging.SharedStringTablePart
        Dim workbookStylesPart As DocumentFormat.OpenXml.Packaging.WorkbookStylesPart

        Try
            ' Create the Excel workbook
            spreadSheet = DocumentFormat.OpenXml.Packaging.SpreadsheetDocument.Create(fileName, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook, False)

            ' Create the parts and the corresponding objects

            ' Workbook
            spreadSheet.AddWorkbookPart()
            spreadSheet.WorkbookPart.Workbook = New DocumentFormat.OpenXml.Spreadsheet.Workbook()
            spreadSheet.WorkbookPart.Workbook.Save()

            ' Shared string table
            sharedStringTablePart = spreadSheet.WorkbookPart.AddNewPart(Of DocumentFormat.OpenXml.Packaging.SharedStringTablePart)()
            sharedStringTablePart.SharedStringTable = New DocumentFormat.OpenXml.Spreadsheet.SharedStringTable()
            sharedStringTablePart.SharedStringTable.Save()

            ' Sheets collection
            spreadSheet.WorkbookPart.Workbook.Sheets = New DocumentFormat.OpenXml.Spreadsheet.Sheets()
            spreadSheet.WorkbookPart.Workbook.Save()

            ' Stylesheet
            workbookStylesPart = spreadSheet.WorkbookPart.AddNewPart(Of DocumentFormat.OpenXml.Packaging.WorkbookStylesPart)()
            workbookStylesPart.Stylesheet = New DocumentFormat.OpenXml.Spreadsheet.Stylesheet()
            workbookStylesPart.Stylesheet.Save()
        Catch exception As System.Exception
            MessageBox.Show(exception.Message, "Excel Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try


        Return spreadSheet
    End Function

    ''' <summary>
    ''' Adds a new worksheet to the workbook
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="name">Name of the worksheet</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function AddWorksheet(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, name As String) As Boolean
        Dim sheets As DocumentFormat.OpenXml.Spreadsheet.Sheets = spreadsheet.WorkbookPart.Workbook.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.Sheets)()
        Dim sheet As DocumentFormat.OpenXml.Spreadsheet.Sheet
        Dim worksheetPart As DocumentFormat.OpenXml.Packaging.WorksheetPart

        If name = Nothing Then name = "Sheet" & sheets.Count + 1

        ' Add the worksheetpart
        worksheetPart = spreadsheet.WorkbookPart.AddNewPart(Of DocumentFormat.OpenXml.Packaging.WorksheetPart)()
        worksheetPart.Worksheet = New DocumentFormat.OpenXml.Spreadsheet.Worksheet(New DocumentFormat.OpenXml.Spreadsheet.SheetData())
        worksheetPart.Worksheet.Save()

        ' Add the sheet and make relation to workbook
        sheet = New DocumentFormat.OpenXml.Spreadsheet.Sheet With {
           .Id = spreadsheet.WorkbookPart.GetIdOfPart(worksheetPart),
           .SheetId = (spreadsheet.WorkbookPart.Workbook.Sheets.Count() + 1),
            .Name = name}
        sheets.Append(sheet)
        spreadsheet.WorkbookPart.Workbook.Save()

        Return True
    End Function

    ''' <summary>
    ''' Adds the basic styles to the workbook
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function AddBasicStyles(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument) As Boolean
        Dim stylesheet As DocumentFormat.OpenXml.Spreadsheet.Stylesheet = spreadsheet.WorkbookPart.WorkbookStylesPart.Stylesheet

        ' Numbering formats (x:numFmts)
        stylesheet.InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.NumberingFormats)(New DocumentFormat.OpenXml.Spreadsheet.NumberingFormats(), 0)
        ' Currency
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.NumberingFormats)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.NumberingFormat)(
           New DocumentFormat.OpenXml.Spreadsheet.NumberingFormat() With {
              .NumberFormatId = 164,
              .FormatCode = "#,##0.00" _
              & " """ & System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol & "  """
           }, 0)

        ' Fonts (x:fonts)
        stylesheet.InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.Fonts)(New DocumentFormat.OpenXml.Spreadsheet.Fonts(), 1)
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.Fonts)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.Font)(
           New DocumentFormat.OpenXml.Spreadsheet.Font() With {
              .FontSize = New DocumentFormat.OpenXml.Spreadsheet.FontSize() With {
                 .Val = 11
              },
              .FontName = New DocumentFormat.OpenXml.Spreadsheet.FontName() With {
                 .Val = "Calibri"
              }
           }, 0)

        ' Fills (x:fills)
        stylesheet.InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.Fills)(New DocumentFormat.OpenXml.Spreadsheet.Fills(), 2)
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.Fills)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.Fill)(
           New DocumentFormat.OpenXml.Spreadsheet.Fill() With {
              .PatternFill = New DocumentFormat.OpenXml.Spreadsheet.PatternFill() With {
                 .PatternType = New DocumentFormat.OpenXml.EnumValue(Of DocumentFormat.OpenXml.Spreadsheet.PatternValues)() With {
                     .Value = DocumentFormat.OpenXml.Spreadsheet.PatternValues.None
                 }
              }
           }, 0)

        ' Borders (x:borders)
        stylesheet.InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.Borders)(New DocumentFormat.OpenXml.Spreadsheet.Borders(), 3)
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.Borders)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.Border)(
           New DocumentFormat.OpenXml.Spreadsheet.Border() With {
              .LeftBorder = New DocumentFormat.OpenXml.Spreadsheet.LeftBorder(),
              .RightBorder = New DocumentFormat.OpenXml.Spreadsheet.RightBorder(),
              .TopBorder = New DocumentFormat.OpenXml.Spreadsheet.TopBorder(),
              .BottomBorder = New DocumentFormat.OpenXml.Spreadsheet.BottomBorder(),
              .DiagonalBorder = New DocumentFormat.OpenXml.Spreadsheet.DiagonalBorder()
           }, 0)

        ' Cell style formats (x:CellStyleXfs)
        stylesheet.InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.CellStyleFormats)(New DocumentFormat.OpenXml.Spreadsheet.CellStyleFormats(), 4)
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.CellStyleFormats)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.CellFormat)(
           New DocumentFormat.OpenXml.Spreadsheet.CellFormat() With {
              .NumberFormatId = 0,
              .FontId = 0,
              .FillId = 0,
              .BorderId = 0
           }, 0)

        ' Cell formats (x:CellXfs)
        stylesheet.InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.CellFormats)(New DocumentFormat.OpenXml.Spreadsheet.CellFormats(), 5)
        ' General text
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.CellFormats)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.CellFormat)(
           New DocumentFormat.OpenXml.Spreadsheet.CellFormat() With {
              .FormatId = 0,
              .NumberFormatId = 0
           }, 0)
        ' Date
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.CellFormats)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.CellFormat)(
           New DocumentFormat.OpenXml.Spreadsheet.CellFormat() With {
              .ApplyNumberFormat = True,
              .FormatId = 0,
              .NumberFormatId = 22,
              .FontId = 0,
              .FillId = 0,
              .BorderId = 0
           }, 1)
        ' Currency
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.CellFormats)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.CellFormat)(
           New DocumentFormat.OpenXml.Spreadsheet.CellFormat() With {
              .ApplyNumberFormat = True,
              .FormatId = 0,
              .NumberFormatId = 164,
              .FontId = 0,
              .FillId = 0,
              .BorderId = 0
           }, 2)
        ' Percentage
        stylesheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.CellFormats)().InsertAt(Of DocumentFormat.OpenXml.Spreadsheet.CellFormat)(
           New DocumentFormat.OpenXml.Spreadsheet.CellFormat() With {
              .ApplyNumberFormat = True,
              .FormatId = 0,
              .NumberFormatId = 10,
              .FontId = 0,
              .FillId = 0,
              .BorderId = 0
           }, 3)

        stylesheet.Save()

        Return True
    End Function

    ''' <summary>
    ''' Adds a list of strings to the shared strings table.
    ''' </summary>
    ''' <param name="spreadsheet">The spreadsheet</param>
    ''' <param name="stringList">Strings to add</param>
    ''' <returns></returns>
    Public Shared Function AddSharedStrings(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, stringList As System.Collections.Generic.List(Of String)) As Boolean
        For Each item As String In stringList
            Excel.AddSharedString(spreadsheet, item, False)
        Next
        spreadsheet.WorkbookPart.SharedStringTablePart.SharedStringTable.Save()

        Return True
    End Function

    ''' <summary>
    ''' Add a single string to shared strings table.
    ''' Shared string table is created if it doesn't exist.
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="stringItem">string to add</param>
    ''' <param name="save">Save the shared string table</param>
    ''' <returns></returns>
    Public Shared Function AddSharedString(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, stringItem As String, Optional save As Boolean = True) As Boolean
        Dim sharedStringTable As DocumentFormat.OpenXml.Spreadsheet.SharedStringTable = spreadsheet.WorkbookPart.SharedStringTablePart.SharedStringTable

        Dim stringQuery = (From item In sharedStringTable
                          Where item.InnerText = stringItem
                          Select item).Count()

        If 0 = stringQuery Then
            sharedStringTable.AppendChild(
               New DocumentFormat.OpenXml.Spreadsheet.SharedStringItem(
                  New DocumentFormat.OpenXml.Spreadsheet.Text(stringItem)))

            ' Save the changes
            If save Then
                sharedStringTable.Save()
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Returns the index of a shared string.
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="stringItem">String to search for</param>
    ''' <returns>Index of a shared string. -1 if not found</returns>
    Public Shared Function IndexOfSharedString(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, stringItem As String) As Int32
        Dim sharedStringTable As DocumentFormat.OpenXml.Spreadsheet.SharedStringTable = spreadsheet.WorkbookPart.SharedStringTablePart.SharedStringTable
        Dim found As Boolean = False
        Dim index As Int32 = 0

        For Each sharedString As DocumentFormat.OpenXml.Spreadsheet.SharedStringItem In sharedStringTable.Elements(Of DocumentFormat.OpenXml.Spreadsheet.SharedStringItem)()
            If sharedString.InnerText = stringItem Then
                found = True
                Exit For
            End If
            index = index + 1
        Next

        If found Then
            Return index
        Else
            Return -1
        End If
    End Function

    ''' <summary>
    ''' Converts a column number to column name (i.e. A, B, C..., AA, AB...)
    ''' </summary>
    ''' <param name="columnIndex">Index of the column</param>
    ''' <returns>Column name</returns>
    Public Shared Function ColumnNameFromIndex(columnIndex As UInt32) As String
        Dim remainder As UInt32
        Dim columnName As String = ""

        While (columnIndex > 0)
            remainder = (columnIndex - 1) Mod 26
            columnName = System.Convert.ToChar(65 + remainder).ToString() + columnName
            columnIndex = ((columnIndex - remainder) / 26)
        End While

        Return columnName
    End Function

    ''' <summary>
    ''' Sets a string value to a cell
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="worksheet">Worksheet to use</param>
    ''' <param name="columnIndex">Index of the column</param>
    ''' <param name="rowIndex">Index of the row</param>
    ''' <param name="stringValue">String value to set</param>
    ''' <param name="useSharedString">Use shared strings? If true and the string isn't found in shared strings, it will be added</param>
    ''' <param name="save">Save the worksheet</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function SetStringCellValue(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, worksheet As DocumentFormat.OpenXml.Spreadsheet.Worksheet, columnIndex As UInt32, rowIndex As UInt32, stringValue As String, useSharedString As Boolean, Optional save As Boolean = True) As Boolean
        Dim columnValue As String = stringValue
        Dim cellValueType As DocumentFormat.OpenXml.Spreadsheet.CellValues

        ' Add the shared string if necessary
        If (useSharedString) Then
            If (Excel.IndexOfSharedString(spreadsheet, stringValue) = -1) Then
                Excel.AddSharedString(spreadsheet, stringValue, True)
            End If
            columnValue = Excel.IndexOfSharedString(spreadsheet, stringValue).ToString()
            cellValueType = DocumentFormat.OpenXml.Spreadsheet.CellValues.SharedString
        Else
            cellValueType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String
        End If

        Return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, cellValueType, columnValue, Nothing, save)
    End Function

    Public Shared Function FormatValue(value As Object, Style As DocumentFormat.OpenXml.Spreadsheet.CellValues) As String
        Dim strValue As String = ""
        If IsDBNull(value) Then
            strValue = ""
            Return strValue
        End If
        If Style = Spreadsheet.CellValues.String Then
            strValue = value
        ElseIf Style = Spreadsheet.CellValues.Boolean Then
            If value Then
                strValue = "1"
            Else
                strValue = "0"
            End If
        ElseIf Style = Spreadsheet.CellValues.Date Then
#If EN_US_CULTURE Then
            strvalue = value.ToOADate().ToString()
#Else
            strValue = value.ToOADate().ToString().Replace(",", ".")
#End If
        ElseIf Style = Spreadsheet.CellValues.Number Then
#If EN_US_CULTURE Then
            strvalue = value.ToString()
#Else
            strValue = value.ToString().Replace(",", ".")
#End If
        End If
        Return strValue
    End Function

    ''' <summary>
    ''' Sets a cell value with a date
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="worksheet">Worksheet to use</param>
    ''' <param name="columnIndex">Index of the column</param>
    ''' <param name="rowIndex">Index of the row</param>
    ''' <param name="datetimeValue">DateTime value</param>
    ''' <param name="styleIndex">Style to use</param>
    ''' <param name="save">Save the worksheet</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function SetDateCellValue(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, worksheet As DocumentFormat.OpenXml.Spreadsheet.Worksheet, columnIndex As UInt32, rowIndex As UInt32, datetimeValue As System.DateTime, styleIndex As UInt32?, Optional save As Boolean = True) As Boolean
#If EN_US_CULTURE Then
        Dim columnValue As String = datetimeValue.ToOADate().ToString()
#Else
        Dim columnValue As String = datetimeValue.ToOADate().ToString().Replace(",", ".")
#End If

        Return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, DocumentFormat.OpenXml.Spreadsheet.CellValues.Date, columnValue, styleIndex, save)
    End Function

    ''' <summary>
    ''' Sets a cell value with double number
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="worksheet">Worksheet to use</param>
    ''' <param name="columnIndex">Index of the column</param>
    ''' <param name="rowIndex">Index of the row</param>
    ''' <param name="doubleValue">Double value</param>
    ''' <param name="styleIndex">Style to use</param>
    ''' <param name="save">Save the worksheet</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function SetDoubleCellValue(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, worksheet As DocumentFormat.OpenXml.Spreadsheet.Worksheet, columnIndex As UInt32, rowIndex As UInt32, doubleValue As Double, styleIndex As UInt32?, Optional save As Boolean = True) As Boolean
#If EN_US_CULTURE Then
        Dim columnValue As String = doubleValue.ToString()
#Else
        Dim columnValue As String = doubleValue.ToString().Replace(",", ".")
#End If

        Return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, DocumentFormat.OpenXml.Spreadsheet.CellValues.Number, columnValue, styleIndex, save)
    End Function

    ''' <summary>
    ''' Sets a cell value with boolean value
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="worksheet">Worksheet to use</param>
    ''' <param name="columnIndex">Index of the column</param>
    ''' <param name="rowIndex">Index of the row</param>
    ''' <param name="boolValue">Boolean value</param>
    ''' <param name="styleIndex">Style to use</param>
    ''' <param name="save">Save the worksheet</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function SetBooleanCellValue(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, worksheet As DocumentFormat.OpenXml.Spreadsheet.Worksheet, columnIndex As UInt32, rowIndex As UInt32, boolValue As Boolean, styleIndex As UInt32?, Optional save As Boolean = True) As Boolean
        Dim columnValue As String
        If boolValue Then
            columnValue = "1"
        Else
            columnValue = "0"
        End If

        Return SetCellValue(spreadsheet, worksheet, columnIndex, rowIndex, DocumentFormat.OpenXml.Spreadsheet.CellValues.Boolean, columnValue, styleIndex, save)
    End Function


    ''' <summary>
    ''' Sets the column width
    ''' </summary>
    ''' <param name="worksheet">Worksheet to use</param>
    ''' <param name="columnIndex">Index of the column</param>
    ''' <param name="width">Width to set</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function SetColumnWidth(worksheet As DocumentFormat.OpenXml.Spreadsheet.Worksheet, columnIndex As Int32, width As Int32) As Boolean
        Dim columns As DocumentFormat.OpenXml.Spreadsheet.Columns
        Dim column As DocumentFormat.OpenXml.Spreadsheet.Column

        ' Get the column collection exists
        columns = worksheet.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Columns)().FirstOrDefault()
        If (columns Is Nothing) Then
            Return False
        End If
        ' Get the column DocumentFormat.OpenXml.UInt32Value.FromUInt32(System.Convert.ToUInt32(
        Dim colQuery = From item In columns.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Column)()
                       Where DocumentFormat.OpenXml.UInt32Value.ToUInt32(item.Min) = columnIndex
                       Select item
        column = colQuery.FirstOrDefault()
        'column = columns.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Column)().Where(Function(item) item.Min = columnIndex).FirstOrDefault()
        If (columns Is Nothing) Then
            Return False
        End If
        column.Width = width
        column.CustomWidth = True

        worksheet.Save()

        Return True
    End Function

    ''' <summary>
    ''' Sets a cell value. The row and the cell are created if they do not exist. If the cell exists, the contents of the cell is overwritten
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="worksheet">Worksheet to use</param>
    ''' <param name="columnIndex">Index of the column</param>
    ''' <param name="rowIndex">Index of the row</param>
    ''' <param name="valueType">Type of the value</param>
    ''' <param name="value">The actual value</param>
    ''' <param name="styleIndex">Index of the style to use. Null if no style is to be defined</param>
    ''' <param name="save">Save the worksheet?</param>
    ''' <returns>True if succesful</returns>
    Private Shared Function SetCellValue(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, worksheet As DocumentFormat.OpenXml.Spreadsheet.Worksheet, columnIndex As UInt32, rowIndex As UInt32, valueType As DocumentFormat.OpenXml.Spreadsheet.CellValues, value As String, styleIndex As UInt32?, Optional save As Boolean = True) As Boolean
        Dim sheetData As DocumentFormat.OpenXml.Spreadsheet.SheetData = worksheet.GetFirstChild(Of DocumentFormat.OpenXml.Spreadsheet.SheetData)()
        Dim row As DocumentFormat.OpenXml.Spreadsheet.Row
        Dim previousRow As DocumentFormat.OpenXml.Spreadsheet.Row = Nothing
        Dim cell As DocumentFormat.OpenXml.Spreadsheet.Cell
        Dim previousCell As DocumentFormat.OpenXml.Spreadsheet.Cell = Nothing
        Dim columns As DocumentFormat.OpenXml.Spreadsheet.Columns
        Dim previousColumn As DocumentFormat.OpenXml.Spreadsheet.Column = Nothing
        Dim cellAddress As String = Excel.ColumnNameFromIndex(columnIndex) & rowIndex
        Dim loopCounter As Int32

        ' Check if the row exists, create if necessary
        Dim rowQuery = From item In sheetData.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Row)()
                       Where DocumentFormat.OpenXml.UInt32Value.ToUInt32(item.RowIndex) = rowIndex
                       Select item
        If rowQuery.Count() <> 0 Then
            row = rowQuery.First()
        Else
            row = New DocumentFormat.OpenXml.Spreadsheet.Row() With {
                .RowIndex = rowIndex
            }
            Dim prevRowQuery = From item In sheetData.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Row)()
                               Where DocumentFormat.OpenXml.UInt32Value.ToUInt32(item.RowIndex) = loopCounter
                               Select item
            For counter As Int32 = rowIndex - 1 To 1 Step -1
                loopCounter = counter
                previousRow = prevRowQuery.FirstOrDefault()
                If Not (previousRow Is Nothing) Then
                    Exit For
                End If
            Next
            sheetData.InsertAfter(row, previousRow)
        End If

        ' Check if the cell exists, create if necessary
        Dim cellQuery = From item In row.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Cell)()
                        Where item.CellReference.Value = cellAddress
                        Select item
        If (cellQuery.Count() > 0) Then
            cell = cellQuery.First()
        Else
            ' Find the previous existing cell in the row
            Dim prevCellQuery = From item In row.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Cell)()
                                Where item.CellReference.Value = Excel.ColumnNameFromIndex(loopCounter) & rowIndex
            For counter As Int32 = columnIndex - 1 To 1 Step -1
                loopCounter = counter
                previousCell = prevCellQuery.FirstOrDefault()
                If Not (previousCell Is Nothing) Then
                    Exit For
                End If
            Next
            cell = New DocumentFormat.OpenXml.Spreadsheet.Cell() With {
                .CellReference = cellAddress
            }
            row.InsertAfter(cell, previousCell)
        End If

        ' Check if the column collection exists
        columns = worksheet.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Columns)().FirstOrDefault()
        If (columns Is Nothing) Then
            columns = worksheet.InsertAt(New DocumentFormat.OpenXml.Spreadsheet.Columns(), 0)
        End If
        ' Check if the column exists
        Dim colQuery = From item In columns.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Column)()
                       Where DocumentFormat.OpenXml.UInt32Value.ToUInt32(item.Min) = columnIndex
                       Select item
        If colQuery.Count() = 0 Then
            ' Find the previous existing column in the columns
            Dim prevColQuery = From item In columns.Elements(Of DocumentFormat.OpenXml.Spreadsheet.Column)()
                   Where DocumentFormat.OpenXml.UInt32Value.ToUInt32(item.Min) = loopCounter
                   Select item
            For counter As Int32 = columnIndex - 1 To 1 Step -1
                loopCounter = counter
                previousColumn = prevColQuery.FirstOrDefault()
                If Not (previousColumn Is Nothing) Then
                    Exit For
                End If
            Next
            columns.InsertAfter(
               New DocumentFormat.OpenXml.Spreadsheet.Column() With {
                  .Min = columnIndex,
                  .Max = columnIndex,
                  .CustomWidth = True,
                  .Width = 9
               }, previousColumn)
        End If

        ' Add the value
        cell.CellValue = New DocumentFormat.OpenXml.Spreadsheet.CellValue(value)
        If Not (styleIndex Is Nothing) Then
            cell.StyleIndex = styleIndex
        End If
        If (valueType <> DocumentFormat.OpenXml.Spreadsheet.CellValues.Date) Then
            cell.DataType = New DocumentFormat.OpenXml.EnumValue(Of DocumentFormat.OpenXml.Spreadsheet.CellValues)(valueType)
        End If

        If (save) Then
            worksheet.Save()
        End If

        Return True
    End Function

    ''' <summary>
    ''' Adds a predefined style from the given xml
    ''' </summary>
    ''' <param name="spreadsheet">Spreadsheet to use</param>
    ''' <param name="xml">Style definition as xml</param>
    ''' <returns>True if succesful</returns>
    Public Shared Function AddPredefinedStyles(spreadsheet As DocumentFormat.OpenXml.Packaging.SpreadsheetDocument, xml As String) As Boolean
        spreadsheet.WorkbookPart.WorkbookStylesPart.Stylesheet.InnerXml = xml
        spreadsheet.WorkbookPart.WorkbookStylesPart.Stylesheet.Save()

        Return True
    End Function
End Class
