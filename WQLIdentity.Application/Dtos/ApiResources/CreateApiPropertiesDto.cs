using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
