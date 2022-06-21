
Partial Class LaporanStokBarangPerUnitPerBarang
    Inherits System.Web.UI.Page


    Protected Sub grid_CustomUnboundColumnData(sender As Object, e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        'If e.Column.FieldName = "K_TOTAL" Then
        '    Dim jmlSaldoAwal As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("SALDO_AWAL"))
        '    Dim jmlSetProkons As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("K_SESPROTKONS"))
        '    Dim jmlProtokol As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("K_PROTOKOL"))
        '    Dim jmlKonsuler As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("K_KONSULER"))
        '    Dim jmlFasdip As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("K_FASDIP"))
        '    Dim jmlPwniBHI As Decimal = Convert.ToDecimal(e.GetListSourceFieldValue("K_PWNIBHI"))


        '    e.Value = jmlSetProkons + jmlSetProkons + jmlProtokol + jmlKonsuler + jmlFasdip + jmlPwniBHI

        'End If
    End Sub

    Private Sub LaporanStokBarangPerUnitPerBarang_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then
            Dim xData As New FungsiData
            Dim xNamaSatker As String = xData.GetData("NAMA_SATKER", "TBL_SATKER", "KD_SATKER='" & Session("KDUNKER") & "'")

            cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            'cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboBulan.SelectedValue = CInt(Month(Date.Now()))

            cboUnitKerja.Enabled = False
            cboTahun.SelectedValue = Year(Date.Now())
            Dim xDate As New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, 1)
            Session("TGLAWAL") = xDate.AddDays(-1).ToShortDateString
            Session("TGLAKHIR1") = xDate.ToShortDateString
            Session("TGLAKHIR2") = xDate.AddMonths(1).AddDays(-1).ToShortDateString
            Label1.Text = Session("TGLAKHIR1") & "-TGL 1"
            Label2.Text = Session("TGLAKHIR2") & "-TGL 2"
            Label3.Text = Session("TGLAWAL") & "-TGL AWAL"



        End If
    End Sub

    Protected Sub cboBulan_TextChanged(sender As Object, e As EventArgs)
        Dim xDate As New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, 1)
        ''Dim xDate = New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, DateTime.DaysInMonth(cboBulan.SelectedValue, cboBulan.SelectedValue - 1))
        ''Label1.Text = xDate.AddDays(-1).ToShortDateString
        Session("TGLAWAL") = xDate.AddDays(-1).ToShortDateString
        Session("TGLAKHIR1") = xDate.ToShortDateString
        Session("TGLAKHIR2") = xDate.AddMonths(1).AddDays(-1).ToShortDateString

        Label1.Text = Session("TGLAKHIR1") & "-TGL 1"
        Label2.Text = Session("TGLAKHIR2") & "-TGL 2"
        Label3.Text = Session("TGLAWAL") & "-TGL AWAL"

    End Sub
    Protected Sub cboTahun_TextChanged(sender As Object, e As EventArgs)
        Dim xDate As New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, 1)
        ''Dim xDate = New DateTime(cboTahun.SelectedValue, cboBulan.SelectedValue, DateTime.DaysInMonth(cboBulan.SelectedValue, cboBulan.SelectedValue - 1))
        ''Label1.Text = xDate.AddDays(-1).ToShortDateString
        Session("TGLAWAL") = xDate.AddDays(-1).ToShortDateString
        Session("TGLAKHIR1") = xDate.ToShortDateString
        Session("TGLAKHIR2") = xDate.AddMonths(1).AddDays(-1).ToShortDateString
        Label1.Text = Session("TGLAKHIR1") & "-TGL 1"
        Label2.Text = Session("TGLAKHIR2") & "-TGL 2"
        Label3.Text = Session("TGLAWAL") & "-TGL AWAL"

    End Sub
End Class
