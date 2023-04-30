namespace ShopApp.Domain.DTOs.JwtToken
{
    public class JwtTokenResponseDto
    {
        public string Token { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public DateTime? IssuedAt { get; set; }
    }
}
