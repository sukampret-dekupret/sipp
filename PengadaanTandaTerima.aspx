<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PengadaanTandaTerima.aspx.vb" Inherits="PengadaanTandaTerima" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin data yang sudah anda masukan sudah benar?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Permohonan</li>
        <li class="breadcrumb-item active">Proses Pembuatan Tanda Terima (Admin Unit) </li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Tanda Terima Barang
            <small>Daftar Barang Persediaan yang akan diserahkan.</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show">
                    <div class="panel-hdr">
                        <h2>Daftar  <span class="fw-300"><i>Barang Pengadaan</i></span>
                        </h2>
                        <div class="panel-toolbar">
                            <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                            <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                            <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>

                        </div>
                    </div>
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">NO. FAKTUR PEMBELIAN/PENGADAAN :</label>
                            <asp:Label ID="lblNoSPK" runat="server" Text="-" CssClass="form-control"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">No. Pengiriman</label>
                            <asp:TextBox ID="txtNoFaktur" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">Tanggal Pengiriman</label>
                            <dx:ASPxDateEdit ID="txtTglFaktur" runat="server" CssClass="form-control"></dx:ASPxDateEdit>
                        </div>
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">File Pengiriman</label>
                            <asp:FileUpload ID="xFileUpload" runat="server" Width="100%" accept=".pdf" CssClass="form-control" />
                        </div>
                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlSPKTTDetail"
                            KeyFieldName="PKSPKDETAILTT" OnRowValidating="grid_RowValidating"
                            OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared" EnableCallBacks="false"
                            Width="100%">
                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>
                            <SettingsEditing Mode="Batch">
                                <BatchEditSettings KeepChangesOnCallbacks="False" />
                            </SettingsEditing>
                            <SettingsCommandButton>
                                <DeleteButton Text="Hapus">
                                </DeleteButton>
                            </SettingsCommandButton>

                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>

                            <SettingsSearchPanel Visible="True" />
                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0" ShowEditButton="True">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="PKSPKDETAILTT" ReadOnly="True" VisibleIndex="1" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="2" ReadOnly="true" Caption="KODE BARANG" Width="20%">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMABARANG" ReadOnly="True" VisibleIndex="3" Caption="NAMA BARANG" Width="50%">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DIPESAN" ReadOnly="True" VisibleIndex="4" Caption="DI PESAN" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataSpinEditColumn FieldName="DITERIMA" ReadOnly="True" VisibleIndex="5" Caption="SUDAH TERIMA SEBELUMNYA" Width="10%">

                                    <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>

                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataSpinEditColumn FieldName="JML" VisibleIndex="6" Width="10%" Caption="JML TERIMA">
                                    <PropertiesSpinEdit DisplayFormatString="g" NullDisplayText="0" MinValue="0" MaxValue="1000000">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">CATATAN / KETERANGAN :</label>
                            <asp:TextBox ID="txtPerihal" runat="server" TextMode="MultiLine" Width="100%" CssClass="form-control"></asp:TextBox>
                        </div>
                        <asp:Button ID="Button1" runat="server" OnClick="btnConfirm_Click" Text="Proses" OnClientClick="Confirm()" CssClass="btn-success" Width="100px" />
                        <asp:Button ID="Button2" runat="server" Text="Batal" CssClass="btn-danger" Width="100px" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

    </div>


    <asp:SqlDataSource ID="sqlSPKTTDetail" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT PKSPKDETAILTT, KODE_BARANG, dbo.GetNamaBarang(KODE_BARANG) AS NAMABARANG, dbo.getJMLBELITperKDBRGperSPK(KODE_BARANG, IDSPK) AS DIPESAN, dbo.getJMLSERTERTperKDBRGperSPK(KODE_BARANG, IDSPK) AS DITERIMA, JML FROM TBL_SPK_TT_DETAIL WHERE PKSPKTT=@PKSPKTT"
        DeleteCommand="DELETE FROM [TBL_SPK_TT_DETAIL] WHERE [PKSPKDETAILTT] = @PKSPKDETAILTT"
        UpdateCommand="UPDATE [TBL_SPK_TT_DETAIL] SET [JML] = @JML WHERE [PKSPKDETAILTT] = @PKSPKDETAILTT">
        <DeleteParameters>
            <asp:Parameter Name="PKSPKDETAILTT" Type="Int64" />
        </DeleteParameters>
        <SelectParameters>
            <asp:SessionParameter Name="PKSPKTT" SessionField="PKSPKTT" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="PKSPKDETAILTT" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <%--    <asp:SqlDataSource ID="sqlMasterBarang" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN] FROM [TBL_BARANG] WHERE ((dbo.jmlSTOKS2(@KDUNKER,getdate(),KODE_BARANG,0,1)-dbo.jmlSTOKMOHON(@KDUNKER,GETDATE(),KODE_BARANG)))&gt;0 ORDER BY NAMA_BARANG ASC">
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKERATAS" />
        </SelectParameters>
    </asp:SqlDataSource>--%>

    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlSPKTT" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_SPK_TT] WHERE [PKSPKTT] = @PKSPKTT" InsertCommand="INSERT INTO [TBL_SPK_TT] ([PKSPKTT], [IDSPK], [NODO], [TGLFAKTUR], [KETERANGAN], [TGL_PROSES], [NAMAPROSES]) VALUES (@PKSPKTT, @IDSPK, @NODO, @TGLFAKTUR, @KETERANGAN, GETDATE(), @NAMAPROSES)">
        <DeleteParameters>
            <asp:Parameter Name="PKSPKTT" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="PKSPKTT" SessionField="PKSPKTT" Type="String" />
            <asp:SessionParameter Name="IDSPK" SessionField="IDspkDRPROSESspk" Type="String" />
            <asp:ControlParameter ControlID="txtNoFaktur" Name="NODO" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtTglFaktur" Name="TGLFAKTUR" PropertyName="Value" Type="DateTime" />
            <asp:ControlParameter ControlID="txtPerihal" Name="KETERANGAN" PropertyName="Text" Type="String" />
            <asp:SessionParameter Name="NAMAPROSES" SessionField="NAMAUSER" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
</asp:Content>

