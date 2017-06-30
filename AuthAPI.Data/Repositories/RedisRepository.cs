using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthAPI.Data.Providers;
namespace AuthAPI.Data.Repositories
{
    public class RedisRepository : RedisProvider
    {
        public void SaveToken(string deviceID, string token)
        {
           db.StringSet("token:"+ token + ":deviceid", deviceID);
        }
        public void SaveCar(string deviceID, string carID)
        {
            db.StringSet("deviceid:" + deviceID + ":carid", carID);
        }
        public void SetValue(string key, string value)
        {
            db.StringSet(key, value);
        }
        public string GetByKey(string key)
        {
           return db.StringGet(key);
        }

    }
}
