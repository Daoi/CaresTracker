<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CaresTracker.Master" CodeBehind="ExportData.aspx.cs" Inherits="CaresTracker.ExportData" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
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
                <h4 class="text-dark">Instructions</h4>
                <div class="row justify-content-center">
                    <p class="w-50 text-dark">
                        Select a report type and a value from the corresponding dropdown list to the right.
                        Enter start and end dates for the interactions analyzed in the report.
                    </p>
                </div>
                <div class="form-group row justify-content-center">
                    <div class="col-md-6 text-secondary">
                        <div class="row">
                            <div class="col">
                                <asp:Label ID="lblReportType" runat="server" class="required">Report Type: </asp:Label>
                                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="D">Development</asp:ListItem>
                                    <asp:ListItem Value="O">Organization</asp:ListItem>
                                    <asp:ListItem Value="C">CHW</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col">
                                <%-- Change this depending on the report type selected --%>
                                <asp:UpdatePanel ID="updtReportDomain" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Label ID="lblReportDomain" runat="server" CssClass="required" Text=""></asp:Label>
                                        <asp:DropDownList ID="ddlReportDomain" CssClass="form-control w-100" runat="server">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlReportType" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col">
                                <label class="required">Start Date: </label><br />
                                <asp:TextBox ID="txtDateStart" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox><br />
                            </div>
                            <div class="col">
                                <label class="required">End Date: </label><br />
                                <asp:TextBox ID="txtDateEnd" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox><br />
                            </div>
                        </div>
                        <asp:Label ID="lblError" runat="server" CssClass="errorLabel" Visible="false"></asp:Label>
                    </div>
                </div>
                <asp:Button ID="btnSubmit" runat="server" Text="Generate Report" CssClass="buttonStyle" OnClick="btnSubmit_Click" />
            </div>
            <div class="card-footer text-muted">
                PHA CARES Tracker
            </div>
        </div>
    </div>
    <script>
        function setupSelect2() {
            $('#MainContent_ddlReportDomain').select2({
                allowClear: false,
                selectOnClose: true
            });
            $('#MainContent_ddlReportType').select2({
                allowClear: false,
                selectOnClose: true
            });
        }
    </script>
</asp:Content>
