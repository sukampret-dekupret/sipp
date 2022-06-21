<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PengadaanAdd.aspx.vb" Inherits="PengadaanAdd" %>

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

    <script type="text/javascript">
        var FocusedCellColumnIndex = 0;
        var FocusedCellRowIndex = 0;
        function OnInit(s, e) {
            ASPxClientUtils.AttachEventToElement(Grid.GetMainElement(), "keydown", function (evt) {
                if (evt.keyCode === ASPxClientUtils.StringToShortcutCode("ENTER"))
                    EnterPressed();
            });
        }
        function OnStartEditCell(s, e) {
            FocusedCellColumnIndex = e.focusedColumn.index;
            FocusedCellRowIndex = e.visibleIndex;
        }
        function OnEndEditCell(s, e) {
            FocusedCellColumnIndex = 0;
            FocusedCellRowIndex = 0;
        }
        function EnterPressed() {
            if ((FocusedCellColumnIndex == (Grid.GetColumnCount() - 1)) && (FocusedCellRowIndex == (Grid.GetVisibleRowsOnPage() - 1))) {
                Grid.AddNewRow();
                Grid.batchEditApi.EndEdit();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Pengadaan</li>
        <li class="breadcrumb-item active">Input Pengadaan</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Pengadaan Barang
            <small>Input Pengadaan Barang Persediaan.</small>
        </h1>

    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">UNIT KERJA</label>
                            <asp:DropDownList ID="cboUnitKerja" runat="server" CssClass="form-control" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" AutoPostBack="True"
                                DataSourceID="sqlUnker">
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
                    <h2>Daftar  <span class="fw-300"><i>Pengadaan</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <%--<button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>
                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="panel-tag">
                            <p>Untuk memasukan data barang persediaan dari excel, pilih upload file kemudian klik tombol Submit dibawah ini, , dengan format seperti berikut <asp:LinkButton ID="btnDownloadTemplate" runat="server" Font-Underline="True" Font-Bold="True">template saldo awal</asp:LinkButton>

                            </p>
                                                       <div class="form-group">
                            <label class="form-label" for="simpleinput">Upload Excel</label>
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                
                        </div>
                            <p>
                                Sedangkan untuk mengambil data barang persediaan dari barang yang belum dipenuhi klik tombol berikut : <code><asp:LinkButton ID="btnBarang" runat="server" OnClick="btnBarang_Click">Import Data</asp:LinkButton></code>
                            </p>
                            
                        </div>

                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlSPKDetail"
                            KeyFieldName="IDSPKDETAIL" Width="100%" EnableCallBacks="false">
                            <SettingsEditing Mode="Batch">
                                <BatchEditSettings KeepChangesOnCallbacks="False" />
                            </SettingsEditing>
                            <Settings ShowFooter="True" />
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsCommandButton>
                                <NewButton Text="Tambah">
                                </NewButton>
                                <DeleteButton Text="Hapus">
                                </DeleteButton>
                            </SettingsCommandButton>

                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>

                            <SettingsSearchPanel Visible="True" />
                            <SettingsText ConfirmDelete="Apakah anda yakin akan dihapus?" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="IDSPK" VisibleIndex="1" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="KODE_BARANG" VisibleIndex="2" Caption="NAMA BARANG">
                                    <PropertiesComboBox DataSourceID="sqlMasterBarang" TextField="NAMA_BARANG" ValueField="KODE_BARANG" TextFormatString="{0}">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="NAMA BARANG" FieldName="NAMA_BARANG" />
                                            <dx:ListBoxColumn Caption="KODE BARANG" FieldName="KODE_BARANG" />
                                            <dx:ListBoxColumn Caption="STOK BARANG" FieldName="STOK" />
                                            <dx:ListBoxColumn Caption="SATUAN" FieldName="SATUAN" />
                                        </Columns>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataSpinEditColumn FieldName="JML" VisibleIndex="3">
                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000000" MinValue="1">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <%--                                <dx:GridViewDataSpinEditColumn Caption="HARGA" FieldName="HARGA" VisibleIndex="4">
                                    <PropertiesSpinEdit DisplayFormatString="Rp. {0:0,0.00}">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>--%>
                                <%--                                <dx:GridViewDataSpinEditColumn Caption="TOTAL" FieldName="TOTAL" VisibleIndex="5" ReadOnly="true">
                                    <PropertiesSpinEdit DisplayFormatString="Rp. {0:0,0.00}">
                                    </PropertiesSpinEdit>
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataSpinEditColumn>--%>
                            </Columns>
                            <Settings ShowFooter="true" />
                            <TotalSummary>
                                <%--<dx:ASPxSummaryItem DisplayFormat="Rp. {0:0,0.00}" FieldName="TOTAL" ShowInColumn="TOTAL" SummaryType="Sum" />--%>
                                <dx:ASPxSummaryItem DisplayFormat="g" FieldName="JML" ShowInColumn="JML" SummaryType="Sum" />
                                <dx:ASPxSummaryItem FieldName="IDSPKDETAIL" ShowInGroupFooterColumn="JML" SummaryType="Count" />
                            </TotalSummary>
                        </dx:ASPxGridView>

                    </div>
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label">No SPK </label>
                            <asp:TextBox ID="txtNoFaktur" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Tanggal Nota</label>
                            <dx:ASPxDateEdit ID="txtTglFaktur" runat="server" placeholder="Masukan tanggal faktur" CssClass="form-control"></dx:ASPxDateEdit>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Perihal</label>
                            <asp:TextBox ID="txtPerihal" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">Supplier / Mitra</label>
                            <dx:ASPxComboBox ID="cboMitra" runat="server" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
                                ValueType="System.Int32" DataSourceID="sqlMitra"
                                TextField="NAMA_MITRA" ValueField="KODE_MITRA"
                                CssClass="form-control">
                            </dx:ASPxComboBox>
                        </div>
                        <div class="form-group">
                            <label class="form-label">File Nota</label>
                            <asp:FileUpload ID="xFileUpload" runat="server" Width="100%" accept=".pdf" CssClass="form-control" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnProses" runat="server" Text="Submit / Proses" CssClass="btn-primary" OnClientClick="return Confirm()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <asp:SqlDataSource ID="sqlspk" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_SPK] WHERE [IDSPK] = @IDSPK"
        InsertCommand="INSERT INTO [TBL_SPK] ([IDSPK], [NOFAKTUR],[TGL_FAKTUR],[PERIHAL],[KODE_MITRA],[NAMAUSER],[KDUNKER],[URLFILE]) VALUES (@IDSPK, @NOFAKTUR,@TGLFAKTUR,@PERIHAL,@KODE_MITRA,@NAMAUSER,@KDUNKER,@URLFILE)"
        SelectCommand="SELECT [IDSPK], [NOFAKTUR] FROM [TBL_SPK] WHERE IDSPK=@IDSPK"
        UpdateCommand="UPDATE [TBL_SPK] SET [NOFAKTUR] = @NOFAKTUR WHERE [IDSPK] = @IDSPK">
        <DeleteParameters>
            <asp:Parameter Name="IDSPK" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" Type="String" />
            <asp:ControlParameter ControlID="txtNoFaktur" Name="NOFAKTUR" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtTglFaktur" Name="TGLFAKTUR" PropertyName="Value" />
            <asp:ControlParameter ControlID="txtPerihal" Name="PERIHAL" PropertyName="Text" />
            <asp:ControlParameter ControlID="cboMitra" Name="KODE_MITRA" PropertyName="Value" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
            <asp:SessionParameter Name="URLFILE" SessionField="URLFILE" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="NOFAKTUR" Type="DateTime" />
            <asp:Parameter Name="IDSPK" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <%--InsertCommand="INSERT INTO [TBL_SPK_DETAIL] ( [IDSPK], [KODE_BARANG], [JML], [NAMAUSER], [KDUNKER],[HARGA],[TOTAL]) VALUES (@IDSPK, @KODE_BARANG, @JML, @NAMAUSER, @KDUNKER,@HARGA,@TOTAL)"--%>
    <asp:SqlDataSource ID="sqlSPKDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_SPK_DETAIL] WHERE [IDSPKDETAIL] = @IDSPKDETAIL"
        InsertCommand="INSERT INTO [TBL_SPK_DETAIL] ( [IDSPK], [KODE_BARANG], [JML], [NAMAUSER], [KDUNKER]) VALUES (@IDSPK, @KODE_BARANG, @JML, @NAMAUSER, @KDUNKER)"
        SelectCommand="SELECT [IDSPKDETAIL], [IDSPK], [KODE_BARANG], [JML], [TGL_TAMBAH], [TGL_PROSES], [NAMAUSER], [KDUNKER],[HARGA],[JML]*[HARGA] AS [TOTAL] FROM [TBL_SPK_DETAIL] where IDSPK=@IDSPK"
        UpdateCommand="UPDATE [TBL_SPK_DETAIL] SET [KODE_BARANG] = @KODE_BARANG, [JML] = @JML,  [HARGA] = @HARGA WHERE [IDSPKDETAIL] = @IDSPKDETAIL">
        <DeleteParameters>
            <asp:Parameter Name="IDSPKDETAIL" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" Type="String" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="HARGA" />
            <asp:Parameter Name="IDSPKDETAIL" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <%--SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN] ,dbo.jmlTERIMABRG(KODE_BARANG,@KDUNKER,GEtdate())-dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),KODE_BARANG,1) as STOK FROM [TBL_BARANG]--%>
    <asp:SqlDataSource ID="sqlMasterBarang" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN] ,dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GEtdate())-dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GEtdate(),1,0) as STOK FROM [TBL_BARANG] WHERE ST_BARANG=1">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_WILAYAH='00' AND KD_STS_AKTIF=1 "></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlMitra" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_MITRA], [NAMA_MITRA] FROM [TBL_MITRA]"></asp:SqlDataSource>
    <br />

</asp:Content>

