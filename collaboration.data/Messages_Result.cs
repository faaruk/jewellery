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
    
    public partial class Messages_Result
    {
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public int MessageThreadID { get; set; }
        public string Subject { get; set; }
        public Nullable<int> SentFromUserID { get; set; }
        public string SentFromUserName { get; set; }
        public string LocationURL { get; set; }
        public string ContentType { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
