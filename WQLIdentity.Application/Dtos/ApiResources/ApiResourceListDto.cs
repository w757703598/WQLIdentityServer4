using System;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class ApiResourceListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
    }
}
