using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    public class Item : IEntity
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceTotal { get; set; }

        //Relationships
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
