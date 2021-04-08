﻿using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using CaresTracker.DataModels;
using CaresTracker.DataAccess.DataAccessors.ResidentAccessors;

namespace CaresTracker
{
    public partial class ResidentLookUp : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetAllResident getAllResident = new GetAllResident();
                DataTable ds = getAllResident.RunCommand();

                gvResidentList.DataSource = ds;
                Session["ResidentList"] = ds;

                gvResidentList.DataBound += (object o, EventArgs ev) =>
                {
                    gvResidentList.HeaderRow.TableSection = TableRowSection.TableHeader;
                };

                if (ds.Rows.Count == 0)
                {
                    lblNoRows.Text = "Couldn't find any events to display.";
                    divNoRows.Visible = true;
                    return;
                }

                gvResidentList.DataBind();
            }

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Server.Transfer("Homepage.aspx");
        }


        protected void NewResident_Click(object sender, EventArgs e)
        {
            
            Response.Redirect($"CreateResident.aspx");
        }

        protected void btnViewResident_Click(object sender, EventArgs e)
        {
            //Get the row containing the clicked button
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            //Recreate the Datarow the GVR is bound to
            DataRow dr = (Session["ResidentList"] as DataTable).Rows[row.DataItemIndex];
            Resident res = new Resident(dr);

            Session["Resident"] = res;
            Response.Redirect("ResidentProfile.aspx");
        }

    }
}