
Imports System.IO

Partial Class OrderUploadTT
    Inherits System.Web.UI.Page

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs)
        If btnFileUpload.HasFile Then
            Try
                Dim xFileUpload As String = Session("PKORDERTT") & Path.GetExtension(btnFileUpload.FileName)
                ' MsgBox(xFileUpload)
                Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "Files/TTOrder/" & Session("TAHUN")))
                If Not Directory.Exists(directoryPath) Then
                    Directory.CreateDirectory(directoryPath)
                    btnFileUpload.SaveAs(directoryPath & "\" & xFileUpload)

                Else
                    btnFileUpload.SaveAs(directoryPath & "\" & xFileUpload)
                End If
            Catch ex As Exception
                Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/errorPersediaan.log"))
                sw.WriteLine(ex.Message)
                sw.Close()
                ' now rethrow the error
                Throw (ex)

            Finally
                Response.Redirect("OrderDistribusi.aspx")
            End Try


        End If
    End Sub
End Class
