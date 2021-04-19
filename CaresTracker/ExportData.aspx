<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CaresTracker.Master" CodeBehind="ExportData.aspx.cs" Inherits="CaresTracker.ExportData" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContainer justify-content-center d-flex py-5">
        <div class="card mb-3 text-center loginCard my-auto ">
            <div class="card-header cherryBackground" style="font-size: 18px">
                Create Report
            </div>
            <div class="card-body justify-content-center">
                <div class="row" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb" class="mb-n3 ml-2">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="./Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">Create Report</li>
                        </ol>
                    </nav>
                </div>

                <div class="form-group row justify-content-center">
                    <div class="col-md-6 text-secondary">
                        Region:<br />
                        <asp:DropDownList ID="ddlHousingDevelopment" CssClass="form-control w-100" runat="server" DataTextField="DevelopmentName" DataValueField="DevelopmentID">
                        </asp:DropDownList><br /><br />
                        <label class="required">Initial Date: </label><br />
                        <asp:TextBox ID="txtDateInitial" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox><br />
                        <label class="required">Final Date: </label><br />
                        <asp:TextBox ID="txtDateFinal" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox><br />
                        <asp:Label ID="lblError" runat="server" CssClass="errorLabel" Visible="false"></asp:Label>
                    </div>
                </div>
                <asp:Button ID="btnSubmit" runat="server" Text="Generate Report" CssClass="buttonStyle" OnClick="btnSubmit_Click" />
            </div>
            <div class="card-footer text-muted">
                CARES Tracker
            </div>
        </div>
    </div>
    <script>
        $('#MainContent_ddlHousingDevelopment').select2({
            allowClear: false,
            selectOnClose: true
        });
    </script>
</asp:Content>
