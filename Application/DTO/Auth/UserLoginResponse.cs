
namespace Application.DTO
{
    public class UserLoginResponse
    {
        public int? UserId { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public DateTime Expires { get; set; }
        public UserDTO? User { get; set; }
    }
}
