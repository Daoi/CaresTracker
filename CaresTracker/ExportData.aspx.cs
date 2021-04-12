using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.DevelopmentAccessors;

namespace CaresTracker
{
    public partial class ExportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CARESUser user = Session["User"] as CARESUser;

            // redirect unauthorized users
            if (!(user.UserType.Equals("T") || user.UserType.Equals("A"))) { Response.Redirect("./Homepage.aspx"); }

            if (!IsPostBack)
            {
                // get list of developments corresponding to this admin user
                ddlHousingDevelopment.DataSource = new GetDevelopmentsByAdminID().ExecuteCommand(user.UserID);
                ddlHousingDevelopment.DataBind();
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }

        // collect inputs to generate report
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDateInitial.Text) || string.IsNullOrEmpty(txtDateFinal.Text))
            {
                lblError.Text = "Please fill out all fields.";
                lblError.Visible = true;
                return;
            }
            else if (!(DateTime.Parse(txtDateInitial.Text) <= DateTime.Parse(txtDateFinal.Text)))
            {
                lblError.Text = "End Date cannot be past Start Date.";
                lblError.Visible = true;
                return;
            }

            lblError.Visible = false;
            Session["ReportDevelopmentID"] = ddlHousingDevelopment.SelectedValue;
            Session["ReportStartDate"] = txtDateInitial.Text;
            Session["ReportEndDate"] = txtDateFinal.Text;
            Response.Redirect("./Report.aspx");
        }
    }
}
