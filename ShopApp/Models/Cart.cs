using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public DateTime Updated { get; set; }

        //Relationships
        public List<Item> Items { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
