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
        Dictionary<string, Panel> links;
        Resident res;
        Interaction interaction;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Key = lnkbtnID Value = Associated Panel
            links = new Dictionary<string, Panel> { {"residentInfo", pnlResidentInfoForm }, { "residentHealth", pnlResidentHealthForm },
                                                    { "meetingInfo", pnlMeetingInfoForm }, { "otherInfo", pnlOtherForm }, { "services", pnlServicesForm },
                                                    {"housingInfo", pnlHousingInfoForm }
                                                    };

            if (!IsPostBack)
            {
                //Set resident info (first tab) to visible, others to false
                links.Keys.ToList().Where(s => !s.Equals("residentInfo")).ToList().ForEach(s => links[s].Visible = false);
            }

            //initialize form values
            if (Session["Resident"] != null && HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile")) //Should be a new interaction in this case
            {
                FillResidentInfo();
            }
            else if (Session["Interaction"] != null && HttpContext.Current.Request.Url.ToString().Contains("InteractionList"))//Old interaction
            {
                interaction = Session["Interaction"] as Interaction;
                FillResidentInfo();
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

        //Navigation
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



        private void FillResidentInfo()
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
            tbRegion.Text = res.Home.RegionName.ToString();
            tbResidentAddress.Text = res.Home.Address;

            //Disable auto filled controls
            pnlResidentInfoForm.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Enabled = false);
            pnlHousingInfoForm.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Enabled = false);
            ddlHousingType.Enabled = false;
        }

    }
}