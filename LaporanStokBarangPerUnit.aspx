<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="LaporanStokBarangPerUnit.aspx.vb" Inherits="LaporanStokBarangPerUnit" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Laporan</li>
        <li class="breadcrumb-item active">Laporan Stok Unit</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Laporan Stok Barang
            <small>Daftar Stok Barang</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">UNIT KERJA</label>
                            <asp:DropDownList ID="cboUnitKerja" runat="server" CssClass="form-control" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" AutoPostBack="True" DataSourceID="sqlUnker"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <%-- <div class="col-lg-6">
            <div id="panel-atas-2" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">TAHUN</label>
                            TAHUN :
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
        </div>--%>
    </div>

        <div class="row">
        <div class="col-lg-6">
            <div id="panel-atas-21" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">SALDO SAMPAI TANGGAL :</label>
                           <dx:ASPxDateEdit ID="txtTanggalSaldo" runat="server" AutoPostBack="true" CssClass="form-control"></dx:ASPxDateEdit>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6">
            <div id="panel-atas-22" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">SALDO AWAL</label>
                                         <asp:DropDownList ID="cboSaldoAwal" runat="server" AutoPostBack="True" CssClass="form-control">
                                            <asp:ListItem Value="0">TANPA SALDO AWAL</asp:ListItem>
                                            <asp:ListItem Value="1">DENGAN SALDO AWAL</asp:ListItem>
                                        </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--end of row atas--%>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Pembelian</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>

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
                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" ReadOnly="True" VisibleIndex="0" Caption="KODE BARANG" Width="20%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="1" Caption="NAMA BARANG" Width="30%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SATUAN" VisibleIndex="2" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MASUK" ReadOnly="True" VisibleIndex="3" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="KELUAR" ReadOnly="True" VisibleIndex="4" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SALDO" ReadOnly="True" VisibleIndex="5" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="STOKRESERVED" ReadOnly="True" VisibleIndex="6" Width="10%" Caption="BRG BELUM DAPAT DIPENUHI">
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
            SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN],dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,@TGLSALDO,0,@SALDOAWAL) as MASUK,
            dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,@TGLSALDO) as KELUAR,
            (dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,@TGLSALDO,0,@SALDOAWAL)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,@TGLSALDO)) as SALDO,
            (dbo.getJMLPENUHIperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE()) - dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) AS STOKRESERVED FROM [TBL_BARANG] 
            WHERE ST_BARANG=1 
            ORDER BY (dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),0,@SALDOAWAL)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) DESC">
            <SelectParameters>
                <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
               
                <asp:ControlParameter ControlID="txtTanggalSaldo" Name="TGLSALDO" PropertyName="Value" />
                <asp:ControlParameter ControlID="cboSaldoAwal" Name="SALDOAWAL" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sqlUnker" runat="server"
            ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
            SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
</asp:Content>

