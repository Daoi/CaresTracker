<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="CHWList.aspx.cs" Inherits="CapstoneUI.CHWList" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="container-fluid pl-5 pr-5">
            <div class="table-responsive tableContainer">
                <div class="row  modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-transparent">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active" aria-current="page">Worker List</li>
                        </ol>
                    </nav>
                </div>
                <div class="container-fluid mt-2">
                    <asp:GridView ID="gvCHWList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                        <HeaderStyle CssClass="cherryBackground" />
                        <Columns>
                            <asp:TemplateField HeaderText="View Worker Profile">
                                <ItemTemplate>
                                    <asp:Button ID="btnViewWorker" CssClass="btn btn-light w-100 p-3 font-weight-bold" runat="server" Text="View this Worker" OnClick="btnViewWorker_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserFirstName" HeaderText="First Name" />
                            <asp:BoundField DataField="UserLastName" HeaderText="Last Name" />
                            <asp:BoundField DataField="Username" HeaderText="Username" />
                            <asp:BoundField DataField="UserEmail" HeaderText="Email " />
                            <asp:BoundField DataField="UserPhoneNumber" HeaderText="Phone" />
                            <asp:BoundField DataField="OrganizationName" HeaderText="Organization" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div style="margin-top: 2%; height: 2%; width: auto;"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContent_gvCHWList').DataTable();
        });
    </script>
</asp:Content>
