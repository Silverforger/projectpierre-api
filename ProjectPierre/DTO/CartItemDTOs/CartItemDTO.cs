using ProjectPierre.DTO.ProductDTOs;

namespace ProjectPierre.DTO.CartItemDTOs
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int CartId { get; set; }
    }
}
