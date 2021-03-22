<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="CHWManagement.aspx.cs" Inherits="CapstoneUI.CHWManagement" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage p-0">
        <div class="row modal-header no-gutters offwhiteBackground" style="height: auto; font-size: large">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lnkWorkerList" NavigateUrl="~/CHWManagement.aspx" runat="server" OnClick="lnkWorkerList_Click">Worker List</asp:LinkButton></li>
                    <li class="breadcrumb-item active" aria-current="page">Worker Management</li>
                </ol>
            </nav>
            <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 60%">Worker Management</asp:Label>
        </div>
        <div class="row">
            <div class="col-md-2 border-right">
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-auto">
                        <asp:Button ID="btnViewInteractions" runat="server" Text="View Interactions" CssClass="btn-primary btn-responsive" OnClick="btnViewInteractions_Click" />
                    </div>
                </div>

                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-auto">

                        <asp:Label ID="lblTotalInteractions" CssClass="labels" runat="server" Text="Total Interactions: "></asp:Label>
                    </div>
                </div>
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-auto">
                        <asp:Label ID="lblWeekInteractions" CssClass="labels" runat="server" Text="Interactions this Week: "></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-6 border-right">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h4 class="text-right">Worker Info</h4>
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
            <div class="col-md-4">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center">
                        <h4>Manage Account</h4>
                    </div>

                    <asp:Label ID="lblAWSError" runat="server" Text="" CssClass="h6 alert-danger mb-1"></asp:Label>

                    <div class="row mt-3">
                        <div id="divResendLink" runat="server" class="col-md-12 mb-3">
                            <asp:Label ID="lblResendLink" CssClass="labels" runat="server" Text="Resend Verification Link for Sign Up:"></asp:Label>
                            <br />
                            <asp:Button ID="btnResendSignUpVerification" runat="server" Text="Resend" CssClass="btn-responsive btn-info mt-1" OnClick="btnResendSignUpVerification_Click" />
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblToggleActivation" CssClass="labels" runat="server" Text="Toggle Account Activation Status:"></asp:Label>
                            <br />
                            <asp:Button ID="btnDeactivate" runat="server" Text="Deactivate" CssClass="btn-responsive btn-danger mt-1" OnClick="btnDeactivate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
