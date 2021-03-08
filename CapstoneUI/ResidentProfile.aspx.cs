using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;
using CapstoneUI.Utilities;
using System;
using System.Data;
using System.Linq;

namespace CapstoneUI
{
    public partial class ResidentProfile : System.Web.UI.Page
    {
        Resident currentRes;
        DataTable interactions;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["Resident"] != null)
            {
                currentRes = (Resident)Session["Resident"];
            }

            if (!IsPostBack && Session["Resident"] != null)
            {
                InitializeProfileValues();
            }
        }


        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        public void InitializeProfileValues()
        {
            //Housing Stuff
            tbAddress.Text = currentRes.Home.Address;
            tbDevelopment.Text = currentRes.HousingDevelopment != null ? currentRes.HousingDevelopment.DevelopmentName : "Resident does not live in a development.";

            tbRegionName.Text = currentRes.Home.RegionName == null ? "Region not implemented yet" : currentRes.Home.RegionName.ToString();
            //# of occupants still needed?
            //Resident Stuff
            tbFirstName.Text = currentRes.FirstName;
            tbLastName.Text = currentRes.LastName;
            tbDoB.Text = TextModeDateFormatter.Format(currentRes.DateOfBirth);
            tbPhone.Text = currentRes.ResidentPhoneNumber;
            tbEmail.Text = currentRes.ResidentEmail;
            rblGender.SelectedValue = currentRes.Gender;
            //Family size still needed?
            ddlRace.SelectedValue = currentRes.Race;


            interactions = new GetAllInteractionsByResidentAttributes().RunCommand(currentRes.FirstName, currentRes.LastName, currentRes.DateOfBirth);
            if (interactions.Rows.Count > 0)
            {
                //Total rows returned = recorded interactions
                lblRecordedInteractions.Text = $"Interactions recorded: {interactions.Rows.Count}";

                //Get all the rows that meet the requirements of needing a follow up and count them;
                int followUpsRequired = interactions.Rows.Cast<DataRow>()
                    .Where(r => (bool)r["RequiresFollowUp"]
                    && string.IsNullOrEmpty(r["FollowUpCompleted"].ToString()))
                    .ToList().Count;


                lblRequestedServices.Text = $"Follow ups requested: {followUpsRequired}";
            }
            else
            {
                lblRequestedServices.Text = $"Follow ups requested: N/A";
                lblRecordedInteractions.Text = $"Interactions recorded: {interactions.Rows.Count}";
            }

        }

        protected void btnReviewPastInteractions_Click(object sender, EventArgs e)
        {
            Server.Transfer("InteractionList.aspx");
        }

        protected void btnCreateNewInteraction_Click(object sender, EventArgs e)
        {
            Server.Transfer("ResidentInteractionForm.aspx");
        }
    }
}