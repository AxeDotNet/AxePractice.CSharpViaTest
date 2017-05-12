using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharpViaTest.Collections
{
    public class CombineCaseInsensitiveDictionarys
    {
        #region Please modifies the code to pass the test

        static IDictionary<string, ISet<T>> Combine<T>(
            IEnumerable<KeyValuePair<string, ISet<T>>> first, 
            IEnumerable<KeyValuePair<string, ISet<T>>> second)
        {
            return first
                .Concat(second)
                .Aggregate(
                    new Dictionary<string, ISet<T>>(StringComparer.OrdinalIgnoreCase),
                    (aggr, pair) =>
                    {
                        if (aggr.ContainsKey(pair.Key)) { aggr[pair.Key].UnionWith(pair.Value); }
                        else { aggr.Add(pair.Key, pair.Value); }
                        return aggr;
                    });
        }

        #endregion

        [Fact]
        public void should_combine_dictionary_case_insensitively()
        {
            var first = new Dictionary<string, ISet<int>>
            {
                {"Nancy", new HashSet<int> {1, 2}},
                {"nAncy", new HashSet<int> {2, 3, 4}},
                {"Rebecca", new HashSet<int> {1, 2}}
            };

            var second = new Dictionary<string, ISet<int>>
            {
                {"NANCY", new HashSet<int> {1, 8}},
                {"Sofia", new HashSet<int> {2, 9}}
            };

            IDictionary<string, ISet<int>> result = Combine(first, second);
            
            Assert.Equal(3, result.Count);
            Assert.Equal(new [] {1, 2, 3, 4, 8}, result["nancy"].OrderBy(item => item));
            Assert.Equal(new [] {1, 2}, result["rebecca"].OrderBy(item => item));
            Assert.Equal(new [] {2, 9}, result["sofia"].OrderBy(item => item));
        }
    }
}