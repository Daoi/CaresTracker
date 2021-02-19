using System;
using System.Collections.Generic;
using CapstoneUI.Utilities;

namespace CapstoneUI
{
    public partial class h : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"Welcome, {Session["UserName"].ToString()}";
            lblUserInfo.Visible = true;
            lblUserInfo.Enabled = true;
            if ((int)Session["AccountType"] == 0)
            {
                divCreateCHW.Visible = false;
            }
            //Temporary Follow Up Testing
            FollowUp fol = new FollowUp("2/10/2021", "2/13/2021", "Vaccine Schedule", "John Doe");
            FollowUp folb = new FollowUp("2/9/2021", "Not Completed", "Vaccine Schedule", "Bob John");
            List<FollowUp> temp = new List<FollowUp>();
            temp.Add(fol);
            temp.Add(folb);
            gvCompletedFollowUps.DataSource = temp;
            gvOutstandingFollowUps.DataSource = temp;
            gvCompletedFollowUps.DataBind();
            gvOutstandingFollowUps.DataBind();


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
            Response.Redirect("ResidentLookUp.aspx");
        }


        public class FollowUp
        {
            public string DateRequested { get; set; }
            public string DateCompleted { get; set; }
            public string Service { get; set; }
            public string Resident { get; set; }

            public FollowUp(string a, string b, string c, string d)
            {
                DateRequested = a;
                DateCompleted = b;
                Service = c;
                Resident = d;
            }
        }
    }
}