using Microsoft.IdentityModel.Tokens;
using ShopApp.Services.PrivateKey;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShopApp.Services.TokenServices
{
    public class TokenService
    {
        public static JwtToken GenerateToken(User user)
        {
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.Id.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(PrivateKeyService.privateKey),SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            var jwtToken = new JwtToken
            {
                Token = tokenHandler.WriteToken(token),
                IssuedAt = DateTime.UtcNow,
                ExpirationTime = tokenConfig.Expires
            };

            return jwtToken;
        }
    }
}
