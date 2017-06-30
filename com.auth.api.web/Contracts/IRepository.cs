using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.auth.api.web.Contracts
{
    public interface IRepository<User>
    {
        User GetById(CancellationToken token, int id);

        IEnumerable<User> GetAll(CancellationToken token);
    }
}
