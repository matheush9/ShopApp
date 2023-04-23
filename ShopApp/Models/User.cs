using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace ShopApp.Models
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

        //

        public void HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = SHA256.HashData(bytes);

            Password = Convert.ToBase64String(hash);
        }
    }
}
