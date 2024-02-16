using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class HoaDon
    {
        public HoaDon() 
        {
            HoaDonDetail = new HashSet<HoaDonDetail>();
        }
        [Key]
        public int Id { get; set; }
        public string SoHoaDon {  get; set; }
        public DateTime HoaDonDate { get; set; }
        public double HoaDonTotal { get; set; }
        public virtual ICollection<HoaDonDetail> HoaDonDetail { get; set;}
    }
}
