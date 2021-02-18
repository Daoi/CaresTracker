<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateResident.aspx.cs" MasterPageFile="~/CapstoneUI.Master" Inherits="CapstoneUI.CreateResident" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid backgroundblue">
        <div class="container homepage">
            <div>
                <div class="row modal-header" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">Create Resident</li>
                        </ol>
                    </nav>
                    <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 70%"></asp:Label>
                </div>
                <div class="container mt-5 mr-5 w-75 mb-5">
                    <div class="row">
                        <div class="col">
                            <h5>Personal Information:</h5>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>First Name: </label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>Last Name: </label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>Date of Birth: </label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>Email: </label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>Phone-number: </label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>Relationship to Head of Household: </label>
                        </div>
                        <div class="col-7">
                            <asp:DropDownList ID="ddlRelationshipHOH" runat="server">
                                <asp:ListItem>Self</asp:ListItem>
                                <asp:ListItem>Spouse</asp:ListItem>
                                <asp:ListItem>Child</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row-m m-3 modal-header">
                        <div class="col mt-4">
                            <label>Gender: </label>
                        </div>
                        <div class="col-7" id="divrblGender">
                            <asp:RadioButtonList ID="rblGender" RepeatDirection="Vertical" runat="server">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Prefer not to say</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>Race: </label>
                        </div>
                        <div class="col-7">
                            <asp:DropDownList CssClass="w-100" ID="ddlRace" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem>American Indian/Alaska Native</asp:ListItem>
                                <asp:ListItem>Asian</asp:ListItem>
                                <asp:ListItem>Black or African American</asp:ListItem>
                                <asp:ListItem>Native Hawaiian or Other Pacific Islander</asp:ListItem>
                                <asp:ListItem>White</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row m-3 modal-header">
                        <div class="col">
                            <label>Family size: </label>
                        </div>
                        <div class="col-7">
                            <asp:TextBox ID="txtFamilySize" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    
                    <%-- Conditional Housing Info --%>
                    <h2>Housing Information</h2>
                    <asp:DropDownList ID="ddlHousing" CssClass="form-control mt-2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHousing_SelectedIndexChanged">
                        <asp:ListItem Value="Select Housing Type">Select Housing Type</asp:ListItem>
                        <asp:ListItem Value="divHouse">House</asp:ListItem>
                        <asp:ListItem Value="divDevelopmentUnit">Development Unit</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:UpdatePanel ID="upHousing" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlHousing" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <%-- House Start --%>
                            <div id="divHouse" runat="server" visible="false">
                                <div class="eventControlBG">
                                    <h4 class="font-weight-light">Region</h4>
                                    <asp:DropDownList id="ddlRegion" CssClass="mb-4" runat="server">
                                        <asp:ListItem>Region1</asp:ListItem>
                                        <asp:ListItem>Region2</asp:ListItem>
                                        <asp:ListItem>Region3</asp:ListItem>
                                        <asp:ListItem>Region4</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtHouseAddress" placeholder="Address" runat="server" CssClass="form-control"></asp:TextBox><br />
                                    <asp:TextBox ID="txtHouseNumOccupants" placeholder="Number of Occupants" runat="server" CssClass="form-control"></asp:TextBox><br />
                                </div>
                            </div>
                            <%-- House End --%>
                            <%-- Development Unit Start --%>
                            
                            <div id="divDevelopmentUnit" runat="server" visible="false">
                                <h4 class="font-weight-light">Development Name</h4>
                                <div class="eventControlBG">
                                    <asp:DropDownList id="ddlDevelopments" runat="server"></asp:DropDownList>
                                    <br />
                                </div>
                            </div>
                            <%-- Development Unit End --%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row m-3">
                        <div class="col">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary" Text="Create Resident Profile" runat="server" OnClick="btnSubmit_Click1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
