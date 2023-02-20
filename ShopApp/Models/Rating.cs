using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public class Rating : IEntity
    {
        public int Id { get; set; }
        public int StarAmount { get; set; }

    }
}
