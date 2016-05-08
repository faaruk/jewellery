using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Data;
using Collaboration.Business.Entities;
using Collaboration.Business.Components.Utilities;
namespace Collaboration.Business.Components
{
    public class AdminManager
    {
        #region ModelType
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModelTypeID"></param>
        /// <returns></returns>
        public List<ModelType> GetModelTypes()
        {
            try
            {
                var lstModel = new AdminContext().GetModelTypes();
                return lstModel.ToList<ModelType>();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModelTypeID"></param>
        /// <returns></returns>
        public ModelType GetModelType(int modelTypeID)
        {
            try
            {
                return new AdminContext().GetModelType(modelTypeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModelType"></param>
        /// <returns></returns>
        public bool ModifyModelType(ModelType modelType)
        {
            try 
            {
                return new AdminContext().ModifyModelType(modelType); 
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }       
       /// <summary>
       /// 
       /// </summary>
       /// <param name="ModelType"></param>
       /// <returns></returns>
        public bool CreateModelType(ModelType modelType)
        {
            try
            {
                return new AdminContext().CreateModelType(modelType);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }            
        }       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModelTypeID"></param>
        /// <returns></returns>
        public bool DeleteModelType(int modelTypeID)
        {
            try
            {
                return new AdminContext().DeleteModelType(modelTypeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }

        }
        #endregion  ModelType

        #region Metal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MetalID"></param>
        /// <returns></returns>
        public List<Metal> GetMetals()
        {
            try
            {
                return new AdminContext().GetMetals();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MetalID"></param>
        /// <returns></returns>
        public Metal GetMetal(int metalID)
        {
            try
            {
                return new AdminContext().GetMetal(metalID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Metal"></param>
        /// <returns></returns>
        public bool ModifyMetal(Metal metal)
        {
            try
            {
                return new AdminContext().ModifyMetal(metal);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Metal"></param>
        /// <returns></returns>
        public bool CreateMetal(Metal metal)
        {
            try
            {
                return new AdminContext().CreateMetal(metal);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MetalID"></param>
        /// <returns></returns>
        public bool DeleteMetal(int metalID)
        {
            try
            {
                return new AdminContext().DeleteMetal(metalID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }

        }
        #endregion  Metal

        #region ProcessType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessTypeID"></param>
        /// <returns></returns>
        public List<ProcessType> GetProcessTypes()
        {
            try
            {
                return new AdminContext().GetProcessTypes();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessTypeID"></param>
        /// <returns></returns>
        public ProcessType GetProcessType(int ProcessTypeID)
        {
            try
            {
                return new AdminContext().GetProcessType(ProcessTypeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessType"></param>
        /// <returns></returns>
        public bool ModifyProcessType(ProcessType ProcessType)
        {
            try
            {
                return new AdminContext().ModifyProcessType(ProcessType);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessType"></param>
        /// <returns></returns>
        public bool CreateProcessType(ProcessType ProcessType)
        {
            try
            {
                return new AdminContext().CreateProcessType(ProcessType);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessTypeID"></param>
        /// <returns></returns>
        public bool DeleteProcessType(int ProcessTypeID)
        {
            try
            {
                return new AdminContext().DeleteProcessType(ProcessTypeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }

        }
        #endregion  ProcessType

        #region FingerSize

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FingerSizeID"></param>
        /// <returns></returns>
        public List<FingerSize> GetFingerSizes()
        {
            try
            {
                return new AdminContext().GetFingerSizes();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FingerSizeID"></param>
        /// <returns></returns>
        public FingerSize GetFingerSize(int fingerSizeID)
        {
            try
            {
                return new AdminContext().GetFingerSize(fingerSizeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FingerSize"></param>
        /// <returns></returns>
        public bool ModifyFingerSize(FingerSize fingerSize)
        {
            try
            {
                return new AdminContext().ModifyFingerSize(fingerSize);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FingerSize"></param>
        /// <returns></returns>
        public bool CreateFingerSize(FingerSize fingerSize)
        {
            try
            {
                return new AdminContext().CreateFingerSize(fingerSize);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FingerSizeID"></param>
        /// <returns></returns>
        public bool DeleteFingerSize(int fingerSizeID)
        {
            try
            {
                return new AdminContext().DeleteFingerSize(fingerSizeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }

        }
        #endregion  FingerSize 

        #region Priority

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <returns></returns>
        public List<Priority> GetPriorities()
        {
            try
            {
                return new AdminContext().GetPriorities();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <returns></returns>
        public Priority GetPriority(int PriorityID)
        {
            try
            {
                return new AdminContext().GetPriority(PriorityID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Priority"></param>
        /// <returns></returns>
        public bool ModifyPriority(Priority Priority)
        {
            try
            {
                return new AdminContext().ModifyPriority(Priority);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Priority"></param>
        /// <returns></returns>
        public bool CreatePriority(Priority Priority)
        {
            try
            {
                return new AdminContext().CreatePriority(Priority);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PriorityID"></param>
        /// <returns></returns>
        public bool DeletePriority(int PriorityID)
        {
            try
            {
                return new AdminContext().DeletePriority(PriorityID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }

        }
        #endregion  Priority 

        #region RingType

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RingTypeID"></param>
        /// <returns></returns>
        public List<RingType> GetRingTypes()
        {
            try
            {
                return new AdminContext().GetRingTypes();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RingTypeID"></param>
        /// <returns></returns>
        public RingType GetRingType(int RingTypeID)
        {
            try
            {
                return new AdminContext().GetRingType(RingTypeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RingType"></param>
        /// <returns></returns>
        public bool ModifyRingType(RingType RingType)
        {
            try
            {
                return new AdminContext().ModifyRingType(RingType);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RingType"></param>
        /// <returns></returns>
        public bool CreateRingType(RingType RingType)
        {
            try
            {
                return new AdminContext().CreateRingType(RingType);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RingTypeID"></param>
        /// <returns></returns>
        public bool DeleteRingType(int RingTypeID)
        {
            try
            {
                return new AdminContext().DeleteRingType(RingTypeID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }

        }
        #endregion  RingType 

        #region Customer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            try
            {
                return new AdminContext().GetCustomers();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateCustomer(Customer customer)
        {
            try
            {
                return new AdminContext().CreateCustomer(customer);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool ModifyCustomer(int CustomerID, string CustomerCode, string CustomerName, string CustomerEmail)
        {
            try
            {
                return new AdminContext().ModifyCustomer(new Customer { CustomerID = CustomerID, CustomerCode = CustomerCode, CustomerName = CustomerName, CustomerEmail = CustomerEmail});
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyCustomer(Customer customer)
        {
            try
            {
                return new AdminContext().ModifyCustomer(customer);
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
        public bool DeleteCustomer(int customerID)
        {
            try
            {
                return new AdminContext().DeleteCustomer(customerID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public int ImportCustomers(string filePath)
        {
            try
            {
                return new AdminContext().ImportCustomers(filePath);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }
        #endregion


        #region SampleStatus

      /// <summary>
      /// 
      /// </summary>
      /// <returns></returns>
        public List<SampleStatu> GetSampleStatus()
        {
            try
            {
                return new AdminContext().GetSampleStatus();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatusID"></param>
        /// <returns></returns>
        public SampleStatu GetSampleStatus(int sampleStatusID)
        {
            try
            {
                return new AdminContext().GetSampleStatus(sampleStatusID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatus"></param>
        /// <returns></returns>
        public bool ModifySampleStatus(SampleStatu sampleStatus)
        {
            try
            {
                return new AdminContext().ModifySampleStatus(sampleStatus);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatus"></param>
        /// <returns></returns>
        public bool CreateSampleStatus(SampleStatu sampleStatus)
        {
            try
            {
                return new AdminContext().CreateSampleStatus(sampleStatus);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        public bool DeleteSampleStatus(int sampleStatusID)
        {
            try
            {
                return new AdminContext().DeleteSampleStatus(sampleStatusID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }

        }
        #endregion  SampleStatus


        #region SampleTracking



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTrackID"></param>
        /// <returns></returns>
        public List<SampleTracking_Result> SamplesTracking_Result()
        {
            try
            {
                return new AdminContext().SamplesTracking_Result();
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTrackID"></param>
        /// <returns></returns>
        public SamplesTracking SamplesTracking_Select(int sampleTrackID)
        {
            try
            {
                return new AdminContext().SamplesTracking_Select(sampleTrackID);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTrack"></param>
        /// <returns></returns>
        public bool SamplesTracking_Update(SamplesTracking sampleTrack)
        {
            try
            {
                return new AdminContext().SamplesTracking_Update(sampleTrack);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTrack"></param>
        /// <returns></returns>
        public bool SamplesTracking_Insert(SamplesTracking sampleTrack)
        {
            try
            {
                return new AdminContext().SamplesTracking_Insert(sampleTrack);
            }
            catch (Exception ex)
            {
                throw MessageUtility.GetErrorMessage(ex);
            }
        }

        //public bool DeleteSampleStatus(int sampleStatusID)
        //{
        //    try
        //    {
        //        return new AdminContext().DeleteSampleStatus(sampleStatusID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw MessageUtility.GetErrorMessage(ex);
        //    }

        //}
        #endregion  SampleTracking

        }
}
