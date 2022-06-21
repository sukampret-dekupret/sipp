<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="OrderDistribusi.aspx.vb" Inherits="OrderDistribusi" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Distribusi Barang</li>
        <li class="breadcrumb-item active">Pembuatan Tanda Terima</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Distribusi Barang
            <small>Pembuatan Tanda Terima.</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-4">
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
        <div class="col-lg-4">
            <div id="panel-atas-2" class="panel">
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

        <div class="col-lg-4">
            <div id="panel-atas-3" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">STATUS</label>
                            <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" CssClass="form-control">
<%--                                <asp:ListItem Value="0">Menunggu Perstujuan</asp:ListItem>
                                <asp:ListItem Value="1">Dalam Proses di Pusat</asp:ListItem>--%>
                                <%--<asp:ListItem Value="2">Ditolak Di ADMIN</asp:ListItem>--%>
                                <asp:ListItem Value="3">Sedang Disiapkan Gudang Pusat</asp:ListItem>
                                <asp:ListItem Value="5">Selesai</asp:ListItem>
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
                    <h2>Daftar  <span class="fw-300"><i>Distribusi Barang</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                       <%-- <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>

                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlOrder"
                            PreviewFieldName="PERIHAL" OnDataBound="grid_DataBound"
                            KeyFieldName="KDORDER" Width="100%" EnableCallBacks="false">
                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxPageControl ID="pagecontrol" runat="server" Width="100%"
                                        EnableCallBacks="false">
                                        <TabPages>
                                            <dx:TabPage Text="Nota Tanda Terima" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <dx:ASPxGridView ID="gridOrderTT" runat="server"
                                                            AutoGenerateColumns="False" DataSourceID="sqlTTOrder"
                                                            KeyFieldName="PKORDERTT" Width="100%"
                                                            OnBeforePerformDataSelect="gridOrderTT_BeforePerformDataSelect" EnableCallBacks="false">
                                                            <SettingsAdaptivity>
                                                                <AdaptiveDetailLayoutProperties ColCount="1">
                                                                </AdaptiveDetailLayoutProperties>
                                                            </SettingsAdaptivity>
                                                            <EditFormLayoutProperties ColCount="1">
                                                            </EditFormLayoutProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="PKTTMOHON" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="NO_TT" ShowInCustomizationForm="True" VisibleIndex="1" Caption="NO TANDA TERIMA">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn FieldName="TGL_CETAK" ShowInCustomizationForm="True" VisibleIndex="2" Caption="TGL CETAK">
                                                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                                                    </PropertiesDateEdit>
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn FieldName="USERCETAK" ShowInCustomizationForm="True" VisibleIndex="3" Caption="OPERATOR">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn VisibleIndex="4" Caption="PILIHAN">
                                                                    <EditFormSettings Visible="False" />
                                                                    <DataItemTemplate>
                                                                        <asp:Button ID="btnCetakNota" runat="server" OnClick="btnCetakNota_Click"
                                                                            Text="Cetak Tanda Terima" />
                                                                        <asp:Button ID="btnUpload" runat="server" Text="Upload Tanda Terima" OnClick="btnUpload_Click" />
                                                                         <asp:Button ID="btnDownloadTT" runat="server" Text="Downlod TT" OnClick="btnDownloadTT_Click" />
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
                                                            DataSourceID="sqlOrderDetail"
                                                            OnBeforePerformDataSelect="ASPxGridView1_BeforePerformDataSelect"
                                                            AutoGenerateColumns="False" KeyFieldName="IDETAILORDER"
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
                                                                <dx:GridViewCommandColumn VisibleIndex="0">
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn FieldName="IDETAILORDER" ReadOnly="True" VisibleIndex="1" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="2" Caption="KODE BARANG">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="3" Caption="NAMA BARANG">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="PENUHI" VisibleIndex="4">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="STOK1" ReadOnly="True" VisibleIndex="6" Caption="STOK UNIT/SATKER">
                                                                    <EditFormSettings Visible="False" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="7">
                                                                    <EditFormSettings Visible="False" />
                                                                    <DataItemTemplate>
                                                                    </DataItemTemplate>
                                                                </dx:GridViewDataTextColumn>
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

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                            <SettingsSearchPanel Visible="True" />
                            <SettingsDetail ShowDetailRow="true" />

                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="KDORDER" ReadOnly="True"
                                    VisibleIndex="1" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NO_NOTA" VisibleIndex="2" Caption="NO NOTA" Width="15%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_NOTA" VisibleIndex="3" Caption="TGL NOTA" Width="10%">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_UNKER" VisibleIndex="4" Caption="NAMA UNIT KERJA" Width="35%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="STATUS" VisibleIndex="5" Caption="STATUS" Width="10%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Sedang Proses" Value="3" />
                                            <dx:ListEditItem Text="Selesai" Value="5" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                                                <dx:GridViewDataDateColumn FieldName="TGL_TAMBAH" VisibleIndex="7" Caption="TAMBAH" Width="10%" Visible="false">
                                                                    <EditFormSettings Visible="False" />
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="6" Width="30%">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnTT" runat="server" Text="Buat Tanda Terima" OnClick="btnTT_Click" />
                                        <asp:Button ID="btnRekap" runat="server" Text="Rekap Permintaan" OnClick="btnRekap_Click" />
                                       

                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>

                    </div>
                </div>
            </div>
        </div>

    </div>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <asp:SqlDataSource ID="sqlOrder" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KDORDER], [NO_NOTA], [TGL_NOTA], [TGL_TAMBAH], dbo.getNAMAUNKER(DRUNKER) as NAMA_UNKER, [PERIHAL],dbo.getSTATUSORDER(KDORDER) as STATUS
