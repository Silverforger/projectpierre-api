namespace ProjectPierre.DTO.CartItemDTOs
{
    public class UpdatedCartItemDTO
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}
