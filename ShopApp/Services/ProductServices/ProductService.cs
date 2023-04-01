using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Products;
using ShopApp.Services.GenericService;
using ShopApp.Services.StockServices;

namespace ShopApp.Services.ProductServices
{
    public class ProductService : IGenericService<GetProductResponseDto, AddProductRequestDto>, IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;

        public ProductService(DataContext context, IMapper mapper, IStockService stockService)
        {
            _mapper = mapper;
            _context = context;
            _stockService = stockService;
        }

        public async Task<GetProductResponseDto> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<List<GetProductResponseDto>> SearchProduct(string query)
        {
            var products = new List<Product>();

            if (!string.IsNullOrEmpty(query))
            {
                products = await _context.Products.Where(p => p.Name.Contains(query) 
                                                           || p.Description.Contains(query) 
                                                           || p.Store.Name.Contains(query)).ToListAsync();
            }

            return _mapper.Map<List<GetProductResponseDto>>(products);
        }

        public async Task Add(AddProductRequestDto newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            await _stockService.AddStock(product.Id, product.StoreId);
        }

        public async Task<GetProductResponseDto> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<GetProductResponseDto> Update(int id, AddProductRequestDto newProduct)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.Description = newProduct.Description;
                product.Price = newProduct.Price;
                product.Name = newProduct.Name;
                product.ImageUrl = newProduct.ImageUrl;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetProductResponseDto>(product);
        }
    }
}
