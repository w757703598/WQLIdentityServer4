using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WQLIdentity.Application.Dtos
{
    public class CreateUserOrRoleClaimDto
    {
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Id { get; set; }
    }
}
