using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Business.Entities;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
namespace Collaboration.Data
{
    public class SampleHistoryContext
    {
        #region SampleHistory

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<SamplesTrackingHistory_Result> GetSamplesTrackingHistory(int SampleID, int SampleTrackID, int userID)
        {
            using (var context = new CollaborationDBContext())
            {
                var messages = context.SamplesTrackingHistory_Result(SampleID);
                return messages.ToList();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool SamplesTrackingMassUpdate(string SampleIDs, int SampleStatusID, int Updatedby)
        {
            using (var context = new CollaborationDBContext())
            {

                var iReturnValue = context.SamplesTracking_MassUpdate(SampleIDs, SampleStatusID, Updatedby);
                return true;
            }
        }
        #endregion SampleHistory
    }
}
