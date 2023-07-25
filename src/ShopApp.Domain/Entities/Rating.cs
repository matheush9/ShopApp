using ShopApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Domain.Entities
{
    public class Rating : IEntity
    {
        public int Id { get; set; }
        public int StarAmount { get; set; }

    }
}
