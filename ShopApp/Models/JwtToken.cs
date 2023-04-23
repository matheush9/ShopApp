using System.Security.Claims;

namespace ShopApp.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public DateTime? IssuedAt { get; set; }
    }
}
