using System;
using System.Text;
using System.Security.Cryptography;
namespace AuthAPI.Helpers
{
    public static class CryptoHelpers
    {
        public static string GetSHA(string rnd)
        {
            byte[] data = Encoding.Default.GetBytes(rnd);
            var result = new SHA256Managed().ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
        public static string GetXOR(string a, string b)
        {
            byte[] toBytes1 = Encoding.UTF8.GetBytes(a);
            byte[] toBytes2 = Encoding.UTF8.GetBytes(b);
            byte[] result = new byte [toBytes1.Length];
            for (int i = 0; i < toBytes1.Length; i++)
            {
                result[i] = Convert.ToByte(toBytes1[i] ^ toBytes2[i]);
            }
            string res = BitConverter.ToString(result).ToLower();
            return BitConverter.ToString(result).Replace("-", String.Empty);
        }
       public static string GetRandomSalt(int length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider(); ;
            const string valid = "abcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder res = new StringBuilder();

            byte[] uintBuffer = new byte[4];
            while (length-- > 0)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                res.Append(valid[(int)(num % (uint)valid.Length)]);
            }
            return res.ToString();
        }
    }
}
