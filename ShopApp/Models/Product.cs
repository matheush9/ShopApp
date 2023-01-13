using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }    
        public string Description { get; set; }
        public decimal Price { get; set; }

        [Required]
        public decimal PriceTotal { get; set; }
        public string ImageUrl { get; set; }    

    }
}
