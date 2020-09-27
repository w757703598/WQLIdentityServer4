using System.ComponentModel.DataAnnotations;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Dtos.Clients
{
    public class CreateClientDto
    {
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string ClientId { get; set; }
        public ClientType ClientType { get; set; } = 0;
    }
}
