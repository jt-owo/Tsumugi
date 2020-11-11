using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tsumugi.Service
{
    public class Password
    {
        /// <summary>
        /// Hashes a password with the SHA256 algorithm.
        /// </summary>
        /// <param name="password">Password that should be hashed</param>
        /// <returns>Hashed password</returns>
        public static string HashSHA256(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashValue.Length; i++)
                {
                    builder.Append(hashValue[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        /// <summary>
        /// Compares a password and hash value.
        /// </summary>
        /// <param name="password">Password that should be compared.</param>
        /// <param name="hash">Hashed password.</param>
        /// <returns>True if both hash values are equal.</returns>
        public static bool Verify(string password, string hash)
        {
            return hash == HashSHA256(password);
        }
    }
}
