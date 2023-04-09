using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Image: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }

        // Relationships
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
