using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class CARESUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public DateTime LastLogin { get; set; }
        public string UserStatus { get; set; }
        public string UserType { get; set; }
        public string Supervisor { get; set; }
        public int RegionID { get; set; }
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

    }



}