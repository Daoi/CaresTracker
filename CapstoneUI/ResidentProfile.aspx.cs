using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;
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
            tbRegionId.Text = currentRes.Home.RegionID.ToString();
            //# of occupants still needed?
            //Resident Stuff
            tbFirstName.Text = currentRes.FirstName;
            tbLastName.Text = currentRes.LastName;
            tbDoB.Text = currentRes.DateOfBirth;
            tbPhone.Text = currentRes.ResidentPhoneNumber;
            tbEmail.Text = currentRes.ResidentEmail;
            rblGender.SelectedValue = currentRes.Gender;
            //Family size still needed?
            ddlRace.SelectedValue = currentRes.Race;


            interactions = new GetAllInteractionsByResidentAttributes().RunCommand(currentRes.FirstName, currentRes.LastName, currentRes.DateOfBirth);

            //Total rows returned = recorded interactions
            lblRecordedInteractions.Text = $"Interactions recorded: {interactions.Rows.Count}";
           
            //Get all the rows that meet the requirements of needing a follow up and count them;
            int followUpsRequired = interactions.Rows.Cast<DataRow>()
                .Where(r => r.Field<bool>("RequiresFollowUp") == true 
                && string.IsNullOrEmpty(r.Field<string>("FollowUpCompleted")))
                .ToList().Count;

            lblRequestedServices.Text = $"Follow ups requested: {followUpsRequired}";


        }

        protected void btnReviewPastInteractions_Click(object sender, EventArgs e)
        {
            Server.Transfer("AdminInteractionList.aspx");
        }

        protected void btnCreateNewInteraction_Click(object sender, EventArgs e)
        {
            Server.Transfer("ResidentInteractionForm.aspx");
        }
    }
}