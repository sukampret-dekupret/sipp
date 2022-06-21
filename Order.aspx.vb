Imports System.Data.SqlClient
Imports DevExpress.Web
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Partial Class Order
    Inherits System.Web.UI.Page

    Private Sub Order_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False
            cboTahun.SelectedValue = Year(Date.Now)
            cboStatus.SelectedValue = 0
        End If
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As ImageClickEventArgs)
        Dim xTombol As ImageButton = CType(sender, ImageButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        Dim xFungsi As New FungsiData
        Dim xStatus As String = xFungsi.GetData("STATUS", "TBL_ORDER", " KDORDER='" & xPK & "'")
        If xStatus = "1" Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order Barang sudah diproses tidak bisa dirubah')", True)
        Else
            Session("UbahOrder") = True
            Session("IDORDERUBAH") = xPK
            Response.Redirect("Order.aspx")
        End If

    End Sub
    Protected Sub btnHapus_Click(sender As Object, e As ImageClickEventArgs)
        Dim xTombol As ImageButton = CType(sender, ImageButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFungsi As New FungsiData
        Dim xStatus As String = xFungsi.GetData("STATUS", "TBL_ORDER", " KDORDER='" & xPK & "'")
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            If xStatus = "1" Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order/Permohonan Barang sudah diproses tidak bisa dirubah')", True)
            Else
                xFungsi.delData("TBL_ORDER", " WHERE KDORDER='" & xPK & "'")
                xFungsi.delData("TBL_ORDER_DETAIL", " WHERE KDORDER='" & xPK & "'")
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order/Permohonan Barang sudah berhasil dihapus')", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('You clicked NO!')", True)
        End If



    End Sub

    Private Sub grid_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs) Handles grid.CommandButtonInitialize
        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        Dim xSTSMOHON As Integer = xGRID.GetRowValues(e.VisibleIndex, "STATUS")

        If xSTSMOHON > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Delete Then
                e.Visible = False
            End If
        End If
    End Sub
    Protected Sub gridDetail_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        'Dim xPK As Integer = xGRID.GetRowValues(e.VisibleIndex, "IDETAILORDER")
        Dim xPKMASTER As String = xGRID.GetMasterRowKeyValue
        Dim xFungsi As New FungsiData
        Dim xSTATUS As Integer = xFungsi.GetData("STATUS", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        If xSTATUS > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Edit Then
                e.Visible = False
            ElseIf e.ButtonType = ColumnCommandButtonType.Delete Then
                e.Visible = False
            ElseIf e.ButtonType = ColumnCommandButtonType.[New] Then
                e.Visible = False
            End If
        End If

    End Sub
    Protected Sub btnCetakPengantar_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFungsi As New FungsiData
        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=PengantarOrderBarang.pdf")

        Dim doc As New Document(PageSize.A4)
        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)
        doc.Open()
        Dim font18 As Font = FontFactory.GetFont("ARIAL", 18)
        Dim cb As PdfContentByte = wri.DirectContent
        Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
        Dim FontHeader2 As Font = FontFactory.GetFont("ARIAL", 14, Font.BOLD And Font.UNDERLINE)
        Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 11)
        Dim para, para2, para3, para4 As New Paragraph("")
        Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")


        Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
        Dim courier As Font = New Font(base, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)
        Dim courierGaris As Font = New Font(base, 10, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK)
        Dim pharse As New Phrase(" ")

        Dim xTabel As New PdfPTable(2)
        xTabel.WidthPercentage = 90
        Dim headerwidths() As Integer = {2, 6}
        xTabel.SetWidths(headerwidths)
        Dim xCell As PdfPCell = Nothing
        xTabel.HorizontalAlignment = Element.ALIGN_CENTER

        'Dim xBarcode As New Barcode128
        ''xBarcode.SetText(xPK)
        'xBarcode.Code = xPK
        'xCell = New PdfPCell(xBarcode.CreateImageWithBarcode(cb, Nothing, Nothing))
        ''xCell = New PdfPCell(xBarcode.GetImage)
        'xCell.Colspan = 2
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.FixedHeight = 50
        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("   ", FontHeader)))
        xCell.Colspan = 2
        xCell.FixedHeight = 100
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("PERMOHONANAN BARANG", FontHeader)))
        'xCell.Colspan = 2
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_CENTER
        'xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NOTA DINAS", FontHeader2)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        'xCell = New PdfPCell(New Phrase(New Chunk("UNIT : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & cboUnitKerja.SelectedValue & "'"), FontHeader)))
        Dim xNota As String
        xNota = xFungsi.GetData("NO_NOTA", "TBL_ORDER", "KDORDER='" & xPK & "'")
        xCell = New PdfPCell(New Phrase(New Chunk("Nomor : " & xNota, FontIsi)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("   ", FontHeader)))
        xCell.Colspan = 2
        xCell.FixedHeight = 25
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        'xCell = New PdfPCell(New Phrase(New Chunk("No. Nota", FontIsi)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)


        'xCell = New PdfPCell(New Phrase(New Chunk(" : " & xNota, FontIsi)))
        'xCell.Colspan = 1
        'xCell.Border = PdfPCell.NO_BORDER
        'xCell.HorizontalAlignment = Element.ALIGN_LEFT
        'xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("Tanggal  ", FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        Dim xTanggal As Date
        xTanggal = xFungsi.GetData("TGL_NOTA", "TBL_ORDER", "KDORDER='" & xPK & "'")
        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xTanggal.ToString("dd-MM-yyyy"), FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("Kepada ", FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        Dim xUnker As String
        xUnker = xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & Session("KDUNKERATAS") & "'")
        xCell = New PdfPCell(New Phrase(New Chunk(" : Yth. " & xUnker, FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("Dari", FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & Session("KDUNKER") & "'"), FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("hal ", FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("PERIHAL", "TBL_ORDER", "KDORDER='" & xPK & "'"), FontIsi)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("___________________________________________________________________________________ ", FontHeader)))
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)

        xTabel.CompleteRow()

        doc.Add(xTabel)

        Dim xTabelKata As New PdfPTable(4)
        xTabelKata.WidthPercentage = 90
        Dim xheaderwidthsKata() As Integer = {2, 2, 2, 2}
        xTabelKata.SetWidths(xheaderwidthsKata)
        Dim xCEllKata As PdfPCell = Nothing
        xTabelKata.HorizontalAlignment = Element.ALIGN_CENTER

        xCEllKata = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCEllKata.Colspan = 4
        xCEllKata.Border = PdfPCell.NO_BORDER
        xCEllKata.HorizontalAlignment = Element.ALIGN_JUSTIFIED
        xTabelKata.AddCell(xCEllKata)

        xCEllKata = New PdfPCell(New Phrase(New Chunk("Merujuk Pokok Nota Dinas " & xNota & " diatas, bersama ini disampaikan permohonan barang persediaan untuk " & xUnker & " dengan daftar sebagai berikut :", FontIsi)))
        xCEllKata.Colspan = 4
        xCEllKata.Border = PdfPCell.NO_BORDER
        xCEllKata.HorizontalAlignment = Element.ALIGN_JUSTIFIED
        xTabelKata.AddCell(xCEllKata)

        xCEllKata = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
        xCEllKata.Colspan = 4
        xCEllKata.Border = PdfPCell.NO_BORDER
        xCEllKata.HorizontalAlignment = Element.ALIGN_JUSTIFIED
        xTabelKata.AddCell(xCEllKata)

        doc.Add(xTabelKata)

        'Dim xText As String = "Merujuk Pokok Nota Dinas " & xNota & " diatas, bersama ini disampaikan permohonan barang persediaan untuk " & xUnker & " dengan daftar sebagai berikut :"
        'Dim xChunk As New Chunk(xText, FontIsi)
        'Dim xPhrase As New Phrase(xChunk)
        'Dim xPara As New Paragraph()
        'xPara.FirstLineIndent = 20
        'xPara.Alignment = Element.ALIGN_JUSTIFIED
        'xPara.Add(xPhrase)

        'doc.Add(xPara)
        'doc.Add(pharse)

        Dim xTabelISI As New PdfPTable(5)
        xTabelISI.WidthPercentage = 80
        Dim xheaderwidths() As Integer = {1, 4, 6, 2, 2}
        xTabelISI.SetWidths(xheaderwidths)
        Dim xCellISI As PdfPCell = Nothing
        xTabelISI.HorizontalAlignment = Element.ALIGN_CENTER


        xCellISI = New PdfPCell(New Phrase(New Chunk("NO.", FontHeader)))
        xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelISI.AddCell(xCellISI)

        xCellISI = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
        xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelISI.AddCell(xCellISI)

        xCellISI = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
        xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelISI.AddCell(xCellISI)

        xCellISI = New PdfPCell(New Phrase(New Chunk("JUMLAH", FontHeader)))
        xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelISI.AddCell(xCellISI)

        xCellISI = New PdfPCell(New Phrase(New Chunk("SATUAN", FontHeader)))
        xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
        xTabelISI.AddCell(xCellISI)


        Dim i As Integer
        i = 0

        Dim myConnection As New SqlConnection()
        Try

            Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
            myConnection.ConnectionString = strConnectionString

            Dim strCommandText As String = ""
            strCommandText = strCommandText + "SELECT KODE_BARANG,dbo.getnamabarang(KODE_BARANG) as NAMA_BARANG,MOHON,dbo.getsatuanbarang(KODE_BARANG) as SATUAN FROM TBL_ORDER_DETAIL "
            strCommandText = strCommandText + " WHERE KDORDER='" & xPK.ToString & "'"

            Dim myCommand As New SqlCommand(strCommandText, myConnection)
            myConnection.Open()
            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()
            While strBaca.Read()
                i = i + 1

                xCellISI = New PdfPCell(New Phrase(New Chunk(i.ToString & ".", FontIsi)))
                xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelISI.AddCell(xCellISI)

                xCellISI = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString, FontIsi)))
                xCellISI.HorizontalAlignment = Element.ALIGN_LEFT
                xTabelISI.AddCell(xCellISI)

                xCellISI = New PdfPCell(New Phrase(New Chunk(strBaca("NAMA_BARANG").ToString, FontIsi)))
                xCellISI.HorizontalAlignment = Element.ALIGN_LEFT
                xTabelISI.AddCell(xCellISI)

                xCellISI = New PdfPCell(New Phrase(New Chunk(strBaca("MOHON").ToString, FontIsi)))
                xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelISI.AddCell(xCellISI)

                xCellISI = New PdfPCell(New Phrase(New Chunk(strBaca("SATUAN").ToString, FontIsi)))
                xCellISI.HorizontalAlignment = Element.ALIGN_CENTER
                xTabelISI.AddCell(xCellISI)
            End While


            xCellISI = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCellISI.HorizontalAlignment = Element.ALIGN_LEFT
            xCellISI.Border = PdfPCell.NO_BORDER
            xCellISI.FixedHeight = 20
            xCellISI.Colspan = 5
            xTabelISI.AddCell(xCellISI)

            xTabelISI.CompleteRow()

            doc.Add(xTabelISI)
            'doc.Add(New Paragraph("\n"))

            xTabelKata = New PdfPTable(4)
            ''xCEllKata = New PdfPCell

            xTabelKata.WidthPercentage = 90
            xCEllKata = New PdfPCell(New Phrase(New Chunk("Sekiranya tidak ada pertimbangan lain, mohon kiranya permohonan tersebut dapat dipenuhi pada kesempatan pertama.", FontIsi)))
            xCEllKata.Colspan = 4
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_JUSTIFIED
            xTabelKata.AddCell(xCEllKata)

            xCEllKata = New PdfPCell(New Phrase(New Chunk("Demikian disampaikan, atas perhatian dan kerjasamanya diucapkan terima kasih.", FontIsi)))
            xCEllKata.Colspan = 4
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_JUSTIFIED
            xTabelKata.AddCell(xCEllKata)

            xCEllKata = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCEllKata.Colspan = 4
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_RIGHT
            xTabelKata.AddCell(xCEllKata)

            xCEllKata = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCEllKata.Colspan = 3
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_RIGHT
            xTabelKata.AddCell(xCEllKata)

            xCEllKata = New PdfPCell(New Phrase(New Chunk("Jakarta, " & Date.Now.ToString("dd MMM yyyy"), FontIsi)))
            xCEllKata.Colspan = 1
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_LEFT
            xTabelKata.AddCell(xCEllKata)

            xCEllKata = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCEllKata.Colspan = 4
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_RIGHT
            xTabelKata.AddCell(xCEllKata)

            xCEllKata = New PdfPCell(New Phrase(New Chunk(" ", FontIsi)))
            xCEllKata.Colspan = 3
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_RIGHT
            xTabelKata.AddCell(xCEllKata)

            xCEllKata = New PdfPCell(New Phrase(New Chunk("_________________", FontIsi)))
            xCEllKata.Colspan = 1
            xCEllKata.Border = PdfPCell.NO_BORDER
            xCEllKata.HorizontalAlignment = Element.ALIGN_LEFT
            xTabelKata.AddCell(xCEllKata)

            doc.Add(xTabelKata)

            'Dim xText2 As String = "Sekiranya tidak ada pertimbangan lain, mohon kiranya permohonan tersebut dapat dipenuhi pada kesempatan pertama."
            'Dim xChunk2 As New Chunk(xText2, FontIsi)
            'Dim xPhrase2 As New Phrase(xChunk2)
            'Dim xPara2 As New Paragraph
            'xPara2.FirstLineIndent = 20
            'xPara2.Alignment = Element.ALIGN_JUSTIFIED
            'xPara2.Add(xPhrase2)

            'doc.Add(xPhrase2)

            'Dim xText3 As String = "Demikian disampaikan, atas perhatian dan kerjasamanya diucapkan terima kasih."
            'Dim xChunk3 As New Chunk(xText3, FontIsi)
            'Dim xPhrase3 As New Phrase(xChunk3)
            'Dim xPara3 As New Paragraph
            'xPara3.FirstLineIndent = 20
            'xPara3.Alignment = Element.ALIGN_JUSTIFIED
            'xPara3.Add(xPhrase3)

            'doc.Add(xPhrase3)

            'Dim xText4 As String = "Jakarta, " & Date.Now.ToString("dd MMM yyyy")
            'Dim xChunk4 As New Chunk(xText4, FontIsi)
            'Dim xPhrase4 As New Phrase(xChunk4)
            'Dim xPara4 As New Paragraph
            'xPara4.FirstLineIndent = 20
            'xPara4.Alignment = Element.ALIGN_RIGHT
            'xPara4.Add(xPhrase4)

            'doc.Add(xPhrase4)



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

        doc.Close()
        Response.Write(doc)
        Response.End()
    End Sub
    'Protected Sub grid_CustomUnboundColumnData(sender As Object, e As ASPxGridViewColumnDataEventArgs)
    '    If e.Column.FieldName = "NO" Then
    '        e.Value = String.Format("Item #{0}", e.ListSourceRowIndex + 1)
    '    End If
    'End Sub
    Protected Sub grid_CustomColumnDisplayText(sender As Object, e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.Caption = "№" Then
            e.DisplayText = (e.VisibleIndex + 1).ToString()
        End If
    End Sub
    Protected Sub grid_RowDeleted(sender As Object, e As Data.ASPxDataDeletedEventArgs)
        Dim xPK As String = e.Values("KDORDER").ToString()
        Dim xFungsi As New FungsiData
        xFungsi.delData("TBL_ORDER_DETAIL", " WHERE KDORDER='" & xPK & "'")
    End Sub
    Protected Sub gridDetail_InitNewRow(sender As Object, e As Data.ASPxDataInitNewRowEventArgs)
        e.NewValues("MOHON") = 10
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

    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("KDORDER") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub
End Class
