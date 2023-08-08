namespace Restfull.DTO.User
{
    public class CreateUserDto
    {
        public required string name { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public DateOnly dob { get; set; }
    }
}
