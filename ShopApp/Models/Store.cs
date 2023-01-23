using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ShopApp.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public List<Product> Product { get; set; }
    }
}
