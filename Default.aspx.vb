Imports System.DirectoryServices
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Partial Class _Default
    Inherits System.Web.UI.Page


    Public Function AuthenticateUser(domain As String, xUsername As String, xPassword As String, LdapPath As String, ByRef Errmsg As String) As Boolean
        Errmsg = ""
        Dim domainAndUsername As String = String.Format("{0}\{1}", domain, xUsername)
        Dim entry As New DirectoryEntry(LdapPath, domainAndUsername, xPassword)

        Try
            ' Bind to the native AdsObject to force authentication.
            Dim obj As [Object] = entry.NativeObject
            Dim search As New DirectorySearcher(entry)
            search.Filter = String.Format("(SAMAccountName={0})", xUsername)
            search.PropertiesToLoad.Add("cn")
            Dim result As SearchResult = search.FindOne()
            If result Is Nothing Then
                Return False
            End If
            ' Update the new path to the user in the directory
            LdapPath = result.Path
            Dim _filterAttribute As String = DirectCast(result.Properties("cn")(0), [String])
        Catch ex As Exception
            Errmsg = ex.Message
            Return False
            Throw New Exception("Error authenticating user." + ex.Message)
        End Try
        Return True
    End Function
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim oPass As String
        Dim oDomain As String = "kemlu.go.id"
        Dim oUser As String = txtUsername.Value
        oPass = txtPassword.Value
        Dim oPath As String = "LDAP://172.16.200.2"
        Dim oErr As String = "error"
        'If AuthenticateUser(oDomain, oUser, oPass, oPath, oErr) = True Then
        '    'MsgBox("SIP LAH")
        '    Session.Clear()

        '    Dim cn As New SqlConnection()
        '    'Try
        '    Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        '    cn.ConnectionString = strConnectionString
        '    Dim sqlUserName As String = ""
        '    sqlUserName = "SELECT NAMA,NAMAUSER,NIP,ROLEID,KD_SATKER,KD_SATKER_ATAS,KODE FROM TBL_USER "
        '    sqlUserName &= " WHERE (NAMAUSER = @Username)"

        '    Dim com As New SqlCommand(sqlUserName, cn)

        '    com.Parameters.AddWithValue("@Username", oUser)
        '    cn.Open()

        '    Dim strBaca As SqlDataReader = com.ExecuteReader()
        '    While strBaca.Read()
        '        If strBaca("NAMAUSER").ToString <> "" Then
        '            Session("NAMAUSER") = strBaca("NAMAUSER").ToString
        '            Session("NAMA") = strBaca("NAMA").ToString
        '            Session("ROLEID") = strBaca("ROLEID").ToString
        '            Session("KDUNKER") = strBaca("KD_SATKER").ToString
        '            Session("KDUNKERATAS") = strBaca("KD_SATKER_ATAS").ToString
        '            Session("NIP") = strBaca("NIP").ToString
        '            Session("KODE") = strBaca("KODE").ToString
        '            Dim xSDM As New ClassSDM
        '            'Session("SATKER") = xSDM.GetDataSDM("B.nama_unker", "tbl_d_pgw A INNER JOIN tbl_r_unker B ON A.kd_unker=B.kd_unker", "A.nip='" & strBaca("NIP").ToStrin & "'")
        '            If Session("ROLEID") = 4 Then
        '                Response.Redirect("DashboardUser.aspx")
        '            ElseIf Session("ROLEID") = 1 Then
        '                Response.Redirect("DashboardAdmin1.aspx")
        '            End If
        '        Else
        '            lbSalah.Text = "DATABASE KAPUT"
        '        End If
        '    End While
        'Else
        '    lbSalah.Text = "GAGAL KONEK"
        'End If


        Session.Clear()

        Dim cn As New SqlConnection()
        'Try
        Dim strConnectionString As String = ConfigurationManager.ConnectionStrings("conPersediaan").ConnectionString
        cn.ConnectionString = strConnectionString
        'Dim sqlUserName As String = "SELECT PKUSER,FUNGSI_KD,NAMA,NAMAUSER,KDNOTA,dbo.GetFungsiGroup(PKUSER) as FUNGSIGROUP FROM TBL_USER "
        Dim sqlUserName As String = "SELECT NAMA,NAMAUSER,NIP,ROLEID,KD_SATKER,KD_SATKER_ATAS,KODE,ISKEU FROM TBL_USER "
        sqlUserName &= " WHERE (NAMAUSER = @Username) AND (PASS=@Password)"

        Dim com As New SqlCommand(sqlUserName, cn)

        com.Parameters.AddWithValue("@Username", oUser)
        com.Parameters.AddWithValue("@Password", oPass)
        cn.Open()

        Dim strBaca As SqlDataReader = com.ExecuteReader()
        While strBaca.Read()
            If strBaca("NAMAUSER").ToString <> "" Then
                'Session("PKUSER") = strBaca("PKUSER").ToString
                'Session("NAMA") = strBaca("NAMA").ToString
                'Session("KD_BAGIAN") = strBaca("KD_BAGIAN").ToString
                'Session("FID") = strBaca("FID").ToString

                Session("NAMAUSER") = strBaca("NAMAUSER").ToString
                Session("NAMA") = strBaca("NAMA").ToString
                Session("ROLEID") = strBaca("ROLEID").ToString
                Session("KDUNKER") = strBaca("KD_SATKER").ToString
                Session("KDUNKERATAS") = strBaca("KD_SATKER_ATAS").ToString
                Session("NIP") = strBaca("NIP").ToString
                Session("KODE") = strBaca("KODE").ToString
                Session("ISKEU") = strBaca("ISKEU").ToString

                Response.Redirect("Dashboard.aspx")

                'If Session("KODE") = 1 Then
                '    Response.Redirect("DashboardAdmin1.aspx")
                'Else
                '    If Session("ROLEID") = 1 Then
                '        Response.Redirect("DashboardAdmin2.aspx")
                '    Else
                '        Response.Redirect("DashboardUser.aspx")
                '    End If
                'End If

            Else
                '//lbSalah.Text = "PASSWORD SALAH"
            End If
        End While


        'Else
        'lbSalah.Text = "PASSWORD SALAH"
        'End If
    End Sub
End Class
