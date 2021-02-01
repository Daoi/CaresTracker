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
                <asp:Label ID="lblCHWName" runat="server" Text="John Doe" CssClass="h3"></asp:Label>
                <%-- CHW Picture --%>
                <div class="chwInfo" style="width: 22vw">
                    <asp:Image ID="imgCHW" runat="server" ImageUrl="https://icon-library.com/images/default-profile-icon/default-profile-icon-16.jpg" CssClass="chwImg" />
                </div>
                <br />
                <div class="p-3" style="width: 22vw; background-color: lightgray">
                    <h3>Quick Stats</h3>
                    <span>Interactions this Week: 3
                            <asp:Label ID="lblWeekInteractions" runat="server" Text=""></asp:Label></span><br />
                    <span>Total Interactions: 28
                            <asp:Label ID="lblTotalInteractions" runat="server" Text=""></asp:Label></span>
                    <br />
                    <br />
                    <asp:Button ID="btnViewInteractions" runat="server" Text="View Interactions" CssClass="btn-primary btn" OnClick="btnViewInteractions_Click" />
                </div>
                
            </div>
            <!-- Section 1 End -->
            <!-- Section 2 Start -->
            <div class="col">
                <h3>Work Area</h3>
                <br />
                <div class="row">
                    <div class="col text-center">
                        CHW Region
                            <asp:DropDownList ID="ddlRegions" CssClass="form-control" runat="server">
                                <asp:ListItem>North Philadelphia</asp:ListItem>
                                <asp:ListItem>West Philadelphia</asp:ListItem>
                                <asp:ListItem>North West Philadelphia</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                    <div class=" col text-center">
                        CHW Supervisor
                            <asp:DropDownList ID="ddlSupervisor" CssClass="form-control" runat="server">
                                <asp:ListItem>Linda</asp:ListItem>
                                <asp:ListItem>Gaye</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="text-center">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-primary btn" Width="107px" />
                </div>
                <br />

                <h3>Account Management</h3>
                <div class="mx-auto">
                    <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" CssClass="btn-primary btn-lg btn-warning mr-3" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn-primary btn-lg btn-danger" />
                </div>
                
            </div>
            <!-- Section 2 End -->
        </div>
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
</asp:Content>
