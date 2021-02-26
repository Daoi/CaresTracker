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

            if(Session["NewResident"] != null)
            {
                currentRes = (Resident)Session["NewResident"];
            }

            if (!IsPostBack && Session["NewResident"] != null)
            {
                InitializeProfileValues();
            }
        }

        protected void btnResidentInteractions_Click(object sender, EventArgs e)
        {
            Server.Transfer("CHWInteractionList.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }

        public void InitializeProfileValues()
        {
            //Housing Stuff
            tbAddress.Text = currentRes.Home.Address;
            tbDevelopment.Text = currentRes.HousingDevelopment.DevelopmentName;
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
            lblRecordedInteractions.Text = $"Interactions recorded: {interactions.Rows.Count}";
            //Get all the rows that meet the requirements of needing a follow up and count them;
            int followUpsRequired = interactions.Rows.Cast<DataRow>()
                .Where(r => r.Field<bool>("RequiresFollowUp") == true 
                && string.IsNullOrEmpty(r.Field<string>("FollowUpCompleted")))
                .ToList().Count;

            lblRequestedServices.Text = $"Follow ups requested: {followUpsRequired}";


        }

    }
}