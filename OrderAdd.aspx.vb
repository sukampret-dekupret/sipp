Imports System.IO
Imports FungsiData

Partial Class OrderAdd
    Inherits System.Web.UI.Page

    Private Sub OrderAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If
        If Not IsPostBack Then

            'Session("KDORDER") = Session("IDORDERUBAH")
            '    Dim xFungsi As New FungsiData
            '    txtNoFaktur.Text = xFungsi.GetData("NO_NOTA", "TBL_ORDER", " KDORDER='" & Session("KDORDER") & "'")
            '    txtPerihal.Text = xFungsi.GetData("PERIHAL", "TBL_ORDER", " KDORDER='" & Session("KDORDER") & "'")
            '    txtTglFaktur.Value = xFungsi.GetData("TGL_NOTA", "TBL_ORDER", " KDORDER='" & Session("KDORDER") & "'")
            '    cboJenis.SelectedValue = xFungsi.GetData("JENIS", "TBL_ORDER", " KDORDER='" & Session("KDORDER") & "'")
            '    'cboMitra.SelectedItem = cboMitra.Items.FindByValue()
            'cboMitra.SelectedItem.Value = cboMitra.Items.FindByValue(xFungsi.GetData("KODE_MITRA", "TBL_SPK", " IDSPK='" & Session("IDSPK") & "'"))
            Dim xFungsi As New FungsiData
            xFungsi.delData("TBL_ORDER_DETAIL", " WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")

            Session("KDORDER") = Guid.NewGuid().ToString


            cboJenis.SelectedValue = 0
            'cboUnitKerja.SelectedValue = Session("KDUNKERATAS").ToString
            cboUnitKerja.Enabled = False
            txtTglFaktur.Value = Date.Now


            'Session("KDORDER") = Guid.NewGuid().ToString
            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString



        End If
    End Sub

    Protected Sub grid_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("MOHON") = 10
    End Sub
    'Protected Sub btnProses_Click(sender As Object, e As EventArgs) Handles btnProses.Click

    '    Dim xFungsi As New FungsiData
    '    Dim confirmValue As String = Request.Form("confirm_value")
    '    If confirmValue = "Yes" Then



    '        Dim xKetemu As Integer = xFungsi.countData("TBL_ORDER", "NO_NOTA", " WHERE NO_NOTA='" & txtNoFaktur.Text & "'")
    '        If xKetemu > 0 Then
    '            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('No Nota Sudah ADA')", True)
    '            txtNoFaktur.Focus()
    '            'Else
    '            '    Dim xNomorNota As String = xFungsi.maxData("TBL_ORDER", "NO_NOTA", "WHERE KEUNKER='" & Session("KEUNKER") & "'")


    '        End If


    '        If txtNoFaktur.Text = "" Then
    '            txtNoFaktur.Focus()
    '        ElseIf grid.VisibleRowCount = 0 Then
    '            grid.Focus()
    '        ElseIf txtPerihal.Text = "" Then
    '            txtPerihal.Focus()
    '        ElseIf cboJenis.SelectedValue = "" Then
    '            cboJenis.Focus()
    '            'ElseIf (xFileUpload.HasFile = False) And (Session("UbahOrder") = False) Then
    '            '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('File masih kosong')", True)
    '        Else
    '            Dim xFUName As String = Session("KDORDER").ToString & Path.GetExtension(xFileUpload.PostedFile.FileName)
    '            Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "ORDER/" & Year(txtTglFaktur.Value)))
    '            Session("URLFILE") = directoryPath & "/" & xFUName
    '            'Dim sqlQuery As New StringBuilder()
    '            'Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
    '            'Dim conn As New SqlConnection()
    '            'conn.ConnectionString = strConnString
    '            'sqlQuery.Clear()
    '            'sqlQuery.Append("INSERT INTO [dbo].[TBL_SPK]([IDSPK], [NOFAKTUR], [TGL_FAKTUR], [PERIHAL], [KODE_MITRA], [KDUNKER], [URLFILE], [NAMAUSER]) VALUES( ")
    '            'sqlQuery.Append("'" & Session("IDSPK") & "', ")
    '            'sqlQuery.Append("'" & txtFaktur.Value & "',")
    '            'sqlQuery.Append("'" & txtTglFaktur.Value & "',")
    '            'sqlQuery.Append("'" & txtPerihal.Value & "',")
    '            'sqlQuery.Append("" & cboMitra.Value & ", ")
    '            'sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "', ")
    '            'sqlQuery.Append("'" & xFileLengkap & "', ")
    '            'sqlQuery.Append("'" & Session("NAMAUSER").ToString & "')")


    '            'conn.Open()
    '            'Dim cmd As New SqlCommand
    '            'cmd = New SqlCommand(sqlQuery.ToString())
    '            'cmd.Connection = conn
    '            'Dim i As Integer = cmd.ExecuteNonQuery()

    '            sqlOrder.Insert()
    '            xFungsi.UpdateData("TBL_ORDER_DETAIL", "KET=1,TGLPROSES=GETDATE(),NAMAPROSES='" & Session("NAMAUSER") & "'", " KDORDER='" & Session("KDORDER") & "'")



    '            If xFileUpload.HasFile Then

    '                If Not Directory.Exists(directoryPath) Then
    '                    Directory.CreateDirectory(directoryPath)
    '                    xFileUpload.SaveAs(directoryPath & " \ " & xFUName)
    '                Else
    '                    xFileUpload.SaveAs(directoryPath & " \ " & xFUName)
    '                End If
    '            End If

    '            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order Permohonan sudah berhasil sudah dikirim ke ADMIN untuk dicek ulang, mohon tunggu untuk konfirmasi')", True)
    '            Session.Remove("KDORDER")
    '            Session("KDORDER") = Guid.NewGuid().ToString
    '            txtPerihal.Text = ""
    '            txtNoFaktur.Text = ""
    '            txtTglFaktur.Value = Date.Now
    '        End If
    '    Else
    '        ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan')", True)
    '    End If

    'End Sub
    'Protected Sub btnAmbilData_Click(sender As Object, e As EventArgs)
    '    Dim xFungsi As New FungsiData
    '    Dim sqlQuery As New StringBuilder()
    '    sqlQuery.Clear()
    '    sqlQuery.Append("INSERT INTO TBL_ORDER_DETAIL(KDORDER, KODE_BARANG, MOHON, PENUHI, KDUNKER, KEUNKER, NAMAUSER,IDPERMOHONAN) SELECT ")
    '    sqlQuery.Append("'" & Session("KDORDER") & "', ")
    '    sqlQuery.Append("KODE_BARANG,")
    '    sqlQuery.Append("JML_PENUHI,")
    '    sqlQuery.Append("JML_PENUHI,")
    '    sqlQuery.Append("'" & Session("KDUNKER") & "', ")
    '    sqlQuery.Append("'" & Session("KDUNKERATAS") & "', ")
    '    sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
    '    sqlQuery.Append("A.IDPERMOHONAN ")
    '    sqlQuery.Append("FROM TBL_PERMOHONAN_DETAIL A INNER JOIN TBL_PERMOHONAN B ON A.IDPERMOHONAN=B.IDPERMOHONAN WHERE STS_PERMOHONAN=3 ")
    '    'Label1.Text = sqlQuery.ToString


    '    xFungsi.insertData(sqlQuery.ToString)
    '    grid.DataBind()

    'End Sub
    'Protected Sub btnAmbilData_Click1(sender As Object, e As EventArgs) Handles btnAmbilData.Click
    '    Dim xFungsi As New FungsiData
    '    Dim sqlQuery As New StringBuilder()
    '    xFungsi.delData("TBL_SPK_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
    '    sqlQuery.Clear()
    '    sqlQuery.Append("INSERT INTO [TBL_ORDER_DETAIL] ( [KDORDER], [KODE_BARANG], [MOHON], [NAMAUSER], [KDUNKER],[KEUNKER]) SELECT  ")
    '    sqlQuery.Append("'" & Session("IDSPK") & "', ")
    '    sqlQuery.Append("KODE_BARANG,")
    '    sqlQuery.Append("(dbo.getJMLPENUHIORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1)) AS JML,")
    '    sqlQuery.Append("ISNULL(dbo.getHargaTerakhir(KODE_BARANG),1) AS HARGA,")
    '    sqlQuery.Append("'" & Session("NAMAUSER").ToString & "',")
    '    sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "' ")
    '    sqlQuery.Append(" FROM TBL_BARANG WHERE ST_BARANG=1 AND (dbo.getJMLPENUHIORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1)) > 0")

    '    xFungsi.insertData(sqlQuery.ToString())
    '    grid.DataBind()
    'End Sub
    'Protected Sub btnAmbilData_Click(sender As Object, e As EventArgs)
    '    Dim xFungsi As New FungsiData
    '    Dim sqlQuery As New StringBuilder()
    '    'Dim sqlQuerynomor As New StringBuilder()

    '    sqlQuery.Clear()
    '    sqlQuery.Append("INSERT INTO TBL_ORDER_DETAIL(KDORDER, KODE_BARANG, MOHON, PENUHI, KDUNKER, KEUNKER, NAMAUSER) SELECT ")
    '    sqlQuery.Append("'" & Session("KDORDER") & "', ")
    '    sqlQuery.Append("KODE_BARANG,")
    '    sqlQuery.Append("(dbo.getJMLPENUHIperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE()) - dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE())) AS MOHON,")
    '    sqlQuery.Append("(dbo.getJMLPENUHIperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE()) - dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE())) AS PENUHI,")
    '    sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "',")
    '    sqlQuery.Append("'" & Session("KDORDERATAS") & "', ")
    '    sqlQuery.Append("'" & Session("NAMAUSER") & "' ")
    '    sqlQuery.Append(" FROM TBL_BARANG WHERE ST_BARANG=1 AND (dbo.getJMLPENUHIperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE()) - dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE())) > 0")


    '    xFungsi.insertData(sqlQuery.ToString)
    '    grid.DataBind()

    '    ' sqlQuerynomor.Clear()


    'End Sub
    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs)
        Dim xFungsi As New FungsiData
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then



            Dim xKetemu As Integer = xFungsi.countData("TBL_ORDER", "NO_NOTA", " WHERE NO_NOTA='" & txtNoFaktur.Text & "'")
            If xKetemu > 0 Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('No Nota Sudah ADA')", True)
                txtNoFaktur.Focus()
                'Else
                '    Dim xNomorNota As String = xFungsi.maxData("TBL_ORDER", "NO_NOTA", "WHERE KEUNKER='" & Session("KEUNKER") & "'")


            End If


            If txtNoFaktur.Text = "" Then
                txtNoFaktur.Focus()
            ElseIf grid.VisibleRowCount = 0 Then
                grid.Focus()
            ElseIf txtPerihal.Text = "" Then
                txtPerihal.Focus()
            ElseIf cboJenis.SelectedValue = "" Then
                cboJenis.Focus()
                'ElseIf (xFileUpload.HasFile = False) And (Session("UbahOrder") = False) Then
                '    ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('File masih kosong')", True)
            Else
                Dim xFUName As String = Session("KDORDER").ToString & Path.GetExtension(xFileUpload.PostedFile.FileName)
                Dim directoryPath As String = Server.MapPath(String.Format("~/{0}/", "ORDER/" & Year(txtTglFaktur.Value)))
                Session("URLFILE") = directoryPath & "/" & xFUName
                'Dim sqlQuery As New StringBuilder()
                'Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
                'Dim conn As New SqlConnection()
                'conn.ConnectionString = strConnString
                'sqlQuery.Clear()
                'sqlQuery.Append("INSERT INTO [dbo].[TBL_SPK]([IDSPK], [NOFAKTUR], [TGL_FAKTUR], [PERIHAL], [KODE_MITRA], [KDUNKER], [URLFILE], [NAMAUSER]) VALUES( ")
                'sqlQuery.Append("'" & Session("IDSPK") & "', ")
                'sqlQuery.Append("'" & txtFaktur.Value & "',")
                'sqlQuery.Append("'" & txtTglFaktur.Value & "',")
                'sqlQuery.Append("'" & txtPerihal.Value & "',")
                'sqlQuery.Append("" & cboMitra.Value & ", ")
                'sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "', ")
                'sqlQuery.Append("'" & xFileLengkap & "', ")
                'sqlQuery.Append("'" & Session("NAMAUSER").ToString & "')")


                'conn.Open()
                'Dim cmd As New SqlCommand
                'cmd = New SqlCommand(sqlQuery.ToString())
                'cmd.Connection = conn
                'Dim i As Integer = cmd.ExecuteNonQuery()

                sqlOrder.Insert()
                xFungsi.UpdateData("TBL_ORDER_DETAIL", "KET=1,TGLPROSES=GETDATE(),NAMAPROSES='" & Session("NAMAUSER") & "'", " KDORDER='" & Session("KDORDER") & "'")



                If xFileUpload.HasFile Then

                    If Not Directory.Exists(directoryPath) Then
                        Directory.CreateDirectory(directoryPath)
                        xFileUpload.SaveAs(directoryPath & " \ " & xFUName)
                    Else
                        xFileUpload.SaveAs(directoryPath & " \ " & xFUName)
                    End If

                    Console.WriteLine(xFUName)
                End If

                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Order Permohonan sudah berhasil sudah dikirim ke ADMIN untuk dicek ulang, mohon tunggu untuk konfirmasi')", True)
                Session.Remove("KDORDER")
                Session("KDORDER") = Guid.NewGuid().ToString
                txtPerihal.Text = ""
                txtNoFaktur.Text = ""
                txtTglFaktur.Value = Date.Now
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda membatalkan pilihan')", True)
        End If

    End Sub
    'Protected Sub btnAmbilData_Click(sender As Object, e As EventArgs)
    '    Dim xFungsi As New FungsiData
    '    Dim sqlQuery As New StringBuilder()
    '    sqlQuery.Clear()
    '    sqlQuery.Append("INSERT INTO TBL_ORDER_DETAIL(KDORDER, KODE_BARANG, MOHON, PENUHI, KDUNKER, KEUNKER, NAMAUSER,IDPERMOHONAN) SELECT ")
    '    sqlQuery.Append("'" & Session("KDORDER") & "', ")
    '    sqlQuery.Append("KODE_BARANG,")
    '    sqlQuery.Append("JML_PENUHI,")
    '    sqlQuery.Append("JML_PENUHI,")
    '    sqlQuery.Append("'" & Session("KDUNKER") & "', ")
    '    sqlQuery.Append("'" & Session("KDUNKERATAS") & "', ")
    '    sqlQuery.Append("'" & Session("NAMAUSER") & "', ")
    '    sqlQuery.Append("A.IDPERMOHONAN ")
    '    sqlQuery.Append("FROM TBL_PERMOHONAN_DETAIL A INNER JOIN TBL_PERMOHONAN B ON A.IDPERMOHONAN=B.IDPERMOHONAN WHERE STS_PERMOHONAN=3 ")
    '    'Label1.Text = sqlQuery.ToString


    '    xFungsi.insertData(sqlQuery.ToString)
    '    grid.DataBind()

    'End Sub
    'Protected Sub btnAmbilData_Click1(sender As Object, e As EventArgs) Handles btnAmbilData.Click
    '    Dim xFungsi As New FungsiData
    '    Dim sqlQuery As New StringBuilder()
    '    xFungsi.delData("TBL_SPK_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
    '    sqlQuery.Clear()
    '    sqlQuery.Append("INSERT INTO [TBL_ORDER_DETAIL] ( [KDORDER], [KODE_BARANG], [MOHON], [NAMAUSER], [KDUNKER],[KEUNKER]) SELECT  ")
    '    sqlQuery.Append("'" & Session("IDSPK") & "', ")
    '    sqlQuery.Append("KODE_BARANG,")
    '    sqlQuery.Append("(dbo.getJMLPENUHIORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1)) AS JML,")
    '    sqlQuery.Append("ISNULL(dbo.getHargaTerakhir(KODE_BARANG),1) AS HARGA,")
    '    sqlQuery.Append("'" & Session("NAMAUSER").ToString & "',")
    '    sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "' ")
    '    sqlQuery.Append(" FROM TBL_BARANG WHERE ST_BARANG=1 AND (dbo.getJMLPENUHIORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'" & cboUnitKerja.SelectedValue & "',GETDATE(),1)) > 0")

    '    xFungsi.insertData(sqlQuery.ToString())
    '    grid.DataBind()
    'End Sub
End Class
