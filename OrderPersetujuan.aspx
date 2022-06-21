<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="OrderPersetujuan.aspx.vb" Inherits="OrderPersetujuan" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript">
        function ConfirmVerifikasi() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin akan diverifikasi?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        function ConfirmKirim() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin akan diteruskan ke eselon I?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
        <script type="text/javascript">
        function ConfirmTolak() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin permohonan dari eselon II akan ditolak?")) {
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
        <div class="col-lg-4">
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

            <div class="col-lg-4">
            <div id="panel-atas-3" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">STATUS</label>
                            <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0">Menunggu Perstujuan</asp:ListItem>
                                <asp:ListItem Value="1">Dalam Proses di Pusat</asp:ListItem>
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
                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlOrder"
                            KeyFieldName="KDORDER" Width="100%" PreviewFieldName="PERIHAL">

                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>

                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="gridDetail" runat="server"
                                        DataSourceID="sqlOrderDetail"
                                        OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect"
                                        AutoGenerateColumns="False" KeyFieldName="IDETAILORDER" Width="100%" OnCommandButtonInitialize="gridDetail_CommandButtonInitialize">
                                        <SettingsAdaptivity>
                                            <AdaptiveDetailLayoutProperties ColCount="1">
                                            </AdaptiveDetailLayoutProperties>
                                        </SettingsAdaptivity>
                                        <SettingsEditing Mode="EditForm">
                                            <BatchEditSettings KeepChangesOnCallbacks="False" />
                                        </SettingsEditing>
                                        <SettingsCommandButton>
                                            <EditButton Text="UBAH">
                                            </EditButton>
                                        </SettingsCommandButton>
                                        <SettingsPopup>
                                            <HeaderFilter MinHeight="140px">
                                            </HeaderFilter>
                                        </SettingsPopup>
                                        <SettingsSearchPanel Visible="True" />
                                        <EditFormLayoutProperties ColCount="1">
                                        </EditFormLayoutProperties>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowEditButton="True">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDETAILORDER" ReadOnly="True" VisibleIndex="1" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="2" Caption="KODE BARANG" ReadOnly="true">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="3" Caption="NAMA BARANG" ReadOnly="true">
                                                <EditFormSettings Visible="false" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="PENUHI" VisibleIndex="5" ReadOnly="true">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="STOK" VisibleIndex="6" Caption="STOK ESELON II" ReadOnly="True">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn VisibleIndex="8" Caption="AKSI">
                                                <EditFormSettings Visible="False" />
                                                <DataItemTemplate>
                                                    <%--<asp:Button ID="btnVerifikasi" runat="server" Text="Verifikasi" OnClick="btnVerifikasi_Click" OnClientClick="ConfirmVerifikasi()" />--%><%--<asp:ImageButton ID="btnSuratPengantar" runat="server" ImageUrl="~/img/report1.png" OnClick="btnSuratPengantar_Click" />--%>
                                                    <asp:Button ID="btnCekData" runat="server" Text="Cek History Barang" OnClick="btnCekData_Click" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="MOHON" VisibleIndex="4">
                                                <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000000" MinValue="1">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
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
                                <dx:GridViewDataTextColumn FieldName="NO_NOTA" VisibleIndex="2" Caption="NO NOTA">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_NOTA" VisibleIndex="3" Caption="TGL NOTA">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_PROSES" VisibleIndex="6" Caption="">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn Caption="STATUS" FieldName="STATUS" VisibleIndex="5">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="Menunggu Persetujuan" Value="0" />
                                            <dx:ListEditItem Text="Proses Validasi" Value="1" />
<%--                                            <dx:ListEditItem Text="DITOLAK DI ADMIN" Value="2" />--%>
                                            <dx:ListEditItem Text="Proses Distribusi" Value="3" />
                                           <%-- <dx:ListEditItem Text="DITOLAK DI ES I" Value="4" />--%>
                                            <dx:ListEditItem Text="Selesai" Value="5" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="7">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnKirim" runat="server" Text="Setujui" OnClick="btnKirim_Click" OnClientClick="return ConfirmKirim()" CssClass="btn-success" />
                                        <asp:Button ID="btnLiatNota" runat="server" Text="Lihat Nota" OnClick="btnLiatNota_Click"  CssClass="btn-info"/>
                                        <asp:Button ID="btnTolak" runat="server" Text="Batalkan" OnClick="btnTolak_Click" OnClientClick="ConfirmTolak()" CssClass="btn-danger" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="JENIS" FieldName="JENIS" VisibleIndex="4">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="REGULER" Value="0" />
                                            <dx:ListEditItem Text="NON REGULER" Value="1" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                            </Columns>
                        </dx:ASPxGridView>

                        </div>
                    </div>
                </div>
            </div>
                
        </div>

    <asp:SqlDataSource ID="sqlOrder" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KDORDER], [NO_NOTA], [TGL_NOTA], [TGL_TAMBAH], B.NAMA_UNKER, [PERIHAL],TGL_PROSES,[STATUS],A.JENIS 
FROM [TBL_ORDER] A
INNER JOIN TBL_UNKER B
ON A.DRUNKER=B.KD_UNKER WHERE YEAR(TGL_NOTA)=@TAHUN AND A.DRUNKER=@KDUNKER
AND STATUS=@STATUS">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboStatus" Name="STATUS" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER]"></asp:SqlDataSource>
    <%--SelectCommand="SELECT dbo.GetStatusProses(A.KDORDER) AS STATUSORDER,[IDETAILORDER], B.NAMA_BARANG, [MOHON], [PENUHI], dbo.jmlSTOKORDER(@KDUNKER1,GEtdate(),A.KODE_BARANG,0)-dbo.jmlSTOKMOHON(@KDUNKER1,GETDATE(),A.KODE_BARANG) AS STOK2,dbo.jmlSPK(@KDUNKER,GEtdate(),A.KODE_BARANG)-dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),A.KODE_BARANG,1)  as STOK1,[TGLACC], [KDUNKER], [NAMAACC] FROM [TBL_ORDER_DETAIL] A INNER JOIN TBL_BARANG B ON A.KODE_BARANG=B.KODE_BARANG WHERE A.KDORDER=@KDORDER" --%>
    <asp:SqlDataSource ID="sqlOrderDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT IDETAILORDER,KODE_BARANG,dbo.getnamabarang(KODE_BARANG) as NAMA_BARANG,MOHON,dbo.jmlNilaiPenuhiOrder(KODE_BARANG,KDORDER) as PENUHI,KET,dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),KODE_BARANG,0)-dbo.jmlSTOKMOHON(@KDUNKER,GETDATE(),KODE_BARANG) AS STOK FROM TBL_ORDER_DETAIL WHERE KDORDER=@KDORDER"
        DeleteCommand="DELETE FROM [TBL_ORDER_DETAIL] WHERE [IDETAILORDER] = @IDETAILORDER"
        InsertCommand="INSERT INTO [TBL_ORDER_DETAIL] ([IDETAILORDER], [KODE_BARANG], [MOHON], [PENUHI], [STOK], [TGLACC], [KDUNKER], [NAMAACC]) VALUES (@IDETAILORDER, @KODE_BARANG, @MOHON, @PENUHI, @STOK, @TGLACC, @KDUNKER, @NAMAACC)"
        UpdateCommand="UPDATE [TBL_ORDER_DETAIL] SET  [MOHON] = @MOHON,[PENUHI]=@MOHON WHERE [IDETAILORDER] = @IDETAILORDER">
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
            <asp:Parameter Name="MOHON" Type="Int32" />
            <asp:Parameter Name="IDETAILORDER" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

