<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CaresTracker.Master" CodeBehind="AddResidentAttendees.aspx.cs" Inherits="CaresTracker.AddResidentAttendees" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        // use before postback to process all GV values
        // TemplateField ctrl values aren't accurate otherwise
        function expandTable(gv) {
            $(`#MainContent_${gv}`).DataTable().page.len(-1).draw();
        }
    </script>
    <div class="container-fluid">
        <div class="container homepage">
            <div class="row modal-header pb-0 offwhiteBackground" style="height: 7%; font-size: large">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb bg-transparent">
                        <li class="breadcrumb-item" style="color: deepskyblue">
                            <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active">
                            <asp:LinkButton ID="lnkEventList" NavigateUrl="~/EventList.aspx" runat="server" OnClick="lnkEventList_Click">Event List</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active">
                            <asp:LinkButton ID="lnkEvent" NavigateUrl="~/Event.aspx" runat="server" OnClick="lnkEvent_Click">Event</asp:LinkButton>
                        </li>
                        <li class="breadcrumb-item active" aria-current="page">Add Resident Attendees</li>
                    </ol>
                </nav>
            </div>
            <div class="container">
                <div class="container mt-5">
                    <asp:GridView ID="gvResidentList" Width="100%" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" ShowFooter="True" >
                        <HeaderStyle CssClass="cherryBackground" />
                        <Columns>
                            <asp:TemplateField HeaderText="Add Resident to Event">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkAddResident" CssClass="form-check" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ResidentFirstName" HeaderText="Resident First Name" />
                            <asp:BoundField DataField="ResidentLastName" HeaderText="Resident Last Name" />
                        </Columns>
                    </asp:GridView>
                    <div class="container text-center">
                        <div class="row">
                            <asp:Label ID="lblError" CssClass="h6 alert-danger" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <asp:Button ID="btnSubmit" CssClass="buttonStyle" Text="Add Residents" OnClick="btnSubmit_Click" OnClientClick="expandTable('gvResidentList');" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfSearchInput" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#MainContent_gvResidentList').DataTable();
        });
    </script>
</asp:Content>
