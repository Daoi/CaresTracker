using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess.DataAccessors.Examples;

namespace CapstoneUI
{
    public partial class CreateCHW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divSelectSupervisor.Visible = false;
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> values = new List<string>();
                values.Add(txtUsername.Text);
                values.Add(txtPassword.Text);
                values.Add(txtFirstName.Text);
                values.Add(txtLastName.Text);
                values.Add(txtEmail.Text);
                values.Add(txtPhoneNumber.Text);
                values.Add("Active");

                if (ddlIsSupervisor.SelectedValue == "yes")
                {
                    values.Add("A");
                    values.Add(null);
                }
                else
                {
                    values.Add("C");
                    values.Add(ddlSupervisor.SelectedValue);
                }
                values.Add(ddlRegion.SelectedValue);

                CHWWriter newCHW = new CHWWriter(values);
                newCHW.ExecuteCommand();
                Response.Write("<script>alert('CHW inserted successfully')</script>");
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert(" + ex.ToString() + ")</script>");
            }
        }

        protected void ddlIsSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlIsSupervisor.SelectedValue == "no")
            {
                divSelectSupervisor.Visible = true;
            }
            else
            {
                divSelectSupervisor.Visible = false;
            }
        }
    }
}