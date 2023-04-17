using ShopApp.Dtos.Products;

namespace ShopApp.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<GetProductResponseDto>> Filter(ProductQueryParams productParams);
        List<Product> Order(List<Product> products, string sort);
        Task<GetProductResponseDto> SellProduct(int id);
    }
}