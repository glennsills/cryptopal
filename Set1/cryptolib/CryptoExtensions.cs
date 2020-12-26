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

        public static string HexStringXORHexString(this string string1, string string2)
        {
            if (string1.Length != string2.Length)
                throw new ArgumentException("The hex strings should be the same length");

            var byteArray1 = string1.HexStringToByteArray();
            var byteArray2 = string2.HexStringToByteArray();
            var outputArray = new byte[byteArray1.Length];

            for(int i = 0; i < byteArray1.Length; ++i)
            {
                outputArray[i] =(byte)(byteArray1[i] ^ byteArray2[i]);
            }

            return outputArray.ToHexString();
        }

        public static string ToHexString(this byte[] input)
        {
              return BitConverter.ToString(input).Replace("-","").ToLower();
        }

        public static string ToBase64String( this byte[] input)
        {
            return Convert.ToBase64String(input, 0, input.Length);
        }

    }
}
