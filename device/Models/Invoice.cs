using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class Invoice
    {
        public Invoice() 
        {
            invoiceDetail = new HashSet<InvoiceDetail>();
        }
        [Key]
        public int Id { get; set; }
        public string InvoiceNumber {  get; set; }
        public DateTime DateInvoice { get; set; }
        public double TotalInvoice { get; set; }
        public virtual ICollection<InvoiceDetail> invoiceDetail { get; set;}
    }
}
