﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace device.Models
{
    public class Laptop
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        //foreign key for Laptop
        [Required]
        public int Producer { get; set; }
        [Required]
        public double GiaVon { get; set; }
        [Required]
        public double Giaban { get; set; }
        [JsonIgnore]
        
        public ICollection<Producer> Producers { get; set; } 
        [JsonIgnore]
        public ICollection<LaptopDetail> LaptopDetails { get; set; }
    }
}
