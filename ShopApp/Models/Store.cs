using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Store : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Relationships
        public List<Product> Products { get; set; }

        public List<Stock> Stocks { get; set; }
    }
}
