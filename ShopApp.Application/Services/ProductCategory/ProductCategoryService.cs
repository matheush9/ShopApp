using AutoMapper;
using ShopApp.Application.DTOs.ProductCategory;
using ShopApp.Application.Interfaces.ProductCategory;
using ShopApp.Domain.DTOs.Category;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.ProductCategory
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<GetProductCategoryResponseDto> GetProductCategoryById(int id)
        {
            var productCategory = await _productCategoryRepository.GetByIdAsync(id);

            return _mapper.Map<GetProductCategoryResponseDto>(productCategory);
        }

        public async Task<List<GetProductCategoryResponseDto>> GetAllProductCategories()
        {
            var categories = await _productCategoryRepository.GetAllProductCategories();
            return _mapper.Map<List<GetProductCategoryResponseDto>>(categories);
        }

        public async Task<GetProductCategoryResponseDto> AddProductCategory(AddProductCategoryRequestDTO newProductCategory)
        {
            var category = _mapper.Map<Domain.Entities.ProductCategory>(newProductCategory);
            await _productCategoryRepository.AddAsync(category);

            return _mapper.Map<GetProductCategoryResponseDto>(category);
        }
    }
}
