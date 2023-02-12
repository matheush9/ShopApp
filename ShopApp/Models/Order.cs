using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Order
    {
        static readonly string[] OrderStatuses = {"Created, Processing, Completed"};

        [Key]
        public int Id { get; set; }

        public string Status { get; set; } = OrderStatuses[0];
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        //Relationships
        public List<Item> Items { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
