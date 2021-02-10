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
            SqlCommand comm = new SqlCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "AddCHW";
            comm.Parameters.AddWithValue("@theUserName", txtUsername.Text);
            comm.Parameters.AddWithValue("@thePassword", txtPassword.Text);
            comm.Parameters.AddWithValue("@theFirstName", txtFirstName.Text);
            comm.Parameters.AddWithValue("@theLastName", txtLastName.Text);
            comm.Parameters.AddWithValue("@theUserEmail", txtEmail.Text);
            comm.Parameters.AddWithValue("@theUserPhoneNumber", txtPhoneNumber.Text);
        }

        //public class CHW
        //{
        //    public int UserID { get; set; }
        //    public string UserName { get; set; }
        //    public string Password { get; set; }
        //    public string FirstName { get; set; }
        //    public string LastName { get; set; }
        //    public string Email { get; set; }
        //    public string PhoneNumber { get; set; }
        //    public DateTime LastLogin { get; set; }
        //    public string UserStatus { get; set; }
        //    public string UserType { get; set; }
        //    public int Supervisor { get; set; }
        //    public int RegionID { get; set; }
        //    public DateTime DateLastModified { get; set; }
        //    public string UserLastModified { get; set; }

        //    public CHW() { }
        //}
    }
}