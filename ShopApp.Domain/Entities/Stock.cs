using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Domain.Entities
{
    public class Stock : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int AvailableQuantity { get; set; } = 0;
        public DateTime Updated { get; set; } 

        //Relationships
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Store Store { get; set; }
        public int StoreId { get; set; }

        public Stock(int productId, int storeId)
        {
            ProductId = productId;
            StoreId = storeId;
            Updated = DateTime.Now;
        }
    }
}
