using ShopApp.Data;
using Microsoft.EntityFrameworkCore;
using ShopApp.Dtos.Product;

namespace ShopApp.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;  
        }

        public async Task<GetProductResponseDto> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<List<GetProductResponseDto>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<GetProductResponseDto> AddProduct(AddProductRequestDto newProduct)
        {
          _context.Products.Add(newProduct);
          await _context.SaveChangesAsync();
          return newProduct;
        }

        public async Task<GetProductResponseDto> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return product;
        }

        public async Task<GetProductResponseDto> UpdateProduct(int id, AddProductRequestDto newProduct)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.Description = newProduct.Description;
                product.Price = newProduct.Price;
                product.PriceTotal = newProduct.PriceTotal;
                product.Name = newProduct.Name;
                product.ImageUrl = newProduct.ImageUrl;

                await _context.SaveChangesAsync();
            }
            return product;
        }
    }
}
