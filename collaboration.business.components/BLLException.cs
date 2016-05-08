using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Collaboration.Business.Components
{
    public class BLLException : Exception
    {
        private string _errorMessage;
        public string ErrorMessage { set { _errorMessage = value; } get { return _errorMessage; } }

        private string _procedureName;
        public  string ProcedureName { set { _procedureName = value; } get { return _procedureName; } }

        private Exception _innerException;
        public new Exception InnerException { set { _innerException = value; } get { return _innerException; } }
    }
}
