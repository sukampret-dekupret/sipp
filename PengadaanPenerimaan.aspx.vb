Imports System.Data.SqlClient
'Imports System.Globalization
Imports System.IO
Imports DevExpress.Web
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Partial Class PengadaanPenerimaan
    Inherits System.Web.UI.Page

    Private Sub PengadaanPenerimaan_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If (Not IsPostBack) Then

            cboTahun.SelectedValue = Year(Date.Now)
            cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False
            'xTanggal1.Value = Date.Now
            'xTanggal2.Value = Date.Now

        End If
    End Sub

    Protected Sub btnTT_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xSTS As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "STATUS").ToString)
        Dim xFungsi As New FungsiData

        If xSTS = 2 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang sudah diterima semua!')", True)
        Else
            Session("IDspkDRPROSESspk") = xPK
            'Label1.Text = xPK
            Response.Redirect("PengadaanTandaTerima.aspx")
        End If
    End Sub
    Protected Sub grid_DataBound(sender As Object, e As EventArgs)
        CType(sender, ASPxGridView).DetailRows.ExpandAllRows()
    End Sub
    Protected Sub btnCetakNota_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFileName As String = xPK & ".pdf"
        Dim xFungsi As New FungsiData

        Dim xTAHUN As String = Year(CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "TGLFAKTUR").ToString))
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "NOTAPENERIMAAN/" & xTAHUN))
        'Label1.Text = directoryPath & xFileName

        If File.Exists(directoryPath & xFileName) Then
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        End If
        'Session("prLaporanPKSPKTT") = xPK
        'Response.Redirect("LaporanTTpenerimaanPERSPK.aspx")
    End Sub
    Protected Sub grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)
        If e.RowType = GridViewRowType.Data AndAlso e.VisibleIndex Mod 2 = 0 Then
            e.Row.BackColor = System.Drawing.Color.CornflowerBlue
        End If
        If e.RowType = GridViewRowType.Detail AndAlso e.VisibleIndex Mod 2 = 0 Then
            e.Row.BackColor = System.Drawing.Color.PaleTurquoise
        End If
        If e.RowType = GridViewRowType.Preview AndAlso e.VisibleIndex Mod 2 = 0 Then
            e.Row.BackColor = System.Drawing.Color.PaleTurquoise
            e.Row.ForeColor = System.Drawing.Color.Black
            e.Row.Font.Bold = True
        End If
    End Sub
    Protected Sub btnCetakNotaPenerimaan_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

    End Sub
    Protected Sub btnCetakLaporan_Click(sender As Object, e As EventArgs) Handles btnCetakLaporan.Click
        Dim xFungsi As New FungsiData
        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=LaporanPenerimaanBarangPerTahun.pdf")

        Dim doc As New Document(PageSize.A4.Rotate)

        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
        doc.Open()
        Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
        Dim cb As PdfContentByte = wri.DirectContent
        Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
        Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 9)
        Dim para, para2, para3, para4 As New Paragraph("")
        Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


        Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
        Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
        Dim pharse As New Phrase(" ")

        Dim xTabel As New PdfPTable(5)
        xTabel.WidthPercentage = 100
        ''Dim headerwidths() As Integer = {1, 4, 6, 2, 9, 2, 2}
        ''menghilangkan total dan harga
        Dim headerwidths() As Integer = {1, 4, 6, 2, 9}
        xTabel.SetWidths(headerwidths)
        xTabel.HeaderRows = 5

        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER

        xCell = New PdfPCell(New Phrase(New Chunk("LAPORAN PENERIMAAN BARANG", FontHeader)))
        'xCell.Colspan = 7
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("PERIODE TAHUN " & cboTahun.SelectedValue, FontHeader)))
        ''xCell.Colspan = 7
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("UNIT KERJA :" & cboUnitKerja.SelectedItem.Text, FontHeader)))
        ''xCell.Colspan = 7
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        ''xCell.Colspan = 7
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_RIGHT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NO", FontHeader)))
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("PIHAK KETIGA", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)


        'xCell = New PdfPCell(New Phrase(New Chunk("HARGA", FontHeader)))
        'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
        'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        'xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        'xCell.FixedHeight = 25
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("TOTAL", FontHeader)))
        'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
        'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        'xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        'xCell.FixedHeight = 25
        'xTabel.AddCell(xCell)

        Dim i As Integer = 0
        Dim myConnection As New SqlConnection()
        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        myConnection.ConnectionString = strConnectionString
        myConnection.Open()
        Dim strCommandText As String = ""
        strCommandText = strCommandText + "SELECT KODE_BARANG,dbo.GetNamaBarang(KODE_BARANG) AS NAMABARANG,JML, "
        strCommandText = strCommandText + " dbo.GetNAMAMITRA(B.KODE_MITRA) as PIHAKKE3, "
        strCommandText = strCommandText + " FORMAT(dbo.getHargaPerSPK(A.KODE_BARANG,A.IDSPK),'C','id-ID') as HARGASATUAN,FORMAT((JML*dbo.getHargaPerSPK(A.KODE_BARANG,A.IDSPK)),'C','id-ID') as JUMLAHTOTAL   "
        strCommandText = strCommandText + "  FROM TBL_SPK_TT_DETAIL A INNER JOIN TBL_SPK B ON A.IDSPK=B.IDSPK WHERE YEAR(A.TGL_PROSES)=" & cboTahun.SelectedValue & ""
        'Label1.Text = strCommandText.ToString

        'xCell = New PdfPCell(New Phrase(New Chunk(strCommandText, FontHeader)))
        'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
        'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        'xCell.FixedHeight = 25
        'xCell.Colspan = 7
        'xTabel.AddCell(xCell)
        Dim myCommand As New SqlCommand(strCommandText, myConnection)
        Dim strBaca As SqlDataReader = myCommand.ExecuteReader()

        While strBaca.Read()
            i = i + 1

            xCell = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
            xCell.HorizontalAlignment = Element.ALIGN_RIGHT
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString(), FontIsi)))
            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("NAMABARANG").ToString(), FontIsi)))
            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString(), FontIsi)))
            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("PIHAKKE3").ToString(), FontIsi)))
            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
            xTabel.AddCell(xCell)

            'Dim xHARGANUMERIC As String = ("N0")
            'xCell = New PdfPCell(New Phrase(New Chunk(strBaca("HARGASATUAN").ToString(), FontIsi)))
            'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
            'xCell.HorizontalAlignment = Element.ALIGN_RIGHT
            'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
            'xTabel.AddCell(xCell)

            'xCell = New PdfPCell(New Phrase(New Chunk(strBaca("JUMLAHTOTAL").ToString(), FontIsi)))
            'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
            'xCell.HorizontalAlignment = Element.ALIGN_RIGHT
            'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
            'xTabel.AddCell(xCell)

        End While
        myConnection.Close()


        xTabel.CompleteRow()

        doc.Add(xTabel)
        doc.Add(pharse)

        doc.Close()
        Response.Write(doc)
        Response.End()
    End Sub
    'Protected Sub btnPrintPerPeriode_Click(sender As Object, e As EventArgs)
    '    Dim xFungsi As New FungsiData
    '    Dim namafile As String
    '    namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
    '    Response.ContentType = "application/pdf"
    '    Response.AddHeader("content-disposition", "attachment; filename=LaporanPenerimaanBarangPerTahun.pdf")

    '    Dim doc As New Document(PageSize.A4.Rotate)

    '    Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
    '    doc.Open()
    '    Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
    '    Dim cb As PdfContentByte = wri.DirectContent
    '    Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
    '    Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 9)
    '    Dim para, para2, para3, para4 As New Paragraph("")
    '    Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


    '    Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
    '    Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
    '    Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
    '    Dim pharse As New Phrase(" ")

    '    Dim xTabel As New PdfPTable(7)
    '    xTabel.WidthPercentage = 100
    '    Dim headerwidths() As Integer = {1, 4, 6, 2, 9, 2, 2}
    '    xTabel.SetWidths(headerwidths)
    '    xTabel.HeaderRows = 5

    '    Dim xCell As PdfPCell = Nothing
    '    xTabel.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell = New PdfPCell(New Phrase(New Chunk("LAPORAN PENERIMAAN BARANG", FontHeader)))
    '    xCell.Colspan = 7
    '    xCell.Border = PdfPCell.NO_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xTabel.AddCell(xCell)

    '    'Dim xDate1 As Date = xTanggal1.Value
    '    'Dim xDate2 As Date = xTanggal2.Value
    '    Dim xDate1 As Date = Date.Now
    '    Dim xDate2 As Date = Date.Now
    '    xCell = New PdfPCell(New Phrase(New Chunk("PERIODE LAPORAN TANGGAL " & xDate1.ToString("dd-MM-yyyy") & " SAMPAI DENGAN " & xDate2.ToString("dd-MM-yyyy"), FontHeader)))
    '    xCell.Colspan = 7
    '    xCell.Border = PdfPCell.NO_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk("UNIT KERJA :" & cboUnitKerja.SelectedItem.Text, FontHeader)))
    '    xCell.Colspan = 7
    '    xCell.Border = PdfPCell.NO_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
    '    xCell.Colspan = 7
    '    xCell.Border = PdfPCell.NO_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_RIGHT
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk("NO", FontHeader)))
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '    xCell.BackgroundColor = BaseColor.LIGHT_GRAY
    '    xCell.FixedHeight = 25
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
    '    xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '    xCell.BackgroundColor = BaseColor.LIGHT_GRAY
    '    xCell.FixedHeight = 25
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
    '    xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '    xCell.BackgroundColor = BaseColor.LIGHT_GRAY
    '    xCell.FixedHeight = 25
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
    '    xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '    xCell.BackgroundColor = BaseColor.LIGHT_GRAY
    '    xCell.FixedHeight = 25
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk("PIHAK KETIGA", FontHeader)))
    '    xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '    xCell.BackgroundColor = BaseColor.LIGHT_GRAY
    '    xCell.FixedHeight = 25
    '    xTabel.AddCell(xCell)


    '    xCell = New PdfPCell(New Phrase(New Chunk("HARGA", FontHeader)))
    '    xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '    xCell.BackgroundColor = BaseColor.LIGHT_GRAY
    '    xCell.FixedHeight = 25
    '    xTabel.AddCell(xCell)

    '    xCell = New PdfPCell(New Phrase(New Chunk("TOTAL", FontHeader)))
    '    xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '    xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '    xCell.BackgroundColor = BaseColor.LIGHT_GRAY
    '    xCell.FixedHeight = 25
    '    xTabel.AddCell(xCell)

    '    Dim i As Integer = 0
    '    Dim myConnection As New SqlConnection()
    '    Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
    '    myConnection.ConnectionString = strConnectionString
    '    myConnection.Open()
    '    Try
    '        Dim strCommandText As String = ""
    '        strCommandText = strCommandText + "SELECT KODE_BARANG,dbo.GetNamaBarang(KODE_BARANG) AS NAMABARANG,JML, "
    '        strCommandText = strCommandText + " dbo.GetNAMAMITRA(B.KODE_MITRA) as PIHAKKE3, "
    '        strCommandText = strCommandText + " FORMAT(dbo.getHargaPerSPK(A.KODE_BARANG,A.IDSPK),'C','id-ID') as HARGASATUAN,FORMAT((JML*dbo.getHargaPerSPK(A.KODE_BARANG,A.IDSPK)),'C','id-ID') as JUMLAHTOTAL   "
    '        strCommandText = strCommandText + "  FROM TBL_SPK_TT_DETAIL A INNER JOIN TBL_SPK B ON A.IDSPK=B.IDSPK WHERE (CONVERT(DAte,A.TGL_PROSES) BETWEEN CONVERT(DATE,'" & xTanggal1.Value & "') AND CONVERT(DATE,'" & xTanggal1.Value & "'))"
    '        'Label1.Text = strCommandText.ToString

    '        'xCell = New PdfPCell(New Phrase(New Chunk(strCommandText, FontHeader)))
    '        'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '        'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '        'xCell.FixedHeight = 25
    '        'xCell.Colspan = 7
    '        'xTabel.AddCell(xCell)
    '        Dim myCommand As New SqlCommand(strCommandText, myConnection)
    '        Dim strBaca As SqlDataReader = myCommand.ExecuteReader()

    '        While strBaca.Read()
    '            i = i + 1

    '            xCell = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
    '            xCell.HorizontalAlignment = Element.ALIGN_RIGHT
    '            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '            xTabel.AddCell(xCell)

    '            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString(), FontIsi)))
    '            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '            xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '            xTabel.AddCell(xCell)

    '            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("NAMABARANG").ToString(), FontIsi)))
    '            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '            xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '            xTabel.AddCell(xCell)

    '            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString(), FontIsi)))
    '            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '            xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '            xTabel.AddCell(xCell)

    '            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("PIHAKKE3").ToString(), FontIsi)))
    '            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '            xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '            xTabel.AddCell(xCell)

    '            'Dim xHARGANUMERIC As String = ("N0")
    '            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("HARGASATUAN").ToString(), FontIsi)))
    '            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '            xCell.HorizontalAlignment = Element.ALIGN_RIGHT
    '            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '            xTabel.AddCell(xCell)

    '            xCell = New PdfPCell(New Phrase(New Chunk(strBaca("JUMLAHTOTAL").ToString(), FontIsi)))
    '            xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
    '            xCell.HorizontalAlignment = Element.ALIGN_RIGHT
    '            xCell.VerticalAlignment = Element.ALIGN_MIDDLE
    '            xTabel.AddCell(xCell)

    '        End While
    '    Catch ex As Exception

    '    Finally
    '        myConnection.Close()
    '    End Try




    '    xTabel.CompleteRow()

    '    doc.Add(xTabel)
    '    doc.Add(pharse)

    '    doc.Close()
    '    Response.Write(doc)
    '    Response.End()
    'End Sub

    Protected Sub btnDataDiambil_Click(sender As Object, e As EventArgs)
        Dim xTombol As LinkButton = CType(sender, LinkButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xPKMASTER As String = CStr(Container.Grid.GetMasterRowKeyValue().ToString)
        Dim xNAMABARANG As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "NAMABARANG").ToString)
        Dim xKODEBARANG As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "KODE_BARANG").ToString)

        Dim xFungsi As New FungsiData
        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=HistoryPenerimaanBarang.pdf")

        Dim doc As New Document(PageSize.A4)

        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
        doc.Open()
        Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
        Dim cb As PdfContentByte = wri.DirectContent
        Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
        Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 9)
        Dim para, para2, para3, para4 As New Paragraph("")
        Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


        Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
        Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
        Dim pharse As New Phrase(" ")

        Dim xTabel As New PdfPTable(5)
        xTabel.WidthPercentage = 100
        Dim headerwidths() As Integer = {1, 3, 2, 6, 3}
        xTabel.SetWidths(headerwidths)

        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER
        xCell = New PdfPCell(New Phrase(New Chunk("LAPORAN PENERIMAAN BARANG PER PEMBELIAN", FontHeader)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        'Dim xDate1 As Date = xTanggal1.Value
        'Dim xDate2 As Date = xTanggal2.Value
        xCell = New PdfPCell(New Phrase(New Chunk("NAMA BARANG :" & xNAMABARANG, FontHeader)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("NO PEMBELIAN :" & xFungsi.GetData("NOFAKTUR", "TBL_SPK", "IDSPK='" & xPKMASTER & "'").ToString, FontHeader)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_RIGHT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NO", FontHeader)))
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("TANGGAL TERIMA", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("DITERIMA OLEH", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NO. DELIVERY", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)


        'xCell = New PdfPCell(New Phrase(New Chunk("TANGGAL TERIMA", FontHeader)))
        'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
        'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        'xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        'xCell.FixedHeight = 25
        'xTabel.AddCell(xCell)

        Dim i As Integer = 0
        Dim myConnection As New SqlConnection()
        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        myConnection.ConnectionString = strConnectionString
        myConnection.Open()
        Try
            Dim strCommandText As String = ""
            strCommandText = strCommandText + "Select JML,"
            strCommandText = strCommandText + " TGL_PROSES,dbo.GetNAMALENGKAP(NAMAPROSES) AS NAMAPROSES,dbo.getNOSPK(IDSPK) as NOFAKTUR   "
            strCommandText = strCommandText + "  FROM TBL_SPK_TT_DETAIL WHERE KODE_BARANG='" & xKODEBARANG & "' AND IDSPK='" & xPKMASTER & "'"
            'Label1.Text = strCommandText.ToString

            'xCell = New PdfPCell(New Phrase(New Chunk(strCommandText, FontHeader)))
            'xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
            'xCell.HorizontalAlignment = Element.ALIGN_CENTER
            'xCell.VerticalAlignment = Element.ALIGN_MIDDLE
            'xCell.FixedHeight = 25
            'xCell.Colspan = 7
            'xTabel.AddCell(xCell)
            Dim myCommand As New SqlCommand(strCommandText, myConnection)
            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()

            While strBaca.Read()
                i = i + 1

                xCell = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
                xCell.HorizontalAlignment = Element.ALIGN_RIGHT
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE
                xTabel.AddCell(xCell)

                Dim xTGLPROSES As Date = strBaca("TGL_PROSES").ToString()
                xCell = New PdfPCell(New Phrase(New Chunk(xTGLPROSES.ToString("dd MMM yyyy"), FontIsi)))
                xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE

                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString(), FontIsi)))
                xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
                xCell.HorizontalAlignment = Element.ALIGN_RIGHT
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE
                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("NAMAPROSES").ToString(), FontIsi)))
                xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
                xCell.HorizontalAlignment = Element.ALIGN_LEFT
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE
                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("NOFAKTUR").ToString(), FontIsi)))
                xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
                xCell.HorizontalAlignment = Element.ALIGN_LEFT
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE
                xTabel.AddCell(xCell)

            End While
        Catch ex As Exception

        Finally
            myConnection.Close()
        End Try

        xTabel.CompleteRow()

        doc.Add(xTabel)
        doc.Add(pharse)

        doc.Close()
        Response.Write(doc)
        Response.End()
    End Sub
    Protected Sub btnNODO_Click(sender As Object, e As EventArgs)
        Dim xFungsi As New FungsiData
        Dim xTombol As LinkButton = CType(sender, LinkButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xPKMASTER As String = CStr(Container.Grid.GetMasterRowKeyValue().ToString)

        Dim xNODO As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "NODO").ToString)
        Dim xTGLFAKTUR As Date = CDate(Container.Grid.GetRowValues(Container.VisibleIndex, "TGLFAKTUR"))
        Dim xPENERIMA As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "NAMAPROSES").ToString)

        Dim xKODESUPPLIER As String = xFungsi.GetData("KODE_MITRA", "TBL_SPK", "IDSPK='" & xPKMASTER & "'")


        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=DaftarPenerimaanPerNoDelivery.pdf")

        Dim doc As New Document(PageSize.A4)

        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
        doc.Open()
        Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
        Dim cb As PdfContentByte = wri.DirectContent
        Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
        Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 9)
        Dim para, para2, para3, para4 As New Paragraph("")
        Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


        Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
        Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
        Dim pharse As New Phrase(" ")

        Dim xTabel As New PdfPTable(4)
        xTabel.WidthPercentage = 100
        Dim headerwidths() As Integer = {1, 3, 5, 2}
        xTabel.SetWidths(headerwidths)

        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER
        xCell = New PdfPCell(New Phrase(New Chunk("LAPORAN PENERIMAAN BARANG PER NOTA PENERIMAAN", FontHeader)))
        xCell.Colspan = 4
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)



        xCell = New PdfPCell(New Phrase(New Chunk("PIHAK KETIGA : " & xFungsi.GetData("NAMA_MITRA", "TBL_MITRA", "KODE_MITRA='" & xKODESUPPLIER & "'"), FontHeader)))
        xCell.Colspan = 4
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("NO FAKTUR PENERIMAN : " & xFungsi.GetData("NOFAKTUR", "TBL_SPK", "IDSPK='" & xPKMASTER & "'").ToString, FontHeader)))
        xCell.Colspan = 4
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("TANGGAL : " & xTGLFAKTUR.ToString("dd MMM yyyy"), FontHeader)))
        xCell.Colspan = 4
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("PENERIMA : " & xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xPENERIMA & "'").ToString, FontHeader)))
        xCell.Colspan = 4
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 4
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_RIGHT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NO", FontHeader)))
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
        xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xCell.VerticalAlignment = Element.ALIGN_MIDDLE
        xCell.BackgroundColor = BaseColor.LIGHT_GRAY
        xCell.FixedHeight = 25
        xTabel.AddCell(xCell)


        Dim i As Integer = 0
        Dim myConnection As New SqlConnection()
        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        myConnection.ConnectionString = strConnectionString
        myConnection.Open()
        Try
            Dim strCommandText As String = ""
            strCommandText = strCommandText + "Select KODE_BARANG,dbo.GetNamaBarang(KODE_BARANG) AS NAMABARANG,JML,"
            strCommandText = strCommandText + " TGL_PROSES,dbo.GetNAMAPEGAWAI(NAMAPROSES) AS NAMAPROSES,dbo.getNOSPK(IDSPK) as NOFAKTUR   "
            strCommandText = strCommandText + "  FROM TBL_SPK_TT_DETAIL WHERE PKSPKTT='" & xPK & "'"
            'Label1.Text = strCommandText.ToString

            Dim myCommand As New SqlCommand(strCommandText, myConnection)
            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()

            While strBaca.Read()
                i = i + 1

                xCell = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
                xCell.HorizontalAlignment = Element.ALIGN_RIGHT
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE
                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString, FontIsi)))
                xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE

                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("NAMABARANG").ToString(), FontIsi)))
                xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
                xCell.HorizontalAlignment = Element.ALIGN_LEFT
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE
                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString(), FontIsi)))
                xCell.Border = PdfPCell.TOP_BORDER Or PdfPCell.BOTTOM_BORDER Or PdfPCell.RIGHT_BORDER
                xCell.HorizontalAlignment = Element.ALIGN_RIGHT
                xCell.VerticalAlignment = Element.ALIGN_MIDDLE
                xTabel.AddCell(xCell)


            End While
        Catch ex As Exception

        Finally
            myConnection.Close()
        End Try

        xTabel.CompleteRow()

        doc.Add(xTabel)
        doc.Add(pharse)

        Dim xTabelBawah As New PdfPTable(4)
        xTabelBawah.WidthPercentage = 100
        Dim headerwidthsbawah() As Integer = {4, 2, 2, 4}
        xTabelBawah.SetWidths(headerwidthsbawah)
        Dim xCellbawah As PdfPCell = Nothing
        xTabelBawah.HorizontalAlignment = Element.ALIGN_CENTER

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk("", FontIsi)))
        xCellbawah.Colspan = 4
        xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.FixedHeight = 15
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk("PETUGAS GUDANG", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk("PENERIMA", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 4
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.FixedHeight = 30
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xPENERIMA & "'"), FontIsi)))
        'xCellbawah.Colspan = 2
        'xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk(".......................", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xPENERIMA & "'"), FontIsi)))
        'xCellbawah.Colspan = 2
        'xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 4
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.FixedHeight = 30
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        'pemindahan kolom ttd admin persediaan level es 2
        xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & Session("KDUNKER") & "'"), FontIsi)))
        xCellbawah.Colspan = 4
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 4
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.FixedHeight = 30
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
        'xCellbawah.Colspan = 2
        'xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk("...........................", FontIsi)))
        xCellbawah.Colspan = 4
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
        'xCellbawah.Colspan = 2
        'xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 4
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        xTabelBawah.CompleteRow()

        doc.Add(xTabelBawah)

        doc.Close()
        Response.Write(doc)
        Response.End()
    End Sub

    Protected Sub gridDetailTT_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDSPK") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub
    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDSPK") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub
End Class
