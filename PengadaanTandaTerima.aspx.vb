Imports System.IO
Imports DevExpress.Web

Partial Class PengadaanTandaTerima
    Inherits System.Web.UI.Page

    Private Sub PengadaanTandaTerima_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If (Not IsPostBack) Then
            'Try
            Dim xFungsi As New FungsiData
            xFungsi.delData("TBL_SPK_TT_DETAIL", "WHERE KET=0 AND NAMAPROSES='" & Session("NAMAUSER") & "'")
            lblNoSPK.Text = xFungsi.GetData("NOFAKTUR", "TBL_SPK", " IDSPK='" & Session("IDspkDRPROSESspk") & "'")
            'lblNoSPK.Text = Session("IDspkDRPROSESspk")
            Session("PKSPKTT") = Guid.NewGuid().ToString
            'txtNoFaktur.Text = Session("PKSPKTT")

            Dim sqlQuery As New StringBuilder()
            sqlQuery.Clear()
            sqlQuery.Append("INSERT INTO TBL_SPK_TT_DETAIL(PKSPKTT, KODE_BARANG, JML, NAMAPROSES, IDSPK,KDUNKER) SELECT ")
            sqlQuery.Append("'" & Session("PKSPKTT") & "', ")
            sqlQuery.Append("KODE_BARANG,")
            sqlQuery.Append("(JML-dbo.getJMLSERTERTperKDBRGperSPK(KODE_BARANG,IDSPK)) AS JML,")
            sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
            sqlQuery.Append("'" & Session("IDspkDRPROSESspk") & "', ")
            sqlQuery.Append("'" & Session("KDUNKERATAS") & "'")
            sqlQuery.Append(" FROM TBL_SPK_DETAIL WHERE IDSPK='" & Session("IDspkDRPROSESspk") & "' AND (JML-dbo.getJMLSERTERTperKDBRGperSPK(KODE_BARANG,IDSPK))>0 ")
            xFungsi.insertData(sqlQuery.ToString())

            txtTglFaktur.Value = Date.Now
            'txtPerihal.Text = Session("IDspkDRPROSESspk")

            'Catch ex As Exception



            'End Try
        End If
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs)
        Dim xFungsi As New FungsiData
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            If grid.VisibleRowCount < 0 Then
                grid.Focus()
            ElseIf txtNoFaktur.Text = "" Then
                txtNoFaktur.Focus()
            ElseIf IsDate(txtTglFaktur.Value) = False Then
                txtTglFaktur.Focus()
            ElseIf xFileUpload.HasFile = False Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('File Masih kosong')", True)
            Else
                Dim xFUName As String = Session("PKSPKTT").ToString & Path.GetExtension(xFileUpload.PostedFile.FileName)
                Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "NOTAPENERIMAAN/" & Year(txtTglFaktur.Value)))
                Session("URLFILE") = directoryPath & "/" & xFUName
                sqlSPKTT.Insert()

                xFungsi.UpdateData("TBL_SPK_TT_DETAIL", "KET=1,TGL_PROSES='" & txtTglFaktur.Value & "'", " PKSPKTT='" & Session("PKSPKTT") & "'")
                'Try
                If xFileUpload.HasFile Then

                    If Not Directory.Exists(directoryPath) Then
                        Directory.CreateDirectory(directoryPath)
                        xFileUpload.SaveAs(directoryPath & "\" & xFUName)

                    Else
                        xFileUpload.SaveAs(directoryPath & "\" & xFUName)
                    End If
                End If
                'Catch ex As Exception

                '               End Try
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Tanda Terima sudah diproses, mohon cetak tanda terima di menu Proses Pengadaan')", True)
                Session.Remove("PKSPKTT")

                Response.Redirect("PengadaanPenerimaan.aspx")

            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan anda')", True)
        End If

    End Sub
    Protected Sub grid_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        Dim xPesan As Int16 = e.OldValues("DIPESAN")
        Dim XDiterima As Int16 = e.OldValues("DITERIMA")
        Dim xDistribusi As Int16 = e.NewValues("JML")

        If xDistribusi > (xPesan) Then
            AddError(e.Errors, sender.Columns("JML"), "Jumlah lebih besar dari yang diminta")
            e.RowError = "Jumlah yang diinput lebih besar daripada yang diminta atau sisa barang"
        End If
    End Sub

    Private Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If errors.ContainsKey(column) Then
            Return
        End If
        errors(column) = errorText
    End Sub
    Protected Sub grid_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        If e.DataColumn.FieldName = "JML" Then
            '        e.Cell.BackColor = System.Drawing.Color.GreenYellow
            e.Cell.ToolTip = "Klik 2x disini untuk ubah"
        ElseIf e.DataColumn.FieldName <> "JML" Then
            'e.Cell.BackColor = System.Drawing.Color.Gray
            e.Cell.ForeColor = System.Drawing.Color.Gray

        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("PengadaanPenerimaan.aspx")
    End Sub
End Class
