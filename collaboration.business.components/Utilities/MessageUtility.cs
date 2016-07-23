using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
namespace Collaboration.Business.Components.Utilities
{
    public class MessageUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static BLLException GetErrorMessage(Exception exceptionMessage)
        {
            BLLException bllException = new BLLException();
            bllException.InnerException = exceptionMessage.InnerException;

           // exceptionMessage = bllException.;
            Exception inner = exceptionMessage.InnerException;
            string paramName = string.Empty;
            exceptionMessage.GetType();
            if (inner is SqlException)
            {
                SqlException SqlExp = inner as SqlException;

                if (SqlExp != null)
                {
                    switch (SqlExp.Number)
                    {
                        case 515:                            
                            bllException.ErrorMessage = string.Format(Resources.NullValueException, SqlExp.Message.Substring(SqlExp.Message.IndexOf("into column")));
                            bllException.ProcedureName = SqlExp.Procedure;
                            break;
                        case 547:
                            bllException.ErrorMessage = Resources.DalFKViolation;
                            bllException.ProcedureName = SqlExp.Procedure;
                            break;
                        case 2627:
                            bllException.ErrorMessage = string.Format(Resources.DalUniqueConstraintViolation, SqlExp.Message.Substring(SqlExp.Message.IndexOf("The duplicate key value is ")));
                            bllException.ProcedureName = SqlExp.Procedure;
                            break;
                        case 2707:
                            bllException.ErrorMessage = Resources.DalUniqueConstraintViolation;
                            bllException.ProcedureName = SqlExp.Procedure;
                            break;
                        case 8152:
                            bllException.ErrorMessage = Resources.MaxLenghtExceeded;
                            bllException.ProcedureName = SqlExp.Procedure;
                            break;
                        case 4060:
                            bllException.ErrorMessage = Resources.DalFailedToConnectToTheDB;
                            bllException.ProcedureName = SqlExp.Procedure;                           
                            break;
                        case 18456:
                            bllException.ErrorMessage = Resources.DalFailedToLogin;
                            bllException.ProcedureName = SqlExp.Procedure;
                          
                            break;
                        default:
                            bllException.ErrorMessage = Resources.DalExceptionOccured;
                            bllException.ProcedureName = SqlExp.Procedure;
                            break;
                    }                   
                }               
            }
            else if (exceptionMessage.Message == "No value given for one or more required parameters.")               
                    bllException.ErrorMessage = Resources.ImportColumnMissing;
            else if (exceptionMessage.Message == "Column 'CustomerCode' does not allow DBNull.Value.")
                bllException.ErrorMessage = Resources.ImportColumnInvalid;                 
            else if (inner is UpdateException)
            {
                bllException.ErrorMessage = Resources.DalExceptionOccured;
                bllException.ProcedureName = inner.Message;
            }
            else
            {
                bllException.ErrorMessage = Resources.DalExceptionOccured;
            }
            //else if (inner is ArgumentException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}
            //else if (inner is ArgumentOutOfRangeException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}
            //else if (inner is BadImageFormatException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}

            //else if (inner is ConstraintException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}

            //else if (inner is DBConcurrencyException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}

            //else if (inner is DuplicateNameException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}
            //else if (inner is EntitySqlException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}
            //else if (inner is InvalidConstraintException)
            //{
            //    paramName = ((ArgumentException)inner).ParamName;
            //    return string.Concat("The ", paramName, " value is illegal.");
            //}
            
            //else return "Some error Occured";
            //else if (inner is )
            //{
            //    string paramName = ((ArgumentException)inner).ParamName;
            //    ErrorMessage = string.Concat("The ", paramName, " value is illegal.");
            //}
            //ErrorMessage = "Some error occured";
            //if (exceptionMessage.InnerException != null)
            //{
            //    if (exceptionMessage.InnerException.ToString().Contains("Foreign"))
            //        ErrorMessage = "Cannot be deleted";
            //    else if (exceptionMessage.InnerException.ToString().Contains("Unique"))
            //        ErrorMessage = "Already Exists";                
            //}
            //return ErrorMessage;
            return bllException;
        }
    }
}
