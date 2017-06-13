using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.OtherBCLs.HandleUris
{
    /* 
     * Description
     * ===========
     * 
     * This test will parse the query string of the uri. 
     * 
     * Difficulty: Super Easy
     * 
     * Knowledge Point
     * ===============
     * 
     * - The usage of Uri class.
     * - The usage of HttpUtility class.
     */
    public class GetQueryParameters
    {
        #region Please modifies the code to pass the test

        class QueryString
        {
            public QueryString(string httpUri)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<string> this[string key]
            {
                get { throw new NotImplementedException(); }
            }
        }

        #endregion

        [Fact]
        public void should_get_query_strings()
        {
            const string uri = "http://www.base.com?key1=value1&key2=value2";

            var queryString = new QueryString(uri);

            Assert.Equal("value1", queryString["key1"].Single());
            Assert.Equal("value2", queryString["key2"].Single());
        }

        [Fact]
        public void should_get_empty_string_if_key_not_exist()
        {
            const string uri = "https://www.base.com?key1=value1&key2=value2";

            var queryString = new QueryString(uri);

            Assert.Equal("value1", queryString["key1"].Single());
            Assert.Equal("value2", queryString["key2"].Single());
            Assert.False(queryString["key3"].Any());
        }

        [Fact]
        public void should_combine_keys()
        {
            const string uri = "https://www.base.com?key1=value1&key1=value2";

            var queryString = new QueryString(uri);

            Assert.Equal(new [] {"key1", "key2"}, queryString["key1"]);
        }

        [Theory]
        [InlineData("ftp://ftp.base.com")]
        [InlineData("mailto:lxconan@gmail.com")]
        [InlineData("/a/relative/uri")]
        public void should_throw_for_non_http_scheme(string nonHttpUri)
        {
            Assert.Throws<ArgumentException>(() => new QueryString(nonHttpUri));
        }

        [Fact]
        public void should_throw_for_null_input()
        {
            Assert.Throws<ArgumentNullException>(() => new QueryString(null));
        }
    }
}