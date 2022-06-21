Imports System.Data.SqlClient
Imports DevExpress.Web
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Partial Class Permohonan
    Inherits System.Web.UI.Page

    Private Sub Permohonan_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            cboTahun.SelectedValue = Year(Date.Now)
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False

        End If
    End Sub
    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDPERMOHONAN") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
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
            ElseIf e.ButtonType = ColumnCommandButtonType.Delete Then
                e.Visible = False
            ElseIf e.ButtonType = ColumnCommandButtonType.[New] Then
                e.Visible = False
            End If
        End If

        'Dim xID As String = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        'Dim xFungsiData As New FungsiData
        'Dim xSTS As Integer = xFungsiData.GetData("STS_PERMOHONAN", "TBL_PERMOHONAN", "IDPERMOHONAN='" & xID & "'")
        'Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        'Dim xSTSMOHON As Integer = xGRID.GetMasterRowFieldValues("STS_PERMOHONAN")
        'If xSTSMOHON > 0 Then
        '    If e.ButtonType = ColumnCommandButtonType.Edit Then
        '        e.Visible = False
        '    End If
        '    If e.ButtonType = ColumnCommandButtonType.New Then
        '        e.Visible = False
        '    End If
        '    If e.ButtonType = ColumnCommandButtonType.Delete Then
        '        e.Visible = False
        '    End If
        'End If


    End Sub
    Protected Sub grid_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        Dim xSTSMOHON As Integer = xGRID.GetRowValues(e.VisibleIndex, "STS_PERMOHONAN")

        If xSTSMOHON > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Delete Then
                e.Visible = False
            End If
        End If
    End Sub
    Protected Sub gridDetail_InitNewRow(sender As Object, e As Data.ASPxDataInitNewRowEventArgs)
        e.NewValues("JML_MOHON") = 0
    End Sub
    Protected Sub btnCetak_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xNOFAKTUR As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "NO_PERMOHONAN").ToString)
        Dim xFungsi As New FungsiData

        Dim namafile As String
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment; filename=LaporanPenerimaan.pdf")

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
        xBarcode.Code = xNOFAKTUR
        xCell = New PdfPCell(xBarcode.CreateImageWithBarcode(cb, Nothing, Nothing))
        'xCell = New PdfPCell(xBarcode.GetImage)
        xCell.Colspan = 2
        xCell.Border = PdfPCell.NO_BORDER
        xCell.FixedHeight = 50
        xCell.HorizontalAlignment = Element.ALIGN_CENTER
        xTabel.AddCell(xCell)


        xCell = New PdfPCell(New Phrase(New Chunk("PERMOHONAN BARANG", FontHeader)))
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


        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NAMA", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("UNIT", FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("KET_UNKER", "TBL_UNKER", "KD_UNKER='" & cboUnitKerja.SelectedValue & "'"), FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk("NIP/NIK", FontHeader)))
        xCell.Colspan = 1
        xCell.Border = PdfPCell.NO_BORDER
        xCell.HorizontalAlignment = Element.ALIGN_LEFT
        xTabel.AddCell(xCell)

        xCell = New PdfPCell(New Phrase(New Chunk(" : " & xFungsi.GetData("NIP", "TBL_USER", "NAMAUSER='" & Session("NAMAUSER") & "'"), FontHeader)))
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
            strCommandText = strCommandText + "SELECT dbo.Getnamabarang(KODE_BARANG) as NAMABARANG,KODE_BARANG,JML_MOHON FROM TBL_PERMOHONAN_DETAIL "
            strCommandText = strCommandText + " WHERE IDPERMOHONAN='" & xPK.ToString & "'"

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

                xCellisi = New PdfPCell(New Phrase(New Chunk(strBaca("JML_MOHON").ToString, FontIsi)))
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

        'Dim xMOHON As Integer = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "MOHON").ToString)
    End Sub

    Private Sub grid_DataBound(sender As Object, e As EventArgs) Handles grid.DataBound
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

    Protected Sub gridDetail_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)

    End Sub
End Class
