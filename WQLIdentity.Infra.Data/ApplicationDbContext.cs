using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WQLIdentity.Domain.Entities;
using WQLIdentity.Infra.Data.Entities;


namespace WQLIdentity.Infra.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }
       
        public  DbSet<Claims> Claims { get; set; }
    }
}
