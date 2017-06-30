using Npgsql;
using AuthAPI.Data.Providers;
using System;
using System.Data;

namespace AuthAPI.Data.Repositories
{
    public class PostgresDBProvider : IDisposable
    {
        private ConnectionStringProvider _provider;
        protected IDbConnection Connection { get; private set; }

        public PostgresDBProvider()
        {
           
            _provider = new ConnectionStringProvider("PostgresConnection");
            Connection = new NpgsqlConnection(_provider.ConnectionString);
            
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
