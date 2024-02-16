using Humanizer.Localisation.TimeToClockNotation;
using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class HoaDonDetail
    {
        [Key]
        public int Id { get; set; }
        public int IdHoaDon { get; set; }
        public int IdLaptop { get; set; }
        public int Number { get; set; }
        public virtual HoaDon HoaDon { get; set; }
    }
}
