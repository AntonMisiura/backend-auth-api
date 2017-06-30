using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Data.Models
{
    public class Device
    {
        public long Id { get; set; }
        public string OriginalId { get; set; }
        public string Info { get; set; }
    }
}
