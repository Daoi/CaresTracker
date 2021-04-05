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
                lblTimeframe.Text = $"{startDate} - {endDate}";

                this.jsonDict = new Dictionary<string, Dictionary<string, List<object>>>();
                DataTable tblTemp;

                tblTemp = new GetTotalGenderReport().ExecuteCommand(developmentID);
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
                DataTable dtEvent = CreateDataTableFromScalar(numAttendees, "Attendees");
                AddDataToJsonDict(dtEvent, "#chrtTotalEvent");
                gvTotalEvent.DataSource = dtEvent;
                gvTotalEvent.DataBind();
                gvTotalEvent.HeaderRow.TableSection = TableRowSection.TableHeader;

                tblTemp = new GetInteractionGenderReport().ExecuteCommand(developmentID, startDate, endDate);
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

                this.jsonReports = JsonConvert.SerializeObject(this.jsonDict);
            };
        }

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
