<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="CHWManagement.aspx.cs" Inherits="CapstoneUI.CHWManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage backgroundblue">
        <div class="row modal-header no-gutters" style="height: auto; font-size: large">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-white">
                    <li class="breadcrumb-item" style="color: deepskyblue">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                    <li class="breadcrumb-item active bg-white" aria-current="page">Worker Management</li>
                </ol>
            </nav>
            <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 60%">Worker Management</asp:Label>
        </div>
        <div class="row p-5">
            <!-- Section 1 Start -->
            <div class="col">
                <asp:Label ID="lblWorkerName" runat="server" Text="<u>John Doe</u>" CssClass="h3"></asp:Label>
                <%-- CHW Picture --%>
                <div class="chwInfo text-center" style="width: 22vw">
                    <asp:Image ID="imgCHW" runat="server" ImageUrl="https://icon-library.com/images/default-profile-icon/default-profile-icon-16.jpg" CssClass="chwImg" />
                </div>
                <br />
                <div class="p-3" style="width: 22vw; background-color: lightgray">
                    <h3>Quick Stats</h3>
                    <span>Interactions this Week: 
                            <asp:Label ID="lblWeekInteractions" runat="server" Text="2"></asp:Label></span><br />
                    <span>Total Interactions: 
                            <asp:Label ID="lblTotalInteractions" runat="server" Text="28"></asp:Label></span>
                    <br />
                    <br />
                    <asp:Button ID="btnViewInteractions" runat="server" Text="View Interactions" CssClass="btn-primary btn" OnClick="btnViewInteractions_Click" />
                </div>
                
            </div>
            <!-- Section 1 End -->
            <!-- Section 2 Start -->
            <div class="col">
                <h3><u>Work Info</u></h3>
                <br />
                    <div class="text-left">
                        <h6>Region</h6>
                            <asp:DropDownList ID="ddlRegions" CssClass="form-control w-50" runat="server">
                                <asp:ListItem>North Philadelphia</asp:ListItem>
                                <asp:ListItem>West Philadelphia</asp:ListItem>
                                <asp:ListItem>North West Philadelphia</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                <br />
                    <div class="text-left">
                        <h6>Supervisor</h6>
                            <asp:DropDownList ID="ddlSupervisor" CssClass="form-control w-50" runat="server">
                                <asp:ListItem>Linda</asp:ListItem>
                                <asp:ListItem>Gaye</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                <br />
                <div class="text-left">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn-primary btn" Width="107px" />
                </div>
                <br />

                <h3><u>Account Info</u></h3>
                <asp:Label ID="lblAWSError" runat="server" Text="" CssClass="h6 alert-danger mb-1"></asp:Label>
                <br />
                <div class="mx-auto">
                    <span class="h6">Resend Verification Link for Sign Up:</span>
                    <asp:Button ID="btnResendSignUpVerification" runat="server" Text="Resend" CssClass="btn btn-info" />

                    <br />
                    <br />

                    <span class="h6">Toggle Account Activation Status:</span>
                    <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" CssClass="btn btn-danger" />
                </div>
                
            </div>
            <!-- Section 2 End -->
        </div>
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
</asp:Content>