FROM [TBL_ORDER] WHERE YEAR(TGL_NOTA)=@TAHUN AND KEUNKER=@KEUNKER AND dbo.getSTATUSORDER(KDORDER)=@STATUS">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboStatus" Name="STATUS" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KEUNKER" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="sqlOrderDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [IDETAILORDER],A.KODE_BARANG, B.NAMA_BARANG, [MOHON], [PENUHI], (dbo.jmlSTOKS2(A.KDUNKER,getdate(),A.KODE_BARANG,0,1)-dbo.jmlSTOKMOHON(A.KDUNKER,GETDATE(),A.KODE_BARANG)) AS STOK2,dbo.jmlSPK(@KDUNKER,GEtdate(),A.KODE_BARANG)-dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),A.KODE_BARANG,1)  as STOK1,[KDUNKER]  FROM [TBL_ORDER_DETAIL] A INNER JOIN TBL_BARANG B ON A.KODE_BARANG=B.KODE_BARANG WHERE A.KDORDER=@KDORDER" DeleteCommand="DELETE FROM [TBL_ORDER_DETAIL] WHERE [IDETAILORDER] = @IDETAILORDER" InsertCommand="INSERT INTO [TBL_ORDER_DETAIL] ([IDETAILORDER], [KODE_BARANG], [MOHON], [PENUHI], [STOK], [TGLACC], [KDUNKER], [NAMAACC]) VALUES (@IDETAILORDER, @KODE_BARANG, @MOHON, @PENUHI, @STOK, @TGLACC, @KDUNKER, @NAMAACC)" UpdateCommand="UPDATE [TBL_ORDER_DETAIL] SET  [PENUHI] = @PENUHI WHERE [IDETAILORDER] = @IDETAILORDER">
        <DeleteParameters>
            <asp:Parameter Name="IDETAILORDER" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="IDETAILORDER" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="MOHON" Type="Int32" />
            <asp:Parameter Name="PENUHI" Type="Int32" />
            <asp:Parameter Name="STOK" Type="Int32" />
            <asp:Parameter Name="TGLACC" Type="DateTime" />
            <asp:Parameter Name="KDUNKER" Type="String" />
            <asp:Parameter Name="NAMAACC" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
            <asp:SessionParameter Name="KDORDER" SessionField="KDORDER" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="PENUHI" Type="Int32" />
            <asp:Parameter Name="IDETAILORDER" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlTTOrder" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [PKORDERTT], [NO_TT], [TGL_CETAK], [USERCETAK] FROM [TBL_ORDER_TT] WHERE KDORDER=@KDORDER">
        <SelectParameters>
            <asp:SessionParameter Name="KDORDER" SessionField="KDORDER" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="dataflag" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [IDFLAG], [KETERANGAN] FROM [TBL_R_FLAG]"></asp:SqlDataSource>
</asp:Content>

