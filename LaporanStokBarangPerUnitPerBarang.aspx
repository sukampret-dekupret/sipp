<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="LaporanStokBarangPerUnitPerBarang.aspx.vb" Inherits="LaporanStokBarangPerUnitPerBarang" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Laporan</li>
        <li class="breadcrumb-item active">Stok Barang Per Unit</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Laporan Stok Barang
            <small>Daftar Stok Barang per Unit</small>
        </h1>
    </div>

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
                            <asp:DropDownList ID="cboBulan" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="cboBulan_TextChanged">
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
                            <asp:DropDownList ID="cboTahun" runat="server" AutoPostBack="True" CssClass="form-control" OnTextChanged="cboTahun_TextChanged">
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


    <%--end of row atas--%>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Stok Barang Per Unit</i></span>
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
                            DataSourceID="sqlStok" Width="100%" KeyFieldName="KODE_BARANG"
                            OnCustomUnboundColumnData="grid_CustomUnboundColumnData" EnableCallBack="false">
                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>

                            <SettingsSearchPanel Visible="True" />
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" ReadOnly="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" ReadOnly="True" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SATUAN" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewBandColumn Caption="SES. PROTKONS" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="MASUK" FieldName="M_SESPROTKONS" ReadOnly="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="AWAL" FieldName="AW_SESPROTKONS" ReadOnly="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="KELUAR" FieldName="K_SESPROTKONS" ReadOnly="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SALDO" FieldName="S_SESPROTKONS" ReadOnly="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="PROTOKOL" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="AWAL" FieldName="AW_PROTOKOL" ReadOnly="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="MASUK" FieldName="M_PROTOKOL" ReadOnly="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="KELUAR" FieldName="K_PROTOKOL" ReadOnly="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SALDO" FieldName="S_PROTOKOL" ReadOnly="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="KONSULER" VisibleIndex="8" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="AWAL" FieldName="AW_KONSULER" ReadOnly="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="MASUK" FieldName="M_KONSULER" ReadOnly="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="KELUAR" FieldName="K_KONSULER" ReadOnly="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SALDO" FieldName="S_KONSULER" ReadOnly="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="FASDIP" VisibleIndex="10" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="AWAL" FieldName="AW_FASDIP" ReadOnly="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="MASUK" FieldName="M_FASDIP" ReadOnly="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="KELUAR" FieldName="K_FASDIP" ReadOnly="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SALDO" FieldName="S_FASDIP" ReadOnly="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>
                                <dx:GridViewBandColumn Caption="PWNI BHI" VisibleIndex="19" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="AWAL" FieldName="AW_PWNIBHI" ReadOnly="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="MASUK" FieldName="M_PWNIBHI" ReadOnly="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="KELUAR" FieldName="K_PWNIBHI" ReadOnly="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="SALDO" FieldName="S_PWNIBHI" ReadOnly="True" VisibleIndex="3">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:GridViewBandColumn>

                            </Columns>
                            <Toolbars>
                                <dx:GridViewToolbar SettingsAdaptivity-Enabled="true">
                                    <Items>
                                        <dx:GridViewToolbarItem Command="ExportToXls">
                                        </dx:GridViewToolbarItem>
                                    </Items>

                                    <SettingsAdaptivity Enabled="True"></SettingsAdaptivity>
                                </dx:GridViewToolbar>
                            </Toolbars>
                            <SettingsExport EnableClientSideExportAPI="true" ExcelExportMode="WYSIWYG" />
                        </dx:ASPxGridView>

                    </div>
                    
                </div>
            </div>
        </div>

    </div>

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>


    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    <asp:SqlDataSource ID="sqlStok" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"

        SelectCommand="SELECT KODE_BARANG,dbo.getNAMABARANG(KODE_BARANG) AS NAMA_BARANG,SATUAN 
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C001',@TGLAWAL,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C001',@TGLAWAL)) AS AW_SESPROTKONS
,dbo.getJMLSERTERORDERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C001',@TGL1,@TGL2,0,1) as M_SESPROTKONS 
,dbo.getJMLSERTERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C001',@TGL1,@TGL2) as K_SESPROTKONS
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C001',@TGL2,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C001',@TGL2)) AS S_SESPROTKONS
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C002',@TGLAWAL,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C002',@TGLAWAL)) AS AW_PROTOKOL
,dbo.getJMLSERTERORDERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C002',@TGL1,@TGL2,0,1) as M_PROTOKOL 
,dbo.getJMLSERTERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C002',@TGL1,@TGL2) as K_PROTOKOL
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C002',@TGL2,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C002',@TGL2)) AS S_PROTOKOL
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C003',@TGLAWAL,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C003',@TGLAWAL)) AS AW_KONSULER
,dbo.getJMLSERTERORDERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C003',@TGL1,@TGL2,0,1) as M_KONSULER 
,dbo.getJMLSERTERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C003',@TGL1,@TGL2) as K_KONSULER
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C003',@TGL2,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C003',@TGL2)) AS S_KONSULER
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C004',@TGLAWAL,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C004',@TGLAWAL)) AS AW_FASDIP
,dbo.getJMLSERTERORDERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C004',@TGL1,@TGL2,0,1) as M_FASDIP 
,dbo.getJMLSERTERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C004',@TGL1,@TGL2) as K_FASDIP
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C004',@TGL2,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C004',@TGL2)) AS S_FASDIP
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C005',@TGLAWAL,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C005',@TGLAWAL)) AS AW_PWNIBHI
,dbo.getJMLSERTERORDERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C005',@TGL1,@TGL2,0,1) as M_PWNIBHI 
,dbo.getJMLSERTERperKDBRGperUNKERperTGLantara(KODE_BARANG,'05A0B010C005',@TGL1,@TGL2) as K_PWNIBHI
,(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,'05A0B010C005',@TGL2,0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,'05A0B010C005',@TGL2)) AS S_PWNIBHI
FROM TBL_BARANG WHERE ST_BARANG=1">
        <SelectParameters>
            <asp:SessionParameter Name="TGLAWAL" SessionField="TGLAWAL" />
            <asp:SessionParameter Name="TGL1" SessionField="TGLAKHIR1" />
            <asp:SessionParameter Name="TGL2" SessionField="TGLAKHIR2" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
</asp:Content>

