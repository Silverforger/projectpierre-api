using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectPierre.Models
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public List<Aisle> Aisles { get; set; }
    }
}
