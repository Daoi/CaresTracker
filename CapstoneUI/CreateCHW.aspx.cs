using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.Utilities;


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

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            //validation
            string phoneNumber = "+1" + txtPhoneNumber.Text;

            AWSCognitoManager man = (AWSCognitoManager)Session["CognitoManager"];

            try
            {
                if (ddlIsSupervisor.SelectedValue == "yes")
                {
                    var res = await man.CreateUserAsync(txtUsername.Text, txtEmail.Text, txtFirstName.Text, txtLastName.Text, phoneNumber, 0);

                    if (res != null)
                    {
                        Response.Write("<script>alert('Admin/Supervisor inserted successfully.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('An unknown error occurred. Please try again later.')</script>");
                    }
                }
                else
                {
                    var res = await man.CreateUserAsync(txtUsername.Text, txtEmail.Text, txtFirstName.Text, txtLastName.Text, phoneNumber, 1);

                    if (res != null)
                    {
                        Response.Write("<script>alert('CHW inserted successfully.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('An unknown error occurred. Please try again later.')</script>");
                    }
                }
            }
            catch (Exception ex)
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