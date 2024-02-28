using device.Models;

namespace device.Response
{
    public class LaptopResponse : LaptopModel
    {
        public string ProducerName { get; set; }
        public string RamName { get; set; }
        public string VgaName { get; set; }
        public string MonitorName { get; set; }
        public int Quantity { get; set; }
        public int SoldQuantity { get; set; }
        
        public decimal Profit { get; set; }
    }
}
