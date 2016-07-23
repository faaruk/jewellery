﻿//------------------------------------------------------------------------------
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
    public partial class SamplesTrackingHistory
    {
        #region Primitive Properties

        public virtual int SampleTrackHistoryID
        {
            get;
            set;
        }

        public virtual Nullable<int> Updatedby
        {
            get;
            set;
        }
        public virtual Nullable<bool> IsActive
        {
            get;
            set;
        }

        public virtual Nullable<System.DateTime> CreateDate
        {
            get;
            set;
        }

        #endregion

        #region Navigation Properties

        public virtual Sample Sample
        {
            get { return _sample; }
            set
            {
                if (!ReferenceEquals(_sample, value))
                {
                    var previousValue = _sample;
                    _sample = value;
                    FixupSample(previousValue);
                }
            }
        }
        private Sample _sample;

        public virtual SampleStatu SampleStatu
        {
            get { return _sampleStatu; }
            set
            {
                if (!ReferenceEquals(_sampleStatu, value))
                {
                    var previousValue = _sampleStatu;
                    _sampleStatu = value;
                    FixupSampleStatu(previousValue);
                }
            }
        }
        private SampleStatu _sampleStatu;

        #endregion

        #region Association Fixup

        private void FixupSample(Sample previousValue)
        {
            if (previousValue != null && previousValue.SamplesTrackingHistories.Contains(this))
            {
                previousValue.SamplesTrackingHistories.Remove(this);
            }

            if (Sample != null)
            {
                if (!Sample.SamplesTrackingHistories.Contains(this))
                {
                    Sample.SamplesTrackingHistories.Add(this);
                }
            }
        }

        private void FixupSampleStatu(SampleStatu previousValue)
        {
            if (previousValue != null && previousValue.SamplesTrackingHistories.Contains(this))
            {
                previousValue.SamplesTrackingHistories.Remove(this);
            }

            if (SampleStatu != null)
            {
                if (!SampleStatu.SamplesTrackingHistories.Contains(this))
                {
                    SampleStatu.SamplesTrackingHistories.Add(this);
                }
            }
        }

        #endregion

    }
}