using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
namespace AuthAPI.Data.Providers
{
     public class RedisProvider: IDisposable
    {
        ConnectionStringProvider _connectionString;
        ConnectionMultiplexer redis;
        protected IDatabase db;
        string _serverName; int _databaseName;
        
        public RedisProvider()
        {
             _connectionString = new ConnectionStringProvider("RedisConnection");
             parseString();
             redis = ConnectionMultiplexer.Connect(_serverName);
            db = redis.GetDatabase(_databaseName);
        }
        void parseString() {
            var x = _connectionString.ConnectionString.Split(';');
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i].IndexOf("Server") != -1) {
                    _serverName = x[i].Substring(x[i].IndexOf('=')+1);
                }
                if (x[i].IndexOf("Database") != -1)
                {
                    _databaseName = Int32.Parse(x[i].Substring(x[i].IndexOf('=')+1));
                }
            }
        }
        public void Dispose()
        {
            redis.Close();
        }

    }
}
