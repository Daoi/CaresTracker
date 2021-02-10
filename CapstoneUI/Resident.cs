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
    
    public partial class Resident
    {
        public Resident()
        {
            this.Interactions = new HashSet<Interaction>();
            this.Events = new HashSet<Event>();
        }
    
        public int ResidentID { get; set; }
        public Nullable<int> ResidenceID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string ResidentEmail { get; set; }
        public string ResidentPhoneNumber { get; set; }
        public string RelationshipToHoH { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public Nullable<int> FamilySize { get; set; }
        public Nullable<System.DateTime> DateLastModified { get; set; }
        public string UserLastModified { get; set; }
    
        public virtual ICollection<Interaction> Interactions { get; set; }
        public virtual Residence Residence { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
