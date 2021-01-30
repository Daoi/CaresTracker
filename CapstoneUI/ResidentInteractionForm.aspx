<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="ResidentInteractionForm.aspx.cs" Inherits="CapstoneUI.ResidentInteractionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="style/InteractionFormStyle.css" rel="stylesheet" />

    <div id="RIForm" class="container-fluid">
        <div id="logo">
            <h1 class="logo"></h1>
            <div class="leftbox">
                <nav>
                    <asp:LinkButton ID="residentInfo" CssClass="active" runat="server" OnClick="formNav_Click">Resident Information</asp:LinkButton>
                    <asp:LinkButton ID="housingInfo" CssClass="active" runat="server" OnClick="formNav_Click">Housing Information</asp:LinkButton>
                    <asp:LinkButton ID="meetingInfo" CssClass="active" runat="server" OnClick="formNav_Click">Meeting Information</asp:LinkButton>
                    <asp:LinkButton ID="residentHealth" CssClass="active" runat="server" OnClick="formNav_Click">Resident Health</asp:LinkButton>
                    <asp:LinkButton ID="services" CssClass="active" runat="server" OnClick="formNav_Click">Services</asp:LinkButton>
                    <asp:LinkButton ID="otherInfo" CssClass="active" runat="server" OnClick="formNav_Click">Other</asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnSave" CssClass="active" runat="server" OnClick="lnkBtnSave_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnHome" CssClass="active" runat="server" OnClick="lnkBtnHome_Click"><i class="fas fa-home" style="margin-right: .5rem"></i>Return To Dashboard</asp:LinkButton>
                </nav>
            </div>
            <%-- Resident Info Form Start --%>
            <div class="rightbox">
                <div class="residentInfo" id="residentInfoForm" runat="server">
                    <h1>Resident Info</h1>
                    <h2>Full Name</h2>
                    <p>
                        <asp:TextBox ID="tbName" runat="server" placeholder="Resident Name" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Age</h2>
                    <p>
                        <asp:TextBox ID="tbAge" runat="server" placeholder="Resident Age" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Gender</h2>
                    <p>
                        <asp:TextBox ID="tbGender" runat="server" placeholder="Resident Gender" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Phone</h2>
                    <p>
                        <asp:TextBox ID="tbPhone" runat="server" placeholder="Resident Phone" TextMode="Phone" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Email</h2>
                    <p>
                        <asp:TextBox ID="tbEmail" runat="server" placeholder="Resident Email" TextMode="Phone" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Date of Contact</h2>
                    <p>
                        <asp:TextBox ID="tbDoC" runat="server" placeholder="Date of Contact" TextMode="Date" CssClass="inputText"></asp:TextBox>
                    </p>
                </div>
                <%-- Resident Info Form End --%>
                <%-- Housing Info Form Start--%>
                <div class="housingInfo" id="housingInfoForm" runat="server">
                    <h1>Housing Info</h1>
                    <h2>Number of residents</h2>
                    <p>
                        <asp:TextBox ID="tbResidenceOccupants" runat="server" placeholder="How many people does the resident live with?" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>HCV or PHA Development</h2>
                    <asp:DropDownList ID="ddlHousingType" CssClass="inputDropDown" runat="server">
                        <asp:ListItem>Select Housing Type</asp:ListItem>
                        <asp:ListItem>Housing Choice Voucher</asp:ListItem>
                        <asp:ListItem>PHA Development</asp:ListItem>
                    </asp:DropDownList>
                    <p></p>
                    <h2>PHA Development Name</h2>
                    <p>
                        <asp:TextBox ID="tbDevelopmentName" runat="server" placeholder="Development Name" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Resident's Region</h2>
                    <p>
                        <asp:TextBox ID="tbRegion" runat="server" placeholder="Resident's Region" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Resident's Address</h2>
                    <p>
                        <asp:TextBox ID="tbResidentAddress" runat="server" placeholder="Resident's Address" CssClass="inputText"></asp:TextBox>
                    </p>

                </div>
                <%-- Housing Info Form End --%>
                <%-- Meeting Info Form Start --%>
                <div id="meetingInfoForm" class="meetingInfo" runat="server">
                    <h1>Meeting Info</h1>
                    <h2>Meeting Type</h2>
                    <asp:DropDownList ID="ddlMeetingType" CssClass="inputDropDown" runat="server">
                        <asp:ListItem>Select Meeting Type</asp:ListItem>
                        <asp:ListItem>Phone</asp:ListItem>
                        <asp:ListItem>Email</asp:ListItem>
                        <asp:ListItem>In-Person</asp:ListItem>
                    </asp:DropDownList>
                    <p></p>
                    <h2>Location</h2>
                    <p>
                        <asp:TextBox ID="tbLocation" runat="server" placeholder="Meeting Location" CssClass="inputText"></asp:TextBox>
                    </p>

                </div>
                <%-- Meeting Info Form End --%>
                <%-- Resident Health Form Start --%>
                <div id="residentHealthForm" class="residentHealth form-group" style="display:inline-grid" runat="server">
                    <h1>Resident Health</h1>
                    <%-- Symtpom List Start --%>
                    <h2>Covid-19 Possible Symptoms</h2>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbShortnessOfBreath" runat="server" CssClass="inputCheckBox" Text="Shortness of Breath" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbNausea" runat="server" CssClass="inputCheckBox" Text="Nausea or Vomiting" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbFatigue" runat="server" CssClass="inputCheckBox" Text="Fatigue" Font-Size="Medium" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbAches" runat="server" CssClass="inputCheckBox" Text="Body/Muscle Aches" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbHeadaches" runat="server" CssClass="inputCheckBox" Text="Headaches" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbSoreThroat" runat="server" CssClass="inputCheckBox" Text="Sore Throat" Font-Size="Medium" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbTasteOrSmell" runat="server" CssClass="inputCheckBox" Text="New Loss of Taste or Smell" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbCongestion" runat="server" CssClass="inputCheckBox" Text="Congestion or Runny Nose" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbCough" runat="server" CssClass="inputCheckBox" Text="Cough" Font-Size="Medium" />
                        </div>
                    </div>
                    <p></p>
                    <h2>Critical Symptoms(Seek Immediate Medical Attention)</h2>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbConfusion" runat="server" CssClass="inputCheckBox" Text="Confusion" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbTroubleBreathing" runat="server" CssClass="inputCheckBox" Text="Trouble Breathing" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbBlueLips" runat="server" CssClass="inputCheckBox" Text="Blueish Lips" Font-Size="Medium" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbInabilityToAwake" runat="server" CssClass="inputCheckBox" Text="Inability to stay awake" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-8">
                            <asp:CheckBox ID="cbChestPain" runat="server" CssClass="inputCheckBox" Text="Persistent Chest Pain or Pressure" Font-Size="Medium" />
                        </div>
                    </div>
                    <p></p>
                    <%-- Symptom List End --%>
                    <h2>Dates Symptoms Occured</h2>
                    <p>
                        <asp:TextBox ID="tbSymptomDates" runat="server" placeholder="Date of Symptoms" CssClass="inputText" TextMode="Date"></asp:TextBox>
                    </p>
                    <h2>Covid-19 Test Results</h2>
                    <asp:DropDownList ID="ddlTestResult" CssClass="inputDropDown" runat="server">
                        <asp:ListItem>Select Test Result</asp:ListItem>
                        <asp:ListItem>Positive</asp:ListItem>
                        <asp:ListItem>Negative</asp:ListItem>
                    </asp:DropDownList>
                    <p></p>
                    <h2>Testing Location</h2>
                    <p>
                        <asp:TextBox ID="tbTestingLocation" runat="server" placeholder="Testing Location" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Possible Exposure</h2>
                    <p>
                        <asp:TextBox ID="tbPossibleExposure" runat="server" placeholder="List of others who might have been exposed in case of positive test." CssClass="inputText"></asp:TextBox>
                    </p>
                </div>
                <%-- Resident Health Form End --%>
                <%-- Services Form Start --%>
                <div id="servicesForm" class="services" runat="server">
                    <h1>Social Services</h1>
                    <h2>Service's Offered</h2>
                    <p>
                        <asp:TextBox ID="tbServices" runat="server" placeholder="List of services offered to resident" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Services Requested</h2>
                    <p>
                        <asp:TextBox ID="tbServicesRequested" runat="server" placeholder="List of services requested by resident" CssClass="inputText"></asp:TextBox>
                    </p>
                </div>
                <%-- Services Form End --%>
                <%-- Other Form Start --%>
                <div id="otherForm" class="otherInfo" runat="server">
                    <h1>Other Comments</h1>
                    <h2>Next Steps:</h2>
                    <textarea id="nextSteps" class="inputTextArea" name="nextSteps" rows="5" cols="70" runat="server">We should try establishing contact with Jane Smith and John Doe.</textarea>
                </div>
                <%-- Other Form End --%>
            </div>
        </div>
    </div>
    <div style="margin-top: 2%"></div>
</asp:Content>
