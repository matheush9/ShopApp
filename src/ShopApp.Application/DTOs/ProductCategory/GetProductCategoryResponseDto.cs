namespace ShopApp.Domain.DTOs.Category
{
    public class GetProductCategoryResponseDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
