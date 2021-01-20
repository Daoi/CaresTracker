using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Equals("CHW"))
            {
                Session["AccountType"] = 0;
                Session["LoginStatus"] = true;
                Session["Username"] = "CHW";
                Response.Redirect("Homepage.aspx");
            }
            else if (txtUsername.Text.Equals("Admin"))
            {
                Session["AccountType"] = 1;
                Session["LoginStatus"] = true;
                Session["Username"] = "Admin";
                Server.Transfer("Homepage.aspx");
            }
        }
    }
}