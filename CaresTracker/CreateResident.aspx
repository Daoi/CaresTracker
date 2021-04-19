<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateResident.aspx.cs" MasterPageFile="~/CaresTracker.Master" Inherits="CaresTracker.CreateResident" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container homepage">
        <div>
            <div class="row modal-header offwhiteBackground" style="height: 7%; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                        <li class="breadcrumb-item active" aria-current="page">Create Resident</li>
                    </ol>
                </nav>
                <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 70%"></asp:Label>

                <!-- Hidden fields for JavaScript + C# variable passing -->
                <asp:HiddenField ID="hdnfldFormattedAddress" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnfldName" ClientIDMode="Static" runat="server" />
            </div>
            <div class="row m-3 justify-content-center mt-5">
                <asp:Label runat="server" class="errorLabel" ID="lblValidationError" role="alert" Visible="false"></asp:Label>
            </div>
            <%-- Resident Info Start --%>
            <div class="container-fluid mt-3 w-75">
                <div class="row">
                    <div class="col">
                        <h5>Personal Information:</h5>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label class="required">First Name: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label class="required">Last Name: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label class="required">Date of Birth: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtDOB" TextMode="Date" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Email: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label class="required">Phone Number: </label>
                    </div>
                    <div class="col-7">
                        <asp:Label runat="server" For="txtPhoneNumber" class="errorLabel" ID="lblValidationPhone" role="alert" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Relationship to Head of Household: </label>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="ddlRelationshipHOH" CssClass="form-control" runat="server">
                            <asp:ListItem>Unknown</asp:ListItem>
                            <asp:ListItem>Head of House</asp:ListItem>
                            <asp:ListItem>Spouse/Partner</asp:ListItem>
                            <asp:ListItem>Sibling</asp:ListItem>
                            <asp:ListItem>Child</asp:ListItem>
                            <asp:ListItem>Parent</asp:ListItem>
                            <asp:ListItem>Other</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Gender: </label>
                    </div>
                    <div class="col-7" id="divrblGender">
                        <asp:Label runat="server" class="errorLabel" ID="lblValidationGender" role="alert" Visible="false"></asp:Label>
                        <asp:RadioButtonList ID="rblGender" CssClass="" RepeatDirection="Vertical" runat="server">
                            <asp:ListItem Selected="True">Unknown</asp:ListItem>
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                            <asp:ListItem>Prefer not to say</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Race: </label>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="ddlRace" CssClass="form-control" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem>Unknown</asp:ListItem>
                            <asp:ListItem>American Indian/Alaska Native</asp:ListItem>
                            <asp:ListItem>Asian</asp:ListItem>
                            <asp:ListItem>Black or African American</asp:ListItem>
                            <asp:ListItem>Native Hawaiian or Other Pacific Islander</asp:ListItem>
                            <asp:ListItem>White</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row m-3 mb-5">
                    <div class="col">
                        <label>Preferred Language: </label>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList CssClass="form-control" ID="ddlLanguage" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem>Unknown</asp:ListItem>
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
                <%-- Resident Info End --%>
                <hr />
                <%-- House Start --%>
                <div class="row">
                    <div class="col">
                        <h5>Housing Information</h5>
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label class="required">Housing Type:</label>
                    </div>
                    <div class="col-7">
                        <asp:Label runat="server" class="errorLabel" ID="lblValidationHousing" role="alert" Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlHousing" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlHousing_SelectedIndexChanged">
                            <asp:ListItem Value="None Selected">Select Housing Type</asp:ListItem>
                            <asp:ListItem Value="divHouse">Housing Choice Voucher</asp:ListItem>
                            <asp:ListItem Value="divDevelopmentUnit">Housing Development</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <%-- Universal Info Start --%>

                <div class="row m-3">
                    <div class="col">
                        <label class="required">Personal Address: </label>
                    </div>
                    <div class="col-7">
                        <asp:Label runat="server" class="errorLabel" ID="lblWrongAddressInput" role="alert" Visible="false">
                                You must select an address from the list
                        </asp:Label>
                        <input id="txtAddress" type="text" placeholder="" runat="server" class="form-control" />
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Unit Number: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtUnitNumber" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <%-- Universal Info End --%>

                <%-- Housing Choice Start --%>
                <asp:UpdatePanel ID="upHousing" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlHousing" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divHouse" runat="server" visible="false">
                            <div class="row m-3">
                                <div class="col">
                                    <label>Region</label>
                                </div>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlRegion" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="1">North</asp:ListItem>
                                        <asp:ListItem Value="2">Upper North</asp:ListItem>
                                        <asp:ListItem Value="3">Lower North</asp:ListItem>
                                        <asp:ListItem Value="4">Upper Northwest</asp:ListItem>
                                        <asp:ListItem Value="5">Lower Northwest</asp:ListItem>
                                        <asp:ListItem Value="6">Lower Northeast</asp:ListItem>
                                        <asp:ListItem Value="7">West</asp:ListItem>
                                        <asp:ListItem Value="8">Central</asp:ListItem>
                                        <asp:ListItem Value="9">South</asp:ListItem>
                                        <asp:ListItem Value="10">Lower Southwest</asp:ListItem>
                                        <asp:ListItem Value="11">University Southwest</asp:ListItem>
                                        <asp:ListItem Value="12">North Delaware</asp:ListItem>
                                        <asp:ListItem Value="13">River Wards</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%-- Housing Choice End --%>

                        <%-- Development Unit Start --%>

                        <div id="divDevelopmentUnit" runat="server" visible="false">
                            <div class="row m-3">
                                <div class="col">
                                    <label>Development Name: </label>
                                </div>
                                <div class="col-7">
                                    <asp:DropDownList ID="ddlDevelopments" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <%-- Development Unit End --%>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row m-3">
                    <asp:Button ID="btnSubmit" CssClass="buttonStyle" Text="Create Resident Profile" runat="server" OnClick="btnSubmit_Click1" />
                </div>
                <%-- House End --%>

                <%-- Alerts start --%>

                <div class="row m-3 justify-content-center mt-3">
                    <div class="col text-center">
                        <asp:Label runat="server" class="errorLabel" ID="lblFail" role="alert" Visible="false">
                                Could not add resident and/or house to the database
                        </asp:Label>
                        <asp:Label runat="server" class="errorLabel" ID="lblUniqueResident" role="alert" Visible="false">
                                Resident profile already exists!
                        </asp:Label>
                    </div>
                </div>
                <%-- Alerts end --%>
            </div>
        </div>
    </div>
    <!-- Google Maps JavaScript library -->
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAUR5TUQYRQwObVxISNpIi7RlFTsg6NQcI&libraries=places&callback=initMap"></script>
    <!-- Attach API to txtAddress -->
    <script type="text/javascript">loadPlacesAPI("MainContent_txtAddress");</script>
    <script>
        function setupSelect2(selectID, placeHolder) {
            $(selectID).select2({
                placeholder: placeHolder,
                allowClear: false,
                selectOnClose: true
            });
        }
    </script>
</asp:Content>
