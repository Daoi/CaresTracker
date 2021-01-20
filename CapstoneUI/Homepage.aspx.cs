using System;


namespace CapstoneUI
{
    public partial class h : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"Welcome, {Session["UserName"].ToString()}";
            lblUserInfo.Visible = true;
            lblUserInfo.Enabled = true;
            if((int)Session["AccountType"] == 0)
            {
                divCreateCHW.Visible = false;
            }

        }

        protected void btnCreateResidentProfile_Click(object sender, EventArgs e)
        {
            Server.Transfer("CreateResident.aspx");
        }

        protected void btnReviewInteractions_Click(object sender, EventArgs e)
        {
            if ((int)Session["AccountType"] == 0)
            {
                Server.Transfer("CHWInteractionList.aspx");
            }
            else
            {
                Server.Transfer("AdminInteractionList.aspx");
            }
        }

        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            Server.Transfer("EventList.aspx");
        }

        protected void btnCHWCreateAccount_Click(object sender, EventArgs e)
        {
            Server.Transfer("CreateCHW.aspx");
        }

        protected void btnCreateEvent_Click1(object sender, EventArgs e)
        {
            Server.Transfer("EventCreator.aspx");
        }

        protected void btnResidentLookUp_Click(object sender, EventArgs e)
        {
            Server.Transfer("ResidentLookUp.aspx");
        }
    }
}