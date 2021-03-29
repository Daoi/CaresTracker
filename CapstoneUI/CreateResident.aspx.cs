using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess.DataAccessors.DevelopmentAccessors;
using CapstoneUI.DataModels;

namespace CapstoneUI
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
            if (hdnfldFormattedAddress.Value.Equals("") || !hdnfldName.Value.Equals(txtAddress.Value))
            {
                lblWrongAddressInput.Visible = true;
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

            if (AddResidentResult == null) //If null Resident is NOT unique
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
            residentHouse.UnitNumber = txtUnitNumber.Text;
            if (ddlHousing.SelectedIndex == 1)
            {
                residentHouse.HouseType = "Housing Choice Voucher";
                residentHouse.RegionID = Int32.Parse(ddlRegion.SelectedValue); // Requires validation to ensure input is a number
                residentHouse.DevelopmentID = -1;
            }
            else
            {
                residentHouse.HouseType = "Development";
                residentHouse.DevelopmentID = Int32.Parse(ddlDevelopments.SelectedValue);
                // Retrieve RegionID of Development
                residentHouse.RegionID = developmentDT.Rows[0].Field<int>("RegionID");
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

                // Create Development object if development is selected house type
                if (residentHouse.HouseType == "Development")
                {
                    //Get the row matching the currently selected Housing Development Name
                    DataRow hdRecord = developmentDT.Rows.Cast<DataRow>()
                        .First(r => r.Field<string>("DevelopmentName")
                        .Equals(ddlDevelopments.SelectedItem.ToString()));

                    newResident.HousingDevelopment = new HousingDevelopment(hdRecord);
                }
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
    }
}
