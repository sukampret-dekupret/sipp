Imports iTextSharp.text
Imports iTextSharp
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports System.Data
Partial Class LaporanStokBarangUnit
    Inherits System.Web.UI.Page

    Private Sub LaporanStokBarangUnit_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            Dim xData As New FungsiData
            Dim xNamaSatker As String = xData.GetData("NAMA_SATKER", "TBL_SATKER", "KD_SATKER='" & Session("KDUNKER") & "'")

            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            'cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboBulan.SelectedValue = CInt(Month(Date.Now()))

            cboUnitKerja.Enabled = False
            cboTahun.SelectedValue = Year(Date.Now())
            Dim xDate As New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, 1)
            Session("TGLAWAL") = xDate.AddDays(-1).ToShortDateString
            Session("TGLAKHIR1") = xDate.ToShortDateString
            Session("TGLAKHIR2") = xDate.AddMonths(1).AddDays(-1).ToShortDateString
            Label1.Text = Session("TGLAWAL") & "-" & Session("TGLAKHIR1") & "-" & Session("TGLAKHIR2") & "-TEST"
        End If
    End Sub

    Protected Sub cboBulan_TextChanged(sender As Object, e As EventArgs)
        Dim xDate As New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, 1)
        ''Dim xDate = New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, DateTime.DaysInMonth(cboBulan.SelectedValue, cboBulan.SelectedValue - 1))
        ''Label1.Text = xDate.AddDays(-1).ToShortDateString
        Session("TGLAWAL") = xDate.AddDays(-1).ToShortDateString
        Session("TGLAKHIR1") = xDate.ToShortDateString
        Session("TGLAKHIR2") = xDate.AddMonths(1).AddDays(-1).ToShortDateString
        Label1.Text = Session("TGLAWAL")
    End Sub
    Protected Sub cboTahun_TextChanged(sender As Object, e As EventArgs)
        Dim xDate As New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, 1)
        ''Dim xDate = New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, DateTime.DaysInMonth(cboBulan.SelectedValue, cboBulan.SelectedValue - 1))
        ''Label1.Text = xDate.AddDays(-1).ToShortDateString
        Session("TGLAWAL") = xDate.AddDays(-1).ToShortDateString
        Session("TGLAKHIR1") = xDate.ToShortDateString
        Session("TGLAKHIR2") = xDate.AddMonths(1).AddDays(-1).ToShortDateString
        Label1.Text = Session("TGLAWAL")
    End Sub

    Protected Sub btnBUKUPERSEDIAAN_Click(sender As Object, e As EventArgs)
        Dim xTombol As LinkButton = CType(sender, LinkButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        ''Dim xPKMASTER As String = CStr(Container.Grid.GetMasterRowKeyValue().ToString)

        Dim xKDBARANG As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "KODE_BARANG").ToString)
        Dim xNAMABARANG As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "NAMA_BARANG").ToString)
        Dim xSATAUN As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "SATUAN").ToString)
        Dim xFungsi As New FungsiData

        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=BukuPersediaan.pdf")

        Dim doc As New Document(PageSize.A4)

        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
        doc.Open()
        Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
        Dim cb As PdfContentByte = wri.DirectContent

        Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 8, Font.BOLD)
        Dim FontHeaderJudul As Font = FontFactory.GetFont("ARIAL", 9, Font.BOLD)
        Dim FontHeader2 As Font = FontFactory.GetFont("ARIAL", 8)
        Dim FontIsiAngka As Font = FontFactory.GetFont("ARIAL", 6)
        Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 8)
        Dim para, para2, para3, para4 As New Paragraph("")
        Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


        Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
        Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
        Dim pharse As New Phrase(" ")

        Dim xTabel As New PdfPTable(7)
        xTabel.WidthPercentage = 100
        Dim headerwidths() As Integer = {1, 3, 8, 2, 2, 2, 2}
        xTabel.SetWidths(headerwidths)
        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER


        xCell = New PdfPCell(New Phrase(New Chunk("UAPB", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(": KEMENTERIAN LUAR NEGERI", FontHeader)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("UAPPB-E1", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        Dim xNAMASATKER As String = xFungsi.GetData("NAMA_UNKER", "TBL_UNKER", " KD_UNKER='" & Session("KDUNKER") & "'")
        xCell = New PdfPCell(New Phrase(New Chunk(": " & xNAMASATKER, FontHeader)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("UAPPB-W", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)



        xCell = New PdfPCell(New Phrase(New Chunk(": INSTANSI PUSAT", FontHeader)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("   ", FontHeader)))
        xCell.Colspan = 7
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)



        xCell = New PdfPCell(New Phrase(New Chunk("BUKU PERSEDIAAN", FontHeaderJudul)))
        xCell.Colspan = 7
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 7
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)
        '---------------------row satu ---------------------------------------------'
        xCell = New PdfPCell(New Phrase(New Chunk("UAKPB", FontHeader2)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        Dim xNAMASATKERPANJANG As String = xFungsi.GetData("KET_UNKER", "TBL_UNKER", " KD_UNKER='" & Session("KDUNKER") & "'")
        xCell = New PdfPCell(New Phrase(New Chunk(": " & xNAMASATKERPANJANG, FontHeader2)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader2)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(": " & xKDBARANG, FontHeader2)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        '-----------------------row dua -----------------------------------------------------'
        xCell = New PdfPCell(New Phrase(New Chunk("KD. UAKPB", FontHeader2)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(": 403247.0000", FontHeader2)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader2)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(": " & xNAMABARANG, FontHeader2)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        '------------------------row tiga------------------------------'
        'xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        'xCell.Colspan = 2
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)


        'xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        'xCell.Colspan = 5
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("SATUAN", FontHeader2)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(": " & xSATAUN, FontHeader2)))
        xCell.Colspan = 5
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("   ", FontHeader2)))
        xCell.Colspan = 7
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        Dim myConnection As New SqlConnection()
        ''Try

        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        myConnection.ConnectionString = strConnectionString
        ' buat commandf
        Using (myConnection)
            Dim myCommand As New SqlCommand
            myCommand.Connection = myConnection
            myCommand.CommandText = "sp_LaporanTrx"
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.Parameters.AddWithValue("KDUNKER", cboUnitKerja.SelectedValue)
            myCommand.Parameters.AddWithValue("TGLAWAL", Date.Now)
            myCommand.Parameters.AddWithValue("KDBARANG", xKDBARANG)
            myConnection.Open()
            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()

            xCell = New PdfPCell(New Phrase(New Chunk("No.", FontHeader2)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xCell.BackgroundColor = BaseColor.GRAY
            xTabel.AddCell(xCell)


            xCell = New PdfPCell(New Phrase(New Chunk("Tanggal", FontHeader2)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xCell.BackgroundColor = BaseColor.GRAY
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("Uraian", FontHeader2)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xCell.BackgroundColor = BaseColor.GRAY
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("Masuk", FontHeader2)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xCell.BackgroundColor = BaseColor.GRAY
            xTabel.AddCell(xCell)


            'xCell = New PdfPCell(New Phrase(New Chunk("Harga Beli", FontHeader2)))
            'xCell.HorizontalAlignment = Element.ALIGN_CENTER
            'xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("Keluar", FontHeader2)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xCell.BackgroundColor = BaseColor.GRAY
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("Jumlah", FontHeader2)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xCell.BackgroundColor = BaseColor.GRAY
            xTabel.AddCell(xCell)

            'xCell = New PdfPCell(New Phrase(New Chunk("Nilai", FontHeader2)))
            'xCell.HorizontalAlignment = Element.ALIGN_CENTER
            'xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("Paraf", FontHeader2)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xCell.VerticalAlignment = Element.ALIGN_MIDDLE

            xCell.BackgroundColor = BaseColor.GRAY
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("1", FontIsiAngka)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xTabel.AddCell(xCell)


            xCell = New PdfPCell(New Phrase(New Chunk("2", FontIsiAngka)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("3", FontIsiAngka)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("4", FontIsiAngka)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xTabel.AddCell(xCell)


            xCell = New PdfPCell(New Phrase(New Chunk("5", FontIsiAngka)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("6", FontIsiAngka)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("7", FontIsiAngka)))
            xCell.HorizontalAlignment = Element.ALIGN_CENTER
            xTabel.AddCell(xCell)


            Dim xTanggal As Date = Session("TGLAKHIR2")
            Dim xLastDay As New Date(xTanggal.Year, xTanggal.Month, 1)
            xLastDay = xLastDay.AddDays(-1)
            Dim i As Integer
            i = 0
            While strBaca.Read()
                i = i + 1
                xCell = New PdfPCell(New Phrase(New Chunk(i.ToString(), FontHeader2)))
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xTabel.AddCell(xCell)


                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("TglFormat").ToString(), FontHeader2)))
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("Uraian").ToString(), FontHeader2)))
                xCell.HorizontalAlignment = Element.ALIGN_LEFT
                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("Debet").ToString(), FontHeader2)))
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xTabel.AddCell(xCell)


                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("Kredit").ToString(), FontHeader2)))
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xTabel.AddCell(xCell)

                xCell = New PdfPCell(New Phrase(New Chunk(strBaca("Saldo").ToString(), FontHeader2)))
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xTabel.AddCell(xCell)



                xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader2)))
                xCell.HorizontalAlignment = Element.ALIGN_CENTER
                xTabel.AddCell(xCell)
            End While
        End Using



        ''Catch

        ''End Try




        'xCell = New PdfPCell(New Phrase(New Chunk(xLastDay.ToString(), FontHeader2)))
        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabel.AddCell(xCell)



        xTabel.CompleteRow()
        doc.Add(xTabel)
        doc.Add(pharse)

        doc.Close()
        Response.Write(doc)
        Response.End()
    End Sub
End Class
