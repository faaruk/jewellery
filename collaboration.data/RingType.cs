//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Collaboration.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class RingType
    {
        public RingType()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public short RingTypeID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
    }
}
