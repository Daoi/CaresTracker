using System;
using CaresTracker.Utilities;
using System.Data;
using System.Web.UI.WebControls;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;
using CaresTracker.DataAccess.DataAccessors.InteractionAccessors.FollowUps;

namespace CaresTracker
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
            else if (user.UserType == "A")
            {
                divResidentList.Visible = false;
            }

            if (!IsPostBack)
            {
                InitializeEvents();
                InitializeFollowUps();
            }
        }



        public void InitializeEvents()
        {
            DataTable dt = new GetUpcomingEvents().ExecuteCommand();
            if (dt.Rows.Count == 0)
            {
                lblEventMsg.Text = "No upcoming events to display.";
            }
            else
            {
                gvEvents.DataSource = dt;
                BindGridview(gvEvents);
                ViewState["EventDT"] = dt;
            }
        }

        public void InitializeFollowUps()
        {
            AWSCognitoManager man = (AWSCognitoManager)Session["CognitoManager"];

            //Probably want to eventually add Date filtering, e.g. only show completed interactions from the past month or something

            DataTable uncompleted = new GetUncompletedFollowUps().RunCommand(man.Username);
            ViewState["Uncompleted"] = uncompleted;
            if (uncompleted.Rows.Count == 0)
            {
                lblOutstandingMsg.Text = "You currently have no residents who require a follow up.";
            }
            else
            {
                gvOutstandingFollowUps.DataSource = uncompleted;
                BindGridview(gvOutstandingFollowUps);
            }

            DataTable completed = new GetCompletedFollowUps().RunCommand(man.Username);
            ViewState["Completed"] = completed;
            if (completed.Rows.Count == 0)
            {
                lblCompletedMsg.Text = "You have no completed follow ups.";
            }
            else
            {
                gvCompletedFollowUps.DataSource = completed;
                BindGridview(gvCompletedFollowUps);
            }
        }

        public void BindGridview(GridView gv)
        {
            gv.DataBound += (object o, EventArgs ev) =>
            {
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            };
            gv.DataBind();
        }

        protected void lnkToEvent_Click(object sender, EventArgs e)
        {
            if(ViewState["EventDT"] != null)
            {
                DataTable events = (DataTable)ViewState["EventDT"];
                LinkButton lnk = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
                DataRow row = events.Rows[gvr.DataItemIndex];

                DataModels.Event selectedEvent = new DataModels.Event(row);
                Session["Event"] = selectedEvent;
                Response.Redirect("Event.aspx");
            }
            //Error handling?

        }
        
        protected void lnkViewOutstandingInteraction_Click(object sender, EventArgs e)
        {
            if (ViewState["Uncompleted"] != null)
            {
                DataTable outStanding = (DataTable)ViewState["Uncompleted"];
                LinkButton lnk = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
                DataRow row = outStanding.Rows[gvr.DataItemIndex];

                Resident res = new Resident(row);
                Session["Resident"] = res;
                Interaction selectedInteraction = new Interaction(row);
                Session["Interaction"] = selectedInteraction;
                Response.Redirect("ResidentInteractionForm.aspx?from=InteractionList"); //Kind of stupid but oh well

            }
            //Error handling?
        }
        protected void lnkViewCompletedInteraction_Click(object sender, EventArgs e)
        {
            if (ViewState["Completed"] != null)
            {
                DataTable outStanding = (DataTable)ViewState["Completed"];
                LinkButton lnk = (LinkButton)sender;
                GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
                DataRow row = outStanding.Rows[gvr.DataItemIndex];

                Resident res = new Resident(row);
                Session["Resident"] = res;
                Interaction selectedInteraction = new Interaction(row);
                Session["Interaction"] = selectedInteraction;
                Response.Redirect("ResidentInteractionForm.aspx?from=InteractionList");
            }
            //Error handling?
        }

    }
}
