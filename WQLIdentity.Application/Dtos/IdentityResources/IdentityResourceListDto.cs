using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Application.Dtos.IdentityResources
{
    public class IdentityResourceListDto
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }  
        public DateTime Created { get; set; }

    }
}
