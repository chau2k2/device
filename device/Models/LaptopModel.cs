using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class LaptopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdProducer { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public bool IsDelete { get; set; }
    }
}
