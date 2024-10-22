using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.CartItemDTOs
{
    public class AddCartItemDTO
    {
        [Required]
        [Range(1, 99)]
        public int Count { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
