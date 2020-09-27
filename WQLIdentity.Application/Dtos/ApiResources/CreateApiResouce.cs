using System.Collections.Generic;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class CreateApiResouce
    {

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<string> UserClaims { get; set; }
    }
}
