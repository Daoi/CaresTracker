using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.DevelopmentAccessors;
using CaresTracker.DataAccess.DataAccessors.HouseAccessors;
using CaresTracker.DataAccess.DataAccessors.ResidentAccessors;
using CaresTracker.DataModels;
using CaresTracker.Utilities;

namespace CaresTracker
{
    public partial class CreateResident : System.Web.UI.Page
    {
        DataTable developmentDT;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get list of developments
                CARESUser user = Session["User"] as CARESUser;
                DataTable developmentDT = new GetDevelopmentsByUserID().ExecuteCommand(user.UserID);

                // Bind to drop down list
                ddlDevelopments.DataSource = developmentDT;
                ddlDevelopments.DataValueField = "DevelopmentID";
                ddlDevelopments.DataTextField = "DevelopmentName";
                // Store list in session
                Session["DevelopmentDT"] = developmentDT;

                ddlDevelopments.DataBind();
            }

            if (Session["DevelopmentDT"] != null)
                developmentDT = (DataTable)Session["DevelopmentDT"];

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            // Check that address is selected from the API predictions list
            if (ValidateForm())
            {
                return;
            }

            // Build Resident object
            Resident newResident = new Resident();

            newResident.ResidentFirstName = txtFirstName.Text;
            newResident.ResidentLastName = txtLastName.Text;
            newResident.DateOfBirth = txtDOB.Text;
            newResident.ResidentEmail = txtEmail.Text;
            newResident.ResidentPhoneNumber = txtPhoneNumber.Text;
            newResident.RelationshipToHoH = ddlRelationshipHOH.SelectedValue;
            newResident.Gender = rblGender.SelectedValue;
            newResident.Race = ddlRace.SelectedValue;
            newResident.PreferredLanguage = ddlLanguage.SelectedValue;

            // Add new Resident
            ResidentWriter RW = new ResidentWriter(newResident);
            object AddResidentResult = RW.ExecuteCommand();

            if (AddResidentResult.GetType().Equals(typeof(DBNull))) //If null Resident is NOT unique
            {
                lblUniqueResident.Visible = true;
                return;
            }

            //Build House object
            House residentHouse = new House();

            // Slice up formatted address from Google API
            string formatted_address = hdnfldFormattedAddress.Value;
            string[] list = formatted_address.Split(',');

            string Address = list[0];
            // Remove "PA" from zipcode string
            string ZipCode = list[2].Remove(0, 4);

            residentHouse.ZipCode = ZipCode;
            residentHouse.Address = Address;
            residentHouse.UnitNumber = string.IsNullOrWhiteSpace(txtUnitNumber.Text) ? "N/A" : txtUnitNumber.Text;
            // If HCV is selected
            if (ddlHousing.SelectedIndex == 1)
            {
                residentHouse.RegionID = Int32.Parse(ddlRegion.SelectedValue); // Requires validation to ensure input is a number
                residentHouse.DevelopmentID = -1;
            }
            // If Development is selected
            else
            {
                residentHouse.DevelopmentID = Int32.Parse(ddlDevelopments.SelectedValue);
                // Retrieve RegionID of Development
                residentHouse.RegionID = developmentDT.Rows.Cast<DataRow>().ToList()
                    .Where(dr => (int)dr["DevelopmentID"] == residentHouse.DevelopmentID)
                    .Select(dr => dr.Field<int>("RegionID")).ElementAt(0);
                //Create Development object for resident object
                DataRow hdRecord = developmentDT.Rows.Cast<DataRow>()
                    .First(r => r.Field<string>("DevelopmentName")
                    .Equals(ddlDevelopments.SelectedItem.ToString()));

                newResident.HousingDevelopment = new HousingDevelopment(hdRecord);

            }

            // Attach newly created house to resident for session storage
            newResident.Home = residentHouse;

            try
            {
                // Write new House to the database
                AddHouse AH = new AddHouse(residentHouse);
                object AddHouseResult = AH.ExecuteCommand();

                // Update new Resident's HouseID to match new House
                UpdateHouseID UHI = new UpdateHouseID();
                UHI.ExecuteCommand(Convert.ToInt32(AddResidentResult), Convert.ToInt32(AddHouseResult));

                newResident.ResidentID = Convert.ToInt32(AddResidentResult);

                //Store new resident in Session to use to redirect/populate resident profile
                Session["Resident"] = newResident;

                Response.Redirect("ResidentProfile.aspx");
            }
            catch (Exception ex) // If the House post fails, display error label
            {
                lblFail.Visible = true;
                lblFail.Text = ex.Message;
            }
        }


        // Show/hide divs depending on which housing option is selected
        protected void ddlHousing_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<HtmlGenericControl> housingDivs = new List<HtmlGenericControl>() { divHouse, divDevelopmentUnit };
            DropDownList ddl = (DropDownList)sender;
            string selectedId = ddl.SelectedValue;

            if (ddl.SelectedIndex == 0)
            {
                housingDivs.ForEach(ed => ed.Visible = false); //Hide all divs as user must select a housing type
                return;
            }

            housingDivs.ForEach(ed => ed.Visible = false); //Turn off all divs
            (housingDivs.Find(ed => ed.ID.Equals(selectedId)) as HtmlGenericControl).Visible = true; //Turn selected div on

            string select2Params = selectedId == "divHouse" ? "'#MainContent_ddlRegion', 'Select Region'" : "'#MainContent_ddlDevelopments', 'Select Development'";
            string select2Call = $"setupSelect2({select2Params});";
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "select2Call", select2Call, true);
            upHousing.Update();//Update the page without doing full postback using update panel
        }

        private bool ValidateForm()
        {
            bool isFormValid = true;

            List<TextBox> mandatory = new List<TextBox>() { txtFirstName, txtLastName, txtDOB, txtPhoneNumber };

            if (!mandatory.All(tb => !string.IsNullOrWhiteSpace(tb.Text)))
            {
                lblValidationError.Text = "First Name, Last Name, Date of Birth and Phone Number must be filled out";
                lblValidationError.Visible = true;
                isFormValid = false;
            }
            else
            {
                lblValidationError.Visible = false;
            }

            string phone;

            (isFormValid, phone) = Validation.IsPhoneNumber(txtPhoneNumber.Text);

            if (isFormValid)
            {
                txtPhoneNumber.Text = phone;
                lblValidationPhone.Visible = false;
            }
            else
            {
                lblValidationPhone.Text = "Enter 10 digits or ###-###-####";
                lblValidationPhone.Visible = true;
            }

            if(rblGender.SelectedItem == null)
            {
                isFormValid = false;
                lblValidationGender.Text = "Please select a value for gender";
                lblValidationGender.Visible = true;
            }
            else
            {
                lblValidationGender.Visible = false;
            }


            if(ddlHousing.SelectedIndex == 0)
            {
                isFormValid = false;
                lblValidationHousing.Text = "Must select a housing type.";
                lblValidationHousing.Visible = true;
            }
            else
            {
                lblValidationHousing.Visible = false;
            }

            // Check that address is selected from the API predictions list
            if (hdnfldFormattedAddress.Value.Equals("") || !hdnfldName.Value.Equals(txtAddress.Value))
            {
                isFormValid = false;
                lblWrongAddressInput.Visible = true;
            }
            else
            {
                lblWrongAddressInput.Visible = true;
            }

            return isFormValid;
        }

    }
}
