<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="CaresTracker.AdminSettings" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        // use before postback to process all GV values
        // TemplateField ctrl values aren't accurate otherwise
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
                                <asp:UpdatePanel ID="pnlRegionCtrls" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblRegionError" runat="server" Text="" CssClass="errorLabel" Visible="false"></asp:Label><br />
                                        <asp:Button ID="btnRegionUpdate" runat="server" Text="Update Regions" CssClass="buttonStyle" OnClick="btnRegionUpdate_Click" OnClientClick="expandTable('gvRegions');" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                <asp:UpdatePanel ID="pnlDevelopmentCtrls" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblDevelopmentError" runat="server" Text="" CssClass="errorLabel" Visible="false"></asp:Label><br />
                                        <asp:Button ID="btnDevelopmentUpdate" runat="server" Text="Update Developments" CssClass="buttonStyle" OnClick="btnDevelopmentUpdate_Click" OnClientClick="expandTable('gvHousingDevelopments');" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <h3>Manage Services</h3>
                <div class="row border-bottom mb-5 pb-5">
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
                                <asp:UpdatePanel ID="pnlServiceCtrls" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblServiceError" runat="server" Text="" CssClass="errorLabel" Visible="false"></asp:Label><br />
                                        <asp:Button ID="btnServiceUpdate" runat="server" Text="Update Services" CssClass="buttonStyle" OnClick="btnServiceUpdate_Click" OnClientClick="expandTable('gvServices');" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="card mt-4">
                            <div class="card-body">
                                <p class="card-text">Enter the name of a new service in the textbox, then click below to add it to the list.</p>
                                <asp:UpdatePanel ID="pnlAddServiceCtrls" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblAddServiceError" runat="server" Text="" CssClass="errorLabel" Visible="false"></asp:Label><br />
                                        <asp:TextBox ID="txtServiceName" runat="server" Placeholder="Service Name..." CssClass="form-control"></asp:TextBox>
                                        <asp:Button ID="btnAddService" runat="server" Text="Add New Service" CssClass="buttonStyle mt-3" OnClick="btnAddService_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <h3>Manage Event Types</h3>
                <div class="row">
                    <div class="col-8">
                        <asp:GridView ID="gvEventTypes" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="EventTypeName" HeaderText="Event Type" />
                                <asp:TemplateField HeaderText="Enabled">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEventTypeIsEnabled" runat="server" CssClass="form-check" Checked='<%# Eval("EventTypeIsEnabled") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col pt-5">
                        <div class="card mx-auto">
                            <div class="card-body">
                                <p class="card-text">Select which event types should be shown as event creation and editing. Click update below to save your changes.</p>
                                <asp:UpdatePanel ID="pnlEventTypeCtrls" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblEventTypeError" runat="server" Text="" CssClass="errorLabel" Visible="false"></asp:Label><br />
                                        <asp:Button ID="btnEventTypeUpdate" runat="server" Text="Update Event Types" CssClass="buttonStyle" OnClick="btnEventTypeUpdate_Click" OnClientClick="expandTable('gvEventTypes');" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="card mt-4">
                            <div class="card-body">
                                <p class="card-text">Enter the name of a new event type in the textbox, then click below to add it to the list.</p>
                                <asp:UpdatePanel ID="pnlAddEventTypeCtrls" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblAddEventTypeError" runat="server" Text="" CssClass="errorLabel" Visible="false"></asp:Label><br />
                                        <asp:TextBox ID="txtEventTypeName" runat="server" Placeholder="Event Type Name..." CssClass="form-control"></asp:TextBox>
                                        <asp:Button ID="btnAddEventType" runat="server" Text="Add New Event Type" CssClass="buttonStyle mt-3" OnClick="btnAddEventType_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            ['gvRegions', 'gvHousingDevelopments', 'gvServices', 'gvEventTypes'].forEach(gv => {
                $(`#MainContent_${gv}`).DataTable({
                    "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]]
                });
            });
        });
    </script>
</asp:Content>
