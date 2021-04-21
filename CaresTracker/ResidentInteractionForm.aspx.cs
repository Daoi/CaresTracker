
using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;
using CaresTracker.DataAccess.DataAccessors.InteractionAccessors;
using CaresTracker.DataAccess.DataAccessors.InteractionAccessors.FollowUps;
using CaresTracker.DataAccess.DataAccessors.ResidentAccessors;
using CaresTracker.DataAccess.DataAccessors.ServiceAccessors;
using CaresTracker.DataModels;
using CaresTracker.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CaresTracker
{
    public partial class ResidentInteractionForm : Page
    {
        Dictionary<string, Panel> links;
        Resident res;
        Interaction interaction;
        CARESUser user;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Key = lnkbtnID Value = Associated Panel
            links = new Dictionary<string, Panel> {
                { "residentInfo", pnlResidentInfoForm }, { "residentHealth", pnlResidentHealthForm },
                { "meetingInfo", pnlMeetingInfoForm }, { "otherInfo", pnlOtherForm }, { "services", pnlServicesForm },
                {"housingInfo", pnlHousingInfoForm }, {"vaccineInfo", pnlVaccineForm}, {"editHistory", pnlEditHistory}
                };

            user = Session["User"] as CARESUser;

            if (!IsPostBack)
            {
                //Set resident info (first tab) to visible, others to false
                links.Keys.ToList().Where(s => !s.Equals("residentInfo")).ToList().ForEach(s => links[s].Visible = false);
                cblServices.DataSource = new GetAllServices().ExecuteCommand();
                cblServices.DataValueField = "ServiceID";
                cblServices.DataTextField = "ServiceName";
                cblServices.DataBind();
                divOldInteractionServices.Visible = false;
                string url = HttpContext.Current.Request.Url.ToString();
                //initialize form values
                if (Session["Resident"] != null && url.Contains("ResidentProfile")) //Should be a new interaction in this case
                {
                    FillResidentInfo();
                    ViewState["OldInteraction"] = false;
                    divFollowUpStatus.Visible = false;
                    divFollowUpRequired.Visible = true;
                    ViewState["FollowUpRequired"] = null;
                    tbDoC.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
                else if (Session["Interaction"] != null && (url.Contains("InteractionList") || url.Contains("SaveSuccessful")))//Old interaction
                {



                    interaction = Session["Interaction"] as Interaction;

                    if (!user.UserType.Equals("T") && user.OrganizationID != new GetOrgIDByUserID().RunCommand(interaction.HealthWorkerID))
                        Response.Redirect("Homepage.aspx");

                    FillResidentInfo();
                    FillInteractionInfo();
                    ViewState["PanelState"] = true;
                    //Panels
                    TogglePanels();
                    //Vaccines require a new interaction to be changed
                    tbVaccineAppointmentDate.Enabled = false;
                    ddlVaccineStatus.Enabled = false;
                    //Services
                    //Side Bar
                    lnkBtnSave.Visible = false; //Can't save old sessions, have to edit
                    lnkBtnEdit.Visible = true;
                    ViewState["OldInteraction"] = true;
                    if (url.Contains("SaveSuccessful"))
                    {
                        lblHome.Text = "Interaction saved.";
                        lblHome.Visible = true;
                    }

                    //Show if follow up is required and not completed
                    divFollowUpStatus.Visible = interaction.RequiresFollowUp && string.IsNullOrEmpty(interaction.FollowUpCompleted);
                    ViewState["FollowUpRequired"] = 0; //Shouldn't matter what this is
                    divFollowUpRequired.Visible = false;

                    // Populate gvEditHistory
                    DataTable editHistoryDT = new GetInteractionEditHistory().RunCommand(interaction.InteractionID);
                    gvEditHistory.DataSource = editHistoryDT;
                    gvEditHistory.DataBind();

                    // Hide Edit History tab if there is no edits to this interaction
                    if (editHistoryDT.Rows.Count > 0)
                    {
                        editHistory.Visible = true;
                    }
                    //Edit Permissions
                    if(( user.UserType.Equals("A") || user.UserType.Equals("C")) && interaction.HealthWorkerID != user.UserID ) //If current user is a partner admin or chw but not the creating user
                    {
                        lnkBtnEdit.Visible = false;
                    }
                }
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
            if (!IsFormValid()) { return; }
            SaveInteraction();
        }

        protected void lnkBtnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Homepage.aspx");
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {

            Interaction interaction = Session["Interaction"] as Interaction;

            if ( (user.UserType.Equals("C") && interaction.HealthWorkerID != user.UserID) || user.UserType.Equals("A") )
            {
                return;
            }

            if (ViewState["EditMode"] == null)
            {
                ViewState["EditMode"] = true;
                lnkBtnEdit.Text = $"<i class='fas fa-save' id='icoEditSave' runat='server'  style='margin-right: .5rem'></i> Save Edits";
                TogglePanels();
            }
            else if ((bool)ViewState["EditMode"] == true)
            {
                if (!IsFormValid()) { return; }
                //Popup modal
                string showModalCall = "$('#modalEditReason').modal({show: true, keyboard: true, backdrop: 'true'});";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "showEditModal", showModalCall, true);
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

            List<Service> incompleteServices = new List<Service>();
            cblCompletedServices.Items.OfType<ListItem>()
                .ToList().Where(li => !li.Selected)
                .ToList()
                .ForEach(li => incompleteServices.Add(new Service(li.Text, int.Parse(li.Value), false)));

            try
            {
                if (completedServices.Count > 0)
                    new UpdateInteractionServices(completedServices, interaction.InteractionID, 1).ExecuteCommand();

                if (incompleteServices.Count > 0)
                    new UpdateInteractionServices(incompleteServices, interaction.InteractionID, 0).ExecuteCommand();

                if (ddlFollowUpStatus.SelectedValue.Equals("complete"))
                {
                    string date = DateTime.Today.ToString("yyyy-MM-dd");
                    new UpdateFollowUpCompleted().ExecuteCommand(date, interaction.InteractionID);
                }
                lblUpdateServices.Text = $"Services updated succesfully";
                lblUpdateServices.Visible = true;
            }
            catch (Exception ex)
            {
                lblUpdateServices.Text = $"Problem updating services, please try again later. {ex}";
                lblUpdateServices.Visible = true;
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
                catch (Exception ex)
                {
                    lblModalError.Text = "Error updating interaction, please try again later";
                    return;
                }
                string hideModalCall = "$('#modalEditReason').modal('hide');";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "hideEditModal", hideModalCall, true);
                ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "showEditModal", "", true);
            }
        }


        //FUNCTIONALITY


        //The first two panels are always disabled, Services is always Enabled.
        private void TogglePanels()
        {
            bool state = (bool)ViewState["PanelState"];
            //Meeting Form
            pnlMeetingInfoForm.Controls.OfType<TextBox>().ToList().ForEach(c => c.Enabled = !c.Enabled);
            pnlMeetingInfoForm.Controls.OfType<DropDownList>().ToList().ForEach(c => c.Enabled = !c.Enabled);
            //Health Form
            pnlResidentHealthForm.Controls.OfType<TextBox>().ToList().ForEach(c => c.Enabled = !c.Enabled);
            pnlResidentHealthForm.Controls.OfType<Panel>().ToList()[0].Controls.OfType<CheckBox>().ToList().ForEach(c => c.Enabled = !c.Enabled);
            pnlResidentHealthForm.Controls.OfType<CheckBox>().ToList().ForEach(c => c.Enabled = !c.Enabled);
            pnlResidentHealthForm.Controls.OfType<DropDownList>().ToList().ForEach(c => c.Enabled = !c.Enabled);
            //Other Form
            if (!state)
            {
                nextSteps.Attributes.Remove("readonly");
                ViewState["PanelState"] = !state;
            }
            else
            {
                nextSteps.Attributes.Add("readonly", "true");
                ViewState["PanelState"] = !state;
            }
        }

        private void SaveEdits(string reason)
        {
            Interaction newInteraction = GenerateInteraction();
            int userId = (Session["User"] as CARESUser).UserID;
            newInteraction.InteractionID = (Session["Interaction"] as Interaction).InteractionID;
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            try
            {
                new UpdateInteraction(newInteraction).ExecuteCommand();
                SaveChronicIllnesses();

                new InsertInteractionEdit().ExecuteCommand(date, reason, newInteraction.InteractionID, userId);
            }
            catch (Exception e)
            {
                throw e;
            }

            //Reset Edit State
            ViewState["EditMode"] = null;
            TogglePanels(); // Should be disabled now
            lnkBtnEdit.Text = $"<i class='fas fa-edit' id='icoEdit' runat='server'  style='margin-right: .5rem'></i> Edit Interaction";

            lblModalError.Text = string.Empty;
            lblSave.Text = "Interaction updated succesfully!";

            Session["Interaction"] = newInteraction;
            Response.Redirect("ResidentInteractionForm.aspx?from=SaveSuccessful");
        }

        private void SaveInteraction()
        {
            res = Session["Resident"] as Resident;
            Interaction newInteraction = GenerateInteraction();

            try
            {
                new InteractionWriter(newInteraction).ExecuteCommand();

                // update chronic illnesses
                SaveChronicIllnesses();

                //Update Resident vaccine values
                string status = ddlVaccineStatus.SelectedValue;
                string date = tbVaccineAppointmentDate.Text;

                res.VaccineStatus = status;
                res.VaccineAppointmentDate = date;

                new UpdateResidentVaccine().ExecuteCommand(res.ResidentID, status, date);
            }
            catch (Exception e)
            {
                lblSave.Text = $"Error saving interaction, please try again: {e.Message}";
                lblSave.Visible = true;
                return;
            }

            Session["InteractionSaved"] = true;
            Session["Interaction"] = newInteraction;
            //Switch buttons to edit
            Response.Redirect("ResidentInteractionForm.aspx?from=SaveSuccessful");
        }

        private Interaction GenerateInteraction()
        {
            Interaction newInteraction = new Interaction();
            res = Session["Resident"] as Resident;
            //ID Values
            if (!(bool)ViewState["OldInteraction"]) //If interaction is new
            {
                newInteraction.HealthWorkerID = (Session["User"] as CARESUser).UserID; //Interaction is being created now, use current user ID
            }
            else
            {
                newInteraction.HealthWorkerID = (Session["Interaction"] as Interaction).HealthWorkerID;
            }

            newInteraction.ResidentID = res.ResidentID;
            //Form Values
            newInteraction.DateOfContact = tbDoC.Text;
            newInteraction.MethodOfContact = ddlMeetingType.SelectedValue;
            newInteraction.LocationOfContact = tbLocation.Text;
            newInteraction.COVIDTestLocation = tbTestingLocation.Text.Equals("N/A") ? "" : tbTestingLocation.Text;
            if (ddlTestResult.SelectedIndex == 0)
            {
                newInteraction.COVIDTestResult = string.Empty;
            }
            else
            {
                newInteraction.COVIDTestResult = ddlTestResult.SelectedValue;
            }
            newInteraction.SymptomStartDate = tbSymptomDates.Text;
            newInteraction.ActionPlan = nextSteps.InnerText;
            //Symptoms
            List<CheckBox> checkedBoxes = pnlResidentHealthForm.Controls.OfType<CheckBox>().Where(cb => cb.Checked).ToList();
            List<Symptom> symptoms = new List<Symptom>();
            checkedBoxes.ForEach(cb => symptoms.Add(new Symptom(cb.Text, int.Parse(cb.ID.Split('_')[1]))));
            newInteraction.Symptoms = symptoms;
            //Services
            List<Service> services = new List<Service>();
            cblServices.Items.OfType<ListItem>().Where(li => li.Selected)
                .ToList()
                .ForEach(li => services.Add(new Service(li.Text, int.Parse(li.Value))));
            newInteraction.RequestedServices = services;
            newInteraction.CompletedServices = new List<Service>();
            newInteraction.RequiresFollowUp = ddlFollowUp.SelectedValue.Equals("true");
            return newInteraction;
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
            //Housing Info(Second Tab)
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
            tbZipCode.Text = res.Home.ZipCode;

            //Disable auto filled controls
            pnlResidentInfoForm.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Enabled = false);
            pnlHousingInfoForm.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Enabled = false);
            ddlHousingType.Enabled = false;

            // Chronic Illnesses
            List<CheckBox> formIllnesses = pnlChronicIllnesses.Controls.OfType<CheckBox>().ToList(); // Get all ChronicIllness Checkboxes
            List<int> residentIllnesses = new List<int>();  //Get all symptom names in interaction 
            res.ChronicIllnesses.ForEach(i => residentIllnesses.Add(i.ChronicIllnessID));
            formIllnesses //Set checkboxes to checked
                .Where(cb => residentIllnesses
                .Contains(int.Parse(cb.ID.Split('_')[1])))
                .ToList()
                .ForEach(cb => cb.Checked = true);

            //Vaccine Info
            ddlVaccineStatus.SelectedValue = res.VaccineStatus ?? "Unknown";

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
            tbDoC.Text = interaction.DateOfContact;
            //Resident Health
            List<CheckBox> formSymptoms = pnlResidentHealthForm.Controls.OfType<CheckBox>().ToList(); //Get all Symptom Checkboxes
            List<string> interactionSymptoms = new List<string>();  //Get all symptom names in interaction 
            interaction.Symptoms.ForEach(s => interactionSymptoms.Add(s.SymptomName));
            formSymptoms //Set checkboxes to checked
                .Where(cb => interactionSymptoms
                .Contains(cb.Text))
                .ToList()
                .ForEach(cb => cb.Checked = true);

            if (!string.IsNullOrWhiteSpace(interaction.SymptomStartDate))
                tbSymptomDates.Text = interaction.SymptomStartDate;

            if (string.IsNullOrWhiteSpace(interaction.COVIDTestResult))
            {
                ddlTestResult.SelectedIndex = 0;
            }
            else if (interaction.COVIDTestResult.Equals("No Recent Test"))
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
            btnUpdateServices.Visible = true;
            divNewInteractionServices.Visible = false;

            List<Service> allServices = new List<Service>();
            bool someServicesAreComplete = false;

            if (interaction.RequestedServices != null && interaction.RequestedServices.Count > 0)
            {
                allServices = allServices.Concat(interaction.RequestedServices).ToList();
            }

            if (interaction.CompletedServices != null && interaction.CompletedServices.Count > 0)
            {
                allServices = allServices.Concat(interaction.CompletedServices).ToList();
                someServicesAreComplete = true;
            }

            if (allServices.Count > 0)
            {
                cblCompletedServices.DataSource = allServices;
                cblCompletedServices.DataTextField = "ServiceName";
                cblCompletedServices.DataValueField = "ServiceID";
                cblCompletedServices.DataBind();

                if (someServicesAreComplete)
                {
                    List<string> interactionCompletedServices = new List<string>();
                    interaction.CompletedServices.ForEach(s => interactionCompletedServices.Add(s.ServiceName));

                    cblCompletedServices.Items.Cast<ListItem>().ToList()
                    .Where(li => interactionCompletedServices.Contains(li.Text))
                    .ToList()
                    .ForEach(li => li.Selected = true);
                }
            }
            else
            {
                lblServicesInfo.Text = "This interaction has no services to display.";
                if (!string.IsNullOrEmpty(interaction.FollowUpCompleted) || !interaction.RequiresFollowUp)
                {
                    btnUpdateServices.Visible = false;
                }
            }

            ddlFollowUp.SelectedValue = interaction.RequiresFollowUp.ToString();

            //Action Plan
            nextSteps.InnerText = interaction.ActionPlan;
        }

        private void SaveChronicIllnesses()
        {
            List<CheckBox> checkedBoxes = pnlChronicIllnesses.Controls.OfType<CheckBox>().Where(cb => cb.Checked).ToList();
            List<ChronicIllness> illnesses = new List<ChronicIllness>();
            checkedBoxes.ForEach(cb => illnesses.Add(new ChronicIllness(int.Parse(cb.ID.Split('_')[1]))));
            new UpdateResidentChronicIllnesses(illnesses, res.ResidentID).ExecuteCommand();

            res.ChronicIllnesses = illnesses;
            Session["Resident"] = res;
        }

        private bool IsFormValid()
        {
            bool isValid = true;
            bool editing = (ViewState["EditMode"] as bool?) ?? false;

            // meeting info
            if (Validation.IsEmpty(tbLocation.Text) || ddlMeetingType.SelectedIndex == 0 || string.IsNullOrWhiteSpace(tbDoC.Text))
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
                lblErrorSymptoms.Visible = true;
                lblErrorSymptoms.Text = "You must enter the date symptoms occurred if you selected any.";

                symptomError = true;
            }
            // date symptoms started w/o any symptoms checked
            else if (!pnlResidentHealthForm.Controls.OfType<CheckBox>().ToList().Any(cb => cb.Checked) &&
                !Validation.IsEmpty(tbSymptomDates.Text))
            {
                isValid = false;
                lblErrorSymptoms.Visible = true;
                lblErrorSymptoms.Text = "You must select symptoms if you entered the date they occurred.";

                symptomError = true;
            }
            else
            {
                lblErrorSymptoms.Visible = false;
                lblErrorSymptoms.Text = string.Empty;
            }

            // test result w/o location
            if ((ddlTestResult.SelectedIndex == 1 || ddlTestResult.SelectedIndex == 2) && Validation.IsEmpty(tbTestingLocation.Text))
            {
                isValid = false;
                lblErrorCOVIDTest.Visible = true;
                lblErrorCOVIDTest.Text = "You must enter a testing location if you selected a test result.";
                icErrorResidentHealth.Visible = true;
            }
            // test location w/o result
            else if ( (ddlTestResult.SelectedIndex == 0 || ddlTestResult.SelectedIndex == 3) && (!tbTestingLocation.Text.Equals("N/A") || !Validation.IsEmpty(tbTestingLocation.Text)))
            {
                isValid = false;
                lblErrorCOVIDTest.Visible = true;
                lblErrorCOVIDTest.Text = "You must select a test result if you entered a testing location.";
                icErrorResidentHealth.Visible = true;
            }
            else
            {
                lblErrorCOVIDTest.Visible = false;
                lblErrorCOVIDTest.Text = string.Empty;
                icErrorResidentHealth.Visible = false || symptomError; // displays if any of this panel's error conditions are met
            }

            // vaccine info

            if (ddlVaccineStatus.SelectedIndex == 0 && !editing)
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

            //FollowUp
            if (ViewState["FollowUpRequired"] == null)
            {
                isValid = false;
                icServices.Visible = true;
                lblFollowUpError.Visible = true;
            }
            else
            {
                icServices.Visible = false;
                lblFollowUpError.Visible = false;
            }


            return isValid;
        }

        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            lblModalError.Text = string.Empty;
            string hideModalCall = "$('#modalEditReason').modal('hide');";
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "hideEditModal", hideModalCall, true);
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "showEditModal", "", true);
        }

        protected void ddlFollowUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFollowUp.SelectedIndex == 0)
            {
                ViewState["FollowUpRequired"] = null;
            }
            else
            {
                ViewState["FollowUpRequired"] = ddlFollowUp.SelectedValue;
            }
        }
    }
}