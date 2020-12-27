using System;
using System.Collections.Generic;
using System.Text;

namespace cryptolib
{
    public static class CryptoExtensions
    {

        private static readonly Dictionary<char, int> CharScoreDictionary
         = new Dictionary<char, int>()
         { 
             {'e',   1260 },{'t',   937 },{'a',   834 },
             {'o',   770 },{'n',   680 },{'i',   671 },
             {'h',   611 },{'s',   611 },{'r',   568 },
             {'l',   424 },{'d',   414 },{'u',   285 },
             {'c',   273 },{'m',   253 },{'w',   234 },
             {'y',   204 },{'f',   203 },{'g',   192 },
             {'p',   166 },{'b',   154 },{'v',   106 },
             {'k',   087 },{'j',   023 },{'x',   020 },
             {'q',   009 },{'z',   006 },{' ',   020 }
         };

         private static string ExcludedCharacters = "$#<>[]{}+^*&%()~|\n\r";

         public static int Score(this string text)
         {
             var textChars = text.ToLower().ToCharArray();
             var score = 0;
             foreach(char c in textChars)
             {
                 if (ExcludedCharacters.Contains(c))
                    return 0;

                 if (CharScoreDictionary.ContainsKey(c))
                    score += CharScoreDictionary[c];
             }
             return score;
         }
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
            StringBuilder builder = new StringBuilder();
            foreach( var b in input)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        public static string ToBase64String( this byte[] input)
        {
            return Convert.ToBase64String(input, 0, input.Length);
        }

        public static (char XorChar, string Message) XorDecode(this string cypherText )
        {
            var characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ".ToCharArray();
            var highScore = 0;
            var bestString = "";
            char? xorChar = null;

            foreach(var c in characters)
            {
                
                var result = cypherText.XorWithChar(c);

                if (result.score > highScore)
                {
                    highScore = result.score;
                    bestString = result.decodedText;
                    xorChar = c;
                }

            }
            return (xorChar.Value, bestString);
        }

        public static (int score, string decodedText) XorWithChar(this string cypherText, char c)
        {   
            var cypherBytes = cypherText.HexStringToByteArray();
            var outputBytes = new byte[cypherBytes.Length];
            for (int i = 0; i < cypherBytes.Length; ++i)
            {
                outputBytes[i] = (byte)(cypherBytes[i] ^ Convert.ToByte(c));
            }
            var outputString =  System.Text.Encoding.ASCII.GetString(outputBytes);
            var score = outputString.Score();
            return( (score,outputString));
        }

    }
}
