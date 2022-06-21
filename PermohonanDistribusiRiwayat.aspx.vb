Imports Microsoft.VisualBasic
Imports System
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO

Partial Class PermohonanDistribusiRiwayat
    Inherits System.Web.UI.Page

    Private Sub PermohonanDistribusiRiwayat_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If (Not IsPostBack) Then
            cboTahun.SelectedValue = Year(Date.Now)
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False
        End If
    End Sub

    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDPERMOHONAN") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub btnConfirmasi_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        MsgBox(xPK)
        'Dim vpnButton As Button = CType(sender, Button)
        'Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        'Dim confirmValue As String = Request.Form("confirm_value")
        'If confirmValue = "Yes" Then
        '    Dim xData As New FungsiData
        '    xData.UpdateData("TBL_PERMOHONAN_DETAIL", "KET=2,TGL_PROSES=GETDATE()", "IDDETAILPERMOHONAN='" & xPK & "'")
        '    grid.DataBind()

        '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang sudah didistribusikan')", True)

        'Else
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
        'End If
    End Sub
    Protected Sub btnCetakTT_Click(sender As Object, e As EventArgs)

        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xSTS As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "STS_PERMOHONAN").ToString)
        Dim xFungsi As New FungsiData

        If xSTS = 4 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang sudah didistribusikan semua!')", True)
        Else
            Session("IDTTPERMOHONAN") = xPK
            Response.Redirect("PermohonanTandaTerima.aspx")
            'Response.Redirect("PermohonanTT.aspx")
        End If

        'Dim sqlQuery As New StringBuilder()
        'sqlQuery.Clear()
        'sqlQuery.Append("INSERT INTO TBL_PERMOHONAN_TT_DETAIL(PKTTMOHON, KODE_BARANG, JML, NAMAUSER, IDPERMOHONAN,KDUNKER) SELECT ")
        'sqlQuery.Append("'" & Session("PKTTMOHON") & "', ")
        'sqlQuery.Append("KODE_BARANG,")
        'sqlQuery.Append("(JML_PENUHI-dbo.getJMLMOHONperTT(KODE_BARANG,IDPERMOHONAN)) AS JML,")
        'sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
        'sqlQuery.Append("'" & Session("IDTTPERMOHONAN") & "', ")
        'sqlQuery.Append("'" & Session("KDUNKER") & "'")
        'sqlQuery.Append(" FROM TBL_PERMOHONAN_DETAIL WHERE IDPERMOHONAN='" & Session("IDTTPERMOHONAN") & "'  ")
        'xFungsi.insertData(sqlQuery.ToString())
        'Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        'Dim conn As New SqlConnection()
        'Dim sqlComm As New SqlCommand()
        'conn.ConnectionString = strConnString
        'sqlComm.Connection = conn
        'sqlComm.CommandType = System.Data.CommandType.StoredProcedure
        'sqlComm.CommandText = "MasukinDataByPermohonan"

        'sqlComm.Parameters.AddWithValue("PKTTMOHON", Session("PKTTMOHON"))
        'sqlComm.Parameters.AddWithValue("IDPERMOHONAN", Session("IDTTPERMOHONAN"))
        'sqlComm.Parameters.AddWithValue("NAMAUSER", Session("NAMAUSER"))
        'sqlComm.Parameters.AddWithValue("KDUNKER", Session("KDUNKER"))
        'conn.Open()
        'Dim i As Integer = sqlComm.ExecuteNonQuery()
        'conn.Close()



    End Sub



    'Dim vpnButton As Button = CType(sender, Button)
    'Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
    'Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
    'Dim xNOFAKTUR As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "NO_PERMOHONAN").ToString)
    'Dim xFungsi As New FungsiData
    'Dim xNAMAUSER As String = xFungsi.GetData("NAMAUSER", "TBL_PERMOHONAN", "IDPERMOHONAN='" & xPK & "'")
    'Dim xNAMAPROSES As String = xFungsi.GetData("IDPEMROSES", "TBL_PERMOHONAN", "IDPERMOHONAN='" & xPK & "'")
    'Dim confirmValue As String = Request.Form("confirm_value")
    'If confirmValue = "Yes" Then

    '    If xFungsi.GetData("STS_PERMOHONAN", "TBL_PERMOHONAN", "IDPERMOHONAN='" & xPK & "'") = 1 Then
    '        Dim xData As New FungsiData
    '        xData.UpdateData("TBL_PERMOHONAN", "STS_PERMOHONAN=4,TGL_DISTRIBUSI=GETDATE(),NAMAOP='" & Session("NAMAUSER") & "'", "IDPERMOHONAN='" & xPK & "'")
    '        xData.UpdateData("TBL_PERMOHONAN_DETAIL", "KET=2,TGL_DISTRIBUSI=GETDATE()", "IDPERMOHONAN='" & xPK & "'")
    '    End If

    '    If xFungsi.countData("TBL_PERMOHONAN_DETAIL", "IDDETAILPERMOHONAN", "WHERE IDPERMOHONAN='" & xPK & "' AND KET=1") = 0 Then
    '        Dim namafile As String
    '        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
    '        Response.ContentType = "application/pdf"
    '        Response.~Add~Header("content-disposition", "attachment; filename=TandaTerimaPengeluaranBarang.pdf")

    '        Dim doc As New Document(PageSize.A4)

    '        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
    '        doc.Open()
    '        Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
    '        Dim cb As PdfContentByte = wri.DirectContent
    '        Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
    '        Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 9)
    '        Dim para, para2, para3, para4 As New Paragraph("")
    '        Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


    '        Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
    '        Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
    '        Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
    '        Dim pharse As New Phrase(" ")

    '        Dim xTabel As New PdfPTable(2)
    '        xTabel.WidthPercentage = 100
    '        Dim headerwidths() As Integer = {2, 6}
    '        xTabel.SetWidths(headerwidths)
    '        Dim xCell As PdfPCell = Nothing
    '        xTabel.HorizontalAlignment = Element.ALIGN_CENTER

    '        Dim xBarcode As New Barcode128
    '        'xBarcode.SetText(xPK)
    '        xBarcode.Code = xNOFAKTUR
    '        xCell = New PdfPCell(xBarcode.CreateImageWithBarcode(cb, Nothing, Nothing))
    '        'xCell = New PdfPCell(xBarcode.GetImage)
    '        xCell.Colspan = 2
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.FixedHeight = 50
    '        xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabel.AddCell(xCell)


    '        xCell = New PdfPCell(New Phrase(New Chunk("TANDA TERIMA", FontHeader)))
    '        xCell.Colspan = 2
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk("KELUAR GUDANG", FontHeader)))
    '        xCell.Colspan = 2
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabel.AddCell(xCell)


    '        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
    '        xCell.Colspan = 2
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.FixedHeight = 25
    '        xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk("NAMA", FontHeader)))
    '        xCell.Colspan = 1
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)


    '        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontHeader)))
    '        xCell.Colspan = 1
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk("UNIT", FontHeader)))
    '        xCell.Colspan = 1
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & cboUnitKerja.SelectedValue & "'"), FontHeader)))
    '        xCell.Colspan = 1
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk("NIP/NIK", FontHeader)))
    '        xCell.Colspan = 1
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontHeader)))
    '        xCell.Colspan = 1
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
    '        xCell.Colspan = 2
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)

    '        xCell = New PdfPCell(New Phrase(New Chunk("DAFTAR BARANG KELUAR", FontHeader)))
    '        xCell.Colspan = 2
    '        xCell.Border = PdfPCell.NO_BORDER
    '        xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '        xTabel.AddCell(xCell)

    '        xTabel.CompleteRow()

    '        doc.Add(xTabel)
    '        'doc.Add(pharse)


    '        Dim xTabelisi As New PdfPTable(4)
    '        xTabelisi.WidthPercentage = 100
    '        Dim headerwidthsisi() As Integer = {1, 6, 4, 5}
    '        xTabelisi.SetWidths(headerwidthsisi)
    '        Dim xCellisi As PdfPCell = Nothing
    '        xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER

    '        xCellisi = New PdfPCell(New Phrase(New Chunk("NO.", FontHeader)))
    '        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabelisi.AddCell(xCellisi)

    '        xCellisi = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
    '        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabelisi.AddCell(xCellisi)

    '        xCellisi = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
    '        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabelisi.AddCell(xCellisi)

    '        xCellisi = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
    '        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '        xTabelisi.AddCell(xCellisi)

    '        Dim i As Integer
    '        i = 0

    '        Dim myConnection As New SqlConnection()
    '        Try

    '            Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
    '            myConnection.ConnectionString = strConnectionString

    '            Dim strCommandText As String = ""
    '            strCommandText = strCommandText + "SELECT dbo.Getnamabarang(KODE_BARANG) as NAMABARANG,KODE_BARANG,JML_MOHON,JML_PENUHI FROM TBL_PERMOHONAN_DETAIL "
    '            strCommandText = strCommandText + " WHERE IDPERMOHONAN='" & xPK.ToString & "' AND KET=2"

    '            Dim myCommand As New SqlCommand(strCommandText, myConnection)
    '            myConnection.Open()
    '            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()
    '            While strBaca.Read()
    '                i = i + 1

    '                xCellisi = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
    '                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '                xTabelisi.AddCell(xCellisi)

    '                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString, FontIsi)))
    '                xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '                xTabelisi.AddCell(xCellisi)

    '                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("NAMABARANG").ToString, FontIsi)))
    '                xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
    '                xTabelisi.AddCell(xCellisi)

    '                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("JML_PENUHI").ToString, FontIsi)))
    '                xCellisi.HorizontalAlignment = Element.ALIGN_RIGHT
    '                xTabelisi.AddCell(xCellisi)
    '            End While

    '            xCellisi = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
    '            xCellisi.FixedHeight = 30
    '            xCellisi.Colspan = 4
    '            xCellisi.Border = PdfPCell.NO_BORDER
    '            xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelisi.AddCell(xCellisi)

    '            xTabelisi.CompleteRow()
    '            doc.Add(xTabelisi)
    '            doc.Add(pharse)

    '            Dim xTabelBawah As New PdfPTable(4)
    '            xTabelBawah.WidthPercentage = 100
    '            Dim headerwidthsbawah() As Integer = {4, 2, 2, 4}
    '            xTabelisi.SetWidths(headerwidthsisi)
    '            Dim xCellbawah As PdfPCell = Nothing
    '            xTabelBawah.HorizontalAlignment = Element.ALIGN_CENTER

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)


    '            xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN", FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '            xCellbawah.Colspan = 4
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.FixedHeight = 30
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)


    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xNAMAPROSES & "'"), FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xNAMAPROSES & "'"), FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '            xCellbawah.Colspan = 4
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            'xCellbawah.FixedHeight = 15
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk("PETUGAS GUDANG", FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk("PENERIMA", FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)


    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '            xCellbawah.Colspan = 4
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.FixedHeight = 30
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)


    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontIsi)))
    '            xCellbawah.Colspan = 2
    '            xCellbawah.Border = PdfPCell.NO_BORDER
    '            xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '            xTabelBawah.AddCell(xCellbawah)

    '            xTabelBawah.CompleteRow()

    '            doc.Add(xTabelBawah)
    '            'doc.Add(pharse)

    '        Catch ex As Exception
    '            ' write the error to file
    '            Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/error.log"))
    '            sw.WriteLine(ex.Message)
    '            sw.Close()
    '            ' now rethrow the error
    '            'Throw (ex)
    '        Finally
    '            myConnection.Close()
    '        End Try

    '        'xTabel.CompleteRow()

    '        'doc.Add(xTabel)
    '        'doc.Add(pharse)

    '        doc.Close()
    '        Response.Write(doc)
    '        Response.End()

    '    End If
    'Else
    '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
    'End If







    Protected Sub btnSelesai_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Dim xData As New FungsiData
            xData.UpdateData("TBL_PERMOHONAN", "STS_PERMOHONAN=3,TGL_DISTRIBUSI=GETDATE(),IDOPERATOR='" & Session("NAMAUSER") & "'", "IDPERMOHONAN='" & xPK & "'")
            xData.UpdateData("TBL_PERMOHONAN_DETAIL", "KET=1,TGL_DISTRIBUSI=GETDATE()", "IDPERMOHONAN='" & xPK & "'")
            grid.DataBind()

            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order Sudah Selesai')", True)
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
        End If
    End Sub

    Protected Sub btnCetakNota_Click(sender As Object, e As EventArgs)
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Dim xFungsi As New FungsiData
            Dim vpnButton As Button = CType(sender, Button)
            Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
            Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
            Dim xPKMASTER As String = xFungsi.GetData("IDPERMOHONAN", "TBL_PERMOHONAN_TT", " PKTTMOHON='" & xPK & "'")
            Dim xNOTT As String = xFungsi.GetData("NO_TT", "TBL_PERMOHONAN_TT", " PKTTMOHON='" & xPK & "'")
            Dim xPERIHAL As String = xFungsi.GetData("PERIHAL", "TBL_PERMOHONAN", " IDPERMOHONAN='" & xPKMASTER & "'")
            Dim xKDPEMOHON As String = xFungsi.GetData("NAMAUSER", "TBL_PERMOHONAN", " IDPERMOHONAN='" & xPKMASTER & "'")
            Dim xKDADMIN As String = xFungsi.GetData("IDPEMROSES", "TBL_PERMOHONAN", " IDPERMOHONAN='" & xPKMASTER & "'")
            Dim xTGLMOHON As String = xFungsi.GetData("TGL_PERMOHONAN", "TBL_PERMOHONAN", " IDPERMOHONAN='" & xPKMASTER & "'")
            Dim xTGLTERIMA As String = xFungsi.GetData("TGL_CETAK", "TBL_PERMOHONAN_TT", " PKTTMOHON='" & xPK & "'")

            Dim namafile As String
            namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment; filename=TandaTerimaPengeluaranBarang.pdf")

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

            xCell = New PdfPCell(New Phrase(New Chunk("PENGELUARAN GUDANG", FontHeader)))
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

            xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & Session("KDUNKER") & "'"), FontHeader)))
            xCell.Colspan = 1
            xCell.Border = PdfPCell.NO_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("PERIHAL", FontHeader)))
            xCell.Colspan = 1
            xCell.Border = PdfPCell.NO_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xTabel.AddCell(xCell)


            xCell = New PdfPCell(New Phrase(New Chunk(" : " & xPERIHAL, FontHeader)))
            xCell.Colspan = 1
            xCell.Border = PdfPCell.NO_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("TGL PERMOHONAN", FontHeader)))
            xCell.Colspan = 1
            xCell.Border = PdfPCell.NO_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xTabel.AddCell(xCell)


            xCell = New PdfPCell(New Phrase(New Chunk(" : " & xTGLMOHON, FontHeader)))
            xCell.Colspan = 1
            xCell.Border = PdfPCell.NO_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xTabel.AddCell(xCell)

            xCell = New PdfPCell(New Phrase(New Chunk("TGL SERAH TERIMA", FontHeader)))
            xCell.Colspan = 1
            xCell.Border = PdfPCell.NO_BORDER
            xCell.HorizontalAlignment = Element.ALIGN_LEFT
            xTabel.AddCell(xCell)


            xCell = New PdfPCell(New Phrase(New Chunk(" : " & xTGLTERIMA, FontHeader)))
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
            Dim headerwidthsisi() As Integer = {1, 4, 6, 3}
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
                strCommandText = strCommandText + "SELECT dbo.Getnamabarang(KODE_BARANG) as NAMABARANG,KODE_BARANG,JML FROM TBL_PERMOHONAN_TT_DETAIL "
                strCommandText = strCommandText + " WHERE PKTTMOHON='" & xPK & "' AND KET=1"

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


                'xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN", FontIsi)))
                'xCellbawah.Colspan = 2
                'xCellbawah.Border = PdfPCell.NO_BORDER
                'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                'xTabelBawah.AddCell(xCellbawah)

                'xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
                'xCellbawah.Colspan = 4
                'xCellbawah.Border = PdfPCell.NO_BORDER
                'xCellbawah.FixedHeight = 30
                'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                'xTabelBawah.AddCell(xCellbawah)

                'xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
                'xCellbawah.Colspan = 2
                'xCellbawah.Border = PdfPCell.NO_BORDER
                'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                'xTabelBawah.AddCell(xCellbawah)


                'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
                'xCellbawah.Colspan = 2
                'xCellbawah.Border = PdfPCell.NO_BORDER
                'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                'xTabelBawah.AddCell(xCellbawah)

                'xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
                'xCellbawah.Colspan = 2
                'xCellbawah.Border = PdfPCell.NO_BORDER
                'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                'xTabelBawah.AddCell(xCellbawah)

                'xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
                'xCellbawah.Colspan = 2
                'xCellbawah.Border = PdfPCell.NO_BORDER
                'xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                'xTabelBawah.AddCell(xCellbawah)

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

                xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
                xCellbawah.Colspan = 4
                xCellbawah.Border = PdfPCell.NO_BORDER
                xCellbawah.FixedHeight = 30
                xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelBawah.AddCell(xCellbawah)

                'pemindahan kolom ttd admin persediaan level es 2
                xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & Session("KDUNKER") & "'"), FontIsi)))
                xCellbawah.Colspan = 2
                xCellbawah.Border = PdfPCell.NO_BORDER
                xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelBawah.AddCell(xCellbawah)

                xCellbawah = New PdfPCell(New Phrase(New Chunk("................... ", FontIsi)))
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


                xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
                xCellbawah.Colspan = 2
                xCellbawah.Border = PdfPCell.NO_BORDER
                xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelBawah.AddCell(xCellbawah)

                xCellbawah = New PdfPCell(New Phrase(New Chunk("......................", FontIsi)))
                xCellbawah.Colspan = 2
                xCellbawah.Border = PdfPCell.NO_BORDER
                xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelBawah.AddCell(xCellbawah)

                xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
                xCellbawah.Colspan = 4
                xCellbawah.Border = PdfPCell.NO_BORDER
                xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelBawah.AddCell(xCellbawah)

                xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xKDADMIN & "'"), FontIsi)))
                xCellbawah.Colspan = 2
                xCellbawah.Border = PdfPCell.NO_BORDER
                xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelBawah.AddCell(xCellbawah)

                xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
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

            'xTabel.CompleteRow()

            'doc.Add(xTabel)
            'doc.Add(pharse)

            doc.Close()
            Response.Write(doc)
            Response.End()
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Mohon ganti kertas dengan kertas bekas!')", True)
        End If



    End Sub
    Protected Sub gridMohonTT_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDPERMOHONAN") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub
    Protected Sub grid_DataBound(sender As Object, e As EventArgs)
        CType(sender, ASPxGridView).DetailRows.ExpandAllRows()
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
    'Protected Sub gridMohonTT_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
    '    Dim detailgrid As ASPxGridView = CType(sender, ASPxGridView)
    '    Dim xPK As String = detailgrid.GetMasterRowKeyValue()
    '    Dim xFU As FileUpload = CType(grid.FindEditRowCellTemplateControl(CType(grid.Columns(5), GridViewDataColumn), "FileUpload1"), FileUpload)

    '    If xFU.HasFile Then
    '        Try
    '            Dim xFileUpload As String = xPK & Path.GetExtension(xFU.FileName)

    '            Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/TTPermohonan/" & Year(Date.Now)))
    '            If Not Directory.Exists(directoryPath) Then
    '                Directory.CreateDirectory(directoryPath)
    '                xFU.SaveAs(directoryPath & "\" & xFileUpload)

    '            Else
    '                xFU.SaveAs(directoryPath & "\" & xFileUpload)
    '            End If
    '        Catch ex As Exception
    '            Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/errorPersediaan.log"))
    '            sw.WriteLine(ex.Message)
    '            sw.Close()
    '            ' now rethrow the error
    '            Throw (ex)

    '        End Try


    '    End If
    'End Sub
    Protected Sub btnUploadTT_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Session("PKTTMOHONDRPROSES") = xPK
        Dim xFileName As String = Session("PKTTMOHONDRPROSES") & ".pdf"
        ' MsgBox(xFileUpload)
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/TTPermohonan/" & Year(Date.Now)))
        If File.Exists(directoryPath & xFileName) Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('File sudah pernah diupload!')", True)
        Else
            Response.Redirect("PermohonanTTUpload.aspx")

        End If


    End Sub
    Protected Sub btnCekTT_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Session("PKTTMOHONDRPROSES") = xPK
        Dim xFileName As String = Session("PKTTMOHONDRPROSES") & ".pdf"
        ' MsgBox(xFileUpload)
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/TTPermohonan/" & Year(Date.Now)))
        If File.Exists(directoryPath & xFileName) Then

            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Bukti tanda terima belum diupload!')", True)

        End If
    End Sub
End Class
