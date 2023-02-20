using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Customer : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //Relationships
        public List<Order> Orders { get; set; }

        public Cart Cart { get; set; }
    }
}
