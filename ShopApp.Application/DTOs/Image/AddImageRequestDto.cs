using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Image
{
    public class AddImageRequestDto : BaseUser
    {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public string ImageId { get; set; }
    }
}
