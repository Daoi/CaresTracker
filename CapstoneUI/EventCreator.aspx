<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="EventCreator.aspx.cs" Inherits="CapstoneUI.EventCreator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid backgroundblue">
        <div class="container homepage">
            <div class="row  modal-header" style="height: 7%; padding-left: 0!important; padding-right: 0!important; font-size: large; padding-bottom: 2%">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-white">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                        <li class="breadcrumb-item active bg-white" aria-current="page">Create Event</li>
                    </ol>
                </nav>
                <asp:Label ID="lblPageInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2 px-0" Style="width: 76%"></asp:Label>
            </div>
            <div id="eventControlBG" >
                <h2>Event General Details</h2>
                <asp:TextBox ID="txtEventName" placeholder="Event Name" runat="server" CssClass="form-control"></asp:TextBox><br />
                <asp:TextBox ID="txtEventLocation" placeholder="Event Location" runat="server" CssClass="form-control"></asp:TextBox><br />
                <asp:Label ID="lblEventDate" runat="server" Text="Event Date:"></asp:Label>
                <asp:TextBox ID="txtEventDate" placeholder="Event Date" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox><br />
                <asp:Label ID="lblEventStartTime" runat="server" Text="Start Time:"></asp:Label>
                <asp:TextBox ID="txtEventTimeStart" placeholder="Event Time Start" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox><br />
                <asp:Label ID="lblEventEndTime" runat="server" Text="End Time:"></asp:Label>
                <asp:TextBox ID="txtEventTimeEnd" placeholder="Event Time End" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox><br />
                <asp:DropDownList ID="ddlNumberAttending" CssClass="form-control" runat="server">
                    <asp:ListItem Value="Select Attendee Range">Select Attendance Range</asp:ListItem>
                    <asp:ListItem Value="1 to 25">1 to 25</asp:ListItem>
                    <asp:ListItem Value="26 to 50">26 to 50</asp:ListItem>
                    <asp:ListItem Value="51 to 75">51 to 75</asp:ListItem>
                    <asp:ListItem Value="76 to 100">76 to 100</asp:ListItem>
                </asp:DropDownList><br />
                <h5>Select Health Workers to Host Event</h5>
                <div id="userCBLDiv" class="DataboundCBLOverflow row">
                    <asp:CheckBoxList ID="cblUsers" runat="server" CssClass="myCheckBoxList" CellPadding="20" RepeatColumns="4" RepeatDirection="Horizontal">
                    </asp:CheckBoxList>
                </div>
            </div>
            <br />
            <br />
            <h2>Event Specific Details</h2>
            <asp:DropDownList ID="ddlEventType" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="Select Event Type">Select Event Type</asp:ListItem>
                <asp:ListItem Value="divResourceTableEvent">Resource Table Event</asp:ListItem>
                <asp:ListItem Value="divFluShotEvent">Flu Shot Event</asp:ListItem>
                <asp:ListItem Value="divHealthEducationEvent">Health Education Event</asp:ListItem>
                <asp:ListItem Value="divOnlineEvent">Online Event</asp:ListItem>
            </asp:DropDownList><br />
            <asp:UpdatePanel ID="upEvents" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEventType" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <%-- Resource Table Event Start --%>
                    <div id="divResourceTableEvent" runat="server" visible="false">
                        <div class="eventControlBG">
                            <asp:TextBox ID="txtRTETopic" placeholder="Event Topic" runat="server" CssClass="form-control"></asp:TextBox><br />
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
                        <div class="eventControlBG">
                            <asp:TextBox ID="txtHEETopic" placeholder="Event Topic" runat="server" CssClass="form-control"></asp:TextBox><br />
                        </div>
                    </div>
                    <%-- Health Education Event End --%>
                    <%-- Online Event Start --%>
                    <div id="divOnlineEvent" runat="server" visible="false">
                        <div class="eventControlBG">
                            <asp:TextBox ID="txtOETopic" placeholder="Event Topic" runat="server" CssClass="form-control"></asp:TextBox><br />
                        </div>
                    </div>
                    <%-- Online Event End --%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <h5>Event Description and Notes</h5>
                <textarea class="w-100" id="TextArea1" rows="6"></textarea>
            </div>

            <br />
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary btn-lg mb-1" />
        </div>
    </div>
</asp:Content>
