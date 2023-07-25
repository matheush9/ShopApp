using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Domain.Entities
{
    public class ProductCategory : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Relationships
        public List<Product> Products { get; set; }
    }
}
