using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class UpdateApiResource
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<string> UserClaims { get; set; }
    }
}
