using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.ReportAccessors;

namespace CaresTracker
{
    public partial class Report : System.Web.UI.Page
    {
        protected string jsonReports;
        private Dictionary<string, Dictionary<string, List<object>>> jsonDict;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    gvTotalGender.DataSource = tblTemp;
                    gvTotalGender.DataBind();
                    gvTotalGender.HeaderRow.TableSection = TableRowSection.TableHeader;

                    tblTemp = new GetTotalLanguageReport().ExecuteCommand(developmentID);
                    AddDataToJsonDict(tblTemp, "#chrtTotalLanguage");
                    gvTotalLanguage.DataSource = tblTemp;
                    gvTotalLanguage.DataBind();
                    gvTotalLanguage.HeaderRow.TableSection = TableRowSection.TableHeader;

                    tblTemp = new GetTotalVaccineReport().ExecuteCommand(developmentID);
                    AddDataToJsonDict(tblTemp, "#chrtTotalVaccine");
                    gvTotalVaccine.DataSource = tblTemp;
                    gvTotalVaccine.DataBind();
                    gvTotalVaccine.HeaderRow.TableSection = TableRowSection.TableHeader;

                    int numAttendees = new GetTotalEventReport().ExecuteCommand(developmentID);
                    DataTable dtEvent = CreateDataTableFromScalar(numAttendees, "Attendances");
                    AddDataToJsonDict(dtEvent, "#chrtTotalEvent");
                    gvTotalEvent.DataSource = dtEvent;
                    gvTotalEvent.DataBind();
                    gvTotalEvent.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                    gvInteractionGender.DataSource = tblTemp;
                    gvInteractionGender.DataBind();
                    gvInteractionGender.HeaderRow.TableSection = TableRowSection.TableHeader;

                    tblTemp = new GetInteractionLanguageReport().ExecuteCommand(developmentID, startDate, endDate);
                    AddDataToJsonDict(tblTemp, "#chrtInteractionLanguage");
                    gvInteractionLanguage.DataSource = tblTemp;
                    gvInteractionLanguage.DataBind();
                    gvInteractionLanguage.HeaderRow.TableSection = TableRowSection.TableHeader;

                    tblTemp = new GetInteractionContactReport().ExecuteCommand(developmentID, startDate, endDate);
                    AddDataToJsonDict(tblTemp, "#chrtInteractionContact");
                    gvInteractionContact.DataSource = tblTemp;
                    gvInteractionContact.DataBind();
                    gvInteractionContact.HeaderRow.TableSection = TableRowSection.TableHeader;

                    tblTemp = new GetInteractionServiceReport().ExecuteCommand(developmentID, startDate, endDate);
                    AddDataToJsonDict(tblTemp, "#chrtInteractionService");
                    gvInteractionService.DataSource = tblTemp;
                    gvInteractionService.DataBind();
                    gvInteractionService.HeaderRow.TableSection = TableRowSection.TableHeader;
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
    }
}
