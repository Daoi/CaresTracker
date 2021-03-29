using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess.DataAccessors.RegionAccessors;
using CapstoneUI.DataAccess.DataAccessors.OrganizationAccessors;
using CapstoneUI.DataAccess.DataAccessors.GenericAccessors;
using CapstoneUI.DataAccess.DataAccessors.ServiceAccessors;

namespace CapstoneUI
{
    public partial class AdminSettings : System.Web.UI.Page
    {
        DataTable dtOrganizations;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                new List<GridView>() { gvRegions, gvServices, gvHousingDevelopments }.ForEach(gv =>
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

                // failed
            }
            catch (Exception ex)
            {

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
            }
            catch (Exception ex)
            {

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
            }
            catch (Exception ex)
            {

            }
        }

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

        protected void btnAddService_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtServiceName.Text))
            {
                lblAddServiceError.Text = "Service Name cannot be empty.";
                lblAddServiceError.Visible = true;
                return;
            }

            lblAddServiceError.Text = string.Empty;
            lblAddServiceError.Visible = false;

            try
            {
                if (new InsertService(txtServiceName.Text).ExecuteCommand() > 0)
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
    }
}
