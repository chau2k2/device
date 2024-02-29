using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Ram 
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName ="varchar(100)")]
        public string Name { get; set; }
        [Range(0, 10000000)]
        public decimal Price { get; set; }
        public bool IsDelete { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetail { get; set;}
    }
}
