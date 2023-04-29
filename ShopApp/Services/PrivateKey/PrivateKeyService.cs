using System.Security.Cryptography;
using System.Text;

namespace ShopApp.Services.PrivateKeyService
{
    public static class PrivateKeyService
    {
        public readonly static byte[] privateKey = GeneratePrivateKey();

        private static byte[] GeneratePrivateKey()
        {
            var rsa = new RSACryptoServiceProvider(2048);
            return rsa.ExportRSAPrivateKey();
        }
    }
}
