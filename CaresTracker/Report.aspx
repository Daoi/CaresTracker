<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="CaresTracker.Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mx-auto homepage px-0 pt-0 pb-5">
        <div class="row modal-header no-gutters offwhiteBackground" style="height: auto; font-size: large">
            <nav aria-label="breadcrumb w-75">
                <ol class="breadcrumb bg-transparent">
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lnkHome" NavigateUrl="~/Homepage.aspx" CssClass="cherryFont" runat="server" OnClick="lnkHome_Click">Dashboard</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item">
                        <asp:LinkButton ID="lnkData" NavigateUrl="~/ExportData.aspx" CssClass="cherryFont" runat="server" OnClick="lnkData_Click">Create Report</asp:LinkButton>
                    </li>
                    <li class="breadcrumb-item active" aria-current="page">Report</li>
                </ol>
            </nav>
        </div>
        <div class="container-fluid p-0">
            <div class="container my-5">
                <%-- Housing Development Level Report --%>
                <h2><ins>Housing Development Totals</ins></h2>
                <br />
                <asp:Label ID="lblErrorDevelopmentTotals" runat="server" CssClass="errorLabel" Visible="false"></asp:Label>
                <asp:Panel ID="pnlDevelopmentTotals" runat="server">
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
                    <h3>Residents per Age Group</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtTotalAge' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvTotalAge" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Age Group" />
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
                    <h3>Residents per Vaccine Status</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtTotalVaccine' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvTotalVaccine" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Vaccine Status" />
                                    <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <h3>Resident Event Attendance</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtTotalEvent' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvTotalEvent" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="series" HeaderText="Attendances" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <%-- Interaction Level Report --%>
                <asp:Panel ID="pnlInteractionDataHeader" runat="server">
                    <h2><ins>Interaction Level Data</ins></h2>
                    <asp:Label ID="lblTimeframe" runat="server" Text="" CssClass="h5"></asp:Label><br /><br />
                    <asp:Label ID="lblErrorInteractionData" runat="server" CssClass="errorLabel" Visible="false"></asp:Label>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlInteractionData" runat="server">
                    <h3>Interactions per Gender</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtInteractionGender' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvInteractionGender" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Gender" />
                                    <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <h3>Interactions per Age Group</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtInteractionAge' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvInteractionAge" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark" >
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Age Group" />
                                    <asp:BoundField DataField="series" HeaderText="# of Residents" />
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
                                    <asp:BoundField DataField="labels" HeaderText="Primary Language" />
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
                                    <asp:BoundField DataField="labels" HeaderText="Service" />
                                    <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            ['gvTotalGender', 'gvTotalAge', 'gvTotalLanguage', 'gvTotalVaccine', 'gvTotalService', 'gvTotalEvent',
                'gvInteractionGender', 'gvInteractionAge', 'gvInteractionLanguage', 'gvInteractionService','gvInteractionContact'].forEach(gv => {
                $(`#MainContent_${gv}`).DataTable({
                    "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                    "searching": false
                });
            });
        });

        // chartist: http://gionkunz.github.io/chartist-js/api-documentation.html#chartistbar-declaration-defaultoptions
        var json = <%= jsonReports %>; // from codebehind
        Object.keys(json).forEach(key => {
            var formatted = { labels: json[key].labels, series: [json[key].series] }; // series must be nested
            new Chartist.Bar(
                key, // the id of the chart
                formatted, // the data
                { // options
                    //seriesBarDistance: 10,
                    axisY: {
                        onlyInteger: true,
                    }
                }
            ).on('draw', function(data) {
                if(data.type === 'bar') {
                    data.element.attr({
                        style: 'stroke-width: 30px; stroke: #A41E35'
                    });
                }
            });
        });
    </script>
</asp:Content>
