using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.Utilities;
using CapstoneUI.DataAccess.DataAccessors;
using System.Data;

namespace CapstoneUI
{
    public partial class CreateCHW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                divSelectSupervisor.Visible = false;
                GetAllCHW chws = new GetAllCHW();
                DataTable ds = chws.RunCommand();
                for(int i = ds.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow record = ds.Rows[i];
                    if (record["UserType"].ToString() != "A")
                    {
                        ds.Rows[i].Delete();
                    }
                }
                ddlSupervisor.DataSource = ds;
                ddlSupervisor.DataTextField = "FirstName";
                ddlSupervisor.DataValueField = "UserID";
                ddlSupervisor.DataBind();
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            //validation
            List<string> values = new List<string>();
            values.Add(txtUsername.Text);
            values.Add(txtFirstName.Text);
            values.Add(txtLastName.Text);
            values.Add(txtEmail.Text);

            string phoneNumber = "+1" + txtPhoneNumber.Text;
            values.Add(phoneNumber);
            string signedInUserName = Session["UserName"].ToString();

            AWSCognitoManager man = (AWSCognitoManager)Session["CognitoManager"];

            try
            {
                if (ddlIsSupervisor.SelectedValue == "yes")
                {
                    var res = await man.CreateUserAsync(txtUsername.Text, txtEmail.Text, 0);

                    if (res != null)
                    {
                        values.Add("A");
                        values.Add(ddlRegion.SelectedValue);
                        values.Add(signedInUserName);
                        values.Add(null);
                        CHWWriter newCHW = new CHWWriter(values);
                        newCHW.ExecuteCommand();
                        Response.Write("<script>alert('Admin/Supervisor inserted successfully.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('An unknown error occurred. Please try again later.')</script>");
                    }
                }
                else
                {
                    var res = await man.CreateUserAsync(txtUsername.Text, txtEmail.Text, 1);

                    if (res != null)
                    {
                        values.Add("C");
                        values.Add(ddlRegion.SelectedValue);
                        values.Add(signedInUserName);
                        values.Add(ddlSupervisor.SelectedValue);
                        CHWWriter newCHW = new CHWWriter(values);
                        newCHW.ExecuteCommand();
                        Response.Write("<script>alert('CHW inserted successfully.')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('An unknown error occurred. Please try again later.')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.ToString() + ")</script>");
            }
        }

        protected void ddlIsSupervisor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlIsSupervisor.SelectedValue == "no")
            {
                divSelectSupervisor.Visible = true;
            }
            else
            {
                divSelectSupervisor.Visible = false;
            }
        }
    }
}