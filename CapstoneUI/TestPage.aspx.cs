using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapstoneUI.DataAccess.DataAccessors;
using CapstoneUI.DataAccess.DataAccessors.Examples;

namespace CapstoneUI
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> values = new List<string>();

            Panel1.Controls.OfType<TextBox>().ToList().ForEach(tb => values.Add(tb.Text));

            CHWWriter newCHW = new CHWWriter(values);
            newCHW.ExecuteCommand();
        }
    }
}