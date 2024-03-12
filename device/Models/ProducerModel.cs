using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class ProducerModel
    {
        public int Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Length of name is not greater than 100")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
