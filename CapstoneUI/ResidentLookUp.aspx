<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="ResidentLookUp.aspx.cs" Inherits="CapstoneUI.ResidentLookUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="background-color: #157CB6;">
        <div class="container-fluid pl-5 pr-5">
            <div class="table-responsive tableContainer" style="background-color: white !important">
                <div class="row  modal-header pb-0" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">Resident Lookup</li>
                        </ol>
                    </nav>
                    <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 58%">Resident List</asp:Label>
                </div>
                <div class="container-fluid mt-2">
                    <asp:GridView ID="gvResidentList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" OnRowCommand="gvResidentList_RowCommand" ShowFooter="True">
                        <Columns>
                            <asp:ButtonField ControlStyle-CssClass="btn btn-light w-100 p-3 font-weight-bold" ButtonType="Button" Text="View this Resident">
                                <ControlStyle CssClass="btn btn-light w-100 p-3 font-weight-bold"></ControlStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="FirstName" HeaderText="Resident First Name" />
                            <asp:BoundField DataField="LastName" HeaderText="Resident Last Name" />
                            <asp:BoundField DataField="DateOfBirth" HeaderText="Resident Date of Birth" />
                        </Columns>
                    </asp:GridView>
                    <div class="row">
                        <div class="col-md-8 mb-3">
                            <asp:Label ID="lblResidentNotFound" CssClass="hidden error-label" runat="server" Text=""></asp:Label>
                            <asp:Button ID="btnCreateNewResident" CssClass="hidden btn btn-primary" runat="server" Text="Create New Resident" OnClick="NewResident_Click" />

                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
            </div>
            <div style="margin-top: 2%; height: 2%; width: auto;"></div>
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
