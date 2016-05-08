using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Collaboration.Business.Entities
{
    public partial class SamplesTrackingHistory_Result
    {
        #region Primitive Properties

        public int SampleTrackHistoryID
        {
            get;
            set;
        }

        public int SampleID
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public string SampleSerialNumber
        {
            get;
            set;
        }



        public string SampleStatusName
        {
            get;
            set;
        }



        public Nullable<int> SampleStatusID
        {
            get;
            set;
        }

        public Nullable<System.DateTime> CreateDate
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string FLName
        {
            get;
            set;
        }

        #endregion
    }
}
