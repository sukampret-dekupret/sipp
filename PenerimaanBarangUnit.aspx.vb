Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports OfficeOpenXml
Partial Class PenerimaanBarangUnit
    Inherits System.Web.UI.Page

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim xFungsi As New FungsiData
        If txtTglFaktur.Text = "" Then
            txtTglFaktur.Focus()
        ElseIf grid.VisibleRowCount = 0 Then
            grid.Focus()
        ElseIf txtPerihal.Text = "" Then
            txtPerihal.Focus()

        Else

            sqlPenerimaan.Insert()

            xFungsi.UpdateData("TBL_PENERIMAAN_BRG_DTL", "KET=1,TGL_DISTRIBUSI=GETDATE(),NAMAPROSES='" & Session("NAMAUSER") & "',NAMADISTRIBUSI='" & Session("NAMAUSER") & "'", " IDPENERIMAAN='" & Session("IDPENERIMAAN") & "'")
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Saldo Awal sudah disimpan')", True)
            Session.Remove("IDPENERIMAAN")
            Session("IDPENERIMAAN") = Guid.NewGuid().ToString
            txtPerihal.Text = ""
            txtTglFaktur.Value = Date.Now
        End If

    End Sub
    Private Sub PenerimaanBarangUnit_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            Dim xData As New FungsiData
            Dim xNamaSatker As String = xData.GetData("NAMA_SATKER", "TBL_SATKER", "KD_SATKER='" & Session("KDUNKER") & "'")
            xData.delData("TBL_PENERIMAAN_BRG_DTL", " WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
            Session("IDPENERIMAAN") = Guid.NewGuid().ToString
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False
            txtTglFaktur.Value = CDate("10/31/2020")
            txtPerihal.Text = "Penerimaan Saldo Awal " & xNamaSatker & " as 31 Oktober 2020"
        End If
    End Sub

    Protected Sub grid_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("JML") = 1
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim filename As String = Server.MapPath("~/Files/") + FileUpload1.PostedFile.FileName
        FileUpload1.PostedFile.SaveAs(filename)
        Dim excel As FileInfo = New FileInfo(filename)
        Dim package = New ExcelPackage(excel)
        Dim workbook = package.Workbook
        Dim worksheet = workbook.Worksheets.First()
        Dim dt As DataTable = ConvertToDataTable(worksheet)
        dt.Columns.Add("IDPENERIMAAN", GetType(String))
        dt.Columns.Add("NAMAUSER", GetType(String))
        dt.Columns.Add("KDUNKER", GetType(String))

        For Each row As DataRow In dt.Rows
            row("IDPENERIMAAN") = Session("IDPENERIMAAN")
            row("NAMAUSER") = Session("NAMAUSER")
            row("KDUNKER") = cboUnitKerja.SelectedValue
        Next

        'ASPxGridView1.DataSource = dt
        'ASPxGridView1.DataBind()


        Dim xTable As String = "TBL_PENERIMAAN_BRG_DTL"

        'xTable.Name = "TBL_SPK_DETAIL"
        Dim CS As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString

        Using conn = New SqlConnection(CS)
            Dim bulkCopy = New SqlBulkCopy(conn)
            bulkCopy.DestinationTableName = xTable
            conn.Open()
            'Dim truncate As SqlCommand = New SqlCommand("TRUNCATE TABLE TBL_IMPORT_DATA", conn)
            'truncate.ExecuteNonQuery()
            Dim schema = conn.GetSchema("Columns", {Nothing, Nothing, xTable, Nothing})

            For Each sourceColumn As DataColumn In dt.Columns

                For Each row As DataRow In schema.Rows

                    If String.Equals(sourceColumn.ColumnName, CStr(row("COLUMN_NAME")), StringComparison.OrdinalIgnoreCase) Then
                        bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, CStr(row("COLUMN_NAME")))
                        Exit For
                    End If
                Next
            Next
            bulkCopy.WriteToServer(dt)
        End Using
        grid.DataBind()
    End Sub

    Private Function ConvertToDataTable(ByVal oSheet As ExcelWorksheet) As DataTable
        Dim totalRows As Integer = oSheet.Dimension.End.Row
        Dim totalCols As Integer = oSheet.Dimension.End.Column
        Dim dt As DataTable = New DataTable(oSheet.Name)
        Dim dr As DataRow = Nothing
        Dim i As Integer = 1
        Do While (i <= totalRows)
            If (i > 1) Then
                dr = dt.Rows.Add
            End If

            Dim j As Integer = 1
            Do While (j <= totalCols)
                If (i = 1) Then
                    dt.Columns.Add(oSheet.Cells(i, j).Value.ToString)
                Else
                    dr((j - 1)) = oSheet.Cells(i, j).Value.ToString
                End If

                j = (j + 1)
            Loop

            i = (i + 1)
        Loop

        Return dt
    End Function
    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Dim xTombol As LinkButton = CType(sender, LinkButton)
        Dim xFileName As String = "TemplateSaldoAwal.xlsx"
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files"))

        If File.Exists(directoryPath & xFileName) Then
            Response.ContentType = "application/xlsx"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        End If
    End Sub
End Class
