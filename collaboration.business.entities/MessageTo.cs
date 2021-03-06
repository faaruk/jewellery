//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Collaboration.Business.Entities
{
    public partial class MessageTo
    {
        #region Primitive Properties
    
        public virtual int ID
        {
            get;
            set;
        }
    
        public virtual int MessageID
        {
            get { return _messageID; }
            set
            {
                if (_messageID != value)
                {
                    if (Message != null && Message.MessageID != value)
                    {
                        Message = null;
                    }
                    _messageID = value;
                }
            }
        }
        private int _messageID;
    
        public virtual int SentTo
        {
            get { return _sentTo; }
            set
            {
                if (_sentTo != value)
                {
                    if (User != null && User.UserID != value)
                    {
                        User = null;
                    }
                    _sentTo = value;
                }
            }
        }
        private int _sentTo;
    
        public virtual Nullable<bool> IsActive
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> HasRead
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> ReadTime
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> CreateDate
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> ModifyDate
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual Message Message
        {
            get { return _message; }
            set
            {
                if (!ReferenceEquals(_message, value))
                {
                    var previousValue = _message;
                    _message = value;
                    FixupMessage(previousValue);
                }
            }
        }
        private Message _message;
    
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (!ReferenceEquals(_user, value))
                {
                    var previousValue = _user;
                    _user = value;
                    FixupUser(previousValue);
                }
            }
        }
        private User _user;

        #endregion
        #region Association Fixup
    
        private void FixupMessage(Message previousValue)
        {
            if (previousValue != null && previousValue.MessageToes.Contains(this))
            {
                previousValue.MessageToes.Remove(this);
            }
    
            if (Message != null)
            {
                if (!Message.MessageToes.Contains(this))
                {
                    Message.MessageToes.Add(this);
                }
                if (MessageID != Message.MessageID)
                {
                    MessageID = Message.MessageID;
                }
            }
        }
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.MessageToes.Contains(this))
            {
                previousValue.MessageToes.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.MessageToes.Contains(this))
                {
                    User.MessageToes.Add(this);
                }
                if (SentTo != User.UserID)
                {
                    SentTo = User.UserID;
                }
            }
        }

        #endregion
    }
}
