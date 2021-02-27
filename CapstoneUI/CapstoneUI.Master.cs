using CapstoneUI.DataModels;
using System;
using System.Web;

namespace CapstoneUI
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        CARESUser user;
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            else
            {
                user = Session["User"] as CARESUser;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (user.UserType == "C")
            {
                lnkImport.Visible = true;
                lnkImport.Enabled = true;
                lnkInteractionForm.Enabled = true;
                lnkInteractionForm.Visible = true;

            }
            else if (user.UserType == "A")
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