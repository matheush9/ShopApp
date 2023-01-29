﻿using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; } 
        public List<Stock> Stocks { get; set; }
    }
}
