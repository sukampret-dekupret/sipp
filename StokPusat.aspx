<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="StokPusat.aspx.vb" Inherits="StokPusat" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Laporan</li>
        <li class="breadcrumb-item active">Daftar Stok Barang</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="row">
        <div class="col-lg-4">
            <div id="panel-atas-1" class="panel">
                <%--                <div class="panel-hdr">
                </div>--%>
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">UNIT KERJA</label>
                            <%--<asp:DropDownList ID="cboUnitKerja" runat="server" CssClass="form-control" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" AutoPostBack="true"></asp:DropDownList>--%>
                            <asp:DropDownList ID="cboUnitKerja" runat="server" AutoPostBack="True" DataSourceID="sqlUnker" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" CssClass="form-control"></asp:DropDownList>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div id="panel-atas-bulan" class="panel">
                <%--                <div class="panel-hdr">
                    

                </div>--%>
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">BULAN</label>
                            <asp:DropDownList ID="cboBulan" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="1">JANUARI</asp:ListItem>
                                <asp:ListItem Value="2">FEBRUARI</asp:ListItem>
                                <asp:ListItem Value="3">MARET</asp:ListItem>
                                <asp:ListItem Value="4">APRIL</asp:ListItem>
                                <asp:ListItem Value="5">MEI</asp:ListItem>
                                <asp:ListItem Value="6">JUNI</asp:ListItem>
                                <asp:ListItem Value="7">JULI</asp:ListItem>
                                <asp:ListItem Value="8">AGUSTUS</asp:ListItem>
                                <asp:ListItem Value="9">SEPTEMBER</asp:ListItem>
                                <asp:ListItem Value="10">OKTOBER</asp:ListItem>
                                <asp:ListItem Value="11">NOVEMBER</asp:ListItem>
                                <asp:ListItem Value="12">DESEMBER</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div id="panel-atas-2" class="panel">
                <%--                <div class="panel-hdr">
                    

                </div>--%>
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">TAHUN</label>
                            <asp:DropDownList ID="cboTahun" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem>2015</asp:ListItem>
                                <asp:ListItem>2016</asp:ListItem>
                                <asp:ListItem>2017</asp:ListItem>
                                <asp:ListItem>2018</asp:ListItem>
                                <asp:ListItem>2019</asp:ListItem>
                                <asp:ListItem>2020</asp:ListItem>
                                <asp:ListItem>2021</asp:ListItem>
                                <asp:ListItem>2022</asp:ListItem>
                                <asp:ListItem>2023</asp:ListItem>
                                <asp:ListItem>2024</asp:ListItem>
                                <asp:ListItem>2025</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Daftar Stok Barang
            <small>Data Barang Persediaan.</small>
        </h1>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Barang</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <%--<button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>
                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False"
                            DataSourceID="sqlStok" Width="100%" KeyFieldName="KODE_BARANG">
                            <SettingsSearchPanel Visible="True" />
                            <Toolbars>
                                <dx:GridViewToolbar SettingsAdaptivity-Enabled="true">
                                    <Items>
                                        <dx:GridViewToolbarItem Command="ExportToXls">
                                        </dx:GridViewToolbarItem>
                                    </Items>
                                </dx:GridViewToolbar>
                            </Toolbars>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" ReadOnly="True" VisibleIndex="0" Caption="KODE BARANG" Width="15%">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="btnBUKUPERSEDIAAN" runat="server" Text="<%# Eval(“KODE_BARANG”) %>" OnClick="btnBUKUPERSEDIAAN_Click" ToolTip="Klik untuk mencetak buku persediaan"></asp:LinkButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="1" Caption="NAMA BARANG" Width="35%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SATUAN" VisibleIndex="2" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MASUK" ReadOnly="True" VisibleIndex="3" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="KELUAR" ReadOnly="True" VisibleIndex="4" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SALDO" ReadOnly="True" VisibleIndex="5" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BELUMDIPENUHI" ReadOnly="True" VisibleIndex="5" Width="10%">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <asp:SqlDataSource ID="sqlStok" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN],
dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate()) as MASUK,
dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),1,0) as KELUAR,
(dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),1,0)) as SALDO,
(dbo.getJMLPENUHIORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),1)- dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),1,0)) as BELUMDIPENUHI
FROM [TBL_BARANG] WHERE ST_BARANG=1 ORDER BY (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),1,0)) DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
</asp:Content>

