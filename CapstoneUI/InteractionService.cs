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
    
    public partial class InteractionService
    {
        public int InteractionID { get; set; }
        public int ServiceID { get; set; }
        public bool IsRequested { get; set; }
    
        public virtual Interaction Interaction { get; set; }
        public virtual Service Service { get; set; }
    }
}
