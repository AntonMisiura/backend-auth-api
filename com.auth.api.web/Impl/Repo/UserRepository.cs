using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.auth.api.web.Impl.Repo
{
    public class UserRepository
    {
        public UserRepository(AuthContext context) : base(context)
        {
        }

        public User GetById(CancellationToken token, int id)
        {
            return Context.Set<User>().FirstOrDefault(e => e.Id == id);
        }
    }
}
