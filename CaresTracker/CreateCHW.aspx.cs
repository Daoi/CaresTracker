using System;
using System.Collections.Generic;
using CaresTracker.Utilities;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.CARESUserAccessors;

namespace CaresTracker
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

        public bool ValidateFields()
        {
            if (Validation.IsEmpty(txtUsername.Text) || Validation.IsEmpty(txtFirstName.Text) || Validation.IsEmpty(txtLastName.Text) ||
            Validation.IsEmpty(txtEmail.Text) || Validation.IsEmpty(txtPhoneNumber.Text))
            {
                lblError.Text = "Fill out all fields";
                return false;
            }

            if (!Validation.IsAlphanumeric(txtUsername.Text))
            {
                lblError.Text = "Incorrect input type in username field";
                return false;
            }

            if (!Validation.IsLetters(txtFirstName.Text) || !Validation.IsLetters(txtLastName.Text))
            {
                lblError.Text = "Sorry we can only accept letters for first and last names";
                return false;
            }

            if (!Validation.IsEmail(txtEmail.Text))
            {
                lblError.Text = "Please enter a valid email address";
                return false;
            }

            if (!Validation.IsPhoneNumber(txtPhoneNumber.Text).isValid)
            {
                lblError.Text = "Please enter a valid phone number";
                return false;
            }

            if (user.UserType == "T" && ddlOrganization.SelectedValue == "default")
            {
                lblError.Text = "Make sure to select an organization";
                return false;
            }

            if (ddlAccountType.SelectedValue == "default")
            {
                lblError.Text = "Make sure to select an account type";
                return false;
            }

            return true;
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                List<string> values = new List<string>();
                values.Add(txtUsername.Text);
                values.Add(txtFirstName.Text);
                values.Add(txtLastName.Text);
                values.Add(txtEmail.Text);
                string phoneNumber = Validation.IsPhoneNumber(txtPhoneNumber.Text).strPhone;
                values.Add(phoneNumber);
                string signedInUserName = user.Username;
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
                        UserFirstName = txtFirstName.Text,
                        UserLastName = txtLastName.Text,
                        UserEmail = txtEmail.Text,
                        UserPhoneNumber = phoneNumber,
                        UserStatus = "Active",
                        UserType = ddlAccountType.SelectedValue,
                        OrganizationName = ddlOrganization.SelectedValue == "default" ?
                            user.OrganizationName : ddlOrganization.SelectedItem.Text,
                        LastLogin = "N/A"
                    };

                    Response.Redirect("CHWManagement.aspx");
                }
                catch (Amazon.CognitoIdentityProvider.Model.UsernameExistsException ex) 
                {
                    lblError.Text = "That username already exists please pick another one";
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.ToString();
                }
            }
        }
    }
}
