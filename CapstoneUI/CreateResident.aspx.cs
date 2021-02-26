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
        DataTable developmentDT;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get list of all developments
                GetAllDevelopments GAD = new GetAllDevelopments();
                developmentDT = GAD.ExecuteCommand();
                Session["DevelopmentDT"] = developmentDT;
                List<string> developmentsList = new List<string>();
                foreach (DataRow row in developmentDT.Rows)
                {
                    string name = row.Field<string>("DevelopmentName");
                    developmentsList.Add(name);
                }
                ddlDevelopments.DataSource = developmentsList;
                ddlDevelopments.DataBind();
            }

            if (Session["DevelopmentDT"] != null)
                developmentDT = (DataTable)Session["DevelopmentDT"];

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            // VALIDATION NEEDED

            // bool valid = Validate();
            // if(valid){...}

            //Build House and Resident objects from user input
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
                //residentHouse.DevelopmentID = ...
            }
            HousingDevelopment newHd = new HousingDevelopment();
            Resident newResident = new Resident();
            newResident.FirstName = txtFirstName.Text;
            newResident.LastName = txtLastName.Text;
            newResident.DateOfBirth = txtDOB.Text; // DOB input field will need to be validated beforehand to ensure that it can be parsed to DateTime
            newResident.ResidentEmail = txtEmail.Text;
            newResident.ResidentPhoneNumber = txtPhoneNumber.Text;
            newResident.RelationshipToHoH = ddlRelationshipHOH.SelectedValue;
            newResident.Gender = rblGender.SelectedValue;
            newResident.Race = ddlRace.SelectedValue;
            newResident.Home = residentHouse;


            //Get the row matching the currently selected Housing Development Name
            DataRow hdRecord = developmentDT.Rows.Cast<DataRow>()
                .First(r => r.Field<string>("DevelopmentName")
                .Equals(ddlDevelopments.Text));

            newResident.HousingDevelopment = new HousingDevelopment()
            {
                DevelopmentName = hdRecord["DevelopmentName"].ToString(),
                DevelopmentID = int.Parse(hdRecord["DevelopmentID"].ToString()),
                NumUnits = int.Parse(hdRecord["NumUnits"].ToString()),
                SiteType = hdRecord["SiteType"].ToString(),
                OfficeAddress = hdRecord["OfficeAddress"].ToString(),
            };
            
            //Store new resident in Session to use to redirect/populate resident profile
            Session["NewResident"] = newResident;
            // Write new resident House to the database
            AddHouse AH = new AddHouse(residentHouse);
            if (AH.ExecuteCommand() == 1)
            {
                Response.Write("<script>alert('House inserted successfully')</script>");
            }
            else
            {
                Response.Write("<script>alert('Insert failed!')</script>");
            }
            Response.Redirect("ResidentProfile.aspx");
        }

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