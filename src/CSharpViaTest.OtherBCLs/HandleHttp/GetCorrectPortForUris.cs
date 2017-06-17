using System;
using Xunit;

namespace CSharpViaTest.OtherBCLs.HandleHttp
{
    /* 
     * Description
     * ===========
     * 
     * This test will extract the port of an URI. 
     * 
     * Difficulty: Super Easy
     * 
     * Knowledge Point
     * ===============
     * 
     * - The usage of Uri class.
     */
    public class GetCorrectPortForUris
    {
        #region Please modifies the code to pass the test

        static int GetPort(string uri)
        {
            throw new NotImplementedException();
        }

        #endregion

        [Theory]
        [InlineData("ftp://ftp.what.a.good.site.com", 21)]
        [InlineData("ftp://ftp.what.a.good.site.com:2121", 2121)]
        [InlineData("http://what.a.good.site.com", 80)]
        [InlineData("http://what.a.good.site.com:8080", 8080)]
        [InlineData("https://what.a.good.site.com", 443)]
        [InlineData("https://what.a.good.site.com:8443", 8443)]
        public void should_get_correct_port_of_uris(string uri, int expectedPort)
        {
            Assert.Equal(expectedPort, GetPort(uri));
        }

        [Fact]
        public void should_throw_for_null_input()
        {
            Assert.Throws<ArgumentNullException>(() => GetPort(null));
        }

        [Fact]
        public void should_throw_for_non_absolute_uri()
        {
            Assert.Throws<ArgumentException>(() => GetPort("relative/uri"));
        }
    }
}