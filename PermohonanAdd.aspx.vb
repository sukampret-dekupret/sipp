Imports System.Data.SqlClient

Partial Class PermohonanAdd
    Inherits System.Web.UI.Page

    Private Sub PermohonanAdd_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then
            Session.Clear()
            Response.Redirect("Default.aspx")
        End If

        If Not IsPostBack Then
            Dim xFungsi As New FungsiData
            Dim pIDPERMOHONAN As String = xFungsi.GetData("IDPERMOHONAN", "TBL_PERMOHONAN_DETAIL", "KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
            Label1.Text = pIDPERMOHONAN
            ''xFungsi.delData("TBL_PERMOHONAN_DETAIL", "WHERE KET=0 AND NAMAUSER='" & Session("NAMAUSER") & "'")
            If pIDPERMOHONAN = "-" Then
                Session("IDPERMOHONAN") = Guid.NewGuid().ToString
            Else
                Session("IDPERMOHONAN") = pIDPERMOHONAN
            End If
            txtTanggal.Value = Date.Now

            cboUnitKerja.SelectedValue = Session("KDUNKER").ToString
            cboUnitKerja.Enabled = False

        End If

    End Sub
    Protected Sub grid_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
        e.NewValues("JML_MOHON") = 1
    End Sub
    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        ''Dim confirmValue As String = Request.Form("confirm_value")
        ''If confirmValue = "Yes" Then
        If grid.VisibleRowCount = 0 Or txtPerihal.Text = "" Then
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Mohon lengkapi permohonan anda')", True)
            Else
                Dim sqlQuery As New StringBuilder()
                Dim strConnString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
                Dim conn As New SqlConnection()
            ''Dim kd_Unker As String = Session("KDUNKER").ToString
            Dim kd_Unker As String = cboUnitKerja.SelectedValue


            conn.ConnectionString = strConnString
                sqlQuery.Clear()
                sqlQuery.Append("INSERT INTO [dbo].[TBL_PERMOHONAN]([NO_PERMOHONAN], [DRUNKER], [TGL_PERMOHONAN], [PERIHAL],[STS_PERMOHONAN],[NAMAUSER],[IDPERMOHONAN]) VALUES( ")
                sqlQuery.Append(" dbo.NextNoPermohonan(" & Year(Date.Now) & ", '" & kd_Unker & "' ), ")
            sqlQuery.Append("'" & cboUnitKerja.SelectedValue & "',")
            sqlQuery.Append("'" & txtTanggal.Value & "',")
            ''sqlQuery.Append("GETDATE(),")

            sqlQuery.Append("'" & txtPerihal.Text & "',")
                sqlQuery.Append("0, ")
                sqlQuery.Append("'" & Session("NAMAUSER").ToString & "',")
                sqlQuery.Append("'" & Session("IDPERMOHONAN").ToString & "')")
                ''MsgBox(sqlQuery)

                conn.Open()
                Dim cmd As New SqlCommand
                cmd = New SqlCommand(sqlQuery.ToString())
                cmd.Connection = conn
                Dim i As Integer = cmd.ExecuteNonQuery()
                Dim xFungsi As New FungsiData
                xFungsi.UpdateData("TBL_PERMOHONAN_DETAIL", "KET=1", " IDPERMOHONAN='" & Session("IDPERMOHONAN") & "'")
                ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Permohonan anda telah diproses')", True)
                Session.Remove("IDPERMOHONAN")
                Session("IDPERMOHONAN") = Guid.NewGuid().ToString
                txtPerihal.Text = ""

            End If

        ''Else
        ''ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('Anda telah membatalkan pilihan!')", True)
        ''End If
    End Sub
    Protected Sub grid_RowInserted(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertedEventArgs) Handles grid.RowInserted
        grid.DataBind()
        grid.FocusedRowIndex = grid.VisibleRowCount - 1

    End Sub
End Class
