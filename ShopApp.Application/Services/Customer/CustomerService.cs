using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopApp.Application.Interfaces.Customer;
using ShopApp.Domain.DTOs.Customer;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Data;

namespace ShopApp.Application.Services.CustomerServices
{
    public class CustomerService: ICustomerService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomerService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetCustomerResponseDto> GetById(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> GetCustomerByUserId(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.UserId == id);

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> Add(AddCustomerRequestDto newCustomer)
        {
            var customer = _mapper.Map<Customer>(newCustomer);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> Update(int id, AddCustomerRequestDto newCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer != null)
            {
                customer.Name = newCustomer.Name;

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }
    }
}
