using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.DevelopmentAccessors;
using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;
using CaresTracker.DataAccess.DataAccessors.OrganizationAccessors;

namespace CaresTracker
{
    public partial class ExportData : System.Web.UI.Page
    {
        CARESUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["User"] as CARESUser;

            // redirect unauthorized users
            if (!(user.UserType.Equals("T") || user.UserType.Equals("A"))) { Response.Redirect("./Homepage.aspx"); }

            if (!IsPostBack)
            {
                // default to developments
                lblReportDomain.Text = "Development: ";
                ddlReportDomain.DataValueField = "DevelopmentID";
                ddlReportDomain.DataTextField = "DevelopmentName";
                ViewState["dtDevelopments"] = new GetDevelopmentsByAdminID().ExecuteCommand(user.UserID);
                ddlReportDomain.DataSource = ViewState["dtDevelopments"] as DataTable;
                ddlReportDomain.DataBind();
            }

            // apply select2 to ddls
            string select2Call = $"setupSelect2();";
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "select2Call", select2Call, true);
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }

        // collect inputs to generate report
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDateStart.Text) || string.IsNullOrEmpty(txtDateEnd.Text))
            {
                lblError.Text = "Please fill out all fields.";
                lblError.Visible = true;
                return;
            }
            else if (!(DateTime.Parse(txtDateStart.Text) <= DateTime.Parse(txtDateEnd.Text)))
            {
                lblError.Text = "End Date cannot be past Start Date.";
                lblError.Visible = true;
                return;
            }

            lblError.Visible = false;

            Session["ReportType"] = ddlReportType.SelectedValue;
            Session["ReportDomainID"] = ddlReportDomain.SelectedValue;
            Session["ReportDomainName"] = ddlReportDomain.SelectedItem.Text;
            Session["ReportStartDate"] = txtDateStart.Text;
            Session["ReportEndDate"] = txtDateEnd.Text;
            Response.Redirect("./Report.aspx");
        }

        // change report domain controls based on report type values
        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportType.SelectedValue.Equals("D"))
            {
                lblReportDomain.Text = "Development: ";
                ddlReportDomain.DataValueField = "DevelopmentID";
                ddlReportDomain.DataTextField = "DevelopmentName";
                if (ViewState["dtDevelopments"] is null)
                {
                    ViewState["dtDevelopments"] = new GetDevelopmentsByAdminID().ExecuteCommand(user.UserID);
                }

                ddlReportDomain.DataSource = ViewState["dtDevelopments"] as DataTable;
            }
            else if (ddlReportType.SelectedValue.Equals("C"))
            {
                lblReportDomain.Text = "Username: ";
                ddlReportDomain.DataValueField = "UserID";
                ddlReportDomain.DataTextField = "Username";
                if (ViewState["dtCHWs"] is null)
                {
                    ViewState["dtCHWs"] = new GetWorkersByUserID().RunCommand(user.UserID);
                }

                ddlReportDomain.DataSource = ViewState["dtCHWs"] as DataTable ;
            }
            else if (ddlReportType.SelectedValue.Equals("O"))
            {
                lblReportDomain.Text = "Organization: ";
                ddlReportDomain.DataValueField = "OrganizationID";
                ddlReportDomain.DataTextField = "OrganizationName";
                if (ViewState["dtOrganizations"] is null)
                {
                    ViewState["dtOrganizations"] = new GetAllOrganizations().ExecuteCommand();
                }

                // partner admins can only run reports for their org
                if (!user.UserType.Equals("T"))
                {
                    ViewState["dtOrganizations"] = (ViewState["dtOrganizations"] as DataTable)
                        .AsEnumerable().Where(row => int.Parse(row["OrganizationID"].ToString()) == user.OrganizationID)
                        .CopyToDataTable();
                }

                ddlReportDomain.DataSource = ViewState["dtOrganizations"] as DataTable;
            }

            ddlReportDomain.DataBind();
        }
    }
}
