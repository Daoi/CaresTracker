using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class CARESUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string LastLogin { get; set; }
        public string UserStatus { get; set; }
        public string UserType { get; set; }
        public string Supervisor { get; set; }
        public int RegionID { get; set; }
        public int OrganizationID { get; set; }
        public string FullName { get; set; }

        /// <summary>
        /// Used for Event Page, temporary?
        /// </summary>
        /// <param name="dataRow"></param>
        public CARESUser(object[] dataRow)
        {
            UserID = (int)dataRow[0];
            Username = dataRow[1].ToString();
            FirstName = dataRow[2].ToString();
            LastName = dataRow[3].ToString();
            FullName = $"{FirstName} {LastName}";
        }

        public CARESUser(DataRow dataRow)
        {
            UserID = int.Parse(dataRow["UserID"].ToString());
            Username = dataRow["Username"].ToString();
            FirstName = dataRow["FirstName"].ToString();
            LastName = dataRow["LastName"].ToString();
            UserEmail = dataRow["UserEmail"].ToString();
            UserPhoneNumber = dataRow["UserPhoneNumber"].ToString();
            LastLogin = dataRow["LastLogin"].ToString();
            UserStatus = dataRow["UserStatus"].ToString();
            UserType = dataRow["UserType"].ToString();
            Supervisor = dataRow["Supervisor"].ToString();
            RegionID = int.Parse(dataRow["RegionID"].ToString());
            OrganizationID = int.Parse(dataRow["OrganizationID"].ToString());
            FullName = $"{FirstName} {LastName}";
        }

    }



}