using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.ReportAccessors;
using CaresTracker.DataAccess.DataAccessors.ReportAccessors.DevelopmentReports;
using CaresTracker.DataModels;

namespace CaresTracker
{
    /// <summary>
    /// Create a report from the Session values collected on ExportData.aspx
    /// Session["ReportType"]: D, O, or C
    /// Session["ReportDomainID"]: PK of the entity selected
    /// Session["ReportDomainName"]: name of the entity selected (just to display)
    /// Session["ReportStartDate"]
    /// Session["ReportEndDate"]
    /// </summary>
    public partial class Report : System.Web.UI.Page
    {
        int domainID;
        string startDate;
        string endDate;
        protected string jsonReports;
        private Dictionary<string, Dictionary<string, List<object>>> jsonDict;

        protected void Page_Load(object sender, EventArgs e)
        {
            CARESUser user = Session["User"] as CARESUser;

            // redirect unauthorized users
            if (!(user.UserType.Equals("T") || user.UserType.Equals("A"))) { Response.Redirect("./Homepage.aspx"); }

            if (!IsPostBack)
            {
                domainID = int.Parse(Session["ReportDomainID"].ToString());
                startDate = Session["ReportStartDate"].ToString();
                endDate = Session["ReportEndDate"].ToString();

                lblDomainHeader.Text = $"<u>Report Totals for {Session["ReportDomainName"].ToString()}</u>";
                lblTimeframe.Text += $"Start Date: {startDate}<br />End Date: {endDate}";

                switch (Session["ReportType"].ToString())
                {
                    case "D":
                        pnlDevelopmentTotals.Visible = true;
                        GenerateDevelopmentReport();
                        GenerateInteractionReport();
                        break;
                    case "O":
                        break;
                    case "C":
                        break;
                }

                this.jsonReports = JsonConvert.SerializeObject(this.jsonDict);
            }
        }

        private void GenerateDevelopmentReport()
        {
            this.jsonDict = new Dictionary<string, Dictionary<string, List<object>>>();
            DataTable tblTemp;
            try
            {
                tblTemp = new GetTotalGenderReport().ExecuteCommand(domainID);

                // if one is empty, then all will be empty since these demographics are
                // aggregated using all residents in a development
                if (tblTemp.Rows.Count == 0)
                {
                    lblErrorDomainTotals.Visible = true;
                    lblErrorDomainTotals.Text = "There are no residents from this housing development in the system.";
                    pnlDevelopmentTotals.Visible = false;
                    pnlInteractionDataHeader.Visible = false;
                    pnlInteractionData.Visible = false;
                    return;
                }

                AddDataToJsonDict(tblTemp, "#chrtTotalGender");
                SetUpGridView(gvTotalGender, tblTemp);

                tblTemp = new GetTotalAgeReport().ExecuteCommand(domainID);
                AddDataToJsonDict(tblTemp, "#chrtTotalAge");
                SetUpGridView(gvTotalAge, tblTemp);

                tblTemp = new GetTotalLanguageReport().ExecuteCommand(domainID);
                AddDataToJsonDict(tblTemp, "#chrtTotalLanguage");
                SetUpGridView(gvTotalLanguage, tblTemp);

                tblTemp = new GetTotalResidentCIReport().ExecuteCommand(domainID);
                AddDataToJsonDict(tblTemp, "#chrtTotalChronicIllness");
                SetUpGridView(gvTotalChronicIllness, tblTemp);

                tblTemp = new GetTotalVaccineReport().ExecuteCommand(domainID);
                AddDataToJsonDict(tblTemp, "#chrtTotalVaccine");
                SetUpGridView(gvTotalVaccine, tblTemp);

                int numAttendees = new GetTotalEventReport().ExecuteCommand(domainID);
                DataTable dtEvent = CreateDataTableFromScalar(numAttendees, "Attendances");
                AddDataToJsonDict(dtEvent, "#chrtTotalEvent");
                SetUpGridView(gvTotalEvent, dtEvent);
            }
            catch (Exception ex)
            {
                lblErrorDomainTotals.Visible = true;
                lblErrorDomainTotals.Text = "A database error occurred: " + ex.Message;
                pnlDevelopmentTotals.Visible = false;
                pnlInteractionDataHeader.Visible = false;
                pnlInteractionData.Visible = false;
                return;
            }
        }

        private void GenerateInteractionReport()
        {
            DataTable tblTemp;
            try
            {
                tblTemp = new GetInteractionGenderReport().ExecuteCommand(domainID, startDate, endDate);

                // if one of these is empty, then the rest will be empty since these counts are
                // aggregated using all interactions with residents in a development
                if (tblTemp.Rows.Count == 0)
                {
                    lblErrorInteractionData.Visible = true;
                    lblErrorInteractionData.Text = "There were no interactions at this housing development during the selected timeframe.";
                    pnlInteractionData.Visible = false;
                    this.jsonReports = JsonConvert.SerializeObject(this.jsonDict); // just show the development level data
                    return;
                }

                AddDataToJsonDict(tblTemp, "#chrtInteractionGender");
                SetUpGridView(gvInteractionGender, tblTemp);

                tblTemp = new GetInteractionAgeReport().ExecuteCommand(domainID, startDate, endDate);
                AddDataToJsonDict(tblTemp, "#chrtInteractionAge");
                SetUpGridView(gvInteractionAge, tblTemp);

                tblTemp = new GetInteractionLanguageReport().ExecuteCommand(domainID, startDate, endDate);
                AddDataToJsonDict(tblTemp, "#chrtInteractionLanguage");
                SetUpGridView(gvInteractionLanguage, tblTemp);

                tblTemp = new GetInteractionContactReport().ExecuteCommand(domainID, startDate, endDate);
                AddDataToJsonDict(tblTemp, "#chrtInteractionContact");
                SetUpGridView(gvInteractionContact, tblTemp);

                tblTemp = new GetInteractionServiceReport().ExecuteCommand(domainID, startDate, endDate);
                AddDataToJsonDict(tblTemp, "#chrtInteractionService");
                SetUpGridView(gvInteractionService, tblTemp);
            }
            catch (Exception ex)
            {
                lblErrorInteractionData.Visible = true;
                lblErrorInteractionData.Text = "A database error occurred: " + ex.Message;
                pnlInteractionData.Visible = false;
            }
        }

        // chartID should match the selector of the chart in the aspx markup
        private void AddDataToJsonDict(DataTable data, string chartID)
        {
            (List<object> labels, List<object> series) pairs = (new List<object>(), new List<object>());

            data.Rows.Cast<DataRow>().ToList()
                .ForEach(row =>
                {
                    pairs.labels.Add(row[0].ToString());
                    pairs.series.Add(int.Parse(row[1].ToString()));
                });

            this.jsonDict.Add(chartID,
                new Dictionary<string, List<object>>()
                {
                    { "labels", pairs.labels },
                    { "series", pairs.series}
                }
            );
        }

        private DataTable CreateDataTableFromScalar(int scalarValue, string groupName)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("labels");
            tbl.Columns.Add("series");

            DataRow newRow = tbl.NewRow();
            newRow["labels"] = groupName;
            newRow["series"] = scalarValue;
            tbl.Rows.Add(newRow);

            return tbl;
        }

        private void SetUpGridView(GridView gv, DataTable data)
        {
            gv.DataSource = data;
            gv.DataBind();
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }

        protected void lnkData_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ExportData.aspx");
        }
    }
}
