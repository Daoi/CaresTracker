using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CapstoneUI.DataModels
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public bool IsRequested { get; set; }

        public Service() { }

        public Service(DataRow dr)
        {
            ServiceID = int.Parse(dr["ServiceID"].ToString());
            ServiceName = dr["ServiceName"].ToString();
            IsRequested = (bool)dr["IsRequested"];
        }

        public static (List<Service> requested, List<Service> offered) CreateServiceLists(DataTable dt)
        {
            (List<Service> requested, List<Service> offered) services;

            services.requested = dt.Rows.OfType<DataRow>().Where(dr => (bool)dr["IsRequested"])
                .Select(dr => new Service(dr))
                .ToList();

            services.offered = dt.Rows.OfType<DataRow>().Where(dr => !(bool)dr["IsRequested"])
                .Select(dr => new Service(dr))
                .ToList();
  
            return services;
        }

    }
}