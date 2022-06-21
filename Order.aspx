<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Order.aspx.vb" Inherits="Order" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript">
        function ConfirmDeleteOrder() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin akan dihapus?")) {
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
                                <asp:ListItem Value="0">PENDING</asp:ListItem>
                                <asp:ListItem Value="1">PROSES</asp:ListItem>
   <%--                             <asp:ListItem Value="2">DITOLAK</asp:ListItem>
                                <asp:ListItem Value="3">PROSES PENGADAAN</asp:ListItem>--%>
                                <asp:ListItem Value="5">SELESAI</asp:ListItem>
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
<%--                            <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                            <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                            <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>

                        </div>
                    </div>

                    <div class="panel-container show">
                        <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlOrder"
                            KeyFieldName="KDORDER" Width="100%" PreviewFieldName="PERIHAL" 
                            OnCustomColumnDisplayText="grid_CustomColumnDisplayText" OnRowDeleted="grid_RowDeleted"  OnHtmlRowPrepared="grid_HtmlRowPrepared">

                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>

                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="gridDetail" runat="server"
                                        DataSourceID="sqlOrderDetail"
                                        OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect"
                                        AutoGenerateColumns="False" KeyFieldName="IDETAILORDER" Width="100%" 
                                        OnCommandButtonInitialize="gridDetail_CommandButtonInitialize"
                                         OnInitNewRow="gridDetail_InitNewRow">
                                        <SettingsAdaptivity>
                                            <AdaptiveDetailLayoutProperties ColCount="1">
                                            </AdaptiveDetailLayoutProperties>
                                        </SettingsAdaptivity>
                                        <SettingsBehavior ConfirmDelete="True" />
                                        <SettingsCommandButton>
                                            <NewButton Text="Tambah">
                                            </NewButton>
                                            <EditButton Text="Ubah">
                                            </EditButton>
                                            <DeleteButton Text="Hapus">
                                            </DeleteButton>
                                        </SettingsCommandButton>
                                        <SettingsSearchPanel Visible="True" />
                                        <SettingsText ConfirmDelete="Apakah data akan dihapus?" />
                                        <EditFormLayoutProperties ColCount="1">
                                        </EditFormLayoutProperties>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDETAILORDER" ReadOnly="True" VisibleIndex="1" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="MOHON" VisibleIndex="3">

                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="STOK" ReadOnly="True" VisibleIndex="6" Caption="STOK DI GUDANG ES II">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="7">
                                                <EditFormSettings Visible="False" />
                                                <DataItemTemplate>

                                                    <%--<asp:ImageButton ID="btnPenuhi" runat="server" ImageUrl="~/img/UP.png" OnClick="btnPenuhi_Click" />--%>
                                                    <%--<asp:ImageButton ID="btnSuratPengantar" runat="server" ImageUrl="~/img/report1.png" OnClick="btnSuratPengantar_Click" />--%>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn Caption="NAMA BARANG" FieldName="KODE_BARANG" VisibleIndex="2">
                                    <PropertiesComboBox DataSourceID="sqlMasterBarang" TextField="NAMA_BARANG" TextFormatString="{0}" ValueField="KODE_BARANG">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="NAMA_BARANG" Width="60%" />
                                            <dx:ListBoxColumn FieldName="STOK2" Caption="STOK GUDANG" Width="15%" />
                                            <dx:ListBoxColumn Caption="STOK PUSAT" FieldName="STOK1" Width="15%" />
                                            <dx:ListBoxColumn FieldName="SATUAN" Width="10%" />
                                        </Columns>
                                    </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="PENUHI" VisibleIndex="5">
                                                <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000000" MinValue="1">
                                                </PropertiesSpinEdit>
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataSpinEditColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <Settings ShowPreview="True" />
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsCommandButton>
                                <DeleteButton Text="Batalkan">
                                </DeleteButton>
                            </SettingsCommandButton>
                            <SettingsSearchPanel Visible="True" />
                            <SettingsDetail ShowDetailRow="true" />

                            <SettingsText ConfirmDelete="Apakah Permohonan Order akan dihapus?" />

                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" ShowDeleteButton="True">
                                </dx:GridViewCommandColumn>

                                <dx:GridViewDataTextColumn FieldName="KDORDER" ReadOnly="True"
                                    VisibleIndex="1" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn ReadOnly="True" VisibleIndex="2" UnboundType="String" Caption="№">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NO_NOTA" VisibleIndex="3" Caption="NO NOTA">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_NOTA" VisibleIndex="4" Caption="TGL NOTA">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_PROSES" VisibleIndex="5" Caption="TGL DIPROSES">
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataComboBoxColumn Caption="STATUS" FieldName="STATUS" VisibleIndex="7">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="PENDING" Value="0" />
                                            <dx:ListEditItem Text="SETUJU" Value="1" />
                                            <dx:ListEditItem Text="DITOLAK" Value="2" />
                                            <dx:ListEditItem Text="DIDISTRIBUSIKAN" Value="3" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="8">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnCetakPengantar" runat="server" Text="Cetak Pengantar" OnClick="btnCetakPengantar_Click" />
                                        <%--<asp:ImageButton ID="btnPrintOrder" runat="server" ImageUrl="~/img/reportpdf.png" OnClick="btnPrintOrder_Click" ToolTip="Print Surat Pengantar" />--%>
                                        <%--<asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/img/edit.png" OnClick="btnEdit_Click" ToolTip="Edit Order" />--%>
                                        <%--<asp:ImageButton ID="btnHapus" runat="server" ImageUrl="~/img/delete.png" OnClick="btnHapus_Click" ToolTip="Batalkan Order" />--%>
                                        <%--<asp:ImageButton ID="btnBatal" runat="server" ImageUrl="~/img/cancel.png" OnClick="btnBatal_Click" />--%>
                                        <%--<asp:ImageButton ID="btnSuratPengantar" runat="server" ImageUrl="~/img/report1.png" OnClick="btnSuratPengantar_Click" />--%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="JENIS" FieldName="JENIS" VisibleIndex="6">
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
        SelectCommand="SELECT [KDORDER], [NO_NOTA], [TGL_NOTA], [TGL_TAMBAH], B.NAMA_UNKER, [PERIHAL],TGL_PROSES,dbo.GetStatusORDER(KDORDER) AS [STATUS],A.JENIS 
