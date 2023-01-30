using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }   
        
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //Relationships
        public List<Item> Items { get; set; }

        public Store Store { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
