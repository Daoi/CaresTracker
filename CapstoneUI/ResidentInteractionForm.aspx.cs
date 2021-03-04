using CapstoneUI.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class ResidentInteractionForm : System.Web.UI.Page
    {
        Dictionary<string, HtmlGenericControl> links;
        Resident res;
        Interaction interaction;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Key = ID Value = linkbutton
            links = new Dictionary<string, HtmlGenericControl> { {"residentInfo", residentInfoForm }, { "residentHealth", residentHealthForm },
                                                                 { "meetingInfo", meetingInfoForm }, { "otherInfo", otherForm }, { "services", servicesForm },
                                                                  {"housingInfo", housingInfoForm }
                                                                };
            if (!IsPostBack)
            {
                //Set resident info (first tab) to visible, others to false
                links.Keys.ToList().Where(s => !s.Equals("residentInfo")).ToList().ForEach(s => links[s].Visible = false);
            }

            //initialize form values
            if (Session["Resident"] != null && HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile")) //Should be a new interaction in this case
            {

                res = Session["Resident"] as Resident;

                DateTime today = DateTime.Today;
                DateTime bday = DateTime.Parse(res.DateOfBirth);
                int age = today.Date.Year - bday.Date.Year;

                //Resident(First Tab)
                tbFirstName.Text = res.FirstName;
                tbLastName.Text = res.LastName;
                tbAge.Text = bday.Date > today.AddYears(-age) ? (age--).ToString() : age.ToString();
                tbGender.Text = res.Gender;
                tbPhone.Text = res.ResidentPhoneNumber;
                tbEmail.Text = res.ResidentEmail;
                tbDoC.Text = today.ToString("yyyy-MM-dd");
                //Housing Info(Second Tab)
                //tbResidenceOccupants.Text = res.Home.NumOfOccupants; NOT IMPLEMENTED ON HOUSE/RESIDENT YET. Change TB NAME
                if(res.Home.DevelopmentID == -1)
                {
                    ddlHousingType.SelectedValue = "HCV";
                    tbDevelopmentName.Visible = false;
                }
                else
                {
                    ddlHousingType.SelectedValue = "Development";
                    tbDevelopmentName.Text = res.HousingDevelopment.DevelopmentName;
                }
                tbRegion.Text = res.Home.RegionID.ToString(); //Need SQL query
                tbResidentAddress.Text = res.Home.Address;


            }
            else if (Session["Interaction"] != null && HttpContext.Current.Request.Url.ToString().Contains("InteractionList"))//Old interaction
            {
                interaction = Session["Interaction"] as Interaction;

                //Maybe unneeded?
                if (Session["Resident"] == null)
                {
                    //Error handling
                }
                else
                {
                    //Resident Object matches Interaction resident Id
                }

            }
         


        }

        protected void formNav_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            links[link.ID].Visible = true;

            links.Keys.ToList().Where(key => !key.Equals(link.ID)).ToList().ForEach(s => links[s].Visible = false);

        }

        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void lnkBtnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Homepage.aspx");
        }
    }
}