<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CaresTracker.Master" CodeBehind="InteractionList.aspx.cs" Inherits="CaresTracker.InteractionList" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage">
        <div class="row modal-header offwhiteBackground p-0" style="height: 50px; padding-left: 0!important; padding-right: 0!important; font-size: large;">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item" style="color: deepskyblue">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                    <li class="breadcrumb-item active" aria-current="page">Interaction List</li>
                </ol>
            </nav>
        </div>
        <div class="table-responsive tableContainer" style="background-color: white !important">
            <div class="container-fluid mt-2">
                <div runat="server" id="divNoRows" visible="false" class="row w-auto justify-content-center" style="height: 10vh;">
                    <asp:Label ID="lblNoRows" runat="server" Text=""></asp:Label>
                </div>
                <asp:GridView ID="gvInteractionList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered thead-dark gvBtn">
                    <HeaderStyle CssClass="cherryBackground" />
                    <Columns>
                        <asp:TemplateField HeaderText="View Interaction">
                            <ItemTemplate>
                                <asp:Button ID="btnViewInteraction" CssClass="btn btn-light w-100 font-weight-bold" runat="server" OnClick="btnViewInteraction_Click" Text="View" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ResidentFirstName" HeaderText="Resident First Name" />
                        <asp:BoundField DataField="ResidentLastName" HeaderText="Resident Last Name" />
                        <asp:TemplateField HeaderText="Resident DoB">
                            <ItemTemplate>
                                <asp:Label ID="lblDateOfBirth" Text='<%# CaresTracker.Utilities.GridViewFormatter.FormatDates(Eval("DateOfBirth"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserFirstName" HeaderText="CHW First Name" />
                        <asp:BoundField DataField="UserLastName" HeaderText="CHW Last Name" />
                        <asp:TemplateField HeaderText="Date of Contact">
                            <ItemTemplate>
                                <asp:Label ID="lblDateofContact" Text='<%# CaresTracker.Utilities.GridViewFormatter.FormatDates(Eval("DateOfContact"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MethodOfContact" HeaderText="Method of Contact" />
                        <asp:BoundField DataField="LocationOfContact" HeaderText="Location" />
                        <asp:TemplateField HeaderText="Action Plan">
                            <ItemTemplate>
                                <asp:Label ID="lblActionPlan" Text='<%# CaresTracker.Utilities.GridViewFormatter.FormatLongText(Eval("ActionPlan"))%>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfResidentDetails" runat="server" />
    <div style="margin-top: 2%; height: 2%; width: auto;"></div>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#MainContent_gvInteractionList').DataTable();;
            var hv = $('#MainContent_hfResidentDetails').val();
            table.search(hv).draw();
        });
    </script>
</asp:Content>
