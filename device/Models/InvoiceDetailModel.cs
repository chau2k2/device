using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class InvoiceDetailModel
    {
        public int Id { get; set; }
        public int IdLaptop { get; set; }
        public int IdInvoice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
