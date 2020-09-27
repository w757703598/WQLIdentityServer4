using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace WQLIdentity.Infra.Data
{
    public class CustomConfigurationDbContext : ConfigurationDbContext<CustomConfigurationDbContext>
    {


        public CustomConfigurationDbContext(DbContextOptions<CustomConfigurationDbContext> options, ConfigurationStoreOptions storeOptions) : base(options, storeOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }

}
