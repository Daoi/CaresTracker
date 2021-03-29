<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="ResidentInteractionForm.aspx.cs" Inherits="CapstoneUI.ResidentInteractionForm" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--<link href="style/InteractionFormStyle.css" rel="stylesheet" />-->
    <div id="RIForm" class="container-fluid">
        <div class="container-fluid homepage">
            <div class="row modal-header offwhiteBackground" style="height: 5%; padding-left: 0!important; padding-right: 0!important; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkBtnHome_Click">Dashboard</asp:LinkButton></li>
                        <li class="breadcrumb-item active" aria-current="page">Interaction Form</li>
                    </ol>
                </nav>
                <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2 px-0" Style="width: 70%"></asp:Label>
            </div>
            <div class="container w-75 mt-5">
                <div id="logo" class="row">
                    <div class="col border-right">
                        <nav class="nav flex-column">
                            <asp:LinkButton ID="residentInfo" CssClass="active my-3" runat="server" OnClick="formNav_Click">Resident Information</asp:LinkButton>
                            <asp:LinkButton ID="housingInfo" CssClass="active my-3" runat="server" OnClick="formNav_Click">Housing Information</asp:LinkButton>
                            <asp:LinkButton ID="meetingInfo" CssClass="active my-3" runat="server" OnClick="formNav_Click">Meeting Information<i id="icErrorMeetingInfo" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                            <asp:LinkButton ID="residentHealth" CssClass="active my-3" runat="server" OnClick="formNav_Click">Resident Health<i id="icErrorResidentHealth" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                            <asp:LinkButton ID="services" CssClass="active my-3" runat="server" OnClick="formNav_Click">Services<i id="icServices" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                            <asp:LinkButton ID="vaccineInfo" CssClass="active my-3" runat="server" OnClick="formNav_Click">Vaccine Information<i id="icErrorVaxInfo" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                            <asp:LinkButton ID="otherInfo" CssClass="active my-3" runat="server" OnClick="formNav_Click">Action Plan<i id="icErrorActionPlan" runat="server" visible="false" class="fas fa-exclamation-triangle" style="margin-left: .5rem; color: yellow;"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkBtnSave" CssClass="active my-3" runat="server" OnClick="lnkBtnSave_Click"><i id="icoSave" runat="server" class="fas fa-save" style="margin-right: .5rem"></i>Save Interaction</asp:LinkButton>
                            <asp:LinkButton ID="lnkBtnEdit" CssClass="active my-3" runat="server" Visible="false" OnClick="lnkBtnEdit_Click">
                                <i class="fas fa-edit" id="icoEdit" style="margin-right: .5rem" runat="server"></i>
                                Edit Interaction
                            </asp:LinkButton>
                            <asp:Label ID="lblSave" runat="server" CssClass="active my-3" Text="Label" Visible="false"></asp:Label>
                            <asp:Label ID="lblHome" runat="server" Text="" Visible="false"></asp:Label>
                        </nav>
                    </div>
                    <%-- Resident Info Form Start --%>
                    <div class="col-9">
                        <asp:Panel class="residentInfo" ID="pnlResidentInfoForm" runat="server">
                            <h5>Resident Info</h5>
                            <div class="row m-3">
                                <label>Full Name</label>
                                <asp:TextBox ID="tbFirstName" runat="server" placeholder="Resident First Name" CssClass="form-control my-1"></asp:TextBox>
                                <asp:TextBox ID="tbLastName" runat="server" placeholder="Resident Last Name" CssClass="form-control my-1"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>Age</label>
                                <asp:TextBox ID="tbAge" runat="server" placeholder="Resident Age" CssClass="form-control my-1"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>Gender</label>
                                <asp:TextBox ID="tbGender" runat="server" placeholder="Resident Gender" CssClass="form-control my-1"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>Phone</label>
                                <asp:TextBox ID="tbPhone" runat="server" placeholder="Resident Phone" TextMode="Phone" CssClass="form-control my-1"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>Email</label>
                                <asp:TextBox ID="tbEmail" runat="server" placeholder="Resident Email" TextMode="Phone" CssClass="form-control my-1"></asp:TextBox>
                            </div>
                        </asp:Panel>
                        <%-- Resident Info Form End --%>
                        <%-- Housing Info Form Start--%>
                        <asp:Panel class="housingInfo" ID="pnlHousingInfoForm" runat="server">
                            <h5>Housing Info</h5>
                            <div class="row m-3">
                                <label>Number of residents</label>
                                <asp:TextBox ID="tbResidenceOccupants" runat="server" placeholder="How many people does the resident live with?" CssClass="form-control my-1"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>HCV or PHA Development</label>
                                <asp:DropDownList ID="ddlHousingType" CssClass="form-control" runat="server">
                                    <asp:ListItem>Select Housing Type</asp:ListItem>
                                    <asp:ListItem Value="HCV">Housing Choice Voucher</asp:ListItem>
                                    <asp:ListItem Value="Development">PHA Development</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="row m-3">
                                <label>PHA Development Name</label>
                                <asp:TextBox ID="tbDevelopmentName" runat="server" placeholder="Development Name" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>Resident's Region</label>
                                <asp:TextBox ID="tbRegion" runat="server" placeholder="Resident's Region" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>Resident's Address</label>
                                <asp:TextBox ID="tbResidentAddress" runat="server" placeholder="Resident's Address" CssClass="form-control"></asp:TextBox>
                            </div>
                        </asp:Panel>
                        <%-- Housing Info Form End --%>
                        <%-- Meeting Info Form Start --%>
                        <asp:Panel ID="pnlMeetingInfoForm" class="meetingInfo" runat="server">
                            <h5>Meeting Info</h5>
                            <asp:Label ID="lblErrorMeetingInfo" runat="server" Text="Please fill out both meeting type and location." CssClass="h4 alert-danger" Visible="false"></asp:Label>
                            <div class="row m-3">
                                <label>Meeting Type</label>
                                <asp:DropDownList ID="ddlMeetingType" CssClass="form-control" runat="server">
                                    <asp:ListItem>Select Meeting Type</asp:ListItem>
                                    <asp:ListItem>Phone</asp:ListItem>
                                    <asp:ListItem>Email</asp:ListItem>
                                    <asp:ListItem>In-Person</asp:ListItem>
                                    <asp:ListItem>Virtual</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="row m-3">
                                <label>Location</label>
                                <asp:TextBox ID="tbLocation" runat="server" placeholder="Meeting Location" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <label>Date of Contact</label>
                                <asp:TextBox ID="tbDoC" runat="server" placeholder="Date of Contact" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            </div>
                        </asp:Panel>
                        <%-- Meeting Info Form End --%>
                        <%-- Resident Health Form Start --%>
                        <asp:Panel ID="pnlResidentHealthForm" class="residentHealth w-100" Style="display: inline-grid" runat="server">
                            <h5>Resident Health</h5>
                            <asp:Label ID="lblErrorSymptoms" runat="server" CssClass="h4 alert-danger"></asp:Label>
                            <%-- Symtpom List Start --%>
                            <label class="pl-3">Covid-19 Possible Symptoms</label>
                            <div class="row pl-3">
                                <%-- Value after _ represents the ID, sorry future developers --%>
                                <div class="col">
                                    <asp:CheckBox ID="cbShortnessOfBreath_1" runat="server" CssClass="inputCheckBox" Text="Shortness of Breath" Font-Size="Medium" />
                                </div>
                                <div class="col">
                                    <asp:CheckBox ID="cbNausea_2" runat="server" CssClass="inputCheckBox" Text="Nausea or Vomiting" Font-Size="Medium" />
                                </div>
                                <div class="col">
                                    <asp:CheckBox ID="cbFatigue_3" runat="server" CssClass="inputCheckBox" Text="Fatigue" Font-Size="Medium" />
                                </div>
                            </div>
                            <div class="row pl-3">
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
                            <div class="row pl-3">
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
                            <label class="pl-3">Critical Symptoms(Seek Immediate Medical Attention)</label>
                            <div class="row pl-3">
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
                            <div class="row pl-3">
                                <div class="col-sm-4 col-md-4">
                                    <asp:CheckBox ID="cbInabilityToAwake_13" runat="server" CssClass="inputCheckBox" Text="Inability to stay awake" Font-Size="Medium" />
                                </div>
                                <div class="col-sm-4 col-md-8">
                                    <asp:CheckBox ID="cbChestPain_14" runat="server" CssClass="inputCheckBox" Text="Persistent Chest Pain or Pressure" Font-Size="Medium" />
                                </div>
                            </div>
                            <p></p>
                            <%-- Symptom List End --%>
                            <div class="row m-3">
                                <label>Dates Symptoms Occured</label>
                                <asp:TextBox ID="tbSymptomDates" runat="server" placeholder="Date of Symptoms" CssClass="form-control" TextMode="Date"></asp:TextBox>
                            </div>
                            <asp:Label ID="lblErrorCOVIDTest" runat="server" CssClass="h4 alert-danger"></asp:Label>
                            <div class="row m-3">
                                <label>Covid-19 Test Results</label>
                                <asp:DropDownList ID="ddlTestResult" CssClass="form-control" runat="server" Height="2em">
                                    <asp:ListItem>Select Test Result</asp:ListItem>
                                    <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                    <asp:ListItem Value="Negative">Negative</asp:ListItem>
                                    <asp:ListItem Value="No Recent Test">No Recent Test</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="row m-3">
                                <label>Testing Location</label>
                                <asp:TextBox ID="tbTestingLocation" runat="server" placeholder="Testing Location" CssClass="form-control"></asp:TextBox>
                            </div>
                        </asp:Panel>
                        <%-- Resident Health Form End --%>
                        <%-- Services Form Start --%>
                        <asp:Panel ID="pnlServicesForm" class="services" runat="server">
                            <h5>Social Services</h5>
                            <div class="row m-3 h-100">
                                <div id="divNewInteractionServices" class="cblServices overflow-auto h-100 w-100" runat="server">
                                    <label>Services</label>
                                    <asp:CheckBoxList ID="cblServices" CssClass="table" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                                </div>
                                <div id="divOldInteractionServices" class="cblServicesCompleted overflow-auto h-75" runat="server">
                                    <label>Services Requested (Check if completed)</label>
                                    <asp:CheckBoxList ID="cblCompletedServices" CssClass="table" runat="server" RepeatDirection="Vertical"></asp:CheckBoxList>
                                </div>
                                <div id="divFollowUpRequired" class="row m-3" runat="server">
                                    <asp:Label ID="lblFollowUp" for="ddlFollowUp" runat="server" Text="Does this interaction require a follow up?"></asp:Label>
                                    <asp:DropDownList ID="ddlFollowUp" CssClass="form-control" runat="server">
                                        <asp:ListItem>Select Option</asp:ListItem>
                                        <asp:ListItem Value="false">Doesn't Require Follow Up</asp:ListItem>
                                        <asp:ListItem Value="true">Requires Follow up</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblFollowUpError" runat="server" CssClass="h4 alert-danger" Text="Must select whether the service requires follow up."></asp:Label>
                                </div>
                                <div id="divFollowUpStatus" class="row m-3" runat="server">
                                    <asp:Label ID="lblFollowUpStatus" for="ddlFollowUpStatus" runat="server" Text="Does this interaction still require follow up?"></asp:Label>
                                    <asp:DropDownList ID="ddlFollowUpStatus" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="incomplete">Still requires follow up</asp:ListItem>
                                        <asp:ListItem Value="complete">Follow up completed</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="row m-3">
                                    <asp:Button ID="btnUpdateServices" CssClass="buttonStyle" runat="server" Text="Update Services Status" Visible="False" OnClick="btnUpdateServices_Click" />
                                    <asp:Label ID="lblUpdateServices" CssClass="ml-1" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </asp:Panel>
                        <p></p>
                        <%-- Services Form End --%>
                        <%-- Vaccine Form Start --%>
                        <asp:Panel ID="pnlVaccineForm" class="vaccineInfo" runat="server">
                            <h5>Vaccine Info</h5>
                            <div class="row m-3">
                                <asp:Label ID="lblErrorVaccine" runat="server" Text="Please fill out vaccine interest and eligibility." CssClass="h4 alert-danger" Visible="false"></asp:Label>
                                <label>Vaccine Interest</label>
                                <asp:DropDownList ID="ddlVaccineInterest" CssClass="form-control" runat="server">
                                    <asp:ListItem>Select Vaccine Interest</asp:ListItem>
                                    <asp:ListItem Value="True">Interested in vaccine</asp:ListItem>
                                    <asp:ListItem Value="False">Not interested in vaccine</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="row m-3">
                                <label>Vaccine Phase</label>
                                <asp:DropDownList ID="ddlVaccineEligibility" CssClass="form-control" runat="server">
                                    <asp:ListItem>Select Eligibility</asp:ListItem>
                                    <asp:ListItem Value="0">Phase1A</asp:ListItem>
                                    <asp:ListItem Value="1">Phase1B</asp:ListItem>
                                    <asp:ListItem Value="2">Phase1C</asp:ListItem>
                                    <asp:ListItem Value="3">Phase2</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div runat="server" id="divVaccineAppointment" class="row m-3">
                                <label>Vaccine Appointment Date</label>
                                <asp:TextBox ID="tbVaccineAppointmentDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            </div>
                        </asp:Panel>
                        <%-- Vaccine Form End --%>
                        <%-- Other Form Start --%>
                        <asp:Panel ID="pnlOtherForm" class="otherInfo" runat="server">
                            <h5>Action Plan</h5>
                            <div class="row m-3">
                                <asp:Label ID="lblErrorActionPlan" runat="server" Text="Please fill out the Action Plan." CssClass="h4 alert-danger" Visible="false"></asp:Label>
                                <label>Next Steps:</label>
                                <textarea id="nextSteps" class="form-control" name="nextSteps" rows="5" cols="70" runat="server"></textarea>
                            </div>
                        </asp:Panel>
                        <%-- Other Form End --%>
                    </div>
                </div>
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
                            <textarea id="taEditReason" class="form-control" cols="50" rows="5" runat="server"></textarea>
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
