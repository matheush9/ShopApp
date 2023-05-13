using ShopApp.Application.Filters;
using ShopApp.Application.ResponseWrappers;
using ShopApp.Domain.DTOs.Products;
using ShopApp.Domain.DTOs.Store;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Interfaces.ProductService
{
    public interface IProductService
    {
        Task<GetProductResponseDto> GetById(int id);
        Task<GetProductResponseDto> Add(AddProductRequestDto newProduct);
        Task<GetProductResponseDto> Delete(int id);
        Task<GetProductResponseDto> Update(int id, AddProductRequestDto newProduct);
        Task<PagedResponse<List<GetProductResponseDto>>> Filter(ProductQueryParamsResponseDto productParams, PaginationFilter paginationFilter);
        List<Product> Order(List<Product> products, string sort);
        Task<GetProductResponseDto> SellProduct(int id);
        Task<List<GetProductResponseDto>> GetProductsByIdsList(List<int> idList);
    }
}