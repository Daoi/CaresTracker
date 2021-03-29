<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CapstoneUI.Master" CodeBehind="AddResidentAttendees.aspx.cs" Inherits="CapstoneUI.AddResidentAttendees" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container homepage">
            <div class="row modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active">
                            <asp:LinkButton ID="lnkEventList" NavigateUrl="~/EventList.aspx" runat="server" OnClick="lnkEventList_Click">Event List</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active">
                            <asp:LinkButton ID="lnkEvent" NavigateUrl="~/Event.aspx" runat="server" OnClick="lnkEvent_Click">Event</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Add Resident Attendees</li>
                    </ol>
                </nav>
            </div>
            <div class="container">
                <div class="container mt-5">
                    <asp:GridView ID="gvResidentList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" ShowFooter="True" OnRowDataBound="gvResidentList_RowDataBound">
                        <HeaderStyle CssClass="cherryBackground" />
                        <Columns>
                            <asp:TemplateField HeaderText="View Resident Profile">
                                <ItemTemplate>
                                    <asp:Button ID="btnAddResident" CssClass="btn btn-light w-100 p-3 font-weight-bold" runat="server" Text="Add Resident to Event" OnClick="btnAddResident_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ResidentFirstName" HeaderText="Resident First Name" />
                            <asp:BoundField DataField="ResidentLastName" HeaderText="Resident Last Name" />
                        </Columns>
                    </asp:GridView>
                    <asp:UpdatePanel ID="upAddResidents" runat="server">
                        <ContentTemplate>
                            <div class="container mt-3 pt-3 border-top">
                                <div class="container w-50">
                                    <h5>Residents to be added:</h5>
                                    <asp:Label ID="lblError" CssClass="h6 text-danger" Visible="false" runat="server"></asp:Label>
                                    <ul class="addResidentList">
                                        <asp:Repeater ID="rptAttendees" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:Button ID="btnResidents" CssClass="btn btn-outline-danger my-2 mx-2" CommandArgument='<%# Eval("ResidentID") %>' Text='<%# Bind("FullName") %>' OnClick="btnResidents_Click" runat="server" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="container text-center">
                        <asp:Button ID="btnSubmit" CssClass="buttonStyle" Text="Add Residents" OnClick="btnSubmit_Click" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfSearchInput" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#MainContent_gvResidentList').DataTable();
            table.on('draw', function () {
                if (table.page.info().recordsDisplay <= 0) {
                    var search = $('.dataTables_filter input').val();
                    $('#MainContent_lblResidentNotFound').text(`Resident with the name ${search} not found`);
                    $('#MainContent_lblResidentNotFound').removeClass('hidden');
                    $('#MainContent_btnCreateNewResident').removeClass('hidden');
                    $('#MainContent_hfSearchInput').val(`${search}`);
                }
                else {
                    $('#MainContent_lblResidentNotFound').addClass('hidden');
                    $('#MainContent_btnCreateNewResident').addClass('hidden');
                }
            });

        });

    </script>
</asp:Content>
