Imports System.IO
Imports System.Net
Imports FungsiData
Partial Class Site
    Inherits System.Web.UI.MasterPage
    Public jmlVerifikasi As String = ""
    Public jmlOrderPending As String = ""
    Public jmlOrderOnProgres As String = ""
    Public xPoto As String = ""
    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs)
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Session.Clear()
            Session("PKUSER") = ""
            Session("NAMA") = ""
            Session("KD_BAGIAN") = ""
            Session("FID") = ""
            Session("KDUNKER") = ""

            Response.Redirect("Default.aspx")
        Else

        End If
    End Sub

    Private Sub Site_Load(sender As Object, e As EventArgs) Handles Me.Load


        If String.IsNullOrEmpty(Session("NAMAUSER")) = True Then

            Session.Clear()
            Response.Redirect("Default.aspx")
        End If

        If (Not IsPostBack) Then
            ''Generate Menu
            '1 ESELON 2
            '2 GUDANG ESELON 1
            '3 GUDANG ESELON 2
            '4 USER BIASA
            '5 VISA
            '6 PASPOR
            '7 LEGALISASI
            '8 EXIT PERMIT
            '9 IJIN TINGGAL
            If ((Session("ROLEID") = 0) Or (Session("ROLEID") = 10)) Then
                liAdmin1.Visible = True
                liAdmin2.Visible = True
                liVerifPermohonan1.Visible = True
                liVerifPermohonan2.Visible = True
                liLaporanPusatHeader.Visible = True
                liLaporanPusatSubHeader.Visible = True
                liReferensi.Visible = True

                jmlVerifikasi = "5"
                xPoto = "img/demo/avatars/admin.jpeg"

            ElseIf Session("ROLEID") = 1 Then
                liMohonDariUser1.Visible = True
                liMohonDariUser2.Visible = True
                'liMohonDariUser3.Visible = True
                liMohonKePusat1.Visible = True
                liMohonKePusat2.Visible = True
                liMohonKePusat4.Visible = True
                liMohonKePusat5.Visible = True
                liMohonKePusat6.Visible = True
                liSetting1.Visible = True
                liSetting2.Visible = True
                liLaporanUnit1.Visible = True
                liLaporanUnit2.Visible = True
                xPoto = "img/demo/avatars/admin.jpeg"
            ElseIf Session("ROLEID") = 2 Then
                liGudangPusat1.Visible = True
                liGudangPusat2.Visible = True
                liGudangPusat3.Visible = True
                liGudangPusat4.Visible = True
                liLaporanPusatHeader.Visible = True
                liLaporanPusatSubHeader.Visible = True
                xPoto = "img/demo/avatars/Operator.jpeg"
            ElseIf Session("ROLEID") = 3 Then
                liMohonKePusat1.Visible = True
                liMohonKePusat3.Visible = True
                liMohonKePusat4.Visible = True
                liMohonKePusat5.Visible = True
                liMohonKePusat6.Visible = True
                liGudang1.Visible = True
                liGudang2.Visible = True
                liLaporanUnit1.Visible = True
                liLaporanUnit2.Visible = True
                xPoto = "img/demo/avatars/Operator.jpeg"
            ElseIf Session("ROLEID") = 4 Then
                liMohonKeUnit.Visible = True
                liMenuMohonKeUnit.Visible = True
                xPoto = "img/demo/avatars/User.jpeg"
            ElseIf Session("ROLEID") = 5 Then

            ElseIf Session("ROLEID") = 6 Then

            ElseIf Session("ROLEID") = 7 Then

            End If
            Me.DataBind()
        End If

        ''Me.DataBind()

    End Sub

    Public Shared Function IsValid(ByVal Url As String) As Boolean
        Dim sStream As Stream
        Dim URLReq As HttpWebRequest
        Dim URLRes As HttpWebResponse

        Try
            URLReq = WebRequest.Create(Url)
            URLRes = URLReq.GetResponse()
            sStream = URLRes.GetResponseStream()
            Dim reader As String = New StreamReader(sStream).ReadToEnd()
            Return True
        Catch ex As Exception
            'Url not valid
            Return False
        End Try
    End Function
End Class

