using System;
using System.Data;
using CapstoneUI.Utilities;
using CapstoneUI.DataModels;
using CapstoneUI.DataAccess.DataAccessors;

namespace CapstoneUI
{
    public partial class CHWManagement : System.Web.UI.Page
    {
        AWSCognitoManager man;
        CARESUser worker;
        protected void Page_Load(object sender, EventArgs e)
        {
            man = Session["CognitoManager"] as AWSCognitoManager;
            worker = Session["Worker"] as CARESUser; // stored from CHWList or CreateCHW

            if (!IsPostBack)
            {
                // get stats
                GetWorkerStats accessor = new GetWorkerStats();
                DataRow row = accessor.RunCommand(worker.Username).Rows[0];
                lblTotalInteractions.Text = row["TotalInteractions"].ToString();
                lblWeekInteractions.Text = row["InteractionsThisWeek"].ToString();

                // get all possible regions and supervisors
                // set region and supervisor from worker props
            }
        }

        // send user to InteractionList which will be pre-filtered for this worker
        protected void btnViewInteractions_Click(object sender, EventArgs e)
        {
            Server.Transfer("InteractionList.aspx");
        }

        // send user back to homepage
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
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
    }
}
