using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class VgaModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsDelete { get; set; }
    }
}
