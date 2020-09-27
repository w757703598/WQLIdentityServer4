using System.ComponentModel.DataAnnotations;

namespace WQLIdentity.Application.Dtos.UserManager
{
    public class PasswordDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
