using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Filters;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Application.Interfaces.Stock;
using ShopApp.Application.ResponseWrappers;
using ShopApp.Domain.DTOs.Products;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.ProductServices
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

        public async Task<PagedResponse<List<GetProductResponseDto>>> Filter(ProductQueryParamsResponseDto productParams, PaginationFilter paginationFilter)
        {
            var pagFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

            var productsQuery = _context.Products
                .Include(p => p.Store)

                .Where(p => p.Name.Contains(productParams.Query)
                           || p.Description.Contains(productParams.Query)
                           || p.Store.Name.Contains(productParams.Query)
                           || p.ProductCategoryId == productParams.CategoryId
                           || p.StoreId == productParams.StoreId);

            var productsPaginated = productsQuery.Skip((pagFilter.PageNumber - 1) * pagFilter.PageSize)
                .Take(pagFilter.PageSize).ToList();

            var products = await productsQuery.ToListAsync();
            products = productsPaginated;

            if (productParams.Query == string.Empty && productParams.CategoryId == null && productParams.StoreId == null)
            {
                products = await _context.Products.Include(p => p.Store)
                                    .Skip((pagFilter.PageNumber - 1) * pagFilter.PageSize)
                                    .Take(pagFilter.PageSize).ToListAsync();
            }

            products = Order(products, productParams.Sort);

            var productsDTO = _mapper.Map<List<GetProductResponseDto>>(products);

            return new PagedResponse<List<GetProductResponseDto>>(productsDTO, pagFilter.PageNumber, pagFilter.PageSize, productsQuery.Count());
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
                    "priceHighToLow" => products.OrderByDescending(p => p.Price).ToList(),
                    "priceLowToHigh" => products.OrderBy(p => p.Price).ToList(),
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
