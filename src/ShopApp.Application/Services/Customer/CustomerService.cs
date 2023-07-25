using AutoMapper;
using ShopApp.Application.Interfaces.Customer;
using ShopApp.Domain.DTOs.Customer;
using ShopApp.Domain.Entities;
using ShopApp.Infrastructure.Repositories.Abstractions;

namespace ShopApp.Application.Services.CustomerServices
{
    public class CustomerService: ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomerResponseDto> GetById(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> GetCustomerByUserId(int id)
        {
            var customer = await _customerRepository.GetCustomerByUserId(id);

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> Add(AddCustomerRequestDto newCustomer)
        {
            var customer = _mapper.Map<Customer>(newCustomer);
            await _customerRepository.AddAsync(customer);

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> Delete(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer != null)
                await _customerRepository.DeleteAsync(customer);

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }

        public async Task<GetCustomerResponseDto> Update(int id, AddCustomerRequestDto newCustomer)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer != null)
            {
                customer.Name = newCustomer.Name;

                await _customerRepository.UpdateAsync(customer);
            }

            return _mapper.Map<GetCustomerResponseDto>(customer);
        }
    }
}
