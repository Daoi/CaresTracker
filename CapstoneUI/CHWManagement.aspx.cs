using System;
using System.Data;
using CapstoneUI.Utilities;
using CapstoneUI.DataModels;
using CapstoneUI.DataAccess.DataAccessors;
using System.Collections.Generic;

namespace CapstoneUI
{
    public partial class CHWManagement : System.Web.UI.Page
    {
        AWSCognitoManager man;
        CARESUser worker;
        Dictionary<string, string> userTypes = new Dictionary<string, string>()
        {
            {"T", "Temple Admin" },
            {"A", "Partner Admin" },
            {"S", "Supervisor" },
            {"C", "Community Health Worker" }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            man = Session["CognitoManager"] as AWSCognitoManager;
            worker = Session["Worker"] as CARESUser; // stored from CHWList or CreateCHW

            if (!IsPostBack)
            {
                if (worker.UserStatus == "Inactive")
                {
                    SetButtonActivate();
                }

                SetUpPage();
            }
        }

        // send user to InteractionList which will be pre-filtered for this worker
        protected void btnViewInteractions_Click(object sender, EventArgs e)
        {
            Server.Transfer("InteractionList.aspx");
        }

        // send user back to homepage
        protected void lnkWorkerList_Click(object sender, EventArgs e)
        {
            Response.Redirect("CHWList.aspx");
        }

        // resends the initial verification link used to finish sign up process
        protected async void btnResendSignUpVerification_Click(object sender, EventArgs e)
        {
            try
            {
                await man.ResendTemporaryPassword(worker.Username);
                lblAWSError.Text = "";
            }
            catch (Exception ex)
            {
                lblAWSError.Text = ex.Message;
            }
        }

        // deactivates/activates a user's account to manage sign in authorization
        protected async void btnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnDeactivate.Text == "Deactivate")
                {
                    await man.DisableUserAsync(worker.Username);
                    UpdateUserStatus updater = new UpdateUserStatus();
                    updater.RunCommand(worker.Username, "Inactive");

                    SetButtonActivate();
                }
                else
                {
                    await man.EnableUserAsync(worker.Username);
                    UpdateUserStatus updater = new UpdateUserStatus();
                    updater.RunCommand(worker.Username, "Active");

                    SetButtonDeactivate();
                }

                lblAWSError.Text = "";
            }
            catch (Exception ex)
            {
                lblAWSError.Text = ex.Message;
            }
        }

        // changes button styling when account deactivated
        private void SetButtonActivate()
        {
            btnDeactivate.Text = "Activate";
            btnDeactivate.CssClass = btnDeactivate.CssClass.Replace("btn-danger", "btn-success");
        }

        // changes button styling when account activated
        private void SetButtonDeactivate()
        {
            btnDeactivate.Text = "Deactivate";
            btnDeactivate.CssClass = btnDeactivate.CssClass.Replace("btn-success", "btn-danger");
        }

        // sets data based on selected user
        private void SetUpPage()
        {
            DataRow row = new GetWorkerStats().RunCommand(worker.Username).Rows[0];
            lblTotalInteractions.Text += row["TotalInteractions"].ToString();
            lblWeekInteractions.Text += row["InteractionsThisWeek"].ToString();

            // only show if user has not yet verified account
            divResendLink.Visible = worker.LastLogin == "N/A";

            txtUsername.Text = worker.Username;
            txtFullName.Text = worker.FullName;
            txtPhone.Text = worker.UserPhoneNumber;
            txtEmail.Text = worker.UserEmail;
            txtOrganizationName.Text = worker.OrganizationName;
            txtAccountType.Text = userTypes[worker.UserType];
            txtLastLogin.Text = worker.LastLogin;
        }
    }
}
