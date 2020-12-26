using System;

namespace cryptolib
{
    public static class CryptoExtensions
    {
        public static byte[] HexStringToByteArray(this string hex)
        {
            
            int NumberChars = hex.Length;
            if (NumberChars%2 != 0)
                throw new FormatException("An even number of hex digits is required");

            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string HexStringToBase64(this string hex)
        {
            var bytes = hex.HexStringToByteArray();
            return bytes.ToBase64String();
        }

        public static string ToHexString(this byte[] input)
        {
              return BitConverter.ToString(input).Replace("-","");
        }

        public static string ToBase64String( this byte[] input)
        {
            return Convert.ToBase64String(input, 0, input.Length);
        }

    }
}
