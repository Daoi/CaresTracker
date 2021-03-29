using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class Event : System.Web.UI.Page
    {
        DataModels.Event theEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Event"] != null)
            {
                theEvent = (DataModels.Event)Session["Event"];
            }

            if(!IsPostBack && Session["Event"] != null)
            {
                FillEventInfo();
            }
        }

        public void FillEventInfo()
        {
            lblEventName.Text = theEvent.EventName;
            txtEventType.Text = theEvent.EventType;
            txtLocation.Text = theEvent.EventLocation;
            txtEventDate.Text = theEvent.EventDate;
            txtStartTime.Text = theEvent.EventStartTime;
            txtEndTime.Text = theEvent.EventEndTime;

            rptHealthWorkers.DataSource = theEvent.Hosts;
            rptHealthWorkers.DataBind();

            rptResidents.DataSource = theEvent.Attendees;
            rptResidents.DataBind();
            
            txtDescription.Text = theEvent.EventDescription;
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void lnkEventList_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventList.aspx");
        }

        private void EnableDisableControls()
        {
            foreach (Control c in form.Controls)
            {
                if (c is TextBox)
                {
                    TextBox temp = c as TextBox;
                    temp.Enabled = !temp.Enabled;
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EnableDisableControls();
            btnEdit.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EnableDisableControls();
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EnableDisableControls();
            FillEventInfo();
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        protected void btnAddResidentAttendees_Click(object sender, EventArgs e)
        {
            Session["Event"] = theEvent;
            Response.Redirect("AddResidentAttendees.aspx");
        }
    }
}