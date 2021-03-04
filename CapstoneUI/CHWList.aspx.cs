using CapstoneUI.DataAccess.DataAccessors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneUI
{
    public partial class CHWList : System.Web.UI.Page
    {
        DataTable CHWDataSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            gvCHWList.DataBound += (object o, EventArgs ev) =>
            {
                gvCHWList.HeaderRow.TableSection = TableRowSection.TableHeader;
            };
            CHWDataSet = new GetAllCHW().RunCommand();
            gvCHWList.DataSource = CHWDataSet;
            gvCHWList.DataBind();
        }

        protected void gvCHWList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Server.Transfer("CHWManagement.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }
    }
}