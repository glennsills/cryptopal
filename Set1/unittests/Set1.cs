using cryptolib;
using Xunit;
using FluentAssertions;

namespace unittests
{
    public class Set1
    {
        [Fact]
        public void Challenge1()
        {
            var testString = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            var expectedResult = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

            var result = testString.HexStringToBase64();
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void Challenge2()
        {
            var string1 = "1c0111001f010100061a024b53535009181c";
            var string2 = "686974207468652062756c6c277320657965";    
            var expectedResult = "746865206b696420646f6e277420706c6179";

            var result = string1.HexStringXORHexString(string2);
            result.Should().Be(expectedResult);
        }
    }
}
