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
    
    public partial class MessageTo
    {
        public int ID { get; set; }
        public int MessageID { get; set; }
        public int SentTo { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> HasRead { get; set; }
        public Nullable<System.DateTime> ReadTime { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual Message Message { get; set; }
        public virtual User User { get; set; }
    }
}