using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WQLIdentity.Infra.Data.Entities
{
    public class ApplicationUser:IdentityUser<int>
    {

        public ApplicationUser()
        {
            CreatedOn = DateTime.Now;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override int Id { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Department { get; set; }
        public string Picture { get; set; }



    }
}
