using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Domain.Entities
{
    public class User : IEntity
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Relationships

        public Store Store { get; set; }
        public Customer Customer { get; set; }
        public List<Image> Images { get; set; }
    }
}
