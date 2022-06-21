<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Permohonan.aspx.vb" Inherits="Permohonan" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnEndCallback(s, e) {
            if ((command == "ADDNEWROW" || command == "UPDATEEDIT") && !s.isError) {
                grdMasterPermohonan.Refresh();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Permohonan</li>
        <li class="breadcrumb-item active">Input Permohonan</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Permohonan Barang
            <small>Daftar Permohonan Barang Persediaan.</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-6">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">UNIT KERJA</label>
                            <asp:DropDownList ID="cboUnitKerja" runat="server" CssClass="form-control" DataTextField="NAMA_UNKER" 
                                DataValueField="KD_UNKER" AutoPostBack="True" DataSourceID="sqlUnker"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-6">
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
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Pembelian</i></span>
                    </h2>
                    <div class="panel-toolbar">
 <%--                       <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>

                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server"
                            ClientInstanceName="grdMasterPermohonan"
                            AutoGenerateColumns="False" DataSourceID="sqlPermohonan"
                            KeyFieldName="IDPERMOHONAN" Width="100%"
                            OnCommandButtonInitialize="grid_CommandButtonInitialize" OnHtmlRowPrepared="grid_HtmlRowPrepared">

                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>

                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="gridDetail" runat="server"
                                        AutoGenerateColumns="False" DataSourceID="sqlMohonDetail"
                                        KeyFieldName="IDDETAILPERMOHONAN" Width="100%"
                                        OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect"
                                        OnCommandButtonInitialize="gridDetail_CommandButtonInitialize" OnInitNewRow="gridDetail_InitNewRow" OnRowInserting="gridDetail_RowInserting">
                                        <SettingsAdaptivity>
                                            <AdaptiveDetailLayoutProperties ColCount="1">
                                            </AdaptiveDetailLayoutProperties>
                                        </SettingsAdaptivity>
                                        <SettingsEditing Mode="Inline">
                                        </SettingsEditing>

                                        <SettingsPopup>
                                            <HeaderFilter MinHeight="140px">
                                            </HeaderFilter>
                                        </SettingsPopup>

                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsText EmptyDataRow="Klik tombol tambah untuk menambahkan" />
                                        <SettingsText ConfirmDelete="Apakah anda yakin barang ini akan dihapus?" />
                                        <EditFormLayoutProperties ColCount="1">
                                        </EditFormLayoutProperties>
                                        <ClientSideEvents EndCallback="OnEndCallback" BeginCallback="OnBeginCallback"/>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDDETAILPERMOHONAN" ReadOnly="True" VisibleIndex="1" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="KODE_BARANG" VisibleIndex="2" Width="70%" Caption="NAMA BARANG">
                                                <PropertiesComboBox DataSourceID="sqlMasterBarang" TextField="NAMA_BARANG" ValueField="KODE_BARANG">
                                                </PropertiesComboBox>
                                                <EditFormSettings RowSpan="2" />
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="JML_MOHON" Caption="JML PERMOHONAN"
                                                VisibleIndex="3" Width="15%">
                                                <PropertiesSpinEdit DisplayFormatString="g" MinValue="1" MaxValue="10000000">
                                                </PropertiesSpinEdit>
                                                <EditFormSettings RowSpan="2" />
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="JML_PENUHI" Caption="JML DIPENUHI" ReadOnly="true"
                                                VisibleIndex="4" Width="15%">
                                                <PropertiesSpinEdit DisplayFormatString="g">
                                                </PropertiesSpinEdit>
                                                <EditFormSettings Visible="False" />
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataSpinEditColumn>

                                        </Columns>
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsCommandButton>
                                <DeleteButton Text="Batalkan">
                                </DeleteButton>
                            </SettingsCommandButton>

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                            <SettingsSearchPanel Visible="True" />
                            <SettingsText ConfirmDelete="Apakah anda yakin permohonan akan dibatalkan?" />

                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="NO_PERMOHONAN" VisibleIndex="1" Width="15%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_PERMOHONAN" VisibleIndex="2" Width="10%">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="PERIHAL" VisibleIndex="3" Width="55%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_PROSES" VisibleIndex="5" Width="10%" Caption="TANGGAL">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="IDPERMOHONAN" ReadOnly="True" VisibleIndex="6" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AKSI" ReadOnly="True" VisibleIndex="7">
                                    <DataItemTemplate>
                                        <asp:Button ID="btnCetak" runat="server" Text="Cetak" OnClick="btnCetak_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="STS_PERMOHONAN" VisibleIndex="4" Caption="STATUS" Width="10%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="MENUNGGU PERSETUJUAN" Value="0" />
                                            <dx:ListEditItem Text="DISETUJUI" Value="1" />
                                            <dx:ListEditItem Text="DITOLAK" Value="2" />
                                            <dx:ListEditItem Text="PENGADAAN" Value="3" />
                                            <dx:ListEditItem Text="SELESAI" Value="4" />

                                        </Items>

                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>

                            </Columns>
                            <SettingsDetail ShowDetailRow="true" />
                        </dx:ASPxGridView>

                    </div>
                </div>
            </div>
        </div>

    </div>


    <asp:SqlDataSource ID="sqlPermohonan" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [NO_PERMOHONAN], [TGL_PERMOHONAN], [PERIHAL], [STS_PERMOHONAN], [TGL_PROSES], [IDPERMOHONAN] FROM
        [TBL_PERMOHONAN] WHERE NAMAUSER=@NAMAUSER AND YEAR(TGL_PERMOHONAN)=@TAHUN ORDER BY TGL_PERMOHONAN DESC"
        DeleteCommand="DELETE FROM TBL_PERMOHONAN WHERE IDPERMOHONAN=@IDPERMOHONAN">
        <DeleteParameters>
            <asp:Parameter Name="IDPERMOHONAN" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" />
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlMohonDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PERMOHONAN_DETAIL] WHERE [IDDETAILPERMOHONAN] = @IDDETAILPERMOHONAN"
        InsertCommand="INSERT INTO [TBL_PERMOHONAN_DETAIL] ( [KODE_BARANG], [JML_MOHON], [IDPERMOHONAN], [KDUNKER]) VALUES ( @KODE_BARANG, @JML_MOHON, @IDPERMOHONAN, @KDUNKER)"
        SelectCommand="SELECT [IDDETAILPERMOHONAN], [KODE_BARANG], [JML_MOHON], [IDPERMOHONAN], [KDUNKER], dbo.jmlNilaiPenuhi(KODE_BARANG,IDPERMOHONAN) AS JML_PENUHI FROM [TBL_PERMOHONAN_DETAIL] WHERE IDPERMOHONAN=@IDPERMOHONAN"
        UpdateCommand="UPDATE [TBL_PERMOHONAN_DETAIL] SET [KODE_BARANG] = @KODE_BARANG, [JML_MOHON] = @JML_MOHON,JML_PENUHI=@JML_MOHON WHERE [IDDETAILPERMOHONAN] = @IDDETAILPERMOHONAN">
        <DeleteParameters>
            <asp:Parameter Name="IDDETAILPERMOHONAN" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML_MOHON" Type="Int32" />
            <asp:SessionParameter Name="IDPERMOHONAN" SessionField="IDPERMOHONAN" Type="String" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDPERMOHONAN" SessionField="IDPERMOHONAN" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML_MOHON" Type="Int32" />
            <asp:Parameter Name="IDPERMOHONAN" Type="String" />
            <asp:Parameter Name="KDUNKER" Type="String" />
            <asp:Parameter Name="IDDETAILPERMOHONAN" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <%-- SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN] FROM [TBL_BARANG] WHERE (dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE()))>0 ORDER BY NAMA_BARANG ASC  --%>

    <asp:SqlDataSource ID="sqlMasterBarang" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand=" SELECT KODE_BARANG, NAMA_BARANG, SATUAN, (dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) as STOK FROM [TBL_BARANG]  ORDER BY (dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) DESC">
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_WILAYAH='00' AND KD_STS_AKTIF=1"></asp:SqlDataSource>
</asp:Content>

