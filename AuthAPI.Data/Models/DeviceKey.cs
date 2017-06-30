using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Data.Models
{
    public class DeviceKey
    {
        public long ID { get; set; }
        public string OriginalID { get; set; }
        public long CarID { get; set; }
        public string SecretKey { get; set; }
       
    }
    public class DeviceKeyParce
    {
        public long ID { get; set; }
        public string Original_ID { get; set; }
        public long Car_ID { get; set; }
        public string Secret_Key { get; set; }

    }
}
