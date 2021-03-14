using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class EventList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<EventInteraction> temp = new List<EventInteraction>();
            EventInteraction eventInteraction = new EventInteraction("The Importance of Masks", "Explaining how masks help control the spread of COVID-19", "Seminar", "Community Center 10", "12/1/2020 5:00PM", "12/1/2020 6:00PM");
            temp.Add(eventInteraction);
            eventInteraction = new EventInteraction("How To: Social Distancing", "Tips on staying connected from afar", "Instructional", "Virtual", "12/2/2020 3:00PM", "12/2/2020 4:00PM");
            temp.Add(eventInteraction);
            for (int i = 0; i < 10; i++)
            {
                EventInteraction tempInteraction = new EventInteraction();
                temp.Add(tempInteraction);
            }

            gvEventList.DataBound += (object o, EventArgs ev) =>
            {
                gvEventList.HeaderRow.TableSection = TableRowSection.TableHeader;
            };

            gvEventList.DataSource = temp;
            gvEventList.DataBind();
        }

        public class EventInteraction
        {
            public string EventName { get; set; }
            public string EventDescription { get; set; }
            public string EventType { get; set; }
            public string EventLocation { get; set; }
            public string EventStartDateTime { get; set; }
            public string EventEndDateTime { get; set; }
            public EventInteraction() { }
            public EventInteraction(string eventname, string eventdescription, string eventtype,
                string eventlocation, String eventstartdatetime, string eventenddatetime)
            {
                EventName = eventname;
                EventDescription = eventdescription;
                EventType = eventtype;
                EventLocation = eventlocation;
                EventStartDateTime = eventstartdatetime;
                EventEndDateTime = eventenddatetime;
            }
        }

        protected void gvEventList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Server.Transfer("Event.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }
    }
}
