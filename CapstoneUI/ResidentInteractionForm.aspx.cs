﻿using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess.DataAccessors.InteractionAccessors;
using CapstoneUI.DataAccess.DataAccessors.ResidentAccessors;
using CapstoneUI.DataModels;
using CapstoneUI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class ResidentInteractionForm : Page
    {
        Dictionary<string, Panel> links;
        Resident res;
        Interaction interaction;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Key = lnkbtnID Value = Associated Panel
            links = new Dictionary<string, Panel> {
                { "residentInfo", pnlResidentInfoForm }, { "residentHealth", pnlResidentHealthForm },
                { "meetingInfo", pnlMeetingInfoForm }, { "otherInfo", pnlOtherForm }, { "services", pnlServicesForm },
                {"housingInfo", pnlHousingInfoForm }, {"vaccineInfo", pnlVaccineForm}
                };

            if (!IsPostBack)
            {
                //Set resident info (first tab) to visible, others to false
                links.Keys.ToList().Where(s => !s.Equals("residentInfo")).ToList().ForEach(s => links[s].Visible = false);
                cblServices.DataSource = new GetAllServices().ExecuteCommand();
                cblServices.DataValueField = "ServiceID";
                cblServices.DataTextField = "ServiceName";
                cblServices.DataBind();
                divOldInteractionServices.Visible = false;

            }

            //initialize form values
            if (Session["Resident"] != null && HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile")) //Should be a new interaction in this case
            {
                FillResidentInfo();
                Session["InteractionSaved"] = false;
            }
            else if (Session["Interaction"] != null && HttpContext.Current.Request.Url.ToString().Contains("InteractionList"))//Old interaction
            {
                FillResidentInfo();
                FillInteractionInfo();
            }
            else
            {

            }
        }

        //Navigation
        protected void formNav_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            links[link.ID].Visible = true;

            links.Keys.ToList().Where(key => !key.Equals(link.ID)).ToList().ForEach(s => links[s].Visible = false);
        }

        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            SaveInteraction();
        }

        protected void lnkBtnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Homepage.aspx");
        }



        private void FillResidentInfo()
        {
            res = Session["Resident"] as Resident;

            DateTime today = DateTime.Today;
            DateTime bday = DateTime.Parse(res.DateOfBirth);
            int age = today.Date.Year - bday.Date.Year;

            //Resident(First Tab)
            tbFirstName.Text = res.ResidentFirstName;
            tbLastName.Text = res.ResidentLastName;
            tbAge.Text = bday.Date > today.AddYears(-age) ? (age--).ToString() : age.ToString();
            tbGender.Text = res.Gender;
            tbPhone.Text = res.ResidentPhoneNumber;
            tbEmail.Text = res.ResidentEmail;
            tbDoC.Text = today.ToString("yyyy-MM-dd");
            //Housing Info(Second Tab)
            //tbResidenceOccupants.Text = res.Home.NumOfOccupants; NOT IMPLEMENTED ON HOUSE/RESIDENT YET. Change TB NAME
            if (res.Home.DevelopmentID == -1)
            {
                ddlHousingType.SelectedValue = "HCV";
                tbDevelopmentName.Text = "Resident does not live in a development";
                
            }
            else
            {
                ddlHousingType.SelectedValue = "Development";
                tbDevelopmentName.Text = res.HousingDevelopment.DevelopmentName;
            }
            tbRegion.Text = res.Home.RegionName == null ? "Region not implemented yet" : res.Home.RegionName.ToString();
            tbResidentAddress.Text = res.Home.Address;

            //Disable auto filled controls
            pnlResidentInfoForm.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Enabled = false);
            pnlHousingInfoForm.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Enabled = false);
            ddlHousingType.Enabled = false;

            //Vaccine Info
            if (res.VaccineEligibility == null)
            {
                ddlVaccineEligibility.SelectedIndex = 0;
            }
            else
            {
                ddlVaccineEligibility.SelectedValue = res.VaccineEligibility.ToString();
            }
            if(res.VaccineInterest == null)
            {
                ddlVaccineInterest.SelectedIndex = 0;
            }
            else
            {
                ddlVaccineInterest.SelectedValue = res.VaccineInterest.ToString();
            }

            if (!string.IsNullOrEmpty(res.VaccineAppointmentDate))
            {
                tbVaccineAppointmentDate.Text = TextModeDateFormatter.Format(res.VaccineAppointmentDate);
            }
        }


        private void FillInteractionInfo()
        {
            interaction = Session["Interaction"] as Interaction;
            //Meeting Information
            ddlMeetingType.SelectedValue = interaction.MethodOfContact;
            tbLocation.Text = interaction.LocationOfContact;

            //Resident Health
            List<CheckBox> formSymptoms = pnlResidentHealthForm.Controls.OfType<CheckBox>().ToList(); //Get all Symptom Checkboxes
            List<string> interactionSymptoms = new List<string>();  //Get all symptom names in interaction 
            interaction.Symptoms.ForEach(s => interactionSymptoms.Add(s.SymptomName));
            formSymptoms //Set checkboxes to checked
                .Where(cb => interactionSymptoms
                .Contains(cb.Text))
                .ToList()
                .ForEach(cb => cb.Checked = true);

            tbSymptomDates.Text = TextModeDateFormatter.Format(interaction.SymptomStartDate);

            if(interaction.COVIDTestResult.Equals("No Recent Test"))
            {
                tbTestingLocation.Text = "N/A";
                ddlTestResult.SelectedValue = "No Recent Test";
            }
            else
            {
                tbTestingLocation.Text = interaction.COVIDTestLocation;
                ddlTestResult.SelectedValue = interaction.COVIDTestResult;
            }

            //Services
            divOldInteractionServices.Visible = true;
            divNewInteractionServices.Visible = false;

            List<Service> interactionServices = interaction.RequestedServices.Concat(interaction.CompletedServices).ToList(); //Completed and Uncompleted services
            cblCompletedServices.DataSource = interactionServices;
            cblCompletedServices.DataTextField = "ServiceName";
            cblCompletedServices.DataValueField = "ServiceID";
            cblCompletedServices.DataBind();

            List<string> interactionCompletedServices = new List<string>();
            interaction.CompletedServices.ForEach(s => interactionCompletedServices.Add(s.ServiceName));

            cblCompletedServices.Items.Cast<ListItem>().ToList()
            .Where(li => interactionCompletedServices.Contains(li.Text))
            .ToList()
            .ForEach(li => li.Selected = true);

            //Action Plan
            nextSteps.InnerText = interaction.ActionPlan;
        }

        private void SaveInteraction()
        {
            if (!IsFormValid()) { return; }

            res = Session["Resident"] as Resident;
            Interaction intact = new Interaction();
            //ID Values
            intact.HealthWorkerID = (Session["User"] as CARESUser).UserID;
            intact.ResidentID = res.ResidentID;
            //Form Values
            intact.DateOfContact = tbDoC.Text;
            intact.MethodOfContact = ddlMeetingType.SelectedValue;
            intact.LocationOfContact = tbLocation.Text;
            intact.COVIDTestLocation = tbTestingLocation.Text;
            intact.COVIDTestResult = ddlTestResult.SelectedValue;
            intact.SymptomStartDate = tbSymptomDates.Text;
            intact.ActionPlan = nextSteps.InnerText;
            //Symptoms
            List<CheckBox> checkedBoxes = pnlResidentHealthForm.Controls.OfType<CheckBox>().Where(cb => cb.Checked).ToList();
            List<Symptom> symptoms = new List<Symptom>();
            checkedBoxes.ForEach(cb => symptoms.Add(new Symptom(cb.Text, int.Parse(cb.ID.Split('_')[1])))); 
            intact.Symptoms = symptoms;
            //Services
            List<Service> services = new List<Service>();
            cblServices.Items.OfType<ListItem>().Where(li => li.Selected)
                .ToList()
                .ForEach(li => services.Add(new Service(li.Text, int.Parse(li.Value))));
            intact.RequestedServices = services;
            intact.RequiresFollowUp = services.Count > 0;

            try
            {
                new InteractionWriter(intact).ExecuteCommand();
            }
            catch (Exception e)
            {
                lblSave.Text = $"Error saving interaction, please try again: {e.Message}";
                lblSave.Visible = true;
                return;
            }

            //Update Resident vaccine values
            bool interest = ddlVaccineInterest.SelectedIndex == 1;
            bool eligibility = ddlVaccineEligibility.SelectedIndex == 1;
            string date = tbVaccineAppointmentDate.Text;

            new UpdateResidentVaccine().ExecuteCommand(res.ResidentID, interest, eligibility, date);
                
            Session["InteractionSaved"] = true;
        }

        private bool IsFormValid()
        {
            bool isValid = true;

            // meeting info
            if (Validation.IsEmpty(tbLocation.Text) || ddlMeetingType.SelectedIndex == 0)
            {
                isValid = false;
                lblErrorMeetingInfo.Visible = true;
                icErrorMeetingInfo.Visible = true;
            }
            else
            {
                lblErrorMeetingInfo.Visible = false;
                icErrorMeetingInfo.Visible = false;
            }

            // resident health
            bool symptomError = false; // helps keep track of when to show this panel's error icon

            // covid symptoms w/o date that they started
            if (pnlResidentHealthForm.Controls.OfType<CheckBox>().ToList().Any(cb => cb.Checked) &&
                Validation.IsEmpty(tbSymptomDates.Text))
            {
                isValid = false;
                lblErrorSymptoms.Text = "You must enter the date symptoms occurred if you selected any.";

                symptomError = true;
            }
            // date symptoms started w/o any symptoms checked
            else if (!pnlResidentHealthForm.Controls.OfType<CheckBox>().ToList().Any(cb => cb.Checked) &&
                !Validation.IsEmpty(tbSymptomDates.Text))
            {
                isValid = false;
                lblErrorSymptoms.Text = "You must select symptoms if you entered the date they occurred.";

                symptomError = true;
            }
            else
            {
                lblErrorSymptoms.Text = "";
            }

            // test result w/o location
            if (ddlTestResult.SelectedIndex != 0 && Validation.IsEmpty(tbTestingLocation.Text))
            {
                isValid = false;
                lblErrorCOVIDTest.Text = "You must enter a testing location if you selected a test result.";
                icErrorResidentHealth.Visible = true;
            }
            // test location w/o result
            else if (ddlTestResult.SelectedIndex == 0 && !Validation.IsEmpty(tbTestingLocation.Text))
            {
                isValid = false;
                lblErrorCOVIDTest.Text = "You must select a test result if you entered a testing location.";
                icErrorResidentHealth.Visible = true;
            }
            else
            {
                lblErrorCOVIDTest.Text = "";
                icErrorResidentHealth.Visible = false || symptomError; // displays if any of this panel's error conditions are met
            }

            // vaccine info
            if (ddlVaccineInterest.SelectedIndex == 0 || ddlVaccineEligibility.SelectedIndex == 0)
            {
                isValid = false;
                lblErrorVaccine.Visible = true;
                icErrorVaxInfo.Visible = true;
            }
            else
            {
                lblErrorVaccine.Visible = false;
                icErrorVaxInfo.Visible = false;
            }

            // action plan
            if (Validation.IsEmpty(nextSteps.InnerText))
            {
                isValid = false;
                lblErrorActionPlan.Visible = true;
                icErrorActionPlan.Visible = true;
            }
            else
            {
                lblErrorActionPlan.Visible = false;
                icErrorActionPlan.Visible = false;
            }

            return isValid;
        }

    }
}