using CapstoneUI.DataModels;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess;

namespace CapstoneUI
{
    public partial class InteractionList : Page
    {
        CARESUser user;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["User"] as CARESUser;

            if (!IsPostBack)
            {
                if (Session["Resident"] != null && HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile"))
                {
                    Resident res = Session["Resident"] as Resident;
                    hfResidentDetails.Value = $"{res.ResidentFirstName} {res.ResidentLastName} {res.DateOfBirth}";
                }
                else if (Session["Worker"] != null && HttpContext.Current.Request.Url.ToString().Contains("CHWManagement"))
                {
                    CARESUser worker = Session["Worker"] as CARESUser;
                    hfResidentDetails.Value = $"{worker.UserFirstName} {worker.UserLastName}";
                }


                if (user.UserType == "T") // temple admins can see all interactions
                {
                    dt = new GetAllInteractions().ExecuteCommand();
                }
                else // filter by user's organization
                {
                    dt = new GetAllInteractionsByWorkerID().RunCommand(user.UserID);
                }

                if (dt.Rows.Count == 0) { return; }

                gvInteractionList.DataBound += (object o, EventArgs ev) =>
                {
                    gvInteractionList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                gvInteractionList.DataSource = dt;
                gvInteractionList.DataBind();
                ViewState["InteractionListDT"] = dt;
            }

            if (ViewState["InteractionListDT"] != null)
                dt = ViewState["InteractionListDT"] as DataTable;
        }



        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }


        protected void btnViewInteraction_Click(object sender, EventArgs e)
        {

            if (ViewState["InteractionListDT"] != null)
               dt = ViewState["InteractionListDT"] as DataTable;
            else
            {
                //Error handling
            }

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //Recreate the Datarow the GVR is bound to
            DataRow dr = dt.Rows[row.DataItemIndex];

            //Create resident
            Resident res = new Resident(dr);
            Session["Resident"] = res;

            //Create Interaction
            Interaction interaction = new Interaction(dr);
            Session["Interaction"] = interaction;

            Server.Transfer("ResidentInteractionForm.aspx");
        }
    }
}