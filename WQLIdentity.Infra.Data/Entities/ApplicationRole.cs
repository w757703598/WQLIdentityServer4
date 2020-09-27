using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WQLIdentity.Infra.Data.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        {
            CreatedOn = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
