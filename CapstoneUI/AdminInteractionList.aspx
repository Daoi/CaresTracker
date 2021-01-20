﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CapstoneUI.Master" CodeBehind="AdminInteractionList.aspx.cs" Inherits="CapstoneUI.AdminInteractionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid" style="background-color: #157CB6;">
        <div class="container-fluid pl-5 pr-5">
            <div class="table-responsive tableContainer" style="background-color: white !important">
                <div class="row  modal-header pb-0" style="height: 7%; font-size: large">
                    <nav aria-label="breadcrumb">
                        <ol class="breadcrumb bg-white">
                            <li class="breadcrumb-item" style="color: deepskyblue">
                                <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton></li>
                            <li class="breadcrumb-item active bg-white" aria-current="page">Interaction List</li>
                        </ol>
                    </nav>
                    <asp:Label ID="lblUserInfo" runat="server" Enabled="true" Visible="true" CssClass="h3 my-2" Style="width: 58%">Interaction List</asp:Label>
                </div>
                <div class="container-fluid mt-2">
                    <asp:GridView ID="gvInteractionList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered thead-dark" OnRowCommand="gvInteractionList_RowCommand">
                        <Columns>
                            <asp:ButtonField ControlStyle-CssClass="btn btn-light w-100 p-3 font-weight-bold" ButtonType="Button" Text="View this Interaction">
                                <ControlStyle CssClass="btn btn-light w-100 p-3 font-weight-bold"></ControlStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="ResidentFirstName" HeaderText="Resident First Name" />
                            <asp:BoundField DataField="ResidentLastName" HeaderText="Resident Last Name" />
                            <asp:BoundField DataField="CHWName" HeaderText="Name of CHW" />
                            <asp:BoundField DataField="DateOfInteraction" HeaderText="Date of Interaction" />
                            <asp:BoundField DataField="MethodOfContact" HeaderText="Method of Contact" />
                            <asp:BoundField DataField="Location" HeaderText="Location" />
                            <asp:BoundField DataField="Notes" HeaderText="Notes" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div style="margin-top: 2%; height: 2%; width: auto;"></div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContent_gvInteractionList').DataTable();
        });
    </script>
</asp:Content>
