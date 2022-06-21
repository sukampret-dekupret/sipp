Imports DevExpress.Web
Imports FungsiData
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data

Partial Class OrderDistribusi
    Inherits System.Web.UI.Page



    Private Sub OrderDistribusi_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If (Not IsPostBack) Then

            cboTahun.SelectedValue = Year(Date.Now)
            cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False

            'grid.DataBind()
            'grid.DetailRows.ExpandRow(0)

        End If
    End Sub

    Protected Sub CetakRekap(xPK As String)
        Dim xFungsi As New FungsiData

        'Dim xPKMASTER As String = xFungsi.GetData("KDORDER", "TBL_ORDER_TT", " PKORDERTT='" & xPK & "'")
        'Dim xNOTT As String = xFungsi.GetData("NO_TT", "TBL_ORDER_TT", " PKORDERTT='" & xPK & "'")
        Dim xKDPEMOHON As String = xFungsi.GetData("NAMAUSER", "TBL_ORDER", " KDORDER='" & xPK & "'")
        Dim xKDADMIN As String = xFungsi.GetData("NAMAPROSES2", "TBL_ORDER", " KDORDER='" & xPK & "'")
        Dim xKDUNKER As String = xFungsi.GetData("DRUNKER", "TBL_ORDER", " KDORDER='" & xPK & "'")

        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=RekapPermintaanUnit.pdf")

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

        Dim xTabel As New PdfPTable(2)
        xTabel.WidthPercentage = 100
        Dim headerwidths() As Integer = {2, 6}
        xTabel.SetWidths(headerwidths)
        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER

        Dim xBarcode As New Barcode128
        'xBarcode.SetText(xPK)
        xBarcode.Code = xFungsi.GetData("TGL_NOTA", "TBL_ORDER", "KDORDER='" & xPK & "'")
        xCell = New PdfPCell(xBarcode.CreateImageWithBarcode(cb, Nothing, Nothing))
        'xCell = New PdfPCell(xBarcode.GetImage)
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.FixedHeight = 50
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("TANDA TERIMA PERMINTAAN BARANG", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("UNIT KERJA : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & xKDUNKER & "'"), FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.FixedHeight = 25
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        Dim xNota As String
        xNota = xFungsi.GetData("NO_NOTA", "TBL_ORDER", "KDORDER='" & xPK & "'")
        xCell = New PdfPCell(New Phrase(New Chunk("Permintaan Nota Dinas No. " & xNota.ToString, FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("  ", FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("Tanggal Permohonan " & xFungsi.GetData("TGL_NOTA", "TBL_ORDER", "KDORDER='" & xPK & "'"), FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("Tanggal Persetujuan " & xFungsi.GetData("TGL_NOTA", "TBL_ORDER", "KDORDER='" & xPK & "'"), FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)




        'xCell = New PdfPCell(New Phrase(New Chunk("NAMA", FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)


        'xCell = New PdfPCell(New Phrase(New Chunk(" :  " & xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("UNIT", FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & Session("KDUNKER") & "'"), FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("NIP/NIK", FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("DAFTAR BARANG KELUAR", FontHeader)))
        'xCell.Colspan = 2
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        xTabel.CompleteRow()

        doc.Add(xTabel)



        Dim xTabelisi As New PdfPTable(6)
        xTabelisi.WidthPercentage = 100
        'Dim headerwidthsisi() As Integer = {1, 6, 4, 5}
        Dim headerwidthsisi() As Integer = {1, 4, 6, 2, 3, 3}
        xTabelisi.SetWidths(headerwidthsisi)
        Dim xCellisi As PdfPCell = Nothing
        xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER


        Dim xTabelFlag As New DataTable()
        xTabelFlag = GetDataFlag()



        ''For Each sourceColumn As DataColumn In xTabelFlag.Columns
        'For Each row As DataRow In xTabelFlag.Rows
        '    xCellisi = Nothing
        '    xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER

        '    xCellisi = New PdfPCell(New Phrase(New Chunk(row.Item("KETERANGAN").ToString(), FontHeader)))
        '    xCellisi.Colspan = 4
        '    xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
        '    xTabelisi.AddCell(xCellisi)

        '    xTabelisi.CompleteRow()
        '        doc.Add(xTabelisi)
        'Next
        '' Next

        xCellisi = Nothing
        xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER

        xCellisi = New PdfPCell(New Phrase(New Chunk("Disetujui Barang Tersedia", FontHeader)))
        xCellisi.Colspan = 6
        xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
        xTabelisi.AddCell(xCellisi)

        xTabelisi.CompleteRow()

        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString


        xCellisi = New PdfPCell(New Phrase(New Chunk("NO.", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        'xCellisi = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
        'xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelisi.AddCell(xCellisi)
        xCellisi = New PdfPCell(New Phrase(New Chunk("SATUAN", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("PERMINTAAN", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("DIPENUHI", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        Dim i As Integer
        i = 0

        Dim myConnection As New SqlConnection()
        Try

            myConnection.ConnectionString = strConnectionString

            Dim strCommandText As String = ""
            'strCommandText = strCommandText + "SELECT dbo.Getnamabarang(KODE_BARANG) as NAMABARANG,KODE_BARANG,JML FROM TBL_ORDER_TT_DETAIL "
            strCommandText = strCommandText + "SELECT KODE_BARANG,dbo.getnamabarang(KODE_BARANG) as NAMA_BARANG,MOHON,dbo.getsatuanbarang(KODE_BARANG) as SATUAN,PENUHI FROM TBL_ORDER_DETAIL"
            strCommandText = strCommandText + " WHERE KDORDER='" & xPK & "' AND KET=1"

            Dim myCommand As New SqlCommand(strCommandText, myConnection)
            myConnection.Open()
            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()
            While strBaca.Read()
                i = i + 1

                xCellisi = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("NAMA_BARANG").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
                xTabelisi.AddCell(xCellisi)

                'xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString, FontIsi)))
                'xCellisi.HorizontalAlignment = Element.ALIGN_RIGHT
                'xTabelisi.AddCell(xCellisi)
                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("SATUAN").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("MOHON").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                If strBaca("MOHON").ToString <> strBaca("PENUHI").ToString Then
                    xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("PENUHI").ToString, FontIsi)))
                    xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                    xCellisi.BackgroundColor = BaseColor.ORANGE
                    xTabelisi.AddCell(xCellisi)
                Else
                    xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("PENUHI").ToString, FontIsi)))
                    xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                    xTabelisi.AddCell(xCellisi)
                End If

            End While

            xCellisi = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
            xCellisi.FixedHeight = 30
            xCellisi.Colspan = 6
            xCellisi.Border = PdfPCell.NO_BORDER
            xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelisi.AddCell(xCellisi)

            xTabelisi.CompleteRow()
            doc.Add(xTabelisi)
        Catch ex As Exception
            ' write the error to file
            Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/error.log"))
            sw.WriteLine(ex.Message)
            sw.Close()
            ' now rethrow the error
            'Throw (ex)
        Finally
            myConnection.Close()
        End Try

        doc.Add(pharse)

        Dim xTabelBawah As New PdfPTable(4)
        xTabelBawah.WidthPercentage = 100
        Dim headerwidthsbawah() As Integer = {4, 2, 2, 4}
        xTabelisi.SetWidths(headerwidthsisi)
        Dim xCellbawah As PdfPCell = Nothing
        xTabelBawah.HorizontalAlignment = Element.ALIGN_CENTER

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN", FontIsi)))
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

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)


        xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
        xCellbawah.Colspan = 2
        xCellbawah.Border = PdfPCell.NO_BORDER
        xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelBawah.AddCell(xCellbawah)

        xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
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

        'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontIsi)))
        'xCellbawah.Colspan = 2
        'xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelBawah.AddCell(xCellbawah)


        'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
        'xCellbawah.Colspan = 2
        'xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelBawah.AddCell(xCellbawah)

        'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontIsi)))
        'xCellbawah.Colspan = 2
        'xCellbawah.Border = PdfPCell.NO_BORDER
        'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelBawah.AddCell(xCellbawah)

        xTabelBawah.CompleteRow()

        doc.Add(xTabelBawah)
        'doc.Add(pharse)



        xTabel.CompleteRow()

        'doc.Add(xTabel)
        doc.Add(pharse)

        doc.Close()
        Response.Write(doc)
        Response.End()
    End Sub


    Private Function GetDataFlag() As DataTable
        Dim table As DataTable = New DataTable()
        Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        Using conn As SqlConnection = New SqlConnection(strConnString)
            Dim sql As String = "SELECT IDFLAG,KETERANGAN FROM TBL_R_FLAG"

            Using cmd As SqlCommand = New SqlCommand(sql, conn)
                cmd.CommandType = CommandType.Text
                'cmd.Parameters.Clear()
                Using ad As SqlDataAdapter = New SqlDataAdapter(cmd)
                    ad.Fill(table)
                End Using
            End Using
        End Using

        Return table
    End Function
    Protected Sub ASPxGridView1_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("KDORDER") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub btnTT_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xSTS As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "STATUS").ToString)
        Dim xFungsi As New FungsiData

        If xSTS = 5 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang sudah didistribusikan semua!')", True)
        Else
            Session("KDORDERTT") = xPK
            ''Response.Redirect("OrderTT.aspx")
            Session("KDUNKERTT") = cboUnitKerja.SelectedValue
            Response.Redirect("OrderTandaTerima.aspx")
        End If
    End Sub

    Protected Sub gridOrderTT_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("KDORDER") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub
    Protected Sub btnCetakNota_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        CetakNotaBaru(xPK)
    End Sub

    Protected Sub CetakNotaBaru(xPK As String)
        Dim xFungsi As New FungsiData

        Dim xPKMASTER As String = xFungsi.GetData("KDORDER", "TBL_ORDER_TT", " PKORDERTT='" & xPK & "'")
        Dim xNOTT As String = xFungsi.GetData("NO_TT", "TBL_ORDER_TT", " PKORDERTT='" & xPK & "'")
        Dim xKDPEMOHON As String = xFungsi.GetData("NAMAUSER", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        Dim xKDADMIN As String = xFungsi.GetData("NAMAPROSES2", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        Dim xKDUNKER As String = xFungsi.GetData("DRUNKER", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=TandaTerimaPengeluaranBarangUnit.pdf")

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

        Dim xTabel As New PdfPTable(2)
        xTabel.WidthPercentage = 100
        Dim headerwidths() As Integer = {2, 6}
        xTabel.SetWidths(headerwidths)
        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER

        Dim xBarcode As New Barcode128
        'xBarcode.SetText(xPK)
        xBarcode.Code = xNOTT
        xCell = New PdfPCell(xBarcode.CreateImageWithBarcode(cb, Nothing, Nothing))
        'xCell = New PdfPCell(xBarcode.GetImage)
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.FixedHeight = 50
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("TANDA TERIMA PERMINTAAN BARANG", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("UNIT KERJA : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & xKDUNKER & "'"), FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.FixedHeight = 25
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        Dim xNota As String
        xNota = xFungsi.GetData("NO_NOTA", "TBL_ORDER", "KDORDER='" & xPKMASTER & "'")
        xCell = New PdfPCell(New Phrase(New Chunk("Permintaan Nota Dinas No. " & xNota.ToString, FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("No Tanda Terima. " & xNOTT.ToString(), FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("  ", FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("Tanggal Permohonan " & xFungsi.GetData("TGL_NOTA", "TBL_ORDER", "KDORDER='" & xPKMASTER & "'"), FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("Tanggal Persetujuan " & xFungsi.GetData("TGL_NOTA", "TBL_ORDER", "KDORDER='" & xPKMASTER & "'"), FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("Tanggal Terima " & xFungsi.GetData("TGL_CETAK", "TBL_ORDER_TT", "PKORDERTT='" & xPK & "'"), FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)



        'xCell = New PdfPCell(New Phrase(New Chunk("NAMA", FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)


        'xCell = New PdfPCell(New Phrase(New Chunk(" :  " & xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("UNIT", FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & Session("KDUNKER") & "'"), FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("NIP/NIK", FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontHeader)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("DAFTAR BARANG KELUAR", FontHeader)))
        'xCell.Colspan = 2
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        xTabel.CompleteRow()

        doc.Add(xTabel)



        Dim xTabelisi As New PdfPTable(6)
        xTabelisi.WidthPercentage = 100
        'Dim headerwidthsisi() As Integer = {1, 6, 4, 5}
        Dim headerwidthsisi() As Integer = {1, 4, 6, 2, 3, 3}
        xTabelisi.SetWidths(headerwidthsisi)
        Dim xCellisi As PdfPCell = Nothing
        xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER


        Dim xTabelFlag As New DataTable()
        xTabelFlag = GetDataFlag()



        ''For Each sourceColumn As DataColumn In xTabelFlag.Columns
        'For Each row As DataRow In xTabelFlag.Rows
        '    xCellisi = Nothing
        '    xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER

        '    xCellisi = New PdfPCell(New Phrase(New Chunk(row.Item("KETERANGAN").ToString(), FontHeader)))
        '    xCellisi.Colspan = 4
        '    xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
        '    xTabelisi.AddCell(xCellisi)

        '    xTabelisi.CompleteRow()
        '        doc.Add(xTabelisi)
        'Next
        '' Next

        xCellisi = Nothing
        xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER

        xCellisi = New PdfPCell(New Phrase(New Chunk("Disetujui Barang Tersedia", FontHeader)))
        xCellisi.Colspan = 6
        xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
        xTabelisi.AddCell(xCellisi)

        xTabelisi.CompleteRow()

        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString


        xCellisi = New PdfPCell(New Phrase(New Chunk("NO.", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        'xCellisi = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
        'xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabelisi.AddCell(xCellisi)
        xCellisi = New PdfPCell(New Phrase(New Chunk("SATUAN", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("PERMINTAAN", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("DIPENUHI", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xCellisi.BackgroundColor = BaseColor.GRAY
        xTabelisi.AddCell(xCellisi)

        Dim i As Integer
        i = 0

        Dim myConnection As New SqlConnection()
        Try

            myConnection.ConnectionString = strConnectionString

            Dim strCommandText As String = ""
            'strCommandText = strCommandText + "SELECT dbo.Getnamabarang(KODE_BARANG) as NAMABARANG,KODE_BARANG,JML FROM TBL_ORDER_TT_DETAIL "
            strCommandText = strCommandText + "SELECT KODE_BARANG,dbo.getnamabarang(KODE_BARANG) as NAMA_BARANG,dbo.getJMLMOHONORDERperTT(KODE_BARANG,KDORDER) as MOHON,dbo.getsatuanbarang(KODE_BARANG) as SATUAN,JML as PENUHI,dbo.getFLAGorder(KODE_BARANG,KDORDER) as FLAG FROM TBL_ORDER_TT_DETAIL"
            strCommandText = strCommandText + " WHERE PKORDERTT='" & xPK & "' AND KET=1"

            Dim myCommand As New SqlCommand(strCommandText, myConnection)
            myConnection.Open()
            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()
            While strBaca.Read()
                i = i + 1

                xCellisi = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("NAMA_BARANG").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
                xTabelisi.AddCell(xCellisi)

                'xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString, FontIsi)))
                'xCellisi.HorizontalAlignment = Element.ALIGN_RIGHT
                'xTabelisi.AddCell(xCellisi)
                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("SATUAN").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("MOHON").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                If strBaca("MOHON").ToString <> strBaca("PENUHI").ToString Then
                    xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("PENUHI").ToString, FontIsi)))
                    xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                    xCellisi.BackgroundColor = BaseColor.ORANGE
                    xTabelisi.AddCell(xCellisi)
                Else
                    xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("PENUHI").ToString, FontIsi)))
                    xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                    xTabelisi.AddCell(xCellisi)
                End If

            End While

            xCellisi = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
            xCellisi.FixedHeight = 30
            xCellisi.Colspan = 6
            xCellisi.Border = PdfPCell.NO_BORDER
            xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelisi.AddCell(xCellisi)

            xTabelisi.CompleteRow()
            doc.Add(xTabelisi)
        Catch ex As Exception
            ' write the error to file
            Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/error.log"))
            sw.WriteLine(ex.Message)
            sw.Close()
            ' now rethrow the error
            'Throw (ex)
        Finally
            myConnection.Close()
        End Try

        doc.Add(pharse)

            Dim xTabelBawah As New PdfPTable(4)
            xTabelBawah.WidthPercentage = 100
            Dim headerwidthsbawah() As Integer = {4, 2, 2, 4}
            xTabelisi.SetWidths(headerwidthsisi)
            Dim xCellbawah As PdfPCell = Nothing
            xTabelBawah.HorizontalAlignment = Element.ALIGN_CENTER

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)


            xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN", FontIsi)))
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

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)


            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
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

            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)


            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xTabelBawah.CompleteRow()

            doc.Add(xTabelBawah)
            'doc.Add(pharse)



            xTabel.CompleteRow()

        'doc.Add(xTabel)
        doc.Add(pharse)

        doc.Close()
        Response.Write(doc)
        Response.End()
    End Sub



    Protected Sub CetakNotaLama(xPK As String)
        Dim xFungsi As New FungsiData

        Dim xPKMASTER As String = xFungsi.GetData("KDORDER", "TBL_ORDER_TT", " PKORDERTT='" & xPK & "'")
        Dim xNOTT As String = xFungsi.GetData("NO_TT", "TBL_ORDER_TT", " PKORDERTT='" & xPK & "'")
        Dim xKDPEMOHON As String = xFungsi.GetData("NAMAUSER", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        Dim xKDADMIN As String = xFungsi.GetData("NAMAPROSES", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        Dim xKDUNKER As String = xFungsi.GetData("DRUNKER", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")

        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=TandaTerimaPengeluaranBarangUnit.pdf")

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

        Dim xTabel As New PdfPTable(2)
        xTabel.WidthPercentage = 100
        Dim headerwidths() As Integer = {2, 6}
        xTabel.SetWidths(headerwidths)
        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER

        Dim xBarcode As New Barcode128
        'xBarcode.SetText(xPK)
        xBarcode.Code = xNOTT
        xCell = New PdfPCell(xBarcode.CreateImageWithBarcode(cb, Nothing, Nothing))
        'xCell = New PdfPCell(xBarcode.GetImage)
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.FixedHeight = 50
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("TANDA TERIMA", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("KELUAR GUDANG ESELON II", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.FixedHeight = 25
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NAMA", FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("UNIT", FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & xKDUNKER & "'"), FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NIP/NIK", FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("DAFTAR BARANG KELUAR", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xTabel.CompleteRow()

        doc.Add(xTabel)
        'doc.Add(pharse)


        Dim xTabelisi As New PdfPTable(4)
        xTabelisi.WidthPercentage = 100
        Dim headerwidthsisi() As Integer = {1, 6, 4, 5}
        xTabelisi.SetWidths(headerwidthsisi)
        Dim xCellisi As PdfPCell = Nothing
        xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER

        xCellisi = New PdfPCell(New Phrase(New Chunk("NO.", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelisi.AddCell(xCellisi)

        xCellisi = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelisi.AddCell(xCellisi)

        Dim i As Integer
        i = 0

        Dim myConnection As New SqlConnection()
        Try

            Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
            myConnection.ConnectionString = strConnectionString

            Dim strCommandText As String = ""
            strCommandText = strCommandText + "SELECT dbo.Getnamabarang(KODE_BARANG) as NAMABARANG,KODE_BARANG,JML FROM TBL_ORDER_TT_DETAIL "
            strCommandText = strCommandText + " WHERE PKORDERTT='" & xPK & "' AND KET=1"

            Dim myCommand As New SqlCommand(strCommandText, myConnection)
            myConnection.Open()
            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()
            While strBaca.Read()
                i = i + 1

                xCellisi = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("NAMABARANG").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
                xTabelisi.AddCell(xCellisi)

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString, FontIsi)))
                xCellisi.HorizontalAlignment = Element.ALIGN_RIGHT
                xTabelisi.AddCell(xCellisi)
            End While

            xCellisi = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
            xCellisi.FixedHeight = 30
            xCellisi.Colspan = 4
            xCellisi.Border = PdfPCell.NO_BORDER
            xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelisi.AddCell(xCellisi)

            xTabelisi.CompleteRow()
            doc.Add(xTabelisi)
            doc.Add(pharse)

            Dim xTabelBawah As New PdfPTable(4)
            xTabelBawah.WidthPercentage = 100
            Dim headerwidthsbawah() As Integer = {4, 2, 2, 4}
            xTabelisi.SetWidths(headerwidthsisi)
            Dim xCellbawah As PdfPCell = Nothing
            xTabelBawah.HorizontalAlignment = Element.ALIGN_CENTER

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)


            xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN", FontIsi)))
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

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)


            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
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

            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)


            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDPEMOHON & "'"), FontIsi)))
            xCellbawah.Colspan = 2
            xCellbawah.Border = PdfPCell.NO_BORDER
            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
            xTabelBawah.AddCell(xCellbawah)

            xTabelBawah.CompleteRow()

            doc.Add(xTabelBawah)
            'doc.Add(pharse)

        Catch ex As Exception
            ' write the error to file
            Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/error.log"))
            sw.WriteLine(ex.Message)
            sw.Close()
            ' now rethrow the error
            'Throw (ex)
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
    Protected Sub grid_DataBound(sender As Object, e As EventArgs)
        CType(sender, ASPxGridView).DetailRows.ExpandAllRows()
    End Sub
    Protected Sub btnRekap_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        CetakRekap(xPK)

    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        Session("PKORDERTT") = xPK
        Session("TAHUN") = cboTahun.SelectedValue
        Response.Redirect("OrderUploadTT.aspx")
    End Sub
    Protected Sub btnDownloadTT_Click(sender As Object, e As EventArgs)
        Dim xTombol As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFileName As String = xPK & ".pdf"
        'Dim xTAHUN As String = Year(CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "TGL_TAMBAH").ToString))
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/TTOrder/" & cboTahun.SelectedValue))
        Label1.Text = directoryPath & xFileName

        If File.Exists(directoryPath & xFileName) Then
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        End If
    End Sub
End Class
