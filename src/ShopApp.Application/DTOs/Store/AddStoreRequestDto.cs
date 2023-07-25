using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Store
{
    public class AddStoreRequestDto : BaseUser
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
    }
}
