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
    
    public partial class Message
    {
        public Message()
        {
            this.MessagesAttachments = new HashSet<MessagesAttachment>();
            this.MessageToes = new HashSet<MessageTo>();
            this.Specimens = new HashSet<Specimen>();
        }
    
        public int MessageThreadID { get; set; }
        public int MessageID { get; set; }
        public string MessageText { get; set; }
        public int SentFrom { get; set; }
        public string Subject { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> HasAttachment { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    
        public virtual MessageThread MessageThread { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MessagesAttachment> MessagesAttachments { get; set; }
        public virtual ICollection<MessageTo> MessageToes { get; set; }
        public virtual ICollection<Specimen> Specimens { get; set; }
    }
}
