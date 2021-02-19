<%@ Page Title="" Language="C#" MasterPageFile="~/CapstoneUI.Master" AutoEventWireup="true" CodeBehind="CHWList.aspx.cs" Inherits="CapstoneUI.CHWList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="background-color: #157CB6;">
        <div class="container-fluid pl-5 pr-5">
            <div class="table-responsive tableContainer" style="background-color: white !important">
                <div class="row  modal-header pb-0" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">CHW List</li>
                        </ol>
                    </nav>
                    <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 58%">CHW List</asp:Label>
                </div>
                <div class="container-fluid mt-2">
                    <asp:GridView ID="gvCHWList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" OnRowCommand="gvCHWList_RowCommand">
                        <Columns>
                            <asp:ButtonField ControlStyle-CssClass="btn btn-light w-100 p-3 font-weight-bold" ButtonType="Button" Text="View this CHW">
                                <ControlStyle CssClass="btn btn-light w-100 p-3 font-weight-bold"></ControlStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="FirstName" HeaderText="CHW First Name" />
                            <asp:BoundField DataField="LastName" HeaderText="CHW Last Name:" />
                            <asp:BoundField DataField="Username" HeaderText="CHW Username:" />
                            <asp:BoundField DataField="Email" HeaderText="Email: " />
                            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone: " />
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
