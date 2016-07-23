using System;
using System.Configuration;


namespace Collaboration.Data
{
    public class ConnectionFactory
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["RiverMountConnectionString"].ConnectionString; }
        }

        public static CollaborationLinqDataContext GetCollaborationLinqDataContext()
        {
            return new CollaborationLinqDataContext(ConnectionString);
        }
    }
}
