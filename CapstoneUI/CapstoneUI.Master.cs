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
                lnkImport.Visible = true;
                lnkImport.Enabled = true;
                lnkInteractionForm.Enabled = true;
                lnkInteractionForm.Visible = true;

            }
            else
            {
                lnkImport.Visible = true;
                lnkManagement.Visible = true;
                lnkData.Visible = true;
                lnkImport.Enabled = true;
                lnkManagement.Enabled = true;
                lnkData.Enabled = true;
                lnkInteractionForm.Enabled = true;
                lnkInteractionForm.Visible = true;

            }

            lnkBtnLogout.Visible = true;
            lnkBtnLogout.Enabled = true;
        }

        protected void lnkBtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Server.Transfer("~/Login.aspx");
        }

    }
}