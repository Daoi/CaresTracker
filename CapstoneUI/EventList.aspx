<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="CapstoneUI.EventList" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container-fluid pl-5 pr-5">
            <div class="table-responsive tableContainer" style="background-color: white !important">
                <div class="row  modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-transparent">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active" aria-current="page">Event List</li>
                        </ol>
                    </nav>
                    <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 58%">Event List</asp:Label>
                </div>
                <div class="container-fluid mt-2">
                    <asp:GridView ID="gvEventList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                        <HeaderStyle CssClass="cherryBackground" />
                        <Columns>
                            <asp:TemplateField HeaderText="View Event">
                                <ItemTemplate>
                                    <asp:Button ID="btnViewEvent" CssClass="btn btn-light w-100 p-3 font-weight-bold" runat="server" Text="View this Event" OnClick="btnViewEvent_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EventName" HeaderText="Event Name" />
                            <asp:BoundField DataField="EventDescription" HeaderText="Event Description:" />
                            <asp:BoundField DataField="EventType" HeaderText="Event Type:" />
                            <asp:BoundField DataField="EventLocation" HeaderText="Location: " />
                            <asp:BoundField DataField="EventDate" HeaderText="Date:" />
                            <asp:BoundField DataField="EventStartTime" HeaderText="Start Time:" />
                            <asp:BoundField DataField="EventEndTime" HeaderText="End Time:" />
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
