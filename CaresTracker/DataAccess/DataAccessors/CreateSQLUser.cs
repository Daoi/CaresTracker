using CaresTracker.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml;


namespace CaresTracker.DataAccess.DataAccessors
{
    public class CreateSQLUser
    {
        private string defaultConnection = "dbConnectionString";

        public CreateSQLUser(CARESUser user)
        {
            StringBuilder sb = new StringBuilder();

            string username = user.Username;
            string password = RandomString();

            const string provider = "System.Data.SqlClient";
            const int passwordStart = 9;
            const int passwordIndex = 4;
            string[] parts = ConfigurationManager.ConnectionStrings[defaultConnection].ConnectionString.Replace("admin", username).Split(';');
            parts[passwordIndex] = parts[passwordIndex].Substring(0, passwordStart) + password;

            parts.ToList().ForEach(s => sb.Append(s + ';'));
            sb.Remove(sb.Length - 1, 1);

            string cString = sb.ToString();
            sb.Clear();
            sb.AppendLine();
            sb.Append($"<add name=\"{username}\"");
            sb.AppendLine();
            sb.Append($"providerName = \"{provider}\"");
            sb.AppendLine();
            sb.Append($"connectionString = \"{cString}\"  />");
            sb.AppendLine();

            string xml = sb.ToString();
            string path = HttpContext.Current.Server.MapPath($"~/connections.config");
            List<string> lines = File.ReadLines(path).ToList();
            int index = lines.IndexOf("  <connectionStrings>");
            lines.Insert(index + 1, xml);
            File.WriteAllLines(path, lines.ToArray());


            try
            {
                AddSQLUser.ExecuteCommand(username, password);
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        private static string RandomString()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int length = 16;
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }




    }



}