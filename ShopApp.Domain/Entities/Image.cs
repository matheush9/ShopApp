using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Domain.Entities
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
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
