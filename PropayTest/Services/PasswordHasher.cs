using System.Text;
using System.Security.Cryptography;
using System;
namespace PropayTest.Services
{
    public class PasswordHasher
    {
        /// <summary>
        /// Hashes a password using SHA256.
        /// </summary>
        /// <returns>A string representing the hashed password.</returns>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a string representation (hex format).
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }



        }

        /// <summary>
        /// Verifies a password by comparing it to a stored hash.
        /// </summary>
        /// <param name="inputPassword">The plaintext password to verify.</param>
        /// <param name="storedHash">The stored hash to compare against.</param>
        /// <returns>True if the password matches the hash, otherwise false.</returns>
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            // Hash the input password and compare it with the stored hash.
            string inputHash = HashPassword(inputPassword);
            return inputHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
        }
    }
}







