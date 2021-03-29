﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="CapstoneUI.Event" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Section 1 Start --%>
    <div class="container-fluid">
        <div class="container homepage pb-3">
            <div class="row modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active">
                            <asp:LinkButton ID="lnkEventList" NavigateUrl="~/EventList.aspx" runat="server" OnClick="lnkEventList_Click">Event List</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Event</li>
                    </ol>
                </nav>
            </div>
            <div id="form" class="container-fluid" runat="server">
                <div class="container w-75 mt-5">
                    <div class="row">
                        <asp:Label ID="lblEventName" CssClass="h3" runat="server"></asp:Label>
                    </div>
                    <div class="border-bottom">
                        <div class="col">
                            <div class="row m-3">
                                <div class="col">
                                    <label>Description:</label>
                                </div>
                                <div class="col-9">
                                    <asp:TextBox ID="txtDescription" Enabled="false" CssClass="form-control mt-2"  TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col">
                                    <label>Type:</label>
                                </div>
                                <div class="col-9">
                                    <asp:TextBox ID="txtEventType" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col">
                                    <label>Location:</label>
                                </div>
                                <div class="col-9">
                                    <asp:TextBox ID="txtLocation" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col">
                                    <label>Event Date:</label>
                                </div>
                                <div class="col-9">
                                    <asp:TextBox ID="txtEventDate" CssClass="form-control" Enabled="false" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col">
                                    <label>Start Time:</label>
                                </div>
                                <div class="col-9">
                                    <asp:TextBox ID="txtStartTime" CssClass="form-control" Enabled="false" TextMode="Time" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col">
                                    <label>End Time:</label>
                                </div>
                                <div class="col-9">
                                    <asp:TextBox ID="txtEndTime" CssClass="form-control mb-3" Enabled="false" TextMode="Time" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <div class="row mt-5">
                        <div class="col border-right">
                            <div class="row justify-content-center">
                                <b>Health Workers:</b>
                            </div>
                            <div class="row justify-content-center">
                                <ul class="eventLists">
                                    <asp:Repeater ID="rptHealthWorkers" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lblHealthWorkerName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <div class="col">
                            <div class="row justify-content-center">
                                <b>Attendees:</b>
                            </div>
                            <div class="row justify-content-center">
                                <ul class="eventLists">
                                    <asp:Repeater ID="rptResidents" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lblResidentName" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div class="container text-center my-3">
                                    <asp:Button ID="btnAddResidentAttendees" CssClass="buttonStyle" Text="Add Resident" OnClick="btnAddResidentAttendees_Click" runat="server"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container w-50">
                        <div class="row my-3 text-center">
                            <div class="col">
                                <asp:Button ID="btnEdit" CssClass="buttonStyle" Text="Edit" OnClick="btnEdit_Click" runat="server" />
                            </div>
                            <div class="col">
                                <asp:Button ID="btnSave" CssClass="buttonStyle" Text="Save" Visible="false" OnClick="btnSave_Click" runat="server" />
                            </div>
                            <div class="col">
                                <asp:Button ID="btnCancel" CssClass="buttonStyle" Text="Cancel" Visible="false" OnClick="btnCancel_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
