Imports iTextSharp.text
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.Web
Imports System.Data
Partial Class OrderTandaTerima
    Inherits System.Web.UI.Page

    'Private Sub GetTableOrder()
    '    Dim kueri As New StringBuilder()
    '    kueri.Clear()
    '    kueri.Append("SELECT NEWID() AS PKORDERTTDETAIL,'" & Session("PKORDERTT") & "' AS PKORDERTT, ")
    '    kueri.Append("KODE_BARANG,")
    '    kueri.Append("(PENUHI-dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG,KDORDER)) AS JML,")
    '    kueri.Append("'" & Session("NAMAUSER") & "' as NAMAUSER, ")
    '    kueri.Append("'" & Session("KDORDERTT") & "' AS KODEORDERTT, ")
    '    kueri.Append("'" & Session("KDUNKERATAS") & "', ")
    '    kueri.Append("'" & Session("KDUNKER") & "'")
    '    kueri.Append(" FROM TBL_ORDER_DETAIL WHERE KDORDER='" & Session("KDORDERTT") & "'  ")
    '    Dim xTable As New DataTable
    '    Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
    '    Dim conn As New SqlConnection(strConnString)
    '    conn.Open()
    '    Dim cmd As New SqlCommand(kueri.ToString(), conn)
    '    Dim xDa As New SqlDataAdapter(cmd)
    '    xDa.Fill(xTable)
    '    grid.DataSource = xTable
    '    grid.KeyFieldName = "PKORDERTTDETAIL"
    '    grid.DataBind()
    '    conn.Close()



    'End Sub

    Private Sub GetDataOrder()
        '(dbo.getJMLSERTERSPKperKDBRGperUNKER(A.KODE_BARANG , @KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(A.KODE_BARANG , @KDUNKER , getdate() , 1,0))
        Dim xFungsi As New FungsiData
        Dim kueri As New StringBuilder()
        kueri.Clear()
        kueri.Append("INSERT INTO TBL_ORDER_TT_DETAIL(PKORDERTTDETAIL,PKORDERTT, KODE_BARANG, JML, NAMAUSER, KODEORDERTT,KDUNKER,KEUNKER) ")
        kueri.Append("SELECT NEWID() AS PKORDERTTDETAIL,'" & Session("PKORDERTT") & "' AS PKORDERTT, ")
        kueri.Append("KODE_BARANG,")

        kueri.Append("(PENUHI-dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG,KDORDER)) AS JML,")
        kueri.Append("'" & Session("NAMAUSER") & "' as NAMAUSER, ")
        kueri.Append("'" & Session("KDORDERTT") & "' AS KODEORDERTT, ")
        kueri.Append("KDUNKER , ")
        kueri.Append("KEUNKER")
        kueri.Append(" FROM TBL_ORDER_DETAIL WHERE KDORDER='" & Session("KDORDERTT") & "'  ")
        xFungsi.insertData(kueri.ToString())
    End Sub

    Private Sub AmbilOrderBarang()
        Try
            Dim xFungsi As New FungsiData
            Dim sqlQuery As New StringBuilder()
            sqlQuery.Clear()
            sqlQuery.Append("INSERT INTO TBL_ORDER_TT_DETAIL(PKORDERTT, KODE_BARANG, JML, NAMAUSER, KDORDER,KDUNKER) SELECT ")
            sqlQuery.Append("'" & Session("PKORDERTT") & "', ")
            sqlQuery.Append("KODE_BARANG,")
            sqlQuery.Append("Case ")
            sqlQuery.Append("WHEN (PENUHI>(dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0))) THEN (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0)) ")
            sqlQuery.Append("Else (PENUHI-dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG,KDORDER)) ")
            sqlQuery.Append("END AS JML, ")
            ''sqlQuery.Append("(PENUHI - dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG, KDORDER)) As JML, ")
            sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
            sqlQuery.Append("'" & Session("KDORDERTT") & "', ")
            sqlQuery.Append("KDUNKER")
            sqlQuery.Append(" FROM TBL_ORDER_DETAIL WHERE KDORDER='" & Session("KDORDERTT") & "'  ")
            ''Label1.Text = sqlQuery.ToString()
            xFungsi.insertData(sqlQuery.ToString())
        Catch ex As Exception

        End Try

    End Sub
    Private Sub OrderTandaTerima_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If IsPostBack = False Then

            Dim xFungsi As New FungsiData
            xFungsi.delData("TBL_ORDER_TT_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
            lblNoPermohonan.Text = xFungsi.GetData("NO_NOTA", "TBL_ORDER", " KDORDER='" & Session("KDORDERTT") & "'")
            Session("PKORDERTT") = Guid.NewGuid().ToString
            ''xFungsi.delData("TBL_ORDER_TT_DETAIL", "WHERE KET=0 AND PKORDERTT='" & Session("PKORDERTT") & "'")
            ''Label1.Text = Session("KDUNKER")
            Label1.Text = Session("KDORDERTT")


            Dim sqlQuery As New StringBuilder()
            sqlQuery.Clear()
            sqlQuery.Append("INSERT INTO TBL_ORDER_TT_DETAIL(PKORDERTT, KODE_BARANG, JML, NAMAUSER, KDORDER,KDUNKER,KEUNKER) SELECT ")
            sqlQuery.Append("'" & Session("PKORDERTT") & "', ")
            sqlQuery.Append("KODE_BARANG,")
            ''sqlQuery.Append("Case ")
            ''sqlQuery.Append("WHEN (PENUHI>(dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0))) THEN (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0)) ")
            ''sqlQuery.Append("Else (PENUHI-dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG,KDORDER)) ")
            ''sqlQuery.Append("END AS JML, ")
            sqlQuery.Append("(PENUHI - dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG, KDORDER)) As JML, ")
            sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
            sqlQuery.Append("'" & Session("KDORDERTT") & "', ")
            sqlQuery.Append("KDUNKER,")
            sqlQuery.Append("KEUNKER")
            sqlQuery.Append(" FROM TBL_ORDER_DETAIL WHERE KDORDER='" & Session("KDORDERTT") & "'  ")
            ''Label1.Text = sqlQuery.ToString()
            xFungsi.insertData(sqlQuery.ToString())
            Try

                'Dim sqlQuery As New StringBuilder()
                'Dim kueri As String = ""
                'kueri = "INSERT INTO TBL_ORDER_TT_DETAIL(PKORDERTT, KODE_BARANG, JML, NAMAUSER, KDORDER,KDUNKER) SELECT "
                'kueri = kueri + "INSERT INTO TBL_ORDER_TT_DETAIL(PKORDERTT, KODE_BARANG, JML, NAMAUSER, KDORDER,KDUNKER) SELECT "
                'kueri = kueri + "'" & Session("PKORDERTT").ToString() & "', "
                'kueri = kueri + "KODE_BARANG,"
                'kueri = kueri + "(PENUHI - dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG, KDORDER)) As JML, "
                'kueri = kueri + "'" & Session("NAMAUSER") & "', "
                'kueri = kueri + "'" & Session("KDORDERTT") & "', "
                'kueri = kueri + "KDUNKER"
                'kueri = kueri + " FROM TBL_ORDER_DETAIL WHERE KDORDER='" & Session("KDORDERTT").ToString() & "'  "





                'sqlQuery.Clear()
                'sqlQuery.Append("INSERT INTO TBL_ORDER_TT_DETAIL(PKORDERTT, KODE_BARANG, JML, NAMAUSER, KDORDER,KDUNKER) SELECT ")
                'sqlQuery.Append("'" & Session("PKORDERTT") & "', ")
                'sqlQuery.Append("KODE_BARANG,")
                'sqlQuery.Append("Case ")
                'sqlQuery.Append("WHEN (PENUHI>(dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0))) THEN (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0)) ")
                'sqlQuery.Append("Else (PENUHI-dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG,KDORDER)) ")
                'sqlQuery.Append("END AS JML, ")
                'sqlQuery.Append("(PENUHI - dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG, KDORDER)) As JML, ")
                'sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
                'sqlQuery.Append("'" & Session("KDORDERTT") & "', ")
                'sqlQuery.Append("KDUNKER")
                'sqlQuery.Append(" FROM TBL_ORDER_DETAIL WHERE KDORDER='" & Session("KDORDERTT").ToString() & "'  ")
                ''Label1.Text = sqlQuery.ToString()
                'xFungsi.insertData(sqlQuery.ToString())
                ''xFungsi.insertData(sqlQuery.ToString())
                ''xFungsi.insertData(kueri)

            Catch ex As Exception

            End Try


        End If
    End Sub


    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Response.Redirect("OrderDistribusi.aspx")
    End Sub
    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs)
        Dim xFungsi As New FungsiData

        'dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER, getdate(), 1, 0))
        If xFungsi.checkData("SELECT PKORDERTTDETAIL FROM TBL_ORDER_TT_DETAIL WHERE PKORDERTT='" & Session("KDORDERTT") & "' AND JML>(dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG,'" & Session("KDUNKERTT") & "',getdate())-dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & Session("KDUNKERTT") & "',GETDATE(),1,0)) ") = True Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Stok tidak tersedia, mohon sesuaikan jml barang atau adakan pengadaan terlebih dahalu')", True)
        ElseIf grid.VisibleRowCount > 0 Then
            Dim confirmValue As String = Request.Form("confirm_value")
            If confirmValue = "Yes" Then


                Dim sqlQuery As New StringBuilder()
                sqlQuery.Clear()
                sqlQuery.Append("INSERT INTO TBL_ORDER_TT(PKORDERTT,KDORDER, NO_TT, USERCETAK, KDUNKER) VALUES( ")
                sqlQuery.Append("'" & Session("PKORDERTT") & "', ")
                sqlQuery.Append("'" & Session("KDORDER") & "', ")
                sqlQuery.Append(" dbo.NextNoTTOrder(YEAR(GETDATE())),")
                sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
                sqlQuery.Append("'" & Session("KDUNKER") & "')")
                ''Label1.Text = sqlQuery.ToString
                xFungsi.insertData(sqlQuery.ToString)


                xFungsi.UpdateData("TBL_ORDER_TT_DETAIL", "KET=1,TGL_PROSES=GETDATE()", " PKORDERTT='" & Session("PKORDERTT") & "'")
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Barang sudah diproses')", True)
                Session.Remove("PKORDERTT")

                Response.Redirect("OrderDistribusi.aspx")
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Data Barang Kosong')", True)
            End If
        End If
        'End If
    End Sub
    Protected Sub grid_RowValidating(sender As Object, e As Data.ASPxDataValidationEventArgs)
        Dim xPenuhi As Int16 = e.NewValues("PENUHI")
        Dim xSudah As Int16 = e.NewValues("SUDAH")
        Dim xJML As Int16 = e.NewValues("JML")
        'Dim xStokReserved As Int16 = e.NewValues("STOKRESERVED")

        ' Dim xKunci As String = sender.Columns("STOK")
        'Label1.Text = xStok
        If (xJML + xSudah) > xPenuhi Then
            AddError(e.Errors, sender.Columns("JML_PENUHI"), "Permohonan lebih besar daripada permintaan")
            e.RowError = "Jumlah yang dipenuhi lebih besar daripada permintaan"
        End If
    End Sub

    Private Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If errors.ContainsKey(column) Then
            Return
        End If
        errors(column) = errorText
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        '  GetTableOrder()
        Dim xFungsi As New FungsiData
        Dim sqlQuery As New StringBuilder()
        sqlQuery.Clear()
        sqlQuery.Append("INSERT INTO TBL_ORDER_TT_DETAIL(PKORDERTT, KODE_BARANG, JML, NAMAUSER, KDORDER,KDUNKER) SELECT ")
        sqlQuery.Append("'" & Session("PKORDERTT") & "', ")
        sqlQuery.Append("KODE_BARANG,")
        ''sqlQuery.Append("Case ")
        ''sqlQuery.Append("WHEN (PENUHI>(dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0))) THEN (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , KDUNKER , getdate() , 1,0)) ")
        ''sqlQuery.Append("Else (PENUHI-dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG,KDORDER)) ")
        ''sqlQuery.Append("END AS JML, ")
        sqlQuery.Append("(PENUHI - dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG, KDORDER)) As JML, ")
        sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
        sqlQuery.Append("'" & Session("KDORDERTT") & "', ")
        sqlQuery.Append("KDUNKER")
        sqlQuery.Append(" FROM TBL_ORDER_DETAIL WHERE KDORDER='" & Session("KDORDERTT") & "'  ")
        ''Label1.Text = sqlQuery.ToString()
        xFungsi.insertData(sqlQuery.ToString())
        grid.DataBind()
    End Sub
End Class
