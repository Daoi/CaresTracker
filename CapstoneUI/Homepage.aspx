<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="CapstoneUI.h" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage backgroundblue">
        <div class="row  modal-header" style="height: auto; padding-left: 0!important; padding-right: 0!important; font-size: large">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-white">
                    <li class="breadcrumb-item" style="color: deepskyblue">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server">Dashboard</asp:LinkButton>
                    </li>
                </ol>
            </nav>
            <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 57%"></asp:Label>
        </div>
        <div class="jumbotron vertical-center bg-transparent">
            <div class="container-fluid">
                <div class="row homepageCol">
                    <div id="divCreateCHW" class="col m-3 homepageCol" runat="server">
                        <!-- Button 1 Start -->
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <asp:LinkButton ID="btnCHWCreateAccount" CssClass="card-title font-weight-bold h5" runat="server" Text="Create CHW Account" OnClick="btnCHWCreateAccount_Click"></asp:LinkButton>
                                <p>Create a new Community Health Worker Account.</p>
                            </div>
                        </div>
                        <%-- Button 1 End --%>
                        <a class="stretched-link" href="CreateCHW.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 2 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <asp:LinkButton ID="btnCreateResidentProfile" CssClass="card-title font-weight-bold h5" runat="server" Text="Create Resident Profile" OnClick="btnCreateResidentProfile_Click"></asp:LinkButton>
                                <p>Create a profile for a resident.</p>
                            </div>
                        </div>
                        <%-- Button 2 End --%>
                        <a class="stretched-link" href="CreateResident.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 3 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <asp:LinkButton ID="lnkCreateEvent" runat="server" CssClass="card-title font-weight-bold h5" Text="Create Event" OnClick="btnCreateEvent_Click1"></asp:LinkButton>
                                <p>Create a new community health event.</p>
                            </div>
                        </div>
                        <%-- Button 3 End --%>
                        <a class="stretched-link" href="EventCreator.aspx"></a>
                    </div>
                </div>
                <div class="row">
                    <div class="col m-3 homepageCol">
                        <%-- Button 4 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <asp:LinkButton ID="btnReviewInteractions" CssClass="card-title font-weight-bold h5" runat="server" Text="Review Past Interactions" OnClick="btnReviewInteractions_Click"></asp:LinkButton>
                                <p>Search for past interactions by resident name.</p>
                            </div>
                        </div>
                        <%-- Button 4 End --%>
                        <a class="stretched-link" href="InteractionList.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 5 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <%-- Change this button ID probably --%>
                                <asp:LinkButton ID="btnCreateEvent" runat="server" CssClass="card-title font-weight-bold h5" Text="Review Past Event" OnClick="btnCreateEvent_Click"></asp:LinkButton>
                                <p>Search for past community health events.</p>
                            </div>
                        </div>
                        <%-- Button 5 End --%>
                        <a class="stretched-link" href="EventCreator.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 6 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <%-- Change this button ID probably --%>
                                <asp:LinkButton ID="btnResidentLookUp" runat="server" CssClass="card-title font-weight-bold h5" Text="Resident Look Up" OnClick="btnResidentLookUp_Click"></asp:LinkButton>
                                <p>Search for a specific resident's profile.</p>
                            </div>
                        </div>
                        <%-- Button 6 End --%>
                        <a class="stretched-link" href="ResidentLookUp.aspx"></a>
                    </div>
                </div>
            </div>
            <div class="container-fluid mt-5">
                <div class="row">
                    <div class="col m-3">
                        <%-- Follow Up Tracker Start --%>
                        <div class="card w-100">
                            <div class="card-header">
                                <div class="row pl-1">
                                    <ul class="nav nav-tabs card-header-tabs" id="follow-up-tracker" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" href="#outstanding" role="tab" aria-controls="Outstanding" aria-selected="true">Oustanding</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="#completed" role="tab" aria-controls="Completed" aria-selected="false">Completed</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="card-body followup-tracker">
                                <h4 class="card-title">Follow Ups</h4>
                                <div class="tab-content mt-3">
                                    <div class="tab-pane active" id="outstanding" role="tabpanel">
                                        <asp:Label ID="lblOutstandingMsg" runat="server" Text="Label"></asp:Label>
                                        <asp:GridView ID="gvOutstandingFollowUps" CssClass="table table-striped table-bordered thead-dark" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="DateOfContact" HeaderText="Date Requested" />
                                                <asp:TemplateField HeaderText="Resident">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResidentName" runat="server" Text='<%#Eval("FirstName")+ " " + Eval("LastName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InteractionID" HeaderText="Interaction(Make link)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="tab-pane" id="completed" role="tabpanel" aria-labelledby="completed-tab">
                                        <asp:Label ID="lblCompletedMsg" runat="server" Text="Label"></asp:Label>
                                        <asp:GridView ID="gvCompletedFollowUps" CssClass="table table-striped table-bordered thead-dark" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="FollowUpCompleted" HeaderText="Date Completed" />
                                                <asp:TemplateField HeaderText="Resident">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("FirstName")+ " " + Eval("LastName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InteractionID" HeaderText="Interaction(Make Link)" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- Follow Up Tracker End --%>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="row">
                    <div class="col m-3">
                        <div class="card mt-5">
                            <div class="card-body">
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
                        </div>
                    </div>
                    <div class="col m-3">
                        <div class="card mt-5">
                            <div class="card-body">
                                <p>
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. 
                                    Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis 
                                    ipsum. Praesent mauris. Fusce nec tellus sed augue semper porta. Mauris massa. Vestibulum lacinia 
                                    arcu eget nulla. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos 
                                    himenaeos. Curabitur sodales ligula in libero. Sed dignissim lacinia nunc. Curabitur tortor. Pellentesque 
                                    nibh. Aenean quam. In scelerisque sem at dolor. Maecenas mattis. Sed convallis tristique sem. Proin ut 
                                    ligula vel nunc egestas porttitor. Morbi lectus risus, iaculis vel, suscipit quis, luctus non, massa.
                                    Fusce ac turpis quis ligula lacinia aliquet. 
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
    <%-- Switch tabs for Follow Up Tracker --%>
    <script>$('#follow-up-tracker a').on('click', function (e) {
            e.preventDefault()
            $(this).tab('show')
        })</script>
    <%-- Completed follow ups Data Table --%>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#MainContent_gvCompletedFollowUps').DataTable();
        });
    </script>
    <%-- Uncompleted follow ups Data Table --%>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#MainContent_gvOutstandingFollowUps').DataTable();
        });
    </script>
</asp:Content>
