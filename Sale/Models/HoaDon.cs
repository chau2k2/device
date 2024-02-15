using System.ComponentModel.DataAnnotations;

namespace Sale.Models
{
    public class HoaDon
    {
        [Key]
        public int Id { get; set; }
        public string SoHoaDon {  get; set; }
        public DateTime HoaDonDate { get; set; }
        public double HoaDonTotal { get; set; }

    }
}
