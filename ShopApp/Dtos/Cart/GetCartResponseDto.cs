namespace ShopApp.Dtos.Cart
{
    public class GetCartResponseDto
    {
        public int Id { get; set; }

        public DateTime Updated { get; }
        public int CustomerId { get; set; }
    }
}