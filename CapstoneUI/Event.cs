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
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Residents = new HashSet<Resident>();
            this.CARESUsers = new HashSet<CARESUser>();
        }
    
        public int EventID { get; set; }
        public byte[] EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventType { get; set; }
        public string EventLocation { get; set; }
        public Nullable<System.DateTime> EventStartDateTime { get; set; }
        public Nullable<System.DateTime> EventEndDateTime { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resident> Residents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARESUser> CARESUsers { get; set; }
    }
}
