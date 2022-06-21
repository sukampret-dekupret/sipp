<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PermohonanAdd.aspx.vb" Inherits="PermohonanAdd" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin barang yang diajukan sudah benar?")) {
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
        <li class="breadcrumb-item active">Input Permohonan</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Permohonan Barang
            <small>Input Permohonan Barang Persediaan.</small>
        </h1>

    </div>

    <div class="row">
        <div class="col-lg-12">
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

    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Permohonan</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>

                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False"
                            DataSourceID="sqlMohonDetail" KeyFieldName="IDDETAILPERMOHONAN" Width="100%"
                            OnInitNewRow="grid_InitNewRow" EnableCallBacks="False">
                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>
                            <SettingsEditing Mode="Batch">
                                <BatchEditSettings KeepChangesOnCallbacks="False" />
                            </SettingsEditing>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsCommandButton>
                                <NewButton Text="Tambah">
                                </NewButton>
                                <EditButton Text="Ubah">
                                </EditButton>
                                <DeleteButton Text="Hapus">
                                </DeleteButton>
                            </SettingsCommandButton>
                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <SettingsText ConfirmDelete="Apakah data barang yakin akan dihapus?" EmptyDataRow="Klik Tambah untuk mulai memasukan barang" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="IDDETAILPERMOHONAN" ReadOnly="True" VisibleIndex="1" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="KODE_BARANG" VisibleIndex="2" Caption="NAMA BARANG" Width="75%">
                                    <PropertiesComboBox DataSourceID="sqlMasterBarang" TextField="NAMA_BARANG" ValueField="KODE_BARANG" TextFormatString="{0}">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="NAMA_BARANG" Width="60%" />
                                            <dx:ListBoxColumn FieldName="KODE_BARANG" Width="20%" />
                                            <dx:ListBoxColumn FieldName="STOK" Width="10%" />
                                            <dx:ListBoxColumn FieldName="SATUAN" Width="10%" />
                                        </Columns>
                                    </PropertiesComboBox>
                                    <EditFormSettings ColumnSpan="2" />
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataSpinEditColumn FieldName="JML_MOHON" VisibleIndex="3" Caption="PERMOHONAN" Width="25%">
                                    <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000000" MinValue="1">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataTextColumn FieldName="IDPERMOHONAN" VisibleIndex="4" Visible="false">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>

                    </div>
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label">TANGGAL : </label>
                            <dx:ASPxDateEdit ID="txtTanggal" runat="server" Width="100%"></dx:ASPxDateEdit>
                        </div>
                        <div class="form-group">
                            <label class="form-label">KEPERLUAN : </label>
                            <asp:TextBox ID="txtPerihal" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </div>
                        
                        <div class="form-group">
                            <asp:Button ID="btnConfirm" runat="server" Text="Submit / Proses" CssClass="btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:SqlDataSource ID="sqlMohonDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PERMOHONAN_DETAIL] WHERE [IDDETAILPERMOHONAN] = @IDDETAILPERMOHONAN"
        InsertCommand="INSERT INTO [TBL_PERMOHONAN_DETAIL] ( [KODE_BARANG], [JML_MOHON] ,[JML_PENUHI],[IDPERMOHONAN], [KDUNKER],NAMAUSER) VALUES ( @KODE_BARANG, @JML_MOHON,@JML_MOHON, @IDPERMOHONAN, @KDUNKER,@NAMAUSER)"
        SelectCommand="SELECT [IDDETAILPERMOHONAN], [KODE_BARANG], [JML_MOHON], [IDPERMOHONAN], [KDUNKER] FROM [TBL_PERMOHONAN_DETAIL] WHERE IDPERMOHONAN=@IDPERMOHONAN"
        UpdateCommand="UPDATE [TBL_PERMOHONAN_DETAIL] SET [KODE_BARANG] = @KODE_BARANG, [JML_MOHON] = @JML_MOHON, [JML_PENUHI]=@JML_MOHON WHERE [IDDETAILPERMOHONAN] = @IDDETAILPERMOHONAN">
        <DeleteParameters>
            <asp:Parameter Name="IDDETAILPERMOHONAN" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML_MOHON" Type="Int32" />
            <asp:SessionParameter Name="IDPERMOHONAN" SessionField="IDPERMOHONAN" Type="String" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" Type="String" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDPERMOHONAN" SessionField="IDPERMOHONAN" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML_MOHON" Type="Int32" />
            <asp:Parameter Name="IDDETAILPERMOHONAN" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlMasterBarang" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT KODE_BARANG, NAMA_BARANG, SATUAN, (dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) as STOK FROM [TBL_BARANG]  ORDER BY (dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,getdate(),0,1)-dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG,@KDUNKER,GETDATE())) DESC">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
</asp:Content>

