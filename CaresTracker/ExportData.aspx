<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CaresTracker.Master" CodeBehind="ExportData.aspx.cs" Inherits="CaresTracker.ExportData" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContainer justify-content-center d-flex py-5">
        <div class="card mb-3 text-center loginCard my-auto ">
            <div class="card-header cherryBackground" style="font-size: 18px">
                Export Data
            </div>
            <div class="card-body justify-content-center">
                <div class="row" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb" class="mb-n3 ml-2">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="./Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">Data Extracts</li>
                        </ol>
                    </nav>
                </div>

                <div class="form-group row justify-content-center">
                    <div class="col-md-6 text-secondary">
                        Region:<br />
                        <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server">
                            <asp:ListItem>North Philadelphia</asp:ListItem>
                            <asp:ListItem>West Philadelphia</asp:ListItem>
                            <asp:ListItem>South Philadelphia</asp:ListItem>
                            <asp:ListItem>Greater Philadelphia</asp:ListItem>
                        </asp:DropDownList><br />
                        Initial Date:<br />
                        <asp:TextBox ID="txtDateInitial" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox><br />
                        Final Date:<br />
                        <asp:TextBox ID="txtDateFinal" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox><br />
                    </div>
                </div>
                <asp:Button ID="btnSubmitImport" runat="server" Text="Generate Report" CssClass="buttonStyle" />
            </div>
            <div class="card-footer text-muted">
                CARES Tracker
            </div>
        </div>
    </div>
</asp:Content>
