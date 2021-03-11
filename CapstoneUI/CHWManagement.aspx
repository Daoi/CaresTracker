<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="CHWManagement.aspx.cs" Inherits="CapstoneUI.CHWManagement" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid managementContainer backgroundblue">
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
        <div class="text-center p-5">
            <!-- Section 1 Start -->
                <asp:Label ID="lblWorkerName" runat="server" Text="<u>John Doe</u>" CssClass="h3 mb-2"></asp:Label>
                <br />
                <div class="p-3 mx-auto" style="width: 22vw;">
                    <span>Interactions this Week: 
                            <asp:Label ID="lblWeekInteractions" runat="server" Text="2"></asp:Label></span><br />
                    <span>Total Interactions: 
                            <asp:Label ID="lblTotalInteractions" runat="server" Text="28"></asp:Label></span>
                    <br />
                    <br />
                    <asp:Button ID="btnViewInteractions" runat="server" Text="View Interactions" CssClass="btn-primary btn" OnClick="btnViewInteractions_Click" />
                </div>
            <br />
            <!-- Section 1 End -->
            <!-- Section 2 Start -->
            <div class="w-50 text-center mx-auto">
                <h3><u>Account Info</u></h3>
                <asp:DetailsView ID="dvAccountInfo" runat="server" CssClass="w-50 mx-auto" Height="50px" Width="125px" AutoGenerateRows="False">
                    <Fields>
                        <asp:BoundField DataField="Username" HeaderText="Username" />
                        <asp:BoundField DataField="UserEmail" HeaderText="Email" />
                        <asp:BoundField DataField="UserPhoneNumber" HeaderText="Phone Number" />
                        <asp:BoundField DataField="OrganizationName" HeaderText="Organization" />
                        <asp:BoundField DataField="LastLogin" HeaderText="Last Login" />
                    </Fields>
                </asp:DetailsView>
                <br />
                <br />
                <h3><u>Manage Account</u></h3>
                <asp:Label ID="lblAWSError" runat="server" Text="" CssClass="h6 alert-danger mb-1"></asp:Label>
                <br />
                <div class="mx-auto">
                    <span class="h6 mb-1">Resend Verification Link for Sign Up:</span><br />
                    <asp:Button ID="btnResendSignUpVerification" runat="server" Text="Resend" CssClass="btn btn-info w-25 mt-1" OnClick="btnResendSignUpVerification_Click" />

                    <br />
                    <br />

                    <span class="h6">Toggle Account Activation Status:</span><br />
                    <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" CssClass="btn btn-danger w-25 mt-1" OnClick="btnDeactivate_Click" />
                </div>
                </div>
            </div>
            <!-- Section 2 End -->
    </div>
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
</asp:Content>
