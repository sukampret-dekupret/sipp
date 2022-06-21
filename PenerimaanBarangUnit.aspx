<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PenerimaanBarangUnit.aspx.vb" Inherits="PenerimaanBarangUnit" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            <i class='fal fa-th-list text-primary'></i>Penerimaan Barang Unit
            <small>Input Saldo Awal Barang Persediaan.</small>
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

                        <div class="panel-tag">
                            <p>
                                
                                Untuk memasukan data barang persediaan dari excel, pilih upload file kemudian klik tombol Submit dibawah ini, dengan format seperti berikut <asp:LinkButton ID="btnDownloadTemplate" runat="server" Font-Underline="True" Font-Bold="True">template saldo awal</asp:LinkButton>

                            </p>
                            <div class="form-group">
                                <label class="form-label" for="simpleinput">Upload Excel</label>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"  OnClick="btnSubmit_Click"/>


                            </div>


                        </div>

                        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False"
                            DataSourceID="sqlPenerimaanDetail" KeyFieldName="IDPENERIMAANDTL" Width="100%" EnableCallBacks="false">
                            <SettingsEditing Mode="Batch">
                                <BatchEditSettings KeepChangesOnCallbacks="False" />
                            </SettingsEditing>
                            <SettingsCommandButton>
                                <NewButton Text="Tambah Barang">
                                </NewButton>
                                <UpdateButton Text="Simpan">
                                </UpdateButton>
                                <CancelButton Text="Batalkan">
                                </CancelButton>
                                <EditButton Text="Ubah">
                                </EditButton>
                                <DeleteButton Text="Hapus">
                                </DeleteButton>
                            </SettingsCommandButton>

                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowNewButtonInHeader="True" VisibleIndex="0" Width="15%">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="IDPENERIMAANDTL" ReadOnly="True" VisibleIndex="1" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="IDPENERIMAAN" VisibleIndex="2" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="KODE_BARANG" VisibleIndex="3" Caption="NAMA BARANG" Width="75%">
                                    <PropertiesComboBox DataSourceID="sqlMasterBarang" TextField="NAMA_BARANG" ValueField="KODE_BARANG" DisplayFormatString="{0}">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="KODE BARANG" FieldName="KODE_BARANG" />
                                            <dx:ListBoxColumn Caption="NAMA BRG" FieldName="NAMA_BARANG" />
                                            <dx:ListBoxColumn Caption="SATUAN" FieldName="SATUAN" />
                                        </Columns>
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataSpinEditColumn FieldName="JML" VisibleIndex="4" Width="10%">
                                    <PropertiesSpinEdit DisplayFormatString="g">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                            </Columns>
                        </dx:ASPxGridView>

                    </div>
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label">Tanggal Penerimaan</label>
                            <dx:ASPxDateEdit ID="txtTglFaktur" runat="server" placeholder="Masukan tanggal faktur" CssClass="form-control"></dx:ASPxDateEdit>
                        </div>

                        <div class="form-group row">
                            <label class="form-label">Catatan</label>
                            <asp:TextBox ID="txtPerihal" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnProses" runat="server" Text="Submit / Proses" CssClass="btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:SqlDataSource ID="sqlPenerimaan" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PENERIMAAN_BRG] WHERE [IDPENERIMAAN] = @IDPENERIMAAN"
        InsertCommand="INSERT INTO [TBL_PENERIMAAN_BRG] ([IDPENERIMAAN], [TGL_PENERIMAAN], [CATATAN], [NAMAUSER], [KDUNKER],TGL_PROSES,KET) VALUES (@IDPENERIMAAN, @TGL_PENERIMAAN, @CATATAN, @NAMAUSER, @KDUNKER,GETDATE(),1)"
        SelectCommand="SELECT [IDPENERIMAAN], [TGL_PENERIMAAN], [CATATAN], [NAMAUSER], [KDUNKER] FROM [TBL_PENERIMAAN_BRG]"
        UpdateCommand="UPDATE [TBL_PENERIMAAN_BRG] SET [TGL_PENERIMAAN] = @TGL_PENERIMAAN, [CATATAN] = @CATATAN, [NAMAUSER] = @NAMAUSER, [KDUNKER] = @KDUNKER WHERE [IDPENERIMAAN] = @IDPENERIMAAN">
        <DeleteParameters>
            <asp:Parameter Name="IDPENERIMAAN" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="IDPENERIMAAN" SessionField="IDPENERIMAAN" Type="String" />
            <asp:ControlParameter ControlID="txtTglFaktur" Name="TGL_PENERIMAAN" PropertyName="Value" Type="DateTime" />
            <asp:ControlParameter ControlID="txtPerihal" Name="CATATAN" PropertyName="Text" Type="String" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" Type="String" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="TGL_PENERIMAAN" Type="DateTime" />
            <asp:Parameter Name="CATATAN" Type="String" />
            <asp:Parameter Name="NAMAUSER" Type="String" />
            <asp:Parameter Name="KDUNKER" Type="String" />
            <asp:Parameter Name="IDPENERIMAAN" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlPenerimaanDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PENERIMAAN_BRG_DTL] WHERE [IDPENERIMAANDTL] = @IDPENERIMAANDTL"
        InsertCommand="INSERT INTO [TBL_PENERIMAAN_BRG_DTL] ([IDPENERIMAAN], [KODE_BARANG], [JML], [NAMAUSER], [KDUNKER]) VALUES (@IDPENERIMAAN, @KODE_BARANG, @JML, @NAMAUSER, @KDUNKER)"
        SelectCommand="SELECT [IDPENERIMAANDTL], [IDPENERIMAAN], [KODE_BARANG], [JML], [NAMAUSER], [KDUNKER] FROM [TBL_PENERIMAAN_BRG_DTL] WHERE IDPENERIMAAN=@IDPENERIMAAN"
        UpdateCommand="UPDATE [TBL_PENERIMAAN_BRG_DTL] SET [KODE_BARANG] = @KODE_BARANG, [JML] = @JML WHERE [IDPENERIMAANDTL] = @IDPENERIMAANDTL">
        <DeleteParameters>
            <asp:Parameter Name="IDPENERIMAANDTL" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="IDPENERIMAAN" SessionField="IDPENERIMAAN" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" Type="String" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDPENERIMAAN" SessionField="IDPENERIMAAN" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="IDPENERIMAANDTL" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="sqlMasterBarang" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN] ,dbo.jmlSPK(@KDUNKER,GEtdate(),KODE_BARANG)-dbo.jmlSTOKORDER(@KDUNKER,GEtdate(),KODE_BARANG,1) as STOK FROM [TBL_BARANG] WHERE ST_BARANG=1 ">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlMitra" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>" SelectCommand="SELECT [KODE_MITRA], [NAMA_MITRA] FROM [TBL_MITRA]"></asp:SqlDataSource>
</asp:Content>

