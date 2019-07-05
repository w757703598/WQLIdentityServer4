using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WQLIdentityServer.Infra.Dto;

namespace WQLIdentity.Application.Dtos.ApiResources
{
    public class CreateApiSecretDto
    {
        public string Description { get; set; }
        [Required]
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        [Required]
        public HashType? Hash { get; set; } = 0;
        [Required]
        public string Type { get; set; }
        public int ApiResourceId { get; set; }
    }
}
