namespace ShopApp.Dtos.Item
{
    public class AddItemRequestDto
    {
        public int Quantity { get; set; }
        public decimal PriceTotal { get; set; }

        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int OrderId { get; set; }
    }
}
