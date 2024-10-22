using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPierre.Models
{
    [Table("Carts")]
    public class Cart
    {
        public int Id { get; set; }
        public string CartUserId { get; set; }
        [ForeignKey("CartUserId")]
        public User User { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
