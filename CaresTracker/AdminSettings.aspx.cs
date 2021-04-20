using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using CaresTracker.DataAccess.DataAccessors;
using CaresTracker.DataAccess.DataAccessors.RegionAccessors;
using CaresTracker.DataAccess.DataAccessors.OrganizationAccessors;
using CaresTracker.DataAccess.DataAccessors.GenericAccessors;
using CaresTracker.DataAccess.DataAccessors.ServiceAccessors;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.EventTypeAccessors;

namespace CaresTracker
{
    public partial class AdminSettings : System.Web.UI.Page
    {
        DataTable dtOrganizations;

        // the DataTables plugin is very picky about postbacks and storing TemplateField ctrl data
        // make sure any controls expected to cause postbacks are in UpdatePanels
        protected void Page_Load(object sender, EventArgs e)
        {
            // redirect non-Temple Admins
            if (!(Session["User"] as CARESUser).UserType.Equals("T")) { Response.Redirect("./Homepage.aspx"); }

            if (!IsPostBack)
            {
                new List<GridView>() { gvRegions, gvServices, gvHousingDevelopments, gvEventTypes }.ForEach(gv =>
                {
                    gv.DataBound += (object o, EventArgs ev) =>
                    {
                        gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                    };
                });

                // set up Regions
                dtOrganizations = new GetAllOrganizations().ExecuteCommand(); // get orgs for dropdown
                gvRegions.DataSource = new GetAllRegions().ExecuteCommand();
                gvRegions.DataKeyNames = new string[] { "RegionID", "OrganizationID" }; // store hidden ids
                gvRegions.DataBind();

                // set up HousingDevelopments
                gvHousingDevelopments.DataSource = new GetAllDevelopments().ExecuteCommand();
                gvHousingDevelopments.DataKeyNames = new string[] { "DevelopmentID" };
                gvHousingDevelopments.DataBind();

                // set up Services
                gvServices.DataSource = new GetAllServices().ExecuteCommand();
                gvServices.DataKeyNames = new string[] { "ServiceID" };
                gvServices.DataBind();

                // set up EventTypes
                gvEventTypes.DataSource = new GetAllEventTypes().ExecuteCommand();
                gvEventTypes.DataKeyNames = new string[] { "EventTypeID" };
                gvEventTypes.DataBind();
            }
        }

        // sets values for region org ddl
        protected void gvRegions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlOrgs = ((DropDownList)e.Row.FindControl("ddlOrganizations"));

            if (ddlOrgs == null) { return; } // skip column headers
            ddlOrgs.DataSource = dtOrganizations;
            ddlOrgs.DataTextField = "OrganizationName";
            ddlOrgs.DataValueField = "OrganizationID";
            ddlOrgs.DataBind();

            // set this region's assigned organization
            ddlOrgs.SelectedValue = gvRegions.DataKeys[e.Row.RowIndex]["OrganizationID"].ToString(); // null org id evaluates to ""
        }

        protected void btnRegionUpdate_Click(object sender, EventArgs e)
        {
            // get all pairs of regions and orgs
            List<(int regionID, int? orgID)> pairs = new List<(int regionID, int? orgID)>();
            gvRegions.Rows.Cast<GridViewRow>().ToList().ForEach(row =>
            {
                int? orgID = null; // unassigned
                string ddlVal = ((DropDownList)row.Cells[1].FindControl("ddlOrganizations")).SelectedValue;
                if (!string.IsNullOrEmpty(ddlVal)) { orgID = int.Parse(ddlVal); }

                pairs.Add((int.Parse(gvRegions.DataKeys[row.RowIndex]["RegionID"].ToString()), orgID));
            });

            try
            {
                if (new UpdateRegionAssignments(pairs).ExecuteCommand() > 0)
                {
                    // update success
                    Response.Redirect("./AdminSettings.aspx", false);
                }

                lblRegionError.Text = "An unknown error occurred. Please try again later.";
                lblRegionError.Visible = true;
            }
            catch (Exception ex)
            {
                lblRegionError.Text = ex.Message;
                lblRegionError.Visible = true;
            }
        }

        protected void btnDevelopmentUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<(int id, bool isEnabled)> pairs = GetIsEnabledPairs(gvHousingDevelopments, "DevelopmentID", "chkDevelopmentEnabled", 2);
                if (new UpdateRecordIsEnabled("HousingDevelopment", "DevelopmentID", "DevelopmentIsEnabled", pairs).ExecuteCommand() > 0)
                {
                    // update success
                    Response.Redirect("./AdminSettings.aspx", false);
                }

