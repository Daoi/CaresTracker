using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

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
            CARESEntities entities = new CARESEntities();

            CARESUser temp = new CARESUser();
            temp.Username = txtUsername.Text;
            temp.Password = txtPassword.Text;
            temp.FirstName = txtFirstName.Text;
            temp.LastName = txtLastName.Text;
            temp.UserEmail = txtEmail.Text;
            temp.UserPhoneNumber = txtPhoneNumber.Text;

            entities.CARESUsers.Add(temp);
            entities.SaveChanges();
        }
    }
}