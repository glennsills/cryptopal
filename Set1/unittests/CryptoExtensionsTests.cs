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
            sut.ToHexString().Should().Be("0109a0");
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
        public void HexStringsMustBeSameLengthToXor()
        {
            var string1 = "010101";
            var string2 = "01";

             string1.Invoking(y => y.HexStringXORHexString(string2))
             .Should().Throw<ArgumentException>().
             WithMessage("The hex strings should be the same length");
        }

        [Fact]
        public void HexStringXORHexString()
        {
            var string1 = "111111";
            var string2 = "010101";
            
            string1.HexStringXORHexString(string2)
            .Should()
            .NotBeNullOrEmpty();
        }

        [Fact]
        public void ScoreText()
        {
            var text = "xxxx";
            text.Score().Should().Be(80);
        }

     }
}
