using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }  
        public int ProductId { get; set; } 
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
