namespace device.DTO.HoaDon
{
    public class UpdateInvoice
    {
        public string InvoiceNumber { get; set; }
        public DateTime DateInvoice { get; set; }
        public int TotalQuanity { get; set; }
        public double TotalPrice { get; set; }
    }
}
