using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.CartItemDTOs
{
    public class UpdateCartItemDTO
    {
        [Required]
        [Range(1, 99)]
        public int Count { get; set; }
    }
}
