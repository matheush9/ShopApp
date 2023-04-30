using System.Security.Cryptography;
using System.Text;

namespace ShopApp.Application.Services.PrivateKey
{
    public static class PrivateKeyService
    {
        public readonly static byte[] privateKey = GeneratePrivateKey();

        private static byte[] GeneratePrivateKey()
        {
            var rsa = new RSACryptoServiceProvider(2048);
            string teste = "efqew12xo3ij1ik2cn4oçxxxx5xx4x1o24lkfq";
            byte[] bytes = Encoding.ASCII.GetBytes(teste);
            return bytes;
        }
    }
}
