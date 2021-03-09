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
            

            if (!IsPostBack)
            {
                try
                {
                    user = Session["User"] as CARESUser;
                }
                catch(Exception ex)
                {
                    
                    Response.Redirect("Login.aspx");
                }


                if (Session["Resident"] != null && HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile"))
                {
                    Resident res = Session["Resident"] as Resident;
                    hfResidentDetails.Value = $"{res.ResidentFirstName} {res.ResidentLastName} {res.Home.Address}";
                }


                if (user.UserType == "C")
                {
                    GetAllInteractionsByWorkerID gi = new  GetAllInteractionsByWorkerID();
                    dt = gi.RunCommand(user.UserID);
                    //Remove the CHW name columns as they can only see their own
                    dt.Columns.Remove("FirstName1");
                    dt.Columns.Remove("LastName1");

                    gvInteractionList.DataSource = dt;

                }
                else
                {
                    //Need to add organization filtering
                    GetAllInteractions getAllInteractions = new GetAllInteractions();
                    dt = getAllInteractions.ExecuteCommand();
                    gvInteractionList.DataSource = dt;
                }

                if (dt.Rows.Count != 0)
                {
                    //Adds table head tag to visual studio html output so gridview format will work DataTables
                    gvInteractionList.DataBound += (object o, EventArgs ev) =>
                    {
                        gvInteractionList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    };
                }

                Session["InteractionListDT"] = dt;
                gvInteractionList.DataBind();
            }

            if (Session["InteractionListDT"] != null)
                dt = Session["InteractionListDT"] as DataTable;
        }



        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }


        protected void btnViewInteraction_Click(object sender, EventArgs e)
        {

            if (Session["InteractionListDT"] != null)
               dt = Session["InteractionListDT"] as DataTable;
            else
            {
                //Error handling
            }

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //Recreate the Datarow the GVR is bound to
            DataRow dr = dt.Rows[row.DataItemIndex];



            //Create resident
            GetResidentByID gr = new GetResidentByID();
            DataRow resDr = gr.RunCommand(int.Parse(dr["ResidentID"].ToString())).Rows[0];
            Resident res = new Resident(dr);

            Session["Resident"] = res;

            //Create Interaction
            Interaction interaction = new Interaction(dr);
            Session["Interaction"] = interaction;



            Server.Transfer("ResidentInteractionForm.aspx");
        }
    }
}