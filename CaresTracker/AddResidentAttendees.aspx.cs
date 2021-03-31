using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors;
using System.Data;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.ResidentAccessors;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;

namespace CaresTracker
{
    public partial class AddResidentAttendees : System.Web.UI.Page
    {
        DataModels.Event theEvent;
        List<Resident> attendees;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Event"] != null)
            {
                theEvent = (DataModels.Event)Session["Event"];
            }

            if (Session["Attendees"] != null)
            {
                attendees = (List<Resident>)Session["Attendees"];
            }

            if (!IsPostBack)
            {
                Session["Attendees"] = null;
                attendees = new List<Resident>();
                Session["Attendees"] = attendees;

                DataTable dt;

                if (Session["ResidentList"] != null)
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
                gvResidentList.DataBind();
            }

            gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        protected void btnAddResident_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            DataRow dr = (Session["ResidentList"] as DataTable).Rows[row.DataItemIndex];
            Resident res = new Resident(dr);

            if (!attendees.Any(r => r.ResidentID == res.ResidentID) && !theEvent.Attendees.Any(r => r.ResidentID == res.ResidentID))
            {
                lblError.Visible = false;
                attendees.Add(res);
                rptAttendees.DataSource = attendees;
                rptAttendees.DataBind();
                Session["Attendees"] = attendees;
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Resident already exists in list of attendees";
            }
        }

        protected void btnResidents_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int id = int.Parse(btn.CommandArgument);
            attendees.RemoveAll(r => r.ResidentID == id);
            Session["Attendees"] = attendees;
            rptAttendees.DataSource = attendees;
            rptAttendees.DataBind();
        }

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