<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="ResidentLookUp.aspx.cs" Inherits="CaresTracker.ResidentLookUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage">
        <div class="row modal-header offwhiteBackground p-0" style="height: 50px; padding-left: 0!important; padding-right: 0!important; font-size: large;">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item" style="color: deepskyblue">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                    <li class="breadcrumb-item active" aria-current="page">Resident Lookup</li>
                </ol>
            </nav>
        </div>
        <div class="table-responsive tableContainer" style="background-color: white !important">
            <div class="container-fluid mt-2">
                <div runat="server" id="divNoRows" visible="false" class="row w-auto justify-content-center" style="height: 10vh;">
                    <asp:Label ID="lblNoRows" runat="server" Text=""></asp:Label>
                </div>
                <asp:GridView ID="gvResidentList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" ShowFooter="True">
                    <HeaderStyle CssClass="cherryBackground" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="View Resident">
                            <ItemTemplate>
                                <asp:Button ID="btnViewResident" CssClass="btn btn-light w-100 font-weight-bold" runat="server" Text="View" OnClick="btnViewResident_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ResidentFirstName" HeaderText="Resident First Name" />
                        <asp:BoundField DataField="ResidentLastName" HeaderText="Resident Last Name" />
                        <asp:TemplateField HeaderText="Resident DoB">
                            <ItemTemplate>
                                <asp:Label ID="lblDoB" Text='<%# CaresTracker.Utilities.GridViewFormatter.FormatDates(Eval("DateOfBirth"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="row">
                    <div class="col-md-8 mb-3">
                        <asp:Label ID="lblResidentNotFound" CssClass="hidden error-label" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnCreateNewResident" CssClass="buttonStyle" runat="server" Text="Create New Resident" OnClick="NewResident_Click" />
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>
        </div>
        <div style="margin-top: 2%; height: 2%; width: auto;"></div>
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
