using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class EventCreator : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<HtmlGenericControl> eventDivs = new List<HtmlGenericControl>() { divResourceTableEvent, divHealthEducationEvent,
                                                                                  divFluShotEvent, divOnlineEvent };
            DropDownList ddl = (DropDownList)sender;
            string selectedId = ddl.SelectedValue;
            if (ddl.SelectedIndex == 0)
            {
                return; // They haven't selected an event Type so don't change anything/show error message/whatever
            }

            eventDivs.ForEach(ed => ed.Visible = false); //Turn off all divs
            (eventDivs.Find(ed => ed.ID.Equals(selectedId)) as HtmlGenericControl).Visible = true; //Turn selected div on
            
            upEvents.Update();//Update the page without doing full postback using update panel
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }
    }
}