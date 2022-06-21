Imports DevExpress.Web
Imports iTextSharp.text
Imports iTextSharp
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data

Partial Class Pengadaan
    Inherits System.Web.UI.Page

    Private Sub Pengadaan_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then

            cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False
            cboTahun.SelectedValue = Year(Date.Now)
            wAllBeli.Text = GetJmlPengadaanTotals.ToString()
            wBelumDiterima.Text = GetJmlPengadaanPerStatus("0").ToString()
            wSebagian.Text = GetJmlPengadaanPerStatus("1").ToString()
            wSelesai.Text = GetJmlPengadaanPerStatus("2").ToString()

        End If

    End Sub

    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDSPK") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Private Sub SPKHistory_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("NAMAUSER") = "" Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False
            cboTahun.SelectedValue = Year(Date.Now)
        End If
    End Sub

    Protected Sub btnCetak_Click(sender As Object, e As ImageClickEventArgs)
        Dim xTombol As ImageButton = CType(sender, ImageButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFileName As String = xPK & ".pdf"
        Dim xTAHUN As String = Year(CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "TGL_FAKTUR").ToString))
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/SPK/" & xTAHUN))


        If File.Exists(directoryPath & xFileName) Then
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        End If
    End Sub
    Protected Sub btnSuratPengantar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vpnButton As ImageButton = CType(sender, ImageButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        Dim namafile As String = ""
        namafile = Date.Now.ToString("ddMMyyHHmmss").ToString & ".PDF"
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "inline; filename=SuratPengantar.pdf")

        Dim doc As New Document(PageSize.A4)
        Dim wri As PdfWriter = PdfWriter.GetInstance(doc, Response.OutputStream)

        doc.Open()
        Dim cb As PdfContentByte = wri.DirectContent
        Dim FontHeader As Font = FontFactory.GetFont("ARIAL", 10, Font.BOLD)
        Dim FontIsi As Font = FontFactory.GetFont("ARIAL", 9)

        'Dim customfont As BaseFont = BaseFont.CreateFont(fontpath + "calibri.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
        Dim para, para2, para3, para4 As New Paragraph("")
        Dim ch1, ch2, ch3, ch4, ch5, ch6, ch7, ch8, ch9, ch10, ch11 As New Phrase("")

        Dim base As BaseFont = BaseFont.CreateFont("C:\Windows\Fonts\Cour.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)
        Dim courier As Font = New Font(base, 10, Font.NORMAL, BaseColor.BLACK)
        Dim courierGaris As Font = New Font(base, 10, Font.UNDERLINE, BaseColor.BLACK)
        Dim pharse As New Phrase(" ")
        Dim xFungsiData As New FungsiData

        'Label1.Text = xPK
        Dim myConnection As New SqlConnection()
        Try

            Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
            myConnection.ConnectionString = strConnectionString
            ' buat commandf
            Dim strCommandText As String = ""
            strCommandText = strCommandText + "SELECT A.KODE_BARANG, B.NAMA_BARANG, JML,HARGA,TOTAL FROM "
            strCommandText = strCommandText + " [TBL_SPK_DETAIL] A INNER JOIN TBL_BARANG B "
            strCommandText = strCommandText + " ON A.KODE_BARANG=B.KODE_BARANG  "
            strCommandText = strCommandText + " WHERE IDSPK='" & xPK.ToString & "'"

            Dim myCommand As New SqlCommand(strCommandText, myConnection)

            Dim PTableHeader As New PdfPTable(7)
            PTableHeader.WidthPercentage = 100
            Dim headerwidths() As Integer = {1, 4, 6, 3, 6, 2, 2}
            PTableHeader.SetWidths(headerwidths)
            Dim PCell As PdfPCell = Nothing
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            ' buka koneksi
            myConnection.Open()
            ' tampil data]
            Dim i As Integer
            i = 0

            PCell = New PdfPCell(New Phrase(New Chunk("LAPORAN PENERIMAAN BARANG", FontHeader)))
            PCell.Colspan = 6
            PCell.Border = PdfPCell.NO_BORDER
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            Dim xKODEUNKER As String = ""
            xKODEUNKER = xFungsiData.GetData("KDUNKER", "TBL_SPK", " WHERE IDSPK='" & xPK & "'")
            Dim xNAMAUNKER As String = ""
            xNAMAUNKER = xFungsiData.GetData("NAMA_UNKER", "TBL_UNKER", " WHERE KD_UNKER='" & xKODEUNKER & "'")
            PCell = New PdfPCell(New Phrase(New Chunk(xNAMAUNKER, FontHeader)))
            PCell.Colspan = 6
            PCell.Border = PdfPCell.NO_BORDER
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk("NO FAKTUR : " & xFungsiData.GetData("NOFAKTUR", "TBL_SPK", " WHERE IDSPK='" & xPK & "'").ToString, FontHeader)))
            PCell.Colspan = 6
            PCell.Border = PdfPCell.NO_BORDER
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk(" ", FontHeader)))
            PCell.Colspan = 6
            PCell.Border = PdfPCell.NO_BORDER
            PCell.FixedHeight = 30.0F
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk("NO.", FontHeader)))
            'PCell.Border = PdfPCell.NO_BORDER
            PCell.FixedHeight = 30.0F
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk("KODE BARANG", FontHeader)))
            'PCell.Border = PdfPCell.NO_BORDER
            PCell.FixedHeight = 30.0F
            PCell.VerticalAlignment = Element.ALIGN_MIDDLE
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk("NAMA BARANG", FontHeader)))
            'PCell.Border = PdfPCell.NO_BORDER
            PCell.FixedHeight = 30.0F
            PCell.VerticalAlignment = Element.ALIGN_MIDDLE
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk("JML", FontHeader)))
            'PCell.Border = PdfPCell.NO_BORDER
            PCell.FixedHeight = 30.0F
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PCell.VerticalAlignment = Element.ALIGN_MIDDLE
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk("HARGA", FontHeader)))
            'PCell.Border = PdfPCell.NO_BORDER
            PCell.FixedHeight = 30.0F
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PCell.VerticalAlignment = Element.ALIGN_MIDDLE
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            PCell = New PdfPCell(New Phrase(New Chunk("TOTAL", FontHeader)))
            'PCell.Border = PdfPCell.NO_BORDER
            PCell.FixedHeight = 30.0F
            PCell.HorizontalAlignment = Element.ALIGN_CENTER
            PCell.VerticalAlignment = Element.ALIGN_MIDDLE
            PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
            PTableHeader.AddCell(PCell)

            Dim strBaca As SqlDataReader = myCommand.ExecuteReader()
            While strBaca.Read()
                i = i + 1

                PCell = New PdfPCell(New Phrase(New Chunk(strBaca("NO").ToString, FontIsi)))
                'PCell.Border = PdfPCell.NO_BORDER
                PCell.HorizontalAlignment = Element.ALIGN_LEFT
                PCell.VerticalAlignment = Element.ALIGN_MIDDLE
                PTableHeader.HorizontalAlignment = Element.ALIGN_LEFT
                PTableHeader.AddCell(PCell)

                PCell = New PdfPCell(New Phrase(New Chunk(strBaca("KODE_BARANG").ToString(), FontIsi)))
                'PCell.Border = PdfPCell.NO_BORDER
                PCell.HorizontalAlignment = Element.ALIGN_CENTER
                PCell.VerticalAlignment = Element.ALIGN_MIDDLE
                PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
                PTableHeader.AddCell(PCell)

                PCell = New PdfPCell(New Phrase(New Chunk(strBaca("NAMA_BARANG").ToString(), FontIsi)))
                'PCell.Border = PdfPCell.NO_BORDER
                PCell.HorizontalAlignment = Element.ALIGN_CENTER
                PCell.VerticalAlignment = Element.ALIGN_MIDDLE
                PTableHeader.HorizontalAlignment = Element.ALIGN_CENTER
                PTableHeader.AddCell(PCell)

                PCell = New PdfPCell(New Phrase(New Chunk(strBaca("JML").ToString, FontIsi)))
                'PCell.Border = PdfPCell.NO_BORDER
                PCell.HorizontalAlignment = Element.ALIGN_LEFT
                PCell.VerticalAlignment = Element.ALIGN_MIDDLE
                PTableHeader.HorizontalAlignment = Element.ALIGN_LEFT
                PTableHeader.AddCell(PCell)

                PCell = New PdfPCell(New Phrase(New Chunk(strBaca("HARGA").ToString, FontIsi)))
                'PCell.Border = PdfPCell.NO_BORDER
                PCell.HorizontalAlignment = Element.ALIGN_LEFT
                PCell.VerticalAlignment = Element.ALIGN_MIDDLE
                PTableHeader.HorizontalAlignment = Element.ALIGN_LEFT
                PTableHeader.AddCell(PCell)

                PCell = New PdfPCell(New Phrase(New Chunk(strBaca("TOTAL").ToString, FontIsi)))
                'PCell.Border = PdfPCell.NO_BORDER
                PCell.HorizontalAlignment = Element.ALIGN_LEFT
                PCell.VerticalAlignment = Element.ALIGN_MIDDLE
                PTableHeader.HorizontalAlignment = Element.ALIGN_LEFT
                PTableHeader.AddCell(PCell)
            End While

            PTableHeader.CompleteRow()
            doc.Add(PTableHeader)
            doc.Add(pharse)

        Catch ex As Exception

        End Try

        doc.Close()

        Response.Write(doc)
        Response.End()
    End Sub

    Protected Sub btnUnduh_Click(sender As Object, e As EventArgs)
        Dim xTombol As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFileName As String = xPK & ".pdf"
        Dim xTAHUN As String = Year(CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "TGL_FAKTUR").ToString))
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/SPK/" & xTAHUN))


        If File.Exists(directoryPath & xFileName) Then
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        End If
    End Sub

    '------- KUMPULAN SKRIPT LAMA
    'Protected Sub btnEdit_Click(sender As Object, e As ImageClickEventArgs)
    '    Dim xTombol As ImageButton = CType(sender, ImageButton)
    '    Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

    '    Dim xFungsi As New FungsiData
    '    'xFungsiData.GetData("KDUNKER", "TBL_SPK", " WHERE IDSPK='" & xPK & "'")
    '    Dim xStatus As String = xFungsi.GetData("STATUS", "TBL_SPK", " IDSPK='" & xPK & "'")
    '    If xStatus = "1" Then
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang SPK sudah sebagian diterima')", True)
    '    Else
    '        Session("UbahSPK") = True
    '        Session("IDSPKUBAH") = xPK
    '        Response.Redirect("SPK.aspx")
    '    End If
    'End Sub

    Protected Sub gridDetail_InitNewRow(sender As Object, e As Data.ASPxDataInitNewRowEventArgs)
        e.NewValues("JML") = 1
        e.NewValues("HARGA") = 1000
    End Sub
    Protected Sub grid_DataBound(sender As Object, e As EventArgs)
        CType(sender, ASPxGridView).DetailRows.ExpandAllRows()
    End Sub
    Protected Sub grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)
        'If e.RowType = GridViewRowType.Data AndAlso e.VisibleIndex Mod 2 = 0 Then
        '    e.Row.BackColor = System.Drawing.Color.CornflowerBlue
        'End If
        'If e.RowType = GridViewRowType.Detail AndAlso e.VisibleIndex Mod 2 = 0 Then
        '    e.Row.BackColor = System.Drawing.Color.PaleTurquoise
        'End If
        'If e.RowType = GridViewRowType.Preview AndAlso e.VisibleIndex Mod 2 = 0 Then
        '    e.Row.BackColor = System.Drawing.Color.PaleTurquoise
        '    e.Row.ForeColor = System.Drawing.Color.Black
        '    e.Row.Font.Bold = True
        'End If
        Dim xGrid = TryCast(sender, ASPxGridView)
        If e.RowType = GridViewRowType.Data Then
            If String.IsNullOrEmpty(e.GetValue("ISDELETED")) Or e.GetValue("ISDELETED") = 1 Then
                e.Row.Style.Add("text-decoration", "line-through")
                e.Row.BackColor = System.Drawing.Color.Plum

            End If
        End If
    End Sub
    Protected Sub gridDetail_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        ''Dim xPK As Integer = xGRID.GetRowValues(e.VisibleIndex, "IDETAILORDER")
        Dim xPKMASTER As String = xGRID.GetMasterRowKeyValue
        Dim xFungsi As New FungsiData
        Dim xSTATUS As Integer = xFungsi.GetData("STATUS", "TBL_SPK", " IDSPK='" & xPKMASTER & "'")
        Dim xDELETED As Integer = xFungsi.GetData("ISDELETED", "TBL_SPK", " IDSPK='" & xPKMASTER & "'")
        If xSTATUS > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Edit Then
                e.Visible = False
            ElseIf e.ButtonType = ColumnCommandButtonType.Delete Then
                e.Visible = False
            ElseIf e.ButtonType = ColumnCommandButtonType.[New] Then
                e.Visible = False
            End If
        End If
        If xDELETED > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Delete Then
                e.Visible = False
            End If
        End If
    End Sub
    Protected Sub grid_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        Dim xPKMASTER As String = xGRID.KeyFieldName
        Dim xHAPUS As Integer = xGRID.GetRowValues(e.VisibleIndex, "ISDELETED")
        Dim xSTATUS As Integer = xGRID.GetRowValues(e.VisibleIndex, "Progress")
        If xSTATUS > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Edit Then
                e.Visible = False
            ElseIf e.ButtonType = ColumnCommandButtonType.Delete Then
                e.Visible = False
            ElseIf e.ButtonType = ColumnCommandButtonType.[New] Then
                e.Visible = False
            End If
        End If

        'If xHAPUS > 0 Then
        '    If e.ButtonType = ColumnCommandButtonType.Delete Then
        '        e.Visible = False
        '    End If
        'End If
    End Sub

    Private Function GetJmlPengadaanPerStatus(ByVal xStatus As String) As Integer
        Dim count As Integer = 0

        Using thisConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("conPersediaan").ToString())

            Using cmdCount As SqlCommand = New SqlCommand("SELECT COUNT(*) FROM TBL_SPK WHERE KDUNKER=@KDUNKER and dbo.GetStatusSPK(IDSPK)=@STATUS", thisConnection)
                cmdCount.CommandType = CommandType.Text
                cmdCount.Parameters.Clear()
                cmdCount.Parameters.AddWithValue("@KDUNKER", Session("KDUNKERATAS"))
                cmdCount.Parameters.AddWithValue("@STATUS", xStatus)
                thisConnection.Open()
                count = CInt(cmdCount.ExecuteScalar())
                thisConnection.Close()
            End Using
        End Using

        Return count
    End Function

    Private Function GetJmlPengadaanTotals() As Integer
        Dim count As Integer = 0

        Using thisConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("conPersediaan").ToString())

            Using cmdCount As SqlCommand = New SqlCommand("SELECT COUNT(*) FROM TBL_SPK WHERE KDUNKER=@KDUNKER ", thisConnection)
                cmdCount.CommandType = CommandType.Text
                cmdCount.Parameters.Clear()
                cmdCount.Parameters.AddWithValue("@KDUNKER", Session("KDUNKERATAS"))

                thisConnection.Open()
                count = CInt(cmdCount.ExecuteScalar())
                thisConnection.Close()
            End Using
        End Using

        Return count
    End Function

    Protected Sub gridDetail_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)

    End Sub
End Class
