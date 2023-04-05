using System;
using System.Security.Cryptography;

namespace LabProject
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private const int SaltSize = 16;
        private const int KeySize = 32; 
        private const int Iterations = 10000; 

        public string EncryptPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = GenerateHash(password, salt, Iterations, KeySize);

            byte[] hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                byte[] hash = GenerateHash(plainTextPassword, salt, Iterations, KeySize);

                for (int i = 0; i < KeySize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private byte[] GenerateHash(string password, byte[] salt, int iterations, int keySize)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(keySize);
            }
        }
    }
}
