Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.Web
Partial Class PermohonanTandaTerima
    Inherits System.Web.UI.Page

    Private Sub PermohonanTandaTerima_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If (Not IsPostBack) Then
            Dim xFungsi As New FungsiData
            lblNoPermohonan.Text = xFungsi.GetData("NO_NOTA", "TBL_ORDER", " KDORDER='" & Session("IDTTPERMOHONAN") & "'")
            Session("PKTTMOHON") = Guid.NewGuid().ToString
            xFungsi.delData("TBL_PERMOHONAN_TT_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
            txtTanggalSerah.Value = Date.Now

            'Label1.Text = sqlQuery.ToString

            'Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
            'Dim conn As New SqlConnection()
            'Dim sqlComm As New SqlCommand()
            'conn.ConnectionString = strConnString
            'sqlComm.Connection = conn
            'sqlComm.CommandType = System.Data.CommandType.StoredProcedure
            'sqlComm.CommandText = "MasukinDataByPermohonan"

            'sqlComm.Parameters.AddWithValue("PKTTMOHON", Session("PKTTMOHON"))
            'sqlComm.Parameters.AddWithValue("IDPERMOHONAN", Session("IDTTPERMOHONAN"))
            'sqlComm.Parameters.AddWithValue("NAMAUSER", Session("NAMAUSER"))
            'sqlComm.Parameters.AddWithValue("KDUNKER", Session("KDUNKER"))
            'conn.Open()
            ''Dim i As Integer = sqlComm.ExecuteNonQuery()
            'conn.Close()
            Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
            Dim conn As New SqlConnection()
            conn.ConnectionString = strConnString
            Try

                Dim sqlQuery As New StringBuilder()
                sqlQuery.Clear()
                sqlQuery.Append("SELECT ")
                sqlQuery.Append("KODE_BARANG,")
                sqlQuery.Append("(JML_PENUHI-dbo.getJMLMOHONperTT(KODE_BARANG,IDPERMOHONAN)) AS JML")
                sqlQuery.Append(" FROM TBL_PERMOHONAN_DETAIL WHERE IDPERMOHONAN='" & Session("IDTTPERMOHONAN") & "'  ")


                conn.Open()
                Dim cmd As New SqlCommand(sqlQuery.ToString, conn)
                Dim strBaca As SqlDataReader = cmd.ExecuteReader()
                'Label1.Text = sqlQuery.ToString

                While strBaca.Read()

                    'Dim xKODEBARANG As String = strBaca("KODE_BARANG").ToString()
                    'Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/errorPersediaan.log"))
                    'sw.WriteLine(xKODEBARANG)
                    'sw.Close()
                    sqlDetail.InsertParameters("KODE_BARANG").DefaultValue = strBaca("KODE_BARANG").ToString()
                    sqlDetail.InsertParameters("JML").DefaultValue = strBaca("JML").ToString()
                    sqlDetail.Insert()
                End While

                'Dim sqlQuery As New StringBuilder()
                'sqlQuery.Clear()
                'sqlQuery.Append("INSERT INTO TBL_PERMOHONAN_TT_DETAIL(PKTTMOHON, KODE_BARANG, JML, NAMAUSER, IDPERMOHONAN,KDUNKER) SELECT ")
                'sqlQuery.Append("'" & Session("PKTTMOHON") & "', ")
                'sqlQuery.Append("KODE_BARANG,")
                'sqlQuery.Append("(JML_PENUHI-dbo.getJMLMOHONperTT(KODE_BARANG,IDPERMOHONAN)) AS JML,")
                'sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
                'sqlQuery.Append("'" & Session("IDTTPERMOHONAN") & "', ")
                'sqlQuery.Append("'" & Session("KDUNKER") & "'")
                'sqlQuery.Append(" FROM TBL_PERMOHONAN_DETAIL WHERE IDPERMOHONAN='" & Session("IDTTPERMOHONAN") & "'  ")
                'xFungsi.insertData(sqlQuery.ToString())

            Catch ex As Exception
                ' write the error to file
                Dim sw As StreamWriter = File.AppendText(Server.MapPath("~/errorPersediaan.log"))
                sw.WriteLine(ex.Message)
                sw.Close()
                ' now rethrow the error
                Throw (ex)
            Finally

                conn.Close()

            End Try


        End If


    End Sub

    Protected Sub gridTandaTerimaDetail_RowValidating(sender As Object, e As DevExpress.Web.Data.ASPxDataValidationEventArgs)
        Dim xMinta As Int16 = e.NewValues("JML_PENUHI")
        Dim xDiambil As Int16 = e.NewValues("SUDAH")
        Dim xDistribusi As Int16 = e.NewValues("JML")
        ' Dim xKunci As String = sender.Columns("STOK")
        'Label1.Text = xStok
        If xDistribusi > (xMinta - xDiambil) Then
            AddError(e.Errors, sender.Columns("JML"), "Jumlah lebih besar dari yang diminta")
            e.RowError = "Jumlah yang diinput lebih besar daripada sisa barang"
            'ElseIf xPenuhi > (xStok + xStokReserved) Then
            'ElseIf (xPenuhi + xStokReserved) > xStok Then
            '    AddError(e.Errors, sender.Columns("JML_PENUHI"), "Permohonan lebih besar daripada stok ")
            '    e.RowError = "Mohon kurangi jumlah barang sesuai dengan stok"
        End If
    End Sub

    Private Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If errors.ContainsKey(column) Then
            Return
        End If
        errors(column) = errorText
    End Sub
    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs)
        Dim xFungsi As New FungsiData

        If xFungsi.checkData("SELECT PKTTMOHONDTL FROM TBL_PERMOHONAN_TT_DETAIL WHERE PKTTMOHON='" & Session("PKTTMOHON") & "' AND JML>(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & Session("KDUNKER") & "',getdate(),0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'" & Session("KDUNKER") & "',GETDATE())) ") = True Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Stok tidak tersedia, mohon sesuaikan jml barang atau adakan pengadaan terlebih dahulu')", True)
        Else
            If gridTandaTerimaDetail.VisibleRowCount > 0 Then
                Dim confirmValue As String = Request.Form("confirm_value")
                If confirmValue = "Yes" Then
                    Dim sqlQuery As New StringBuilder()
                    sqlQuery.Clear()
                    sqlQuery.Append("INSERT INTO TBL_PERMOHONAN_TT(PKTTMOHON,IDPERMOHONAN, NO_TT, USERCETAK, KDUNKER) VALUES( ")
                    sqlQuery.Append("'" & Session("PKTTMOHON") & "', ")
                    sqlQuery.Append("'" & Session("IDTTPERMOHONAN") & "', ")
                    sqlQuery.Append(" dbo.NextNoTTMohon(YEAR(GETDATE())),")
                    sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
                    sqlQuery.Append("'" & Session("KDUNKER") & "')")
                    'Label1.Text = sqlQuery.ToString
                    xFungsi.insertData(sqlQuery.ToString)

                    ''xFungsi.UpdateData("TBL_PERMOHONAN_TT_DETAIL", "KET=1,TGL_PROSES=GETDATE()", " PKTTMOHON='" & Session("PKTTMOHON") & "'")
                    xFungsi.UpdateData("TBL_PERMOHONAN_TT_DETAIL", "KET=1,TGL_PROSES='" & txtTanggalSerah.Value & "'", "PKTTMOHON='" & Session("PKTTMOHON") & "'")
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang sudah diproses')", True)
                    Session.Remove("PKTTMOHON")

                    Response.Redirect("PermohonanDistribusi.aspx")
                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Data Barang Kosong')", True)
                End If
            End If
        End If
    End Sub
    Protected Sub gridTandaTerimaDetail_HtmlRowPrepared(sender As Object, e As ASPxGridViewTableRowEventArgs)
        If e.RowType = GridViewRowType.Data AndAlso e.VisibleIndex Mod 2 = 0 Then
            e.Row.BackColor = System.Drawing.Color.CornflowerBlue
        End If
        If e.RowType = GridViewRowType.Detail AndAlso e.VisibleIndex Mod 2 = 0 Then
            e.Row.BackColor = System.Drawing.Color.PaleTurquoise
        End If
        If e.RowType = GridViewRowType.Preview AndAlso e.VisibleIndex Mod 2 = 0 Then
            e.Row.BackColor = System.Drawing.Color.PaleTurquoise
            e.Row.ForeColor = System.Drawing.Color.Black
            e.Row.Font.Bold = True
        End If
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("PermohonanDistribusi.aspx")
    End Sub
End Class
