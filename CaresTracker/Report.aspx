<%@ Page Title="" Language="C#" MasterPageFile="~/CaresTracker.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="CaresTracker.Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid homepage pb-5">
        <div class="row modal-header offwhiteBackground p-0" style="height: 50px; padding-left: 0!important; padding-right: 0!important; font-size: large;">
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
                <%-- Domain Header + Error Message --%>
                <asp:Label ID="lblDomainHeader" runat="server" CssClass="h2"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblErrorDomainTotals" runat="server" CssClass="errorLabel" Visible="false"></asp:Label>
                <%-- Housing Development Level Report --%>
                <asp:Panel ID="pnlDevelopmentTotals" runat="server" Visible="false">
                    <h3>Residents per Gender</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtTotalGender' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvTotalGender" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
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
                            <asp:GridView ID="gvTotalAge" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
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
                            <asp:GridView ID="gvTotalLanguage" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Primary Language" />
                                    <asp:BoundField DataField="series" HeaderText="# of Residents" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <h3>Residents per Chronic Illness</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtTotalChronicIllness' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvTotalChronicIllness" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Chronic Illness" />
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
                            <asp:GridView ID="gvTotalVaccine" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Vaccine Status" />
                                    <asp:BoundField DataField="series" HeaderText="# of Residents" />
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
                            <asp:GridView ID="gvTotalEvent" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="series" HeaderText="Attendances" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <%-- Organization/CHW Level Report --%>
                <asp:Panel ID="pnlOrgCHWTotals" runat="server" Visible="false">
                    <h3>Total Number of Interactions</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtTotalInteractions' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvTotalInteractions" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <h3>Total Interactions per Service</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtTotalServices' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvTotalServices" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
                                <HeaderStyle CssClass="cherryBackground" />
                                <Columns>
                                    <asp:BoundField DataField="labels" HeaderText="Service" />
                                    <asp:BoundField DataField="series" HeaderText="# of Interactions" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <%-- Interaction Level Report --%>
                <asp:Panel ID="pnlInteractionDataHeader" runat="server">
                    <h2><ins>Interaction Level Data</ins></h2>
                    <asp:Label ID="lblTimeframe" runat="server" Text="" CssClass="h5"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblErrorInteractionData" runat="server" CssClass="errorLabel" Visible="false"></asp:Label>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlInteractionData" runat="server" Visible="false">
                    <h3>Interactions per Gender</h3>
                    <div class="row border-bottom mb-5 pb-5">
                        <div class="col-8">
                            <div id='chrtInteractionGender' class="ct-chart ct-perfect-fourth"></div>
                        </div>
                        <div class="col pt-5">
                            <asp:GridView ID="gvInteractionGender" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
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
                            <asp:GridView ID="gvInteractionAge" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
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
                            <asp:GridView ID="gvInteractionLanguage" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
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
                            <asp:GridView ID="gvInteractionContact" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
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
                            <asp:GridView ID="gvInteractionService" runat="server" AutoGenerateColumns="False" CssClass="table table-light table-striped table-bordered thead-dark">
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
            $(`.table`).DataTable({
                "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
                "searching": false
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
                    axisY: {
                        onlyInteger: true,
                    }
                }
            ).on('draw', function (data) {
                if (data.type === 'bar') {
                    data.element.attr({
                        style: 'stroke-width: 30px; stroke: #A41E35'
                    });
                }
            });
        });
    </script>
</asp:Content>
