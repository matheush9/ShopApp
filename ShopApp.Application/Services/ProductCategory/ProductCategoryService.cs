using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.DTOs.ProductCategory;
using ShopApp.Application.Interfaces.ProductCategory;
using ShopApp.Domain.DTOs.Category;
using ShopApp.Domain.DTOs.Item;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.ProductCategory
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetProductCategoryResponseDto> GetProductCategoryById(int id)
        {
            var productCategory = await _context.ProductCategories.FirstOrDefaultAsync(c => c.Id == id);

            return _mapper.Map<GetProductCategoryResponseDto>(productCategory);
        }

        public async Task<List<GetProductCategoryResponseDto>> GetAllProductCategories()
        {
            var categories = await _context.ProductCategories.ToListAsync();
            return _mapper.Map<List<GetProductCategoryResponseDto>>(categories);
        }

        public async Task<GetProductCategoryResponseDto> AddProductCategory(AddProductCategoryRequestDTO newProductCategory)
        {
            var category = _mapper.Map<Domain.Entities.ProductCategory>(newProductCategory);
            _context.ProductCategories.Add(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetProductCategoryResponseDto>(category);
        }
    }
}
