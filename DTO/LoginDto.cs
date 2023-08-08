using System.ComponentModel.DataAnnotations;

namespace Restfull.DTO
{
    public class LoginDto
    {
        [Required]
        public  required string email { get; set; }

        [Required]
        public required string password { get; set; }
    }
}
