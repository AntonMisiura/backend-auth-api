using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Data.Providers
{
    class ConnectionStringProvider
    {
        string _connectionName;
        public ConnectionStringProvider(string connectionName)
        {
            _connectionName = connectionName;
        }
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[_connectionName].ToString();
            }
        }
    }
}
