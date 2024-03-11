using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Entity
{
    public class InvoiceDetail
    {
        /// <summary>
        /// Id Chi tiết hóa đơn
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// khóa ngoại LaptopId - liên kết với bảng Laptop
        /// </summary>
        public int LaptopId { get; set; }
        /// <summary>
        /// khóa ngoại invoiceId - liên kết với bảng invoice
        /// </summary>
        public int InvoiceId { get; set; }
        /// <summary>
        /// giá của 1 laptop
        /// </summary>
        [Range(0,100000000)]
        public decimal Price { get; set; }
        /// <summary>
        /// số lượng laptop bán
        /// </summary>
        [Range(0,999)]
        public int Quantity { get; set; }
        /// <summary>
        /// trường isdelete => xóa mềm
        /// </summary>
        [JsonIgnore] 
        public bool IsDelete { get; set; }
        /// <summary>
        /// liên kết 1 - n với bảng invoice
        /// </summary>
        [JsonIgnore]
        public virtual Invoice invoices { get; set; }
        /// <summary>
        /// liết kết 1 - n với bảng laptop
        /// </summary>
        [JsonIgnore]
        public virtual Laptop Laptop { get; set; }
    }
}
