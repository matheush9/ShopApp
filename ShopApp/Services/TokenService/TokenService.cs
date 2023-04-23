using Microsoft.IdentityModel.Tokens;
using ShopApp.Services.PrivateKey;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShopApp.Services.TokenService
{
    public class TokenService
    {
        public static string GenerateToken()
        {
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {

                }),

                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(PrivateKeyService.privateKey),SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);

            return tokenHandler.WriteToken(token);
        }
    }
}
