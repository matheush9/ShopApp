using ShopApp.Domain.DTOs.Customer;

namespace ShopApp.Application.Interfaces.Customer
{
    public interface ICustomerService
    {
        Task<GetCustomerResponseDto> GetById(int id);
        Task<GetCustomerResponseDto> GetCustomerByUserId(int id);
        Task<GetCustomerResponseDto> Add(AddCustomerRequestDto newCustomer);        
        Task<GetCustomerResponseDto> Delete(int id);
        Task<GetCustomerResponseDto> Update(int id, AddCustomerRequestDto newCustomer);
    }
}
