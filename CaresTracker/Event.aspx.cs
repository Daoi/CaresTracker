using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;


namespace CaresTracker
{
    public partial class Event : System.Web.UI.Page
    {
        DataModels.Event theEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Event"] != null)
            {
                theEvent = (DataModels.Event)Session["Event"];
            }

            if(!IsPostBack && Session["Event"] != null)
            {
                FillEventInfo();
            }
        }

        public void FillEventInfo()
        {
            txtEventName.Text = theEvent.EventName;
            txtEventType.Text = theEvent.EventType;
            txtLocation.Text = theEvent.EventLocation;
            txtEventDate.Text = theEvent.EventDate;
            txtStartTime.Text = theEvent.EventStartTime;
            txtEndTime.Text = theEvent.EventEndTime;
            txtDescription.Text = theEvent.EventDescription;

            gvCHWList.DataBound += (object o, EventArgs ev) =>
            {
                gvCHWList.HeaderRow.TableSection = TableRowSection.TableHeader;
            };
            gvCHWList.DataSource = theEvent.Hosts;
            gvCHWList.DataBind();

            if (theEvent.Attendees != null && theEvent.Attendees.Count != 0)
            {
                gvResidentList.DataBound += (object o, EventArgs ev) =>
                {
                    gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                gvResidentList.DataSource = theEvent.Attendees;
                gvResidentList.DataBind();
            }

            ddlMainHost.DataSource = theEvent.Hosts;
            ddlMainHost.DataTextField = "FullName";
            ddlMainHost.DataValueField = "UserID";
            ddlMainHost.DataBind();
            ddlMainHost.SelectedIndex = ddlMainHost.Items.IndexOf(ddlMainHost.Items.FindByValue(theEvent.MainHostID.ToString()));
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void lnkEventList_Click(object sender, EventArgs e)
        {
            Response.Redirect("EventList.aspx");
        }

        private void EnableDisableControls()
        {
            string defaultCSS = "h3 border-0 bg-transparent text-dark";
            string textboxCSS = "form-control w-50";
            foreach (Control c in form.Controls)
            {
                if (c is TextBox)
                {
                    TextBox temp = c as TextBox;
                    if (temp.ID == "txtEventName")
                    { 
                        if(temp.CssClass == defaultCSS)
                        {
                            temp.CssClass = temp.CssClass.Replace(defaultCSS, textboxCSS);
                        }
                        else
                        {
                            temp.CssClass = temp.CssClass.Replace(textboxCSS, defaultCSS);
                        }
                    }
                    temp.Enabled = !temp.Enabled;
                }
                else if (c is DropDownList)
                {
                    DropDownList temp = c as DropDownList;
                    temp.Enabled = !temp.Enabled;
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EnableDisableControls();
            btnEdit.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EnableDisableControls();
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            lblError.Visible = false;

            // Create new event to replace old one
            DataModels.Event editedEvent = new DataModels.Event();
            editedEvent.EventName = txtEventName.Text;
            editedEvent.EventDescription = txtDescription.Text;
            editedEvent.EventType = txtEventType.Text;
            editedEvent.EventLocation = txtLocation.Text;
            editedEvent.EventDate = txtEventDate.Text;
            editedEvent.EventStartTime = txtStartTime.Text;
            editedEvent.EventEndTime = txtEndTime.Text;
            editedEvent.MainHostID = Int32.Parse(ddlMainHost.SelectedValue);
            editedEvent.EventID = theEvent.EventID;
            // Update event
            try{
            new UpdateEvent(editedEvent).ExecuteCommand();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EnableDisableControls();
            FillEventInfo();
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        protected void btnViewResident_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int index = row.RowIndex;
            Session["Resident"] = theEvent.Attendees[index];
            Response.Redirect("./ResidentProfile.aspx");
        }

        protected void btnAddResidentAttendees_Click(object sender, EventArgs e)
        {
            Session["Event"] = theEvent;
            Response.Redirect("AddResidentAttendees.aspx");
        }
    }
}