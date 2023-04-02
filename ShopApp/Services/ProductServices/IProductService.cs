using ShopApp.Dtos.Products;

namespace ShopApp.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<GetProductResponseDto>> SearchProduct(string query);
        Task<GetProductResponseDto> SellProduct(int id);
    }
}
