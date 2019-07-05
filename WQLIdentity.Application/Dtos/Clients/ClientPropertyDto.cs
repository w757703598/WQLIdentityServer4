using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Application.Dtos.Clients
{
    public class ClientPropertyDto
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int ClientId { get; set; }
    }
}
