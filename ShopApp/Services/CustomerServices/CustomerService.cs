using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Customer;
using ShopApp.Services.GenericService;

namespace ShopApp.Services.CustomerServices
{
    public class CustomerService : IGenericService<GetCustomerResponseDto, AddCustomerRequestDto>
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

        public async Task Add(AddCustomerRequestDto newCustomer)
        {
            _context.Customers.Add(_mapper.Map<Customer>(newCustomer));
            await _context.SaveChangesAsync();
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
