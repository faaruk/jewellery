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
    public partial class User
    {
        #region Primitive Properties
    
        public virtual int UserID
        {
            get;
            set;
        }
    
        public virtual string UserName
        {
            get;
            set;
        }
    
        public virtual string FirstName
        {
            get;
            set;
        }
    
        public virtual string LastName
        {
            get;
            set;
        }
    
        public virtual byte[] Password
        {
            get;
            set;
        }
    
        public virtual string EMail
        {
            get;
            set;
        }
    
        public virtual string Mobile
        {
            get;
            set;
        }
    
        public virtual string ImageLocationURL
        {
            get;
            set;
        }
    
        public virtual bool IsActive
        {
            get;
            set;
        }
    
        public virtual int RoleID
        {
            get { return _roleID; }
            set
            {
                if (_roleID != value)
                {
                    if (Role != null && Role.RoleID != value)
                    {
                        Role = null;
                    }
                    _roleID = value;
                }
            }
        }
        private int _roleID;
    
        public virtual bool DefaultPasswordChanged
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
    
        public virtual Nullable<System.DateTime> LastLoginDate
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<Message> Messages
        {
            get
            {
                if (_messages == null)
                {
                    var newCollection = new FixupCollection<Message>();
                    newCollection.CollectionChanged += FixupMessages;
                    _messages = newCollection;
                }
                return _messages;
            }
            set
            {
                if (!ReferenceEquals(_messages, value))
                {
                    var previousValue = _messages as FixupCollection<Message>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMessages;
                    }
                    _messages = value;
                    var newValue = value as FixupCollection<Message>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMessages;
                    }
                }
            }
        }
        private ICollection<Message> _messages;
    
        public virtual ICollection<MessageThread> MessageThreads
        {
            get
            {
                if (_messageThreads == null)
                {
                    var newCollection = new FixupCollection<MessageThread>();
                    newCollection.CollectionChanged += FixupMessageThreads;
                    _messageThreads = newCollection;
                }
                return _messageThreads;
            }
            set
            {
                if (!ReferenceEquals(_messageThreads, value))
                {
                    var previousValue = _messageThreads as FixupCollection<MessageThread>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMessageThreads;
                    }
                    _messageThreads = value;
                    var newValue = value as FixupCollection<MessageThread>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMessageThreads;
                    }
                }
            }
        }
        private ICollection<MessageThread> _messageThreads;
    
        public virtual ICollection<MessageThread> MessageThreads1
        {
            get
            {
                if (_messageThreads1 == null)
                {
                    var newCollection = new FixupCollection<MessageThread>();
                    newCollection.CollectionChanged += FixupMessageThreads1;
                    _messageThreads1 = newCollection;
                }
                return _messageThreads1;
            }
            set
            {
                if (!ReferenceEquals(_messageThreads1, value))
                {
                    var previousValue = _messageThreads1 as FixupCollection<MessageThread>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMessageThreads1;
                    }
                    _messageThreads1 = value;
                    var newValue = value as FixupCollection<MessageThread>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMessageThreads1;
                    }
                }
            }
        }
        private ICollection<MessageThread> _messageThreads1;
    
        public virtual ICollection<MessageTo> MessageToes
        {
            get
            {
                if (_messageToes == null)
                {
                    var newCollection = new FixupCollection<MessageTo>();
                    newCollection.CollectionChanged += FixupMessageToes;
                    _messageToes = newCollection;
                }
                return _messageToes;
            }
            set
            {
                if (!ReferenceEquals(_messageToes, value))
                {
                    var previousValue = _messageToes as FixupCollection<MessageTo>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupMessageToes;
                    }
                    _messageToes = value;
                    var newValue = value as FixupCollection<MessageTo>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupMessageToes;
                    }
                }
            }
        }
        private ICollection<MessageTo> _messageToes;
    
        public virtual ICollection<OrderParticipant> OrderParticipants
        {
            get
            {
                if (_orderParticipants == null)
                {
                    var newCollection = new FixupCollection<OrderParticipant>();
                    newCollection.CollectionChanged += FixupOrderParticipants;
                    _orderParticipants = newCollection;
                }
                return _orderParticipants;
            }
            set
            {
                if (!ReferenceEquals(_orderParticipants, value))
                {
                    var previousValue = _orderParticipants as FixupCollection<OrderParticipant>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOrderParticipants;
                    }
                    _orderParticipants = value;
                    var newValue = value as FixupCollection<OrderParticipant>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOrderParticipants;
                    }
                }
            }
        }
        private ICollection<OrderParticipant> _orderParticipants;
    
        public virtual ICollection<OrdersCAD> OrdersCADs
        {
            get
            {
                if (_ordersCADs == null)
                {
                    var newCollection = new FixupCollection<OrdersCAD>();
                    newCollection.CollectionChanged += FixupOrdersCADs;
                    _ordersCADs = newCollection;
                }
                return _ordersCADs;
            }
            set
            {
                if (!ReferenceEquals(_ordersCADs, value))
                {
                    var previousValue = _ordersCADs as FixupCollection<OrdersCAD>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOrdersCADs;
                    }
                    _ordersCADs = value;
                    var newValue = value as FixupCollection<OrdersCAD>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOrdersCADs;
                    }
                }
            }
        }
        private ICollection<OrdersCAD> _ordersCADs;
    
        public virtual Role Role
        {
            get { return _role; }
            set
            {
                if (!ReferenceEquals(_role, value))
                {
                    var previousValue = _role;
                    _role = value;
                    FixupRole(previousValue);
                }
            }
        }
        private Role _role;
    
        public virtual ICollection<Order> Orders
        {
            get
            {
                if (_orders == null)
                {
                    var newCollection = new FixupCollection<Order>();
                    newCollection.CollectionChanged += FixupOrders;
                    _orders = newCollection;
                }
                return _orders;
            }
            set
            {
                if (!ReferenceEquals(_orders, value))
                {
                    var previousValue = _orders as FixupCollection<Order>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOrders;
                    }
                    _orders = value;
                    var newValue = value as FixupCollection<Order>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOrders;
                    }
                }
            }
        }
        private ICollection<Order> _orders;
    
        public virtual ICollection<TicketThread> TicketThreads
        {
            get
            {
                if (_ticketThreads == null)
                {
                    var newCollection = new FixupCollection<TicketThread>();
                    newCollection.CollectionChanged += FixupTicketThreads;
                    _ticketThreads = newCollection;
                }
                return _ticketThreads;
            }
            set
            {
                if (!ReferenceEquals(_ticketThreads, value))
                {
                    var previousValue = _ticketThreads as FixupCollection<TicketThread>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTicketThreads;
                    }
                    _ticketThreads = value;
                    var newValue = value as FixupCollection<TicketThread>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTicketThreads;
                    }
                }
            }
        }
        private ICollection<TicketThread> _ticketThreads;
    
        public virtual ICollection<TicketThread> TicketThreads1
        {
            get
            {
                if (_ticketThreads1 == null)
                {
                    var newCollection = new FixupCollection<TicketThread>();
                    newCollection.CollectionChanged += FixupTicketThreads1;
                    _ticketThreads1 = newCollection;
                }
                return _ticketThreads1;
            }
            set
            {
                if (!ReferenceEquals(_ticketThreads1, value))
                {
                    var previousValue = _ticketThreads1 as FixupCollection<TicketThread>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTicketThreads1;
                    }
                    _ticketThreads1 = value;
                    var newValue = value as FixupCollection<TicketThread>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTicketThreads1;
                    }
                }
            }
        }
        private ICollection<TicketThread> _ticketThreads1;
    
        public virtual ICollection<TicketThread> TicketThreads1_1
        {
            get
            {
                if (_ticketThreads1_1 == null)
                {
                    var newCollection = new FixupCollection<TicketThread>();
                    newCollection.CollectionChanged += FixupTicketThreads1_1;
                    _ticketThreads1_1 = newCollection;
                }
                return _ticketThreads1_1;
            }
            set
            {
                if (!ReferenceEquals(_ticketThreads1_1, value))
                {
                    var previousValue = _ticketThreads1_1 as FixupCollection<TicketThread>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTicketThreads1_1;
                    }
                    _ticketThreads1_1 = value;
                    var newValue = value as FixupCollection<TicketThread>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTicketThreads1_1;
                    }
                }
            }
        }
        private ICollection<TicketThread> _ticketThreads1_1;
    
        public virtual ICollection<Ticket> Tickets
        {
            get
            {
                if (_tickets == null)
                {
                    var newCollection = new FixupCollection<Ticket>();
                    newCollection.CollectionChanged += FixupTickets;
                    _tickets = newCollection;
                }
                return _tickets;
            }
            set
            {
                if (!ReferenceEquals(_tickets, value))
                {
                    var previousValue = _tickets as FixupCollection<Ticket>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupTickets;
                    }
                    _tickets = value;
                    var newValue = value as FixupCollection<Ticket>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupTickets;
                    }
                }
            }
        }
        private ICollection<Ticket> _tickets;
    
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
    
        private void FixupRole(Role previousValue)
        {
            if (previousValue != null && previousValue.Users.Contains(this))
            {
                previousValue.Users.Remove(this);
            }
    
            if (Role != null)
            {
                if (!Role.Users.Contains(this))
                {
                    Role.Users.Add(this);
                }
                if (RoleID != Role.RoleID)
                {
                    RoleID = Role.RoleID;
                }
            }
        }
    
        private void FixupMessages(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Message item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Message item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupMessageThreads(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MessageThread item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MessageThread item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupMessageThreads1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MessageThread item in e.NewItems)
                {
                    item.User1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MessageThread item in e.OldItems)
                {
                    if (ReferenceEquals(item.User1, this))
                    {
                        item.User1 = null;
                    }
                }
            }
        }
    
        private void FixupMessageToes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MessageTo item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MessageTo item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupOrderParticipants(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (OrderParticipant item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OrderParticipant item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupOrdersCADs(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (OrdersCAD item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OrdersCAD item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupOrders(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Order item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Order item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupTicketThreads(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TicketThread item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TicketThread item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }
    
        private void FixupTicketThreads1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TicketThread item in e.NewItems)
                {
                    item.User1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TicketThread item in e.OldItems)
                {
                    if (ReferenceEquals(item.User1, this))
                    {
                        item.User1 = null;
                    }
                }
            }
        }
    
        private void FixupTicketThreads1_1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TicketThread item in e.NewItems)
                {
                    item.User1_1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TicketThread item in e.OldItems)
                {
                    if (ReferenceEquals(item.User1_1, this))
                    {
                        item.User1_1 = null;
                    }
                }
            }
        }
    
        private void FixupTickets(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Ticket item in e.NewItems)
                {
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Ticket item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
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
                    item.User = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (TicketTo item in e.OldItems)
                {
                    if (ReferenceEquals(item.User, this))
                    {
                        item.User = null;
                    }
                }
            }
        }

        #endregion
    }
}
