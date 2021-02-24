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

        public class CHW
        {
            public int UserID { get; set; }
            public string Username { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserPhoneNumber { get; set; }
            public string UserEmail { get; set; }
            public DateTime LastLogin { get; set; }
            public string UserStatus { get; set; }
            public int Supervisor { get; set; }
            public int RegionID { get; set; }

            
            public CHW() { }
            public CHW(int userid, string username, string firstname, string lastname,
                string userphonenumber, string useremail, DateTime lastlogin, string userstatus, int supervisor, int regionid)
            {
                UserID = userid;
                Username = username;
                FirstName = firstname;
                LastName = lastname;
                UserPhoneNumber = userphonenumber;
                UserEmail = useremail;
                LastLogin = lastlogin;
                UserStatus = userstatus;
                Supervisor = supervisor;
                RegionID = regionid;
            }
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