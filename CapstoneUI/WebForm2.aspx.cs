using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataAccess.DataAccessors.ExampleClass ec = new DataAccess.DataAccessors.ExampleClass();
            DataTable dbResult = ec.LookUpUser(29, false);
            StringBuilder sb = new StringBuilder();
            object[] rowData = dbResult.Rows[0].ItemArray;

            foreach(object cell in rowData)
            {
                sb.Append(cell.ToString() + " ");
            }

            Label1.Text = sb.ToString();
        }
    }
}