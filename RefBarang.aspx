<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="RefBarang.aspx.vb" Inherits="RefBarang" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Referensi</li>
        <li class="breadcrumb-item active">Daftar Referensi Barang</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Daftar Referensi Barang
            <small>Data Barang Persediaan.</small>
        </h1>
    </div>


    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Barang</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <%--<button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>
                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <dx:ASPxGridView ID="gridBarang" runat="server" AutoGenerateColumns="False" DataSourceID="sqlBarang" KeyFieldName="KODE_BARANG" Width="100%" OnInitNewRow="gridBarang_InitNewRow">
                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <SettingsSearchPanel Visible="True" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="1" Caption="KODE BARANG" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="2" Caption="NAMA BARANG">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="SATUAN" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataSpinEditColumn FieldName="MIN" VisibleIndex="5" Caption="STOK MIN" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                    <PropertiesSpinEdit DisplayFormatString="g" EnableFocusedStyle="False" MinValue="1" MaxValue="10000000">
                                    </PropertiesSpinEdit>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataCheckColumn FieldName="ST_BARANG" VisibleIndex="4" Caption="STATUS">
                                </dx:GridViewDataCheckColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:SqlDataSource ID="sqlBarang" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_BARANG] WHERE [KODE_BARANG] = @KODE_BARANG"
        InsertCommand="INSERT INTO [TBL_BARANG] ([KODE_BARANG], [NAMA_BARANG], [SATUAN], [ST_BARANG], [MIN]) VALUES (@KODE_BARANG, @NAMA_BARANG, @SATUAN, @ST_BARANG, @MIN)"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [SATUAN], [ST_BARANG], [MIN] FROM [TBL_BARANG] ORDER BY ST_BARANG DESC" UpdateCommand="UPDATE [TBL_BARANG] SET [NAMA_BARANG] = @NAMA_BARANG, [SATUAN] = @SATUAN, [ST_BARANG] = @ST_BARANG, [MIN] = @MIN WHERE [KODE_BARANG] = @KODE_BARANG">
        <DeleteParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="NAMA_BARANG" Type="String" />
            <asp:Parameter Name="SATUAN" Type="String" />
            <asp:Parameter Name="ST_BARANG" Type="Byte" />
            <asp:Parameter Name="MIN" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="NAMA_BARANG" Type="String" />
            <asp:Parameter Name="SATUAN" Type="String" />
            <asp:Parameter Name="ST_BARANG" Type="Byte" />
            <asp:Parameter Name="MIN" Type="Int32" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

