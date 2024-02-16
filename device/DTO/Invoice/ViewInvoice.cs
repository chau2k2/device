namespace device.DTO.HoaDon
{
    public class ViewInvoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateInvoice { get; set; }
        public double TotalInvoice { get; set; }
    }
}
