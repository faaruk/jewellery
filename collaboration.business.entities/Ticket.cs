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
    public partial class Ticket
    {
        #region Primitive Properties
    
        public virtual int TicketThreadID
        {
            get { return _ticketThreadID; }
            set
            {
                if (_ticketThreadID != value)
                {
                    if (TicketThread != null && TicketThread.TicketThreadID != value)
                    {
                        TicketThread = null;
                    }
                    _ticketThreadID = value;
                }
            }
        }
        private int _ticketThreadID;
    
        public virtual int TicketID
        {
            get;
            set;
        }
    
        public virtual string TicketText
        {
            get;
            set;
        }
    
        public virtual int AssignedFrom
        {
            get { return _assignedFrom; }
            set
            {
                if (_assignedFrom != value)
                {
                    if (User != null && User.UserID != value)
                    {
                        User = null;
                    }
                    _assignedFrom = value;
                }
            }
        }
        private int _assignedFrom;
    
        public virtual Nullable<bool> IsActive
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> HasAttachment
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
    
        public virtual TicketThread TicketThread
        {
            get { return _ticketThread; }
            set
            {
                if (!ReferenceEquals(_ticketThread, value))
                {
                    var previousValue = _ticketThread;
                    _ticketThread = value;
                    FixupTicketThread(previousValue);
                }
            }
        }
        private TicketThread _ticketThread;
    
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
    
        public virtual ICollection<TicketsAttachment> TicketsAttachments
        {
            get
            {
                if (_ticketsAttachments == null)
                {
                    var newCollection = new FixupCollection<TicketsAttachment>();
                    newCollection.CollectionChanged += FixupTicketsAttachments;
                    _ticketsAttachments = newCollection;
                }
                return _ticketsAttachments;
            }
            set
            {
                if (!ReferenceEquals(_ticketsAttachments, value))
                {
                    var previousValue = _ticketsAttachments as FixupCollection<TicketsAttachment>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTicketsAttachments;
                    }
                    _ticketsAttachments = value;
                    var newValue = value as FixupCollection<TicketsAttachment>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTicketsAttachments;
                    }
                }
            }
        }
        private ICollection<TicketsAttachment> _ticketsAttachments;
    
        public virtual ICollection<TicketTo> TicketToes
        {
            get
            {
                if (_ticketToes == null)
                {
                    var newCollection = new FixupCollection<TicketTo>();
                    newCollection.CollectionChanged += FixupTicketToes;
                    _ticketToes = newCollection;
                }
                return _ticketToes;
            }
            set
            {
                if (!ReferenceEquals(_ticketToes, value))
                {
                    var previousValue = _ticketToes as FixupCollection<TicketTo>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTicketToes;
                    }
                    _ticketToes = value;
                    var newValue = value as FixupCollection<TicketTo>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTicketToes;
                    }
                }
            }
        }
        private ICollection<TicketTo> _ticketToes;

        #endregion
        #region Association Fixup
    
        private void FixupTicketThread(TicketThread previousValue)
        {
            if (previousValue != null && previousValue.Tickets.Contains(this))
            {
                previousValue.Tickets.Remove(this);
            }
    
            if (TicketThread != null)
            {
                if (!TicketThread.Tickets.Contains(this))
                {
                    TicketThread.Tickets.Add(this);
                }
                if (TicketThreadID != TicketThread.TicketThreadID)
                {
                    TicketThreadID = TicketThread.TicketThreadID;
                }
            }
        }
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.Tickets.Contains(this))
            {
                previousValue.Tickets.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.Tickets.Contains(this))
                {
                    User.Tickets.Add(this);
                }
                if (AssignedFrom != User.UserID)
                {
                    AssignedFrom = User.UserID;
                }
            }
        }
    
        private void FixupTicketsAttachments(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TicketsAttachment item in e.NewItems)
                {
                    item.Ticket = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TicketsAttachment item in e.OldItems)
                {
                    if (ReferenceEquals(item.Ticket, this))
                    {
                        item.Ticket = null;
                    }
                }
            }
        }
    
        private void FixupTicketToes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TicketTo item in e.NewItems)
                {
                    item.Ticket = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TicketTo item in e.OldItems)
                {
                    if (ReferenceEquals(item.Ticket, this))
                    {
                        item.Ticket = null;
                    }
                }
            }
        }

        #endregion
    }
}
