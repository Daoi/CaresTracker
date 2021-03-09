﻿using System;
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
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNumber { get; set; }
        public string LastLogin { get; set; }
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
            UserFirstName = dataRow[2].ToString();
            UserLastName = dataRow[3].ToString();
            FullName = $"{UserFirstName} {UserLastName}";
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
            Supervisor = dataRow["Supervisor"].ToString();
            RegionID = int.Parse(dataRow["RegionID"].ToString());
            FullName = $"{UserFirstName} {UserLastName}";
        }

    }



}