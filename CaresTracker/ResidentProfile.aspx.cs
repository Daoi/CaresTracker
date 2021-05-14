using CaresTracker.DataAccess.DataAccessors.ResidentAccessors;
using CaresTracker.DataAccess.DataAccessors.DevelopmentAccessors;
using CaresTracker.DataModels;
using CaresTracker.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.HouseAccessors;
using CaresTracker.DataAccess.DataAccessors.InteractionAccessors;
using CaresTracker.DataAccess.DataAccessors.ChronicIllnessAccessors;

namespace CaresTracker
{
    public partial class ResidentProfile : System.Web.UI.Page
    {
        Resident currentRes;
        DataTable interactions;
        protected void Page_Load(object sender, EventArgs e)
        {
            CARESUser user = Session["User"] as CARESUser;

            if (Session["Resident"] != null)
            {
                currentRes = (Resident)Session["Resident"];
            }
            else
            {
                Response.Redirect("Homepage.aspx");
            }

            if (!IsPostBack)
            {
                ToggleControls(); //Set page to disabled status

                //Bind control data
                DataTable ciDT = new GetAllCi().RunCommand();
                cblChronicHealth.DataSource = ciDT;
                cblChronicHealth.DataTextField = "ChronicIllnessName";
                cblChronicHealth.DataValueField = "ChronicIllnessID";
                cblChronicHealth.DataBind();
                ViewState["CI"] = ciDT;


                ddlHoH.DataSource = new GetRelationships().RunCommand();
                ddlHoH.DataTextField = "Relationship";
                ddlHoH.DataValueField = "Relationship";
                ddlHoH.DataBind();

                DataTable developmentDT = new GetDevelopmentsByUserID().ExecuteCommand(user.UserID);
                ViewState["DevelopmentDT"] = developmentDT;
                ddlHousingDevelopment.DataSource = developmentDT;
                ddlHousingDevelopment.DataValueField = "DevelopmentID";
                ddlHousingDevelopment.DataTextField = "DevelopmentName";
                ddlHousingDevelopment.DataBind();

                InitializeProfileValues();
                // Provide initial values for hidden fields for google API validation
                hdnfldFormattedAddress.Value = txtAddress.Value;
                hdnfldName.Value = txtAddress.Value;

                //Import error handling
                if (currentRes.Imported)
                {
                    lblImportWarning.Text = "This resident was imported, please ensure their address is correct by editing their profile and selecting the value from the drop downlist.";
                    lblImportWarning.Visible = true;

                }

                if (currentRes.DoBImported)
                {
                    lblDoBWarning.Text = "This resident was imported, the day and month of their Date of Birth are likely inaccurate. Please update it if you can.";
                    lblDoBWarning.Visible = true;
                    ViewState["DoB"] = tbDoB.Text;
                }

                if (!currentRes.IsActive)
                {
                    btnToggleActivation.Text = "Reactivate Profile";
                }
            }


            //Enable ddl searching 
            string select2Params = "'#MainContent_ddlHousingDevelopment', 'Select Development'";
            string select2Call = $"setupSelect2({select2Params});";
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "select2Call", select2Call, true);
        }


        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        public void InitializeProfileValues()
        {
            //Housing Stuff
            txtAddress.Value = currentRes.Home.Address;
            ddlHousingDevelopment.SelectedValue = currentRes.Home.DevelopmentID.ToString();
            tbUnitNumber.Text = currentRes.Home.UnitNumber;
            ddlRegion.SelectedValue = currentRes.Home.RegionID.ToString();
            tbZipcode.Text = currentRes.Home.ZipCode;
            //Resident Stuff
            tbFirstName.Text = currentRes.ResidentFirstName;
            tbLastName.Text = currentRes.ResidentLastName;
            tbDoB.Text = TextModeDateFormatter.Format(currentRes.DateOfBirth);
            tbPhone.Text = currentRes.ResidentPhoneNumber;
            tbEmail.Text = currentRes.ResidentEmail;
            rblGender.SelectedValue = string.IsNullOrWhiteSpace(currentRes.Gender) ? "Unknown" : currentRes.Gender;
            ddlHoH.SelectedValue = currentRes.RelationshipToHoH;
            ddlRace.SelectedValue = currentRes.Race;
            ddlLanguage.SelectedValue = string.IsNullOrWhiteSpace(currentRes.PreferredLanguage) ? "Unknown" : currentRes.PreferredLanguage;
            //Vaccine
            ddlVaccineStatus.SelectedValue = currentRes.VaccineStatus;
            tbAppointmentDate.Text = currentRes.VaccineAppointmentDate;
            //Chronic Illness
            if (currentRes.ChronicIllnesses.Count > 0)
            {
                List<ListItem> ciList = cblChronicHealth.Items.OfType<ListItem>().ToList(); //Check the matches
                currentRes.ChronicIllnesses.ForEach(ci =>
                    ciList.Find(li => int.Parse(li.Value) == ci.ChronicIllnessID).Selected = true
                    );
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
            if (ddlHousingDevelopment.SelectedIndex == 0) //if hcv housing
            {
                ddlRegion.Enabled = true;
            }
            ToggleHTMLInputElement();
            ViewState["EditMode"] = true;
            btnSaveEdits.Visible = true;
            btnCancelEdits.Visible = true;
            btnEditProfile.Visible = false;
            if (currentRes.Imported)
            {
                hdnfldFormattedAddress.Value = "";
            }
        }

        //Toggle all non-button controls enabled status 
        private void ToggleControls(Control control = null)
        {
            List<Control> controls = control != null ? control.Controls.OfType<Control>().ToList() : Page.Controls.OfType<Control>().ToList();
            List<string> excluded = new List<string>() { "tbZipcode", "ddlRegion" }; //Control Ids that should be excluded
            foreach (Control c in controls)
            {
                Type type = c.GetType();
                PropertyInfo prop = type.GetProperty("Enabled");

                if ( prop != null && (type != typeof(Button) && !c.ID.Contains("lnk") && !excluded.Contains(c.ID))) //Dont disable links, buttons, or excluded controls
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

        // Toggle HTMLInput element used for API
        private void ToggleHTMLInputElement()
        {
            if (!txtAddress.Disabled)
            {
                txtAddress.Disabled = true;
            }
            else
            {
                txtAddress.Disabled = false;
            }
        }

        protected void btnSaveEdits_Click(object sender, EventArgs e)
        {
            if (!ValidatePage())
                return;

            Resident res = new Resident();
            //Resident Info
            res.ResidentID = currentRes.ResidentID;
            res.ResidentPhoneNumber = Validation.IsPhoneNumber(tbPhone.Text).strPhone;
            tbPhone.Text = res.ResidentPhoneNumber; // display formatted phone
            res.ResidentEmail = tbEmail.Text;
            res.ResidentFirstName = tbFirstName.Text;
            res.ResidentLastName = tbLastName.Text;
            res.DateOfBirth = tbDoB.Text;
            res.Gender = rblGender.SelectedValue;
            res.Race = ddlRace.SelectedValue;
            res.RelationshipToHoH = ddlHoH.SelectedValue;
            res.PreferredLanguage = ddlLanguage.SelectedItem.Text;
            //Vaccine Info
            res.VaccineStatus = ddlVaccineStatus.SelectedValue;
            res.VaccineAppointmentDate = tbAppointmentDate.Text;
            //Chronic Illnesses
            //Get all currently selected CI values(Value = CI ID)
            List<string> selectedCI = cblChronicHealth.Items.Cast<ListItem>()
               .Where(li => li.Selected)
               .Select(li => li.Value)
               .ToList();
            DataTable ciDT = ViewState["CI"] as DataTable;
            
            //Find matching rows in DataTable
            List<DataRow> CIs = new List<DataRow>();
            selectedCI.ForEach(val =>
                CIs.AddRange(ciDT.Select($"ChronicIllnessID='{val}'")
                ));
            //Create List of Chronic Illnesses with DRs
            List<ChronicIllness> newCIs = new List<ChronicIllness>();
            CIs.ForEach(dr => newCIs.Add(new ChronicIllness(dr)));
            try
            {
                new UpdateResidentChronicIllnesses(newCIs, res.ResidentID).ExecuteCommand();
                res.ChronicIllnesses = newCIs;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = ex.Message;
            }

            //Housing Info
            res.Home = new House();
            res.Home.HouseID = currentRes.Home.HouseID;
            res.Home.Address = txtAddress.Value;
            res.Home.ZipCode = tbZipcode.Text;
            res.Home.UnitNumber = tbUnitNumber.Text;
            res.Home.RegionName = ddlRegion.SelectedItem.Text;
            res.Home.RegionID = int.Parse(ddlRegion.SelectedValue);
            res.Home.DevelopmentID = int.Parse(ddlHousingDevelopment.SelectedValue);

            if (!hdnfldFormattedAddress.Value.Equals(txtAddress.Value)) // If a new address was selected
            {
                // Slice up formatted address from Google API
                string formatted_address = hdnfldFormattedAddress.Value;
                string[] list = formatted_address.Split(',');

                string Address = list[0];
                // Remove "PA" from zipcode string
                string ZipCode = list[2].Remove(0, 4);

                // Assign values
                res.Home.Address = Address;
                res.Home.ZipCode = ZipCode;

                // Change txtZipCode to reflect new ZipCode
                tbZipcode.Text = ZipCode;

                AddHouse AH = new AddHouse(res.Home);
                object AddHouseResult = AH.ExecuteCommand();
                res.Home.HouseID = Convert.ToInt32(AddHouseResult);

                // Update Resident's HouseID to match new House
                UpdateHouseID UHI = new UpdateHouseID();
                UHI.ExecuteCommand(res.ResidentID, res.Home.HouseID);
            }
            else if (res.Home.DevelopmentID != currentRes.Home.DevelopmentID) // if housing dev was changed but address wasn't
            {
                new UpdateHouse(res.Home).ExecuteCommand();
            }

            if (res.Home.DevelopmentID != -1) //If not HCV
            {
                res.HousingDevelopment = new HousingDevelopment();
                res.HousingDevelopment.DevelopmentID = res.Home.DevelopmentID;
                res.HousingDevelopment.DevelopmentName = ddlHousingDevelopment.SelectedItem.Text;
            }

            //Update the resident values
            try
            {
                new UpdateResident(res).ExecuteCommand();
                ToggleControls();
                ToggleHTMLInputElement();
                btnEditProfile.Visible = true;
                btnSaveEdits.Visible = false;
                btnCancelEdits.Visible = false;
                lblErrorMessage.Text = string.Empty;
                if (currentRes.Imported && currentRes.DoBImported)
                {
                    res.Imported = false;
                    lblImportWarning.Visible = false;
                    string DoB = ViewState["DoB"].ToString() ?? res.DateOfBirth;
                    bool dobUpdated = !DoB.Equals(tbDoB.Text);
                    if (dobUpdated) //Fixed DoB and Address
                    {
                        new SetImported().ExecuteCommand(currentRes.ResidentID, false, false);
                        res.DoBImported = false;
                        lblDoBWarning.Visible = false;
                    }
                    else //Fixed only address
                    {
                        new SetImported().ExecuteCommand(currentRes.ResidentID, false, true);
                    }
                }
                else if (currentRes.DoBImported)//Address was fixed previously, DoB still not fixed
                {
                    string DoB = ViewState["DoB"].ToString() ?? res.DateOfBirth;
                    bool dobUpdated = !DoB.Equals(tbDoB.Text);

                    if (dobUpdated) //DoB fixed
                    {
                        new SetImported().ExecuteCommand(currentRes.ResidentID, false, false);
                        res.DoBImported = false;
                    }

                }

                Session["Resident"] = res;

            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = $"Failed to update profile: {ex.Message}";
            }

            ddlRegion.Enabled = false;
            ViewState["EditMode"] = false;
        }

        protected void lnkResidentLookup_Click(object sender, EventArgs e)
        {
            Response.Redirect("ResidentLookup.aspx");
        }

        protected void ddlHousingDevelopment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlHousingDevelopment.SelectedIndex == 0)
            {
                if(ViewState["EditMode"] != null && (bool)ViewState["EditMode"])
                    ddlRegion.Enabled = true;
            }
            else
            {
                DataTable dt = ViewState["DevelopmentDT"] as DataTable;
                ddlRegion.Enabled = false;
                int devId = int.Parse(ddlHousingDevelopment.SelectedValue);
                ddlRegion.SelectedValue = (dt.Rows.Cast<DataRow>().First(dr => dr.Field<int>("DevelopmentID") == devId) as DataRow).Field<int>("RegionID").ToString();
            }

            upRegion.Update();
        }

        protected void btnCancelEdits_Click(object sender, EventArgs e)
        {
            ToggleControls();
            ToggleHTMLInputElement();
            lblErrorMessage.Text = string.Empty;
            btnEditProfile.Visible = true;
            btnSaveEdits.Visible = false;
            btnCancelEdits.Visible = false;
            ddlRegion.Enabled = false;
            ViewState["EditMode"] = false;
            InitializeProfileValues();
        }

        protected void btnToggleActivation_Click(object sender, EventArgs e)
        {
            try
            {
                new SetProfileActivationStatus().ExecuteCommand(currentRes.ResidentID, !currentRes.IsActive);

                currentRes.IsActive = !currentRes.IsActive;
                btnToggleActivation.Text = currentRes.IsActive ? "Deactivate Profile" : "Reactivate Profile";

                Session["Resident"] = currentRes;
            }
            catch (Exception ex)
            {
                lblErrorInactivate.Text = $"Error toggling activation status: {ex.Message}";
            }
        }

        private bool ValidatePage()
        {
            List<TextBox> mandatory = new List<TextBox>() { tbFirstName, tbLastName, tbPhone, tbDoB };
            bool isValid = true;

            if (hdnfldFormattedAddress.Value.Equals("") || !hdnfldName.Value.Equals(txtAddress.Value))
            {
                isValid = false;
                lblWrongAddressInput.Visible = true;
            }
            else
            {
                lblWrongAddressInput.Visible = false;
            }

            if (mandatory.Any(tb => string.IsNullOrWhiteSpace(tb.Text)))
            {
                isValid = false;
                lblRIError.Text = "First Name, Last Name, Phone Number and Date of Birth are mandatory";
                lblRIError.Visible = true;
            }
            else
            {
                lblRIError.Visible = false;
            }

            if (!Validation.IsPhoneNumber(tbPhone.Text).isValid)
            {
                isValid = false;
                lblErrorPhone.Text = "Enter 10 digits or ###-###-####";
                lblErrorPhone.Visible = true;
            }
            else
            {
                lblErrorPhone.Visible = false;
            }

            return isValid;
        }
    }
}
