using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class RamModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(0, 10000000)]
        public decimal Price { get; set; }
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