FROM [TBL_ORDER] A
INNER JOIN TBL_UNKER B
ON A.DRUNKER=B.KD_UNKER WHERE YEAR(TGL_NOTA)=@TAHUN AND A.DRUNKER=@KDUNKER AND dbo.GetStatusORDER(KDORDER)=@STATUS" 
        DeleteCommand="DELETE FROM TBL_ORDER WHERE KDORDER=@KDORDER">
        <DeleteParameters>
            <asp:Parameter Name="KDORDER" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboStatus" Name="STATUS" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
    <%--SelectCommand="SELECT dbo.GetStatusProses(A.KDORDER) AS STATUSORDER,[IDETAILORDER], B.NAMA_BARANG, [MOHON], [PENUHI], dbo.jmlSTOKORDER(@KDUNKER1,GEtdate(),A.KODE_BARANG,0)-dbo.jmlSTOKMOHON(@KDUNKER1,GETDATE(),A.KODE_BARANG) AS STOK2,dbo.jmlSPK(@KDUNKER,GEtdate(),A.KODE_BARANG)-dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),A.KODE_BARANG,1)  as STOK1,[TGLACC], [KDUNKER], [NAMAACC] FROM [TBL_ORDER_DETAIL] A INNER JOIN TBL_BARANG B ON A.KODE_BARANG=B.KODE_BARANG WHERE A.KDORDER=@KDORDER" --%>
    <asp:SqlDataSource ID="sqlOrderDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT IDETAILORDER,KODE_BARANG,dbo.getnamabarang(KODE_BARANG) as NAMA_BARANG,MOHON,dbo.jmlNilaiPenuhiOrder(KODE_BARANG,KDORDER) as PENUHI,dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),KODE_BARANG,0)-dbo.jmlSTOKMOHON(@KDUNKER,GETDATE(),KODE_BARANG) AS STOK FROM TBL_ORDER_DETAIL WHERE KDORDER=@KDORDER"
        DeleteCommand="DELETE FROM [TBL_ORDER_DETAIL] WHERE [IDETAILORDER] = @IDETAILORDER"
        InsertCommand="INSERT INTO [TBL_ORDER_DETAIL] ( [KDORDER], [KODE_BARANG], [MOHON], [KDUNKER], [NAMAUSER], [KEUNKER],[PENUHI]) VALUES (@KDORDER, @KODE_BARANG, @MOHON, @KDUNKER, @NAMAUSER, @KEUNKER,@MOHON)"
        UpdateCommand="UPDATE [TBL_ORDER_DETAIL] SET  [MOHON] = @MOHON WHERE [IDETAILORDER] = @IDETAILORDER">
        <DeleteParameters>
            <asp:Parameter Name="IDETAILORDER" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="KDORDER" SessionField="KDORDER" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="MOHON" Type="Int32" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" Type="String" />
            <asp:SessionParameter Name="KEUNKER" SessionField="KDUNKERATAS" Type="String" />
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

        <asp:SqlDataSource ID="sqlMasterBarang" runat="server" 
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN],
(dbo.jmlSTOKS2(@KDUNKER,getdate(),KODE_BARANG,0,1)-dbo.jmlSTOKMOHON(@KDUNKER,GETDATE(),KODE_BARANG)) AS STOK2,CASE  WHEN (dbo.jmlSPK(@KDUNKER,GEtdate(),KODE_BARANG)-dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),KODE_BARANG,1))&lt;=0 THEN 'KOSONG'
ELSE 'ADA' END as STOK1  FROM [TBL_BARANG] WHERE ST_BARANG=1">
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
            <%--<asp:SessionParameter Name="KDUNKER1" SessionField="KDUNKER" />--%>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

