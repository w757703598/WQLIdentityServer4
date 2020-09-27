using System.ComponentModel.DataAnnotations;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class CreateApiPropertiesDto
    {
        public int ApiResourceId { get; set; }


        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
