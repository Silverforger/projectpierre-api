using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.CartItemDTOs
{
    public class UpdateCartItemDTO
    {
        [Required]
        [Range(1, 9999)]
        public int Count { get; set; }
    }
}
