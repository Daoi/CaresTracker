﻿using CaresTracker.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;
using CaresTracker.DataAccess.DataAccessors.EventTypeAccessors;

namespace CaresTracker
{
    public partial class EventCreator : System.Web.UI.Page
    {
        DataTable CHWDataSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CHWDataSet = new GetAllCHW().RunCommand();
                CHWDataSet.Columns.Add("FullName", typeof(string), "UserFirstName+' '+UserLastName");
                ViewState["CHWDataSet"] = CHWDataSet;

                cblUsers.DataSource = CHWDataSet;
                cblUsers.DataTextField = "FullName";
                cblUsers.DataValueField = "UserID";
                cblUsers.DataBind();

                DataTable dtEventTypes = new GetAllEventTypes().ExecuteCommand()
                    .AsEnumerable()
                    .Where(row => Convert.ToSByte(row["EventTypeIsEnabled"]) == 1)
                    .CopyToDataTable();
                ddlEventType.DataSource = dtEventTypes;
                ddlEventType.DataTextField = "EventTypeName";
                ddlEventType.DataValueField = "EventTypeID";
                ddlEventType.DataBind();
            }
            else
            {
                CHWDataSet = (DataTable)ViewState["CHWDataSet"];
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataModels.Event newEvent = new DataModels.Event();
            newEvent.EventName = txtEventName.Text;
            newEvent.EventLocation = txtEventLocation.Text;
            newEvent.EventDate = txtEventDate.Text;
            newEvent.EventStartTime = txtEventTimeStart.Text;
            newEvent.EventEndTime = txtEventTimeEnd.Text;
            newEvent.Hosts = new List<CARESUser>();
            foreach(ListItem item in cblUsers.Items)
            {
                if (item.Selected)
                {
                    int userid = int.Parse(item.Value);
                    newEvent.Hosts.Add(new CARESUser(CHWDataSet
                        .AsEnumerable()
                        .Where(row => int.Parse(row["UserID"].ToString()) == userid)
                        .First()));
                }
            }
            newEvent.Attendees = new List<Resident>();
            newEvent.MainHostID = int.Parse(ddlMainHost.SelectedValue);
            newEvent.EventTypeID = int.Parse(ddlEventType.SelectedValue);
            newEvent.EventTypeName = ddlEventType.SelectedItem.Text;
            newEvent.EventDescription = txtDescription.InnerText;

            AddEvent add = new AddEvent(newEvent);
            int res = add.ExecuteCommand();
            if (res == 1)
            {
                Session["Event"] = newEvent;
                Response.Redirect("Event.aspx");
            }
        }

        protected void cblUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CARESUser> hosts = new List<CARESUser>();
            foreach(ListItem item in cblUsers.Items)
            {
                if (item.Selected)
                {
                    int userid = int.Parse(item.Value);
                    hosts.Add(new CARESUser(CHWDataSet
                        .AsEnumerable()
                        .Where(row => int.Parse(row["UserID"].ToString()) == userid)
                        .First()));
                }
            }
            ddlMainHost.DataSource = hosts;
            ddlMainHost.DataTextField = "FullName";
            ddlMainHost.DataValueField = "UserID";
            ddlMainHost.DataBind();
        }
    }
}