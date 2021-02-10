//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Region
    {
        public Region()
        {
            this.CARESUsers = new HashSet<CARESUser>();
            this.Residences = new HashSet<Residence>();
        }
    
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public Nullable<int> RegionPopulation { get; set; }
        public Nullable<int> NumCHWRequired { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
    
        public virtual ICollection<CARESUser> CARESUsers { get; set; }
        public virtual ICollection<Residence> Residences { get; set; }
    }
}
