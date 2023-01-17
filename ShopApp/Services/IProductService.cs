namespace ShopApp.Services
﻿using ShopApp.Dtos.Product;

namespace ShopApp.Services
{
    public interface IProductService
    {
        Task<List<GetProductResponseDto>> GetAllProducts();
        Task<GetProductResponseDto> GetProductById(int id);
        Task<GetProductResponseDto> AddProduct(AddProductRequestDto newProduct);
        Task<GetProductResponseDto> UpdateProduct(int id, AddProductRequestDto newProduct);
        Task<GetProductResponseDto> DeleteProduct(int id);
    }
}
