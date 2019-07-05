using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
