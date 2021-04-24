using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;
using CaresTracker.DataModels;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace CaresTracker
{
    public partial class CHWList : System.Web.UI.Page
    {
        CARESUser user;
        DataTable dtCHWList;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["User"] as CARESUser;

            // redirect CHWs
            if (user.UserType == "C") { Response.Redirect("./Homepage.aspx"); }

            if (!IsPostBack)
            {
                gvCHWList.DataBound += (object o, EventArgs ev) =>
                {
                    gvCHWList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                dtCHWList = new GetWorkersByUserID().RunCommand(user.UserID);

                if (dtCHWList.Rows.Count == 0)
                {
                    lblNoRows.Text = "Couldn't find any Community Health Workers to display.";
                    divNoRows.Visible = true;
                    return;
                }
                gvCHWList.DataSource = dtCHWList;
                gvCHWList.DataBind();

                ViewState["CHWListDT"] = dtCHWList;
            }

            dtCHWList = ViewState["CHWListDT"] as DataTable;
        }

        // redirect to the management page
        protected void btnViewWorker_Click(object sender, EventArgs e)
        {
            // get the selected DataRow
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            DataRow dr = dtCHWList.Rows[row.DataItemIndex];

            // create worker in Session
            Session["Worker"] = new CARESUser(dr);

            Response.Redirect("./CHWManagement.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }
    }
}
