<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="RefMitra.aspx.vb" Inherits="RefMitra" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Referensi</li>
        <li class="breadcrumb-item active">Daftar Referensi Mitra</li>
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
                        <dx:ASPxGridView ID="gridMitra" runat="server" AutoGenerateColumns="False" DataSourceID="sqlMitra" KeyFieldName="KODE_MITRA" Width="100%">
                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <SettingsSearchPanel Visible="True" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="KODE_MITRA" ReadOnly="True" VisibleIndex="1" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="BIDANG" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_MITRA" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ALAMAT" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_KONTAK" VisibleIndex="5">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NPWP" VisibleIndex="6">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EMAIL" VisibleIndex="7">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:SqlDataSource ID="sqlMitra" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_MITRA] WHERE [KODE_MITRA] = @KODE_MITRA"
        InsertCommand="INSERT INTO [TBL_MITRA] ([BIDANG], [NAMA_MITRA], [ALAMAT], [NAMA_KONTAK], [NPWP], [EMAIL]) VALUES (@BIDANG, @NAMA_MITRA, @ALAMAT, @NAMA_KONTAK, @NPWP, @EMAIL)"
        SelectCommand="SELECT [KODE_MITRA], [BIDANG], [NAMA_MITRA], [ALAMAT], [NAMA_KONTAK], [NPWP], [EMAIL] FROM [TBL_MITRA]" UpdateCommand="UPDATE [TBL_MITRA] SET [BIDANG] = @BIDANG, [NAMA_MITRA] = @NAMA_MITRA, [ALAMAT] = @ALAMAT, [NAMA_KONTAK] = @NAMA_KONTAK, [NPWP] = @NPWP, [EMAIL] = @EMAIL WHERE [KODE_MITRA] = @KODE_MITRA">
        <DeleteParameters>
            <asp:Parameter Name="KODE_MITRA" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="BIDANG" Type="String" />
            <asp:Parameter Name="NAMA_MITRA" Type="String" />
            <asp:Parameter Name="ALAMAT" Type="String" />
            <asp:Parameter Name="NAMA_KONTAK" Type="String" />
            <asp:Parameter Name="NPWP" Type="String" />
            <asp:Parameter Name="EMAIL" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="BIDANG" Type="String" />
            <asp:Parameter Name="NAMA_MITRA" Type="String" />
            <asp:Parameter Name="ALAMAT" Type="String" />
            <asp:Parameter Name="NAMA_KONTAK" Type="String" />
            <asp:Parameter Name="NPWP" Type="String" />
            <asp:Parameter Name="EMAIL" Type="String" />
            <asp:Parameter Name="KODE_MITRA" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>



</asp:Content>

