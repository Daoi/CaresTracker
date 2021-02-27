﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataModels;
using CapstoneUI.Utilities;


namespace CapstoneUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            if (Validation.IsEmpty(txtUsername.Text) || Validation.IsEmpty(txtPassword.Text))
            {
                lblError.Text = "Enter your Username and Password.";
                return;
            }

            // AWS Cognito Login
            AWSCognitoManager man = new AWSCognitoManager();

            try
            {
                var authResponse = await man.SignInAsync(txtUsername.Text, txtPassword.Text);

                if (authResponse != null)
                {
                    lblError.Text = "";

                    // get user data from db
                    GetUser accessor = new GetUser();
                    Session["User"] = new CARESUser(accessor.RunCommand(txtUsername.Text).Rows[0]);
                    Session["CognitoManager"] = man;
                    Response.Redirect("./Homepage.aspx", false);
                }
                else
                {
                    lblError.Text = "An unknown error occurred. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}