﻿namespace ShopApp.Application.Filters
{
    public class ProductFilter
    {
        public string Query { get; set; } = string.Empty;
        public int? CategoryId { get; set; }
        public int? StoreId { get; set; }
        public string Sort { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public string? PriceRange { get; set; }
    }
}
