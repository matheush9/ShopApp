namespace ShopApp.Dtos.Image
{
    public class GetImageResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public int ProductId { get; set; }
    }
}
