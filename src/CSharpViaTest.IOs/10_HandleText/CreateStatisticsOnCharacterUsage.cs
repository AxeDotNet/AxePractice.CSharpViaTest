using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace CSharpViaTest.IOs._10_HandleText
{
    /* 
     * Description
     * ===========
     * 
     * This test will show the basic usage of text stream.
     * 
     * Difficulty: Super Easy
     */
    public class CreateStatisticsOnCharacterUsage
    {
        static Stream CreateStringStream(string text)
        {
            var stream = new MemoryStream();
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            stream.Write(textBytes, 0, textBytes.Length);
            stream.Flush();

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        #region Please modifies the code to pass the test

        static Dictionary<char, int> StatCharacterUsage(Stream stream)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        [Fact]
        public void should_create_statistics_on_character_usage()
        {
            Stream textFileStream = CreateStringStream("A quick brown fox jumps over a lazy dog.");
            Dictionary<char, int> histogram = StatCharacterUsage(textFileStream);

            Assert.Equal(3, histogram['a']);
            Assert.Equal(1, histogram['q']);
            Assert.Equal(2, histogram['u']);
            Assert.Equal(1, histogram['i']);
            Assert.Equal(1, histogram['c']);
            Assert.Equal(1, histogram['k']);
            Assert.Equal(1, histogram['b']);
            Assert.Equal(2, histogram['r']);
            Assert.Equal(4, histogram['o']);
            Assert.Equal(1, histogram['w']);
            Assert.Equal(1, histogram['n']);
            Assert.Equal(1, histogram['f']);
            Assert.Equal(1, histogram['x']);
            Assert.Equal(1, histogram['j']);
            Assert.Equal(1, histogram['m']);
            Assert.Equal(1, histogram['p']);
            Assert.Equal(1, histogram['s']);
            Assert.Equal(1, histogram['v']);
            Assert.Equal(1, histogram['e']);
            Assert.Equal(1, histogram['l']);
            Assert.Equal(1, histogram['z']);
            Assert.Equal(1, histogram['y']);
            Assert.Equal(1, histogram['d']);
            Assert.Equal(1, histogram['g']);
        }
    }
}