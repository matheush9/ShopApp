using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    public class Product : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        //Relationships
        public Store Store { get; set; }
        public int StoreId { get; set; }

        public List<Item> Items { get; set; }
    }
}
