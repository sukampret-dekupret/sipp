<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="Pengadaan.aspx.vb" Inherits="Pengadaan" %>

<%@ Register Assembly="DevExpress.Web.v19.2, Version=19.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Pengadaan Pusat</li>
        <li class="breadcrumb-item active">Daftar Pengadaan</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Daftar Pengadaan Barang
            <small>Data Pengadaan Barang Persediaan.</small>
        </h1>
    </div>

    <div class="row">
        <div class="col-lg-6">
            <div id="panel-atas-1" class="panel">
                <%--                <div class="panel-hdr">
                </div>--%>
                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            <label class="form-label" for="simpleinput">UNIT KERJA </label>
                            <%--<asp:DropDownList ID="cboUnitKerja" runat="server" CssClass="form-control" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" AutoPostBack="true"></asp:DropDownList>--%>
                            <asp:DropDownList ID="cboUnitKerja" runat="server" AutoPostBack="True" DataSourceID="sqlUnker" DataTextField="NAMA_UNKER" DataValueField="KD_UNKER" CssClass="form-control"></asp:DropDownList>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-6">
            <div id="panel-atas-2" class="panel">
                <%--                <div class="panel-hdr">
                    

                </div>--%>
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
        <div class="col-sm-6 col-xl-3">
            <div class="p-3 bg-primary-300 rounded overflow-hidden position-relative text-white mb-g">
                <div class="">
                    <h3 class="display-4 d-block l-h-n m-0 fw-500">
                        <asp:Literal ID="wAllBeli" runat="server" EnableViewState="false" Text="0"></asp:Literal>
                        <small class="m-0 l-h-n">Semua Pembelian</small>
                    </h3>
                </div>
                <i class="fal fa-user position-absolute pos-right pos-bottom opacity-15 mb-n1 mr-n1" style="font-size: 6rem"></i>
            </div>
        </div>
        <div class="col-sm-6 col-xl-3">
            <div class="p-3 bg-warning-400 rounded overflow-hidden position-relative text-white mb-g">
                <div class="">
                    <h3 class="display-4 d-block l-h-n m-0 fw-500">
                        <asp:Literal ID="wBelumDiterima" runat="server" EnableViewState="false" Text="0"></asp:Literal>
                        <small class="m-0 l-h-n">Belum Diterima</small>
                    </h3>
                </div>
                <i class="fal fa-gem position-absolute pos-right pos-bottom opacity-15  mb-n1 mr-n4" style="font-size: 6rem;"></i>
            </div>
        </div>
        <div class="col-sm-6 col-xl-3">
            <div class="p-3 bg-success-200 rounded overflow-hidden position-relative text-white mb-g">
                <div class="">
                    <h3 class="display-4 d-block l-h-n m-0 fw-500"><asp:Literal ID="wSebagian" runat="server" EnableViewState="false" Text="0"></asp:Literal>
                         <small class="m-0 l-h-n">Sebagian Diterima</small>
                    </h3>
                </div>
                <i class="fal fa-lightbulb position-absolute pos-right pos-bottom opacity-15 mb-n5 mr-n6" style="font-size: 8rem;"></i>
            </div>
        </div>
        <div class="col-sm-6 col-xl-3">
            <div class="p-3 bg-info-200 rounded overflow-hidden position-relative text-white mb-g">
                <div class="">
                    <h3 class="display-4 d-block l-h-n m-0 fw-500"><asp:Literal ID="wSelesai" runat="server" EnableViewState="false" Text="0"></asp:Literal>
                        <small class="m-0 l-h-n">Sudah Diterima Semua</small>
                    </h3>
                </div>
                <i class="fal fa-globe position-absolute pos-right pos-bottom opacity-15 mb-n1 mr-n4" style="font-size: 6rem;"></i>
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
                        <dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="sqlSPK" KeyFieldName="IDSPK" Width="100%" OnHtmlRowPrepared="grid_HtmlRowPrepared" 
                            OnCommandButtonInitialize="grid_CommandButtonInitialize">
                            <SettingsPopup>
                                <HeaderFilter MinHeight="140px"></HeaderFilter>
                            </SettingsPopup>
                            <Templates>
                                <DetailRow>
                                    <dx:ASPxGridView ID="gridDetail" runat="server" AutoGenerateColumns="False"
                                        DataSourceID="sqlSPKDetail" KeyFieldName="IDSPKDETAIL" OnBeforePerformDataSelect="gridDetail_BeforePerformDataSelect" Width="100%" OnHtmlRowPrepared="gridDetail_HtmlRowPrepared" OnCommandButtonInitialize="gridDetail_CommandButtonInitialize">
                                        <SettingsPopup>
                                            <HeaderFilter MinHeight="140px">
                                            </HeaderFilter>
                                        </SettingsPopup>
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="IDSPKDETAIL" ReadOnly="True" VisibleIndex="0" Visible="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataComboBoxColumn FieldName="KODE_BARANG" VisibleIndex="2" Caption="NAMA BARANG" Width="60%">
                                                <PropertiesComboBox DataSourceID="sqlBarang" TextField="NAMA_BARANG" ValueField="KODE_BARANG" DisplayFormatString="{0}">
                                                </PropertiesComboBox>
                                            </dx:GridViewDataComboBoxColumn>
                                            <dx:GridViewDataTextColumn FieldName="KODE_BARANG" VisibleIndex="1" Caption="KODE BARANG" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="JML" VisibleIndex="3" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="DIAMBIL" VisibleIndex="4" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center" Caption="DITERIMA">
                                            </dx:GridViewDataTextColumn>
                                            <%--                                            <dx:GridViewDataDateColumn FieldName="TGL_TAMBAH" VisibleIndex="4">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataDateColumn FieldName="TGL_PROSES" VisibleIndex="5">
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn FieldName="NAMAUSER" VisibleIndex="6">
                                            </dx:GridViewDataTextColumn>--%>
                                        </Columns>
                                    </dx:ASPxGridView>
                                </DetailRow>
                            </Templates>
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsCommandButton>
                                <NewButton Text="Tambah">
                                </NewButton>
                                <DeleteButton Text="Batal">
                                </DeleteButton>
                            </SettingsCommandButton>
                            <SettingsText ConfirmDelete="Apakah anda yakin akan dibatalkan?" />
                            <SettingsDetail ShowDetailRow="true" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="NOFAKTUR" VisibleIndex="1" Caption="NO FAKTUR" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL_FAKTUR" VisibleIndex="2" Caption="TGL FAKTUR" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="TGL" VisibleIndex="3" Caption="TGL INPUT" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                    <EditFormSettings Visible="False" />
                                    <PropertiesDateEdit DisplayFormatString="dd MMM yyyy">
                                    </PropertiesDateEdit>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="PERIHAL" VisibleIndex="4" Caption="PERIHAL / KETERANGAN">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="AKSI" VisibleIndex="7">
                                    <EditFormSettings Visible="False" />
                                    <DataItemTemplate>
                                        <asp:Button ID="btnUnduh" runat="server" Text="Lihat Nota" OnClick="btnUnduh_Click" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataComboBoxColumn FieldName="KODE_MITRA" VisibleIndex="5" Caption="MITRA / SUPPLIER">
                                    <PropertiesComboBox DataSourceID="sqlMitra" TextField="NAMA_MITRA" ValueField="KODE_MITRA">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataProgressBarColumn Caption="STATUS" FieldName="Progress" ReadOnly="True" VisibleIndex="6">
                                    <PropertiesProgressBar DisplayFormatString="" IndicatorStyle-CssClass="progress-bar">
                                        <IndicatorStyle CssClass="progress-bar"></IndicatorStyle>
                                    </PropertiesProgressBar>
                                </dx:GridViewDataProgressBarColumn>
                                <dx:GridViewDataTextColumn FieldName="ISDELETED" VisibleIndex="8" Visible="false">
                                </dx:GridViewDataTextColumn>

                            </Columns>
                        </dx:ASPxGridView>

                    </div>
                </div>
            </div>
        </div>

    </div>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

    <asp:SqlDataSource ID="sqlBarang" DataSourceMode="DataReader" runat="server" ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        DeleteCommand="DELETE FROM [TBL_BARANG] WHERE [KODE_BARANG] = @KODE_BARANG"
        InsertCommand="INSERT INTO [TBL_BARANG] ([KODE_BARANG], [NAMA_BARANG], [KETERANGAN], [SATUAN]) VALUES (@KODE_BARANG, @NAMA_BARANG, @KETERANGAN, @SATUAN)"
        SelectCommand="SELECT [KODE_BARANG], [NAMA_BARANG], [KETERANGAN], [SATUAN] FROM [TBL_BARANG] WHERE ST_BARANG=1"
        UpdateCommand="UPDATE [TBL_BARANG] SET [NAMA_BARANG] = @NAMA_BARANG, [KETERANGAN] = @KETERANGAN, [SATUAN] = @SATUAN WHERE [KODE_BARANG] = @KODE_BARANG">
        <DeleteParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="NAMA_BARANG" Type="String" />
            <asp:Parameter Name="KETERANGAN" Type="String" />
            <asp:Parameter Name="SATUAN" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="NAMA_BARANG" Type="String" />
            <asp:Parameter Name="KETERANGAN" Type="String" />
            <asp:Parameter Name="SATUAN" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlSPK" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [NOFAKTUR], [TGL_FAKTUR], [PERIHAL], [KODE_MITRA], [IDSPK], [TGL],dbo.GetStatusSPK(IDSPK) AS STSSPK,dbo.GetProgressPembelian(IDSPK) as Progress,ISDELETED FROM [TBL_SPK] WHERE YEAR(TGL)=@TAHUN AND KDUNKER=@KDUNKER"
        DeleteCommand="DELETE FROM TBL_SPK  WHERE IDSPK = @IDSPK"
        InsertCommand="INSERT INTO [TBL_SPK] ([NOFAKTUR], [TGL_FAKTUR], [PERIHAL], [KODE_MITRA]) VALUES (@NOFAKTUR, @TGL_FAKTUR, @PERIHAL, @KODE_MITRA)"
        UpdateCommand="UPDATE [TBL_SPK] SET [NOFAKTUR] = @NOFAKTUR, [TGL_FAKTUR] = @TGL_FAKTUR, [PERIHAL] = @PERIHAL, [KODE_MITRA] = @KODE_MITRA WHERE [IDSPK] = @IDSPK">
        <DeleteParameters>
            <asp:Parameter Name="IDSPK" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="NOFAKTUR" Type="String" />
            <asp:Parameter Name="TGL_FAKTUR" Type="DateTime" />
            <asp:Parameter Name="PERIHAL" Type="String" />
            <asp:Parameter Name="KODE_MITRA" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="cboTahun" Name="TAHUN" PropertyName="SelectedValue" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" />

        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="NOFAKTUR" Type="String" />
            <asp:Parameter Name="TGL_FAKTUR" Type="DateTime" />
            <asp:Parameter Name="PERIHAL" Type="String" />
            <asp:Parameter Name="KODE_MITRA" Type="Int32" />
            <asp:Parameter Name="IDSPK" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlSPKDetail" runat="server"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [IDSPKDETAIL], [IDSPK], [KODE_BARANG], [JML], [TGL_TAMBAH], [TGL_PROSES], [NAMAUSER], [KDUNKER],dbo.getJMLSERTERTperKDBRGperSPK(KODE_BARANG,IDSPK) AS DIAMBIL,[HARGA],[JML]*[HARGA] AS [TOTAL] FROM [TBL_SPK_DETAIL] WHERE IDSPK=@IDSPK"
        DeleteCommand="UPDATE TBL_SPK_DETAIL SET ISDELETED=1 WHERE [IDSPKDETAIL] = @IDSPKDETAIL"
        InsertCommand="INSERT INTO [TBL_SPK_DETAIL] ( [IDSPK], [KODE_BARANG], [JML], [TGL_PROSES], [NAMAUSER], [KDUNKER],  [HARGA]) VALUES (@IDSPK, @KODE_BARANG, @JML,  GETDATE(), @NAMAUSER, @KDUNKER, @HARGA)"
        UpdateCommand="UPDATE [TBL_SPK_DETAIL] SET [KODE_BARANG] = @KODE_BARANG, [JML] = @JML,[HARGA]=@HARGA WHERE [IDSPKDETAIL] = @IDSPKDETAIL">
        <DeleteParameters>
            <asp:Parameter Name="IDSPKDETAIL" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" Type="String" />
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:SessionParameter Name="NAMAUSER" SessionField="NAMAUSER" Type="String" />
            <asp:ControlParameter ControlID="cboUnitKerja" Name="KDUNKER" PropertyName="SelectedValue" Type="String" />
            <asp:Parameter Name="HARGA" Type="Decimal" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter Name="IDSPK" SessionField="IDSPK" />
            <asp:SessionParameter Name="KDUNKER" SessionField="KDUNKERATAS" />
            <asp:SessionParameter Name="KDUNKER1" SessionField="KDUNKER" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="KODE_BARANG" Type="String" />
            <asp:Parameter Name="JML" Type="Int32" />
            <asp:Parameter Name="HARGA" Type="Decimal" />
            <asp:Parameter Name="IDSPKDETAIL" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlMitra" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KODE_MITRA], [NAMA_MITRA] FROM [TBL_MITRA]"></asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlUnker" runat="server" DataSourceMode="DataReader"
        ConnectionString="<%$ ConnectionStrings:conPersediaan %>"
        SelectCommand="SELECT [KD_UNKER], [NAMA_UNKER] FROM [TBL_UNKER] WHERE KD_WILAYAH='00' AND KD_STS_AKTIF=1"></asp:SqlDataSource>
    <br />
    

</asp:Content>

