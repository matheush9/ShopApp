using ShopApp.Domain.DTOs.Products;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Interfaces.ProductService
{
    public interface IProductService
    {
        Task<List<GetProductResponseDto>> Filter(ProductQueryParamsResponseDto productParams);
        List<Product> Order(List<Product> products, string sort);
        Task<GetProductResponseDto> SellProduct(int id);
    }
}