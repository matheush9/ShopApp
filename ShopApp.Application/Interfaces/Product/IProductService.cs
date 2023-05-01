using ShopApp.Application.Filters;
using ShopApp.Application.ResponseWrappers;
using ShopApp.Domain.DTOs.Products;
using ShopApp.Domain.Entities;

namespace ShopApp.Application.Interfaces.ProductService
{
    public interface IProductService
    {
        Task<PagedResponse<List<GetProductResponseDto>>> Filter(ProductQueryParamsResponseDto productParams, PaginationFilter paginationFilter);
        List<Product> Order(List<Product> products, string sort);
        Task<GetProductResponseDto> SellProduct(int id);
        Task<List<GetProductResponseDto>> GetProductsByIdsList(List<int> idList);
    }
}