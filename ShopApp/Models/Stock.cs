using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime Updated { get; set; } 
    }
}
