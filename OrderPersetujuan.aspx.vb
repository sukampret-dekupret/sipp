Imports DevExpress.Web
Imports System.IO
Imports FungsiData
Imports iTextSharp.text
Imports iTextSharp
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient

Partial Class OrderPersetujuan
    Inherits System.Web.UI.Page

    Private Sub OrderPersetujuan_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False
            cboTahun.SelectedValue = Year(Date.Now)
        End If
    End Sub
    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("KDORDER") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub
    'Protected Sub btnVerifikasi_Click(sender As Object, e As EventArgs)
    '    Dim xTombol As Button = CType(sender, Button)
    '    Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
    '    Dim xFungsi As New FungsiData
    '    Dim confirmValue As String = Request.Form("confirm_value")
    '    If confirmValue = "Yes" Then
    '        xFungsi.UpdateData("TBL_ORDER_DETAIL", "KET=2,TGLPROSES=GETDATE(),NAMAPROSES='" & Session("NAMAUSER") & "'", " IDETAILORDER='" & xPK & "'")
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang sudah dikonfirmasi')", True)
    '        grid.DataBind()
    '    Else
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang belum dikonfirmasi')", True)
    '    End If
    'End Sub
    Protected Sub gridDetail_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
        'Dim xID As String = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        'Dim xFungsiData As New FungsiData
        'Dim xSTS As Integer = xFungsiData.GetData("KET", "TBL_ORDER_DETAIL", "IDETAILORDER='" & xID & "'")

        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        Dim xPKMASTER As String = xGRID.GetMasterRowKeyValue
        Dim xFungsi As New FungsiData
        Dim xSTATUS As Integer = xFungsi.GetData("STATUS", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        'Dim xSTSMOHON As Integer = xGRID.GetRowValues(e.VisibleIndex, "KET")
        If xSTATUS > 0 Then
            If e.ButtonType = ColumnCommandButtonType.Edit Then
                e.Visible = False
            End If
        End If
    End Sub
    Protected Sub btnKirim_Click(sender As Object, e As EventArgs)
        Dim xTombol As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xSTATUS As Integer = (Container.Grid.GetRowValues(Container.VisibleIndex, "KET"))
        If xSTATUS > 0 Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order sudah diproses ke Eselon I tidak bisa diubah')", True)
        Else
            Dim xFungsi As New FungsiData
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then
                xFungsi.UpdateData("TBL_ORDER", "STATUS=1,TGL_PROSES=GETDATE(),NAMAPROSES='" & Session("NAMAUSER") & "'", " KDORDER='" & xPK & "'")
                xFungsi.UpdateData("TBL_ORDER_DETAIL", "TGLPROSES=GETDATE(),NAMAPROSES='" & Session("NAMAUSER") & "'", " KDORDER='" & xPK & "'")
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan Barang sudah dikirim ke eselon I')", True)
                grid.DataBind()
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan Barang belum dikirim ke eselon I')", True)
            End If
        End If

    End Sub
    Protected Sub btnLiatNota_Click(sender As Object, e As EventArgs)
        Dim xTombol As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFileName As String = " " & xPK & ".pdf"
        Dim xTAHUN As String = Year(CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "TGL_NOTA").ToString))
        Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "ORDER/" & xTAHUN))
        'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert(directoryPath & xFileName)", True)
        ''MsgBox(xFileName)

        If File.Exists(directoryPath & xFileName) Then
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & xFileName & "")
            Response.TransmitFile(directoryPath & xFileName)
            Response.End()
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('File yang dicari tidak ada')", True)
        End If
    End Sub
    Protected Sub btnCekData_Click(sender As Object, e As EventArgs)
        Dim xTombol As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Session("KDBARANGFROMORDERPROSES") = xPK
        ''Response.Redirect("HistoryBarangOrder.aspx")
    End Sub

    Protected Sub btnTolak_Click(sender As Object, e As EventArgs)
        Dim xTombol As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(xTombol.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
    End Sub
End Class
