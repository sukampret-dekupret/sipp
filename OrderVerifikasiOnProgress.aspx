<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="OrderVerifikasiOnProgress.aspx.vb" Inherits="OrderVerifikasiOnProgress" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Verifikasi</li>
        <li class="breadcrumb-item active">On Progress</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Permohonan Barang
            <small>Daftar Permohonan Barang Persediaan.</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-8">
            <div id="panel-atas-1" class="panel">
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">SATUAN KERJA/UNIT KERJA</label>
                            <asp:DropDownList ID="cboUnitKerja" runat="server" CssClass="form-control" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" AutoPostBack="True" DataSourceID="sqlUnker"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
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


    </div>


    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Daftar  <span class="fw-300"><i>Permohonan Satker On Progress</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <%--<button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>
                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="panel-tag">
                            <p>
                                Klik tanda <code>[+]</code> untuk melihat data barang
                            </p>
                        </div>
                        <dx:ASPxGridView ID="grid" runat="server"
                            AutoGenerateColumns="False" DataSourceID="sqlOrder"
                            KeyFieldName="KDORDER" Width="100%">

                            <SettingsAdaptivity>
                                <AdaptiveDetailLayoutProperties ColCount="1"></AdaptiveDetailLayoutProperties>
                            </SettingsAdaptivity>

                            <Templates>
                                <DetailRow>

                                    <dx:ASPxGridView ID="gridDetail" runat="server"
                                        DataSourceID="sqlOrderDetail"
                                        OnBeforePerformDataSelect="ASPxGridView1_BeforePerformDataSelect"
                                        AutoGenerateColumns="False" KeyFieldName="IDETAILORDER"
                                        SettingsEditing-Mode="Inline"
                                        OnHtmlRowPrepared="gridDetail_HtmlRowPrepared"
                                        OnCommandButtonInitialize="gridDetail_CommandButtonInitialize"
                                        Width="100%" OnDataBound="gridDetail_DataBound"
                                        OnRowValidating="gridDetail_RowValidating"
                                        EnableCallBacks="False">
                                        <SettingsAdaptivity>
                                            <AdaptiveDetailLayoutProperties ColCount="1">
                                            </AdaptiveDetailLayoutProperties>
                                        </SettingsAdaptivity>
                                        <SettingsEditing Mode="Batch">
                                            <BatchEditSettings KeepChangesOnCallbacks="False" />
                                        </SettingsEditing>
                                        <SettingsPopup>
                                            <HeaderFilter MinHeight="140px">
                                            </HeaderFilter>
                                        </SettingsPopup>
                                        <SettingsSearchPanel Visible="True" />
                                        <EditFormLayoutProperties ColCount="1">
                                        </EditFormLayoutProperties>
                                        <Columns>
                                            <dx:GridViewCommandColumn VisibleIndex="0">
                                            </dx:GridViewCommandColumn>
                                            <dx:GridViewDataTextColumn FieldName="IDETAILORDER" ReadOnly="True" VisibleIndex="1" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="NAMA_BARANG" VisibleIndex="2" Caption="NAMA BARANG" ReadOnly="true">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="MOHON" VisibleIndex="3" ReadOnly="true">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataSpinEditColumn FieldName="PENUHI" VisibleIndex="4">
                                                <PropertiesSpinEdit DisplayFormatString="g" MaxValue="1000000" MinValue="0" AllowNull="false"></PropertiesSpinEdit>
                                            </dx:GridViewDataSpinEditColumn>
                                            <dx:GridViewDataTextColumn FieldName="STOK2" ReadOnly="True" VisibleIndex="5" Caption="STOK UNIT">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="STOK1" ReadOnly="True" VisibleIndex="6" Caption="STOK SATKER">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataTextColumn>

                                            <dx:GridViewDataComboBoxColumn Caption="KET" FieldName="IDFLAG" VisibleIndex="7">
                                                <EditFormSettings Visible="False" />
                                                <PropertiesComboBox DataSourceID="sqlIDflag" TextField="KETERANGAN" ValueField="IDFLAG"></PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <Settings ShowPreview="True" />

                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>

                            <SettingsSearchPanel Visible="True" />
                            <SettingsDetail ShowDetailRow="true" />

                            <EditFormLayoutProperties ColCount="1"></EditFormLayoutProperties>
                            <Columns>
                                <dx:GridViewCommandColumn VisibleIndex="0" Visible="false">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="KDORDER" ReadOnly="True"
                                    VisibleIndex="1" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NO_NOTA" VisibleIndex="2" Caption="NO NOTA" Width="10%">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_NOTA" VisibleIndex="3" Caption="TGL NOTA">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="NAMA_UNKER" VisibleIndex="4" Caption="NAMA SATUAN KERJA">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PERIHAL" VisibleIndex="4" Caption="PERIHAL">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PROGRESS" VisibleIndex="4" Caption="PROGRESS">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <CellStyle HorizontalAlign="Center"></CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="5" Visible="false">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnProses" runat="server" Text="Proses ke Gudang" OnClientClick="ConfirmProses()" OnClick="btnProses_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CEKORDER" VisibleIndex="7" Visible="false">
                                    <EditFormSettings Visible="False" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles AlternatingRow-Enabled="True" AlternatingRow-BackColor="#554A7C" AlternatingRow-ForeColor="White">
                                <Header CssClass="thead-dark"></Header>

                                <AlternatingRow Enabled="True" BackColor="#554A7C" ForeColor="White"></AlternatingRow>

                            </Styles>
                        </dx:ASPxGridView>


                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:SqlDataSource ID="sqlOrder" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KDORDER], [NO_NOTA], [TGL_NOTA], [TGL_TAMBAH], B.NAMA_UNKER, [PERIHAL],dbo.GetStatusORDER(KDORDER) as [STATUS],dbo.cekORDERlebih(A.KDORDER) as CEKORDER,dbo.getJMLPENUHIORDERperKDORDER(KDORDER) AS TOTALPENUHI,CONCAT(dbo.getJMLPENUHIORDERperKDORDER(KDORDER),' / ', dbo.getJMLSERTERORDERperKDORDER(KDORDER)) AS PROGRESS
