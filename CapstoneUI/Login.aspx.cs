using System;
using System.Linq;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors.CARESUserAccessors;
using CapstoneUI.DataModels;
using CapstoneUI.Utilities;


namespace CapstoneUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        AWSCognitoManager man; // for Cognito API calls
        protected void Page_Load(object sender, EventArgs e)
        {
            man = new AWSCognitoManager();
        }

        // user login
        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            if (Validation.IsEmpty(txtUsername.Text) || Validation.IsEmpty(txtPassword.Text))
            {
                lblError.Text = "Fill out all fields.";
                return;
            }

            try
            {
                var authResponse = await man.SignInAsync(txtUsername.Text, txtPassword.Text);

                if (authResponse != null)
                {
                    lblError.Text = "";

                    // get user data from db
                    GetUser accessor = new GetUser();
                    Session["User"] = new CARESUser(accessor.RunCommand(txtUsername.Text).Rows[0]);
                    Session["CognitoManager"] = man;

                    // record login
                    UpdateLastLogin updater = new UpdateLastLogin();
                    updater.ExecuteCommand(txtUsername.Text, DateTime.Now.ToString());

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

        // swaps panels
        protected void switchPanels(object sender, EventArgs e)
        {
            SwitchCardClass();

            if (pnlLogin.Visible)
            {
                ClearLoginPanel();
                pnlPasswordReset.Visible = true;
            }
            else
            {
                ClearPasswordResetPanel();
                pnlLogin.Visible = true;
            }
        }

        
        // request verification code
        protected async void btnPRSendCode_Click(object sender, EventArgs e)
        {
            if (Validation.IsEmpty(txtPRUsername.Text))
            {
                lblPRError.Text = "Enter your Username.";
                return;
            }

            try
            {
                await man.SendForgotPasswordCodeAsync(txtPRUsername.Text);

                SetUpPasswordResetPanel(); // enable the rest of the controls
            }
            catch (Exception ex)
            {
                lblPRError.Text = ex.Message;
            }
        }

        // change password
        protected async void btnPRConfirm_Click(object sender, EventArgs e)
        {
            // check for empty textboxes
            if (pnlPasswordReset.Controls.OfType<TextBox>().ToList().Any(tb => Validation.IsEmpty(tb.Text)))
            {
                lblPRError.Text = "Fill out all fields.";
                return;
            }

            if (txtPRNewPassword.Text != txtPRRetypePassword.Text)
            {
                lblPRError.Text = "New password does not match confirmation.";
                return;
            }

            if (!Validation.IsValidPassword(txtPRNewPassword.Text))
            {
                lblPRError.Text = "The new password does not fit the requirements.";
                return;
            }

            try
            {
                await man.ChangePasswordAsync(txtPRUsername.Text, txtPRNewPassword.Text, txtPRVerificationCode.Text);

                // send to login panel
                SwitchCardClass();
                ClearPasswordResetPanel();
                pnlLogin.Visible = true;
                lblInstructions.Text = "Password changed successfully! Please login.";
            }
            catch (Exception ex)
            {
                lblPRError.Text = ex.Message;
            }
        }

        // makes card wider/narrower
        private void SwitchCardClass()
        {
            pnlCard.CssClass = pnlLogin.Visible ?
                pnlCard.CssClass.Replace("loginCard", "verifyCard") :
                pnlCard.CssClass.Replace("verifyCard", "loginCard");
        }

        // resets the login panel
        private void ClearLoginPanel()
        {
            pnlLogin.Visible = false;

            lblError.Text = "";
            lblInstructions.Text = "Enter your username and password.";

            ClearTextboxes(pnlLogin);
        }

        // resets the password reset panel
        private void ClearPasswordResetPanel()
        {
            pnlPasswordReset.Visible = false;

            txtPRVerificationCode.Enabled = false;
            txtPRNewPassword.Enabled = false;
            txtPRRetypePassword.Enabled = false;

            btnPRConfirm.Enabled = false;
            btnPRConfirm.CssClass = btnPRConfirm.CssClass.Replace("btn-primary", "btn-secondary");

            btnPRSendCode.Text = "Send Code";

            lblPRError.Text = "";
            lblPRInstructions.Text = "Enter your username to email your verification code.";

            ClearTextboxes(pnlPasswordReset);
        }

        // use to enable controls after verification code is sent
        private void SetUpPasswordResetPanel()
        {
            txtPRVerificationCode.Enabled = true;
            txtPRNewPassword.Enabled = true;
            txtPRRetypePassword.Enabled = true;

            btnPRConfirm.Enabled = true;
            btnPRConfirm.CssClass = btnPRConfirm.CssClass.Replace("btn-secondary", "btn-primary");

            btnPRSendCode.Text = "Resend Code";

            lblPRError.Text = "";
            lblPRInstructions.Text = "Check your email for code, then enter a new password.";
        }

        // clears all textboxes in given panel
        private void ClearTextboxes(Panel pnl)
        {
            pnl.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Text = "");
        }
    }
}