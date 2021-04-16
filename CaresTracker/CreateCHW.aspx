<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/CaresTracker.Master" CodeBehind="CreateCHW.aspx.cs" Inherits="CaresTracker.CreateCHW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container homepage">
            <div class="row modal-header offwhiteBackground" style="height: 5%; padding-left: 0!important; padding-right: 0!important; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                        <li class="breadcrumb-item active" aria-current="page">Create CHW Account</li>
                    </ol>
                </nav>
                <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2 px-0" Style="width: 70%"></asp:Label>
            </div>
            <div class="container-fluid mt-5 w-75">
                <div class="row">
                    <h5>Personal Information:</h5>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>First Name: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Last Name: </label>
                    </div>

                    <div class="col-7">
                        <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Username: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Email: </label>
                    </div>
                    <div class="col-7">
                        <div class="input-group">
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                            <div class="input-group-append">
                                <asp:DropDownList ID="ddlEmail" CssClass="dropdown-menu" runat="server">
                                    <asp:ListItem Value="@gphainc.org">@gphainc.org</asp:ListItem>
                                    <asp:ListItem Value="@temple.edu">@temple.edu</asp:ListItem>
                                    <asp:ListItem Value="@drexel.edu">@drexel.edu</asp:ListItem>
                                    <asp:ListItem Value="@tuhs.temple.edu">@tuhs.temple.edu</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Phone Number: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <h5>Admin Information:</h5>
                    </div>
                </div>
                <div id="ddlOrganizationDiv" class="row m-3" runat="server">
                    <div class="col">
                        <label>Organization:</label>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="ddlOrganization" CssClass="form-control" runat="server">
                            <asp:ListItem Value="default" Selected>Select an option</asp:ListItem>
                            <asp:ListItem Value="1">Temple</asp:ListItem>
                            <asp:ListItem Value="2">Drexel</asp:ListItem>
                            <asp:ListItem Value="3">Greater Philadelphia Health Action</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Account Type:</label>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="ddlAccountType" CssClass="form-control" runat="server">
                            <asp:ListItem Value="default" Selected>Select an option</asp:ListItem>
                            <asp:ListItem Value="A">Partner Admin</asp:ListItem>
                            <asp:ListItem Value="S">Supervisor</asp:ListItem>
                            <asp:ListItem Value="C">CHW</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <asp:Label ID="lblError" CssClass="errorLabel" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row m-3">
                    <asp:Button ID="btnSubmit" CssClass="buttonStyle" Text="Create CHW Profile" OnClick="btnSubmit_Click" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
