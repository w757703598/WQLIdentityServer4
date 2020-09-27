using System.ComponentModel.DataAnnotations;

namespace WQLIdentity.Application.Dtos.Clients
{
    public class ClientClaimDto
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
