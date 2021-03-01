using CapstoneUI.DataModels;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;

namespace CapstoneUI
{
    public partial class AdminInteractionList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.Url.ToString().Contains("ResidentProfile"))
                {
                    Resident res = Session["Resident"] as Resident;
                    hfResidentDetails.Value = $"{res.FirstName} {res.LastName} {res.Home.Address}";
                }

                GetAllInteractions getAllInteractions = new GetAllInteractions();
                DataTable ds = getAllInteractions.ExecuteCommand();
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

        public string CHWName(object user)
        {
            int id = (int)user;
            GetCHWByID chw = new GetCHWByID();
            DataRow row = chw.RunCommand(id).Rows[0];
            return row["FirstName"].ToString() + " " + row["LastName"].ToString();
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
            Response.Redirect("Homepage.aspx");
        }

    }
}