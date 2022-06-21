<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="DaftarPermohonanSatker.aspx.vb" Inherits="admin_DaftarPermohonanSatker" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.11.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ConfirmProses() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin akan diproses?")) {
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
            if (confirm("Apakah anda yakin akan ditolak?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function ConfirmBatalkan() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin diproses ulang?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <script type="text/javascript">
        function ConfirmBelanja() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah permintaan akan dilakukan pengadaaan?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        var command = "";
        function OnBeginCallback(s, e) {
            command = e.command;
        }

        function OnEndCallback(s, e) {
            if (command == "UPDATEEDIT") {
                masterGrid.Refresh();

            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageWrapper" runat="Server">

    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Permohonan Satker</h2>
            <ol class="breadcrumb">
                <li class="breadcrumb-item">
                    <a href="Home.aspx">Home</a>
                </li>
                <li class="breadcrumb-item active">
                    <a>Daftar Permohonan</a>
                </li>
            </ol>
        </div>
        <div class="col-lg-2">
        </div>
    </div>

    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="row">
            <div class="col-lg-3">
                <div class="widget style1">
                    <div class="row">
                        <div class="col-4 text-center">
                            <i class="fa fa-sort fa-5x"></i>
                        </div>
                        <div class="col-8 text-right">
                            <span>Belum Diproses </span>
                            <h2 class="font-bold"><%=xPROSES%></h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="widget style1 yellow-bg">
                    <div class="row">
                        <div class="col-4">
                            <i class="fa fa-spinner fa-5x"></i>
                        </div>
                        <div class="col-8 text-right">
                            <span>Penyiapan Barang </span>
                            <h2 class="font-bold"><%=xSIAP%></h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="widget style1 lazur-bg">
                    <div class="row">
                        <div class="col-4">
                            <i class="fa fa-star-half-o fa-5x"></i>
                        </div>
                        <div class="col-8 text-right">
                            <span>Menunggu Konfirmasi</span>
                            <h2 class="font-bold"><%=xKONFIRMASI%></h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="widget style1 navy-bg">
                    <div class="row">
                        <div class="col-4">
                            <i class="fa fa-star fa-5x"></i>
                        </div>
                        <div class="col-8 text-right">
                            <span>Selesai </span>
                            <h2 class="font-bold"><%=xSELESAI%></h2>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="ibox">
                    <div class="ibox-title">
                        <h5 class="card-title"></h5>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
                                UNIT KERJA :
                            <asp:DropDownList ID="cboUnitKerja" runat="server" AutoPostBack="True"
                                DataSourceID="sqlUnker" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" CssClass="form-control">
                            </asp:DropDownList>

                            </div>
                            <div class="col-lg-3">
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
                            <%--<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>--%>
                            <div class="col-lg-3">
                                STATUS :
                            <asp:DropDownList ID="cboStatus" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="1">Belum diproses</asp:ListItem>
                                <asp:ListItem Value="3">Proses Penyiapan Barang</asp:ListItem>
                                <asp:ListItem Value="4">Menunggu Konfirmasi Penerimaan</asp:ListItem>
                                <asp:ListItem Value="5">Selesai</asp:ListItem>
                            </asp:DropDownList>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="ibox-content">
                    <dx:ASPxGridView ID="grid" runat="server" ClientInstanceName="masterGrid"
                        OnCustomCallback="grid_CustomCallback" OnDataBound="grid_DataBound"
                        AutoGenerateColumns="False" DataSourceID="sqlOrder" PreviewFieldName="PERIHAL"
                        KeyFieldName="KDORDER" Width="100%">

                        <SettingsAdaptivity>
                            <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                        </SettingsAdaptivity>

                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="gridDetail" runat="server"
                                    DataSourceID="sqlOrderDetail"
                                    OnBeforePerformDataSelect="ASPxGridView1_BeforePerformDataSelect"
                                    AutoGenerateColumns="False" KeyFieldName="IDETAILORDER"
                                    SettingsEditing-Mode="Inline"
                                    OnRowUpdating="gridDetail_RowUpdating"
                                    OnHtmlRowPrepared="gridDetail_HtmlRowPrepared"
                                    OnCellEditorInitialize="gridDetail_CellEditorInitialize"
                                    OnCommandButtonInitialize="gridDetail_CommandButtonInitialize"
                                    Width="100%" OnDataBound="gridDetail_DataBound"
                                    OnRowValidating="gridDetail_RowValidating">
                                    <ClientSideEvents EndCallback="OnEndCallback" BeginCallback="OnBeginCallback" />
                                    <SettingsAdaptivity>
                                        <AdaptiveDetailLayoutProperties ColCount="1">
                                        </AdaptiveDetailLayoutProperties>
                                    </SettingsAdaptivity>
                                    <SettingsEditing Mode="Inline">
                                    </SettingsEditing>
                                    <SettingsCommandButton>
                                        <EditButton Text="Ubah">
                                        </EditButton>
                                    </SettingsCommandButton>
                                    <SettingsSearchPanel Visible="True" />
                                    <EditFormLayoutProperties ColCount="1">
                                    </EditFormLayoutProperties>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowEditButton="True" VisibleIndex="0">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataTextColumn FieldName="IDETAILORDER" ReadOnly="True" VisibleIndex="1" Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="2" Caption="KODE BARANG" ReadOnly="true" CellStyle-HorizontalAlign="Center">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="3" Caption="NAMA BARANG" ReadOnly="true">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="MOHON" VisibleIndex="4" ReadOnly="true" Caption="PERMINTAAN">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataSpinEditColumn FieldName="PENUHI" VisibleIndex="5">
                                            <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000000" MinValue="0" AllowNull="false">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn FieldName="PENUHISBLMNYA" ReadOnly="True" VisibleIndex="6" Caption="PENUHI SBLMNYA">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="STOK2" ReadOnly="True" VisibleIndex="7" Caption="STOK SATKER">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="STOK1" ReadOnly="True" VisibleIndex="8" Caption="STOK ADMIN">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="STOKBELUMDIAMBIL" ReadOnly="True" VisibleIndex="9" Caption="STOK RESERVED">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataTextColumn>

                                        <%--                                            <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="7">
                                                <EditFormSettings Visible="False" />
                                                <DataItemTemplate>

                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>--%>
                                    </Columns>
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <Settings ShowPreview="True" />
                        <SettingsSearchPanel Visible="True" />
                        <SettingsDetail ShowDetailRow="true" />

                        <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                        <Columns>
                            <dx:GridViewCommandColumn VisibleIndex="0" Visible="false">
                            </dx:GridViewCommandColumn>
                            <dx:GridViewDataTextColumn FieldName="KDORDER" ReadOnly="true"
                                VisibleIndex="1" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="NO_NOTA" VisibleIndex="2" Caption="NO NOTA" Width="15%" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="TGL_NOTA" VisibleIndex="3" Caption="TGL NOTA" Width="10%" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn FieldName="NAMA_UNKER" VisibleIndex="4" Caption="NAMA UNIT KERJA" Width="50%">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Pilihan" VisibleIndex="5" Visible="false" Width="15%">
                                <EditFormSettings Visible="False" />
                                <DataItemTemplate>
                                    <asp:Button ID="btnProses" runat="server" Text="Diproses" OnClientClick="ConfirmProses()" OnClick="btnProses_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Verifikasi Ulang" VisibleIndex="6" Visible="false" Width="10%">
                                <EditFormSettings Visible="False" />
                                <DataItemTemplate>
                                    <%--<asp:Button ID="btnProsesTT" runat="server" Text="Cetak Tanda Terima" OnClick="btnProsesTT_Click" />--%>
                                    <asp:Button ID="btnBatalkan" runat="server" Text="Verifikasi Ulang" OnClick="btnBatalkan_Click" OnClientClick="ConfirmBatalkan()" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CEKORDER" VisibleIndex="7" Visible="false">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="File" VisibleIndex="8" Width="15%">
                                <EditFormSettings Visible="False" />
                                <DataItemTemplate>
                                    <asp:Button ID="btnDownload" runat="server" Text="Unduh" OnClick="btnDownload_Click" />
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                </div>

            </div>
        </div>
    </div>
    </div>

    <div>
        <asp:SqlDataSource ID="sqlOrder" runat="server"
            ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
            SelectCommand="SELECT [KDORDER], [NO_NOTA], [TGL_NOTA], [TGL_TAMBAH], B.NAMA_UNKER, CONCAT('KETERANGAN : ',[PERIHAL], ' (klik tanda + untuk melihat data barang)') as PERIHAL,dbo.GetStatusORDER(KDORDER) as [STATUS], dbo.cekORDERlebih(A.KDORDER) as CEKORDER
FROM [TBL_ORDER] A
INNER JOIN TBL_UNKER B
ON A.DRUNKER=B.KD_UNKER WHERE YEAR(TGL_NOTA)=@TAHUN AND A.KEUNKER=@KEUNKER AND dbo.GetStatusORDER(KDORDER)=@STATUS">
            <SelectParameters>
                <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="cboStatus" Name="STATUS" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="cboUnitKerja" Name="KEUNKER" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <asp:SqlDataSource ID="sqlOrderDetail" runat="server"
            ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
            SelectCommand="SELECT [IDETAILORDER], dbo.GetNAMABARANG(KODE_BARANG) AS NAMA_BARANG,KODE_BARANG, [MOHON], [PENUHI], 
        (dbo.getJMLSERTERORDERperKDBRGperUNKERv2(KODE_BARANG,KDUNKER,GETDATE(),0,1)-dbo.getJMLSERTERperKDBRGperUNKERv2(KODE_BARANG,KDUNKER,GETDATE())) AS STOK2,
        (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , @KDUNKER1 , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKERv2(KODE_BARANG , @KDUNKER1 , getdate() , 1,0))  as STOK1,
        (dbo.getJMLPENUHIORDERperKDBRGperUNKERv2(KODE_BARANG,KDUNKER,getdate(),1)) as STOKBELUMDIAMBIL,
        dbo.getMINTATERAKHIRperKDBRGperUNKER(KODE_BARANG,KDUNKER) AS PENUHISBLMNYA, [KDUNKER]  FROM [TBL_ORDER_DETAIL] WHERE KDORDER=@KDORDER"
            DeleteCommand="DELETE FROM [TBL_ORDER_DETAIL] WHERE [IDETAILORDER] = @IDETAILORDER"
            InsertCommand="INSERT INTO [TBL_ORDER_DETAIL] ([IDETAILORDER], [KODE_BARANG], [MOHON], [PENUHI], [STOK], [TGLACC], [KDUNKER], [NAMAACC]) VALUES (@IDETAILORDER, @KODE_BARANG, @MOHON, @PENUHI, @STOK, @TGLACC, @KDUNKER, @NAMAACC)"
            UpdateCommand="UPDATE [TBL_ORDER_DETAIL] SET  [PENUHI] = @PENUHI WHERE [IDETAILORDER] = @IDETAILORDER">
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
                <asp:SessionParameter Name="KDUNKER1" SessionField="KDUNKERATAS" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="PENUHI" Type="Int32" />
                <asp:Parameter Name="IDETAILORDER" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="sqlUnker" runat="server"
            ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
            SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </div>
</asp:Content>

