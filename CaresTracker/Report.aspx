<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="CaresTracker.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mx-auto homepage px-0 pt-0 pb-5">
        <div class="row modal-header no-gutters offwhiteBackground" style="height: auto; font-size: large">
            <nav aria-label="breadcrumb w-75">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" CssClass="cherryFont" runat="server">Dashboard</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Report</li>
                </ol>
            </nav>
        </div>
        <div class="container-fluid p-0">
            <div class="container my-5">
                <%-- Housing Development Level Report --%>
                <h2><ins>Housing Development Totals</ins></h2>
                <h3>Residents per Gender</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtTotalGender' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvTotalGender" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Gender" />
                                <asp:BoundField DataField="series" HeaderText="# of Residents" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3>Residents per Primary Language</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtTotalLanguage' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvTotalLanguage" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Primary Language" />
                                <asp:BoundField DataField="series" HeaderText="# of Residents" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3>Residents per Vaccine Interest</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtTotalVaccine' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvTotalVaccine" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Contact Method" />
                                <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3>All-time Services Rendered</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtTotalService' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvTotalService" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Contact Method" />
                                <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3>All-time Resident Event Attendance</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtTotalEvent' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvTotalEvent" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Contact Method" />
                                <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <%-- Interaction Level Report --%>
                <h2><ins>Interaction Data for Selected Timeframe <asp:Label ID="lblTimeframe" runat="server" Text=""></asp:Label></ins></h2>
                <h3>Interactions per Gender</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtInteractionGender' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvInteractionGender" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Contact Method" />
                                <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3>Interactions per Primary Language</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtInteractionLanguage' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvInteractionLanguage" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Contact Method" />
                                <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3>Interactions per Contact Method</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtInteractionContact' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvInteractionContact" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Contact Method" />
                                <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <h3>Interactions per Service</h3>
                <div class="row border-bottom mb-5 pb-5">
                    <div class="col-8">
                        <div id='chrtInteractionService' class="ct-chart ct-perfect-fourth"></div>
                    </div>
                    <div class="col pt-5">
                        <asp:GridView ID="gvInteractionService" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                            <HeaderStyle CssClass="cherryBackground" />
                            <Columns>
                                <asp:BoundField DataField="labels" HeaderText="Contact Method" />
                                <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            ['gvTotalGender', 'gvTotalLanguage', 'gvTotalVaccine', 'gvTotalService', 'gvTotalEvent',
                'gvInteractionGender', 'gvInteractionLanguage', 'gvInteractionService','gvInteractionContact'].forEach(gv => {
                $(`#MainContent_${gv}`).DataTable({
                    "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                    "searching": false
                });
            });
        });
    </script>
</asp:Content>
