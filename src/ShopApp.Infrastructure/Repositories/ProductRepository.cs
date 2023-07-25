using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Filters;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public new async Task<Product> GetByIdAsync(int id)
        {
            return await _dbset.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByIdsList(List<int> idList)
        {
            var products = new List<Product>();

            products = await _dbset.Where(p => idList.Contains(p.Id)).Include(i => i.Images).ToListAsync();
            return products.OrderBy(p => idList.IndexOf(p.Id)).ToList();
        }

        public async Task<List<Product>> Filter(ProductFilter productParams, PaginationFilter paginationFilter, decimal minPrice, decimal maxPrice)
        {
            var productsQuery = _context.Products
                .Include(i => i.Images)
                .Include(s => s.Store)
                .Where(p =>
                    (string.IsNullOrEmpty(productParams.Query)
                    || p.Name.Contains(productParams.Query)
                    || p.Description.Contains(productParams.Query)
                    || p.Store.Name.Contains(productParams.Query))
                    && (productParams.CategoryId == null || p.ProductCategoryId == productParams.CategoryId)
                    && (productParams.StoreId == null || p.StoreId == productParams.StoreId)
                    && (String.IsNullOrEmpty(productParams.PriceRange) || (p.Price >= minPrice && p.Price <= maxPrice)));

            return await Paginate(productsQuery, paginationFilter);
        }

        private async Task<List<Product>> Paginate(IQueryable<Product> productsQuery, PaginationFilter paginationFilter)
        {
            return await productsQuery
                .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();
        }
    }
}
