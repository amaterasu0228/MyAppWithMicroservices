namespace UserService.API.Models
{
    public class RegisterRequestDto
    {
        public required string Username { get; set; }
        public required string Phone { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }

}
