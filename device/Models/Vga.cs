using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Vga
    {
        [Key]
        public int Id { get; set; }
        [Required,Column(TypeName ="varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName ="decimal(10,3)")]
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> laptopDetail { get; set; }
    }
}
