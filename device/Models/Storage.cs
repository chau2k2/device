using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }
        
        public int idDetail { get; set; }
        public int SoldNumber { get; set; }
        [Required]
        public int ImportNumber { get; set; }
        [JsonIgnore]
        [ForeignKey("LaptopDetail")]
        public virtual LaptopDetail laptopDetail { get; set; }
    }
}
