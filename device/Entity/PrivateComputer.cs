namespace device.Entity
{
    public class PrivateComputer
    {
        /// <summary>
        /// Id Pc guid
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Tên PC
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// giá nhập 
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// Giá bán
        /// </summary>
        public decimal SoldPrice { get; set; }
        /// <summary>
        /// Id Nhà sản xuất
        /// </summary>
        public int ProducerId { get; set; }
        /// <summary>
        /// liên kết với bảng nhà sản xuất
        /// </summary>
        public virtual Producer Producer { get; set; }
    }
}
