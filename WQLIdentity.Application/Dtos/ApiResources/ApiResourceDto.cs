using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class ApiResourceDto
    {

        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public List<string> UserClaims { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        public bool NonEditable { get; set; }
    }
}
