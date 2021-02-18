using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using CapstoneUI.Utilities;

namespace CapstoneUI
{
    public partial class CreateCHW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // if (!IsPostBack)
            // {
            //   stored procedure to retrieve all regions
            //   ddlRegion.DataSource = ... 
            //   ddlRegion.DataTextField = "RegionName"
            //   ddlRegion.DataValueField = "RegionID"
            //   ddlRegion.DataBind()
            //   
            //   stored procedure to retrieve all users with type admin/supervisor
            //   ddlSupervisor.DataSource = ...
            //   ddlSupervisor.DataTextField = "FirstName" + " " + "LastName" 
            //   ddlSupervisor.DataValueField = "UserID"
            //   ddlSuperVisor.DataBind()
            // }

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
            {
                Response.Write("<script>alert('Please enter first name')</script>");
            }
            else if (txtLastName.Text == "")
            {
                Response.Write("<script>alert('Please enter last name.')</script>");
            }
            else if (txtUsername.Text == "")
            {
                Response.Write("<script>alert('Please enter username.')</script>");
            }
            else if (txtPassword.Text == "")
            {
                Response.Write("<script>alert('Please enter password.')</script>");
            }
            else if (txtEmail.Text == "")
            {
                Response.Write("<script>alert('Please enter email.')</script>");
            }
            else if (txtPhoneNumber.Text == "")
            {
                Response.Write("<script>alert('Please enter phone number.')</script>");
            }






        }
    }
}