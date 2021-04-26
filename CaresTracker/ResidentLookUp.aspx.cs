using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.ResidentAccessors;
using CaresTracker.Utilities;

namespace CaresTracker
{
    public partial class ResidentLookUp : Page
    {
        CARESUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["User"] as CARESUser;
            if (!IsPostBack)
            {
                DataTable ds = new GetResidentsByUserID().ExecuteCommand(user.UserID);

                gvResidentList.DataSource = ds;
                ViewState["ResidentList"] = ds;

                gvResidentList.DataBound += (object o, EventArgs ev) =>
                {
                    gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                if (ds.Rows.Count == 0)
                {
                    lblNoRows.Text = "Couldn't find any Residents to display.";
                    divNoRows.Visible = true;
                    return;
                }

                gvResidentList.DataBind();
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void NewResident_Click(object sender, EventArgs e)
        {

            Response.Redirect("CreateResident.aspx");
        }

        protected void btnViewResident_Click(object sender, EventArgs e)
        {
            //Get the row containing the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //Recreate the Datarow the GVR is bound to
            DataRow dr = (ViewState["ResidentList"] as DataTable).Rows[row.DataItemIndex];
            Resident res = new Resident(dr);

            Session["Resident"] = res;
            Response.Redirect("ResidentProfile.aspx");
        }
    }
}
