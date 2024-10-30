using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.CartItemDTOs
{
    public class AddCartItemDTO
    {
        [Required]
        [Range(1, 9999)]
        public int Count { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
