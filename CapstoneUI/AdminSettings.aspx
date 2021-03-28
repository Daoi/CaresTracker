<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="CapstoneUI.AdminSettings" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mx-auto homepage p-0">
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
        <div class="container-fluid">
                <div class="row p-5">
                    <div class="col border-right">
                        <h5>Manage Regions</h5>
                        <asp:GridView ID="gvRegions" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="RegionName" HeaderText="Region" />
                                <asp:TemplateField HeaderText="Organization">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlOrganizations" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col border-right">
                        <h5>Manage Developments</h5>
                        <asp:GridView ID="gvHousingDevelopments" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="DevelopmentName" HeaderText="Development" />
                                <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlRegions" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Enable">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDevelopmentEnabled" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col">
                        <h5>Manage Services</h5>
                        <asp:GridView ID="gvServices" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="ServiceName" HeaderText="Service" />
                                <asp:TemplateField HeaderText="Enable">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkServiceEnabled" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
        </div>
    </div>
</asp:Content>
