using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class ResidentInteractionForm : System.Web.UI.Page
    {
        Dictionary<string, HtmlGenericControl> links;

        protected void Page_Load(object sender, EventArgs e)
        {
            links = new Dictionary<string, HtmlGenericControl> { {"residentInfo", residentInfoForm }, { "residentHealth", residentHealthForm },
                                                                 { "meetingInfo", meetingInfoForm }, { "otherInfo", otherForm }, { "services", servicesForm },
                                                                  {"housingInfo", housingInfoForm }
                                                                };
            if (!IsPostBack)
            {
                links.Keys.ToList().Where(s => !s.Equals("residentInfo")).ToList().ForEach(s => links[s].Visible = false);
            }
        }

        protected void formNav_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            links[link.ID].Visible = true;
            links.Keys.ToList().Where(s => !s.Equals(link.ID)).ToList().ForEach(s => links[s].Visible = false);

        }

        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            return;
        }

        protected void lnkBtnHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/Homepage.aspx");
        }
    }
}