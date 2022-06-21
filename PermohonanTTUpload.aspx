<%@ Page Title="" Language="VB" MasterPageFile="~/Site.master" AutoEventWireup="false" CodeFile="PermohonanTTUpload.aspx.vb" Inherits="PermohonanTTUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ol class="breadcrumb page-breadcrumb">
        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
        <li class="breadcrumb-item">Permohonan</li>
        <li class="breadcrumb-item active">Permohonan Upload</li>
        <li class="position-absolute pos-top pos-right d-none d-sm-block"><span class="js-get-date"></span></li>
    </ol>

    <div class="subheader">
        <h1 class="subheader-title">
            <i class='fal fa-th-list text-primary'></i>Permohonan Barang
            <small>Upload Permohonan Barang Persediaan.</small>
        </h1>

    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="panel-1" class="panel">
                <div class="panel-hdr">
                    <h2>Upload  <span class="fw-300"><i>Tanda Terima</i></span>
                    </h2>
                    <div class="panel-toolbar">
                        <button class="btn btn-panel" data-action="panel-collapse" data-toggle="tooltip" data-offset="0,10" data-original-title="Collapse"></button>
                        <button class="btn btn-panel" data-action="panel-fullscreen" data-toggle="tooltip" data-offset="0,10" data-original-title="Fullscreen"></button>
                        <%--<button class="btn btn-panel" data-action="panel-close" data-toggle="tooltip" data-offset="0,10" data-original-title="Close"></button>--%>
                    </div>
                </div>

                <div class="panel-container show">
                    <div class="panel-content">
                        <div class="form-group">
                            
                                <label class="col-sm-2 col-form-label">File Tanda Terima</label>

                                <div class="col-sm-10">
                                    <asp:FileUpload ID="btnFileUpload" runat="server" accept=".pdf" />
                                </div>
                            
                        </div>
                        <div class="hr-line-dashed"></div>
                        <div class="mb-3">
                            <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Proses" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

