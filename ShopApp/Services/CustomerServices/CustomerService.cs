using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Customer;
using ShopApp.Services.GenericService;
using ShopApp.Services.ResponseHandlers;

namespace ShopApp.Services.CustomerServices
{
    public class CustomerService : IGenericService<GetCustomerResponseDto, AddCustomerRequestDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DefaultResponseHandler<GetCustomerResponseDto> responseHandler = new();

        public CustomerService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCustomerResponseDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCustomerResponseDto>();

            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            serviceResponse.Data = _mapper.Map<GetCustomerResponseDto>(customer);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<List<GetCustomerResponseDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetCustomerResponseDto>>();
            var responseHandler = new DefaultResponseHandler<List<GetCustomerResponseDto>>();
            serviceResponse.Data = await _context.Customers.Select(p => _mapper.Map<GetCustomerResponseDto>(p)).ToListAsync();
            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetCustomerResponseDto>> Add(AddCustomerRequestDto newCustomer)
        {
            var serviceResponse = new ServiceResponse<GetCustomerResponseDto>();

            _context.Customers.Add(_mapper.Map<Customer>(newCustomer));
            await _context.SaveChangesAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCustomerResponseDto>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<GetCustomerResponseDto>();
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetCustomerResponseDto>(customer);

            return responseHandler.SetResponse(serviceResponse);
        }

        public async Task<ServiceResponse<GetCustomerResponseDto>> Update(int id, AddCustomerRequestDto newCustomer)
        {
            var serviceResponse = new ServiceResponse<GetCustomerResponseDto>();
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                customer.Name = newCustomer.Name;

                await _context.SaveChangesAsync();
            }

            serviceResponse.Data = _mapper.Map<GetCustomerResponseDto>(customer);

            return responseHandler.SetResponse(serviceResponse);
        }
    }
}
