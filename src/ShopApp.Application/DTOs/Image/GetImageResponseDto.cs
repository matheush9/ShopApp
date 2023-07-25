using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Image
{
    public class GetImageResponseDto: BaseUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public int ProductId { get; set; }
    }
}
