using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.ProductDTOs
{
    public class UpdateProductDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Label must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Label can only be 50 characters long.")]
        public string Label { get; set; }
        [MaxLength(100, ErrorMessage = "Description can only be 100 characters long.")]
        public string Description { get; set; }
        [Required]
        [Range(1, 999999)]
        public int Price { get; set; }
    }
}
