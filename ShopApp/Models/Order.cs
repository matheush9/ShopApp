using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }       
        public Store Store { get; set; }    
        public List<Product> Products { get; set; }
        public int OrderId { get; set; }
        public Customer Customer { get; set; }            
    }
}
