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
            lblEventType.Text = theEvent.EventType;
            lblLocation.Text = theEvent.EventLocation;
            lblStartTime.Text = theEvent.EventStartTime;
            lblEndTime.Text = theEvent.EventEndTime;

            rptHealthWorkers.DataSource = theEvent.Hosts;
            rptHealthWorkers.DataBind();

            rptResidents.DataSource = theEvent.Attendees;
            rptResidents.DataBind();
            
            lblDescription.Text = theEvent.EventDescription;
        }
    }
}