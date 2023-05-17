using System.Text;

namespace ShopApp.Application.Services.PrivateKey
{
    public static class PrivateKeyService
    {
        public readonly static byte[] privateKey = GetPrivateKey();

        private static byte[] GetPrivateKey()
        {
            return Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_PRIVATE_KEY"));
        }
    }
}
