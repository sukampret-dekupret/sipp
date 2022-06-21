<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Dashboard</li>
        <li class="breadcrumb-item active">Dashboard</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Dashboard
            <small>Dashboard</small>
        </h1>


    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show" runat="server" id="panelunitpermohonan" visible="false">
                    <div class="panel-hdr">
                        <h2>Dashboard  <span class="fw-300"><i>Permohonan</i></span>
                        </h2>
                        <div class="panel-toolbar">
                            <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                            <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                            <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>

                        </div>
                    </div>
                    <div class="panel-content">
                        <asp:Literal ID="liPermohonan" runat="server"></asp:Literal>
                    </div>

                    <div class="panel-content">
                        <asp:Literal ID="liPerbanding" runat="server"></asp:Literal>
                    </div>

                    <div class="panel-content">
                        <asp:Literal ID="liDasborUnit" runat="server"></asp:Literal>
                    </div>

                                        <div class="panel-content">
                        <asp:Literal ID="liDaborAdmin1" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:SqlDataSource ID="sqlPermohonanTerbanyak" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>" SelectCommand="SELECT TOP 10 CONCAT(A.KODE_BARANG,'-',B.NAMA_BARANG) AS NAMABARANG,JML_MOHON FROM TBL_PERMOHONAN_DETAIL A INNER JOIN TBL_BARANG B 
ON A.KODE_BARANG=B.KODE_BARANG WHERE A.KDUNKER=@KDUNKER ORDER BY JML_MOHON DESC
">
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlKompareMohonPenuhi" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT NAMA_UNKER,dbo.getJMLMOHONORDERperUNKER(KD_UNKER,GETDATE()) as PERMOHONAN,dbo.getJMLPENUHIORDERperUNKER(KD_UNKER,GETDATE()) as DIPENUHI 
        FROM TBL_UNKER WHERE KD_STS_AKTIF=1 AND KDUNKERUP='05A0B010C000' AND KD_UNKER&lt;&gt;'05A0B010C000'"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlDasborUnit1" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT NAMA_BULAN,
dbo.getJMLSERTERORDERperUNKERperTGLantaraV2(
@KDUNKER,
DATEADD(m, BULAN - 1, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0)),
DATEADD(d, -1, DATEADD(m, BULAN, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0))),0,1) AS MASUK,
dbo.getJMLSERTERperUNKERperTGLantaraV2(
@KDUNKER,
DATEADD(m, BULAN - 1, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0)),
DATEADD(d, -1, DATEADD(m, BULAN, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0)))
) AS KELUAR
  FROM TBL_BULAN">
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    <asp:SqlDataSource ID="sqlBoardAdmin1" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>" SelectCommand="SELECT NAMA_BULAN,
dbo.getJMLSERTERSPKperUNKERperTGLantara(
@KDUNKER,
DATEADD(m, BULAN - 1, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0)),
DATEADD(d, -1, DATEADD(m, BULAN, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0)))
) AS MASUK,
dbo.getJMLSERTERORDERperUNKERperTGLantaraV2(
@KDUNKER,
DATEADD(m, BULAN - 1, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0)),
DATEADD(d, -1, DATEADD(m, BULAN, DATEADD(yyyy, YEAR(GETDATE()) - 1900, 0))),1,0) AS KELUAR
  FROM TBL_BULAN">
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKERATAS" />
        </SelectParameters>
        </asp:SqlDataSource>
</asp:Content>

