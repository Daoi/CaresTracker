<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="CaresTracker.EventList" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container-fluid pl-5 pr-5">
            <div class="table-responsive tableContainer">
                <div class="row  modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-transparent">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active" aria-current="page">Event List</li>
                        </ol>
                    </nav>
                </div>
                <div class="container-fluid mt-2">
                    <div runat="server" id="divNoRows" visible="false" class="row w-auto justify-content-center" style="height: 10vh;">
                    <asp:Label ID="lblNoRows" runat="server" Text=""></asp:Label>
                        </div>
                    <asp:GridView ID="gvEventList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                        <HeaderStyle CssClass="cherryBackground" />
                        <Columns>
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <asp:Button ID="btnViewEvent" CssClass="btn btn-light w-100 p-3 font-weight-bold" runat="server" Text="View this Event" OnClick="btnViewEvent_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                            <asp:BoundField DataField="EventDescription" HeaderText="Event Description" />
                            <asp:BoundField DataField="EventTypeName" HeaderText="Event Type" />
                            <asp:BoundField DataField="EventLocation" HeaderText="Location" />
                            <asp:BoundField DataField="EventDate" HeaderText="Date" />
                            <asp:TemplateField HeaderText="Start Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartTime" Text='<%# DateTime.Parse(Eval("EventStartTime").ToString()).ToString("hh:mm tt")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndTime" Text='<%# DateTime.Parse(Eval("EventEndTime").ToString()).ToString("hh:mm tt")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Event Host">
                                <ItemTemplate>
                                    <asp:Label ID="lblChwName" runat="server" Text='<%#Eval("UserFirstName")+ " " + Eval("UserLastName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserEmail" HeaderText="Host Email" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div style="margin-top: 2%; height: 2%; width: auto;"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContent_gvEventList').DataTable();
        });
    </script>
</asp:Content>
