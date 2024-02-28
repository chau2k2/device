﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Vga
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0, 10000000)]
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<LaptopDetail> laptopDetail { get; set; }
    }
}