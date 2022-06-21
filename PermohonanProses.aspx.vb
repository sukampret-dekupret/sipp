Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO

Partial Class PermohonanProses
    Inherits System.Web.UI.Page
    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDPERMOHONAN") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue().ToString
    End Sub

    Private Sub PermohonanProses_Load(sender As Object, e As EventArgs) Handles Me.Load

        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If (Not IsPostBack) Then

            cboTahun.SelectedValue = Year(Date.Now)
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False
            cboSTATUSPermohonan.SelectedValue = 0
        End If
    End Sub
    Protected Sub btnProses_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As DevExpress.Web.GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xKET As String = CInt(Container.Grid.GetRowValues(Container.VisibleIndex, "KETLEBIH"))
        'Dim xCEKORDER As Integer = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "CEKORDER").ToString)

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Dim xData As New FungsiData

            If xKET >= 1 Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Jumlah yang dipenuhi melebihi stok')", True)
            Else
                Try
                    xData.UpdateData("TBL_PERMOHONAN", "STS_PERMOHONAN=1,TGL_PROSES=GETDATE(),IDPEMROSES='" & Session("NAMAUSER") & "'", "IDPERMOHONAN='" & xPK & "'")
                    xData.UpdateData("TBL_PERMOHONAN_DETAIL", "TGL_PROSES=GETDATE()", "IDPERMOHONAN='" & xPK & "'")
                Catch ex As Exception
                Finally
                    grid.DataBind()
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan telah berhasil diproses')", True)
                End Try
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
        End If


        'SKRIP LAMA
        'sqlPermohonan.UpdateParameters("STS_PERMOHONAN").DefaultValue = 1
        'sqlPermohonan.UpdateParameters("IDPEMROSES").DefaultValue = Session("USERNAME")
        'sqlPermohonan.UpdateParameters("TGL_PROSES").DefaultValue = Date.Now
        'sqlPermohonan.UpdateParameters("IDPERMOHONAN").DefaultValue = xPK
        'sqlPermohonan.Update()
        'Dim confirmValue As String = Request.Form("confirm_value")
        'If confirmValue = "Yes" Then


        '    If xKET > 0 Then
        '        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan melebihi stok, mohon lakukan pengadaan terlebih dahulu atau tolak jika tidak urgent')", True)

        '    Else
        '        Dim xData As New FungsiData
        '        xData.UpdateData("TBL_PERMOHONAN", "STS_PERMOHONAN=1,TGL_PROSES=GETDATE(),IDPEMROSES='" & Session("NAMAUSER") & "'", "IDPERMOHONAN='" & xPK & "'")
        '        xData.UpdateData("TBL_PERMOHONAN_DETAIL", "TGL_PROSES=GETDATE()", "IDPERMOHONAN='" & xPK & "'")
        '        grid.DataBind()

        '        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan telah diproses, cek permohonan history untuk proses lebih lanjut')", True)
        '    End If


        'Else
        '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
        'End If
    End Sub
    Protected Sub btnTolak_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            'sqlPermohonan.UpdateParameters("STS_PERMOHONAN").DefaultValue = 2
            'sqlPermohonan.UpdateParameters("IDPEMROSES").DefaultValue = Session("USERNAME")
            'sqlPermohonan.UpdateParameters("TGL_PROSES").DefaultValue = Date.Now
            'sqlPermohonan.UpdateParameters("IDPERMOHONAN").DefaultValue = xPK
            'sqlPermohonan.Update()

            Dim xData As New FungsiData
            xData.UpdateData("TBL_PERMOHONAN", "STS_PERMOHONAN=2,TGL_PROSES=GETDATE(),IDPEMROSES='" & Session("NAMAUSER") & "'", "IDPERMOHONAN='" & xPK & "'")
            xData.UpdateData("TBL_PERMOHONAN_DETAIL", "JML_PENUHI=0,TGL_PROSES=GETDATE()", "IDPERMOHONAN='" & xPK & "'")
            grid.DataBind()

            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan telah ditolak semua')", True)

        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
        End If

    End Sub
    Protected Sub btnProsesPengadaan_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            'sqlPermohonan.UpdateParameters("STS_PERMOHONAN").DefaultValue = 2
            'sqlPermohonan.UpdateParameters("IDPEMROSES").DefaultValue = Session("USERNAME")
            'sqlPermohonan.UpdateParameters("TGL_PROSES").DefaultValue = Date.Now
            'sqlPermohonan.UpdateParameters("IDPERMOHONAN").DefaultValue = xPK
            'sqlPermohonan.Update()

            Dim xData As New FungsiData
            Dim xPKMASTER As String = xData.GetData("IDPERMOHONAN", "TBL_PERMOHONAN", "IDDETAILPERMOHONAN='" & xPK & "'")
            xData.UpdateData("TBL_PERMOHONAN_DETAIL", "JML_PENUHI=0", "IDDETAILPERMOHONAN='" & xPK & "'")
            'xData.UpdateData("TBL_PERMOHONAN_DETAIL", "KET=3", "IDDETAILPERMOHONAN='" & xPK & "'")
            grid.DataBind()

            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan anda telah diproses untuk pengadaan')", True)

        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
        End If
    End Sub
    Protected Sub gridDetail_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)

        Dim xMinta As Integer = e.OldValues("JML_MOHON")
        Dim xPenuhi As Integer = e.NewValues("JML_PENUHI")
        Dim xStok As Integer = e.OldValues("STOK1")
        Dim xStokReserved As Integer = e.NewValues("STOKRESERVED")

        ' Dim xKunci As String = sender.Columns("STOK")
        'Label1.Text = xStok
        If xPenuhi > xMinta Then
            AddError(e.Errors, sender.Columns("JML_PENUHI"), "Permohonan lebih besar daripada permintaan")
            e.RowError = "Jumlah yang dipenuhi lebih besar daripada permintaan"
            ''e.RowError = xMinta.ToString & "Penuhi" & xPenuhi.ToString
            'ElseIf xPenuhi > (xStok + xStokReserved) Then
            ''ElseIf (xPenuhi + xStokReserved) > xStok Then
        ElseIf xPenuhi > xStok Then
            AddError(e.Errors, sender.Columns("JML_PENUHI"), "Permohonan lebih besar daripada stok ")
            e.RowError = "Mohon kurangi jumlah barang sesuai dengan stok"
            ''e.RowError = xPenuhi.ToString & "STOK" & 
        End If
    End Sub

    Private Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If errors.ContainsKey(column) Then
            Return
        End If
        errors(column) = errorText
    End Sub
    Protected Sub gridDetail_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)
        Dim xGrid = TryCast(sender, ASPxGridView)
        If e.RowType = GridViewRowType.Data Then
            If e.GetValue("JML_PENUHI") > e.GetValue("STOK1") Then
                e.Row.BackColor = System.Drawing.Color.Yellow
            ElseIf (e.GetValue("JML_PENUHI") + e.GetValue("STOKRESERVED")) > e.GetValue("STOK1") Then
                e.Row.BackColor = System.Drawing.Color.Red
            End If
        End If
    End Sub
    Protected Sub gridDetail_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        'Dim xPK As Integer = xGRID.GetRowValues(e.VisibleIndex, "IDETAILORDER")
        Dim xPKMASTER As String = xGRID.GetMasterRowKeyValue
        Dim xFungsi As New FungsiData
        Dim xSTATUS As Integer = xFungsi.GetData("STS_PERMOHONAN", "TBL_PERMOHONAN", " IDPERMOHONAN='" & xPKMASTER & "'")
        If xSTATUS > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Edit Then
                e.Visible = False
            End If
        End If
    End Sub

    Private Sub grid_DataBound(sender As Object, e As EventArgs) Handles grid.DataBound

        If cboSTATUSPermohonan.SelectedValue = 1 Or cboSTATUSPermohonan.SelectedValue = 2 Or cboSTATUSPermohonan.SelectedValue = 4 Then
            grid.Columns("PILIHAN").Visible = False
        ElseIf cboSTATUSPermohonan.SelectedValue = 0 Or cboSTATUSPermohonan.SelectedValue = 3 Then
            grid.Columns("PILIHAN").Visible = True
        End If

        CType(sender, ASPxGridView).DetailRows.ExpandAllRows()

    End Sub
    Protected Sub btnProseskePengadaan_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then

            Dim xData As New FungsiData
            xData.UpdateData("TBL_PERMOHONAN", "STS_PERMOHONAN=3,TGL_PROSES=GETDATE(),IDPEMROSES='" & Session("NAMAUSER") & "'", "IDPERMOHONAN='" & xPK & "'")
            xData.UpdateData("TBL_PERMOHONAN_DETAIL", "TGL_PROSES=GETDATE()", "IDPERMOHONAN='" & xPK & "'")
            grid.DataBind()

            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan akan dipenuhi setelah dilakukan pengadaan')", True)

        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan!')", True)
        End If
    End Sub
    'Protected Sub btnPrintPermohonan_Click(sender As Object, e As EventArgs) Handles btnPrintPermohonan.Click
    '    'Dim namafile As String
    '    'namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
    '    'Response.ContentType = "application/pdf"
    '    'Response.AddHeader("content-disposition", "attachment; filename=Permohonan.pdf")

    '    'Dim doc As New Document(PageSize.A4)

    '    'Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
    '    'doc.Open()
    '    'Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
    '    'Dim cb As PdfContentByte = wri.DirectContent
    '    'Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
    '    'Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 9)
    '    'Dim para, para2, para3, para4 As New Paragraph("")
    '    'Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


    '    'Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
    '    'Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
    '    'Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
    '    'Dim pharse As New Phrase(" ")

    '    'Dim xTabel As New PdfPTable(2)
    '    'xTabel.WidthPercentage = 100
    '    'Dim headerwidths() As Integer = {2, 6}
    '    'xTabel.SetWidths(headerwidths)
    '    'Dim xCell As PdfPCell = Nothing
    '    'xTabel.HorizontalAlignment = Element.ALIGN_CENTER

    '    'xCell = New PdfPCell(New Phrase(New Chunk("LAPORAN DAFTAR PERMOHONAN BARANG", FontHeader)))
    '    'xCell.Colspan = 2
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk("PER TGL :" & Date.Now.ToString("dd MMM yyyy"), FontHeader)))
    '    'xCell.Colspan = 2
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    'xTabel.AddCell(xCell)


    '    'xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
    '    'xCell.Colspan = 2
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.FixedHeight = 25
    '    'xCell.HorizontalAlignment = Element.ALIGN_CENTER
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk("NAMA", FontHeader)))
    '    'xCell.Colspan = 1
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)


    '    'xCell = New PdfPCell(New Phrase(New Chunk(" :  " & xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontHeader)))
    '    'xCell.Colspan = 1
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk("UNIT", FontHeader)))
    '    'xCell.Colspan = 1
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & cboUnitKerja.SelectedValue & "'"), FontHeader)))
    '    'xCell.Colspan = 1
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk("NIP/NIK", FontHeader)))
    '    'xCell.Colspan = 1
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontHeader)))
    '    'xCell.Colspan = 1
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
    '    'xCell.Colspan = 2
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)

    '    'xCell = New PdfPCell(New Phrase(New Chunk("DAFTAR BARANG KELUAR", FontHeader)))
    '    'xCell.Colspan = 2
    '    'xCell.Border = PdfPCell.NO_BORDER
    '    'xCell.HorizontalAlignment = Element.ALIGN_LEFT
    '    'xTabel.AddCell(xCell)

    '    'xTabel.CompleteRow()

    '    'doc.Add(xTabel)
    '    ''doc.Add(pharse)


    '    'Dim xTabelisi As New PdfPTable(4)
    '    'xTabelisi.WidthPercentage = 100
    '    'Dim headerwidthsisi() As Integer = {1, 6, 4, 5}
    '    'xTabelisi.SetWidths(headerwidthsisi)
    '    'Dim xCellisi As PdfPCell = Nothing
    '    'xTabelisi.HorizontalAlignment = Element.ALIGN_CENTER

    '    'xCellisi = New PdfPCell(New Phrase(New Chunk("NO.", FontHeader)))
    '    'xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '    'xTabelisi.AddCell(xCellisi)

    '    'xCellisi = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
    '    'xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '    'xTabelisi.AddCell(xCellisi)

    '    'xCellisi = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
    '    'xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '    'xTabelisi.AddCell(xCellisi)

    '    'xCellisi = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
    '    'xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '    'xTabelisi.AddCell(xCellisi)

    '    'Dim i As Integer
    '    'i = 0

    '    'Dim myConnection As New SqlConnection()
    '    'Try

    '    '    Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
    '    '    myConnection.ConnectionString = strConnectionString

    '    '    Dim strCommandText As String = ""
    '    '    strCommandText = strCommandText + "SELECT dbo.Getnamabarang(KODE_BARANG) as NAMABARANG,KODE_BARANG,JML_MOHON,JML_PENUHI FROM TBL_PERMOHONAN_DETAIL "
    '    '    strCommandText = strCommandText + " WHERE IDPERMOHONAN='" & xPK.ToString & "' AND KET=2"

    '    '    Dim myCommand As New SqlCommand(strCommandText, myConnection)
    '    '    myConnection.Open()
    '    '    Dim strBaca As SqlDataReader = myCommand.ExecuteReader()
    '    '    While strBaca.Read()
    '    '        i = i + 1

    '    '        xCellisi = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
    '    '        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '    '        xTabelisi.AddCell(xCellisi)

    '    '        xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString, FontIsi)))
    '    '        xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '    '        xTabelisi.AddCell(xCellisi)

    '    '        xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("NAMABARANG").ToString, FontIsi)))
    '    '        xCellisi.HorizontalAlignment = Element.ALIGN_LEFT
    '    '        xTabelisi.AddCell(xCellisi)

    '    '        xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("JML_PENUHI").ToString, FontIsi)))
    '    '        xCellisi.HorizontalAlignment = Element.ALIGN_RIGHT
    '    '        xTabelisi.AddCell(xCellisi)
    '    '    End While

    '    '    xCellisi = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
    '    '    xCellisi.FixedHeight = 30
    '    '    xCellisi.Colspan = 4
    '    '    xCellisi.Border = PdfPCell.NO_BORDER
    '    '    xCellisi.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelisi.AddCell(xCellisi)

    '    '    xTabelisi.CompleteRow()
    '    '    doc.Add(xTabelisi)
    '    '    doc.Add(pharse)

    '    '    Dim xTabelBawah As New PdfPTable(4)
    '    '    xTabelBawah.WidthPercentage = 100
    '    '    Dim headerwidthsbawah() As Integer = {4, 2, 2, 4}
    '    '    xTabelisi.SetWidths(headerwidthsisi)
    '    '    Dim xCellbawah As PdfPCell = Nothing
    '    '    xTabelBawah.HorizontalAlignment = Element.ALIGN_CENTER

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)


    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk("ADMIN", FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '    '    xCellbawah.Colspan = 4
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.FixedHeight = 30
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)


    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xNAMAPROSES & "'"), FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xNAMAPROSES & "'"), FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '    '    xCellbawah.Colspan = 4
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    'xCellbawah.FixedHeight = 15
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk("PETUGAS GUDANG", FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk("PENERIMA", FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)


    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
    '    '    xCellbawah.Colspan = 4
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.FixedHeight = 30
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)


    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xCellbawah = New PdfPCell(New Phrase(New Chunk(xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & xNAMAUSER & "'"), FontIsi)))
    '    '    xCellbawah.Colspan = 2
    '    '    xCellbawah.Border = PdfPCell.NO_BORDER
    '    '    xCellbawah.HorizontalAlignment = Element.ALIGN_CENTER
    '    '    xTabelBawah.AddCell(xCellbawah)

    '    '    xTabelBawah.CompleteRow()

    '    '    doc.Add(xTabelBawah)
    '    '    'doc.Add(pharse)

    '    'Catch ex As Exception
    '    '    ' write the error to file
    '    '    Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/error.log"))
    '    '    sw.WriteLine(ex.Message)
    '    '    sw.Close()
    '    '    ' now rethrow the error
    '    '    'Throw (ex)
    '    'Finally
    '    '    myConnection.Close()
    '    'End Try

    '    ''xTabel.CompleteRow()

    '    ''doc.Add(xTabel)
    '    ''doc.Add(pharse)

    '    'doc.Close()
    '    'Response.Write(doc)
    '    'Response.End()
    'End Sub
    Protected Sub grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "KETLEBIH" Then
            If e.CellValue > 0 Then
                e.Cell.BackColor = System.Drawing.Color.Red
            End If
        End If
    End Sub
    Protected Sub gridDetail_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        grid.DataBind()
    End Sub

    Protected Sub grid_CustomCallback(sender As Object, e As ASPxGridViewCustomCallbackEventArgs)
        grid.DataBind()
    End Sub
End Class
