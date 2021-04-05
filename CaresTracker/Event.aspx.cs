﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;


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
            txtEventType.Text = theEvent.EventType;
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