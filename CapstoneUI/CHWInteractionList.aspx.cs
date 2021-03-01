using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;

namespace CapstoneUI
{
    public partial class CHWInteractionList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CARESUser user = Session["User"] as CARESUser;
                GetAllInteractions getAllInteractions = new GetAllInteractions();
                DataTable ds = getAllInteractions.ExecuteCommand();
                for (int i = ds.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow record = ds.Rows[i];
                    if (int.Parse(record["HealthWorkerID"].ToString()) != user.UserID)
                    {
                        ds.Rows[i].Delete();
                    }
                }
                gvInteractionList.DataSource = ds;
            }

            if (gvInteractionList.Rows.Count != 0)
            {
                //Adds table head tag to visual studio html output so gridview format will work DataTables
                gvInteractionList.DataBound += (object o, EventArgs ev) =>
                {
                    gvInteractionList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };
            }

            gvInteractionList.DataBind();
        }

        public List<string> ResidentName(object resident)
        {
            List<string> name = new List<string>();
            int id = (int)resident;
            GetResidentByID res = new GetResidentByID();
            DataRow row = res.RunCommand(id).Rows[0];
            name.Add(row["FirstName"].ToString());
            name.Add(row["LastName"].ToString());
            return name;
        }

        public class Interaction
        {
            public string ResidentFirstName { get; set; }
            public string ResidentLastName { get; set; }
            public string DateOfInteraction { get; set; }
            public string MethodOfContact { get; set; }
            public string Location { get; set; }
            public string Notes { get; set; }
            public Interaction() { }
            public Interaction(string firstName, string lastName, string dateofinteraction, string methodofcontact, string location, string notes)
            {
                ResidentFirstName = firstName;
                ResidentLastName = lastName;
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