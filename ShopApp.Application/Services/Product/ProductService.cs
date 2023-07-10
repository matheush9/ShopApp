using AutoMapper;
using ShopApp.Application.Filters;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Application.Interfaces.Stock;
using ShopApp.Application.ResponseWrappers;
using ShopApp.Domain.DTOs.Products;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;
        private readonly IStoreRepository _storeRepository;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IStockService stockService, IProductRepository productRepository, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _stockService = stockService;
            _productRepository = productRepository;
            _storeRepository = storeRepository;
        }

        public async Task<GetProductResponseDto> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            return _mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<PagedResponse<List<GetProductResponseDto>>> Filter(ProductFilter productParams, PaginationFilter paginationFilter)
        {
            decimal minPrice = new();
            decimal maxPrice = new();

            if (productParams.PriceRange != null)
            {
                var priceRangeParts = productParams.PriceRange.Split('.');
                minPrice = decimal.Parse(priceRangeParts[0]);
                maxPrice = decimal.Parse(priceRangeParts[1]);
            }

            var productsPaginated = await _productRepository.Filter(productParams, paginationFilter, minPrice, maxPrice);
            productsPaginated = Order(productsPaginated, productParams.Sort);

            var productsDTO = _mapper.Map<List<GetProductResponseDto>>(productsPaginated);

            return new PagedResponse<List<GetProductResponseDto>>(productsDTO, paginationFilter.PageNumber, paginationFilter.PageSize, productsPaginated.Count);
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

        public async Task<GetProductResponseDto> Add(AddProductRequestDto newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            await _productRepository.AddAsync(product);

            var store = await _storeRepository.GetByIdAsync(product.StoreId);
            store.ProductCatalogCount++;
            await _stockService.AddStock(product.Id, product.StoreId);

            return _mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<GetProductResponseDto> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product != null)
            {
                await _productRepository.DeleteAsync(product);
                var store = await _storeRepository.GetByIdAsync(product.StoreId);
                store.ProductCatalogCount--;
                await _storeRepository.UpdateAsync(store);
            }

            return _mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<GetProductResponseDto> Update(int id, AddProductRequestDto newProduct)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product != null)
            {
                product.Description = newProduct.Description;
                product.Price = newProduct.Price;
                product.Name = newProduct.Name;

                await _productRepository.UpdateAsync(product);
            }

            return _mapper.Map<GetProductResponseDto>(product);
        }

        public async Task<List<GetProductResponseDto>> GetProductsByIdsList(List<int> idList)
        {
            if (idList != null && idList.Count > 0)
            {
                var products = await _productRepository.GetProductsByIdsList(idList);
                return _mapper.Map<List<GetProductResponseDto>>(products);
            }

            return new List<GetProductResponseDto>();
        }
    }
}
