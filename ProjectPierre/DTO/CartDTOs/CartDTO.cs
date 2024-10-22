using ProjectPierre.DTO.CartItemDTOs;

namespace ProjectPierre.DTO.CartDTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string CartUserId { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }
}
