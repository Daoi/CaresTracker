<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="CHWManagement.aspx.cs" Inherits="CapstoneUI.CHWManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage backgroundblue">
            <div class="row modal-header no-gutters" style="height: auto; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-white">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                        <li class="breadcrumb-item active bg-white" aria-current="page">CHW Management</li>
                    </ol>
                </nav>
                <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 60%">CHW Management</asp:Label>
            </div>
        <div class="row m-0 p-0">
            <div class="col p-2 bg-secondary text-white">
                <label>Search Name: </label>
                <asp:TextBox ID="txtName" runat="server" Text="John Doe"></asp:TextBox>
            </div>
        </div>
        <div class="row p-5">
            <!-- Section 1 Start -->
            <div class="col">
                <div style="font-size: larger">
                    Name:
                    <asp:Label ID="lblCHWName" runat="server" Text="John Doe" Style="font-weight: bold; font-size: larger"></asp:Label>
                </div>
                <%-- CHW Picture --%>
                <div class="chwInfo" style="width:22vw">
                    <asp:Image ID="imgCHW" runat="server" ImageUrl="https://icon-library.com/images/default-profile-icon/default-profile-icon-16.jpg" CssClass="chwImg" />
                </div>
                <br />
                <div class="mx-auto">
                    <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate Account" CssClass="btn-primary btn-lg btn-warning mx-auto" />
                    <br />
                    <br />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete Account" CssClass="btn-primary btn-lg btn-danger mx-auto" />
                </div>
            </div>
            <!-- Section 1 End -->
            <!-- Section 2 Start -->
            <div class="col">
                <div class="col-md-3  col-sm-1">
                    <h3 style="margin-left: 1em">CHW Work Area </h3>
                </div>
                <div class="col-md-3  col-sm-2">
                    <div class="text-center">
                        CHW Region:
                        <asp:DropDownList ID="ddlRegions" CssClass="form-control" runat="server">
                            <asp:ListItem>North Philadelphia</asp:ListItem>
                            <asp:ListItem>West Philadelphia</asp:ListItem>
                            <asp:ListItem>North West Philadelphia</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 col-sm-2">
                    <div class="text-center">
                        CHW Supervisor:
                        <asp:DropDownList ID="ddlSupervisor" CssClass="form-control" runat="server">
                            <asp:ListItem>Linda</asp:ListItem>
                            <asp:ListItem>Gaye</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-2 col-sm-2">
                    <div class="text-center">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-primary" Width="107px" />
                    </div>
                </div>
                <div class="row form-inline form-group ">
                    <div class="textContainerThree">
                        <div class="col-6-md offset-1 my-5">
                            <h5>Quick Stats</h5>
                            <span>Interactions this Week: 3
                            <asp:Label ID="lblWeekInteractions" runat="server" Text=""></asp:Label></span><br />
                            <span>Total Interactions: 28
                            <asp:Label ID="lblTotalInteractions" runat="server" Text=""></asp:Label></span>
                            <asp:Button ID="btnViewInteractions" runat="server" Text="View Interactions" CssClass="btn-primary btn-lg" OnClick="btnViewInteractions_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Section 2 End -->
        </div>
    </div>
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
</asp:Content>
