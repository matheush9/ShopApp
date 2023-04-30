using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Domain.Entities
{
    public class Customer : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Relationships
        public List<Order> Orders { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
