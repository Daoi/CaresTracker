using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess.DataAccessors.ResidentAccessors;
using CapstoneUI.DataModels;
using CapstoneUI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

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
            else
            {
                Response.Redirect("Homepage.aspx");
            }

            if (!IsPostBack)
            {
                InitializeProfileValues();
                ToggleControls(); //Set page to disabled status
                if (Session["DevelopmentDT"] == null)
                {
                    GetAllDevelopments GAD = new GetAllDevelopments();
                    DataTable developmentDT = GAD.ExecuteCommand();
                    // Bind to drop down list
                    ddlHousingDevelopment.DataSource = developmentDT;
                    ddlHousingDevelopment.DataValueField = "DevelopmentID";
                    ddlHousingDevelopment.DataTextField = "DevelopmentName";
                    ddlHousingDevelopment.DataBind();

                }
                else
                {
                    DataTable developmentDT = (DataTable)Session["DevelopmentDT"];
                    ddlHousingDevelopment.DataSource = developmentDT;
                    ddlHousingDevelopment.DataValueField = "DevelopmentID";
                    ddlHousingDevelopment.DataTextField = "DevelopmentName";
                    ddlHousingDevelopment.DataBind();

                }
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
            ddlHousingDevelopment.SelectedValue = currentRes.Home.DevelopmentID.ToString();
            tbUnitNumber.Text = currentRes.Home.UnitNumber;
            ddlRegion.SelectedValue = currentRes.Home.RegionID.ToString();
            tbZipcode.Text = currentRes.Home.ZipCode;
            //# of occupants still needed?
            //Resident Stuff
            tbFirstName.Text = currentRes.ResidentFirstName;
            tbLastName.Text = currentRes.ResidentLastName;
            tbDoB.Text = TextModeDateFormatter.Format(currentRes.DateOfBirth);
            tbPhone.Text = currentRes.ResidentPhoneNumber.Insert(3, "-").Insert(7, "-");
            tbEmail.Text = currentRes.ResidentEmail;
            rblGender.SelectedValue = currentRes.Gender;
            //Family size still needed?
            ddlRace.SelectedValue = currentRes.Race;
            ddlLanguage.SelectedValue = currentRes.PreferredLanguage;
            //Vaccine
            ddlVaccinePhases.SelectedIndex = currentRes.VaccineEligibility != null ? (int)currentRes.VaccineEligibility : 4;
            bool? flag = currentRes.VaccineInterest;
            if(flag != null && (bool)flag)
            {
                if (string.IsNullOrEmpty(currentRes.VaccineAppointmentDate))
                {
                    ddlVaccineStatus.SelectedIndex = 2; //Interested, not scheduled
                }
                else if(DateTime.Parse(currentRes.VaccineAppointmentDate) > DateTime.Now )
                {
                    ddlVaccineStatus.SelectedIndex = 3; //Appointment scheduled and hasn't happened yet.
                    tbAppointmentDate.Text = currentRes.VaccineAppointmentDate.ToString();
                }
                else //Should double check with vaccinated status
                {
                    ddlVaccineStatus.SelectedIndex = 4; //Vaccinated
                    tbAppointmentDate.Text = currentRes.VaccineAppointmentDate.ToString();
                }
            }
            else if(flag == null)
            {
                ddlVaccineStatus.SelectedIndex = 0; //No information
            }
            else
            {
                ddlVaccineStatus.SelectedIndex = 1; //Not interested
            }


            interactions = new GetAllInteractionsByResidentAttributes().RunCommand(currentRes.ResidentFirstName, currentRes.ResidentLastName, currentRes.DateOfBirth);
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

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            ToggleControls(); //Enable controls for editing
            btnSaveEdits.Visible = true;
            btnEditProfile.Visible = false;
        }

        //Toggle all non-button controls enabled status 
        private void ToggleControls(Control control = null)
        {
            List<Control> controls = control != null ? control.Controls.OfType<Control>().ToList() : Page.Controls.OfType<Control>().ToList();

            foreach (Control c in controls)
            {
                Type type = c.GetType();
                PropertyInfo prop = type.GetProperty("Enabled");

                if (prop != null && type != typeof(Button) && !c.ID.Contains("lnk")) //Dont disable links or buttons
                {
                    bool flag = (bool)prop.GetValue(c);
                    prop.SetValue(c, !flag, null);
                }
                //Repeat on child controls(E.g. checkbox list)
                if (c.Controls.Count > 0)
                {
                    ToggleControls(c);
                }
            }
        }

        protected void btnSaveEdits_Click(object sender, EventArgs e)
        {

            Resident res = new Resident();
            //Resident Info
            res.ResidentID = currentRes.ResidentID;
            res.ResidentPhoneNumber = tbPhone.Text.Replace("-", "");
            res.ResidentEmail = tbEmail.Text;
            res.ResidentFirstName = tbFirstName.Text;
            res.ResidentLastName = tbLastName.Text;
            res.DateOfBirth = tbDoB.Text;
            res.Gender = rblGender.SelectedValue;
            res.Race = ddlRace.SelectedValue;
            res.RelationshipToHoH = ddlHoH.SelectedValue;
            res.VaccineEligibility = int.Parse(ddlVaccinePhases.SelectedValue);
            res.PreferredLanguage = ddlLanguage.SelectedItem.Text;
            int interest = ddlVaccineStatus.SelectedIndex;
            if (interest == 0)
            {
                res.VaccineInterest = null; //No info
            }
            else if(interest == 1)
            {
                res.VaccineInterest = false; //Not interested
            }
            else
            {
                res.VaccineInterest = true; //Interested or vaccinated
            }
            res.VaccineAppointmentDate = tbAppointmentDate.Text;
            //Housing Info
            res.Home = new House();
                
            res.Home.Address = tbAddress.Text;
            res.Home.UnitNumber = tbUnitNumber.Text;
            res.Home.RegionName = ddlRegion.SelectedItem.Text;
            res.Home.RegionID = int.Parse(ddlRegion.SelectedValue);
            res.Home.DevelopmentID = int.Parse(ddlHousingDevelopment.SelectedValue);
            res.Home.ZipCode = tbZipcode.Text;

            if (res.Home.DevelopmentID != -1) //If not HCV
            {
                res.HousingDevelopment = new HousingDevelopment();
                res.HousingDevelopment.DevelopmentID = res.Home.DevelopmentID;
                res.HousingDevelopment.DevelopmentName = ddlHousingDevelopment.SelectedItem.Text;
            }


            AddHouse AH = new AddHouse(res.Home);
            object AddHouseResult = AH.ExecuteCommand();

            // Update Resident's HouseID to match new House
            UpdateHouseID UHI = new UpdateHouseID();
            UHI.ExecuteCommand(res.ResidentID, Convert.ToInt32(AddHouseResult));

            Session["Resident"] = res;
            //Update the resident values
            try
            {
                new UpdateResident(res).ExecuteCommand();
                ToggleControls();
                btnEditProfile.Visible = true;
                btnSaveEdits.Visible = false;
            }
            catch(Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = $"Failed to update profile: {ex.Message}";
            }
            
        }
    }
}