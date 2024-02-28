﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Storage
    {
        [Key]
        public int Id { get; set; }
        public int idDetail { get; set; }
        [Range(0, 1000, ErrorMessage ="nhap gia tri trong khoang 0 den 1000")]
        public int SoldNumber { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage ="nhap gia tri trong khoang 0 den 1000")]
        public int ImportNumber { get; set; }
        [JsonIgnore]
        [ForeignKey("LaptopDetail")]
        public virtual LaptopDetail laptopDetail { get; set; }
    }
}