using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collaboration.Business.Entities
{
    partial class Order
    {       
        public virtual int UserID
        {
            get;
            set;
        }
        public virtual string SampleSerialNumber
        {
            get;
            set;
        }
        public virtual bool UpdateCADInfo
        {
            get;
            set;
        }
        public virtual bool UpdateSampleInfo
        {
            get;
            set;
        }
        public virtual string SpecimenData
        {
            get;
            set;
        }
    }
}
