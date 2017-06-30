using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.auth.api.web.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        User GetById(CancellationToken token, int id);
    }
}
