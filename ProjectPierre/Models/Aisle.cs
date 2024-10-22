using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPierre.Models
{
    [Table("Aisles")]
    public class Aisle
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
}
