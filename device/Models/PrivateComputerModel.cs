﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class PrivateComputerModel
    {
        /// <summary>
        /// Id Pc guid
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tên PC
        /// </summary>
        [MaxLength(100)]
        public string? Name { get; set; }
        /// <summary>
        /// giá nhập 
        /// </summary>
        [Range(0, 100000000)]
        public decimal CostPrice { get; set; }
        /// <summary>
        /// Giá bán
        /// </summary>
        [Range(0, 100000000)]
        public decimal SoldPrice { get; set; }
        /// <summary>
        /// Id Nhà sản xuất
        /// </summary>
        public int ProducerId { get; set; }
        /// <summary>
        /// liên kết với bảng nhà sản xuất
        /// </summary>
        [JsonIgnore]
        public bool IsDelete { get; set; }
    }
}
