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

        public async Task<List<GetProductResponseDto>> Filter(ProductQueryParams productParams)
        {
            var productsQuery = _context.Products
                .Include(p => p.Store)

                .Where(p => p.Name.Contains(productParams.Query)
                           || p.Description.Contains(productParams.Query)
                           || p.Store.Name.Contains(productParams.Query)
                           || p.ProductCategoryId == productParams.CategoryId
                           || p.StoreId == productParams.StoreId);

            var products = await productsQuery.ToListAsync();

            if (productParams.Query == string.Empty && productParams.CategoryId == null && productParams.StoreId == null)
            {
                products = await _context.Products.Include(p => p.Store).ToListAsync();
            }

            products = Order(products, productParams.Sort);

            return _mapper.Map<List<GetProductResponseDto>>(products);
        }

        public List<Product> Order(List<Product> products, string sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                return sort switch
                {
                    "featured" => products.OrderByDescending(p => p.SoldAmount).ToList(),
                    "newProducts" => products.OrderByDescending(p => p.CreatedAt).ToList(),
                    "newStores" => products.OrderByDescending(p => p.Store.CreatedAt).ToList(),
                    _ => products,
                };
            }

            return products;
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

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<GetProductResponseDto> SellProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.SoldAmount++;
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetProductResponseDto>(product);
        }
    }
}
