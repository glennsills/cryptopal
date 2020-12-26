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
    }
}
