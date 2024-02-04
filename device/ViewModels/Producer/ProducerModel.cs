using System.ComponentModel.DataAnnotations;

namespace device.Views.Producer
{
    public class ProducerModel
    {
        [Required(ErrorMessage = "please insert name's producer")]
        public string name { get; set; }
        public bool isActive { get; set; }
    }
}
