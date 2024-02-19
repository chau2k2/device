using System.ComponentModel.DataAnnotations.Schema;

namespace device.DTO.LaptopDetail
{
    public class CreateLaptopDetail
    {
        public int Id { get; set; }
        public string Cpu { get; set; }
        public string Seri { get; set; }
        public int IdVga { get; set; }
        public int IdRam { get; set; }
        public string HardDriver { get; set; }
        public int IdMonitor { get; set; }
        public string Webcam { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double BatteryCapacity { get; set; }
        public byte[]? Image { get; set; }
        public int idLaptop { get; set; }
    }
}
