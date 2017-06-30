using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.auth.api.web.Impl
{
    public class AuthContext : DbContext
    {
        private IConfigurationRoot _config;
    
        public AuthContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
            //Database.SetInitializer<TubeContext>(null);
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder.UseSqlServer(_config["ConnectionStrings:TubeContextConnection"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
        }
    }

}
