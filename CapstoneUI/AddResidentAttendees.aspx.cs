using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;
using System.Data;
using CapstoneUI.DataModels;

namespace CapstoneUI
{
    public partial class AddResidentAttendees : System.Web.UI.Page
    {
        DataModels.Event theEvent;
        List<Resident> attendees;

        protected void Page_Load(object sender, EventArgs e)
        {
            // on first page load empty temporary attendees list and bind resident list
            if (!IsPostBack)
            {
                Session["Attendees"] = null;
                BindResidents();
            }
            // grab event object from session storage
            if (Session["Event"] != null)
            {
                theEvent = (DataModels.Event)Session["Event"];
            }
            // persist attendees list across async postbacks or instantiate for first time
            if (Session["Attendees"] != null)
            {
                attendees = (List<Resident>)Session["Attendees"];
            }
            else
            {
                attendees = new List<Resident>();
            }
        }

        public void BindResidents()
        {
            DataTable dt;
            // check if residentlist in session already exists if not create it   
            if(Session["ResidentList"] != null)
            {
                dt = (DataTable)Session["ResidentList"];
            }
            else
            {
                GetAllResident getAllResident = new GetAllResident();
                dt = getAllResident.RunCommand();
                Session["ResidentList"] = dt;
            }

            gvResidentList.DataSource = dt;

            gvResidentList.DataBound += (object o, EventArgs ev) =>
            {
                gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
            };

            gvResidentList.DataBind();
        }

        protected void btnAddResident_Click(object sender, EventArgs e)
        {
            // grab the resident object from the bound control
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            DataRow dr = (Session["ResidentList"] as DataTable).Rows[row.DataItemIndex];
            Resident res = new Resident(dr);

            // tried list.contain and list.find both didnt work
            // query the temporary attendee list and the event attendees list to prevent duplicates
            if (attendees.FindIndex(r => r.ResidentID == res.ResidentID) == -1 && 
                theEvent.Attendees.FindIndex(r => r.ResidentID == res.ResidentID) == -1)
            {
                // persist attendees list across async postbacks
                // make error label invisible if triggered
                lblError.Visible = false;
                attendees.Add(res);
                Session["Attendees"] = attendees;
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Resident already exists in list of attendees";
            }
            //had to bind resident list each time so that datatable controls still exist across postbacks
            BindResidents();
            // update the repeater with the new attendee
            rptAttendees.DataSource = attendees;
            rptAttendees.DataBind();
        }

        // attach a postbacktrigger to each button in the gridview
        protected void gvResidentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button addResident = e.Row.FindControl("btnAddResident") as Button;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(addResident);
            }
        }

        // remove resident from temp list
        protected void btnResidents_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int id = int.Parse(btn.CommandArgument);
            attendees.RemoveAt(attendees.FindIndex(r => r.ResidentID == id));
            Session["Attendees"] = attendees;
            rptAttendees.DataSource = attendees;
            rptAttendees.DataBind();
        }

        // combine temp list with event attendees list
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(attendees.Count != 0)
            {
                theEvent.Attendees.AddRange(attendees);
                CTextWriter writer = new CTextWriter(AddAttendees.WriteSQL(theEvent, attendees));
                int ret = writer.ExecuteCommand();
                if(ret > 0)
                {
                    Session["Event"] = theEvent;
                    Response.Redirect("Event.aspx");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Select at least one Resident to add to Event";
                BindResidents();
            }
        }

        protected void lnkEvent_Click(object sender, EventArgs e)
        {
            Response.Redirect("Event.aspx");
        }

        protected void lnkEventList_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventList.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

    }
}