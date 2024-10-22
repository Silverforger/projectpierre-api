using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.UserDTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
