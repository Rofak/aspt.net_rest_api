namespace Restfull.DTO.User
{
    public class UpdateUserDto
    {
        public  string? name { get; set; }
        public  string? email { get; set; }
        public  string? password { get; set; }
        public DateOnly? dob { get; set; }
    }
}
