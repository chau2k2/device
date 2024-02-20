using Humanizer.Localisation.TimeToClockNotation;

namespace device.DTO.HoaDon
{
    public class CreateInvoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateInvoice { get; set; }
        public int TotalQuanity { get; set; }
        public double TotalPrice { get; set; }
    }
}