                lblDevelopmentError.Text = "An unknown error occurred. Please try again later.";
                lblDevelopmentError.Visible = true;
            }
            catch (Exception ex)
            {
                lblDevelopmentError.Text = ex.Message;
                lblDevelopmentError.Visible = true;
            }
        }

        protected void btnServiceUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<(int id, bool isEnabled)> pairs = GetIsEnabledPairs(gvServices, "ServiceID", "chkServiceEnabled", 1);
                if (new UpdateRecordIsEnabled("Service", "ServiceID", "ServiceIsEnabled", pairs).ExecuteCommand() > 0)
                {
                    // update success
                    Response.Redirect("./AdminSettings.aspx", false);
                }

                lblServiceError.Text = "An unknown error occurred. Please try again later.";
                lblServiceError.Visible = true;
            }
            catch (Exception ex)
            {
                lblServiceError.Text = ex.Message;
                lblServiceError.Visible = true;
            }
        }

        // gets the values that IsEnabled should be set to for each PK id for the given GridView
        private List<(int id, bool isEnabled)> GetIsEnabledPairs(GridView gv, string idCol, string chkID, int chkIndex)
        {
            List<(int id, bool isEnabled)> pairs = new List<(int id, bool isEnabled)>();
            gv.Rows.Cast<GridViewRow>().ToList().ForEach(row =>
            {
                pairs.Add((int.Parse(gv.DataKeys[row.RowIndex][idCol].ToString()),
                    ((CheckBox)row.Cells[chkIndex].FindControl(chkID)).Checked));
            });

            return pairs;
        }

        /// <summary>
        /// Checks if a value is unique in a GridView.
        /// Use before inserting a new database value.
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="colIndex">The index of the column to check against.</param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private bool IsNewValueUnique(GridView gv, int colIndex, string newValue)
        {
            return !gv.Rows.OfType<GridViewRow>().ToList().Any(row =>
            {
                return row.Cells[colIndex].Text.ToLower().Equals(newValue.ToLower());
            });
        }

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            string strClean = txtServiceName.Text.Trim();
            if (string.IsNullOrEmpty(strClean))
            {
                lblAddServiceError.Text = "Service Name cannot be empty.";
                lblAddServiceError.Visible = true;
                return;
            }

            if (!IsNewValueUnique(gvServices, 0, strClean))
            {
                lblAddServiceError.Text = "This Service already exists.";
                lblAddServiceError.Visible = true;
                return;
            }


            lblAddServiceError.Text = string.Empty;
            lblAddServiceError.Visible = false;

            try
            {
                if (new InsertService(strClean).ExecuteCommand() > 0)
                {
                    // insert success
                    Response.Redirect("./AdminSettings.aspx", false);
                }

                lblAddServiceError.Text = "An unknown error occurred. Please try again later.";
                lblAddServiceError.Visible = true;
            }
            catch (Exception ex)
            {
                lblAddServiceError.Text = ex.Message;
                lblAddServiceError.Visible = true;
            }
        }

        protected void btnEventTypeUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<(int id, bool isEnabled)> pairs = GetIsEnabledPairs(gvEventTypes, "EventTypeID", "chkEventTypeIsEnabled", 1);
                if (new UpdateRecordIsEnabled("EventType", "EventTypeID", "EventTypeIsEnabled", pairs).ExecuteCommand() > 0)
                {
                    // update success
                    Response.Redirect("./AdminSettings.aspx", false);
                }

                lblServiceError.Text = "An unknown error occurred. Please try again later.";
                lblServiceError.Visible = true;
            }
            catch (Exception ex)
            {
                lblServiceError.Text = ex.Message;
                lblServiceError.Visible = true;
            }
        }

        protected void btnAddEventType_Click(object sender, EventArgs e)
        {
            string strClean = txtEventTypeName.Text.Trim();
            if (string.IsNullOrEmpty(strClean))
            {
                lblAddEventTypeError.Text = "Event Type Name cannot be empty.";
                lblAddEventTypeError.Visible = true;
                return;
            }

            if (!IsNewValueUnique(gvEventTypes, 0, strClean))
            {
                lblAddEventTypeError.Text = "This Event Type already exists.";
                lblAddEventTypeError.Visible = true;
                return;
            }

            lblAddEventTypeError.Text = string.Empty;
            lblAddEventTypeError.Visible = false;

            try
            {
                if (new InsertEventType(strClean).ExecuteCommand() > 0)
                {
                    // insert success
                    Response.Redirect("./AdminSettings.aspx", false);
                }

                lblAddEventTypeError.Text = "An unknown error occurred. Please try again later.";
                lblAddEventTypeError.Visible = true;
            }
            catch (Exception ex)
            {
                lblAddEventTypeError.Text = ex.Message;
                lblAddEventTypeError.Visible = true;
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }
    }
}
