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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            if (Validation.IsEmpty(txtUsername.Text) || Validation.IsEmpty(txtPassword.Text))
            {
                lblError.Text = "Enter your Username and Password.";
                return;
            }

            // AWS Cognito Login
            AWSCognitoManager man = new AWSCognitoManager();

            try
            {
                var authResponse = await man.SignInAsync(txtUsername.Text, txtPassword.Text);

                if (authResponse != null)
                {
                    lblError.Text = "";
                    Session["CognitoManager"] = man;
                    Session["AccountType"] = man.IsAdmin;
                    Session["LoginStatus"] = true;
                    Session["UserName"] = man.UserFirstName;
                    Response.Redirect("./Homepage.aspx", false);
                }
                else
                {
                    lblError.Text = "An unknown error occurred. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}