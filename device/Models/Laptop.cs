using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //foreign key for Laptop
        [Required]
        [ForeignKey("producer")]
        public int Producer { get; set; }
        [JsonIgnore]
        public Producer producer { get; set; }
        [Required]
        [ForeignKey("laptop_detail")]
        public int LaptopDetail { get; set; }
        [JsonIgnore]
        public LaptopDetail laptopDetail { get; set; }
    }
}
