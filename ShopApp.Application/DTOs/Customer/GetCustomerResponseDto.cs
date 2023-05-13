using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Customer
{
    public class GetCustomerResponseDto: BaseUser
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
