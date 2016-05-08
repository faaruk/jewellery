using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collaboration.Business.Entities;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using System.Data.Entity;

namespace Collaboration.Data
{
    public class AdminContext
    {
        #region ModelType
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ModelType> GetModelTypes()
        {
            using (var context = new CollaborationDBContext())
            {
                var modelType = context.GetGetModelTypes(0);
                return modelType.ToList < ModelType>();
            }
        } 
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public ModelType GetModelType(int ModelTypeID)
        {
            using (var context = new CollaborationDBContext())
            {
                var modelTypes = context.GetGetModelTypes(ModelTypeID).First();                
                return modelTypes;
            }
        }
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateModelType(ModelType modelType)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateModelType(modelType.ModelCode, modelType.Description, modelType.SortOrder, modelType.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyModelType(ModelType modelType)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyModelType(modelType.ModelTypeID, modelType.ModelCode, modelType.Description,modelType.SortOrder, modelType.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool DeleteModelType(int modelTypeID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteModelType(modelTypeID);
                return true;
            }
        }
        #endregion ModelType

        #region Metal
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Metal> GetMetals()
        {
            using (var context = new CollaborationDBContext())
            {
                var metals = context.GetMetals(0);
                return metals.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Metal GetMetal(int metalID)
        {
            using (var context = new CollaborationDBContext())
            {
                var metals = context.GetMetals(metalID).First();
                return metals;
            }
        }
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateMetal(Metal metal)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateMetal(metal.MetalName, metal.Description, metal.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyMetal(Metal metal)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyMetal(metal.MetalID, metal.MetalName, metal.Description, metal.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool DeleteMetal(int metalID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteMetal(metalID);
                return true;
            }
        }
        #endregion Metal

        #region ProcessType
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProcessType> GetProcessTypes()
        {
            using (var context = new CollaborationDBContext())
            {
                var ProcessType = context.GetProcessTypes(0);
                return ProcessType.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ProcessType GetProcessType(int ProcessTypeID)
        {
            using (var context = new CollaborationDBContext())
            {
                var ProcessType = context.GetProcessTypes(ProcessTypeID).First();
                return ProcessType;
            }
        }
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateProcessType(ProcessType ProcessType)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateProcessType(ProcessType.Type, ProcessType.Description, ProcessType.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyProcessType(ProcessType ProcessType)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyProcessType(ProcessType.ProcessTypeID, ProcessType.Type, ProcessType.Description, ProcessType.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool DeleteProcessType(int ProcessTypelID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteProcessType(ProcessTypelID);
                return true;
            }
        }
        #endregion ProcessType

        #region FingerSize
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FingerSize> GetFingerSizes()
        {
            using (var context = new CollaborationDBContext())
            {
                //int? sizeis;
                //sizeis = DBNull.Value;
                var fingerSize = context.GetFingerSizes(0);
                return fingerSize.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FingerSize GetFingerSize(int fingerSizeID)
        {
            using (var context = new CollaborationDBContext())
            {
                var fingerSize = context.GetFingerSizes(fingerSizeID).First();
                return fingerSize;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateFingerSize(FingerSize fingerSize)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateFingerSize(fingerSize.Size, fingerSize.Description, fingerSize.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyFingerSize(FingerSize fingerSize)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyFingerSize(fingerSize.FingerSizeID, fingerSize.Size, fingerSize.Description, fingerSize.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool DeleteFingerSize(int fingerSizeID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteFingerSize(fingerSizeID);
                return true;
            }
        }
        #endregion FingerSize       

        #region Priority
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Priority> GetPriorities()
        {
            using (var context = new CollaborationDBContext())
            {
                //int? sizeis;
                //sizeis = DBNull.Value;
                var Priority = context.GetPriorities(0);
                return Priority.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Priority GetPriority(int PriorityID)
        {
            using (var context = new CollaborationDBContext())
            {
                var Priority = context.GetPriorities(PriorityID).First();
                return Priority;
            }
        }
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreatePriority(Priority Priority)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreatePriority(Priority.Name, Priority.Frequency,Priority.Description, Priority.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyPriority(Priority Priority)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyPriority(Priority.PriorityID, Priority.Name,Priority.Frequency, Priority.Description, Priority.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool DeletePriority(int PriorityID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeletePriority(PriorityID);
                return true;
            }
        }
        #endregion Priority       

        #region RingType
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<RingType> GetRingTypes()
        {
            using (var context = new CollaborationDBContext())
            {               
                var RingType = context.GetRingTypes(0);
                return RingType.ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RingType GetRingType(int RingTypeID)
        {
            using (var context = new CollaborationDBContext())
            {
                var RingType = context.GetRingTypes(RingTypeID).First();
                return RingType;
            }
        }
        // <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateRingType(RingType RingType)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateRingType(RingType.Type, RingType.Description, RingType.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyRingType(RingType RingType)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyRingType(RingType.RingTypeID, RingType.Type, RingType.Description, RingType.IsActive);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool DeleteRingType(int RingTypeID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteRingType(RingTypeID);
                return true;
            }
        }
        #endregion RingType       

        #region Customer
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            using (var context = new CollaborationDBContext())
            {
                var customers = context.GetCustomers(0);
                return customers.OrderByDescending(c => c.CreateDate).ToList();
            }
        }
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CreateCustomer(Customer customer)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateCustomer(customer.CustomerCode, customer.ReferenceCode, customer.CustomerName, customer.CustomerEmail);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelTypeInfo"></param>
        /// <returns></returns>
        public bool ModifyCustomer(Customer customer)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifyCustomer(customer.CustomerID, customer.CustomerCode, customer.ReferenceCode, customer.CustomerName, customer.CustomerEmail);
                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserInfo"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int customerID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteCustomer(customerID);
                return true;
            }
        }
        public int ImportCustomers(string filePath)
        {
            // Connect to Excel 2007 earlier version
            //string sSourceConstr = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\AgentList.xls; Extended Properties=""Excel 8.0;HDR=YES;""";
            // Connect to Excel 2007 (and later) files with the Xlsx file extension 
            string extension = Path.GetExtension(filePath);
            string sSourceConstr = string.Empty;
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    sSourceConstr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    //sSourceConstr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    break;
                case ".xlsx": //Excel 07 or higher
                    sSourceConstr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    //sSourceConstr = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    break;

            }
           
           // sSourceConstr = string.Format(sSourceConstr, filePath);
            string sDestConstr = ConfigurationManager.ConnectionStrings["RiverMountConnectionString"].ConnectionString;
            OleDbConnection sSourceConnection = new OleDbConnection(sSourceConstr);
            using (sSourceConnection)
            {
                var sheetNames = sSourceConnection.GetSchema("Tables");
                string name = "";
                foreach (DataRow row in sheetNames.Rows)
                {
                    name = row["TABLE_NAME"].ToString();
                    //select from this sheet
                    //do whatever else
                }
                string sql = string.Format("Select [Customer Code],[Name 1],[Email] FROM [{0}]", name);
                OleDbCommand command = new OleDbCommand(sql, sSourceConnection);
                sSourceConnection.Open();
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    int a = dr.RecordsAffected;
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sDestConstr))
                    {
                        bulkCopy.DestinationTableName = "Customer_Temp";
                        bulkCopy.ColumnMappings.Add("Customer Code", "CustomerCode");
                        //bulkCopy.ColumnMappings.Add("Ref. Code", "ReferenceCode");
                        bulkCopy.ColumnMappings.Add("Name 1", "CustomerName");
                        bulkCopy.ColumnMappings.Add("Email", "CustomerEmail");
                        bulkCopy.WriteToServer(dr);
                    }
                }
            }
            using (var context = new CollaborationDBContext())
            {
                ObjectResult<int?> iReturnValue = context.ImportCustomer();
                return Convert.ToInt32(iReturnValue.SingleOrDefault());
            }
            return 0;
        }
        #endregion


        #region SampleStatus

       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<SampleStatu> GetSampleStatus()
        {
            using (var context = new CollaborationDBContext())
            {
                var sampleStatus = context.GetSampleStatus(0);
                return sampleStatus.ToList();
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatusID"></param>
        /// <returns></returns>
        public SampleStatu GetSampleStatus(int sampleStatusID)
        {
            using (var context = new CollaborationDBContext())
            {
                var sampleStatus = context.GetSampleStatus(sampleStatusID).First();
                return sampleStatus;
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatus"></param>
        /// <returns></returns>
        public bool CreateSampleStatus(SampleStatu sampleStatus)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.CreateSampleStatus(sampleStatus.SampleStatusName, sampleStatus.Description,sampleStatus.IsActive);
                return true;
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatus"></param>
        /// <returns></returns>
        public bool ModifySampleStatus(SampleStatu sampleStatus)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.ModifySampleStatus(sampleStatus.SampleStatusID, sampleStatus.SampleStatusName, sampleStatus.Description,sampleStatus.IsActive);
                return true;
            }
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleStatusID"></param>
        /// <returns></returns>
        public bool DeleteSampleStatus(int sampleStatusID)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.DeleteSampleStatus(sampleStatusID);
                return true;
            }
        }

        #endregion SampleStatus


        #region SampleTracking

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SampleTracking_Result> SamplesTracking_Result()
        {
            using (var context = new CollaborationDBContext())
            {
                
                var sampleTrack = context.SamplesTracking_Result(0);
                return sampleTrack.ToList(); 
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTrackID"></param>
        /// <returns></returns>
        public SamplesTracking SamplesTracking_Select(int sampleTrackID)
        {
            using (var context = new CollaborationDBContext())
            {
                context.ContextOptions.LazyLoadingEnabled = false;
                var lstSampleTrack = context.SamplesTracking_Select(sampleTrackID).First();
                
                return lstSampleTrack;

               
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTrack"></param>
        /// <returns></returns>
        public bool SamplesTracking_Insert(SamplesTracking sampleTrack)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.SamplesTracking_Insert(sampleTrack.SampleID, sampleTrack.SampleStatusID, sampleTrack.IsActive);
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sampleTrack"></param>
        /// <returns></returns>
        public bool SamplesTracking_Update(SamplesTracking sampleTrack)
        {
            using (var context = new CollaborationDBContext())
            {
                var iReturnValue = context.SamplesTracking_Update(sampleTrack.SampleTrackID, sampleTrack.SampleID, sampleTrack.SampleStatusID, sampleTrack.IsActive, sampleTrack.Updatedby);
                return true;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sampleStatusID"></param>
        ///// <returns></returns>
        //public bool DeleteSampleStatus(int sampleStatusID)
        //{
        //    using (var context = new CollaborationDBContext())
        //    {
        //        var iReturnValue = context.DeleteSampleStatus(sampleStatusID);
        //        return true;
        //    }
        //}

        #endregion SampleTracking



    }
}
