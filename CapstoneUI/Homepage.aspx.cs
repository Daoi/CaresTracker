using System;
using System.Collections.Generic;
using CapstoneUI.Utilities;
using CapstoneUI.DataAccess;
using System.Data;
using CapstoneUI.DataAccess.DataAccessors;
using System.Web.UI.WebControls;
using CapstoneUI.DataModels;

namespace CapstoneUI
{
    public partial class h : System.Web.UI.Page
    {
        CARESUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["User"] as CARESUser;

            lblUserInfo.Text = $"Welcome, {user.UserFirstName}";
            lblUserInfo.Visible = true;
            lblUserInfo.Enabled = true;

            if (user.UserType == "C")
            {
                divCreateCHW.Visible = false;
            }

            InitializeFollowUps();
        }

        protected void btnCreateResidentProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateResident.aspx");
        }

        protected void btnReviewInteractions_Click(object sender, EventArgs e)
        {
            Response.Redirect("InteractionList.aspx");
        }

        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventList.aspx");
        }

        protected void btnCHWCreateAccount_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateCHW.aspx");
        }

        protected void btnCreateEvent_Click1(object sender, EventArgs e)
        {
            Response.Redirect("EventCreator.aspx");
        }

        protected void btnResidentLookUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResidentLookUp.aspx");
        }

        public void InitializeFollowUps()
        {
            AWSCognitoManager man = (AWSCognitoManager)Session["CognitoManager"];

            //Probably want to eventually add Date filtering, e.g. only show completed interactions from the past month or something

            DataTable uncompleted = new GetUncompletedFollowUps().RunCommand(man.Username);

            if (uncompleted.Rows.Count == 0)
            {
                lblOutstandingMsg.Text = "You currently have no residents who require a follow up.";
            }
            else
            {
                gvOutstandingFollowUps.DataBound += (object o, EventArgs ev) =>
                {
                    gvOutstandingFollowUps.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                gvOutstandingFollowUps.DataSource = uncompleted;
                gvOutstandingFollowUps.DataBind();
            }

            DataTable completed = new GetCompletedFollowUps().RunCommand(man.Username);

            if (completed.Rows.Count == 0)
            {
                lblCompletedMsg.Text = "You have no completed follow ups.";
            }
            else
            {
                gvCompletedFollowUps.DataBound += (object o, EventArgs ev) =>
                {
                    gvCompletedFollowUps.HeaderRow.TableSection = TableRowSection.TableHeader;
                };
                gvCompletedFollowUps.DataSource = completed;
                gvCompletedFollowUps.DataBind();
            }
        }
    }
}
