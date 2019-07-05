using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Dtos.Clients
{
    public class ClientSecretDto
    {
        [Required]
        public string Type { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }
        [Required]
        public HashType? Hash { get; set; } = 0;
        [Required]
        public string Value { get; set; }

        public DateTime? Expiration { get; set; }
        [Required]
        public int ClientId { get; set; }
    }
}
