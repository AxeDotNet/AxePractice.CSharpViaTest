using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.OtherBCLs.HandleHttp
{
    /* 
     * Description
     * ===========
     * 
     * This test will parse the query string of the uri. 
     * 
     * Difficulty: Medium
     * 
     * Knowledge Point
     * ===============
     * 
     * - The usage of Uri class.
     * - The usage of HttpUtility class (.NET Framework) or WebUtility (.NET Core).
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

        [Theory]
        [InlineData("http://www.base.com/path?%e7%9b%b8%e5%85%b3=%e5%bd%93%e7%84%b6%e4%ba%86", "\u76f8\u5173", "\u5f53\u7136\u4e86")]
        public void should_handle_escaping_characters(string uri, string key, string value)
        {
            var queryString = new QueryString(uri);

            Assert.Equal(value, queryString[key].Single());
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