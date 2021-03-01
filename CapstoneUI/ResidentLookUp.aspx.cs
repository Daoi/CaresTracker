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
                for(int i = 0; i < ds.Rows.Count; i++)
                {
                    DataRow record = ds.Rows[i];
                    Resident temp = new Resident(record);
                    residents.Add(temp);
                }
                gvResidentList.DataSource = residents;

                gvResidentList.DataBound += (object o, EventArgs ev) =>
                {
                    gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };
                Button btnCreateNewResident = new Button()
                {
                    Text = "Create New Resident",
                    CssClass = "btn btn-primary",
                    CommandName = "CreateNewResident"
                };

                gvResidentList.DataBind();
            }
        }

        protected void gvResidentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Server.Transfer("ResidentProfile.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }


        protected void NewResident_Click(object sender, EventArgs e)
        {
            
            Response.Redirect($"CreateResident.aspx");
        }
    }
}