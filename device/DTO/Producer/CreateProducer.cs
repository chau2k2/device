using System.ComponentModel.DataAnnotations;

namespace device.DTO.Producer
{
    public class CreateProducer
    {
        [StringLength(100, ErrorMessage = "nhap qua ki tu cho phep")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
