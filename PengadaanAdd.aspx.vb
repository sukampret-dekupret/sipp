Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports OfficeOpenXml
Imports OfficeOpenXml.Table
Partial Class PengadaanAdd
    Inherits System.Web.UI.Page


    Protected Sub grid_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("JML") = 10
        e.NewValues("HARGA") = 0
        e.NewValues("TOTAL") = e.NewValues("JML") * e.NewValues("HARGA")
    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Dim xFungsi As New FungsiData
            If txtNoFaktur.Text = "" Then
                txtNoFaktur.Focus()
            ElseIf grid.VisibleRowCount = 0 Then
                grid.Focus()
            ElseIf txtPerihal.Text = "" Then
                txtPerihal.Focus()
            ElseIf cboMitra.Value.ToString = "" Then
                cboMitra.Focus()
            ElseIf xFileUpload.HasFile = False Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('File Masih kosong')", True)

            Else
                Dim xFUName As String = Session("IDSPK").ToString & Path.GetExtension(xFileUpload.PostedFile.FileName)
                Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/SPK/" & Year(txtTglFaktur.Value)))
                Session("URLFILE") = directoryPath & "/" & xFUName

                sqlspk.Insert()
                xFungsi.UpdateData("TBL_SPK_DETAIL", "KET=1,CEK=1,TGL_PROSES=GETDATE()", " IDSPK='" & Session("IDSPK") & "'")

                Try
                    If xFileUpload.HasFile Then

                        If Not Directory.Exists(directoryPath) Then
                            Directory.CreateDirectory(directoryPath)
                            xFileUpload.SaveAs(directoryPath & "\" & xFUName)

                        Else
                            xFileUpload.SaveAs(directoryPath & "\" & xFUName)
                        End If
                    End If
                Catch ex As Exception

                End Try

                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('SPK anda telah berhasil disimpan')", True)

                Session.Remove("IDSPK")
                Session("IDSPK") = Guid.NewGuid().ToString
                txtPerihal.Text = ""
                txtNoFaktur.Text = ""
                txtTglFaktur.Value = Date.Now
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan penyimpanan')", True)
        End If

        '----- skrip lama
        'Dim sqlQuery As New StringBuilder()
        'Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        'Dim conn As New SqlConnection()
        'conn.ConnectionString = strConnString
        'sqlQuery.Clear()
        'sqlQuery.Append("INSERT INTO [dbo].[TBL_SPK]([IDSPK], [NOFAKTUR], [TGL_FAKTUR], [PERIHAL],[KODE_MITRA],[KDUNKER],[URLFILE],[NAMAUSER]) VALUES( ")
        'sqlQuery.Append("'" & Session("IDSPK") & "', ")
        'sqlQuery.Append("'" & txtFaktur.Value & "',")
        'sqlQuery.Append("'" & txtTglFaktur.Value & "',")
        'sqlQuery.Append("'" & txtPerihal.Value & "',")
        'sqlQuery.Append("" & cboMitra.Value & ", ")
        'sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "', ")
        'sqlQuery.Append("'" & xFileLengkap & "', ")
        'sqlQuery.Append("'" & Session("NAMAUSER").ToString & "')")


        'conn.Open()
        'Dim cmd As New SqlCommand
        'cmd = New SqlCommand(sqlQuery.ToString())
        'cmd.Connection = conn
        'Dim i As Integer = cmd.ExecuteNonQuery()
    End Sub

    Protected Sub btnDokKonsuler_Click(sender As Object, e As EventArgs)
        Try
            cboMitra.Value = 24
            Dim sqlQuery As New StringBuilder()
            'Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
            'Dim conn As New SqlConnection()
            'conn.ConnectionString = strConnString

            Dim xFungsi As New FungsiData
            xFungsi.delData("TBL_SPK_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
            sqlQuery.Clear()
            sqlQuery.Append("INSERT INTO [TBL_SPK_DETAIL] ( [IDSPK], [KODE_BARANG], [JML],[HARGA], [NAMAUSER], [KDUNKER]) SELECT  ")
            sqlQuery.Append("'" & Session("IDSPK") & "', ")
            sqlQuery.Append("KODE_BARANG,")
            sqlQuery.Append("ISNULL(dbo.getBeliTerakhir(KODE_BARANG),1) AS JML,")
            sqlQuery.Append("ISNULL(dbo.getHargaTerakhir(KODE_BARANG),1) AS HARGA,")
            sqlQuery.Append("'" & Session("NAMAUSER").ToString & "',")
            sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "' ")
            sqlQuery.Append(" FROM TBL_BARANG WHERE KODE_GROUP=1 ")

            xFungsi.insertData(sqlQuery.ToString())
            grid.DataBind()

        Catch ex As Exception

        End Try



    End Sub
    Protected Sub btnBelumDipenuhi_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PengadaanAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then

            'If Session("UbahSPK") = True Then
            '    Session("IDSPK") = Session("IDSPKUBAH")
            '    Dim xFungsi As New FungsiData
            '    txtNoFaktur.Text = xFungsi.GetData("NOFAKTUR", "TBL_SPK", " IDSPK='" & Session("IDSPK") & "'")
            '    txtPerihal.Text = xFungsi.GetData("PERIHAL", "TBL_SPK", " IDSPK='" & Session("IDSPK") & "'")
            '    txtTglFaktur.Value = xFungsi.GetData("TGL_FAKTUR", "TBL_SPK", " IDSPK='" & Session("IDSPK") & "'")
            '    cboMitra.DataBind()
            '    'cboMitra.SelectedItem = cboMitra.Items.FindByValue()
            '    'cboMitra.SelectedItem.Value = cboMitra.Items.FindByValue(xFungsi.GetData("KODE_MITRA", "TBL_SPK", " IDSPK='" & Session("IDSPK") & "'"))
            '    cboMitra.SelectedIndex = cboMitra.Items.IndexOf(cboMitra.Items.FindByValue(xFungsi.GetData("KODE_MITRA", "TBL_SPK", " IDSPK='" & Session("IDSPK") & "'")))
            'Else
            Dim xFungsi As New FungsiData
            xFungsi.delData("TBL_SPK_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")

            Session("IDSPK") = Guid.NewGuid().ToString
            txtTglFaktur.Value = Date.Now
            'End If

            'cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False



        End If
    End Sub
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        'Dim excelConnectionString As String = String.Empty
        'Dim uploadPath As String = "~/Files/"
        'Dim filePath As String = Server.MapPath(uploadPath + FileUpload1.PostedFile.FileName)
        'Dim fileExt As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
        'Dim strConnection As [String] = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        'If fileExt = ".xls" OrElse fileExt = "XLS" Then
        '    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='" & filePath & "'" & "; Extended Properties ='Excel 8.0;HDR=Yes'"
        'ElseIf fileExt = ".xlsx" OrElse fileExt = "XLSX" Then
        '    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filePath & ";Extended Properties=Excel 12.0;Persist Security Info=False"
        'End If
        'Dim excelConnection As New OleDbConnection(excelConnectionString)
        'Dim cmd As New OleDbCommand("Select * from [Sheet1$]", excelConnection)
        'excelConnection.Open()
        'Dim dReader As OleDbDataReader
        'dReader = cmd.ExecuteReader()
        'Dim sqlBulk As New SqlBulkCopy(strConnection)
        'sqlBulk.DestinationTableName = "TBL_SPK_DETAIL"
        'sqlBulk.WriteToServer(dReader)

        Dim filename As String = Server.MapPath("~/Files/") + FileUpload1.PostedFile.FileName
        FileUpload1.PostedFile.SaveAs(filename)
        Dim excel As FileInfo = New FileInfo(filename)
        Dim package = New ExcelPackage(excel)
        Dim workbook = package.Workbook
        Dim worksheet = workbook.Worksheets.First()
        Dim dt As DataTable = ConvertToDataTable(worksheet)
        dt.Columns.Add("IDSPK", GetType(String))
        dt.Columns.Add("NAMAUSER", GetType(String))
        dt.Columns.Add("KDUNKER", GetType(String))

        For Each row As DataRow In dt.Rows
            row("IDSPK") = Session("IDSPK")
            row("NAMAUSER") = Session("NAMAUSER")
            row("KDUNKER") = cboUnitKerja.SelectedValue
        Next

        'ASPxGridView1.DataSource = dt
        'ASPxGridView1.DataBind()


        Dim xTable As String = "TBL_SPK_DETAIL"

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

    'If FileUpload1.HasFile Then
    ''load the uploaded file into the memorystream
    'Dim stream As MemoryStream = New MemoryStream(FileUpload1.FileBytes)
    'Dim excelPackage As ExcelPackage = New ExcelPackage(stream)
    ''loop all worksheets
    'For Each worksheet As ExcelWorksheet In excelPackage.Workbook.Worksheets
    ''loop all rows
    'Dim i As Integer = worksheet.Dimension.Start.Row
    'Do While (i <= worksheet.Dimension.End.Row)
    ''loop all columns in a row
    'Dim j As Integer = worksheet.Dimension.Start.Column
    'Do While (j <= worksheet.Dimension.End.Column)
    ''add the cell data to the List
    'If (Not (worksheet.Cells(i, j).Value) Is Nothing) Then
    '                excelData.Add(worksheet.Cells(i, j).Value.ToString)
    '            End If

    '            j = (j + 1)
    '        Loop

    '        i = (i + 1)
    '    Loop

    'Next
    'End If
    Protected Sub btnBarang_Click(sender As Object, e As EventArgs)
        Dim xFungsi As New FungsiData
        Dim sqlQuery As New StringBuilder()
        xFungsi.delData("TBL_SPK_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
        sqlQuery.Clear()
        sqlQuery.Append("INSERT INTO [TBL_SPK_DETAIL] ( [IDSPK], [KODE_BARANG], [JML],[HARGA], [NAMAUSER], [KDUNKER]) SELECT  ")
        sqlQuery.Append("'" & Session("IDSPK") & "', ")
        sqlQuery.Append("KODE_BARANG,")
        sqlQuery.Append("(dbo.getJMLPENUHIORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1,0)) AS JML,")
        sqlQuery.Append("ISNULL(dbo.getHargaTerakhir(KODE_BARANG),1) AS HARGA,")
        sqlQuery.Append("'" & Session("NAMAUSER").ToString & "',")
        sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "' ")
        sqlQuery.Append(" FROM TBL_BARANG WHERE ST_BARANG=1 AND (dbo.getJMLPENUHIORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1,0)) > 0")
        xFungsi.insertData(sqlQuery.ToString())
        grid.DataBind()
    End Sub
    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Dim xTombol As LinkButton = CType(sender, LinkButton)
        Dim xFileName As String = "TemplatePengadaan.xlsx"
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files"))

        If File.Exists(directoryPath & xFileName) Then
            Response.ContentType = "application/xlsx"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        End If
    End Sub
End Class
