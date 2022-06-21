<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PermohonanProses.aspx.vb" Inherits="PermohonanProses" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin permohonan akan diproses/disetujui ke Op. Gudang?")) {
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
            if (confirm("Apakah anda yakin permohonan akan ditolak?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

    <%--    <script type="text/javascript">
        function ConfirmPengadaan() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin permohonan akan diproses di pengadaan?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>--%>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
        <div class="col-lg-6">
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
        <div class="col-lg-3">
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
        <div class="col-lg-3">
            <div id="panel-atas-3" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">STATUS</label>
                            <asp:DropDownList ID="cboSTATUSPermohonan" runat="server" AutoPostBack="True" CssClass="form-control">
                                <asp:ListItem Value="0">PENDING</asp:ListItem>
                                <asp:ListItem Value="1">PROSES</asp:ListItem>
                                <asp:ListItem Value="4">SELESAI</asp:ListItem>
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
                    <h2>Daftar  <span class="fw-300"><i>Daftar Permohonan</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <%--                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>
                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared"
                            OnCustomCallback="grid_CustomCallback" ClientInstanceName="masterGrid"
                            DataSourceID="sqlPermohonan" KeyFieldName="IDPERMOHONAN" Width="100%"
                            PreviewFieldName="PERIHAL" EnableCallBacks="false">

                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="gridDetail" runat="server"
                                        AutoGenerateColumns="False" DataSourceID="sqlPerDetail"
                                        KeyFieldName="IDDETAILPERMOHONAN" Width="100%"
                                        OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect"
                                        OnRowValidating="gridDetail_RowValidating"
                                        OnHtmlRowPrepared="gridDetail_HtmlRowPrepared"
                                        SettingsEditing-Mode="Batch"
                                        OnRowUpdating="gridDetail_RowUpdating"
                                        OnCommandButtonInitialize="gridDetail_CommandButtonInitialize">
                                        <%--<ClientSideEvents EndCallback="OnEndCallback" BeginCallback="OnBeginCallback" />--%>
                                        <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                                        <SettingsPopup>
                                            <HeaderFilter MinHeight="140px">
                                            </HeaderFilter>
                                        </SettingsPopup>
                                        <SettingsSearchPanel Visible="True" />
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDDETAILPERMOHONAN" ReadOnly="True" VisibleIndex="1" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDPERMOHONAN" VisibleIndex="5" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="2" Caption="KODE BARANG" ReadOnly="true">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="3" Caption="NAMA BARANG" ReadOnly="true">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="JML_MOHON" VisibleIndex="4" Caption="JUMLAH YG DIMOHON" ReadOnly="true">

                                                <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000" MinValue="1" NullText="0">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="JML_PENUHI" VisibleIndex="5" Caption="JUMLAH YANG DIPENUHI">
                                                <PropertiesSpinEdit DisplayFormatString="g" MaxValue="100000" NullText="0">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="STOK1" VisibleIndex="6" Caption="STOK GUDANG UNIT" ReadOnly="true">

                                                <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000" MinValue="1">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <%--                                            <dx:GridViewDataSpinEditColumn FieldName="STOKRESERVED" VisibleIndex="7" Caption="STOKRESERVED" ReadOnly="true">
                                                <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000" MinValue="1" NullText="0">
                                                </PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>--%>
                                            <dx:GridViewDataTextColumn VisibleIndex="8" Caption="PILIHAN" Visible="false" ReadOnly="false">
                                                <EditFormSettings Visible="False" />
                                                <DataItemTemplate>
                                                    <%--<asp:Button ID="btnProsesPengadaan" runat="server" Text="Proses Pengadaan" OnClick="btnProsesPengadaan_Click" OnClientClick="return ConfirmPengadaan();" CssClass="label label-primary" />--%>
                                                </DataItemTemplate>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsEditing Mode="Batch" />
                                       <%-- <Settings ShowPreview="True" />--%>
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsDetail ShowDetailRow="true" />
                            <Settings ShowPreview="True" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <SettingsSearchPanel Visible="True" />
                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="NO_PERMOHONAN" VisibleIndex="1" Width="10%" Caption="NO PERMOHONAN">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_PERMOHONAN" VisibleIndex="2" Caption="TGL MOHON" Width="5%">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMAUSER" VisibleIndex="3" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PERIHAL" VisibleIndex="4" Width="45%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDPERMOHONAN" ReadOnly="True" VisibleIndex="0" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="STS_PERMOHONAN" VisibleIndex="5" Caption="STATUS" Width="10%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="PENDING" Value="0" />
                                            <dx:ListEditItem Text="DISETUJUI" Value="1" />
                                            <dx:ListEditItem Text="SELESAI" Value="4" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="6" Caption="PILIHAN" Width="10%">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnProses" runat="server" Text="PROSES" OnClick="btnProses_Click" OnClientClick="return Confirm();" CssClass="btn-success btn-pills" />
                                        <%-- <asp:Button ID="btnTolak" runat="server" Text="Tolak" OnClick="btnTolak_Click" OnClientClick="return ConfirmTolak();" />--%>
                                        <%--<asp:Button ID="btnProseskePengadaan" runat="server" Text="Proses ke Pengadaan" OnClick="btnProseskePengadaan_Click" OnClientClick="return ConfirmPengadaan();" />--%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn Caption="KETERANGAN" FieldName="KETLEBIH" VisibleIndex="7" Width="10%">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dx:ListEditItem Text="STOK ADA" Value="0" />
                                            <dx:ListEditItem Text="STOK KURANG" Value="1" />
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
    <asp:SqlDataSource ID="sqlPerDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PERMOHONAN_DETAIL] WHERE [IDDETAILPERMOHONAN] = @IDDETAILPERMOHONAN"
        InsertCommand="INSERT INTO [TBL_PERMOHONAN_DETAIL] ([IDDETAILPERMOHONAN], [KODE_BARANG], [JML_MOHON], [JML_PENUHI]) VALUES (@IDDETAILPERMOHONAN, @KODE_BARANG, @JML_MOHON, @JML_PENUHI)"
        SelectCommand="SELECT [IDDETAILPERMOHONAN], [KODE_BARANG], dbo.getNAMABARANG(KODE_BARANG) as NAMA_BARANG,[JML_MOHON], [JML_PENUHI],(dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) AS STOK1,(dbo.getJMLPENUHIperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) AS STOKRESERVED FROM [TBL_PERMOHONAN_DETAIL] WHERE ([IDPERMOHONAN] = @IDPERMOHONAN)"
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

    <%--    <asp:SqlDataSource ID="sqlMasterBarang" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG] FROM [TBL_BARANG] WHERE ST_BARANG=1"></asp:SqlDataSource>--%>

    <br />

    <asp:SqlDataSource
        ID="sqlPermohonan"
        runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [NO_PERMOHONAN], [TGL_PERMOHONAN], [PERIHAL], [STS_PERMOHONAN], [NAMAUSER],[DRUNKER], [IDPERMOHONAN],dbo.getKetLebihSTOK(@DRUNKER,IDPERMOHONAN) as KETLEBIH FROM [TBL_PERMOHONAN] WHERE DRUNKER=@DRUNKER AND YEAR(TGL_PERMOHONAN)=@TAHUN AND STS_PERMOHONAN=@STS_PERMOHONAN"
        UpdateCommand="UPDATE [TBL_PERMOHONAN] SET [STS_PERMOHONAN] = @STS_PERMOHONAN, [IDPEMROSES] = @IDPEMROSES,TGL_PROSES=@TGL_PROSES WHERE [IDPERMOHONAN] = @IDPERMOHONAN">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboUnitKerja" Name="DRUNKER" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboSTATUSPermohonan" Name="STS_PERMOHONAN" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="STS_PERMOHONAN" Type="Int16" />
            <asp:Parameter Name="IDPEMROSES" Type="String" />
            <asp:Parameter Name="TGL_PROSES" Type="Datetime" />
            <asp:Parameter Name="IDPERMOHONAN" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

