using System;
using System.Web.UI.WebControls;
using System.Data;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;

namespace CaresTracker
{
    public partial class EventList : System.Web.UI.Page
    {
        DataTable dtEventList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvEventList.DataBound += (object o, EventArgs ev) =>
                {
                    gvEventList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                dtEventList = new GetAllEvents().ExecuteCommand();

                if (dtEventList.Rows.Count == 0) {
                    lblNoRows.Text = "Couldn't find any events to display.";
                    divNoRows.Visible = true;
                    return;
                }
                gvEventList.DataSource = dtEventList;
                gvEventList.DataBind();

                Session["EventListDT"] = dtEventList;
            }

            dtEventList = Session["EventListDT"] as DataTable;

            if (Request.Browser.IsMobileDevice)
                gvEventList.Columns[2].Visible = false; //Hide event description on mobile
        }

        protected void btnViewEvent_Click(object sender, EventArgs e)
        {
            // get the selected DataRow
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            DataRow dr = dtEventList.Rows[row.DataItemIndex];

            // create event in Session
            Session["Event"] = new DataModels.Event(dr);

            Response.Redirect("./Event.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }
        
    }
}
