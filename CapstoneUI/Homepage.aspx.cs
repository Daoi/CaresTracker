using System;
using System.Collections.Generic;
using CapstoneUI.Utilities;
using CapstoneUI.DataAccess;
using System.Data;
using CapstoneUI.DataAccess.DataAccessors;

namespace CapstoneUI
{
    public partial class h : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"Welcome, {Session["UserName"].ToString()}";
            lblUserInfo.Visible = true;
            lblUserInfo.Enabled = true;
            if ((int)Session["AccountType"] == 0)
            {
                divCreateCHW.Visible = false;
            }

            InitializeFollowUps();
        }

        protected void btnCreateResidentProfile_Click(object sender, EventArgs e)
        {
            Server.Transfer("CreateResident.aspx");
        }

        protected void btnReviewInteractions_Click(object sender, EventArgs e)
        {
            if ((int)Session["AccountType"] == 0)
            {
                Server.Transfer("CHWInteractionList.aspx");
            }
            else
            {
                Server.Transfer("AdminInteractionList.aspx");
            }
        }

        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            Server.Transfer("EventList.aspx");
        }

        protected void btnCHWCreateAccount_Click(object sender, EventArgs e)
        {
            Server.Transfer("CreateCHW.aspx");
        }

        protected void btnCreateEvent_Click1(object sender, EventArgs e)
        {
            Server.Transfer("EventCreator.aspx");
        }

        protected void btnResidentLookUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResidentLookUp.aspx");
        }

        public void InitializeFollowUps()
        {
            AWSCognitoManager man = (AWSCognitoManager)Session["CognitoManager"];
            DataTable uncompleted = new GetUncompletedFollowUps().RunCommand(man.Username);
            if (uncompleted.Rows.Count == 0)
            {
                lblOutstandingMsg.Text = "You currently have no residents whomst require a follow up.";
            }
            DataTable completed = new GetCompletedFollowUps().RunCommand(man.Username);
            if (completed.Rows.Count == 0)
            {
                lblCompletedMsg.Text = "You habe no completed follow ups";
            }

            //Probably want to eventually add Date filtering, e.g. only show completed interactions from the past month or something
            gvCompletedFollowUps.DataSource = completed;
            gvOutstandingFollowUps.DataSource = uncompleted;
            gvCompletedFollowUps.DataBind();
            gvOutstandingFollowUps.DataBind();
        }
    }
}