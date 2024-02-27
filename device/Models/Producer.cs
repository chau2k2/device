using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName ="varchar"), MaxLength(100, ErrorMessage ="Length of name is not greater than 100")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public ICollection<Laptop> Laptops { get; set;}
    }
}
