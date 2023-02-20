using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Cart : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime Updated { get; } = DateTime.Now;

        //Relationships
        public List<Item> Items { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
