using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Store
{
    public class GetStoreResponseDto: BaseUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Country { get; set; }
        public int ProductCatalogCount { get; set; }
    }
}
