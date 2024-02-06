using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class MonitorM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetail { get; set;}
    }
}
