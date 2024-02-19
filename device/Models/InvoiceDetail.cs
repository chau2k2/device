using Humanizer.Localisation.TimeToClockNotation;
using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }
        public int IdInvoice { get; set; }
        public int IdLaptop { get; set; }
        public int Number { get; set; }
        public virtual Invoice invoices { get; set; }
        public virtual Laptop laptops { get; set; }
    }
}
