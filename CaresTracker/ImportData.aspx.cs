using CaresTracker.DataAccess.DataAccessors.DevelopmentAccessors;
using CaresTracker.DataModels;
using CaresTracker.Importing;
using CaresTracker.Importing.FileModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace CaresTracker
{
    public partial class ImportData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CARESUser user = Session["User"] as CARESUser;
                DataTable developmentDT = new GetDevelopmentsByUserID().ExecuteCommand(user.UserID);

                // Bind to drop down list
                ddlDevelopments.DataSource = developmentDT;
                ddlDevelopments.DataValueField = "DevelopmentID";
                ddlDevelopments.DataTextField = "DevelopmentName";
                ddlDevelopments.DataBind();
                // Store list in session
                ViewState["DevelopmentDT"] = developmentDT;
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }

        protected void btnSubmitImport_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (fileUpload.HasFile && FileIsValid(fileUpload))
            {
                try
                {
                    sb.Append($" Uploading file: {fileUpload.FileName}");

                    //saving the file
                    string path = Server.MapPath($"./Importing/Files/") + fileUpload.FileName;
                    fileUpload.SaveAs(path);


                    ReadFile(path);
                }
                catch (Exception ex)
                {
                    sb.Append($"Error uploading file: {ex.Message}");
                    lblMessage.Text = sb.ToString();
                }
            }
            else
            {
                lblMessage.Text = "Excel file must be saved as CSV (File -> Save As -> Select \"CSV\" from drop down.)";
            }
        }


        private void ReadFile(string path)
        {

            DataTable developmentDT = ViewState["DevelopmentDT"] as DataTable;

            try
            {
                int devID = int.Parse(ddlDevelopments.SelectedValue);
                int regionID = developmentDT.Rows.Cast<DataRow>().ToList()
                .Where(dr => (int)dr["DevelopmentID"] == devID)
                .Select(dr => dr.Field<int>("RegionID")).ElementAt(0);

                FileManager fm = new FileManager(path);

                (int inserts, List<int> errors) = ImportResidents.Import(fm.results, devID, regionID);

                StringBuilder sb = new StringBuilder();
                sb.Append($"Succesfully inserted {inserts} out of {fm.results.Count} residents from the file. ");
                if(errors.Count > 0)
                {
                    divUploadErrors.Visible = true;
                    sb.Append("The following rows had errors: <br />");
                    errors.ForEach(i => sb.Append($"Row #{i} Resident Name: {fm.results[i-1].ResidentFirstName} {fm.results[i - 1].ResidentLastName} <br />"));
                }
                lblMessage.Text = sb.ToString();
            }
            catch (Exception e)
            {
                lblMessage.Text = e.Message;
            }


        }


        private bool FileIsValid(FileUpload fileUpload)
        {
            return fileUpload.PostedFile.ContentType == "application/vnd.ms-excel" && fileUpload.HasFile; //Make sure theres a CSV file selected
        }

    }
}