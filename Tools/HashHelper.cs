using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ModefyEcommerce.Helpers
{
    public class HashHelper
    {
        private readonly string _staticSalt;

        public HashHelper(IConfiguration configuration)
        {
            _staticSalt = configuration["Security:StaticSalt"] ?? throw new Exception("StaticSalt not configured");
        }

        /// <summary>
        /// hasher le input avec SHA256
        /// </summary>
        public string ComputeSha256Hash(string input)
        {
            //on rajoute le sel statique pour renforcer la sécurité
            string saltedInput = input + _staticSalt;


            // crée une instance de SHA256 et calcule le hash
            using (SHA256 sha256 = SHA256.Create())
            {
                // convertit l'input en bytes et calcule le hash
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedInput));
                // convertit le tableau de bytes (le hash) en une chaîne hexadécimale
                return Convert.ToHexString(hashBytes); 
            }
        }
    }
}
