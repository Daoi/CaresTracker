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
        DataTable dtRegions;

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
                dtRegions = new GetAllRegions().ExecuteCommand();
                gvRegions.DataSource = dtRegions;
                gvRegions.DataBind();

                gvHousingDevelopments.DataSource = new GetAllDevelopments().ExecuteCommand();
                gvHousingDevelopments.DataBind();

                gvServices.DataSource = new GetAllServices().ExecuteCommand();
                gvServices.DataBind();
            }
        }

        // sets values for org ddl
        protected void gvRegions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddlOrgs = ((DropDownList)e.Row.FindControl("ddlOrganizations"));

            if (ddlOrgs == null) { return; } // skip column headers
            ddlOrgs.DataSource = dtOrganizations;
            ddlOrgs.DataTextField = "OrganizationName";
            ddlOrgs.DataValueField = "OrganizationID";
            ddlOrgs.DataBind();

            // find this region's assigned organization
            ddlOrgs.SelectedValue = dtRegions.Rows.Cast<DataRow>()
                .First(r => r.Field<string>("RegionName") == e.Row.Cells[0].Text)
                .Field<int?>("OrganizationID").ToString(); // null org id evaluates to ""
        }
    }
}
