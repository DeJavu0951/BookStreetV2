using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }

        [Required]
        [RegularExpression("ADMINISTRATOR|VISITOR|BRAND|STORE")]
        public string Role { get; set; }
    }
}
