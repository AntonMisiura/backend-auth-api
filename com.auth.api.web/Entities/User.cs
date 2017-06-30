using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.auth.api.web.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public int Password { get; set; }

    }
}
