using System.Security.Cryptography;
using System.Text;

namespace ConsoleGame.Services
{
    internal class CryptoService
    {
        public static string GenerateKey()
        {
            const byte bufferSize = 32;
            using (var generator = RandomNumberGenerator.Create())
            {
                var buffer = new byte[bufferSize];
                generator.GetBytes(buffer);
                var key = Convert.ToHexString(buffer);
                return key;
            }
        }

        public static string GenerateHMAC(string text, string key)
        {
            key = key ?? "";

            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return Convert.ToHexString(hash);
            }

        }
    }
}
