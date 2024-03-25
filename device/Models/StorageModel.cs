using device.Entity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class StorageModel
    {
        public int Id { get; set; }
        [Range(0, 1000, ErrorMessage = "Nhập giá trị trong khoảng 0 đến 1000")]
        public int SoldNumber { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Nhập giá trị trong khoảng 0 đến 1000")]
        public int ImportNumber { get; set; }
        public EProductType ProductType { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
