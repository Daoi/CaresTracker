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
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDescription.Text = "Lorem ipsum dolor sit amet, consectetur " +
                "adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex " +
                "ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse " +
                "cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, " +
                "sunt in culpa qui officia deserunt mollit anim id est laborum.";

            List<HealthWorker> healthWorkers = new List<HealthWorker>();
            HealthWorker healthWorker = new HealthWorker("CHW-101", "Alice");
            healthWorkers.Add(healthWorker);
            healthWorker = new HealthWorker("CHW-104", "Bob");
            healthWorkers.Add(healthWorker);

            rptHealthWorkers.DataSource = healthWorkers;
            rptHealthWorkers.DataBind();
        }

        public class HealthWorker
        {
            public string UserID { get; set; }
            public string UserName { get; set; }

            public HealthWorker(string userid, string username)
            {
                UserID = userid;
                UserName = username;
            }
        }
    }
}