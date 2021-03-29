<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="EventCreator.aspx.cs" Inherits="CapstoneUI.EventCreator" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container homepage">
            <div class="row modal-header offwhiteBackground" style="height: 7%; padding-left: 0!important; padding-right: 0!important; font-size: large; padding-bottom: 2%">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                        <li class="breadcrumb-item active" aria-current="page">Create Event</li>
                    </ol>
                </nav>
                <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2 px-0" Style="width: 76%"></asp:Label>
            </div>
            <div class="container-fluid w-75 mt-5">
                <div class="row">
                    <h5>Event General Details</h5>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Event Name:</label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtEventName" placeholder="Event Name" runat="server" CssClass="form-control"></asp:TextBox><br />
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Event Location: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtEventLocation" placeholder="Event Location" runat="server" CssClass="form-control"></asp:TextBox><br />
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Event Date: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtEventDate" placeholder="Event Date" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Event Start Time: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtEventTimeStart" placeholder="Event Time Start" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox><br />
                    </div>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Event End Time: </label>
                    </div>
                    <div class="col-7">
                        <asp:TextBox ID="txtEventTimeEnd" placeholder="Event Time End" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox><br />
                    </div>
                </div>
                          <asp:UpdatePanel ID="upMainHost" UpdateMode="Conditional" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cblUsers" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="row m-3">
                            <div class="col">
                                <label>Main Host:</label>
                            </div>
                            <div class="col-7">
                                <asp:DropDownList ID="ddlMainHost" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="default">Select Main Host</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row m-3">
                    <div class="col">
                        <label>Select Health Workers to Host Event:</label>
                        <div id="userCBLDiv" class="DataboundCBLOverflow row">
                            <asp:CheckBoxList ID="cblUsers" runat="server" CssClass="myCheckBoxList" CellPadding="15" RepeatColumns="4" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <h5>Event Specific Details</h5>
                </div>
                <div class="row m-3">
                    <div class="col">
                        <label>Event Type: </label>
                    </div>
                    <div class="col-7">
                        <asp:DropDownList ID="ddlEventType" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="Select Event Type">Select Event Type</asp:ListItem>
                            <asp:ListItem Value="divResourceTableEvent">Resource Table Event</asp:ListItem>
                            <asp:ListItem Value="divFluShotEvent">Flu Shot Event</asp:ListItem>
                            <asp:ListItem Value="divHealthEducationEvent">Health Education Event</asp:ListItem>
                            <asp:ListItem Value="divOnlineEvent">Online Event</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:UpdatePanel ID="upEvents" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEventType" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <%-- Resource Table Event Start --%>
                        <div id="divResourceTableEvent" runat="server" visible="false">
                            <div class="row m-3">
                                <div class="col">
                                    <label>Table Event Topic: </label>
                                </div>
                                <div class="col-7">
                                    <asp:TextBox ID="txtRTETopic" placeholder="Event Topic" runat="server" CssClass="form-control"></asp:TextBox><br />
                                </div>
                            </div>
                            <%-- Resource Table Event End --%>
                            <%-- Flu Shot Event Start --%>
                            <div id="divFluShotEvent" runat="server" visible="false">
                                <div class="eventControlBG">
                                    <asp:DropDownList ID="ddlFSENurse" CssClass="form-control" runat="server">
                                        <asp:ListItem>Select Healthcare Provider</asp:ListItem>
                                        <asp:ListItem>Jane Nurse</asp:ListItem>
                                        <asp:ListItem>John Nurse</asp:ListItem>
                                        <asp:ListItem>Unlisted</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                </div>
                            </div>
                        </div>
                        <%-- Flu Shot Event End --%>
                        <%-- Health Education Event Start --%>
                        <div id="divHealthEducationEvent" runat="server" visible="false">
                            <div class="row m-3">
                                <div class="col">
                                    <label>Health Education Event Topic: </label>
                                </div>
                                <div class="col-7">
                                    <asp:TextBox ID="txtHEETopic" placeholder="Event Topic" runat="server" CssClass="form-control"></asp:TextBox><br />
                                </div>
                            </div>
                        </div>
                        <%-- Health Education Event End --%>
                        <%-- Online Event Start --%>
                        <div id="divOnlineEvent" runat="server" visible="false">
                            <div class="row m-3">
                                <div class="col">
                                    <label>Online Event Topic: </label>
                                </div>
                                <div class="col-7">
                                    <asp:TextBox ID="txtOETopic" placeholder="Event Topic" runat="server" CssClass="form-control"></asp:TextBox><br />
                                </div>
                            </div>
                        </div>
                        <%-- Online Event End --%>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row m-3">
                    <div class="col">
                        <label>Event Description and Notes:</label>
                    </div>
                    <div class="col-7">
                        <textarea class="w-100 form-control" id="txtDescription" rows="6" runat="server"></textarea>
                    </div>
                </div>
                <div class="row m-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="buttonStyle" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
