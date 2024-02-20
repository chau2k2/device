using Humanizer.Localisation.TimeToClockNotation;
using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }
        public int IdLaptop { get; set; }
        public int IdInvoice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public virtual Invoice invoices { get; set; }
        public virtual Laptop Laptop { get; set; }
    }
}
