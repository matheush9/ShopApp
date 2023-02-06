using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Dtos.Item
{
    public class GetItemResponseDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal PriceTotal { get; set; }

        public int ProductId { get; set; }
        public int CartId { get; set; }
        public int OrderId { get; set; }
    }
}
