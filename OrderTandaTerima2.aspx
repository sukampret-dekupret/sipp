<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="OrderTandaTerima2.aspx.vb" Inherits="OrderTandaTerima2" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Order</li>
        <li class="breadcrumb-item active">Tanda Terima</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>
    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Serah Terima Barang
            <small>Daftar Barang yang akan diserahkan</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">NO NOTA</label>
                            <asp:Label ID="lblNoPermohonan" runat="server" Text="-" CssClass="form-control"></asp:Label>
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
                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlDetail" KeyFieldName="PKORDERTTDETAIL"
                            Width="100%" OnRowValidating="grid_RowValidating" Theme="Default" EnableCallBacks="False">
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
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsText ConfirmDelete="Apakah data barang yakin akan dihapus?" />
                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                    <dx:GridViewCommandColumn VisibleIndex="0">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn FieldName="PKORDERTTDETAIL" ReadOnly="True" VisibleIndex="1" Visible="false">
                                        <EditFormSettings Visible="False" />
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PKORDERTT" VisibleIndex="2" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="3">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" ReadOnly="True" VisibleIndex="4">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="PENUHI" ReadOnly="true" VisibleIndex="5">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="SUDAH" ReadOnly="True" VisibleIndex="6">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="JML" VisibleIndex="7">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="STOK" ReadOnly="True" VisibleIndex="8">
                                    </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>

                        <br />

                        <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Proses" OnClientClick="Confirm()" CssClass="btn-success" Width="100px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="btn-danger" Width="100px" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
        </div>

        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>


    <asp:SqlDataSource ID="sqldetail" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>" 
        DeleteCommand="DELETE FROM [TBL_ORDER_TT_DETAIL] WHERE [PKORDERTTDETAIL] = @PKORDERTTDETAIL" 
        InsertCommand="INSERT INTO [TBL_ORDER_TT_DETAIL] ([PKORDERTTDETAIL], [PKORDERTT], [KDORDER], [TGL_TAMBAH], [NAMAUSER], [KODE_BARANG], [JML], [KET]) VALUES (@PKORDERTTDETAIL, @PKORDERTT, @KDORDER, @TGL_TAMBAH, @NAMAUSER, @KODE_BARANG, @JML, @KET)" 
        SelectCommand="SELECT A.PKORDERTTDETAIL, A.PKORDERTT, A.KODE_BARANG, dbo.GetNamaBarang(A.KODE_BARANG) AS NAMA_BARANG, B.PENUHI, dbo.getJMLSERTERTperKDBRGperORDER(A.KODE_BARANG, A.KDORDER) AS SUDAH, A.JML, (dbo.getJMLSERTERSPKperKDBRGperUNKER(A.KODE_BARANG , @KDUNKER , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(A.KODE_BARANG , @KDUNKER , getdate() , 1,0)) AS STOK FROM TBL_ORDER_TT_DETAIL AS A INNER JOIN TBL_ORDER_DETAIL AS B ON A.KODE_BARANG = B.KODE_BARANG WHERE (A.PKORDERTT = @PKORDERTT)" 
        UpdateCommand="UPDATE [TBL_ORDER_TT_DETAIL] SET [PKORDERTT] = @PKORDERTT, [KDORDER] = @KDORDER, [TGL_TAMBAH] = @TGL_TAMBAH, [NAMAUSER] = @NAMAUSER, [KODE_BARANG] = @KODE_BARANG, [JML] = @JML, [KET] = @KET WHERE [PKORDERTTDETAIL] = @PKORDERTTDETAIL">
        <DeleteParameters>
            <asp:Parameter Name="PKORDERTTDETAIL" Type="String" />
        </DeleteParameters>
                <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKERATAS" />
            <asp:SessionParameter Name="PKORDERTT" SessionField="PKORDERTT" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="PKORDERTTDETAIL" Type="String" />
            <asp:Parameter Name="PKORDERTT" Type="String" />
            <asp:Parameter Name="KDORDER" Type="String" />
            <asp:Parameter Name="TGL_TAMBAH" Type="DateTime" />
            <asp:Parameter Name="NAMAUSER" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="KET" Type="Int16" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="PKORDERTT" Type="String" />
            <asp:Parameter Name="KDORDER" Type="String" />
            <asp:Parameter Name="TGL_TAMBAH" Type="DateTime" />
            <asp:Parameter Name="NAMAUSER" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="KET" Type="Int16" />
            <asp:Parameter Name="PKORDERTTDETAIL" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

