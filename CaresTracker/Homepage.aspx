<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="CaresTracker.h" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage">
        <div class="row  modal-header offwhiteBackground" style="height: auto; padding-left: 0!important; padding-right: 0!important; font-size: large;">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item">
                        <asp:linkbutton id="lnkHome" navigateurl="~/Homepage.aspx" cssclass="cherryFont" runat="server">Dashboard</asp:linkbutton>
                    </li>
                </ol>
            </nav>
            <asp:label id="lblUserInfo" runat="server" enabled="true" visible="true" cssclass="h3 my-2" style="width: 57%"></asp:label>
        </div>
        <div class="jumbotron vertical-center bg-transparent cherryFont">
            <div class="container-fluid">
                <div class="row homepageCol">
                    <div id="divCreateCHW" class="col m-3 homepageCol" runat="server">
                        <!-- Button 1 Start -->
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <label class="card-title font-weight-bold h5">Create CHW Account</label>
                                <p class="text-dark">Create a new Community Health Worker Account</p>
                            </div>
                        </div>
                        <%-- Button 1 End --%>
                        <a class="stretched-link" href="CreateCHW.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 2 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <label class="card-title font-weight-bold h5">Create Resident Profile</label>
                                <p class="text-dark">Create a profile for a resident</p>
                            </div>
                        </div>
                        <%-- Button 2 End --%>
                        <a class="stretched-link" href="CreateResident.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 3 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <label class="card-title font-weight-bold h5">Create Event</label>
                                <p class="text-dark">Create a new community health event</p>
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
                                <label class="card-title font-weight-bold h5">Review Past Interactions</label>
                                <p class="text-dark">Search for past interactions by resident name</p>
                            </div>
                        </div>
                        <%-- Button 4 End --%>
                        <a class="stretched-link" href="InteractionList.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 5 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <label class="card-title font-weight-bold h5">Resident Look Up</label>
                                <p class="text-dark">Search for a specific resident's profile</p>
                            </div>
                        </div>
                        <%-- Button 5 End --%>
                        <a class="stretched-link" href="ResidentLookUp.aspx"></a>
                    </div>
                    <div class="col m-3 homepageCol">
                        <%-- Button 6 Start --%>
                        <div class="card text-center homepageCard">
                            <div class="card-body shadow">
                                <label class="card-title font-weight-bold h5">Review Past Events</label>
                                <p class="text-dark">Search for past community health events</p>
                            </div>
                        </div>
                        <%-- Button 6 End --%>
                        <a class="stretched-link" href="EventList.aspx"></a>
                    </div>
                </div>
            </div>
            <div class="container-fluid mt-5">
                <div class="row">
                    <div class="col m-3">
                        <%-- Follow Up Tracker Start --%>
                        <div class="card w-100">
                            <div class="card-header offwhiteBackground">
                                <div class="row pl-1">
                                    <ul class="nav nav-tabs card-header-tabs" id="follow-up-tracker" role="tablist">
                                        <li class="nav-item cherryFont">
                                            <a class="nav-link cherryFont active" href="#outstanding" role="tab" aria-controls="Outstanding" aria-selected="true">Oustanding</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link cherryFont" href="#completed" role="tab" aria-controls="Completed" aria-selected="false">Completed</a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                            <div class="card-body followup-tracker text-dark">
                                <h4 id="headerFollowUps" class="card-title">Outstanding Follow Ups</h4>
                                <div class="tab-content mt-3">
                                    <div class="tab-pane active" id="outstanding" role="tabpanel">
                                        <asp:label id="lblOutstandingMsg" runat="server" text=""></asp:label>
                                        <asp:gridview id="gvOutstandingFollowUps" cssclass="table table-striped table-bordered thead-dark" runat="server" autogeneratecolumns="False">
                                            <HeaderStyle CssClass="cherryBackground" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="View Interaction">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewInteraction" runat="server" CssClass="btn btn-light w-100 p-3 font-weight-bold" OnClick="lnkViewOutstandingInteraction_Click">View this Interaction</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resident">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResidentName" runat="server" Text='<%# Eval("ResidentFirstName")+ " " + Eval("ResidentLastName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DateOfContact" HeaderText="Date Requested" />
                                            </Columns>
                                        </asp:gridview>
                                    </div>
                                    <div class="tab-pane" id="completed" role="tabpanel" aria-labelledby="completed-tab">
                                        <asp:label id="lblCompletedMsg" runat="server" text=""></asp:label>
                                        <asp:gridview id="gvCompletedFollowUps" cssclass="table table-striped table-bordered thead-dark" runat="server" autogeneratecolumns="False">
                                            <HeaderStyle CssClass="cherryBackground" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="View Interaction">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewInteraction" CssClass="btn btn-light w-100 p-3 font-weight-bold" runat="server" OnClick="lnkViewCompletedInteraction_Click">View this Interaction</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resident">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResidentName" runat="server" Text='<%#Eval("ResidentFirstName")+ " " + Eval("ResidentLastName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FollowUpCompleted" HeaderText="Date Completed" />
                                            </Columns>
                                        </asp:gridview>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- Follow Up Tracker End --%>
                    </div>
                </div>
            </div>
            <div class="container-fluid mt-5">
                <div class="row">
                    <div class="col m-3">
                        <%-- Event Tracker Start --%>
                        <div class="card-body border text-dark offwhiteBackground">
                            <h4 class="card-title">Upcoming Events</h4>
                            <div class="card mt-5">
                                <div class="card-body UCEventsCard">
                                    <asp:label id="lblEventMsg" runat="server" text=""></asp:label>
                                    <asp:gridview id="gvEvents" cssclass="table table-striped table-bordered thead-dark" runat="server" autogeneratecolumns="False">
                                        <HeaderStyle CssClass="cherryBackground" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="View Event">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkToEvent" runat="server" Text='View This Event' CssClass="btn btn-light w-100 p-3 font-weight-bold" OnClick="lnkToEvent_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                                            <asp:BoundField DataField="EventDate" HeaderText="Event Date" />
                                            <asp:TemplateField HeaderText="Start Time">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartTime" Text='<%# DateTime.Parse(Eval("EventStartTime").ToString()).ToString("hh:mm tt")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Event Host">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChwName" runat="server" Text='<%#Eval("UserFirstName")+ " " + Eval("UserLastName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserEmail" HeaderText="Host Email" />
                                        </Columns>
                                    </asp:gridview>
                                </div>
                            </div>
                        </div>
                        <%-- Event Tracker End --%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
    <%-- Switch tabs for Follow Up Tracker --%>
    <script>$('#follow-up-tracker a').on('click', function (e) {
            e.preventDefault();
            $(this).tab('show');

            if ($(this).text() == 'Completed') {
                $('#headerFollowUps').text('Completed Follow Ups (Past 2 weeks)');
            }
            else {
                $('#headerFollowUps').text('Outstanding Follow Ups');
            }
        });

    </script>
    <%-- Create DataTables --%>
    <script type="text/javascript">
        $(document).ready(function () {
            ['gvCompletedFollowUps', 'gvOutstandingFollowUps', 'gvEvents'].forEach(gv => {
                $(`#MainContent_${gv}`).DataTable({
                    "lengthMenu": [5, 10]
                });
            });
        });
    </script>
</asp:Content>
