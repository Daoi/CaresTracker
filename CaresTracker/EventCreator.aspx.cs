using CaresTracker.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;
using CaresTracker.Utilities;
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
            if (ValidateFields())
            {
                DataModels.Event newEvent = new DataModels.Event();
                newEvent.EventName = txtEventName.Text;
                newEvent.EventLocation = txtEventLocation.Text;
                newEvent.EventDate = txtEventDate.Text;
                newEvent.EventStartTime = txtEventTimeStart.Text;
                newEvent.EventEndTime = txtEventTimeEnd.Text;
                newEvent.Hosts = new List<CARESUser>();
                foreach (ListItem item in cblUsers.Items)
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

        public bool ValidateFields()
        {
            if (Validation.IsEmpty(txtEventName.Text) || Validation.IsEmpty(txtEventLocation.Text) || Validation.IsEmpty(txtEventDate.Text) ||
            Validation.IsEmpty(txtEventTimeStart.Text) || Validation.IsEmpty(txtEventTimeEnd.Text) || Validation.IsEmpty(txtDescription.InnerText))
            {
                lblError.Visible = true;
                lblError.Text = "Fill out all fields";
                return false;
            }

            if (!Validation.IsAlphanumeric(txtEventName.Text) || !Validation.IsAlphanumeric(txtEventLocation.Text))
            {
                lblError.Visible = true;
                lblError.Text = "Can only accept alphanumeric characters(a-z, 0-9) for event name and location";
                return false;
            }

            DateTime startTime = DateTime.Parse(txtEventTimeStart.Text);
            DateTime endTime = DateTime.Parse(txtEventTimeEnd.Text);
            if(DateTime.Compare(endTime, startTime) < 0 || TimeSpan.Compare(startTime.TimeOfDay, endTime.TimeOfDay) == 0)
            {
                lblError.Visible = true;
                lblError.Text = "Make sure that start and end time are correct";
                return false;
            }

            bool oneChecked = false;
            foreach(ListItem item in cblUsers.Items)
            {
                if (item.Selected)
                {
                    oneChecked = true;
                    break;
                }
            }

            if(oneChecked == false)
            {
                lblError.Visible = true;
                lblError.Text = "Please select at least one worker to host the event";
                return false;
            }

            lblError.Visible = false;
            return true;
        }

    }
}