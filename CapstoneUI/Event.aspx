﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="Event.aspx.cs" Inherits="CapstoneUI.Event" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Section 1 Start --%>
    <div class="container homepage backgroundblue p-0">
        <div class="container-fluid">
            <div class="row modal-header pb-0" style="height: 7%; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-white">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active bg-white">
                            <asp:LinkButton ID="lnkEventList" NavigateUrl="~/EventList.aspx" runat="server" OnClick="lnkEventList_Click">Event List</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active bg-white" aria-current="page">Event</li>
                    </ol>
                </nav>
                <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 54%">Event</asp:Label>
            </div>
            <div id="form" class="container-fluid" runat="server">
                <div class="container w-75 mr-5 mt-5">
                    <div class="row">
                        <asp:Label ID="lblEventName" CssClass="h3" runat="server"></asp:Label>
                    </div>
                    <div class="row w-75 border-bottom">
                        <div class="col">
                            <div class="row m-3">
                                <b>Description:</b>
                                <asp:TextBox ID="txtDescription" Enabled="false" CssClass="form-control mt-2"  TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                            <div class="row m-3">
                                <div class="col-3 p-0">
                                    <b>Type:</b>
                                </div>
                                <div class="col p-0">
                                    <asp:TextBox ID="txtEventType" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col-3 p-0">
                                    <b>Location:</b>
                                </div>
                                <div class="col p-0">
                                    <asp:TextBox ID="txtLocation" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col-3 p-0">
                                    <b>Event Date:</b>
                                </div>
                                <div class="col p-0">
                                    <asp:TextBox ID="txtEventDate" CssClass="form-control" Enabled="false" TextMode="Date" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col-3 p-0">
                                    <b>Start Time:</b>
                                </div>
                                <div class="col p-0">
                                    <asp:TextBox ID="txtStartTime" CssClass="form-control" Enabled="false" TextMode="Time" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row m-3">
                                <div class="col-3 p-0">
                                    <b>End Time:</b>
                                </div>
                                <div class="col p-0">
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
                            </div>
                        </div>
                    </div>
                    <div class="container w-50">
                        <div class="row my-3 text-center">
                            <div class="col">
                                <asp:Button ID="btnEdit" CssClass="btn btn-primary btn-lg" Text="Edit" OnClick="btnEdit_Click" runat="server" />
                            </div>
                            <div class="col">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary btn-lg" Text="Save" Visible="false" OnClick="btnSave_Click" runat="server" />
                            </div>
                            <div class="col">
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger btn-lg" Text="Cancel" Visible="false" OnClick="btnCancel_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
