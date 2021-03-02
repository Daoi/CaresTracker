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

namespace CapstoneUI
{
    public partial class InteractionList : Page
    {
        CARESUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile"))
                {
                    Resident res = Session["Resident"] as Resident;
                    hfResidentDetails.Value = $"{res.FirstName} {res.LastName} {res.Home.Address}";
                }
                else if (HttpContext.Current.Request.Url.ToString().Contains("CHWManagement"))
                {
                    CARESUser worker = Session["Worker"] as CARESUser;
                    hfResidentDetails.Value = $"{worker.FirstName} {worker.LastName}";
                }

                user = Session["User"] as CARESUser;
                GetAllInteractions getAllInteractions = new GetAllInteractions();
                DataTable ds = getAllInteractions.ExecuteCommand();

                if (user.UserType == "C")
                {
                    for (int i = ds.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow record = ds.Rows[i];
                        if (int.Parse(record["HealthWorkerID"].ToString()) != user.UserID)
                        {
                            ds.Rows[i].Delete();
                        }
                    }
                    ds.AcceptChanges();
                    gvInteractionList.Columns[3].Visible = false;
                    gvInteractionList.DataSource = ds;
                }
                else
                {
                    gvInteractionList.DataSource = ds;
                }

                if (ds.Rows.Count != 0)
                {
                    //Adds table head tag to visual studio html output so gridview format will work DataTables
                    gvInteractionList.DataBound += (object o, EventArgs ev) =>
                    {
                        gvInteractionList.HeaderRow.TableSection = TableRowSection.TableHeader;
                    };
                }

                gvInteractionList.DataBind();
            }
        }



        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }


        protected void btnViewInteraction_Click(object sender, EventArgs e)
        {

        }
    }
}