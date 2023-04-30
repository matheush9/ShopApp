namespace ShopApp.Domain.DTOs.Stock
{
    public class GetStockResponseDto
    {
        public int Id { get; set; } 
        public int AvailableQuantity { get; set; }
        public DateTime Updated { get; set; }

        //Relationships
        public int ProductId { get; set; }
        public int StoreId { get; set; }
    }
}