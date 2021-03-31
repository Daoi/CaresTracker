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
        DataTable dtResidents;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Event"] != null)
            {
                theEvent = (DataModels.Event)Session["Event"];
            }

            if (!IsPostBack)
            {
                if (Session["ResidentList"] != null)
                {
                    dtResidents = (DataTable)Session["ResidentList"];
                }
                else
                {
                    dtResidents = new GetAllResident().RunCommand();
                }

                gvResidentList.DataSource = dtResidents;
                Session["ResidentList"] = dtResidents;

                gvResidentList.DataBind();
            }

            gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
            dtResidents = Session["ResidentList"] as DataTable;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<Resident> attendees = new List<Resident>();
            foreach(GridViewRow row in gvResidentList.Rows)
            {
                CheckBox chk = row.FindControl("chkAddResident") as CheckBox;
                if (chk.Checked)
                {
                    DataRow dr = dtResidents.Rows[row.RowIndex];
                    Resident res = new Resident(dr);
                    if(!theEvent.Attendees.Any(r => r.ResidentID == res.ResidentID))
                    {
                        attendees.Add(res);
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "One or more residents is already in list of attendees";
                        return;
                    }
                }
            }

            if(attendees.Count != 0)
            {
                theEvent.Attendees.AddRange(attendees);
                CTextWriter writer = new CTextWriter(AddAttendees.WriteSQL(theEvent, attendees));
                int ret = writer.ExecuteCommand();
                if (ret > 0)
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