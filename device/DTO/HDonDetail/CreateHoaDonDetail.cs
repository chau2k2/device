using device.Models;

namespace device.DTO.HDonDetail
{
    public class CreateHoaDonDetail
    {
        public int Id { get; set; }
        public int IdHoaDon { get; set; }
        public int IdLaptop { get; set; }
        public int Number { get; set; }
    }
}
