using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaresTracker.DataModels
{
    public class CARESUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string LastLogin { get; set; }
        public string UserStatus { get; set; }
        public string UserType { get; set; }
        public int OrganizationID { get; set; } //1 = Temple, 2 = Greater Philadelphia Health Action, 3 = Drexel
        public string OrganizationName { get; set; }
        public string FullName { get { return $"{UserFirstName} {UserLastName}"; } }

        public CARESUser()
        {

        }

        public CARESUser(DataRow dataRow)
        {
            UserID = int.Parse(dataRow["UserID"].ToString());
            Username = dataRow["Username"].ToString();
            UserFirstName = dataRow["UserFirstName"].ToString();
            UserLastName = dataRow["UserLastName"].ToString();
            UserEmail = dataRow["UserEmail"].ToString();
            UserPhoneNumber = dataRow["UserPhoneNumber"].ToString();
            LastLogin = dataRow["LastLogin"].ToString();
            UserStatus = dataRow["UserStatus"].ToString();
            UserType = dataRow["UserType"].ToString();
            OrganizationID = int.Parse(dataRow["OrganizationID"].ToString());
            OrganizationName = dataRow["OrganizationName"].ToString();
        }

        /// <summary>
        /// Create a list of CARESUser objects from a CARESUser DataTable
        /// </summary>
        /// <param name="dataTable">Contains CARESUser table data</param>
        /// <returns></returns>
        public static List<CARESUser> CreateEventHostList(DataTable dataTable)
        {
            return dataTable.Rows.OfType<DataRow>()
                .Select(dr => new CARESUser(dr))
                .ToList();
        }
    }
}
