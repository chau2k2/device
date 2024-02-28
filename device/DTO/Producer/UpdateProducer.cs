using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.ComponentModel.DataAnnotations;

namespace device.DTO.Producer
{
    public class UpdateProducer
    {
        [StringLength(100,ErrorMessage ="Nhaap qua ki tu cho phep")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
