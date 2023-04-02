using ShopApp.Dtos.Products;

namespace ShopApp.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<GetProductResponseDto>> SearchProduct(string query);
        Task<List<GetProductResponseDto>> FilterFeaturedProducts();
        Task<List<GetProductResponseDto>> FilterNewProducts();
        Task<List<GetProductResponseDto>> FilterNewStores();
        Task<GetProductResponseDto> SellProduct(int id);
    }
}
