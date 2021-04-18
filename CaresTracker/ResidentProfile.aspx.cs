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

namespace CaresTracker
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
                // Provide initial values for hidden fields
                hdnfldFormattedAddress.Value = txtAddress.Value;
                hdnfldName.Value = txtAddress.Value;
                ToggleControls(); //Set page to disabled status
                if (Session["DevelopmentDT"] == null)
                {
                    CARESUser user = Session["User"] as CARESUser;
                    DataTable developmentDT = new GetDevelopmentsByUserID().ExecuteCommand(user.UserID);
                    ViewState["DevelopmentDT"] = developmentDT;
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
                    ViewState["DevelopmentDT"] = developmentDT;
                }
            }

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
            //# of occupants still needed?
            //Resident Stuff
            tbFirstName.Text = currentRes.ResidentFirstName;
            tbLastName.Text = currentRes.ResidentLastName;
            tbDoB.Text = TextModeDateFormatter.Format(currentRes.DateOfBirth);
            tbPhone.Text = currentRes.ResidentPhoneNumber;
            tbEmail.Text = currentRes.ResidentEmail;
            rblGender.SelectedValue = currentRes.Gender;
            //Family size still needed?
            ddlRace.SelectedValue = currentRes.Race;
            ddlLanguage.SelectedValue = currentRes.PreferredLanguage;
            //Vaccine
            ddlVaccineStatus.SelectedValue = currentRes.VaccineStatus;
            tbAppointmentDate.Text = currentRes.VaccineAppointmentDate;
            


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
            // Check that address is selected from the API predictions list
            if (hdnfldFormattedAddress.Value.Equals("") || !hdnfldName.Value.Equals(txtAddress.Value))
            {
                lblWrongAddressInput.Visible = true;
                return;
            }
            lblWrongAddressInput.Visible = false;

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
            res.PreferredLanguage = ddlLanguage.SelectedItem.Text;
            //Vaccine Info
            res.VaccineStatus = ddlVaccineStatus.SelectedValue;
            res.VaccineAppointmentDate = tbAppointmentDate.Text;
            //Housing Info
            res.Home = new House();

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
            }

            if (res.Home.DevelopmentID != -1) //If not HCV
            {
                res.HousingDevelopment = new HousingDevelopment();
                res.HousingDevelopment.DevelopmentID = res.Home.DevelopmentID;
                res.HousingDevelopment.DevelopmentName = ddlHousingDevelopment.SelectedItem.Text;
            }


            AddHouse AH = new AddHouse(res.Home);
            object AddHouseResult = AH.ExecuteCommand();
            res.Home.HouseID = Convert.ToInt32(AddHouseResult);

            // Update Resident's HouseID to match new House
            UpdateHouseID UHI = new UpdateHouseID();
            UHI.ExecuteCommand(res.ResidentID, res.Home.HouseID);

            Session["Resident"] = res;
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
            }
            catch(Exception ex)
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
    }
}