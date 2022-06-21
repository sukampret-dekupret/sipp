<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PermohonanTandaTerima.aspx.vb" Inherits="PermohonanTandaTerima" %>

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
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">NO PERMOHONAN</label>
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
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>

                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <%-- OnHtmlRowPrepared="gridTandaTerimaDetail_HtmlRowPrepared"--%>
                        <dx:ASPxGridView ID="gridTandaTerimaDetail" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlDetail" KeyFieldName="PKTTMOHONDTL"
                            Width="100%" OnRowValidating="gridTandaTerimaDetail_RowValidating" Theme="Default" EnableCallBacks="false">
                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>
                            <SettingsEditing Mode="EditForm">
                                <BatchEditSettings KeepChangesOnCallbacks="False" />
                            </SettingsEditing>
                            <SettingsCommandButton>
                                <DeleteButton Text="Hapus">
                                </DeleteButton>
                                <EditButton Text="Ubah">
                                </EditButton>
                            </SettingsCommandButton>
                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsText ConfirmDelete="Apakah data barang yakin akan dihapus?" />
                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0" ShowEditButton="True">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="PKTTMOHONDTL" ReadOnly="True" VisibleIndex="1" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PKTTMOHON" VisibleIndex="2" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="3" ReadOnly="true" Width="20%" Caption="KODE BARANG">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="4" ReadOnly="True" Width="40%" Caption="NAMA BARANG">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="JML_PENUHI" VisibleIndex="5" ReadOnly="True" Caption="JML PERSETUJUAN" Width="10%">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataSpinEditColumn FieldName="JML" VisibleIndex="6" Width="10%">
                                    <PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataTextColumn FieldName="SUDAH" VisibleIndex="7" ReadOnly="True" Caption="SUDAH DIAMBIL" Width="10%">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="STOK" ReadOnly="True" VisibleIndex="8" Caption="STOK TERSEDIA">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                        <br />
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">TANGGAL PENYERAHAN</label>
                            <dx:ASPxDateEdit ID="txtTanggalSerah" runat="server" CssClass="form-control"></dx:ASPxDateEdit>
                        </div>
                        <%--                        <div class="form-group">
                            <%--<label class="form-label" for="simpleinput">NO PERMOHONAN</label>--%>
                        <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Proses" OnClientClick="Confirm()" CssClass="btn-success" Width="100px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="btn-danger" Width="100px" OnClick="btnCancel_Click" />
                        <%--                        </div>--%>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:SqlDataSource ID="sqlDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PERMOHONAN_TT_DETAIL] WHERE [PKTTMOHONDTL] = @PKTTMOHONDTL"
        InsertCommand="INSERT INTO [TBL_PERMOHONAN_TT_DETAIL] ([PKTTMOHON], [KODE_BARANG], [JML], [NAMAUSER],[KDUNKER],[IDPERMOHONAN]) VALUES (@PKTTMOHON, @KODE_BARANG, @JML, @NAMAUSER,@KDUNKER,@IDPERMOHONAN)"
        SelectCommand="SELECT PKTTMOHONDTL, PKTTMOHON, KODE_BARANG, dbo.GetNamaBarang(KODE_BARANG) AS NAMA_BARANG, dbo.getJMLPENUHIperIDMOHON(KODE_BARANG, IDPERMOHONAN) AS JML_PENUHI, JML, dbo.getJMLMOHONperTT(KODE_BARANG, IDPERMOHONAN) AS SUDAH, dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG, KDUNKER, GETDATE(), 0, 1) - dbo.getJMLSERTERperKDBRGperUNKER(KODE_BARANG, KDUNKER, GETDATE()) AS STOK FROM TBL_PERMOHONAN_TT_DETAIL WHERE (PKTTMOHON = @PKTTMOHON) AND (KDUNKER = @KDUNKER)"
        UpdateCommand="UPDATE [TBL_PERMOHONAN_TT_DETAIL] SET [PKTTMOHON] = @PKTTMOHON, [KODE_BARANG] = @KODE_BARANG, [JML] = @JML, [NAMAUSER] = @NAMAUSER, [TGL_TAMBAH] = @TGL_TAMBAH WHERE [PKTTMOHONDTL] = @PKTTMOHONDTL">
        <DeleteParameters>
            <asp:Parameter Name="PKTTMOHONDTL" Type="Int64" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="PKTTMOHON" SessionField="PKTTMOHON" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" Type="String" />
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
            <asp:SessionParameter Name="IDPERMOHONAN" SessionField="IDTTPERMOHONAN" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
            <asp:SessionParameter Name="PKTTMOHON" SessionField="PKTTMOHON" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="PKTTMOHON" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="NAMAUSER" Type="String" />
            <asp:Parameter Name="TGL_TAMBAH" Type="DateTime" />
            <asp:Parameter Name="PKTTMOHONDTL" Type="Int64" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

