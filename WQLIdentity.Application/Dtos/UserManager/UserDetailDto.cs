using System;

namespace WQLIdentity.Application.Dtos.UserManager
{
    public class UserDetailDto
    {

        public string UserName { get; set; }




        public string Picture { get; set; }

        public string Email { get; set; }



        public string PhoneNumber { get; set; }


        public bool LockoutEnabled { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public string AccessFailedCount { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }
        public DateTime CreatedOn { get; }

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
