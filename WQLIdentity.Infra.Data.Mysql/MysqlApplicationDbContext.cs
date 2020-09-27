using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WQLIdentity.Infra.Data.Entities;


namespace WQLIdentity.Infra.Data
{
    public class MysqlApplicationDbContext : ApplicationDbContext<MysqlApplicationDbContext>
    {
        public MysqlApplicationDbContext(DbContextOptions<MysqlApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            //mysql字段编辑
            builder.Entity<ApplicationUser>().Property(d => d.NormalizedUserName).HasMaxLength(128);
            builder.Entity<ApplicationRole>().Property(d => d.NormalizedName).HasMaxLength(128);
            builder.Entity<IdentityUserLogin<int>>().Property(d => d.LoginProvider).HasMaxLength(128);
            builder.Entity<IdentityUserLogin<int>>().Property(d => d.ProviderKey).HasMaxLength(128);
            builder.Entity<IdentityUserToken<int>>().Property(d => d.LoginProvider).HasMaxLength(128);
            builder.Entity<IdentityUserToken<int>>().Property(d => d.Name).HasMaxLength(128);
        }
    }
}
