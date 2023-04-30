namespace ShopApp.Domain.DTOs.Products
{
    public class GetProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
