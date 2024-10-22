using ProjectPierre.DTO.ProductDTOs;
using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.CategoryDTOs
{
    public class AddCategoryDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Category name must be at least 3 characters long.")]
        [MaxLength(50, ErrorMessage = "Category name can only be 50 characters long.")]
        public string Name { get; set; }
    }
}
