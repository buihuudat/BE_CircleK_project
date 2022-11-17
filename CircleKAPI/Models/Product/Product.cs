﻿using System.ComponentModel.DataAnnotations;

namespace CircleKAPI.Models.Product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;


        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
        public int ProducerId { get; set; }
        public int Count { get; set; }
        public DateTime HXS { get; set; }
        public DateTime HSD { get; set; }
    }
}
