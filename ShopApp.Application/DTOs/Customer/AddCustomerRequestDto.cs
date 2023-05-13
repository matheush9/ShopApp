using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Customer
{
    public class AddCustomerRequestDto : BaseUser
    {
        public string Name { get; set; }
    }
}
