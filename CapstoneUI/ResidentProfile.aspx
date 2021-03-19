<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="ResidentProfile.aspx.cs" Inherits="CapstoneUI.ResidentProfile" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid homepage backgroundblue">
        <div class="row">
            <div class="col-md-2 border-right">
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-auto">
                        <asp:Button ID="btnReviewPastInteractions" CssClass="btn-responsive btn-primary" runat="server" Text="Past Interactions" OnClick="btnReviewPastInteractions_Click" />
                    </div>
                </div>
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-auto">
                        <asp:Button ID="btnCreateNewInteraction" CssClass="btn-responsive btn-primary" runat="server" Text="New Interaction" OnClick="btnCreateNewInteraction_Click" />
                    </div>
                </div>
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-auto">

                        <asp:Label ID="lblRecordedInteractions" CssClass="labels" runat="server" Text="Interactions recorded: "></asp:Label>
                    </div>
                </div>
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-auto">
                        <asp:Label ID="lblRequestedServices" CssClass="labels" runat="server" Text="Services Requested: "></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-6 border-right">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center mb-3">
                        <h4 class="text-right">Resident Info</h4>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <asp:Label ID="lblFirstName" CssClass="labels" runat="server" Text="First Name"></asp:Label><asp:TextBox ID="tbFirstName" placeholder="First Name" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblLastName" CssClass="labels" runat="server" Text="Last Name"></asp:Label><asp:TextBox ID="tbLastName" placeholder="Last Name" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblDoB" CssClass="labels" runat="server" Text="Date of Birth"></asp:Label><asp:TextBox ID="tbDoB" placeholder="" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <asp:Label ID="lblPhone" CssClass="labels" runat="server" Text="Phone Number"></asp:Label><asp:TextBox ID="tbPhone" placeholder="###-###-####" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblEmail" CssClass="labels" runat="server" Text="Email Address"></asp:Label><asp:TextBox ID="tbEmail" placeholder="Email Address" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <asp:Label ID="lblGender" CssClass="labels" runat="server" Text="Gender"></asp:Label>
                            <asp:RadioButtonList ID="rblGender" CssClass="rp-gender" runat="server" RepeatDirection="Horizontal" CellPadding="8">
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <asp:Label ID="lblRelationshipToHoH" CssClass="labels" runat="server" Text="Relation to Head of House"></asp:Label>
                            <asp:DropDownList ID="ddlHoH" runat="server">
                                <asp:ListItem Value="Head of House">Head of House</asp:ListItem>
                                <asp:ListItem Value="Spouse/Partner">Spouse/Partner</asp:ListItem>
                                <asp:ListItem Value="Sibling">Sibling</asp:ListItem>
                                <asp:ListItem>Child</asp:ListItem>
                                <asp:ListItem>Parent</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblPrimLanguage" CssClass="labels" runat="server" Text="Priamry Language"></asp:Label>
                            <asp:DropDownList CssClass="w-100" ID="ddlLanguage" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem>English</asp:ListItem>
                                <asp:ListItem>Spanish</asp:ListItem>
                                <asp:ListItem>French</asp:ListItem>
                                <asp:ListItem>Arabic</asp:ListItem>
                                <asp:ListItem>Vietnamese</asp:ListItem>
                                <asp:ListItem>Arabic</asp:ListItem>
                                <asp:ListItem>Mandarin</asp:ListItem>
                                <asp:ListItem>Other</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <asp:Label ID="lblRace" CssClass="labels" runat="server" Text="Race"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlRace" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem>American Indian/Alaska Native</asp:ListItem>
                                <asp:ListItem>Asian</asp:ListItem>
                                <asp:ListItem>Black or African American</asp:ListItem>
                                <asp:ListItem>Native Hawaiian or Other Pacific Islander</asp:ListItem>
                                <asp:ListItem>White</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="mt-5 text-center">
                        <asp:Button ID="btnEditProfile" CssClass="btn btn-primary" runat="server" Text="Edit Profile" OnClick="btnEditProfile_Click" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="p-3 py-5">
                    <div class="d-flex justify-content-between align-items-center">
                        <h4>Housing Info</h4>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-12">
                            <asp:Label ID="lblDevelopment" CssClass="labels" runat="server" Text="Development"></asp:Label><asp:TextBox ID="tbDevelopment" placeholder="Development Name" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <br />
                        <div class="col-md-12">
                            <asp:Label ID="lblAddress" CssClass="labels" runat="server" Text="Address"></asp:Label><asp:TextBox ID="tbAddress" placeholder="Street Address" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblRegionName" CssClass="labels" runat="server" Text="Region"></asp:Label><asp:TextBox ID="tbRegionName" placeholder="Region" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="d-flex justify-content-between align-items-center mt-3">
                        <h4>Vaccine Info</h4>
                    </div>
                    <div class="mt-2">
                        <asp:Label ID="lblVaccinePhase" CssClass="labels" runat="server" Text="Vaccine Phase"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlVaccinePhases" runat="server">
                            <asp:ListItem Value="0">Phase1A</asp:ListItem>
                            <asp:ListItem Value="1">Phase1B</asp:ListItem>
                            <asp:ListItem Value="2">Phase1C</asp:ListItem>
                            <asp:ListItem Value="3">Phase2</asp:ListItem>
                            <asp:ListItem Value="4">No Information</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="mt-3">
                        <asp:Label ID="lblVaccineStatus" CssClass="labels" runat="server" Text="Vaccine Status"></asp:Label>
                        <br />
                        <asp:DropDownList ID="ddlVaccineStatus" runat="server">
                            <asp:ListItem>No Information</asp:ListItem>
                            <asp:ListItem>Not Interested</asp:ListItem>
                            <asp:ListItem>Interested, not scheduled</asp:ListItem>
                            <asp:ListItem>Appointment Scheduled</asp:ListItem>
                            <asp:ListItem>Vaccinated</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="divAppointmentInfo"  runat="server" class="col-md-12 mt-3">
                        <div class="row">
                            <asp:Label ID="lblVaccineAppointment" CssClass="labels" runat="server" Text="Appointment Date"></asp:Label><br />
                            <asp:TextBox ID="tbAppointmentDate" placeholder="Region" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                        </div>
                        <div class="row">
                            <asp:Label ID="lblAppointmentTime" CssClass="labels" runat="server" Text="Appointment Time"></asp:Label>
                            <asp:TextBox ID="tbAppointmentTime" TextMode="Time" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
