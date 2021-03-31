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

namespace CaresTracker
{
    public partial class EventCreator : System.Web.UI.Page
    {
        DataTable CHWDataSet;
        List<CARESUser> UserList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserList = new List<CARESUser>();
                CHWDataSet = new GetAllCHW().RunCommand();
                Session["CHWDataSet"] = CHWDataSet;
                foreach (DataRow row in CHWDataSet.Rows)
                {
                    CARESUser temp = new CARESUser(row.ItemArray);
                    UserList.Add(temp);
                }
                Session["CHWUserList"] = UserList;

                cblUsers.DataSource = UserList;
                cblUsers.DataTextField = "FullName";
                cblUsers.DataValueField = "UserID";
                cblUsers.DataBind();
            }
            else
            {
                UserList = (List<CARESUser>)Session["CHWUserList"];
            }
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
                        int index = cblUsers.Items.IndexOf(item);
                        newEvent.Hosts.Add(UserList.ElementAt(index));
                    }
                }
                newEvent.MainHostID = int.Parse(ddlMainHost.SelectedValue);
                newEvent.EventType = ddlEventType.SelectedItem.Text;
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
                    int index = cblUsers.Items.IndexOf(item);
                    hosts.Add(UserList.ElementAt(index));
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
                lblError.Text = "Fill out all fields";
                return false;
            }

            DateTime startTime = DateTime.Parse(txtEventTimeStart.Text);
            DateTime endTime = DateTime.Parse(txtEventTimeEnd.Text);
            if(TimeSpan.Compare(startTime.TimeOfDay, endTime.TimeOfDay) == -1 || TimeSpan.Compare(startTime.TimeOfDay, endTime.TimeOfDay) == 0)
            {
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
                lblError.Text = "Please select at least one worker to host the event";
                return false;
            }

            if(ddlEventType.SelectedValue == "Select Event Type")
            {
                lblError.Text = "Please select an event type";
                return false;
            }

            return true;
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }
    }
}