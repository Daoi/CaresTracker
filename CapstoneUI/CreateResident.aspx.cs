using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class CreateResident : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

            CARESEntities entities = new CARESEntities();

            Resident temp = new Resident();
            temp.FirstName = txtFirstName.Text;
            temp.LastName = txtLastName.Text;
            temp.DateOfBirth = DateTime.Parse(txtDOB.Text); // DOB input field will need to be validated beforehand to ensure that it can be parsed to DateTime
            temp.ResidentEmail = txtEmail.Text;
            temp.ResidentPhoneNumber = txtPhoneNumber.Text;
            temp.RelationshipToHoH = ddlRelationshipHOH.SelectedValue;
            temp.Gender = rblGender.SelectedValue;
            temp.Race = ddlRace.SelectedValue;
            temp.FamilySize = Int32.Parse(txtFamilySize.Text); //FamilySize input field will need to be validated beforehand to ensure that it can be parsed to Int

            entities.Residents.Add(temp);
            entities.SaveChanges();
        }

        protected void ddlHousing_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<HtmlGenericControl> housingDivs = new List<HtmlGenericControl>() { divHouse, divDevelopmentUnit };
            DropDownList ddl = (DropDownList)sender;
            string selectedId = ddl.SelectedValue;
            if (ddl.SelectedIndex == 0)
            {
                return; // They haven't selected an event Type so don't change anything/show error message/whatever
            }

            housingDivs.ForEach(ed => ed.Visible = false); //Turn off all divs
            (housingDivs.Find(ed => ed.ID.Equals(selectedId)) as HtmlGenericControl).Visible = true; //Turn selected div on

            upHousing.Update();//Update the page without doing full postback using update panel
        }
    }
}