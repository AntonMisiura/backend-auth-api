using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.auth.api.web.Impl.Repo
{
    public abstract class BaseRepository
    {
        protected AuthContext Context { get; set; }

        protected BaseRepository(AuthContext context)
        {
            Context = context;
        }
    }
}
