using System.ComponentModel.DataAnnotations;

namespace ProjectPierre.DTO.AisleDTOs
{
    public class RemoveAisleDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
