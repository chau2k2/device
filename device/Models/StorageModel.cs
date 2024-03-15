using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class StorageModel
    {
        public int Id { get; set; }
        [Range(0, 1000, ErrorMessage = "nhap gia tri trong khoang 0 den 1000")]
        public int SoldNumber { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "nhap gia tri trong khoang 0 den 1000")]
        public int ImportNumber { get; set; }
        public int LaptopId { get; set; }
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
