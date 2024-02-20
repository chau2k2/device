using device.Models;

namespace device.DTO.HDonDetail
{
    public class CreateInvoiceDetail
    {
        public int Id { get; set; }
        public int IdLaptop { get; set; }
        public int IdInvoice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
