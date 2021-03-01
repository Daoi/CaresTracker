using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.Utilities;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;

namespace CapstoneUI
{
    public partial class ResidentLookUp : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Resident> residents = new List<Resident>();
                GetAllResident getAllResident = new GetAllResident();
                DataTable ds = getAllResident.RunCommand();

                gvResidentList.DataSource = ds;
                Session["ResidentList"] = ds;

                gvResidentList.DataBound += (object o, EventArgs ev) =>
                {
                    gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                gvResidentList.DataBind();
            }

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }


        protected void NewResident_Click(object sender, EventArgs e)
        {
            
            Response.Redirect($"CreateResident.aspx");
        }

        protected void btnViewResident_Click(object sender, EventArgs e)
        {
            //Get the row containing the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //Recreate the Datarow the GVR is bound to
            DataRow dr = (Session["ResidentList"] as DataTable).Rows[row.DataItemIndex];

            Resident res = new Resident(dr);
            GetHouseByID gh = new GetHouseByID();
            res.Home = new House(gh.RunCommand(int.Parse(dr["HouseID"].ToString())).Rows[0]); //Look up House by ID, create house obj, add to resident

            if (res.Home.DevelopmentID != -1) //-1 = HCV/Non development housing
            {
                GetDevelopmentByID gd = new GetDevelopmentByID();
                res.HousingDevelopment = new HousingDevelopment(gd.RunCommand(res.Home.DevelopmentID).Rows[0]);
            }
            Session["Resident"] = res;
            Response.Redirect("ResidentProfile.aspx");
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            Session.Remove("ResidentList");
        }
    }
}