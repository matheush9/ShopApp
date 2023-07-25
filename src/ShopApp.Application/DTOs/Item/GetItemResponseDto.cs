using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Item
{
    public class GetItemResponseDto: BaseOrder
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal PriceTotal { get; set; }
        public int ProductId { get; set; }
    }
}
