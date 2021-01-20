using System;


namespace CapstoneUI
{
    public partial class ResidentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResidentInteractions_Click(object sender, EventArgs e)
        {
            Server.Transfer("CHWInteractionList.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }
    }
}