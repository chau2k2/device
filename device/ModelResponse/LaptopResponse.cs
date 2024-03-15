using device.Models;

namespace device.Response
{
    public class LaptopResponse : LaptopModel
    {
        public string? ProducerName { get; set; }
        /// <summary>
        /// số lượng còn lại
        /// </summary>
        public int inventory { get; set; }
    }
}
