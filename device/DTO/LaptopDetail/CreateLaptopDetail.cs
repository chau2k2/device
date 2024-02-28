namespace device.DTO.LaptopDetail
{
    public class CreateLaptopDetail
    {
        public string Cpu { get; set; }
        public string Seri { get; set; }
        public int IdVga { get; set; }
        public int IdRam { get; set; }
        public string HardDriver { get; set; }
        public int IdMonitor { get; set; }
        public string Webcam { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public decimal BatteryCapacity { get; set; }
        public int idLaptop { get; set; }
    }
}
