using CapstoneUI.DataAccess.DataAccessors;
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
                //Panels
                TogglePanels();
                pnlVaccineForm.Enabled = false; //Requires a new interaction to be changed
                //Services
                btnUpdateServices.Visible = true;
                //Side Bar
                lnkBtnSave.Visible = false; //Can't save old sessions, have to edit
                lnkBtnEdit.Visible = true;

                
            }

        }
        
        
        //EVENT HANDLERS


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
            if(Session["InteractionSaved"] != null && (bool)Session["InteractionSaved"] == false)
            {
                lblHome.Text = "Interaction not saved yet. Click Home again to leave without saving.";
                warningHome.Visible = true;
                Session["InteractionSaved"] = null;
                return;
            }

            Response.Redirect("~/Homepage.aspx");
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            if(ViewState["EditMode"] == null)
            {
                ViewState["EditMode"] = true;
                lnkBtnEdit.Text = $"<i class='fas fa-save' id='icoEditSave' runat='server'  style='margin-right: .5rem'></i> Save Edits";
                TogglePanels();
            }
            else if((bool)ViewState["EditMode"] == true)
            {
                //Popup modal
                string showModalCall = "$('#modalEditReason').modal({show: true, keyboard: true, backdrop: 'true'});";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "showModal", showModalCall, true);
            }

        }
        
        protected void btnUpdateServices_Click(object sender, EventArgs e)
        {
            interaction = Session["Interaction"] as Interaction;

            List<Service> completedServices = new List<Service>();
            cblCompletedServices.Items.OfType<ListItem>()
                .ToList().Where(li => li.Selected)
                .ToList()
                .ForEach(li => completedServices.Add(new Service(li.Text, int.Parse(li.Value), true)));

            try
            {
                new UpdateInteractionServices(completedServices, interaction.InteractionID).ExecuteCommand();

                if (cblCompletedServices.Items.Count == completedServices.Count)
                {
                    string date = DateTime.Today.ToString("yyyy-mm-dd");
                    new UpdateFollowUpCompleted().ExecuteCommand(date, interaction.InteractionID);
                }
                lblUpdateServices.Text = $"Services updated succesfully";
            }
            catch(Exception ex)
            {
                lblUpdateServices.Text = $"Problem updating services, please try again later. {ex}";
            }
        }

        protected void btnEditSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(taEditReason.InnerText))
            {
                lblModalError.Text = "Reason for edit is required";
            }
            else
            {
                try
                {
                    SaveEdits(taEditReason.InnerText);
                }
                catch(Exception ex)
                {
                    lblModalError.Text = "Error updating interaction, please try again later";
                    return;
                }
                string hideModalCall = "$('#modalEditReason').modal('hide');";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "hideModal", hideModalCall, true);
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "showModal", "", true);
            }
        }


        //FUNCTIONALITY


        //The first two panels are always disabled, Services is always Enabled.
        private void TogglePanels()
        {
            pnlMeetingInfoForm.Enabled = !pnlMeetingInfoForm.Enabled;
            pnlOtherForm.Enabled = !pnlOtherForm.Enabled;
            pnlResidentHealthForm.Enabled = !pnlResidentHealthForm.Enabled;
            if (pnlOtherForm.Enabled)
            {
                nextSteps.Attributes.Remove("readonly");
            }
            else
            {
                nextSteps.Attributes.Add("readonly", "true");
            }
        }

        private void SaveEdits(string reason)
        {
            Interaction interaction = GenerateInteraction();
            interaction.InteractionID = (Session["Interaction"] as Interaction).InteractionID;
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            try
            {
                new UpdateInteraction(interaction).ExecuteCommand();
                new UpdateInteractionSymptoms(interaction.Symptoms, interaction.InteractionID).ExecuteCommand();
                new InsertInteractionEdit().ExecuteCommand(date, reason, interaction.InteractionID);
            }
            catch(Exception e)
            {
                throw e;
            }

            //Reset Edit State
            ViewState["EditMode"] = null;
            TogglePanels(); // Should be disabled now
            lnkBtnEdit.Text = $"<i class='fas fa-edit' id='icoEdit' runat='server'  style='margin-right: .5rem'></i> Edit Interaction";

            lblSave.Text = "Interaction updated succesfully!";
        }

        private void SaveInteraction()
        {
            res = Session["Resident"] as Resident;
            Interaction interaction = GenerateInteraction();

            try
            {
                new InteractionWriter(interaction).ExecuteCommand();
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

        private Interaction GenerateInteraction()
        {
            Interaction interaction = new Interaction();
            res = Session["Resident"] as Resident;
            //ID Values
            interaction.HealthWorkerID = (Session["User"] as CARESUser).UserID;
            interaction.ResidentID = res.ResidentID;
            //Form Values
            interaction.DateOfContact = tbDoC.Text;
            interaction.MethodOfContact = ddlMeetingType.SelectedValue;
            interaction.LocationOfContact = tbLocation.Text;
            interaction.COVIDTestLocation = tbTestingLocation.Text;
            interaction.COVIDTestResult = ddlTestResult.SelectedValue;
            interaction.SymptomStartDate = tbSymptomDates.Text;
            interaction.ActionPlan = nextSteps.InnerText;
            //Symptoms
            List<CheckBox> checkedBoxes = pnlResidentHealthForm.Controls.OfType<CheckBox>().Where(cb => cb.Checked).ToList();
            List<Symptom> symptoms = new List<Symptom>();
            checkedBoxes.ForEach(cb => symptoms.Add(new Symptom(cb.Text, int.Parse(cb.ID.Split('_')[1]))));
            interaction.Symptoms = symptoms;
            //Services
            List<Service> services = new List<Service>();
            cblServices.Items.OfType<ListItem>().Where(li => li.Selected)
                .ToList()
                .ForEach(li => services.Add(new Service(li.Text, int.Parse(li.Value))));
            interaction.RequestedServices = services;
            interaction.RequiresFollowUp = services.Count > 0; //A service should imply requires follow up
            return interaction;
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
            if (res.VaccineInterest == null)
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

            if (interaction.COVIDTestResult.Equals("No Recent Test"))
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


    }
}