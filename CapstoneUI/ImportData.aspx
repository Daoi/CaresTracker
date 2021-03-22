<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="ImportData.aspx.cs" Inherits="CapstoneUI.ImportData" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContainer justify-content-center d-flex py-5">
        <div class="card mb-3 text-center loginCard my-auto ">
            <div class="card-header cherryBackground" style="font-size: 18px">
                Import Data
            </div>
            <div class="card-body justify-content-center">
                <div class="row" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb" class="mb-n3 ml-2">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="./Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">Import Data</li>
                        </ol>
                    </nav>
                </div>

                <div class="form-group row justify-content-center">
                    <div class="col-md-6 text-secondary">
                        Select file:<br />
                        <asp:FileUpload runat="server"></asp:FileUpload>
                    </div>
                </div>
                <asp:Button ID="btnSubmitImport" runat="server" Text="Import Resident List" CssClass="btn btn-lg cherryBackground" />
            </div>
            <div class="card-footer text-muted">
            CARES Tracker
            </div>
        </div>
    </div>
</asp:Content>
