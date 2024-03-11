using System.ComponentModel.DataAnnotations;

namespace device.Models
{
    public class InvoiceDetailModel
    {
        /// <summary>
        /// Id Chi tiết hóa đơn
        /// </summary>
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
        [Range(0, 100000000)]
        public decimal Price { get; set; }
        /// <summary>
        /// số lượng laptop bán
        /// </summary>
        [Range(0, 999)]
        public int Quantity { get; set; }
        /// <summary>
        /// trường isdelete => xóa mềm
        /// </summary>
        public bool IsDelete { get; set; }
    }
}
