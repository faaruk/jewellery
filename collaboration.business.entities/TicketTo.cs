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
    public partial class TicketTo
    {
        #region Primitive Properties
    
        public virtual int ID
        {
            get;
            set;
        }
    
        public virtual int TicketID
        {
            get { return _ticketID; }
            set
            {
                if (_ticketID != value)
                {
                    if (Ticket != null && Ticket.TicketID != value)
                    {
                        Ticket = null;
                    }
                    _ticketID = value;
                }
            }
        }
        private int _ticketID;
    
        public virtual int AssignedTo
        {
            get { return _assignedTo; }
            set
            {
                if (_assignedTo != value)
                {
                    if (User != null && User.UserID != value)
                    {
                        User = null;
                    }
                    _assignedTo = value;
                }
            }
        }
        private int _assignedTo;
    
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
    
        public virtual Ticket Ticket
        {
            get { return _ticket; }
            set
            {
                if (!ReferenceEquals(_ticket, value))
                {
                    var previousValue = _ticket;
                    _ticket = value;
                    FixupTicket(previousValue);
                }
            }
        }
        private Ticket _ticket;
    
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
    
        private void FixupTicket(Ticket previousValue)
        {
            if (previousValue != null && previousValue.TicketToes.Contains(this))
            {
                previousValue.TicketToes.Remove(this);
            }
    
            if (Ticket != null)
            {
                if (!Ticket.TicketToes.Contains(this))
                {
                    Ticket.TicketToes.Add(this);
                }
                if (TicketID != Ticket.TicketID)
                {
                    TicketID = Ticket.TicketID;
                }
            }
        }
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.TicketToes.Contains(this))
            {
                previousValue.TicketToes.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.TicketToes.Contains(this))
                {
                    User.TicketToes.Add(this);
                }
                if (AssignedTo != User.UserID)
                {
                    AssignedTo = User.UserID;
                }
            }
        }

        #endregion
    }
}