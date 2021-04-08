using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataAccess.DataAccessors.EventAccessors;
using CaresTracker.DataAccess.DataAccessors.EventTypeAccessors;
using CaresTracker.Utilities;

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
                gvCHWList.DataSource = theEvent.Hosts;
                gvCHWList.DataBind();
                gvResidentList.DataSource = theEvent.Attendees;
                gvResidentList.DataBind();
            }

            gvCHWList.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (theEvent.Attendees != null && theEvent.Attendees.Count != 0)
            {
                gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        public void FillEventInfo()
        {
            txtEventName.Text = theEvent.EventName;

            DataTable dtEventTypes = new GetAllEventTypes().ExecuteCommand()
                    .AsEnumerable()
                    .Where(row => Convert.ToSByte(row["EventTypeIsEnabled"]) == 1)
                    .CopyToDataTable();
            ddlEventType.DataSource = dtEventTypes;
            ddlEventType.DataTextField = "EventTypeName";
            ddlEventType.DataValueField = "EventTypeID";
            ddlEventType.DataBind();
            ddlEventType.SelectedIndex = ddlEventType.Items.IndexOf(ddlEventType.Items.FindByValue(theEvent.EventTypeID.ToString()));

            txtLocation.Text = theEvent.EventLocation;
            txtEventDate.Text = theEvent.EventDate;
            txtStartTime.Text = theEvent.EventStartTime;
            txtEndTime.Text = theEvent.EventEndTime;
            txtDescription.Text = theEvent.EventDescription;

            ddlMainHost.DataSource = theEvent.Hosts;
            ddlMainHost.DataTextField = "FullName";
            ddlMainHost.DataValueField = "UserID";
            ddlMainHost.DataBind();
            ddlMainHost.SelectedIndex = ddlMainHost.Items.IndexOf(ddlMainHost.Items.FindByValue(theEvent.MainHostID.ToString()));
            txtMainHostEmail.Text = theEvent.Hosts.Find(host => host.UserID == theEvent.MainHostID).UserEmail;
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
                    if (temp.ID == "txtMainHostEmail") { continue; }
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
            if (ValidateFields())
            {
                // Create new event to replace old one
                DataModels.Event editedEvent = new DataModels.Event();
                editedEvent.EventName = txtEventName.Text;
                editedEvent.EventDescription = txtDescription.Text;
                editedEvent.EventTypeID = int.Parse(ddlEventType.SelectedValue);
                editedEvent.EventLocation = txtLocation.Text;
                editedEvent.EventDate = txtEventDate.Text;
                editedEvent.EventStartTime = txtStartTime.Text;
                editedEvent.EventEndTime = txtEndTime.Text;
                editedEvent.MainHostID = Int32.Parse(ddlMainHost.SelectedValue);
                editedEvent.EventID = theEvent.EventID;
                // Update event
                try
                {
                    new UpdateEvent(editedEvent).ExecuteCommand();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }

                EnableDisableControls();
                btnEdit.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
                lblError.Visible = false;
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

        protected void ddlMainHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            int newMainHostID = Int32.Parse(ddlMainHost.SelectedValue);
            txtMainHostEmail.Text = theEvent.Hosts.Find(host => host.UserID == newMainHostID).UserEmail;
        }

        public bool ValidateFields()
        {
            if (Validation.IsEmpty(txtEventName.Text) || Validation.IsEmpty(txtLocation.Text) || Validation.IsEmpty(txtEventDate.Text) ||
            Validation.IsEmpty(txtStartTime.Text) || Validation.IsEmpty(txtEndTime.Text) || Validation.IsEmpty(txtDescription.Text))
            {
                lblError.Visible = true;
                lblError.Text = "Fill out all fields";
                return false;
            }

            if (!Validation.IsAlphanumeric(txtEventName.Text) || !Validation.IsAlphanumeric(txtLocation.Text))
            {
                lblError.Visible = true;
                lblError.Text = "Can only accept alphanumeric characters(a-z, 0-9) for event name and location";
                return false;
            }

            DateTime startTime = DateTime.Parse(txtStartTime.Text);
            DateTime endTime = DateTime.Parse(txtEndTime.Text);
            if (DateTime.Compare(endTime, startTime) < 0 || DateTime.Compare(startTime, endTime) == 0)
            {
                lblError.Visible = true;
                lblError.Text = "Make sure that start and end time are correct";
                return false;
            }

            lblError.Visible = false;
            return true;
        }
    }
}