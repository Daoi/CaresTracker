﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="ResidentInteractionForm.aspx.cs" Inherits="CapstoneUI.ResidentInteractionForm" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="style/InteractionFormStyle.css" rel="stylesheet" />

    <div id="RIForm" class="container-fluid">
        <div id="logo">
            <h1 class="logo"></h1>
            <div class="leftbox">
                <nav>
                    <asp:LinkButton ID="residentInfo" CssClass="active" runat="server" OnClick="formNav_Click">Resident Information</asp:LinkButton>
                    <asp:LinkButton ID="housingInfo" CssClass="active" runat="server" OnClick="formNav_Click">Housing Information</asp:LinkButton>
                    <asp:LinkButton ID="meetingInfo" CssClass="active" runat="server" OnClick="formNav_Click">Meeting Information<i id="icErrorMeetingInfo" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                    <asp:LinkButton ID="residentHealth" CssClass="active" runat="server" OnClick="formNav_Click">Resident Health<i id="icErrorResidentHealth" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                    <asp:LinkButton ID="services" CssClass="active" runat="server" OnClick="formNav_Click">Services</asp:LinkButton>
                    <asp:LinkButton ID="vaccineInfo" CssClass="active" runat="server" OnClick="formNav_Click">Vaccine Information<i id="icErrorVaxInfo" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                    <asp:LinkButton ID="otherInfo" CssClass="active" runat="server" OnClick="formNav_Click">Action Plan<i id="icErrorActionPlan" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnSave" CssClass="active" runat="server" OnClick="lnkBtnSave_Click"><i id="icoSave" runat="server" class="fas fa-save" style="margin-right: .5rem"></i>Save Interaction</asp:LinkButton>
                    <asp:LinkButton ID="lnkBtnEdit" CssClass="active" runat="server" Visible="false" OnClick="lnkBtnEdit_Click">
                        <i class="fas fa-edit" id="icoEdit" style="margin-right: .5rem" runat="server"></i>
                        Edit Interaction
                    </asp:LinkButton>


                    <asp:Label ID="lblSave" runat="server" CssClass="active" Text="Label" Visible="false"></asp:Label>
                    <asp:LinkButton ID="lnkBtnHome" CssClass="active" runat="server" OnClick="lnkBtnHome_Click"><i class="fas fa-home" style="margin-right: .5rem"></i>Return To Dashboard<i id="warningHome" runat="server" visible="false" class="fas fa-exclamation-triangle"></i></asp:LinkButton>
                    <asp:Label ID="lblHome" runat="server" Text="" Visible="false"></asp:Label>
                </nav>
            </div>
            <%-- Resident Info Form Start --%>
            <div class="rightbox">
                <asp:Panel class="residentInfo" ID="pnlResidentInfoForm" runat="server">
                    <h1>Resident Info</h1>
                    <h2>Full Name</h2>
                    <p>
                        <asp:TextBox ID="tbFirstName" runat="server" placeholder="Resident First Name" CssClass="inputText"></asp:TextBox>
                    </p>
                    <p>
                        <asp:TextBox ID="tbLastName" runat="server" placeholder="Resident Last Name" CssClass="inputText"></asp:TextBox>
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
                </asp:Panel>
                <%-- Resident Info Form End --%>
                <%-- Housing Info Form Start--%>
                <asp:Panel class="housingInfo" ID="pnlHousingInfoForm" runat="server">
                    <h1>Housing Info</h1>
                    <h2>Number of residents</h2>
                    <p>
                        <asp:TextBox ID="tbResidenceOccupants" runat="server" placeholder="How many people does the resident live with?" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>HCV or PHA Development</h2>
                    <asp:DropDownList ID="ddlHousingType" CssClass="inputDropDown" runat="server">
                        <asp:ListItem>Select Housing Type</asp:ListItem>
                        <asp:ListItem Value="HCV">Housing Choice Voucher</asp:ListItem>
                        <asp:ListItem Value="Development">PHA Development</asp:ListItem>
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
                </asp:Panel>
                <%-- Housing Info Form End --%>
                <%-- Meeting Info Form Start --%>
                <asp:Panel ID="pnlMeetingInfoForm" class="meetingInfo" runat="server">
                    <h1>Meeting Info</h1>
                    <asp:Label ID="lblErrorMeetingInfo" runat="server" Text="Please fill out both meeting type and location." CssClass="h4 alert-danger" Visible="false"></asp:Label>
                    <div class="mb-3"></div>
                    <h2>Meeting Type</h2>
                    <asp:DropDownList ID="ddlMeetingType" CssClass="inputDropDown" runat="server">
                        <asp:ListItem>Select Meeting Type</asp:ListItem>
                        <asp:ListItem>Phone</asp:ListItem>
                        <asp:ListItem>Email</asp:ListItem>
                        <asp:ListItem>In-Person</asp:ListItem>
                        <asp:ListItem>Virtual</asp:ListItem>
                    </asp:DropDownList>
                    <p></p>
                    <h2>Location</h2>
                    <p>
                        <asp:TextBox ID="tbLocation" runat="server" placeholder="Meeting Location" CssClass="inputText"></asp:TextBox>
                    </p>
                    <h2>Date of Contact</h2>
                    <p>
                        <asp:TextBox ID="tbDoC" runat="server" placeholder="Date of Contact" TextMode="Date" CssClass="inputText"></asp:TextBox>
                    </p>
                </asp:Panel>
                <%-- Meeting Info Form End --%>
                <%-- Resident Health Form Start --%>
                <asp:Panel ID="pnlResidentHealthForm" class="residentHealth form-group" Style="display: inline-grid" runat="server">
                    <h1>Resident Health</h1>
                    <asp:Label ID="lblErrorSymptoms" runat="server" CssClass="h4 alert-danger"></asp:Label>
                    <div class="mb-3"></div>
                    <%-- Symtpom List Start --%>
                    <h2>Covid-19 Possible Symptoms</h2>
                    <div class="row">
                        <%-- Value after _ represents the ID, sorry future developers --%>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbShortnessOfBreath_1" runat="server" CssClass="inputCheckBox" Text="Shortness of Breath" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbNausea_2" runat="server" CssClass="inputCheckBox" Text="Nausea or Vomiting" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbFatigue_3" runat="server" CssClass="inputCheckBox" Text="Fatigue" Font-Size="Medium" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbAches_4" runat="server" CssClass="inputCheckBox" Text="Body/Muscle Aches" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbHeadaches_5" runat="server" CssClass="inputCheckBox" Text="Headaches" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbSoreThroat_6" runat="server" CssClass="inputCheckBox" Text="Sore Throat" Font-Size="Medium" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbTasteOrSmell_7" runat="server" CssClass="inputCheckBox" Text="New Loss of Taste or Smell" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbCongestion_8" runat="server" CssClass="inputCheckBox" Text="Congestion or Runny Nose" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbCough_9" runat="server" CssClass="inputCheckBox" Text="Cough" Font-Size="Medium" />
                        </div>
                    </div>
                    <p></p>
                    <h2>Critical Symptoms(Seek Immediate Medical Attention)</h2>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbConfusion_10" runat="server" CssClass="inputCheckBox" Text="Confusion" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbTroubleBreathing_11" runat="server" CssClass="inputCheckBox" Text="Trouble Breathing" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbBlueLips_12" runat="server" CssClass="inputCheckBox" Text="Blueish Lips" Font-Size="Medium" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            <asp:CheckBox ID="cbInabilityToAwake_13" runat="server" CssClass="inputCheckBox" Text="Inability to stay awake" Font-Size="Medium" />
                        </div>
                        <div class="col-sm-4 col-md-8">
                            <asp:CheckBox ID="cbChestPain_14" runat="server" CssClass="inputCheckBox" Text="Persistent Chest Pain or Pressure" Font-Size="Medium" />
                        </div>
                    </div>
                    <p></p>
                    <%-- Symptom List End --%>
                    <h2>Dates Symptoms Occured</h2>
                    <p>
                        <asp:TextBox ID="tbSymptomDates" runat="server" placeholder="Date of Symptoms" CssClass="inputText" TextMode="Date"></asp:TextBox>
                    </p>
                    <asp:Label ID="lblErrorCOVIDTest" runat="server" CssClass="h4 alert-danger"></asp:Label>
                    <div class="mb-3"></div>
                    <h2>Covid-19 Test Results</h2>
                    <asp:DropDownList ID="ddlTestResult" CssClass="inputDropDown" runat="server" Height="2em">
                        <asp:ListItem >Select Test Result</asp:ListItem>
                        <asp:ListItem Value="Positive">Positive</asp:ListItem>
                        <asp:ListItem Value="Negative">Negative</asp:ListItem>
                        <asp:ListItem Value="No Recent Test">No Recent Test</asp:ListItem>
                    </asp:DropDownList>
                    <p></p>
                    <h2>Testing Location</h2>
                    <p>
                        <asp:TextBox ID="tbTestingLocation" runat="server" placeholder="Testing Location" CssClass="inputText"></asp:TextBox>
                    </p>
                </asp:Panel>
                <%-- Resident Health Form End --%>
                <%-- Services Form Start --%>
                <asp:Panel ID="pnlServicesForm" class="services" runat="server">
                    <h1>Social Services</h1>
                    <div id="divNewInteractionServices" class="cblServices" runat="server">
                        <h2>Services</h2>
                        <asp:CheckBoxList ID="cblServices" CssClass="table" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                    </div>
                    <div id="divOldInteractionServices" class="cblServicesCompleted" runat="server">
                        <h2>Services Requested (Check if completed)</h2>
                        <asp:CheckBoxList ID="cblCompletedServices" CssClass="table" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                    </div>
                    <div class="row">
                        <asp:Button ID="btnUpdateServices" CssClass="btn-primary btn" runat="server" Text="Update Services" Visible="False" OnClick="btnUpdateServices_Click" />
                        <asp:Label ID="lblUpdateServices" CssClass="ml-1" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                </asp:Panel>
                <p></p>
                <%-- Services Form End --%>
                <%-- Vaccine Form Start --%>
                <asp:Panel ID="pnlVaccineForm" class="vaccineInfo" runat="server">
                    <h1>Vaccine Info</h1>
                    <asp:Label ID="lblErrorVaccine" runat="server" Text="Please fill out vaccine interest and eligibility." CssClass="h4 alert-danger" Visible="false"></asp:Label>
                    <div class="mb-3"></div>
                    <h2>Vaccine Interest</h2>
                    <asp:DropDownList ID="ddlVaccineInterest" CssClass="inputDropDown" runat="server">
                        <asp:ListItem>Select Vaccine Interest</asp:ListItem>
                        <asp:ListItem Value="True">Interested in vaccine</asp:ListItem>
                        <asp:ListItem Value="False">Not interested in vaccine</asp:ListItem>
                    </asp:DropDownList>
                    <p></p>
                    <h2>Vaccine Phase</h2>
                    <asp:DropDownList ID="ddlVaccineEligibility" CssClass="inputDropDown" runat="server">
                        <asp:ListItem>Select Eligibility</asp:ListItem>
                        <asp:ListItem Value="0">Phase1A</asp:ListItem>
                        <asp:ListItem Value="1">Phase1B</asp:ListItem>
                        <asp:ListItem Value="2">Phase1C</asp:ListItem>
                        <asp:ListItem Value="3">Phase2</asp:ListItem>
                    </asp:DropDownList>
                    <p></p>
                    <div runat="server" id="divVaccineAppointment">
                        <h2>Vaccine Appointment Date</h2>
                        <p>
                            <asp:TextBox ID="tbVaccineAppointmentDate" runat="server" TextMode="Date" CssClass="inputText"></asp:TextBox>
                        </p>
                    </div>
                </asp:Panel>
                <%-- Vaccine Form End --%>
                <%-- Other Form Start --%>
                <asp:Panel ID="pnlOtherForm" class="otherInfo" runat="server">
                    <h1>Action Plan</h1>
                    <asp:Label ID="lblErrorActionPlan" runat="server" Text="Please fill out the Action Plan." CssClass="h4 alert-danger" Visible="false"></asp:Label>
                    <div class="mb-3"></div>
                    <h2>Next Steps:</h2>
                    <textarea id="nextSteps" class="inputTextArea" name="nextSteps" rows="5" cols="70" runat="server"></textarea>
                </asp:Panel>
                <%-- Other Form End --%>
            </div>
        </div>
    </div>
    <div style="margin-top: 2%"></div>
    <!-- Modal -->
    <div class="modal fade" id="modalEditReason" tabindex="-1" role="dialog"
        aria-labelledby="modalEditReasonTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4>Edit Submission</h4>
                </div>
                <asp:UpdatePanel ID="pnlModalControls" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body px-5 pb-3">
                            <h5>Reason For Edit</h5>
                            <asp:Label ID="lblModalError" runat="server" Text="" CssClass="alert-danger modalError"></asp:Label>
                            <br />
                            <textarea id="taEditReason" class="inputTextArea" cols="50" rows="5" runat="server"></textarea>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnEditCancel" CssClass="btn btn-danger mr-3" runat="server" Text="Cancel" OnClick="btnEditCancel_Click" />
                            <asp:Button ID="btnEditSubmit" CssClass="btn btn-primary" runat="server" Text="Save Edit" OnClick="btnEditSubmit_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- End Modal -->

</asp:Content>
