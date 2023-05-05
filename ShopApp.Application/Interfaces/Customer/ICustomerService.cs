using ShopApp.Domain.DTOs.Customer;

namespace ShopApp.Application.Interfaces.Customer
{
    public interface ICustomerService
    {
        Task<GetCustomerResponseDto> GetCustomerByUserId(int id);
    }
}
