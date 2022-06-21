
Partial Class RefMitra
    Inherits System.Web.UI.Page

    Private Sub RefMitra_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
    End Sub
End Class
