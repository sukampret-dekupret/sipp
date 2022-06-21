<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PengadaanPenerimaan.aspx.vb" Inherits="PengadaanPenerimaan" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Pengadaan Pusat</li>
        <li class="breadcrumb-item active">Penerimaan barang</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

            <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Pengadaan Pusat
            <small>Daftar Penerimaan Barang Persediaan.</small>
        </h1>
    </div>

            <div class="row">
        <div class="col-lg-4">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">UNIT KERJA</label>
                            <asp:DropDownList ID="cboUnitKerja" runat="server" CssClass="form-control" 
                                DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" AutoPostBack="True" 
                                DataSourceID="sqlUnker"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
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
                            <asp:Button ID="btnCetakLaporan" runat="server" Text="Cetak Penerimaan" CssClass="form-control alert-dark" />
                        </div>
                    </div>

                </div>
            </div>
        </div>

            <div class="col-lg-4">
            <div id="panel-atas-3" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">STATUS</label>
                            <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="-1">Semua Status</asp:ListItem>
                                <asp:ListItem Value="0">Belum Diterima</asp:ListItem>
                                <asp:ListItem Value="1">Sebagian Diterima</asp:ListItem>
                                <asp:ListItem Value="2">Selesai</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        
                    </div>

                </div>
            </div>
        </div>
        </div>

                <div class="row">
            <div class="col-lg-12">
                <div id="panel-1" class="panel">
                    <div class="panel-hdr">
                        <h2>Daftar  <span class="fw-300"><i>Penerimaan Barang</i></span>
                        </h2>
                        <div class="panel-toolbar">
                            <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                            <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                            <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>

                        </div>
                    </div>

                    <div class="panel-container show">
                        <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server" OnDataBound="grid_DataBound" OnHtmlRowPrepared="grid_HtmlRowPrepared"
                            AutoGenerateColumns="False" DataSourceID="sqlSPK" PreviewFieldName="PERIHAL" EnableCallBacks="false"
                            KeyFieldName="IDSPK" Width="100%">
                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxPageControl ID="pagecontrol" runat="server" Width="100%" EnableCallBacks="true" ActiveTabIndex="0">
                                        <TabPages>
                                            <dx:TabPage Text="Nota Tanda Terima" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <dx:ASPxGridView ID="gridDetailTT" runat="server"
                                                            AutoGenerateColumns="False" DataSourceID="sqlSPKTT"
                                                            KeyFieldName="PKSPKTT" Width="100%"
                                                            OnBeforePerformDataSelect="gridDetailTT_BeforePerformDataSelect" Theme="Default" EnableCallBacks="false">
                                                            <SettingsAdaptivity>
                                                                <AdaptiveDetailLayoutProperties ColCount="1">
                                                                </AdaptiveDetailLayoutProperties>
                                                            </SettingsAdaptivity>
                                                            <EditFormLayoutProperties ColCount="1">
                                                            </EditFormLayoutProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="PKSPKTT" ReadOnly="True"
                                                                    ShowInCustomizationForm="True" VisibleIndex="0" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="NODO" ShowInCustomizationForm="True" VisibleIndex="1" Caption="NO. DELIVERY" ToolTip="Klik di no delivery untuk melihat file nota delivery">
                                                                    <DataItemTemplate>
                                                                        <asp:LinkButton ID="btnNODO" runat="server" Text="<%# Eval(“NODO”) %>" OnClick="btnNODO_Click" ToolTip="Klik disini untuk melihat data penerimaan"></asp:LinkButton>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn FieldName="TGLFAKTUR" ShowInCustomizationForm="True" VisibleIndex="2" Caption="TGL FAKTUR">
                                                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                                                    </PropertiesDateEdit>
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn FieldName="KETERANGAN" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn FieldName="TGL_PROSES" ShowInCustomizationForm="True" VisibleIndex="4" Caption="TGL PROSES">
                                                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                                                    </PropertiesDateEdit>
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="NAMAPROSES" Caption="NAMA">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn VisibleIndex="6" Caption="PILIHAN">
                                                                    <EditFormSettings Visible="False" />
                                                                    <DataItemTemplate>
                                                                        <asp:Button ID="btnCetakNota" runat="server" OnClick="btnCetakNota_Click" Text="Lihat Tanda Terima" />
                                                                        <%--<asp:Button ID="btnCetakNotaPenerimaan" runat="server" OnClick="btnCetakNotaPenerimaan_Click" Text="Lihat Tanda Terima" />--%>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage Text="Data Barang" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <dx:ASPxGridView ID="gridDetail" runat="server"
                                                            DataSourceID="sqlSPKDetail"
                                                            OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect"
                                                            AutoGenerateColumns="False" KeyFieldName="IDSPKDETAIL"
                                                            SettingsEditing-Mode="Inline"
                                                            Width="100%">
                                                            <SettingsAdaptivity>
                                                                <AdaptiveDetailLayoutProperties ColCount="1">
                                                                </AdaptiveDetailLayoutProperties>
                                                            </SettingsAdaptivity>
                                                            <SettingsEditing Mode="Inline">
                                                            </SettingsEditing>
                                                            <SettingsSearchPanel Visible="True" />
                                                            <EditFormLayoutProperties ColCount="1">
                                                            </EditFormLayoutProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="IDSPKDETAIL" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" ShowInCustomizationForm="True" VisibleIndex="1" Caption="KODE BARANG">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="NAMABARANG" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2" Caption="NAMA BARANG">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="JML" ShowInCustomizationForm="True" VisibleIndex="3">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="DIAMBIL" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="4" Caption="SUDAH DITERIMA">
                                                                    <DataItemTemplate>
                                                                        <asp:LinkButton ID="btnDataDiambil" runat="server" Text="<%# Eval(“DIAMBIL”) %>" OnClick="btnDataDiambil_Click" ToolTip="Klik disini untuk melihat history penerimaan"></asp:LinkButton>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="HARGA" ShowInCustomizationForm="True" VisibleIndex="5">
                                                                    <PropertiesSpinEdit DisplayFormatString="Rp. {0:0,0.00}">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="TOTAL" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="6">
                                                                    <PropertiesSpinEdit DisplayFormatString="Rp. {0:0,0.00}">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                        </dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                        </TabPages>

                                    </dx:ASPxPageControl>

                                </DetailRow>
                            </Templates>
                            <Settings ShowPreview="True" />
                            <SettingsSearchPanel Visible="True" />
                            <SettingsDetail ShowDetailRow="true" />

                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="IDSPK" ReadOnly="True"
                                    VisibleIndex="0" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NOFAKTUR" VisibleIndex="1" Caption="NO FAKTUR" Width="15%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_FAKTUR" VisibleIndex="2" Caption="TGL FAKTUR" Width="10%">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="3" FieldName="NAMASUPPLIER" ReadOnly="True" Caption="NAMA SUPPLIER">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="STATUS" VisibleIndex="4" ReadOnly="True">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Belum Diterima" Value="0" />
                                            <dx:ListEditItem Text="Diterima Sebagian" Value="1" />
                                            <dx:ListEditItem Text="Selesai" Value="2" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="5" Width="30%">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnTT" runat="server" Text="Terima Barang" OnClick="btnTT_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>

                        </div>
                    </div>
                </div>
            </div>
                    
        </div>
        <asp:SqlDataSource ID="sqlSPK" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [IDSPK], [NOFAKTUR], [TGL_FAKTUR], [TGL], concat('Perihal: ',[PERIHAL]) as PERIHAL, 
        dbo.GetNAMAMITRA([KODE_MITRA]) AS NAMASUPPLIER, dbo.GetStatusSPK(IDSPK) as STATUS FROM [TBL_SPK]
WHERE KDUNKER=@KDUNKER AND dbo.GetStatusSPK(IDSPK)=@STATUS OR @STATUS=-1 ">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboStatus" Name="STATUS" PropertyName="SelectedValue" DefaultValue="0" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlSPKTT" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [PKSPKTT], [NODO], [TGLFAKTUR], [KETERANGAN], [TGL_PROSES], [NAMAPROSES] FROM [TBL_SPK_TT] WHERE IDSPK=@IDSPK">
        <SelectParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlSPKDetail" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [IDSPKDETAIL], [KODE_BARANG],dbo.GetNAMABARANG(KODE_BARANG) AS NAMABARANG, [JML],dbo.getJMLSERTERTperKDBRGperSPK(KODE_BARANG,IDSPK) AS DIAMBIL, [HARGA], (JML*HARGA) as TOTAL  FROM [TBL_SPK_DETAIL] WHERE IDSPK=@IDSPK">
        <SelectParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
</asp:Content>

