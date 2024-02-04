using System.ComponentModel.DataAnnotations;

namespace device.Views.Producer
{
    public class UpdateProducer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="please insert name's producer")]
        public string name { get; set; }
    }
}
