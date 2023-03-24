using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagment.Data.HelperClass
{
    public static class HashingPassword
    {
        private static string MD5(this string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] array = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString("x2"));
            }

            return sb.ToString();
        }

        private static string SHA_1(this string password)
        {
            SHA1 sha1Hasher = SHA1.Create();
            byte[] array = sha1Hasher.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static string HashPassword(this string password)
        {
            password = password.SHA_1();
            password = password.MD5();
            password = password.SHA_1();
            password = password.MD5();
            return password;
        }

        public static string CreateNewPassword(int numberOfCharacters)
        {
            var chars = "ABCDEFGHIJKLMNOPRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new String(
                Enumerable.Repeat(chars, numberOfCharacters)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
