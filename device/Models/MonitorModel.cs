using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class MonitorModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "length of name must not be greater than 50")]
        public string Name { get; set; }
        [Range(0, 100000000)]
        public decimal Price { get; set; }
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
