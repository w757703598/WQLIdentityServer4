using System;
using System.Collections.Generic;
using System.Text;

namespace WQLIdentity.Application.Dtos.UserManager
{
    public class UserListDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }

        public string Department { get; set; }
        public string Email { get; set; }
    }
}
