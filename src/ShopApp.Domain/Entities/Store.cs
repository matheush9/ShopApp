using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Domain.Entities
{
    public class Store : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int ProductCatalogCount { get; set; } 

        //Relationships
        public List<Product> Products { get; set; }

        public List<Stock> Stocks { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
