using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Data.Models
{
    public class DevicePlacement
    {
        public long DeviceId { get; set; }
        public long CarId { get; set; }
        public string SecretKey { get; set; }
    }
}
