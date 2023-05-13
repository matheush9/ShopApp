﻿using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Products
{
    public class GetProductResponseDto: BaseStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
