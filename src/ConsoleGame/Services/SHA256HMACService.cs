using System.Security.Cryptography;
using System.Text;

namespace ConsoleGame.Service
{
    public class SHA256HMACService : IHMACGenerator
    {
        public string GenerateKey()
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

        public string GenerateHMAC(string text, string key)
        {
            key = key ?? "";

            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return Convert.ToHexString(hash);
            }

        }
    }

    public interface IHMACGenerator
    {
        string GenerateKey();
        string GenerateHMAC(string text, string key);
    }
}
