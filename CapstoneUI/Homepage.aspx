<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="CapstoneUI.h" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage backgroundblue">
        <div class="row  modal-header" style="height: 7%; padding-left: 0!important; padding-right: 0!important; font-size: large">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-white">
                    <li class="breadcrumb-item" style="color: deepskyblue">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server">Dashboard</asp:LinkButton></li>
                </ol>
            </nav>
            <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 57%"></asp:Label>
        </div>
        <div class="jumbotron vertical-center bg-transparent">
            <div class="mt-5" style="margin-left: 50%">
                <h2>Helpful Links</h2>
                <p>
                    <a href="http://www.pha.phila.gov/"><span style="font-weight: bold">Philadelphia Housing Authority Website</span> </a>
                    <br />
                    <a href="https://templelnpwi.org/"><span style="font-weight: bold">Temple Lenfest North Philadelphia Work Initiative</span></a>
                    <br />
                    <a href="https://www.templehealth.org/"><span style="font-weight: bold">Temple Health</span></a>
                    <br />
                    <a href="https://www.cdc.gov/coronavirus/2019-ncov/index.html"><span style="font-weight: bold">CDC Coronavirus Guidelines</span></a>
                    <br />
                </p>
            </div>
            <div class="container-fluid">
                <%-- Button 1 Start --%>
                <div id="divCreateCHW" class="row form-inline form-group" runat="server">
                    <div class="col-md-2 col-sm-1"><i class="fas fa-plus-square" style="padding-left: 10vw; margin-top: 10%; font-size: 5rem"></i></div>
                    <div class="col-md-4 quickAccess col-sm-1">
                        <asp:LinkButton ID="btnCHWCreateAccount" runat="server" Text="Create CHW Account" OnClick="btnCHWCreateAccount_Click"></asp:LinkButton>
                        <div>
                            <p>Create a new Community Health Worker Account.</p>
                        </div>
                    </div>
                </div>
                <%-- Button 1 End --%>
                <%-- Button 2 Start --%>
                <div class="row form-inline form-group">
                    <div class="col-md-2 col-sm-1"><i class="fas fa-plus-square" style="padding-left: 10vw; margin-top: 10%; font-size: 5rem"></i></div>
                    <div class="col-md-4 col-sm-1 quickAccess">
                        <asp:LinkButton ID="btnCreateResidentProfile" runat="server" Text="Create Resident Profile" OnClick="btnCreateResidentProfile_Click"></asp:LinkButton>
                        <div>
                            <p>Create a profile for a resident.</p>
                        </div>
                    </div>
                </div>
                <%-- Button 2 End --%>
                <%-- Button 3 Start --%>
                <div class="row form-inline form-group">
                    <div class="col-md-2 col-sm-1"><i class="fas fa-plus-square" style="padding-left: 10vw; margin-top: 10%; font-size: 5rem"></i></div>
                    <div class="col-md-4 col-sm-1 quickAccess">
                        <asp:LinkButton ID="lnkCreateEvent" runat="server" Text="Create Event" OnClick="btnCreateEvent_Click1"></asp:LinkButton>
                        <div>
                            <p>Create a new community health event.</p>
                        </div>
                    </div>
                </div>
                <%-- Button 3 End --%>
                <%-- Button 4 Start --%>
                <div class="row form-inline form-group">
                    <div class="col-md-2 col-sm-1"><i class="fas fa-search" style="padding-left: 10vw; margin-top: 10%; font-size: 4.5rem"></i></div>
                    <div class="col-md-4 quickAccess col-sm-1">
                        <asp:LinkButton ID="btnReviewInteractions" runat="server" Text="Review Past Interactions" OnClick="btnReviewInteractions_Click"></asp:LinkButton>
                        <div>
                            <p>Search for past interactions by resident name.</p>
                        </div>
                    </div>
                </div>
                <%-- Button 4 End --%>
                <%-- Button 5 Start --%>
                <div class="row form-inline form-group ">
                    <div class="col-md-2 col-sm-1"><i class="fas fa-search" style="padding-left: 10vw; margin-top: 10%; font-size: 4.5rem"></i></div>
                    <div class="col-md-4 quickAccess col-sm-1">
                        <%-- Change this button ID probably --%>
                        <asp:LinkButton ID="btnCreateEvent" runat="server" Text="Review Past Event" OnClick="btnCreateEvent_Click"></asp:LinkButton>
                        <div>
                            <p>Search for past community health events.</p>
                        </div>
                    </div>
                </div>
                <%-- Button 5 End --%>
                <%-- Button 6 Start --%>
                <div class="row form-inline form-group ">
                    <div class="col-md-2 col-sm-1"><i class="fas fa-search" style="padding-left: 10vw; margin-top: 10%; font-size: 4.5rem"></i></div>
                    <div class="col-md-4 quickAccess col-sm-1">
                        <%-- Change this button ID probably --%>
                        <asp:LinkButton ID="btnResidentLookUp" runat="server" Text="Resident Look Up" OnClick="btnResidentLookUp_Click"></asp:LinkButton>
                        <div>
                            <p>Search for a specific resident's profile.</p>
                        </div>
                    </div>
                </div>
                <%-- Button 6 End --%>
            </div>
        </div>
    </div>
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
</asp:Content>
