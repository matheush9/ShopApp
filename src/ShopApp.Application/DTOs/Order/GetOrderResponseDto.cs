using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Order
{
    public class GetOrderResponseDto: BaseCustomer
    {
        public int Id { get; set; }

        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
