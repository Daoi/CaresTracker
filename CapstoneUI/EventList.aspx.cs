using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapstoneUI.DataAccess.DataAccessors.EventAccessors;

namespace CapstoneUI
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

                if (dtEventList.Rows.Count == 0) { return; }
                gvEventList.DataSource = dtEventList;
                gvEventList.DataBind();

                Session["EventListDT"] = dtEventList;
            }

            dtEventList = Session["EventListDT"] as DataTable;
        }

        protected void btnViewEvent_Click(object sender, EventArgs e)
        {

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }
    }
}
