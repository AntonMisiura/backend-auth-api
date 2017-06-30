using Dapper;
using AuthAPI.Data.Models;
using System.Linq;


namespace AuthAPI.Data.Repositories
{
    public class PostgresRepository : PostgresDBProvider
    {
        public DeviceKey GetById(string original_id)
        {
            var result = Connection.Query<DeviceKeyParce>("select * from config.device inner join config.device_placement on config.device.id = config.device_placement.device_id where config.device.original_id = @orig_id",
                new { orig_id = original_id }).FirstOrDefault();
            DeviceKey key = new DeviceKey();
            if (result != null) {
                key.ID = result.ID;
                key.OriginalID = result.Original_ID;
                key.SecretKey = result.Secret_Key;
                key.CarID = result.Car_ID;
            }
            
            return key;
        }
    }
}
