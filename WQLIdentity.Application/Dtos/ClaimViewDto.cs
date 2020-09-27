using System.ComponentModel.DataAnnotations;

namespace WQLIdentity.Application.Dtos
{
    public class ClaimViewDto
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }

    }
}
