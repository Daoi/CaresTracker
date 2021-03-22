<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CapstoneUI.Master" CodeBehind="InteractionList.aspx.cs" Inherits="CapstoneUI.InteractionList" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container-fluid pl-5 pr-5">
            <div class="table-responsive tableContainer" style="background-color: white !important">
                <div class="row modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-transparent">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active" aria-current="page">Interaction List</li>
                        </ol>
                    </nav>
                    <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 58%">Interaction List</asp:Label>
                </div>
                <div class="container-fluid mt-2">
                    <asp:GridView ID="gvInteractionList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered thead-dark">
                        <HeaderStyle CssClass="cherryBackground" />
                        <Columns>
                            <asp:TemplateField HeaderText="View this Interaction">
                                <ItemTemplate>
                                    <asp:Button ID="btnViewInteraction" CssClass="btn btn-light w-100 p-3 font-weight-bold" runat="server" OnClick="btnViewInteraction_Click" Text="View This Interaction" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ResidentFirstName" HeaderText="Resident First Name" />
                            <asp:BoundField DataField="ResidentLastName" HeaderText="Resident First Name" />
                            <asp:BoundField DataField="DateOfBirth" HeaderText="Resident DoB" />
                            <asp:BoundField DataField="UserFirstName" HeaderText="CHW First Name" />
                            <asp:BoundField DataField="UserLastName" HeaderText="CHW Last Name" />
                            <asp:BoundField DataField="DateOfContact" HeaderText="Date of Interaction" />
                            <asp:BoundField DataField="MethodOfContact" HeaderText="Method of Contact" />
                            <asp:BoundField DataField="LocationOfContact" HeaderText="Location" />
                            <asp:BoundField DataField="ActionPlan" HeaderText="Notes" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfResidentDetails" runat="server" />
        <div style="margin-top: 2%; height: 2%; width: auto;"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#MainContent_gvInteractionList').DataTable();;
            var hv = $('#MainContent_hfResidentDetails').val();
            table.search(hv).draw();
        });
    </script>
</asp:Content>
