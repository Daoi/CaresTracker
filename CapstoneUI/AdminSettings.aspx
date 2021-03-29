<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="CapstoneUI.AdminSettings" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        // use before postback to process all GV values
        function expandTable(gv) {
            $(`#MainContent_${gv}`).DataTable().page.len(-1).draw();
        }
    </script>
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
                <h3>Manage Regions</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <asp:GridView ID="gvRegions" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" OnRowDataBound="gvRegions_RowDataBound">
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="RegionName" HeaderText="Region" />
                                <asp:TemplateField HeaderText="Organization">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlOrganizations" runat="server" CssClass="form-control" AppendDataBoundItems="True">
                                            <asp:ListItem Text="Unassigned" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col pt-5">
                        <div class="card mx-auto">
                            <div class="card-body">
                                <p class="card-text">Assign regions to partner organizations, or leave them unassigned. Click update below to save your changes.</p>
                                <asp:Button ID="btnRegionUpdate" runat="server" Text="Update Regions" CssClass="btn btn-primary" OnClick="btnRegionUpdate_Click" OnClientClick="expandTable('gvRegions');" />
                            </div>
                        </div>
                    </div>
                </div>
                <h3>Manage Developments</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <asp:GridView ID="gvHousingDevelopments" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="DevelopmentName" HeaderText="Development" />
                                <asp:BoundField DataField="RegionName" HeaderText="Region" />
                                <asp:TemplateField HeaderText="Enabled">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDevelopmentEnabled" runat="server" CssClass="form-check" Checked='<%# Eval("DevelopmentIsEnabled") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col pt-5">
                        <div class="card mx-auto">
                            <div class="card-body">
                                <p class="card-text">Select which developments should be shown as options on the resident profile creation page. Click update below to save your changes.</p>
                                <asp:Button ID="btnDevelopmentUpdate" runat="server" Text="Update Developments" CssClass="btn btn-primary" OnClick="btnDevelopmentUpdate_Click" OnClientClick="expandTable('gvHousingDevelopments');" />
                            </div>
                        </div>
                    </div>
                </div>
                <h3>Manage Services</h3>
                <div class="row">
                    <div class="col-8">
                        <asp:GridView ID="gvServices" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="ServiceName" HeaderText="Service" />
                                <asp:TemplateField HeaderText="Enabled">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkServiceEnabled" runat="server" CssClass="form-check" Checked='<%# Eval("ServiceIsEnabled") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col pt-5">
                        <div class="card mx-auto">
                            <div class="card-body">
                                <p class="card-text">Select which services should be shown as options on the interaction form. Click update below to save your changes.</p>
                                <asp:Button ID="btnServiceUpdate" runat="server" Text="Update Services" CssClass="btn btn-primary" OnClick="btnServiceUpdate_Click" OnClientClick="expandTable('gvServices');" />
                            </div>
                        </div>
                        <div class="card mt-4">
                            <div class="card-body">
                                <p class="card-text">Enter the name of a new service in the textbox, then click below to add it to the list.</p>
                                <asp:TextBox ID="txtServiceName" runat="server" Placeholder="Service Name..." CssClass="form-control"></asp:TextBox>
                                <asp:Button ID="btnAddService" runat="server" Text="Add New Service" CssClass="btn btn-primary mt-3" OnClick="btnAddService_Click" />
                            </div>
                        </div>
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
