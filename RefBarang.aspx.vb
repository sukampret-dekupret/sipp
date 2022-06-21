
Partial Class RefBarang
    Inherits System.Web.UI.Page

    Private Sub RefBarang_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
    End Sub
    Protected Sub gridBarang_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
        e.NewValues("MIN") = 10
    End Sub
End Class
