<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="CapstoneUI.Event" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Section 1 Start --%>
    <div class="container">
        <div class="eventCreator">
            <div class="row form-inline form-group no-gutters">
                <div class="col-md-1 col-sm-1"></div>
                <div class="col-md-4 quickAccess col-sm-1">
                    <h3>
                        <asp:Label ID="lblEventName" runat="server"></asp:Label></h3>
                    <br />
                    <span><b>Type:</b>
                        <asp:Label ID="lblEventType" runat="server"></asp:Label></span>
                    <br />
                    <br />
                    <span><b>Location:</b>
                        <asp:Label ID="lblLocation" runat="server"></asp:Label></span>
                    <br />
                    <br />
                    <span><b>Start Time:</b>
                        <asp:Label ID="lblStartTime" runat="server"></asp:Label></span>
                    <br />
                    <br />
                    <span><b>End Time:</b>
                        <asp:Label ID="lblEndTime" runat="server"></asp:Label></span>
                    <br />
                    <br />
                </div>

                <div class="col-md-2 col-sm-1"></div>

                <div class="col-md-4 col-sm-1">
                    <br />
                    <br />
                    <h4 class="mt-2">Health Workers:</h4>
                    <ul>
                        <asp:Repeater ID="rptHealthWorkers" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:Label ID="lblHealthWorkerName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="col-md-2 col-sm-1"></div>
                <div class="col-md-4 col-sm-1">
                    <br />
                    <br />
                    <h4 class="mt-2">Attendees:</h4>
                    <ul>
                        <asp:Repeater ID="rptResidents" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:Label ID="lblResidentName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <div class="col-md-2 col-sm-1"></div>
            </div>
            <div class="row form-inline form-group no-gutters">
                <div class="col-md-1 col-sm-1"></div>
                <div class="col-md-4 quickAccess col-sm-1">
                    <div>
                        <h4>Description:</h4>
                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
