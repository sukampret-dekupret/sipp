Imports iTextSharp.text
Imports iTextSharp
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO


Partial Class LaporanStokBarang
    Inherits System.Web.UI.Page

    Private Sub LaporanStokBarang_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then

            Dim xData As New FungsiData
            Dim xNamaSatker As String = xData.GetData("NAMA_SATKER", "TBL_SATKER", "KD_SATKER='" & Session("KDUNKER") & "'")

            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            'cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False

            'xTGLAWAL.Value = Date.Now
            'xTGLAKHIR.Value = Date.Now
            txtTanggalSaldo.Value = Date.Now
            cboSaldoAwal.SelectedValue = 0
        End If
    End Sub
End Class
