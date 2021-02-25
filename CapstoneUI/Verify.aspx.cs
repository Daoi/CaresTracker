using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.Utilities;

namespace CapstoneUI
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Validation.IsEmpty(txtUsername.Text) || Validation.IsEmpty(txtTemporaryPassword.Text) ||
                Validation.IsEmpty(txtNewPassword.Text) || Validation.IsEmpty(txtRetypePassword.Text))
            {
                lblError.Text = "Fill out all fields.";
                return;
            }

            if (txtNewPassword.Text != txtRetypePassword.Text)
            {
                lblError.Text = "New password does not match confirmation.";
                return;
            }

            // AWS Cognito Login
            AWSCognitoManager man = new AWSCognitoManager();

            try
            {
                var signUpResponse = await man.ConfirmSignUpAsync(txtUsername.Text, txtTemporaryPassword.Text, txtNewPassword.Text);
                //check if response is OK
                var authResponse = await man.SignInAsync(txtUsername.Text, txtNewPassword.Text);

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