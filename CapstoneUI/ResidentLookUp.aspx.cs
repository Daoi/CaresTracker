using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class ResidentLookUp : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Resident> temp = new List<Resident>();
                Resident interaction = new Resident("John", "Doe", "35", "111 Philadelphia Street", "West Philadelphia", "Last Covid-19 Test Result: Negative (1/1/2021)");
                temp.Add(interaction);
                for (int i = 0; i < 10; i++)
                {
                    Resident tempPatient = new Resident();
                    temp.Add(tempPatient);
                }

                gvResidentList.DataBound += (object o, EventArgs ev) =>
                {
                    gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };
                Button btnCreateNewResident = new Button()
                {
                    Text = "Create New Resident",
                    CssClass = "btn btn-primary",
                    CommandName = "CreateNewResident"
                };


                gvResidentList.DataSource = temp;
                gvResidentList.DataBind();
            }
        }

        public class Resident
        {
            public string ResidentFirstName { get; set; }
            public string ResidentLastName { get; set; }

            public string ResidentAge { get; set; }
            public string ResidentAddress { get; set; }
            public string ResidentRegion { get; set; }
            public string Notes { get; set; }
            public Resident() { }
            public Resident(string firstName, string lastName, string age, string address, string location, string notes)
            {
                ResidentFirstName = firstName;
                ResidentLastName = lastName;
                ResidentAge = age;
                ResidentAddress = address;
                ResidentRegion = location;
                Notes = notes;
            }
        }

        protected void gvResidentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Server.Transfer("ResidentProfile.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }


        protected void NewResident_Click(object sender, EventArgs e)
        {
            
            Response.Redirect($"CreateResident.aspx?name={hfSearchInput.Value}");
        }
    }
}