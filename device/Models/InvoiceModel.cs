using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime DateInvoice { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
