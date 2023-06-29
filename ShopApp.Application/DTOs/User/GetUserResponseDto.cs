using ShopApp.Domain.DTOs.Customer;
using ShopApp.Domain.DTOs.Image;
using ShopApp.Domain.DTOs.Store;

namespace ShopApp.Domain.DTOs.User
{
    public class GetUserResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<GetImageResponseDto> Images { get; set; }
        public GetStoreResponseDto Store { get; set; }
        public GetCustomerResponseDto Customer { get; set; }
    }
}
