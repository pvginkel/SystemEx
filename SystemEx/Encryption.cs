using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace SystemEx
{
    public static class Encryption
    {
        public static string Sha1Encode(string value)
        {
            return CryptoEncode<SHA1CryptoServiceProvider>(value);
        }

        public static string Md5Encode(string value)
        {
            return CryptoEncode<MD5CryptoServiceProvider>(value);
        }

        private static string CryptoEncode<T>(string value)
            where T : HashAlgorithm, new()
        {
            byte[] bytes = Encoding.UTF8.GetBytes(value == null ? "" : value);

            using (var hashProvider = new T())
            {
                return Conversion.ToHex(hashProvider.ComputeHash(bytes));
            }
        }

        private const string RANDOM_TEXT_SET = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string RandomTextKey(int length)
        {
            var rand = new Random();
            var sb = new StringBuilder();

            for (int i = 0; i < length; i++)
                sb.Append(RANDOM_TEXT_SET[rand.Next(RANDOM_TEXT_SET.Length)]);

            return sb.ToString();
        }
    }
}
