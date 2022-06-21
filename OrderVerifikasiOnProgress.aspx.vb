Imports DevExpress.Web
Imports FungsiData

Partial Class OrderVerifikasiOnProgress
    Inherits System.Web.UI.Page


    Protected Sub ASPxGridView1_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("KDORDER") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub gridDetail_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)
        'If (e.GetValue("PENUHI") + e.GetValue("STOKBELUMDIAMBIL")) > (e.GetValue("STOK1")) Then
        '    e.Row.ForeColor = System.Drawing.Color.Red
        'ElseIf (e.GetValue("PENUHI") + e.GetValue("STOKBELUMDIAMBIL")) <= (e.GetValue("STOK1")) Then
        '    e.Row.ForeColor = System.Drawing.Color.LimeGreen
        If (e.GetValue("STOK2")) > (e.GetValue("STOK1")) Then
            e.Row.ForeColor = System.Drawing.Color.Yellow
        End If
    End Sub

    'Protected Sub grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles grid.HtmlRowPrepared
    '    'If (e.GetValue("STATUS")) = 1 Then
    '    '    e.Row.ForeColor = System.Drawing.Color.LimeGreen
    '    'ElseIf ((e.GetValue("STATUS"))) = 2 Then
    '    '    e.Row.ForeColor = System.Drawing.Color.Red
    '    'End If
    'End Sub
    'Protected Sub btnPenuhi_Click(sender As Object, e As ImageClickEventArgs)
    '    Dim vpnButton As ImageButton = CType(sender, ImageButton)
    '    Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
    '    Dim xMOHON As Integer = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "MOHON").ToString)

    '    Dim xFungsi As New FungsiData
    '    xFungsi.UpdateData("TBL_ORDER", "STATUS=3,TGL_PROSES2=GETDATE(),NAMAPROSES2='" & Session("NAMAUSER") & "'", "KDORDER='" & xPK & "'")
    '    'xFungsi.UpdateData("TBL_ORDER_DETAIL", "PENUHI=" & xMOHON & "", "IDETAILORDER='" & xPK & "'")
    '    grid.DataBind()
    '    'sqlOrderDetail.UpdateParameters("PENUHI").DefaultValue = xMOHON
    '    'sqlOrderDetail.Update()

    '    Label1.Text = xMOHON.ToString
    'End Sub
    'Protected Sub gridDetail_CellEditorInitialize(sender As Object, e As ASPxGridViewEditorEventArgs)
    '    Label1.Text = Session("KDORDER")
    'End Sub
    Protected Sub grid_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles grid.HtmlRowPrepared
        'If (e.GetValue("CEKORDER")) = 0 Then
        '    e.Row.ForeColor = System.Drawing.Color.LimeGreen
        'ElseIf ((e.GetValue("CEKORDER"))) = 1 Then
        '    e.Row.ForeColor = System.Drawing.Color.Red
        'End If
    End Sub
    'Protected Sub btnProses_Click(sender As Object, e As ImageClickEventArgs)
    '    Dim vpnButton As ImageButton = CType(sender, ImageButton)
    '    Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
    '    Dim xFungsi As New FungsiData
    '    Dim confirmValue As String = Request.Form("confirm_value")
    '    If confirmValue = "Yes" Then
    '        If xFungsi.GetData("STATUS", "TBL_ORDER", "KDORDER='" & xPK & "'") = 1 Then
    '            'xFungsi.UpdateData("TBL_ORDER", "STATUS=1", "KDORDER='" & xPK & "'")
    '            xFungsi.UpdateData("TBL_ORDER", "TGL_PROSES2=GETDATE(),NAMAPROSES2='" & Session("NAMAUSER") & "',STATUS=3", "KDORDER='" & xPK & "'")
    '            'xFungsi.UpdateData("TBL_ORDER", "", "KDORDER='" & xPK & "'")
    '            grid.DataBind()
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order/Permohonan Barang sudah diproses')", True)
    '        End If
    '    Else
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan')", True)
    '    End If


    'End Sub
    Protected Sub btnBatal_Click(sender As Object, e As ImageClickEventArgs)
        Dim vpnButton As ImageButton = CType(sender, ImageButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFungsi As New FungsiData
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            If xFungsi.GetData("STATUS", "TBL_ORDER", "KDORDER='" & xPK & "'") = 0 Then
                xFungsi.UpdateData("TBL_ORDER", "STATUS=2,TGL_ACC=GETDATE(),ACCUSER='" & Session("NAMAUSER") & "'", "KDORDER='" & xPK & "'")
                xFungsi.UpdateData("TBL_ORDER", "TGL_ACC=GETDATE()", "KDORDER='" & xPK & "'")
                'xFungsi.UpdateData("TBL_ORDER", "TGL_ACC=GETDATE()", "KDORDER='" & xPK & "'")
                'xFungsi.UpdateData("TBL_ORDER", "ACCUSER='" & Session("NAMAUSER") & "'", "KDORDER='" & xPK & "'")
                grid.DataBind()
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order/Permohonan Barang sudah diproses')", True)
            End If
        End If


    End Sub
    Protected Sub btnPO_Click(sender As Object, e As ImageClickEventArgs)
        Dim vpnButton As ImageButton = CType(sender, ImageButton)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)

        Dim xMOHON As Integer = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "MOHON").ToString)
        Dim xFungsi As New FungsiData
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            xFungsi.UpdateData("TBL_ORDER_DETAIL", "KET=2,TGL_ACC=GETDATE(),ACCUSER='" & Session("NAMAUSER") & "'", "KDORDER='" & xPK & "'")
        End If

    End Sub

    Protected Sub btnProses_Click(sender As Object, e As EventArgs)
        Dim vpnButton As Button = CType(sender, Button)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xCEKORDER As Integer = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, "CEKORDER").ToString)
        Dim xFungsi As New FungsiData

        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            If xCEKORDER = 1 Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Jumlah yang dipenuhi melebihi stok')", True)
            Else
                Try
                    xFungsi.UpdateData("TBL_ORDER", "TGL_PROSES2=GETDATE(),NAMAPROSES2='" & Session("NAMAUSER") & "',STATUS=3", "KDORDER='" & xPK & "'")
                    'xFungsi.UpdateData("TBL_ORDER", "", "KDORDER='" & xPK & "'")
                    grid.DataBind()
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order/Permohonan Barang sudah diproses')", True)
                Catch ex As Exception

                Finally
                    grid.DataBind()
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order/Permohonan Barang sudah diproses')", True)

                End Try

            End If
            'If xFungsi.GetData("STATUS", "TBL_ORDER", "KDORDER='" & xPK & "'") = 1 Then
            '    'xFungsi.UpdateData("TBL_ORDER", "STATUS=1", "KDORDER='" & xPK & "'")
            '    xFungsi.UpdateData("TBL_ORDER", "TGL_PROSES2=GETDATE(),NAMAPROSES2='" & Session("NAMAUSER") & "',STATUS=3", "KDORDER='" & xPK & "'")
            '    'xFungsi.UpdateData("TBL_ORDER", "", "KDORDER='" & xPK & "'")
            '    grid.DataBind()
            '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order/Permohonan Barang sudah diproses')", True)
            'End If


        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan')", True)
        End If
    End Sub

    Protected Sub gridDetail_CommandButtonInitialize(sender As Object, e As ASPxGridViewCommandButtonEventArgs)
        Dim xGRID As ASPxGridView = CType(sender, ASPxGridView)
        'Dim xPK As Integer = xGRID.GetRowValues(e.VisibleIndex, "IDETAILORDER")
        Dim xPKMASTER As String = xGRID.GetMasterRowKeyValue
        Dim xFungsi As New FungsiData
        Dim xSTATUS As Integer = xFungsi.GetData("STATUS", "TBL_ORDER", " KDORDER='" & xPKMASTER & "'")
        If xSTATUS > 2 Then
            If e.ButtonType = ColumnCommandButtonType.Edit Then
                e.Visible = False
            End If
        End If
    End Sub
    Protected Sub gridDetail_DataBound(sender As Object, e As EventArgs)
        CType(sender, ASPxGridView).DetailRows.ExpandAllRows()
    End Sub

    Private Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If errors.ContainsKey(column) Then
            Return
        End If
        errors(column) = errorText
    End Sub
    Protected Sub gridDetail_RowValidating(sender As Object, e As Data.ASPxDataValidationEventArgs)
        Dim xMinta As Int16 = e.NewValues("MOHON")
        Dim xPenuhi As Int16 = e.NewValues("PENUHI")
        Dim xStok As Int16 = e.NewValues("STOK1")
        'Dim xStokReserved As Int16 = e.NewValues("STOKRESERVED")

        ' Dim xKunci As String = sender.Columns("STOK")
        'Label1.Text = xStok
        If xPenuhi > xMinta Then
            AddError(e.Errors, sender.Columns("JML_PENUHI"), "Permohonan lebih besar daripada permintaan")
            e.RowError = "Jumlah yang dipenuhi lebih besar daripada permintaan"
        End If
    End Sub
    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        Dim vpnButton As CheckBox = CType(sender, CheckBox)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFungsi As New FungsiData
        xFungsi.UpdateData("TBL_ORDER_DETAIL", "CEK1=1,CEK2=0,CEK3=0,IDFLAG='01',PENUHI=MOHON", "IDETAILORDER='" & xPK & "'")
        grid.DataBind()
    End Sub
    Protected Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)
        Dim vpnButton As CheckBox = CType(sender, CheckBox)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFungsi As New FungsiData
        xFungsi.UpdateData("TBL_ORDER_DETAIL", "CEK1=0,CEK2=1,CEK3=0,IDFLAG='02',PENUHI=MOHON", "IDETAILORDER='" & xPK & "'")
        grid.DataBind()
    End Sub
    Protected Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
        Dim vpnButton As CheckBox = CType(sender, CheckBox)
        Dim Container As GridViewDataItemTemplateContainer = DirectCast(vpnButton.NamingContainer, GridViewDataItemTemplateContainer)
        Dim xPK As String = CStr(Container.Grid.GetRowValues(Container.VisibleIndex, Container.Grid.KeyFieldName).ToString)
        Dim xFungsi As New FungsiData
        xFungsi.UpdateData("TBL_ORDER_DETAIL", "CEK1=0,CEK2=0,CEK3=1,IDFLAG='03',PENUHI=0", "IDETAILORDER='" & xPK & "'")
        grid.DataBind()
    End Sub

    Private Sub OrderVerifikasiOnProgress_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False
            cboTahun.SelectedValue = Year(Date.Now)

        End If
    End Sub
End Class