FROM [TBL_ORDER] A
INNER JOIN TBL_UNKER B
ON A.DRUNKER=B.KD_UNKER WHERE YEAR(TGL_NOTA)=@TAHUN AND A.KEUNKER=@KEUNKER AND dbo.GetStatusORDER(KDORDER)=3">
        <SelectParameters>
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KEUNKER" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="sqlOrderDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [IDETAILORDER], dbo.GetNAMABARANG(KODE_BARANG) AS NAMA_BARANG, [MOHON], [PENUHI], dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG, @KDUNKER, GETDATE(), 0,1) - dbo.getJMLMOHONperKDBRGperUNKER(KODE_BARANG, @KDUNKER, GETDATE()) AS STOK2,(dbo.getJMLSERTERSPKperKDBRGperUNKER(KODE_BARANG , @KDUNKER1 , getdate()) - dbo.getJMLSERTERORDERperKDBRGperUNKER(KODE_BARANG , @KDUNKER1 , getdate() , 1,0))  as STOK1,[KDUNKER],IDFLAG,CEK1,CEK2,CEK3,dbo.getJMLSERTERTperKDBRGperORDER(KODE_BARANG,KDORDER) AS TERIMA  FROM [TBL_ORDER_DETAIL] WHERE KDORDER=@KDORDER"
        DeleteCommand="DELETE FROM [TBL_ORDER_DETAIL] WHERE [IDETAILORDER] = @IDETAILORDER"
        InsertCommand="INSERT INTO [TBL_ORDER_DETAIL] ([IDETAILORDER], [KODE_BARANG], [MOHON], [PENUHI], [STOK], [TGLACC], [KDUNKER], [NAMAACC]) VALUES (@IDETAILORDER, @KODE_BARANG, @MOHON, @PENUHI, @STOK, @TGLACC, @KDUNKER, @NAMAACC)"
        UpdateCommand="UPDATE [TBL_ORDER_DETAIL] SET  [PENUHI] = @PENUHI WHERE [IDETAILORDER] = @IDETAILORDER">
        <DeleteParameters>
            <asp:Parameter Name="IDETAILORDER" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="IDETAILORDER" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="MOHON" Type="Int32" />
            <asp:Parameter Name="PENUHI" Type="Int32" />
            <asp:Parameter Name="STOK" Type="Int32" />
            <asp:Parameter Name="TGLACC" Type="DateTime" />
            <asp:Parameter Name="KDUNKER" Type="String" />
            <asp:Parameter Name="NAMAACC" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKER" />
            <asp:SessionParameter Name="KDORDER" SessionField="KDORDER" />
            <asp:SessionParameter Name="KDUNKER1" SessionField="KDUNKERATAS" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="PENUHI" Type="Int32" />
            <asp:Parameter Name="IDETAILORDER" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlUnker" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_STS_AKTIF=1 AND KD_WILAYAH='00'"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlIDflag" runat="server" DataSourceMode="DataReader" ConnectionString="<%$ ConnectionStrings:conPersediaan %>" SelectCommand="SELECT [IDFLAG], [KETERANGAN] FROM [TBL_R_FLAG]"></asp:SqlDataSource>

</asp:Content>

