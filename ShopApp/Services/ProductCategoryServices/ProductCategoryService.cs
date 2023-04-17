using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Category;

namespace ShopApp.Services.CategoryServices
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
    }
}
