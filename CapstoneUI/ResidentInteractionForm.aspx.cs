using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;
using CapstoneUI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class ResidentInteractionForm : Page
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
                cblServices.DataSource = new GetAllServices().ExecuteCommand();
                cblServices.DataValueField = "ServiceID";
                cblServices.DataTextField = "ServiceName";
                cblServices.DataBind();
                divOldInteractionServices.Visible = false;

            }

            //initialize form values
            if (Session["Resident"] != null && HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile")) //Should be a new interaction in this case
            {
                FillResidentInfo();
            }
            else if (Session["Interaction"] != null && HttpContext.Current.Request.Url.ToString().Contains("InteractionList"))//Old interaction
            {
                FillResidentInfo();
                FillInteractionInfo();
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


        private void FillInteractionInfo()
        {
            interaction = Session["Interaction"] as Interaction;
            //Meeting Information
            ddlMeetingType.SelectedValue = interaction.MethodOfContact;
            tbLocation.Text = interaction.LocationOfContact;

            //Resident Health
            List<CheckBox> formSymptoms = pnlResidentHealthForm.Controls.OfType<CheckBox>().ToList(); //Get all Symptom Checkboxes
            List<string> interactionSymptoms = new List<string>();  //Get all symptom names in interaction 
            interaction.Symptoms.ForEach(s => interactionSymptoms.Add(s.SymptomName));
            formSymptoms //Set checkboxes to checked
                .Where(cb => interactionSymptoms
                .Contains(cb.Text))
                .ToList()
                .ForEach(cb => cb.Checked = true);

            tbSymptomDates.Text = TextModeDateFormatter.Format(interaction.SymptomStartDate);

            if(interaction.COVIDTestResult.Equals("No Recent Test"))
            {
                tbTestingLocation.Text = "N/A";
                ddlTestResult.SelectedValue = "No Recent Test";
            }
            else
            {
                tbTestingLocation.Text = interaction.COVIDTestLocation;
                ddlTestResult.SelectedValue = interaction.COVIDTestResult;
            }

            //Services
            divOldInteractionServices.Visible = true;
            divNewInteractionServices.Visible = false;

            List<Service> interactionServices = interaction.RequestedServices.Concat(interaction.CompletedServices).ToList(); //Completed and Uncompleted services
            cblCompletedServices.DataSource = interactionServices;
            cblCompletedServices.DataTextField = "ServiceName";
            cblCompletedServices.DataValueField = "ServiceID";
            cblCompletedServices.DataBind();

            List<string> interactionCompletedServices = new List<string>();
            interaction.CompletedServices.ForEach(s => interactionCompletedServices.Add(s.ServiceName));

            cblCompletedServices.Items.Cast<ListItem>().ToList()
            .Where(li => interactionCompletedServices.Contains(li.Text))
            .ToList()
            .ForEach(li => li.Selected = true);

            //Action Plan
            nextSteps.InnerText = interaction.ActionPlan;
            
                
        }

    }
}