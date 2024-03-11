using device.Models;

namespace device.Response
{
    public class LaptopResponse : LaptopModel
    {
        public string ProducerName { get; set; }
        
        public decimal Profit { get; set; }
    }
}
