using device.Models;

namespace device.DTO.HDonDetail
{
    public class CreateInvoiceDetail
    {
        public int Id { get; set; }
        public int IdInvoice { get; set; }
        public int IdLaptop { get; set; }
        public int Number { get; set; }
    }
}
