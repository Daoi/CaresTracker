using System;
using System.Collections.Generic;
using CapstoneUI.Utilities;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;

namespace CapstoneUI
{
    public partial class CreateCHW : System.Web.UI.Page
    {
        CARESUser user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Session["User"] as CARESUser;
            if (user.UserType == "T")
            {
                ddlOrganizationDiv.Visible = true;
            }
            else
            {
                ddlAccountType.Items[1].Enabled = false;
                ddlOrganizationDiv.Visible = false;
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>();
            values.Add(txtUsername.Text);
            values.Add(txtFirstName.Text);
            values.Add(txtLastName.Text);
            values.Add(txtEmail.Text);

            string phoneNumber = "+1" + txtPhoneNumber.Text;
            values.Add(phoneNumber);
            string signedInUserName = ((CARESUser)Session["User"]).Username;

            values.Add(ddlAccountType.SelectedValue);

            if (user.UserType == "T")
            {
                values.Add(ddlOrganization.SelectedValue);
            }
            else
            {
                values.Add(user.OrganizationID.ToString());
            }

            values.Add(signedInUserName);

            AWSCognitoManager man = (AWSCognitoManager)Session["CognitoManager"];

            try
            {
                if (ddlAccountType.SelectedValue == "A" || ddlAccountType.SelectedValue == "S")
                {
                    var res = await man.CreateUserAsync(txtUsername.Text, txtEmail.Text, 1);

                    if (res != null)
                    {
                        CHWWriter newCHW = new CHWWriter(values);
                        newCHW.ExecuteCommand();
                    }
                    else
                    {
                        throw new TimeoutException("An unknown error occurred. Please try again later.");
                    }
                }
                else if (ddlAccountType.SelectedValue == "C")
                {
                    var res = await man.CreateUserAsync(txtUsername.Text, txtEmail.Text, 0);

                    if (res != null)
                    {
                        CHWWriter newCHW = new CHWWriter(values);
                        newCHW.ExecuteCommand();
                    }
                    else
                    {
                        throw new TimeoutException("An unknown error occurred. Please try again later.");
                    }
                }

                // creation successful, redirect
                Session["Worker"] = new CARESUser()
                {
                    Username = txtUsername.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    UserEmail = txtEmail.Text,
                    UserPhoneNumber = txtPhoneNumber.Text,
                    UserStatus = "Active",
                    UserType = ddlIsSupervisor.SelectedValue == "yes" ? "A" : "C",
                    RegionID = int.Parse(ddlRegion.SelectedValue),
                    Supervisor = ddlSupervisor.SelectedValue
                };
                Response.Redirect("CHWManagement.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.ToString() + ")</script>");
            }
        }
    }
}
