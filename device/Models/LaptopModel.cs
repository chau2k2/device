using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class LaptopModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int ProducerId { get; set; }
        [Range(0, 10000000)]
        public decimal CostPrice { get; set; }
        [Required]
        [Range(0, 10000000)]
        public decimal SoldPrice { get; set; }
        /// <summary>
        /// số lượng hàng còn lại
        /// </summary>
        public int inventory { get; set; }
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
