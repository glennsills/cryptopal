using System;
using cryptolib;
using Xunit;
using FluentAssertions;

namespace unittests
{
    public class CryptoExtensionsTests
    {
        [Theory]
        [InlineData("0a")]
        [InlineData("fa")]
        [InlineData("FA")]
        public void HexStringToByteArrayReturnsData(string value)
        {
            var result = value.HexStringToByteArray();
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void HexStringToByteArrayLongerString()
        {
            var value = "ffdeac0100";
            var result = value.HexStringToByteArray();
            result.Should().HaveCount(5);
        }

        [Fact]
        public void HexStringToByteArrayBadStringFormat()
        {
            var value = "f";
            value.Invoking(y => y.HexStringToByteArray())
            .Should().Throw<FormatException>()
            .WithMessage("An even number of hex digits is required");

        }

        [Fact]
        public void HexStringToByteArrayReturnsCorrectValue()
        {
            var value = "0109a0";
            var result = value.HexStringToByteArray();
            result.Should()
            .HaveCount(3);

            result[0].Should().Be(1);
            result[1].Should().Be(9);
            result[2].Should().Be(0xa0);
        }   

        [Fact]
        public void ByteArrayToHexStringReturnsCorrectValue()
        {
            var sut = new Byte[]{01, 09, 0xa0};
            sut.ToHexString().Should().Be("0109A0");
        }

        [Fact]
        
        public void ByteArrayToHexStringHandlesEmptyStrings()
        {
            var sut = new Byte[]{};
            sut.ToHexString().Should().Be("");
        }

        [Fact]
        public void ConvertToBase64()
        {
            var sut = new byte[] {1,1,1,};
            var result = sut.ToBase64String();
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Set1Challenge1()
        {
            var testString = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            var expectedResult = "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t";

            var result = testString.HexStringToBase64();
            result.Should().Be(expectedResult);
        }
     }
}
