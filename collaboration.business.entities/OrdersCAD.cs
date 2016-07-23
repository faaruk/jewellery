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
    public partial class OrdersCAD
    {
        #region Primitive Properties
    
        public virtual int CADID
        {
            get;
            set;
        }
    
        public virtual int OrderID
        {
            get { return _orderID; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_orderID != value)
                    {
                        if (Order != null && Order.OrderID != value)
                        {
                            Order = null;
                        }
                        _orderID = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private int _orderID;
    
        public virtual string CADLocationURL
        {
            get;
            set;
        }
    
        public virtual Nullable<int> UploadedBy
        {
            get { return _uploadedBy; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_uploadedBy != value)
                    {
                        if (User != null && User.UserID != value)
                        {
                            User = null;
                        }
                        _uploadedBy = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<int> _uploadedBy;
    
        public virtual Nullable<bool> IsApproved
        {
            get;
            set;
        }
        public virtual Nullable<bool> IsUpdatedByCustomer
        {
            get;
            set;
        }
        
    
        public virtual string Remarks
        {
            get;
            set;
        }
    
        public virtual string ChangeInstructions
        {
            get;
            set;
        }

        public virtual string ChangeInstructionsCustomer
        {
            get;
            set;
        }
        
        public virtual Nullable<System.DateTime> UploadedOn
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
    
        public virtual Order Order
        {
            get { return _order; }
            set
            {
                if (!ReferenceEquals(_order, value))
                {
                    var previousValue = _order;
                    _order = value;
                    FixupOrder(previousValue);
                }
            }
        }
        private Order _order;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupUser(User previousValue)
        {
            if (previousValue != null && previousValue.OrdersCADs.Contains(this))
            {
                previousValue.OrdersCADs.Remove(this);
            }
    
            if (User != null)
            {
                if (!User.OrdersCADs.Contains(this))
                {
                    User.OrdersCADs.Add(this);
                }
                if (UploadedBy != User.UserID)
                {
                    UploadedBy = User.UserID;
                }
            }
            else if (!_settingFK)
            {
                UploadedBy = null;
            }
        }
    
        private void FixupOrder(Order previousValue)
        {
            if (previousValue != null && previousValue.OrdersCADs.Contains(this))
            {
                previousValue.OrdersCADs.Remove(this);
            }
    
            if (Order != null)
            {
                if (!Order.OrdersCADs.Contains(this))
                {
                    Order.OrdersCADs.Add(this);
                }
                if (OrderID != Order.OrderID)
                {
                    OrderID = Order.OrderID;
                }
            }
        }

        #endregion
    }
}
