﻿using System;
using System.Collections.Generic;
using CapstoneUI.Utilities;
using CapstoneUI.DataAccess;
using System.Data;
using CapstoneUI.DataAccess.DataAccessors;
using System.Web.UI.WebControls;
using CapstoneUI.DataModels;
using CapstoneUI.DataAccess.DataAccessors.EventAccessors;
using System.Linq;

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
            if (!IsPostBack)
            {
                InitializeEvents();
                InitializeFollowUps();
            }
        }



        public void InitializeEvents()
        {
            DataTable dt = new GetUpcomingEvents().ExecuteCommand();
            BindHeader(gvEvents);
            gvEvents.DataSource = dt;
            gvEvents.DataBind();
            ViewState["EventDT"] = dt;
            upEvents.Update();
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
                BindHeader(gvOutstandingFollowUps);
                gvOutstandingFollowUps.DataSource = uncompleted;
                gvOutstandingFollowUps.DataBind();
            }

            DataTable completed = new GetCompletedFollowUps().RunCommand(man.Username);
            ViewState["Completed"] = completed;
            if (completed.Rows.Count == 0)
            {
                lblCompletedMsg.Text = "You have no completed follow ups.";
            }
            else
            {
                BindHeader(gvCompletedFollowUps);
                gvCompletedFollowUps.DataSource = completed;
                gvCompletedFollowUps.DataBind();
            }
        }

        public void BindHeader(GridView gv)
        {
            gv.DataBound += (object o, EventArgs ev) =>
            {
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            };
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
