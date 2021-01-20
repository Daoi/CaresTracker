using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class AdminInteractionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Interaction> temp = new List<Interaction>();
            Interaction interaction = new Interaction("John", " Doe", "Jane Deer", "1/1/2021", "Phone", "Office", "Resident wanted more information on laundary services.");
            temp.Add(interaction);
            interaction = new Interaction("Sally", "Seashells", "Fake Name", "1/2/2021", "Email", "Office", "Resident was interested in zoom lessons.");
            temp.Add(interaction);
            for (int i=0; i < 10; i++)
            {
                Interaction tempInteraction = new Interaction();
                temp.Add(tempInteraction);
            }

            //Adds table head tag to visual studio html output so gridview format will work DataTables
            gvInteractionList.DataBound += (object o, EventArgs ev) =>
            {
                gvInteractionList.HeaderRow.TableSection = TableRowSection.TableHeader;
            };

            gvInteractionList.DataSource = temp;
            gvInteractionList.DataBind();
        }


        public class Interaction
        {
            public string ResidentFirstName { get; set; }
            public string ResidentLastName { get; set; }

            public string CHWName { get; set; }
            public string DateOfInteraction { get; set; }
            public string MethodOfContact { get; set; }
            public string Location { get; set; }
            public string Notes { get; set; }
            public Interaction() { }
            public Interaction(string firstName, string lastName, string chwname, string dateofinteraction, string methodofcontact, string location, string notes)
            {
                ResidentFirstName = firstName;
                ResidentLastName = lastName;
                CHWName = chwname;
                DateOfInteraction = dateofinteraction;
                MethodOfContact = methodofcontact;
                Location = location;
                Notes = notes;
            }
        }

        protected void gvInteractionList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Server.Transfer("ResidentInteractionForm.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }
    }
}