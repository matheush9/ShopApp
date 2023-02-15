using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Products;
using ShopApp.Services.GenericService;
using ShopApp.Services.ResponseHandlers;
using ShopApp.Services.StockServices;

namespace ShopApp.Services.ProductServices
{
    public class ProductService : IGenericService<GetProductResponseDto, AddProductRequestDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IStockService _stockService;

        public DefaultResponseHandler<GetProductResponseDto> responseHandler = new();

        public ProductService(DataContext context, IMapper mapper, IStockService stockService)
        {
            _mapper = mapper;
            _context = context;
            _stockService = stockService;
        }

        public async Task<ServiceResponse<GetProductResponseDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetProductResponseDto>();

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetProductResponseDto>(product);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetProductResponseDto>> Add(AddProductRequestDto newProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductResponseDto>();

            var product = _mapper.Map<Product>(newProduct);
            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            await _stockService.AddStock(product.Id, product.StoreId);
                
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductResponseDto>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<GetProductResponseDto>();
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetProductResponseDto>(product);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetProductResponseDto>> Update(int id, AddProductRequestDto newProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductResponseDto>();
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                product.Description = newProduct.Description;
                product.Price = newProduct.Price;
                product.Name = newProduct.Name;
                product.ImageUrl = newProduct.ImageUrl;

                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetProductResponseDto>(product);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
