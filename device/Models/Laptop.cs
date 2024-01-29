using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Producer { get; set; }
        [Required]
        public int LaptopDetail { get; set; }
    }
}
