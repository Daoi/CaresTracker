﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess.DataAccessors.Examples;
using CapstoneUI.DataModels;

namespace CapstoneUI
{
    public partial class CreateResident : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get list of all developments
                GetAllDevelopments GAD = new GetAllDevelopments();
                DataTable dataTable = GAD.ExecuteCommand();

                // Bind to drop down list
                ddlDevelopments.DataSource = dataTable;
                ddlDevelopments.DataValueField = "DevelopmentID";
                ddlDevelopments.DataTextField = "DevelopmentName";
                ddlDevelopments.DataBind();
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            bool houseResult = false;
            bool residentResult = false;
            // VALIDATION NEEDED

            // bool valid = Validate();
            // if(valid){...}

            //Build House object
            House residentHouse = new House();
            residentHouse.Address = txtAddress.Text;
            residentHouse.ZipCode = txtZipCode.Text;

            if (ddlHousing.SelectedIndex == 1)
            {
                residentHouse.HouseType = "Housing Choice Voucher";
                residentHouse.RegionID = Int32.Parse(ddlRegion.SelectedValue); // Requires validation to ensure input is a number
            }
            else
            {
                residentHouse.HouseType = "Development";
                residentHouse.DevelopmentID = Int32.Parse(ddlDevelopments.SelectedValue);
            }
            // Write new House to the database
            AddHouse AH = new AddHouse(residentHouse);
            if(AH.ExecuteCommand() == 1)
            {
                houseResult = true;
            }

            // Build Resident object
            Resident newResident = new Resident();

            newResident.FirstName = txtFirstName.Text;
            newResident.LastName = txtLastName.Text;
            newResident.DateOfBirth = txtDOB.Text; // DOB input field will need to be validated beforehand to ensure that it can be parsed to DateTime
            newResident.ResidentEmail = txtEmail.Text;
            newResident.ResidentPhoneNumber = txtPhoneNumber.Text;
            newResident.RelationshipToHoH = ddlRelationshipHOH.SelectedValue;
            newResident.Gender = rblGender.SelectedValue;
            newResident.Race = ddlRace.SelectedValue;
            // Retrieve HouseID of House that was just created
            GetHouse GH = new GetHouse();
            DataTable dataTable = GH.RunCommand(txtAddress.Text);
            newResident.HouseID =dataTable.Rows[0].Field<int>("HouseID");

            // Add new Resident
            ResidentWriter RW = new ResidentWriter(newResident);
            if(RW.ExecuteCommand() == 1)
            {
                residentResult = true;
            }
            

            // Hide alert labels then show which is appropriate
            lblFail.Visible = false;
            lblSuccess.Visible = false;
            if (residentResult == true && houseResult == true )
            {
                lblSuccess.Visible = true;
            }
            else
            {
                lblFail.Visible = true;
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

            upHousing.Update();//Update the page without doing full postback using update panel
        }
    }
}