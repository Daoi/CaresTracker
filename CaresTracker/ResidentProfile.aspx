<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="ResidentProfile.aspx.cs" Inherits="CaresTracker.ResidentProfile" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <asp:HiddenField ID="hdnfldFormattedAddress" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnfldName" ClientIDMode="Static" runat="server" />
        <div class="container homepage">
            <div class="row modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active">
                            <asp:LinkButton ID="lnkResidentLookup" NavigateUrl="~/ResidentLookup.aspx" runat="server" OnClick="lnkResidentLookup_Click">Resident Lookup</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Resident Profile</li>
                    </ol>
                </nav>
            </div>
            <div class="container-fluid w-75">
                <div class="container my-4 w-75">
                    <div class="row">
                        <h5>Resident Interactions:</h5>
                    </div>
                    <div class="row">
                        <div class="col">
                            <div class="row mt-3 justify-content-center">
                                <div class="col-sm-auto">
                                    <asp:Label ID="lblRecordedInteractions" CssClass="labels" runat="server" Text="Interactions recorded: "></asp:Label>
                                </div>
                            </div>
                            <div class="row mt-3 justify-content-center">
                                <div class="col-sm-auto">
                                    <asp:Button ID="btnReviewPastInteractions" CssClass="buttonStyle" runat="server" Text="Past Interactions" OnClick="btnReviewPastInteractions_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <div class="row mt-3 justify-content-center">
                                <div class="col-sm-auto">
                                    <asp:Label ID="lblRequestedServices" CssClass="labels" runat="server" Text="Services Requested: "></asp:Label>
                                </div>
                            </div>
                            <div class="row mt-3 justify-content-center">
                                <div class="col-sm-auto">
                                    <asp:Button ID="btnCreateNewInteraction" CssClass="buttonStyle" runat="server" Text="New Interaction" OnClick="btnCreateNewInteraction_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row pt-3 border-top">
                    <div class="col border-right">
                        <div class="p-3 py-5">
                            <div class="d-flex justify-content-between align-items-center mb-3">
                                <h5 class="text-right">Resident Info</h5>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="lblFirstName" CssClass="labels" runat="server" Text="First Name"></asp:Label><asp:TextBox ID="tbFirstName" placeholder="First Name" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblLastName" CssClass="labels" runat="server" Text="Last Name"></asp:Label><asp:TextBox ID="tbLastName" placeholder="Last Name" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:Label ID="lblDoB" CssClass="labels" runat="server" Text="Date of Birth"></asp:Label><asp:TextBox ID="tbDoB" placeholder="" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:Label ID="lblPhone" CssClass="labels" runat="server" Text="Phone Number"></asp:Label><asp:TextBox ID="tbPhone" placeholder="###-###-####" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:Label ID="lblEmail" CssClass="labels" runat="server" Text="Email Address"></asp:Label><asp:TextBox ID="tbEmail" placeholder="Email Address" CssClass="form-control" runat="server"></asp:TextBox>
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
                                <div class="col-md-12">
                                    <asp:Label ID="lblRelationshipToHoH" CssClass="labels" runat="server" Text="Relation to Head of House"></asp:Label><br />
                                    <asp:DropDownList ID="ddlHoH" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="Unknown">Unknown</asp:ListItem>
                                        <asp:ListItem Value="Head of House">Head of House</asp:ListItem>
                                        <asp:ListItem Value="Spouse/Partner">Spouse/Partner</asp:ListItem>
                                        <asp:ListItem Value="Sibling">Sibling</asp:ListItem>
                                        <asp:ListItem Value="Child">Child</asp:ListItem>
                                        <asp:ListItem Value="Parent">Parent</asp:ListItem>
                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:Label ID="lblPrimLanguage" CssClass="labels" runat="server" Text="Primary Language"></asp:Label>
                                    <asp:DropDownList CssClass="form-control" ID="ddlLanguage" RepeatDirection="Horizontal" runat="server">
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
                                    <asp:DropDownList ID="ddlRace" CssClass="form-control" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem>American Indian/Alaska Native</asp:ListItem>
                                        <asp:ListItem>Asian</asp:ListItem>
                                        <asp:ListItem>Black or African American</asp:ListItem>
                                        <asp:ListItem>Native Hawaiian or Other Pacific Islander</asp:ListItem>
                                        <asp:ListItem>White</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="mt-5 text-center">
                                <asp:Button ID="btnEditProfile" CssClass="buttonStyle" runat="server" Text="Edit Profile" OnClick="btnEditProfile_Click" />
                                <div class="btn-group">
                                    <asp:Button ID="btnSaveEdits" CssClass="buttonStyle" Visible="false" runat="server" Text="Save Edits" OnClick="btnSaveEdits_Click" />
                                    <asp:Button ID="btnCancelEdits" CssClass="buttonStyle ml-1" Visible="false" runat="server" Text="Cancel Editing" OnClick="btnCancelEdits_Click" />
                                </div>

                                <asp:Label ID="lblErrorMessage" CssClass="errorLabel" runat="server" Text="" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col">
                        <div class="p-3 py-5">
                            <div class="d-flex justify-content-between align-items-center">
                                <h5>Housing Info</h5>
                            </div>
                            <div class="mt-3">
                                <asp:UpdatePanel ID="upHousingDevelopment" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlHousingDevelopment" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Label ID="lblDevelopment" CssClass="labels" runat="server" Text="Development"></asp:Label><br />
                                        <asp:DropDownList ID="ddlHousingDevelopment" CssClass="form-control" runat="server" DataValueField='DevelopmentID' DataTextField='DevelopmentName' AppendDataBoundItems="True" OnSelectedIndexChanged="ddlHousingDevelopment_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="-1">HCV Housing</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="mt-3">
                                <div class="col m-0 p-0">
                                    <asp:Label ID="lblAddress" CssClass="labels" runat="server" Text="Address"></asp:Label>
                                </div>
                                <asp:Label runat="server" class="h6 rounded px-2 py-1 alert-danger" ID="lblWrongAddressInput" role="alert" Visible="false">
                                You must select an address from the list
                                </asp:Label>
                                <input id="txtAddress" type="text" runat="server" class="form-control" disabled="true" />
                            </div>
                            <div class="mt-3">
                                <asp:Label ID="lblUnitNumber" CssClass="labels" runat="server" Text="Unit Number"></asp:Label><asp:TextBox ID="tbUnitNumber" placeholder="Unit Number" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="mt-3">
                                <asp:Label ID="lblZipcode" CssClass="labels" runat="server" Text="Zipcode"></asp:Label><asp:TextBox ID="tbZipcode" placeholder="Zipcode" CssClass="form-control" runat="server" Enabled="False"></asp:TextBox>
                            </div>
                            <div class="mt-3">
                                <asp:UpdatePanel ID="upRegion" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlHousingDevelopment" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Label ID="lblRegionName" CssClass="labels" runat="server" Text="Region"></asp:Label><br />
                                        <asp:DropDownList ID="ddlRegion" CssClass="form-control" Enabled="false" runat="server">
                                            <asp:ListItem Value="1">North</asp:ListItem>
                                            <asp:ListItem Value="2">Upper North</asp:ListItem>
                                            <asp:ListItem Value="3">Lower North</asp:ListItem>
                                            <asp:ListItem Value="4">Upper Northwest</asp:ListItem>
                                            <asp:ListItem Value="5">Lower Northwest</asp:ListItem>
                                            <asp:ListItem Value="6">Lower Northeast</asp:ListItem>
                                            <asp:ListItem Value="7">West</asp:ListItem>
                                            <asp:ListItem Value="8">Central</asp:ListItem>
                                            <asp:ListItem Value="9">South</asp:ListItem>
                                            <asp:ListItem Value="10">Lower Soutwest</asp:ListItem>
                                            <asp:ListItem Value="11">University Southwest</asp:ListItem>
                                            <asp:ListItem Value="12">North Delaware</asp:ListItem>
                                            <asp:ListItem Value="13">River Wards</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="d-flex justify-content-between align-items-center mt-3">
                                <h5>Vaccine Info</h5>
                            </div>
                            <div class="mt-3">
                                <asp:Label ID="lblVaccineStatus" CssClass="labels" runat="server" Text="Vaccine Status"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlVaccineStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="Unknown">Unknown</asp:ListItem>
                                    <asp:ListItem Value="Not interested in vaccine">Not interested in vaccine</asp:ListItem>
                                    <asp:ListItem Value="Interested in vaccine, no appointment">Interested in vaccine, no appointment</asp:ListItem>
                                    <asp:ListItem Value="Appointment Scheduled">Appointment Scheduled</asp:ListItem>
                                    <asp:ListItem Value="Vaccinated">Vaccinated</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="divAppointmentInfo" runat="server" class="col-md-12 mt-3">
                                <div class="row">
                                    <asp:Label ID="lblVaccineAppointment" CssClass="labels" runat="server" Text="Appointment Date"></asp:Label><br />
                                    <asp:TextBox ID="tbAppointmentDate" placeholder="Region" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        function setupSelect2(selectID, placeHolder) {
            $(selectID).select2({
                placeholder: placeHolder,
                allowClear: false,
                selectOnClose: true
            });
        }
    </script>

    <!-- Google Maps JavaScript library -->
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAUR5TUQYRQwObVxISNpIi7RlFTsg6NQcI&libraries=places&callback=initMap"></script>
    <!-- Attach API to txtAddress -->
    <script type="text/javascript">loadPlacesAPI("MainContent_txtAddress");</script>
</asp:Content>
