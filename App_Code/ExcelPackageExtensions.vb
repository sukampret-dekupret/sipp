Imports System.Collections.Generic
Imports System.Linq
Imports OfficeOpenXml
Imports System.Data
Imports System.Runtime.CompilerServices


''' <summary>
''' Summary description for ExcelPackageExtenstions
''' </summary>
Public Module ExcelPackageExtensions
    <Extension()>
    Public Function ToDataTable(ByVal package As ExcelPackage) As DataTable
        Dim workSheet As ExcelWorksheet = package.Workbook.Worksheets.First()
        Dim table As DataTable = New DataTable()

        For Each firstRowCell In workSheet.Cells(1, 1, 1, workSheet.Dimension.End.Column)
            table.Columns.Add(firstRowCell.Text)
        Next

        For rowNumber = 2 To workSheet.Dimension.End.Row
            Dim row = workSheet.Cells(rowNumber, 1, rowNumber, workSheet.Dimension.End.Column)
            Dim newRow = table.NewRow()

            For Each cell In row
                newRow(cell.Start.Column - 1) = cell.Text
            Next

            table.Rows.Add(newRow)
        Next

        Return table
    End Function

    <Extension()>
    Public Function ExcelPackageToDataTable(ByVal excelPackage As ExcelPackage) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim worksheet As ExcelWorksheet = excelPackage.Workbook.Worksheets(1)


        'check if the worksheet is completely empty
        If worksheet.Dimension Is Nothing Then
            Return dt
        End If


        'create a list to hold the column names
        Dim columnNames As List(Of String) = New List(Of String)()

        'needed to keep track of empty column headers
        Dim currentColumn As Integer = 1


        'loop all columns in the sheet and add them to the datatable
        For Each cell In worksheet.Cells(1, 1, 1, worksheet.Dimension.End.Column)
            Dim columnName As String = cell.Text.Trim()


            'check if the previous header was empty and add it if it was
            If cell.Start.Column <> currentColumn Then
                columnNames.Add("Header_" & currentColumn)
                dt.Columns.Add("Header_" & currentColumn)
                currentColumn += 1
            End If


            'add the column name to the list to count the duplicates
            columnNames.Add(columnName)

            'count the duplicate column names and make them unique to avoid the exception
            'A column named 'Name' already belongs to this DataTable
            'Dim occurrences As Integer = columnNames.Count(Function(x) x.Equals(columnName))

            'If occurrences > 1 Then
            '    columnName = columnName & "_" & occurrences
            'End If


            'add the column to the datatable
            dt.Columns.Add(columnName)
            currentColumn += 1
        Next


        'start adding the contents of the excel file to the datatable
        For i As Integer = 2 To worksheet.Dimension.End.Row
            Dim row = worksheet.Cells(i, 1, i, worksheet.Dimension.End.Column)
            Dim newRow As DataRow = dt.NewRow()


            'loop all cells in the row
            For Each cell In row
                newRow(cell.Start.Column - 1) = cell.Text
            Next

            dt.Rows.Add(newRow)
        Next

        Return dt
    End Function
End Module
