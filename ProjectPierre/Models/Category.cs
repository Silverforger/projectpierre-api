using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectPierre.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Aisle> Aisles { get; set; }
    }
}
