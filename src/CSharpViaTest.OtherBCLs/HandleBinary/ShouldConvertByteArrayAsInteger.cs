using System;
using System.Collections.Generic;
using Xunit;

namespace CSharpViaTest.OtherBCLs.HandleBinary
{
    public class ShouldConvertByteArrayAsInteger
    {
        #region Please modifies the code to pass the test

        static int ConvertByteToInteger(byte[] buffer, bool bigEndian = false)
        {
            throw new NotImplementedException();
        }

        #endregion

        static IEnumerable<object[]> GetLittleEndianBytes()
        {
            return new[]
            {
                new object[] {new byte[] {0x01, 0x02, 0x03, 0x04}, 0x04030201 },
                new object[] {new byte[] {0x01, 0x02, 0x03, 0x04, 0x05}, 0x04030201 }
            };
        }

        [Fact]
        public void should_convert_byte_array_as_integer_big_endian()
        {
            byte[] bigEndian = { 0x01, 0x02, 0x03, 0x04 };

            int integer = ConvertByteToInteger(bigEndian, true);

            Assert.Equal(0x01020304, integer);
        }

        [Theory]
        [MemberData(nameof(GetLittleEndianBytes))]
        public void should_convert_byte_array_as_integer(byte[] littleEndian, int expected)
        {
            int integer = ConvertByteToInteger(littleEndian);

            Assert.Equal(expected, integer);
        }

        [Fact]
        public void should_throw_if_input_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => ConvertByteToInteger(null));
        }

        [Fact]
        public void should_throw_if_input_is_less_than_four_bytes()
        {
            byte[] notEnoughData = { 0x01, 0x02, 0x03 };

            Assert.Throws<ArgumentException>(() => ConvertByteToInteger(notEnoughData));
        }
    }
}