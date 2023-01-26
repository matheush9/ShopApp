using ShopApp.Dtos.Products;

namespace ShopApp.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<List<GetProductResponseDto>>> GetAllProducts();
        Task<ServiceResponse<GetProductResponseDto>> GetProductById(int id);
        Task<ServiceResponse<GetProductResponseDto>> AddProduct(AddProductRequestDto newProduct);
        Task<ServiceResponse<GetProductResponseDto>> UpdateProduct(int id, AddProductRequestDto newProduct);
        Task<ServiceResponse<GetProductResponseDto>> DeleteProduct(int id);
    }
}
    