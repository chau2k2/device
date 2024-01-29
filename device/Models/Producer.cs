using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
