using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.Utilities;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;

namespace CapstoneUI
{
    public partial class Verify : System.Web.UI.Page
    {
        string usr;
        string pwd;
        protected void Page_Load(object sender, EventArgs e)
        {
            // try to autofill username from email link
            //if (Request.QueryString["usr"] != null && Request.QueryString["pwd"] != null)
            //{
            //    usr = Request.QueryString["usr"];
            //    pwd = Request.QueryString["pwd"];
            //    lblUsername.Text = $"Hello, {usr}!";
            //}
            //else
            //{
            //    Response.Redirect("./Login.aspx");
            //}
        }

        protected async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Validation.IsEmpty(txtNewPassword.Text) || Validation.IsEmpty(txtRetypePassword.Text))
            {
                lblError.Text = "Fill out all fields.";
                return;
            }

            if (txtNewPassword.Text != txtRetypePassword.Text)
            {
                lblError.Text = "New password does not match confirmation.";
                return;
            }

            if (!Validation.IsValidPassword(txtNewPassword.Text))
            {
                lblError.Text = "The new password does not fit the requirements.";
                return;
            }

            // AWS Cognito Login
            AWSCognitoManager man = new AWSCognitoManager();

            try
            {
                // confirm user and change password
                var signUpResponse = await man.ConfirmSignUpAsync(usr, pwd, txtNewPassword.Text);

                // I haven't run into a scenario in which the previous method fails and doesn't throw an error, but just in case...
                if (signUpResponse == null) { lblError.Text = "An unknown error occurred. Please try again later."; }

                // automatically sign user in
                var authResponse = await man.SignInAsync(usr, txtNewPassword.Text);

                if (authResponse != null)
                {
                    lblError.Text = "";

                    // get user data from db
                    GetUser accessor = new GetUser();
                    Session["User"] = new CARESUser(accessor.RunCommand(usr).Rows[0]);
                    Session["CognitoManager"] = man;

                    // record login
                    UpdateLastLogin updater = new UpdateLastLogin();
                    updater.ExecuteCommand(usr, DateTime.Now.ToString());

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