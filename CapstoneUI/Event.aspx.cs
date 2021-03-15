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
    }
}