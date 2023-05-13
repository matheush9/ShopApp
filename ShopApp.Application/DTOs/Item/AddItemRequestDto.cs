using ShopApp.Domain.Common;

namespace ShopApp.Domain.DTOs.Item
{
    public class AddItemRequestDto : BaseOrder
    {
        public int Quantity { get; set; }
        public decimal PriceTotal { get; set; }

        public int ProductId { get; set; }
    }
}
