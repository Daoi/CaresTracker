<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CaresTracker.Master" CodeBehind="AddResidentAttendees.aspx.cs" Inherits="CaresTracker.AddResidentAttendees" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        // use before postback to process all GV values
        // TemplateField ctrl values aren't accurate otherwise
        function expandTable(gv) {
            $(`#MainContent_${gv}`).DataTable().page.len(-1).draw();
        }
    </script>
    <div class="container-fluid homepage">
        <div class="row modal-header offwhiteBackground p-0" style="height: 50px; padding-left: 0!important; padding-right: 0!important; font-size: large;">
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
        <div class="container mt-5">
            <div runat="server" id="divNoRows" visible="false" class="row w-auto justify-content-center" style="height: 10vh;">
                <asp:Label ID="lblNoRows" runat="server" Text=""></asp:Label>
            </div>
            <asp:GridView ID="gvResidentList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" ShowFooter="True">
                <HeaderStyle CssClass="cherryBackground" />
                <Columns>
                    <asp:TemplateField HeaderText="Add">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAddResident" CssClass="form-check" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ResidentFirstName" HeaderText="Resident First Name" />
                    <asp:BoundField DataField="ResidentLastName" HeaderText="Resident Last Name" />
                    <asp:BoundField DataField="ResidentPhoneNumber" HeaderText="Resident Phone Number" />
                    <asp:BoundField DataField="ResidentEmail" HeaderText="Resident Email" />
                </Columns>
            </asp:GridView>
            <div class="row">
                <div class="col-md-8 mb-3">
                    <asp:Label ID="lblResidentNotFound" CssClass="hidden error-label" runat="server" Text=""></asp:Label>
                    <asp:Button ID="btnCreateNewResident" CssClass="hidden buttonStyle" runat="server" Text="Create New Resident" OnClick="btnCreateNewResident_Click" />
                </div>
                <div class="col-md-4">
                </div>
            </div>
            <div class="container text-center">
                <div class="row">
                    <asp:Label ID="lblError" CssClass="errorLabel" runat="server"></asp:Label>
                </div>
                <div class="row">
                    <asp:Button ID="btnSubmit" CssClass="buttonStyle" Text="Add Residents" OnClick="btnSubmit_Click" OnClientClick="expandTable('gvResidentList');" runat="server" />
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
                    $('#MainContent_lblResidentNotFound').text(`Resident matching the search value ${search} not found`);
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
