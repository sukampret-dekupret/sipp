Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Imports OfficeOpenXml

Partial Class PenerimaanSaldo
    Inherits System.Web.UI.Page

    Private Sub PenerimaanSaldo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False

        End If


    End Sub

    Protected Sub gridDetail_BeforePerformDataSelect(sender As Object, e As EventArgs)
        Session("IDPENERIMAAN") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

End Class
