<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PermohonanDistribusi.aspx.vb" Inherits="PermohonanDistribusi" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript">
        function ConfirmOK() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin barang sudah sesuai?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function ConfirmSelesai() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin semua barang sudah diterima semua?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Permohonan</li>
        <li class="breadcrumb-item active">Proses Permohonan (Admin Unit) </li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Daftar Permohonan Barang
            <small>Daftar Permohonan Barang Persediaan.</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-8">
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
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <%--<div class="col-lg-4">
            <div id="panel-atas-3" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">STATUS</label>
                            <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="1">DISETUJUI</asp:ListItem>
                                <asp:ListItem Value="3">SELESAI/SUDAH DIDISTRIBUSIKAN</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                </div>
            </div>
        </div>--%>
    </div>

        <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Daftar Permohonan</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>

                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                       <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" OnHtmlRowPrepared="grid_HtmlRowPrepared"
                            PreviewFieldName="PERIHAL"
                            DataSourceID="sqlPermohonan" KeyFieldName="IDPERMOHONAN" OnDataBound="grid_DataBound"
                            Width="100%">

                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>

                            <Templates>
                                <DetailRow>
                                    <dx:ASPxPageControl ID="pagecontrol" runat="server" Width="100%" EnableCallBacks="true">
                                        <TabPages>
                                            <dx:TabPage Text="Nota Tanda Terima" Visible="true">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <dx:ASPxGridView ID="gridMohonTT" runat="server"
                                                            AutoGenerateColumns="False" DataSourceID="sqlTTMohon"
                                                            KeyFieldName="PKTTMOHON" Width="100%"
                                                            OnBeforePerformDataSelect="gridMohonTT_BeforePerformDataSelect">
                                                            <SettingsAdaptivity>
                                                                <AdaptiveDetailLayoutProperties ColCount="1">
                                                                </AdaptiveDetailLayoutProperties>
                                                            </SettingsAdaptivity>
                                                            <EditFormLayoutProperties ColCount="1">
                                                            </EditFormLayoutProperties>
                                                            <Columns>
                                                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" VisibleIndex="0">
                                                                </dx:GridViewCommandColumn>
                                                                <dx:GridViewDataTextColumn FieldName="PKTTMOHON" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1" Visible="false">
                                                                    <EditFormSettings Visible="False" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="NO_TT" ShowInCustomizationForm="True" VisibleIndex="2" Caption="NO TANDA TERIMA">
                                                                    <EditFormSettings Visible="False" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataDateColumn FieldName="TGL_CETAK" ShowInCustomizationForm="True" VisibleIndex="3" Caption="TGL CETAK">
                                                                    <EditFormSettings Visible="False" />
                                                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                                                    </PropertiesDateEdit>
                                                                </dx:GridViewDataDateColumn>
                                                                <dx:GridViewDataTextColumn FieldName="USERCETAK" ShowInCustomizationForm="True" VisibleIndex="4" Caption="OPERATOR">
                                                                    <EditFormSettings Visible="False" />
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn VisibleIndex="5" Caption="PILIHAN">
                                                                    <DataItemTemplate>
                                                                        <asp:Button ID="btnCetakNota" runat="server" OnClick="btnCetakNota_Click" Text="Cetak Tanda Terima" />
                                                                        <asp:Button ID="btnUploadTT" runat="server" OnClick="btnUploadTT_Click" Text="Upload Tanda Terima" />
                                                                        <asp:Button ID="btnCekTT" runat="server" OnClick="btnCekTT_Click" Text="Download Tanda Terima" />

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
                                                            AutoGenerateColumns="False" DataSourceID="sqlPerDetail"
                                                            KeyFieldName="IDDETAILPERMOHONAN" Width="100%"
                                                            OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect"
                                                            SettingsEditing-Mode="Batch">
                                                            <SettingsDataSecurity AllowDelete="False"
                                                                AllowInsert="False" AllowEdit="False" />
                                                            <SettingsSearchPanel Visible="True" />
                                                            <EditFormLayoutProperties ColCount="1">
                                                            </EditFormLayoutProperties>
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="IDDETAILPERMOHONAN" ReadOnly="True" VisibleIndex="0" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataTextColumn FieldName="IDPERMOHONAN" VisibleIndex="4" Visible="false">
                                                                </dx:GridViewDataTextColumn>
                                                                <dx:GridViewDataComboBoxColumn FieldName="KODE_BARANG" VisibleIndex="1" Caption="NAMA BARANG">
                                                                    <PropertiesComboBox DataSourceID="sqlMasterBarang" TextField="NAMA_BARANG" ValueField="KODE_BARANG">
                                                                    </PropertiesComboBox>
                                                                </dx:GridViewDataComboBoxColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="JML_MOHON" VisibleIndex="2" Caption="JUMLAH YG DIMOHON">
                                                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000" MinValue="1" NullText="0">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="JML_PENUHI" VisibleIndex="3" Caption="JUMLAH YANG DISETUJUI">
                                                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000" MinValue="1" NullText="0">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                                <dx:GridViewDataSpinEditColumn FieldName="SUDAH" VisibleIndex="5" Caption="JUMLAH YANG SUDAH DITERIMA">
                                                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000" MinValue="1" NullText="0">
                                                                    </PropertiesSpinEdit>
                                                                </dx:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                            <SettingsAdaptivity>
                                                                <AdaptiveDetailLayoutProperties ColCount="1">
                                                                </AdaptiveDetailLayoutProperties>
                                                            </SettingsAdaptivity>
                                                            <SettingsEditing Mode="Batch" />
                                                        </dx:ASPxGridView>
                                                    </dx:ContentControl>
                                                </ContentCollection>

                                            </dx:TabPage>
                                        </TabPages>


                                    </dx:ASPxPageControl>

                                </DetailRow>
                            </Templates>
                            <SettingsDetail ShowDetailRow="true" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                            <SettingsSearchPanel Visible="True" />
                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="IDPERMOHONAN" ReadOnly="True" VisibleIndex="1" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NO_PERMOHONAN" VisibleIndex="2" Width="10%" Caption="NO PERMOHONAN">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_PERMOHONAN" VisibleIndex="3" Caption="TGL MOHON" Width="5%">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMAUSER" VisibleIndex="4" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="DRUNKER" VisibleIndex="5" Caption="UNIT KERJA" Width="10%">
                                    <PropertiesComboBox DataSourceID="sqlUnker" TextField="NAMA_UNKER" ValueField="KD_UNKER">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="STS_PERMOHONAN" VisibleIndex="6" Caption="STATUS" Width="5%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="DISETUJUI" Value="1" />
                                            <dx:ListEditItem Text="SELESAI" Value="3" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn Caption="KETERANGAN" FieldName="KETLEBIH" VisibleIndex="7" Width="10%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="STOK ADA" Value="0" />
                                            <dx:ListEditItem Text="STOK KURANG" Value="1" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="8" Caption="PILIHAN" Width="15%">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnCetakTT" runat="server" Text="Proses Pengeluaran Barang" OnClick="btnCetakTT_Click" />
                                        <%--<asp:Button ID="btnSelesai" runat="server" Text="Order Selesai" OnClick="btnSelesai_Click" OnClientClick="ConfirmSelesai()" />--%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowPreview="True" />
                        </dx:ASPxGridView>

                    </div>
                </div>
            </div>
        </div>
            
    </div>

        <asp:SqlDataSource ID="sqlPerDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PERMOHONAN_DETAIL] WHERE [IDDETAILPERMOHONAN] = @IDDETAILPERMOHONAN"
        InsertCommand="INSERT INTO [TBL_PERMOHONAN_DETAIL] ([IDDETAILPERMOHONAN], [KODE_BARANG], [JML_MOHON], [JML_PENUHI]) VALUES (@IDDETAILPERMOHONAN, @KODE_BARANG, @JML_MOHON, @JML_PENUHI)"
        SelectCommand="SELECT [IDDETAILPERMOHONAN], KET,[KODE_BARANG], [JML_MOHON], [JML_PENUHI],dbo.getJMLMOHONperTT(KODE_BARANG, IDPERMOHONAN) AS SUDAH FROM [TBL_PERMOHONAN_DETAIL] WHERE ([IDPERMOHONAN] = @IDPERMOHONAN)"
        UpdateCommand="UPDATE [TBL_PERMOHONAN_DETAIL] SET  [JML_PENUHI] = @JML_PENUHI WHERE [IDDETAILPERMOHONAN] = @IDDETAILPERMOHONAN">
        <DeleteParameters>
            <asp:Parameter Name="IDDETAILPERMOHONAN" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="IDDETAILPERMOHONAN" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML_MOHON" Type="Int32" />
            <asp:Parameter Name="JML_PENUHI" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDPERMOHONAN" SessionField="IDPERMOHONAN" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="JML_PENUHI" Type="Int32" />
            <asp:Parameter Name="IDDETAILPERMOHONAN" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_WILAYAH='00' AND KD_STS_AKTIF=1"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlMasterBarang" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG] FROM [TBL_BARANG] WHERE ST_BARANG=1"></asp:SqlDataSource>

    <br />

    <asp:SqlDataSource
        ID="sqlPermohonan"
        runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [NO_PERMOHONAN], [TGL_PERMOHONAN], CONCAT('Perihal : ',[PERIHAL]) as PERIHAL, dbo.getSTATUSPERMOHONAN(IDPERMOHONAN) AS STS_PERMOHONAN, [NAMAUSER],[DRUNKER], [IDPERMOHONAN],dbo.getKetLebihSTOK(@DRUNKER,IDPERMOHONAN) as KETLEBIH FROM [TBL_PERMOHONAN] WHERE DRUNKER=@DRUNKER AND YEAR(TGL_PERMOHONAN)=@TAHUN AND dbo.getSTATUSPERMOHONAN(IDPERMOHONAN)=1"
        UpdateCommand="UPDATE [TBL_PERMOHONAN] SET [STS_PERMOHONAN] = @STS_PERMOHONAN, [IDPEMROSES] = @IDPEMROSES,TGL_PROSES=@TGL_PROSES WHERE [IDPERMOHONAN] = @IDPERMOHONAN">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboUnitKerja" Name="DRUNKER" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
            <%--<asp:ControlParameter ControlID="cboStatus" Name="STS_PERMOHONAN" PropertyName="SelectedValue" />--%>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="STS_PERMOHONAN" Type="Int16" />
            <asp:Parameter Name="IDPEMROSES" Type="String" />
            <asp:Parameter Name="TGL_PROSES" Type="Datetime" />
            <asp:Parameter Name="IDPERMOHONAN" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlTTMohon" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [PKTTMOHON], [NO_TT], [TGL_CETAK], [USERCETAK] FROM [TBL_PERMOHONAN_TT] WHERE IDPERMOHONAN=@IDPERMOHONAN">
        <SelectParameters>
            <asp:SessionParameter Name="IDPERMOHONAN" SessionField="IDPERMOHONAN" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

