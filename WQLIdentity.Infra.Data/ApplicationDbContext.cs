using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Domain.Entities;
using WQLIdentity.Infra.Data.Entities;


namespace WQLIdentity.Infra.Data
{
    public class ApplicationDbContext<TDbcontext>: IdentityDbContext<ApplicationUser,ApplicationRole,int> where TDbcontext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<TDbcontext> options) : base(options)
        {
           
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    base.OnModelCreating(builder);
        //    //mysql字段编辑
        //    builder.Entity<ApplicationUser>().Property(d => d.NormalizedUserName).HasMaxLength(128);
        //    builder.Entity<ApplicationRole>().Property(d => d.NormalizedName).HasMaxLength(128);
        //    builder.Entity<IdentityUserLogin<int>>().Property(d => d.LoginProvider).HasMaxLength(128);
        //    builder.Entity<IdentityUserLogin<int>>().Property(d => d.ProviderKey).HasMaxLength(128);
        //    builder.Entity<IdentityUserToken<int>>().Property(d => d.LoginProvider).HasMaxLength(128);
        //    builder.Entity<IdentityUserToken<int>>().Property(d => d.Name).HasMaxLength(128);
        //}
        public  DbSet<Claims> Claims { get; set; }
    }
    public class ApplicationDbContext : ApplicationDbContext<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }


}
