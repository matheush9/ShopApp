using ShopApp.Dtos.Products;
using ShopApp.Models;

namespace ShopApp.Services.ProductService
{
    public interface IProductService
    {
        Task<List<GetProductResponseDto>> Filter(ProductQueryParams productParams);
        List<Product> Order(List<Product> products, string sort);
        Task<GetProductResponseDto> SellProduct(int id);
    }
}