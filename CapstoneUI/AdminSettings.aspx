<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="CapstoneUI.AdminSettings" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mx-auto homepage px-0 pt-0 pb-5">
        <div class="row modal-header no-gutters offwhiteBackground" style="height: auto; font-size: large">
            <nav aria-label="breadcrumb w-75">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" CssClass="cherryFont" runat="server">Dashboard</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Admin Settings</li>
                </ol>
            </nav>
        </div>
        <div class="container-fluid p-0">
            <div class="container my-5">

                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <h3>Manage Regions</h3>
                        <asp:GridView ID="gvRegions" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="RegionName" HeaderText="Region" />
                                <asp:TemplateField HeaderText="Organization">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlOrganizations" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col">
                        <asp:Button ID="btnRegionUpdate" runat="server" Text="Update Regions" />
                    </div>
                </div>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <h3>Manage Developments</h3>
                        <asp:GridView ID="gvHousingDevelopments" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="DevelopmentName" HeaderText="Development" />
                                <asp:BoundField DataField="RegionName" HeaderText="Region" />
                                <asp:TemplateField HeaderText="Enable">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDevelopmentEnabled" runat="server" CssClass="form-check" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col">
                        <asp:Button ID="btnDevelopmentUpdate" runat="server" Text="Update Developments" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-8">
                    <h3>Manage Services</h3>
                        <asp:GridView ID="gvServices" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="ServiceName" HeaderText="Service" />
                                <asp:TemplateField HeaderText="Enable">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkServiceEnabled" runat="server" CssClass="form-check" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col">
                        <asp:Button ID="btnServiceUpdate" runat="server" Text="Update Services" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            ['gvRegions', 'gvHousingDevelopments', 'gvServices'].forEach(gv => {
                $(`#MainContent_${gv}`).DataTable({
                    "searching": false,
                    "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
                });
            });
        });
    </script>
</asp:Content>
