using System;


namespace CapstoneUI
{
    public partial class CHWManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // send user to InteractionList which will be pre-filtered for this worker
        protected void btnViewInteractions_Click(object sender, EventArgs e)
        {
            Server.Transfer("InteractionList.aspx");
        }

        // send user back to homepage
        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }
    }
}