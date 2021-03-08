﻿using CapstoneUI.DataAccess.DataAccessors;
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
            if (!IsPostBack)
            {
                gvCHWList.DataBound += (object o, EventArgs ev) =>
                {
                    gvCHWList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                CHWDataSet = new GetAllCHW().RunCommand();
                gvCHWList.DataSource = CHWDataSet;
                gvCHWList.DataBind();
            }
        }

        protected void btnViewWorker_Click(object sender, EventArgs e)
        {
            Response.Redirect("./CHWManagement.aspx");
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("./Homepage.aspx");
        }
    }
}
