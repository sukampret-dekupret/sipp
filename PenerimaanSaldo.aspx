<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PenerimaanSaldo.aspx.vb" Inherits="PenerimaanSaldo" %>

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
            <i class='fal fa-th-list text-primary'></i>Daftar Input Saldo Awal
            <small>Daftar Input Saldo Barang Persediaan.</small>
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
                    <h2>Daftar  <span class="fw-300"><i>Input Saldo Awal</i></span>
                    </h2>
                    <div class="panel-toolbar">
<%--                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>

                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
 

                    </div>

                    <dx:ASPxGridView ID="gridSaldoAwal" runat="server" AutoGenerateColumns="False" DataSourceID="sqlPenerimaan" KeyFieldName="IDPENERIMAAN" Width="100%">
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="gridDetail" runat="server"
                                    AutoGenerateColumns="False" DataSourceID="sqlPenerimaanDetail" KeyFieldName="IDPENERIMAANDTL"
                                    OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect" Width="100%" EnableCallBacks="false">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="IDPENERIMAANDTL" ReadOnly="True" VisibleIndex="0" Visible="false">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="2" Width="30%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NAMABARANG" VisibleIndex="3" Caption="NAMA BARANG" Width="60%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="JML" VisibleIndex="4" Width="10%">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="IDPENERIMAAN" ReadOnly="True" VisibleIndex="0" Visible="false">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn FieldName="TGL_PENERIMAAN" VisibleIndex="1" Caption="TGL INPUT">
                                <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                </PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                            <dx:GridViewDataTextColumn FieldName="CATATAN" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>

                        </Columns>
                        <SettingsDetail ShowDetailRow="true" />
                    </dx:ASPxGridView>
                </div>
            </div>
        </div>

    </div>

    </div>

           <asp:SqlDataSource ID="sqlPenerimaan" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
               DeleteCommand="DELETE FROM [TBL_PENERIMAAN_BRG] WHERE [IDPENERIMAAN] = @IDPENERIMAAN"
               InsertCommand="INSERT INTO [TBL_PENERIMAAN_BRG] ([IDPENERIMAAN], [TGL_PENERIMAAN], [CATATAN], [TGL_TAMBAH]) VALUES (@IDPENERIMAAN, @TGL_PENERIMAAN, @CATATAN, @TGL_TAMBAH)"
               SelectCommand="SELECT [IDPENERIMAAN], [TGL_PENERIMAAN], [CATATAN], [TGL_TAMBAH] FROM [TBL_PENERIMAAN_BRG] WHERE KDUNKER=@KDUNKER"
               UpdateCommand="UPDATE [TBL_PENERIMAAN_BRG] SET [TGL_PENERIMAAN] = @TGL_PENERIMAAN, [CATATAN] = @CATATAN, [TGL_TAMBAH] = @TGL_TAMBAH WHERE [IDPENERIMAAN] = @IDPENERIMAAN">
               <DeleteParameters>
                   <asp:Parameter Name="IDPENERIMAAN" Type="String" />
               </DeleteParameters>
               <InsertParameters>
                   <asp:Parameter Name="IDPENERIMAAN" Type="String" />
                   <asp:Parameter Name="TGL_PENERIMAAN" Type="DateTime" />
                   <asp:Parameter Name="CATATAN" Type="String" />
                   <asp:Parameter Name="TGL_TAMBAH" Type="DateTime" />
               </InsertParameters>
               <SelectParameters>
                   <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />
               </SelectParameters>
               <UpdateParameters>
                   <asp:Parameter Name="TGL_PENERIMAAN" Type="DateTime" />
                   <asp:Parameter Name="CATATAN" Type="String" />
                   <asp:Parameter Name="TGL_TAMBAH" Type="DateTime" />
                   <asp:Parameter Name="IDPENERIMAAN" Type="String" />
               </UpdateParameters>
           </asp:SqlDataSource>



    <asp:SqlDataSource ID="sqlPenerimaanDetail" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_PENERIMAAN_BRG_DTL] WHERE [IDPENERIMAANDTL] = @IDPENERIMAANDTL"
        InsertCommand="INSERT INTO [TBL_PENERIMAAN_BRG_DTL] ([IDPENERIMAANDTL], [IDPENERIMAAN], [KODE_BARANG], [JML]) VALUES (@IDPENERIMAANDTL, @IDPENERIMAAN, @KODE_BARANG, @JML)"
        SelectCommand="SELECT [IDPENERIMAANDTL], dbo.getNAMABARANG(KODE_BARANG) AS NAMABARANG,[IDPENERIMAAN], [KODE_BARANG], [JML] FROM [TBL_PENERIMAAN_BRG_DTL] WHERE IDPENERIMAAN=@IDPENERIMAAN" UpdateCommand="UPDATE [TBL_PENERIMAAN_BRG_DTL] SET [IDPENERIMAAN] = @IDPENERIMAAN, [KODE_BARANG] = @KODE_BARANG, [JML] = @JML WHERE [IDPENERIMAANDTL] = @IDPENERIMAANDTL">
        <DeleteParameters>
            <asp:Parameter Name="IDPENERIMAANDTL" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="IDPENERIMAANDTL" Type="String" />
            <asp:Parameter Name="IDPENERIMAAN" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDPENERIMAAN" SessionField="IDPENERIMAAN" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IDPENERIMAAN" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="IDPENERIMAANDTL" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_WILAYAH='00' AND KD_STS_AKTIF=1"></asp:SqlDataSource>


</asp:Content>

