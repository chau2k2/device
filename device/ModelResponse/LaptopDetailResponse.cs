using device.Models;

namespace device.Response
{
    public class LaptopDetailResponse : LaptopDetailModel
    {
        public string? LaptopName { get; set; }
        public string? RamName { get; set; }
        public string? VgaName { get; set; }
        public string? MonitorName { get; set; }
    }
}
