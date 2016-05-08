using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Data;
using Collaboration.Business.Entities;
using Collaboration.Business.Components.Utilities;
namespace Collaboration.Business.Components
{
    public class SampleHistoryManager
    {
        #region SampleHistory


        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public List<SamplesTrackingHistory_Result> GetHistory(int SampleID, int SampleTrackID, int userID)
        {
            try
            {
                return new SampleHistoryContext().GetSamplesTrackingHistory(SampleID, SampleTrackID, userID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool SamplesTrackingMassUpdate(string SampleIDs, int SampleStatusID, int Updatedby)
        {
            try
            {
                return new SampleHistoryContext().SamplesTrackingMassUpdate(SampleIDs, SampleStatusID, Updatedby);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        #endregion SampleHistory
    }
}
