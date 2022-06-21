<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="OrderAdd.aspx.vb" Inherits="OrderAdd" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ConfirmOrder() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Apakah anda yakin barang yang dimohon/diorder sudah benar?")) {
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
                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlOrderDetail"
                            KeyFieldName="IDETAILORDER" Width="100%">
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

<SettingsPopup>
<HeaderFilter MinHeight="140px"></HeaderFilter>
</SettingsPopup>

                            <SettingsSearchPanel Visible="True" />
                            <SettingsText ConfirmDelete="Apakah anda yakin akan dihapus?" EmptyDataRow="Klik tombol TAMBAH untuk menambahkan barang" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="IDETAILORDER" ReadOnly="True" VisibleIndex="1" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="KDORDER" Visible="False" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="MOHON" VisibleIndex="4" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="KODE_BARANG" VisibleIndex="3" Width="90%" Caption="NAMA BARANG">
                                    <PropertiesComboBox DataSourceID="sqlMasterBarang" TextField="NAMA_BARANG" TextFormatString="{0}" ValueField="KODE_BARANG">
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="NAMA_BARANG" Width="60%" />
                                            <dx:ListBoxColumn FieldName="STOK2" Caption="STOK GUDANG" Width="15%" />
                                            <dx:ListBoxColumn Caption="STOK PUSAT" FieldName="STOK1" Width="15%" />
                                            <dx:ListBoxColumn FieldName="SATUAN" Width="10%" />
                                        </Columns>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                            </Columns>
                        </dx:ASPxGridView>

                    </div>
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label">No Nota Dinas </label>
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
                            <label class="form-label">Jenis Order</label>
                            <asp:DropDownList ID="cboJenis" runat="server" Width="100%" CssClass="form-control">
                                <asp:ListItem Value="0">NON REGULER</asp:ListItem>
                                <asp:ListItem Value="1">REGULER</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label class="form-label">File Nota</label>
                            <asp:FileUpload ID="xFileUpload" runat="server" Width="100%" accept=".pdf" CssClass="form-control"/>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnConfirm" runat="server" Text="Submit / Proses" CssClass="btn-primary" OnClientClick="return ConfirmOrder()" OnClick="btnConfirm_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <asp:SqlDataSource ID="sqlOrder" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>" DeleteCommand="DELETE FROM [TBL_ORDER] WHERE [KDORDER] = @KDORDER" InsertCommand="INSERT INTO [TBL_ORDER] ( [KDORDER],[NO_NOTA], [TGL_NOTA], [JENIS], [NAMAFILE], [DRUNKER], [KEUNKER], [NAMAUSER],  [PERIHAL]) VALUES (@KDORDER,@NO_NOTA, @TGL_NOTA, @JENIS,  @NAMAFILE, @DRUNKER, @KEUNKER, @NAMAUSER,  @PERIHAL)" UpdateCommand="UPDATE [TBL_ORDER] SET [NO_NOTA] = @NO_NOTA, [TGL_NOTA] = @TGL_NOTA, [JENIS] = @JENIS, [STATUS] = @STATUS, [NAMAFILE] = @NAMAFILE, [TGL_TAMBAH] = @TGL_TAMBAH, [DRUNKER] = @DRUNKER, [KEUNKER] = @KEUNKER, [NAMAUSER] = @NAMAUSER, [ACCUSER] = @ACCUSER, [TGL_ACC] = @TGL_ACC, [PERIHAL] = @PERIHAL WHERE [KDORDER] = @KDORDER">
        <DeleteParameters>
            <asp:Parameter Name="KDORDER" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:ControlParameter ControlID="txtNoFaktur" Name="NO_NOTA" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="txtTglFaktur" Name="TGL_NOTA" PropertyName="Value" Type="DateTime" />
            <asp:ControlParameter ControlID="cboJenis" Name="JENIS" PropertyName="SelectedValue" Type="Int16" />
            <asp:SessionParameter Name="DRUNKER" SessionField="KDUNKER" Type="String" />
            <asp:SessionParameter Name="KEUNKER" SessionField="KDUNKERATAS" Type="String" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" Type="String" />
            <asp:ControlParameter ControlID="txtPerihal" Name="PERIHAL" PropertyName="Text" Type="String" />
            <asp:SessionParameter Name="NAMAFILE" SessionField="KDORDER" />
            <asp:SessionParameter Name="KDORDER" SessionField="KDORDER" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="NO_NOTA" Type="String" />
            <asp:Parameter Name="TGL_NOTA" Type="DateTime" />
            <asp:Parameter Name="JENIS" Type="Int16" />
            <asp:Parameter Name="STATUS" Type="Int16" />
            <asp:Parameter Name="NAMAFILE" Type="String" />
            <asp:Parameter Name="TGL_TAMBAH" Type="DateTime" />
            <asp:Parameter Name="DRUNKER" Type="String" />
            <asp:Parameter Name="KEUNKER" Type="String" />
            <asp:Parameter Name="NAMAUSER" Type="String" />
            <asp:Parameter Name="ACCUSER" Type="String" />
            <asp:Parameter Name="TGL_ACC" Type="DateTime" />
            <asp:Parameter Name="PERIHAL" Type="String" />
            <asp:Parameter Name="KDORDER" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
   <%-- CASE WHEN (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , @KDUNKER1 , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , @KDUNKER1 , getdate() , 1,0)) = 0 THEN 'KOSONG' ELSE 'ADA' END--%>
    <asp:SqlDataSource ID="sqlMasterBarang" runat="server" 
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT KODE_BARANG, NAMA_BARANG, SATUAN, dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG, @KDUNKER, GETDATE(), 0,1) - dbo.getJMLMOHONperKDBRGperUNKER(KODE_BARANG, @KDUNKER, GETDATE()) AS STOK2, 
        (dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , @KDUNKER1 , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , @KDUNKER1 , getdate() , 1,0)) AS STOK1 FROM TBL_BARANG WHERE (ST_BARANG = 1)">
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
            
            <asp:SessionParameter Name="KDUNKER1" SessionField="KDUNKERATAS" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>
    <br />

    <asp:SqlDataSource ID="sqlOrderDetail" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_ORDER_DETAIL] WHERE [IDETAILORDER] = @IDETAILORDER"
        InsertCommand="INSERT INTO [TBL_ORDER_DETAIL] ( [KDORDER], [KODE_BARANG], [MOHON], [KDUNKER], [NAMAUSER], [KEUNKER],[PENUHI]) VALUES (@KDORDER, @KODE_BARANG, @MOHON, @KDUNKER, @NAMAUSER, @KEUNKER,@MOHON)"
        SelectCommand="SELECT [IDETAILORDER], [KDORDER], [KODE_BARANG], [MOHON], [KDUNKER], [NAMAUSER], [KEUNKER] FROM [TBL_ORDER_DETAIL] WHERE KDORDER=@KDORDER"
        UpdateCommand="UPDATE [TBL_ORDER_DETAIL] SET [KDORDER] = @KDORDER, [KODE_BARANG] = @KODE_BARANG, [MOHON] = @MOHON,[PENUHI]=@MOHON, [KDUNKER] = @KDUNKER, [NAMAUSER] = @NAMAUSER, [KEUNKER] = @KEUNKER WHERE [IDETAILORDER] = @IDETAILORDER">
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
            <asp:SessionParameter Name="KDORDER" SessionField="KDORDER" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="KDORDER" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="MOHON" Type="Int32" />
            <asp:Parameter Name="KDUNKER" Type="String" />
            <asp:Parameter Name="NAMAUSER" Type="String" />
            <asp:Parameter Name="KEUNKER" Type="String" />
            <asp:Parameter Name="IDETAILORDER" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

