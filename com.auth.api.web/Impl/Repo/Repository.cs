using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.auth.api.web.Impl.Repo
{
    public class Repository : IRepository<User>
    {
        protected TubeContext Context { get; private set; }

        public Repository(TubeContext context)
        {
            Context = context;
        }

        public User GetById(CancellationToken token, int id)
        {
            return Context.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<User> GetAll(CancellationToken token)
        {
            return Context.Set<T>().ToList();
        }
    }
}
