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
    public partial class Order
    {
        #region Primitive Properties
    
        public virtual int OrderID
        {
            get;
            set;
        }
    
        public virtual int CustomerID
        {
            get { return _customerID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_customerID != value)
                    {
                        if (Customer != null && Customer.CustomerID != value)
                        {
                            Customer = null;
                        }
                        _customerID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _customerID;
    
        public virtual int ModelTypeID
        {
            get { return _modelTypeID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_modelTypeID != value)
                    {
                        if (ModelType != null && ModelType.ModelTypeID != value)
                        {
                            ModelType = null;
                        }
                        _modelTypeID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _modelTypeID;
    
        public virtual Nullable<int> ModelSubTypeID
        {
            get;
            set;
        }
    
        public virtual string ModelNumber
        {
            get;
            set;
        }
    
        public virtual string SerialNumber
        {
            get;
            set;
        }
    
        public virtual int ProcessTypeID
        {
            get { return _processTypeID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_processTypeID != value)
                    {
                        if (ProcessType != null && ProcessType.ProcessTypeID != value)
                        {
                            ProcessType = null;
                        }
                        _processTypeID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _processTypeID;
    
        public virtual Nullable<int> MetalID
        {
            get { return _metalID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_metalID != value)
                    {
                        if (Metal != null && Metal.MetalID != value)
                        {
                            Metal = null;
                        }
                        _metalID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _metalID;
    
        public virtual string MetalOther
        {
            get;
            set;
        }
    
        public virtual Nullable<int> FingerSizeID
        {
            get { return _fingerSizeID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_fingerSizeID != value)
                    {
                        if (FingerSize != null && FingerSize.FingerSizeID != value)
                        {
                            FingerSize = null;
                        }
                        _fingerSizeID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _fingerSizeID;
    
        public virtual string FingerSizeOther
        {
            get;
            set;
        }
    
        public virtual string Quantity
        {
            get;
            set;
        }
    
        public virtual string QuantityOther
        {
            get;
            set;
        }
    
        public virtual Nullable<decimal> Length
        {
            get;
            set;
        }
    
        public virtual string LengthMeasurement
        {
            get;
            set;
        }
    
        public virtual short PriorityID
        {
            get { return _priorityID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_priorityID != value)
                    {
                        if (Priority != null && Priority.PriorityID != value)
                        {
                            Priority = null;
                        }
                        _priorityID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private short _priorityID;
    
        public virtual Nullable<bool> MakeExactCopies
        {
            get;
            set;
        }
    
        public virtual Nullable<short> RingTypeID
        {
            get { return _ringTypeID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_ringTypeID != value)
                    {
                        if (RingType != null && RingType.RingTypeID != value)
                        {
                            RingType = null;
                        }
                        _ringTypeID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<short> _ringTypeID;
    
        public virtual Nullable<bool> IsExistingModel
        {
            get;
            set;
        }
    
        public virtual string ModelToMatch
        {
            get;
            set;
        }
    
        public virtual string CurveType
        {
            get;
            set;
        }
    
        public virtual string TailoredType
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IsFinishAtSomePoint
        {
            get;
            set;
        }
    
        public virtual string AdditionalInfo
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IsPF
        {
            get;
            set;
        }
    
        public virtual string HeadSize
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IsCADRequested
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IsSampleProvided
        {
            get;
            set;
        }
    
        public virtual Nullable<byte> NoOfSamples
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> MakeExactCopiesSample
        {
            get;
            set;
        }
    
        public virtual Nullable<bool> IsStoneProvided
        {
            get;
            set;
        }
    
        public virtual string StoneDescription
        {
            get;
            set;
        }
    
        public virtual string SettingInstructions
        {
            get;
            set;
        }
    
        public virtual string Remarks
        {
            get;
            set;
        }
    
        public virtual Nullable<int> TMUserID
        {
            get;
            set;
        }
    
        public virtual Nullable<int> AssignedTo
        {
            get { return _assignedTo; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_assignedTo != value)
                    {
                        if (User != null && User.UserID != value)
                        {
                            User = null;
                        }
                        _assignedTo = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _assignedTo;
    
        public virtual int OrderStatusID
        {
            get { return _orderStatusID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_orderStatusID != value)
                    {
                        if (OrderStatu != null && OrderStatu.StatusID != value)
                        {
                            OrderStatu = null;
                        }
                        _orderStatusID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _orderStatusID;
    
        public virtual Nullable<int> CreatedBy
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
    
        public virtual Nullable<System.DateTime> ExpectedShippingDate
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual FingerSize FingerSize
        {
            get { return _fingerSize; }
            set
            {
                if (!ReferenceEquals(_fingerSize, value))
                {
                    var previousValue = _fingerSize;
                    _fingerSize = value;
                    FixupFingerSize(previousValue);
                }
            }
        }
        private FingerSize _fingerSize;
    
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
    
        public virtual Metal Metal
        {
            get { return _metal; }
            set
            {
                if (!ReferenceEquals(_metal, value))
                {
                    var previousValue = _metal;
                    _metal = value;
                    FixupMetal(previousValue);
                }
            }
        }
        private Metal _metal;
    
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
    
        public virtual OrderStatu OrderStatu
        {
            get { return _orderStatu; }
            set
            {
                if (!ReferenceEquals(_orderStatu, value))
                {
                    var previousValue = _orderStatu;
                    _orderStatu = value;
                    FixupOrderStatu(previousValue);
                }
            }
        }
        private OrderStatu _orderStatu;
    
        public virtual Priority Priority
        {
            get { return _priority; }
            set
            {
                if (!ReferenceEquals(_priority, value))
                {
                    var previousValue = _priority;
                    _priority = value;
                    FixupPriority(previousValue);
                }
            }
        }
        private Priority _priority;
    
        public virtual ProcessType ProcessType
        {
            get { return _processType; }
            set
            {
                if (!ReferenceEquals(_processType, value))
                {
                    var previousValue = _processType;
                    _processType = value;
                    FixupProcessType(previousValue);
                }
            }
        }
        private ProcessType _processType;
    
        public virtual RingType RingType
        {
            get { return _ringType; }
            set
            {
                if (!ReferenceEquals(_ringType, value))
                {
                    var previousValue = _ringType;
                    _ringType = value;
                    FixupRingType(previousValue);
                }
            }
        }
        private RingType _ringType;
    
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
    
        public virtual ICollection<Specimen> Specimens
        {
            get
            {
                if (_specimens == null)
                {
                    var newCollection = new FixupCollection<Specimen>();
                    newCollection.CollectionChanged += FixupSpecimens;
                    _specimens = newCollection;
                }
                return _specimens;
            }
            set
            {
                if (!ReferenceEquals(_specimens, value))
                {
                    var previousValue = _specimens as FixupCollection<Specimen>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupSpecimens;
                    }
                    _specimens = value;
                    var newValue = value as FixupCollection<Specimen>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupSpecimens;
                    }
                }
            }
        }
        private ICollection<Specimen> _specimens;
    
        public virtual Customer Customer
        {
            get { return _customer; }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer;
                    _customer = value;
                    FixupCustomer(previousValue);
                }
            }
        }
        private Customer _customer;
    
        public virtual ModelType ModelType
        {
            get { return _modelType; }
            set
            {
                if (!ReferenceEquals(_modelType, value))
                {
                    var previousValue = _modelType;
                    _modelType = value;
                    FixupModelType(previousValue);
                }
            }
        }
        private ModelType _modelType;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupFingerSize(FingerSize previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (FingerSize != null)
            {
                if (!FingerSize.Orders.Contains(this))
                {
                    FingerSize.Orders.Add(this);
                }
                if (FingerSizeID != FingerSize.FingerSizeID)
                {
                    FingerSizeID = FingerSize.FingerSizeID;
                }
            }
            else if (!_settingFK)
            {
                FingerSizeID = null;
            }
        }
    
        private void FixupMetal(Metal previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (Metal != null)
            {
                if (!Metal.Orders.Contains(this))
                {
                    Metal.Orders.Add(this);
                }
                if (MetalID != Metal.MetalID)
                {
                    MetalID = Metal.MetalID;
                }
            }
            else if (!_settingFK)
            {
                MetalID = null;
            }
        }
    
        private void FixupOrderStatu(OrderStatu previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (OrderStatu != null)
            {
                if (!OrderStatu.Orders.Contains(this))
                {
                    OrderStatu.Orders.Add(this);
                }
                if (OrderStatusID != OrderStatu.StatusID)
                {
                    OrderStatusID = OrderStatu.StatusID;
                }
            }
        }
    
        private void FixupPriority(Priority previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (Priority != null)
            {
                if (!Priority.Orders.Contains(this))
                {
                    Priority.Orders.Add(this);
                }
                if (PriorityID != Priority.PriorityID)
                {
                    PriorityID = Priority.PriorityID;
                }
            }
        }
    
        private void FixupProcessType(ProcessType previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (ProcessType != null)
            {
                if (!ProcessType.Orders.Contains(this))
                {
                    ProcessType.Orders.Add(this);
                }
                if (ProcessTypeID != ProcessType.ProcessTypeID)
                {
                    ProcessTypeID = ProcessType.ProcessTypeID;
                }
            }
        }
    
        private void FixupRingType(RingType previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (RingType != null)
            {
                if (!RingType.Orders.Contains(this))
                {
                    RingType.Orders.Add(this);
                }
                if (RingTypeID != RingType.RingTypeID)
                {
                    RingTypeID = RingType.RingTypeID;
                }
            }
            else if (!_settingFK)
            {
                RingTypeID = null;
            }
        }
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.Orders.Contains(this))
                {
                    User.Orders.Add(this);
                }
                if (AssignedTo != User.UserID)
                {
                    AssignedTo = User.UserID;
                }
            }
            else if (!_settingFK)
            {
                AssignedTo = null;
            }
        }
    
        private void FixupCustomer(Customer previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (Customer != null)
            {
                if (!Customer.Orders.Contains(this))
                {
                    Customer.Orders.Add(this);
                }
                if (CustomerID != Customer.CustomerID)
                {
                    CustomerID = Customer.CustomerID;
                }
            }
        }
    
        private void FixupModelType(ModelType previousValue)
        {
            if (previousValue != null && previousValue.Orders.Contains(this))
            {
                previousValue.Orders.Remove(this);
            }
    
            if (ModelType != null)
            {
                if (!ModelType.Orders.Contains(this))
                {
                    ModelType.Orders.Add(this);
                }
                if (ModelTypeID != ModelType.ModelTypeID)
                {
                    ModelTypeID = ModelType.ModelTypeID;
                }
            }
        }
    
        private void FixupMessageThreads(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MessageThread item in e.NewItems)
                {
                    item.Order = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MessageThread item in e.OldItems)
                {
                    if (ReferenceEquals(item.Order, this))
                    {
                        item.Order = null;
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
                    item.Order = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OrderParticipant item in e.OldItems)
                {
                    if (ReferenceEquals(item.Order, this))
                    {
                        item.Order = null;
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
                    item.Order = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OrdersCAD item in e.OldItems)
                {
                    if (ReferenceEquals(item.Order, this))
                    {
                        item.Order = null;
                    }
                }
            }
        }
    
        private void FixupSpecimens(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Specimen item in e.NewItems)
                {
                    item.Order = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Specimen item in e.OldItems)
                {
                    if (ReferenceEquals(item.Order, this))
                    {
                        item.Order = null;
                    }
                }
            }
        }

        #endregion
    }
}
