using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WQLIdentity.Application.Dtos.Claims
{
    public class CreateClaimDto
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
