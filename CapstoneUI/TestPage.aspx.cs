using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            //Gather data from inputs
            List<string> values = new List<string>();

            Panel1.Controls.OfType<TextBox>().ToList().ForEach(tb => values.Add(tb.Text));

            //Create instance of class for the CHW Insert command and execute it
            CHWWriter newCHW = new CHWWriter(values);
            newCHW.ExecuteCommand();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Create instance of LookUpUser command class
            LookUpUser reader = new LookUpUser();
            //Store DataTable returned by LookUpUser sql command
            DataTable dbResult = reader.RunCommand(TextBox7.Text);
            
            //Loop over values and display result as a string
            StringBuilder sb = new StringBuilder();
            object[] rowData = dbResult.Rows[0].ItemArray;

            foreach (object cell in rowData)
            {
                sb.Append(cell.ToString() + " ");
            }

            Label7.Text = sb.ToString();

        }
    }
}