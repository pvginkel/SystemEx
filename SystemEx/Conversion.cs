using System;
using System.Collections.Generic;
using System.Text;

namespace SystemEx
{
    public static class Conversion
    {
        public static string ToHex(byte[] value)
        {
            var sb = new StringBuilder(value.Length * 2);

            foreach (byte b in value)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static byte[] FromHex(string value)
        {
            var result = new byte[(value.Length + 1) / 2];

            for (int i = 0; i < result.Length; i++)
            {
                string part = value.Substring(i * 2, Math.Min((i + 1) * 2, value.Length) - i * 2);

                result[i] = Convert.ToByte(part, 16);
            }

            return result;
        }
    }
}
