//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Residence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Residence()
        {
            this.Residents = new HashSet<Resident>();
        }
    
        public int ResidenceID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public string Address { get; set; }
        public string NumOccupants { get; set; }
        public string UnitNumber { get; set; }
        public string HouseType { get; set; }
        public Nullable<int> DevelopmentID { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
    
        public virtual HousingDevelopment HousingDevelopment { get; set; }
        public virtual Region Region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resident> Residents { get; set; }
    }
}
