using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class MonitorM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage ="length of name must not be greater than 100")]
        public string Name { get; set; }
        [Range(0, 10000000)]
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetail { get; set;}
    }
}
