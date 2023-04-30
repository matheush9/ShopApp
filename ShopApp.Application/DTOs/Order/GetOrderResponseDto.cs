namespace ShopApp.Domain.DTOs.Order
{
    public class GetOrderResponseDto
    {
        public int Id { get; set; }

        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public int CustomerId { get; set; }
    }
}
