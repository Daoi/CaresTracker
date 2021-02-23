using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;

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
                List<string> developmentsList = new List<string>();
                foreach (DataRow row in dataTable.Rows)
                {
                    string name = row.Field<string>("DevelopmentName");
                    developmentsList.Add(name);
                }
                ddlDevelopments.DataSource = developmentsList;
                ddlDevelopments.DataBind();
            }
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

            //FirstName = txtFirstName.Text;
            //LastName = txtLastName.Text;
            //DateOfBirth = DateTime.Parse(txtDOB.Text); // DOB input field will need to be validated beforehand to ensure that it can be parsed to DateTime
            //ResidentEmail = txtEmail.Text;
            //ResidentPhoneNumber = txtPhoneNumber.Text;
            //RelationshipToHoH = ddlRelationshipHOH.SelectedValue;
            //Gender = rblGender.SelectedValue;
            //Race = ddlRace.SelectedValue;
            //FamilySize = Int32.Parse(txtFamilySize.Text); //FamilySize input field will need to be validated beforehand to ensure that it can be parsed to Int
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