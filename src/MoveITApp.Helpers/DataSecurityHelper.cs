using System.Text;
using XSystem.Security.Cryptography;

namespace MoveITApp.Helpers
{
    public static class DataSecurityHelper
    {
        /// <summary>
        /// Generates hash from the <paramref name="text"/>
        /// </summary>
        /// <param name="text">Text to be hashed</param>
        public static string GenerateHash(string text)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(text);
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hash = Encoding.ASCII.GetString(hashBytes);

            return hash;
        }
    }
}
