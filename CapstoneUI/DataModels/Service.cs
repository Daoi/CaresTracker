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
        public bool IsCompleted { get; set; }

        public Service() { }

        public Service(DataRow dr)
        {
            ServiceID = int.Parse(dr["ServiceID"].ToString());
            ServiceName = dr["ServiceName"].ToString();
            IsCompleted = (bool)dr["IsCompleted"];
        }

        public static (List<Service> requested, List<Service> completed) CreateServiceLists(DataTable dt)
        {
            (List<Service> requested, List<Service> completed) services;

            services.requested = dt.Rows.OfType<DataRow>().Where(dr => !(bool)dr["IsCompleted"])
                .Select(dr => new Service(dr))
                .ToList();

            services.completed = dt.Rows.OfType<DataRow>().Where(dr => (bool)dr["IsCompleted"])
                .Select(dr => new Service(dr))
                .ToList();
  
            return services;
        }


    }
}