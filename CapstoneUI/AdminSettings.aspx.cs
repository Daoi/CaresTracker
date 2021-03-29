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

                // get orgs for dropdown
                dtOrganizations = new GetAllOrganizations().ExecuteCommand();
                gvRegions.DataSource = new GetAllRegions().ExecuteCommand();

                // store hidden region id
                gvRegions.DataKeyNames = new string[] { "RegionID", "OrganizationID" };
                gvRegions.DataBind();

                gvHousingDevelopments.DataSource = new GetAllDevelopments().ExecuteCommand();
                gvHousingDevelopments.DataBind();

                gvServices.DataSource = new GetAllServices().ExecuteCommand();
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
    }
}
