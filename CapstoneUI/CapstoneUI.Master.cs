using System;
using System.Web;

namespace CapstoneUI
{
    enum AcctType
    {
        CHW,
        Admin
    }
    public partial class Site1 : System.Web.UI.MasterPage
    {


        bool loggedIn;
        int userType;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["AccountType"] == null || Session["LoginStatus"] == null)
            {
                Server.Transfer("~/Login.aspx");
            }
            else
            {
                userType = (int)Session["AccountType"];
                loggedIn = (bool)Session["LoginStatus"];
            }

            if (loggedIn)
            {
                if (userType == (int) AcctType.CHW )
                {
                    lnkLogin.Visible = false;
                    lnkRegister.Visible = false;
                    lnkImport.Visible = true;
                    lnkImport.Enabled = true;
                    lnkInteractionForm.Enabled = true;
                    lnkInteractionForm.Visible = true;

                }
                else if (userType == (int)AcctType.Admin)
                {
                    lnkLogin.Visible = false;
                    lnkRegister.Visible = false;
                    lnkImport.Visible = true;
                    lnkManagement.Visible = true;
                    lnkData.Visible = true;
                    lnkImport.Enabled = true;
                    lnkManagement.Enabled = true;
                    lnkData.Enabled = true;
                    lnkInteractionForm.Enabled = true;
                    lnkInteractionForm.Visible = true;

                }

                //Change this
                //lblUserInfo.Text = $"Welcome, {Session["UserName"].ToString()}";
                //lblUserInfo.Visible = true;
                //lblUserInfo.Enabled = true;
                lnkBtnLogout.Visible = true;
                lnkBtnLogout.Enabled = true;
            }
            else
            {
                Server.Transfer("~/Login.aspx");
            }

        }

        protected void lnkBtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Server.Transfer("~/Login.aspx");
        }

    }
}