using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Product;
using ShopApp.Services.ResponseHandlers;

namespace ShopApp.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DefaultResponseHandler<GetProductResponseDto> responseHandler = new();

        public ProductService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetProductResponseDto>> GetProductById(int id)
        {
            var serviceResponse = new ServiceResponse<GetProductResponseDto>();

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetProductResponseDto>(product);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<List<GetProductResponseDto>>> GetAllProducts()
        {
            var serviceResponse = new ServiceResponse<List<GetProductResponseDto>>();
            var responseHandler = new DefaultResponseHandler<List<GetProductResponseDto>>();
            serviceResponse.Data = await _context.Products.Select(p => _mapper.Map<GetProductResponseDto>(p)).ToListAsync();

            return responseHandler.SetResponse(serviceResponse);
        }   

        public async Task<ServiceResponse<GetProductResponseDto>> AddProduct(AddProductRequestDto newProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductResponseDto>();
            _context.Products.Add(_mapper.Map<Product>(newProduct));            
            await _context.SaveChangesAsync();

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetProductResponseDto>> DeleteProduct(int id)
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

        public async Task<ServiceResponse<GetProductResponseDto>> UpdateProduct(int id, AddProductRequestDto newProduct)
        {
            var serviceResponse = new ServiceResponse<GetProductResponseDto>();
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

            serviceResponse.Data = _mapper.Map<GetProductResponseDto>(product);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
