using CapstoneUI.DataModels;
using CapstoneUI.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        CARESUser user;
        AWSCognitoManager man;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                user = Session["User"] as CARESUser;
                man = Session["CognitoManager"] as AWSCognitoManager;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (user.UserType == "C")
            {
                lnkImport.Visible = true;
                lnkImport.Enabled = true;
                lnkInteractionForm.Enabled = true;
                lnkInteractionForm.Visible = true;

            }
            else if (user.UserType == "A")
            {
                lnkImport.Visible = true;
                lnkManagement.Visible = true;
                lnkData.Visible = true;
                lnkImport.Enabled = true;
                lnkManagement.Enabled = true;
                lnkData.Enabled = true;
                lnkInteractionForm.Enabled = true;
                lnkInteractionForm.Visible = true;

            }

            lnkBtnLogout.Visible = true;
            lnkBtnLogout.Enabled = true;
        }

        protected void lnkBtnLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        // fires every tmrSessionTimeout.Interval milliseconds to check session token status
        protected void tmrSessionTimeout_Tick(object sender, EventArgs e)
        {
            // set to tick every 10 mins after initial page load
            if (tmrSessionTimeout.Interval == 500) { tmrSessionTimeout.Interval = 600000; }

            TimeSpan timeLeft = man.TokenExpirationTime - DateTime.Now;

            // less than 10 mins left and modal has not been popped
            if (lblTimeLeft.Text == "" && timeLeft < new TimeSpan(0, 10, 0))
            {
                // pop modal
                string showModalCall = "$('#timeoutModal').modal({show: true, keyboard: false, backdrop: 'static'});";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "showModal", showModalCall, true);
                txtModalUsername.Text = user.Username;

                // set tries
                Session["ReauthTries"] = 3;
                lblTriesLeft.Text = "3";

                tmrSessionTimeout.Interval = 60000; // update every min
                lblTimeLeft.Text = $"{timeLeft.Minutes} minutes";
            }
            else if (timeLeft.TotalMilliseconds < 0) // time expired
            {
                Logout();
            }
            else if (timeLeft < new TimeSpan(0, 2, 0)) // less than 2 mins left
            {
                tmrSessionTimeout.Interval = 1000; // update every second
                string secs = timeLeft.Seconds >= 10 ? timeLeft.Seconds.ToString() : "0" + timeLeft.Seconds;
                lblTimeLeft.Text = $"{timeLeft.Minutes}:{secs}";
            }
            else if (timeLeft < new TimeSpan(0, 10, 0)) // less than 10 mins left
            {
                lblTimeLeft.Text = $"{timeLeft.Minutes} minutes";
            }
        }

        // reauthenticate user
        protected async void btnModalLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // validation
                if (pnlModalControls.Controls[0].Controls.OfType<TextBox>().ToList().Any(tb => Validation.IsEmpty(tb.Text)))
                {
                    lblModalError.Text = "Fill out all fields.";
                    return;
                }

                // make sure user is reauthenticating with the same account
                if (txtModalUsername.Text.ToLower() != user.Username.ToLower())
                {
                    lblModalError.Text = "Must reauthenticate with the current account.";
                    return;
                }

                // call login to refresh access tokens
                await man.SignInAsync(txtModalUsername.Text, txtModalPassword.Text);

                // reset modal
                tmrSessionTimeout.Interval = 600000; // set back to ten minutes
                lblTimeLeft.Text = "";
                lblModalError.Text = "";
                pnlModalControls.Controls[0].Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Text = "");
                string hideModalCall = "$('#timeoutModal').modal('hide');";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "hideModal", hideModalCall, true);
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "showModal", "", true);
            }
            catch (Exception ex)
            {
                lblModalError.Text = ex.Message;

                int triesLeft = int.Parse(Session["ReauthTries"].ToString()) - 1;

                if (triesLeft < 1) { Logout(); }
                Session["ReauthTries"] = triesLeft;
                lblTriesLeft.Text = triesLeft.ToString();
            }
        }

        private void Logout()
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}
