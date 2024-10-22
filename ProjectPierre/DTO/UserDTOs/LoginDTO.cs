using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.UserDTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }  
        [Required]
        public string Password { get; set; }
    }
}
