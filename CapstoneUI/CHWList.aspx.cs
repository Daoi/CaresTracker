using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class CHWList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public class CHW
        {
            public string FirsttName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
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

        protected void gvCHWList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Server.Transfer("CHWManagement.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }
    }
}