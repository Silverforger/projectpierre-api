using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPierre.Models
{
    [Table("Users")]
    public class User : IdentityUser
    {
        public string RefreshToken {  get; set; } = string.Empty;
        public DateTime TokenCreateDate { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}
