<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="CHWManagement.aspx.cs" Inherits="CapstoneUI.CHWManagement" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container homepage p-0">
        <div class="row modal-header no-gutters offwhiteBackground" style="height: auto; font-size: large">
            <nav aria-label="breadcrumb w-75">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" CssClass="cherryFont" OnClick="lnkHome_Click" runat="server">Dashboard</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lnkWorkerList" NavigateUrl="~/CHWManagement.aspx" runat="server" OnClick="lnkWorkerList_Click">Worker List</asp:LinkButton></li>
                    <li class="breadcrumb-item active" aria-current="page">Worker Management</li>
                </ol>
            </nav>
        </div>
        <div class="container-fluid">
            <div class="container w-75 mt-5">
                <div class="row">
                    <div class="col border-right">
                        <div class="row m-3">
                            <h5>Worker Stats:</h5>
                        </div>
                        <div class="row m-3">
                            <asp:Label ID="lblTotalInteractions" CssClass="labels" runat="server" Text="Total Interactions: "></asp:Label>
                        </div>
                        <div class="row m-3">
                            <asp:Label ID="lblWeekInteractions" CssClass="labels" runat="server" Text="Interactions this Week: "></asp:Label>
                        </div>
                        <div class="row m-3">
                            <asp:Button ID="btnViewInteractions" runat="server" Text="View Interactions" CssClass="buttonStyle cherryBackground" OnClick="btnViewInteractions_Click" />
                        </div>
                    </div>
                    <div class="col">
                        <div class="row m-3">
                            <h5>Manage Account:</h5>
                        </div>
                        <div id="divResendLink" runat="server" class="row m-3">
                            <div class="col p-0">
                                <asp:Label ID="lblResendLink" CssClass="labels d-block" runat="server" Text="Resend Verification Link for Sign Up:"></asp:Label>
                                <asp:Button ID="btnResendSignUpVerification" runat="server" Text="Resend" CssClass="buttonStyle cherryBackground mt-3" OnClick="btnResendSignUpVerification_Click" />
                            </div>
                        </div>
                        <div class="row m-3">
                            <div class="col p-0">
                                <asp:Label ID="lblToggleActivation" CssClass="labels d-block" runat="server" Text="Toggle Account Activation Status:"></asp:Label>
                                <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" CssClass="buttonStyle cherryBackground mt-3" OnClick="btnDeactivate_Click" />
                            </div>
                        </div> 
                        <div class="row">
                            <asp:Label ID="lblAWSError" runat="server" Text="" CssClass="h6 alert-danger mb-1"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="container-fluid my-4 border-top">
                        <div class=" py-3">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <h5 class="text-right">Worker Info:</h5>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="lblUserName" CssClass="labels" runat="server" Text="Username"></asp:Label><asp:TextBox ID="txtUsername" placeholder="Username" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblFullName" CssClass="labels" runat="server" Text="Full Name"></asp:Label><asp:TextBox ID="txtFullName" placeholder="Full Name" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:Label ID="lblPhone" CssClass="labels" runat="server" Text="Phone Number"></asp:Label><asp:TextBox ID="txtPhone" placeholder="###-###-####" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label ID="lblEmail" CssClass="labels" runat="server" Text="Email Address"></asp:Label><asp:TextBox ID="txtEmail" placeholder="Email Address" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="lblOrganizationName" CssClass="labels" runat="server" Text="Organization"></asp:Label><asp:TextBox ID="txtOrganizationName" placeholder="Organization Name" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblAccountType" CssClass="labels" runat="server" Text="Account Type"></asp:Label><asp:TextBox ID="txtAccountType" placeholder="Account Type" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="lblLastLogin" CssClass="labels" runat="server" Text="Last Login"></asp:Label><asp:TextBox ID="txtLastLogin" placeholder="Last Login Time" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
