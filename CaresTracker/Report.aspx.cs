using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.ReportAccessors;
using CaresTracker.DataModels;

namespace CaresTracker
{
    public partial class Report : System.Web.UI.Page
    {
        protected string jsonReports;
        private Dictionary<string, Dictionary<string, List<object>>> jsonDict;
        protected void Page_Load(object sender, EventArgs e)
        {
            CARESUser user = Session["User"] as CARESUser;

            // redirect unauthorized users
            if (!(user.UserType.Equals("T") || user.UserType.Equals("A"))) { Response.Redirect("./Homepage.aspx"); }

            if (!IsPostBack)
            {
                int developmentID = int.Parse(Session["ReportDevelopmentID"].ToString());
                string startDate = Session["ReportStartDate"].ToString();
                string endDate = Session["ReportEndDate"].ToString();
                lblTimeframe.Text += $"Start Date: {startDate}<br />End Date: {endDate}";

                this.jsonDict = new Dictionary<string, Dictionary<string, List<object>>>();
                DataTable tblTemp;
                try
                {
                    tblTemp = new GetTotalGenderReport().ExecuteCommand(developmentID);

                    // if one is empty, then all will be empty since these demographics are
                    // aggregated using all residents in a development
                    if (tblTemp.Rows.Count == 0)
                    {
                        lblErrorDevelopmentTotals.Visible = true;
                        lblErrorDevelopmentTotals.Text = "There are no residents from this housing development in the system.";
                        pnlDevelopmentTotals.Visible = false;
                        pnlInteractionDataHeader.Visible = false;
                        pnlInteractionData.Visible = false;
                        return;
                    }

                    AddDataToJsonDict(tblTemp, "#chrtTotalGender");
                    SetUpGridView(gvTotalGender, tblTemp);

                    tblTemp = new GetTotalLanguageReport().ExecuteCommand(developmentID);
                    AddDataToJsonDict(tblTemp, "#chrtTotalLanguage");
                    SetUpGridView(gvTotalLanguage, tblTemp);

                    tblTemp = new GetTotalVaccineReport().ExecuteCommand(developmentID);
                    AddDataToJsonDict(tblTemp, "#chrtTotalVaccine");
                    SetUpGridView(gvTotalVaccine, tblTemp);

                    int numAttendees = new GetTotalEventReport().ExecuteCommand(developmentID);
                    DataTable dtEvent = CreateDataTableFromScalar(numAttendees, "Attendances");
                    AddDataToJsonDict(dtEvent, "#chrtTotalEvent");
                    SetUpGridView(gvTotalEvent, dtEvent);
                }
                catch (Exception ex)
                {
                    lblErrorDevelopmentTotals.Text = ex.Message;
                }

                try
                {
                    tblTemp = new GetInteractionGenderReport().ExecuteCommand(developmentID, startDate, endDate);

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

                    tblTemp = new GetInteractionLanguageReport().ExecuteCommand(developmentID, startDate, endDate);
                    AddDataToJsonDict(tblTemp, "#chrtInteractionLanguage");
                    SetUpGridView(gvInteractionLanguage, tblTemp);

                    tblTemp = new GetInteractionContactReport().ExecuteCommand(developmentID, startDate, endDate);
                    AddDataToJsonDict(tblTemp, "#chrtInteractionContact");
                    SetUpGridView(gvInteractionContact, tblTemp);

                    tblTemp = new GetInteractionServiceReport().ExecuteCommand(developmentID, startDate, endDate);
                    AddDataToJsonDict(tblTemp, "#chrtInteractionService");
                    SetUpGridView(gvInteractionService, tblTemp);
                }
                catch (Exception ex)
                {
                    lblErrorInteractionData.Text = ex.Message;
                }

                this.jsonReports = JsonConvert.SerializeObject(this.jsonDict);
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
