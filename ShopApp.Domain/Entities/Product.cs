using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Domain.Entities
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string Name { get; set; }    
        public string Description { get; set; }
        public int SoldAmount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //Relationships
        public Store Store { get; set; }
        public int StoreId { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        public List<Image> Images { get; set; } 
        public List<Item> Items { get; set; }
    }
}
